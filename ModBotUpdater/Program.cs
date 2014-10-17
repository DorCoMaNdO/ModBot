using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ModBotUpdater
{
    static class Program
    {
        public static iniUtil ini = new iniUtil(AppDomain.CurrentDomain.BaseDirectory + @"Data\Settings\", "ModBot.ini", "\r\n[Default]");
        public static List<string> args;
        public static Updater Updater;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            System.Net.ServicePointManager.DefaultConnectionLimit = int.MaxValue;

            Program.args = args.ToList();

            if (!Directory.Exists("Data")) Directory.CreateDirectory("Data");
            if (!Directory.Exists(@"Data\Settings")) Directory.CreateDirectory(@"Data\Settings");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(Updater = new Updater());
        }
    }
}
