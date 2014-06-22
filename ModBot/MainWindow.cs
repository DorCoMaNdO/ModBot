using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Windows.Forms;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Diagnostics;

namespace ModBot
{
    public partial class MainWindow : CustomForm
    {
        public Irc IRC;
        public Dictionary<string, Dictionary<string, string>> dSettings = new Dictionary<string, Dictionary<string, string>>();
        public iniUtil ini;
        private bool bIgnoreUpdates = false;
        public int iSettingsPresent = -2;
        private Donations donations;
        private bool bUpdateNote = false;
        private string sCurrentVersion = "";
        private bool g_bLoaded = false;

        public MainWindow(Irc IRC)
        {
            InitializeComponent();
            sCurrentVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            Text = "ModBot v" + sCurrentVersion.Replace("." + Assembly.GetExecutingAssembly().GetName().Version.Revision.ToString(), "");
            this.IRC = IRC;
            ini = IRC.ini;

            Giveaway_WinnerChat.Select(0, 7);
            Giveaway_WinnerChat.SelectionColor = Color.Blue;
            Giveaway_WinnerChat.SelectionFont = new Font("Segoe Print", 8, FontStyle.Bold);
            Giveaway_WinnerChat.Select(Giveaway_WinnerChat.Text.Length, Giveaway_WinnerChat.Text.Length);

            //string[] lines = File.ReadAllLines("modbot.txt");
            //Dictionary<string, string> dict = lines.Select(l => l.Split('=')).ToDictionary(a => a[0], a => a[1]);
            //iniUtil ini = new iniUtil(@"C:\program files (x86)\myapp\myapp.ini");

            /*Type colorType = typeof(System.Drawing.Color);
            // We take only static property to avoid properties like Name, IsSystemColor ...
            PropertyInfo[] propInfos = colorType.GetProperties(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public);
            foreach (PropertyInfo propInfo in propInfos)
            {
                Console.WriteLine(propInfo.Name);
            }*/

            //GetSettings();

            donations = new Donations(this);
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            //Settings loading
            ChannelLabel.Text = "Loading...";
            Dictionary<Control, bool> dState = new Dictionary<Control, bool>();
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
            tLoad.Start();

            //Update checking
            new Thread(() =>
            {
                while (true)
                {
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "ModBotUpdater.exe"))
                    {
                        using (WebClient w = new WebClient())
                        {
                            try
                            {
                                w.Proxy = null;
                                string sLatestVersion = w.DownloadString("https://dl.dropboxusercontent.com/u/60356733/ModBot.txt");
                                if (sLatestVersion != "")
                                {
                                    string[] sCurrent = sCurrentVersion.Split('.');
                                    string[] sLatest = sLatestVersion.Split('.');
                                    int iCurrentMajor = Convert.ToInt32(sCurrent[0]), iCurrentMinor = Convert.ToInt32(sCurrent[1]), iCurrentBuild = Convert.ToInt32(sCurrent[2]), iCurrentRev = Convert.ToInt32(sCurrent[3]);
                                    int iLatestMajor = Convert.ToInt32(sLatest[0]), iLatestMinor = Convert.ToInt32(sLatest[1]), iLatestBuild = Convert.ToInt32(sLatest[2]), iLatestRev = Convert.ToInt32(sLatest[3]);
                                    if (iLatestMajor > iCurrentMajor || iLatestMajor == iCurrentMajor && iLatestMinor > iCurrentMinor || iLatestMajor == iCurrentMajor && iLatestMinor == iCurrentMinor && iLatestBuild > iCurrentBuild || iLatestMajor == iCurrentMajor && iLatestMinor == iCurrentMinor && iLatestBuild == iCurrentBuild && iLatestRev > iCurrentRev)
                                    {
                                        Console.WriteLine("\r\n********************************************************************************\r\nAn update to ModBot is available, please use the updater to update! (Current version: " + sCurrentVersion + ", Latest version: " + sLatestVersion + ")\r\n\r\n********************************************************************************\r\n");
                                        if (IsHandleCreated && IsActivated && !bUpdateNote)
                                        {
                                            BeginInvoke((MethodInvoker)delegate
                                            {
                                                bUpdateNote = true;
                                                if (MessageBox.Show("An update to ModBot is available! (Current version: " + sCurrentVersion + ", Latest version: " + sLatestVersion + ")\r\nDo you want to update now?", "ModBot", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                                                {
                                                    Process.Start(AppDomain.CurrentDomain.BaseDirectory + "ModBotUpdater.exe");
                                                    Environment.Exit(0);
                                                }
                                            });
                                        }
                                    }
                                }
                            }
                            catch (SocketException)
                            {
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                    Thread.Sleep(60000);
                }
            }).Start();
        }

        public void GetSettings()
        {
            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "modbot.ini"))
            {
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "modbot.ini", "\r\n[Default]");
            }
            if (!bIgnoreUpdates)
            {
                ////SettingsPresents.TabPages.Clear();
                //Console.WriteLine("Getting Settings");
                bIgnoreUpdates = true;
                Dictionary<Control, bool> dState = new Dictionary<Control, bool>();
                foreach (Control ctrl in Controls)
                {
                    if (!BaseControls.Contains(ctrl) && !dState.ContainsKey(ctrl))
                    {
                        dState.Add(ctrl, ctrl.Enabled);
                        ctrl.Enabled = false;
                    }
                }
                bool bRecreateSections = false;
                foreach (string section in ini.GetSectionNames())
                {
                    if(section != "Settings" && !dSettings.Keys.Contains(section))
                    {
                        bRecreateSections = true;
                        SettingsPresents.TabPages.Clear();
                        dSettings.Clear();
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
                            string sLockCurrencyCmd = ini.GetValue(section, "Misc_LockCurrencyCmd", "0");
                            ini.SetValue(section, "Misc_LockCurrencyCmd", sLockCurrencyCmd);
                            dSectionSettings.Add("Misc_LockCurrencyCmd", sLockCurrencyCmd);
                            string sMinCurrencyChecked = ini.GetValue(section, "Giveaway_MinCurrencyChecked", "0");
                            ini.SetValue(section, "Giveaway_MinCurrencyChecked", sMinCurrencyChecked);
                            dSectionSettings.Add("Giveaway_MinCurrencyChecked", sMinCurrencyChecked);
                            string sMustFollow = ini.GetValue(section, "Giveaway_MustFollow", "0");
                            ini.SetValue(section, "Giveaway_MustFollow", sMustFollow);
                            dSectionSettings.Add("Giveaway_MustFollow", sMustFollow);
                            string sMinCurrency = ini.GetValue(section, "Giveaway_MinCurrency", "1");
                            ini.SetValue(section, "Giveaway_MinCurrency", sMinCurrency);
                            dSectionSettings.Add("Giveaway_MinCurrency", sMinCurrency);
                            string sActiveUserTime = ini.GetValue(section, "Giveaway_ActiveUserTime", "5");
                            ini.SetValue(section, "Giveaway_ActiveUserTime", sActiveUserTime);
                            dSectionSettings.Add("Giveaway_ActiveUserTime", sActiveUserTime);
                            string sAutoBanWinnerChecked = ini.GetValue(section, "Giveaway_AutoBanWinner", "0");
                            ini.SetValue(section, "Giveaway_AutoBanWinner", sAutoBanWinnerChecked);
                            dSectionSettings.Add("Giveaway_AutoBanWinner", sAutoBanWinnerChecked);
                            string sBanList = ini.GetValue(section, "Giveaway_BanList", "");
                            ini.SetValue(section, "Giveaway_BanList", sBanList);
                            dSectionSettings.Add("Giveaway_BanList", sBanList);

                            /*foreach (KeyValuePair<string, string> kv in dSectionSettings)
                            {
                                Console.WriteLine(kv.Key + " = " + kv.Value);
                            }*/

                            if (bRecreateSections)
                            {
                                dSettings.Add(section, dSectionSettings);

                                SettingsPresents.TabPages.Add(section);
                            }
                            else
                            {
                                if (dSettings.Keys.Contains(section))
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

                foreach (Control ctrl in dState.Keys)
                {
                    if (Controls.Contains(ctrl))
                    {
                        ctrl.Enabled = dState[ctrl];
                    }
                }

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
                string sCurrencyHandoutTime = ini.GetValue("Settings", "Currency_HandoutTime", "5");
                ini.SetValue("Settings", "Currency_HandoutTime", sCurrencyHandoutTime);
                Currency_HandoutLastActive.Value = Convert.ToInt32(sCurrencyHandoutTime);

                string sSelectedPresent = ini.GetValue("Settings", "SelectedPresent", "Default");
                if (sSelectedPresent != "")
                {
                    for (int i = 0; i < SettingsPresents.TabPages.Count; i++)
                    {
                        if (SettingsPresents.TabPages[i].Text.Equals(sSelectedPresent))
                        {
                            iSettingsPresent = SettingsPresents.SelectedIndex = i;
                            break;
                        }
                    }
                }

                if (Giveaway_BanListListBox.Items.Count > 0)
                {
                    Giveaway_BanListListBox.Items.Clear();
                }

                if (SettingsPresents.SelectedIndex > -1)
                {
                    if (dSettings.ContainsKey(SettingsPresents.TabPages[SettingsPresents.SelectedIndex].Text))
                    {
                        foreach (KeyValuePair<string, string> KeyValue in dSettings[SettingsPresents.TabPages[SettingsPresents.SelectedIndex].Text])
                        {
                            if (KeyValue.Key != "")
                            {
                                if (KeyValue.Key.Equals("Misc_LockCurrencyCmd"))
                                {
                                    Misc_LockCurrencyCmdCheckBox.Checked = KeyValue.Value.Equals("1");
                                }
                                else if (KeyValue.Key.Equals("Giveaway_MinCurrencyChecked"))
                                {
                                    Giveaway_MinCurrencyCheckBox.Checked = KeyValue.Value.Equals("1");
                                }
                                else if (KeyValue.Key.Equals("Giveaway_MustFollow"))
                                {
                                    Giveaway_MustFollowCheckBox.Checked = KeyValue.Value.Equals("1");
                                }
                                else if (KeyValue.Key.Equals("Giveaway_MinCurrency"))
                                {
                                    Giveaway_MinCurrency.Value = Convert.ToInt32(KeyValue.Value);
                                }
                                else if (KeyValue.Key.Equals("Giveaway_ActiveUserTime"))
                                {
                                    Giveaway_ActiveUserTime.Value = Convert.ToInt32(KeyValue.Value);
                                }
                                else if (KeyValue.Key.Equals("Giveaway_AutoBanWinner"))
                                {
                                    Giveaway_AutoBanWinnerCheckBox.Checked = KeyValue.Value.Equals("1");
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

                if (SettingsPresents.TabPages.Count == 0)
                {
                    SettingsPresents.TabPages.Add("Default");
                    iSettingsPresent = 0;
                    Dictionary<string, string> dSectionSettings = new Dictionary<string, string>();
                    string sLockCurrencyCmd = ini.GetValue("Default", "Misc_LockCurrencyCmd", "0");
                    ini.SetValue("Default", "Misc_LockCurrencyCmd", sLockCurrencyCmd);
                    dSectionSettings.Add("Misc_LockCurrencyCmd", sLockCurrencyCmd);
                    string sMinCurrencyChecked = ini.GetValue("Default", "Giveaway_MinCurrencyChecked", "0");
                    ini.SetValue("Default", "Giveaway_MinCurrencyChecked", sMinCurrencyChecked);
                    dSectionSettings.Add("Giveaway_MinCurrencyChecked", sMinCurrencyChecked);
                    string sMustFollow = ini.GetValue("Default", "Giveaway_MustFollow", "0");
                    ini.SetValue("Default", "Giveaway_MustFollow", sMustFollow);
                    dSectionSettings.Add("Giveaway_MustFollow", sMustFollow);
                    string sMinCurrency = ini.GetValue("Default", "Giveaway_MinCurrency", "1");
                    ini.SetValue("Default", "Giveaway_MinCurrency", sMinCurrency);
                    dSectionSettings.Add("Giveaway_MinCurrency", sMinCurrency);
                    string sActiveUserTime = ini.GetValue("Default", "Giveaway_ActiveUserTime", "5");
                    ini.SetValue("Default", "Giveaway_ActiveUserTime", sActiveUserTime);
                    dSectionSettings.Add("Giveaway_ActiveUserTime", sActiveUserTime);
                    string sAutoBanWinnerChecked = ini.GetValue("Default", "Giveaway_AutoBanWinner", "0");
                    ini.SetValue("Default", "Giveaway_AutoBanWinner", sAutoBanWinnerChecked);
                    dSectionSettings.Add("Giveaway_AutoBanWinner", sAutoBanWinnerChecked);
                    string sBanList = ini.GetValue("Default", "Giveaway_BanList", "");
                    ini.SetValue("Default", "Giveaway_BanList", sBanList);
                    dSectionSettings.Add("Giveaway_BanList", sBanList);

                    dSettings.Add("Default", dSectionSettings);

                    SaveSettings();
                }
                bIgnoreUpdates = false;
            }
        }

        public void GrabData()
        {
            List<Transaction> transactions = Api.UpdateTransactions();
            if (transactions.Count > 0)
            {
                IOrderedEnumerable<Transaction> Trans = transactions.OrderByDescending(key => key.date);

                string sDonationsIgnoreRecent = ini.GetValue("Settings", "Donations_Ignore_Recent", "");
                ini.SetValue("Settings", "Donations_Ignore_Recent", sDonationsIgnoreRecent);
                string[] sRecentIgnores = sDonationsIgnoreRecent.Split(',');
                string sDonationsIgnoreLatest = ini.GetValue("Settings", "Donations_Ignore_Latest", "");
                ini.SetValue("Settings", "Donations_Ignore_Latest", sDonationsIgnoreLatest);
                string[] sLatestIgnores = sDonationsIgnoreLatest.Split(',');
                string sDonationsIgnoreTop = ini.GetValue("Settings", "Donations_Ignore_Top", "");
                ini.SetValue("Settings", "Donations_Ignore_Top", sDonationsIgnoreTop);
                string[] sTopIgnores = sDonationsIgnoreTop.Split(',');

                if (!donations.Visible)
                {
                    if (donations.IsHandleCreated)
                    {
                        donations.BeginInvoke((MethodInvoker)delegate
                        {
                            donations.Donations_List.Rows.Clear();
                            for (int i = 0; i < transactions.Count; i++)
                            {
                                donations.Donations_List.Rows.Add(Trans.ElementAt(i).date, Trans.ElementAt(i).donor, Trans.ElementAt(i).amount, Trans.ElementAt(i).id, Trans.ElementAt(i).notes, !sRecentIgnores.Contains(Trans.ElementAt(i).id), !sLatestIgnores.Contains(Trans.ElementAt(i).id), !sTopIgnores.Contains(Trans.ElementAt(i).id), true);
                            }
                        });
                    }
                    else
                    {
                        donations.Donations_List.Rows.Clear();
                        for (int i = 0; i < transactions.Count; i++)
                        {
                            donations.Donations_List.Rows.Add(Trans.ElementAt(i).date, Trans.ElementAt(i).donor, Trans.ElementAt(i).amount, Trans.ElementAt(i).id, Trans.ElementAt(i).notes, !sRecentIgnores.Contains(Trans.ElementAt(i).id), !sLatestIgnores.Contains(Trans.ElementAt(i).id), !sTopIgnores.Contains(Trans.ElementAt(i).id), true);
                        }
                        //donations.FirstDonationLabel.Text = Trans.ElementAt(Trans.Count() - 1).date;
                    }
                }

                int count = Convert.ToInt32(donations.RecentDonorsLimit.Value);
                if (transactions.Count < count)
                {
                    count = transactions.Count;
                }
                string sTopDonors = "", sRecentDonors = "", sLatestDonor = "";
                int iCount = 0;
                List<Transaction> Donors = new List<Transaction>();
                foreach (Transaction transaction in Trans)
                {
                    if (donations.UpdateRecentDonorsCheckBox.Checked)
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
                    if (donations.UpdateLastDonorCheckBox.Checked && !sLatestIgnores.Contains(transaction.id) && sLatestDonor == "")
                    {
                        File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "LatestDonation.txt", (sLatestDonor = transaction.ToString("$AMOUNT - DONOR")));
                    }

                    if (donations.UpdateTopDonorsCheckBox.Checked)
                    {
                        if (!sTopIgnores.Contains(transaction.id))
                        {
                            if (!Donors.Any(c => c.donor == transaction.donor))
                            {
                                Donors.Add(transaction);
                            }
                            else
                            {
                                foreach (Transaction trans in Donors)
                                {
                                    if (transaction.donor == trans.donor)
                                    {
                                        trans.amount = (float.Parse(trans.amount, CultureInfo.InvariantCulture.NumberFormat) + float.Parse(transaction.amount, CultureInfo.InvariantCulture.NumberFormat)).ToString("0.00");
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                if (donations.UpdateRecentDonorsCheckBox.Checked)
                {
                    File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "RecentDonors.txt", sRecentDonors);
                }

                Trans = Donors.OrderByDescending(key => float.Parse(key.amount));
                if (donations.UpdateTopDonorsCheckBox.Checked)
                {
                    count = Convert.ToInt32(donations.TopDonorsLimit.Value);
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
                            sTopDonors += Trans.ElementAt(iCount).ToString("$AMOUNT - DONOR");
                            iCount++;
                        }
                    }
                    File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "TopDonors.txt", sTopDonors);
                }
            }

            string sName = Api.capName(IRC.channel.Substring(1)), sTitle = "Unavailable...", sGame = "Unavailable...", sViewers = "";
            int iStatus = 0;
            if (IRC.irc.Connected)
            {
                if (IRC.g_bIsStreaming)
                {
                    iStatus = 2;
                }
                else
                {
                    iStatus = 1;
                }
            }
            using (WebClient w = new WebClient())
            {
                string json_data = "";
                try
                {
                    w.Proxy = null;
                    json_data = w.DownloadString("https://api.twitch.tv/kraken/channels/" + IRC.channel.Substring(1));
                    JObject stream = JObject.Parse(json_data);
                    if (!stream["display_name"].ToString().Equals("")) sName = stream["display_name"].ToString();
                    if (!stream["status"].ToString().Equals("")) sTitle = stream["status"].ToString();
                    if (!stream["game"].ToString().Equals("")) sGame = stream["game"].ToString();

                    json_data = w.DownloadString("http://tmi.twitch.tv/group/user/" + IRC.channel.Substring(1) + "/chatters");
                    if (json_data.Replace("\"", "") != "")
                    {
                        stream = JObject.Parse(json_data);
                        int iViewers = int.Parse(stream["chatter_count"].ToString());
                        foreach (string user in IRC.IgnoredUsers)
                        {
                            if (json_data.Contains("\"" + user + "\""))
                            {
                                iViewers--;
                            }
                        }
                        sViewers += " (" + iViewers + ")";
                    }
                }
                catch (SocketException)
                {
                    Console.WriteLine("Unable to connect to twitch API to check stream data.");
                }
                catch (Exception e)
                {
                    StreamWriter errorLog = new StreamWriter("Error_Log.log", true);
                    errorLog.WriteLine("*************Error Message (via GrabData()): " + DateTime.Now + "*********************************");
                    errorLog.WriteLine(e);
                    errorLog.WriteLine("");
                    errorLog.Close();
                }
            }

            if (IsHandleCreated)
            {
                BeginInvoke((MethodInvoker)delegate
                {
                    Currency_HandoutLabel.Text = "Handout " + IRC.currency + " to :";

                    Giveaway_MinCurrencyCheckBox.Text = "Min. " + IRC.currency;

                    ChannelLabel.Text = sName;
                    ChannelLabel.ForeColor = Color.Red;
                    if (iStatus == 2)
                    {
                        ChannelLabel.ForeColor = Color.Green;
                        ChannelLabel.Text += sViewers;
                    }
                    else if (iStatus == 1)
                    {
                        ChannelLabel.ForeColor = Color.Blue;
                    }
                    ChannelTitleTextBox.Text = sTitle;
                    ChannelGameTextBox.Text = sGame;
                });
            }
            else
            {
                Currency_HandoutLabel.Text = "Handout " + IRC.currency + " to :";

                Giveaway_MinCurrencyCheckBox.Text = "Min. " + IRC.currency;

                ChannelLabel.Text = sName;
                ChannelLabel.ForeColor = Color.Red;
                if (iStatus == 2)
                {
                    ChannelLabel.ForeColor = Color.Green;
                    ChannelLabel.Text += sViewers;
                }
                else if (iStatus == 1)
                {
                    ChannelLabel.ForeColor = Color.Blue;
                }
                ChannelTitleTextBox.Text = sTitle;
                ChannelGameTextBox.Text = sGame;
            }

            g_bLoaded = true;
            if (IRC.g_bResourceKeeper)
            {
                Thread.Sleep(30000);
            }
            GrabData();
        }

        private void aboutButton_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.ShowDialog();
            about.Dispose();
        }

        private void Giveaway_MinCurrencyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Giveaway_MinCurrency.Enabled = Giveaway_MinCurrencyCheckBox.Checked;
            SaveSettings();
        }

        private void Giveaway_RerollButton_Click(object sender, EventArgs e)
        {
            IRC.giveaway.getWinner();
        }

        private void Giveaway_StartButton_Click(object sender, EventArgs e)
        {
            IRC.giveaway.startGiveaway();
        }

        private void Giveaway_StopButton_Click(object sender, EventArgs e)
        {
            IRC.giveaway.endGiveaway();
        }

        private void Giveaway_AnnounceWinnerButton_Click(object sender, EventArgs e)
        {
            string sMessage = Giveaway_WinnerLabel.Text + " has won the giveaway!";
            if (Api.IsFollowingChannel(Giveaway_WinnerLabel.Text))
            {
                sMessage = sMessage + " (Currently follows the channel";
                sMessage = sMessage + " | Has " + IRC.db.checkCurrency(Giveaway_WinnerLabel.Text) + " " + IRC.currency + ")";
            }
            else
            {
                sMessage = sMessage + " (Has " + IRC.db.checkCurrency(Giveaway_WinnerLabel.Text) + " " + IRC.currency + ")";
            }
            IRC.sendMessage(sMessage);
        }

        private void Giveaway_BanButton_Click(object sender, EventArgs e)
        {
            Giveaway_BanListListBox.Items.Add(Giveaway_AddBanTextBox.Text);
            Giveaway_AddBanTextBox.Text = "";
            Giveaway_BanButton.Enabled = false;
            SaveSettings();
        }

        private void Giveaway_AddBanTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!Giveaway_AddBanTextBox.Text.Equals("") && Giveaway_BanListListBox.Items.Contains(Giveaway_AddBanTextBox.Text))
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
            if(Giveaway_BanListListBox.SelectedIndex >= 0) Giveaway_UnbanButton.Enabled = true;
        }

        private void Giveaway_UnbanButton_Click(object sender, EventArgs e)
        {
            Giveaway_BanListListBox.Items.RemoveAt(Giveaway_BanListListBox.SelectedIndex);
            Giveaway_UnbanButton.Enabled = false;
            SaveSettings();
        }

        private void Giveaway_CopyWinnerButton_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Clipboard.SetText(Giveaway_WinnerLabel.Text);
        }

        private void Giveaway_MustFollowCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void Misc_LockCurrencyCmdCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SaveSettings();
            IRC.g_iLastCurrencyLockAnnounce = 0;
        }

        private void Giveaway_MinCurrency_ValueChanged(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void Giveaway_ActiveUserTime_ValueChanged(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void Giveaway_AutoBanWinnerCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SaveSettings();
        }

        public void SaveSettings(int SettingsPresent=-2)
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
                    if (dSettings.ContainsKey(SettingsPresents.TabPages[SettingsPresent].Text))
                    {
                        //ini.SetValue("Settings", "SelectedPresent", SettingsPresents.TabPages[SettingsPresent].Text);
                        ini.SetValue(SettingsPresents.TabPages[SettingsPresent].Text, "Misc_LockCurrencyCmd", Misc_LockCurrencyCmdCheckBox.Checked ? "1" : "0");
                        ini.SetValue(SettingsPresents.TabPages[SettingsPresent].Text, "Giveaway_MustFollow", Giveaway_MustFollowCheckBox.Checked ? "1" : "0");
                        ini.SetValue(SettingsPresents.TabPages[SettingsPresent].Text, "Giveaway_MinCurrencyChecked", Giveaway_MinCurrencyCheckBox.Checked ? "1" : "0");
                        ini.SetValue(SettingsPresents.TabPages[SettingsPresent].Text, "Giveaway_MinCurrency", Giveaway_MinCurrency.Value.ToString());
                        ini.SetValue(SettingsPresents.TabPages[SettingsPresent].Text, "Giveaway_ActiveUserTime", Giveaway_ActiveUserTime.Value.ToString());
                        ini.SetValue(SettingsPresents.TabPages[SettingsPresent].Text, "Giveaway_AutoBanWinner", Giveaway_AutoBanWinnerCheckBox.Checked ? "1" : "0");
                        string items = "";
                        foreach (object item in Giveaway_BanListListBox.Items)
                        {
                            items = items + item.ToString() + ";";
                            //Console.WriteLine("Ban : " + item.ToString());
                        }
                        ini.SetValue(SettingsPresents.TabPages[SettingsPresent].Text, "Giveaway_BanList", items);
                    }
                    GetSettings();
                }
            }
        }

        private void Giveaway_WinnerTimer_Tick(object sender, EventArgs e)
        {
            if (IRC.ActiveUsers.ContainsKey(Api.capName(Giveaway_WinnerLabel.Text)))
            {
                int time = Api.GetUnixTimeNow() - IRC.ActiveUsers[Api.capName(Giveaway_WinnerLabel.Text)];
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
                if(t.Days > 0)
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

            if (IRC.giveaway.iLastWin > 0)
            {
                int time = Api.GetUnixTimeNow() - IRC.giveaway.iLastWin;
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

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("ModBot is currently active! Are you sure you want to close it?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
            else if (result == DialogResult.Yes)
            {
                Environment.Exit(0);
            }
        }

        private void SettingsPresents_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!bIgnoreUpdates)
            {
                IRC.giveaway.endGiveaway();
                ini.SetValue("Settings", "SelectedPresent", SettingsPresents.TabPages[SettingsPresents.SelectedIndex].Text);
                SaveSettings(iSettingsPresent);
                iSettingsPresent = SettingsPresents.SelectedIndex;
            }
        }

        private void Donations_ManageButton_Click(object sender, EventArgs e)
        {
            donations.ShowDialog();
        }

        private void Currency_HandoutEveryone_CheckedChanged(object sender, EventArgs e)
        {
            ini.SetValue("Settings", "Currency_Handout", "0");
        }

        private void Currency_HandoutActiveStream_CheckedChanged(object sender, EventArgs e)
        {
            ini.SetValue("Settings", "Currency_Handout", "1");
        }

        private void Currency_HandoutActiveTime_CheckedChanged(object sender, EventArgs e)
        {
            ini.SetValue("Settings", "Currency_Handout", "2");
            Currency_HandoutLastActive.Enabled = Currency_HandoutActiveTime.Checked;
        }

        private void Currency_HandoutLastActive_ValueChanged(object sender, EventArgs e)
        {
            ini.SetValue("Settings", "Currency_HandoutTime", Currency_HandoutLastActive.Value.ToString());
        }
    }
}
