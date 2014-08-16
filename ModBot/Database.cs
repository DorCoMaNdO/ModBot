using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;

namespace ModBot
{
    public static class Database
    {
        public static SQLiteConnection DB;
        private static string channel;

        public static void Initialize()
        {
            if (DB == null)
            {
                if (!File.Exists("ModBot.sqlite"))
                {
                    SQLiteConnection.CreateFile("ModBot.sqlite");
                }

                while (ModBot.Api.IsFileLocked("ModBot.sqlite", FileShare.Read) && File.Exists("ModBot.sqlite"))
                {
                    if (MessageBox.Show("ModBot's database file is in use, Please close it in order to let ModBot use it.", "ModBot Updater", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning) == DialogResult.Cancel) Environment.Exit(0);
                }

                if (!File.Exists("ModBot.sqlite"))
                {
                    SQLiteConnection.CreateFile("ModBot.sqlite");
                }

                DB = new SQLiteConnection("Data Source=ModBot.sqlite;Version=3;");
                DB.Open();

                using (SQLiteCommand query = new SQLiteCommand("CREATE TABLE IF NOT EXISTS 'commands' (id INTEGER PRIMARY KEY, command TEXT, level INTEGER DEFAULT 0, output TEXT DEFAULT null);", DB))
                {
                    query.ExecuteNonQuery();
                }
            }
        }

        public static void CreateTable()
        {
            Console.WriteLine("Setting up the database...");

            channel = Irc.channel.Substring(1);

            using (SQLiteCommand query = new SQLiteCommand("CREATE TABLE IF NOT EXISTS '" + channel + "' (id INTEGER PRIMARY KEY, user TEXT, currency INTEGER DEFAULT 0, subscriber INTEGER DEFAULT 0, btag TEXT DEFAULT null, userlevel INTEGER DEFAULT 0, display_name TEXT DEFAULT null, time_watched INTEGER DEFAULT 0);", DB))
            {
                query.ExecuteNonQuery();
            }

            // Handle old users
            using (SQLiteCommand query = new SQLiteCommand("SELECT display_name FROM '" + channel + "';", DB))
            {
                try
                {
                    query.ExecuteNonQuery();
                    /*using (query = new SQLiteCommand("SELECT * FROM '" + channel + "';", myDB))
                    {
                        using (SQLiteDataReader r = query.ExecuteReader())
                        {
                            while (r.Read())
                            {
                                if (r["display_name"].ToString() == "")
                                {
                                    Console.WriteLine(r["user"].ToString());
                                    Api.GetDisplayName(r["user"].ToString(), true);
                                }
                            }
                        }
                    }*/
                }
                catch (SQLiteException)
                {
                    using (SQLiteCommand query2 = new SQLiteCommand("ALTER TABLE '" + channel + "' ADD COLUMN display_name TEXT DEFAULT null;", DB))
                    {
                        query2.ExecuteNonQuery();
                    }
                }
            }

            using (SQLiteCommand query = new SQLiteCommand("SELECT time_watched FROM '" + channel + "';", DB))
            {
                try
                {
                    query.ExecuteNonQuery();
                }
                catch (SQLiteException)
                {
                    using (SQLiteCommand query2 = new SQLiteCommand("ALTER TABLE '" + channel + "' ADD COLUMN time_watched INTEGER DEFAULT 0;", DB))
                    {
                        query2.ExecuteNonQuery();
                    }
                }
            }

            if (tableExists("transfers") && !tableHasData(channel))
            {
                using (SQLiteCommand query = new SQLiteCommand("INSERT INTO '" + channel + "' SELECT * FROM transfers;", DB))
                {
                    query.ExecuteNonQuery();
                }

                /*using (SQLiteCommand query = new SQLiteCommand("DROP TABLE transfers;", myDB))
                {
                    query.ExecuteNonQuery();
                }*/
            }

            //DB.Close();

            /*Commands.Add("test", new CommandExecutedHandler((string command, string[] args) =>
            {
                if (args != null) Console.WriteLine("Command : " + command + " Args : " + args[0]); else Console.WriteLine("Command : " + command);
                System.Threading.Thread.Sleep(5000);
                Console.WriteLine("Test 2");
            }));*/

            Console.WriteLine("Database set.\r\n");
        }

        public static void newUser(string user, bool bCheckDisplayName = true)
        {
            user = Api.capName(user);
            if (!userExists(user))
            {
                using (SQLiteCommand query = new SQLiteCommand("INSERT INTO '" + channel + "' (user) VALUES ('" + user + "');", DB))
                {
                    query.ExecuteNonQuery();
                }
                if (bCheckDisplayName)
                {
                    Api.GetDisplayName(user);
                }
            }
        }

        public static List<string> GetAllUsers()
        {
            List<string> users = new List<string>();
            using (SQLiteCommand query = new SQLiteCommand("SELECT * FROM '" + channel + "';", DB))
            {
                using (SQLiteDataReader r = query.ExecuteReader())
                {
                    while (r.Read())
                    {
                        if (users.Contains(r["user"].ToString()))
                        {
                            continue;
                        }
                        users.Add(r["user"].ToString());
                    }
                }
            }
            return users;
        }

        public static void setDisplayName(string user, string name)
        {
            user = Api.capName(user);
            if (!userExists(user))
            {
                newUser(user, false);
            }
            using (SQLiteCommand query = new SQLiteCommand("UPDATE '" + channel + "' SET display_name = '" + name + "' WHERE user = '" + user + "';", DB))
            {
                query.ExecuteNonQuery();
            }
        }

        public static string getDisplayName(string user)
        {
            user = Api.capName(user);
            if (userExists(user))
            {
                using (SQLiteCommand query = new SQLiteCommand("SELECT * FROM '" + channel + "' WHERE user = '" + user + "';", DB))
                {
                    using (SQLiteDataReader r = query.ExecuteReader())
                    {
                        if (r.Read())
                        {
                            return r["display_name"].ToString();
                        }
                        else return "";
                    }
                }
            }
            else
            {
                newUser(user);
                return "";
            }
        }

        public static void setCurrency(string user, int amount)
        {
            user = Api.capName(user);
            if (amount < 0)
            {
                amount = 0;
            }
            if (!userExists(user))
            {
                newUser(user);
            }
            using (SQLiteCommand query = new SQLiteCommand("UPDATE '" + channel + "' SET currency = " + amount + " WHERE user = '" + user + "';", DB))
            {
                query.ExecuteNonQuery();
            }
        }

        public static int checkCurrency(string user)
        {
            user = Api.capName(user);
            if (userExists(user)) {
                using (SQLiteCommand query = new SQLiteCommand("SELECT * FROM '" + channel + "' WHERE user = '" + user + "';", DB))
                {
                    using (SQLiteDataReader r = query.ExecuteReader())
                    {
                        if (r.Read())
                        {
                            //Console.WriteLine("1: " + r["currency"].ToString());
                            int currency = int.Parse(r["currency"].ToString());
                            if (currency < 0)
                            {
                                setCurrency(user, 0);
                                return 0;
                            }
                            return currency;
                        }
                        else return 0;
                    }
                }
            }
            else {
                newUser(user);
                return 0;
            }
        }

        public static void addCurrency(string user, int amount)
        {
            user = Api.capName(user);
            if (amount < 0)
            {
                amount = -amount;
            }
            if (!userExists(user))
            {
                newUser(user);
            }
            using (SQLiteCommand query = new SQLiteCommand("UPDATE '" + channel + "' SET currency = currency + " + amount + " WHERE user = '" + user + "';", DB))
            {
                query.ExecuteNonQuery();
            }
        }

        public static void removeCurrency(string user, int amount)
        {
            user = Api.capName(user);
            if (amount < 0)
            {
                amount = -amount;
            }
            if (amount > checkCurrency(user))
            {
                amount = checkCurrency(user);
            }
            if (!userExists(user))
            {
                newUser(user);
            }
            using (SQLiteCommand query = new SQLiteCommand("UPDATE '" + channel + "' SET currency = currency - " + amount + " WHERE user = '" + user + "';", DB))
            {
                query.ExecuteNonQuery();
            }
        }

        public static bool userExists(string user)
        {
            user = Api.capName(user);
            using (SQLiteCommand query = new SQLiteCommand("SELECT * FROM '" + channel + "';", DB))
            {
                using (SQLiteDataReader r = query.ExecuteReader())
                {
                    while (r.Read())
                    {
                        if (r["user"].ToString().Equals(user))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public static string getBtag(string user)
        {
            user = Api.capName(user);
            if (userExists(user))
            {
                using (SQLiteCommand query = new SQLiteCommand("SELECT * FROM '" + channel + "' WHERE user = '" + user + "';", DB))
                {
                    using (SQLiteDataReader r = query.ExecuteReader())
                    {
                        if (r.Read())
                        {
                            //Console.WriteLine(r["btag"]);
                            if (System.DBNull.Value.Equals(r["btag"]))
                            {
                                //Console.WriteLine("btag is null");
                                return null;
                            }
                            else return r["btag"].ToString();
                        }
                    }
                }
            }
            else {
                newUser(user);
            }
            return null;
        }

        public static void setBtag(string user, string btag)
        {
            user = Api.capName(user);
            if (!userExists(user))
            {
                newUser(user);
            }
            using (SQLiteCommand query = new SQLiteCommand("UPDATE '" + channel + "' SET btag = '" + btag + "' WHERE user = '" + user + "';", DB))
            {
                query.ExecuteNonQuery();
            }
        }

        public static bool isSubscriber(string user)
        {
            user = Api.capName(user);
            if (!userExists(user))
            {
                newUser(user);
            }
            else
            {
                using (SQLiteCommand query = new SQLiteCommand("SELECT * FROM '" + channel + "' WHERE user = '" + user + "';", DB))
                {
                    using (SQLiteDataReader r = query.ExecuteReader())
                    {
                        if (r.Read())
                        {
                            if (int.Parse(r["subscriber"].ToString()) == 1)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public static bool addSub(string user)
        {
            user = Api.capName(user);
            if (userExists(user))
            {
                using (SQLiteCommand query = new SQLiteCommand("UPDATE '" + channel + "' SET subscriber = 1 WHERE user = '" + user + "';", DB))
                {
                    query.ExecuteNonQuery();
                    return true;
                }
            }
            return false;
        }

        public static bool removeSub(string user)
        {
            user = Api.capName(user);
            if (userExists(user))
            {
                using (SQLiteCommand query = new SQLiteCommand("UPDATE '" + channel + "' SET subscriber = 0 WHERE user = '" + user + "';", DB))
                {
                    query.ExecuteNonQuery();
                }
                return true;
            }
            return false;
        }

        public static int getUserLevel(string user)
        {
            user = Api.capName(user);
            if (!userExists(user))
            {
                newUser(user);
            }
            else
            {
                using (SQLiteCommand query = new SQLiteCommand("SELECT * FROM '" + channel + "' WHERE user = '" + user + "';", DB))
                {
                    using (SQLiteDataReader r = query.ExecuteReader())
                    {
                        if (r.Read())
                        {
                            return Convert.ToInt32(r["userlevel"].ToString());
                        }
                    }
                }
            }
            return 0;
        }

        public static void setUserLevel(string user, int level)
        {
            user = Api.capName(user);
            if(!userExists(user))
            {
                newUser(user);
            }
            using (SQLiteCommand query = new SQLiteCommand("UPDATE '" + channel + "' SET userlevel = " + level + " WHERE user = '" + user + "';", DB))
            {
                query.ExecuteNonQuery();
            }
        }

        public static TimeSpan getTimeWatched(string user)
        {
            user = Api.capName(user);
            if (!userExists(user))
            {
                newUser(user);
            }
            else
            {
                using (SQLiteCommand query = new SQLiteCommand("SELECT * FROM '" + channel + "' WHERE user = '" + user + "';", DB))
                {
                    using (SQLiteDataReader r = query.ExecuteReader())
                    {
                        if (r.Read())
                        {
                            return TimeSpan.FromMinutes(Convert.ToInt32(r["time_watched"].ToString()));
                        }
                    }
                }
            }
            return new TimeSpan();
        }

        public static void addTimeWatched(string user, int time)
        {
            user = Api.capName(user);
            if (!userExists(user))
            {
                newUser(user);
            }
            using (SQLiteCommand query = new SQLiteCommand("UPDATE '" + channel + "' SET time_watched = time_watched + " + time + " WHERE user = '" + user + "';", DB))
            {
                query.ExecuteNonQuery();
            }
        }

        private static bool tableExists(string table)
        {
            try
            {
                using (SQLiteCommand query = new SQLiteCommand("SELECT COUNT(*) FROM sqlite_master WHERE name = '" + table + "';", DB))
                {
                    using (SQLiteDataReader r = query.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            if (int.Parse(r["COUNT(*)"].ToString()) != 0)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            catch (SQLiteException e)
            {
                Console.WriteLine(e);
            }
            return false;
        }

        private static bool tableHasData(string table)
        {
            using (SQLiteCommand query = new SQLiteCommand("SELECT * FROM '" + table + "';", DB))
            {
                using (SQLiteDataReader r = query.ExecuteReader())
                {
                    if (r.HasRows)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
    }
}