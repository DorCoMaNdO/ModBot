using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading;

namespace ModBot
{
    public class Api
    {
        private MainWindow MainForm;
        public List<string> Followers = new List<string>();

        public Api(MainWindow MainForm)
        {
            this.MainForm = MainForm;
        }

        public int GetUnixTimeNow()
        {
            return (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }

        public bool IsFollowingChannel(String user)
        {
            bool bFollowing = false;
            Thread thread = new Thread(
                () =>
                {
                    using (WebClient w = new WebClient())
                    {
                        String json_data = "";
                        try
                        {
                            w.Proxy = null;
                            json_data = w.DownloadString("https://api.twitch.tv/kraken/users/" + user + "/follows/channels/" + MainForm.IRC.channel.Substring(1));
                            if (json_data.Contains("\"" + MainForm.IRC.channel.Substring(1) + "\""))
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

        public List<Transaction> UpdateTransactions()
        {
            List<Transaction> Transactions = new List<Transaction>();
            using (WebClient w = new WebClient())
            {
                String json_data = "";
                try
                {
                    w.Proxy = null;
                    json_data = w.DownloadString("https://www.streamdonations.net/api/donations?channel=" + MainForm.IRC.channel.Substring(1).ToLower() + "&key=" + MainForm.IRC.donationkey);
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
            return Transactions;
        }
    }
}
