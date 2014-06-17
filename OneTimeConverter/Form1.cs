using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace OneTimeConverter
{
    public partial class Form1 : Form
    {
        private SQLiteConnection sqliteDB;
        private SQLiteCommand sqliteCMD;
        private MySqlConnection mysqlDB;
        private MySqlCommand mysqlCMD;

        public Form1()
        {
            InitializeComponent();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            Dictionary<string, int> transfers = new Dictionary<string, int>();
            this.Hide();
            if (!File.Exists("ModBot.sqlite"))
            {
                try
                {
                    String connstring = String.Format("Database={0};Data Source={1};User Id={2};Password={3};", databaseBox.Text, serverBox.Text, usernameBox.Text, passwordBox.Text);
                    mysqlDB = new MySqlConnection(connstring);
                    mysqlDB.Open();

                    String sql = "SELECT * FROM viewers;";

                    using (mysqlCMD = new MySqlCommand(sql, mysqlDB))
                    {
                        using (MySqlDataReader r = mysqlCMD.ExecuteReader())
                        {
                            while (r.Read())
                            {
                                //Console.WriteLine(r["user"] + " has " + r["points"] + " points.");
                                transfers.Add(capName(r["user"].ToString()), int.Parse(r["points"].ToString()));
                            }
                        }
                    }
                    Console.WriteLine("Data grabbed from MySQL successfully.");

                    SQLiteConnection.CreateFile("ModBot.sqlite");
                    sqliteDB = new SQLiteConnection("Data Source=ModBot.sqlite;Version=3;");
                    sqliteDB.Open();

                    Console.WriteLine("ModBot.sqlite created.  Adding new users now.");

                    sql = "CREATE TABLE IF NOT EXISTS transfers (id INTEGER PRIMARY KEY, user TEXT, currency INTEGER DEFAULT 0, subscriber INTEGER DEFAULT 0, btag TEXT DEFAULT null, userlevel INTEGER DEFAULT 0);";
                
                    using (sqliteCMD = new SQLiteCommand(sql, sqliteDB))
                    {
                        sqliteCMD.ExecuteNonQuery();
                    }

                    foreach (string user in transfers.Keys)
                    {
                        sql = String.Format("INSERT INTO transfers (user, currency) VALUES (\"{0}\", {1})", user, transfers[user]);
                        //Console.WriteLine(sql);
                        using (sqliteCMD = new SQLiteCommand(sql, sqliteDB))
                        {
                            sqliteCMD.ExecuteNonQuery();
                        }
                        Console.WriteLine(String.Format("Added {0} to the new database with {1} currency", user, transfers[user]));
                    }
                    Console.WriteLine("Transfers complete.  Run ModBot.exe now.");
                }
                catch (Exception error)
                {
                    Console.WriteLine(error);
                }
                
            }
            else Console.WriteLine("ModBot.sqlite file already exists.  To run the converter, it must be deleted/renamed first.  **Make a backup of it if you're not sure what you're doing.");
        }

        private String capName(String user)
        {
            return char.ToUpper(user[0]) + user.Substring(1);
        }

        
    }
}
