using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace ModBot
{
    public static class Database
    {
        private static MainWindow MainForm;
        public static SQLiteConnection DB;
        public static MySqlConnection MySqlDB;
        private static string channel;

        public static void Initialize()
        {
            MainForm = Program.MainForm;
            
            if (DB != null) DB.Close();
            if (MySqlDB != null) MySqlDB.Close();

            Console.WriteLine("Setting up the database...");

            channel = Irc.channel.Substring(1);

            if (MainForm.MySQL_Host.Text == "" || MainForm.MySQL_Database.Text == "" || MainForm.MySQL_Username.Text == "")
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

                using (SQLiteCommand query = new SQLiteCommand("CREATE TABLE IF NOT EXISTS 'commands' (id INTEGER PRIMARY KEY AUTOINCREMENT, command TEXT, level INTEGER DEFAULT 0, output TEXT DEFAULT null);", DB)) query.ExecuteNonQuery();

                using (SQLiteCommand query = new SQLiteCommand("CREATE TABLE IF NOT EXISTS " + channel + " (id INTEGER PRIMARY KEY AUTOINCREMENT, user TEXT, currency INTEGER DEFAULT 0, subscriber INTEGER DEFAULT 0, btag TEXT DEFAULT null, userlevel INTEGER DEFAULT 0, display_name TEXT DEFAULT null, time_watched INTEGER DEFAULT 0);", DB)) query.ExecuteNonQuery();

                // Handle old users
                using (SQLiteCommand query = new SQLiteCommand("SELECT display_name FROM " + channel + ";", DB))
                {
                    try
                    {
                        query.ExecuteNonQuery();
                        /*using (query = new SQLiteCommand("SELECT * FROM " + channel + ";", myDB))
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
                        using (SQLiteCommand query2 = new SQLiteCommand("ALTER TABLE " + channel + " ADD COLUMN display_name TEXT DEFAULT null;", DB)) query2.ExecuteNonQuery();
                    }
                }

                using (SQLiteCommand query = new SQLiteCommand("SELECT time_watched FROM " + channel + ";", DB))
                {
                    try
                    {
                        query.ExecuteNonQuery();
                    }
                    catch (SQLiteException)
                    {
                        using (SQLiteCommand query2 = new SQLiteCommand("ALTER TABLE " + channel + " ADD COLUMN time_watched INTEGER DEFAULT 0;", DB)) query2.ExecuteNonQuery();
                    }
                }

                //if (tableExists("transfers") && !tableHasData(channel))
                //{
                //    using (SQLiteCommand query = new SQLiteCommand("INSERT INTO " + channel + " SELECT * FROM transfers;", DB)) query.ExecuteNonQuery();

                //    /*using (SQLiteCommand query = new SQLiteCommand("DROP TABLE transfers;", myDB))
                //    {
                //        query.ExecuteNonQuery();
                //    }*/
                //}

                //DB.Close();

                /*Commands.Add("test", new CommandExecutedHandler((string command, string[] args) =>
                {
                    if (args != null) Console.WriteLine("Command : " + command + " Args : " + args[0]); else Console.WriteLine("Command : " + command);
                    System.Threading.Thread.Sleep(5000);
                    Console.WriteLine("Test 2");
                }));*/
            }
            else
            {
                Console.WriteLine("Connecting to MySQL server...");
                try
                {
                    MySqlDB = new MySqlConnection("Server=" + MainForm.MySQL_Host.Text + ";Port=" + MainForm.MySQL_Port.Value + ";Database=" + MainForm.MySQL_Database.Text + ";Uid=" + MainForm.MySQL_Username.Text + ";Pwd=" + MainForm.MySQL_Password.Text + ";");
                    MySqlDB.Open();

                    Console.WriteLine("Connected to MySQL server.");

                    using (MySqlCommand query = new MySqlCommand("CREATE TABLE IF NOT EXISTS commands (id INTEGER PRIMARY KEY AUTO_INCREMENT, command TEXT, level INTEGER DEFAULT 0, output TEXT DEFAULT null);", MySqlDB)) query.ExecuteNonQuery();

                    using (MySqlCommand query = new MySqlCommand("CREATE TABLE IF NOT EXISTS " + channel + " (id INTEGER PRIMARY KEY AUTO_INCREMENT, user TEXT, currency INTEGER DEFAULT 0, subscriber INTEGER DEFAULT 0, btag TEXT DEFAULT null, userlevel INTEGER DEFAULT 0, display_name TEXT DEFAULT null, time_watched INTEGER DEFAULT 0);", MySqlDB)) query.ExecuteNonQuery();

                    /*if (tableExists("transfers") && !tableHasData(channel))
                    {
                        using (MySqlCommand query = new MySqlCommand("INSERT INTO " + channel + " SELECT * FROM transfers;", MySqlDB)) query.ExecuteNonQuery();
                    }*/
                }
                catch (MySqlException e)
                {
                    Console.WriteLine(e);
                    Console.WriteLine(e.Number);
                    MainForm.BeginInvoke((MethodInvoker)delegate
                    {
                        if (e.Number == 1042)
                        {
                            Console.WriteLine(MainForm.SettingsErrorLabel.Text = "Unable to connect to MySQL server.\r\n");
                        }
                        else if (e.Number == 0 || e.Number == 1045)
                        {
                            Console.WriteLine(MainForm.SettingsErrorLabel.Text = "Incorrect MySQL login details.\r\n");
                        }
                    });

                    MySqlDB = null;

                    Thread.Sleep(10);

                    return;
                }
            }

            Console.WriteLine("Database set.\r\n");
        }

        public static void newUser(string user, bool bCheckDisplayName = true)
        {
            if (user == "") return;
            user = Api.capName(user);
            if (!userExists(user))
            {
                if (DB != null)
                {
                    using (SQLiteCommand query = new SQLiteCommand("INSERT INTO " + channel + " (user) VALUES ('" + user + "');", DB)) query.ExecuteNonQuery();
                }
                else if (MySqlDB != null)
                {
                    using (MySqlCommand query = new MySqlCommand("INSERT INTO " + channel + " (user) VALUES ('" + user + "');", MySqlDB)) query.ExecuteNonQuery();
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
            if (DB != null)
            {
                using (SQLiteCommand query = new SQLiteCommand("SELECT * FROM " + channel + ";", DB))
                {
                    using (SQLiteDataReader r = query.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            if (!users.Contains(r["user"].ToString())) users.Add(r["user"].ToString());
                        }
                    }
                }
            }
            else if (MySqlDB != null)
            {
                using (MySqlCommand query = new MySqlCommand("SELECT * FROM " + channel + ";", MySqlDB))
                {
                    using (MySqlDataReader r = query.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            if (!users.Contains(r["user"].ToString())) users.Add(r["user"].ToString());
                        }
                    }
                }
            }
            return users;
        }

        public static void setDisplayName(string user, string name)
        {
            user = Api.capName(user);
            if (!userExists(user)) newUser(user, false);
            if (DB != null)
            {
                using (SQLiteCommand query = new SQLiteCommand("UPDATE " + channel + " SET display_name = '" + name + "' WHERE user = '" + user + "';", DB)) query.ExecuteNonQuery();
            }
            else if (MySqlDB != null)
            {
                using (MySqlCommand query = new MySqlCommand("UPDATE " + channel + " SET display_name = '" + name + "' WHERE user = '" + user + "';", MySqlDB)) query.ExecuteNonQuery();
            }
        }

        public static string getDisplayName(string user)
        {
            user = Api.capName(user);
            if (userExists(user))
            {
                if (DB != null)
                {
                    using (SQLiteCommand query = new SQLiteCommand("SELECT * FROM " + channel + " WHERE user = '" + user + "';", DB))
                    {
                        using (SQLiteDataReader r = query.ExecuteReader())
                        {
                            if (r.Read())
                            {
                                return r["display_name"].ToString();
                            }
                        }
                    }
                }
                else if (MySqlDB != null)
                {
                    using (MySqlCommand query = new MySqlCommand("SELECT * FROM " + channel + " WHERE user = '" + user + "';", MySqlDB))
                    {
                        using (MySqlDataReader r = query.ExecuteReader())
                        {
                            if (r.Read())
                            {
                                return r["display_name"].ToString();
                            }
                        }
                    }
                }
            }
            else
            {
                newUser(user);
            }
            return "";
        }

        public static void setCurrency(string user, int amount)
        {
            user = Api.capName(user);
            if (amount < 0) amount = 0;
            if (!userExists(user)) newUser(user, false);
            if (DB != null)
            {
                using (SQLiteCommand query = new SQLiteCommand("UPDATE " + channel + " SET currency = " + amount + " WHERE user = '" + user + "';", DB)) query.ExecuteNonQuery();
            }
            else if (MySqlDB != null)
            {
                using (MySqlCommand query = new MySqlCommand("UPDATE " + channel + " SET currency = " + amount + " WHERE user = '" + user + "';", MySqlDB)) query.ExecuteNonQuery();
            }
        }

        public static int checkCurrency(string user)
        {
            user = Api.capName(user);
            if (userExists(user))
            {
                if (DB != null)
                {
                    using (SQLiteCommand query = new SQLiteCommand("SELECT * FROM " + channel + " WHERE user = '" + user + "';", DB))
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
                        }
                    }
                }
                else if (MySqlDB != null)
                {
                    using (MySqlCommand query = new MySqlCommand("SELECT * FROM " + channel + " WHERE user = '" + user + "';", MySqlDB))
                    {
                        using (MySqlDataReader r = query.ExecuteReader())
                        {
                            if (r.Read())
                            {
                                int currency = int.Parse(r["currency"].ToString());
                                if (currency < 0)
                                {
                                    setCurrency(user, 0);
                                    return 0;
                                }
                                return currency;
                            }
                        }
                    }
                }
            }
            else
            {
                newUser(user);
            }
            return 0;
        }

        public static void addCurrency(string user, int amount)
        {
            user = Api.capName(user);
            if (amount < 0) amount = -amount;
            if (!userExists(user)) newUser(user, false);
            if (DB != null)
            {
                using (SQLiteCommand query = new SQLiteCommand("UPDATE " + channel + " SET currency = currency + " + amount + " WHERE user = '" + user + "';", DB)) query.ExecuteNonQuery();
            }
            else if (MySqlDB != null)
            {
                using (MySqlCommand query = new MySqlCommand("UPDATE " + channel + " SET currency = currency + " + amount + " WHERE user = '" + user + "';", MySqlDB)) query.ExecuteNonQuery();
            }
        }

        public static void removeCurrency(string user, int amount)
        {
            user = Api.capName(user);
            if (amount < 0) amount = -amount;
            if (amount > checkCurrency(user)) amount = checkCurrency(user);
            if (!userExists(user)) newUser(user, false);
            if (DB != null)
            {
                using (SQLiteCommand query = new SQLiteCommand("UPDATE " + channel + " SET currency = currency - " + amount + " WHERE user = '" + user + "';", DB)) query.ExecuteNonQuery();
            }
            else if (MySqlDB != null)
            {
                using (MySqlCommand query = new MySqlCommand("UPDATE " + channel + " SET currency = currency - " + amount + " WHERE user = '" + user + "';", MySqlDB)) query.ExecuteNonQuery();
            }
        }

        public static bool userExists(string user)
        {
            user = Api.capName(user);
            try
            {
                if (DB != null)
                {
                    using (SQLiteCommand query = new SQLiteCommand("SELECT * FROM " + channel + " WHERE user = '" + user + "';", DB))
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
                }
                else if (MySqlDB != null)
                {
                    using (MySqlCommand query = new MySqlCommand("SELECT * FROM " + channel + " WHERE user = '" + user + "';", MySqlDB))
                    {
                        using (MySqlDataReader r = query.ExecuteReader())
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
                }
            }
            catch
            {
            }
            return false;
        }

        public static string getBtag(string user)
        {
            user = Api.capName(user);
            if (userExists(user))
            {
                if (DB != null)
                {
                    using (SQLiteCommand query = new SQLiteCommand("SELECT * FROM " + channel + " WHERE user = '" + user + "';", DB))
                    {
                        using (SQLiteDataReader r = query.ExecuteReader())
                        {
                            if (r.Read())
                            {
                                /*//Console.WriteLine(r["btag"]);
                                if (System.DBNull.Value.Equals(r["btag"]))
                                {
                                    //Console.WriteLine("btag is null");
                                    return null;
                                }
                                else return r["btag"].ToString();*/
                                return r["btag"].ToString();
                            }
                        }
                    }
                }
                else if (MySqlDB != null)
                {
                    using (MySqlCommand query = new MySqlCommand("SELECT * FROM " + channel + " WHERE user = '" + user + "';", MySqlDB))
                    {
                        using (MySqlDataReader r = query.ExecuteReader())
                        {
                            if (r.Read())
                            {
                                return r["btag"].ToString();
                            }
                        }
                    }
                }
            }
            else
            {
                newUser(user);
            }
            return "";
        }

        public static void setBtag(string user, string btag)
        {
            user = Api.capName(user);
            if (!userExists(user)) newUser(user, false);
            if (DB != null)
            {
                using (SQLiteCommand query = new SQLiteCommand("UPDATE " + channel + " SET btag = '" + btag + "' WHERE user = '" + user + "';", DB)) query.ExecuteNonQuery();
            }
            else if (MySqlDB != null)
            {
                using (MySqlCommand query = new MySqlCommand("UPDATE " + channel + " SET btag = '" + btag + "' WHERE user = '" + user + "';", MySqlDB)) query.ExecuteNonQuery();
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
                if (DB != null)
                {
                    using (SQLiteCommand query = new SQLiteCommand("SELECT * FROM " + channel + " WHERE user = '" + user + "';", DB))
                    {
                        using (SQLiteDataReader r = query.ExecuteReader())
                        {
                            if (r.Read())
                            {
                                return (int.Parse(r["subscriber"].ToString()) == 1);
                            }
                        }
                    }
                }
                else if (MySqlDB != null)
                {
                    using (MySqlCommand query = new MySqlCommand("SELECT * FROM " + channel + " WHERE user = '" + user + "';", MySqlDB))
                    {
                        using (MySqlDataReader r = query.ExecuteReader())
                        {
                            if (r.Read())
                            {
                                return (int.Parse(r["subscriber"].ToString()) == 1);
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
                if (DB != null)
                {
                    using (SQLiteCommand query = new SQLiteCommand("UPDATE " + channel + " SET subscriber = 1 WHERE user = '" + user + "';", DB)) query.ExecuteNonQuery();
                }
                else if (MySqlDB != null)
                {
                    using (MySqlCommand query = new MySqlCommand("UPDATE " + channel + " SET subscriber = 1 WHERE user = '" + user + "';", MySqlDB)) query.ExecuteNonQuery();
                }
                return true;
            }
            return false;
        }

        public static bool removeSub(string user)
        {
            user = Api.capName(user);
            if (userExists(user))
            {
                if (DB != null)
                {
                    using (SQLiteCommand query = new SQLiteCommand("UPDATE " + channel + " SET subscriber = 0 WHERE user = '" + user + "';", DB)) query.ExecuteNonQuery();
                }
                else if (MySqlDB != null)
                {
                    using (MySqlCommand query = new MySqlCommand("UPDATE " + channel + " SET subscriber = 0 WHERE user = '" + user + "';", MySqlDB)) query.ExecuteNonQuery();
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
                if (DB != null)
                {
                    using (SQLiteCommand query = new SQLiteCommand("SELECT * FROM " + channel + " WHERE user = '" + user + "';", DB))
                    {
                        using (SQLiteDataReader r = query.ExecuteReader())
                        {
                            if (r.Read())
                            {
                                return int.Parse(r["userlevel"].ToString());
                            }
                        }
                    }
                }
                else if (MySqlDB != null)
                {
                    using (MySqlCommand query = new MySqlCommand("SELECT * FROM " + channel + " WHERE user = '" + user + "';", MySqlDB))
                    {
                        using (MySqlDataReader r = query.ExecuteReader())
                        {
                            if (r.Read())
                            {
                                return int.Parse(r["userlevel"].ToString());
                            }
                        }
                    }
                }
            }
            return 0;
        }

        public static void setUserLevel(string user, int level)
        {
            user = Api.capName(user);
            if (!userExists(user)) newUser(user, false);
            if (DB != null)
            {
                using (SQLiteCommand query = new SQLiteCommand("UPDATE " + channel + " SET userlevel = " + level + " WHERE user = '" + user + "';", DB)) query.ExecuteNonQuery();
            }
            else if (MySqlDB != null)
            {
                using (MySqlCommand query = new MySqlCommand("UPDATE " + channel + " SET userlevel = " + level + " WHERE user = '" + user + "';", MySqlDB)) query.ExecuteNonQuery();
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
                if (DB != null)
                {
                    using (SQLiteCommand query = new SQLiteCommand("SELECT * FROM " + channel + " WHERE user = '" + user + "';", DB))
                    {
                        using (SQLiteDataReader r = query.ExecuteReader())
                        {
                            if (r.Read())
                            {
                                return TimeSpan.FromMinutes(int.Parse(r["time_watched"].ToString()));
                            }
                        }
                    }
                }
                else if (MySqlDB != null)
                {
                    using (MySqlCommand query = new MySqlCommand("SELECT * FROM " + channel + " WHERE user = '" + user + "';", MySqlDB))
                    {
                        using (MySqlDataReader r = query.ExecuteReader())
                        {
                            if (r.Read())
                            {
                                return TimeSpan.FromMinutes(int.Parse(r["time_watched"].ToString()));
                            }
                        }
                    }
                }
            }
            return new TimeSpan();
        }

        public static void addTimeWatched(string user, int time)
        {
            user = Api.capName(user);
            if (!userExists(user)) newUser(user, false);
            if (DB != null)
            {
                using (SQLiteCommand query = new SQLiteCommand("UPDATE " + channel + " SET time_watched = time_watched + " + time + " WHERE user = '" + user + "';", DB)) query.ExecuteNonQuery();
            }
            else if (MySqlDB != null)
            {
                using (MySqlCommand query = new MySqlCommand("UPDATE " + channel + " SET time_watched = time_watched + " + time + " WHERE user = '" + user + "';", MySqlDB)) query.ExecuteNonQuery();
            }
        }

        /*private static bool tableExists(string table)
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
                using (MySqlCommand query = new MySqlCommand("SHOW TABLES LIKE '" + table + "';", MySqlDB))
                {
                    using (MySqlDataReader r = query.ExecuteReader())
                    {
                        return r.HasRows;
                    }
                }
            }
            catch (SQLiteException e)
            {
                Console.WriteLine(e);
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e);
            }
            return false;
        }

        private static bool tableHasData(string table)
        {
            if (DB != null)
            {
                using (SQLiteCommand query = new SQLiteCommand("SELECT * FROM '" + table + "';", DB))
                {
                    using (SQLiteDataReader r = query.ExecuteReader())
                    {
                        return r.HasRows;
                    }
                }
            }
            else if (MySqlDB != null)
            {
                using (MySqlCommand query = new MySqlCommand("SELECT * FROM '" + table + "';", MySqlDB))
                {
                    using (MySqlDataReader r = query.ExecuteReader())
                    {
                        return r.HasRows;
                    }
                }
            }
            return false;
        }*/

        public static class Commands
        {
            public static bool cmdExists(string command)
            {
                if (DB != null)
                {
                    using (SQLiteCommand query = new SQLiteCommand("SELECT * FROM commands", DB))
                    {
                        using (SQLiteDataReader r = query.ExecuteReader())
                        {
                            while (r.Read())
                            {
                                if (r["command"].ToString().Equals(command, StringComparison.OrdinalIgnoreCase))
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
                else if (MySqlDB != null)
                {
                    using (MySqlCommand query = new MySqlCommand("SELECT * FROM commands", MySqlDB))
                    {
                        using (MySqlDataReader r = query.ExecuteReader())
                        {
                            while (r.Read())
                            {
                                if (r["command"].ToString().Equals(command, StringComparison.OrdinalIgnoreCase))
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
                return false;
            }

            public static void addCommand(string command, int level, string output)
            {
                if (DB != null)
                {
                    using (SQLiteCommand query = new SQLiteCommand("INSERT INTO commands (command, level, output) VALUES ('" + command + "', " + level + ", '" + output + "');", DB)) query.ExecuteNonQuery();
                }
                else if (MySqlDB != null)
                {
                    using (MySqlCommand query = new MySqlCommand("INSERT INTO commands (command, level, output) VALUES ('" + command + "', " + level + ", '" + output + "');", MySqlDB)) query.ExecuteNonQuery();
                }
            }

            public static void removeCommand(string command)
            {
                if (DB != null)
                {
                    using (SQLiteCommand query = new SQLiteCommand("DELETE FROM commands WHERE command = '" + command + "';", DB)) query.ExecuteNonQuery();
                }
                else if (MySqlDB != null)
                {
                    using (MySqlCommand query = new MySqlCommand("DELETE FROM commands WHERE command = '" + command + "';", MySqlDB)) query.ExecuteNonQuery();
                }
            }

            public static int LevelRequired(string command)
            {
                if (DB != null)
                {
                    using (SQLiteCommand query = new SQLiteCommand("SELECT * FROM commands WHERE command = '" + command + "';", DB))
                    {
                        using (SQLiteDataReader r = query.ExecuteReader())
                        {
                            while (r.Read())
                            {
                                return int.Parse(r["level"].ToString());
                            }
                        }
                    }
                }
                else if (MySqlDB != null)
                {
                    using (MySqlCommand query = new MySqlCommand("SELECT * FROM commands WHERE command = '" + command + "';", MySqlDB))
                    {
                        using (MySqlDataReader r = query.ExecuteReader())
                        {
                            while (r.Read())
                            {
                                return int.Parse(r["level"].ToString());
                            }
                        }
                    }
                }
                return 0;
            }

            public static string getList()
            {
                string commands = "";
                if (DB != null)
                {
                    using (SQLiteCommand query = new SQLiteCommand("SELECT * FROM commands;", DB))
                    {
                        using (SQLiteDataReader r = query.ExecuteReader())
                        {
                            while (r.Read())
                            {
                                commands += r["command"].ToString() + ", ";
                            }
                        }
                    }
                }
                else if (MySqlDB != null)
                {
                    using (MySqlCommand query = new MySqlCommand("SELECT * FROM commands;", MySqlDB))
                    {
                        using (MySqlDataReader r = query.ExecuteReader())
                        {
                            while (r.Read())
                            {
                                commands += r["command"].ToString() + ", ";
                            }
                        }
                    }
                }
                return commands.Substring(0, commands.Length - 2);
            }

            public static string getOutput(string command)
            {
                if (DB != null)
                {
                    using (SQLiteCommand query = new SQLiteCommand("SELECT * FROM commands WHERE command = '" + command + "';", DB))
                    {
                        using (SQLiteDataReader r = query.ExecuteReader())
                        {
                            while (r.Read())
                            {
                                return r["output"].ToString();
                            }
                        }
                    }
                }
                else if (MySqlDB != null)
                {
                    using (MySqlCommand query = new MySqlCommand("SELECT * FROM commands WHERE command = '" + command + "';", MySqlDB))
                    {
                        using (MySqlDataReader r = query.ExecuteReader())
                        {
                            while (r.Read())
                            {
                                return r["output"].ToString();
                            }
                        }
                    }
                }
                return "";
            }
        }
    }
}