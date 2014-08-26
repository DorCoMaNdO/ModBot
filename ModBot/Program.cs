using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public static iniUtil ini = new iniUtil(AppDomain.CurrentDomain.BaseDirectory + "ModBot.ini", "\r\n[Default]");
        public static MainWindow MainForm;
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

            //_handler += new EventHandler(Handler);
            //SetConsoleCtrlHandler(_handler, true);

            DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), 0xF060, 0x00000000);

            EmbeddedAssembly.Load("ModBot.Resources.System.Data.SQLite.dll", "System.Data.SQLite.dll");
            EmbeddedAssembly.Load("ModBot.Resources.Newtonsoft.Json.dll", "Newtonsoft.Json.dll");
            EmbeddedAssembly.Load("ModBot.Resources.MySql.Data.dll", "MySql.Data.dll");

            AppDomain.CurrentDomain.AssemblyResolve += (sender, e) =>
            {
                return EmbeddedAssembly.Get(e.Name);
            };

            /*string originalTitle = Console.Title;
            //string uniqueTitle = Guid.NewGuid().ToString();
            //Console.Title = uniqueTitle;
            //System.Threading.Thread.Sleep(50);
            handle = FindWindowByCaption(IntPtr.Zero, Console.Title = Guid.NewGuid().ToString());

            if (handle == IntPtr.Zero)
            {
                Console.WriteLine("Oops, cant find main window.");
                //return;
            }
            Console.Title = originalTitle;*/

            ServicePointManager.DefaultConnectionLimit = int.MaxValue;

            foreach (string arg in args) Program.args.Add(arg);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Console.CancelKeyPress += new ConsoleCancelEventHandler(Console_CancelKeyPress);
            /*while (File.Exists("ModBot.ini") && Api.IsFileLocked("ModBot.ini", FileShare.Read))
            {
                MessageBox.Show("ModBot's config file is in use, Please close it in order to let ModBot use it.", "ModBot Updater", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }*/
            if (!File.Exists("ModBot.ini"))
            {
                File.WriteAllText("ModBot.ini", "\r\n[Default]");
            }
            //stream = new FileInfo("ModBot.ini").Open(FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
            Updates.ExtractUpdater();
            Updates.CheckUpdate();
            Application.Run(MainForm = new MainWindow());
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
            if(MainForm.Misc_ShowConsole.Checked) SetForegroundWindow(GetConsoleWindow());
        }

        private static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            e.Cancel = true;
            if (MainForm == null) return;
            MainForm.BeginInvoke((MethodInvoker)delegate
            {
                MainForm.Close();
            });
        }

        public static class Updates
        {
            public static bool CheckUpdate(bool bConsole = true, bool bMessageBox = true)
            {
                if (File.Exists("ModBotUpdater.exe"))
                {
                    using (WebClient w = new WebClient())
                    {
                        w.Proxy = null;
                        try
                        {
                            string sCurrentVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString(), sLatestVersion = w.DownloadString("https://dl.dropboxusercontent.com/u/60356733/ModBot/ModBot.txt");
                            string sBetaVersion = sLatestVersion;

                            if (sLatestVersion != "")
                            {
                                bool Beta = false;
                                string[] sCurrent = sCurrentVersion.Split('.'), sLatest = sLatestVersion.Split('.');
                                if (ini.GetValue("Settings", "BetaUpdates", "0") == "1")
                                {
                                    sBetaVersion = w.DownloadString("https://dl.dropboxusercontent.com/u/60356733/ModBot/ModBotBeta.txt");
                                    if (sBetaVersion != "")
                                    {
                                        string[] sBeta = sBetaVersion.Split('.');
                                        if (TimeSpan.FromDays(int.Parse(sLatest[2])).Add(TimeSpan.FromSeconds(int.Parse(sLatest[3]))).CompareTo(TimeSpan.FromDays(int.Parse(sBeta[2])).Add(TimeSpan.FromSeconds(int.Parse(sBeta[3])))) == -1)
                                        {
                                            Beta = true;
                                            sLatestVersion = sBetaVersion;
                                            sLatest = sBeta;
                                        }
                                    }
                                }

                                if (TimeSpan.FromDays(int.Parse(sCurrent[2])).Add(TimeSpan.FromSeconds(int.Parse(sCurrent[3]))).CompareTo(TimeSpan.FromDays(int.Parse(sLatest[2])).Add(TimeSpan.FromSeconds(int.Parse(sLatest[3])))) == -1)
                                {
                                    bool Update = args.Contains("-autoupdate");

                                    if (bConsole)
                                    {
                                        Console.WriteLine("\r\n********************************************************************************\r\n" + (Beta ? "A beta" : "An") + " update to ModBot is available, please use the updater to update!\r\n(Current version: " + sCurrentVersion + ", Latest version: " + sLatestVersion + ")\r\n\r\n********************************************************************************\r\n");
                                    }

                                    if (bMessageBox)
                                    {
                                        if (MessageBox.Show((Beta ? "A beta" : "An") + " update to ModBot is available!\r\n(Current version: " + sCurrentVersion + ", Latest version: " + sLatestVersion + ")\r\nDo you want to update now?", "ModBot", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                                        {
                                            if (!File.Exists("ModBotUpdater.exe"))
                                            {
                                                ExtractUpdater();
                                            }
                                            Update = true;
                                        }
                                    }

                                    if(Update)
                                    {
                                        Process.Start("ModBotUpdater.exe", "-force -close -modbot" + (Irc.DetailsConfirmed ? " -modbotconnect" : "") + (args.Contains("-autoupdate") ? " -modbotupdate" : ""));
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
                    string[] sCurrent = sCurrentVersion.Split('.'), sLatest = sLatestVersion.Split('.');
                    if (TimeSpan.FromDays(int.Parse(sCurrent[2])).Add(TimeSpan.FromSeconds(int.Parse(sCurrent[3]))).CompareTo(TimeSpan.FromDays(int.Parse(sLatest[2])).Add(TimeSpan.FromSeconds(int.Parse(sLatest[3])))) == -1)
                    {
                        while (File.Exists("ModBotUpdater.exe") && Api.IsFileLocked("ModBotUpdater.exe"))
                        {
                            if (MessageBox.Show("Please close ModBot's Updater, a new version of the updater is available and will be extracted.", "ModBot", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information) == DialogResult.Cancel) Environment.Exit(0);
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
