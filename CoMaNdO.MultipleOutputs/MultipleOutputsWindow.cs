using ModBot;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;

namespace CoMaNdO.MultipleOutputs
{
    public partial class MultipleOutputsWindow : Form
    {
        private IExtension extension;
        private Dictionary<string, List<Tuple<string, string, string>>> dCommands = new Dictionary<string, List<Tuple<string, string, string>>>();
        private bool CommandsRegistered = false;
        private SQLiteConnection DB;

        public MultipleOutputsWindow(IExtension sender)
        {
            InitializeComponent();

            extension = sender;

            while (Api.IsFileLocked(Api.GetDataPath(sender) + "Data.sqlite", FileShare.Read) && File.Exists(Api.GetDataPath(sender) + "Data.sqlite")) if (MessageBox.Show("Multiple Command Outputs' database file is in use, Please close it in order to let ModBot use it.", "Multiple Command Outputs", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning) == DialogResult.Cancel) Program.Close();

            if (!File.Exists(Api.GetDataPath(sender) + "Data.sqlite")) SQLiteConnection.CreateFile(Api.GetDataPath(sender) + "Data.sqlite");

            DB = new SQLiteConnection(@"Data Source=" + Api.GetDataPath(sender) + "Data.sqlite;Version=3;");
            DB.Open();

            using (SQLiteCommand query = new SQLiteCommand("CREATE TABLE IF NOT EXISTS 'commands' (id INTEGER PRIMARY KEY AUTOINCREMENT, type TEXT, command TEXT, data TEXT, output TEXT);", DB)) query.ExecuteNonQuery();
            using (SQLiteCommand query = new SQLiteCommand("CREATE TABLE IF NOT EXISTS 'userdata' (id INTEGER PRIMARY KEY AUTOINCREMENT, type TEXT, user TEXT, data TEXT);", DB)) query.ExecuteNonQuery();

            using (SQLiteCommand query = new SQLiteCommand("SELECT * FROM 'commands';", DB))
            {
                using (SQLiteDataReader r = query.ExecuteReader())
                {
                    while (r.Read())
                    {
                        CommandsDataGrid.Rows.Add(r["type"].ToString(), r["command"].ToString(), r["data"].ToString(), r["output"].ToString());
                    }
                }
            }

            CommandsDataGrid_Changed(CommandsDataGrid, null);

            CommandsDataGrid.CellValueChanged += new DataGridViewCellEventHandler(CommandsDataGrid_Changed);
            CommandsDataGrid.RowsAdded += new DataGridViewRowsAddedEventHandler(CommandsDataGrid_Changed);
            CommandsDataGrid.RowsRemoved += new DataGridViewRowsRemovedEventHandler(CommandsDataGrid_Changed);

            ModBot.Events.Connected += Events_Connected;
            ModBot.Events.OnDisconnect += Events_OnDisconnect;
        }

        private void CommandsDataGrid_Changed(object sender, EventArgs e)
        {
            lock (dCommands)
            {
                if (CommandsRegistered) foreach (string command in dCommands.Keys) dCommands.Remove(command);

                dCommands.Clear();

                using (SQLiteCommand query = new SQLiteCommand("DELETE FROM 'commands';", DB)) query.ExecuteNonQuery();

                foreach (DataGridViewRow row in CommandsDataGrid.Rows)
                {
                    if (row.Cells["ID"].Value != null && row.Cells["Command"].Value != null && row.Cells["Output"].Value != null && row.Cells["Data"].Value != null && row.Cells["Command"].Value.ToString() != "" && row.Cells["Output"].Value.ToString() != "")
                    {
                        string id = row.Cells["ID"].Value.ToString();
                        string cmd = row.Cells["Command"].Value.ToString();
                        string data = row.Cells["Data"].Value.ToString();
                        string output = row.Cells["Output"].Value.ToString();

                        bool skip = false;
                        foreach (string command in Commands.List)
                        {
                            if (command.ToLower() == cmd.ToLower())
                            {
                                skip = true;
                                break;
                            }
                        }
                        if (skip) continue;

                        if (!dCommands.ContainsKey(cmd.ToLower())) dCommands.Add(cmd.ToLower(), new List<Tuple<string, string, string>>());
                        dCommands[cmd.ToLower()].Add(new Tuple<string, string, string>(id, data, output));
                        using (SQLiteCommand query = new SQLiteCommand("INSERT INTO 'commands' (type, command, data, output) VALUES ('" + id.Replace("'", "''") + "', '" + cmd.Replace("'", "''") + "', '" + data.Replace("'", "''") + "', '" + output.Replace("'", "''") + "');", DB)) query.ExecuteNonQuery();
                    }
                }

                if (Chat.connection != null && Chat.connection.Connected)
                {
                    foreach (string command in dCommands.Keys) Commands.Add(extension, command, Command_Custom);
                    CommandsRegistered = true;
                }
            }
        }

        private void Events_Connected(string channel, string nick, bool partnered, bool subprogram)
        {
            if (!CommandsRegistered) foreach (string command in dCommands.Keys) Commands.Add(extension, command, Command_Custom);

            Commands.Add(extension, "!multipleoutputs", Command_MultipleOutputs, 2, 0);
            Commands.Add(extension, "!mcp", Command_MultipleOutputs, 2, 0);

            CommandsRegistered = true;
        }

        private void Command_Custom(string user, Command cmd, string[] args)
        {
            try
            {
                if (dCommands.ContainsKey(cmd.Cmd.ToLower()))
                {
                    if (dCommands[cmd.Cmd.ToLower()].Count == 0) return;

                    Tuple<string, string, string> command = dCommands[cmd.Cmd.ToLower()][new Random().Next(0, dCommands[cmd.Cmd.ToLower()].Count)];
                    string output = command.Item3;

                    if (command.Item1 != "" && command.Item2 != "")
                    {
                        bool found = false;
                        using (SQLiteCommand query = new SQLiteCommand("SELECT * FROM 'userdata' WHERE user = '" + user + "' COLLATE NOCASE;", DB))
                        {
                            using (SQLiteDataReader r = query.ExecuteReader())
                            {
                                while (r.Read())
                                {
                                    if (r["user"].ToString().ToLower() == user.ToLower() && r["type"].ToString() == command.Item1)
                                    {
                                        foreach (Tuple<string, string, string> data in dCommands[cmd.Cmd.ToLower()])
                                        {
                                            if (data.Item2 == r["data"].ToString())
                                            {
                                                found = true;
                                                output = data.Item3.Replace("@data", r["data"].ToString());
                                                break;
                                            }
                                        }

                                        if (!found) using (SQLiteCommand query2 = new SQLiteCommand("DELETE FROM 'userdata' WHERE user = '" + user + "' AND type = '" + command.Item1.Replace("'", "''") + "' COLLATE NOCASE;", DB)) query2.ExecuteNonQuery();

                                        break;
                                    }
                                }
                            }
                        }

                        if (!found) using (SQLiteCommand query = new SQLiteCommand("INSERT INTO 'userdata' (user, type, data) VALUES ('" + user + "', '" + command.Item1.Replace("'", "''") + "', '" + command.Item2.Replace("'", "''") + "');", DB)) query.ExecuteNonQuery();
                    }

                    output = output.Replace("@user", user);
                    output = output.Replace("@data", command.Item2);

                    Chat.SendMessage(output);
                }
            }
            catch(Exception e)
            {
                Api.Log(extension, LogType.Error, e.ToString());
                Console.WriteLine("An error has occured in Multiple Command Outputs (Command_Custom).");
            }
        }

        private void Command_MultipleOutputs(string user, Command cmd, string[] args)
        {
            try
            {
                if (args.Length > 2)
                {
                    if (args[0].ToLower() == "clear" && Users.CanTarget(user, args[1]) > -1)
                    {
                        string id = "";
                        for (int i = 2; i < args.Length; i++)
                        {
                            id += args[i] + " ";
                        }

                        if (id != "") id = id.Substring(0, id.Length - 1);

                        using (SQLiteCommand query = new SQLiteCommand("DELETE FROM 'userdata' WHERE user = '" + args[1] + "' AND type = '" + id + "' COLLATE NOCASE;", DB)) query.ExecuteNonQuery();

                        Chat.SendMessage(extension, "Cleared " + args[1] + "'s \"" + id + "\" data.", user + " cleared " + args[1] + "'s \"" + id + "\" data.");
                    }
                }
            }
            catch (Exception e)
            {
                Api.Log(extension, LogType.Error, e.ToString());
                Console.WriteLine("An error has occured in Multiple Command Outputs (Command_MultipleOutputs).");
            }
        }

        private void Events_OnDisconnect()
        {
            if (CommandsRegistered) foreach (string command in dCommands.Keys) dCommands.Remove(command);

            CommandsRegistered = false;
        }
    }
}