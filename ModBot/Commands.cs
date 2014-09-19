using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace ModBot
{
    public delegate void CommandExecutedHandler(string user, string command, string[] args);

    public static class Commands
    {
        private class Command
        {
            public string Cmd { get; private set; }
            private event CommandExecutedHandler Executed;

            public Command(string Command, CommandExecutedHandler Handler)
            {
                if (Command == null || Command == "" || Command.Contains(" "))
                {
                    throw (new Exception("A command can not be null and cannot contain spaces"));
                }

                lock (lCommands)
                {
                    foreach (Command cmd in lCommands)
                    {
                        if (cmd.Cmd == Command)
                        {
                            cmd.Executed += Handler;
                            return;
                        }
                    }
                    this.Cmd = Command;
                    Executed += Handler;
                    lCommands.Add(this);
                }
            }

            public void Call(string user, string message)
            {
                string[] args = message.Contains(" ") ? message.Substring(message.IndexOf(" ") + 1).Split(' ') : new string[0];
                if (Executed != null)
                {
                    new System.Threading.Thread(() =>
                    {
                        Executed(user, Cmd, args);
                    }).Start();
                }
            }

            public void Remove()
            {
                Executed = null;
            }

            public void Remove(CommandExecutedHandler Handler)
            {
                Executed -= Handler;
            }
        }

        private static List<Command> lCommands = new List<Command>();

        public static void Add(string Command, CommandExecutedHandler Handler)
        {
            new Command(Command, Handler);
        }

        public static void Remove(string Command, CommandExecutedHandler Handler)
        {
            lock (lCommands)
            {
                foreach (Command cmd in lCommands)
                {
                    if (cmd.Cmd.ToLower() == Command.ToLower())
                    {
                        cmd.Remove(Handler);
                    }
                }
            }
        }

        public static void Remove(string Command)
        {
            lock (lCommands)
            {
                List<Command> lCmds = new List<Command>();
                foreach (Command cmd in lCommands)
                {
                    lCmds.Add(cmd);
                    cmd.Remove();
                }
                foreach (Command cmd in lCmds)
                {
                    if (cmd.Cmd.ToLower() == Command.ToLower())
                    {
                        lCommands.Remove(cmd);
                    }
                }
            }
        }

        public static bool Exists(string Command)
        {
            lock (lCommands)
            {
                foreach (Command cmd in lCommands)
                {
                    if (cmd.Cmd.ToLower() == Command.ToLower())
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool CheckCommand(string user, string message, bool call = false)
        {
            while (message.Contains("  "))
            {
                message = message.Replace("  ", " ");
            }
            if (message.StartsWith(" ")) message = message.Substring(1);
            if (message.EndsWith(" ")) message = message.Substring(0, message.Length - 1);
            //message = message.ToLower();
            string[] cmd = message.Split(' ');
            lock (lCommands)
            {
                foreach (Command Command in lCommands)
                {
                    if (Command.Cmd.ToLower() == cmd[0].ToLower())
                    {
                        if (call)
                        {
                            if (user != "") Log(user + " has used the \"" + Command.Cmd + "\" command.");
                            Command.Call(user, message);
                        }
                        return true;
                    }
                }
            }
            if (Database.Commands.cmdExists(cmd[0]))
            {
                if (call) // ToDo : Convert to the new system
                {
                    if (user != "") Log(user + " has used the \"" + cmd[0] + "\" command.");
                    if (Database.getUserLevel(user) >= Database.Commands.LevelRequired(cmd[0]))
                    {
                        if (cmd.Length > 1 && Database.getUserLevel(user) > 0)
                        {
                            Irc.sendMessage(Database.Commands.getOutput(cmd[0]).Replace("@user", Api.GetDisplayName(cmd[1])));
                        }
                        else
                        {
                            Irc.sendMessage(Database.Commands.getOutput(cmd[0]).Replace("@user", user));
                        }
                    }
                }
                return true;
            }
            return false;
        }

        public static void Log(string output)
        {
            output = "[" + DateTime.Now + "] " + output;
            for (int attempts = 0; attempts < 10; attempts++)
            {
                try
                {
                    using (StreamWriter log = new StreamWriter(@"Data\Logs\Commands.txt", true))
                    {
                        log.WriteLine(output);
                    }
                    break;
                }
                catch
                {
                    System.Threading.Thread.Sleep(250);
                }
            }
        }
    }
}
