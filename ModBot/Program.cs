using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Windows.Forms;

namespace ModBot
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SettingsDialog());
        }

        public static class Updates
        {
            public static void CheckUpdate()
            {
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "ModBotUpdater.exe"))
                {
                    string sLatestVersion = "", sCurrentVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                    using (WebClient w = new WebClient())
                    {
                        try
                        {
                            w.Proxy = null;
                            sLatestVersion = w.DownloadString("https://dl.dropboxusercontent.com/u/60356733/ModBot.txt");
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
                        catch (SocketException)
                        {
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
            }

            public static void ExtractUpdater()
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

            private static bool IsFileLocked(String FileLocation)
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
        }
    }
}
