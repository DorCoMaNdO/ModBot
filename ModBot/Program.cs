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
        public static iniUtil ini = new iniUtil(AppDomain.CurrentDomain.BaseDirectory + "ModBot.ini", "\r\n[Default]");
        //private static FileStream stream = null;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            EmbeddedAssembly.Load("ModBot.Resources.System.Data.SQLite.dll", "System.Data.SQLite.dll");
            EmbeddedAssembly.Load("ModBot.Resources.Newtonsoft.Json.dll", "Newtonsoft.Json.dll");
            EmbeddedAssembly.Load("ModBot.Resources.MySql.Data.dll", "MySql.Data.dll");

            AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
            {
                return EmbeddedAssembly.Get(args.Name);
            };

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            while (File.Exists("ModBot.ini") && Api.IsFileLocked("ModBot.ini", FileShare.Read))
            {
                MessageBox.Show("ModBot's config file is in use, Please close it in order to let ModBot use it.", "ModBot Updater", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (!File.Exists("ModBot.ini"))
            {
                File.WriteAllText("ModBot.ini", "\r\n[Default]");
            }
            //stream = new FileInfo("ModBot.ini").Open(FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
            Updates.ExtractUpdater();
            Updates.CheckUpdate();
            Application.Run(new SettingsDialog());
        }

        public static class Updates
        {
            public static bool CheckUpdate(bool bConsole=true, bool bMessageBox=true)
            {
                if (File.Exists("ModBotUpdater.exe"))
                {
                    string sLatestVersion = "", sCurrentVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                    using (WebClient w = new WebClient())
                    {
                        try
                        {
                            w.Proxy = null;
                            sLatestVersion = w.DownloadString("https://dl.dropboxusercontent.com/u/60356733/ModBot/ModBot.txt");
                            if (sLatestVersion != "")
                            {
                                string[] sCurrent = sCurrentVersion.Split('.');
                                string[] sLatest = sLatestVersion.Split('.');
                                int iCurrentMajor = Convert.ToInt32(sCurrent[0]), iCurrentMinor = Convert.ToInt32(sCurrent[1]), iCurrentBuild = Convert.ToInt32(sCurrent[2]), iCurrentRev = Convert.ToInt32(sCurrent[3]);
                                int iLatestMajor = Convert.ToInt32(sLatest[0]), iLatestMinor = Convert.ToInt32(sLatest[1]), iLatestBuild = Convert.ToInt32(sLatest[2]), iLatestRev = Convert.ToInt32(sLatest[3]);
                                if (iLatestMajor > iCurrentMajor || iLatestMajor == iCurrentMajor && iLatestMinor > iCurrentMinor || iLatestMajor == iCurrentMajor && iLatestMinor == iCurrentMinor && iLatestBuild > iCurrentBuild || iLatestMajor == iCurrentMajor && iLatestMinor == iCurrentMinor && iLatestBuild == iCurrentBuild && iLatestRev > iCurrentRev)
                                {
                                    if (bConsole)
                                    {
                                        Console.WriteLine("\r\n********************************************************************************\r\nAn update to ModBot is available, please use the updater to update!\r\n(Current version: " + sCurrentVersion + ", Latest version: " + sLatestVersion + ")\r\n\r\n********************************************************************************\r\n");
                                    }
                                    if (bMessageBox)
                                    {
                                        if (MessageBox.Show("An update to ModBot is available!\r\n(Current version: " + sCurrentVersion + ", Latest version: " + sLatestVersion + ")\r\nDo you want to update now?", "ModBot", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                                        {
                                            if (!File.Exists("ModBotUpdater.exe"))
                                            {
                                                ExtractUpdater();
                                            }
                                            Process.Start("ModBotUpdater.exe");
                                            Environment.Exit(0);
                                        }
                                    }
                                    return true;
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
                return false;
            }

            public static void ExtractUpdater()
            {
                byte[] rawUpdater = ModBot.Properties.Resources.ModBotUpdater;
                string sLatestVersion = Assembly.Load(rawUpdater).GetName().Version.ToString();
                if (File.Exists("ModBotUpdater.exe"))
                {
                    string sCurrentVersion = FileVersionInfo.GetVersionInfo("ModBotUpdater.exe").FileVersion.ToString();
                    string[] sCurrent = sCurrentVersion.Split('.');
                    string[] sLatest = sLatestVersion.Split('.');
                    int iCurrentMajor = Convert.ToInt32(sCurrent[0]), iCurrentMinor = Convert.ToInt32(sCurrent[1]), iCurrentBuild = Convert.ToInt32(sCurrent[2]), iCurrentRev = Convert.ToInt32(sCurrent[3]);
                    int iLatestMajor = Convert.ToInt32(sLatest[0]), iLatestMinor = Convert.ToInt32(sLatest[1]), iLatestBuild = Convert.ToInt32(sLatest[2]), iLatestRev = Convert.ToInt32(sLatest[3]);
                    if (iLatestMajor > iCurrentMajor || iLatestMajor == iCurrentMajor && iLatestMinor > iCurrentMinor || iLatestMajor == iCurrentMajor && iLatestMinor == iCurrentMinor && iLatestBuild > iCurrentBuild || iLatestMajor == iCurrentMajor && iLatestMinor == iCurrentMinor && iLatestBuild == iCurrentBuild && iLatestRev > iCurrentRev)
                    {
                        while (File.Exists("ModBotUpdater.exe") && Api.IsFileLocked("ModBotUpdater.exe"))
                        {
                            MessageBox.Show("Please close ModBot's Updater, a new version of the updater is available and will be extracted.", "ModBot", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        File.Delete("ModBotUpdater.exe");
                        while (File.Exists("ModBotUpdater.exe")) { }

                        using (FileStream fsUpdater = new FileStream("ModBotUpdater.exe", FileMode.CreateNew, FileAccess.Write))
                        {
                            fsUpdater.Write(rawUpdater, 0, rawUpdater.Length);
                        }
                        MessageBox.Show("ModBot Updater has been updated from v" + sCurrentVersion + " to v" + sLatestVersion + " sucessfully.", "ModBot", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    using (FileStream fsUpdater = new FileStream("ModBotUpdater.exe", FileMode.CreateNew, FileAccess.Write))
                    {
                        fsUpdater.Write(rawUpdater, 0, rawUpdater.Length);
                    }
                    MessageBox.Show("ModBot Updater has been extracted sucessfully (v" + sLatestVersion + ").", "ModBot", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
