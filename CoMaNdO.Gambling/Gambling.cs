using ModBot;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Threading;

namespace CoMaNdO.Gambling
{
    [Export(typeof(IExtension))]
    public class Gambling : IExtension
    {
        private string LatestVersion;

        public void Load()
        {
            Pool.Load();
        }

        public string Name { get { return "Gambling System"; } }
        public string FileName { get { return "CoMaNdO.Gambling.dll"; } }
        public string Author { get { return "CoMaNdO"; } }
        public string UniqueID { get { return "CoMaNdO.Gambling"; } }
        public string ContactInfo { get { return "CoMaNdO.ModBot@gmail.com"; } }
        public string Version { get { return "0.0.1"; } }
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

    static class Pool
    {
        public static Dictionary<string, int> Winners = new Dictionary<string, int>(), FalseEntries = new Dictionary<string,int>();
        public static Dictionary<string, KeyValuePair<string, int>> bets = new Dictionary<string, KeyValuePair<string, int>>();
        private static int MinBet, MaxBet, TotalBets, WinReward, LastUsedHelp, LastAnnouncedCost;
        private static System.Threading.Timer BetQueue;
        public static List<string> Options = new List<string>();
        public static bool Running, Locked;

        public static void Load()
        {
            Events.Connected += Events_Connected;
            Events.Currency.OnQueue += Events_OnCurrencyQueue;
            Events.OnDisconnect += Events_OnDisconnect;
        }

        private static void Events_Connected(string channel, string nick, bool partnered)
        {
            Commands.Add("!gamble", Command_Gamble, 2, 0);
            Commands.Add("!bet", Command_Bet, 0, 0);

            if (BetQueue == null) BetQueue = new System.Threading.Timer(BetQueueHandler, null, Timeout.Infinite, Timeout.Infinite);
            BetQueue.Change(Timeout.Infinite, Timeout.Infinite);
        }

        private static void Events_OnCurrencyQueue(string user, ref string output, bool showtime)
        {
            if (Running && isInPool(user)) lock (bets) output += " [" + bets[user].Value + "]";
        }

        private static void Events_OnDisconnect()
        {
            Cancel();
        }

        public static void CreatePool(int min, int max, int reward, List<string> lOptions)
        {
            MinBet = min;
            MaxBet = max;
            WinReward = reward;
            lock(Options) Options = lOptions;
            TotalBets = 0;
            lock (bets) bets.Clear();
            lock (FalseEntries) FalseEntries.Clear();
            lock (Winners) Winners.Clear();
            Locked = false;
            Running = true;
        }

        public static bool placeBet(string user, string option, int amount)
        {
            if (Running && !Locked)
            {
                lock (bets)
                {
                    lock (FalseEntries)
                    {
                        lock (Options)
                        {
                            if (!FalseEntries.ContainsKey(user) && !bets.ContainsKey(user)) BetQueue.Change(10000, Timeout.Infinite);

                            if (Options.Contains(option))
                            {
                                if (amount >= MinBet && amount <= MaxBet)
                                {
                                    int paid = 0;
                                    if (bets.ContainsKey(user)) paid = bets[user].Value;

                                    if (Currency.Check(user) + paid >= amount)
                                    {
                                        if (bets.ContainsKey(user)) bets.Remove(user);

                                        Currency.Add(user, paid);
                                        Currency.Remove(user, amount);
                                        bets.Add(user, new KeyValuePair<string, int>(option, amount));

                                        if (FalseEntries.ContainsKey(user)) FalseEntries.Remove(user);

                                        return true;
                                    }
                                    else
                                    {
                                        if (!FalseEntries.ContainsKey(user)) FalseEntries.Add(user, 3);
                                    }
                                }
                                else
                                {
                                    if (!FalseEntries.ContainsKey(user)) FalseEntries.Add(user, 2);
                                }
                            }
                            else
                            {
                                if (!FalseEntries.ContainsKey(user)) FalseEntries.Add(user, 1);
                            }
                        }
                    }
                }
            }
            return false;
        }

        public static void closePool(string winBet)
        {
            Running = false;
            Locked = false;
            buildTotalBets();
            buildWinners(winBet);
        }

        public static void buildWinners(string winBet)
        {
            lock (Winners)
            {
                Winners.Clear();
                lock (bets)
                {
                    foreach (string user in bets.Keys)
                    {
                        if (bets[user].Key == winBet)
                        {
                            double temp = (double)(TotalBets - getTotalBetsOn(winBet)) * ((double)bets[user].Value / getTotalBetsOn(winBet));
                            int payout = (int)Math.Round(temp) + bets[user].Value + WinReward;

                            if (!Winners.ContainsKey(user)) Winners.Add(user, payout);

                            //Console.WriteLine("Pre-Round: " + temp + " Post Round and addition = " + payout);
                            /*if (Winners.ContainsKey(nick))
                            {
                                Console.WriteLine(Winners[nick].ToString());
                            }
                            else
                            {
                                Winners.Add(nick, payout);
                                //Currency.Add(nick, payout);
                            }*/
                        }
                    }
                }
            }
        }

        public static void buildTotalBets()
        {
            TotalBets = 0;
            lock (bets) foreach (string nick in bets.Keys) TotalBets += bets[nick].Value;
        }

        public static int getTotalBets()
        {
            return TotalBets;
        }

        public static int getNumberOfBets(string bet)
        {
            int numberOfBets = 0;
            lock (bets) foreach (string nick in bets.Keys) if (bets[nick].Key == bet) numberOfBets++;
            return numberOfBets;
        }

        public static int getTotalBetsOn(string bet)
        {
            int totalBetsOnOption = 0;
            lock (bets) foreach (string nick in bets.Keys) if (bets[nick].Key == bet) totalBetsOnOption += bets[nick].Value;

            return totalBetsOnOption;
        }

        public static void Cancel()
        {
            lock (bets) foreach (string nick in bets.Keys) Currency.Add(nick, bets[nick].Value);
            Clear();
            Running = false;
            Locked = false;
        }

        public static void Clear()
        {
            lock (Options) Options.Clear();
            lock (bets) bets.Clear();
            lock (FalseEntries) FalseEntries.Clear();
            lock (Winners) Winners.Clear();
        }

        public static bool isInPool(string nick)
        {
            bool inpool = false;
            lock (bets) inpool = bets.ContainsKey(nick);
            return inpool;
        }

        public static string GetOptionFromNumber(int optionnumber)
        {
            optionnumber--;
            lock (Options) if (optionnumber >= 0 && optionnumber < Options.Count) return Options[optionnumber];

            return "";
        }

        public static int GetNumberFromOption(string option)
        {
            lock (Options) for (int i = 0; i < Options.Count; i++) if (Options[i].ToLower() == option.ToLower()) return i;

            return -1;
        }

        public static List<string> buildBetOptions(string[] temp, int index = 3)
        {
            List<string> betOptions = new List<string>();
            try
            {
                lock (betOptions)
                {
                    bool inQuote = false;
                    string option = "";
                    for (int i = index; i < temp.Length; i++)
                    {
                        if (temp[i].StartsWith("\""))
                        {
                            inQuote = true;
                        }
                        if (!inQuote)
                        {
                            option = temp[i];
                        }
                        if (inQuote)
                        {
                            option += temp[i] + " ";
                        }
                        if (temp[i].EndsWith("\""))
                        {
                            option = option.Substring(1, option.Length - 3);
                            inQuote = false;
                        }
                        if (!inQuote && !option.StartsWith("#"))
                        {
                            bool exists = false;
                            foreach (string optn in betOptions)
                            {
                                if (optn.ToLower() == option.ToLower())
                                {
                                    exists = true;
                                    break;
                                }
                            }
                            if (!exists)
                            {
                                betOptions.Add(option);
                            }
                            option = "";
                        }
                    }
                }
            }
            catch
            {
            }
            return betOptions;
        }

        private static void Command_Gamble(string user, string cmd, string[] args)
        {
            if (args.Length > 0)
            {
                if (args[0].ToLower() == "open" && args.Length > 4)
                {
                    if (!Running)
                    {
                        int min, max, reward;
                        if (int.TryParse(args[1], out min) && int.TryParse(args[2], out max) && int.TryParse(args[3], out reward))
                        {
                            if (min > 0)
                            {
                                if (max >= min)
                                {
                                    if (reward >= 0)
                                    {
                                        List<string> lOptions = buildBetOptions(args, 4);
                                        if (lOptions.Count > 1)
                                        {
                                            LastUsedHelp = Api.GetUnixTimeNow();

                                            CreatePool(min, max, reward, lOptions);
                                            Chat.SendMessage("New Betting Pool opened! Min bet: " + MinBet + " " + Currency.Name + ", Max bet: " + MaxBet + " " + Currency.Name + ".");
                                            string temp = "Betting open for: ";
                                            for (int i = 0; i < lOptions.Count; i++)
                                            {
                                                temp += "(" + (i + 1).ToString() + ") " + lOptions[i];
                                                if (i + 1 < lOptions.Count)
                                                {
                                                    temp += ", ";
                                                }
                                            }
                                            Chat.SendMessage(temp + ".");
                                            Chat.SendMessage("Bet by typing \"!bet 50 #1\" to bet 50 " + Currency.Name + " on option 1, \"!bet 25 #2\" to bet 25 " + Currency.Name + " on option 2, etc.");
                                        }
                                        else
                                        {
                                            Chat.SendMessage("You need at least two betting options in order to start a betting pool!");
                                        }
                                    }
                                    else
                                    {
                                        Chat.SendMessage("WinReward can not be lower than 0!");
                                    }
                                }
                                else
                                {
                                    Chat.SendMessage("MaxBet can not be lower than MinBet!");
                                }
                            }
                            else
                            {
                                Chat.SendMessage("MinBet can not be lower than 1!");
                            }
                        }
                        else
                        {
                            Chat.SendMessage("Invalid syntax. Open a betting pool with: !gamble open {MinBet} {MaxBet} {WinReward} {option1} {option2} ... {optionN} (space delimited options)");
                        }
                    }
                    else
                    {
                        Chat.SendMessage("Betting Pool already opened. Close or cancel the current one before starting a new one.");
                    }
                }
                else if (args[0].ToLower() == "close")
                {
                    if (Running)
                    {
                        if (!Locked)
                        {
                            Locked = true;
                            BetQueue.Change(0, Timeout.Infinite);
                            Chat.SendMessage("Bets locked in. Good luck everyone!");
                            string temp = "The following options were open for betting: ";
                            lock (Options) for (int i = 0; i < Options.Count; i++) temp += "(" + (i + 1).ToString() + ") " + Options[i] + " - " + getNumberOfBets(Options[i]) + " bets (" + getTotalBetsOn(Options[i]) + " " + Currency.Name + ")" + (i + 1 < Options.Count ? ", " : "");
                            Chat.SendMessage(temp + ".");
                        }
                        else
                        {
                            Chat.SendMessage("Pool is already locked.");
                        }
                    }
                    else
                    {
                        Chat.SendMessage("The betting pool is not running.");
                    }
                }
                else if (args[0].ToLower() == "winner" && args.Length > 1)
                {
                    if (Running && Locked)
                    {
                        bool inQuote = false;
                        string option = "";
                        for (int i = 1; i < args.Length; i++)
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
                        }
                        if (option.ToLower() == args[1].ToLower())
                        {
                            if (option.StartsWith("#"))
                            {
                                int optionnumber = 0;
                                if (int.TryParse(option.Substring(1), out optionnumber))
                                {
                                    option = GetOptionFromNumber(optionnumber);
                                }
                            }
                        }

                        lock (Options)
                        {
                            if (Options.Contains(option))
                            {
                                closePool(option);
                                Chat.SendMessage("Betting Pool closed! A total of " + getTotalBets() + " " + Currency.Name + " were bet.");
                                string output = "Bets for:";
                                for (int i = 0; i < Options.Count; i++)
                                {
                                    double x = ((double)getTotalBetsOn(Options[i]) / getTotalBets()) * 100;
                                    output += " " + Options[i] + " - " + getNumberOfBets(Options[i]) + " (" + Math.Round(x) + "%);";
                                    //Console.WriteLine("TESTING: getTotalBetsOn(" + i + ") = " + getTotalBetsOn(i) + " --- getTotalBets() = " + getTotalBets() + " ---  (double)betsOn(i)/totalBets() = " + (double)(getTotalBetsOn(i) / getTotalBets()) + " --- *100 = " + (double)(getTotalBetsOn(i) / getTotalBets()) * 100 + " --- Converted to a double = " + (double)((getTotalBetsOn(i) / getTotalBets()) * 100) + " --- Rounded double = " + Math.Round((double)((getTotalBetsOn(i) / getTotalBets()) * 100)));
                                }
                                Chat.SendMessage(output);
                                Dictionary<string, int> wins = Winners;
                                output = "Winners:";
                                if (wins.Count == 0) output += " No One!";

                                for (int i = 0; i < wins.Count; i++)
                                {
                                    Currency.Add(wins.ElementAt(i).Key, wins.ElementAt(i).Value);
                                    string msg = " " + wins.ElementAt(i).Key + " - " + wins.ElementAt(i).Value + " (Bet " + bets[wins.ElementAt(i).Key].Value + ")";
                                    if (output.Length + msg.Length > 996)
                                    {
                                        Chat.SendMessage(output);
                                        output = "";
                                    }
                                    output += msg;
                                }

                                Chat.SendMessage(output);

                                Clear();
                            }
                            else
                            {
                                Chat.SendMessage("The option you specified is not available in the current pool!");
                            }
                        }
                    }
                    else
                    {
                        Chat.SendMessage("Betting pool must be running and bets must be locked before you can specify a winner, lock the bets by using \"!gamble close\".");
                        Chat.SendMessage("Pick a winning option by typing \"!gamble winner #1\" if option 1 won, \"!gamble winner #2\" for option 2, etc.");
                        Chat.SendMessage("You can type \"!bet help\" to get a list of the options as a reminder.");
                    }
                }
                else if (args[0].ToLower() == "cancel")
                {
                    if (Running)
                    {
                        Cancel();
                        Chat.SendMessage("Betting Pool canceled. All bets refunded");
                    }
                    else
                    {
                        Chat.SendMessage("The betting pool is not running.");
                    }
                }
            }
        }

        private static void Command_Bet(string user, string cmd, string[] args)
        {
            if (Running)
            {
                if (args.Length > 0)
                {
                    int Value;
                    if (args[0] == "help")
                    {
                        if (Api.GetUnixTimeNow() - LastUsedHelp < 60 && Users.GetLevel(user) < 1) return;

                        LastUsedHelp = Api.GetUnixTimeNow();

                        if (!Locked)
                        {
                            string temp = "Betting open for: ";
                            lock (Options) for (int i = 0; i < Options.Count; i++) temp += "(" + (i + 1).ToString() + ") " + Options[i] + " - " + getNumberOfBets(Options[i]) + " bets (" + getTotalBetsOn(Options[i]) + " " + Currency.Name + ")" + (i + 1 < Options.Count ? ", " : "");
                            Chat.SendMessage(temp + ".");
                            Chat.SendMessage("Bet by typing \"!bet 50 #1\" to bet 50 " + Currency.Name + " on option 1, \"bet 25 #2\" to bet 25 " + Currency.Name + " on option 2, etc.");
                        }
                        else
                        {
                            string temp = "The pool is now closed, the following options were open for betting: ";
                            lock (Options) for (int i = 0; i < Options.Count; i++) temp += "(" + (i + 1).ToString() + ") " + Options[i] + " - " + getNumberOfBets(Options[i]) + " bets (" + getTotalBetsOn(Options[i]) + " " + Currency.Name + ")" + (i + 1 < Options.Count ? ", " : "");
                            Chat.SendMessage(temp + ".");
                        }
                    }
                    else if (!Locked && int.TryParse(args[0], out Value) && args.Length > 1)
                    {
                        bool inQuote = false;
                        string option = "";
                        for (int i = 1; i < args.Length; i++)
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
                        }

                        if (option.ToLower() == args[1].ToLower())
                        {
                            if (option.StartsWith("#"))
                            {
                                int optionnumber = 0;
                                if (int.TryParse(option.Substring(1), out optionnumber))
                                {
                                    option = GetOptionFromNumber(optionnumber);
                                    if (option == "")
                                    {
                                        //Chat.SendMessage(user + " the option number does not exist");
                                        lock (FalseEntries)
                                        {
                                            if (!FalseEntries.ContainsKey(user))
                                            {
                                                FalseEntries.Add(user, 0);
                                                BetQueue.Change(10000, Timeout.Infinite);
                                            }
                                        }

                                        return;
                                    }
                                }
                            }
                        }

                        placeBet(user, option, Value);
                    }
                }
                else
                {
                    //if (isInPool(user)) Chat.SendMessage(user + ": " + bets[user].Key + " (" + bets[user].Value + ")");
                }
            }
        }

        private static void BetQueueHandler(object state)
        {
            if (Running)
            {
                lock (bets)
                {
                    if (bets.Count > 0)
                    {
                        Chat.SendMessage("A total of " + bets.Count + " bets have been placed!");

                        string finalmessage = "";
                        lock (FalseEntries)
                        {
                            foreach (string user in FalseEntries.Keys)
                            {
                                string msg = "the option number does not exist";
                                if (FalseEntries[user] == 1)
                                {
                                    msg = "the option does not exist";
                                }
                                else if (FalseEntries[user] == 2)
                                {
                                    msg = "the amount you put is invalid";
                                }
                                else if (FalseEntries[user] == 3)
                                {
                                    msg = "you have insufficient " + Currency.Name;
                                }

                                if (finalmessage.Length + msg.Length > 996)
                                {
                                    Chat.SendMessage(finalmessage);
                                    finalmessage = "";
                                }
                                finalmessage += user + ", " + msg + ". ";
                            }

                            FalseEntries.Clear();
                            if (finalmessage != "") Chat.SendMessage(finalmessage);
                        }
                    }
                }

                if(Api.GetUnixTimeNow() - LastAnnouncedCost > 60)
                {
                    LastAnnouncedCost = Api.GetUnixTimeNow();

                    string temp = "Betting open for: ";
                    lock (Options) for (int i = 0; i < Options.Count; i++) temp += "(" + (i + 1).ToString() + ") " + Options[i] + (i + 1 < Options.Count ? ", " : "");
                    Chat.SendMessage(temp + ".");
                    Chat.SendMessage("Min bet: " + MinBet + " " + Currency.Name + ", Max bet: " + MaxBet + " " + Currency.Name + ".");
                }
            }
        }
    }

    /*public static class Api
    {
        public static List<string> Options { get { return Pool.Options; } }
        public static Dictionary<string, int> Winners { get { return Pool.Winners; } }
        public static bool Running { get { return Pool.Running; } }
        public static bool Locked { get { return Pool.Locked; } set { Pool.Locked = value; } }

        public static void CreatePool(int min, int max, int reward, List<string> lOptions) { Pool.CreatePool(min, max, reward, lOptions); }

        public static bool placeBet(string nick, string option, int amount) { return Pool.placeBet(nick, option, amount); }

        public static void closePool(string winBet) { Pool.closePool(winBet); }

        public static void buildWinners(string winBet) { Pool.buildWinners(winBet); }

        public static void buildTotalBets() { Pool.buildTotalBets(); }

        public static int getTotalBets() { return Pool.getTotalBets(); }

        public static int getNumberOfBets(string bet) { return Pool.getNumberOfBets(bet); }

        public static int getTotalBetsOn(string bet) { return Pool.getTotalBetsOn(bet); }

        public static int getBetAmount(string nick) { return Pool.bets[nick].Value; }

        public static string getBetOn(string nick) { return Pool.bets[nick].Key; }

        public static void Cancel() { Pool.Cancel(); }

        public static void Clear() { Pool.Clear(); }

        public static bool isInPool(string nick) { return Pool.isInPool(nick); }

        public static string GetOptionFromNumber(int optionnumber) { return Pool.GetOptionFromNumber(optionnumber); }

        public static List<string> buildBetOptions(string[] temp) { return Pool.buildBetOptions(temp); }
    }*/
}
