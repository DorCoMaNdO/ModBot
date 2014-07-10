using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;

namespace ModBot
{
    public partial class About : CustomForm
    {
        public About()
        {
            InitializeComponent();
        }

        private void WebsiteLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://sourceforge.net/projects/twitchmodbot/");
            System.Diagnostics.Process.Start("http://modbot.wordpress.com/");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SupportLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://modbot.wordpress.com/about/");
        }

        private void EmailLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:DorCoMaNdO@gmail.com");
        }
    }
}
