using ModBot;
using System;
using System.Collections.Generic;
using System.Net;

namespace CoMaNdO.Greetings
{
    public class Greetings : IExtension
    {
        private string LatestVersion, Message;
        private bool On;
        private Settings Settings;

        public void Load()
        {
            Events.Connected += Events_Connected;
            Events.Users.UserlistRefreshed += Events_UserlistRefreshed;
            Events.Users.Added += Events_UserAdded;

            Settings = new Settings(this, "Greetings.ini");
            Settings.SetValue("Settings", "Message", Message = Settings.GetValue("Settings", "Message", "Hello {user}! Welcome to the stream!"));
            Settings.SetValue("Settings", "On", (On = (Settings.GetValue("Settings", "On", "0") == "1")) ? "1" : "0");
        }

        private void Events_Connected(string channel, string nick, bool partnered, bool subprogram)
        {
            Commands.Add(this, "!modbot", Command_ModBot, Users.UserLevel.TrustedMod, 300);
        }

        private void Events_UserlistRefreshed(List<string> joined, List<string> left, bool initial)
        {
            if (!initial && Channel.IsStreaming && On)
            {
                string msg = "";
                foreach (string user in joined)
                {
                    msg += (msg != "" ? ", " : "") + user;
                    joined.Add(user);
                }
                if (msg != "" && Message != "") Chat.SendMessage(Message.Replace("@user", msg).Replace("{user}", msg));
            }
        }

        private void Events_UserAdded(string user, bool initial, bool FromReload)
        {
            if (!initial && !FromReload && Channel.IsStreaming && On) Chat.SendMessage(Message.Replace("@user", user).Replace("{user}", user));
        }

        private void Command_ModBot(string user, Command cmd, string[] args, string origin)
        {
            if (args.Length > 0)
            {
                if (Users.GetLevel(user) >= 4)
                {
                    if (args.Length > 1)
                    {
                        if (args[0].ToLower() == "greeting" || args[0].ToLower() == "greetings")
                        {
                            if (args[1].ToLower() == "on")
                            {
                                On = true;
                                Settings.SetValue("Settings", "On", "1");
                                Chat.SendMessage("Greetings turned on.");
                            }
                            else if (args[1].ToLower() == "off")
                            {
                                On = false;
                                Settings.SetValue("Settings", "On", "0");
                                Chat.SendMessage("Greetings turned off.");
                            }
                            else if (args[1].ToLower() == "set" && args.Length > 2)
                            {
                                string sGreeting = "";
                                for (int i = 2; i < args.Length; i++)
                                {
                                    sGreeting += args[i] + " ";
                                }
                                Message = sGreeting.Substring(0, sGreeting.Length - 1);
                                Settings.SetValue("Settings", "Message", Message);
                                Chat.SendMessage("Your new greeting is: " + Message);
                            }
                        }
                    }
                }
            }
        }

        public string Name { get { return "Greetings"; } }
        public string FileName { get { return "CoMaNdO.Greetings.dll"; } }
        public string Author { get { return "CoMaNdO"; } }
        public string UniqueID { get { return "CoMaNdO.Greetings"; } }
        public string ContactInfo { get { return "CoMaNdO.ModBot@gmail.com"; } }
        public string Version { get { return "0.0.3"; } }
        public int ApiVersion { get { return 5; } }
        public int LoadPriority { get { return 2; } }

        public bool UpdateCheck()
        {
            using (WebClient w = new WebClient())
            {
                w.Proxy = null;
                try
                {
                    LatestVersion = w.DownloadString("https://dl.dropboxusercontent.com/u/60356733/ModBot/Extensions/Extensions.txt");
                    if (LatestVersion != "")
                    {
                        foreach (string Extension in LatestVersion.Split(Environment.NewLine.ToCharArray()))
                        {
                            string[] data = Extension.Split(';');
                            if (data.Length > 6)
                            {
                                if (data[3] == UniqueID)
                                {
                                    LatestVersion = data[5];

                                    string[] Latest = LatestVersion.Split('.'), Current = Version.Split('.');
                                    int LatestMajor = int.Parse(Latest[0]), LatestMinor = int.Parse(Latest[1]), LatestBuild = int.Parse(Latest[2]);
                                    int CurrentMajor = int.Parse(Current[0]), CurrentMinor = int.Parse(Current[1]), CurrentBuild = int.Parse(Current[2]);
                                    return (LatestMajor > CurrentMajor || LatestMajor == CurrentMajor && LatestMinor > CurrentMinor || LatestMajor == CurrentMajor && LatestMinor == CurrentMinor && LatestBuild > CurrentBuild);
                                }
                            }
                        }
                    }
                }
                catch
                {
                }
            }
            return false;
        }

        public string UpdateURL { get { return "https://dl.dropboxusercontent.com/u/60356733/ModBot/Extensions/" + UniqueID + "/" + LatestVersion + "/" + FileName; } }
    }
}
