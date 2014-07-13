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
        private iniUtil ini = Program.ini;
        public SettingsDialog()
        {
            InitializeComponent();
            ini.SetValue("Settings", "BOT_Name", botNameBox.Text = ini.GetValue("Settings", "BOT_Name", "ModBot"));
            ini.SetValue("Settings", "BOT_Password", passwordBox.Text = ini.GetValue("Settings", "BOT_Password", ""));
            ini.SetValue("Settings", "Channel_Name", channelBox.Text = ini.GetValue("Settings", "Channel_Name", "ModChannel"));
            ini.SetValue("Settings", "Currency_Name", currencyBox.Text = ini.GetValue("Settings", "Currency_Name", "ModCoins"));
            ini.SetValue("Settings", "Currency_Interval", (intervalBox.SelectedIndex = Convert.ToInt32(ini.GetValue("Settings", "Currency_Interval", "4"))).ToString());
            ini.SetValue("Settings", "Currency_Payout", (payoutBox.SelectedIndex = Convert.ToInt32(ini.GetValue("Settings", "Currency_Payout", "0"))).ToString());
            ini.SetValue("Settings", "Subsribers_URL", subBox.Text = ini.GetValue("Settings", "Subsribers_URL", ""));
            ini.SetValue("Settings", "Donations_Key", DonationsKeyBox.Text = ini.GetValue("Settings", "Donations_Key", ""));
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            //save session settings
            ini.SetValue("Settings", "BOT_Name", Irc.nick = botNameBox.Text);
            ini.SetValue("Settings", "BOT_Password", Irc.password = passwordBox.Text);
            ini.SetValue("Settings", "Channel_Name", channelBox.Text);
            Irc.admin = channelBox.Text.Replace("#", "");
            Irc.channel = "#" + channelBox.Text.Replace("#", "");
            ini.SetValue("Settings", "Currency_Name", currencyBox.Text);
            Irc.currency = currencyBox.Text.Replace("!", "");
            ini.SetValue("Settings", "Currency_Interval", intervalBox.SelectedIndex.ToString());
            Irc.interval = Convert.ToInt32(intervalBox.SelectedItem.ToString());
            ini.SetValue("Settings", "Currency_Payout", payoutBox.SelectedIndex.ToString());
            Irc.payout = Convert.ToInt32(payoutBox.SelectedItem.ToString());
            ini.SetValue("Settings", "Donations_Key", Irc.donationkey = DonationsKeyBox.Text);
            if (subBox.Text != "")
            {
                if ((subBox.Text.StartsWith("https://spreadsheets.google.com") || subBox.Text.StartsWith("http://spreadsheets.google.com")) && subBox.Text.EndsWith("?alt=json"))
                {
                    ini.SetValue("Settings", "Subsribers_URL", subBox.Text);
                }
                else
                {
                    Console.WriteLine("Invalid subscriber link. Reverting to the last known good link, or blank. Restart the program to fix it.");
                }
            }
            ////

            //Console.WriteLine(nick + ' ' + password + ' ' + channel + ' ' + currency + ' ' + interval);
            Irc.Initialize();
        }

        private void aboutButton_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.ShowDialog();
            about.Dispose();
        }
    }
}
