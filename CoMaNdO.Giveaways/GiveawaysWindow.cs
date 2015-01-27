using ModBot;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace CoMaNdO.Giveaways
{
    public partial class GiveawaysWindow : Form
    {
        public Settings ini;
        public Dictionary<string, Dictionary<string, string>> dSettings = new Dictionary<string, Dictionary<string, string>>();
        public bool bIgnoreUpdates;
        public int iSettingsPresent = -1;

        public GiveawaysWindow(IExtension sender)
        {
            InitializeComponent();

            ini = new Settings(sender, "Settings.ini", "[Default]");

            UI.CenterSpacer(GiveawayTypeLabel, GiveawayTypeSpacer);
            UI.CenterSpacer(GiveawaySettingsLabel, GiveawaySettingsSpacer, false, true);
            UI.CenterSpacer(GiveawayBansLabel, GiveawayBansSpacer);
            UI.CenterSpacer(GiveawayUsersLabel, GiveawayUsersSpacer);

            Panel panel = new Panel();
            panel.Size = new Size(1, 1);
            panel.Location = new Point(GiveawayTypeSpacer.Location.X + GiveawayTypeSpacer.Size.Width - 1, GiveawayTypeSpacer.Location.Y + 9);
            Controls.Add(panel);
            panel.BringToFront();
            panel = new Panel();
            panel.Size = new Size(1, 1);
            panel.Location = new Point(GiveawayBansSpacer.Location.X + GiveawayBansSpacer.Size.Width - 1, GiveawayBansSpacer.Location.Y + 9);
            Controls.Add(panel);
            panel.BringToFront();
            /*panel.BackColor = Color.Black;
            panel.Size = new Size(Giveaway_AddPresent.Size.Width + Giveaway_RemovePresent.Size.Width, 1);
            panel.Location = new Point(Giveaway_AddPresent.Location.X, Giveaway_AddPresent.Location.Y + 1);
            Controls.Add(panel);
            panel.BringToFront();*/
        }

        private void Giveaway_Settings_Changed(object sender, EventArgs e)
        {
            Control ctrl = (Control)sender;
            if (ctrl == Giveaway_UnbanButton)
            {
                int iOldIndex = Giveaway_BanListListBox.SelectedIndex;
                Giveaway_BanListListBox.Items.RemoveAt(iOldIndex);
                Giveaway_UnbanButton.Enabled = false;
                if (Giveaway_BanListListBox.Items.Count > 0) Giveaway_BanListListBox.SelectedIndex = iOldIndex > Giveaway_BanListListBox.Items.Count - 1 ? Giveaway_BanListListBox.Items.Count - 1 : iOldIndex;
                Giveaway.UserListHandler(null);
            }
            else if (ctrl == Giveaway_BanButton)
            {
                Giveaway_BanListListBox.Items.Add(Giveaway_AddBanTextBox.Text.ToLower());
                Giveaway_AddBanTextBox.Text = "";
                Giveaway.UserListHandler(null);
            }
            else if (ctrl == Giveaway_MinCurrency)
            {
                Giveaway_MinCurrencyBox.Enabled = Giveaway_MinCurrency.Checked;
            }
            else if (ctrl == Giveaway_TypeActive)
            {
                Giveaway_ActiveUserTime.Enabled = Giveaway_TypeActive.Checked;
                Giveaway_AnnounceFalseEntries.Enabled = !Giveaway_TypeActive.Checked;
                Giveaway_WarnFalseEntries.Enabled = (Giveaway_AnnounceFalseEntries.Checked && !Giveaway_TypeActive.Checked && Chat.Moderators.Contains(Channel.Bot.ToLower()));
                if (Giveaway_TypeActive.Checked)
                {
                    Giveaway_AnnounceFalseEntries.Checked = false;
                    if (!Chat.Moderators.Contains(Channel.Bot.ToLower())) Giveaway_WarnFalseEntries.Checked = false;
                }
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
                Giveaway_MustWatchHours.Enabled = Giveaway_MustWatchMinutes.Enabled = Giveaway_MustWatch.Checked;
            }
            else if (ctrl == Giveaway_MustWatchMinutes)
            {
                if (Giveaway_MustWatchMinutes.Value == -1)
                {
                    if (Giveaway_MustWatchHours.Value > 0)
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
            else if (ctrl == Giveaway_AnnounceFalseEntries)
            {
                Giveaway_WarnFalseEntries.Enabled = Giveaway_AnnounceFalseEntries.Checked;
                if (!Giveaway_AnnounceFalseEntries.Checked) Giveaway_WarnFalseEntries.Checked = false;
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
                            sVariable = ini.GetValue(section, "Giveaway_AnnounceFalseEntries", "0");
                            ini.SetValue(section, "Giveaway_AnnounceFalseEntries", sVariable);
                            dSectionSettings.Add("Giveaway_AnnounceFalseEntries", sVariable);
                            sVariable = ini.GetValue(section, "Giveaway_AnnounceWarnedEntries", "0");
                            ini.SetValue(section, "Giveaway_AnnounceWarnedEntries", sVariable);
                            dSectionSettings.Add("Giveaway_AnnounceWarnedEntries", sVariable);
                            sVariable = ini.GetValue(section, "Giveaway_SubscribersWinMultiplier", "0");
                            ini.SetValue(section, "Giveaway_SubscribersWinMultiplier", sVariable);
                            dSectionSettings.Add("Giveaway_SubscribersWinMultiplier", sVariable);
                            sVariable = ini.GetValue(section, "Giveaway_SubscribersWinMultiplierAmount", "2");
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
                        if (Controls.Contains(ctrl))
                        {
                            ctrl.Enabled = dState[ctrl];
                        }
                    }

                    /*string sSelectedPresent = ini.GetValue("Settings", "SelectedPresent", "Default");
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
                    }*/

                    if (Giveaway_BanListListBox.Items.Count > 0) Giveaway_BanListListBox.Items.Clear();

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
                                        if (Convert.ToInt32(KeyValue.Value) >= Giveaway_TicketCost.Minimum && Convert.ToInt32(KeyValue.Value) <= Giveaway_TicketCost.Maximum) Giveaway_TicketCost.Value = Convert.ToInt32(KeyValue.Value);
                                    }
                                    else if (KeyValue.Key.Equals("Giveaway_MaxTickets"))
                                    {
                                        if (Convert.ToInt32(KeyValue.Value) >= Giveaway_MaxTickets.Minimum && Convert.ToInt32(KeyValue.Value) <= Giveaway_MaxTickets.Maximum) Giveaway_MaxTickets.Value = Convert.ToInt32(KeyValue.Value);
                                    }
                                    else if (KeyValue.Key.Equals("Giveaway_MinCurrencyChecked"))
                                    {
                                        Giveaway_MinCurrency.Checked = (KeyValue.Value == "1");
                                    }
                                    else if (KeyValue.Key.Equals("Giveaway_MustFollow"))
                                    {
                                        Giveaway_MustFollow.Checked = (KeyValue.Value == "1");
                                    }
                                    else if (KeyValue.Key.Equals("Giveaway_MustSubscribe"))
                                    {
                                        Giveaway_MustSubscribe.Checked = (KeyValue.Value == "1" && Channel.HasSubProgram);
                                    }
                                    else if (KeyValue.Key.Equals("Giveaway_MustWatch"))
                                    {
                                        Giveaway_MustWatch.Checked = (KeyValue.Value == "1");
                                    }
                                    else if (KeyValue.Key.Equals("Giveaway_MustWatchHours"))
                                    {
                                        if (Convert.ToInt32(KeyValue.Value) >= Giveaway_MustWatchHours.Minimum && Convert.ToInt32(KeyValue.Value) <= Giveaway_MustWatchHours.Maximum) Giveaway_MustWatchHours.Value = Convert.ToInt32(KeyValue.Value);
                                    }
                                    else if (KeyValue.Key.Equals("Giveaway_MustWatchMinutes"))
                                    {
                                        if (Convert.ToInt32(KeyValue.Value) >= Giveaway_MustWatchMinutes.Minimum && Convert.ToInt32(KeyValue.Value) <= Giveaway_MustWatchMinutes.Maximum) Giveaway_MustWatchMinutes.Value = Convert.ToInt32(KeyValue.Value);
                                    }
                                    else if (KeyValue.Key.Equals("Giveaway_MinCurrency"))
                                    {
                                        if (Convert.ToInt32(KeyValue.Value) >= Giveaway_MinCurrencyBox.Minimum && Convert.ToInt32(KeyValue.Value) <= Giveaway_MinCurrencyBox.Maximum) Giveaway_MinCurrencyBox.Value = Convert.ToInt32(KeyValue.Value);
                                    }
                                    else if (KeyValue.Key.Equals("Giveaway_ActiveUserTime"))
                                    {
                                        if (Convert.ToInt32(KeyValue.Value) >= Giveaway_ActiveUserTime.Minimum && Convert.ToInt32(KeyValue.Value) <= Giveaway_ActiveUserTime.Maximum) Giveaway_ActiveUserTime.Value = Convert.ToInt32(KeyValue.Value);
                                    }
                                    else if (KeyValue.Key.Equals("Giveaway_AutoBanWinner"))
                                    {
                                        Giveaway_AutoBanWinner.Checked = (KeyValue.Value == "1");
                                    }
                                    else if (KeyValue.Key.Equals("Giveaway_WarnFalseEntries"))
                                    {
                                        Giveaway_WarnFalseEntries.Checked = (KeyValue.Value == "1");
                                    }
                                    else if (KeyValue.Key.Equals("Giveaway_AnnounceFalseEntries"))
                                    {
                                        Giveaway_AnnounceFalseEntries.Checked = (KeyValue.Value == "1");
                                    }
                                    else if (KeyValue.Key.Equals("Giveaway_AnnounceWarnedEntries"))
                                    {
                                        Giveaway_AnnounceWarnedEntries.Checked = (KeyValue.Value == "1" && Chat.Moderators.Contains(Channel.Bot.ToLower()));
                                    }
                                    else if (KeyValue.Key.Equals("Giveaway_SubscribersWinMultiplier"))
                                    {
                                        Giveaway_SubscribersWinMultiplier.Checked = (KeyValue.Value == "1" && Channel.HasSubProgram);
                                    }
                                    else if (KeyValue.Key.Equals("Giveaway_SubscribersWinMultiplierAmount"))
                                    {
                                        if (Convert.ToInt32(KeyValue.Value) >= Giveaway_SubscribersWinMultiplierAmount.Minimum && Convert.ToInt32(KeyValue.Value) <= Giveaway_SubscribersWinMultiplierAmount.Maximum) Giveaway_SubscribersWinMultiplierAmount.Value = Convert.ToInt32(KeyValue.Value);
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
                                            if (!ban.Equals("") && !Giveaway_BanListListBox.Items.Contains(ban.ToLower()))
                                            {
                                                Giveaway_BanListListBox.Items.Add(ban.ToLower());
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

        public void SaveSettings()
        {
            new Thread(() =>
            {
                if (!bIgnoreUpdates)
                {
                    if (iSettingsPresent > -1)
                    {
                        string Present = Giveaway_SettingsPresents.TabPages[iSettingsPresent].Text;
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
                            ini.SetValue(Present, "Giveaway_MustWatchHours", Giveaway_MustWatchHours.Value.ToString());
                            ini.SetValue(Present, "Giveaway_MustWatchMinutes", Giveaway_MustWatchMinutes.Value.ToString());
                            ini.SetValue(Present, "Giveaway_MinCurrencyChecked", Giveaway_MinCurrency.Checked ? "1" : "0");
                            ini.SetValue(Present, "Giveaway_MinCurrency", Giveaway_MinCurrencyBox.Value.ToString());
                            ini.SetValue(Present, "Giveaway_ActiveUserTime", Giveaway_ActiveUserTime.Value.ToString());
                            ini.SetValue(Present, "Giveaway_AutoBanWinner", Giveaway_AutoBanWinner.Checked ? "1" : "0");
                            ini.SetValue(Present, "Giveaway_WarnFalseEntries", Giveaway_WarnFalseEntries.Checked ? "1" : "0");
                            ini.SetValue(Present, "Giveaway_AnnounceFalseEntries", Giveaway_AnnounceFalseEntries.Checked ? "1" : "0");
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
                }
            }).Start();
        }

        private void Giveaway_RerollButton_Click(object sender, EventArgs e)
        {
            Giveaway.Roll(false);
        }

        private void Giveaway_StartButton_Click(object sender, EventArgs e)
        {
            if (Giveaway_TypeTickets.Checked)
            {
                Giveaway.Start(true, int.Parse(Giveaway_TicketCost.Value.ToString()), int.Parse(Giveaway_MaxTickets.Value.ToString()));
                return;
            }

            Giveaway.Start();
        }

        private void Giveaway_OpenButton_Click(object sender, EventArgs e)
        {
            Giveaway.Open();
        }

        private void Giveaway_CloseButton_Click(object sender, EventArgs e)
        {
            Giveaway.Close();
        }

        private void Giveaway_StopButton_Click(object sender, EventArgs e)
        {
            Giveaway.End();
        }

        private void Giveaway_CancelButton_Click(object sender, EventArgs e)
        {
            Giveaway.Cancel();
        }

        private void Giveaway_AnnounceWinnerButton_Click(object sender, EventArgs e)
        {
            TimeSpan t = Users.GetTimeWatched(Giveaway_WinnerLabel.Text);
            string winner = Giveaway_WinnerLabel.Text;
            //Chat.SendMessage(winner + " has won the giveaway! (" + (Users.IsSubscriber(winner) ? "Subscribes to the channel | " : "") + (Users.IsFollower(winner) ? "Follows the channel | " : "") + "Has " + Currency.Check(winner) + " " + Currency.Name + " | Has watched the stream for " + (int)Math.Floor(t.TotalHours) + " hours and " + t.Minutes + " minutes | Chance : " + Giveaway.Chance.ToString("0.00") + "%)");
            Chat.SendMessage(winner + " has won the giveaway! (" + (Users.IsSubscriber(winner) ? "Subscribes to the channel | " : "") + (Users.IsFollower(winner) ? "Follows the channel | " : "") + "Has " + Currency.Check(winner) + " " + Currency.Name + " | Has watched the stream for " + (int)Math.Floor(t.TotalHours) + " hours and " + t.Minutes + " minutes)");
        }

        private void Giveaway_AddBanTextBox_TextChanged(object sender, EventArgs e)
        {
            Giveaway_BanButton.Enabled = (Giveaway_AddBanTextBox.Text != "" && Giveaway_AddBanTextBox.Text.Length > 2 && !Giveaway_AddBanTextBox.Text.Contains(" ") && !Giveaway_AddBanTextBox.Text.Contains(";") && !Giveaway_AddBanTextBox.Text.Contains(".") && !Giveaway_AddBanTextBox.Text.Contains(",") && !Giveaway_AddBanTextBox.Text.Contains("\"") && !Giveaway_AddBanTextBox.Text.Contains("'") && !Chat.IgnoredUsers.Contains(Giveaway_AddBanTextBox.Text.ToLower()) && !Giveaway_BanListListBox.Items.Contains(Giveaway_AddBanTextBox.Text.ToLower()));
        }

        private void Giveaway_BanListListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Giveaway_BanListListBox.SelectedIndex >= 0) Giveaway_UnbanButton.Enabled = true;
        }

        private void Giveaway_CopyWinnerButton_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Clipboard.SetText(Giveaway_WinnerLabel.Text);
        }

        private void SettingsPresents_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!bIgnoreUpdates)
            {
                ini.SetValue("Settings", "SelectedPresent", Giveaway_SettingsPresents.TabPages[Giveaway_SettingsPresents.SelectedIndex].Text);
                SaveSettings();
                iSettingsPresent = Giveaway_SettingsPresents.SelectedIndex;
                GetSettings();
            }
        }

        private void Giveaway_WinnerTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                Dictionary<string, int> ActiveUsers = null;
                lock (Chat.Users) ActiveUsers = Chat.Users.ToDictionary(kv => kv.Key, kv => kv.Value);

                if (ActiveUsers.ContainsKey(Giveaway_WinnerLabel.Text.ToLower()))
                {
                    int time = Api.GetUnixTimeNow() - ActiveUsers[Giveaway_WinnerLabel.Text.ToLower()];
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

        private void Giveaway_SettingsPresents_MouseClick(object sender, MouseEventArgs e)
        {
            /*if(e.Button == MouseButtons.Right)
            {
                Console.WriteLine(e.Location);
                
                Giveaway_SettingsPresentsContextMenu.Show(MousePosition);
            }*/
        }
    }
}