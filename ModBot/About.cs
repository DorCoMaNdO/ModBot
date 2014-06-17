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
            version.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        private void About_Load(object sender, EventArgs e)
        {
            LinkLabel.Link link = new LinkLabel.Link();
            link.LinkData = "https://sourceforge.net/projects/twitchmodbot/";
            linkLabel1.Links.Add(link);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Link.LinkData as string);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
