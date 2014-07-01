using System;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.IO;
using System.Collections.Generic;

namespace ModBot
{
    public class Database
    {
        private SQLiteConnection myDB;
        private SQLiteCommand cmd;
        private string channel;

        public Database(Irc IRC)
        {
            channel = IRC.admin;
            IRC.db = this;
            InitializeDB();
        }

        private void InitializeDB()
        {
            if (File.Exists("ModBot.sqlite"))
            {
                myDB = new SQLiteConnection("Data Source=ModBot.sqlite;Version=3;");
                myDB.Open();

                using (cmd = new SQLiteCommand("CREATE TABLE IF NOT EXISTS '" + channel + "' (id INTEGER PRIMARY KEY, user TEXT, currency INTEGER DEFAULT 0, subscriber INTEGER DEFAULT 0, btag TEXT DEFAULT null, userlevel INTEGER DEFAULT 0, display_name TEXT DEFAULT null);", myDB))
                {
                    cmd.ExecuteNonQuery();
                }

                using (cmd = new SQLiteCommand("SELECT display_name FROM '" + channel + "';", myDB))
                {
                    try
                    {
                        cmd.ExecuteNonQuery();
                        /*using (cmd = new SQLiteCommand("SELECT * FROM '" + channel + "';", myDB))
                        {
                            using (SQLiteDataReader r = cmd.ExecuteReader())
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
                    catch(SQLiteException)
                    {
                        using (cmd = new SQLiteCommand("ALTER TABLE '" + channel + "' ADD COLUMN display_name TEXT DEFAULT null;", myDB))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                if (tableExists("transfers") && !tableHasData(channel))
                {
                    using (cmd = new SQLiteCommand("INSERT INTO '" + channel + "' SELECT * FROM transfers;", myDB))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    /*sql = "DROP TABLE transfers;";

                    using (cmd = new SQLiteCommand(sql, myDB))
                    {
                        cmd.ExecuteNonQuery();
                    }*/
                }
                
            }
            else
            {
                SQLiteConnection.CreateFile("ModBot.sqlite");
                myDB = new SQLiteConnection("Data Source=ModBot.sqlite;Version=3;");
                myDB.Open();

                using (cmd = new SQLiteCommand("CREATE TABLE IF NOT EXISTS '" + channel + "' (id INTEGER PRIMARY KEY, user TEXT, currency INTEGER DEFAULT 0, subscriber INTEGER DEFAULT 0, btag TEXT DEFAULT null, userlevel INTEGER DEFAULT 0, display_name TEXT DEFAULT null);", myDB))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void newUser(String user, bool bCheckDisplayName=true)
        {
            user = Api.capName(user);
            if (!userExists(user))
            {
                using (cmd = new SQLiteCommand("INSERT INTO '" + channel + "' (user) VALUES ('" + user + "');", myDB))
                {
                    cmd.ExecuteNonQuery();
                }
                if (bCheckDisplayName)
                {
                    Api.GetDisplayName(user);
                }
            }
        }

        public List<string> GetAllUsers()
        {
            List<string> users = new List<string>();
            using (cmd = new SQLiteCommand("SELECT * FROM '" + channel + "';", myDB))
            {
                using (SQLiteDataReader r = cmd.ExecuteReader())
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

        public void setDisplayName(String user, string name)
        {
            user = Api.capName(user);
            if (!userExists(user))
            {
                newUser(user, false);
            }
            using (cmd = new SQLiteCommand("UPDATE '" + channel + "' SET display_name = '" + name + "' WHERE user = '" + user + "';", myDB))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public string getDisplayName(String user)
        {
            user = Api.capName(user);
            if (userExists(user))
            {
                using (cmd = new SQLiteCommand("SELECT * FROM '" + channel + "' WHERE user = '" + user + "';", myDB))
                {
                    using (SQLiteDataReader r = cmd.ExecuteReader())
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

        public void setCurrency(String user, int amount)
        {
            user = Api.capName(user);
            if (!userExists(user))
            {
                newUser(user);
            }
            using (cmd = new SQLiteCommand("UPDATE '" + channel + "' SET currency = " + amount + " WHERE user = '" + user + "';", myDB))
            {
                cmd.ExecuteNonQuery();
            }

        }

        public int checkCurrency(String user)
        {
            user = Api.capName(user);
            if (userExists(user)) {
                using (cmd = new SQLiteCommand("SELECT * FROM '" + channel + "' WHERE user = '" + user + "';", myDB))
                {
                    using (SQLiteDataReader r = cmd.ExecuteReader())
                    {
                        if (r.Read())
                        {
                            //Console.WriteLine("1: " + r["currency"].ToString());
                            return int.Parse(r["currency"].ToString());
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

        public void addCurrency(String user, int amount)
        {
            user = Api.capName(user);
            if (!userExists(user))
            {
                newUser(user);
            }
            using (cmd = new SQLiteCommand("UPDATE '" + channel + "' SET currency = currency + " + amount + " WHERE user = '" + user + "';", myDB))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public void removeCurrency(String user, int amount)
        {
            user = Api.capName(user);
            if (!userExists(user))
            {
                newUser(user);
            }
            using (cmd = new SQLiteCommand("UPDATE '" + channel + "' SET currency = currency - " + amount + " WHERE user = '" + user + "';", myDB))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public bool userExists(String user)
        {
            user = Api.capName(user);
            using (cmd = new SQLiteCommand("SELECT * FROM '" + channel + "';", myDB))
            {
                using (SQLiteDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        if (r["user"].ToString().Equals(user, StringComparison.OrdinalIgnoreCase))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public String getBtag(String user)
        {
            user = Api.capName(user);
            if (userExists(user))
            {
                using (cmd = new SQLiteCommand("SELECT * FROM '" + channel + "' WHERE user = '" + user + "';", myDB))
                {
                    using (SQLiteDataReader r = cmd.ExecuteReader())
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

        public void setBtag(String user, String btag)
        {
            user = Api.capName(user);
            if (!userExists(user))
            {
                newUser(user);
            }
            using (cmd = new SQLiteCommand("UPDATE '" + channel + "' SET btag = '" + btag + "' WHERE user = '" + user + "';", myDB))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public bool isSubscriber(String user)
        {
            user = Api.capName(user);
            if (!userExists(user))
            {
                newUser(user);
            }
            else
            {
                using (cmd = new SQLiteCommand("SELECT * FROM '" + channel + "' WHERE user = '" + user + "';", myDB))
                {
                    using (SQLiteDataReader r = cmd.ExecuteReader())
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

        public bool addSub(String user)
        {
            user = Api.capName(user);
            if (userExists(user))
            {
                using (cmd = new SQLiteCommand("UPDATE '" + channel + "' SET subscriber = 1 WHERE user = '" + user + "';", myDB))
                {
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            return false;
        }

        public bool removeSub(String user)
        {
            user = Api.capName(user);
            if (userExists(user))
            {
                using (cmd = new SQLiteCommand("UPDATE '" + channel + "' SET subscriber = 0 WHERE user = '" + user + "';", myDB))
                {
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            return false;
        }

        public int getUserLevel(String user)
        {
            user = Api.capName(user);
            if (!userExists(user))
            {
                newUser(user);
            }
            else
            {
                using (cmd = new SQLiteCommand("SELECT * FROM '" + channel + "' WHERE user = '" + user + "';", myDB))
                {
                    using (SQLiteDataReader r = cmd.ExecuteReader())
                    {
                        if (r.Read())
                        {
                            int level;
                            if (int.TryParse(r["userlevel"].ToString(), out level))
                            {
                                return level;
                            }
                        }
                    }
                }
            }
            return 0;
        }

        public void setUserLevel(String user, int level)
        {
            user = Api.capName(user);
            using (cmd = new SQLiteCommand("UPDATE '" + channel + "' SET userlevel = " + level + " WHERE user = '" + user + "';", myDB))
            {
                cmd.ExecuteNonQuery();
            }
        }

        private bool tableExists(String table)
        {
            try
            {
                using (cmd = new SQLiteCommand("SELECT COUNT(*) FROM sqlite_master WHERE name = '" + table + "';", myDB))
                {
                    using (SQLiteDataReader r = cmd.ExecuteReader())
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

        private bool tableHasData(String table)
        {
            using (cmd = new SQLiteCommand("SELECT * FROM '" + table + "';", myDB))
            {
                using (SQLiteDataReader r = cmd.ExecuteReader())
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