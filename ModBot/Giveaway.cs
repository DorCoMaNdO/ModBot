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
        private static MainWindow MainForm;
        private static string sWinner;
        public static int iLastWin;
        private static float fChance;

        public static void SetMainForm(MainWindow Form)
        {
            MainForm = Form;
        }


        public static void startGiveaway()
        {
            MainForm.BeginInvoke((MethodInvoker)delegate
            {
                MainForm.Giveaway_StartButton.Visible = false;
                MainForm.Giveaway_RerollButton.Visible = true;
                MainForm.Giveaway_AnnounceWinnerButton.Visible = true;
                MainForm.Giveaway_StopButton.Enabled = true;
                MainForm.Giveaway_AnnounceWinnerButton.Enabled = false;
                MainForm.Giveaway_MustFollowCheckBox.Enabled = false;
                MainForm.Giveaway_MinCurrencyCheckBox.Enabled = false;
                MainForm.Giveaway_MinCurrency.Enabled = false;
                MainForm.Giveaway_ActiveUserTime.Enabled = false;
                MainForm.Giveaway_CopyWinnerButton.Enabled = false;
                MainForm.Giveaway_WinnerTimerLabel.Text = "0:00";
                MainForm.Giveaway_WinnerTimerLabel.ForeColor = Color.Black;
                MainForm.Giveaway_WinTimeLabel.Text = "0:00";
                MainForm.Giveaway_WinTimeLabel.ForeColor = Color.Black;
                MainForm.Giveaway_WinnerChat.Clear();
                MainForm.Giveaway_WinnerLabel.Text = "Waiting for a roll...";
                MainForm.Giveaway_WinnerLabel.ForeColor = Color.Red;
                MainForm.Giveaway_WinnerStatusLabel.Text = "";
            });
            sWinner = "";
            iLastWin = 0;
        }

        public static void endGiveaway()
        {
            MainForm.BeginInvoke((MethodInvoker)delegate
            {
                MainForm.Giveaway_StartButton.Visible = true;
                MainForm.Giveaway_RerollButton.Visible = false;
                MainForm.Giveaway_AnnounceWinnerButton.Visible = false;
                MainForm.Giveaway_StopButton.Enabled = false;
                MainForm.Giveaway_MustFollowCheckBox.Enabled = true;
                MainForm.Giveaway_MinCurrencyCheckBox.Enabled = true;
                MainForm.Giveaway_MinCurrency.Enabled = MainForm.Giveaway_MinCurrencyCheckBox.Checked;
                MainForm.Giveaway_ActiveUserTime.Enabled = true;
                MainForm.Giveaway_CopyWinnerButton.Enabled = false;
                MainForm.Giveaway_WinnerTimerLabel.Text = "0:00";
                MainForm.Giveaway_WinnerTimerLabel.ForeColor = Color.Black;
                MainForm.Giveaway_WinTimeLabel.Text = "0:00";
                MainForm.Giveaway_WinTimeLabel.ForeColor = Color.Black;
                MainForm.Giveaway_WinnerChat.Clear();
                MainForm.Giveaway_WinnerLabel.Text = "Giveaway isn't active";
                MainForm.Giveaway_RerollButton.Text = "Roll";
                MainForm.Giveaway_WinnerLabel.ForeColor = Color.Blue;
                MainForm.Giveaway_WinnerStatusLabel.Text = "";
            });
            sWinner = "";
            iLastWin = 0;
        }

        private static int GetMinCurrency()
        {
            if (MainForm.Giveaway_MinCurrencyCheckBox.Checked)
            {
                return Convert.ToInt32(MainForm.Giveaway_MinCurrency.Value);
            }
            return 0;
        }

        private static void GetWinnerThread()
        {
            if (MainForm.Giveaway_StartButton.Visible) startGiveaway();
            sWinner = "";
            iLastWin = 0;
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
            Irc.buildUserList();

            try
            {
                List<string> ValidUsers = new List<string>();
                int ActiveTime = Convert.ToInt32(MainForm.Giveaway_ActiveUserTime.Value) * 60, CurrentTime = Api.GetUnixTimeNow();
                lock (Irc.ActiveUsers)
                {
                    foreach (KeyValuePair<string, int> user in Irc.ActiveUsers)
                    {
                        if (!Irc.IgnoredUsers.Any(c => c.Equals(user.Key.ToLower())) && Irc.IsUserInList(user.Key))
                        {
                            if (!MainForm.Giveaway_BanListListBox.Items.Contains(user.Key))
                            {
                                if (CurrentTime - user.Value <= ActiveTime)
                                {
                                    if ((Database.checkCurrency(user.Key) >= GetMinCurrency()))
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
                }

                if (ValidUsers.Count > 0)
                {
                    sWinner = Api.GetDisplayName(ValidUsers[new Random().Next(0, ValidUsers.Count - 1)]);
                    fChance = 100F / ValidUsers.Count;
                    MainForm.BeginInvoke((MethodInvoker)delegate
                    {
                        //string WinnerLabel = "Winner : ";
                        string WinnerLabel = "";
                        if (Api.IsFollowingChannel(sWinner)) WinnerLabel += "Following | ";
                        WinnerLabel += Database.checkCurrency(sWinner) + " " + Irc.currency + " | Chance : " + fChance.ToString("0.00") + "%";
                        MainForm.Giveaway_WinnerStatusLabel.Text = WinnerLabel;
                        MainForm.Giveaway_WinnerLabel.Text = sWinner;
                        MainForm.Giveaway_WinnerTimerLabel.ForeColor = Color.FromArgb(0, 200, 0);
                        MainForm.Giveaway_WinTimeLabel.ForeColor = Color.FromArgb(0, 200, 0);
                        MainForm.Giveaway_WinnerLabel.ForeColor = Color.Green;
                        MainForm.Giveaway_CopyWinnerButton.Enabled = true;
                        MainForm.Giveaway_AnnounceWinnerButton.Enabled = true;
                        MainForm.Giveaway_RerollButton.Enabled = true;
                        iLastWin = Api.GetUnixTimeNow();
                        if (MainForm.Giveaway_AutoBanWinnerCheckBox.Checked && !MainForm.Giveaway_BanListListBox.Items.Contains(sWinner)) MainForm.Giveaway_BanListListBox.Items.Add(Api.capName(sWinner));
                    });
                    new Thread(() =>
                    {
                        sWinner = Api.GetDisplayName(sWinner, true);
                        MainForm.BeginInvoke((MethodInvoker)delegate
                        {
                            MainForm.Giveaway_WinnerLabel.Text = sWinner;
                        });
                    }).Start();
                    return;
                }
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Error while rolling, retrying");
                Thread thread = new Thread(new ThreadStart(GetWinnerThread));
                thread.Start();
                thread.Join();
                return;
            }

            MainForm.BeginInvoke((MethodInvoker)delegate
            {
                MainForm.Giveaway_WinnerLabel.Text = "No valid winner found";
                MainForm.Giveaway_RerollButton.Enabled = true;
            });
        }

        public static String getWinner()
        {
            Thread thread = new Thread(new ThreadStart(GetWinnerThread));
            thread.Start();
            thread.Join();
            return sWinner;
        }

        public static float getLastRollWinChance()
        {
            return fChance;
        }
    }
}