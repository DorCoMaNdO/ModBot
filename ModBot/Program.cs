using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
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
    public delegate void OnUILoaded();

    static class Program
    {
        public static Extensions extensions;
        public static int ApiVersion = 1;

        public static iniUtil ini = new iniUtil(AppDomain.CurrentDomain.BaseDirectory + @"Data\Settings\", "ModBot.ini", "\r\n[Default]");
        public static MainWindow MainForm;
        public static ImageWindow LoadingScreen;
        public static List<string> args = new List<string>();
        public static Dictionary<Control, string> Windows = new Dictionary<Control, string>();
        //private static FileStream stream = null;

        public static event OnUILoaded OnUILoaded = (() => { });

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

        /*[DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        static extern UInt32 GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);*/

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

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteFile(string name);

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            LoadingScreen = new ImageWindow(Properties.Resources.ModBotLoading, true, 16);

            LoadingScreen.Show();

            //new Changelog().Show();

            //_handler += new EventHandler(Handler);
            //SetConsoleCtrlHandler(_handler, true);

            DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), 0xF060, 0x00000000); LoadingScreen.AddProgress();

            Console.CancelKeyPress += new ConsoleCancelEventHandler(Console_CancelKeyPress); LoadingScreen.AddProgress();

            if (!Directory.Exists("Data")) Directory.CreateDirectory("Data");
            if (!Directory.Exists(@"Data\Settings")) Directory.CreateDirectory(@"Data\Settings");
            if (!Directory.Exists(@"Data\Logs")) Directory.CreateDirectory(@"Data\Logs");
            if (!Directory.Exists(@"Data\Extensions")) Directory.CreateDirectory(@"Data\Extensions");
            if (!Directory.Exists("Extensions")) Directory.CreateDirectory("Extensions");
            if (!Directory.Exists(@"Extensions\Disabled")) Directory.CreateDirectory(@"Extensions\Disabled");
            if (!Directory.Exists(@"Extensions\Updates")) Directory.CreateDirectory(@"Extensions\Updates");

            LoadingScreen.AddProgress();

            /*while (File.Exists(@"Data\Settings\ModBot.ini") && Api.IsFileLocked(@"Data\Settings\ModBot.ini", FileShare.Read))
            {
                MessageBox.Show("ModBot's config file is in use, Please close it in order to let ModBot use it.", "ModBot", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }*/
            if (!File.Exists(@"Data\Settings\ModBot.ini")) File.WriteAllText(@"Data\Settings\ModBot.ini", "\r\n[Default]"); LoadingScreen.AddProgress();

            //WebCore.Initialize(new WebConfig() { LogLevel = LogLevel.None }); LoadingScreen.AddProgress();

            foreach (string arg in args) Program.args.Add(arg); LoadingScreen.AddProgress();

            extensions = new Extensions(); LoadingScreen.AddProgress();

            /*new Thread(() =>
            {
                if (extensions.exts.Count > 0) Thread.Sleep(5000);
                if (ini.GetValue("Settings", "Misc_ShowConsole", "1") == "0") HideConsole();
            }).Start();*/

            if (ini.GetValue("Settings", "Misc_ShowConsole", "1") == "0")
            {
                Program.LoadingScreen.TopMost = false;
                if (extensions.exts.Count > 0) MessageBox.Show("Since you're using extensions, you might want to check the console as it will contain information about updates or plugins that couldn't load.", "ModBot Extensions", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Program.LoadingScreen.TopMost = true;
                HideConsole();
            }

            LoadingScreen.AddProgress();

            EmbeddedAssembly.Load("ModBot.System.Data.SQLite.dll", "System.Data.SQLite.dll"); LoadingScreen.AddProgress();
            EmbeddedAssembly.Load("ModBot.Newtonsoft.Json.dll", "Newtonsoft.Json.dll"); LoadingScreen.AddProgress();
            EmbeddedAssembly.Load("ModBot.MySql.Data.dll", "MySql.Data.dll"); LoadingScreen.AddProgress();

            AppDomain.CurrentDomain.AssemblyResolve += (sender, e) =>
            {
                return EmbeddedAssembly.Get(e.Name);
            };

            LoadingScreen.AddProgress();

            ServicePointManager.DefaultConnectionLimit = int.MaxValue; LoadingScreen.AddProgress();

            if (File.Exists("modbot.ini") || File.Exists("modbot.sqlite") || File.Exists("games.txt") || Directory.Exists("Settings") && (File.Exists(@"Settings\modbot.ini") || File.Exists(@"Settings\games.txt")))
            {
                Program.LoadingScreen.TopMost = false;
                if (MessageBox.Show("Files from older versions of ModBot have been detected, would you like to update them or delete them?\r\n\r\nYes to update.\r\nNo to delete.", "Old files detected", MessageBoxButtons.YesNo, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    if (File.Exists("modbot.ini"))
                    {
                        if (!Directory.Exists("Data")) Directory.CreateDirectory("Data");
                        if (!Directory.Exists(@"Data\Settings")) Directory.CreateDirectory(@"Data\Settings");
                        if (File.Exists(@"Data\Settings\ModBot.ini")) File.Delete(@"Data\Settings\ModBot.ini");
                        File.Move("modbot.ini", @"Data\Settings\ModBot.ini");
                    }

                    if (File.Exists("games.txt"))
                    {
                        if (!Directory.Exists("Data")) Directory.CreateDirectory("Data");
                        if (!Directory.Exists(@"Data\Settings")) Directory.CreateDirectory(@"Data\Settings");
                        if (File.Exists(@"Data\Games.txt")) File.Delete(@"Data\Games.txt");
                        File.Move("games.txt", @"Data\Games.txt");
                    }

                    if (File.Exists("modbot.sqlite"))
                    {
                        if (!Directory.Exists("Data")) Directory.CreateDirectory("Data");
                        if (!Directory.Exists(@"Data\Users")) Directory.CreateDirectory(@"Data\Users");
                        if (File.Exists(@"Data\Users\ModBot.sqlite")) File.Delete(@"Data\Users\ModBot.sqlite");
                        File.Move("modbot.sqlite", @"Data\Users\ModBot.sqlite");
                    }

                    if (Directory.Exists("Settings"))
                    {
                        if (File.Exists(@"Settings\modbot.ini"))
                        {
                            if (!Directory.Exists("Data")) Directory.CreateDirectory("Data");
                            if (!Directory.Exists(@"Data\Settings")) Directory.CreateDirectory(@"Data\Settings");
                            if (File.Exists(@"Data\Settings\ModBot.ini")) File.Delete(@"Data\Settings\ModBot.ini");
                            File.Move(@"Settings\modbot.ini", @"Data\Settings\ModBot.ini");
                        }

                        if (File.Exists(@"Settings\games.txt"))
                        {
                            if (!Directory.Exists("Data")) Directory.CreateDirectory("Data");
                            if (!Directory.Exists(@"Data\Settings")) Directory.CreateDirectory(@"Data\Settings");
                            if (File.Exists(@"Data\Games.txt")) File.Delete(@"Data\Games.txt");
                            File.Move(@"Settings\games.txt", @"Data\Games.txt");
                        }
                    }

                    while (File.Exists("modbot.ini") || File.Exists("modbot.sqlite") || File.Exists("games.txt") || Directory.Exists("Settings") && (File.Exists(@"Settings\modbot.ini") || File.Exists(@"Settings\games.txt"))) Thread.Sleep(100);

                    if (Directory.Exists("Settings")) Directory.Delete("Settings", true);

                    Process.Start("ModBot.exe");

                    Close();
                }
                else
                {
                    if (File.Exists("modbot.ini")) File.Delete("modbot.ini");
                    if (File.Exists("modbot.sqlite")) File.Delete("modbot.sqlite");
                    if (File.Exists("games.txt")) File.Delete("games.txt");

                    /*if (Directory.Exists("Settings"))
                    {
                        if (File.Exists(@"Settings\modbot.ini")) File.Delete(@"Settings\modbot.ini");
                        if (File.Exists(@"Settings\games.txt")) File.Delete(@"Settings\games.txt");
                        Directory.Delete("Settings", true);
                    }*/
                    if (Directory.Exists("Settings")) Directory.Delete("Settings", true);
                }
                Program.LoadingScreen.TopMost = true;
            }

            LoadingScreen.AddProgress();

            //stream = new FileInfo(@"Data\Settings\ModBot.ini").Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);

            Program.LoadingScreen.TopMost = false;
            Updates.ExtractUpdater(); LoadingScreen.AddProgress();
            Updates.CheckUpdate(false, false, true); LoadingScreen.AddProgress();
            Program.LoadingScreen.TopMost = true;
            Application.Run(MainForm = new MainWindow());

            //SetWindowLong(MainForm.Handle, -20, GetWindowLong(MainForm.Handle, -20) | 0x80000);
            //SetLayeredWindowAttributes(MainForm.Handle, 0, 155, 0x2);
        }

        /*public static void Invoke(Delegate method)
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
                //form.Invoke(method);
                method.DynamicInvoke(null);
            }
        }*/

        public static void Invoke(Action method)
        {
            Invoke(method, MainForm);
        }

        public static void Invoke(Action method, Control ctrl)
        {
            if (ctrl.IsHandleCreated)
            {
                ctrl.BeginInvoke(method);
            }
            else
            {
                method();
            }
        }

        public static void MainFormLoaded()
        {
            OnUILoaded();
        }

        public static void AddToMainWindow(string name, Form form, bool RequiresConnection = true, bool RequiresMod = false, bool RequiresPartership = false, bool ControlManually = false, string AlternativeName = "", CheckBox Button = null, Font Font = null)
        {
            if (name == "" || name == null || form == null) return;
            
            Invoke(() =>
            {
                form.TopLevel = false;
                form.Location = new Point(108, 30);
                form.Size = new Size(1024, 562);
                form.FormBorderStyle = FormBorderStyle.None;
                form.BackColor = Color.White;
                /*if(form.GetType() == typeof(CustomForm))
                {
                    CustomForm cform = (CustomForm)form;
                    cform.CustomFormBorderStyle = CustomForm.FormBorderStyles.None;
                    cform.FixBorders();
                }*/
                form.Show();
            }, form);

            AddToMainWindow(form, name, RequiresConnection, RequiresMod, RequiresPartership, ControlManually, AlternativeName, Button, Font);
        }

        public static void AddToMainWindow(Control ctrl, string name, bool RequiresConnection = true, bool RequiresMod = false, bool RequiresPartership = false, bool ControlManually = false, string AlternativeName = "", CheckBox Button = null, Font Font = null)
        {
            if (name == "" || name == null || ctrl == null) return;

            if (MainForm == null)
            {
                if(!Windows.ContainsKey(ctrl)) Windows.Add(ctrl, name);
                return;
            }

            /*Invoke(() =>
            {
                ctrl.Visible = true;
                ctrl.Enabled = true;
            }, ctrl);*/
            
            Invoke(() =>
            {
                MainForm.Controls.Add(ctrl);
                while (ctrl.Location.X != 108 || ctrl.Location.Y != 30 || ctrl.Size.Width != 1024 || ctrl.Size.Height != 562)
                {
                    ctrl.Location = new Point(108, 30);
                    ctrl.Size = new Size(1024, 562);
                    Thread.Sleep(5);
                }

                Invoke(() =>
                {
                    if (MainForm.Windows.ContainsControl(MainForm.AboutWindow)) MainForm.Windows.RemoveWindow(MainForm.AboutWindow);
                    MainForm.Windows.Add(new Window(name, ctrl, RequiresConnection, RequiresMod, RequiresPartership, ControlManually, AlternativeName, Button, Font));
                    MainForm.Windows.Add(new Window("About", MainForm.AboutWindow, false));

                    int count = MainForm.Windows.Count; // Amount of buttons that will be in view.
                    int h = MainForm.Height - 38; // Height that can be used.
                    int minsize = 84; // Min size of each button.
                    while (h / count < minsize) count--;
                    if (count < MainForm.Windows.Count)
                    {
                        h -= 24;
                        while (h / count < minsize) count--;
                    }

                    foreach (CheckBox btn in MainForm.Windows.Buttons)
                    {
                        btn.Size = new Size(100, h / count);
                    }

                    int y = -(h / count * count - MainForm.Height + 38 + count < MainForm.Windows.Count ? 24 : 0);
                    while (y > 0)
                    {
                        int c = 0;
                        foreach (CheckBox btn in MainForm.Windows.Buttons)
                        {
                            c++;
                            if (c > count || y == 0) break;
                            btn.Size = new Size(btn.Size.Width, btn.Size.Height + 1);
                            y--;
                        }
                    }

                    y = 0;
                    int btnc = 0;
                    foreach (CheckBox btn in MainForm.Windows.Buttons)
                    {
                        btnc++;
                        if (btnc > count) break;
                        y += btn.Size.Height;
                    }
                    while (y < h)
                    {
                        btnc = 0;
                        foreach (CheckBox btn in MainForm.Windows.Buttons)
                        {
                            btnc++;
                            if (btnc > count || y >= h) break;
                            btn.Size = new Size(btn.Size.Width, btn.Size.Height + 1);
                            y++;
                        }
                    }

                    int currenty = 30;
                    foreach (CheckBox btn in MainForm.Windows.Buttons)
                    {
                        btn.Location = new Point(8, currenty);
                        currenty += btn.Size.Height;
                    }

                    MainForm.WindowsScroll.Visible = (count < MainForm.Windows.Count);
                    MainForm.WindowsScroll.Maximum = MainForm.Windows.Count - count + 9;
                });
            }, ctrl);
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

        public static bool IsConsoleHidden()
        {
            bool hidden = false;
            Invoke(() =>
            {
                hidden = MainForm.Misc_ShowConsole.Checked;
            });
            return !hidden;
        }

        private static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            e.Cancel = true;
            if (MainForm == null) return;
            Program.Invoke(() =>
            {
                MainForm.Close();
            });
        }

        public static void LegalNotice(Form form = null)
        {
            if (form == null) form = MainForm;

            Invoke(() =>
            {
                MessageBox.Show("This program is distributed under the Simplified BSD License.\r\n\r\nCopyright (c) 2013, Jonathan Smith (Keirathi)\r\nAll rights reserved.\r\n\r\nRedistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:\r\n\r\nRedistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.\r\nRedistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.\r\n\r\nTHIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS \"AS IS\" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.", "Legal");
            }, form);
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

                        int UpdateType = 0;
                        if (ini.GetValue("Settings", "BetaUpdates", "0") == "1")
                        {
                            string sBetaVersion = w.DownloadString("https://dl.dropboxusercontent.com/u/60356733/ModBot/ModBotBeta.txt");
                            if (sBetaVersion != "")
                            {
                                if (sLatestVersion != "")
                                {
                                    string[] sLatest = sLatestVersion.Split('.'), sBeta = sBetaVersion.Split('.');
                                    if (TimeSpan.FromDays(int.Parse(sLatest[2])).Add(TimeSpan.FromSeconds(int.Parse(sLatest[3]))).CompareTo(TimeSpan.FromDays(int.Parse(sBeta[2])).Add(TimeSpan.FromSeconds(int.Parse(sBeta[3])))) == -1)
                                    {
                                        UpdateType = 1;
                                        sLatestVersion = sBetaVersion;
                                    }
                                }
                                else
                                {
                                    sLatestVersion = sBetaVersion;
                                }
                            }
                        }
                        if (ini.GetValue("Settings", "DevUpdates", "0") == "1")
                        {
                            string sDevVersion = w.DownloadString("https://dl.dropboxusercontent.com/u/60356733/ModBot/ModBotDev.txt");
                            if (sDevVersion != "")
                            {
                                if (sLatestVersion != "")
                                {
                                    string[] sLatest = sLatestVersion.Split('.'), sDev = sDevVersion.Split('.');
                                    if (TimeSpan.FromDays(int.Parse(sLatest[2])).Add(TimeSpan.FromSeconds(int.Parse(sLatest[3]))).CompareTo(TimeSpan.FromDays(int.Parse(sDev[2])).Add(TimeSpan.FromSeconds(int.Parse(sDev[3])))) == -1)
                                    {
                                        UpdateType = 2;
                                        sLatestVersion = sDevVersion;
                                    }
                                }
                                else
                                {
                                    sLatestVersion = sDevVersion;
                                }
                            }
                        }

                        if (sLatestVersion != "")
                        {
                            string[] sCurrent = sCurrentVersion.Split('.'), sLatest = sLatestVersion.Split('.');

                            if (TimeSpan.FromDays(int.Parse(sCurrent[2])).Add(TimeSpan.FromSeconds(int.Parse(sCurrent[3]))).CompareTo(TimeSpan.FromDays(int.Parse(sLatest[2])).Add(TimeSpan.FromSeconds(int.Parse(sLatest[3])))) == -1)
                            {
                                bool Update = (args.Contains("-autoupdate") || ForceUpdate);

                                if (!Update)
                                {
                                    if (bConsole) Console.WriteLine("\r\n********************************************************************************\r\n" + (UpdateType == 1 ? "A beta" : UpdateType == 2 ? "A development" : "An") + " update to ModBot is available, please use the updater to update!\r\n(Current version: " + sCurrentVersion + ", Latest version: " + sLatestVersion + ")\r\n\r\n********************************************************************************\r\n");

                                    if (bMessageBox) Update = (MessageBox.Show((UpdateType == 1 ? "A beta" : UpdateType == 2 ? "A development" : "An") + " update to ModBot is available!\r\n(Current version: " + sCurrentVersion + ", Latest version: " + sLatestVersion + ")\r\nDo you want to update now?", "ModBot", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.Yes);

                                }

                                if (ForceUpdate && NotifyForce) if (MessageBox.Show((UpdateType == 1 ? "A beta" : UpdateType == 2 ? "A development" : "An") + " update to ModBot is available!\r\nIn order to use ModBot you have to install the latest version!\r\n(Current version: " + sCurrentVersion + ", Latest version: " + sLatestVersion + ")\r\nClick OK to update now or Cancel to close the application.", "ModBot", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.Cancel) Close();

                                if (Update)
                                {
                                    while (!File.Exists("ModBotUpdater.exe")) ExtractUpdater(false);
                                    Process.Start("ModBotUpdater.exe", "-force -bg -close -modbot" + (Irc.DetailsConfirmed ? " -modbotconnect" : "") + (args.Contains("-autoupdate") ? " -modbotupdate" : ""));
                                    Close();
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
                        while (File.Exists("ModBotUpdater.exe") && Api.IsFileLocked("ModBotUpdater.exe")) if (MessageBox.Show("Please close ModBot's Updater, a new version of the updater is available and will be extracted.", "ModBot", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information) == DialogResult.Cancel) Close();

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

                Thread thread = new Thread(() =>
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
                });
                thread.Start();
                thread.Join();

                /*Invoke(() =>
                {
                    if (Data != "") MessageBox.Show(Data, "Welcome");
                    LegalNotice();
                }, form);*/
                if (Data != "") MessageBox.Show(Data, "Welcome");
                LegalNotice();
            }

            public static void MsgOfTheDay(Form form = null)
            {
                if (form == null) form = MainForm;

                string Data = "";

                Thread thread = new Thread(() =>
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
                });
                thread.Start();
                thread.Join();

                /*Invoke(() =>
                {
                    if (Data != "") MessageBox.Show(Data, "Message of the day");
                }, form);*/
                if (Data != "") MessageBox.Show(Data, "Message of the day");
            }

            public static void WhatsNew(Form form = null)
            {
                if (ini.GetValue("Settings", "Notification_WhatsNew", "") == Assembly.GetExecutingAssembly().GetName().Version.ToString()) return;

                if (form == null) form = MainForm;

                string Data = "";

                Thread thread = new Thread(() =>
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
                });
                thread.Start();
                thread.Join();

                /*Invoke(() =>
                {
                    if (Data != "")
                    {
                        MessageBox.Show(Data, "What's New in version " + Assembly.GetExecutingAssembly().GetName().Version);
                        ini.SetValue("Settings", "Notification_WhatsNew", Assembly.GetExecutingAssembly().GetName().Version.ToString());
                    }
                }, form);*/
                if (Data != "")
                {
                    MessageBox.Show(Data, "What's New in version " + Assembly.GetExecutingAssembly().GetName().Version);
                    ini.SetValue("Settings", "Notification_WhatsNew", Assembly.GetExecutingAssembly().GetName().Version.ToString());
                }
            }
        }

        public static void Close()
        {
            /*if (MainForm != null)
            {
                MainForm.AuthenticationBrowser.Dispose();
                Thread.Sleep(50);
                while (!MainForm.AuthenticationBrowser.IsDisposed) Thread.Sleep(50);

                foreach(Control ctrl in MainForm.Windows.Values)
                {
                    ctrl.Dispose();
                    Thread.Sleep(10);
                    while (!MainForm.AuthenticationBrowser.IsDisposed) Thread.Sleep(10);
                }
            }*/
            Environment.Exit(0);
        }
    }

    public interface IExtension
    {
        void Load();

        string Name { get; }
        string FileName { get; }
        string Author { get; }
        string UniqueID { get; }
        string Version { get; }
        //string MinVersion { get; }
        int ApiVersion { get; }

        bool UpdateCheck();
        string UpdateURL { get; }
    }

    class Extensions
    {
#pragma warning disable 0649
        [ImportMany]
        public List<IExtension> exts;
#pragma warning restore 0649
        public List<IExtension> extensions = new List<IExtension>(), updated = new List<IExtension>();
        public int fails;

        public Extensions()
        {
            if(Program.args.Contains("-noextensions"))
            {
                exts = new List<IExtension>();
                return;
            }

            if (Directory.Exists(@"Extensions\Updates"))
            {
                List<string> files = new List<string>();
                foreach (string file in Directory.GetFiles(@"Extensions\Updates")) files.Add(file.Substring(19));
                if (files.Count > 0) Console.WriteLine("Applying updates to extensions...\r\n ==============================================================================\r\n");
                int updated = 0;

                foreach (string file in files)
                {
                    try
                    {
                        FileVersionInfo.GetVersionInfo(@"Extensions\Updates\" + file);

                        if (File.Exists(@"Extensions\" + file)) File.Delete(@"Extensions\" + file);
                        File.Move(@"Extensions\Updates\" + file, @"Extensions\" + file);
                        Console.WriteLine(" Updated \"" + file + "\".");
                        updated++;
                    }
                    catch (NullReferenceException)
                    {
                        File.Delete(@"Extensions\Updates\" + file);
                        Console.WriteLine(" \"" + file + "\" has been found corrupt.");
                    }
                }

                if (files.Count > 0) Console.WriteLine("\r\n ==============================================================================\r\nApplied updates to " + updated + " extensions.\r\n");
            }

            AggregateCatalog catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(Program).Assembly));
            foreach (string file in Directory.GetFiles("Extensions", "*.dll")) Program.DeleteFile(file + ":Zone.Identifier");
            foreach (string file in Directory.GetFiles("Extensions", "*.ext")) Program.DeleteFile(file + ":Zone.Identifier");
            catalog.Catalogs.Add(new DirectoryCatalog("Extensions", "*.dll"));
            catalog.Catalogs.Add(new DirectoryCatalog("Extensions", "*.ext"));
            //foreach (string dir in Directory.GetDirectories(@"Extensions")) if (dir.Substring(11) != "Disabled" && dir.Substring(11) != "Updates") catalog.Catalogs.Add(new DirectoryCatalog(dir, "*.dll"));
            //AppDomain domain = AppDomain.CreateDomain("ExampleExtension (3)");
            //AssemblyName asm = new AssemblyName();
            //asm.CodeBase = AppDomain.CurrentDomain.BaseDirectory + @"\Extensions\ExampleExtension (3).dll";
            //catalog.Catalogs.Add(new AssemblyCatalog(domain.Load(asm)));
            ////Assembly asm = Assembly.LoadFile(AppDomain.CurrentDomain.BaseDirectory + @"\Extensions\ExampleExtension (3).dll");
            ////catalog.Catalogs.Add(new AssemblyCatalog(asm));
            ////AppDomain.Unload(asm);

            CompositionContainer container = new CompositionContainer(catalog);

            try
            {
                container.ComposeParts(this);

                Thread thread = new Thread(() =>
                {
                    if (exts.Count > 0)
                    {
                        Program.Invoke(() =>
                        {
                            Program.LoadingScreen.TopMost = false;
                            if (exts.Count >= 5 && Program.ini.GetValue("Settings", "ExtensionUpdates", "1") == "1") if (MessageBox.Show("Seems like you're running 5 or more extensions, update checking every time the bot starts could majorly impact the startup time.\r\n\r\nWould you like to skip update checks for extensions? (To reverse delete the line ExtensionUpdates=0 in the config file).", "ModBot", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes) Program.ini.SetValue("Settings", "ExtensionUpdates", "0");
                            Program.LoadingScreen.TopMost = true;
                        }, Program.LoadingScreen);

                        if (Program.ini.GetValue("Settings", "ExtensionUpdates", "1") == "1")
                        {
                            Console.WriteLine("Checking for updates to extensions...\r\n ==============================================================================\r\n");

                            foreach (IExtension extension in exts)
                            {
                                if (extension.Name == null || extension.Name == "" || extension.FileName == null || extension.FileName == "" || extension.Author == null || extension.Author == "" || extension.UniqueID == null || extension.UniqueID == "" || extension.Version == null || extension.Version == "")
                                {
                                    Console.WriteLine(" An extension hasn't filled the metadata, skipping.");
                                    continue;
                                }

                                if (extension.UpdateURL == null || extension.UpdateURL == "")
                                {
                                    Console.WriteLine(" No update url from " + extension.Name + " (by " + extension.Author + "), skipping.");
                                    continue;
                                }

                                if (extension.UpdateCheck())
                                {
                                    Console.WriteLine(" An update to " + extension.Name + " (by " + extension.Author + ") has been found, downloading...");

                                    int value = Program.LoadingScreen.Progress.Value;
                                    Program.LoadingScreen.Progress.Value = 0;
                                    Program.LoadingScreen.Progress.Text = "Initializing download...";
                                    bool done = false;
                                    Thread t = new Thread(() =>
                                    {
                                        using (WebClient w = new WebClient())
                                        {
                                            w.Proxy = null;
                                            try
                                            {
                                                w.DownloadProgressChanged += new DownloadProgressChangedEventHandler((object sender, DownloadProgressChangedEventArgs e) =>
                                                {
                                                    Program.Invoke(() =>
                                                    {
                                                        Program.LoadingScreen.Progress.Value = int.Parse(Math.Truncate(double.Parse(e.BytesReceived.ToString()) / double.Parse(e.TotalBytesToReceive.ToString()) * 100).ToString());
                                                        Program.LoadingScreen.Progress.Text = "Downloading... (PRECENTAGE)";
                                                    }, Program.LoadingScreen);
                                                });
                                                w.DownloadFileCompleted += new AsyncCompletedEventHandler((object sender, AsyncCompletedEventArgs e) =>
                                                {
                                                    if (e.Error == null && !e.Cancelled)
                                                    {
                                                        Console.WriteLine(" The update to " + extension.Name + " (by " + extension.Author + ") has been downloaded, restart the bot to apply.");
                                                        updated.Add(extension);
                                                        done = true;
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine(" Error while attempting to update " + extension.Name + " (by " + extension.Author + ")!");
                                                        done = true;
                                                    }
                                                });
                                                w.DownloadFileAsync(new Uri(extension.UpdateURL), @"Extensions\Updates\" + extension.FileName);
                                            }
                                            catch
                                            {
                                                Console.WriteLine(" Error while attempting to update " + extension.Name + " (by " + extension.Author + ")!");
                                                done = true;
                                            }
                                        }
                                    });
                                    t.Name = "Update downloading for " + extension.Name + " (by " + extension.Author + ")";
                                    t.Start();
                                    while (!done) Thread.Sleep(10);

                                    Program.LoadingScreen.Progress.Value = value;
                                    Program.LoadingScreen.Progress.Text = "";
                                }
                            }

                            if (updated.Count == 0)
                            {
                                Console.WriteLine(" No updates found.");
                            }
                            else
                            {
                                Console.WriteLine("\r\n Updated " + updated.Count + " extensions.");
                            }

                            //Console.WriteLine(" ==============================================================================\r\nDone checking for updates to extensions, " + (updated.Count == 0 ? "no updates found." : "updated " + updated.Count + " extensions") + "\r\n\r\nLoading extensions...\r\n ==============================================================================");
                            //Console.WriteLine("\r\n ==============================================================================\r\nDone checking for updates to extensions.\r\n\r\nLoading extensions...\r\n ==============================================================================\r\n");
                            Console.WriteLine("\r\n ==============================================================================\r\nDone checking for updates to extensions.\r\n");
                        }

                        Console.WriteLine("Loading extensions...\r\n ==============================================================================\r\n");

                        foreach (IExtension extension in exts)
                        {
                            if (extension.Name == null || extension.Name == "" || extension.FileName == null || extension.FileName == "" || extension.Author == null || extension.Author == "" || extension.UniqueID == null || extension.UniqueID == "" || extension.Version == null || extension.Version == "")
                            {
                                Console.WriteLine(" An extension hasn't filled the metadata, skipping.");
                                fails++;
                                continue;
                            }

                            if (updated.Contains(extension))
                            {
                                Console.Write(" The extension " + extension.Name + " (by " + extension.Author + ") has a newer version available and will not load until applied.\r\n");
                                fails++;
                                continue;
                            }

                            Console.WriteLine(" Loading " + extension.Name + " (v" + extension.Version + " by " + extension.Author + ")... ");

                            if (extension.ApiVersion == Program.ApiVersion || extension.ApiVersion == 0)
                            {
                                extensions.Add(extension);

                                Thread t = new Thread(() =>
                                {
                                    extension.Load();
                                });
                                t.Name = extension.Name;
                                t.Start();
                                t.Join();

                                Console.Write(" " + extension.Name + " (v" + extension.Version + " by " + extension.Author + ") has been loaded.\r\n");
                            }
                            else
                            {
                                Console.Write(" Could not load " + extension.Name + " (v" + extension.Version + " by " + extension.Author + "), API version mismatch, check for updates to the extension (and the bot if haven't updated already).\r\n");
                            }
                        }

                        Console.WriteLine("\r\n ==============================================================================\r\nExtensions loaded, " + fails + " failed.\r\n");
                    }
                });
                thread.Name = "Extensions - Updates & Initialization";
                thread.Start();
                thread.Join();
            }
            catch (ReflectionTypeLoadException)
            {
                Console.WriteLine("Could not load extensions, one or more of the extensions' basic form is incorrect.");
                fails++;
            }
            catch (CompositionException e)
            {
                Console.WriteLine(e.ToString());
                fails++;
            }
        }
    }
}