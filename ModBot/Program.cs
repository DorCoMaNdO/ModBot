using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace ModBot
{
    static class Program
    {
        public static iniUtil ini = new iniUtil(AppDomain.CurrentDomain.BaseDirectory + @"Settings\", "ModBot.ini", "\r\n[Default]");
        public static MainWindow MainForm;
        public static ImageWindow LoadingScreen;
        public static List<string> args = new List<string>();
        //private static FileStream stream = null;

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll")]
        static extern bool DeleteMenu(IntPtr hMenu, uint uPosition, uint uFlags);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        static extern UInt32 GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

        /*[DllImport("kernel32.dll")]
        private static extern bool SetConsoleCtrlHandler(EventHandler handler, bool add);

        private delegate bool EventHandler(CtrlType sig);
        static EventHandler _handler;

        [DllImport("kernel32.dll")]
        static extern bool AllocConsole();

        [DllImport("kernel32.dll")]
        static extern bool FreeConsole();

        [DllImport("kernel32.dll")]
        static extern void ExitThread(int dwExitCode);

        [DllImport("kernel32.dll")]
        static extern bool AttachConsole(uint dwProcessId);

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);

        enum CtrlType
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT = 1,
            CTRL_CLOSE_EVENT = 2,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT = 6
        }

        private static bool Handler(CtrlType sig)
        {
            Console.WriteLine(sig);
            //Thread.Sleep(Timeout.Infinite);
            ExitThread(0);
            if (MessageBox.Show(MainForm.DisconnectButton.Enabled ? "ModBot is currently active! Are you sure you want to close it?" : "Are you sure you want to close ModBot?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                FreeConsole();
                AllocConsole();
                ExitThread(0);
                uint pid;
                GetWindowThreadProcessId(GetConsoleWindow(), out pid);
                AttachConsole(pid);
                Console.WriteLine("test");
            }
            return false;
        }*/

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            HideConsole();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            LoadingScreen = new ImageWindow(Properties.Resources.ModBotLoading, true, 11);

            LoadingScreen.Show();
            //new Changelog().Show();

            //_handler += new EventHandler(Handler);
            //SetConsoleCtrlHandler(_handler, true);

            DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), 0xF060, 0x00000000);

            LoadingScreen.AddProgress();

            EmbeddedAssembly.Load("ModBot.Resources.System.Data.SQLite.dll", "System.Data.SQLite.dll"); LoadingScreen.AddProgress();
            EmbeddedAssembly.Load("ModBot.Resources.Newtonsoft.Json.dll", "Newtonsoft.Json.dll"); LoadingScreen.AddProgress();
            EmbeddedAssembly.Load("ModBot.Resources.MySql.Data.dll", "MySql.Data.dll"); LoadingScreen.AddProgress();

            AppDomain.CurrentDomain.AssemblyResolve += (sender, e) =>
            {
                return EmbeddedAssembly.Get(e.Name);
            };

            LoadingScreen.AddProgress();

            ServicePointManager.DefaultConnectionLimit = int.MaxValue;

            foreach (string arg in args) Program.args.Add(arg);

            LoadingScreen.AddProgress();

            if (File.Exists("modbot.ini") || File.Exists("modbot.sqlite") || File.Exists("games.txt"))
            {
                if (MessageBox.Show("Files from older versions of ModBot have been detected, would you like to update them or delete them?\r\n\r\nYes to update.\r\nNo to delete.", "Old files detected", MessageBoxButtons.YesNo, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    if (File.Exists("modbot.ini"))
                    {
                        if (!Directory.Exists("Settings")) Directory.CreateDirectory("Settings");
                        if (File.Exists(@"Settings\ModBot.ini")) File.Delete(@"Settings\ModBot.ini");
                        File.Move("modbot.ini", @"Settings\ModBot.ini");
                    }

                    if (File.Exists("games.txt"))
                    {
                        if (!Directory.Exists("Settings")) Directory.CreateDirectory("Settings");
                        if (File.Exists(@"Settings\Games.txt")) File.Delete(@"Settings\Games.txt");
                        File.Move("games.txt", @"Settings\Games.txt");
                    }

                    if (File.Exists("modbot.sqlite"))
                    {
                        if (!Directory.Exists("Data")) Directory.CreateDirectory("Data");
                        if (!Directory.Exists(@"Data\Users")) Directory.CreateDirectory(@"Data\Users");
                        if (File.Exists(@"Data\Users\ModBot.sqlite")) File.Delete(@"Data\Users\ModBot.sqlite");
                        File.Move("modbot.sqlite", @"Data\Users\ModBot.sqlite");
                    }

                    while (File.Exists("modbot.ini") || File.Exists("modbot.sqlite") || File.Exists("games.txt")) Thread.Sleep(100);

                    Process.Start("ModBot.exe");

                    Environment.Exit(0);
                }
                else
                {
                    if (File.Exists("modbot.ini")) File.Delete("modbot.ini");
                    if (File.Exists("modbot.sqlite")) File.Delete("modbot.sqlite");
                    if (File.Exists("games.txt")) File.Delete("games.txt");
                }
            }

            LoadingScreen.AddProgress();

            Console.CancelKeyPress += new ConsoleCancelEventHandler(Console_CancelKeyPress);

            if (!Directory.Exists("Settings")) Directory.CreateDirectory("Settings");
            if (!Directory.Exists("Data")) Directory.CreateDirectory("Data");
            if (!Directory.Exists(@"Data\Logs")) Directory.CreateDirectory(@"Data\Logs");

            LoadingScreen.AddProgress();

            /*while (File.Exists(@"Settings\ModBot.ini") && Api.IsFileLocked(@"Settings\ModBot.ini", FileShare.Read))
            {
                MessageBox.Show("ModBot's config file is in use, Please close it in order to let ModBot use it.", "ModBot Updater", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }*/
            if (!File.Exists(@"Settings\ModBot.ini"))
            {
                File.WriteAllText(@"Settings\ModBot.ini", "\r\n[Default]");
            }

            LoadingScreen.AddProgress();
            //stream = new FileInfo(@"Settings\ModBot.ini").Open(FileMode.Open, FileAccess.ReadWrite, FileShare.Read);

            Updates.ExtractUpdater(); LoadingScreen.AddProgress();
            Updates.CheckUpdate(false, false, true); LoadingScreen.AddProgress();
            Application.Run(MainForm = new MainWindow());

            //SetWindowLong(MainForm.Handle, -20, GetWindowLong(MainForm.Handle, -20) | 0x80000);
            //SetLayeredWindowAttributes(MainForm.Handle, 0, 155, 0x2);
        }

        public static void Invoke(Delegate method)
        {
            Invoke(method, MainForm);
        }

        public static void Invoke(Delegate method, Form form)
        {
            if (form.IsHandleCreated)
            {
                form.BeginInvoke(method);
            }
            else
            {
                form.Invoke(method);
            }
        }

        public static void HideConsole()
        {
            ShowWindow(GetConsoleWindow(), 0);
        }

        public static void ShowConsole()
        {
            ShowWindow(GetConsoleWindow(), 1);
        }

        public static void FocusConsole()
        {
            if (MainForm.Misc_ShowConsole.Checked) SetForegroundWindow(GetConsoleWindow());
        }

        private static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            e.Cancel = true;
            if (MainForm == null) return;
            Program.Invoke((MethodInvoker)delegate
            {
                MainForm.Close();
            });
        }

        public static class Updates
        {
            public static bool CheckUpdate(bool bConsole = true, bool bMessageBox = true, bool ForceUpdate = false, bool NotifyForce = true)
            {
                using (WebClient w = new WebClient())
                {
                    w.Proxy = null;
                    try
                    {
                        string sCurrentVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString(), sLatestVersion = w.DownloadString("https://dl.dropboxusercontent.com/u/60356733/ModBot/ModBot.txt");

                        if (sLatestVersion != "")
                        {
                            int UpdateType = 0;
                            string[] sCurrent = sCurrentVersion.Split('.'), sLatest = sLatestVersion.Split('.');
                            if (ini.GetValue("Settings", "BetaUpdates", "0") == "1")
                            {
                                string sBetaVersion = w.DownloadString("https://dl.dropboxusercontent.com/u/60356733/ModBot/ModBotBeta.txt");
                                if (sBetaVersion != "")
                                {
                                    string[] sBeta = sBetaVersion.Split('.');
                                    if (TimeSpan.FromDays(int.Parse(sLatest[2])).Add(TimeSpan.FromSeconds(int.Parse(sLatest[3]))).CompareTo(TimeSpan.FromDays(int.Parse(sBeta[2])).Add(TimeSpan.FromSeconds(int.Parse(sBeta[3])))) == -1)
                                    {
                                        UpdateType = 1;
                                        sLatestVersion = sBetaVersion;
                                        sLatest = sBeta;
                                    }
                                }
                            }
                            if (ini.GetValue("Settings", "DevUpdates", "0") == "1")
                            {
                                string sDevVersion = w.DownloadString("https://dl.dropboxusercontent.com/u/60356733/ModBot/ModBotDev.txt");
                                if (sDevVersion != "")
                                {
                                    string[] sDev = sDevVersion.Split('.');
                                    if (TimeSpan.FromDays(int.Parse(sLatest[2])).Add(TimeSpan.FromSeconds(int.Parse(sLatest[3]))).CompareTo(TimeSpan.FromDays(int.Parse(sDev[2])).Add(TimeSpan.FromSeconds(int.Parse(sDev[3])))) == -1)
                                    {
                                        UpdateType = 2;
                                        sLatestVersion = sDevVersion;
                                        sLatest = sDev;
                                    }
                                }
                            }

                            if (TimeSpan.FromDays(int.Parse(sCurrent[2])).Add(TimeSpan.FromSeconds(int.Parse(sCurrent[3]))).CompareTo(TimeSpan.FromDays(int.Parse(sLatest[2])).Add(TimeSpan.FromSeconds(int.Parse(sLatest[3])))) == -1)
                            {
                                bool Update = (args.Contains("-autoupdate") || ForceUpdate);

                                if (!Update)
                                {
                                    if (bConsole) Console.WriteLine("\r\n********************************************************************************\r\n" + (UpdateType == 1 ? "A beta" : UpdateType == 2 ? "A development" : "An") + " update to ModBot is available, please use the updater to update!\r\n(Current version: " + sCurrentVersion + ", Latest version: " + sLatestVersion + ")\r\n\r\n********************************************************************************\r\n");

                                    if (bMessageBox) Update = (MessageBox.Show((UpdateType == 1 ? "A beta" : UpdateType == 2 ? "A development" : "An") + " update to ModBot is available!\r\n(Current version: " + sCurrentVersion + ", Latest version: " + sLatestVersion + ")\r\nDo you want to update now?", "ModBot", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.Yes);

                                }

                                if (ForceUpdate && NotifyForce) if (MessageBox.Show((UpdateType == 1 ? "A beta" : UpdateType == 2 ? "A development" : "An") + " update to ModBot is available!\r\nIn order to use ModBot you have to install the latest version!\r\n(Current version: " + sCurrentVersion + ", Latest version: " + sLatestVersion + ")\r\nClick OK to update now or Cancel to close the application.", "ModBot", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.Cancel) Environment.Exit(0);

                                if (Update)
                                {
                                    while (!File.Exists("ModBotUpdater.exe")) ExtractUpdater(false);
                                    Process.Start("ModBotUpdater.exe", "-force -bg -close -modbot" + (Irc.DetailsConfirmed ? " -modbotconnect" : "") + (args.Contains("-autoupdate") ? " -modbotupdate" : ""));
                                    Environment.Exit(0);
                                }

                                return true;
                            }
                        }
                    }
                    catch
                    {
                    }
                }
                return false;
            }

            public static void ExtractUpdater(bool Announce = true)
            {
                byte[] rawUpdater = ModBot.Properties.Resources.ModBotUpdater;
                string sLatestVersion = Assembly.Load(rawUpdater).GetName().Version.ToString();
                if (File.Exists("ModBotUpdater.exe"))
                {
                    string sCurrentVersion = FileVersionInfo.GetVersionInfo("ModBotUpdater.exe").FileVersion.ToString();
                    string[] sCurrent = sCurrentVersion.Split('.'), sLatest = sLatestVersion.Split('.');
                    if (TimeSpan.FromDays(int.Parse(sCurrent[2])).Add(TimeSpan.FromSeconds(int.Parse(sCurrent[3]))).CompareTo(TimeSpan.FromDays(int.Parse(sLatest[2])).Add(TimeSpan.FromSeconds(int.Parse(sLatest[3])))) == -1)
                    {
                        while (File.Exists("ModBotUpdater.exe") && Api.IsFileLocked("ModBotUpdater.exe")) if (MessageBox.Show("Please close ModBot's Updater, a new version of the updater is available and will be extracted.", "ModBot", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information) == DialogResult.Cancel) Environment.Exit(0);

                        File.Delete("ModBotUpdater.exe");
                        while (File.Exists("ModBotUpdater.exe")) { }

                        using (FileStream fsUpdater = new FileStream("ModBotUpdater.exe", FileMode.CreateNew, FileAccess.Write))
                        {
                            fsUpdater.Write(rawUpdater, 0, rawUpdater.Length);
                        }

                        if (Announce) MessageBox.Show("ModBot Updater has been updated from v" + sCurrentVersion + " to v" + sLatestVersion + " sucessfully.", "ModBot", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    using (FileStream fsUpdater = new FileStream("ModBotUpdater.exe", FileMode.CreateNew, FileAccess.Write))
                    {
                        fsUpdater.Write(rawUpdater, 0, rawUpdater.Length);
                    }

                    if (Announce) MessageBox.Show("ModBot Updater has been extracted sucessfully (v" + sLatestVersion + ").", "ModBot", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            public static void WelcomeMsg(Form form = null)
            {
                if (ini.GetValue("Settings", "Notification_Welcome", "0") != "0") return;

                if (form == null) form = MainForm;

                string Data = "";

                new Thread(() =>
                {
                    using (WebClient w = new WebClient())
                    {
                        w.Proxy = null;
                        try
                        {
                            Data = w.DownloadString("https://dl.dropboxusercontent.com/u/60356733/ModBot/WelcomeMsg.txt");
                            ini.SetValue("Settings", "Notification_Welcome", "1");
                        }
                        catch
                        {
                        }
                    }

                    Invoke((MethodInvoker)delegate
                    {
                        if (Data != "") MessageBox.Show(Data, "Welcome");
                    }, form);
                }).Start();
            }

            public static void MsgOfTheDay(Form form = null)
            {
                if (form == null) form = MainForm;

                string Data = "";

                new Thread(() =>
                {
                    using (WebClient w = new WebClient())
                    {
                        w.Proxy = null;
                        try
                        {
                            Data = w.DownloadString("https://dl.dropboxusercontent.com/u/60356733/ModBot/MsgOfTheDay.txt");
                        }
                        catch
                        {
                        }
                    }
                
                    Invoke((MethodInvoker)delegate
                    {
                        if (Data != "") MessageBox.Show(Data, "Message of the day");
                    }, form);
                }).Start();
            }

            public static void WhatsNew(Form form = null)
            {
                if (ini.GetValue("Settings", "Notification_WhatsNew", "") == Assembly.GetExecutingAssembly().GetName().Version.ToString()) return;

                if (form == null) form = MainForm;

                string Data = "";

                new Thread(() =>
                {
                    using (WebClient w = new WebClient())
                    {
                        w.Proxy = null;
                        try
                        {
                            Data += "[" + Assembly.GetExecutingAssembly().GetName().Version.Major + "." + Assembly.GetExecutingAssembly().GetName().Version.Minor + "]\r\n" + w.DownloadString("https://dl.dropboxusercontent.com/u/60356733/ModBot/" + Assembly.GetExecutingAssembly().GetName().Version.Major + "." + Assembly.GetExecutingAssembly().GetName().Version.Minor + ".txt");
                        }
                        catch
                        {
                        }

                        try
                        {
                            //Data += (Data != "" ? "\r\n\r\n[" : "[") + Assembly.GetExecutingAssembly().GetName().Version + "]\r\n" + w.DownloadString("https://dl.dropboxusercontent.com/u/60356733/ModBot/" + Assembly.GetExecutingAssembly().GetName().Version + "/WhatsNew.txt");
                            Data += (Data != "" ? "\r\n\r\n[" : "[") + Assembly.GetExecutingAssembly().GetName().Version + "]\r\n" + w.DownloadString("https://dl.dropboxusercontent.com/u/60356733/ModBot/" + Assembly.GetExecutingAssembly().GetName().Version + ".txt");
                        }
                        catch
                        {
                        }
                    }

                    Invoke((MethodInvoker)delegate
                    {
                        if (Data != "")
                        {
                            MessageBox.Show(Data, "What's New in version " + Assembly.GetExecutingAssembly().GetName().Version);
                            ini.SetValue("Settings", "Notification_WhatsNew", Assembly.GetExecutingAssembly().GetName().Version.ToString());
                        }
                    }, form);
                }).Start();
            }
        }
    }
}