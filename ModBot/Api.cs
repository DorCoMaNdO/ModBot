using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading;

namespace ModBot
{
    public static class Api
    {
        private static MainWindow MainForm;
        private static Irc IRC;
        public static Dictionary<string, string> g_dDisplayNames = new Dictionary<string, string>();
        private static Dictionary<string, Thread> g_lCheckingDisplayName = new Dictionary<string, Thread>();

        public static void SetMainForm(MainWindow Form)
        {
            MainForm = Form;
            IRC = MainForm.IRC;
        }

        public static int GetUnixTimeNow()
        {
            return (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }

        public static string GetDisplayName(string user, bool bWait = false)
        {
            user = user.ToLower();
            if (!g_lCheckingDisplayName.ContainsKey(user))
            {
                if (!g_dDisplayNames.ContainsKey(user))
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
                                JObject stream = JObject.Parse(json_data);
                                g_dDisplayNames.Add(user, stream["display_name"].ToString());
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
                    g_lCheckingDisplayName.Add(user, thread);
                    thread.Start();
                    if (bWait)
                    {
                        thread.Join();
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
            if (!g_dDisplayNames.ContainsKey(user))
            {
                return capName(user);
            }
            return g_dDisplayNames[user];
        }

        public static string capName(string name)
        {
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
                            sData = w.DownloadString("https://api.twitch.tv/kraken/users/" + user + "/follows/channels/" + IRC.admin);
                            if (sData.Contains("\"" + IRC.admin + "\""))
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
            if (IRC.donationkey != "")
            {
                using (WebClient w = new WebClient())
                {
                    string json_data = "";
                    try
                    {
                        w.Proxy = null;
                        json_data = w.DownloadString("https://www.streamdonations.net/api/donations?channel=" + IRC.admin.ToLower() + "&key=" + IRC.donationkey);
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
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            return Transactions;
        }
    }
}
