using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;

namespace ModBot
{
    public static class Api
    {
        public static MainWindow MainForm = Program.MainForm;
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
                            w.Proxy = null;
                            string json_data = "";
                            try
                            {
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
            if (Database.getDisplayName(user) == "")
            {
                return capName(user);
            }
            return Database.getDisplayName(user);
        }

        public static string capName(string name)
        {
            if(name == null || name.Length < 2)
            {
                return name;
            }
            return char.ToUpper(name[0]) + name.Substring(1).ToLower();
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
                    //bFollower = !sData.Contains("is not following");
                    bFollower = true;
                }
                catch
                {
                }
            }
            return bFollower;
        }

        public static bool IsSubscriber(string user)
        {
            bool bSubscriber = false;
            if (Irc.partnered)
            {
                using (WebClient w = new WebClient())
                {
                    w.Proxy = null;
                    try
                    {
                        string sData = w.DownloadString("https://api.twitch.tv/kraken/users/" + Irc.channel.Substring(1) + "/subscriptions/" + user.ToLower() + "?oauth_token=" + Irc.channeltoken);
                        //bSubscriber = (!sData.Contains("Token invalid or missing required scope"));
                        bSubscriber = true;
                    }
                    catch
                    {
                    }
                }
            }
            return bSubscriber;
        }

        public static bool UpdateTitleGame(string title, string game, int delay=0)
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
                    if(e.Message.Contains("422 Unprocessable Entity"))
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
                            lSubs.Add(Api.capName(x["title"]["$t"].ToString()));
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
                                Transactions.Add(new Transaction(transaction["transactionId"].ToString(), DateTime.Parse(transaction["date"].ToString(), null, System.Globalization.DateTimeStyles.RoundtripKind).ToString(), transaction["amount"].ToString(), transaction["username"].ToString(), transaction["note"].ToString()));
                            }
                        }
                        MainForm.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate
                        {
                            if (Irc.DetailsConfirmed)
                            {
                                MainForm.DonationsWindowButton.Enabled = true;
                            }
                        });
                    }
                    catch (Exception e)
                    {
                        if (e.Message.Contains("(401) Unauthorized"))
                        {
                            Console.WriteLine("Stream Tip key is incorrect. Donations checks disabled.");
                            MainForm.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate
                            {
                                MainForm.DonationsWindowButton.Enabled = false;
                                MainForm.DonationsWindowButton.Text = "Donations\r\n(Disabled)";
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
