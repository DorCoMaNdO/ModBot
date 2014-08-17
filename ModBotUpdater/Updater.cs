using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace ModBotUpdater
{
    public partial class Updater : CustomForm
    {
        private Changelog changelog = new Changelog();
        public Updater()
        {
            InitializeComponent();
            Text = "ModBot - Updater (v" + Assembly.GetExecutingAssembly().GetName().Version.ToString() + ")";

            CheckUpdates();
        }

        private bool IsFileLocked(string FileLocation)
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

        private void startDownload()
        {
            CurrentVersionLabel.Text = "Not Found";
            if (File.Exists("ModBot.exe"))
            {
                try
                {
                    CurrentVersionLabel.Text = FileVersionInfo.GetVersionInfo("ModBot.exe").FileVersion.ToString();
                }
                catch (NullReferenceException)
                {
                    while (File.Exists("ModBot.exe") && IsFileLocked("ModBot.exe"))
                    {
                        if (MessageBox.Show("The current ModBot version has been found corrupt, please close any open instences of it or applications that access or attempt to access it.", "ModBot Updater", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning) == DialogResult.Cancel) Environment.Exit(0);
                    }
                    File.Delete("ModBot.exe");
                }
            }

            if (!Directory.Exists("Updater"))
            {
                Directory.CreateDirectory("Updater");
            }
            else
            {
                if (File.Exists(@"Updater\ModBot.exe"))
                {
                    while (File.Exists(@"Updater\ModBot.exe") && IsFileLocked(@"Updater\ModBot.exe"))
                    {
                        if (MessageBox.Show("Please close ModBot that is inside the \"Updater\" inorder to continue with the update.", "ModBot Updater", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning) == DialogResult.Cancel) Environment.Exit(0);
                    }
                    File.Delete(@"Updater\ModBot.exe");
                }
            } 

            StateLabel.Text = "Initializing download...";
            Thread thread = new Thread(() =>
            {
                using (WebClient w = new WebClient())
                {
                    w.Proxy = null;
                    try
                    {
                        w.DownloadProgressChanged += new DownloadProgressChangedEventHandler((object sender, DownloadProgressChangedEventArgs e) =>
                        {
                            //label2.Text = "Downloaded " + e.BytesReceived + " of " + e.TotalBytesToReceive;
                            BeginInvoke((MethodInvoker)delegate
                            {
                                DownloadProgressBar.Value = int.Parse(Math.Truncate(double.Parse(e.BytesReceived.ToString()) / double.Parse(e.TotalBytesToReceive.ToString()) * 100).ToString());
                                //DownloadProgressBar.Value = e.ProgressPercentage;
                                StateLabel.Text = "Downloading...";
                            });
                        });
                        w.DownloadFileCompleted += new AsyncCompletedEventHandler((object sender, AsyncCompletedEventArgs e) =>
                        {
                            if (e.Error == null && !e.Cancelled)
                            {
                                BeginInvoke((MethodInvoker)delegate
                                {
                                    DoneDownloading();
                                });
                            }
                            else
                            {
                                BeginInvoke((MethodInvoker)delegate
                                {
                                    StateLabel.Text = "Error while attempting to update!";
                                });
                            }
                        });
                        w.DownloadFileAsync(new Uri("https://dl.dropboxusercontent.com/u/60356733/ModBot/" + LatestVersionLabel.Text + "/ModBot.exe"), @"Updater\ModBot.exe");
                    }
                    catch
                    {
                        StateLabel.Text = "Error while attempting to update!";
                    }
                }
            });
            thread.Start();
            thread.Join();
        }

        private void DoneDownloading()
        {
            if (File.Exists(@"Updater\ModBot.exe"))
            {
                while (File.Exists(@"Updater\ModBot.exe") && IsFileLocked(@"Updater\ModBot.exe"))
                {
                    if(MessageBox.Show("Please close ModBot that is inside the \"Updater\" inorder to continue with the update.", "ModBot Updater", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning) == DialogResult.Cancel) Environment.Exit(0);
                }

                FileStream fiLockFile = new FileInfo(@"Updater\ModBot.exe").Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                StateLabel.Text = "Checking for older version...";
                if (File.Exists("ModBot.exe"))
                {
                    StateLabel.Text = "Deleting older version...";
                    while (File.Exists("ModBot.exe") && IsFileLocked("ModBot.exe"))
                    {
                        if (MessageBox.Show("Please close ModBot inorder to continue with the update.", "ModBot Updater", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning) == DialogResult.Cancel) Environment.Exit(0);
                    }
                    File.Delete("ModBot.exe");
                    while (File.Exists("ModBot.exe")) { }
                }

                if (fiLockFile != null)
                {
                    fiLockFile.Close();
                }

                while (File.Exists(@"Updater\ModBot.exe") && IsFileLocked(@"Updater\ModBot.exe"))
                {
                    if (MessageBox.Show("Please close ModBot that is inside the \"Updater\" inorder to continue with the update.", "ModBot Updater", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning) == DialogResult.Cancel) Environment.Exit(0);
                }

                StateLabel.Text = "Moving updated version...";
                using (Stream inStream = File.Open(@"Updater\ModBot.exe", FileMode.Open)) // ToDo: report progress without lagging the UI
                {
                    using (Stream outStream = File.Create("ModBot.exe"))
                    {
                        while (inStream.Position < inStream.Length)
                        {
                            DownloadProgressBar.Value = int.Parse(Math.Truncate(double.Parse(inStream.Position.ToString()) / double.Parse(inStream.Length.ToString()) * 100).ToString());
                            outStream.WriteByte((byte)inStream.ReadByte());
                        }
                    }
                }
                File.Delete(@"Updater\ModBot.exe");
                //File.Move(@"Updater\ModBot.exe", "ModBot.exe");
                while (File.Exists(@"Updater\ModBot.exe")) { }

                if (Directory.Exists("Updater"))
                {
                    Directory.Delete("Updater", true);
                }

                DownloadProgressBar.Value = 100;
                CurrentVersionLabel.Text = "Not Found";
                StateLabel.Text = "Done updating!";
                if (File.Exists("ModBot.exe"))
                {
                    CurrentVersionLabel.Text = FileVersionInfo.GetVersionInfo("ModBot.exe").FileVersion.ToString();
                    if (CurrentVersionLabel.Text == LatestVersionLabel.Text)
                    {
                        StateLabel.Text = "Done updating and up-to-date!";
                    }
                }

                if (Program.args.Contains("-modbot"))
                {
                    Program.args.Remove("-modbot");
                    string arg = "";
                    if (Program.args.Contains("-modbotconnect"))
                    {
                        Program.args.Remove("-modbotconnect");
                        arg = "-connect";
                    }
                    if (Program.args.Contains("-modbotupdate"))
                    {
                        Program.args.Remove("-modbotupdate");
                        arg += (arg != "" ? " " : "") + "-autoupdate";
                    }
                    Process.Start("ModBot.exe", arg);
                    if (Program.args.Contains("-close")) Close();
                }

                UpdateChangelog();
            }
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            startDownload();
        }

        private void CheckUpdatesButton_Click(object sender, EventArgs e)
        {
            CheckUpdates();
        }

        private void CheckUpdates()
        {
            ChangelogButton.Enabled = false;
            CurrentVersionLabel.Text = "Not Found";
            if (File.Exists("ModBot.exe"))
            {
                try
                {
                    CurrentVersionLabel.Text = FileVersionInfo.GetVersionInfo("ModBot.exe").FileVersion.ToString();
                }
                catch (NullReferenceException)
                {
                    while (File.Exists("ModBot.exe") && IsFileLocked("ModBot.exe"))
                    {
                        if (MessageBox.Show("The current ModBot version has been found corrupt, please close any open instences of it or applications that access or attempt to access it.", "ModBot Updater", MessageBoxButtons.OK, MessageBoxIcon.Warning) == DialogResult.Cancel) Environment.Exit(0);
                    }
                    File.Delete("ModBot.exe");
                }
            }

            string sLatestVersion = "", sFileSizeSuffix = "Bytes";
            double dFileSize = 0;
            LatestVersionLabel.Text = "Checking...";
            StateLabel.Text = "Checking for updates...";
            Thread thread = new Thread(() =>
            {
                thread = new Thread(() =>
                {
                    using (WebClient w = new WebClient())
                    {
                        w.Proxy = null;
                        try
                        {
                            sLatestVersion = w.DownloadString("https://dl.dropboxusercontent.com/u/60356733/ModBot/ModBot.txt");
                            w.OpenRead("https://dl.dropboxusercontent.com/u/60356733/ModBot/" + sLatestVersion + "/ModBot.exe");
                            dFileSize = Convert.ToDouble(w.ResponseHeaders["Content-Length"]);
                        }
                        catch
                        {
                        }
                    }
                });
                thread.Start();
                thread.Join();

                while (dFileSize >= 1024)
                {
                    dFileSize /= 1024;
                    if (sFileSizeSuffix == "Bytes")
                    {
                        sFileSizeSuffix = "KBs";
                    }
                    else if (sFileSizeSuffix == "KBs")
                    {
                        sFileSizeSuffix = "MBs";
                    }
                    else if (sFileSizeSuffix == "MBs")
                    {
                        sFileSizeSuffix = "GBs";
                    }
                }

                BeginInvoke((MethodInvoker)delegate
                {
                    if (sLatestVersion != "")
                    {
                        string[] sCurrent = CurrentVersionLabel.Text.Split('.'), sLatest = (LatestVersionLabel.Text = sLatestVersion).Split('.');
                        if (CurrentVersionLabel.Text == "Not Found" || TimeSpan.FromDays(int.Parse(sCurrent[2])).Add(TimeSpan.FromSeconds(int.Parse(sCurrent[3]))).CompareTo(TimeSpan.FromDays(int.Parse(sLatest[2])).Add(TimeSpan.FromSeconds(int.Parse(sLatest[3])))) == -1)
                        {
                            DownloadProgressBar.Value = 0;
                            if (dFileSize > 0)
                            {
                                StateLabel.Text = "Updates available... (" + dFileSize.ToString("0.00") + " " + sFileSizeSuffix + ")";
                            }
                            else
                            {
                                StateLabel.Text = "Updates available...";
                            }
                        }
                        else
                        {
                            StateLabel.Text = "Current version is up-to-date!";
                        }
                    }
                    else
                    {
                        LatestVersionLabel.Text = "Error!";
                        StateLabel.Text = "Error while checking for updates!";
                    }
                });

                if (Program.args.Contains("-force"))
                {
                    Program.args.Remove("-force");
                    startDownload();
                    return;
                }

                UpdateChangelog();
            });
            thread.Start();
        }

        private void StateLabel_TextChanged(object sender, EventArgs e)
        {
            StateLabel.ForeColor = Color.Black;
            if (StateLabel.Text == "Error while checking for updates!" || StateLabel.Text == "Error while attempting to update!" || StateLabel.Text.Contains("Updates available...") || StateLabel.Text == "Unknown")
            {
                StateLabel.ForeColor = Color.Red;
            }
            else if (StateLabel.Text == "Current version is up-to-date!" || StateLabel.Text == "Downloading..." || StateLabel.Text.Contains("Done updating") || StateLabel.Text == "Moving updated version...")
            {
                StateLabel.ForeColor = Color.Green;
            }
            else if (StateLabel.Text == "Initializing download..." || StateLabel.Text == "Checking for updates..." || StateLabel.Text == "Checking for older version..." || StateLabel.Text == "Deleting older version...")
            {
                StateLabel.ForeColor = Color.Orange;
            }

            DownloadProgressBar.Text = "";
            DownloadProgressBar.TextColor = Brushes.Black;
            if (StateLabel.Text == "Error while checking for updates!" || StateLabel.Text == "Error while attempting to update!")
            {
                DownloadProgressBar.Text = "Error!";
                DownloadProgressBar.TextColor = Brushes.Red;
            }

            if (StateLabel.Text.Contains("Updates available...") || StateLabel.Text == "Error while attempting to update!")
            {
                UpdateButton.Enabled = true;
                CheckUpdatesButton.Enabled = false;
            }
            else if (StateLabel.Text == "Initializing download..." || StateLabel.Text == "Downloading..." || StateLabel.Text == "Checking for updates...")
            {
                UpdateButton.Enabled = false;
                CheckUpdatesButton.Enabled = false;
            }
            else
            {
                UpdateButton.Enabled = false;
                CheckUpdatesButton.Enabled = true;
            }
        }

        private void LatestVersionLabel_TextChanged(object sender, EventArgs e)
        {
            LatestVersionLabel.ForeColor = Color.Black;
            if(LatestVersionLabel.Text == "Error!" || LatestVersionLabel.Text == "Not Found")
            {
                LatestVersionLabel.ForeColor = Color.Red;
            }
            else if(LatestVersionLabel.Text == "Checking...")
            {
                LatestVersionLabel.ForeColor = Color.Orange;
            }
        }

        private void CurrentVersionLabel_TextChanged(object sender, EventArgs e)
        {
            CurrentVersionLabel.ForeColor = Color.Black;
            if (CurrentVersionLabel.Text == "Error!" || CurrentVersionLabel.Text == "Not Found")
            {
                CurrentVersionLabel.ForeColor = Color.Red;
            }
        }

        public void UpdateChangelog()
        {
            Thread thread = new Thread(() =>
            {
                string sData = "";
                thread = new Thread(() =>
                {
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
                });
                thread.Start();
                thread.Join();

                BeginInvoke((MethodInvoker)delegate
                {
                    if (LatestVersionLabel.Text.IndexOfAny(new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' }) != -1) return;
                    bool Found = (CurrentVersionLabel.Text.IndexOfAny(new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' }) == -1);
                    string[] sCurrent = CurrentVersionLabel.Text.Split('.'), sLatest = LatestVersionLabel.Text.Split('.');
                    int iCurrentMajor = Found ? int.Parse(sCurrent[0]) : 0, iCurrentMinor = Found ? int.Parse(sCurrent[1]) : 0, iCurrentBuild = Found ? int.Parse(sCurrent[2]) : 0, iCurrentRev = Found ? int.Parse(sCurrent[3]) : 0, iLatestMajor = int.Parse(sLatest[0]), iLatestMinor = int.Parse(sLatest[1]), iLatestBuild = int.Parse(sLatest[2]), iLatestRev = int.Parse(sLatest[3]);

                    sData = sData.Replace("* ", "*  ");

                    changelog.ChangelogNotes.Text = "";

                    while (sData != "")
                    {
                        string sVersion = sData.Substring(sData.IndexOf("[\"") + 2, sData.IndexOf("\"]\r\n{\"") - sData.IndexOf("[\"") - 2), sChanges = sData.Substring(sData.IndexOf("\"]\r\n{\"") + 6, sData.IndexOf("\"}") - sData.IndexOf("\"]\r\n{\"") - 6);
                        string[] sLogVersion = sVersion.Split('.');
                        string sDate = "";
                        int iLogMajor = int.Parse(sLogVersion[0]), iLogMinor = sLogVersion[1] != "*" ? int.Parse(sLogVersion[1]) : 0, iLogBuild = sLogVersion.Length > 2 ? sLogVersion[2] != "*" ? int.Parse(sLogVersion[2]) : 0 : 0, iLogRev = sLogVersion.Length > 3 ? sLogVersion[3] != "*" ? int.Parse(sLogVersion[3]) : 0 : 0;
                        if (iLogBuild > 0)
                        {
                            sDate = " (" + new DateTime(2000, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc).AddDays(iLogBuild).AddSeconds(iLogRev * 2).ToLocalTime().ToString(iLogRev > 0 ? "M/dd/yyyy hh:mm:ss tt" : "M/dd/yyyy") + ")";
                        }

                        changelog.ChangelogNotes.SelectionColor = Color.Blue;
                        int Compare = Found ? TimeSpan.FromDays(iCurrentBuild).Add(TimeSpan.FromSeconds(iCurrentRev)).CompareTo(TimeSpan.FromDays(iLogBuild).Add(TimeSpan.FromSeconds(iLogRev))) : -1;
                        if (Compare == -1 || iLogMajor > iCurrentMajor || iLogMinor > iCurrentMinor)
                        {
                            changelog.ChangelogNotes.SelectionColor = Color.Red;
                        }
                        else if (Compare == 0 || iLogMajor == iCurrentMajor && sLogVersion[1] == "*" || iLogMajor == iCurrentMajor && iLogMinor == iCurrentMinor && sLogVersion[2] == "*" || iLogMajor == iCurrentMajor && iLogMinor == iCurrentMinor && iLogBuild == iCurrentBuild && sLogVersion[3] == "*")
                        {
                            changelog.ChangelogNotes.SelectionColor = Color.Green;
                        }
                        changelog.ChangelogNotes.SelectionFont = new Font("Segoe Print", 8, FontStyle.Bold);
                        //changelog.ChangelogNotes.SelectedText = sVersion + " " + sDate + " :\r\n";
                        changelog.ChangelogNotes.SelectedText = sVersion;
                        changelog.ChangelogNotes.SelectionColor = Color.Red;
                        changelog.ChangelogNotes.SelectedText = sDate;
                        //changelog.ChangelogNotes.SelectionColor = Color.Red;
                        //changelog.ChangelogNotes.SelectionFont = new Font("Segoe Print", 7, FontStyle.Regular);
                        changelog.ChangelogNotes.SelectionColor = Color.Black;
                        changelog.ChangelogNotes.SelectedText = " :\r\n";
                        changelog.ChangelogNotes.SelectionFont = new Font("Microsoft Sans Serif", 8);
                        changelog.ChangelogNotes.SelectedText = sChanges;
                        sData = sData.Substring(sData.IndexOf("\"}") + 2);
                        if (sData != "")
                        {
                            changelog.ChangelogNotes.SelectedText = "\r\n\r\n";
                        }
                    }
                    changelog.ChangelogNotes.Select(0, 0);
                    changelog.ChangelogNotes.ScrollToCaret();
                    ChangelogButton.Enabled = true;
                });
            });
            thread.Start();
        }

        private void ChangelogButton_Click(object sender, EventArgs e)
        {
            changelog.ShowDialog();
        }

        private void Updater_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
