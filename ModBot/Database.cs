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
        private Api api;
        private string channel;

        public Database(Irc IRC)
        {
            channel = IRC.admin;
            api = IRC.api;
            InitializeDB();
        }

        private void InitializeDB()
        {
            if (File.Exists("ModBot.sqlite"))
            {
                myDB = new SQLiteConnection("Data Source=ModBot.sqlite;Version=3;");
                myDB.Open();
                String sql;
                sql = "CREATE TABLE IF NOT EXISTS '" + channel + "' (id INTEGER PRIMARY KEY, user TEXT, currency INTEGER DEFAULT 0, subscriber INTEGER DEFAULT 0, btag TEXT DEFAULT null, userlevel INTEGER DEFAULT 0);";


                using (cmd = new SQLiteCommand(sql, myDB))
                {
                    cmd.ExecuteNonQuery();
                }

                if (tableExists("transfers") && !tableHasData(channel))
                {
                    sql = "INSERT INTO '" + channel + "' SELECT * FROM transfers;";

                    using (cmd = new SQLiteCommand(sql, myDB))
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

                String sql = "CREATE TABLE IF NOT EXISTS '"+channel+"' (id INTEGER PRIMARY KEY, user TEXT, currency INTEGER DEFAULT 0, subscriber INTEGER DEFAULT 0, btag TEXT DEFAULT null, userlevel INTEGER DEFAULT 0);";
                
                using (cmd = new SQLiteCommand(sql, myDB))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void newUser(String user)
        {
            user = api.capName(user);
            if (!userExists(user))
            {
                String sql = "INSERT INTO '"+channel+"' (user) VALUES ('" + user + "');";
                using (cmd = new SQLiteCommand(sql, myDB))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<string> GetAllUsers()
        {
            List<string> users = new List<string>();
            String sql = "SELECT * FROM '" + channel + "';";
            using (cmd = new SQLiteCommand(sql, myDB))
            {
                using (SQLiteDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        if (users.Contains(r["user"].ToString()))
                        {
                            break;
                        }
                        users.Add(r["user"].ToString());
                    }

                }
            }
            return users;
        }

        public void setCurrency(String user, int amount)
        {
            user = api.capName(user);
            if (!userExists(user))
            {
                newUser(user);
            }
            String sql = "UPDATE '" + channel + "' SET currency = " + amount + " WHERE user = \"" + user + "\";";
            using (cmd = new SQLiteCommand(sql, myDB))
            {
                cmd.ExecuteNonQuery();
            }

        }

        public int checkCurrency(String user)
        {
            user = api.capName(user);
            if (userExists(user)) {
                String sql = "SELECT * FROM '" + channel + "' WHERE user = \"" + user + "\";";
                using (cmd = new SQLiteCommand(sql, myDB))
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
            user = api.capName(user);
            if (!userExists(user))
            {
                newUser(user);
            }
            String sql = "UPDATE '" + channel + "' SET currency = currency + " + amount + " WHERE user = \"" + user + "\";";
            using (cmd = new SQLiteCommand(sql, myDB))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public void removeCurrency(String user, int amount)
        {
            user = api.capName(user);
            if (!userExists(user))
            {
                newUser(user);
            }
            String sql = "UPDATE '" + channel + "' SET currency = currency - " + amount + " WHERE user = \"" + user + "\";";
            using (cmd = new SQLiteCommand(sql, myDB))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public bool userExists(String user)
        {
            user = api.capName(user);
            String sql = "SELECT * FROM '" + channel + "';";
            using (cmd = new SQLiteCommand(sql, myDB))
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
            user = api.capName(user);
            if (userExists(user))
            {
                String sql = "SELECT * FROM '" + channel + "' WHERE user = \"" + user + "\";";
                using (cmd = new SQLiteCommand(sql, myDB))
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
                        else return null;
                    }
                }
            }
            else {
                newUser(user);
                return null;
            }
        }

        public void setBtag(String user, String btag)
        {
            user = api.capName(user);
            if (!userExists(user))
            {
                newUser(user);
            }
            String sql = "UPDATE '" + channel + "' SET btag = \"" + btag + "\" WHERE user = \"" + user + "\";";
            using (cmd = new SQLiteCommand(sql, myDB))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public bool isSubscriber(String user)
        {
            user = api.capName(user);
            if (!userExists(user))
            {
                newUser(user);
                return false;
            }
            else
            {
                String sql = "SELECT * FROM '" + channel + "' WHERE user = \"" + user + "\";";
                using (cmd = new SQLiteCommand(sql, myDB))
                {
                    using (SQLiteDataReader r = cmd.ExecuteReader())
                    {
                        if (r.Read())
                        {
                            if (int.Parse(r["subscriber"].ToString()) == 1)
                            {
                                return true;
                            }
                            else return false;
                        }
                        else return false;
                    }
                }
            }
        }

        public bool addSub(String user)
        {
            user = api.capName(user);
            if (userExists(user))
            {
                String sql = String.Format("UPDATE '" + channel + "' SET subscriber = 1 WHERE user = \"{0}\";", user);
                using (cmd = new SQLiteCommand(sql, myDB))
                {
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            return false;
        }

        public bool removeSub(String user)
        {
            user = api.capName(user);
            if (userExists(user))
            {
                String sql = String.Format("UPDATE '" + channel + "' SET subscriber = 0 WHERE user = \"{0}\";", user);
                using (cmd = new SQLiteCommand(sql, myDB))
                {
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            return false;
        }

        public int getUserLevel(String user)
        {
            user = api.capName(user);
            if (!userExists(user))
            {
                newUser(user);
                return 0;
            }
            else
            {
                String sql = "SELECT * FROM '" + channel + "' WHERE user = \"" + user + "\";";
                using (cmd = new SQLiteCommand(sql, myDB))
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
                            else return 0;
                        }
                        else return 0;
                    }
                }
            }
        }

        public void setUserLevel(String user, int level)
        {
            user = api.capName(user);
            String sql = "UPDATE '" + channel + "' SET userlevel = \"" + level + "\" WHERE user = \"" + user + "\";";
            using (cmd = new SQLiteCommand(sql, myDB))
            {
                cmd.ExecuteNonQuery();
            }
        }

        private bool tableExists(String table)
        {
            String sql = "SELECT COUNT(*) FROM sqlite_master WHERE name = '" + table + "';";
            try
            {
                using (cmd = new SQLiteCommand(sql, myDB))
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
                        return false;
                    }
                }
            }
            catch (SQLiteException e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        private bool tableHasData(String table)
        {
            String sql = "SELECT * FROM '" + table + "';";

            using (cmd = new SQLiteCommand(sql, myDB))
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