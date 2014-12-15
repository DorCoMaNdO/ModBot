using ModBot;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace CoMaNdO.Giveaways
{
    public delegate void OnRoll(ref List<string> ValidUsers);

    [Export(typeof(IExtension))]
    public class Giveaways : IExtension
    {
        private string LatestVersion;

        public void Load()
        {
            Giveaway.Load(this);
        }

        public string Name { get { return "Giveaways System"; } }
        public string FileName { get { return "CoMaNdO.Giveaways.dll"; } }
        public string Author { get { return "CoMaNdO"; } }
        public string UniqueID { get { return "CoMaNdO.Giveaways"; } }
        public string ContactInfo { get { return "CoMaNdO.ModBot@gmail.com"; } }
        public string Version { get { return "0.0.1"; } }
        public int ApiVersion { get { return 0; } }
        public int LoadPriority { get { return 0; } }

        public bool UpdateCheck()
        {
            using (WebClient w = new WebClient())
            {
                w.Proxy = null;
                try
                {
                    LatestVersion = w.DownloadString("https://dl.dropboxusercontent.com/u/60356733/ModBot/Extensions/Extensions.txt");
                    if (LatestVersion != "")
                    {
                        foreach (string Extension in LatestVersion.Split(Environment.NewLine.ToCharArray()))
                        {
                            string[] data = Extension.Split(';');
                            if (data.Length > 6)
                            {
                                if (data[3] == UniqueID)
                                {
                                    LatestVersion = data[5];

                                    string[] Latest = LatestVersion.Split('.'), Current = Version.Split('.');
                                    int LatestMajor = int.Parse(Latest[0]), LatestMinor = int.Parse(Latest[1]), LatestBuild = int.Parse(Latest[2]);
                                    int CurrentMajor = int.Parse(Current[0]), CurrentMinor = int.Parse(Current[1]), CurrentBuild = int.Parse(Current[2]);
                                    return (LatestMajor > CurrentMajor || LatestMajor == CurrentMajor && LatestMinor > CurrentMinor || LatestMajor == CurrentMajor && LatestMinor == CurrentMinor && LatestBuild > CurrentBuild);
                                }
                            }
                        }
                    }
                }
                catch
                {
                }
            }
            return false;
        }

        public string UpdateURL { get { return "https://dl.dropboxusercontent.com/u/60356733/ModBot/Extensions/" + UniqueID + "/" + LatestVersion + "/" + FileName; } }
    }

    static class Giveaway
    {
        public static GiveawaysWindow Window;
        public static System.Threading.Timer GiveawayQueue, UserList;
        public static int LastAnnouncedTickets = 0;
        public static IExtension extension;
        public static bool Started, IsOpen, Custom;
        public static int LastRoll, Cost, MaxTickets = 0;
        public static float Chance = 0.0F;
        public static Dictionary<string, int> dUsers = new Dictionary<string, int>(), FalseEntries = new Dictionary<string, int>();
        private static Dictionary<Control, bool> dState = new Dictionary<Control, bool>();

        public static event OnRoll OnRoll = (ref List<string> ValidUsers) => { };

        public static void Load(IExtension sender)
        {
            extension = sender;

            UI.AddWindow("Giveaway", Window = new GiveawaysWindow(extension));

            //Events.UI.Loaded += Events_OnUILoaded;
            Events.Connected += Events_Connected;
            Events.Chat.ModeratorsChecked += Events_ModeratorsChecked;
            Events.Chat.MessageReceived += Events_MessageReceived;
            Events.UserAdded += Events_UserAdded;
            Events.UserRemoved += Events_UserRemoved;
            Events.OnDisconnect += Events_OnDisconnect;
        }

        /*private static void Events_OnUILoaded()
        {
            UI.AddWindow("Giveaway", Window = new GiveawaysWindow(extension));
        }*/

        private static void Events_Connected(string channel, string nick, bool partnered)
        {
            Commands.Add("!raffle", Command_Giveaway, 0, 0);
            Commands.Add("!giveaway", Command_Giveaway, 0, 0);
            Commands.Add("!ticket", Command_Ticket, 0, 0);
            Commands.Add("!tickets", Command_Ticket, 0, 0);

            if (GiveawayQueue == null) GiveawayQueue = new System.Threading.Timer(GiveawayQueueHandler, null, Timeout.Infinite, Timeout.Infinite);
            GiveawayQueue.Change(Timeout.Infinite, Timeout.Infinite);

            if (UserList == null) UserList = new System.Threading.Timer(UserListHandler, null, Timeout.Infinite, Timeout.Infinite);
            UserList.Change(Timeout.Infinite, Timeout.Infinite);

            Window.BeginInvoke((MethodInvoker)delegate
            {
                Window.Giveaway_MustSubscribe.Enabled = partnered;
                Window.Giveaway_SubscribersWinMultiplier.Enabled = partnered;
                if (!partnered)
                {
                    Window.Giveaway_MustSubscribe.Checked = false;
                    Window.Giveaway_SubscribersWinMultiplier.Checked = false;
                }

                Window.Giveaway_MinCurrency.Text = "Must have at least                       " + Currency.Name;

                Window.GetSettings();
                
                string sSelectedPresent = Window.ini.GetValue("Settings", "SelectedPresent", "Default");
                if (sSelectedPresent != "")
                {
                    for (int i = 0; i < Window.Giveaway_SettingsPresents.TabPages.Count; i++)
                    {
                        if (Window.Giveaway_SettingsPresents.TabPages[i].Text.Equals(sSelectedPresent))
                        {
                            Window.iSettingsPresent = Window.Giveaway_SettingsPresents.SelectedIndex = i;
                            break;
                        }
                    }
                }
            });
        }

        private static void Command_Giveaway(string user, string cmd, string[] args)
        {
            if (args.Length > 0)
            {
                //ADMIN GIVEAWAY COMMANDS: !giveaway open <TicketCost> <MaxTickets>, !giveaway close, !giveaway draw, !giveaway cancel//
                if (Users.GetLevel(user) >= 1)
                {
                    if (args[0].ToLower() == "announce")
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
                        //sendMessage("Get the party started! Viewers active in chat within the last " + MainForm.Giveaway_ActiveUserTime.Value + " minutes" + (MainForm.Giveaway_MustFollow.Checked ? MainForm.Giveaway_MinCurrency.Checked ? " follow the stream, and have " + MainForm.Giveaway_MinCurrencyBox.Value + " " + currencyName : " and follow the stream" : "") + " will qualify for the giveaway!");
                    }

                    if (Users.GetLevel(user) >= 2)
                    {
                        if (args[0].ToLower() == "roll")
                        {
                            if (Started)
                            {
                                if (!IsOpen)
                                {
                                    Roll();
                                }
                                else
                                {
                                    Chat.SendMessage("The giveaway has to be closed first!");
                                }
                            }
                            else
                            {
                                Chat.SendMessage("No giveaway running!");
                            }
                        }
                        else if (args[0].ToLower() == "start" || args[0].ToLower() == "create" || args[0].ToLower() == "run" && args.Length > 1)
                        {
                            if (!Started)
                            {
                                int type;
                                if (int.TryParse(args[1], out type) && type >= 1 && type <= 3)
                                {
                                    Window.BeginInvoke((MethodInvoker)delegate
                                    {
                                        Window.Giveaway_TypeActive.Checked = (type == 1);
                                        Window.Giveaway_TypeKeyword.Checked = (type == 2);
                                        Window.Giveaway_TypeTickets.Checked = (type == 3);
                                    });

                                    if (Window.Giveaway_TypeTickets.Checked)
                                    {
                                        int ticketcost = 5, maxtickets = 1;

                                        if (args.Length > 2 && !int.TryParse(args[2], out ticketcost)) ticketcost = 5;
                                        if (args.Length > 3 && !int.TryParse(args[3], out maxtickets)) maxtickets = 1;

                                        if (ticketcost >= 0 && maxtickets > 0)
                                        {
                                            Start(true, ticketcost, maxtickets);
                                        }
                                        else
                                        {
                                            Chat.SendMessage("Ticket cost cannot be lower than 0 and max tickets cannot be lower than 1.");
                                        }
                                    }
                                    else if (Window.Giveaway_TypeKeyword.Checked)
                                    {
                                        string keyword = "";
                                        if (args.Length > 2)
                                        {
                                            for (int i = 2; i < args.Length; i++) keyword += args[i] + " ";

                                            keyword.Substring(0, keyword.Length - 1);
                                        }

                                        Window.BeginInvoke((MethodInvoker)delegate
                                        {
                                            Window.Giveaway_CustomKeyword.Text = keyword;
                                            Start();
                                        });
                                    }
                                    else
                                    {
                                        Start();
                                    }
                                }
                            }
                            else
                            {
                                Chat.SendMessage("A giveaway is already running.");
                            }
                        }
                        else if (args[0].ToLower() == "close" || args[0].ToLower() == "lock")
                        {
                            if (Started)
                            {
                                if (IsOpen)
                                {
                                    Close();
                                }
                                else
                                {
                                    Chat.SendMessage("Entries to the giveaway has been closed already.");
                                }
                            }
                            else
                            {
                                Chat.SendMessage("A giveaway is not running.");
                            }
                        }
                        else if (args[0] == "open" || args[0].ToLower() == "unlock")
                        {
                            if (Started)
                            {
                                if (!IsOpen)
                                {
                                    Open();
                                }
                                else
                                {
                                    Chat.SendMessage("Entries to the giveaway are already open.");
                                }
                            }
                            else
                            {
                                Chat.SendMessage("A giveaway is not running.");
                            }
                        }
                        else if (args[0].ToLower() == "stop" || args[0].ToLower() == "end")
                        {
                            if (Started)
                            {
                                End();
                            }
                            else
                            {
                                Chat.SendMessage("A giveaway is not running.");
                            }
                        }
                        else if (args[0].ToLower() == "cancel" || args[0].ToLower() == "abort")
                        {
                            if (Started)
                            {
                                Cancel();
                            }
                        }
                    }
                }

                //REGULAR USER COMMANDS: !giveaway help
                /*if (args[0].ToLower() == "help")
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
                    sMessage = sMessage + "the winner is selected from a list of viewers that were active within the last " + MainForm.Giveaway_ActiveUserTime.Value + " minutes";
                    if (MainForm.Giveaway_MustFollowCheckBox.Checked || MainForm.Giveaway_MinCurrencyCheckBox.Checked) sMessage = sMessage + " and comply the terms";
                    sMessage = sMessage + ".";
                    sendMessage(sMessage);
                }*/

                if ((args[0].ToLower() == "buy" || args[0].ToLower() == "join" || args[0].ToLower() == "purchase" || args[0].ToLower() == "ticket" || args[0].ToLower() == "tickets") && args.Length > 1)
                {
                    Command_Ticket(user, "!ticket", new string[] { args[1] });
                }
            }
            else
            {
                Command_Ticket(user, "!ticket", args);
            }
        }

        private static void Command_Ticket(string user, string cmd, string[] args)
        {
            if (Started && (Window.Giveaway_TypeKeyword.Checked && (Window.Giveaway_CustomKeyword.Text == "" || cmd == "Custom") || Window.Giveaway_TypeTickets.Checked))
            {
                if (IsOpen)
                {
                    if (Window.Giveaway_TypeKeyword.Checked)
                    {
                        if (args.Length > 0 && args[0] == "0")
                        {
                            if (HasBoughtTickets(user))
                            {
                                BuyTickets(user, 0);
                                return;
                            }
                        }

                        if (!HasBoughtTickets(user))
                        {
                            user = user.ToLower();
                            lock (FalseEntries)
                            {
                                if (BuyTickets(user))
                                {
                                    if (FalseEntries.ContainsKey(user)) FalseEntries.Remove(user);
                                    GiveawayQueue.Change(10000, Timeout.Infinite);
                                }
                                else
                                {
                                    if (!FalseEntries.ContainsKey(user))
                                    {
                                        int id = -1;
                                        if (Currency.Check(user) < Cost)
                                        {
                                            id = 1;
                                        }
                                        else if (Window.Giveaway_MustFollow.Checked && !Users.IsFollower(user))
                                        {
                                            id = 2;
                                        }
                                        else if (Window.Giveaway_MustSubscribe.Checked && !Users.IsSubscriber(user))
                                        {
                                            id = 3;
                                        }
                                        else if (Window.Giveaway_MustWatch.Checked && Users.CompareTimeWatched(user, new TimeSpan((int)Window.Giveaway_MustWatchHours.Value, (int)Window.Giveaway_MustWatchMinutes.Value, 0)) == -1)
                                        {
                                            id = 4;
                                        }

                                        FalseEntries.Add(user, id);
                                    }
                                    if (FalseEntries.Count == 1) GiveawayQueue.Change(10000, Timeout.Infinite);
                                }
                            }
                        }
                        else
                        {
                            if (Window.Giveaway_WarnFalseEntries.Checked) Users.Warn(user, 1, 5, "Giveaway entries closed and/or is in the giveaway already", 0, false, true, Window.Giveaway_AnnounceWarnedEntries.Checked, 3);
                        }
                    }
                    else
                    {
                        if (args.Length > 0)
                        {
                            user = user.ToLower();
                            lock (FalseEntries)
                            {
                                int tickets;
                                if (int.TryParse(args[0], out tickets) && tickets >= 0 && BuyTickets(user, tickets))
                                {
                                    if (FalseEntries.ContainsKey(user)) FalseEntries.Remove(user);
                                    GiveawayQueue.Change(10000, Timeout.Infinite);
                                }
                                else
                                {
                                    if (!FalseEntries.ContainsKey(user))
                                    {
                                        int id = -1;
                                        if (tickets < 1 || tickets > MaxTickets)
                                        {
                                            id = 0;
                                        }
                                        else if (Currency.Check(user) < Cost * tickets)
                                        {
                                            id = 1;
                                        }
                                        else if (Window.Giveaway_MustFollow.Checked && !Users.IsFollower(user))
                                        {
                                            id = 2;
                                        }
                                        else if (Window.Giveaway_MustSubscribe.Checked && !Users.IsSubscriber(user))
                                        {
                                            id = 3;
                                        }
                                        else if (Window.Giveaway_MustWatch.Checked && Users.CompareTimeWatched(user, new TimeSpan((int)Window.Giveaway_MustWatchHours.Value, (int)Window.Giveaway_MustWatchMinutes.Value, 0)) == -1)
                                        {
                                            id = 4;
                                        }

                                        FalseEntries.Add(user, id);
                                    }
                                    if (FalseEntries.Count == 1) GiveawayQueue.Change(10000, Timeout.Infinite);
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (Window.Giveaway_WarnFalseEntries.Checked) Users.Warn(user, 1, 5, "Giveaway entries closed and/or is in the giveaway already", 0, false, true, Window.Giveaway_AnnounceWarnedEntries.Checked, 3);
                }
            }
        }

        private static void Events_ModeratorsChecked()
        {
            Window.BeginInvoke((MethodInvoker)delegate
            {
                Window.Giveaway_WarnFalseEntries.Enabled = (!Window.Giveaway_TypeActive.Checked && Chat.Moderators.Contains(Channel.Bot.ToLower()));
                if (Window.Giveaway_TypeActive.Checked || !Chat.Moderators.Contains(Channel.Bot.ToLower())) Window.Giveaway_WarnFalseEntries.Checked = false;
            });
        }

        private static void Events_MessageReceived(string user, string message)
        {
            if (Window.Giveaway_TypeKeyword.Checked && message.ToLower() == Window.Giveaway_CustomKeyword.Text.ToLower())
            {
                Command_Ticket(user, "Custom", new string[0]);
            }

            if (Window.Giveaway_TypeActive.Checked && Started)
            {
                Window.BeginInvoke((MethodInvoker)delegate
                {
                    lock (Window.Giveaway_UserList.Items)
                    {
                        //if (!Chat.IgnoredUsers.Contains(user.ToLower()) && !Window.Giveaway_UserList.Items.Contains(user) && CheckUser(user, Chat.Users.Count < 100))
                        if (!Chat.IgnoredUsers.Contains(user.ToLower()) && !Window.Giveaway_BanListListBox.Items.Contains(user.ToLower()) && !Window.Giveaway_UserList.Items.Contains(user) && CheckUser(user, false))
                        {
                            Window.Giveaway_UserList.Items.Add(user);

                            Window.Giveaway_UserCount.Text = "Count: " + Window.Giveaway_UserList.Items.Count;
                        }
                    }
                });
            }

            if (user.ToLower() == Window.Giveaway_WinnerLabel.Text.ToLower())
            {
                Window.BeginInvoke((MethodInvoker)delegate
                {
                    int start = Window.Giveaway_WinnerChat.SelectionStart, length = Window.Giveaway_WinnerChat.SelectionLength;
                    if (start == Window.Giveaway_WinnerChat.Text.Length) start = -1;
                    Window.Giveaway_WinnerChat.Select(Window.Giveaway_WinnerChat.Text.Length, Window.Giveaway_WinnerChat.Text.Length);
                    Window.Giveaway_WinnerChat.SelectionColor = Color.Blue;
                    if (Chat.UserColors.ContainsKey(user)) Window.Giveaway_WinnerChat.SelectionColor = ColorTranslator.FromHtml(Chat.UserColors[user]);
                    Window.Giveaway_WinnerChat.SelectionFont = new Font("Segoe Print", 7, FontStyle.Bold);
                    Window.Giveaway_WinnerChat.SelectedText = user;
                    Window.Giveaway_WinnerChat.SelectionColor = Color.Black;
                    Window.Giveaway_WinnerChat.SelectionFont = new Font("Tahoma", 8);
                    Window.Giveaway_WinnerChat.SelectedText = ": " + message + "\r\n";
                    if (start > -1)
                    {
                        Window.Giveaway_WinnerChat.Select(start, length);
                    }
                    else
                    {
                        Window.Giveaway_WinnerChat.ScrollToCaret();
                    }
                    Window.Giveaway_WinnerTimerLabel.ForeColor = Color.FromArgb(0, 200, 0);
                });
            }
        }

        private static void Events_UserAdded(string user, bool inital, bool FromReload)
        {
            if (user.Equals(Window.Giveaway_WinnerLabel.Text.ToLower()))
            {
                Window.BeginInvoke((MethodInvoker)delegate
                {
                    int start = Window.Giveaway_WinnerChat.SelectionStart, length = Window.Giveaway_WinnerChat.SelectionLength;
                    if (start == Window.Giveaway_WinnerChat.Text.Length) start = -1;
                    Window.Giveaway_WinnerChat.Select(Window.Giveaway_WinnerChat.Text.Length, Window.Giveaway_WinnerChat.Text.Length);
                    Window.Giveaway_WinnerChat.SelectionColor = Color.Green;
                    Window.Giveaway_WinnerChat.SelectionFont = new Font("Segoe Print", 7, FontStyle.Bold);
                    Window.Giveaway_WinnerChat.SelectedText = user + " has joined the channel.\r\n";
                    if (start > -1)
                    {
                        Window.Giveaway_WinnerChat.Select(start, length);
                    }
                    else
                    {
                        Window.Giveaway_WinnerChat.ScrollToCaret();
                    }
                });
            }
        }

        private static void Events_UserRemoved(string user, bool FromReload)
        {
            if (user.Equals(Window.Giveaway_WinnerLabel.Text.ToLower()))
            {
                Window.BeginInvoke((MethodInvoker)delegate
                {
                    int start = Window.Giveaway_WinnerChat.SelectionStart, length = Window.Giveaway_WinnerChat.SelectionLength;
                    if (start == Window.Giveaway_WinnerChat.Text.Length) start = -1;
                    Window.Giveaway_WinnerChat.Select(Window.Giveaway_WinnerChat.Text.Length, Window.Giveaway_WinnerChat.Text.Length);
                    Window.Giveaway_WinnerTimerLabel.Text = "The winner left!";
                    Window.Giveaway_WinnerTimerLabel.ForeColor = Color.FromArgb(255, 0, 0);

                    Window.Giveaway_WinnerChat.SelectionColor = Color.Red;
                    Window.Giveaway_WinnerChat.SelectionFont = new Font("Segoe Print", 7, FontStyle.Bold);
                    Window.Giveaway_WinnerChat.SelectedText = user + " has left the channel.\r\n";
                    if (start > -1)
                    {
                        Window.Giveaway_WinnerChat.Select(start, length);
                    }
                    else
                    {
                        Window.Giveaway_WinnerChat.ScrollToCaret();
                    }
                });
            }
        }

        private static void Events_OnDisconnect()
        {
            if (Started) Cancel(false);
            LastAnnouncedTickets = 0;
            FalseEntries.Clear();
        }

        public static void Start(bool announce = true, int ticketcost = 0, int maxtickets = 1, bool custom = false)
        {
            Window.BeginInvoke((MethodInvoker)delegate
            {
                Custom = custom;

                Window.Giveaway_SettingsPresents.Enabled = false;
                Window.Giveaway_StartButton.Enabled = false;
                Window.Giveaway_RerollButton.Enabled = false;
                Window.Giveaway_CloseButton.Enabled = true;
                Window.Giveaway_OpenButton.Enabled = false;
                Window.Giveaway_AnnounceWinnerButton.Enabled = false;
                Window.Giveaway_StopButton.Enabled = true;
                Window.Giveaway_AnnounceWinnerButton.Enabled = false;
                dState.Clear();
                foreach (Control ctrl in Window.Controls)
                {
                    if (!dState.ContainsKey(ctrl) && ((ctrl.GetType() == typeof(RadioButton) || ctrl.GetType().BaseType == typeof(NumericUpDown) || ctrl.GetType() == typeof(CheckBox)) && ctrl != Window.Giveaway_AnnounceFalseEntries && ctrl != Window.Giveaway_WarnFalseEntries && ctrl != Window.Giveaway_AnnounceWarnedEntries && ctrl != Window.Giveaway_AutoBanWinner && ctrl != Window.Giveaway_SubscribersWinMultiplier && ctrl != Window.Giveaway_SubscribersWinMultiplierAmount || ctrl == Window.Giveaway_CustomKeyword))
                    {
                        dState.Add(ctrl, ctrl.Enabled);
                        ctrl.Enabled = false;
                    }
                }
                Window.Giveaway_CopyWinnerButton.Enabled = false;
                Window.Giveaway_WinnerTimerLabel.Text = "0:00";
                Window.Giveaway_WinnerTimerLabel.ForeColor = Color.Black;
                Window.Giveaway_WinTimeLabel.Text = "0:00";
                Window.Giveaway_WinTimeLabel.ForeColor = Color.Black;
                Window.Giveaway_WinnerChat.Clear();
                Window.Giveaway_WinnerLabel.Text = "Entries open, close to roll for a winner...";
                Window.Giveaway_WinnerLabel.ForeColor = Color.Red;
                Window.Giveaway_WinnerStatusLabel.Text = "";
                Window.Giveaway_UserList.Items.Clear();
                UserListHandler(null);
                LastRoll = 0;
                Cost = ticketcost;
                MaxTickets = maxtickets;
                dUsers.Clear();
                Started = true;
                IsOpen = true;

                if (!custom)
                {
                    UserList.Change(5000, 5000);

                    string msg = "";
                    if (Window.Giveaway_TypeActive.Checked)
                    {
                        msg = " who sent a message or joined within the last " + Window.Giveaway_ActiveUserTime.Value + " minutes";
                    }
                    if (Window.Giveaway_MustSubscribe.Checked)
                    {
                        if (msg != "")
                        {
                            if (!Window.Giveaway_MustFollow.Checked && !Window.Giveaway_MustWatch.Checked && !Window.Giveaway_MinCurrency.Checked) msg += " and"; else msg += ",";
                        }
                        else
                        {
                            msg += " who";
                        }
                        msg += " subscribes to the stream";
                    }
                    if (Window.Giveaway_MustFollow.Checked)
                    {
                        if (msg != "")
                        {
                            if (!Window.Giveaway_MustWatch.Checked && !Window.Giveaway_MinCurrency.Checked) msg += " and"; else msg += ",";
                        }
                        else
                        {
                            msg += " who";
                        }
                        msg += " follows the stream";
                    }
                    if (Window.Giveaway_MustWatch.Checked)
                    {
                        if (msg != "")
                        {
                            if (!Window.Giveaway_MinCurrency.Checked) msg += " and"; else msg += ",";
                        }
                        else
                        {
                            msg += " who";
                        }
                        msg += " watched the stream for at least " + Window.Giveaway_MustWatchHours.Value + " hours and " + Window.Giveaway_MustWatchMinutes.Value + " minutes";
                    }
                    if (Window.Giveaway_MinCurrency.Checked)
                    {
                        if (msg != "")
                        {
                            msg += " and";
                        }
                        else
                        {
                            msg += " who";
                        }
                        msg += " has " + Window.Giveaway_MinCurrencyBox.Value + " " + Currency.Name;
                    }
                    if (Window.Giveaway_TypeTickets.Checked)
                    {
                        Window.Giveaway_CancelButton.Enabled = true;

                        if (announce) Chat.SendMessage("A giveaway has started! Ticket cost: " + ticketcost + ", max. tickets: " + maxtickets + ". Anyone" + msg + " can join!");
                        if (announce) Chat.SendMessage("Join by typing \"!ticket {amount}\".");
                    }
                    else if (Window.Giveaway_TypeKeyword.Checked)
                    {
                        if (announce) Chat.SendMessage("A giveaway has started! Join by typing \"" + (Window.Giveaway_CustomKeyword.Text == "" ? "!ticket" : Window.Giveaway_CustomKeyword.Text) + "\". Anyone" + msg + " can join!");
                    }
                    else
                    {
                        Close(false, false);

                        if (announce) Chat.SendMessage("A giveaway has started! Anyone" + msg + " qualifies!");
                    }
                }
            });
        }

        public static void Close(bool announce = true, bool open = true)
        {
            IsOpen = false;
            Window.BeginInvoke((MethodInvoker)delegate
            {
                Window.Giveaway_WinnerLabel.Text = "Waiting for a roll...";
                Window.Giveaway_WinnerLabel.ForeColor = Color.Blue;
                Window.Giveaway_RerollButton.Text = "Roll";
                Window.Giveaway_RerollButton.Enabled = true;
                Window.Giveaway_CloseButton.Enabled = false;
                Window.Giveaway_OpenButton.Enabled = true;
                Window.Giveaway_AnnounceWinnerButton.Enabled = false;
            });

            if(!Custom) UserList.Change(15000, 15000);

            if (announce)
            {
                if (!Window.Giveaway_TypeActive.Checked) GiveawayQueue.Change(0, Timeout.Infinite);

                Chat.SendMessage("Entries to the giveaway are now closed.");
            }
        }

        public static void Open(bool announce = true)
        {
            IsOpen = true;
            Window.BeginInvoke((MethodInvoker)delegate
            {
                Window.Giveaway_WinnerLabel.Text = "Entries open, close to roll for a winner...";
                Window.Giveaway_WinnerLabel.ForeColor = Color.Red;
                Window.Giveaway_RerollButton.Text = "Roll";
                Window.Giveaway_RerollButton.Enabled = false;
                Window.Giveaway_CloseButton.Enabled = true;
                Window.Giveaway_OpenButton.Enabled = false;
                Window.Giveaway_AnnounceWinnerButton.Enabled = false;
            });

            if (!Custom) UserList.Change(5000, 5000);

            if (announce)
            {
                if (!Window.Giveaway_TypeActive.Checked) GiveawayQueue.Change(0, Timeout.Infinite);

                Chat.SendMessage("Entries to the giveaway are now open.");
            }
        }

        public static void End(bool announce = true)
        {
            UserList.Change(Timeout.Infinite, Timeout.Infinite);

            Window.BeginInvoke((MethodInvoker)delegate
            {
                Window.Giveaway_SettingsPresents.Enabled = true;
                Window.Giveaway_StartButton.Enabled = true;
                Window.Giveaway_RerollButton.Enabled = false;
                Window.Giveaway_CloseButton.Enabled = false;
                Window.Giveaway_OpenButton.Enabled = false;
                Window.Giveaway_CancelButton.Enabled = false;
                Window.Giveaway_AnnounceWinnerButton.Enabled = false;
                Window.Giveaway_StopButton.Enabled = false;
                foreach (Control ctrl in dState.Keys)
                {
                    ctrl.Enabled = dState[ctrl];
                }
                dState.Clear();
                Window.Giveaway_CopyWinnerButton.Enabled = false;
                Window.Giveaway_WinnerTimerLabel.Text = "0:00";
                Window.Giveaway_WinnerTimerLabel.ForeColor = Color.Black;
                Window.Giveaway_WinTimeLabel.Text = "0:00";
                Window.Giveaway_WinTimeLabel.ForeColor = Color.Black;
                Window.Giveaway_WinnerChat.Clear();
                Window.Giveaway_WinnerLabel.Text = "Giveaway isn't active";
                Window.Giveaway_WinnerLabel.ForeColor = Color.Blue;
                Window.Giveaway_RerollButton.Text = "Roll";
                Window.Giveaway_WinnerStatusLabel.Text = "";
                Window.Giveaway_UserList.Items.Clear();
                Window.Giveaway_UserCount.Text = "Count: " + Window.Giveaway_UserList.Items.Count;
                LastRoll = 0;
                Cost = 0;
                MaxTickets = 0;
                dUsers.Clear();
                Started = false;
                IsOpen = false;
            });

            if (announce) Chat.SendMessage("The giveaway has ended!");
        }

        public static void Cancel(bool announce = true)
        {
            foreach (string user in dUsers.Keys) Currency.Add(user, dUsers[user] * Cost);

            End(false);

            if (announce) Chat.SendMessage("The giveaway has been cancelled" + (!Window.Giveaway_TypeActive.Checked ? ", entries have been refunded." : "."));
        }

        public static bool HasBoughtTickets(string user)
        {
            return dUsers.ContainsKey(user.ToLower());
        }

        public static bool BuyTickets(string user, int tickets = 1)
        {
            string name = Users.GetDisplayName(user, true);
            user = user.ToLower();
            if (Started && (Window.Giveaway_TypeKeyword.Checked || Window.Giveaway_TypeTickets.Checked) && IsOpen && tickets <= MaxTickets && CheckUser(user))
            {
                lock (dUsers)
                {
                    int paid = 0;
                    if (dUsers.ContainsKey(user)) paid = dUsers[user] * Cost;

                    if (Currency.Check(user) + paid >= tickets * Cost)
                    {
                        Currency.Add(user, paid);

                        Window.BeginInvoke((MethodInvoker)delegate
                        {
                            Dictionary<string, int> ActiveUsers = null;
                            lock (Chat.Users) ActiveUsers = Chat.Users.ToDictionary(kv => kv.Key, kv => kv.Value);

                            lock (Window.Giveaway_UserList.Items)
                            {
                                List<string> delete = new List<string>();

                                foreach (string item in Window.Giveaway_UserList.Items) if (item.StartsWith(name) || !ActiveUsers.ContainsKey(item.Split(' ')[0].ToLower())) delete.Add(item);

                                foreach (string item in delete) while (Window.Giveaway_UserList.Items.Contains(item)) Window.Giveaway_UserList.Items.Remove(item);

                                if (tickets > 0) Window.Giveaway_UserList.Items.Add(name + (Window.Giveaway_TypeTickets.Checked ? " (" + tickets + ")" : ""));

                                Window.Giveaway_UserCount.Text = "Count: " + Window.Giveaway_UserList.Items.Count;
                            }
                        });

                        if (tickets < 1)
                        {
                            dUsers.Remove(user);

                            return true;
                        }

                        Currency.Remove(user, tickets * Cost);

                        if (dUsers.ContainsKey(user))
                        {
                            dUsers[user] = tickets;
                        }
                        else
                        {
                            dUsers.Add(user, tickets);
                        }

                        return true;
                    }
                }
            }
            return false;
        }

        public static int GetMinCurrency()
        {
            if (Window.Giveaway_MinCurrency.Checked) return Convert.ToInt32(Window.Giveaway_MinCurrencyBox.Value);

            return 0;
        }

        public static bool CheckUser(string user, bool checkfollow = true, bool checksubscriber = true, bool checktime = true)
        {
            user = user.ToLower();
            return (!Chat.IgnoredUsers.Contains(user) && !Window.Giveaway_BanListListBox.Items.Contains(user) && Currency.Check(user) >= GetMinCurrency() && (!checkfollow || !Window.Giveaway_MustFollow.Checked || Users.IsFollower(user)) && (!checksubscriber || !Window.Giveaway_MustSubscribe.Checked || Users.IsSubscriber(user)) && (!checktime || !Window.Giveaway_MustWatch.Checked || Users.CompareTimeWatched(user, new TimeSpan((int)Window.Giveaway_MustWatchHours.Value, (int)Window.Giveaway_MustWatchMinutes.Value, 0)) >= 0));
        }

        public static string Roll(bool announce = true, bool checkusers = true)
        {
            string winner = "";
            Window.BeginInvoke((MethodInvoker)delegate
            {
                Window.Giveaway_RerollButton.Enabled = false;
                Window.Giveaway_AnnounceWinnerButton.Enabled = false;
                Window.Giveaway_CopyWinnerButton.Enabled = false;
                Window.Giveaway_WinnerStatusLabel.Text = "";
                Window.Giveaway_WinnerLabel.Text = "Rolling...";
                Window.Giveaway_WinnerLabel.ForeColor = Color.Red;
                Window.Giveaway_RerollButton.Text = "Reroll";
                Window.Giveaway_WinnerTimerLabel.Text = "0:00";
                Window.Giveaway_WinnerTimerLabel.ForeColor = Color.Black;
                Window.Giveaway_WinTimeLabel.Text = "0:00";
                Window.Giveaway_WinTimeLabel.ForeColor = Color.Black;
                Window.Giveaway_WinnerChat.Clear();
            });
            LastRoll = 0;

            Thread thread = new Thread(() =>
            {
                //Irc.RefreshUserlist();

                while (true)
                {
                    try
                    {
                        List<string> ValidUsers = new List<string>();
                        if (!Custom)
                        {
                            Dictionary<string, int> ActiveUsers = null;
                            lock (Chat.Users) ActiveUsers = Chat.Users.ToDictionary(kv => kv.Key, kv => kv.Value);

                            if (Window.Giveaway_TypeActive.Checked)
                            {
                                //int ActiveTime = Convert.ToInt32(MainForm.Giveaway_ActiveUserTime.Value) * 60, RollTime = GetUnixTimeNow();
                                //int ActiveTime = Convert.ToInt32(Window.Giveaway_ActiveUserTime.Value) * 60;

                                foreach (string user in ActiveUsers.Keys)
                                {
                                    //if (!ValidUsers.Contains(user) && RollTime - Chat.Users[user] <= ActiveTime && CheckUser(user, Chat.Users.Count < 100))
                                    //if (!ValidUsers.Contains(user) && GetUnixTimeNow() - Chat.Users[user] <= ActiveTime && CheckUser(user, Chat.Users.Count < 100))
                                    //if (!ValidUsers.Contains(user) && GetUnixTimeNow() - Chat.Users[user] <= Convert.ToInt32(Window.Giveaway_ActiveUserTime.Value) * 60 && CheckUser(user, Chat.Users.Count < 100))
                                    if (!ValidUsers.Contains(user) && Api.GetUnixTimeNow() - ActiveUsers[user] <= Convert.ToInt32(Window.Giveaway_ActiveUserTime.Value) * 60 && CheckUser(user, false))
                                    //if (!ValidUsers.Contains(user) && RollTime  Chat.Users[user] <= ActiveTime && CheckUser(user, false, false))
                                    {
                                        ValidUsers.Add(user);

                                        if (Window.Giveaway_SubscribersWinMultiplier.Checked && Users.IsSubscriber(user)) for (int i = 1; i < Window.Giveaway_SubscribersWinMultiplierAmount.Value; i++) ValidUsers.Add(user);
                                    }
                                }
                            }
                            else if (Window.Giveaway_TypeKeyword.Checked || Window.Giveaway_TypeTickets.Checked)
                            {
                                lock (dUsers)
                                {
                                    lock (Window.Giveaway_UserList.Items)
                                    {
                                        List<string> Delete = new List<string>();
                                        foreach (string user in dUsers.Keys)
                                        {
                                            //if (Chat.Users.ContainsKey(user) && CheckUser(user, Chat.Users.Count < 100))
                                            if (ActiveUsers.ContainsKey(user) && CheckUser(user, false))
                                            {
                                                int entries = dUsers[user];

                                                if (Window.Giveaway_SubscribersWinMultiplier.Checked && Users.IsSubscriber(user)) entries *= Convert.ToInt32(Window.Giveaway_SubscribersWinMultiplierAmount.Value);

                                                for (int i = 0; i < entries; i++) ValidUsers.Add(user);
                                            }
                                            else
                                            {
                                                Delete.Add(user);

                                                List<string> delete = new List<string>();

                                                string name = Users.GetDisplayName(user);

                                                foreach (string item in Window.Giveaway_UserList.Items) if (item.StartsWith(name) || !ActiveUsers.ContainsKey(item.Split(' ')[0].ToLower())) delete.Add(item);

                                                foreach (string item in delete) while (Window.Giveaway_UserList.Items.Contains(item)) Window.Giveaway_UserList.Items.Remove(item);

                                                Window.Giveaway_UserCount.Text = "Count: " + Window.Giveaway_UserList.Items.Count;
                                            }
                                        }

                                        foreach (string user in Delete)
                                        {
                                            while (dUsers.ContainsKey(user)) dUsers.Remove(user);

                                            Currency.Add(user, dUsers[user] * Cost);

                                            /*string name = Users.GetDisplayName(user);

                                            List<string> delete = new List<string>();

                                            foreach (string item in Window.Giveaway_UserList.Items) if (item.StartsWith(name) || !Chat.Users.ContainsKey(item.Split(' ')[0].ToLower())) delete.Add(item);

                                            foreach (string item in delete) while (Window.Giveaway_UserList.Items.Contains(item)) Window.Giveaway_UserList.Items.Remove(item);

                                            Window.Giveaway_UserCount.Text = "Count: " + Window.Giveaway_UserList.Items.Count;*/
                                        }
                                    }
                                }
                            }
                        }

                        OnRoll(ref ValidUsers);

                        if (ValidUsers.Count > 0)
                        {
                            winner = ValidUsers[new Random().Next(0, ValidUsers.Count)];
                            if (!Custom)
                            {
                                if (checkusers)
                                {
                                    List<string> Ignore = new List<string>();
                                    while (!CheckUser(winner) || Ignore.Contains(winner))
                                    {
                                        Ignore.Add(winner);
                                        winner = ValidUsers[new Random().Next(0, ValidUsers.Count)];
                                    }
                                }

                                //Chance = 100F / ValidUsers.Count;
                                int tickets = ValidUsers.Count, winnertickets = 1;
                                if (Window.Giveaway_TypeTickets.Checked)
                                {
                                    tickets = dUsers.Count;
                                    winnertickets = winnertickets = dUsers[winner];
                                }
                                Chance = (float)winnertickets / tickets * 100;
                            }
                            Window.BeginInvoke((MethodInvoker)delegate
                            {
                                //string WinnerLabel = "Winner : ";
                                string WinnerLabel = "";
                                if (Users.IsSubscriber(winner)) WinnerLabel += "Subscribing | ";
                                if (Users.IsFollower(winner)) WinnerLabel += "Following | ";
                                //WinnerLabel += Currency.Check(sWinner) + " " + Irc.currencyName + " | Watched : " + Users.GetTimeWatched(sWinner).ToString(@"d\d\ hh\h\ mm\m") + " | Chance : " + Chance.ToString("0.00") + "%";
                                WinnerLabel += Currency.Check(winner) + " " + Currency.Name + " | Watched : " + Users.GetTimeWatched(winner).ToString(@"d\d\ hh\h\ mm\m");
                                winner = Users.GetDisplayName(winner);
                                Window.Giveaway_WinnerStatusLabel.Text = WinnerLabel;
                                Window.Giveaway_WinnerLabel.Text = winner;
                                Window.Giveaway_WinnerTimerLabel.ForeColor = Color.FromArgb(0, 200, 0);
                                Window.Giveaway_WinTimeLabel.ForeColor = Color.FromArgb(0, 200, 0);
                                Window.Giveaway_WinnerLabel.ForeColor = Color.Green;
                                Window.Giveaway_CopyWinnerButton.Enabled = true;
                                Window.Giveaway_AnnounceWinnerButton.Enabled = true;
                                Window.Giveaway_RerollButton.Enabled = true;
                                LastRoll = Api.GetUnixTimeNow();
                                if (Window.Giveaway_AutoBanWinner.Checked && !Window.Giveaway_BanListListBox.Items.Contains(winner.ToLower())) Window.Giveaway_BanListListBox.Items.Add(winner.ToLower());

                                if (announce)
                                {
                                    TimeSpan t = Users.GetTimeWatched(winner);
                                    //Chat.SendMessage(Users.GetDisplayName(winner) + " has won the giveaway! (" + (Users.IsSubscriber(winner) ? "Subscribes to the channel | " : "") + (Users.IsFollower(winner) ? "Follows the channel | " : "") + "Has " + Currency.Check(winner) + " " + Currency.Name + " | Has watched the stream for " + (int)Math.Floor(t.TotalHours) + " hours and " + t.Minutes + " minutes | Chance : " + Chance.ToString("0.00") + "%)");
                                    Chat.SendMessage(Users.GetDisplayName(winner) + " has won the giveaway! (" + (Users.IsSubscriber(winner) ? "Subscribes to the channel | " : "") + (Users.IsFollower(winner) ? "Follows the channel | " : "") + "Has " + Currency.Check(winner) + " " + Currency.Name + " | Has watched the stream for " + (int)Math.Floor(t.TotalHours) + " hours and " + t.Minutes + " minutes)");
                                }
                            });
                            thread = new Thread(() =>
                            {
                                winner = Users.GetDisplayName(winner, true);
                                Window.BeginInvoke((MethodInvoker)delegate
                                {
                                    Window.Giveaway_WinnerLabel.Text = winner;
                                });
                            });
                            thread.Name = "Use winner's (" + winner + ") display name";
                            thread.Start();
                            return;
                        }
                    }
                    catch
                    {
                        Window.BeginInvoke((MethodInvoker)delegate
                        {
                            Console.WriteLine(Window.Giveaway_WinnerLabel.Text = "Error while rolling, retrying...");
                        });
                        continue;
                    }

                    Window.BeginInvoke((MethodInvoker)delegate
                    {
                        Window.Giveaway_WinnerLabel.Text = "No valid winner found";
                        Window.Giveaway_RerollButton.Enabled = true;
                    });

                    if (announce) Chat.SendMessage("No valid winner found, please try again!");
                    return;
                }
            });
            thread.Name = "Roll for winner";
            thread.Start();
            thread.Join();
            return winner;
        }

        private static void GiveawayQueueHandler(object state)
        {
            if (Started && dUsers.Count > 0)
            {
                Chat.SendMessage("A total of " + dUsers.Count + " people joined the giveaway!");

                string finalmessage = "";
                lock (FalseEntries)
                {
                    foreach (string user in FalseEntries.Keys)
                    {
                        string name = Users.GetDisplayName(user);
                        string msg = "you have insufficient " + Currency.Name + ", you don't answer the requirements or the tickets amount you put is invalid";
                        if (FalseEntries[user] == 0)
                        {
                            msg = "the tickets amount you put is invalid";
                        }
                        else if (FalseEntries[user] == 1)
                        {
                            msg = "you have insufficient " + Currency.Name;
                        }
                        else if (FalseEntries[user] == 2)
                        {
                            msg = "you don't follow the channel";
                        }
                        else if (FalseEntries[user] == 3)
                        {
                            msg = "you are not subscribed to the channel";
                        }
                        else if (FalseEntries[user] == 4)
                        {
                            msg = "you haven't watched the stream for long enough";
                        }

                        if (Window.Giveaway_WarnFalseEntries.Checked && Chat.Moderators.Contains(Channel.Bot.ToLower()))
                        {
                            if (Users.Warn(user, 1, 10, "Attempting to buy tickets without meeting the requirements or with insufficient funds or invalid parameters", 3, true, true, Window.Giveaway_AnnounceWarnedEntries.Checked, 6) == 1)
                            {
                                if (finalmessage.Length + msg.Length > 996)
                                {
                                    Chat.SendMessage(finalmessage);
                                    finalmessage = "";
                                }
                                finalmessage += name + ", " + msg + " (Warning: " + Users.Warnings[user] + "/3). ";
                            }
                        }
                        else if (Window.Giveaway_AnnounceFalseEntries.Checked)
                        {
                            if (finalmessage.Length + msg.Length > 996)
                            {
                                Chat.SendMessage(finalmessage);
                                finalmessage = "";
                            }
                            finalmessage += name + ", " + msg + ". ";
                        }
                    }

                    FalseEntries.Clear();
                    if (finalmessage != "") Chat.SendMessage(finalmessage);

                    if (Window.Giveaway_TypeTickets.Checked && Api.GetUnixTimeNow() - LastAnnouncedTickets > 60)
                    {
                        LastAnnouncedTickets = Api.GetUnixTimeNow();
                        Chat.SendMessage("Ticket cost: " + Cost + ", max. tickets: " + MaxTickets + ".");
                    }
                }
            }
        }

        public static void UserListHandler(object state)
        {
            if (Window == null || !Window.IsHandleCreated) return;

            if (!Started)
            {
                Window.BeginInvoke((MethodInvoker)delegate
                {
                    lock (Window.Giveaway_UserList.Items)
                    {
                        Window.Giveaway_UserList.Items.Clear();
                        Window.Giveaway_UserCount.Text = "Count: " + Window.Giveaway_UserList.Items.Count;
                    }
                });
                return;
            }

            if (Window.Giveaway_TypeActive.Checked)
            {
                Window.BeginInvoke((MethodInvoker)delegate
                {
                    Dictionary<string, int> ActiveUsers = null;
                    lock (Chat.Users) ActiveUsers = Chat.Users.ToDictionary(kv => kv.Key, kv => kv.Value);

                    lock (Window.Giveaway_UserList.Items)
                    {
                        foreach (string user in ActiveUsers.Keys)
                        {
                            string name = Users.GetDisplayName(user);

                            //if (!Chat.IgnoredUsers.Contains(user) && !Window.Giveaway_UserList.Items.Contains(name) && GetUnixTimeNow() - Chat.Users[user] <= Convert.ToInt32(Window.Giveaway_ActiveUserTime.Value) * 60 && CheckUser(user, Chat.Users.Count < 100)) Window.Giveaway_UserList.Items.Add(name);
                            if (!Chat.IgnoredUsers.Contains(user) && !Window.Giveaway_BanListListBox.Items.Contains(user) && !Window.Giveaway_UserList.Items.Contains(name) && Api.GetUnixTimeNow() - ActiveUsers[user] <= Convert.ToInt32(Window.Giveaway_ActiveUserTime.Value) * 60 && CheckUser(user, false)) Window.Giveaway_UserList.Items.Add(name);
                        }

                        List<string> delete = new List<string>();

                        //foreach (string user in Window.Giveaway_UserList.Items) if (!Chat.Users.ContainsKey(user.ToLower()) || GetUnixTimeNow() - Chat.Users[user.ToLower()] > Convert.ToInt32(Window.Giveaway_ActiveUserTime.Value) * 60 || !CheckUser(user, Chat.Users.Count < 100)) delete.Add(user);
                        foreach (string user in Window.Giveaway_UserList.Items) if (!ActiveUsers.ContainsKey(user.ToLower()) || Api.GetUnixTimeNow() - ActiveUsers[user.ToLower()] > Convert.ToInt32(Window.Giveaway_ActiveUserTime.Value) * 60 || !CheckUser(user, false)) delete.Add(user);

                        foreach (string user in delete) while (Window.Giveaway_UserList.Items.Contains(user)) Window.Giveaway_UserList.Items.Remove(user);

                        Window.Giveaway_UserCount.Text = "Count: " + Window.Giveaway_UserList.Items.Count;
                    }
                });
            }
            else if (Window.Giveaway_TypeKeyword.Checked || Window.Giveaway_TypeTickets.Checked)
            {
                Window.BeginInvoke((MethodInvoker)delegate
                {
                    lock (dUsers)
                    {
                        Dictionary<string, int> ActiveUsers = null;
                        lock (Chat.Users) ActiveUsers = Chat.Users.ToDictionary(kv => kv.Key, kv => kv.Value);

                        lock (Window.Giveaway_UserList.Items)
                        {
                            //foreach (string usr in Users.Keys) if (!Window.Giveaway_UserList.Items.Contains(usr)) Window.Giveaway_UserList.Items.Add(usr + " (" + Users[usr] + ")");

                            List<string> delete = new List<string>();

                            foreach (string user in Window.Giveaway_UserList.Items) if (!dUsers.ContainsKey(user.Split(' ')[0].ToLower()) || !ActiveUsers.ContainsKey(user.Split(' ')[0].ToLower())) delete.Add(user);

                            foreach (string user in delete)
                            {
                                while (Window.Giveaway_UserList.Items.Contains(user)) Window.Giveaway_UserList.Items.Remove(user);

                                while (dUsers.ContainsKey(user.ToLower())) dUsers.Remove(user.ToLower());
                            }

                            Window.Giveaway_UserCount.Text = "Count: " + Window.Giveaway_UserList.Items.Count;
                        }
                    }
                });
            }
        }
    }

    /*public static class Api
    {
        public static int LastRoll { get { return Giveaway.LastRoll; } }
        public static bool Started { get { return Giveaway.Started; } }
        public static bool IsOpen { get { return Giveaway.IsOpen; } }
        public static int Cost { get { return Giveaway.Cost; } }
        public static int MaxTickets { get { return Giveaway.MaxTickets; } }
        public static Dictionary<string, int> Users { get { return Giveaway.dUsers; } set { Giveaway.dUsers = value; } }
        public static float Chance { get { return Giveaway.Chance; } }
        public static GiveawaysWindow Window { get { return Giveaway.Window; } }

        public static void Start(bool announce = true, int ticketcost = 0, int maxtickets = 1) { Giveaway.Start(announce, ticketcost, maxtickets); }

        public static void Close(bool announce = true, bool open = true) { Giveaway.Close(announce, open); }

        public static void Open(bool announce = true) { Giveaway.Open(announce); }

        public static void End(bool announce = true) { Giveaway.End(announce); }

        public static void Cancel(bool announce = true) { Giveaway.Cancel(announce); }

        public static bool HasBoughtTickets(string user) { return Giveaway.HasBoughtTickets(user); }

        public static bool BuyTickets(string user, int tickets = 1) { return Giveaway.BuyTickets(user, tickets); }

        public static int GetMinCurrency() { return Giveaway.GetMinCurrency(); }

        public static bool CheckUser(string user, bool checkfollow = true, bool checksubscriber = true, bool checktime = true) { return Giveaway.CheckUser(user, checkfollow, checksubscriber, checktime); }

        public static string Roll(bool announce = true, bool checkusers = true) { return Giveaway.Roll(announce, checkusers); }

        public static class Events
        {
            public static event OnRoll Roll { add { Giveaway.OnRoll += value; } remove { Giveaway.OnRoll -= value; } }
        }
    }*/
}