using ModBot;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.SQLite;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace CoMaNdO.UserRanks
{
    [Export(typeof(IExtension))]
    public class UserRanks : IExtension
    {
        private string LatestVersion;
        private SQLiteConnection DB;

        public void Load()
        {
            while (File.Exists(Api.GetDataPath(this) + "Data.sqlite") && Api.IsFileLocked(Api.GetDataPath(this) + "Data.sqlite", FileShare.Read)) if (MessageBox.Show("ModBot's database file is in use, Please close it in order to let ModBot use it.", "ModBot", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning) == DialogResult.Cancel) Program.Close();

            if (!File.Exists(Api.GetDataPath(this) + "Data.sqlite")) SQLiteConnection.CreateFile(Api.GetDataPath(this) + "Data.sqlite");

            DB = new SQLiteConnection(@"Data Source=" + Api.GetDataPath(this) + "Data.sqlite;Version=3;");
            DB.Open();

            using (SQLiteCommand query = new SQLiteCommand("CREATE TABLE IF NOT EXISTS 'ranks' (id INTEGER PRIMARY KEY AUTOINCREMENT, rank TEXT);", DB)) query.ExecuteNonQuery();
            using (SQLiteCommand query = new SQLiteCommand("CREATE TABLE IF NOT EXISTS 'userdata' (id INTEGER PRIMARY KEY AUTOINCREMENT, user TEXT, rank TEXT);", DB)) query.ExecuteNonQuery();

            Events.Connected += Events_Connected;
            Events.Currency.OnQueue += Events_OnCurrencyQueue;
        }

        private void Events_Connected(string channel, string nick, bool partnered)
        {
            Commands.Add("!ranks", Command_Ranks, 3, 0);
        }

        private void Command_Ranks(string user, string command, string[] args)
        {
            if (args.Length > 0)
            {
                if (args[0].ToLower() == "add" && args.Length > 1)
                {
                    string rank = "";
                    for (int i = 1; i < args.Length; i++)
                    {
                        rank += args[i] + " ";
                    }
                    
                    if (rank != "")
                    {
                        rank = rank.Substring(0, rank.Length - 1);

                        bool found = false;
                        using (SQLiteCommand query = new SQLiteCommand("SELECT * FROM 'ranks';", DB))
                        {
                            using (SQLiteDataReader r = query.ExecuteReader())
                            {
                                while (r.Read())
                                {
                                    if (r["rank"].ToString().ToLower() == rank.ToLower())
                                    {
                                        found = true;
                                        break;
                                    }
                                }
                            }
                        }

                        if (!found)
                        {
                            using (SQLiteCommand query = new SQLiteCommand("INSERT INTO 'ranks' (rank) VALUES ('" + rank.Replace("'", "''") + "');", DB)) query.ExecuteNonQuery();
                            Chat.SendMessage("The rank \"" + rank + "\" has been added.");
                        }
                        else
                        {
                            Chat.SendMessage("The rank \"" + rank + "\" already exists.");
                        }
                    }
                }
                else if (args[0].ToLower() == "remove" && args.Length > 1)
                {
                    string rank = "";
                    for (int i = 1; i < args.Length; i++)
                    {
                        rank += args[i] + " ";
                    }

                    if (rank != "")
                    {
                        rank = rank.Substring(0, rank.Length - 1);

                        bool found = false;
                        using (SQLiteCommand query = new SQLiteCommand("SELECT * FROM 'ranks';", DB))
                        {
                            using (SQLiteDataReader r = query.ExecuteReader())
                            {
                                while (r.Read())
                                {
                                    if (r["rank"].ToString().ToLower() == rank.ToLower())
                                    {
                                        found = true;
                                        break;
                                    }
                                }
                            }
                        }

                        if (found)
                        {
                            using (SQLiteCommand query = new SQLiteCommand("DELETE FROM 'ranks' WHERE rank = '" + rank.Replace("'", "''") + "' COLLATE NOCASE;", DB)) query.ExecuteNonQuery();
                            Chat.SendMessage("The rank \"" + rank + "\" has been removed.");
                        }
                        else
                        {
                            Chat.SendMessage("The rank \"" + rank + "\" does not exist.");
                        }
                    }
                }
                else if (args[0].ToLower() == "list")
                {
                    string output = "List of ranks: ";
                    using (SQLiteCommand query = new SQLiteCommand("SELECT * FROM 'ranks';", DB))
                    {
                        using (SQLiteDataReader r = query.ExecuteReader())
                        {
                            while (r.Read())
                            {
                                output += r["rank"].ToString() + ", ";
                            }
                        }
                    }

                    if (output.EndsWith(", "))
                    {
                        output = output.Substring(0, output.Length - 2) + ".";
                    }
                    else
                    {
                        output = "No ranks were added.";
                    }

                    Chat.SendMessage(output);
                }
            }
        }

        private void Events_OnCurrencyQueue(string user, ref string output, bool showtime)
        {
            string rank = "";

            List<string> Ranks = new List<string>();
            using (SQLiteCommand query = new SQLiteCommand("SELECT * FROM 'ranks';", DB))
            {
                using (SQLiteDataReader r = query.ExecuteReader())
                {
                    while (r.Read())
                    {
                        Ranks.Add(r["rank"].ToString());
                    }
                }
            }

            if (Ranks.Count == 0) return;

            bool found = false;
            using (SQLiteCommand query = new SQLiteCommand("SELECT * FROM 'userdata' WHERE user = '" + user + "' COLLATE NOCASE;", DB))
            {
                using (SQLiteDataReader r = query.ExecuteReader())
                {
                    while (r.Read())
                    {
                        if (r["user"].ToString().ToLower() == user.ToLower())
                        {
                            foreach(string Rank in Ranks)
                            {
                                if (Rank.ToLower() == r["rank"].ToString().ToLower())
                                {
                                    found = true;
                                    break;
                                }
                            }
                            
                            if (found)
                            {
                                rank = r["rank"].ToString();
                            }
                            else
                            {
                                using (SQLiteCommand query2 = new SQLiteCommand("DELETE FROM 'userdata' WHERE user = '" + user + "' COLLATE NOCASE;", DB)) query2.ExecuteNonQuery();
                            }

                            break;
                        }
                    }
                }
            }

            if (!found)
            {
                rank = Ranks[new Random().Next(0, Ranks.Count)];

                using (SQLiteCommand query = new SQLiteCommand("INSERT INTO 'userdata' (user, rank) VALUES ('" + user + "', '" + rank.Replace("'", "''") + "');", DB)) query.ExecuteNonQuery();
            }
            
            if(rank != "") output = "[" + rank + "]" + output;
        }

        public string Name { get { return "User Ranks"; } }
        public string FileName { get { return "CoMaNdO.UserRanks.dll"; } }
        public string Author { get { return "CoMaNdO"; } }
        public string UniqueID { get { return "CoMaNdO.UserRanks"; } }
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
}
