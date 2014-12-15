using ModBot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CoMaNdO.SongRequests
{
    public partial class SongRequestsWindow : Form
    {
        public Settings ini;
        private IExtension extension;

        public SongRequestsWindow(IExtension sender)
        {
            extension = sender;

            InitializeComponent();

            ini = SongRequest.ini; new Settings(extension, "Settings.ini");

            UI.CenterSpacer(RequestingRulesLabel, RequestingRulesSpacer, false, true);

            ChargeRequest.Text = "Requesting costs                       " + Currency.Name;

            ini.SetValue("Settings", "ChargeRequest", (ChargeRequest.Checked = (ini.GetValue("Settings", "ChargeRequest", "1") == "1")) ? "1" : "0");
            int variable = Convert.ToInt32(ini.GetValue("Settings", "RequestPrice", "25"));
            if (variable > RequestPrice.Maximum || variable < RequestPrice.Minimum)
            {
                variable = 25;
            }
            ini.SetValue("Settings", "RequestPrice", (RequestPrice.Value = variable).ToString());
            ini.SetValue("Settings", "LimitRequests", (LimitRequests.Checked = (ini.GetValue("Settings", "LimitRequests", "1") == "1")) ? "1" : "0");
            variable = Convert.ToInt32(ini.GetValue("Settings", "RequestsLimit", "2"));
            if (variable > RequestsLimit.Maximum || variable < RequestsLimit.Minimum)
            {
                variable = 2;
            }
            ini.SetValue("Settings", "RequestsLimit", (RequestsLimit.Value = variable).ToString());
        }

        private void SongRequestsWindow_Load(object sender, EventArgs e)
        {
            /*SongRequestPlayer.Visible = true;
            SongRequestPlayer.BringToFront();
            SongRequestPlayer.Url = new Uri("http://google.com");
            //YouTube.PlaySong();
            //SongRequestPlayer.Document.Write("<iframe id=\"musicPlayer\" type=\"text/html\" src=\"https://www.youtube.com/apiplayer?video_id=oOxPvmXNVtA&version=3&autoplay=1&enablejsapi=1&feature=player_embedded&controls=0&modestbranding=1&rel=0&showinfo=0&autohide=1&color=white&playerapiid=musicPlayer&iv_load_policy=3\" frameborder=\"0\" allowfullscreen>");
            SongRequestPlayer.Navigated += YouTube.SongRequestPlayer_Navigated;*/
        }

        private void Settings_Changed(object sender, EventArgs e)
        {
            Control ctrl = (Control)sender;
            if(ctrl.GetType() == typeof(CheckBox))
            {
                CheckBox cb = (CheckBox)ctrl;

                if (cb == ChargeRequest)
                {
                    RequestPrice.Enabled = cb.Checked;
                }
                else if(cb == LimitRequests)
                {
                    RequestsLimit.Enabled = cb.Checked;
                }

                ini.SetValue("Settings", cb.Name, cb.Checked ? "1" : "0");
            }
            else if (ctrl.GetType() == typeof(NumericUpDown))
            {
                NumericUpDown nud = (NumericUpDown)ctrl;

                ini.SetValue("Settings", nud.Name, nud.Value.ToString());
            }
        }
    }
}
