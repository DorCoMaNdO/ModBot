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
using System.Data.SQLite;

namespace ModBot
{
    public delegate void OnInitialize(InitializationStep step);

    public enum InitializationStep
    {
        ValidateBotToken,
        ValidateChannelToken,
        ConfirmChannel,
        ConnectionAborted,
        ConfigureSettings,
        DatabaseSetup,
        Connect,
        ConnectionSuccessful,
        ConnectionFailed,
        CommandRegistration,
        ThreadsCreation
    }

    public static class Irc
    {
        private static iniUtil ini = Program.ini;
        public static TcpClient irc;
        public static StreamReader read;
        public static StreamWriter write;
        public static string nick, password, channel, currency, currencyName, admin, donation_clientid, donation_token, channeltoken;
        public static bool partnered;
        public static int interval, payout, subpayout;
        public static bool auctionOpen;
        public static string greeting;
        public static bool greetingOn;
        public static int g_iLastCurrencyDisabledAnnounce, g_iLastTop5Announce;
        public static int g_iStreamStartTime = 0;
        public static bool IsStreaming;
        public static bool ResourceKeeper;
        public static MainWindow MainForm = Program.MainForm;
        public static Dictionary<string, int> ActiveUsers = new Dictionary<string, int>();
        public static Dictionary<string, int> Warnings = new Dictionary<string, int>();
        public static List<string> IgnoredUsers = new List<string>();
        public static Timer currencyQueue, auctionLoop, giveawayQueue, warningsRemoval;
        public static List<string> usersToLookup = new List<string>();
        public static bool DetailsConfirmed;
        private static bool CommandsRegistered;
        public static List<string> Moderators = new List<string>();
        public static bool IsModerator;
        public static List<Thread> Threads = new List<Thread>();
        private static StreamWriter log = new StreamWriter("CommandLog.log", true);

        public static event OnInitialize OnInitialize = ((InitializationStep step) => { });

        public static void Initialize()
        {
            Api.MainForm = MainForm = Program.MainForm;
            ActiveUsers.Clear();
            Warnings.Clear();
            usersToLookup.Clear();
            Moderators.Clear();
            DetailsConfirmed = false;
            IsModerator = false;

            if (donation_clientid == "" || donation_token == "")
            {
                MainForm.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate
                {
                    MainForm.DonationsWindowButton.Enabled = false;
                    MainForm.DonationsWindowButton.Text = "Donations\r\n(Disabled)";
                });
                Irc.donation_clientid = "";
                Irc.donation_token = "";
            }

            MainForm.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate
            {
                foreach (System.Windows.Forms.Control ctrl in MainForm.SettingsWindow.Controls)
                {
                    if (ctrl.GetType() != typeof(System.Windows.Forms.Label))
                    {
                        ctrl.Enabled = false;
                    }
                }
            });

            Program.FocusConsole();

            Console.WriteLine("Validating bot's access token...");

            OnInitialize(InitializationStep.ValidateBotToken);

            bool bAbort = true;
            for (int attempts = 0; attempts < 5; attempts++)
            {
                Console.WriteLine("Bot's access token validation attempt : " + (attempts + 1) + "/5");
                using (WebClient w = new WebClient())
                {
                    w.Proxy = null;
                    try
                    {
                        string json_data = w.DownloadString("https://api.twitch.tv/kraken?oauth_token=" + password.Replace("oauth:", ""));
                        JObject json = JObject.Parse(json_data);
                        if (json["token"]["valid"].ToString() == "True" && json["token"]["user_name"].ToString() == nick)
                        {
                            foreach (JToken x in json["token"]["authorization"]["scopes"])
                            {
                                if (x.ToString() == "chat_login")
                                {
                                    Console.WriteLine("Bot's access token has been validated.\r\n");
                                    bAbort = false;
                                    break;
                                }
                            }
                            if (!bAbort) break;
                        }
                        else
                        {
                            MainForm.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate
                            {
                                Console.WriteLine(MainForm.SettingsErrorLabel.Text = "Twitch reported bot's auth token invalid.");
                            });
                            Thread.Sleep(10);
                            break;
                        }
                    }
                    catch (Exception e)
                    {
                        Api.LogError("*************Error Message (via validateBotToken()): " + DateTime.Now + "*********************************\r\n" + e + "\r\n");
                    }
                }

                if (attempts == 4)
                {
                    Console.WriteLine("Failed to validate the bot's access token after 5 attempts.");
                }
                Thread.Sleep(100);
            }

            if(!bAbort)
            {
                Console.WriteLine("Confirming channel's existence in Twitch...");

                OnInitialize(InitializationStep.ConfirmChannel);

                for (int attempts = 0; attempts < 5; attempts++)
                {
                    Console.WriteLine("Channel's existence confirming attempt : " + (attempts + 1) + "/5");
                    using (WebClient w = new WebClient())
                    {
                        w.Proxy = null;
                        try
                        {
                            w.DownloadString("https://api.twitch.tv/kraken/channels/" + channel.Substring(1));
                            Console.WriteLine("Channel existence confirmed.\r\n");
                            bAbort = false;
                            break;
                        }
                        catch (Exception e)
                        {
                            if (e.Message.Contains("(404) Not Found."))
                            {
                                MainForm.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate
                                {
                                    Console.WriteLine(MainForm.SettingsErrorLabel.Text = "Twitch reported channel not found.");
                                });
                                bAbort = true;
                                System.Threading.Thread.Sleep(10);
                                break;
                            }
                            else
                            {
                                Api.LogError("*************Error Message (via confirmStream()): " + DateTime.Now + "*********************************\r\n" + e + "\r\n");
                            }
                        }
                    }

                    if (attempts == 4)
                    {
                        Console.WriteLine("Failed to confirm the channel's existence after 5 attempts.");
                    }
                    Thread.Sleep(100);
                }

                if (!bAbort && channeltoken != "")
                {
                    Console.WriteLine("Validating channel's access token...");

                    OnInitialize(InitializationStep.ValidateChannelToken);

                    for (int attempts = 0; attempts < 5; attempts++)
                    {
                        Console.WriteLine("Channel's access token validation attempt : " + (attempts + 1) + "/5");
                        using (WebClient w = new WebClient())
                        {
                            w.Proxy = null;
                            try
                            {
                                JObject json = JObject.Parse(w.DownloadString("https://api.twitch.tv/kraken?oauth_token=" + channeltoken));
                                if (json["token"]["valid"].ToString() == "True" && json["token"]["user_name"].ToString() == channel.Substring(1))
                                {
                                    int scopes = 0;
                                    foreach (JToken x in json["token"]["authorization"]["scopes"])
                                    {
                                        if (x.ToString() == "user_read" || x.ToString() == "channel_editor" || x.ToString() == "channel_commercial" || x.ToString() == "channel_check_subscription" || x.ToString() == "chat_login")
                                        {
                                            scopes++;
                                        }
                                    }

                                    if (scopes == 5)
                                    {
                                        Console.WriteLine("Channel's access token has been validated.\r\n\r\nChecking partnership status...");

                                        json = JObject.Parse(w.DownloadString("https://api.twitch.tv/kraken/user?oauth_token=" + channeltoken));
                                        Console.WriteLine((partnered = json["partnered"].ToString() == "True") ? "Partnered.\r\n" : "Not partnered.\r\n");

                                        MainForm.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate
                                        {
                                            /*if (MainForm.ChannelTitleBox.Text != "Loading..." && MainForm.ChannelGameBox.Text != "Loading...")
                                            {
                                                MainForm.ChannelTitleBox.ReadOnly = false;
                                                MainForm.ChannelGameBox.ReadOnly = false;
                                                MainForm.UpdateTitleGameButton.Enabled = true;
                                            }*/
                                            MainForm.Channel_UseSteam.Enabled = true;
                                            ini.SetValue("Settings", "Channel_UseSteam", (MainForm.Channel_UseSteam.Checked = (ini.GetValue("Settings", "Channel_UseSteam", "0") == "1")) ? "1" : "0");
                                        });
                                        break;
                                    }
                                    else
                                    {
                                        MainForm.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate
                                        {
                                            Console.WriteLine(MainForm.SettingsErrorLabel.Text = "The channel's access token is missing access and will not be used, some functions will be disabled..\r\n");
                                        });
                                        channeltoken = "";
                                        Thread.Sleep(10);
                                        break;
                                    }
                                }
                                else
                                {
                                    MainForm.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate
                                    {
                                        Console.WriteLine(MainForm.SettingsErrorLabel.Text = "Twitch reported channel's auth token invalid, some functions will be disabled.\r\n");
                                    });
                                    channeltoken = "";
                                    Thread.Sleep(10);
                                    break;
                                }
                            }
                            catch (Exception e)
                            {
                                Api.LogError("*************Error Message (via validateBotToken()): " + DateTime.Now + "*********************************\r\n" + e + "\r\n");
                            }
                        }

                        if (attempts == 4)
                        {
                            Console.WriteLine("Failed to validate the channel's access token after 5 attempts.");
                        }
                        Thread.Sleep(100);
                    }
                }
            }

            if (bAbort)
            {
                Console.WriteLine("Aborting connection...");

                OnInitialize(InitializationStep.ConnectionAborted);

                MainForm.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate
                {
                    foreach (System.Windows.Forms.Control ctrl in MainForm.SettingsWindow.Controls)
                    {
                        ctrl.Enabled = true;
                    }
                    MainForm.DisconnectButton.Enabled = false;
                    MainForm.ConnectButton.Enabled = false;
                });

                Console.WriteLine("Connection aborted.\r\n");
                return;
            }

            DetailsConfirmed = true;

            Console.WriteLine("Configuring settings...");

            OnInitialize(InitializationStep.ConfigureSettings);

            MainForm.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate
            {
                string name = "";
                foreach (string word in currencyName.Split(' '))
                {
                    if (word != "")
                    {
                        int length = (name + word).Length;
                        string suffix = (length < currencyName.Length) ? (currencyName.Substring(length).Split(' ')).Length > 0 ? currencyName.Substring(length).Split(' ')[0] : currencyName.Substring(length) : "";
                        name += word + ((MainForm.CreateGraphics().MeasureString(word, MainForm.CurrencyWindowButton.Font).Width > MainForm.CurrencyWindowButton.Width - 16 || MainForm.CreateGraphics().MeasureString(word + " " + suffix, MainForm.CurrencyWindowButton.Font).Width > MainForm.CurrencyWindowButton.Width - 16) ? "\r\n" : " ");
                    }
                }
                MainForm.CurrencyWindowButton.Text = name;
                while (MainForm.CreateGraphics().MeasureString(name, MainForm.CurrencyWindowButton.Font).Width > MainForm.CurrencyWindowButton.Width - 16 || MainForm.CreateGraphics().MeasureString(name, MainForm.CurrencyWindowButton.Font).Height > MainForm.CurrencyWindowButton.Height - 16)
                {
                    MainForm.CurrencyWindowButton.Font = new Font(MainForm.CurrencyWindowButton.Font.Name, MainForm.CurrencyWindowButton.Font.Size - 1, FontStyle.Bold);
                }
                if (MainForm.CurrencyWindowButton.Font.Size < 6)
                {
                    MainForm.CurrencyWindowButton.Text = "Currency";
                    MainForm.CurrencyWindowButton.Font = new Font(MainForm.CurrencyWindowButton.Font.Name, 10F, FontStyle.Bold);
                }
                MainForm.ChannelWindowButton.Text = admin;

                MainForm.Currency_HandoutLabel.Text = "Handout " + currencyName + " to :";

                MainForm.Giveaway_MinCurrencyCheckBox.Text = "Must have at least                       " + currencyName;
            });

            ini.SetValue("Settings", "ResourceKeeper", (ResourceKeeper = (ini.GetValue("Settings", "ResourceKeeper", "1") == "1")) ? "1" : "0");

            /*if (donationkey == "")
            {
                MainForm.Donations_ManageButton.Enabled = false;
            }*/

            ini.SetValue("Settings", "Channel_Greeting", greeting = ini.GetValue("Settings", "Channel_Greeting", "Hello @user! Welcome to the stream!"));

            IgnoredUsers.Add("jtv");
            IgnoredUsers.Add("moobot");
            IgnoredUsers.Add("nightbot");
            IgnoredUsers.Add(nick.ToLower());
            IgnoredUsers.Add(admin.ToLower());

            Console.WriteLine("Settings configured.\r\n");

            if (Database.DB == null) OnInitialize(InitializationStep.DatabaseSetup);

            Database.CreateTable();

            //Database.newUser(admin);
            Database.setUserLevel(admin, 4);

            Connect();
        }

        private static void RegisterCommands()
        {
            if (!CommandsRegistered)
            {
                Console.WriteLine("Registering commands...");

                OnInitialize(InitializationStep.CommandRegistration);

                Commands.Add("!raffle", Command_Giveaway);
                Commands.Add("!giveaway", Command_Giveaway);
                Commands.Add("!ticket", Command_Tickets);
                Commands.Add("!tickets", Command_Tickets);
                Commands.Add("!" + currency, Command_Currency);
                Commands.Add("!gamble", Command_Gamble);
                Commands.Add("!bet", Command_Bet);
                Commands.Add("!auction", Command_Auction);
                Commands.Add("!bid", Command_Bid);
                Commands.Add("!btag", Command_BTag);
                Commands.Add("!battletag", Command_BTag);
                Commands.Add("!modbot", Command_ModBot);

                Console.WriteLine("Commands registered.\r\n");
                CommandsRegistered = true;
            }
        }

        private static void Connect()
        {
            Console.WriteLine("Initializing connection...");

            OnInitialize(InitializationStep.Connect);

            for (int attempt = 1; attempt <= 5; attempt++)
            {
                if (irc != null)
                {
                    //Console.WriteLine("Irc connection already exists. Closing it and opening a new one.");
                    irc.Close();
                }

                irc = new TcpClient();

                Console.WriteLine("Connection attempt number : " + attempt + "/5");

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
                    sendRaw("JOIN " + channel);

                    if (!read.ReadLine().Contains("Login unsuccessful"))
                    {
                        OnInitialize(InitializationStep.ConnectionSuccessful);

                        nick = Api.GetDisplayName(nick);
                        admin = Api.GetDisplayName(admin);

                        Console.WriteLine("Joined the channel.\r\n\r\nSending lame entrance line...\r\n");

                        List<string> lLines = new List<string>();
                        lLines.Add("ModBot has entered the building.");
                        lLines.Add("No fear, ModBot is here!");
                        lLines.Add("ModBot with style.");
                        lLines.Add("Fear not, here's the (Mod)Bot.");
                        lLines.Add("ModBot's in the HOUSE!");
                        sendMessage(lLines[new Random().Next(0, lLines.Count)], "", false);

                        RegisterCommands();

                        StartThreads();

                        MainForm.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate
                        {
                            foreach (System.Windows.Forms.CheckBox btn in MainForm.Windows.Keys)
                            {
                                btn.Enabled = true;
                            }

                            MainForm.ChannelWindowButton.Text = admin;
                            while (MainForm.CreateGraphics().MeasureString(admin, MainForm.ChannelWindowButton.Font).Width > MainForm.ChannelWindowButton.Width - 16)
                            {
                                MainForm.ChannelWindowButton.Font = new Font(MainForm.ChannelWindowButton.Font.Name, MainForm.ChannelWindowButton.Font.Size - 1, FontStyle.Bold);
                            }

                            MainForm.DonationsWindowButton.Enabled = false;

                            MainForm.SpamFilterWindowButton.Enabled = Moderators.Contains(Api.capName(nick));

                            MainForm.DisconnectButton.Enabled = true;

                            MainForm.GetSettings();
                        });
                    }
                    else
                    {
                        OnInitialize(InitializationStep.ConnectionFailed);

                        MainForm.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate
                        {
                            Console.WriteLine(MainForm.SettingsErrorLabel.Text = "Username and/or password (oauth token) are incorrect!");
                            MainForm.ConnectButton.Enabled = false;
                            MainForm.DisconnectButton.Enabled = false;
                        });
                    }

                    return;
                }
                catch (SocketException e)
                {
                    Api.LogError("*************Error Message (via Connect()): " + DateTime.Now + "*********************************\r\n" + e + "\r\n");
                }
                catch (Exception e)
                {
                    Api.LogError("*************Error Message (via Connect()): " + DateTime.Now + "*********************************\r\n" + e + "\r\n");
                }

                MainForm.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate
                {
                    MainForm.ConnectButton.Enabled = true;
                    MainForm.DisconnectButton.Enabled = false;
                });

                if (attempt <= 5)
                {
                    Console.WriteLine("Failed connect or configure post-connection settings.\r\nRetrying in 5 seconds.\r\n");
                    Thread.Sleep(5000);
                }
                else
                {
                    Console.WriteLine("Failed to connect to Twitch.TV chat servers...");

                    OnInitialize(InitializationStep.ConnectionFailed);

                    DetailsConfirmed = false;
                    IsModerator = false;

                    //MainForm.Hide();
                    List<Thread> Ts = new List<Thread>();
                    foreach (Thread t in Threads)
                    {
                        t.Abort();
                        Ts.Add(t);
                    }
                    Threads.Clear();
                    foreach (Thread t in Api.dCheckingDisplayName.Values)
                    {
                        t.Abort();
                        Ts.Add(t);
                    }
                    Api.dCheckingDisplayName.Clear();
                    foreach (Thread t in Ts)
                    {
                        while (t.IsAlive) Thread.Sleep(10);
                    }
                    //Thread.Sleep(TimeSpan.FromDays(365));

                    Pool.cancel();

                    if (irc != null && irc.Connected)
                    {
                        irc.Close();
                    }

                    MainForm.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate
                    {
                        MainForm.CurrencyWindowButton.Text = "Currency";
                        MainForm.CurrencyWindowButton.Font = new Font(MainForm.CurrencyWindowButton.Font.Name, 10F, FontStyle.Bold);
                        MainForm.ChannelWindowButton.Text = "Channel";
                        MainForm.ChannelWindowButton.Font = new Font(MainForm.ChannelWindowButton.Font.Name, 10F, FontStyle.Bold);
                        MainForm.ChannelStatusLabel.Text = "DISCONNECTED";
                        MainForm.ChannelStatusLabel.ForeColor = Color.Red;
                        MainForm.DonationsWindowButton.Text = "Donations";

                        MainForm.Channel_UseSteam.Enabled = false;

                        foreach (System.Windows.Forms.CheckBox btn in MainForm.Windows.Keys)
                        {
                            if (btn != MainForm.SettingsWindowButton && btn != MainForm.AboutWindowButton)
                            {
                                btn.Enabled = false;
                            }
                        }
                        MainForm.SettingsWindowButton.Enabled = true;

                        foreach (System.Windows.Forms.Control ctrl in MainForm.SettingsWindow.Controls)
                        {
                            ctrl.Enabled = true;
                        }
                        MainForm.DisconnectButton.Enabled = false;
                        MainForm.ConnectButton.Enabled = true;
                    });
                }
            }
        }

        public static void Disconnect()
        {
            Program.FocusConsole();

            Console.WriteLine("\r\nDisconnecting...\r\n");

            Pool.cancel();

            DetailsConfirmed = false;
            IsModerator = false;

            MainForm.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate
            {
                MainForm.CurrencyWindowButton.Text = "Currency";
                MainForm.CurrencyWindowButton.Font = new Font(MainForm.CurrencyWindowButton.Font.Name, 10F, FontStyle.Bold);
                MainForm.ChannelWindowButton.Text = "Channel";
                MainForm.ChannelWindowButton.Font = new Font(MainForm.ChannelWindowButton.Font.Name, 10F, FontStyle.Bold);
                MainForm.ChannelStatusLabel.Text = "DISCONNECTED";
                MainForm.ChannelStatusLabel.ForeColor = Color.Red;
                MainForm.DonationsWindowButton.Text = "Donations";

                MainForm.Channel_UseSteam.Enabled = false;

                foreach (System.Windows.Forms.CheckBox btn in MainForm.Windows.Keys)
                {
                    if (btn != MainForm.SettingsWindowButton && btn != MainForm.AboutWindowButton)
                    {
                        btn.Enabled = false;
                    }
                }
                MainForm.SettingsWindowButton.Enabled = true;

                foreach (System.Windows.Forms.Control ctrl in MainForm.SettingsWindow.Controls)
                {
                    ctrl.Enabled = true;
                }
                MainForm.DisconnectButton.Enabled = false;
                MainForm.ConnectButton.Enabled = false;
            });

            if (Threads.Count > 0)
            {
                Console.WriteLine("Stopping threads...");
                List<Thread> Ts = new List<Thread>();
                foreach (Thread t in Threads)
                {
                    t.Abort();
                    Ts.Add(t);
                }
                Threads.Clear();
                foreach (Thread t in Api.dCheckingDisplayName.Values)
                {
                    t.Abort();
                    Ts.Add(t);
                }
                Api.dCheckingDisplayName.Clear();
                /*foreach (Thread t in Ts)
                {
                    Console.Write(t.Name + " thread... ");
                    while (t.IsAlive) Thread.Sleep(10);
                    Console.Write("DONE\r\n");
                }*/
                Console.WriteLine("Threads stopped.\r\n");
                //Console.WriteLine("Threads stopped.\r\nClearing threads list...");
                //Threads.Clear();
                //Console.WriteLine("Threads list clear.\r\n");
            }

            if (irc != null && irc.Connected)
            {
                Console.WriteLine("Closing connection...");
                irc.Close();
                Console.WriteLine("Connection closed.\r\n");
            }

            MainForm.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate
            {
                MainForm.ConnectButton.Enabled = true;
            });

            Console.WriteLine("Disconnected.\r\n");
        }

        private static void StartThreads()
        {
            if (Threads.Count > 0)
            {
                Console.WriteLine("Stopping previously created threads...");
                List<Thread> Ts = new List<Thread>();
                foreach (Thread t in Threads)
                {
                    t.Abort();
                    Ts.Add(t);
                }
                Threads.Clear();
                foreach (Thread t in Api.dCheckingDisplayName.Values)
                {
                    t.Abort();
                    Ts.Add(t);
                }
                Api.dCheckingDisplayName.Clear();
                /*foreach (Thread t in Ts)
                {
                    Console.Write(t.Name + " thread... ");
                    while (t.IsAlive) Thread.Sleep(10);
                    Console.Write("DONE\r\n");
                }*/
                Console.WriteLine("Previously created threads stopped.\r\n");
                //Console.WriteLine("Previously created threads stopped.\r\nClearing threads list...");
                //Threads.Clear();
                //Console.WriteLine("Threads list clear.");
            }

            Console.WriteLine("Creating and starting threads and timers...");

            OnInitialize(InitializationStep.ThreadsCreation);

            bool Running = false;

            Console.Write("Time watched and currency handout thread... ");

            Thread thread = new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(60000);
                    if (irc.Connected && Running && IsStreaming)
                    {
                        new Thread(() =>
                        {
                            List<string> spreadsheetSubs = Api.checkSpreadsheetSubs();
                            buildUserList();
                            lock (ActiveUsers)
                            {
                                foreach (string user in ActiveUsers.Keys)
                                {
                                    Database.addTimeWatched(user, 1);
                                    TimeSpan t = Database.getTimeWatched(user);
                                    if (t.TotalMinutes % interval == 0 && (!MainForm.Currency_HandoutActiveStream.Checked && !MainForm.Currency_HandoutActiveTime.Checked || MainForm.Currency_HandoutActiveStream.Checked && ActiveUsers[user] >= g_iStreamStartTime || MainForm.Currency_HandoutActiveTime.Checked && Api.GetUnixTimeNow() - ActiveUsers[user] <= Convert.ToInt32(MainForm.Currency_HandoutLastActive.Value) * 60))
                                    {
                                        int old = Database.checkCurrency(user);
                                        while (old == Database.checkCurrency(user))
                                        {
                                            //old = Database.checkCurrency(user);
                                            if (Database.isSubscriber(user) || spreadsheetSubs.Contains(user) || Api.IsSubscriber(user))
                                            {
                                                Database.addCurrency(user, subpayout);
                                            }
                                            else
                                            {
                                                Database.addCurrency(user, payout);
                                            }
                                        }
                                    }
                                }

                                /*if (!MainForm.Currency_HandoutActiveStream.Checked && !MainForm.Currency_HandoutActiveTime.Checked)
                                {
                                    Console.WriteLine("Handout type : 0");
                                }
                                if (MainForm.Currency_HandoutActiveStream.Checked)
                                {
                                    Console.WriteLine("Handout type : 1");
                                }
                                if (MainForm.Currency_HandoutActiveTime.Checked)
                                {
                                    Console.WriteLine("Handout type : 2");
                                }
                                foreach (string user in ActiveUsers.Keys)
                                {
                                    Console.Write("\r\n" + user);
                                    Database.addTimeWatched(user, 1);
                                    TimeSpan t = Database.getTimeWatched(user);
                                    if (t.TotalMinutes % interval == 0)
                                    {
                                        Console.Write(" should get points");
                                    }
                                    if (t.TotalMinutes % interval == 0 && (!MainForm.Currency_HandoutActiveStream.Checked && !MainForm.Currency_HandoutActiveTime.Checked || MainForm.Currency_HandoutActiveStream.Checked && ActiveUsers[user] >= g_iStreamStartTime || MainForm.Currency_HandoutActiveTime.Checked && Api.GetUnixTimeNow() - ActiveUsers[user] <= Convert.ToInt32(MainForm.Currency_HandoutLastActive.Value) * 60))
                                    {
                                        Console.Write(", passed the checks");
                                        int old = Database.checkCurrency(user);
                                        if (Database.isSubscriber(user) || spreadsheetSubs.Contains(user) || Api.IsSubscriber(user))
                                        {
                                            Database.addCurrency(user, subpayout);
                                            Console.Write(", got a point, old amount : " + old + ", new amount : " + Database.checkCurrency(user));
                                        }
                                        else
                                        {
                                            Database.addCurrency(user, payout);
                                            Console.Write(", got a point, old amount : " + old + ", new amount : " + Database.checkCurrency(user));
                                        }
                                    }
                                }*/
                            }
                        }).Start();
                    }
                }
            });
            Threads.Add(thread);
            thread.Name = "Time watched and currency handout";
            thread.Start();

            Console.Write("DONE\r\nUser list thread... ");

            thread = new Thread(() =>
            {
                while (true)
                {
                    buildUserList();
                    Thread.Sleep(10000);
                }
            });
            Threads.Add(thread);
            thread.Name = "User list";
            thread.Start();

            Console.Write("DONE\r\nStream status thread... ");

            thread = new Thread(() =>
            {
                while (true)
                {
                    bool bIsStreaming = false;
                    using (WebClient w = new WebClient())
                    {
                        w.Proxy = null;
                        string json_data = "";
                        try
                        {
                            json_data = w.DownloadString("https://api.twitch.tv/kraken/streams/" + channel.Substring(1));
                            JObject stream = JObject.Parse(json_data);
                            if (stream["stream"].HasValues)
                            {
                                if (!IsStreaming)
                                {
                                    g_iStreamStartTime = Api.GetUnixTimeNow();
                                }
                                bIsStreaming = true;
                            }
                        }
                        catch (Exception e)
                        {
                            if (e.Message.Contains("(404) Not Found."))
                            {
                                new Thread(() =>
                                {
                                    MainForm.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate
                                    {
                                        Console.WriteLine(MainForm.SettingsErrorLabel.Text = "Twitch reported that the channel was not found.");
                                        Disconnect();
                                        MainForm.ConnectButton.Enabled = false;
                                    });
                                }).Start();
                            }
                            /*else if (e.Message.Contains("(503) Server Unavailable"))
                            {
                                Console.WriteLine("Unable to connect to Twitch API to check stream status, retrying...");
                            }
                            else if (!e.Message.Contains("(503) Server Unavailable"))
                            {
                                Console.WriteLine("Unable to connect to Twitch API to check stream status, retrying...");
                                Api.LogError("*************Error Message (via checkStream()): " + DateTime.Now + "*********************************\r\n" + e + "\r\n");
                            }*/
                        }
                    }
                    IsStreaming = bIsStreaming;
                    Thread.Sleep(1000);
                    if (ResourceKeeper)
                    {
                        Thread.Sleep(29000);
                    }
                }
            });
            Threads.Add(thread);
            thread.Name = "Stream status";
            thread.Start();

            Console.Write("DONE\r\nConnection ping thread... ");

            //KeepAlive();
            thread = new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(30000);
                    sendRaw("PING 1245");
                }
            });
            Threads.Add(thread);
            thread.Name = "Connection ping";
            thread.Start();

            Console.Write("DONE\r\nCurrency check queue timer... ");

            if (currencyQueue == null)
            {
                currencyQueue = new Timer(handleCurrencyQueue, null, Timeout.Infinite, Timeout.Infinite);
            }
            else
            {
                currencyQueue.Change(Timeout.Infinite, Timeout.Infinite);
            }

            Console.Write("DONE\r\nAuction highest bidder timer... ");

            if (auctionLoop == null)
            {
                auctionLoop = new Timer(auctionLoopHandler, null, Timeout.Infinite, Timeout.Infinite);
            }
            else
            {
                auctionLoop.Change(Timeout.Infinite, Timeout.Infinite);
            }

            Console.Write("DONE\r\nGiveaway joining report timer... ");

            if (giveawayQueue == null)
            {
                giveawayQueue = new Timer(giveawayQueueHandler, null, Timeout.Infinite, Timeout.Infinite);
            }
            else
            {
                giveawayQueue.Change(Timeout.Infinite, Timeout.Infinite);
            }

            Console.Write("DONE\r\nWarnings removal timer... ");

            if (warningsRemoval == null)
            {
                warningsRemoval = new Timer(warningsRemovalHandler, null, 900000, 900000);
            }

            Console.Write("DONE\r\nLayout updating thread... ");

            thread = new Thread(() =>
            {
                MainForm.GrabData();
            });
            Threads.Add(thread);
            thread.Name = "Layout updating";
            thread.Start();

            Console.Write("DONE\r\nInput listening thread... ");

            //Listen();
            thread = new Thread(() =>
            {
                Console.WriteLine("Attempting to listen to input...");
                int attempt = 0;
                while (attempt < 5)
                {
                    attempt++;
                    Console.WriteLine((Running ? "Fix " : "Listening to input ") + "attempt number : " + attempt + "/5");
                    try
                    {
                        while (irc.Connected && Threads.Count > 0)
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
                    catch (Exception e)
                    {
                        if ((attempt == 0 || attempt == 5 && !Running) && Threads.Count > 0 && !e.Message.Contains("System.Threading.ThreadAbortException"))
                        {
                            if (attempt == 0)
                            {
                                Console.WriteLine("Uh oh, there was an error! Attempts to keep everything running are being executed, if the attempts fail or if you keep seeing this message, email your Error_log.txt file to DorCoMaNdO@gmail.com with the title \"ModBot - Error\" (Other titles will most likely be ignored).");
                            }
                            else
                            {
                                Console.WriteLine("Failed to listen to input... Please try restarting the bot... If this issue keeps occouring, please email your Error_log.txt file to DorCoMaNdO@gmail.com with the title \"ModBot - Error\" (Other titles will most likely be ignored).");
                            }
                            Api.LogError("*************Error Message (via Listen()): " + DateTime.Now + "*********************************\r\n" + e + "\r\n");
                        }
                    }
                    Thread.Sleep(500);
                }
                Running = false;
                //MainForm.Hide();
                Console.WriteLine("The attempts were unsuccessful... In order get ModBot back to function please restart it...");
                System.Windows.Forms.MessageBox.Show("ModBot has encountered an error, more information available in the console...", "ModBot", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            });
            Threads.Add(thread);
            thread.Name = "Input listening";
            thread.Start();

            Console.Write("DONE\r\nSuccessfully created and started all threads and timers!\r\n\r\n");
        }

        private static void warningsRemovalHandler(object state)
        {
            lock (Warnings)
            {
                Dictionary<string, int> warns = new Dictionary<string, int>();
                foreach (string user in Warnings.Keys)
                {
                    if (Warnings[user] > 1)
                    {
                        warns.Add(user, Warnings[user] - 1);
                    }
                }
                if (Warnings.Count > 0)
                {
                    Warnings = warns;
                    sendMessage("Users with warnings now have one less.");
                }
            }
        }

        private static void giveawayQueueHandler(object state)
        {
            if(Giveaway.Started && Giveaway.Open)
            {
                sendMessage("Total of " + Giveaway.Users.Count + " people joined the giveaway.");
            }
        }

        private static void parseMessage(string message)
        {
            //Console.WriteLine(message);
            string[] msg = message.Split(' ');
            string user;

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
                if (user == "Jtv")
                {
                    if (temp.StartsWith("HISTORYEND"))
                    {
                        Console.WriteLine("Everything should be set and ready!\r\nModBot is good to go!\r\n");
                        return;
                    }
                    else if (temp.StartsWith("You have banned"))
                    {
                        return;
                    }
                    else if (temp.StartsWith("The moderators of this room are: "))
                    {
                        lock (Moderators)
                        {
                            Moderators.Clear();
                            foreach (string mod in temp.Substring(33).Replace(" ", "").Split(','))
                            {
                                user = Api.capName(mod);
                                if (mod != "" && !Moderators.Contains(user))
                                {
                                    Moderators.Add(user);
                                }
                            }
                        }

                        MainForm.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate
                        {
                            MainForm.SpamFilterWindowButton.Enabled = Moderators.Contains(Api.capName(nick));
                        });
                        return;
                    }
                }
                string name = Api.GetDisplayName(user);
                Console.WriteLine(name + ": " + temp);
                //handleMessage(temp);
                if (IsModerator && MainForm.Spam_CWL.Checked)
                {
                    foreach (char character in temp)
                    {
                        if (!"()*&^%$@!'\"\\/.,?[]{}+_=-<>|:; ".Contains(character) && !MainForm.Spam_CWLBox.Text.ToLower().Contains(character.ToString().ToLower()))
                        {
                            warnUser(user, 1, 30, "Using a restricted character", 0, false, true, true);
                            return;
                        }
                    }
                }
                Commands.CheckCommand(name, temp, true);
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
            else if (msg[1].Equals("MODE"))
            {
                user = msg[4].ToLower();
                if (msg[3] == "+o")
                {
                    /*if (!Moderators.Contains(user))
                    {
                        Moderators.Add(user);
                    }*/
                    if (user == nick.ToLower())
                    {
                        IsModerator = true;
                    }
                }
                else if (msg[3] == "-o")
                {
                    /*if (Moderators.Contains(user))
                    {
                        Moderators.Remove(user);
                    }*/
                    if (user == nick.ToLower())
                    {
                        IsModerator = false;
                    }
                }
                buildUserList();
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
            if (args.Length > 0)
            {
                //ADMIN GIVEAWAY COMMANDS: !giveaway open <TicketCost> <MaxTickets>, !giveaway close, !giveaway draw, !giveaway cancel//
                if (Database.getUserLevel(user) >= 1)
                {
                    if (args[0].Equals("announce"))
                    {
                        /*string sMessage = "Get the party started! Viewers active in chat within the last " + Convert.ToInt32(MainForm.Giveaway_ActiveUserTime.Value) + " minutes ";
                        if (MainForm.Giveaway_MustFollowCheckBox.Checked)
                        {
                            if (!MainForm.Giveaway_MinCurrencyCheckBox.Checked)
                            {
                                sMessage = sMessage + "and follow the stream ";
                            }
                            else
                            {
                                sMessage = sMessage + "follow the stream, and have " + MainForm.Giveaway_MinCurrency.Value + " " + currencyName + " ";
                            }
                        }
                        else
                        {
                            sMessage = sMessage + "and have " + MainForm.Giveaway_MinCurrency.Value + " " + currencyName + " ";
                        }
                        sMessage = sMessage + "will qualify for the giveaway!";
                        sendMessage(sMessage);*/
                        sendMessage("Get the party started! Viewers active in chat within the last " + MainForm.Giveaway_ActiveUserTime.Value + " minutes" + (MainForm.Giveaway_MustFollowCheckBox.Checked ? MainForm.Giveaway_MinCurrencyCheckBox.Checked ? " follow the stream, and have " + MainForm.Giveaway_MinCurrency.Value + " " + currencyName : " and follow the stream" : "") + " will qualify for the giveaway!");
                    }

                    if (Database.getUserLevel(user) >= 2)
                    {
                        if (args[0].Equals("roll"))
                        {
                            if (Giveaway.Started)
                            {
                                if (!Giveaway.Open)
                                {
                                    string winner = Giveaway.getWinner();
                                    if (winner.Equals(""))
                                    {
                                        sendMessage("No valid winner found, please try again!");
                                    }
                                    else
                                    {
                                        TimeSpan t = Database.getTimeWatched(winner);
                                        sendMessage(winner + " has won the giveaway! (" + (Api.IsFollower(winner) ? "Currently follows the channel | " : "") + "Has " + Database.checkCurrency(winner) + " " + currencyName + " | Has watched the stream for " + t.Days + " days, " + t.Hours + " hours and " + t.Minutes + " minutes | Chance : " + Giveaway.Chance.ToString("0.00") + "%)");
                                    }
                                }
                                else
                                {
                                    sendMessage("The giveaway has to be closed first!");
                                }
                            }
                            else
                            {
                                sendMessage("No giveaway running!");
                            }
                        }
                        else if (args[0].Equals("type") && args.Length > 1)
                        {
                            if (!Giveaway.Started)
                            {
                                int type;
                                if (int.TryParse(args[1], out type) && type >= 1 && type <= 3)
                                {
                                    MainForm.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate
                                    {
                                        MainForm.Giveaway_TypeActive.Checked = (type == 1);
                                        MainForm.Giveaway_TypeKeyword.Checked = (type == 2);
                                        MainForm.Giveaway_TypeTickets.Checked = (type == 3);
                                    });
                                    sendMessage("Giveaway type changed!");
                                }
                            }
                            else
                            {
                                sendMessage("Can't change giveaway type while a giveaway is running!");
                            }
                        }
                        else if (args[0].Equals("start") || args[0].Equals("create") || args[0].Equals("run"))
                        {
                            if (!Giveaway.Started)
                            {
                                if(MainForm.Giveaway_TypeTickets.Checked)
                                {
                                    int ticketcost = 0, maxtickets = 1;
                                    if(args.Length > 1)
                                    {
                                        int.TryParse(args[1], out ticketcost);
                                        if (args.Length > 2)
                                        {
                                            int.TryParse(args[2], out maxtickets);
                                        }
                                    }
                                    if (ticketcost >= 0 && maxtickets > 0)
                                    {
                                        Giveaway.startGiveaway(ticketcost, maxtickets);;
                                    }
                                    else
                                    {
                                        sendMessage("Ticket cost cannot be lower than 0 and max tickets cannot be lower than 1.");
                                    }
                                }
                                else
                                {
                                    Giveaway.startGiveaway();
                                }
                            }
                            else
                            {
                                sendMessage("A giveaway is already running.");
                            }
                        }
                        else if (args[0].Equals("close") || args[0].Equals("lock"))
                        {
                            if (Giveaway.Started)
                            {
                                if (Giveaway.Open)
                                {
                                    giveawayQueue.Change(0, Timeout.Infinite);
                                    Giveaway.closeGiveaway();
                                }
                                else
                                {
                                    sendMessage("Entries to the giveaway has been closed already.");
                                }
                            }
                            else
                            {
                                sendMessage("A giveaway is not running.");
                            }
                        }
                        else if (args[0].Equals("open") || args[0].Equals("unlock"))
                        {
                            if (Giveaway.Started)
                            {
                                if (!Giveaway.Open)
                                {
                                    Giveaway.openGiveaway();
                                }
                                else
                                {
                                    sendMessage("Entries to the giveaway are already open.");
                                }
                            }
                            else
                            {
                                sendMessage("A giveaway is not running.");
                            }
                        }
                        else if (args[0].Equals("stop") || args[0].Equals("end"))
                        {
                            if (Giveaway.Started)
                            {
                                Giveaway.endGiveaway();
                            }
                            else
                            {
                                sendMessage("A giveaway is not running.");
                            }
                        }
                        else if (args[0].Equals("cancel") || args[0].Equals("abort"))
                        {
                            if (Giveaway.Started)
                            {
                                Giveaway.cancelGiveaway();
                            }
                        }
                    }
                }

                //REGULAR USER COMMANDS: !giveaway help
                /*if (args[0].Equals("help"))
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
                            sMessage = sMessage + ", follow the stream and have " + MainForm.Giveaway_MinCurrency.Value + " " + currencyName + ", ";
                        }
                    }
                    else
                    {
                        sMessage = sMessage + "and have " + MainForm.Giveaway_MinCurrency.Value + " " + currencyName + ", ";
                    }
                    sMessage = sMessage + "the winner is selected from a list of viewers that were active in the last " + MainForm.Giveaway_ActiveUserTime.Value + " minutes";
                    if (MainForm.Giveaway_MustFollowCheckBox.Checked || MainForm.Giveaway_MinCurrencyCheckBox.Checked) sMessage = sMessage + " and comply the terms";
                    sMessage = sMessage + ".";
                    sendMessage(sMessage);
                }*/

                if ((args[0].Equals("buy") || args[0].Equals("join") || args[0].Equals("purchase") || args[0].Equals("ticket") || args[0].Equals("tickets")) && args.Length > 1)
                {
                    Command_Tickets(user, "!ticket", new string[] { args[1] });
                }
            }
            else
            {
                Command_Tickets(user, "!ticket", args);
            }
        }

        private static void Command_Tickets(string user, string cmd, string[] args)
        {
            if (Giveaway.Started && (MainForm.Giveaway_TypeKeyword.Checked || MainForm.Giveaway_TypeTickets.Checked))
            {
                if (Giveaway.Open)
                {
                    if (MainForm.Giveaway_TypeKeyword.Checked)
                    {
                        if (!Giveaway.HasBoughtTickets(user))
                        {
                            if (Giveaway.BuyTickets(user))
                            {
                                giveawayQueue.Change(5000, Timeout.Infinite);
                            }
                        }
                        else
                        {
                            warnUser(user, 1, 5, "Giveaway entries closed and/or is in the giveaway already.", 0, false);
                        }
                    }
                    else
                    {
                        if (args.Length > 0)
                        {
                            int tickets;
                            if (int.TryParse(args[0], out tickets) && tickets > 0 && Giveaway.BuyTickets(user, tickets))
                            {
                                giveawayQueue.Change(5000, Timeout.Infinite);
                            }
                            else if (Moderators.Contains(Api.capName(nick)))
                            {
                                if (!warnUser(user, 1, 10, "Attempting to buy tickets with insufficient funds and/or invalid parameters") && Warnings.ContainsKey(Api.capName(user))) sendMessage(user + " you have insufficient " + currencyName + ", you don't answer the requirements or the tickets amount you put is invalid. (Warning number: " + Warnings[Api.capName(user)] + "/3) Ticket cost: " + Giveaway.Cost + ", max. tickets: " + Giveaway.MaxTickets + ".");
                            }
                        }
                    }
                }
                else
                {
                    warnUser(user, 1, 5, "Giveaway entries closed and/or is in the giveaway already.", 0, false);
                }
            }
        }

        private static void Command_Currency(string user, string cmd, string[] args)
        {
            if (args.Length > 0)
            {
                if (args.Length == 1)
                {
                    if (args[0].Equals("top5"))
                    {
                        if (!MainForm.Currency_DisableCommandCheckBox.Checked && Api.GetUnixTimeNow() - g_iLastTop5Announce > 600 || Database.getUserLevel(user) >= 1)
                        {
                            g_iLastTop5Announce = Api.GetUnixTimeNow();
                            int max = 5;
                            Dictionary<string, int> TopPoints = new Dictionary<string, int>();
                            //"SELECT * FROM table ORDER BY amount DESC LIMIT 5;"
                            List<string> users = new List<string>();
                            using (SQLiteCommand query = new SQLiteCommand("SELECT * FROM '" + channel.Substring(1) + "' ORDER BY currency DESC LIMIT " + (max + IgnoredUsers.Count) + ";", Database.DB))
                            {
                                using (SQLiteDataReader r = query.ExecuteReader())
                                {
                                    while (r.Read())
                                    {
                                        string usr = Api.capName(r["user"].ToString());
                                        if (!IgnoredUsers.Any(c => c.Equals(usr.ToLower())) && !TopPoints.ContainsKey(usr))
                                        {
                                            TopPoints.Add(usr, int.Parse(r["currency"].ToString()));
                                        }
                                    }
                                }
                            }
                            //TopPoints = TopPoints.OrderByDescending(key => key.Value).ToDictionary(item => item.Key, item => item.Value);
                            if (TopPoints.Count > 0)
                            {
                                string output = "";
                                if (TopPoints.Count < max)
                                {
                                    max = TopPoints.Count;
                                }
                                for (int i = 0; i < max; i++)
                                {
                                    output += Api.GetDisplayName(TopPoints.ElementAt(i).Key) + " (" + Database.getTimeWatched(TopPoints.ElementAt(i).Key).ToString(@"d\d\ hh\h\ mm\m") + ") - " + TopPoints.ElementAt(i).Value + ", ";
                                }
                                sendMessage("The " + max + " users with the most points are: " + output.Substring(0, output.Length - 2) + ".");
                            }
                            else
                            {
                                sendMessage("An error has occoured while looking for the 5 users with the most points! Try again later.");
                            }
                        }
                    }
                    else if ((args[0].Equals("lock") || args[0].Equals("disable")) && Database.getUserLevel(user) >= 2)
                    {
                        MainForm.Currency_DisableCommandCheckBox.Checked = true;
                        sendMessage("The !" + currency + " command is now disabled.", user + " disabled the currency command.");
                    }
                    else if ((args[0].Equals("unlock") || args[0].Equals("enable")) && Database.getUserLevel(user) >= 2)
                    {
                        MainForm.Currency_DisableCommandCheckBox.Checked = false;
                        sendMessage("The !" + currency + " command is now available to use.", user + " enabled the currency command.");
                    }
                    else if (args[0].Equals("clear") && Database.getUserLevel(user) >= 3)
                    {
                        foreach (string usr in Database.GetAllUsers())
                        {
                            Database.setCurrency(usr, 0);
                        }
                        sendMessage("Cleared all the " + currencyName + "!", user + " cleared all the " + currencyName + "!");
                    }
                    else
                    {
                        if (Database.getUserLevel(user) >= 1)
                        {
                            if (!args[0].Contains(","))
                            {
                                if (Database.userExists(args[0]))
                                {
                                    sendMessage("Mod check: " + Api.GetDisplayName(args[0], true) + " (" + Database.getTimeWatched(args[0]).ToString(@"d\d\ hh\h\ mm\m") + ")" + " has " + Database.checkCurrency(args[0]) + " " + currencyName);
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
                                sendMessage("Added " + amount + " " + currencyName + " to everyone.", user + " added " + amount + " " + currencyName + " to everyone.");
                            }
                            else if (args[2].Equals("online"))
                            {
                                foreach (string usr in ActiveUsers.Keys)
                                {
                                    Database.addCurrency(usr, amount);
                                }
                                sendMessage("Added " + amount + " " + currencyName + " to online users.", user + " added " + amount + " " + currencyName + " to online users.");
                            }
                            else
                            {
                                Database.addCurrency(args[2], amount);
                                sendMessage("Added " + amount + " " + currencyName + " to " + Api.GetDisplayName(args[2]), user + " added " + amount + " " + currencyName + " to " + Api.GetDisplayName(args[2]));
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
                                sendMessage("Set everyone's " + currencyName + " to " + amount + ".", user + " set everyone's " + currencyName + " to " + amount + ".");
                            }
                            else if (args[2].Equals("online"))
                            {
                                foreach (string usr in ActiveUsers.Keys)
                                {
                                    Database.setCurrency(usr, amount);
                                }
                                sendMessage("Set online users's " + currencyName + " to " + amount + ".", user + " set online users's " + currencyName + " to " + amount + ".");
                            }
                            else
                            {
                                Database.setCurrency(args[2], amount);
                                sendMessage("Set " + Api.capName(args[2]) + "'s " + currencyName + " to " + amount + ".", user + " set " + Api.capName(args[2]) + "'s " + currencyName + " to " + amount + ".");
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
                                sendMessage("Removed " + amount + " " + currencyName + " from everyone.", user + " removed " + amount + " " + currencyName + " from everyone.");
                            }
                            else if (args[2].Equals("online"))
                            {
                                foreach (string usr in ActiveUsers.Keys)
                                {
                                    Database.removeCurrency(usr, amount);
                                }
                                sendMessage("Removed " + amount + " " + currencyName + " from online users.", user + " removed " + amount + " " + currencyName + " from online users.");
                            }
                            else
                            {
                                Database.removeCurrency(args[2], amount);
                                sendMessage("Removed " + amount + " " + currencyName + " from " + Api.capName(args[2]), user + " removed " + amount + " " + currencyName + " from " + Api.capName(args[2]));
                            }

                        }
                    }
                }
            }
            else
            {
                if (MainForm.Currency_DisableCommandCheckBox.Checked && Database.getUserLevel(user) == 0 && Api.GetUnixTimeNow() - g_iLastCurrencyDisabledAnnounce > 600)
                {
                    g_iLastCurrencyDisabledAnnounce = Api.GetUnixTimeNow();
                    sendMessage("The !" + currency + " command is disabled, you may politely ask a mod to check your " + currencyName + " for you.");
                }
                if (!MainForm.Currency_DisableCommandCheckBox.Checked || Database.getUserLevel(user) >= 1)
                {
                    addToLookups(user);
                }
            }
        }

        private static void Command_Gamble(string user, string cmd, string[] args)
        {
            if (Database.getUserLevel(user) >= 2)
            {
                if (args[0].Equals("open") && args.Length >= 4)
                {
                    if (!Pool.Running)
                    {
                        int maxBet;
                        if (int.TryParse(args[1], out maxBet))
                        {
                            if (maxBet > 0)
                            {
                                List<string> Options = buildBetOptions(args);
                                if (Options.Count > 1)
                                {
                                    Pool.CreatePool(maxBet, Options);
                                    sendMessage("New Betting Pool opened!  Max bet = " + maxBet + " " + currencyName);
                                    string temp = "Betting open for: ";
                                    for (int i = 0; i < Options.Count; i++)
                                    {
                                        temp += "(" + (i + 1).ToString() + ") " + Options[i] + " ";
                                    }
                                    sendMessage(temp);
                                    sendMessage("Bet by typing \"!bet 50 option1name\" to bet 50 " + currencyName + " on option 1, \"!bet 25 option2name\" to bet 25 " + currencyName + " on option 2, etc. You can also bet with \"!bet 10 #OptionNumber\"");
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
                    if (Pool.Running)
                    {
                        if (!Pool.Locked)
                        {
                            Pool.Locked = true;
                            sendMessage("Bets locked in. Good luck everyone!");
                            string temp = "The following options were open for betting: ";
                            for (int i = 0; i < Pool.options.Count; i++)
                            {
                                temp += "(" + (i + 1).ToString() + ") " + Pool.options[i] + " - " + Pool.getNumberOfBets(Pool.options[i]) + " bets (" + Pool.getTotalBetsOn(Pool.options[i]) + " " + currencyName + " bet)";
                                if (i + 1 < Pool.options.Count)
                                {
                                    temp += ", ";
                                }
                            }
                            sendMessage(temp);
                        }
                        else
                        {
                            sendMessage("Pool is already locked.");
                        }
                    }
                    else
                    {
                        sendMessage("The betting pool is not running.");
                    }
                }
                else if (args[0].Equals("winner") && args.Length >= 2)
                {
                    if (Pool.Running && Pool.Locked)
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
                                int optionnumber = 0;
                                if (int.TryParse(option.Substring(1), out optionnumber))
                                {
                                    option = Pool.GetOptionFromNumber(optionnumber);
                                }
                            }
                        }
                        if (Pool.options.Contains(option))
                        {
                            Pool.closePool(option);
                            sendMessage("Betting Pool closed! A total of " + Pool.getTotalBets() + " " + currencyName + " were bet.");
                            string output = "Bets for:";
                            for (int i = 0; i < Pool.options.Count; i++)
                            {
                                double x = ((double)Pool.getTotalBetsOn(Pool.options[i]) / Pool.getTotalBets()) * 100;
                                output += " " + Pool.options[i] + " - " + Pool.getNumberOfBets(Pool.options[i]) + " (" + Math.Round(x) + "%);";
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

                            Pool.Clear();
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
                        sendMessage("Betting Pool canceled. All bets refunded");
                    }
                    else
                    {
                        sendMessage("The betting pool is not running.");
                    }
                }
            }
        }

        private static void Command_Bet(string user, string cmd, string[] args)
        {
            if (Pool.Running)
            {
                if (args.Length > 0)
                {
                    int betAmount;
                    if (args[0].Equals("help"))
                    {
                        if (!Pool.Locked)
                        {
                            string temp = "Betting open for: ";
                            for (int i = 0; i < Pool.options.Count; i++)
                            {
                                temp += "(" + (i + 1).ToString() + ") " + Pool.options[i];
                                if(i + 1 < Pool.options.Count)
                                {
                                    temp += ", ";
                                }
                            }
                            sendMessage(temp);
                            sendMessage("Bet by typing \"!bet 50 option1name\" to bet 50 " + currencyName + " on option 1, \"bet 25 option2name\" to bet 25 " + currencyName + " on option 2, etc. You can also bet with \"!bet 10 #OptionNumber\".");
                        }
                        else
                        {
                            string temp = "The pool is now closed, the following options were open for betting: ";
                            for (int i = 0; i < Pool.options.Count; i++)
                            {
                                temp += "(" + (i + 1).ToString() + ") " + Pool.options[i] + " - " + Pool.getNumberOfBets(Pool.options[i]) + " bets (" + Pool.getTotalBetsOn(Pool.options[i]) + " " + currencyName + " bet)";
                                if (i + 1 < Pool.options.Count)
                                {
                                    temp += ", ";
                                }
                            }
                            sendMessage(temp);
                        }
                    }
                    else if (!Pool.Locked && int.TryParse(args[0], out betAmount) && args.Length >= 2)
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
                                int optionnumber = 0;
                                if (int.TryParse(option.Substring(1), out optionnumber))
                                {
                                    option = Pool.GetOptionFromNumber(optionnumber);
                                    if (option == "")
                                    {
                                        sendMessage(user + " the option number does not exist");
                                        return;
                                    }
                                }
                            }
                        }
                        if (Pool.placeBet(user, option, betAmount))
                        {
                            sendMessage(user + " has placed a " + betAmount + " " + currencyName + " bet on \"" + option + "\"");
                        }
                    }
                }
                else
                {
                    if (Pool.isInPool(user))
                    {
                        sendMessage(user + ": " + Pool.getBetOn(user) + " (" + Pool.getBetAmount(user) + ")");
                    }
                }
            }
        }

        private static void Command_Auction(string user, string cmd, string[] args)
        {
            if (args.Length > 0 && Database.getUserLevel(user) >= 2)
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
            if (args.Length > 0)
            {
                int amount;
                if (int.TryParse(args[0], out amount))
                {
                    if (auctionOpen)
                    {
                        if (Auction.placeBid(user, amount))
                        {
                            auctionLoop.Change(0, 30000);
                        }
                    }
                }
            }
        }

        private static void Command_BTag(string user, string cmd, string[] args)
        {
            if (args.Length > 0 && args[0].Contains("#"))
            {
                Database.setBtag(user, args[0]);
            }
        }

        private static void Command_ModBot(string user, string cmd, string[] args)
        {
            if (args.Length >= 0)
            {
                if (Database.getUserLevel(user) >= 4)
                {
                    if (args.Length >= 2)
                    {
                        if (args[0].Equals("payout"))
                        {
                            int amount = 0;
                            if (int.TryParse(args[1], out amount) && amount >= MainForm.CurrencyHandoutAmount.Minimum && amount <= MainForm.CurrencyHandoutAmount.Maximum)
                            {
                                payout = amount;
                                sendMessage("New payout amount: " + amount);
                            }
                            else
                            {
                                sendMessage("Can't change payout amount. Must be a valid integer greater than " + (MainForm.CurrencyHandoutAmount.Minimum - 1) + " and less than " + (MainForm.CurrencyHandoutAmount.Maximum + 1));
                            }
                        }
                        else if (args[0].Equals("subpayout"))
                        {
                            int amount = 0;
                            if (int.TryParse(args[1], out amount) && amount >= MainForm.CurrencySubHandoutAmount.Minimum && amount <= MainForm.CurrencySubHandoutAmount.Maximum)
                            {
                                payout = amount;
                                sendMessage("New subscribers' payout amount: " + amount);
                            }
                            else
                            {
                                sendMessage("Can't change subscribers' payout amount. Must be a valid integer greater than " + (MainForm.CurrencySubHandoutAmount.Minimum - 1) + " and less than " + (MainForm.CurrencySubHandoutAmount.Maximum + 1));
                            }
                        }
                        else if (args[0].Equals("interval"))
                        {
                            int tempInterval = -1;
                            if (int.TryParse(args[1], out tempInterval) && tempInterval >= MainForm.CurrencyHandoutInterval.Minimum && tempInterval <= MainForm.CurrencyHandoutInterval.Maximum)
                            {
                                interval = tempInterval;
                                sendMessage("New currency payout interval: " + tempInterval);
                            }
                            else
                            {
                                sendMessage("Payout interval could not be changed. A valid interval must be greater than " + (MainForm.CurrencyHandoutInterval.Minimum - 1) + " and less than " + (MainForm.CurrencyHandoutInterval.Maximum + 1) + " minutes.");
                            }
                        }
                        else if (args[0].Equals("greeting") || args[0].Equals("greetings"))
                        {
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
                        else if (args[0].Equals("addsub"))
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
                        else if (args[0].Equals("removesub") || args[0].Equals("delsub") || args[0].Equals("deletesub"))
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
                }
                if (Database.getUserLevel(user) >= 3)
                {
                    if (args.Length >= 2)
                    {
                        if (args[0].Equals("addmod"))
                        {
                            string tNick = Api.GetDisplayName(args[1]);
                            if (Database.userExists(tNick))
                            {
                                if (!tNick.Equals(admin, StringComparison.OrdinalIgnoreCase) && (Database.getUserLevel(tNick) < 3 && Database.getUserLevel(user) == 3 || Database.getUserLevel(user) >= 4))
                                {
                                    Database.setUserLevel(tNick, 1);
                                    sendMessage(tNick + " added as a bot moderator.", user + " added " + tNick + "as a bot moderator.");
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
                        if (args[0].Equals("addsuper"))
                        {
                            string tNick = Api.GetDisplayName(args[1]);
                            if (Database.userExists(tNick))
                            {
                                if (!tNick.Equals(admin, StringComparison.OrdinalIgnoreCase) && (Database.getUserLevel(tNick) < 3 && Database.getUserLevel(user) == 3 || Database.getUserLevel(user) >= 4))
                                {
                                    Database.setUserLevel(tNick, 2);
                                    sendMessage(tNick + " added as a bot Super Mod.", user + " added " + tNick + "as a super bot moderator.");
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
                        if (args[0].Equals("demote"))
                        {
                            string tNick = Api.GetDisplayName(args[1]);
                            if (Database.userExists(tNick))
                            {
                                if (Database.getUserLevel(tNick) > 0)
                                {
                                    if (!tNick.Equals(admin, StringComparison.OrdinalIgnoreCase) && (Database.getUserLevel(tNick) < 3 && Database.getUserLevel(user) == 3 || Database.getUserLevel(user) >= 4))
                                    {
                                        Database.setUserLevel(tNick, Database.getUserLevel(tNick) - 1);
                                        sendMessage(tNick + " has been demoted.", user + "demoted " + tNick);
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
                                        sendMessage(tNick + " set to Access Level " + level, user + "set " + tNick + "'s Access Level to " + level);
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
                }
                if (Database.getUserLevel(user) >= 2)
                {
                    if ((args[0].Equals("addcommand") || args[0].Equals("addcmd")) && args.Length >= 4)
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
                                sendMessage(command + " command added.", user + " added the command " + command);
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
                    else if ((args[0].Equals("removecommand") || args[0].Equals("removecmd") || args[0].Equals("delcmd") || args[0].Equals("deletecmd") || args[0].Equals("deletecmd") || args[0].Equals("deletecommand")) && args.Length >= 2)
                    {
                        string command = args[1].ToLower();
                        if (Commands.cmdExists(command))
                        {
                            Commands.removeCommand(command);
                            sendMessage(command + " command removed.", user + " removed the command " + command);
                        }
                        else
                        {
                            sendMessage(command + " command does not exist.");
                        }
                    }
                }
                if (Database.getUserLevel(user) >= 1)
                {
                    if (args[0].Equals("commmandlist") || args[0].Equals("cmdlist") || args[0].Equals("commmandslist") || args[0].Equals("cmdslist") || args[0].Equals("cmds") || args[0].Equals("commands"))
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

        private static void addUserToList(string user)
        {
            user = Api.capName(user);
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
        }

        public static bool IsUserInList(string user)
        {
            user = Api.capName(user);
            lock (ActiveUsers)
            {
                if (ActiveUsers.ContainsKey(user))
                {
                    return true;
                }
            }
            return false;
        }

        private static void removeUserFromList(string user)
        {
            user = Api.capName(user);
            lock (ActiveUsers)
            {
                if (ActiveUsers.ContainsKey(user))
                {
                    ActiveUsers.Remove(user);
                }
            }
        }

        public static void buildUserList()
        {
            //sendRaw("WHO " + channel);
            Thread thread = new Thread(() =>
            {
                sendMessage("/mods", "", false, false);

                using (WebClient w = new WebClient())
                {
                    string json_data = "";
                    try
                    {
                        json_data = w.DownloadString("http://tmi.twitch.tv/group/user/" + channel.Substring(1) + "/chatters");
                        //users.Clear();
                        if (json_data.Replace("\"", "") != "")
                        {
                            JObject stream = JObject.Parse(JObject.Parse(json_data)["chatters"].ToString());
                            /*string[] sMods = stream["moderators"].ToString().Replace(" ", "").Replace("\"", "").Replace("\r\n", "").Replace("[", "").Replace("]", "").Split(',');
                            lock (Moderators)
                            {
                                Moderators.Clear();
                                foreach (string sMod in sMods)
                                {
                                    string user = Api.capName(sMod);
                                    if (sMod != "" && !Moderators.Contains(user))
                                    {
                                        Moderators.Add(user);
                                    }
                                }
                            }*/

                            string[] sUsers = (stream["moderators"].ToString().Replace(" ", "").Replace("\"", "").Replace("\r\n", "").Replace("[", "").Replace("]", "") + "," + stream["staff"].ToString().Replace(" ", "").Replace("\"", "").Replace("\r\n", "").Replace("[", "").Replace("]", "") + "," + stream["admins"].ToString().Replace(" ", "").Replace("\"", "").Replace("\r\n", "").Replace("[", "").Replace("]", "") + "," + stream["viewers"].ToString().Replace(" ", "").Replace("\"", "").Replace("\r\n", "").Replace("[", "").Replace("]", "")).Split(',');
                            lock (ActiveUsers)
                            {
                                foreach (string sUser in sUsers)
                                {
                                    Api.GetDisplayName(sUser);
                                    string user = Api.capName(sUser);
                                    if (sUser != "" && !ActiveUsers.ContainsKey(user))
                                    {
                                        addUserToList(user);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Api.LogError("*************Error Message (via buildUserList()): " + DateTime.Now + "*********************************\r\nUnable to connect to Twitch API to build the user list.\r\n" + e + "\r\n");
                    }
                }
            });
            Threads.Add(thread);
            thread.Name = "User updating";
            thread.Start();
            thread.Join();
            if (Threads.Contains(thread)) Threads.Remove(thread);
        }

        private static string getUser(string message)
        {
            return Api.capName(message.Split('!')[0].Substring(1));
        }

        private static void sendRaw(string message)
        {
            for (int attempt = 1; attempt <= 5; attempt++)
            {
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

        public static void sendMessage(string message, string log = "", bool logtoconsole = true, bool usemecommand = true)
        {
            sendRaw("PRIVMSG " + channel + " :" + (usemecommand ? "/me " : "") + message);
            if (log != "")
            {
                Log(log);
            }
            if (logtoconsole)
            {
                Console.WriteLine(nick + ": " + message);
            }
        }

        private static bool warnUser(string user, int add = 1, int rate = 5, string reason = "", int max = 3, bool announcewarns = true, bool console = true, bool chat = true)
        {
            string name = Api.GetDisplayName(user = Api.capName(user));
            if (!Moderators.Contains(user) && !IgnoredUsers.Contains(user) && Database.getUserLevel(user) == 0)
            {
                lock (Warnings)
                {
                    if (Warnings.Count == 0)
                    {
                        warningsRemoval.Change(900000, 900000);
                    }

                    if (!Warnings.ContainsKey(user))
                    {
                        Warnings.Add(user, add);
                    }
                    else
                    {
                        Warnings[user] += add;
                    }
                    if (Warnings[user] > max)
                    {
                        timeoutUser(name, Warnings[user] * rate, (reason != "" ? reason + " " : "") + (announcewarns ? "after " + max + " warnings." : ""), console, chat);
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool timeoutUser(string user, int interval=10, string reason="", bool console = true, bool chat = true)
        {
            user = user.ToLower();
            if (!IsModerator || Moderators.Contains(Api.capName(user)) || IgnoredUsers.Contains(user) || Database.getUserLevel(user) > 0) return false;

            //sendRaw("PRIVMSG " + channel + " :/timeout " + user + " " + interval);
            sendMessage("/timeout " + user + " " + interval, "", false, false);
            user = Api.GetDisplayName(user);
            if (chat)
            {
                sendMessage(user + " has been timed out for " + interval + " seconds." + (reason != "" ? " Reason: " + reason : ""), "", false);
            }
            if (console)
            {
                Console.WriteLine(user + " has been timed out for " + interval + " seconds." + (reason != "" ? " Reason: " + reason : ""));
            }
            Log(user + " has been timed out for " + interval + " seconds." + (reason != "" ? " Reason: " + reason : ""));
            return true;
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

        private static List<string> buildBetOptions(string[] temp)
        {
            List<string> betOptions = new List<string>();
            try
            {
                lock (betOptions)
                {
                    bool inQuote = false;
                    string option = "";
                    for (int i = 2; i < temp.Length; i++)
                    {
                        if (temp[i].StartsWith("\""))
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
            //catch (Exception e)
            catch
            {
                //Console.WriteLine(e.ToString());
                //Api.LogError("*************Error Message (via buildBetOptions()): " + DateTime.Now + "*********************************\r\n" + e + "\r\n");
            }
            return betOptions;
        }

        private static void addToLookups(string user)
        {
            if (!usersToLookup.Contains(user))
            {
                currencyQueue.Change(5000, Timeout.Infinite);
                usersToLookup.Add(user);
            }
        }

        private static void handleCurrencyQueue(Object state)
        {
            if (usersToLookup.Count > 0)
            {
                string output = currencyName + ":";
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
                        if (Pool.Running && Pool.isInPool(person))
                        {
                            output += " [" + Pool.getBetAmount(person) + "]";
                        }
                        if (auctionOpen && Auction.highBidder.Equals(person))
                        {
                            output += " {" + Auction.highBid + "}";
                        }
                        addComma = true;
                    }
                }
                usersToLookup.Clear();
                sendMessage(output);
            }
        }

        private static void auctionLoopHandler(Object state)
        {
            if (auctionOpen)
            {
                sendMessage(Api.GetDisplayName(Auction.highBidder) + " is currently winning, with a bid of " + Auction.highBid + "!");
            }
        }

        private static void Log(string output)
        {
            for (int attempts = 0; attempts < 5; attempts++)
            {
                try
                {
                    log.WriteLine("[" + DateTime.Now + "] " + output);
                    break;
                }
                catch (IOException)
                {
                    System.Threading.Thread.Sleep(100);
                }
                catch (Exception e)
                {
                    //Console.WriteLine(e);
                    Api.LogError("*************Error Message (via Log()): " + DateTime.Now + "*********************************\r\n" + e + "\r\n");
                    break;
                }
            }
        }
    }
}
