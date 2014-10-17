using System;
using System.Collections.Generic;
using System.IO;

namespace ModBot
{
    public delegate void CommandExecutedHandler(string user, string command, string[] args);

    static class Commands
    {
        public class Command
        {
            public string Cmd { get; private set; }
            public int MinLevel = 0, Delay = 5;
            public bool StreamerNoDelay, ModNoDelay, SubNoDelay;
            private event CommandExecutedHandler Executed;

            public Command(string Command, CommandExecutedHandler Handler, int MinLevel = 0, int Delay = 5, bool StreamerNoDelay = true, bool ModNoDelay = true, bool SubNoDelay = false)
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

                    Cmd = Command;

                    this.MinLevel = MinLevel;

                    this.Delay = Delay;

                    if (ModNoDelay) StreamerNoDelay = true;
                    this.StreamerNoDelay = StreamerNoDelay;
                    this.ModNoDelay = ModNoDelay;
                    this.SubNoDelay = SubNoDelay;

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

        public static List<Command> lCommands = new List<Command>();
        public static Dictionary<string, int> dLastUsed = new Dictionary<string, int>();

        public static void Add(string Command, CommandExecutedHandler Handler, int MinLevel = 0, int Delay = 5, bool StreamerNoDelay = true, bool ModNoDelay = true, bool SubNoDelay = false)
        {
            new Command(Command, Handler, MinLevel, Delay, StreamerNoDelay, ModNoDelay, SubNoDelay);
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
                    if (cmd.Cmd.ToLower() == Command.ToLower()) return true;
                }
            }

            return false;
        }

        public static bool CheckCommand(string user, string message, bool call = false, bool log = true)
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
                            lock (dLastUsed)
                            {
                                if (Command.MinLevel > Database.getUserLevel(user) || dLastUsed.ContainsKey(cmd[0].ToLower()) && Api.GetUnixTimeNow() - dLastUsed[cmd[0].ToLower()] < Command.Delay && (!Command.StreamerNoDelay || user.ToLower() != Irc.channel.Substring(1)) && (!Command.ModNoDelay || !Irc.Moderators.Contains(user.ToLower())) && (!Command.SubNoDelay || !Irc.Subscribers.Contains(user.ToLower()))) return true;

                                if (!dLastUsed.ContainsKey(cmd[0].ToLower())) dLastUsed.Add(cmd[0].ToLower(), Api.GetUnixTimeNow());
                                dLastUsed[cmd[0].ToLower()] = Api.GetUnixTimeNow();
                            }

                            if (user != "" && log) Log(user + " has used the \"" + Command.Cmd + "\" command.");

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
                    if (Database.getUserLevel(user) >= Database.Commands.LevelRequired(cmd[0]))
                    {
                        //if (dLastUsed.ContainsKey(cmd[0].ToLower()) && Api.GetUnixTimeNow() - dLastUsed[cmd[0].ToLower()] < 5 && Database.getUserLevel(user) < 4) return true;
                        //if (dLastUsed.ContainsKey(cmd[0].ToLower()) && Api.GetUnixTimeNow() - dLastUsed[cmd[0].ToLower()] < 5 && (!Command.StreamerNoDelay || user.ToLower() != Irc.channel.Substring(1)) && (!Command.ModNoDelay || !Irc.Moderators.Contains(user.ToLower())) && (!Command.SubNoDelay || !Irc.Subscribers.Contains(user.ToLower()))) return true;
                        lock (dLastUsed)
                        {
                            if (dLastUsed.ContainsKey(cmd[0].ToLower()) && Api.GetUnixTimeNow() - dLastUsed[cmd[0].ToLower()] < 5 && (user.ToLower() != Irc.channel.Substring(1) || Database.getUserLevel(user) > 0 || !Irc.Moderators.Contains(user.ToLower()))) return true;

                            if (!dLastUsed.ContainsKey(cmd[0].ToLower())) dLastUsed.Add(cmd[0].ToLower(), Api.GetUnixTimeNow());
                            dLastUsed[cmd[0].ToLower()] = Api.GetUnixTimeNow();
                        }

                        if (user != "" && log) Log(user + " has used the \"" + cmd[0] + "\" command.");

                        //if (cmd.Length > 1 && Database.getUserLevel(user) > 0)
                        if (cmd.Length > 1)
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
