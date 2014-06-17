using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ModBot
{
    public partial class SettingsDialog : Form
    {
        public String nick;
        public String password;
        public String channel;
        public String currency;
        public int interval;



        public SettingsDialog()
        {
            InitializeComponent();
            intervalBox.SelectedIndex = global::ModBot.Properties.Settings.Default.interval;
            payoutBox.SelectedIndex = global::ModBot.Properties.Settings.Default.payout;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            String nick = botNameBox.Text.ToLower();
            String password = passwordBox.Text;
            String channel = channelBox.Text.ToLower();
            String currency = currencyBox.Text;
            int interval = int.Parse(intervalBox.SelectedItem.ToString());
            int payout = int.Parse(payoutBox.SelectedItem.ToString());

            //save session settings
            Properties.Settings.Default.name = botNameBox.Text;
            Properties.Settings.Default.password = passwordBox.Text;
            Properties.Settings.Default.channel = channelBox.Text;
            Properties.Settings.Default.currency = currencyBox.Text;
            Properties.Settings.Default.interval = intervalBox.SelectedIndex;
            Properties.Settings.Default.payout = payoutBox.SelectedIndex;
            if ((subBox.Text.StartsWith("https://spreadsheets.google.com") || subBox.Text.StartsWith("http://spreadsheets.google.com"))&& subBox.Text.EndsWith("?alt=json"))
            {
                Properties.Settings.Default.subUrl = subBox.Text;
            }
            else Console.WriteLine("Invalid subscriber link.  Reverting to the last known good link, or blank.  Restart the program to fix it.");
            Properties.Settings.Default.Save();
            ////
            
            //Console.WriteLine(nick + ' ' + password + ' ' + channel + ' ' + currency + ' ' + interval);
            Irc IRC = new Irc(nick, password, channel, currency, interval, payout);
            
        }

        private void aboutButton_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.ShowDialog();
            about.Dispose();
        }
        
    }
}
