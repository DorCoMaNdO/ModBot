using ModBot;
using System;
using System.ComponentModel.Composition;
using System.Net;

namespace CoMaNdO.Uptime
{
    [Export(typeof(IExtension))]
    public class Uptime : IExtension
    {
        private string LatestVersion;

        public void Load()
        {
            Events.Connected += Events_Connected;
        }

        private void Events_Connected(string channel, string nick, bool partnered, bool subprogram)
        {
            Commands.Add(this, "!uptime", Command_Uptime, 0, 300);
        }

        private void Command_Uptime(string user, Command cmd, string[] args)
        {
            if (Channel.IsStreaming)
            {
                TimeSpan t = TimeSpan.FromSeconds(Api.GetUnixTimeNow() - Channel.StreamStartTime);
                Chat.SendMessage("The stream is up for " + t.Days + " days, " + t.Hours + " hours, " + t.Minutes + " minutes and " + t.Seconds + " seconds.");
            }
            else
            {
                Chat.SendMessage("The stream is offline.");
            }
        }

        public string Name { get { return "Uptime Command"; } }
        public string FileName { get { return "CoMaNdO.Uptime.dll"; } }
        public string Author { get { return "CoMaNdO"; } }
        public string UniqueID { get { return "CoMaNdO.Uptime"; } }
        public string ContactInfo { get { return "CoMaNdO.ModBot@gmail.com"; } }
        public string Version { get { return "0.0.4"; } }
        public int ApiVersion { get { return 0; } }
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
