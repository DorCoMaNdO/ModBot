using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ModBot
{
    public partial class Changelog : CustomForm
    {
        public Changelog()
        {
            InitializeComponent();
            ChangesList.Location = new Point(8, 30);
            ChangesList.Size = new Size(Width - 16, Height - 38);

            ChangesList.Visible = false;
            /*new Thread(() =>
            {
                string sData = "";
                using (WebClient w = new WebClient())
                {
                    w.Proxy = null;
                    try
                    {
                        sData = w.DownloadString("https://dl.dropboxusercontent.com/u/60356733/ModBot/ModBot-Changelog.txt");
                    }
                    catch
                    {
                    }
                }

                Dictionary<string, string> Versions = new Dictionary<string, string>();
                while (sData != "")
                {
                    string Version = sData.Substring(sData.IndexOf("[\"") + 2, sData.IndexOf("\"]\r\n{\"") - sData.IndexOf("[\"") - 2), Changes = sData.Substring(sData.IndexOf("\"]\r\n{\"") + 6, sData.IndexOf("\"}") - sData.IndexOf("\"]\r\n{\"") - 6);

                    sData = sData.Substring(sData.IndexOf("\"}") + 2);

                    if (Versions.ContainsKey(Version)) continue;

                    Versions.Add(Version, Changes);
                }

                BeginInvoke((MethodInvoker)delegate
                {
                    foreach (string Version in Versions.Keys)
                    {
                        TreeNode Node = new TreeNode();
                        Node.Text = Version;
                        foreach (string change in Versions[Version].Split(Environment.NewLine.ToCharArray())) if(change != "") Node.Nodes.Add(change);
                        ChangesList.Nodes.Add(Node);
                    }
                });
            }).Start();*/
        }
    }
}
