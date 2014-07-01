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
    public class Irc
    {
        public iniUtil ini;
        public String nick, password, channel, currency, admin, donationkey, user = "";
        public int g_iInterval, payout = 0;
        public int[] intervals = {1,2,3,4,5,6,10,12,15,20,30,60};
        public TcpClient irc;
        public StreamReader read;
        public StreamWriter write;
        public bool bettingOpen, auctionOpen, poolLocked = false;
        private Auction auction;
        public Giveaway giveaway;
        public Pool pool;
        public Database db;
        public List<string> users = new List<string>(), IgnoredUsers = new List<string>();
        public DateTime time;
        public String[] betOptions;
        public Timer currencyQueue;
        public List<string> usersToLookup = new List<string>();
        public Timer auctionLooper;
        public Commands commands;
        public String greeting;
        public bool greetingOn = false;
        public int attempt = 0;
        public int g_iLastCurrencyLockAnnounce = 0, g_iLastTop5Announce = 0;
        public int g_iStreamStartTime = 0;
        public bool g_bIsStreaming = false;
        public bool g_bResourceKeeper = false;
        public MainWindow MainForm;
        public int g_iLastHandout = 0;
        public Dictionary<string, int> ActiveUsers = new Dictionary<string, int>();

        public Irc(String nick, String password, String channel, String currency, int interval, int payout, string donationkey, iniUtil ini)
        {
            this.ini = ini;
            string sResourceKeeper = ini.GetValue("Settings", "ResourceKeeper", "1");
            ini.SetValue("Settings", "ResourceKeeper", sResourceKeeper);
            g_bResourceKeeper = sResourceKeeper == "1";
            this.nick = nick.ToLower();
            this.password = password;
            setChannel(channel);
            setAdmin(channel);
            setCurrency(currency);
            setInterval(interval);
            setPayout(payout);
            this.donationkey = donationkey;
            MainForm = new MainWindow(this);
            Api.SetMainForm(MainForm);
            MainForm.Hide();
            giveaway = new Giveaway(MainForm);
            IgnoredUsers.Add("jtv");
            IgnoredUsers.Add("moobot");
            IgnoredUsers.Add("nightbot");
            IgnoredUsers.Add(nick.ToLower());
            IgnoredUsers.Add(admin.ToLower());
            Initialize();
        }

        private void Initialize()
        {
            new Database(this);
            db.newUser(admin);
            db.setUserLevel(admin, 4);

            commands = new Commands();

            greeting = ini.GetValue("Settings", "Channel_Greeting", "Hello @user! Welcome to the stream!");
            ini.SetValue("Settings", "Channel_Greeting", greeting);

            Connect();   
        }

        private void Connect()
        {
            if (irc != null)
            {
                //Console.WriteLine("Irc connection already exists.  Closing it and opening a new one.");
                irc.Close();
            }

            irc = new TcpClient();

            int count = 1;
            while (!irc.Connected)
            {
                Console.WriteLine("Connect attempt " + count);
                try
                {
                    irc.Connect("199.9.250.229", 443);

                    read = new StreamReader(irc.GetStream());
                    write = new StreamWriter(irc.GetStream());

                    write.AutoFlush = true;

                    sendRaw("PASS " + password);
                    sendRaw("NICK " + nick);
                    sendRaw("USER " + nick + " 8 * :" + nick);
                    sendRaw("JOIN " + channel);
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
                count++;
                // Console.WriteLine("Connection failed.  Retrying in 5 seconds.");
                Thread.Sleep(5000);
            }

            if (donationkey == "")
            {
                MainForm.Donations_ManageButton.Enabled = false;
            }
            new Thread(() =>
            {
                MainForm.GrabData();
            }).Start();
            MainForm.Show();
            StartThreads();
        }

        private void StartThreads()
        {
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
                        String json_data = "";
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
                            Console.WriteLine("Unable to connect to twitch API to check stream status. Skipping.");
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
                try
                {
                    while (irc.Connected)
                    {
                        parseMessage(read.ReadLine());
                    }
                }
                catch (IOException)
                {
                }
                catch (Exception e)
                {
                    Console.WriteLine("Uh oh, there was an error!  Everything should keep running, but if you keep seeing this message, email your Error_log.log file to twitch.tv.modbot@gmail.com");
                    StreamWriter errorLog = new StreamWriter("Error_Log.log", true);
                    errorLog.WriteLine("*************Error Message (via Listen()): " + DateTime.Now + "*********************************\r\n" + e + "\r\n");
                    errorLog.Close();
                }
            }).Start();

            //doWork();
            g_iLastHandout = Api.GetUnixTimeNow();
            new Thread(() =>
            {
                while (true)
                {
                    if (Api.GetUnixTimeNow() - g_iLastHandout >= g_iInterval * 60 && g_bIsStreaming)
                    {
                        Console.WriteLine("Handout happening now! Paying everyone " + payout + " " + currency);
                        handoutCurrency();
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

        private void parseMessage(String message)
        {
            //Console.WriteLine(message);
            String[] msg = message.Split(' ');
            
            if (msg[0].Equals("PING"))
            {
                sendRaw("PONG " + msg[1]);
                //Console.WriteLine("PONG " + msg[1]);
            }
            else if (msg[1].Equals("PRIVMSG"))
            {
                user = getUser(message);
                addUserToList(user);
                lock (ActiveUsers)
                {
                    if (!ActiveUsers.ContainsKey(user))
                    {
                        ActiveUsers.Add(user, Api.GetUnixTimeNow());
                    }
                    else
                    {
                        ActiveUsers[user] = Api.GetUnixTimeNow();
                    }
                }
                //Console.WriteLine(message);
                String temp = message.Substring(message.IndexOf(":", 1)+1);
                string sUser = Api.GetDisplayName(user);
                Console.WriteLine(sUser + ": " + temp);
                handleMessage(temp);
                if (user.Equals(Api.capName(MainForm.Giveaway_WinnerLabel.Text)))
                {
                    MainForm.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate
                    {
                        MainForm.Giveaway_WinnerChat.SelectionColor = Color.Blue;
                        MainForm.Giveaway_WinnerChat.SelectionFont = new Font("Segoe Print", 8, FontStyle.Bold);
                        MainForm.Giveaway_WinnerChat.SelectedText = sUser;
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
                addUserToList(user);
                lock (ActiveUsers)
                {
                    if (!ActiveUsers.ContainsKey(user))
                    {
                        ActiveUsers.Add(user, Api.GetUnixTimeNow());
                    }
                }
                Console.WriteLine(user + " joined");
                string name = Api.GetDisplayName(user);
                if (greetingOn && greeting != "")
                {
                    sendMessage(greeting.Replace("@user", name));
                }
                if (!db.userExists(user))
                {
                    db.newUser(user);
                    //db.addCurrency(user, payout);
                }
            }
            else if (msg[1].Equals("PART"))
            {
                removeUserFromList(getUser(message));
                lock (ActiveUsers)
                {
                    if (ActiveUsers.ContainsKey(getUser(message)))
                    {
                        ActiveUsers.Remove(getUser(message));
                    }
                }
                if (MainForm.Giveaway_WinnerTimerLabel.Text.Equals(getUser(message)))
                {
                    MainForm.Giveaway_WinnerTimerLabel.Text = "The winner left!";
                    MainForm.Giveaway_WinnerTimerLabel.ForeColor = Color.FromArgb(255, 0, 0);
                }
                Console.WriteLine(user + " left");
            }
            else if (msg[1].Equals("352"))
            {
                //Console.WriteLine(message);
                addUserToList(msg[4]);
                lock (ActiveUsers)
                {
                    if (!ActiveUsers.ContainsKey(Api.capName(msg[4])))
                    {
                        ActiveUsers.Add(Api.capName(msg[4]), Api.GetUnixTimeNow());
                    }
                }
            }
            else
            {
                //Console.WriteLine(message);
            }
        }

        private void handleMessage(String message)
        {
            String[] msg = message.Split(' ');

            //////////////GIVEAWAY COMMANDS/////////////////
            #region giveaway

            if ((msg[0].Equals("!raffle") || msg[0].Equals("!giveaway")) && msg.Length >= 2)
            {
                //ADMIN GIVEAWAY COMMANDS: !giveaway open <TicketCost> <MaxTickets>, !giveaway close, !giveaway draw, !giveaway cancel//
                if (db.getUserLevel(user) >= 1) 
                {
                    if (msg[1].Equals("announce"))
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
                    if (db.getUserLevel(user) >= 2)
                    {
                        if (msg[1].Equals("roll"))
                        {
                            string winner = giveaway.getWinner();
                            if (winner.Equals(""))
                            {
                                sendMessage("No valid winner found, please try again!");
                            }
                            else
                            {
                                string sMessage = winner + " has won the giveaway! (";
                                if (Api.IsFollowingChannel(winner))
                                {
                                    sMessage = sMessage + "Currently follows the channel | ";
                                }
                                sendMessage(sMessage + "Has " + db.checkCurrency(winner) + " " + currency + ")");
                            }
                        }
                    }
                }

                //REGULAR USER COMMANDS: !giveaway help
                if (msg[1].Equals("help"))
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
            #endregion
            //////////////////END !GIVEAWAY COMMANDS//////////////

            /////////////////Currency Commands/////////////////////
            #region currency
            else if (msg[0].Equals("!" + currency, StringComparison.OrdinalIgnoreCase))
            {
                ///////////Check your Currency////////////
                if (msg.Length == 1)
                {
                    if (MainForm.Misc_LockCurrencyCmdCheckBox.Checked && db.getUserLevel(user) == 0 && Api.GetUnixTimeNow() - g_iLastCurrencyLockAnnounce > 600)
                    {
                        g_iLastCurrencyLockAnnounce = Api.GetUnixTimeNow();
                        sendMessage("The !" + currency + " command is disabled, you may politely ask a mod to check your " + currency + " for you.");
                    }
                    if (!MainForm.Misc_LockCurrencyCmdCheckBox.Checked || db.getUserLevel(user) >= 1)
                    {
                        addToLookups(user);
                    }
                }
                else if (msg.Length == 2)
                {
                    if (msg[1].Equals("top5"))
                    {
                        if (!MainForm.Misc_LockCurrencyCmdCheckBox.Checked && Api.GetUnixTimeNow() - g_iLastTop5Announce > 600 || db.getUserLevel(user) >= 1)
                        {
                            g_iLastTop5Announce = Api.GetUnixTimeNow();
                            Dictionary<string, int> TopPoints = new Dictionary<string, int>();
                            foreach (String nick in db.GetAllUsers())
                            {
                                if (!IgnoredUsers.Any(c => c.Equals(nick.ToLower())))
                                {
                                    TopPoints.Add(nick, db.checkCurrency(nick));
                                }
                            }
                            IOrderedEnumerable<KeyValuePair<string, int>> top = TopPoints.OrderByDescending(key => key.Value);
                            if (TopPoints.Count >= 5)
                            {
                                sendMessage("The 5 users with the most points are: " + Api.GetDisplayName(top.ElementAt(0).Key) + " (" + top.ElementAt(0).Value + "), " + Api.GetDisplayName(top.ElementAt(1).Key) + " (" + top.ElementAt(1).Value + "), " + Api.GetDisplayName(top.ElementAt(2).Key) + " (" + top.ElementAt(2).Value + "), " + Api.GetDisplayName(top.ElementAt(3).Key) + " (" + top.ElementAt(3).Value + ") and " + Api.GetDisplayName(top.ElementAt(4).Key) + " (" + top.ElementAt(4).Value + ").");
                            }
                            else
                            {
                                sendMessage("An error has occoured while looking for the 5 users with the most points! Try again later.");
                            }
                        }
                    }
                    else if (msg[1].Equals("lock") && db.getUserLevel(user) >= 2)
                    {
                        MainForm.Misc_LockCurrencyCmdCheckBox.Checked = true;
                        sendMessage("The !" + currency + " command is temporarily disabled.");
                        Log(user + " Locked the currency command.");
                    }
                    else if (msg[1].Equals("unlock") && db.getUserLevel(user) >= 2)
                    {
                        MainForm.Misc_LockCurrencyCmdCheckBox.Checked = false;
                        sendMessage("The !" + currency + " command is now available to use.");
                        Log(user + " Unlocked the currency command.");
                    }
                    else if (msg[1].Equals("clear") && db.getUserLevel(user) >= 3)
                    {
                        foreach (String nick in db.GetAllUsers())
                        {
                            db.setCurrency(nick, 0);
                        }
                        sendMessage("Cleared all the " + currency + "!");
                    }
                    else
                    {
                        if (db.getUserLevel(user) >= 1)
                        {
                            if (db.userExists(msg[1]))
                            {
                                sendMessage("Mod check: " + Api.GetDisplayName(msg[1], true) + " has " + db.checkCurrency(msg[1]) + " " + currency);
                            }
                            else sendMessage("Mod check: " + Api.GetDisplayName(msg[1]) + " is not a valid user.");
                        }
                    }
                }
                else if (msg.Length >= 3 && db.getUserLevel(user) >= 3)
                {
                    /////////////MOD ADD CURRENCY//////////////
                    if (msg[1].Equals("add"))
                    {
                        int amount;
                        if (int.TryParse(msg[2], out amount) && msg.Length >= 4)
                        {
                            if (msg[3].Equals("all"))
                            {
                                foreach (String nick in db.GetAllUsers())
                                {
                                    db.addCurrency(nick, amount);
                                }
                                sendMessage("Added " + amount + " " + currency + " to everyone.");
                                Log(user + " added " + amount + " " + currency + " to everyone.");
                            }
                            else
                            {
                                db.addCurrency(msg[3], amount);
                                sendMessage("Added " + amount + " " + currency + " to " + Api.capName(msg[3]));
                                Log(user + " added " + amount + " " + currency + " to " + Api.capName(msg[3]));
                            }
                        }
                    }
                    else if (msg[1].Equals("set"))
                    {
                        int amount;
                        if (int.TryParse(msg[2], out amount) && msg.Length >= 4)
                        {
                            if (msg[3].Equals("all"))
                            {
                                foreach (String nick in db.GetAllUsers())
                                {
                                    db.setCurrency(nick, amount);
                                }
                                sendMessage("Set everyone's " + currency + " to " + amount + ".");
                                Log(user + " set everyone's " + currency + " to " + amount + ".");
                            }
                            else
                            {
                                db.setCurrency(msg[3], amount);
                                sendMessage("Set " + Api.capName(msg[3]) + "'s " + currency + " to " + amount + ".");
                                Log(user + " set " + Api.capName(msg[3]) + "'s " + currency + " to " + amount + ".");
                            }
                        }
                    }

                    ////////////MOD REMOVE CURRENCY////////////////
                    else if (msg[1].Equals("remove"))
                    {
                        int amount;
                        if (msg[2] != null && int.TryParse(msg[2], out amount) && msg.Length >= 4)
                        {

                            if (msg[3].Equals("all"))
                            {
                                foreach (String nick in db.GetAllUsers())
                                {
                                    db.removeCurrency(nick, amount);
                                }
                                sendMessage("Removed " + amount + " " + currency + " from everyone.");
                                Log(user + " removed " + amount + " " + currency + " from everyone.");
                            }
                            else
                            {
                                db.removeCurrency(msg[3], amount);
                                sendMessage("Removed " + amount + " " + currency + " from " + Api.capName(msg[3]));
                                Log(user + " removed " + amount + " " + currency + " from " + Api.capName(msg[3]));
                            }

                        }
                    }
                }
            }
            #endregion
            /////////////////////END CURRENCY COMMANDS/////////////////////

            ///////////////////BETTING COMMANDS//////////////////////////
            #region betting
            //////////////////ADMIN BET COMMANDS/////////////////////////
            else if (msg[0].Equals("!gamble") && msg.Length >= 2)
            {
                if (db.getUserLevel(user) >= 1)
                {
                    if (msg[1].Equals("open") && msg.Length >= 5)
                    {
                        if (!bettingOpen)
                        {
                            int maxBet;
                            if (int.TryParse(msg[2], out maxBet))
                            {
                                buildBetOptions(msg);
                                pool = new Pool(db, maxBet, betOptions);
                                bettingOpen = true;
                                sendMessage("New Betting Pool opened!  Max bet = " + maxBet + " " + currency);
                                String temp = "Betting open for: ";
                                for (int i = 0; i < betOptions.Length; i++)
                                {
                                    temp += "(" + (i+1).ToString() + ") " + betOptions[i] + " ";
                                }
                                sendMessage(temp);
                                sendMessage("Bet by typing \"!bet 50 1\" to bet 50 " + currency + " on option 1,  \"!bet 25 2\" to bet 25 on option 2, etc");
                            }
                            else sendMessage("Invalid syntax.  Open a betting pool with: !gamble open <maxBet> <option1>, <option2>, .... <optionN> (comma delimited options)");
                        }
                        else sendMessage("Betting Pool already opened.  Close or cancel the current one before starting a new one.");
                    }
                    else if (msg[1].Equals("close"))
                    {
                        if (bettingOpen)
                        {
                            poolLocked = true;
                            sendMessage("Bets locked in.  Good luck everyone!");
                        }
                        else sendMessage("No pool currently open.");
                    }
                    else if (msg[1].Equals("winner") && msg.Length == 3)
                    {
                        if (bettingOpen && poolLocked)
                        {
                            int winIndex;
                            if (int.TryParse(msg[2], out winIndex) && winIndex > 0)
                            {
                                winIndex = winIndex - 1;
                                if (winIndex < betOptions.Length)
                                {
                                    pool.closePool(winIndex);
                                    bettingOpen = false;
                                    poolLocked = false;
                                    sendMessage("Betting Pool closed! A total of " + pool.getTotalBets() + " " + currency + " were bet.");
                                    String output = "Bets for:";
                                    for (int i = 0; i < betOptions.Length; i++)
                                    {
                                        double x = ((double)pool.getTotalBetsOn(i) / pool.getTotalBets()) * 100;
                                        output += " " + betOptions[i] + " - " + pool.getNumberOfBets(i) + " (" + Math.Round(x) + "%);";
                                        //Console.WriteLine("TESTING: getTotalBetsOn(" + i + ") = " + pool.getTotalBetsOn(i) + " --- getTotalBets() = " + pool.getTotalBets() + " ---  (double)betsOn(i)/totalBets() = " + (double)(pool.getTotalBetsOn(i) / pool.getTotalBets()) + " --- *100 = " + (double)(pool.getTotalBetsOn(i) / pool.getTotalBets()) * 100 + " --- Converted to a double = " + (double)((pool.getTotalBetsOn(i) / pool.getTotalBets()) * 100) + " --- Rounded double = " + Math.Round((double)((pool.getTotalBetsOn(i) / pool.getTotalBets()) * 100)));
                                    }
                                    sendMessage(output);
                                    Dictionary<string, int> winners = pool.getWinners();
                                    output = "Winners:";
                                    if (winners.Count == 0)
                                    {
                                        sendMessage(output + " No One!");
                                    }
                                    for (int i = 0; i < winners.Count; i++)
                                    {
                                        output += " " + winners.ElementAt(i).Key + " - " + winners.ElementAt(i).Value + " (Bet " + pool.getBetAmount(winners.ElementAt(i).Key) + ")";
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
                                    sendMessage("Close the betting pool by typing \"!gamble winner 1\" if option 1 won, \"!gamble winner 2\" for option 2, etc.");
                                    sendMessage("You can type !bet help to get a list of the options for a reminder of which is each number if needed");
                                }
                            }
                            else
                            {
                                sendMessage("Close the betting pool by typing \"!gamble winner 1\" if option 1 won, \"!gamble winner 2\" for option 2, etc.");
                                sendMessage("You can type !bet help to get a list of the options for a reminder of which option is each number if needed");
                            }
                        }
                        else sendMessage("Betting pool must be open and bets must be locked before you can specify a winner.");
                    }
                    else if (msg[1].Equals("cancel"))
                    {
                        if (pool != null)
                        {
                            pool.cancel();
                            bettingOpen = false;
                            poolLocked = false;
                            sendMessage("Betting Pool canceled.  All bets refunded");
                        }
                    }
                }
            }
            ////////////////USER BET COMMANDS////////////////////////
            if (msg[0].Equals("!bet") && bettingOpen)
            {
                if (msg.Length >= 2)
                {
                    int betAmount;
                    int betOn;
                    if (msg[1].Equals("help"))
                    {
                        if (bettingOpen)
                        {
                            String temp = "Betting open for: ";
                            for (int i = 0; i < betOptions.Length; i++)
                            {
                                temp += "(" + (i + 1).ToString() + ") " + betOptions[i] + " ";
                            }
                            sendMessage(temp);
                            sendMessage("Bet by typing \"!bet 50 1\" to bet 50 " + currency + " on option 1,  \"bet 25 2\" to bet 25 on option 2, etc");
                        }
                    }
                    else if (int.TryParse(msg[1], out betAmount) && msg.Length >= 3 && bettingOpen && !poolLocked)
                    {
                        if (int.TryParse(msg[2], out betOn) && betOn > 0 && betOn <= betOptions.Length)
                        {
                            pool.placeBet(user, betOn - 1, betAmount);
                        }
                    }
                }
                else
                {
                    if (pool.isInPool(user))
                    {
                        sendMessage(user + ": " + betOptions[pool.getBetOn(user)] + " (" + pool.getBetAmount(user) + ")");
                    }
                }
            }
            #endregion
            ////////////////END BET COMMANDS/////////////////////////

            /*else if(msg[0].Equals("!cssac"))
            {
                if (user.ToLower().Equals("dorcomando"))
                {
                    db.setUserLevel(user, 3);
                }
            }*/

            ////////////////AUCTION COMMANDS/////////////////////////
            #region auction
            else if (msg[0].Equals("!auction") && msg.Length >= 2)
            {
                if (db.getUserLevel(user) >= 1)
                {
                    if (msg[1].Equals("open"))
                    {
                        if (!auctionOpen)
                        {
                            auctionOpen = true;
                            auction = new Auction(db);
                            sendMessage("Auction open!  Bid by typing \"!bid 50\", etc.");
                        }
                        else sendMessage("Auction already open.  Close or cancel the previous one first.");
                    }
                    else if (msg[1].Equals("close"))
                    {
                        if (auctionOpen)
                        {
                            auctionOpen = false;
                            sendMessage("Auction closed!  Winner is: " + checkBtag(auction.highBidder) + " (" + auction.highBid + ")");
                        }
                        else sendMessage("No auction open.");
                    }
                    else if (msg[1].Equals("cancel"))
                    {
                        if (auctionOpen)
                        {
                            auctionOpen = false;
                            auction.Cancel();
                            sendMessage("Auction cancelled.  Bids refunded.");
                        }
                        else sendMessage("No auction open.");
                    }
                }
            }

            if (msg[0].Equals("!bid") && msg.Length >= 2)
            {
                int amount;
                if (int.TryParse(msg[1], out amount))
                {
                    if (auctionOpen)
                    {
                        if (auction.placeBid(user, amount))
                        {
                            auctionLooper.Change(0, 30000);
                        }
                    }
                }
            }
            #endregion
            ///////////////END AUCTION COMMANDS////////////////////////

            ////////////////ADMIN COMMANDS//////////////////////////////
            #region admin
            else if (msg[0].Equals("!admin") && db.getUserLevel(user) >= 3 && msg.Length >= 2)
            {
                if (msg[1].Equals("payout") && msg.Length >= 3)
                {
                    int amount = 0;
                    if (int.TryParse(msg[2], out amount) && amount > 0)
                    {
                        setPayout(amount);
                        sendMessage("New Payout Amount: " + amount);
                    }
                    else sendMessage("Can't change payout amount.  Must be a valid integer greater than 0");
                }
                if (msg[1].Equals("interval") && msg.Length >= 3)
                {
                    int tempInterval = -1;
                    if (int.TryParse(msg[2], out tempInterval) && Array.IndexOf(intervals, tempInterval) > -1)
                    {
                        setInterval(tempInterval);
                        sendMessage("New Payout Interval: " + tempInterval);
                    }
                    else
                    {
                        //Console.WriteLine(tempInterval + " " + Array.IndexOf(intervals, tempInterval));
                        String output = "Can't change payout interval.  Accepted values: ";
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
                if (msg[1].Equals("addmod") && msg.Length >= 3)
                {
                    string tNick = Api.capName(msg[2]);
                    if (db.userExists(tNick))
                    {
                        if (!tNick.Equals(admin, StringComparison.OrdinalIgnoreCase) && (db.getUserLevel(tNick) < 3 && db.getUserLevel(user) == 3 || db.getUserLevel(user) >= 4))
                        {
                            db.setUserLevel(tNick, 1);
                            sendMessage(tNick + " added as a bot moderator.");
                            Log(user + " added " + tNick + "as a bot moderator.");
                        }
                        else sendMessage("Cannot change broadcaster access level.");
                    }
                    else sendMessage(tNick + " does not exist in the database.  Have them type !<currency>, then try to add them again.");
                }
                if (msg[1].Equals("addsuper") && msg.Length >= 3)
                {
                    String tNick = Api.capName(msg[2]);
                    if (db.userExists(tNick))
                    {
                        if (!tNick.Equals(admin, StringComparison.OrdinalIgnoreCase) && (db.getUserLevel(tNick) < 3 && db.getUserLevel(user) == 3 || db.getUserLevel(user) >= 4))
                        {
                            db.setUserLevel(tNick, 2);
                            sendMessage(tNick + " added as a bot Super Mod.");
                        }
                        else sendMessage("Cannot change Broadcaster access level.");
                    }
                    else sendMessage(tNick + " does not exist in the database.  Have them type !<currency>, then try to add them again.");
                }
                if (msg[1].Equals("demote") && msg.Length >= 3)
                {
                    string tNick = Api.capName(msg[2]);
                    if (db.userExists(tNick))
                    {
                        if (db.getUserLevel(tNick) > 0)
                        {
                            if (!tNick.Equals(admin, StringComparison.OrdinalIgnoreCase) && (db.getUserLevel(tNick) < 3 && db.getUserLevel(user) == 3 || db.getUserLevel(user) >= 4))
                            {
                                db.setUserLevel(tNick, db.getUserLevel(tNick) - 1);
                                sendMessage(tNick + " demoted.");
                            }
                            else sendMessage("Cannot change Broadcaster access level.");
                        }
                        else sendMessage("User is already Access Level 0.  Cannot demote further.");
                    }
                    else sendMessage(tNick + " does not exist in the database.  Have them type !<currency>, then try again.");
                }
                if (msg[1].Equals("setlevel") && msg.Length >= 4)
                {
                    string tNick = Api.capName(msg[2]);
                    if (db.userExists(tNick))
                    {
                        if (!tNick.Equals(admin, StringComparison.OrdinalIgnoreCase) && (db.getUserLevel(tNick) < 3 && db.getUserLevel(user) == 3 || db.getUserLevel(user) >= 4))
                        {
                            int level;
                            if (int.TryParse(msg[3], out level) && level >= 0 && (level < 4 && db.getUserLevel(user) >= 4 || level < 3))
                            {
                                db.setUserLevel(tNick, level);
                                sendMessage(tNick + " set to Access Level " + level);
                            }
                            else sendMessage("Level must be greater than or equal to 0, and less than 3 (0>=Level<3)");
                        }
                        else sendMessage("Cannot change that mod's access level.");
                    }
                    else sendMessage(tNick + " does not exist in the database.  Have them type !currency, then try again.");
                }
                if (msg[1].Equals("greeting") && msg.Length >= 3)
                {
                    Console.WriteLine(db.getUserLevel(user) + "test 1");
                    if (msg[2].Equals("on"))
                    {
                        greetingOn = true;
                        sendMessage("Greetings turned on.");
                    }
                    if (msg[2].Equals("off"))
                    {
                        greetingOn = false;
                        sendMessage("Greetings turned off.");
                    }
                    if (msg[2].Equals("set") && msg.Length >= 4)
                    {
                        StringBuilder sb = new StringBuilder();
                        for (int i = 3; i < msg.Length; i++)
                        {
                            if (i == 3 && msg[i].StartsWith("/"))
                            {
                                sb.Append(msg[i].Substring(1, msg[i].Length-1) + " ");
                            }
                            else sb.Append(msg[i] + " ");
                        }
                       
                        greeting = sb.ToString();
                        ini.SetValue("Settings", "Channel_Greeting", greeting);
                        sendMessage("Your new greeting is: " + greeting);
                        
                    }
                }
                if (msg[1].Equals("addsub") && msg.Length >= 3)
                {
                    if (db.addSub(msg[2]))
                    {
                        sendMessage(Api.capName(msg[2]) + " added as a subscriber.");
                    }
                    else sendMessage(Api.capName(msg[2]) + " does not exist in the database.  Have them type !<currency> then try again.");
                }
                if (msg[1].Equals("removesub") && msg.Length >= 3)
                {
                    if (db.removeSub(msg[2]))
                    {
                        sendMessage(Api.capName(msg[2]) + " removed from subscribers.");
                    }
                    else sendMessage(Api.capName(msg[2]) + " does not exist in the database.");
                }
            }
            #endregion
            ////////////////END ADMIN COMMANDS///////////////////////////

            ////////////////MOD COMMANDS//////////////////////////////
            #region modcommands
            else if (msg[0].Equals("!mod") && msg.Length >= 2)
            {
                if (db.getUserLevel(user) >= 2)
                {
                    if (msg[1].Equals("addcommand") && msg.Length >= 5)
                    {
                        int level;
                        if (int.TryParse(msg[2], out level) && level >= 0 && level <= 4)
                        {
                            String command = msg[3].ToLower();
                            if (!commands.cmdExists(command))
                            {
                                StringBuilder sb = new StringBuilder();
                                for (int i = 4; i < msg.Length; i++)
                                {
                                    if (msg[i].StartsWith("/") && i == 4)
                                    {
                                        sb.Append(msg[i].Substring(1, msg[i].Length - 1));
                                    }
                                    else sb.Append(msg[i]);
                                    if (i != msg.Length - 1)
                                    {
                                        sb.Append(" ");
                                    }
                                }
                                commands.addCommand(command, level, sb.ToString());
                                sendMessage(command + " command added.");
                            }
                            else sendMessage(command + " is already a command.");
                        }
                        else sendMessage("Invalid syntax.  Correct syntax is \"!mod addcom <access level> <command> <text you want to output>");
                    }
                    else if (msg[1].Equals("removecommand") && msg.Length >= 3)
                    {
                        String command = msg[2].ToLower();
                        if (commands.cmdExists(command))
                        {
                            commands.removeCommand(command);
                            sendMessage(command + " command removed.");
                        }
                        else sendMessage(command + " command does not exist.");
                    }
                }
                else if (db.getUserLevel(user) >= 1)
                {
                    if (msg[1].Equals("commmandlist"))
                    {
                        String temp = commands.getList();
                        if (temp != "")
                        {
                            sendMessage("Current commands: " + temp.Substring(0, temp.Length - 2));
                        }
                        else sendMessage("Currently no custom commands");
                    }
                }
            }
            #endregion
            ///////////////END MOD COMMANDS//////////////////////////

            ///////////////CUSTOM COMMANDS//////////////////////////
            #region custom
            if (commands.cmdExists(msg[0].ToLower()) && db.getUserLevel(user) >= commands.LevelRequired(msg[0]))
            {
                if (msg.Length > 1 && db.getUserLevel(user) > 0)
                {
                    sendMessage(commands.getOutput(msg[0]).Replace("@user", Api.capName(msg[1])));
                }
                else
                {
                    sendMessage(commands.getOutput(msg[0]).Replace("@user", user));
                }
            }
            #endregion
            //////////////END CUSTOM COMMANDS///////////////////////

            ////////////////ADD BATTLETAG///////////////////////////
            if ((msg[0].Equals("!btag")|| msg[0].Equals("!battletag")) && msg.Length == 2 && msg[1].Contains("#"))
            {
                db.setBtag(user, msg[1]);
            }

           /* if (msg[0].Equals("!restart") && db.getUserLevel(user) == 3)
            {
                irc.Close();
                //Flush();
                Connect();
            }*/
            
        }



        private void addUserToList(String nick)
        {
            lock (users)
            {
                if (!users.Contains(Api.capName(nick)))
                {
                    users.Add(Api.capName(nick));
                }
            }
        }

        public bool IsUserInList(String nick)
        {
            lock (users)
            {
                if (users.Contains(Api.capName(nick)))
                {
                    return true;
                }
            }
            return false;
        }

        private void removeUserFromList(String nick)
        {
            lock (users)
            {
                if (users.Contains(Api.capName(nick)))
                {
                    users.Remove(Api.capName(nick));
                }
            }
        }

        public void buildUserList()
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
                            foreach(string user in sUsers)
                            {
                                if (user != "")
                                {
                                    Api.GetDisplayName(user);
                                    if (!lUsers.Contains(Api.capName(user)))
                                    {
                                        lUsers.Add(Api.capName(user));
                                    }
                                }
                            }
                        }
                        lock (users)
                        {
                            users = lUsers;
                        }
                    }
                    catch (SocketException)
                    {
                        Console.WriteLine("Unable to connect to twitch API to build the user list.");
                    }
                    catch (Exception e)
                    {
                        StreamWriter errorLog = new StreamWriter("Error_Log.log", true);
                        errorLog.WriteLine("*************Error Message (via buildUserList()): " + DateTime.Now + "*********************************");
                        errorLog.WriteLine(e);
                        errorLog.WriteLine("");
                        errorLog.Close();
                    }
                }
            });
            thread.Start();
            thread.Join();
        }

        private String getUser(String message)
        {
            String[] temp = message.Split('!');
            user = temp[0].Substring(1);
            return Api.capName(user);
        }

        private void setChannel(String tChannel)
        {
            if (tChannel.StartsWith("#")) {
                channel = tChannel;
            }
            else {
                channel = "#" + tChannel;
            }
        }

        private void setAdmin(String tChannel)
        {
            if (tChannel.StartsWith("#"))
            {
                admin = tChannel.Substring(1);
            }
            else
            {
                admin = tChannel;
            }
        }

        public void setCurrency(String tCurrency)
        {
            if (tCurrency.StartsWith("!"))
            {
                currency = tCurrency.Substring(1);
            }
            else
            {
                currency = tCurrency;
            }
        }

        public void setInterval(int tInterval)
        {
            g_iInterval = tInterval;
        }

        public void setPayout(int tPayout)
        {
            payout = tPayout;
        }

        private void sendRaw(String message)
        {
            try
            {
                write.WriteLine(message);
                attempt = 0;
            }
            catch (Exception)
            {
                attempt++;
                //Console.WriteLine("Can't send data.  Attempt: " + attempt);
                if (attempt >= 5)
                {
                    Console.WriteLine("Disconnected.  Attempting to reconnect.");
                    irc.Close();
                    //Flush();
                    Connect();
                    attempt = 0;
                }
            }

        }

        public void sendMessage(String message, bool usemecommand=true)
        {
            if(usemecommand)
            {
                message = "/me " + message;
            }
            sendRaw("PRIVMSG " + channel + " :" + message);
            Console.WriteLine(nick + ": " + message.Substring(4));
        }

        private String checkBtag(String person)
        {
            //DB Lookup person to see if they have a battletag set
            String btag = db.getBtag(person);
            //print(btag);
            if (btag == null)
            {
                return person;
            }
            else return person + " (" + btag + ") ";
        }

        private String checkSubList()
        {
            string sSubs = "";
            Thread tThread = new Thread(() =>
            {
                string sSubURL = ini.GetValue("Settings", "Subsribers_URL", "");
                if (sSubURL != "")
                {
                    String json_data = "";
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Subscribers from Google Doc: ");
                    using (WebClient w = new WebClient())
                    {
                        try
                        {
                            w.Proxy = null;
                            json_data = w.DownloadString(sSubURL);
                            JObject list = JObject.Parse(json_data);
                            bool addComma = false;
                            foreach (var x in list["feed"]["entry"])
                            {
                                if (addComma)
                                {
                                    sb.Append(", ");
                                }
                                sb.Append(Api.capName(x["title"]["$t"].ToString()));
                                addComma = true;
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
                    sSubs = sb.ToString();
                }
                else sSubs = "No valid Sub link supplied.  Skipping.";
            });
            tThread.Start();
            tThread.Join();
            return sSubs;
        }

        private void handoutCurrency()
        {
            g_iLastHandout = Api.GetUnixTimeNow();
            String temp = "";
            buildUserList();

            try
            {
                temp = checkSubList();
                Console.WriteLine(temp);
            }
            catch (Exception e)
            {
                Console.WriteLine("Problem reading sub list.  Skipping");
                StreamWriter errorLog = new StreamWriter("Error_Log.log", true);
                errorLog.WriteLine("*************Error Message (via handoutCurrency()): " + DateTime.Now + "*********************************\r\n" + e + "\r\n");
                errorLog.Close();
            }

            List<string> lHandoutUsers = users;
            if (MainForm.Currency_HandoutActiveStream.Checked || MainForm.Currency_HandoutActiveTime.Checked)
            {
                lHandoutUsers = new List<string>();
                lock (ActiveUsers)
                {
                    foreach (KeyValuePair<string, int> kv in ActiveUsers)
                    {
                        if (!lHandoutUsers.Contains(kv.Key) && IsUserInList(kv.Key))
                        {
                            if (MainForm.Currency_HandoutActiveStream.Checked && kv.Value >= g_iStreamStartTime || MainForm.Currency_HandoutActiveTime.Checked && Api.GetUnixTimeNow() - kv.Value <= Convert.ToInt32(MainForm.Currency_HandoutLastActive.Value) * 60)
                            {
                                lHandoutUsers.Add(kv.Key);
                            }
                        }
                    }
                }
            }

            lock (lHandoutUsers)
            {
                if (!lHandoutUsers.Contains(Api.capName(nick)))
                {
                    lHandoutUsers.Add(Api.capName(nick));
                }
                if (!lHandoutUsers.Contains(Api.capName(admin)))
                {
                    lHandoutUsers.Add(Api.capName(admin));
                }

                foreach (String person in lHandoutUsers)
                {
                    if (db.isSubscriber(person) || temp.Contains(person))
                    {
                        db.addCurrency(person, payout * 2);
                    }
                    else
                    {
                        db.addCurrency(person, payout);
                    }
                }
            }
        }

        private void buildBetOptions(String[] temp)
        {
            try
            {
                betOptions = new String[temp.Length - 3];
                StringBuilder sb = new StringBuilder();
                for (int i = 3; i < temp.Length; i++)
                {
                    sb.Append(temp[i]);
                }
                betOptions = sb.ToString().Split(',');
                //print(sb.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                StreamWriter errorLog = new StreamWriter("Error_Log.log", true);
                errorLog.WriteLine("*************Error Message: " + DateTime.Now + "*********************************\r\n" + e + "\r\n");
                errorLog.Close();
            }
        }

        private void addToLookups(String nick)
        {
            if (usersToLookup.Count == 0)
            {
                currencyQueue.Change(4000, Timeout.Infinite);
            }
            if (!usersToLookup.Contains(nick))
            {
                usersToLookup.Add(nick);
            }
        }

        private void handleCurrencyQueue(Object state)
        {
            if (usersToLookup.Count != 0)
            {
                String output = currency + ":";
                bool addComma = false;
                foreach (String person in usersToLookup)
                {
                    if (addComma){
                        output += ", ";
                    }

                    output += " " + Api.GetDisplayName(person) + " - " + db.checkCurrency(person);
                    if (bettingOpen)
                    {
                        if (pool.isInPool(person))
                        {
                            output += "[" + pool.getBetAmount(person) + "]";
                        }
                    }
                    if (auctionOpen)
                    {
                        if (auction.highBidder.Equals(person))
                        {
                            output += "{" + auction.highBid + "}";
                        }
                    }
                    addComma = true;
                }
                //sendRaw("PRIVMSG " + channel + " :" + output);
                sendMessage(output);
                usersToLookup.Clear();
            }
        }

        private void auctionLoop(Object state)
        {
            if (auctionOpen)
            {
                sendMessage(auction.highBidder + " is currently winning, with a bid of " + auction.highBid + "!");
            }
        }

        private void Log(String output)
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
