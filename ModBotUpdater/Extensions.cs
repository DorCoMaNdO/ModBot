using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace ModBotUpdater
{
    public partial class Extensions : CustomForm
    {
        public int SelectedExtension;

        public Extensions()
        {
            InitializeComponent();

            //DownloadProgressBar.Text = "Loading...";
            DownloadProgressBar.Text = "Select an extension and then press the Download button to begin the download";
            BaseControls[0].Enabled = false;

            new Thread(() =>
            {
                using (WebClient w = new WebClient())
                {
                    w.Proxy = null;
                    try
                    {
                        string Extensions = w.DownloadString("https://dl.dropboxusercontent.com/u/60356733/ModBot/Extensions/Extensions.txt");
                        if (Extensions != "")
                        {
                            Program.Updater.Invoke(() =>
                            {
                                foreach (string Extension in Extensions.Split(Environment.NewLine.ToCharArray()))
                                {
                                    string[] data = Extension.Split(';');
                                    //if (data.Length > 3) ExtensionsDataGrid.Rows.Add(data[0], data[1], data[2], data[3], "Loading...", "Loading...");
                                    if (data.Length > 6) ExtensionsDataGrid.Rows.Add(data[0], data[1], data[2], data[3], data[4], data[5], data[6]);
                                }
                                DownloadProgressBar.Text = "Select an extension and then press the Download button to begin the download";
                                BaseControls[0].Enabled = true;
                            });
                        }
                    }
                    catch
                    {
                        Program.Updater.Invoke(() =>
                        {
                            DownloadProgressBar.Text = "An error has occoured while listing the extensions";
                            BaseControls[0].Enabled = true;
                        });
                    }
                }
            }).Start();
        }

        private void ExtensionSelected(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            SelectedExtension = e.RowIndex;
            ExtensionLabel.Text = "Selected Extension: " + ExtensionsDataGrid["ExtensionName", SelectedExtension].Value.ToString() + " By " + ExtensionsDataGrid["Author", SelectedExtension].Value.ToString();
            DownloadButton.Enabled = true;
        }

        private void DownloadButton_Click(object s, EventArgs args)
        {
            if (!Directory.Exists("Extensions")) Directory.CreateDirectory("Extensions");
            if (!Directory.Exists(@"Extensions\Updates")) Directory.CreateDirectory(@"Extensions\Updates");

            string FileName = ExtensionsDataGrid["FileName", SelectedExtension].Value.ToString();

            if (File.Exists(@"Extensions\Updates\" + FileName))
            {
                while (File.Exists(@"Extensions\Updates\" + FileName) && Program.Updater.IsFileLocked(@"Extensions\Updates\" + FileName))
                {
                    if (MessageBox.Show("Please close any application using the file \"" + FileName + "\" in the folder \"Extensions\\Updates\" in order to continue.", "ModBot Updater", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning) == DialogResult.Cancel) return;
                }
                File.Delete(@"Extensions\Updates\" + FileName);
            }

            DownloadButton.Enabled = false;
            ExtensionsDataGrid.Enabled = false;
            BaseControls[0].Enabled = false;
            DownloadProgressBar.Text = "Initializing download...";

            Thread thread = new Thread(() =>
            {
                using (WebClient w = new WebClient())
                {
                    w.Proxy = null;
                    try
                    {
                        w.DownloadProgressChanged += new DownloadProgressChangedEventHandler((object sender, DownloadProgressChangedEventArgs e) =>
                        {
                            Program.Updater.Invoke(() =>
                            {
                                DownloadProgressBar.Value = int.Parse(Math.Truncate(double.Parse(e.BytesReceived.ToString()) / double.Parse(e.TotalBytesToReceive.ToString()) * 100).ToString());
                                DownloadProgressBar.Text = "Downloading... (PRECENTAGE)";
                            });
                        });
                        w.DownloadFileCompleted += new AsyncCompletedEventHandler((object sender, AsyncCompletedEventArgs e) =>
                        {
                            if (e.Error == null && !e.Cancelled)
                            {
                                Program.Updater.Invoke(() =>
                                {
                                    DownloadProgressBar.Text = "Successfully downloaded " + ExtensionsDataGrid["ExtensionName", SelectedExtension].Value.ToString() + " By " + ExtensionsDataGrid["Author", SelectedExtension].Value.ToString();
                                    DownloadProgressBar.Value = 0;
                                    DownloadButton.Enabled = true;
                                    ExtensionsDataGrid.Enabled = true;
                                    BaseControls[0].Enabled = true;
                                });
                            }
                            else
                            {
                                Program.Updater.Invoke(() =>
                                {
                                    DownloadProgressBar.Text = "Error while attempting to download extension!";
                                    DownloadProgressBar.Value = 0;
                                    DownloadButton.Enabled = true;
                                    ExtensionsDataGrid.Enabled = true;
                                    BaseControls[0].Enabled = true;
                                });
                            }
                        });
                        w.DownloadFileAsync(new Uri("https://dl.dropboxusercontent.com/u/60356733/ModBot/Extensions/" + ExtensionsDataGrid["UniqueID", SelectedExtension].Value.ToString() + "/" + ExtensionsDataGrid["Version", SelectedExtension].Value.ToString() + "/" + FileName), @"Extensions\Updates\" + FileName);
                    }
                    catch(Exception e)
                    {
                        Program.Updater.Invoke(() =>
                        {
                            DownloadProgressBar.Text = "Error while attempting to download extension!";
                            DownloadProgressBar.Value = 0;
                            DownloadButton.Enabled = true;
                            ExtensionsDataGrid.Enabled = true;
                            BaseControls[0].Enabled = true;
                        });
                    }
                }
            });
            thread.Start();
            thread.Join();
        }
    }
}
