using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ModBotUpdater
{
    static class Program
    {
        public static List<string> args;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            System.Net.ServicePointManager.DefaultConnectionLimit = int.MaxValue;

            Program.args = args.ToList();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Updater());
        }
    }
}
