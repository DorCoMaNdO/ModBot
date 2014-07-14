using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Concurrent;
using System.Drawing;

namespace ModBot
{
    public static class Irc
    {
        private static iniUtil ini = Program.ini;
        public static TcpClient irc;
        public static StreamReader read;
        public static StreamWriter write;
        public static string nick, password, channel, currency, admin, donationkey = "";
        public static int interval, payout = 0;
        public static int[] intervals = { 1, 2, 3, 4, 5, 6, 10, 12, 15, 20, 30, 60 };
        public static bool bettingOpen, auctionOpen, poolLocked = false;
        public static List<string> IgnoredUsers = new List<string>();
        public static List<string> betOptions = new List<string>();
        public static Timer currencyQueue;
        public static List<string> usersToLookup = new List<string>();
        public static Timer auctionLooper;
        public static string greeting;
        public static bool greetingOn = false;
        public static int g_iLastCurrencyLockAnnounce = 0, g_iLastTop5Announce = 0;
        public static int g_iStreamStartTime = 0;
        public static bool g_bIsStreaming = false;
        public static bool g_bResourceKeeper = false;
        public static MainWindow MainForm = new MainWindow();
        public static int g_iLastHandout = 0;
        public static Dictionary<string, int> ActiveUsers = new Dictionary<string, int>();

        public static void Initialize()
        {
            Console.WriteLine("Initializing connection...");
            ini.SetValue("Settings", "ResourceKeeper", (g_bResourceKeeper = (ini.GetValue("Settings", "ResourceKeeper", "1") == "1")) ? "1" : "0");
            if (donationkey == "")
            {
                MainForm.Donations_ManageButton.Enabled = false;
            }
            Database.Initialize();
            IgnoredUsers.Add("jtv");
            IgnoredUsers.Add("moobot");
            IgnoredUsers.Add("nightbot");
            IgnoredUsers.Add(nick.ToLower());
            IgnoredUsers.Add(admin.ToLower());

            Database.newUser(admin);
            Database.setUserLevel(admin, 4);

            ini.SetValue("Settings", "Channel_Greeting", greeting = ini.GetValue("Settings", "Channel_Greeting", "Hello @user! Welcome to the stream!"));

            Connect();

            RegisterCommands();
        }

        private static void RegisterCommands()
        {
            Commands.Add("!raffle", Command_Giveaway);
            Commands.Add("!giveaway", Command_Giveaway);
            Commands.Add("!" + currency, Command_Currency);
            Commands.Add("!gamble", Command_Gamble);
            Commands.Add("!bet", Command_Bet);
            Commands.Add("!auction", Command_Auction);
            Commands.Add("!bid", Command_Bid);
            Commands.Add("!btag", Command_BTag);
            Commands.Add("!battletag", Command_BTag);
            Commands.Add("!modbot", Command_ModBot);
        }

        private static void Connect()
        {
            int count = 0;
            while (count < 5)
            {
                if (irc != null)
                {
                    //Console.WriteLine("Irc connection already exists. Closing it and opening a new one.");
                    irc.Close();
                }

                irc = new TcpClient();

                count++;
                Console.WriteLine("Connection attempt number : " + count + "/5");

                try
                {
                    irc.Connect("199.9.250.229", 443);

                    Console.WriteLine("Connection successful.\r\n\r\nConfiguring input/output...");

                    read = new StreamReader(irc.GetStream());
                    write = new StreamWriter(irc.GetStream());

                    write.AutoFlush = true;
                    
                    Console.WriteLine("Input/output configured.\r\n\r\nJoining the channel...");

                    sendRaw("PASS " + password);
                    sendRaw("NICK " + nick);
                    sendRaw("USER " + nick + " 8 * :" + nick);
                    sendRaw("JOIN " + channel.ToLower());

                    if (!read.ReadLine().Contains("Login unsuccessful"))
                    {
                        nick = Api.GetDisplayName(nick);
                        Console.WriteLine("Joined the channel.\r\n");
                        sendMessage("ModBot has entered the building.");

                        new Thread(() =>
                        {
                            MainForm.GrabData();
                        }).Start();
                        MainForm.Show();
                        StartThreads();
                    }
                    else
                    {
                        Console.WriteLine("Username and/or password (oauth token) are incorrect!");
                    }

                    break;
                }
                catch (SocketException e)
                {
                    Console.WriteLine("Unable to connect. Retrying in 5 seconds (" + e + ")");
                }
                catch (Exception e)
                {
                    StreamWriter errorLog = new StreamWriter("Error_Log.log", true);
                    errorLog.WriteLine("*************Error Message (via Connect()): " + DateTime.Now + "*********************************\r\n" + e + "\r\n");
                    errorLog.Close();
                }

                //count++;
                // Console.WriteLine("Connection failed. Retrying in 5 seconds.");
                Thread.Sleep(5000);
            }
        }

        private static void StartThreads()
        {
            bool Running = false;
            new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(60000);
                    if (irc.Connected && Running && g_bIsStreaming)
                    {
                        foreach (string user in ActiveUsers.Keys)
                        {
                            Database.addTimeWatched(user, 1);
                        }
                    }
                }
            }).Start();

            new Thread(() =>
            {
                while (true)
                {
                    buildUserList();
                    Thread.Sleep(10000);
                }
            }).Start();

            new Thread(() =>
            {
                while (true)
                {
                    bool bIsStreaming = false;
                    using (WebClient w = new WebClient())
                    {
                        string json_data = "";
                        try
                        {
                            w.Proxy = null;
                            json_data = w.DownloadString("https://api.twitch.tv/kraken/streams/" + channel.Substring(1));
                            JObject stream = JObject.Parse(json_data);
                            if (stream["stream"].HasValues)
                            {
                                if (!g_bIsStreaming)
                                {
                                    g_iStreamStartTime = Api.GetUnixTimeNow();
                                }
                                bIsStreaming = true;
                            }
                        }
                        catch (SocketException)
                        {
                            Console.WriteLine("Unable to connect to twitch API to check stream status.");
                        }
                        catch (IOException)
                        {
                        }
                        catch (Exception e)
                        {
                            StreamWriter errorLog = new StreamWriter("Error_Log.log", true);
                            errorLog.WriteLine("*************Error Message (via checkStream()): " + DateTime.Now + "*********************************\r\n" + e + "\r\n");
                            errorLog.Close();
                        }
                    }
                    g_bIsStreaming = bIsStreaming;
                    if (g_bResourceKeeper)
                    {
                        Thread.Sleep(30000);
                    }
                }
            }).Start();

            //Listen();
            new Thread(() =>
            {
                int attempt = 0;
                while (attempt < 5)
                {
                    attempt++;
                    Console.WriteLine((Running ? "Fix " : "Listening to input ") + "attempt number : " + attempt + "/5");
                    try
                    {
                        while (irc.Connected)
                        {
                            parseMessage(read.ReadLine());
                            if (attempt > 0)
                            {
                                if (!Running)
                                {
                                    Console.WriteLine("Listening to input.\r\n");
                                }
                                else
                                {
                                    Console.WriteLine("The attempt was successful, everything should keep running the way it should.");
                                }
                                attempt = 0;
                                Running = true;
                            }
                        }
                    }
                    catch (IOException)
                    {
                    }
                    catch (Exception e)
                    {
                        if (attempt == 0 || attempt == 5 && !Running)
                        {
                            if (attempt == 0)
                            {
                                Console.WriteLine("Uh oh, there was an error! Attempts to keep everything running are being executed, if the attempts fail or if you keep seeing this message, email your Error_log.log file to DorCoMaNdO@gmail.com with the title \"ModBot - Error\" (Other titles will most likely be ignored).");
                            }
                            else
                            {
                                Console.WriteLine("Failed to listen to input... Please try restarting the bot... If this issue keeps occouring, please email your Error_log.log file to DorCoMaNdO@gmail.com with the title \"ModBot - Error\" (Other titles will most likely be ignored).");
                            }
                            StreamWriter errorLog = new StreamWriter("Error_Log.log", true);
                            errorLog.WriteLine("*************Error Message (via Listen()): " + DateTime.Now + "*********************************\r\n" + e + "\r\n");
                            errorLog.Close();
                        }
                    }
                    Thread.Sleep(500);
                }
                Running = false;
                MainForm.Hide();
                System.Windows.Forms.MessageBox.Show("ModBot has encountered an error, more information available in the console...", "ModBot", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                Console.WriteLine("The attempts were unsuccessful... In order get ModBot back to function please restart it...");
            }).Start();

            //doWork();
            new Thread(() =>
            {
                g_iLastHandout = Api.GetUnixTimeNow();
                while (true)
                {
                    if (Running)
                    {
                        if (Api.GetUnixTimeNow() - g_iLastHandout >= interval * 60 && g_bIsStreaming)
                        {
                            Console.WriteLine("Handout happening now! Paying everyone " + payout + " " + currency);
                            handoutCurrency();
                        }
                    }
                    Thread.Sleep(1000);
                    /*if (g_bResourceKeeper)
                    {
                        Thread.Sleep(29000);
                    }*/
                }
            }).Start();

            //KeepAlive();
            new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(30000);
                    sendRaw("PING 1245");
                }
            }).Start();

            currencyQueue = new Timer(handleCurrencyQueue, null, Timeout.Infinite, Timeout.Infinite);

            auctionLooper = new Timer(auctionLoop, null, Timeout.Infinite, Timeout.Infinite);
        }

        private static void parseMessage(string message)
        {
            //Console.WriteLine(message);
            string[] msg = message.Split(' ');
            string user = "";

            if (msg[0].Equals("PING"))
            {
                sendRaw("PONG " + msg[1]);
                //Console.WriteLine("PONG " + msg[1]);
            }
            else if (msg[1].Equals("PRIVMSG"))
            {
                user = getUser(message);
                addUserToList(user);
                //Console.WriteLine(message);
                string temp = message.Substring(message.IndexOf(":", 1) + 1);
                string name = Api.GetDisplayName(user);
                Console.WriteLine(name + ": " + temp);
                //handleMessage(temp);
                Commands.CheckCommand(user, temp, true);
                if (user.Equals(Api.capName(MainForm.Giveaway_WinnerLabel.Text)))
                {
                    MainForm.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate
                    {
                        MainForm.Giveaway_WinnerChat.SelectionColor = Color.Blue;
                        MainForm.Giveaway_WinnerChat.SelectionFont = new Font("Segoe Print", 7, FontStyle.Bold);
                        MainForm.Giveaway_WinnerChat.SelectedText = name;
                        MainForm.Giveaway_WinnerChat.SelectionColor = Color.Black;
                        MainForm.Giveaway_WinnerChat.SelectionFont = new Font("Microsoft Sans Serif", 8);
                        MainForm.Giveaway_WinnerChat.SelectedText = ": " + temp + "\r\n";
                        MainForm.Giveaway_WinnerChat.Select(MainForm.Giveaway_WinnerChat.Text.Length, MainForm.Giveaway_WinnerChat.Text.Length);
                        MainForm.Giveaway_WinnerChat.ScrollToCaret();
                        MainForm.Giveaway_WinnerTimerLabel.ForeColor = Color.FromArgb(0, 200, 0);
                    });
                }
            }
            else if (msg[1].Equals("JOIN"))
            {
                user = getUser(message);
                if (user == Api.capName(nick)) return;
                addUserToList(user);
                if (!Database.userExists(user))
                {
                    Database.newUser(user);
                    //db.addCurrency(user, payout);
                }
                string name = Api.GetDisplayName(user);
                Console.WriteLine(name + " joined");
                if (greetingOn && greeting != "")
                {
                    sendMessage(greeting.Replace("@user", name));
                }
                if (user.Equals(Api.capName(MainForm.Giveaway_WinnerLabel.Text)))
                {
                    MainForm.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate
                    {
                        MainForm.Giveaway_WinnerChat.SelectionColor = Color.Green;
                        MainForm.Giveaway_WinnerChat.SelectionFont = new Font("Segoe Print", 7, FontStyle.Bold);
                        MainForm.Giveaway_WinnerChat.SelectedText = name + " has joined the channel.\r\n";
                    });
                }
            }
            else if (msg[1].Equals("PART"))
            {
                user = getUser(message);
                removeUserFromList(user);
                string name = Api.GetDisplayName(user);
                Console.WriteLine(name + " left");
                if (user.Equals(Api.capName(MainForm.Giveaway_WinnerLabel.Text)))
                {
                    MainForm.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate
                    {
                        MainForm.Giveaway_WinnerTimerLabel.Text = "The winner left!";
                        MainForm.Giveaway_WinnerTimerLabel.ForeColor = Color.FromArgb(255, 0, 0);

                        MainForm.Giveaway_WinnerChat.SelectionColor = Color.Red;
                        MainForm.Giveaway_WinnerChat.SelectionFont = new Font("Segoe Print", 7, FontStyle.Bold);
                        MainForm.Giveaway_WinnerChat.SelectedText = name + " has left the channel.\r\n";
                    });
                }
            }
            else if (msg[1].Equals("352"))
            {
                //Console.WriteLine(message);
                addUserToList(msg[4]);
            }
            /*else
            {
                //Console.WriteLine(message);
            }*/
        }

        private static void Command_Giveaway(string user, string cmd, string[] args)
        {
            if (args != null && args.Length >= 1)
            {
                //ADMIN GIVEAWAY COMMANDS: !giveaway open <TicketCost> <MaxTickets>, !giveaway close, !giveaway draw, !giveaway cancel//
                if (Database.getUserLevel(user) >= 1)
                {
                    if (args[0].Equals("announce"))
                    {
                        string sMessage = "Get the party started! Viewers active in chat within the last " + Convert.ToInt32(MainForm.Giveaway_ActiveUserTime.Value) + " minutes ";
                        if (MainForm.Giveaway_MustFollowCheckBox.Checked)
                        {
                            if (!MainForm.Giveaway_MinCurrencyCheckBox.Checked)
                            {
                                sMessage = sMessage + "and follow the stream ";
                            }
                            else
                            {
                                sMessage = sMessage + "follow the stream, and have " + MainForm.Giveaway_MinCurrency.Value + " " + currency + " ";
                            }
                        }
                        else
                        {
                            sMessage = sMessage + "and have " + MainForm.Giveaway_MinCurrency.Value + " " + currency + " ";
                        }
                        sMessage = sMessage + "will qualify for the giveaway!";
                        sendMessage(sMessage);
                    }
                    if (Database.getUserLevel(user) >= 2)
                    {
                        if (args[0].Equals("roll"))
                        {
                            string winner = Giveaway.getWinner();
                            if (winner.Equals(""))
                            {
                                sendMessage("No valid winner found, please try again!");
                            }
                            else
                            {
                                TimeSpan t = Database.getTimeWatched(winner);
                                sendMessage(winner + " has won the giveaway! (" + (Api.IsFollowingChannel(winner) ? "Currently follows the channel | " : "") + "Has " + Database.checkCurrency(winner) + " " + currency + " | Has watched the stream for " + t.Days + " days, " + t.Hours + " hours and " + t.Minutes + " minutes | Chance : " + Giveaway.getLastRollWinChance().ToString("0.00") + "%)");
                            }
                        }
                    }
                }

                //REGULAR USER COMMANDS: !giveaway help
                if (args[0].Equals("help"))
                {
                    string sMessage = "In order to join the giveaway, you have to be active in chat ";
                    if (MainForm.Giveaway_MustFollowCheckBox.Checked)
                    {
                        if (!MainForm.Giveaway_MinCurrencyCheckBox.Checked)
                        {
                            sMessage = sMessage + "and follow the stream, ";
                        }
                        else
                        {
                            sMessage = sMessage + ", follow the stream and have " + MainForm.Giveaway_MinCurrency.Value + " " + currency + ", ";
                        }
                    }
                    else
                    {
                        sMessage = sMessage + "and have " + MainForm.Giveaway_MinCurrency.Value + " " + currency + ", ";
                    }
                    sMessage = sMessage + "the winner is selected from a list of viewers that were active in the last " + Convert.ToInt32(MainForm.Giveaway_ActiveUserTime.Value) + " minutes";
                    if (MainForm.Giveaway_MustFollowCheckBox.Checked || MainForm.Giveaway_MinCurrencyCheckBox.Checked) sMessage = sMessage + " and comply the terms";
                    sMessage = sMessage + ".";
                    sendMessage(sMessage);
                }
            }
        }

        private static void Command_Currency(string user, string cmd, string[] args)
        {
            if (args != null)
            {
                if (args.Length == 1)
                {
                    if (args[0].Equals("top5"))
                    {
                        if (!MainForm.Misc_LockCurrencyCmdCheckBox.Checked && Api.GetUnixTimeNow() - g_iLastTop5Announce > 600 || Database.getUserLevel(user) >= 1)
                        {
                            g_iLastTop5Announce = Api.GetUnixTimeNow();
                            Dictionary<string, int> TopPoints = new Dictionary<string, int>();
                            foreach (string usr in Database.GetAllUsers())
                            {
                                if (!IgnoredUsers.Any(c => c.Equals(usr.ToLower())))
                                {
                                    TopPoints.Add(usr, Database.checkCurrency(usr));
                                }
                            }
                            IOrderedEnumerable<KeyValuePair<string, int>> top = TopPoints.OrderByDescending(key => key.Value);
                            if(TopPoints.Count > 0)
                            {
                                string output = "";
                                int max = 5;
                                if (TopPoints.Count < max)
                                {
                                    max = TopPoints.Count;
                                }
                                for(int i = 0; i < max; i++)
                                {
                                    output += Api.GetDisplayName(top.ElementAt(i).Key) + " (" + Database.getTimeWatched(top.ElementAt(i).Key).ToString(@"d\d\ hh\h\ mm\m") + ") - " + top.ElementAt(i).Value + ", ";
                                }
                                sendMessage("The " + max + " users with the most points are: " + output.Substring(0, output.Length - 2) + ".");
                            }
                            else
                            {
                                sendMessage("An error has occoured while looking for the 5 users with the most points! Try again later.");
                            }
                        }
                    }
                    else if (args[0].Equals("lock") && Database.getUserLevel(user) >= 2)
                    {
                        MainForm.Misc_LockCurrencyCmdCheckBox.Checked = true;
                        sendMessage("The !" + currency + " command is temporarily disabled.", user + " locked the currency command.");
                    }
                    else if (args[0].Equals("unlock") && Database.getUserLevel(user) >= 2)
                    {
                        MainForm.Misc_LockCurrencyCmdCheckBox.Checked = false;
                        sendMessage("The !" + currency + " command is now available to use.", user + " unlocked the currency command.");
                    }
                    else if (args[0].Equals("clear") && Database.getUserLevel(user) >= 3)
                    {
                        foreach (string usr in Database.GetAllUsers())
                        {
                            Database.setCurrency(usr, 0);
                        }
                        sendMessage("Cleared all the " + currency + "!", user + " cleared all the " + currency + "!");
                    }
                    else
                    {
                        if (Database.getUserLevel(user) >= 1)
                        {
                            if (!args[0].Contains(","))
                            {
                                if (Database.userExists(args[0]))
                                {
                                    sendMessage("Mod check: " + Api.GetDisplayName(args[0], true) + " (" + Database.getTimeWatched(args[0]).ToString(@"d\d\ hh\h\ mm\m") + ")" + " has " + Database.checkCurrency(args[0]) + " " + currency);
                                }
                                else
                                {
                                    sendMessage("Mod check: " + Api.GetDisplayName(args[0]) + " is not a valid user.");
                                }
                            }
                            else
                            {
                                foreach (string usr in args[0].Split(','))
                                {
                                    addToLookups(usr);
                                }
                            }
                        }
                    }
                }
                else if (args.Length >= 2 && Database.getUserLevel(user) >= 3)
                {
                    /////////////MOD ADD CURRENCY//////////////
                    if (args[0].Equals("add"))
                    {
                        int amount;
                        if (int.TryParse(args[1], out amount) && args.Length >= 3)
                        {
                            if (args[2].Equals("all"))
                            {
                                foreach (string usr in Database.GetAllUsers())
                                {
                                    Database.addCurrency(usr, amount);
                                }
                                sendMessage("Added " + amount + " " + currency + " to everyone.", user + " added " + amount + " " + currency + " to everyone.");
                            }
                            else if (args[2].Equals("online"))
                            {
                                foreach (string usr in ActiveUsers.Keys)
                                {
                                    Database.addCurrency(usr, amount);
                                }
                                sendMessage("Added " + amount + " " + currency + " to online users.", user + " added " + amount + " " + currency + " to online users.");
                            }
                            else
                            {
                                Database.addCurrency(args[2], amount);
                                sendMessage("Added " + amount + " " + currency + " to " + Api.GetDisplayName(args[2]), user + " added " + amount + " " + currency + " to " + Api.GetDisplayName(args[2]));
                            }
                        }
                    }
                    else if (args[0].Equals("set"))
                    {
                        int amount;
                        if (int.TryParse(args[1], out amount) && args.Length >= 3)
                        {
                            if (args[2].Equals("all"))
                            {
                                foreach (string usr in Database.GetAllUsers())
                                {
                                    Database.setCurrency(usr, amount);
                                }
                                sendMessage("Set everyone's " + currency + " to " + amount + ".", user + " set everyone's " + currency + " to " + amount + ".");
                            }
                            else if (args[2].Equals("online"))
                            {
                                foreach (string usr in ActiveUsers.Keys)
                                {
                                    Database.setCurrency(usr, amount);
                                }
                                sendMessage("Set online users's " + currency + " to " + amount + ".", user + " set online users's " + currency + " to " + amount + ".");
                            }
                            else
                            {
                                Database.setCurrency(args[2], amount);
                                sendMessage("Set " + Api.capName(args[2]) + "'s " + currency + " to " + amount + ".", user + " set " + Api.capName(args[2]) + "'s " + currency + " to " + amount + ".");
                            }
                        }
                    }

                    ////////////MOD REMOVE CURRENCY////////////////
                    else if (args[0].Equals("remove"))
                    {
                        int amount;
                        if (args[1] != null && int.TryParse(args[1], out amount) && args.Length >= 3)
                        {

                            if (args[2].Equals("all"))
                            {
                                foreach (string usr in Database.GetAllUsers())
                                {
                                    Database.removeCurrency(usr, amount);
                                }
                                sendMessage("Removed " + amount + " " + currency + " from everyone.", user + " removed " + amount + " " + currency + " from everyone.");
                            }
                            else if (args[2].Equals("online"))
                            {
                                foreach (string usr in ActiveUsers.Keys)
                                {
                                    Database.removeCurrency(usr, amount);
                                }
                                sendMessage("Removed " + amount + " " + currency + " from online users.", user + " removed " + amount + " " + currency + " from online users.");
                            }
                            else
                            {
                                Database.removeCurrency(args[2], amount);
                                sendMessage("Removed " + amount + " " + currency + " from " + Api.capName(args[2]), user + " removed " + amount + " " + currency + " from " + Api.capName(args[2]));
                            }

                        }
                    }
                }
            }
            else
            {
                if (MainForm.Misc_LockCurrencyCmdCheckBox.Checked && Database.getUserLevel(user) == 0 && Api.GetUnixTimeNow() - g_iLastCurrencyLockAnnounce > 600)
                {
                    g_iLastCurrencyLockAnnounce = Api.GetUnixTimeNow();
                    sendMessage("The !" + currency + " command is disabled, you may politely ask a mod to check your " + currency + " for you.");
                }
                if (!MainForm.Misc_LockCurrencyCmdCheckBox.Checked || Database.getUserLevel(user) >= 1)
                {
                    addToLookups(user);
                }
            }
        }

        private static void Command_Gamble(string user, string cmd, string[] args)
        {
            if (args != null && Database.getUserLevel(user) >= 2)
            {
                if (args[0].Equals("open") && args.Length >= 4)
                {
                    if (!bettingOpen)
                    {
                        int maxBet;
                        if (int.TryParse(args[1], out maxBet))
                        {
                            if (maxBet > 0)
                            {
                                buildBetOptions(args);
                                if (betOptions.Count > 1)
                                {
                                    Pool.CreatePool(maxBet, betOptions);
                                    bettingOpen = true;
                                    sendMessage("New Betting Pool opened!  Max bet = " + maxBet + " " + currency);
                                    string temp = "Betting open for: ";
                                    for (int i = 0; i < betOptions.Count; i++)
                                    {
                                        temp += "(" + (i + 1).ToString() + ") " + betOptions[i] + " ";
                                    }
                                    sendMessage(temp);
                                    sendMessage("Bet by typing \"!bet 50 option1name\" to bet 50 " + currency + " on option 1, \"!bet 25 option2name\" to bet 25 on option 2, etc. You can also bet with \"!bet 10 #OptionNumber\"");
                                }
                                else
                                {
                                    sendMessage("You need at least two betting options in order to start a betting pool!");
                                }
                            }
                            else
                            {
                                sendMessage("Max bet can not be lower than 1!");
                            }
                        }
                        else
                        {
                            sendMessage("Invalid syntax. Open a betting pool with: !gamble open <maxBet> <option1>, <option2>, .... <optionN> (comma delimited options)");
                        }
                    }
                    else
                    {
                        sendMessage("Betting Pool already opened. Close or cancel the current one before starting a new one.");
                    }
                }
                else if (args[0].Equals("close"))
                {
                    if (bettingOpen)
                    {
                        if (!poolLocked)
                        {
                            poolLocked = true;
                            sendMessage("Bets locked in. Good luck everyone!");
                        }
                        else
                        {
                            sendMessage("Pool is already locked.");
                        }
                    }
                    else
                    {
                        sendMessage("No pool currently open.");
                    }
                }
                else if (args[0].Equals("winner") && args.Length >= 2)
                {
                    if (bettingOpen && poolLocked)
                    {
                        bool inQuote = false;
                        string option = "";
                        for (int i = 1; i < args.Length; i++)
                        {
                            if (args[i].StartsWith("\""))
                            {
                                inQuote = true;
                            }
                            if (!inQuote)
                            {
                                option = args[i];
                            }
                            if (inQuote)
                            {
                                option += args[i] + " ";
                            }
                            if (args[i].EndsWith("\""))
                            {
                                option = option.Substring(1, option.Length - 3);
                                inQuote = false;
                            }
                        }
                        if (option == args[1])
                        {
                            if (option.StartsWith("#"))
                            {
                                int optionnumber = Convert.ToInt32(option.Substring(1));
                                if (("#" + optionnumber) == option)
                                {
                                    option = Pool.GetOptionFromNumber(optionnumber);
                                }
                            }
                        }
                        if (betOptions.Contains(option))
                        {
                            Pool.closePool(option);
                            bettingOpen = false;
                            poolLocked = false;
                            sendMessage("Betting Pool closed! A total of " + Pool.getTotalBets() + " " + currency + " were bet.");
                            string output = "Bets for:";
                            for (int i = 0; i < betOptions.Count; i++)
                            {
                                double x = ((double)Pool.getTotalBetsOn(betOptions[i]) / Pool.getTotalBets()) * 100;
                                output += " " + betOptions[i] + " - " + Pool.getNumberOfBets(betOptions[i]) + " (" + Math.Round(x) + "%);";
                                //Console.WriteLine("TESTING: getTotalBetsOn(" + i + ") = " + pool.getTotalBetsOn(i) + " --- getTotalBets() = " + pool.getTotalBets() + " ---  (double)betsOn(i)/totalBets() = " + (double)(pool.getTotalBetsOn(i) / pool.getTotalBets()) + " --- *100 = " + (double)(pool.getTotalBetsOn(i) / pool.getTotalBets()) * 100 + " --- Converted to a double = " + (double)((pool.getTotalBetsOn(i) / pool.getTotalBets()) * 100) + " --- Rounded double = " + Math.Round((double)((pool.getTotalBetsOn(i) / pool.getTotalBets()) * 100)));
                            }
                            sendMessage(output);
                            Dictionary<string, int> winners = Pool.getWinners();
                            output = "Winners:";
                            if (winners.Count == 0)
                            {
                                sendMessage(output + " No One!");
                            }
                            for (int i = 0; i < winners.Count; i++)
                            {
                                output += " " + winners.ElementAt(i).Key + " - " + winners.ElementAt(i).Value + " (Bet " + Pool.getBetAmount(winners.ElementAt(i).Key) + ")";
                                if (i == 0 && i == winners.Count - 1)
                                {
                                    sendMessage(output);
                                    output = "";
                                }
                                else if ((i != 0 && i % 10 == 0) || i == winners.Count - 1)
                                {
                                    sendMessage(output);
                                    output = "";
                                }
                            }
                        }
                        else
                        {
                            sendMessage("The option you specified is not available in the current pool!");
                        }
                    }
                    else
                    {
                        sendMessage("Betting pool must be open and bets must be locked before you can specify a winner, lock the bets by using \"!gamble close\".");
                        sendMessage("Close the betting pool by typing \"!gamble winner option1name\" if option 1 won, \"!gamble winner option2name\" for option 2, etc.");
                        sendMessage("You can type \"!bet help\" to get a list of the options as a reminder.");
                    }
                }
                else if (args[0].Equals("cancel"))
                {
                    if (Pool.Running)
                    {
                        Pool.cancel();
                        bettingOpen = false;
                        poolLocked = false;
                        sendMessage("Betting Pool canceled. All bets refunded");
                    }
                }
            }
        }

        private static void Command_Bet(string user, string cmd, string[] args)
        {
            if (bettingOpen)
            {
                if (args != null)
                {
                    int betAmount;
                    if (args[0].Equals("help"))
                    {
                        if (bettingOpen)
                        {
                            string temp = "Betting open for: ";
                            for (int i = 0; i < betOptions.Count; i++)
                            {
                                temp += "(" + (i + 1).ToString() + ") " + betOptions[i] + " ";
                            }
                            sendMessage(temp);
                            sendMessage("Bet by typing \"!bet 50 option1name\" to bet 50 " + currency + " on option 1, \"bet 25 option2name\" to bet 25 on option 2, etc. You can also bet with \"!bet 10 #OptionNumber\".");
                        }
                    }
                    else if (int.TryParse(args[0], out betAmount) && args.Length >= 2 && bettingOpen && !poolLocked)
                    {
                        bool inQuote = false;
                        string option = "";
                        for (int i = 1; i < args.Length; i++)
                        {
                            if (args[i].StartsWith("\""))
                            {
                                inQuote = true;
                            }
                            if (!inQuote)
                            {
                                option = args[i];
                            }
                            if (inQuote)
                            {
                                option += args[i] + " ";
                            }
                            if (args[i].EndsWith("\""))
                            {
                                option = option.Substring(1, option.Length - 3);
                                inQuote = false;
                            }
                        }
                        if(option == args[1])
                        {
                            if(option.StartsWith("#"))
                            {
                                int optionnumber = Convert.ToInt32(option.Substring(1));
                                if(("#" + optionnumber) == option)
                                {
                                    option = Pool.GetOptionFromNumber(optionnumber);
                                    if(option == "")
                                    {
                                        sendMessage(Api.GetDisplayName(user) + " the option number does not exist");
                                        return;
                                    }
                                }
                            }
                        }
                        if (Pool.placeBet(user, option, betAmount))
                        {
                            sendMessage(Api.GetDisplayName(user) + " you have placed a " + betAmount + " " + currency + " bet on \"" + option + "\"");
                        }
                    }
                }
                else
                {
                    if (Pool.isInPool(user))
                    {
                        sendMessage(Api.GetDisplayName(user) + ": " + Pool.getBetOn(user) + " (" + Pool.getBetAmount(user) + ")");
                    }
                }
            }
        }

        private static void Command_Auction(string user, string cmd, string[] args)
        {
            if (args != null && Database.getUserLevel(user) >= 2)
            {
                if (args[0].Equals("open"))
                {
                    if (!auctionOpen)
                    {
                        Auction.Start();
                        auctionOpen = true;
                        sendMessage("Auction open!  Bid by typing \"!bid 50\", etc.");
                    }
                    else sendMessage("Auction already open. Close or cancel the previous one first.");
                }
                else if (args[0].Equals("close"))
                {
                    if (auctionOpen)
                    {
                        auctionOpen = false;
                        sendMessage("Auction closed!  Winner is: " + checkBtag(Auction.highBidder) + " (" + Auction.highBid + ")");
                    }
                    else sendMessage("No auction open.");
                }
                else if (args[0].Equals("cancel"))
                {
                    if (auctionOpen)
                    {
                        auctionOpen = false;
                        Auction.Cancel();
                        sendMessage("Auction cancelled. Bids refunded.");
                    }
                    else sendMessage("No auction open.");
                }
            }
        }

        private static void Command_Bid(string user, string cmd, string[] args)
        {
            if (args != null)
            {
                int amount;
                if (int.TryParse(args[0], out amount))
                {
                    if (auctionOpen)
                    {
                        if (Auction.placeBid(user, amount))
                        {
                            auctionLooper.Change(0, 30000);
                        }
                    }
                }
            }
        }

        private static void Command_BTag(string user, string cmd, string[] args)
        {
            if (args != null && args[1].Contains("#"))
            {
                Database.setBtag(user, args[1]);
            }
        }

        private static void Command_ModBot(string user, string cmd, string[] args)
        {
            if (args != null)
            {
                if (Database.getUserLevel(user) >= 4)
                {
                    if (args[0].Equals("payout") && args.Length >= 2)
                    {
                        int amount = 0;
                        if (int.TryParse(args[1], out amount) && amount > 0)
                        {
                            payout = amount;
                            sendMessage("New Payout Amount: " + amount);
                        }
                        else sendMessage("Can't change payout amount. Must be a valid integer greater than 0");
                    }
                    if (args[0].Equals("interval") && args.Length >= 2)
                    {
                        int tempInterval = -1;
                        if (int.TryParse(args[1], out tempInterval) && Array.IndexOf(intervals, tempInterval) > -1)
                        {
                            interval = tempInterval;
                            sendMessage("New Payout Interval: " + tempInterval);
                        }
                        else
                        {
                            //Console.WriteLine(tempInterval + " " + Array.IndexOf(intervals, tempInterval));
                            string output = "Can't change payout interval. Accepted values: ";
                            bool addComma = false;
                            foreach (int x in intervals)
                            {
                                if (addComma)
                                {
                                    output += ", ";
                                }
                                output += x;
                                addComma = true;
                            }
                            sendMessage(output + " minutes.");
                        }
                    }
                    if (args[0].Equals("greeting") && args.Length >= 2)
                    {
                        //Console.WriteLine(Database.getUserLevel(user) + "test 1");
                        if (args[1].Equals("on"))
                        {
                            greetingOn = true;
                            sendMessage("Greetings turned on.");
                        }
                        if (args[1].Equals("off"))
                        {
                            greetingOn = false;
                            sendMessage("Greetings turned off.");
                        }
                        if (args[1].Equals("set") && args.Length >= 3)
                        {
                            string sGreeting = "";
                            for (int i = 2; i < args.Length; i++)
                            {
                                sGreeting += args[i] + " ";
                            }
                            greeting = sGreeting.Substring(0, sGreeting.Length - 1);
                            ini.SetValue("Settings", "Channel_Greeting", greeting);
                            sendMessage("Your new greeting is: " + greeting);
                        }
                    }
                    if (args[0].Equals("addsub") && args.Length >= 2)
                    {
                        if (Database.addSub(args[1]))
                        {
                            sendMessage(Api.GetDisplayName(args[1]) + " added as a subscriber.");
                        }
                        else
                        {
                            sendMessage(Api.GetDisplayName(args[1]) + " does not exist in the database. Have them type !<currency> then try again.");
                        }
                    }
                    if (args[0].Equals("removesub") && args.Length >= 2)
                    {
                        if (Database.removeSub(args[1]))
                        {
                            sendMessage(Api.GetDisplayName(args[1]) + " removed from subscribers.");
                        }
                        else
                        {
                            sendMessage(Api.GetDisplayName(args[1]) + " does not exist in the database.");
                        }
                    }
                }
                if (Database.getUserLevel(user) >= 3)
                {
                    if (args[0].Equals("addmod") && args.Length >= 2)
                    {
                        string tNick = Api.GetDisplayName(args[1]);
                        if (Database.userExists(tNick))
                        {
                            if (!tNick.Equals(admin, StringComparison.OrdinalIgnoreCase) && (Database.getUserLevel(tNick) < 3 && Database.getUserLevel(user) == 3 || Database.getUserLevel(user) >= 4))
                            {
                                Database.setUserLevel(tNick, 1);
                                sendMessage(tNick + " added as a bot moderator.", Api.GetDisplayName(user) + " added " + tNick + "as a bot moderator.");
                            }
                            else
                            {
                                sendMessage("Cannot change broadcaster access level.");
                            }
                        }
                        else
                        {
                            sendMessage(tNick + " does not exist in the database. Have them type !<currency>, then try to add them again.");
                        }
                    }
                    if (args[0].Equals("addsuper") && args.Length >= 2)
                    {
                        string tNick = Api.GetDisplayName(args[1]);
                        if (Database.userExists(tNick))
                        {
                            if (!tNick.Equals(admin, StringComparison.OrdinalIgnoreCase) && (Database.getUserLevel(tNick) < 3 && Database.getUserLevel(user) == 3 || Database.getUserLevel(user) >= 4))
                            {
                                Database.setUserLevel(tNick, 2);
                                sendMessage(tNick + " added as a bot Super Mod.", Api.GetDisplayName(user) + " added " + tNick + "as a super bot moderator.");
                            }
                            else
                            {
                                sendMessage("Cannot change Broadcaster access level.");
                            }
                        }
                        else
                        {
                            sendMessage(tNick + " does not exist in the database. Have them type !<currency>, then try to add them again.");
                        }
                    }
                    if (args[0].Equals("demote") && args.Length >= 2)
                    {
                        string tNick = Api.GetDisplayName(args[1]);
                        if (Database.userExists(tNick))
                        {
                            if (Database.getUserLevel(tNick) > 0)
                            {
                                if (!tNick.Equals(admin, StringComparison.OrdinalIgnoreCase) && (Database.getUserLevel(tNick) < 3 && Database.getUserLevel(user) == 3 || Database.getUserLevel(user) >= 4))
                                {
                                    Database.setUserLevel(tNick, Database.getUserLevel(tNick) - 1);
                                    sendMessage(tNick + " has been demoted.", Api.GetDisplayName(user) + "demoted " + tNick);
                                }
                                else
                                {
                                    sendMessage("Cannot change Broadcaster access level.");
                                }
                            }
                            else
                            {
                                sendMessage("User is already Access Level 0. Cannot demote further.");
                            }
                        }
                        else
                        {
                            sendMessage(tNick + " does not exist in the database. Have them type !<currency>, then try again.");
                        }
                    }
                    if (args[0].Equals("setlevel") && args.Length >= 3)
                    {
                        string tNick = Api.GetDisplayName(args[1]);
                        if (Database.userExists(tNick))
                        {
                            if (!tNick.Equals(admin, StringComparison.OrdinalIgnoreCase) && (Database.getUserLevel(tNick) < 3 && Database.getUserLevel(user) == 3 || Database.getUserLevel(user) >= 4))
                            {
                                int level;
                                if (int.TryParse(args[2], out level) && level >= 0 && (level < 4 && Database.getUserLevel(user) >= 4 || level < 3))
                                {
                                    Database.setUserLevel(tNick, level);
                                    sendMessage(tNick + " set to Access Level " + level, Api.GetDisplayName(user) + "set " + tNick + "'s Access Level to " + level);
                                }
                                else sendMessage("Level must be greater than or equal to 0, and less than 3 (0>=Level<3)");
                            }
                            else sendMessage("Cannot change that mod's access level.");
                        }
                        else
                        {
                            sendMessage(tNick + " does not exist in the database. Have them type !currency, then try again.");
                        }
                    }
                }
                if (Database.getUserLevel(user) >= 2)
                {
                    if (args[0].Equals("addcommand") && args.Length >= 4)
                    {
                        int level;
                        if (int.TryParse(args[1], out level) && level >= 0 && level <= 4)
                        {
                            string command = args[2].ToLower();
                            if (!Commands.CheckCommand(command))
                            {
                                string output = "";
                                for (int i = 3; i < args.Length; i++)
                                {
                                    output += args[i] + " ";
                                }
                                Commands.addCommand(command, level, output.Substring(0, output.Length - 1));
                                sendMessage(command + " command added.", Api.GetDisplayName(user) + " added the command " + command);
                            }
                            else
                            {
                                sendMessage(command + " is already a command.");
                            }
                        }
                        else 
                        {
                            sendMessage("Invalid syntax. Correct syntax is \"!modbot addcommand <access level> <command> <text you want to output>");
                        }
                    }
                    else if (args[0].Equals("removecommand") && args.Length >= 2)
                    {
                        string command = args[1].ToLower();
                        if (Commands.cmdExists(command))
                        {
                            Commands.removeCommand(command);
                            sendMessage(command + " command removed.", Api.GetDisplayName(user) + " removed the command " + command);
                        }
                        else
                        {
                            sendMessage(command + " command does not exist.");
                        }
                    }
                }
                if (Database.getUserLevel(user) >= 1)
                {
                    if (args[0].Equals("commmandlist"))
                    {
                        string temp = Commands.getList();
                        if (temp != "")
                        {
                            sendMessage("Current commands: " + temp);
                        }
                        else
                        {
                            sendMessage("No custom commands were added.");
                        }
                    }
                }
            }
        }

        /*private static void handleMessage(string message)
        {
            if (Commands.CheckCommand(user, message, true)) return;

            //string[] msg = message.Split(' ');

            //if (msg[0].Equals("!restart") && db.getUserLevel(user) == 3)
            //{
            //    irc.Close();
            //    //Flush();
            //    Connect();
            //}
        }*/



        private static void addUserToList(string usr)
        {
            usr = Api.capName(usr);
            lock (ActiveUsers)
            {
                if (!ActiveUsers.ContainsKey(usr))
                {
                    ActiveUsers.Add(usr, Api.GetUnixTimeNow());
                }
                else
                {
                    ActiveUsers[usr] = Api.GetUnixTimeNow();
                }
            }
        }

        public static bool IsUserInList(string usr)
        {
            usr = Api.capName(usr);
            lock (ActiveUsers)
            {
                if (ActiveUsers.ContainsKey(usr))
                {
                    return true;
                }
            }
            return false;
        }

        private static void removeUserFromList(string usr)
        {
            usr = Api.capName(usr);
            lock (ActiveUsers)
            {
                if (ActiveUsers.ContainsKey(usr))
                {
                    ActiveUsers.Remove(usr);
                }
            }
        }

        public static void buildUserList()
        {
            //sendRaw("WHO " + channel);
            Thread thread = new Thread(() =>
            {
                using (WebClient w = new WebClient())
                {
                    string json_data = "";
                    try
                    {
                        w.Proxy = null;
                        json_data = w.DownloadString("http://tmi.twitch.tv/group/user/" + channel.Substring(1) + "/chatters");
                        //users.Clear();
                        List<string> lUsers = new List<string>();
                        if (json_data.Replace("\"", "") != "")
                        {
                            JObject stream = JObject.Parse(JObject.Parse(json_data)["chatters"].ToString());
                            string[] sUsers = (stream["moderators"].ToString().Replace(" ", "").Replace("\"", "").Replace("\r\n", "").Replace("[", "").Replace("]", "") + "," + stream["staff"].ToString().Replace(" ", "").Replace("\"", "").Replace("\r\n", "").Replace("[", "").Replace("]", "") + "," + stream["admins"].ToString().Replace(" ", "").Replace("\"", "").Replace("\r\n", "").Replace("[", "").Replace("]", "") + "," + stream["viewers"].ToString().Replace(" ", "").Replace("\"", "").Replace("\r\n", "").Replace("[", "").Replace("]", "")).Split(',');
                            foreach (string sUser in sUsers)
                            {
                                if (sUser != "")
                                {
                                    Api.GetDisplayName(sUser);
                                    if (!lUsers.Contains(Api.capName(sUser)))
                                    {
                                        lUsers.Add(Api.capName(sUser));
                                    }
                                }
                            }
                        }
                        lock (ActiveUsers)
                        {
                            foreach(string usr in lUsers)
                            {
                                if(!ActiveUsers.ContainsKey(usr))
                                {
                                    addUserToList(usr);
                                }
                            }
                        }
                    }
                    catch (SocketException)
                    {
                        Console.WriteLine("Unable to connect to twitch API to build the user list.");
                    }
                    catch (Exception e)
                    {
                        StreamWriter errorLog = new StreamWriter("Error_Log.log", true);
                        errorLog.WriteLine("*************Error Message (via buildUserList()): " + DateTime.Now + "*********************************\r\nUnable to connect to twitch API to build the user list.\r\n" + e + "\r\n");
                        errorLog.Close();
                    }
                }
            });
            thread.Start();
            thread.Join();
        }

        private static string getUser(string message)
        {
            return Api.capName(message.Split('!')[0].Substring(1));
        }

        private static void sendRaw(string message)
        {
            int attempt = 0;
            while (attempt < 5)
            {
                attempt++;
                try
                {
                    write.WriteLine(message);
                    break;
                }
                catch (Exception)
                {
                    if (attempt == 5)
                    {
                        Console.Clear();
                        //Console.WriteLine("Can't send data. Attempt: " + attempt);
                        Console.WriteLine("Disconnected. Attempting to reconnect.");
                        irc.Close();
                        //Flush();
                        Connect();
                    }
                }
            }
        }

        public static void sendMessage(string message, string log="", bool usemecommand = true)
        {
            sendRaw("PRIVMSG " + channel.ToLower() + " :" + (usemecommand ? "/me " : "") + message);
            if(log != "")
            {
                Log(log);
            }
            Console.WriteLine(nick + ": " + message);
        }

        private static string checkBtag(string person)
        {
            //DB Lookup person to see if they have a battletag set
            string btag = Database.getBtag(person);
            //print(btag);
            if (btag == null)
            {
                return person;
            }
            else return person + " (" + btag + ") ";
        }

        private static List<string> checkSubList()
        {
            List<string> lSubs = new List<string>();
            string sSubURL = ini.GetValue("Settings", "Subsribers_URL", "");
            if (sSubURL != "")
            {
                Thread tThread = new Thread(() =>
                {
                    string json_data = "";
                    using (WebClient w = new WebClient())
                    {
                        try
                        {
                            w.Proxy = null;
                            json_data = w.DownloadString(sSubURL);
                            JObject list = JObject.Parse(json_data);
                            foreach (var x in list["feed"]["entry"])
                            {
                                lSubs.Add(Api.capName(x["title"]["$t"].ToString()));
                            }
                        }
                        catch (SocketException)
                        {
                            Console.WriteLine("Unable to read from Google Drive. Skipping.");
                        }
                        catch (Exception e)
                        {
                            StreamWriter errorLog = new StreamWriter("Error_Log.log", true);
                            errorLog.WriteLine("*************Error Message (via checkSubList()): " + DateTime.Now + "*********************************\r\n" + e + "\r\n");
                            errorLog.Close();
                        }
                    }
                });
                tThread.Start();
                tThread.Join();
            }
            return lSubs;
        }

        private static void handoutCurrency()
        {
            g_iLastHandout = Api.GetUnixTimeNow();
            List<string> temp = new List<string>();
            buildUserList();

            try
            {
                temp = checkSubList();
                //Console.WriteLine(temp);
            }
            catch (Exception e)
            {
                Console.WriteLine("Problem reading sub list. Skipping");
                StreamWriter errorLog = new StreamWriter("Error_Log.log", true);
                errorLog.WriteLine("*************Error Message (via handoutCurrency()): " + DateTime.Now + "*********************************\r\n" + e + "\r\n");
                errorLog.Close();
            }

            List<string> lHandoutUsers = ActiveUsers.Keys.ToList();
            if (MainForm.Currency_HandoutActiveStream.Checked || MainForm.Currency_HandoutActiveTime.Checked)
            {
                lHandoutUsers = new List<string>();
                lock (ActiveUsers)
                {
                    foreach (KeyValuePair<string, int> kv in ActiveUsers)
                    {
                        if (!lHandoutUsers.Contains(kv.Key))
                        {
                            if (MainForm.Currency_HandoutActiveStream.Checked && kv.Value >= g_iStreamStartTime || MainForm.Currency_HandoutActiveTime.Checked && Api.GetUnixTimeNow() - kv.Value <= Convert.ToInt32(MainForm.Currency_HandoutLastActive.Value) * 60)
                            {
                                lHandoutUsers.Add(kv.Key);
                            }
                        }
                    }
                }
            }

            if (!lHandoutUsers.Contains(Api.capName(nick)))
            {
                lHandoutUsers.Add(Api.capName(nick));
            }
            if (!lHandoutUsers.Contains(Api.capName(admin)))
            {
                lHandoutUsers.Add(Api.capName(admin));
            }

            foreach (string person in lHandoutUsers)
            {
                if (Database.isSubscriber(person) || temp.Contains(person))
                {
                    Database.addCurrency(person, payout * 2);
                }
                else
                {
                    Database.addCurrency(person, payout);
                }

            }
        }

        private static void buildBetOptions(string[] temp)
        {
            try
            {
                lock (betOptions)
                {
                    betOptions.Clear();
                    bool inQuote = false;
                    string option = "";
                    for (int i = 2; i < temp.Length; i++)
                    {
                        if(temp[i].StartsWith("\""))
                        {
                            inQuote = true;
                        }
                        if (!inQuote)
                        {
                            option = temp[i];
                        }
                        if (inQuote)
                        {
                            option += temp[i] + " ";
                        }
                        if (temp[i].EndsWith("\""))
                        {
                            option = option.Substring(1, option.Length - 3);
                            inQuote = false;
                        }
                        if (!inQuote && !option.StartsWith("#") && !betOptions.Contains(option))
                        {
                            betOptions.Add(option);
                            option = "";
                        }
                    }
                }

                //print(sb.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                StreamWriter errorLog = new StreamWriter("Error_Log.log", true);
                errorLog.WriteLine("*************Error Message (via buildBetOptions()): " + DateTime.Now + "*********************************\r\n" + e + "\r\n");
                errorLog.Close();
            }
        }

        private static void addToLookups(string usr)
        {
            if (usersToLookup.Count == 0)
            {
                currencyQueue.Change(4000, Timeout.Infinite);
            }
            if (!usersToLookup.Contains(usr))
            {
                usersToLookup.Add(usr);
            }
        }

        private static void handleCurrencyQueue(Object state)
        {
            if (usersToLookup.Count != 0)
            {
                string output = currency + ":";
                bool addComma = false;
                foreach (string person in usersToLookup)
                {
                    if (Database.userExists(person))
                    {
                        if (addComma)
                        {
                            output += ", ";
                        }

                        output += " " + Api.GetDisplayName(person) + " (" + Database.getTimeWatched(person).ToString(@"d\d\ hh\h\ mm\m") + ")" + " - " + Database.checkCurrency(person);
                        if (bettingOpen)
                        {
                            if (Pool.isInPool(person))
                            {
                                output += " [" + Pool.getBetAmount(person) + "]";
                            }
                        }
                        if (auctionOpen)
                        {
                            if (Auction.highBidder.Equals(person))
                            {
                                output += " {" + Auction.highBid + "}";
                            }
                        }
                        addComma = true;
                    }
                }
                //sendRaw("PRIVMSG " + channel + " :" + output);
                sendMessage(output);
                usersToLookup.Clear();
            }
        }

        private static void auctionLoop(Object state)
        {
            if (auctionOpen)
            {
                sendMessage(Api.GetDisplayName(Auction.highBidder) + " is currently winning, with a bid of " + Auction.highBid + "!");
            }
        }

        private static void Log(string output)
        {
            try
            {
                StreamWriter log = new StreamWriter("CommandLog.log", true);
                log.WriteLine("<" + DateTime.Now + "> " + output);
                log.Close();
            }
            catch (Exception e)
            {
                //Console.WriteLine(e);
                StreamWriter errorLog = new StreamWriter("Error_Log.log", true);
                errorLog.WriteLine("*************Error Message (via Log()): " + DateTime.Now + "*********************************\r\n" + e + "\r\n");
                errorLog.Close();
            }
        }
    }
}
