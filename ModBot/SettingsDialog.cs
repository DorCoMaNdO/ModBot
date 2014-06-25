using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ModBot
{
    public partial class SettingsDialog : CustomForm
    {
        public iniUtil ini = new iniUtil(AppDomain.CurrentDomain.BaseDirectory + "modbot.ini");
        public SettingsDialog()
        {
            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "modbot.ini"))
            {
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "modbot.ini", "\r\n[Default]");
            }
            InitializeComponent();
            string sBotName = ini.GetValue("Settings", "BOT_Name", "ModBot");
            ini.SetValue("Settings", "BOT_Name", sBotName);
            botNameBox.Text = sBotName;
            string sBotPass = ini.GetValue("Settings", "BOT_Password", "");
            ini.SetValue("Settings", "BOT_Password", sBotPass);
            passwordBox.Text = sBotPass;
            string sChannelName = ini.GetValue("Settings", "Channel_Name", "ModChannel");
            ini.SetValue("Settings", "Channel_Name", sChannelName);
            channelBox.Text = sChannelName;
            string sCurrency = ini.GetValue("Settings", "Currency_Name", "ModCoins");
            ini.SetValue("Settings", "Currency_Name", sCurrency);
            currencyBox.Text = sCurrency;
            string sCurrencyInterval = ini.GetValue("Settings", "Currency_Interval", "4");
            ini.SetValue("Settings", "Currency_Interval", sCurrencyInterval);
            intervalBox.SelectedIndex = Convert.ToInt32(sCurrencyInterval);
            string sCurrencyPayout = ini.GetValue("Settings", "Currency_Payout", "0");
            ini.SetValue("Settings", "Currency_Payout", sCurrencyPayout);
            payoutBox.SelectedIndex = Convert.ToInt32(sCurrencyPayout);
            string sSubURL = ini.GetValue("Settings", "Subsribers_URL", "");
            ini.SetValue("Settings", "Subsribers_URL", sSubURL);
            subBox.Text = sSubURL;
            string sDonationKey = ini.GetValue("Settings", "Donations_Key", "");
            ini.SetValue("Settings", "Donations_Key", sDonationKey);
            DonationsKeyBox.Text = sDonationKey;

            ExtractUpdater();

            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "ModBotUpdater.exe"))
            {
                string sLatestVersion = "", sCurrentVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                Thread thread = new Thread(
                () =>
                {
                    using (WebClient w = new WebClient())
                    {
                        try
                        {
                            w.Proxy = null;
                            sLatestVersion = w.DownloadString("https://dl.dropboxusercontent.com/u/60356733/ModBot.txt");
                        }
                        catch (SocketException)
                        {
                        }
                        catch (Exception)
                        {
                        }
                    }
                });
                thread.Start();
                thread.Join();

                if (sLatestVersion != "")
                {
                    string[] sCurrent = sCurrentVersion.Split('.');
                    string[] sLatest = sLatestVersion.Split('.');
                    int iCurrentMajor = Convert.ToInt32(sCurrent[0]), iCurrentMinor = Convert.ToInt32(sCurrent[1]), iCurrentBuild = Convert.ToInt32(sCurrent[2]), iCurrentRev = Convert.ToInt32(sCurrent[3]);
                    int iLatestMajor = Convert.ToInt32(sLatest[0]), iLatestMinor = Convert.ToInt32(sLatest[1]), iLatestBuild = Convert.ToInt32(sLatest[2]), iLatestRev = Convert.ToInt32(sLatest[3]);
                    if (iLatestMajor > iCurrentMajor || iLatestMajor == iCurrentMajor && iLatestMinor > iCurrentMinor || iLatestMajor == iCurrentMajor && iLatestMinor == iCurrentMinor && iLatestBuild > iCurrentBuild || iLatestMajor == iCurrentMajor && iLatestMinor == iCurrentMinor && iLatestBuild == iCurrentBuild && iLatestRev > iCurrentRev)
                    {
                        Console.WriteLine("\r\n********************************************************************************\r\nAn update to ModBot is available, please use the updater to update!\r\n(Current version: " + sCurrentVersion + ", Latest version: " + sLatestVersion + ")\r\n\r\n********************************************************************************\r\n");
                        if (MessageBox.Show("An update to ModBot is available!\r\n(Current version: " + sCurrentVersion + ", Latest version: " + sLatestVersion + ")\r\nDo you want to update now?", "ModBot", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                        {
                            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "ModBotUpdater.exe"))
                            {
                                ExtractUpdater();
                            }
                            Process.Start(AppDomain.CurrentDomain.BaseDirectory + "ModBotUpdater.exe");
                            Environment.Exit(0);
                        }
                    }
                }
            }
        }

        private void ExtractUpdater()
        {
            byte[] rawUpdater = ModBot.Properties.Resources.ModBotUpdater;
            string sLatestVersion = Assembly.Load(rawUpdater).GetName().Version.ToString();
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "ModBotUpdater.exe"))
            {
                string sCurrentVersion = FileVersionInfo.GetVersionInfo(AppDomain.CurrentDomain.BaseDirectory + "ModBotUpdater.exe").FileVersion.ToString();
                string[] sCurrent = sCurrentVersion.Split('.');
                string[] sLatest = sLatestVersion.Split('.');
                int iCurrentMajor = Convert.ToInt32(sCurrent[0]), iCurrentMinor = Convert.ToInt32(sCurrent[1]), iCurrentBuild = Convert.ToInt32(sCurrent[2]), iCurrentRev = Convert.ToInt32(sCurrent[3]);
                int iLatestMajor = Convert.ToInt32(sLatest[0]), iLatestMinor = Convert.ToInt32(sLatest[1]), iLatestBuild = Convert.ToInt32(sLatest[2]), iLatestRev = Convert.ToInt32(sLatest[3]);
                if (iLatestMajor > iCurrentMajor || iLatestMajor == iCurrentMajor && iLatestMinor > iCurrentMinor || iLatestMajor == iCurrentMajor && iLatestMinor == iCurrentMinor && iLatestBuild > iCurrentBuild || iLatestMajor == iCurrentMajor && iLatestMinor == iCurrentMinor && iLatestBuild == iCurrentBuild && iLatestRev > iCurrentRev)
                {
                    while (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "ModBotUpdater.exe") && IsFileLocked(AppDomain.CurrentDomain.BaseDirectory + "ModBotUpdater.exe"))
                    {
                        MessageBox.Show("Please close ModBot's Updater, a new version of the updater is available and will be extracted.", "ModBot", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + "ModBotUpdater.exe");
                    while (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "ModBotUpdater.exe")) { }

                    using (FileStream fsUpdater = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "ModBotUpdater.exe", FileMode.CreateNew, FileAccess.Write))
                    {
                        fsUpdater.Write(rawUpdater, 0, rawUpdater.Length);
                    }

                    MessageBox.Show("ModBot Updater has been updated from v" + sCurrentVersion + " to v" + sLatestVersion + " sucessfully.", "ModBot", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                using (FileStream fsUpdater = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "ModBotUpdater.exe", FileMode.CreateNew, FileAccess.Write))
                {
                    fsUpdater.Write(rawUpdater, 0, rawUpdater.Length);
                }
                MessageBox.Show("ModBot Updater has been extracted sucessfully (v" + sLatestVersion + ").", "ModBot", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private bool IsFileLocked(String FileLocation)
        {
            FileInfo file = new FileInfo(FileLocation);
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            //save session settings
            ini.SetValue("Settings", "BOT_Name", botNameBox.Text);
            ini.SetValue("Settings", "BOT_Password", passwordBox.Text);
            ini.SetValue("Settings", "Channel_Name", channelBox.Text);
            ini.SetValue("Settings", "Currency_Name", currencyBox.Text);
            ini.SetValue("Settings", "Currency_Interval", intervalBox.SelectedIndex.ToString());
            ini.SetValue("Settings", "Currency_Payout", payoutBox.SelectedIndex.ToString());
            ini.SetValue("Settings", "Donations_Key", DonationsKeyBox.Text);
            if ((subBox.Text.StartsWith("https://spreadsheets.google.com") || subBox.Text.StartsWith("http://spreadsheets.google.com")) && subBox.Text.EndsWith("?alt=json"))
            {
                ini.SetValue("Settings", "Subsribers_URL", subBox.Text);
            }
            else Console.WriteLine("Invalid subscriber link.  Reverting to the last known good link, or blank.  Restart the program to fix it.");
            ////

            //Console.WriteLine(nick + ' ' + password + ' ' + channel + ' ' + currency + ' ' + interval);
            Irc IRC = new Irc(botNameBox.Text.ToLower(), passwordBox.Text, channelBox.Text.ToLower(), currencyBox.Text, int.Parse(intervalBox.SelectedItem.ToString()), int.Parse(payoutBox.SelectedItem.ToString()), DonationsKeyBox.Text, ini);
        }

        private void aboutButton_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.ShowDialog();
            about.Dispose();
        }
    }
}
