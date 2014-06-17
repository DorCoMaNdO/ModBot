using System;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.IO;

namespace ModBot
{    
    class Commands
    {
        private SQLiteConnection myDB;
        private SQLiteCommand cmd;

        public Commands()
        {
            InitializeDB();
        }

        private void InitializeDB()
        {
            if (File.Exists("ModBot.sqlite"))
            {
                myDB = new SQLiteConnection("Data Source=ModBot.sqlite;Version=3;");
                myDB.Open();

                String sql = "CREATE TABLE IF NOT EXISTS commands (id INTEGER PRIMARY KEY, command TEXT, level INTEGER DEFAULT 0, output TEXT DEFAULT null);";

                using (cmd = new SQLiteCommand(sql, myDB))
                {
                    cmd.ExecuteNonQuery();
                }
                
            }
            else
            {
                SQLiteConnection.CreateFile("ModBot.sqlite");
                myDB = new SQLiteConnection("Data Source=ModBot.sqlite;Version=3;");
                myDB.Open();

                String sql = "CREATE TABLE IF NOT EXISTS commands (id INTEGER PRIMARY KEY, command TEXT, level INTEGER DEFAULT 0, output TEXT DEFAULT null);";
                
                using (cmd = new SQLiteCommand(sql, myDB))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public bool cmdExists(String command)
        {
            String sql = "SELECT * FROM commands";
            using (cmd = new SQLiteCommand(sql, myDB))
            {
                using (SQLiteDataReader r = cmd.ExecuteReader())
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
            return false;
        }

        public void addCommand(String command, int level, String output)
        {
            String sql = String.Format("INSERT INTO commands (command, level, output) VALUES (\"{0}\", {1}, \"{2}\");", command, level, output);
            //String sql = "INSERT INTO commands (command, level, output) VALUES (\"" + command + "\", " + level + ", \"" + output + "\");";
            Console.WriteLine(sql);
            using (cmd = new SQLiteCommand(sql, myDB))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public void removeCommand(String command)
        {
            String sql = "DELETE FROM commands WHERE command = \"" + command + "\";";
            using (cmd = new SQLiteCommand(sql, myDB))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public int LevelRequired(String command)
        {
            String sql = String.Format("SELECT * FROM commands WHERE command = \"{0}\";", command);
            using (cmd = new SQLiteCommand(sql, myDB))
            {
                using (SQLiteDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        return int.Parse(r["level"].ToString());
                    }
                    return 0;
                }
            }
        }

        public string getList()
        {
            StringBuilder list = new StringBuilder();
            String sql = "SELECT * FROM commands;";
            using (cmd = new SQLiteCommand(sql, myDB))
            {
                using (SQLiteDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        list.Append(r["command"].ToString() + ", ");
                    }
                }
            }
            return list.ToString();
        }

        public string getOutput(String command)
        {
            String sql = "SELECT * FROM commands WHERE command = \"" + command + "\";";
            using (cmd = new SQLiteCommand(sql, myDB))
            {
                using (SQLiteDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        return r["output"].ToString();
                    }
                    return "";
                }
            }
        }
    }
}
