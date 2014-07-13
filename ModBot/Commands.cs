using System;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.IO;
using System.Collections.Generic;

namespace ModBot
{
    public delegate void CommandExecutedHandler(string user, string command, string[] args);

    public static class Commands
    {
        private class Command
        {
            public string Cmd { get; private set; }
            private event CommandExecutedHandler Executed;
            private List<CommandExecutedHandler> Handlers = new List<CommandExecutedHandler>();

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
                            if (!Handlers.Contains(Handler))
                            {
                                cmd.Executed += Handler;
                                Handlers.Add(Handler);
                            }
                            return;
                        }
                    }
                    this.Cmd = Command;
                    Executed += Handler;
                    Handlers.Add(Handler);
                    lCommands.Add(this);
                }
            }

            public void Call(string message)
            {
                string[] args = message.Contains(" ") ? message.Substring(message.IndexOf(" ") + 1).Split(' ') : null;
                if (Executed != null)
                {
                    new System.Threading.Thread(() =>
                    {
                        Executed("", Cmd, args);
                    }).Start();
                }
            }

            public void Call(string user, string message)
            {
                string[] args = message.Contains(" ") ? message.Substring(message.IndexOf(" ") + 1).Split(' ') : null;
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
                Handlers = new List<CommandExecutedHandler>();
            }

            public void Remove(CommandExecutedHandler Handler)
            {
                if (Handlers.Contains(Handler))
                {
                    Executed -= Handler;
                    Handlers.Remove(Handler);
                }
            }
        }

        private static SQLiteConnection DB = Database.DB;
        private static SQLiteCommand cmd;
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

        public static bool CheckCommand(string message, bool call = false)
        {
            while (message.Contains("  "))
            {
                message = message.Replace("  ", " ");
            }
            if (message.StartsWith(" ")) message = message.Substring(1);
            if (message.EndsWith(" ")) message = message.Substring(0, message.Length - 1);
            string[] cmd = message.Split(' ');
            lock (lCommands)
            {
                foreach (Command Command in lCommands)
                {
                    if (Command.Cmd.ToLower() == cmd[0].ToLower())
                    {
                        if (call)
                        {
                            Command.Call(message);
                        }
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
            string[] cmd = message.Split(' ');
            lock (lCommands)
            {
                foreach (Command Command in lCommands)
                {
                    if (Command.Cmd.ToLower() == cmd[0].ToLower())
                    {
                        if (call)
                        {
                            Command.Call(user, message);
                        }
                        return true;
                    }
                }
            }
            if (cmdExists(cmd[0]))
            {
                if (call) // ToDo : Convert to the new system
                {
                    if (Database.getUserLevel(user) >= LevelRequired(cmd[0]))
                    {
                        if (cmd.Length > 1 && Database.getUserLevel(user) > 0)
                        {
                            Irc.sendMessage(getOutput(cmd[0]).Replace("@user", Api.GetDisplayName(cmd[1])));
                        }
                        else
                        {
                            Irc.sendMessage(getOutput(cmd[0]).Replace("@user", Api.GetDisplayName(user)));
                        }
                    }
                }
                return true;
            }
            return false;
        }

        public static bool cmdExists(string command)
        {
            using (cmd = new SQLiteCommand("SELECT * FROM commands", DB))
            {
                using (SQLiteDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        if (r["command"].ToString().Equals(command, StringComparison.OrdinalIgnoreCase))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public static void addCommand(string command, int level, string output)
        {
            using (cmd = new SQLiteCommand("INSERT INTO commands (command, level, output) VALUES (\"" + command + "\", " + level + ", \"" + output + "\");", DB))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public static void removeCommand(string command)
        {
            using (cmd = new SQLiteCommand("DELETE FROM commands WHERE command = \"" + command + "\";", DB))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public static int LevelRequired(string command)
        {
            using (cmd = new SQLiteCommand("SELECT * FROM commands WHERE command = \"" + command + "\";", DB))
            {
                using (SQLiteDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        return int.Parse(r["level"].ToString());
                    }
                }
            }
            return 0;
        }

        public static string getList()
        {
            string commands = "";
            using (cmd = new SQLiteCommand("SELECT * FROM commands;", DB))
            {
                using (SQLiteDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        commands += r["command"].ToString() + ", ";
                    }
                }
            }
            return commands.Substring(0, commands.Length - 2);
        }

        public static string getOutput(string command)
        {
            using (cmd = new SQLiteCommand("SELECT * FROM commands WHERE command = \"" + command + "\";", DB))
            {
                using (SQLiteDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        return r["output"].ToString();
                    }
                    return "";
                }
            }
        }
    }
}
