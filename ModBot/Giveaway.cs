using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.ComponentModel;
using System.Windows.Forms;

namespace ModBot
{
    public static class Giveaway
    {
        private static MainWindow MainForm = Program.MainForm;
        public static int LastRoll;
        public static bool Started { get; private set; }
        public static bool Open;
        public static int Cost { get; private set; }
        public static int MaxTickets { get; private set; }
        public static Dictionary<string, int> Users = new Dictionary<string, int>();
        public static float Chance { get; private set; }
        private static Dictionary<Control, bool> dState = new Dictionary<Control, bool>();

        public static void startGiveaway(int ticketcost = 0, int maxtickets = 1)
        {
            MainForm.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate
            {
                MainForm.Giveaway_SettingsPresents.Enabled = false;
                MainForm.Giveaway_StartButton.Enabled = false;
                MainForm.Giveaway_RerollButton.Enabled = false;
                MainForm.Giveaway_CloseButton.Enabled = true;
                MainForm.Giveaway_OpenButton.Enabled = false;
                MainForm.Giveaway_AnnounceWinnerButton.Enabled = false;
                MainForm.Giveaway_StopButton.Enabled = true;
                MainForm.Giveaway_AnnounceWinnerButton.Enabled = false;
                /*MainForm.Giveaway_MustFollowCheckBox.Enabled = false;
                MainForm.Giveaway_MinCurrencyCheckBox.Enabled = false;
                MainForm.Giveaway_MinCurrency.Enabled = false;
                MainForm.Giveaway_ActiveUserTime.Enabled = false;*/
                dState.Clear();
                foreach (Control ctrl in MainForm.GiveawayWindow.Controls)
                {
                    if (!dState.ContainsKey(ctrl) && (ctrl.GetType() == typeof(CheckBox) || ctrl.GetType() == typeof(RadioButton) || ctrl.GetType() == typeof(NumericUpDown)) && ctrl != MainForm.Giveaway_AutoBanWinnerCheckBox)
                    {
                        dState.Add(ctrl, ctrl.Enabled);
                        ctrl.Enabled = false;
                    }
                }
                MainForm.Giveaway_CopyWinnerButton.Enabled = false;
                MainForm.Giveaway_WinnerTimerLabel.Text = "0:00";
                MainForm.Giveaway_WinnerTimerLabel.ForeColor = Color.Black;
                MainForm.Giveaway_WinTimeLabel.Text = "0:00";
                MainForm.Giveaway_WinTimeLabel.ForeColor = Color.Black;
                MainForm.Giveaway_WinnerChat.Clear();
                MainForm.Giveaway_WinnerLabel.Text = "Entries open, close to roll for a winner...";
                MainForm.Giveaway_WinnerLabel.ForeColor = Color.Red;
                MainForm.Giveaway_WinnerStatusLabel.Text = "";
                LastRoll = 0;
                Cost = ticketcost;
                MaxTickets = maxtickets;
                Users.Clear();
                Started = true;
                Open = true;

                string msg = "";
                if (MainForm.Giveaway_TypeActive.Checked)
                {
                    msg = " who sent a message in the last " + MainForm.Giveaway_ActiveUserTime.Value + " minutes";
                }
                if (MainForm.Giveaway_MustSubscribe.Checked)
                {
                    if (msg != "")
                    {
                        if (!MainForm.Giveaway_MustFollow.Checked && !MainForm.Giveaway_MustWatch.Checked && !MainForm.Giveaway_MinCurrency.Checked) msg += " and"; else msg += ",";
                    }
                    else
                    {
                        msg += " who";
                    }
                    msg += " subscribes to the stream";
                }
                if (MainForm.Giveaway_MustFollow.Checked)
                {
                    if (msg != "")
                    {
                        if (!MainForm.Giveaway_MustWatch.Checked && !MainForm.Giveaway_MinCurrency.Checked) msg += " and"; else msg += ",";
                    }
                    else
                    {
                        msg += " who";
                    }
                    msg += " follows the stream";
                }
                if (MainForm.Giveaway_MustWatch.Checked)
                {
                    if (msg != "")
                    {
                        if (!MainForm.Giveaway_MinCurrency.Checked) msg += " and"; else msg += ",";
                    }
                    else
                    {
                        msg += " who";
                    }
                    msg += " watched the stream for " + MainForm.Giveaway_MustWatchDays.Value + " days, " + MainForm.Giveaway_MustWatchHours.Value + " hours and " + MainForm.Giveaway_MustWatchMinutes.Value + " minutes";
                }
                if (MainForm.Giveaway_MinCurrency.Checked)
                {
                    if (msg != "")
                    {
                        msg += " and";
                    }
                    else
                    {
                        msg += " who";
                    }
                    msg += " has " + MainForm.Giveaway_MinCurrencyBox.Value + " " + Irc.currencyName;
                }
                if (MainForm.Giveaway_TypeTickets.Checked)
                {
                    MainForm.Giveaway_CancelButton.Enabled = true;

                    Irc.sendMessage("A giveaway has started! Ticket cost: " + ticketcost + ", max. tickets: " + maxtickets + ". Anyone" + msg + " can join!");
                    Irc.sendMessage("Join by using \"!ticket AMOUNT\".");
                }
                else if (MainForm.Giveaway_TypeKeyword.Checked)
                {
                    Irc.sendMessage("A giveaway has started! Join by using \"!ticket\". Anyone" + msg + " can join!");
                }
                else
                {
                    closeGiveaway(false);

                    Irc.sendMessage("A giveaway has started! Anyone" + msg + " qualifies!");
                }
            });
        }

        public static void closeGiveaway(bool announce = true)
        {
            Open = false;
            MainForm.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate
            {
                MainForm.Giveaway_WinnerLabel.Text = "Waiting for a roll...";
                MainForm.Giveaway_RerollButton.Text = "Roll";
                MainForm.Giveaway_RerollButton.Enabled = true;
                MainForm.Giveaway_CloseButton.Enabled = false;
                MainForm.Giveaway_OpenButton.Enabled = true;
            });
            if (announce)
            {
                Irc.sendMessage("Entries to the giveaway are now closed.");
            }
        }

        public static void openGiveaway()
        {
            Open = true;
            MainForm.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate
            {
                MainForm.Giveaway_WinnerLabel.Text = "Entries open, close to roll for a winner...";
                MainForm.Giveaway_RerollButton.Text = "Roll";
                MainForm.Giveaway_RerollButton.Enabled = false;
                MainForm.Giveaway_CloseButton.Enabled = true;
                MainForm.Giveaway_OpenButton.Enabled = false;
            });
            Irc.sendMessage("Entries to the giveaway are now open.");
        }

        public static void endGiveaway(bool announce = true)
        {
            MainForm.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate
            {
                MainForm.Giveaway_SettingsPresents.Enabled = true;
                MainForm.Giveaway_StartButton.Enabled = true;
                MainForm.Giveaway_RerollButton.Enabled = false;
                MainForm.Giveaway_CloseButton.Enabled = false;
                MainForm.Giveaway_OpenButton.Enabled = false;
                MainForm.Giveaway_CancelButton.Enabled = false;
                MainForm.Giveaway_AnnounceWinnerButton.Enabled = false;
                MainForm.Giveaway_StopButton.Enabled = false;
                /*MainForm.Giveaway_MustFollowCheckBox.Enabled = true;
                MainForm.Giveaway_MinCurrencyCheckBox.Enabled = true;
                MainForm.Giveaway_MinCurrency.Enabled = MainForm.Giveaway_MinCurrencyCheckBox.Checked;
                MainForm.Giveaway_ActiveUserTime.Enabled = true;*/
                foreach (Control ctrl in dState.Keys)
                {
                    ctrl.Enabled = dState[ctrl];
                }
                dState.Clear();
                MainForm.Giveaway_CopyWinnerButton.Enabled = false;
                MainForm.Giveaway_WinnerTimerLabel.Text = "0:00";
                MainForm.Giveaway_WinnerTimerLabel.ForeColor = Color.Black;
                MainForm.Giveaway_WinTimeLabel.Text = "0:00";
                MainForm.Giveaway_WinTimeLabel.ForeColor = Color.Black;
                MainForm.Giveaway_WinnerChat.Clear();
                MainForm.Giveaway_WinnerLabel.Text = "Giveaway isn't active";
                MainForm.Giveaway_WinnerLabel.ForeColor = Color.Blue;
                MainForm.Giveaway_RerollButton.Text = "Roll";
                MainForm.Giveaway_WinnerStatusLabel.Text = "";
                LastRoll = 0;
                Cost = 0;
                MaxTickets = 0;
                Users.Clear();
                Started = false;
                Open = false;
            });
            if (announce)
            {
                Irc.sendMessage("The giveaway has ended!");
            }
        }

        public static void cancelGiveaway()
        {
            foreach (string user in Users.Keys)
            {
                Database.addCurrency(user, Users[user] * Cost);
            }
            endGiveaway(false);
            Irc.sendMessage("The giveaway has been cancelled" + (MainForm.Giveaway_TypeTickets.Checked ? ", entries has been refunded." : "."));
        }

        public static bool HasBoughtTickets(string user)
        {
            return Users.ContainsKey(Api.capName(user));
        }

        public static bool BuyTickets(string user, int tickets=1)
        {
            user = Api.capName(user);
            if (Started && (MainForm.Giveaway_TypeKeyword.Checked || MainForm.Giveaway_TypeTickets.Checked) && Open && tickets <= MaxTickets && CheckUser(user))
            {
                int paid = 0;
                if (Users.ContainsKey(user))
                {
                    paid = Users[user] * Cost;
                }
                if (Database.checkCurrency(user) + paid >= tickets * Cost)
                {
                    Database.addCurrency(user, paid);
                    Database.removeCurrency(user, tickets * Cost);
                    if (Users.ContainsKey(user))
                    {
                        Users[user] = tickets;
                    }
                    else
                    {
                        Users.Add(user, tickets);
                    }
                    return true;
                }
            }
            return false;
        }

        private static int GetMinCurrency()
        {
            if (MainForm.Giveaway_MinCurrency.Checked)
            {
                return Convert.ToInt32(MainForm.Giveaway_MinCurrencyBox.Value);
            }
            return 0;
        }

        private static bool CheckUser(string user)
        {
            return (!Irc.IgnoredUsers.Any(c => c.Equals(user.ToLower())) && !MainForm.Giveaway_BanListListBox.Items.Contains(user) && Database.checkCurrency(user) >= GetMinCurrency() && (MainForm.Giveaway_MustFollow.Checked && Api.IsFollower(user) || !MainForm.Giveaway_MustFollow.Checked) && (MainForm.Giveaway_MustSubscribe.Checked && Api.IsSubscriber(user) || !MainForm.Giveaway_MustSubscribe.Checked) && (MainForm.Giveaway_MustWatch.Checked && Api.CompareTimeWatched(user) >= 0 || !MainForm.Giveaway_MustWatch.Checked));
        }

        public static string getWinner()
        {
            string sWinner = "";
            MainForm.BeginInvoke((MethodInvoker)delegate
            {
                MainForm.Giveaway_RerollButton.Enabled = false;
                MainForm.Giveaway_AnnounceWinnerButton.Enabled = false;
                MainForm.Giveaway_CopyWinnerButton.Enabled = false;
                MainForm.Giveaway_WinnerStatusLabel.Text = "";
                MainForm.Giveaway_WinnerLabel.Text = "Rolling...";
                MainForm.Giveaway_WinnerLabel.ForeColor = Color.Red;
                MainForm.Giveaway_RerollButton.Text = "Reroll";
                MainForm.Giveaway_WinnerTimerLabel.Text = "0:00";
                MainForm.Giveaway_WinnerTimerLabel.ForeColor = Color.Black;
                MainForm.Giveaway_WinTimeLabel.Text = "0:00";
                MainForm.Giveaway_WinTimeLabel.ForeColor = Color.Black;
                MainForm.Giveaway_WinnerChat.Clear();
            });
            LastRoll = 0;

            Thread thread = new Thread(() =>
            {
                //Irc.buildUserList();

                while (true)
                {
                    try
                    {
                        List<string> ValidUsers = new List<string>();
                        if (MainForm.Giveaway_TypeActive.Checked)
                        {
                            int ActiveTime = Convert.ToInt32(MainForm.Giveaway_ActiveUserTime.Value) * 60, CurrentTime = Api.GetUnixTimeNow();
                            lock (Irc.ActiveUsers)
                            {
                                foreach (string user in Irc.ActiveUsers.Keys)
                                {
                                    if (!ValidUsers.Contains(user) && CurrentTime - Irc.ActiveUsers[user] <= ActiveTime && CheckUser(user))
                                    {
                                        ValidUsers.Add(user);
                                    }
                                }
                            }
                        }
                        else if (MainForm.Giveaway_TypeKeyword.Checked || MainForm.Giveaway_TypeTickets.Checked)
                        {
                            lock (Users)
                            {
                                lock (Irc.ActiveUsers)
                                {
                                    List<string> Delete = new List<string>();
                                    foreach (string user in Users.Keys)
                                    {
                                        if (Irc.ActiveUsers.ContainsKey(user) && CheckUser(user))
                                        {
                                            for (int i = 0; i < Users[user]; i++)
                                            {
                                                ValidUsers.Add(user);
                                            }
                                        }
                                        else
                                        {
                                            Database.addCurrency(user, Users[user] * Cost);
                                            Delete.Add(user);
                                        }
                                    }
                                    foreach (string user in Delete)
                                    {
                                        if (Users.ContainsKey(user))
                                        {
                                            Users.Remove(user);
                                        }
                                    }
                                }
                            }
                        }

                        if (ValidUsers.Count > 0)
                        {
                            sWinner = Api.GetDisplayName(ValidUsers[new Random().Next(0, ValidUsers.Count)]);
                            //Chance = 100F / ValidUsers.Count;
                            int tickets = ValidUsers.Count;
                            int winnertickets = 1;
                            if (MainForm.Giveaway_TypeTickets.Checked)
                            {
                                tickets = 0;
                                foreach(string user in Users.Keys)
                                {
                                    tickets += Users[user];
                                    if(user.ToLower() == sWinner.ToLower())
                                    {
                                        winnertickets = Users[user];
                                    }
                                }
                            }
                            Chance = (float)winnertickets / tickets * 100;
                            MainForm.BeginInvoke((MethodInvoker)delegate
                            {
                                //string WinnerLabel = "Winner : ";
                                string WinnerLabel = "";
                                if (Api.IsSubscriber(sWinner)) WinnerLabel += "Subscribing | ";
                                if (Api.IsFollower(sWinner)) WinnerLabel += "Following | ";
                                WinnerLabel += Database.checkCurrency(sWinner) + " " + Irc.currencyName + " | Watched : " + Database.getTimeWatched(sWinner).ToString(@"d\d\ hh\h\ mm\m") + " | Chance : " + Chance.ToString("0.00") + "%";
                                MainForm.Giveaway_WinnerStatusLabel.Text = WinnerLabel;
                                MainForm.Giveaway_WinnerLabel.Text = sWinner;
                                MainForm.Giveaway_WinnerTimerLabel.ForeColor = Color.FromArgb(0, 200, 0);
                                MainForm.Giveaway_WinTimeLabel.ForeColor = Color.FromArgb(0, 200, 0);
                                MainForm.Giveaway_WinnerLabel.ForeColor = Color.Green;
                                MainForm.Giveaway_CopyWinnerButton.Enabled = true;
                                MainForm.Giveaway_AnnounceWinnerButton.Enabled = true;
                                MainForm.Giveaway_RerollButton.Enabled = true;
                                LastRoll = Api.GetUnixTimeNow();
                                if (MainForm.Giveaway_AutoBanWinnerCheckBox.Checked && !MainForm.Giveaway_BanListListBox.Items.Contains(sWinner)) MainForm.Giveaway_BanListListBox.Items.Add(Api.capName(sWinner));
                            });
                            thread = new Thread(() =>
                            {
                                sWinner = Api.GetDisplayName(sWinner, true);
                                MainForm.BeginInvoke((MethodInvoker)delegate
                                {
                                    MainForm.Giveaway_WinnerLabel.Text = sWinner;
                                });
                            });
                            thread.Name = "Use winner's (" + sWinner + ") display name";
                            thread.Start();
                            return;
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Error while rolling, retrying");
                        continue;
                    }

                    MainForm.BeginInvoke((MethodInvoker)delegate
                    {
                        MainForm.Giveaway_WinnerLabel.Text = "No valid winner found";
                        MainForm.Giveaway_RerollButton.Enabled = true;
                    });
                    return;
                }
            });
            thread.Name = "Roll for winner";
            thread.Start();
            thread.Join();
            return sWinner;
        }
    }
}