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
    public class Giveaway
    {
        private MainWindow MainForm;
        public string winner;
        public int iLastWin;

        public Giveaway(MainWindow MainForm)
        {
            this.MainForm = MainForm;
        }

        public void startGiveaway()
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
            winner = "";
            iLastWin = 0;
        }

        public void endGiveaway()
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
            winner = "";
            iLastWin = 0;
        }

        private int GetMinCurrency()
        {
            if (MainForm.Giveaway_MinCurrencyCheckBox.Checked)
            {
                return Convert.ToInt32(MainForm.Giveaway_MinCurrency.Value);
            }
            return 0;
        }

        private void GetWinnerThread()
        {
            if (MainForm.Giveaway_StartButton.Visible) startGiveaway();
            winner = "";
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
                MainForm.IRC.buildUserList();
            });

            try
            {
                List<string> ValidUsers = new List<string>();
                int ActiveTime = Convert.ToInt32(MainForm.Giveaway_ActiveUserTime.Value) * 60, CurrentTime = MainForm.IRC.api.GetUnixTimeNow();
                lock (MainForm.IRC.ActiveUsers)
                {
                    foreach (KeyValuePair<string, int> user in MainForm.IRC.ActiveUsers)
                    {
                        if (!MainForm.IRC.IgnoredUsers.Any(c => c.Equals(user.Key.ToLower())) && MainForm.IRC.IsUserInList(MainForm.IRC.capName(user.Key)))
                        {
                            if (!MainForm.Giveaway_BanListListBox.Items.Contains(MainForm.IRC.capName(user.Key)))
                            {
                                if (CurrentTime - MainForm.IRC.ActiveUsers[MainForm.IRC.capName(user.Key)] <= ActiveTime)
                                {
                                    if ((MainForm.IRC.db.checkCurrency(MainForm.IRC.capName(user.Key)) >= GetMinCurrency()))
                                    {
                                        if (MainForm.Giveaway_MustFollowCheckBox.Checked && MainForm.IRC.api.IsFollowingChannel(MainForm.IRC.capName(user.Key)) || !MainForm.Giveaway_MustFollowCheckBox.Checked)
                                        {
                                            ValidUsers.Add(MainForm.IRC.capName(user.Key));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (ValidUsers.Count > 0)
                {
                    winner = ValidUsers[new Random().Next(0, ValidUsers.Count - 1)];
                    MainForm.BeginInvoke((MethodInvoker)delegate
                    {
                        string WinnerLabel = "Winner : ";
                        if (MainForm.IRC.api.IsFollowingChannel(winner)) WinnerLabel = WinnerLabel + "Following | ";
                        MainForm.Giveaway_WinnerStatusLabel.Text = WinnerLabel + MainForm.IRC.db.checkCurrency(winner) + " " + MainForm.IRC.currency;
                        MainForm.Giveaway_WinnerLabel.Text = winner;
                        MainForm.Giveaway_WinnerTimerLabel.ForeColor = Color.FromArgb(0, 200, 0);
                        MainForm.Giveaway_WinTimeLabel.ForeColor = Color.FromArgb(0, 200, 0);
                        MainForm.Giveaway_WinnerLabel.ForeColor = Color.Green;
                        MainForm.Giveaway_CopyWinnerButton.Enabled = true;
                        MainForm.Giveaway_AnnounceWinnerButton.Enabled = true;
                        MainForm.Giveaway_RerollButton.Enabled = true;
                        iLastWin = MainForm.IRC.api.GetUnixTimeNow();
                        if (MainForm.Giveaway_AutoBanWinnerCheckBox.Checked && !MainForm.Giveaway_BanListListBox.Items.Contains(winner)) MainForm.Giveaway_BanListListBox.Items.Add(winner);
                    });
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

        public String getWinner()
        {
            Thread thread = new Thread(new ThreadStart(GetWinnerThread));
            thread.Start();
            thread.Join();
            return winner;
        }
    }
}