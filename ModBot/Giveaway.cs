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
        public static bool Open { get; private set; }
        public static int Cost { get; private set; }
        public static int MaxTickets { get; private set; }
        public static Dictionary<string, int> Users = new Dictionary<string, int>();
        public static float Chance { get; private set; }
        private static Dictionary<Control, bool> dState = new Dictionary<Control, bool>();

        public static void startGiveaway(int ticketcost = 0, int maxtickets = 1)
        {
            MainForm.Giveaway_StartButton.Enabled = false;
            MainForm.Giveaway_RerollButton.Enabled = true;
            MainForm.Giveaway_AnnounceWinnerButton.Enabled = false;
            MainForm.Giveaway_StopButton.Enabled = true;
            MainForm.Giveaway_AnnounceWinnerButton.Enabled = false;
            /*MainForm.Giveaway_MustFollowCheckBox.Enabled = false;
            MainForm.Giveaway_MinCurrencyCheckBox.Enabled = false;
            MainForm.Giveaway_MinCurrency.Enabled = false;
            MainForm.Giveaway_ActiveUserTime.Enabled = false;*/
            dState.Clear();
            foreach(Control ctrl in MainForm.GiveawayWindow.Controls)
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
            MainForm.Giveaway_WinnerLabel.Text = "Waiting for a roll...";
            MainForm.Giveaway_WinnerLabel.ForeColor = Color.Red;
            MainForm.Giveaway_WinnerStatusLabel.Text = "";
            LastRoll = 0;
            Cost = ticketcost;
            MaxTickets = maxtickets;
            Users.Clear();
            Started = true;
            Open = true;
        }

        public static void endGiveaway()
        {
            MainForm.Giveaway_StartButton.Enabled = true;
            MainForm.Giveaway_RerollButton.Enabled = false;
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
        }

        public static bool HasBoughtTickets(string user)
        {
            return Users.ContainsKey(Api.capName(user));
        }

        public static bool BuyTickets(string user, int tickets=1)
        {
            user = Api.capName(user);
            if (Started && (MainForm.Giveaway_TypeKeyword.Checked || MainForm.Giveaway_TypeTickets.Checked) && Open && tickets <= MaxTickets && !Irc.IgnoredUsers.Any(c => c.Equals(user.ToLower())) && !MainForm.Giveaway_BanListListBox.Items.Contains(user) && (MainForm.Giveaway_MustFollowCheckBox.Checked && Api.IsFollowingChannel(user) || !MainForm.Giveaway_MustFollowCheckBox.Checked))
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
            if (MainForm.Giveaway_MinCurrencyCheckBox.Checked)
            {
                return Convert.ToInt32(MainForm.Giveaway_MinCurrency.Value);
            }
            return 0;
        }

        public static string getWinner()
        {
            string sWinner = "";
            if (!Started) startGiveaway();
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
                                foreach (KeyValuePair<string, int> user in Irc.ActiveUsers)
                                {
                                    if (!ValidUsers.Contains(user.Key) && !Irc.IgnoredUsers.Any(c => c.Equals(user.Key.ToLower())) && !MainForm.Giveaway_BanListListBox.Items.Contains(user.Key))
                                    {
                                        if (CurrentTime - user.Value <= ActiveTime && Database.checkCurrency(user.Key) >= GetMinCurrency())
                                        {
                                            if (MainForm.Giveaway_MustFollowCheckBox.Checked && Api.IsFollowingChannel(user.Key) || !MainForm.Giveaway_MustFollowCheckBox.Checked)
                                            {
                                                ValidUsers.Add(user.Key);
                                            }
                                        }
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
                                        if (Irc.ActiveUsers.ContainsKey(user) && !Irc.IgnoredUsers.Any(c => c.Equals(user.ToLower())) && !MainForm.Giveaway_BanListListBox.Items.Contains(user) && (MainForm.Giveaway_MustFollowCheckBox.Checked && Api.IsFollowingChannel(user) || !MainForm.Giveaway_MustFollowCheckBox.Checked))
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
                                if (Api.IsFollowingChannel(sWinner)) WinnerLabel += "Following | ";
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
                    catch (InvalidOperationException)
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