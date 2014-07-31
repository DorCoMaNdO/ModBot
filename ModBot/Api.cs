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

        public static bool IsFollowingChannel(string user)
        {
            user = user.ToLower();
            bool bFollowing = false;
            Thread thread = new Thread(
                () =>
                {
                    using (WebClient w = new WebClient())
                    {
                        string sData = "";
                        try
                        {
                            w.Proxy = null;
                            sData = w.DownloadString("https://api.twitch.tv/kraken/users/" + user + "/follows/channels/" + Irc.admin);
                            if (sData.Contains("\"" + Irc.admin + "\""))
                            {
                                bFollowing = true;
                            }
                        }
                        catch
                        {
                        }
                    }
                });
            thread.Name = "Follower check for " + user;
            thread.Start();
            thread.Join();
            return bFollowing;
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
                        string data = w.DownloadString("https://www.streamdonations.net/api/donations?channel=" + Irc.channel.Substring(1) + "&key=" + Irc.donationkey);
                        while (data.Contains("\"DT_RowId\""))
                        {
                            string date = "Donated at some point", name = "Unknown", amount = "0.00", notes = "", transaction = "";
                            data = data.Substring(data.IndexOf("\"0\":") + 4);
                            if (!data.StartsWith("null"))
                            {
                                name = data.Substring(1, data.IndexOf("\",") - 1);
                            }
                            data = data.Substring(data.IndexOf(",\"1\":") + 4);
                            if (!data.StartsWith("null"))
                            {
                                notes = data.Substring(2, data.IndexOf("\",") - 2).Replace("&lt;", "<").Replace("&gt;", ">");
                            }
                            data = data.Substring(data.IndexOf(",\"2\":") + 4);
                            if (!data.StartsWith("null"))
                            {
                                date = data.Substring(2, data.IndexOf("\",") - 2);
                            }
                            data = data.Substring(data.IndexOf(",\"3\":") + 4);
                            if (!data.StartsWith("null"))
                            {
                                amount = data.Substring(2, data.IndexOf("\",") - 2);
                            }

                            data = data.Substring(data.IndexOf("\"DT_RowId\":\"") + 12);
                            transaction = data.Substring(0, data.IndexOf("\""));
                            Transactions.Add(new Transaction(transaction, date, amount, name, notes));
                        }
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
