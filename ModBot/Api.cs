using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.IO;

namespace ModBot
{
    public static class Api
    {
        private static MainWindow MainForm = Irc.MainForm;
        private static Dictionary<string, Thread> g_lCheckingDisplayName = new Dictionary<string, Thread>();

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
            if (!g_lCheckingDisplayName.ContainsKey(user))
            {
                if (Database.getDisplayName(user) == "")
                {
                    Thread thread = new Thread(
                    () =>
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
                            catch (SocketException)
                            {
                            }
                            catch (Exception)
                            {
                            }
                        }
                        if (g_lCheckingDisplayName.ContainsKey(user))
                        {
                            g_lCheckingDisplayName.Remove(user);
                        }
                    });
                    if (!g_lCheckingDisplayName.ContainsKey(user))
                    {
                        g_lCheckingDisplayName.Add(user, thread);
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
                    g_lCheckingDisplayName[user].Join();
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
                        catch (SocketException)
                        {
                        }
                        catch (Exception)
                        {
                        }
                    }
                });
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
                    string json_data = "";
                    try
                    {
                        w.Proxy = null;
                        json_data = w.DownloadString("https://www.streamdonations.net/api/donations?channel=" + Irc.channel.Substring(1).ToLower() + "&key=" + Irc.donationkey);
                        while (json_data.Contains("\"DT_RowId\""))
                        {
                            string date = "Donated at some point", name = "Unknown", amount = "0.00", notes = "", transaction = "";
                            json_data = json_data.Substring(json_data.IndexOf("\"0\":") + 4);
                            if (!json_data.StartsWith("null"))
                            {
                                name = json_data.Substring(1, json_data.IndexOf("\",") - 1);
                            }
                            json_data = json_data.Substring(json_data.IndexOf(",\"1\":") + 4);
                            if (!json_data.StartsWith("null"))
                            {
                                notes = json_data.Substring(2, json_data.IndexOf("\",") - 2).Replace("&lt;", "<").Replace("&gt;", ">");
                            }
                            json_data = json_data.Substring(json_data.IndexOf(",\"2\":") + 4);
                            if (!json_data.StartsWith("null"))
                            {
                                date = json_data.Substring(2, json_data.IndexOf("\",") - 2);
                            }
                            json_data = json_data.Substring(json_data.IndexOf(",\"3\":") + 4);
                            if (!json_data.StartsWith("null"))
                            {
                                amount = json_data.Substring(2, json_data.IndexOf("\",") - 2);
                            }

                            json_data = json_data.Substring(json_data.IndexOf("\"DT_RowId\":\"") + 12);
                            transaction = json_data.Substring(0, json_data.IndexOf("\""));
                            Transactions.Add(new Transaction(transaction, date, amount, name, notes));
                        }
                    }
                    catch (SocketException)
                    {
                        Console.WriteLine("Unable to connect to Stream Dontaions to check the transactions.");
                    }
                    catch (Exception e)
                    {
                        StreamWriter errorLog = new StreamWriter("Error_Log.log", true);
                        errorLog.WriteLine("*************Error Message (via UpdateTransactions()): " + DateTime.Now + "*********************************\r\nUnable to connect to Stream Dontaions to check the transactions.\r\n" + e + "\r\n");
                        errorLog.Close();
                    }
                }
            }
            return Transactions;
        }
    }
}
