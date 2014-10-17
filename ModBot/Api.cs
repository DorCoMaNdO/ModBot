using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace ModBot
{
    static class Api
    {
        public static MainWindow MainForm = Program.MainForm;
        public static Dictionary<string, Thread> dCheckingDisplayName = new Dictionary<string, Thread>();
        private static iniUtil ini = Program.ini;

        public static int GetUnixTimeNow()
        {
            return (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }

        public static int GetUnixFromTime(DateTime time)
        {
            return (Int32)(time.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }

        public static DateTime GetTimeFromUnix(int unix, bool tolocal = true)
        {
            if (tolocal) return new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc).AddSeconds(unix).ToLocalTime();
            return new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc).AddSeconds(unix);
        }

        public static bool IsFileLocked(string FileLocation, FileShare fs = FileShare.None)
        {
            FileInfo file = new FileInfo(FileLocation);
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, fs);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }

        public static string GetDisplayName(string user, bool bWait = false)
        {
            user = user.ToLower();
            if (!dCheckingDisplayName.ContainsKey(user))
            {
                if (Database.getDisplayName(user) == "")
                {
                    Thread thread = new Thread(() =>
                    {
                        using (WebClient w = new WebClient())
                        {
                            w.Proxy = null;
                            try
                            {
                                Database.setDisplayName(user, JObject.Parse(w.DownloadString("https://api.twitch.tv/kraken/users/" + user))["display_name"].ToString());
                            }
                            catch
                            {
                            }
                        }
                        if (dCheckingDisplayName.ContainsKey(user))
                        {
                            dCheckingDisplayName.Remove(user);
                        }
                    });
                    if (!dCheckingDisplayName.ContainsKey(user))
                    {
                        dCheckingDisplayName.Add(user, thread);
                        thread.Name = "Display name check for " + user;
                        thread.Start();
                        if (bWait)
                        {
                            thread.Join();
                        }
                    }
                }
            }
            else
            {
                if (bWait)
                {
                    dCheckingDisplayName[user].Join();
                }
            }
            string name = Database.getDisplayName(user);
            if (name == "") return user;
            return name;
        }

        public static bool IsFollower(string user)
        {
            bool bFollower = false;
            using (WebClient w = new WebClient())
            {
                w.Proxy = null;
                try
                {
                    string sData = w.DownloadString("https://api.twitch.tv/kraken/users/" + user.ToLower() + "/follows/channels/" + Irc.channel.Substring(1));
                    bFollower = true;
                }
                catch
                {
                }
            }
            return bFollower;
        }

        public static bool IsSubscriber(string user, bool check = false, bool manual = false)
        {
            user = user.ToLower();
            bool bSubscriber = false;
            lock (Irc.Subscribers)
            {
                bSubscriber = Irc.Subscribers.Contains(user);
            }
            if (!check) return (bSubscriber || manual && Database.isSubscriber(user));

            bSubscriber = false;
            if (Irc.partnered)
            {
                using (WebClient w = new WebClient())
                {
                    w.Proxy = null;
                    try
                    {
                        string sData = w.DownloadString("https://api.twitch.tv/kraken/channels/" + Irc.channel.Substring(1) + "/subscriptions/" + user.ToLower() + "?oauth_token=" + Irc.channeltoken);
                        bSubscriber = true;
                    }
                    catch
                    {
                    }
                }
            }
            return bSubscriber;
        }

        public static Dictionary<string, DateTime> GetLastSubscribers(DateTime startingat, int count = 25)
        {
            Dictionary<string, DateTime> Subscribers = new Dictionary<string, DateTime>();
            if (Irc.partnered)
            {
                using (WebClient w = new WebClient())
                {
                    w.Proxy = null;
                    try
                    {
                        foreach (JToken subscription in JObject.Parse(w.DownloadString("https://api.twitch.tv/kraken/channels/" + Irc.channel.Substring(1) + "/subscriptions?direction=desc&limit=" + count + "&oauth_token=" + Irc.channeltoken))["subscriptions"])
                        {
                            DateTime date = DateTime.Parse(subscription["created_at"].ToString());
                            if (startingat.ToUniversalTime().CompareTo(date) > -1)
                            {
                                Subscribers.Add(subscription["user"]["display_name"].ToString(), date);
                            }
                        }
                    }
                    catch
                    {
                    }
                }
            }
            return Subscribers;
        }

        public static List<string> GetAllSubscribers()
        {
            List<string> Subscribers = new List<string>();
            if (Irc.partnered)
            {
                using (WebClient w = new WebClient())
                {
                    w.Proxy = null;
                    try
                    {
                        int count = 100;
                        while (Subscribers.Count < count)
                        {
                            JObject json = JObject.Parse(w.DownloadString("https://api.twitch.tv/kraken/channels/" + Irc.channel.Substring(1) + "/subscriptions?limit=100&offset=" + Subscribers.Count + "&oauth_token=" + Irc.channeltoken));
                            count = Convert.ToInt32(json["_total"]);
                            foreach (JToken subscription in json["subscriptions"])
                            {
                                Subscribers.Add(subscription["user"]["display_name"].ToString());
                            }
                        }
                    }
                    catch
                    {
                    }
                }
            }
            return Subscribers;
        }

        public static bool UpdateMetadata(string title, string game, int delay = 0)
        {
            using (WebClient w = new WebClient())
            {
                w.Proxy = null;
                try
                {
                    w.Headers["Authorization"] = "OAuth " + Irc.channeltoken;
                    w.UploadString("https://api.twitch.tv/kraken/channels/" + Irc.channel.Substring(1) + "?channel[status]=" + title + "&channel[game]=" + game + "&channel[delay]=" + delay, "PUT", "");
                    return true;
                }
                catch
                {
                }
            }
            return false;
        }

        public static string GetSteamCurrentGame()
        {
            using (WebClient w = new WebClient())
            {
                w.Proxy = null;
                try
                {
                    return JObject.Parse(w.DownloadString("http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key=5C18EE317A1E58DD98BCAF4872977055&steamids=" + MainForm.Channel_SteamID64.Text))["response"]["players"][0]["gameextrainfo"].ToString();
                }
                catch { }
            }
            return "";
        }

        public static string GetSteamCurrentServer()
        {
            using (WebClient w = new WebClient())
            {
                w.Proxy = null;
                try
                {
                    return JObject.Parse(w.DownloadString("http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key=5C18EE317A1E58DD98BCAF4872977055&steamids=" + MainForm.Channel_SteamID64.Text))["response"]["players"][0]["gameserverip"].ToString();
                }
                catch { }
            }
            return "";
        }

        public static bool RunCommercial(int length = 30)
        {
            using (WebClient w = new WebClient())
            {
                w.Proxy = null;
                try
                {
                    w.Headers["Authorization"] = "OAuth " + Irc.channeltoken;
                    w.UploadString("https://api.twitch.tv/kraken/channels/" + Irc.channel.Substring(1) + "/commercial?length=" + length, "POST", "");
                    return true;
                }
                catch (WebException e)
                {
                    if (e.Message.Contains("422 Unprocessable Entity"))
                    {
                        //invalid length
                    }
                }
            }
            return false;
        }

        public static List<string> checkSpreadsheetSubs()
        {
            List<string> lSubs = new List<string>();
            string sSubURL = ini.GetValue("Settings", "Subsribers_URL", "");
            if (sSubURL != "")
            {
                string json_data = "";
                using (WebClient w = new WebClient())
                {
                    w.Proxy = null;
                    try
                    {
                        json_data = w.DownloadString(sSubURL);
                        JObject list = JObject.Parse(json_data);
                        foreach (JToken x in list["feed"]["entry"])
                        {
                            lSubs.Add(x["title"]["$t"].ToString().ToLower());
                        }
                    }
                    catch (WebException)
                    {
                        sSubURL = "";
                        Console.WriteLine("A web exception has occurred while checking subscribers from spreadsheet,\r\nAssumed not found.");
                    }
                    catch (Exception e)
                    {
                        Api.LogError("*************Error Message (via checkSpreadsheetSubs()): " + DateTime.Now + "*********************************\r\n" + e + "\r\n");
                    }
                }
            }
            return lSubs;
        }

        public static List<Transaction> UpdateTransactions()
        {
            List<Transaction> Transactions = new List<Transaction>();
            if (Irc.DetailsConfirmed && Irc.donation_clientid != "" && Irc.donation_token != "")
            {
                using (WebClient w = new WebClient())
                {
                    w.Proxy = null;
                    try
                    {
                        int count = 100;
                        while (Transactions.Count < count)
                        {
                            string json_data = w.DownloadString("https://streamtip.com/api/tips?client_id=" + Irc.donation_clientid + "&access_token=" + Irc.donation_token + "&limit=100&offset=" + Transactions.Count);
                            JObject json = JObject.Parse(json_data);
                            count = (int)json["_count"];
                            foreach (JToken transaction in json["tips"])
                            {
                                Transactions.Add(new Transaction(transaction["transactionId"].ToString(), DateTime.Parse(transaction["date"].ToString(), null, System.Globalization.DateTimeStyles.RoundtripKind), transaction["amount"].ToString(), transaction["username"].ToString(), transaction["note"].ToString()));
                            }
                        }
                        Program.Invoke(() =>
                        {
                            if (Irc.DetailsConfirmed)
                            {
                                MainForm.Windows.FromControl(MainForm.DonationsWindow).Button.Enabled = true;
                            }
                        });
                    }
                    catch (Exception e)
                    {
                        if (e.Message.Contains("(401) Unauthorized"))
                        {
                            Console.WriteLine("Stream Tip key is incorrect. Donations checks disabled.");
                            Program.Invoke(() =>
                            {
                                MainForm.Windows.FromControl(MainForm.DonationsWindow).Button.Enabled = false;
                                MainForm.Windows.FromControl(MainForm.DonationsWindow).Button.Text = "Donations\r\n(Disabled)";
                            });
                            Irc.donation_clientid = "";
                            Irc.donation_token = "";
                            return Transactions;
                        }

                        Console.WriteLine("Unable to connect to Stream Tip to check the transactions.");
                        LogError("*************Error Message (via UpdateTransactions()): " + DateTime.Now + "*********************************\r\nUnable to connect to Stream Tip to check the transactions.\r\n" + e + "\r\n");
                    }
                }
            }
            return Transactions;
        }

        public static int CompareTimeWatched(string user)
        {
            return TimeSpan.Compare(Database.getTimeWatched(user), new TimeSpan((int)MainForm.Giveaway_MustWatchDays.Value, (int)MainForm.Giveaway_MustWatchHours.Value, (int)MainForm.Giveaway_MustWatchMinutes.Value, 0));
        }

        public static void LogError(string error, IExtension sender = null)
        {
            if (!error.Contains("System.Threading.ThreadAbortException"))
            {
                for (int attempts = 0; attempts < 10; attempts++)
                {
                    try
                    {
                        using (StreamWriter log = new StreamWriter(@"Data\Logs\Errors.txt", true))
                        {
                            log.WriteLine(error);
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

    public enum LogType
    {
        Command,
        Error,
        Timeout,
        Other
    }

    public static class API
    {
        public class Settings
        {
            private static iniUtil ini;

            /// <summary>
            /// Generates or loads a settings file.
            /// </summary>
            /// <param name="sender">The extension sending the request to generate the settings file.</param>
            /// <param name="filename">The name of the settings file.</param>
            public Settings(IExtension sender, string filename)
            {
                if (sender == null || filename == null || filename == "") throw new Exception("The sender and/or filename may not be null.");

                ini = new iniUtil(AppDomain.CurrentDomain.BaseDirectory + GetDataPath(sender) + @"\Settings\", filename + (!filename.EndsWith(".ini") ? ".ini" : ""), "\r\n");
            }

            /// <summary>
            /// Set the value for a specific key in a section.
            /// </summary>
            /// <param name="section">Section containing the key to write to</param>
            /// <param name="key">Key to insert/update</param>
            /// <param name="keyvalue">Value for the key</param>
            /// <returns>True if OK</returns>
            public bool SetValue(string section, string key, string keyvalue) { return ini.SetValue(section, key, keyvalue); }

            /// <summary>
            /// Gets the value of the specidied key in the specified section, 
            /// If the key doesn't exists returns the default value
            /// </summary>
            /// <param name="section">Section containing the key to read from</param>
            /// <param name="key">Required key</param>
            /// <param name="ifMissing">Value to return in case the key is missing</param>
            /// <returns>string value of the key or missing value</returns>
            public string GetValue(string section, string key, string ifMissing) { return ini.GetValue(section, key, ifMissing); }

            /// <summary>
            /// Returns the NameValueCollection for every key in the section
            /// </summary>
            /// <param name="section">Section name</param>
            /// <returns>NameValueCollection with nake=Key and value=value</returns>
            public NameValueCollection GetSectionKeysvalues(string section) { return ini.GetSectionKeysvalues(section); }

            /// <summary>
            /// Get the names of all sections in .INI
            /// </summary>
            /// <returns>string array with all the key names</returns>
            public string[] GetSectionNames() { return ini.GetSectionNames(); }
        }

        //public static iniUtil ini { get { return ModBot.Program.ini; } }

        public static Dictionary<string, Thread> dCheckingDisplayName { get { return ModBot.Api.dCheckingDisplayName; } }

        public static int GetUnixTimeNow() { return ModBot.Api.GetUnixTimeNow(); }

        public static int GetUnixFromTime(DateTime time) { return ModBot.Api.GetUnixFromTime(time); }

        public static DateTime GetTimeFromUnix(int unix, bool tolocal = true) { return ModBot.Api.GetTimeFromUnix(unix, tolocal); }

        public static bool IsFileLocked(string FileLocation, FileShare fs = FileShare.None) { return ModBot.Api.IsFileLocked(FileLocation, fs); }

        public static string GetDisplayName(string user, bool bWait = false) { return ModBot.Api.GetDisplayName(user, bWait); }

        public static bool IsFollower(string user) { return ModBot.Api.IsFollower(user); }

        public static bool IsSubscriber(string user, bool check = false, bool manual = false) { return ModBot.Api.IsSubscriber(user, check, manual); }

        public static Dictionary<string, DateTime> GetLastSubscribers(DateTime startingat, int count = 25) { return ModBot.Api.GetLastSubscribers(startingat, count); }

        public static List<string> GetAllSubscribers() { return ModBot.Api.GetAllSubscribers(); }

        public static bool UpdateMetadata(string title, string game, int delay = 0) { return ModBot.Api.UpdateMetadata(title, game, delay); }

        public static string GetSteamID64() { return ModBot.Api.MainForm.Channel_SteamID64.Text; }

        public static string GetSteamCurrentGame() { return ModBot.Api.GetSteamCurrentGame(); }

        public static string GetSteamCurrentServer() { return ModBot.Api.GetSteamCurrentServer(); }

        public static bool RunCommercial(int length = 30) { return ModBot.Api.RunCommercial(length); }

        public static List<string> checkSpreadsheetSubs() { return ModBot.Api.checkSpreadsheetSubs(); }

        public static List<Transaction> UpdateTransactions() { return ModBot.Api.UpdateTransactions(); }

        public static int CompareTimeWatched(string user) { return ModBot.Api.CompareTimeWatched(user); }

        public static string GetDataPath(IExtension sender) { if (sender == null) throw new Exception("The sender may not be null."); if (!Directory.Exists(@"Data\Extensions\" + sender.UniqueID)) Directory.CreateDirectory(@"Data\Extensions\" + sender.UniqueID); return @"Data\Extensions\" + sender.UniqueID + @"\"; }

        public static void Log(IExtension sender, LogType type, string output, string filename = "")
        {
            if (sender == null || type == LogType.Other && (filename == null || filename == "")) throw new Exception("The sender and/or filename may not be null (filename may be null if LogType is not Other).");

            if (!Directory.Exists(GetDataPath(sender) + @"\Logs\")) Directory.CreateDirectory(GetDataPath(sender) + @"\Logs\");

            output = "[" + DateTime.Now + "] " + output;

            if (type == LogType.Command) filename = "Commands";
            else if (type == LogType.Error) filename = "Errors";
            else if (type == LogType.Timeout) filename = "Timeouts";

            for (int attempts = 0; attempts < 10; attempts++)
            {
                try
                {
                    using (StreamWriter log = new StreamWriter(GetDataPath(sender) + @"\Logs\" + filename + ".txt", true))
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

        public static class Auction
        {
            public static string highBidder { get { return ModBot.Auction.highBidder; } }
            public static int highBid { get { return ModBot.Auction.highBid; } }
            public static bool Open { get { return ModBot.Auction.Open; } }

            public static void Start() { ModBot.Auction.Start(); }

            public static bool placeBid(string nick, int amount) { return ModBot.Auction.placeBid(nick, amount); }

            public static System.Tuple<string, int> Close() { return ModBot.Auction.Close(); }

            public static void Cancel() { ModBot.Auction.Cancel(); }
        }

        public static class Commands
        {
            public static void Add(string Command, CommandExecutedHandler Handler, int MinLevel = 0, int Delay = 5, bool StreamerNoDelay = true, bool ModNoDelay = true, bool SubNoDelay = false) { ModBot.Commands.Add(Command, Handler, MinLevel, Delay, StreamerNoDelay, ModNoDelay, SubNoDelay); }

            public static void Remove(string Command, CommandExecutedHandler Handler) { ModBot.Commands.Remove(Command, Handler); }

            public static void Remove(string Command) { ModBot.Commands.Remove(Command); }

            public static bool Exists(string Command) { return ModBot.Commands.Exists(Command); }

            public static bool CheckCommand(string user, string message, bool call = false, bool log = true) { return ModBot.Commands.CheckCommand(user, message, call, log); }
        }

        public static class Database
        {
            public static System.Data.SQLite.SQLiteConnection DB { get { return ModBot.Database.DB; } }
            public static MySql.Data.MySqlClient.MySqlConnection MySqlDB { get { return ModBot.Database.MySqlDB; } }
            public static string table { get { return ModBot.Database.table; } }

            public static void newUser(string name, bool bCheckDisplayName = true) { ModBot.Database.newUser(name, bCheckDisplayName); }

            public static List<string> GetAllUsers() { return ModBot.Database.GetAllUsers(); }

            public static void setDisplayName(string user, string name) { ModBot.Database.setDisplayName(user, name); }

            public static string getDisplayName(string user) { return ModBot.Database.getDisplayName(user); }

            public static void setCurrency(string user, int amount) { ModBot.Database.setCurrency(user, amount); }

            public static int checkCurrency(string user) { return ModBot.Database.checkCurrency(user); }

            public static void addCurrency(string user, int amount) { ModBot.Database.addCurrency(user, amount); }

            public static void removeCurrency(string user, int amount) { ModBot.Database.removeCurrency(user, amount); }

            public static bool userExists(string user) { return ModBot.Database.userExists(user); }

            public static string getBtag(string user) { return ModBot.Database.getBtag(user); }

            public static void setBtag(string user, string btag) { ModBot.Database.setBtag(user, btag); }

            public static bool isSubscriber(string user) { return ModBot.Database.isSubscriber(user); }

            public static bool addSub(string user) { return ModBot.Database.addSub(user); }

            public static bool removeSub(string user) { return ModBot.Database.removeSub(user); }

            public static int getUserLevel(string user) { return ModBot.Database.getUserLevel(user); }

            public static void setUserLevel(string user, int level) { ModBot.Database.setUserLevel(user, level); }

            public static TimeSpan getTimeWatched(string user) { return ModBot.Database.getTimeWatched(user); }

            public static void addTimeWatched(string user, int time) { ModBot.Database.addTimeWatched(user, time); }


            public static class Commands
            {
                public static bool cmdExists(string command) { return ModBot.Database.Commands.cmdExists(command); }

                public static void addCommand(string command, int level, string output) { ModBot.Database.Commands.addCommand(command, level, output); }

                public static void removeCommand(string command) { ModBot.Database.Commands.removeCommand(command); }

                public static int LevelRequired(string command) { return ModBot.Database.Commands.LevelRequired(command); }

                public static string getList() { return ModBot.Database.Commands.getList(); }

                public static string getOutput(string command) { return ModBot.Database.Commands.getOutput(command); }
            }
        }

        public static class Giveaway
        {
            public static int LastRoll { get { return ModBot.Giveaway.LastRoll; } }
            public static bool Started { get { return ModBot.Giveaway.Started; } }
            public static bool Open { get { return ModBot.Giveaway.Open; } }
            public static int Cost { get { return ModBot.Giveaway.Cost; } }
            public static int MaxTickets { get { return ModBot.Giveaway.MaxTickets; } }
            public static Dictionary<string, int> Users = ModBot.Giveaway.Users;
            public static float Chance { get { return ModBot.Giveaway.Chance; } }

            public static void startGiveaway(int ticketcost = 0, int maxtickets = 1) { ModBot.Giveaway.startGiveaway(ticketcost, maxtickets); }

            public static void closeGiveaway(bool announce = true, bool open = true) { ModBot.Giveaway.closeGiveaway(announce, open); }

            public static void openGiveaway() { ModBot.Giveaway.openGiveaway(); }

            public static void endGiveaway(bool announce = true) { ModBot.Giveaway.endGiveaway(announce); }

            public static void cancelGiveaway(bool announce = true) { ModBot.Giveaway.cancelGiveaway(announce); }

            public static bool HasBoughtTickets(string user) { return ModBot.Giveaway.HasBoughtTickets(user); }

            public static bool BuyTickets(string user, int tickets = 1) { return ModBot.Giveaway.BuyTickets(user, tickets); }

            public static int GetMinCurrency() { return ModBot.Giveaway.GetMinCurrency(); }

            public static bool CheckUser(string user, bool checkfollow = true, bool checksubscriber = true, bool checktime = true) { return ModBot.Giveaway.CheckUser(user, checkfollow, checksubscriber, checktime); }

            public static string getWinner() { return ModBot.Giveaway.getWinner(); }
        }

        public static class Irc
        {
            public static TcpClient irc { get { return ModBot.Irc.irc; } }
            public static StreamReader read { get { return ModBot.Irc.read; } }
            public static StreamWriter write { get { return ModBot.Irc.write; } }
            public static string nick { get { return ModBot.Irc.nick; } }
            public static string channel { get { return ModBot.Irc.channel; } }
            public static string currency { get { return ModBot.Irc.currency; } }
            public static string currencyName { get { return ModBot.Irc.currencyName; } }
            public static int interval { get { return ModBot.Irc.interval; } }
            public static int payout { get { return ModBot.Irc.payout; } }
            public static int subpayout { get { return ModBot.Irc.subpayout; } }
            public static int StreamStartTime { get { return ModBot.Irc.StreamStartTime; } }
            public static bool partnered { get { return ModBot.Irc.partnered; } }
            public static bool IsStreaming { get { return ModBot.Irc.IsStreaming; } }
            public static bool ResourceKeeper { get { return ModBot.Irc.ResourceKeeper; } }
            public static bool IsModerator { get { return ModBot.Irc.IsModerator; } }
            public static bool DetailsConfirmed { get { return ModBot.Irc.DetailsConfirmed; } }
            public static bool greetingOn { get { return ModBot.Irc.greetingOn; } set { ModBot.Irc.greetingOn = value; } }
            public static string greeting { get { return ModBot.Irc.greeting; } set { ModBot.Irc.greeting = value; } }
            public static int LastCurrencyDisabledAnnounce { get { return ModBot.Irc.LastCurrencyDisabledAnnounce; } set { ModBot.Irc.LastCurrencyDisabledAnnounce = value; } }
            public static int LastUsedCurrencyTop5 { get { return ModBot.Irc.LastUsedCurrencyTop5; } set { ModBot.Irc.LastUsedCurrencyTop5 = value; } }
            public static Dictionary<string, int> ActiveUsers { get { return ModBot.Irc.ActiveUsers; } }
            public static Dictionary<string, int> Warnings { get { return ModBot.Irc.Warnings; } }
            public static Dictionary<string, int> giveawayFalseEntries { get { return ModBot.Irc.giveawayFalseEntries; } }
            public static List<string> IgnoredUsers { get { return ModBot.Irc.IgnoredUsers; } set { ModBot.Irc.IgnoredUsers = value; } }
            public static List<string> Moderators { get { return ModBot.Irc.Moderators; } }
            public static List<string> Subscribers { get { return ModBot.Irc.Subscribers; } }
            public static List<string> Users { get { return ModBot.Irc.Users; } }
            public static Dictionary<string, string> UserColors { get { return ModBot.Irc.UserColors; } }
            public static System.Threading.Timer currencyQueue { get { return ModBot.Irc.currencyQueue; } }
            public static System.Threading.Timer auctionLoop { get { return ModBot.Irc.auctionLoop; } }
            public static System.Threading.Timer giveawayQueue { get { return ModBot.Irc.giveawayQueue; } }
            public static System.Threading.Timer warningsRemoval { get { return ModBot.Irc.warningsRemoval; } }
            public static System.Threading.Timer newViewers { get { return ModBot.Irc.newViewers; } }

            public static void addUserToList(string user, int time = -1, bool welcome = false) { ModBot.Irc.addUserToList(user, time, welcome); }

            public static bool IsUserInList(string user) { return ModBot.Irc.IsUserInList(user); }

            public static void removeUserFromList(string user) { ModBot.Irc.removeUserFromList(user); }

            public static void buildUserList(bool justjoined = true) { ModBot.Irc.buildUserList(justjoined); }

            public static bool sendRaw(string message) { return ModBot.Irc.sendRaw(message); }

            public static void sendMessage(string message, string log = "", bool logtoconsole = true, bool useaction = true) { ModBot.Irc.sendMessage(message, log, logtoconsole, useaction); }

            public static int warnUser(string user, int add = 1, int lengthrate = 5, string reason = "", int max = 3, bool announcewarns = true, bool console = true, bool chat = true, int limit = 0) { return ModBot.Irc.warnUser(user, add, lengthrate, reason, max, announcewarns, console, chat, limit); }

            public static bool timeoutUser(string user, int interval = 10, string reason = "", bool console = true, bool chat = true) { return ModBot.Irc.timeoutUser(user, interval, reason, console, chat); }

            public static string checkBtag(string person) { return ModBot.Irc.checkBtag(person); }

            public static void addToLookups(string user) { ModBot.Irc.addToLookups(user); }
        }

        public static class Pool
        {
            public static List<string> options { get { return ModBot.Pool.options; } }
            public static bool Running { get { return ModBot.Pool.Running; } }
            public static bool Locked { get { return ModBot.Pool.Locked; } set { ModBot.Pool.Locked = value; } }

            public static void CreatePool(int max, List<string> lOptions) { ModBot.Pool.CreatePool(max, lOptions); }

            public static bool placeBet(string nick, string option, int amount) { return ModBot.Pool.placeBet(nick, option, amount); }

            public static void closePool(string winBet) { ModBot.Pool.closePool(winBet); }

            public static void buildWinners(string winBet) { ModBot.Pool.buildWinners(winBet); }

            public static void buildTotalBets() { ModBot.Pool.buildTotalBets(); }

            public static int getTotalBets() { return ModBot.Pool.getTotalBets(); }

            public static int getNumberOfBets(string bet) { return ModBot.Pool.getNumberOfBets(bet); }

            public static int getTotalBetsOn(string bet) { return ModBot.Pool.getTotalBetsOn(bet); }

            public static Dictionary<string, int> getWinners() { return ModBot.Pool.getWinners(); }

            public static int getBetAmount(string nick) { return ModBot.Pool.getBetAmount(nick); }

            public static string getBetOn(string nick) { return ModBot.Pool.getBetOn(nick); }

            public static void cancel() { ModBot.Pool.cancel(); }

            public static void Clear() { ModBot.Pool.Clear(); }

            public static bool isInPool(string nick) { return ModBot.Pool.isInPool(nick); }

            public static string GetOptionFromNumber(int optionnumber) { return ModBot.Pool.GetOptionFromNumber(optionnumber); }

            public static List<string> buildBetOptions(string[] temp) { return ModBot.Pool.buildBetOptions(temp); }
        }

        public static class Program
        {
            public static void HideConsole() { ModBot.Program.HideConsole(); }

            public static void ShowConsole() { ModBot.Program.ShowConsole(); }

            public static void FocusConsole() { ModBot.Program.FocusConsole(); }

            public static bool IsConsoleHidden() { return ModBot.Program.IsConsoleHidden(); }

            //public static void Invoke(Action method) { ModBot.Program.Invoke(method); }
        }

        public static class UI
        {
            public static void AddWindow(string name, Form form, bool RequiresConnection = true, bool RequiresMod = false, bool RequiresPartership = false, bool ControlManually = false, string AlternativeName = "", CheckBox Button = null, Font Font = null) { ModBot.Program.AddToMainWindow(name, form, RequiresConnection, RequiresMod, RequiresPartership, ControlManually, AlternativeName, Button, Font); }
        }

        public static class Events
        {
            public static event OnUILoaded OnUILoaded { add { ModBot.Program.OnUILoaded += value; } remove { ModBot.Program.OnUILoaded -= value; } }

            public static event OnConnect OnConnect { add { ModBot.Irc.OnConnect += value; } remove { ModBot.Irc.OnConnect -= value; } }

            public static event Connected Connected { add { ModBot.Irc.Connected += value; } remove { ModBot.Irc.Connected -= value; } }

            public static event OnMessageReceived OnMessageReceived { add { ModBot.Irc.OnMessageReceived += value; } remove { ModBot.Irc.OnMessageReceived -= value; } }

            public static event OnDisconnect OnDisconnect { add { ModBot.Irc.OnDisconnect += value; } remove { ModBot.Irc.OnDisconnect -= value; } }

            public static event Disconnected Disconnected { add { ModBot.Irc.Disconnected += value; } remove { ModBot.Irc.Disconnected -= value; } }
        }
    }
}