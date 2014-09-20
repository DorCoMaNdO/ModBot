using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace ModBot
{
    public partial class MainWindow : CustomForm
    {
        private iniUtil ini = Program.ini;
        public Dictionary<string, Dictionary<string, string>> dSettings = new Dictionary<string, Dictionary<string, string>>();
        private bool bIgnoreUpdates, MetadataModified;
        public int iSettingsPresent = -2;
        //private bool g_bLoaded = false;
        public Dictionary<CheckBox, Panel> Windows = new Dictionary<CheckBox, Panel>();
        public Panel CurrentWindow = null;
        private List<Thread> Threads = new List<Thread>();
        private string AuthenticationScopes;
        public string ChannelTitle, ChannelGame;
        public Dictionary<string, string> SubscriptionRewards = new Dictionary<string, string>();

        public MainWindow()
        {
            InitializeComponent();

            /*foreach (Control ctrl in Controls)
            {
                ctrl.TabStop = false;
            }*/

            Text = "ModBot v" + (VersionLabel.Text = "Version: " + Assembly.GetExecutingAssembly().GetName().Version).Substring(9);

            Thread thread = new Thread(() =>
            {
                string Hash;
                using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
                {
                    using (FileStream stream = File.OpenRead(AppDomain.CurrentDomain.FriendlyName))
                    {
                        Hash = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower();
                    }
                }

                while (true)
                {
                    using (WebClient w = new WebClient())
                    {
                        w.Proxy = null;
                        try
                        {
                            // Thanks to Illarvan for giving me some space on his server!
                            List<string> channels;
                            string url = "http://ddoguild.co.uk/modbot/streams/";
                            if (Irc.DetailsConfirmed)
                            {
                                int iViewers = Irc.ActiveUsers.Count;
                                foreach (string user in Irc.ActiveUsers.Keys)
                                {
                                    if (Irc.IgnoredUsers.Contains(user.ToLower()))
                                    {
                                        iViewers--;
                                    }
                                }
                                url = "http://ddoguild.co.uk/modbot/streams/?channel=" + Irc.channel.Substring(1) + "&bot=" + Irc.nick + "&hash=" + Hash + "&version=" + Assembly.GetExecutingAssembly().GetName().Version + "&viewers=" + iViewers + "&date=" + new DateTime(2000, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc).AddDays(Assembly.GetExecutingAssembly().GetName().Version.Build).AddSeconds(Assembly.GetExecutingAssembly().GetName().Version.Revision * 2).ToString("M/dd/yyyy hh:mm:ss tt") + "&status=" + (Irc.IsStreaming ? "2" : "1") + "&title=" + ChannelTitle + "&game=" + ChannelGame;
                            }

                            channels = w.DownloadString(url).Replace("<pre>", "").Replace("</pre>", "").Split(Environment.NewLine.ToCharArray()).ToList();
                            Dictionary<Tuple<string, string, string, string, string>, Tuple<string, string, int>> Channels = new Dictionary<Tuple<string, string, string, string, string>, Tuple<string, string, int>>();
                            foreach (string channel in channels)
                            {
                                if (channel != "")
                                {
                                    JObject json = JObject.Parse(channel);
                                    int status = (int)json["Status"], updated = (int)json["Time"];
                                    string sStatus = "Disconnected";
                                    if (Api.GetUnixTimeNow() - updated < 300)
                                    {
                                        if (status == 2)
                                        {
                                            sStatus = "On air";
                                        }
                                        else
                                        {
                                            sStatus = "Off air";
                                        }
                                    }
                                    //Channels.Add(new Tuple<string, string, string, int, string>(JObject.Parse(w.DownloadString("https://api.twitch.tv/kraken/users/" + json["Channel"].ToString()))["display_name"].ToString(), sStatus, json["Version"].ToString(), int.Parse(json["Viewers"].ToString()), new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc).AddSeconds(updated).ToString()));
                                    Channels.Add(new Tuple<string, string, string, string, string>(json["Channel"].ToString(), json["Bot"].ToString(), sStatus, json["Version"].ToString(), new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc).AddSeconds(updated).ToLocalTime().ToString()), new Tuple<string, string, int>(json["Title"].ToString(), json["Game"].ToString(), int.Parse(json["Viewers"].ToString())));
                                }
                            }
                            while (!IsHandleCreated) Thread.Sleep(100);
                            BeginInvoke((MethodInvoker)delegate
                            {
                                foreach (Tuple<string, string, string, string, string> channel in Channels.Keys)
                                {
                                    bool Found = false;
                                    foreach (DataGridViewRow row in About_Users.Rows)
                                    {
                                        if (row.Cells["Channel"].Value.ToString() == channel.Item1 && row.Cells["Bot"].Value.ToString() == channel.Item2)
                                        {
                                            row.Cells["Status"].Value = channel.Item3;
                                            row.Cells["Version"].Value = channel.Item4;
                                            row.Cells["Updated"].Value = channel.Item5;
                                            row.Cells["Title"].Value = Channels[channel].Item1;
                                            row.Cells["Game"].Value = Channels[channel].Item2;
                                            row.Cells["Viewers"].Value = Channels[channel].Item3;
                                            Found = true;
                                            break;
                                        }
                                    }
                                    if (!Found)
                                    {
                                        About_Users.Rows.Add(channel.Item1, channel.Item2, Channels[channel].Item1, Channels[channel].Item2, channel.Item3, Channels[channel].Item3, channel.Item4, channel.Item5);
                                    }
                                }

                                About_UsersLabel.Text = "Other users (" + About_Users.Rows.Count + " total):";

                                About_Users.Sort(About_Users.SortedColumn, About_Users.SortOrder == SortOrder.Ascending ? System.ComponentModel.ListSortDirection.Ascending : System.ComponentModel.ListSortDirection.Descending);
                            });
                        }
                        catch
                        {
                        }
                    }
                    Thread.Sleep(60000);
                }
            });
            thread.Name = "Status reporting";
            thread.Start();
            Threads.Add(thread);

            About_Users.Sort(About_Users.Columns["Status"], System.ComponentModel.ListSortDirection.Ascending);
            //About_Users.Columns["Version"].Visible = false;
            //About_Users.Columns["Updated"].Visible = false;
            //About_Users.Columns["Title"].Width += 120;
            //About_Users.Columns["Game"].Width += 80;

            /*new Thread(() =>
            {
                while(true)
                {
                    Console.WriteLine(YouTubePlayer.IsPlaying() + " " + YouTubePlayer.Playing);
                    Thread.Sleep(1000);
                }
            }).Start();*/

            /*thread = new Thread(() =>
            {
                AutoCompleteStringCollection Games = new AutoCompleteStringCollection();
                int count = 0, max = 100;
                List<string> games = new List<string>();
                using (WebClient w = new WebClient())
                {
                    w.Proxy = null;
                    try
                    {
                        while (count < max)
                        {
                            System.Xml.Linq.XElement xml = System.Xml.Linq.XElement.Parse(w.DownloadString("http://www.giantbomb.com/api/games/?api_key=1b38477055d29fcf3e5d5ca264ea20e25d457c31&limit=100&offset=" + count + "&sort=date_added:asc"));
                            max = int.Parse(xml.Element("number_of_total_results").Value);
                            foreach (System.Xml.Linq.XElement game in xml.Element("results").Elements())
                            {
                                if (!games.Contains(game.Element("name").Value))
                                {
                                    games.Add(game.Element("name").Value);
                                }
                            }
                            count += 100;
                            Console.WriteLine(count + "/" + max);
                        }
                    }
                    catch
                    {
                    }
                }
                BeginInvoke((MethodInvoker)delegate
                {
                    File.WriteAllLines(@"Settings\Games.txt", games.ToArray());
                    Games.AddRange(games.ToArray());
                    ChannelGameBox.AutoCompleteCustomSource = Games;
                });
            });
            thread.Name = "Download games list";
            thread.Start();
            Threads.Add(thread);*/
            if (File.Exists(@"Settings\Games.txt"))
            {
                AutoCompleteStringCollection Games = new AutoCompleteStringCollection();
                Games.AddRange(File.ReadAllLines(@"Settings\Games.txt"));
                ChannelGameBox.AutoCompleteCustomSource = Games;
            }

            CenterSpacer(ConnectionLabel, ConnectionSpacer);
            CenterSpacer(CurrencyLabel, CurrencySpacer);
            CenterSpacer(SubscribersLabel, SubscribersSpacer);
            CenterSpacer(DonationsLabel, DonationsSpacer);
            CenterSpacer(HandoutLabel, HandoutSpacer);
            CenterSpacer(GiveawayTypeLabel, GiveawayTypeSpacer);
            CenterSpacer(GiveawayRulesLabel, GiveawayRulesSpacer, false, true);
            CenterSpacer(GiveawayBansLabel, GiveawayBansSpacer);
            CenterSpacer(GiveawayUsersLabel, GiveawayUsersSpacer);
            CenterSpacer(Spam_CWLLabel, Spam_CWLSpacer, false, true);
            CenterSpacer(MySQLLabel, MySQLSpacer, true, false);
            CenterSpacer(SubscribersLabel, SubscribersSpacer);
            CenterSpacer(SubscriptionsLabel, SubscriptionsSpacer);
            CenterSpacer(MessageTimersLabel, MessageTimersSpacer);

            Panel panel = new Panel();
            panel.Size = new Size(1, 1);
            panel.Location = new Point(GiveawayTypeSpacer.Location.X + GiveawayTypeSpacer.Size.Width - 1, GiveawayTypeSpacer.Location.Y + 9);
            GiveawayWindow.Controls.Add(panel);
            panel.BringToFront();
            panel = new Panel();
            panel.Size = new Size(1, 1);
            panel.Location = new Point(GiveawayBansSpacer.Location.X + GiveawayBansSpacer.Size.Width - 1, GiveawayBansSpacer.Location.Y + 9);
            GiveawayWindow.Controls.Add(panel);
            panel.BringToFront();

            // Todo : Create a method
            panel = new Panel();
            panel.Size = new Size(GenerateBotTokenButton.Size.Width, 1);
            panel.Location = new Point(GenerateBotTokenButton.Location.X, GenerateBotTokenButton.Location.Y);
            SettingsWindow.Controls.Add(panel);
            panel.BringToFront();
            panel = new Panel();
            panel.Size = new Size(GenerateBotTokenButton.Size.Width, 1);
            panel.Location = new Point(GenerateBotTokenButton.Location.X, GenerateBotTokenButton.Location.Y + GenerateBotTokenButton.Size.Height - 1);
            SettingsWindow.Controls.Add(panel);
            panel.BringToFront();
            panel = new Panel();
            panel.BackColor = Color.Black;
            panel.Size = new Size(GenerateBotTokenButton.Size.Width, 1);
            panel.Location = new Point(GenerateBotTokenButton.Location.X, GenerateBotTokenButton.Location.Y + 1);
            SettingsWindow.Controls.Add(panel);
            panel.BringToFront();
            panel = new Panel();
            panel.BackColor = Color.Black;
            panel.Size = new Size(GenerateBotTokenButton.Size.Width, 1);
            panel.Location = new Point(GenerateBotTokenButton.Location.X, GenerateBotTokenButton.Location.Y + GenerateBotTokenButton.Size.Height - 2);
            SettingsWindow.Controls.Add(panel);
            panel.BringToFront();

            panel = new Panel();
            panel.Size = new Size(GenerateChannelTokenButton.Size.Width, 1);
            panel.Location = new Point(GenerateChannelTokenButton.Location.X, GenerateChannelTokenButton.Location.Y);
            SettingsWindow.Controls.Add(panel);
            panel.BringToFront();
            panel = new Panel();
            panel.Size = new Size(GenerateChannelTokenButton.Size.Width, 1);
            panel.Location = new Point(GenerateChannelTokenButton.Location.X, GenerateChannelTokenButton.Location.Y + GenerateChannelTokenButton.Size.Height - 1);
            SettingsWindow.Controls.Add(panel);
            panel.BringToFront();
            panel = new Panel();
            panel.BackColor = Color.Black;
            panel.Size = new Size(GenerateChannelTokenButton.Size.Width, 1);
            panel.Location = new Point(GenerateChannelTokenButton.Location.X, GenerateChannelTokenButton.Location.Y + 1);
            SettingsWindow.Controls.Add(panel);
            panel.BringToFront();
            panel = new Panel();
            panel.BackColor = Color.Black;
            panel.Size = new Size(GenerateChannelTokenButton.Size.Width, 1);
            panel.Location = new Point(GenerateChannelTokenButton.Location.X, GenerateChannelTokenButton.Location.Y + GenerateChannelTokenButton.Size.Height - 2);
            SettingsWindow.Controls.Add(panel);
            panel.BringToFront();

            Windows.Add(SettingsWindowButton, SettingsWindow);
            Windows.Add(ChannelWindowButton, ChannelWindow);
            Windows.Add(CurrencyWindowButton, CurrencyWindow);
            Windows.Add(GiveawayWindowButton, GiveawayWindow);
            Windows.Add(DonationsWindowButton, DonationsWindow);
            Windows.Add(SpamFilterWindowButton, SpamFilterWindow);
            Windows.Add(AboutWindowButton, AboutWindow);

            int y = -((Height - 38) / Windows.Keys.Count * Windows.Keys.Count - Height + 38);
            int y2 = y;
            foreach (CheckBox btn in Windows.Keys)
            {
                btn.Size = new Size(100, (Height - 38) / Windows.Keys.Count);
            }
            while (y > 0)
            {
                foreach (CheckBox btn in Windows.Keys)
                {
                    if (y == 0) break;
                    btn.Size = new Size(btn.Size.Width, btn.Size.Height + 1);
                    y--;
                }
            }
            y = 30;
            foreach (CheckBox btn in Windows.Keys)
            {
                btn.Location = new Point(8, y);
                y += btn.Size.Height;
            }

            CurrentWindow = SettingsWindow;
            SettingsWindow.BringToFront();

            SettingsErrorLabel.Text = "";

            bIgnoreUpdates = true;

            ini.SetValue("Settings", "BOT_Name", BotNameBox.Text = ini.GetValue("Settings", "BOT_Name", "ModBot"));
            ini.SetValue("Settings", "BOT_Password", BotPasswordBox.Text = ini.GetValue("Settings", "BOT_Password", ""));

            ini.SetValue("Settings", "Channel_Name", ChannelBox.Text = ini.GetValue("Settings", "Channel_Name", "ModChannel"));
            ini.SetValue("Settings", "Channel_Token", ChannelTokenBox.Text = ini.GetValue("Settings", "Channel_Token", ""));
            ini.SetValue("Settings", "Channel_SteamID64", Channel_SteamID64.Text = ini.GetValue("Settings", "Channel_SteamID64", "SteamID64"));
            ini.SetValue("Settings", "Channel_ViewersChange", (Channel_ViewersChange.Checked = (ini.GetValue("Settings", "Channel_ViewersChange", "0") == "1")) ? "1" : "0");
            int variable = Convert.ToInt32(ini.GetValue("Settings", "Channel_ViewersChangeInterval", "5"));
            if (variable > Channel_ViewersChangeInterval.Maximum || variable < Channel_ViewersChangeInterval.Minimum)
            {
                variable = 5;
            }
            ini.SetValue("Settings", "Channel_ViewersChangeInterval", (Channel_ViewersChangeInterval.Value = variable).ToString());
            variable = Convert.ToInt32(ini.GetValue("Settings", "Channel_ViewersChangeRate", "10"));
            if (variable > Channel_ViewersChangeRate.Maximum || variable < Channel_ViewersChangeRate.Minimum)
            {
                variable = 10;
            }
            ini.SetValue("Settings", "Channel_ViewersChangeRate", (Channel_ViewersChangeRate.Value = variable).ToString());
            ini.SetValue("Settings", "Channel_ViewersChangeMessage", Channel_ViewersChangeMessage.Text = ini.GetValue("Settings", "Channel_ViewersChangeMessage", "New viewers remember to follow the channel!"));
            ini.SetValue("Settings", "Channel_WelcomeSub", (Channel_WelcomeSub.Checked = (ini.GetValue("Settings", "Channel_WelcomeSub", "0") == "1")) ? "1" : "0");
            ini.SetValue("Settings", "Channel_WelcomeSubMessage", Channel_WelcomeSubMessage.Text = ini.GetValue("Settings", "Channel_WelcomeSubMessage", "Welcome to the team @user!"));
            ini.SetValue("Settings", "Channel_SubscriptionRewards", (Channel_SubscriptionRewards.Checked = (ini.GetValue("Settings", "Channel_SubscriptionRewards", "0") == "1")) ? "1" : "0");
            if (!Directory.Exists(@"Data\Subscriptions")) Directory.CreateDirectory(@"Data\Subscriptions");
            if (File.Exists(@"Data\Subscriptions\Rewards.txt"))
            {
                foreach (string line in File.ReadAllLines(@"Data\Subscriptions\Rewards.txt"))
                {
                    string[] reward = line.Split(';');
                    Channel_SubscriptionRewardsList.Rows.Add(reward[0], reward[1]);
                }
            }
            Channel_SubscriptionRewardsList.CellValueChanged += new DataGridViewCellEventHandler(Channel_SubscriptionRewardsList_Changed);
            Channel_SubscriptionRewardsList.RowsAdded += new DataGridViewRowsAddedEventHandler(Channel_SubscriptionRewardsList_Changed);
            Channel_SubscriptionRewardsList.RowsRemoved += new DataGridViewRowsRemovedEventHandler(Channel_SubscriptionRewardsList_Changed);

            ini.SetValue("Settings", "Currency_Name", CurrencyNameBox.Text = ini.GetValue("Settings", "Currency_Name", "Mod Coins"));
            ini.SetValue("Settings", "Currency_Command", CurrencyCommandBox.Text = ini.GetValue("Settings", "Currency_Command", "ModCoins"));
            variable = Convert.ToInt32(ini.GetValue("Settings", "Currency_Interval", "5"));
            if (variable > CurrencyHandoutInterval.Maximum || variable < CurrencyHandoutInterval.Minimum)
            {
                variable = 5;
            }
            ini.SetValue("Settings", "Currency_Interval", (CurrencyHandoutInterval.Value = variable).ToString());
            variable = Convert.ToInt32(ini.GetValue("Settings", "Currency_Payout", "1"));
            if (variable > CurrencyHandoutAmount.Maximum || variable < CurrencyHandoutAmount.Minimum)
            {
                variable = 1;
            }
            ini.SetValue("Settings", "Currency_Payout", (CurrencyHandoutAmount.Value = variable).ToString());
            variable = Convert.ToInt32(ini.GetValue("Settings", "Currency_SubscriberPayout", "1"));
            if (variable > CurrencySubHandoutAmount.Maximum || variable < CurrencySubHandoutAmount.Minimum)
            {
                variable = 1;
            }
            ini.SetValue("Settings", "Currency_SubscriberPayout", (CurrencySubHandoutAmount.Value = variable).ToString());

            ini.SetValue("Settings", "Subsribers_URL", SubLinkBox.Text = ini.GetValue("Settings", "Subsribers_URL", ""));

            ini.SetValue("Settings", "Donations_ClientID", DonationsClientIdBox.Text = ini.GetValue("Settings", "Donations_ClientID", ""));
            ini.SetValue("Settings", "Donations_Token", DonationsTokenBox.Text = ini.GetValue("Settings", "Donations_Token", ""));
            ini.SetValue("Settings", "Donations_UpdateTop", (UpdateTopDonorsCheckBox.Checked = (ini.GetValue("Settings", "Donations_UpdateTop", "0") == "1")) ? "1" : "0");
            variable = Convert.ToInt32(ini.GetValue("Settings", "Donations_Top_Limit", "20"));
            if (variable > TopDonorsLimit.Maximum || variable < TopDonorsLimit.Minimum)
            {
                variable = 20;
            }
            ini.SetValue("Settings", "Donations_Top_Limit", (TopDonorsLimit.Value = variable).ToString());
            ini.SetValue("Settings", "Donations_UpdateRecent", (UpdateRecentDonorsCheckBox.Checked = (ini.GetValue("Settings", "Donations_UpdateRecent", "0") == "1")) ? "1" : "0");
            variable = Convert.ToInt32(ini.GetValue("Settings", "Donations_Recent_Limit", "5"));
            if (variable > RecentDonorsLimit.Maximum || variable < RecentDonorsLimit.Minimum)
            {
                variable = 5;
            }
            ini.SetValue("Settings", "Donations_Recent_Limit", (RecentDonorsLimit.Value = variable).ToString());
            ini.SetValue("Settings", "Donations_UpdateLast", (UpdateLastDonorCheckBox.Checked = (ini.GetValue("Settings", "Donations_UpdateLast", "0") == "1")) ? "1" : "0");

            ini.SetValue("Settings", "Currency_DisableCommand", (Currency_DisableCommand.Checked = (ini.GetValue("Settings", "Currency_DisableCommand", "0") == "1")) ? "1" : "0");
            string sCurrencyHandout = ini.GetValue("Settings", "Currency_Handout", "0");
            ini.SetValue("Settings", "Currency_Handout", sCurrencyHandout);
            if (sCurrencyHandout.Equals("0"))
            {
                Currency_HandoutEveryone.Checked = true;
            }
            else if (sCurrencyHandout.Equals("1"))
            {
                Currency_HandoutActiveStream.Checked = true;
            }
            else if (sCurrencyHandout.Equals("2"))
            {
                Currency_HandoutActiveTime.Checked = true;
            }
            variable = Convert.ToInt32(ini.GetValue("Settings", "Currency_HandoutTime", "5"));
            if (variable > Currency_HandoutLastActive.Maximum || variable < Currency_HandoutLastActive.Minimum)
            {
                variable = 5;
            }
            ini.SetValue("Settings", "Currency_HandoutTime", (Currency_HandoutLastActive.Value = variable).ToString());

            ini.SetValue("Settings", "Spam_CWL", (Spam_CWL.Checked = (ini.GetValue("Settings", "Spam_CWL", "0") == "1")) ? "1" : "0");
            ini.SetValue("Settings", "Spam_CWLAnnounceTimeouts", (Spam_CWLAnnounceTimeouts.Checked = (ini.GetValue("Settings", "Spam_CWLAnnounceTimeouts", "0") == "1")) ? "1" : "0");
            ini.SetValue("Settings", "Spam_CWhiteList", Spam_CWLBox.Text = ini.GetValue("Settings", "Spam_CWhiteList", "abcdefghijklmnopqrstuvwxyz0123456789"));

            ini.SetValue("Settings", "Misc_ShowConsole", (Misc_ShowConsole.Checked = (ini.GetValue("Settings", "Misc_ShowConsole", "1") == "1")) ? "1" : "0");

            ini.SetValue("Settings", "MySQL_Host", MySQL_Host.Text = ini.GetValue("Settings", "MySQL_Host", ""));
            variable = Convert.ToInt32(ini.GetValue("Settings", "MySQL_Port", "3306"));
            if (variable > MySQL_Port.Maximum || variable < MySQL_Port.Minimum)
            {
                variable = 3306;
            }
            ini.SetValue("Settings", "MySQL_Port", (MySQL_Port.Value = variable).ToString());
            ini.SetValue("Settings", "MySQL_Database", MySQL_Database.Text = ini.GetValue("Settings", "MySQL_Database", ""));
            ini.SetValue("Settings", "MySQL_Username", MySQL_Username.Text = ini.GetValue("Settings", "MySQL_Username", ""));
            ini.SetValue("Settings", "MySQL_Password", MySQL_Password.Text = ini.GetValue("Settings", "MySQL_Password", ""));

            ini.SetValue("Settings", "Database_Table", Database_Table.Text = ini.GetValue("Settings", "Database_Table", ""));

            Channel_SubscriptionsDate.Value = DateTime.Now;
            //Channel_SubscriptionsDate.CustomFormat = "dddd, MMMM M, yyyy H:mm:ss";
            //Channel_SubscriptionsDate.CustomFormat = "d/MM/yy H:mm:ss";

            bIgnoreUpdates = false;

            SongRequestPlayer.Navigated += YouTube.SongRequestPlayer_Navigated;
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            Modifications.Load();

            //Settings loading
            /*Dictionary<Control, bool> dState = new Dictionary<Control, bool>();
            Thread tLoad = new Thread(() =>
            {
                while (!g_bLoaded)
                {
                    foreach (Control ctrl in Controls)
                    {
                        if (!BaseControls.Contains(ctrl) && !dState.ContainsKey(ctrl))
                        {
                            dState.Add(ctrl, ctrl.Enabled);
                            BeginInvoke((MethodInvoker)delegate
                            {
                                ctrl.Enabled = false;
                            });
                        }
                    }
                    Thread.Sleep(100);
                }

                BeginInvoke((MethodInvoker)delegate
                {
                    foreach (Control ctrl in dState.Keys)
                    {
                        if (Controls.Contains(ctrl))
                        {
                            ctrl.Enabled = dState[ctrl];
                        }
                    }
                    GetSettings();
                });
            });
            tLoad.Start();*/

            Program.LoadingScreen.Hide();

            //Update checking
            Thread thread = new Thread(() =>
            {
                try
                {
                    bool bUpdateNote = false;
                    while (!bUpdateNote)
                    {
                        Thread.Sleep(60000);
                        if (IsActivated)
                        {
                            bUpdateNote = true;
                            Program.Updates.CheckUpdate(false, true);
                        }
                        else
                        {
                            bUpdateNote = true;
                            bool bNote = false;
                            Activated += (object form, EventArgs eargs) =>
                            {
                                if (!bNote)
                                {
                                    bNote = true;
                                    Program.Updates.CheckUpdate(false, true);
                                }
                            };
                        }
                        Program.Updates.CheckUpdate(true, false);
                    }
                }
                catch
                {
                }
            });
            Threads.Add(thread);
            thread.Name = "Update checking";
            thread.Start();

            if (Program.args.Contains("-connect") && ConnectButton.Enabled)
            {
                ConnectButton.PerformClick();
            }
            else
            {
                Program.Updates.WelcomeMsg();
                Program.Updates.WhatsNew();
                if (!Program.args.Contains("-skipmotd")) Program.Updates.MsgOfTheDay();
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Tab || keyData == (Keys.Shift | Keys.Tab)) return true;

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void CenterSpacer(Label label, GroupBox spacer, bool hideleft = false, bool hideright = false)
        {
            label.Location = new Point(spacer.Location.X + spacer.Size.Width / 2 - label.Size.Width / 2, spacer.Location.Y);
            if (hideleft)
            {
                Panel panel = new Panel();
                panel.Size = new Size(1, 2);
                panel.Location = new Point(spacer.Location.X, spacer.Location.Y + 9);
                spacer.Parent.Controls.Add(panel);
                panel.BringToFront();
            }
            if (hideright)
            {
                Panel panel = new Panel();
                panel.Size = new Size(1, 2);
                panel.Location = new Point(spacer.Location.X + spacer.Size.Width - 1, spacer.Location.Y + 9);
                spacer.Parent.Controls.Add(panel);
                panel.BringToFront();
            }
        }

        public void GetSettings()
        {
            if (!bIgnoreUpdates)
            {
                bIgnoreUpdates = true;
                ////SettingsPresents.TabPages.Clear();
                //Console.WriteLine("Getting Settings");
                Dictionary<Control, bool> dState = new Dictionary<Control, bool>();
                /*foreach (Control ctrl in GiveawayWindow.Controls)
                {
                    if (!dState.ContainsKey(ctrl))
                    {
                        dState.Add(ctrl, ctrl.Enabled);
                        ctrl.Enabled = false;
                    }
                }*/
                bool bRecreateSections = false;
                foreach (string section in ini.GetSectionNames())
                {
                    if (section != "Settings" && !dSettings.ContainsKey(section))
                    {
                        bRecreateSections = true;
                        Giveaway_SettingsPresents.TabPages.Clear();
                        dSettings.Clear();
                        break;
                    }
                }
                foreach (string section in ini.GetSectionNames())
                {
                    if (section != "")
                    {
                        //Console.WriteLine(section);
                        if (!section.Equals("Settings"))
                        {
                            Dictionary<string, string> dSectionSettings = new Dictionary<string, string>();
                            string sVariable = ini.GetValue(section, "Giveaway_Type", "0");
                            ini.SetValue(section, "Giveaway_Type", sVariable);
                            dSectionSettings.Add("Giveaway_Type", sVariable);
                            sVariable = ini.GetValue(section, "Giveaway_TicketCost", "5");
                            ini.SetValue(section, "Giveaway_TicketCost", sVariable);
                            dSectionSettings.Add("Giveaway_TicketCost", sVariable);
                            sVariable = ini.GetValue(section, "Giveaway_MaxTickets", "10");
                            ini.SetValue(section, "Giveaway_MaxTickets", sVariable);
                            dSectionSettings.Add("Giveaway_MaxTickets", sVariable);
                            sVariable = ini.GetValue(section, "Giveaway_MinCurrencyChecked", "0");
                            ini.SetValue(section, "Giveaway_MinCurrencyChecked", sVariable);
                            dSectionSettings.Add("Giveaway_MinCurrencyChecked", sVariable);
                            sVariable = ini.GetValue(section, "Giveaway_MustFollow", "0");
                            ini.SetValue(section, "Giveaway_MustFollow", sVariable);
                            dSectionSettings.Add("Giveaway_MustFollow", sVariable);
                            sVariable = ini.GetValue(section, "Giveaway_MustSubscribe", "0");
                            ini.SetValue(section, "Giveaway_MustSubscribe", sVariable);
                            dSectionSettings.Add("Giveaway_MustSubscribe", sVariable);
                            sVariable = ini.GetValue(section, "Giveaway_MustWatch", "0");
                            ini.SetValue(section, "Giveaway_MustWatch", sVariable);
                            dSectionSettings.Add("Giveaway_MustWatch", sVariable);
                            sVariable = ini.GetValue(section, "Giveaway_MustWatchDays", "0");
                            ini.SetValue(section, "Giveaway_MustWatchDays", sVariable);
                            dSectionSettings.Add("Giveaway_MustWatchDays", sVariable);
                            sVariable = ini.GetValue(section, "Giveaway_MustWatchHours", "0");
                            ini.SetValue(section, "Giveaway_MustWatchHours", sVariable);
                            dSectionSettings.Add("Giveaway_MustWatchHours", sVariable);
                            sVariable = ini.GetValue(section, "Giveaway_MustWatchMinutes", "1");
                            ini.SetValue(section, "Giveaway_MustWatchMinutes", sVariable);
                            dSectionSettings.Add("Giveaway_MustWatchMinutes", sVariable);
                            sVariable = ini.GetValue(section, "Giveaway_MinCurrency", "1");
                            ini.SetValue(section, "Giveaway_MinCurrency", sVariable);
                            dSectionSettings.Add("Giveaway_MinCurrency", sVariable);
                            sVariable = ini.GetValue(section, "Giveaway_ActiveUserTime", "5");
                            ini.SetValue(section, "Giveaway_ActiveUserTime", sVariable);
                            dSectionSettings.Add("Giveaway_ActiveUserTime", sVariable);
                            sVariable = ini.GetValue(section, "Giveaway_AutoBanWinner", "0");
                            ini.SetValue(section, "Giveaway_AutoBanWinner", sVariable);
                            dSectionSettings.Add("Giveaway_AutoBanWinner", sVariable);
                            sVariable = ini.GetValue(section, "Giveaway_WarnFalseEntries", "0");
                            ini.SetValue(section, "Giveaway_WarnFalseEntries", sVariable);
                            dSectionSettings.Add("Giveaway_WarnFalseEntries", sVariable);
                            sVariable = ini.GetValue(section, "Giveaway_AnnounceWarnedEntries", "0");
                            ini.SetValue(section, "Giveaway_AnnounceWarnedEntries", sVariable);
                            dSectionSettings.Add("Giveaway_AnnounceWarnedEntries", sVariable);
                            sVariable = ini.GetValue(section, "Giveaway_SubscribersWinMultiplier", "0");
                            ini.SetValue(section, "Giveaway_SubscribersWinMultiplier", sVariable);
                            dSectionSettings.Add("Giveaway_SubscribersWinMultiplier", sVariable);
                            sVariable = ini.GetValue(section, "Giveaway_SubscribersWinMultiplierAmount", "1");
                            ini.SetValue(section, "Giveaway_SubscribersWinMultiplierAmount", sVariable);
                            dSectionSettings.Add("Giveaway_SubscribersWinMultiplierAmount", sVariable);
                            sVariable = ini.GetValue(section, "Giveaway_CustomKeyword", "");
                            ini.SetValue(section, "Giveaway_CustomKeyword", sVariable);
                            dSectionSettings.Add("Giveaway_CustomKeyword", sVariable);
                            sVariable = ini.GetValue(section, "Giveaway_BanList", "");
                            ini.SetValue(section, "Giveaway_BanList", sVariable);
                            dSectionSettings.Add("Giveaway_BanList", sVariable);

                            /*foreach (KeyValuePair<string, string> kv in dSectionSettings)
                            {
                                Console.WriteLine(kv.Key + " = " + kv.Value);
                            }*/

                            if (bRecreateSections)
                            {
                                dSettings.Add(section, dSectionSettings);

                                Giveaway_SettingsPresents.TabPages.Add(section);
                            }
                            else
                            {
                                if (dSettings.ContainsKey(section))
                                {
                                    dSettings[section] = dSectionSettings;
                                }
                                else
                                {
                                    dSettings.Add(section, dSectionSettings);
                                }
                            }
                        }
                    }
                }

                BeginInvoke((MethodInvoker)delegate
                {
                    foreach (Control ctrl in dState.Keys)
                    {
                        if (GiveawayWindow.Controls.Contains(ctrl))
                        {
                            ctrl.Enabled = dState[ctrl];
                        }
                    }

                    string sSelectedPresent = ini.GetValue("Settings", "SelectedPresent", "Default");
                    if (sSelectedPresent != "")
                    {
                        for (int i = 0; i < Giveaway_SettingsPresents.TabPages.Count; i++)
                        {
                            if (Giveaway_SettingsPresents.TabPages[i].Text.Equals(sSelectedPresent))
                            {
                                iSettingsPresent = Giveaway_SettingsPresents.SelectedIndex = i;
                                break;
                            }
                        }
                    }

                    if (Giveaway_BanListListBox.Items.Count > 0)
                    {
                        Giveaway_BanListListBox.Items.Clear();
                    }

                    if (Giveaway_SettingsPresents.SelectedIndex > -1)
                    {
                        if (dSettings.ContainsKey(Giveaway_SettingsPresents.TabPages[Giveaway_SettingsPresents.SelectedIndex].Text))
                        {
                            foreach (KeyValuePair<string, string> KeyValue in dSettings[Giveaway_SettingsPresents.TabPages[Giveaway_SettingsPresents.SelectedIndex].Text])
                            {
                                if (KeyValue.Key != "")
                                {
                                    if (KeyValue.Key.Equals("Giveaway_Type"))
                                    {
                                        Giveaway_TypeActive.Checked = (KeyValue.Value == "0");
                                        Giveaway_TypeKeyword.Checked = (KeyValue.Value == "1");
                                        Giveaway_TypeTickets.Checked = (KeyValue.Value == "2");
                                    }
                                    else if (KeyValue.Key.Equals("Giveaway_TicketCost"))
                                    {
                                        Giveaway_TicketCost.Value = Convert.ToInt32(KeyValue.Value);
                                    }
                                    else if (KeyValue.Key.Equals("Giveaway_MaxTickets"))
                                    {
                                        Giveaway_MaxTickets.Value = Convert.ToInt32(KeyValue.Value);
                                    }
                                    else if (KeyValue.Key.Equals("Giveaway_MinCurrencyChecked"))
                                    {
                                        Giveaway_MinCurrency.Checked = (KeyValue.Value == "1");
                                    }
                                    else if (KeyValue.Key.Equals("Giveaway_MustFollow"))
                                    {
                                        Giveaway_MustFollow.Checked = (KeyValue.Value == "1");
                                    }
                                    else if (KeyValue.Key.Equals("Giveaway_MustSubscribe") && Irc.partnered)
                                    {
                                        Giveaway_MustSubscribe.Checked = (KeyValue.Value == "1");
                                    }
                                    else if (KeyValue.Key.Equals("Giveaway_MustWatch"))
                                    {
                                        Giveaway_MustWatch.Checked = (KeyValue.Value == "1");
                                    }
                                    else if (KeyValue.Key.Equals("Giveaway_MustWatchDays"))
                                    {
                                        Giveaway_MustWatchDays.Value = Convert.ToInt32(KeyValue.Value);
                                    }
                                    else if (KeyValue.Key.Equals("Giveaway_MustWatchHours"))
                                    {
                                        Giveaway_MustWatchHours.Value = Convert.ToInt32(KeyValue.Value);
                                    }
                                    else if (KeyValue.Key.Equals("Giveaway_MustWatchMinutes"))
                                    {
                                        Giveaway_MustWatchMinutes.Value = Convert.ToInt32(KeyValue.Value);
                                    }
                                    else if (KeyValue.Key.Equals("Giveaway_MinCurrency"))
                                    {
                                        Giveaway_MinCurrencyBox.Value = Convert.ToInt32(KeyValue.Value);
                                    }
                                    else if (KeyValue.Key.Equals("Giveaway_ActiveUserTime"))
                                    {
                                        Giveaway_ActiveUserTime.Value = Convert.ToInt32(KeyValue.Value);
                                    }
                                    else if (KeyValue.Key.Equals("Giveaway_AutoBanWinner"))
                                    {
                                        Giveaway_AutoBanWinner.Checked = (KeyValue.Value == "1");
                                    }
                                    else if (KeyValue.Key.Equals("Giveaway_WarnFalseEntries"))
                                    {
                                        Giveaway_WarnFalseEntries.Checked = (KeyValue.Value == "1");
                                    }
                                    else if (KeyValue.Key.Equals("Giveaway_AnnounceWarnedEntries"))
                                    {
                                        Giveaway_AnnounceWarnedEntries.Checked = (KeyValue.Value == "1");
                                    }
                                    else if (KeyValue.Key.Equals("Giveaway_SubscribersWinMultiplier"))
                                    {
                                        Giveaway_SubscribersWinMultiplier.Checked = (KeyValue.Value == "1");
                                    }
                                    else if (KeyValue.Key.Equals("Giveaway_SubscribersWinMultiplierAmount"))
                                    {
                                        Giveaway_SubscribersWinMultiplierAmount.Value = Convert.ToInt32(KeyValue.Value);
                                    }
                                    else if (KeyValue.Key.Equals("Giveaway_CustomKeyword"))
                                    {
                                        Giveaway_CustomKeyword.Text = KeyValue.Value;
                                    }
                                    else if (KeyValue.Key.Equals("Giveaway_BanList"))
                                    {
                                        string[] bans = KeyValue.Value.Split(';');
                                        foreach (string ban in bans)
                                        {
                                            //Console.WriteLine(ban);
                                            if (!ban.Equals("") && !Giveaway_BanListListBox.Items.Contains(Api.capName(ban)))
                                            {
                                                Giveaway_BanListListBox.Items.Add(Api.capName(ban));
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                });

                if (Giveaway_SettingsPresents.TabPages.Count == 0)
                {
                    Giveaway_SettingsPresents.TabPages.Add("Default");
                    iSettingsPresent = 0;
                    SaveSettings();
                }
                bIgnoreUpdates = false;
            }
        }

        public void UpdateDonations()
        {
            while (Irc.donation_clientid != "" && Irc.donation_token != "")
            {
                List<Transaction> transactions = Api.UpdateTransactions().OrderByDescending(key => Convert.ToDateTime(key.date)).ToList();
                if (transactions.Count > 0)
                {
                    string sDonationsIgnoreRecent = ini.GetValue("Settings", "Donations_Ignore_Recent", "");
                    ini.SetValue("Settings", "Donations_Ignore_Recent", sDonationsIgnoreRecent);
                    string[] sRecentIgnores = sDonationsIgnoreRecent.Split(',');
                    string sDonationsIgnoreLatest = ini.GetValue("Settings", "Donations_Ignore_Latest", "");
                    ini.SetValue("Settings", "Donations_Ignore_Latest", sDonationsIgnoreLatest);
                    string[] sLatestIgnores = sDonationsIgnoreLatest.Split(',');
                    string sDonationsIgnoreTop = ini.GetValue("Settings", "Donations_Ignore_Top", "");
                    ini.SetValue("Settings", "Donations_Ignore_Top", sDonationsIgnoreTop);
                    string[] sTopIgnores = sDonationsIgnoreTop.Split(',');

                    BeginInvoke((MethodInvoker)delegate
                    {
                        foreach (Transaction transaction in transactions)
                        {
                            bool found = false;
                            foreach (DataGridViewRow row in Donations_List.Rows) if (row.Cells["ID"].Value.ToString() == transaction.id) found = true;
                            if (!found) Donations_List.Rows.Add(transaction.date, transaction.donor, transaction.amount, transaction.id, transaction.notes, !sRecentIgnores.Contains(transaction.id), !sLatestIgnores.Contains(transaction.id), !sTopIgnores.Contains(transaction.id), true);
                        }
                    });

                    if (!Directory.Exists(@"Data\Donations")) Directory.CreateDirectory(@"Data\Donations");

                    int count = Convert.ToInt32(RecentDonorsLimit.Value), iCount = 0;
                    if (transactions.Count < count) count = transactions.Count;
                    string sTopDonors = "", sRecentDonors = "", sLatestDonor = "";
                    List<Transaction> Donors = new List<Transaction>();
                    foreach (Transaction transaction in transactions)
                    {
                        if (UpdateRecentDonorsCheckBox.Checked)
                        {
                            if (!sRecentIgnores.Contains(transaction.id) && iCount < count)
                            {
                                if (iCount > 0)
                                {
                                    sRecentDonors += ", ";
                                }
                                sRecentDonors += transaction.ToString("$AMOUNT - DONOR");
                                iCount++;
                            }
                        }
                        if (UpdateLastDonorCheckBox.Checked && !sLatestIgnores.Contains(transaction.id) && sLatestDonor == "")
                        {
                            File.WriteAllText(@"Data\Donations\LatestDonation.txt", (sLatestDonor = transaction.ToString("$AMOUNT - DONOR")));
                        }

                        if (UpdateTopDonorsCheckBox.Checked)
                        {
                            if (!sTopIgnores.Contains(transaction.id))
                            {
                                if (!Donors.Any(c => c.donor.ToLower() == transaction.donor.ToLower()))
                                {
                                    Donors.Add(transaction);
                                }
                                else
                                {
                                    foreach (Transaction trans in Donors)
                                    {
                                        if (transaction.donor.ToLower() == trans.donor.ToLower())
                                        {
                                            trans.amount = (float.Parse(trans.amount, CultureInfo.InvariantCulture.NumberFormat) + float.Parse(transaction.amount, CultureInfo.InvariantCulture.NumberFormat)).ToString("0.00");
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (UpdateRecentDonorsCheckBox.Checked)
                    {
                        File.WriteAllText(@"Data\Donations\RecentDonors.txt", sRecentDonors);
                    }

                    transactions = Donors.OrderByDescending(key => float.Parse(key.amount)).ToList();
                    if (UpdateTopDonorsCheckBox.Checked)
                    {
                        count = Convert.ToInt32(TopDonorsLimit.Value);
                        if (Donors.Count < count)
                        {
                            count = Donors.Count;
                        }
                        iCount = 0;
                        foreach (Transaction transaction in Donors)
                        {
                            if (iCount < count)
                            {
                                if (iCount > 0)
                                {
                                    sTopDonors += "\r\n";
                                }
                                sTopDonors += transactions[iCount].ToString("$AMOUNT - DONOR");
                                iCount++;
                            }
                        }
                        File.WriteAllText(@"Data\Donations\TopDonors.txt", sTopDonors);
                    }
                }

                Thread.Sleep(1000);
                if (Irc.ResourceKeeper)
                {
                    Thread.Sleep(29000);
                }
            }
        }

        public void UpdateChannelData()
        {
            while (true)
            {
                int iStatus = 0;
                if (Irc.irc.Connected)
                {
                    if (Irc.IsStreaming)
                    {
                        iStatus = 2;
                    }
                    else
                    {
                        iStatus = 1;
                    }
                }

                if (!MetadataModified)
                {
                    using (WebClient w = new WebClient())
                    {
                        w.Proxy = null;
                        try
                        {
                            JObject stream = JObject.Parse(w.DownloadString("https://api.twitch.tv/kraken/channels/" + Irc.channel.Substring(1)));

                            ChannelGame = stream["game"].ToString();
                            ChannelTitle = stream["status"].ToString();

                            if (Channel_UseSteam.Checked)
                            {
                                JObject SteamData = JObject.Parse(w.DownloadString("http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key=5C18EE317A1E58DD98BCAF4872977055&steamids=" + Channel_SteamID64.Text));

                                try
                                {
                                    if (SteamData["response"]["players"].HasValues) ChannelGame = SteamData["response"]["players"][0]["gameextrainfo"].ToString();
                                }
                                catch { }

                                if (stream["game"].ToString() != ChannelGame)
                                {
                                    Api.UpdateMetadata(ChannelTitle, ChannelGame);
                                }
                            }

                            BeginInvoke((MethodInvoker)delegate
                            {
                                if (!MetadataModified)
                                {
                                    ChannelTitleBox.Text = ChannelTitle;
                                    ChannelGameBox.Text = ChannelGame;

                                    MetadataModified = false;
                                }
                            });
                        }
                        //catch (Exception e)
                        catch
                        {
                            //Console.WriteLine(e);
                            //Api.LogError("*************Error Message (via GrabData()): " + DateTime.Now + "*********************************\r\nUnable to connect to Twitch API to check stream data.\r\n" + e + "\r\n");
                        }
                    }
                }

                BeginInvoke((MethodInvoker)delegate
                {
                    ChannelStatusLabel.Text = "DISCONNECTED";
                    ChannelStatusLabel.ForeColor = Color.Red;
                    if (iStatus == 2)
                    {
                        ChannelStatusLabel.Text = "ON AIR";
                        ChannelStatusLabel.ForeColor = Color.Green;
                        int iViewers = Irc.ActiveUsers.Count;
                        foreach (string user in Irc.ActiveUsers.Keys)
                        {
                            if (Irc.IgnoredUsers.Contains(user.ToLower()))
                            {
                                iViewers--;
                            }
                        }
                        ChannelStatusLabel.Text += " (" + iViewers + ")";
                    }
                    else if (iStatus == 1)
                    {
                        ChannelStatusLabel.Text = "OFF AIR";
                        ChannelStatusLabel.ForeColor = Color.Blue;
                    }

                    //g_bLoaded = true;
                });

                Thread.Sleep(1000);
                if (Irc.ResourceKeeper)
                {
                    Thread.Sleep(29000);
                }
            }
        }

        private void Giveaway_RerollButton_Click(object sender, EventArgs e)
        {
            Giveaway.getWinner();
        }

        private void Giveaway_StartButton_Click(object sender, EventArgs e)
        {
            if (Giveaway_TypeTickets.Checked)
            {
                Giveaway.startGiveaway(int.Parse(Giveaway_TicketCost.Value.ToString()), int.Parse(Giveaway_MaxTickets.Value.ToString()));
            }
            else
            {
                Giveaway.startGiveaway();
            }
        }

        private void Giveaway_OpenButton_Click(object sender, EventArgs e)
        {
            Giveaway.openGiveaway();
        }

        private void Giveaway_CloseButton_Click(object sender, EventArgs e)
        {
            Giveaway.closeGiveaway();
        }

        private void Giveaway_StopButton_Click(object sender, EventArgs e)
        {
            Giveaway.endGiveaway();
        }

        private void Giveaway_CancelButton_Click(object sender, EventArgs e)
        {
            Giveaway.cancelGiveaway();
        }

        private void Giveaway_AnnounceWinnerButton_Click(object sender, EventArgs e)
        {
            TimeSpan t = Database.getTimeWatched(Giveaway_WinnerLabel.Text);
            string winner = Giveaway_WinnerLabel.Text;
            //Irc.sendMessage(winner + " has won the giveaway! (" + (Api.IsSubscriber(winner) ? "Subscribes to the channel | " : "") + (Api.IsFollower(winner) ? "Follows the channel | " : "") + "Has " + Database.checkCurrency(winner) + " " + Irc.currencyName + " | Has watched the stream for " + t.Days + " days, " + t.Hours + " hours and " + t.Minutes + " minutes | Chance : " + Giveaway.Chance.ToString("0.00") + "%)");
            //Irc.sendMessage(winner + " has won the giveaway! (" + (Api.IsSubscriber(winner) ? "Subscribes to the channel | " : "") + (Api.IsFollower(winner) ? "Follows the channel | " : "") + "Has " + Database.checkCurrency(winner) + " " + Irc.currencyName + " | Has watched the stream for " + t.Days + " days, " + t.Hours + " hours and " + t.Minutes + " minutes)");
            Irc.sendMessage(winner + " has won the giveaway! (" + (Irc.Subscribers.Contains(Api.capName(winner)) ? "Subscribes to the channel | " : "") + (Api.IsFollower(winner) ? "Follows the channel | " : "") + "Has " + Database.checkCurrency(winner) + " " + Irc.currencyName + " | Has watched the stream for " + t.Days + " days, " + t.Hours + " hours and " + t.Minutes + " minutes)");
        }

        private void Giveaway_AddBanTextBox_TextChanged(object sender, EventArgs e)
        {
            if (Giveaway_AddBanTextBox.Text == "" || Giveaway_AddBanTextBox.Text.Length < 5 || Giveaway_AddBanTextBox.Text.Contains(" ") || Giveaway_AddBanTextBox.Text.Contains(".") || Giveaway_AddBanTextBox.Text.Contains(",") || Giveaway_AddBanTextBox.Text.Contains("\"") || Giveaway_AddBanTextBox.Text.Contains("'") || Irc.IgnoredUsers.Any(user => user.ToLower() == Giveaway_AddBanTextBox.Text.ToLower()) || Giveaway_BanListListBox.Items.Contains(Giveaway_AddBanTextBox.Text))
            {
                Giveaway_BanButton.Enabled = false;
            }
            else
            {
                Giveaway_BanButton.Enabled = true;
            }
        }

        private void Giveaway_BanListListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Giveaway_BanListListBox.SelectedIndex >= 0) Giveaway_UnbanButton.Enabled = true;
        }

        private void Giveaway_CopyWinnerButton_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Clipboard.SetText(Giveaway_WinnerLabel.Text);
        }

        private void Giveaway_Settings_Changed(object sender, EventArgs e)
        {
            Control ctrl = (Control)sender;
            if (ctrl == Giveaway_UnbanButton)
            {
                int iOldIndex = Giveaway_BanListListBox.SelectedIndex;
                Giveaway_BanListListBox.Items.RemoveAt(iOldIndex);
                Giveaway_UnbanButton.Enabled = false;
                if (Giveaway_BanListListBox.Items.Count > 0)
                {
                    if (iOldIndex > Giveaway_BanListListBox.Items.Count - 1)
                    {
                        Giveaway_BanListListBox.SelectedIndex = Giveaway_BanListListBox.Items.Count - 1;
                    }
                    else
                    {
                        Giveaway_BanListListBox.SelectedIndex = iOldIndex;
                    }
                }
            }
            else if (ctrl == Giveaway_BanButton)
            {
                Giveaway_BanListListBox.Items.Add(Giveaway_AddBanTextBox.Text);
                Giveaway_AddBanTextBox.Text = "";
                Giveaway_BanButton.Enabled = false;
            }
            else if (ctrl == Giveaway_MinCurrency)
            {
                Giveaway_MinCurrencyBox.Enabled = Giveaway_MinCurrency.Checked;
            }
            else if (ctrl == Giveaway_TypeActive)
            {
                Giveaway_ActiveUserTime.Enabled = Giveaway_TypeActive.Checked;
                Giveaway_WarnFalseEntries.Enabled = (!Giveaway_TypeActive.Checked && Irc.Moderators.Contains(Api.capName(Irc.nick)));
                if (Giveaway_TypeActive.Checked || !Irc.Moderators.Contains(Api.capName(Irc.nick))) Giveaway_WarnFalseEntries.Checked = false;
            }
            else if (ctrl == Giveaway_TypeKeyword)
            {
                Giveaway_CustomKeyword.Enabled = Giveaway_TypeKeyword.Checked;
            }
            else if (ctrl == Giveaway_TypeTickets)
            {
                if (Giveaway_TypeTickets.Checked) Giveaway_MinCurrency.Checked = false;
                Giveaway_MinCurrency.Enabled = !Giveaway_TypeTickets.Checked;
                Giveaway_TicketCost.Enabled = Giveaway_MaxTickets.Enabled = Giveaway_TypeTickets.Checked;
            }
            else if (ctrl == Giveaway_MustWatch)
            {
                Giveaway_MustWatchDays.Enabled = Giveaway_MustWatchHours.Enabled = Giveaway_MustWatchMinutes.Enabled = Giveaway_MustWatch.Checked;
            }
            else if (ctrl == Giveaway_MustWatchHours)
            {
                if (Giveaway_MustWatchHours.Value == -1)
                {
                    if (Giveaway_MustWatchDays.Value > 0)
                    {
                        Giveaway_MustWatchHours.Value = 23;
                        Giveaway_MustWatchDays.Value--;
                    }
                }
                else if (Giveaway_MustWatchHours.Value == 24)
                {
                    Giveaway_MustWatchHours.Value = 0;
                    Giveaway_MustWatchDays.Value++;
                }
            }
            else if (ctrl == Giveaway_MustWatchMinutes)
            {
                if (Giveaway_MustWatchMinutes.Value == -1)
                {
                    if (Giveaway_MustWatchHours.Value > 0 || Giveaway_MustWatchDays.Value > 0)
                    {
                        Giveaway_MustWatchMinutes.Value = 59;
                        Giveaway_MustWatchHours.Value--;
                    }
                    else
                    {
                        Giveaway_MustWatchMinutes.Value = 0;
                    }
                }
                else if (Giveaway_MustWatchMinutes.Value == 60)
                {
                    Giveaway_MustWatchMinutes.Value = 0;
                    Giveaway_MustWatchHours.Value++;
                }
            }
            else if (ctrl == Giveaway_WarnFalseEntries)
            {
                Giveaway_AnnounceWarnedEntries.Enabled = Giveaway_WarnFalseEntries.Checked;
                if (!Giveaway_WarnFalseEntries.Checked) Giveaway_AnnounceWarnedEntries.Checked = false;
            }
            else if (ctrl == Giveaway_SubscribersWinMultiplier)
            {
                Giveaway_SubscribersWinMultiplierAmount.Enabled = Giveaway_SubscribersWinMultiplier.Checked;
            }
            SaveSettings();
        }

        public void SaveSettings(int SettingsPresent = -2, bool ReloadSettings = false)
        {
            new Thread(() =>
            {
                if (SettingsPresent == -2)
                {
                    if (iSettingsPresent != -2)
                    {
                        SettingsPresent = iSettingsPresent;
                    }
                }
                if (!bIgnoreUpdates)
                {
                    if (SettingsPresent > -1)
                    {
                        string Present = Giveaway_SettingsPresents.TabPages[SettingsPresent].Text;
                        if (dSettings.ContainsKey(Present))
                        {
                            //ini.SetValue("Settings", "SelectedPresent", Present);
                            if (Giveaway_TypeActive.Checked)
                            {
                                ini.SetValue(Present, "Giveaway_Type", "0");
                            }
                            else if (Giveaway_TypeKeyword.Checked)
                            {
                                ini.SetValue(Present, "Giveaway_Type", "1");
                            }
                            else if (Giveaway_TypeTickets.Checked)
                            {
                                ini.SetValue(Present, "Giveaway_Type", "2");
                            }
                            Giveaway_ActiveUserTime.Enabled = Giveaway_TypeActive.Checked;

                            ini.SetValue(Present, "Giveaway_TicketCost", Giveaway_TicketCost.Value.ToString());
                            ini.SetValue(Present, "Giveaway_MaxTickets", Giveaway_MaxTickets.Value.ToString());
                            ini.SetValue(Present, "Giveaway_MustFollow", Giveaway_MustFollow.Checked ? "1" : "0");
                            ini.SetValue(Present, "Giveaway_MustSubscribe", Giveaway_MustSubscribe.Checked ? "1" : "0");
                            ini.SetValue(Present, "Giveaway_MustWatch", Giveaway_MustWatch.Checked ? "1" : "0");
                            ini.SetValue(Present, "Giveaway_MustWatchDays", Giveaway_MustWatchDays.Value.ToString());
                            ini.SetValue(Present, "Giveaway_MustWatchHours", Giveaway_MustWatchHours.Value.ToString());
                            ini.SetValue(Present, "Giveaway_MustWatchMinutes", Giveaway_MustWatchMinutes.Value.ToString());
                            ini.SetValue(Present, "Giveaway_MinCurrencyChecked", Giveaway_MinCurrency.Checked ? "1" : "0");
                            ini.SetValue(Present, "Giveaway_MinCurrency", Giveaway_MinCurrencyBox.Value.ToString());
                            ini.SetValue(Present, "Giveaway_ActiveUserTime", Giveaway_ActiveUserTime.Value.ToString());
                            ini.SetValue(Present, "Giveaway_AutoBanWinner", Giveaway_AutoBanWinner.Checked ? "1" : "0");
                            ini.SetValue(Present, "Giveaway_WarnFalseEntries", Giveaway_WarnFalseEntries.Checked ? "1" : "0");
                            ini.SetValue(Present, "Giveaway_AnnounceWarnedEntries", Giveaway_AnnounceWarnedEntries.Checked ? "1" : "0");
                            ini.SetValue(Present, "Giveaway_SubscribersWinMultiplier", Giveaway_SubscribersWinMultiplier.Checked ? "1" : "0");
                            ini.SetValue(Present, "Giveaway_SubscribersWinMultiplierAmount", Giveaway_SubscribersWinMultiplierAmount.Value.ToString());
                            ini.SetValue(Present, "Giveaway_CustomKeyword", Giveaway_CustomKeyword.Text);
                            string items = "";
                            foreach (object item in Giveaway_BanListListBox.Items)
                            {
                                items = items + item.ToString() + ";";
                                //Console.WriteLine("Ban : " + item.ToString());
                            }
                            ini.SetValue(Present, "Giveaway_BanList", items);
                        }
                    }
                    if (ReloadSettings) GetSettings();
                }
            }).Start();
        }

        private void Giveaway_WinnerTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (Irc.ActiveUsers.ContainsKey(Api.capName(Giveaway_WinnerLabel.Text)))
                {
                    int time = Api.GetUnixTimeNow() - Irc.ActiveUsers[Api.capName(Giveaway_WinnerLabel.Text)];
                    int color = time - 120;
                    if (color >= 0 && color < 120)
                    {
                        color = 200 / 120 * color;
                        Giveaway_WinnerTimerLabel.ForeColor = Color.FromArgb(color, 200, 0);
                    }
                    else if (color >= 120)
                    {
                        if (color <= 180)
                        {
                            color = 255 / 60 * (color - 120);
                            int red = 200;
                            if (color > 200)
                            {
                                red = color;
                                color = 200;
                            }
                            Giveaway_WinnerTimerLabel.ForeColor = Color.FromArgb(red, 200 - color, 0);
                        }
                        else
                        {
                            Giveaway_WinnerTimerLabel.ForeColor = Color.FromArgb(255, 0, 0);
                        }
                    }

                    TimeSpan t = TimeSpan.FromSeconds(time);
                    if (t.Days > 0)
                    {
                        Giveaway_WinnerTimerLabel.Text = t.ToString(@"d\:hh\:mm\:ss");
                    }
                    else if (t.Hours > 0)
                    {
                        Giveaway_WinnerTimerLabel.Text = t.ToString(@"h\:mm\:ss");
                    }
                    else
                    {
                        Giveaway_WinnerTimerLabel.Text = t.ToString(@"m\:ss");
                    }
                }

                if (Giveaway.LastRoll > 0)
                {
                    int time = Api.GetUnixTimeNow() - Giveaway.LastRoll;
                    int color = time;
                    if (color >= 0 && color < 60)
                    {
                        color = 200 / 60 * color;
                        Giveaway_WinTimeLabel.ForeColor = Color.FromArgb(color, 200, 0);
                    }
                    else if (color >= 60)
                    {
                        if (color <= 90)
                        {
                            color = 255 / 30 * (color - 60);
                            int red = 200;
                            if (color > 200)
                            {
                                red = color;
                                color = 200;
                            }
                            Giveaway_WinTimeLabel.ForeColor = Color.FromArgb(red, 200 - color, 0);
                        }
                        else
                        {
                            Giveaway_WinTimeLabel.ForeColor = Color.FromArgb(255, 0, 0);
                        }
                    }

                    TimeSpan t = TimeSpan.FromSeconds(time);
                    if (t.Days > 0)
                    {
                        Giveaway_WinTimeLabel.Text = t.ToString(@"d\:hh\:mm\:ss");
                    }
                    else if (t.Hours > 0)
                    {
                        Giveaway_WinTimeLabel.Text = t.ToString(@"h\:mm\:ss");
                    }
                    else
                    {
                        Giveaway_WinTimeLabel.Text = t.ToString(@"m\:ss");
                    }
                }
            }
            catch
            {
            }
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!(e.Cancel = (MessageBox.Show(DisconnectButton.Enabled ? "ModBot is currently active! Are you sure you want to close it?" : "Are you sure you want to close ModBot?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)))
            {
                Environment.Exit(0);
                /*Console.WriteLine("Closing...");
                List<Thread> Ts = new List<Thread>();
                foreach (Thread t in Threads)
                {
                    t.Abort();
                    Ts.Add(t);
                }
                Threads.Clear();
                foreach (Thread t in Irc.Threads)
                {
                    t.Abort();
                    Ts.Add(t);
                }
                Irc.Threads.Clear();
                foreach (Thread t in Api.dCheckingDisplayName.Values)
                {
                    t.Abort();
                    Ts.Add(t);
                }
                Api.dCheckingDisplayName.Clear();
                foreach (Thread t in Ts)
                {
                    while (t.IsAlive) Thread.Sleep(10);
                }*/
            }
        }

        private void SettingsPresents_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!bIgnoreUpdates)
            {
                ini.SetValue("Settings", "SelectedPresent", Giveaway_SettingsPresents.TabPages[Giveaway_SettingsPresents.SelectedIndex].Text);
                SaveSettings(iSettingsPresent, true);
                iSettingsPresent = Giveaway_SettingsPresents.SelectedIndex;
            }
        }

        private void Currency_HandoutType_Changed(object sender, EventArgs e)
        {
            if (Currency_HandoutEveryone.Checked)
            {
                ini.SetValue("Settings", "Currency_Handout", "0");
            }
            else if (Currency_HandoutActiveStream.Checked)
            {
                ini.SetValue("Settings", "Currency_Handout", "1");
            }
            else if (Currency_HandoutActiveTime.Checked)
            {
                ini.SetValue("Settings", "Currency_Handout", "2");
            }
            Currency_HandoutLastActive.Enabled = Currency_HandoutActiveTime.Checked;
        }

        private void Currency_HandoutLastActive_ValueChanged(object sender, EventArgs e)
        {
            ini.SetValue("Settings", "Currency_HandoutTime", Currency_HandoutLastActive.Value.ToString());
        }

        private void WindowChanged(object sender, EventArgs e)
        {
            /*CheckBox CB = (CheckBox)sender;
            if (CB.Checked)
            {
                CB.Enabled = false;
                WindowButtons[CB].Visible = true;
                foreach (CheckBox cb in WindowButtons.Keys)
                {
                    if (cb != CB)
                    {
                        cb.Enabled = true;
                        cb.Checked = false;
                        WindowButtons[cb].Visible = false;
                    }
                }
            }*/
            CheckBox CB = (CheckBox)sender;
            if (CB.Checked)
            {
                //CB.Enabled = false;
                CurrentWindow = Windows[CB];
                Windows[CB].BringToFront();
                foreach (CheckBox cb in Windows.Keys)
                {
                    if (cb != CB)
                    {
                        //cb.Enabled = true;
                        cb.Checked = false;
                    }
                }
            }
            else
            {
                if (Windows[CB] == CurrentWindow)
                {
                    CB.Checked = true;
                    Windows[CB].BringToFront();
                }
            }

            if (CurrentWindow != ChannelWindow)
            {
                MetadataModified = false;
            }
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            ini.SetValue("Settings", "BOT_Name", Irc.nick = BotNameBox.Text);
            Irc.nick = BotNameBox.Text.ToLower();
            ini.SetValue("Settings", "BOT_Password", Irc.password = BotPasswordBox.Text);
            ini.SetValue("Settings", "Channel_Name", ChannelBox.Text);
            Irc.admin = ChannelBox.Text.Replace("#", "");
            Irc.channel = "#" + ChannelBox.Text.Replace("#", "").ToLower();
            ini.SetValue("Settings", "Channel_Token", Irc.channeltoken = ChannelTokenBox.Text);
            if (Irc.channeltoken.StartsWith("oauth:")) Irc.channeltoken = ChannelTokenBox.Text.Substring(6);
            ini.SetValue("Settings", "Currency_Name", Irc.currencyName = CurrencyNameBox.Text);
            ini.SetValue("Settings", "Currency_Command", Irc.currency = CurrencyCommandBox.Text.StartsWith("!") ? CurrencyCommandBox.Text.Substring(1) : CurrencyCommandBox.Text);
            ini.SetValue("Settings", "Currency_Interval", CurrencyHandoutInterval.Value.ToString());
            Irc.interval = Convert.ToInt32(CurrencyHandoutInterval.Value.ToString());
            ini.SetValue("Settings", "Currency_Payout", CurrencyHandoutAmount.Value.ToString());
            Irc.payout = Convert.ToInt32(CurrencyHandoutAmount.Value.ToString());
            ini.SetValue("Settings", "Currency_SubscriberPayout", CurrencySubHandoutAmount.Value.ToString());
            Irc.subpayout = Convert.ToInt32(CurrencySubHandoutAmount.Value.ToString());
            ini.SetValue("Settings", "Donations_ClientID", Irc.donation_clientid = DonationsClientIdBox.Text);
            ini.SetValue("Settings", "Donations_Token", Irc.donation_token = DonationsTokenBox.Text);
            if (SubLinkBox.Text != "")
            {
                if ((SubLinkBox.Text.StartsWith("https://spreadsheets.google.com") || SubLinkBox.Text.StartsWith("http://spreadsheets.google.com")) && SubLinkBox.Text.EndsWith("?alt=json"))
                {
                    ini.SetValue("Settings", "Subsribers_URL", SubLinkBox.Text);
                }
                else
                {
                    Console.WriteLine("Invalid subscriber link. Reverting to the last known good link, or blank. Restart the program to fix it.");
                }
            }

            new Thread(() => { Irc.Initialize(); }).Start();
        }

        private void DisconnectButton_Click(object sender, EventArgs e)
        {
            new Thread(() => { Irc.Disconnect(); }).Start();
        }

        private void WebsiteLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Process.Start("https://sourceforge.net/projects/twitchmodbot/");
            Process.Start("http://modbot.wordpress.com/");
        }

        private void SupportLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Process.Start("http://modbot.wordpress.com/about/");
            Process.Start("http://modbot.wordpress.com/");
        }

        private void EmailLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("mailto:CoMaNdO.ModBot@gmail.com");
        }

        private void Donations_List_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > 4)
            {
                string sDonationsIgnoreRecent = "", sDonationsIgnoreLatest = "", sDonationsIgnoreTop = "";

                foreach (DataGridViewRow row in Donations_List.Rows)
                {
                    string sId = row.Cells["ID"].Value.ToString();

                    if (row.Cells["IncludeRecent"].Value.ToString().Equals("False")) sDonationsIgnoreRecent += sId + ",";

                    if (row.Cells["IncludeLatest"].Value.ToString().Equals("False")) sDonationsIgnoreLatest += sId + ",";

                    if (row.Cells["IncludeTop"].Value.ToString().Equals("False")) sDonationsIgnoreTop += sId + ",";
                }

                ini.SetValue("Settings", "Donations_Ignore_Recent", sDonationsIgnoreRecent);
                ini.SetValue("Settings", "Donations_Ignore_Latest", sDonationsIgnoreLatest);
                ini.SetValue("Settings", "Donations_Ignore_Top", sDonationsIgnoreTop);
            }
        }

        private void RecentDonorsLimit_ValueChanged(object sender, EventArgs e)
        {
            ini.SetValue("Settings", "Donations_Recent_Limit", RecentDonorsLimit.Value.ToString());
        }

        private void TopDonorsLimit_ValueChanged(object sender, EventArgs e)
        {
            ini.SetValue("Settings", "Donations_Top_Limit", TopDonorsLimit.Value.ToString());
        }

        private void Donations_List_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            if (e.Column.Name == "Date")
            {
                e.SortResult = Convert.ToDateTime(e.CellValue1.ToString()).CompareTo(Convert.ToDateTime(e.CellValue2.ToString()));
                e.Handled = true;
            }
            else if (e.Column.Name == "Amount")
            {
                e.SortResult = float.Parse(e.CellValue1.ToString()).CompareTo(float.Parse(e.CellValue2.ToString()));
                e.Handled = true;
            }
        }

        private void ConnectionDetailsChanged(object sender, EventArgs e)
        {
            //ConnectButton.Enabled = ((SettingsErrorLabel.Text = (BotNameBox.Text.Length < 3 ? "Bot name too short or the field is empty.\r\n" : "") + (!BotPasswordBox.Text.StartsWith("oauth:") ? (BotPasswordBox.Text == "" ? "Bot's oauth token field is empty.\r\n" : "Bot's oauth token field must contain \"oauth:\" at the beginning.\r\n") : "") + (ChannelBox.Text.Length < 3 ? "Channel name too short or the field is empty.\r\n" : "") + (ChannelTokenBox.Text == "" ? "Channel token is missing.\r\n" : "") + (CurrencyNameBox.Text.Length < 2 ? "Currency name too short or the field is empty.\r\n" : "") + (CurrencyCommandBox.Text.Length < 2 ? "Currency command too short or the field is empty.\r\n" : "") + (CurrencyCommandBox.Text.Contains(" ") ? "The currency command can not contain spaces.\r\n" : "")) == "");
            SettingsErrorLabel.Text = (BotNameBox.Text.Length < 3 ? "Bot name too short or the field is empty.\r\n" : "") + (!BotPasswordBox.Text.StartsWith("oauth:") ? (BotPasswordBox.Text == "" ? "Bot's oauth token field is empty.\r\n" : "Bot's oauth token field must contain \"oauth:\" at the beginning.\r\n") : "") + (ChannelBox.Text.Length < 3 ? "Channel name too short or the field is empty.\r\n" : "") + (ChannelTokenBox.Text == "" ? "Channel token is missing.\r\n" : "") + (CurrencyNameBox.Text.Length < 2 ? "Currency name too short or the field is empty.\r\n" : "") + (CurrencyCommandBox.Text.Length < 2 ? "Currency command too short or the field is empty.\r\n" : "") + (CurrencyCommandBox.Text.Contains(" ") ? "The currency command can not contain spaces.\r\n" : "");
        }

        private void GenerateToken_Request(object sender, EventArgs e)
        {
            foreach (Control ctrl in SettingsWindow.Controls)
            {
                if (ctrl.GetType() != typeof(Label) && ctrl != Misc_ShowConsole)
                {
                    ctrl.Enabled = false;
                }
            }
            SettingsWindowButton.Enabled = false;
            AboutWindowButton.Enabled = false;
            if ((Button)sender == GenerateBotTokenButton)
            {
                AuthenticationScopes = "chat_login";
            }
            else if ((Button)sender == GenerateChannelTokenButton)
            {
                AuthenticationScopes = "user_read channel_editor channel_commercial channel_check_subscription channel_subscriptions chat_login";
            }
            AuthenticationBrowser.Url = new Uri("https://api.twitch.tv/kraken/oauth2/authorize?response_type=token&client_id=9c70dw37ms89rfhn0jbkdxmtzf5egdq&redirect_uri=http://twitch.tv/&scope=" + AuthenticationScopes);
        }

        private void AuthenticationBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            if (e.Url.ToString().StartsWith("https://api.twitch.tv/kraken/oauth2/"))
            {
                AuthenticationBrowser.BringToFront();
                if (AuthenticationScopes == "chat_login")
                {
                    AuthenticationLabel.Text = "Connect to the bot's account";
                }
                else if (AuthenticationScopes == "user_read channel_editor channel_commercial channel_check_subscription channel_subscriptions chat_login")
                {
                    AuthenticationLabel.Text = "Connect to your account (channel)";
                }
                AuthenticationLabel.BringToFront();
            }
            else if (e.Url.Fragment.Contains("access_token"))
            {
                if (AuthenticationScopes == "chat_login")
                {
                    BotPasswordBox.Text = "oauth:" + e.Url.Fragment.Substring(14).Split('&')[0];
                }
                else if (AuthenticationScopes == "user_read channel_editor channel_commercial channel_check_subscription channel_subscriptions chat_login")
                {
                    ChannelTokenBox.Text = e.Url.Fragment.Substring(14).Split('&')[0];
                }

                AuthenticationScopes = "";
                foreach (Control ctrl in SettingsWindow.Controls)
                {
                    ctrl.Enabled = true;
                }
                DisconnectButton.Enabled = false;
                SettingsWindow.BringToFront();
                SettingsWindowButton.Enabled = true;
                AboutWindowButton.Enabled = true;
                AuthenticationBrowser.Url = new Uri("http://www.twitch.tv/logout");
            }
            else if (e.Url.ToString() == "http://www.twitch.tv/" || e.Url.ToString().StartsWith("http://www.twitch.tv/?error="))
            {
                if (e.Url.ToString().StartsWith("http://www.twitch.tv/?error="))
                {
                    AuthenticationScopes = "";
                    foreach (Control ctrl in SettingsWindow.Controls)
                    {
                        ctrl.Enabled = true;
                    }
                    DisconnectButton.Enabled = false;
                    SettingsWindow.BringToFront();
                    SettingsWindowButton.Enabled = true;
                    AboutWindowButton.Enabled = true;
                    GenerateBotTokenButton.Enabled = true;
                }

                if (AuthenticationScopes == "")
                {
                    AuthenticationBrowser.Url = null;
                }
                else
                {
                    AuthenticationLabel.SendToBack();
                    AuthenticationBrowser.Dispose(); // A weird workaround I had to use as for some reason it wouldn't let me use the same Web Browser to get the token if a login was required before generating the token.
                    AuthenticationBrowser = new WebBrowser();
                    AuthenticationBrowser.ScriptErrorsSuppressed = true;
                    AuthenticationBrowser.Location = new Point(108, 30);
                    AuthenticationBrowser.Size = new Size(814, 562);
                    AuthenticationBrowser.Navigated += new WebBrowserNavigatedEventHandler(this.AuthenticationBrowser_Navigated);
                    AuthenticationBrowser.Url = new Uri("https://api.twitch.tv/kraken/oauth2/authorize?response_type=token&client_id=9c70dw37ms89rfhn0jbkdxmtzf5egdq&redirect_uri=http://twitch.tv/&scope=" + AuthenticationScopes);
                    Controls.Add(AuthenticationBrowser);
                }
            }
        }

        private void UpdateTitleGameButton_Click(object sender, EventArgs e)
        {
            new Thread(() =>
            {
                if (Api.UpdateMetadata(ChannelTitleBox.Text, ChannelGameBox.Text))
                {
                    if (MetadataModified) MetadataModified = false;
                }
            }).Start();
        }

        private void TitleGame_Modified(object sender, EventArgs e)
        {
            MetadataModified = true;
        }

        private void DonateImage_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=4GJUF2L9KUKP8");
        }

        private void Spam_CWLBox_TextChanged(object sender, EventArgs e)
        {
            ini.SetValue("Settings", "Spam_CWhiteList", Spam_CWLBox.Text);
        }

        private void Channel_SteamID64_TextChanged(object sender, EventArgs e)
        {
            ini.SetValue("Settings", "Channel_SteamID64", Channel_SteamID64.Text);
        }

        private void About_Users_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            if (e.Column.Name == "Viewers")
            {
                e.SortResult = (int.Parse(e.CellValue2.ToString())).CompareTo(int.Parse(e.CellValue1.ToString()));
                if (e.SortResult == 0)
                {
                    e.SortResult = About_Users.Rows[e.RowIndex2].Cells["Status"].Value.ToString().CompareTo(About_Users.Rows[e.RowIndex1].Cells["Status"].Value.ToString());
                    if (e.SortResult == 0)
                    {
                        e.SortResult = About_Users.Rows[e.RowIndex1].Cells["Game"].Value.ToString().ToString() == "" && About_Users.Rows[e.RowIndex2].Cells["Game"].Value.ToString().ToString() != "" ? 1 : About_Users.Rows[e.RowIndex2].Cells["Game"].Value.ToString().ToString() == "" && About_Users.Rows[e.RowIndex1].Cells["Game"].Value.ToString().ToString() != "" ? -1 : About_Users.Rows[e.RowIndex1].Cells["Game"].Value.ToString().CompareTo(About_Users.Rows[e.RowIndex2].Cells["Game"].Value.ToString());
                        if (e.SortResult == 0)
                        {
                            e.SortResult = Convert.ToDateTime(About_Users.Rows[e.RowIndex2].Cells["Updated"].Value.ToString()).CompareTo(Convert.ToDateTime(About_Users.Rows[e.RowIndex1].Cells["Updated"].Value.ToString()));
                            if (e.SortResult == 0)
                            {
                                string[] cell1 = About_Users.Rows[e.RowIndex2].Cells["Version"].Value.ToString().Split('.'), cell2 = About_Users.Rows[e.RowIndex1].Cells["Version"].Value.ToString().Split('.');
                                e.SortResult = TimeSpan.FromDays(int.Parse(cell1[2])).Add(TimeSpan.FromSeconds(int.Parse(cell1[3]))).CompareTo(TimeSpan.FromDays(int.Parse(cell2[2])).Add(TimeSpan.FromSeconds(int.Parse(cell2[3]))));
                            }
                        }
                    }
                }
                e.Handled = true;
            }
            else if (e.Column.Name == "Updated")
            {
                e.SortResult = Convert.ToDateTime(e.CellValue2.ToString()).CompareTo(Convert.ToDateTime(e.CellValue1.ToString()));
                if (e.SortResult == 0)
                {
                    string[] cell1 = About_Users.Rows[e.RowIndex2].Cells["Version"].Value.ToString().Split('.'), cell2 = About_Users.Rows[e.RowIndex1].Cells["Version"].Value.ToString().Split('.');
                    e.SortResult = TimeSpan.FromDays(int.Parse(cell1[2])).Add(TimeSpan.FromSeconds(int.Parse(cell1[3]))).CompareTo(TimeSpan.FromDays(int.Parse(cell2[2])).Add(TimeSpan.FromSeconds(int.Parse(cell2[3]))));
                    if (e.SortResult == 0)
                    {
                        e.SortResult = About_Users.Rows[e.RowIndex2].Cells["Status"].Value.ToString().CompareTo(About_Users.Rows[e.RowIndex1].Cells["Status"].Value.ToString());
                        if (e.SortResult == 0)
                        {
                            e.SortResult = int.Parse(About_Users.Rows[e.RowIndex2].Cells["Viewers"].Value.ToString()).CompareTo(int.Parse(About_Users.Rows[e.RowIndex1].Cells["Viewers"].Value.ToString()));
                            if (e.SortResult == 0)
                            {
                                e.SortResult = About_Users.Rows[e.RowIndex1].Cells["Game"].Value.ToString().ToString() == "" && About_Users.Rows[e.RowIndex2].Cells["Game"].Value.ToString().ToString() != "" ? 1 : About_Users.Rows[e.RowIndex2].Cells["Game"].Value.ToString().ToString() == "" && About_Users.Rows[e.RowIndex1].Cells["Game"].Value.ToString().ToString() != "" ? -1 : About_Users.Rows[e.RowIndex1].Cells["Game"].Value.ToString().CompareTo(About_Users.Rows[e.RowIndex2].Cells["Game"].Value.ToString());
                            }
                        }
                    }
                }
                e.Handled = true;
            }
            else if (e.Column.Name == "Version")
            {
                string[] cell1 = e.CellValue2.ToString().Split('.'), cell2 = e.CellValue1.ToString().Split('.');
                e.SortResult = TimeSpan.FromDays(int.Parse(cell1[2])).Add(TimeSpan.FromSeconds(int.Parse(cell1[3]))).CompareTo(TimeSpan.FromDays(int.Parse(cell2[2])).Add(TimeSpan.FromSeconds(int.Parse(cell2[3]))));
                if (e.SortResult == 0)
                {
                    e.SortResult = About_Users.Rows[e.RowIndex2].Cells["Status"].Value.ToString().CompareTo(About_Users.Rows[e.RowIndex1].Cells["Status"].Value.ToString());
                    if (e.SortResult == 0)
                    {
                        e.SortResult = int.Parse(About_Users.Rows[e.RowIndex2].Cells["Viewers"].Value.ToString()).CompareTo(int.Parse(About_Users.Rows[e.RowIndex1].Cells["Viewers"].Value.ToString()));
                        if (e.SortResult == 0)
                        {
                            e.SortResult = About_Users.Rows[e.RowIndex1].Cells["Game"].Value.ToString().ToString() == "" && About_Users.Rows[e.RowIndex2].Cells["Game"].Value.ToString().ToString() != "" ? 1 : About_Users.Rows[e.RowIndex2].Cells["Game"].Value.ToString().ToString() == "" && About_Users.Rows[e.RowIndex1].Cells["Game"].Value.ToString().ToString() != "" ? -1 : About_Users.Rows[e.RowIndex1].Cells["Game"].Value.ToString().CompareTo(About_Users.Rows[e.RowIndex2].Cells["Game"].Value.ToString());
                            if (e.SortResult == 0)
                            {
                                e.SortResult = Convert.ToDateTime(About_Users.Rows[e.RowIndex2].Cells["Updated"].Value.ToString()).CompareTo(Convert.ToDateTime(About_Users.Rows[e.RowIndex1].Cells["Updated"].Value.ToString()));
                            }
                        }
                    }
                }
                e.Handled = true;
            }
            else if (e.Column.Name == "Status")
            {
                e.SortResult = e.CellValue2.ToString().CompareTo(e.CellValue1.ToString());
                if (e.SortResult == 0)
                {
                    e.SortResult = int.Parse(About_Users.Rows[e.RowIndex2].Cells["Viewers"].Value.ToString()).CompareTo(int.Parse(About_Users.Rows[e.RowIndex1].Cells["Viewers"].Value.ToString()));
                    if (e.SortResult == 0)
                    {
                        e.SortResult = About_Users.Rows[e.RowIndex1].Cells["Game"].Value.ToString().ToString() == "" && About_Users.Rows[e.RowIndex2].Cells["Game"].Value.ToString().ToString() != "" ? 1 : About_Users.Rows[e.RowIndex2].Cells["Game"].Value.ToString().ToString() == "" && About_Users.Rows[e.RowIndex1].Cells["Game"].Value.ToString().ToString() != "" ? -1 : About_Users.Rows[e.RowIndex1].Cells["Game"].Value.ToString().CompareTo(About_Users.Rows[e.RowIndex2].Cells["Game"].Value.ToString());
                        if (e.SortResult == 0)
                        {
                            e.SortResult = Convert.ToDateTime(About_Users.Rows[e.RowIndex2].Cells["Updated"].Value.ToString()).CompareTo(Convert.ToDateTime(About_Users.Rows[e.RowIndex1].Cells["Updated"].Value.ToString()));
                            if (e.SortResult == 0)
                            {
                                string[] cell1 = About_Users.Rows[e.RowIndex2].Cells["Version"].Value.ToString().Split('.'), cell2 = About_Users.Rows[e.RowIndex1].Cells["Version"].Value.ToString().Split('.');
                                e.SortResult = TimeSpan.FromDays(int.Parse(cell1[2])).Add(TimeSpan.FromSeconds(int.Parse(cell1[3]))).CompareTo(TimeSpan.FromDays(int.Parse(cell2[2])).Add(TimeSpan.FromSeconds(int.Parse(cell2[3]))));
                            }
                        }
                    }
                }
                e.Handled = true;
            }
            else if (e.Column.Name == "Game")
            {
                e.SortResult = e.CellValue1.ToString() == "" && e.CellValue2.ToString() != "" ? 1 : e.CellValue2.ToString() == "" && e.CellValue1.ToString() != "" ? -1 : e.CellValue1.ToString().CompareTo(e.CellValue2.ToString());
                if (e.SortResult == 0)
                {
                    e.SortResult = About_Users.Rows[e.RowIndex2].Cells["Status"].Value.ToString().CompareTo(About_Users.Rows[e.RowIndex1].Cells["Status"].Value.ToString());
                    if (e.SortResult == 0)
                    {
                        e.SortResult = int.Parse(About_Users.Rows[e.RowIndex2].Cells["Viewers"].Value.ToString()).CompareTo(int.Parse(About_Users.Rows[e.RowIndex1].Cells["Viewers"].Value.ToString()));
                        if (e.SortResult == 0)
                        {
                            e.SortResult = Convert.ToDateTime(About_Users.Rows[e.RowIndex2].Cells["Updated"].Value.ToString()).CompareTo(Convert.ToDateTime(About_Users.Rows[e.RowIndex1].Cells["Updated"].Value.ToString()));
                            if (e.SortResult == 0)
                            {
                                string[] cell1 = About_Users.Rows[e.RowIndex2].Cells["Version"].Value.ToString().Split('.'), cell2 = About_Users.Rows[e.RowIndex1].Cells["Version"].Value.ToString().Split('.');
                                e.SortResult = TimeSpan.FromDays(int.Parse(cell1[2])).Add(TimeSpan.FromSeconds(int.Parse(cell1[3]))).CompareTo(TimeSpan.FromDays(int.Parse(cell2[2])).Add(TimeSpan.FromSeconds(int.Parse(cell2[3]))));
                            }
                        }
                    }
                }
                e.Handled = true;
            }
            else if (e.Column.Name == "Title")
            {
                e.SortResult = e.CellValue1.ToString() == "" && e.CellValue2.ToString() != "" ? 1 : e.CellValue2.ToString() == "" && e.CellValue1.ToString() != "" ? -1 : e.CellValue1.ToString().CompareTo(e.CellValue2.ToString());
            }
        }

        private void Settings_Changed(object sender, EventArgs e)
        {
            if (sender.GetType() == typeof(CheckBox))
            {
                CheckBox cb = (CheckBox)sender;

                if (cb == Currency_DisableCommand)
                {
                    Irc.LastCurrencyDisabledAnnounce = 0;
                }
                else if (cb == UpdateTopDonorsCheckBox)
                {
                    TopDonorsLimit.Enabled = cb.Checked;
                }
                else if (cb == UpdateRecentDonorsCheckBox)
                {
                    RecentDonorsLimit.Enabled = cb.Checked;
                }
                else if (cb == Channel_UseSteam)
                {
                    Channel_SteamID64.Enabled = cb.Checked;
                    long dummy;
                    if (!bIgnoreUpdates && cb.Checked && (Channel_SteamID64.Text.Length < 10 || !long.TryParse(Channel_SteamID64.Text, out dummy))) Process.Start("http://steamidconverter.com/");
                }
                else if (cb == Misc_ShowConsole)
                {
                    if (cb.Checked)
                    {
                        Program.ShowConsole();
                    }
                    else
                    {
                        Program.HideConsole();
                    }
                }
                else if (cb == Channel_SubscriptionRewards)
                {
                    Channel_SubscriptionsDate.Enabled = Channel_SubscriptionRewardsList.Enabled = cb.Checked;
                }
                else if (cb == Channel_ViewersChange)
                {
                    Channel_ViewersChangeInterval.Enabled = Channel_ViewersChangeRate.Enabled = Channel_ViewersChangeMessage.Enabled = cb.Checked;
                }
                else if (cb == Channel_WelcomeSub)
                {
                    Channel_WelcomeSubMessage.Enabled = cb.Checked;
                }

                ini.SetValue("Settings", cb.Name, cb.Checked ? "1" : "0");
            }
            else if (sender.GetType() == typeof(TextBox))
            {
                TextBox tb = (TextBox)sender;

                if (tb == MySQL_Password)
                {
                    /*foreach (char c in ";\\")
                    {
                        if (tb.Text.Contains(c))
                        {
                            if (!SettingsErrorLabel.Text.Contains("MySQL passwords can not contain semicolons (;) or backslashes (\\).\r\n")) SettingsErrorLabel.Text += "MySQL passwords can not contain semicolons (;) or backslashes (\\).\r\n";
                            return;
                        }
                    }
                    SettingsErrorLabel.Text = SettingsErrorLabel.Text.Replace("MySQL passwords can not contain semicolons (;) or backslashes (\\).\r\n", "");*/
                    foreach (char c in "'\"")
                    {
                        if (tb.Text.Contains(c))
                        {
                            if (!SettingsErrorLabel.Text.Contains("MySQL passwords can not contain quotation marks.\r\n")) SettingsErrorLabel.Text += "MySQL passwords can not contain quotation marks.\r\n";
                            return;
                        }
                    }
                    SettingsErrorLabel.Text = SettingsErrorLabel.Text.Replace("MySQL passwords can not contain quotation marks.\r\n", "");
                }
                else if (tb == Database_Table)
                {
                    foreach (char c in " !@#$%^&*()[]{}\\/|'\"~`")
                    {
                        if (tb.Text.Contains(c))
                        {
                            if (!SettingsErrorLabel.Text.Contains("Invalid MySQL table name.\r\n")) SettingsErrorLabel.Text += "Invalid MySQL table name.\r\n";
                            return;
                        }
                    }
                    SettingsErrorLabel.Text = SettingsErrorLabel.Text.Replace("Invalid MySQL table name.\r\n", "");
                }

                ini.SetValue("Settings", tb.Name, tb.Text);
            }
            else if (sender.GetType() == typeof(NumericUpDown) || sender.GetType() == typeof(FlatNumericUpDown))
            {
                NumericUpDown nud = (NumericUpDown)sender;

                if (nud == Channel_ViewersChangeInterval)
                {
                    if (Irc.newViewers != null) Irc.newViewers.Change((int)nud.Value * 60000, (int)nud.Value * 60000);
                }

                ini.SetValue("Settings", nud.Name, nud.Value.ToString());
            }
        }

        private void SettingsErrorLabel_TextChanged(object sender, EventArgs e)
        {
            ConnectButton.Enabled = (SettingsErrorLabel.Text == "");
        }

        private void Channel_SubscriptionRewardsList_Changed(object sender, EventArgs e)
        {
            lock (SubscriptionRewards)
            {
                SubscriptionRewards.Clear();
                string text = "";
                foreach (DataGridViewRow row in Channel_SubscriptionRewardsList.Rows)
                {
                    if (row.Cells["Reward"].Value != null && row.Cells["Instructions"].Value != null && row.Cells["Reward"].Value.ToString() != "")
                    {
                        SubscriptionRewards.Add(row.Cells["Reward"].Value.ToString(), row.Cells["Instructions"].Value.ToString());
                        text += row.Cells["Reward"].Value.ToString() + ";" + row.Cells["Instructions"].Value.ToString() + "\r\n";
                    }
                    if (!Directory.Exists(@"Data\Subscriptions")) Directory.CreateDirectory(@"Data\Subscriptions");
                    File.WriteAllText(@"Data\Subscriptions\Rewards.txt", text);
                }
            }
        }
    }
}