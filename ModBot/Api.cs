using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.IO;

namespace ModBot
{
    public static class Api
    {
        private static MainWindow MainForm = Program.MainForm;
        public static Dictionary<string, Thread> dCheckingDisplayName = new Dictionary<string, Thread>();
        private static iniUtil ini = Program.ini;
        private static StreamWriter errorLog = new StreamWriter("Error_Log.txt", true);

        public static int GetUnixTimeNow()
        {
            return (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
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
                            string json_data = "";
                            try
                            {
                                w.Proxy = null;
                                json_data = w.DownloadString("https://api.twitch.tv/kraken/users/" + user);
                                Database.setDisplayName(user, JObject.Parse(json_data)["display_name"].ToString());
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
            /*else
            {
                if(bWait)
                {
                    for(int i = 0; i < 20; i++)
                    {
                        if (!g_dDisplayNames.ContainsKey(user))
                        {
                            Thread.Sleep(100);
                        }
                    }
                }
            }*/
            if (Database.getDisplayName(user) == "")
            {
                return capName(user);
            }
            return Database.getDisplayName(user);
        }

        public static string capName(string name)
        {
            if(name.Length < 2)
            {
                return name;
            }
            return char.ToUpper(name[0]) + name.Substring(1).ToLower();
        }

        public static bool IsFollower(string user)
        {
            user = user.ToLower();
            bool bFollower = false;
            Thread thread = new Thread(
                () =>
                {
                    using (WebClient w = new WebClient())
                    {
                        try
                        {
                            w.Proxy = null;
                            string sData = w.DownloadString("https://api.twitch.tv/kraken/users/" + user + "/follows/channels/" + Irc.channel.Substring(1));
                            bFollower = !sData.Contains("is not following");
                        }
                        catch
                        {
                        }
                    }
                });
            thread.Name = "Follower check for " + user;
            thread.Start();
            thread.Join();
            return bFollower;
        }

        public static bool IsSubscriber(string user)
        {
            user = user.ToLower();
            bool bSubscriber = false;
            Thread thread = new Thread(
                () =>
                {
                    using (WebClient w = new WebClient())
                    {
                        try
                        {
                            w.Proxy = null;
                            string sData = w.DownloadString("https://api.twitch.tv/kraken/users/" + Irc.channel.Substring(1) + "/subscriptions/channels/" + user + "?oauth_token=");
                            bSubscriber = !sData.Contains("Token invalid or missing required scope");
                        }
                        catch
                        {
                        }
                    }
                });
            thread.Name = "Subscriber check for " + user;
            thread.Start();
            thread.Join();
            return bSubscriber;
        }

        public static List<string> checkSpreadsheetSubs()
        {
            List<string> lSubs = new List<string>();
            string sSubURL = ini.GetValue("Settings", "Subsribers_URL", "");
            if (sSubURL != "")
            {
                Thread thread = new Thread(() =>
                {
                    string json_data = "";
                    using (WebClient w = new WebClient())
                    {
                        try
                        {
                            w.Proxy = null;
                            json_data = w.DownloadString(sSubURL);
                            JObject list = JObject.Parse(json_data);
                            foreach (JToken x in list["feed"]["entry"])
                            {
                                lSubs.Add(Api.capName(x["title"]["$t"].ToString()));
                            }
                        }
                        catch (Exception e)
                        {
                            Api.LogError("*************Error Message (via checkSpreadsheetSubs()): " + DateTime.Now + "*********************************\r\n" + e + "\r\n");
                        }
                    }
                });
                Irc.Threads.Add(thread);
                thread.Name = "Update subscribers from spreadsheet";
                thread.Start();
                thread.Join();
                if (Irc.Threads.Contains(thread)) Irc.Threads.Remove(thread);
            }
            return lSubs;
        }

        public static List<Transaction> UpdateTransactions()
        {
            List<Transaction> Transactions = new List<Transaction>();
            if (Irc.donationkey != "")
            {
                using (WebClient w = new WebClient())
                {
                    try
                    {
                        w.Proxy = null;
                        string json_data = w.DownloadString("https://www.streamdonations.net/api/donations?channel=" + Irc.channel.Substring(1) + "&key=" + Irc.donationkey);
                        if (json_data.Contains("Not a valid channel and/or key"))
                        {
                            Console.WriteLine("Stream Donations key is incorrect. Donations checks disabled.");
                            Irc.donationkey = "";
                            return Transactions;
                        }
                        foreach (JToken transaction in JObject.Parse(json_data)["aaData"])
                        {
                            Transactions.Add(new Transaction(transaction["4"].ToString(), transaction["2"].ToString(), transaction["3"].ToString(), transaction["0"].ToString(), transaction["1"].ToString()));
                        }
                        /*int count = 100;
                        while (Transactions.Count < count)
                        {
                            string json_data = w.DownloadString("https://streamtip.com/api/tips?client_id=" + Irc.donation_clientid + "&access_token=" + Irc.donation_token + "&limit=100&offset=" + Transactions.Count);
                            if (json_data == "{\"status\":401,\"message\":\"Unauthorized\"}")
                            {
                                Console.WriteLine("Stream Tip key is incorrect. Donations checks disabled.");
                                Irc.donationkey = "";
                                return Transactions;
                            }

                            JObject json = JObject.Parse(json_data);
                            count = (int)json["_count"];
                            foreach (JToken transaction in json["tips"])
                            {
                                Transactions.Add(new Transaction(transaction["transactionId"].ToString(), DateTime.Parse(transaction["date"].ToString(), null, System.Globalization.DateTimeStyles.RoundtripKind).ToString(), transaction["amount"].ToString(), transaction["username"].ToString(), transaction["note"].ToString()));
                            }
                        }*/
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Unable to connect to Stream Donations to check the transactions.");
                        LogError("*************Error Message (via UpdateTransactions()): " + DateTime.Now + "*********************************\r\nUnable to connect to Stream Dontaions to check the transactions.\r\n" + e + "\r\n");
                    }
                }
            }
            return Transactions;
        }

        public static void LogError(string error)
        {
            if (!error.Contains("System.Threading.ThreadAbortException"))
            {
                for (int attempts = 0; attempts < 5; attempts++)
                {
                    try
                    {
                        errorLog.WriteLine(error);
                        break;
                    }
                    catch (IOException)
                    {
                        System.Threading.Thread.Sleep(100);
                    }
                }
            }
        }
    }
}
