using ModBot;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;

namespace CoMaNdO.Polls
{
    [Export(typeof(IExtension))]
    public class Polls : IExtension
    {
        private string LatestVersion;

        public void Load()
        {
            Poll.Load(this);
        }

        public string Name { get { return "Polls"; } }
        public string FileName { get { return "CoMaNdO.Polls.dll"; } }
        public string Author { get { return "CoMaNdO"; } }
        public string UniqueID { get { return "CoMaNdO.Polls"; } }
        public string ContactInfo { get { return "CoMaNdO.ModBot@gmail.com"; } }
        public string Version { get { return "0.0.3"; } }
        public int ApiVersion { get { return 0; } }
        public int LoadPriority { get { return 1; } }

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

    public delegate void OnPollStart(int Cost, int Goal, int TotalGoal, List<string> Options);

    public delegate void OnPollEnd(string WinningOption, int Votes, EndReason Reason);

    public enum EndReason
    {
        Manual,
        Goal,
        TotalGoal
    }

    static class Poll
    {
        public static List<string> Options = new List<string>();
        public static Dictionary<string, string> Voters = new Dictionary<string, string>();
        public static int VoteCost = 5, MaxVotes = 1, VotesGoal, TotalVotesGoal;
        public static bool ModsCanVote = true, SubsCanVote = true, UsersCanVote = true, AnnounceVotes, IsOpen;
        public static string Title = "";
        public static System.Threading.Timer VotesQueue;
        private static IExtension extension;

        public static event OnPollStart OnPollStart = ((int Cost, int Goal, int TotalGoal, List<string> Options) => { });

        public static event OnPollEnd OnPollEnd = ((string WinningOption, int Votes, EndReason Reason) => { });

        public static void Load(IExtension sender)
        {
            extension = sender;

            //UI.AddWindow("Poll", new PollsWindow());

            Events.Connected += Events_Connected;
        }
        private static void Events_Connected(string channel, string nick, bool partnered, bool subprogram)
        {
            Commands.Add(extension, "!poll", Command_Poll, 0, 0);
            Commands.Add(extension, "!vote", Command_Vote, 0, 0);

            /*string text = "";
            for (int i = 0; i < Options.Count; i++) text += "(" + (i + 1) + ") " + Options[i] + ". ";

            Chat.SendMessage("Vote over: " + title);
            Chat.SendMessage("Poll voting options: " + text);*/
        }

        private static void Command_Poll(string user, Command cmd, string[] args)
        {
            if (args.Length > 0)
            {
                if (args[0].ToLower() == "vote" && args.Length > 1)
                {
                    Command_Vote(user, cmd, new string[] { args[1] });
                    return;
                }

                if (Users.GetLevel(user) > 0)
                {
                    if (args[0].ToLower() == "votes")
                    {
                        lock (Options)
                        {
                            if (Options.Count < 2) return;

                            string text = "";

                            Dictionary<string, int> Votes = new Dictionary<string, int>();
                            foreach(string voter in Voters.Keys)
                            {
                                if (!Votes.ContainsKey(Voters[voter])) Votes.Add(Voters[voter], 0);

                                Votes[Voters[voter]]++;
                            }

                            Votes.OrderByDescending(vote => vote.Value);

                            int count = 0, max = 0;

                            if (args.Length < 3 || args[1].ToLower() != "top" || !int.TryParse(args[2], out max)) max = 0;

                            if (max > 0 && max < Options.Count) text = "Leading " + max + "options: ";

                            foreach (KeyValuePair<string, int> kv in Votes)
                            {
                                if (count >= max) break;
                                //text += kv.Value + " votes for " + kv.Key + ". ";
                                text += kv.Key + " with " + kv.Value + " votes. ";
                                count++;
                            }

                            Chat.SendMessage(text);
                            return;
                        }
                    }

                    if (Users.GetLevel(user) > 1)
                    {
                        if (args[0].ToLower() == "create" || args[0].ToLower() == "start")
                        {
                            lock (Options)
                            {
                                if (Options.Count > 1)
                                {
                                    Chat.SendMessage("A poll is already running.");
                                    return;
                                }

                                int cost, goal, totalgoal;
                                if (args.Length < 7 || !int.TryParse(args[1], out cost) || cost < 0 || !int.TryParse(args[2], out goal) || !int.TryParse(args[3], out totalgoal))
                                {
                                    Chat.SendMessage("Syntax: !poll create {VoteCost} {VotesGoal} {TotalVotesGoal} {Title} {Option1} {Option2} ... {OptionN}");
                                    return;
                                }

                                List<string> lOptions = new List<string>();
                                try
                                {
                                    bool inQuote = false;
                                    string option = "";
                                    for (int i = 4; i < args.Length; i++)
                                    {
                                        if (args[i].StartsWith("\""))
                                        {
                                            inQuote = true;
                                        }
                                        if (!inQuote)
                                        {
                                            option = args[i];
                                        }
                                        if (inQuote)
                                        {
                                            option += args[i] + " ";
                                        }
                                        if (args[i].EndsWith("\""))
                                        {
                                            option = option.Substring(1, option.Length - 3);
                                            inQuote = false;
                                        }
                                        if (!inQuote && !option.StartsWith("#"))
                                        {
                                            bool exists = false;
                                            foreach (string optn in lOptions)
                                            {
                                                if (optn.ToLower() == option.ToLower())
                                                {
                                                    exists = true;
                                                    break;
                                                }
                                            }
                                            if (!exists)
                                            {
                                                lOptions.Add(option);
                                            }
                                            option = "";
                                        }
                                    }
                                }
                                catch
                                {
                                    lOptions.Clear();
                                }

                                string title = lOptions[0];
                                lOptions.Remove(title);

                                if (title.StartsWith("\"") && title.EndsWith("\"")) title.Substring(1, title.Length - 2);

                                Start(cost, goal, totalgoal, title, lOptions, true);
                            }
                        }
                        else if (args[0].ToLower() == "open")
                        {
                            Open(true);
                        }
                        else if (args[0].ToLower() == "close")
                        {
                            Close(true);
                        }
                        else if (args[0].ToLower() == "stop" || args[0].ToLower() == "end")
                        {
                            Stop("", EndReason.Manual, true);
                        }
                        else if (args[0].ToLower() == "cancel")
                        {
                            Cancel(true);
                        }
                    }
                }
            }
        }

        public static bool Start(int cost, int goal, int totalgoal, string title, List<string> options, bool Announce = false)
        {
            if (Options.Count > 1)
            {
                Chat.SendMessage("A poll is already running.");

                return false;
            }

            if (options.Count > 1)
            {
                lock (Options)
                {
                    Options = options;
                }

                Voters.Clear();
                VoteCost = cost;
                VotesGoal = goal;
                TotalVotesGoal = totalgoal;
                Title = title;
                IsOpen = true;

                OnPollStart(VoteCost, VotesGoal, TotalVotesGoal, Options);

                if (Announce)
                {
                    Chat.SendMessage("A poll has been started! Vote cost: " + VoteCost + " " + Currency.Name);

                    string text = "";
                    for (int i = 0; i < Options.Count; i++) text += "(" + (i + 1) + ") " + Options[i] + ". ";

                    Chat.SendMessage("Vote over: " + title);
                    Chat.SendMessage("Poll voting options: " + text);
                }

                return true;
            }
            return false;
        }

        public static bool Open(bool Announce = false)
        {
            if (Options.Count < 2 || IsOpen)
            {
                if (Announce) Chat.SendMessage("A poll must be started and closed first.");

                return false;
            }

            IsOpen = true;
            if (Announce) Chat.SendMessage("The poll has been opened, new votes are now accepted again.");

            return true;
        }

        public static bool Close(bool Announce = false)
        {
            if (Options.Count < 2 || !IsOpen)
            {
                if (Announce) Chat.SendMessage("A poll must be started and open first.");
                return false;
            }

            IsOpen = false;
            if (Announce) Chat.SendMessage("The poll has been closed, new votes will no longer be accepted.");

            return true;
        }

        public static KeyValuePair<string, int> Stop(string Winner = "", EndReason Reason = EndReason.Manual, bool Announce = false)
        {
            KeyValuePair<string, int> WinningOption = new KeyValuePair<string,int>();

            lock (Options)
            {
                if (Options.Count > 1)
                {
                    IsOpen = false;

                    Dictionary<string, int> Votes = new Dictionary<string, int>();
                    foreach (string voter in Voters.Keys)
                    {
                        if (!Votes.ContainsKey(Voters[voter])) Votes.Add(Voters[voter], 0);

                        Votes[Voters[voter]]++;
                    }

                    if (Winner == "")
                    {
                        Votes.OrderByDescending(vote => vote.Value);

                        Winner = Votes.ElementAt(0).Key;
                    }

                    WinningOption = new KeyValuePair<string, int>(Winner, Votes[Winner]);

                    OnPollEnd(WinningOption.Key, WinningOption.Value, Reason);

                    if (Announce) Chat.SendMessage("The poll has ended! Winning option: " + WinningOption.Key + " with " + WinningOption.Value + " votes.");

                    VoteCost = 5;
                    VotesGoal = 0;
                    TotalVotesGoal = 0;
                    Options.Clear();
                    Voters.Clear();
                    Winner = "";
                }
                else
                {
                    if (Announce) Chat.SendMessage("A poll must be started first.");
                }
            }

            return WinningOption;
        }

        public static bool Cancel(bool Announce = false)
        {
            lock (Options)
            {
                if (Options.Count < 2)
                {
                    if (Announce) Chat.SendMessage("A poll must be started first.");

                    return false;
                }

                IsOpen = false;
                VotesGoal = 0;
                TotalVotesGoal = 0;
                Options.Clear();

                foreach (string voter in Voters.Keys) Currency.Add(voter, VoteCost);

                VoteCost = 5;
                Voters.Clear();

                if (Announce) Chat.SendMessage("The poll has been cancelled. All votes have been refunded.");

                return true;
            }
        }

        public static bool Vote(string user, string option, bool Announce = false)
        {
            if (!IsOpen || Options.Count < 2 || Voters.ContainsKey(user.ToLower())) return false;

            if(Currency.Check(user) >= VoteCost)
            {
                if (Options.Contains(option.ToLower()))
                {
                    Currency.Remove(user, VoteCost);
                    Voters.Add(user.ToLower(), option.ToLower());

                    if (Announce) Chat.SendMessage(user + " has voted for " + option + ".");

                    Dictionary<string, int> Votes = new Dictionary<string, int>();
                    foreach (string voter in Voters.Keys)
                    {
                        if (!Votes.ContainsKey(Voters[voter])) Votes.Add(Voters[voter], 0);

                        Votes[Voters[voter]]++;
                    }

                    int totalvotes = 0;
                    foreach (int votes in Votes.Values) totalvotes += votes;
                    if (VotesGoal > 0 && Votes[option.ToLower()] >= VotesGoal || TotalVotesGoal > 0 && totalvotes >= TotalVotesGoal)
                    {
                        Chat.SendMessage(VotesGoal > 0 && Votes[option.ToLower()] >= VotesGoal ? "The votes goal has been reached!" : "The total votes goal has been reached!");
                        Stop(VotesGoal > 0 && Votes[option.ToLower()] >= VotesGoal ? option.ToLower() : "", VotesGoal > 0 && Votes[option.ToLower()] >= VotesGoal ? EndReason.Goal : EndReason.TotalGoal);
                    }
                }
                else
                {
                    if (Announce) Chat.SendMessage(user + ", the option does not exist.");
                }
                return true;
            }
            return false;
        }

        private static void Command_Vote(string user, Command cmd, string[] args)
        {
            if (args.Length > 0)
            {
                string vote = "";
                foreach (string arg in args) vote += (vote == "" ? "" : " ") + arg;
                lock (Options)
                {
                    if (args[0].StartsWith("#"))
                    {
                        int optionnumber = 0;
                        if (int.TryParse(args[0].Substring(1), out optionnumber))
                        {
                            optionnumber--;
                            if (optionnumber >= 0 && optionnumber < Options.Count)
                            {
                                vote = Options[optionnumber];
                            }
                        }
                    }

                    Vote(user, vote, AnnounceVotes);
                }
            }
            else
            {
                if (AnnounceVotes) Chat.SendMessage(user + " you have insufficient " + Currency.Name + ".");
            }
        }
    }

    public static class Api
    {
        public static bool Start(int cost, int goal, int totalgoal, string title, List<string> options, bool Announce = false) { return Poll.Start(cost, goal, totalgoal, title, options, Announce); }

        public static bool Open(bool Announce = false) { return Poll.Open(Announce); }

        public static bool Close(bool Announce = false) { return Poll.Close(Announce); }

        public static KeyValuePair<string, int> Stop(string Winner = "", EndReason Reason = EndReason.Manual, bool Announce = false) { return Poll.Stop(Winner, Reason, Announce); }

        public static bool Cancel(bool Announce = false) { return Poll.Cancel(Announce); }

        public static class PollEvents
        {
            public static event OnPollStart OnStart { add { Poll.OnPollStart += value; } remove { Poll.OnPollStart -= value; } }

            public static event OnPollEnd OnEnd { add { Poll.OnPollEnd += value; } remove { Poll.OnPollEnd -= value; } }
        }
    }
}