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
        public iniUtil ini = Irc.ini = new iniUtil(AppDomain.CurrentDomain.BaseDirectory + "modbot.ini");
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
            Irc.Initialize(botNameBox.Text.ToLower(), passwordBox.Text, channelBox.Text.ToLower(), currencyBox.Text, int.Parse(intervalBox.SelectedItem.ToString()), int.Parse(payoutBox.SelectedItem.ToString()), DonationsKeyBox.Text);
        }

        private void aboutButton_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.ShowDialog();
            about.Dispose();
        }
    }
}
