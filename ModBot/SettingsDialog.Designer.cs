namespace ModBot
{
    partial class SettingsDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsDialog));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.intervalBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.payoutBox = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.subBox = new System.Windows.Forms.TextBox();
            this.passwordBox = new System.Windows.Forms.MaskedTextBox();
            this.currencyBox = new System.Windows.Forms.TextBox();
            this.channelBox = new System.Windows.Forms.TextBox();
            this.botNameBox = new System.Windows.Forms.TextBox();
            this.startButton = new System.Windows.Forms.Button();
            this.aboutButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Bot Name:";
            this.toolTip.SetToolTip(this.label1, "Your bot account\'s username.\r\n\r\nIt\'s a good idea to use a separate bot account fr" +
                    "om your streaming account.");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Bot Password:";
            this.toolTip.SetToolTip(this.label2, "Your bot account\'s password.");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(306, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Currency: ";
            this.toolTip.SetToolTip(this.label3, "The name of your channel\'s currency.\r\n\r\nChanging it does not cause old currency t" +
                    "o be lost.");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(39, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Channel:";
            this.toolTip.SetToolTip(this.label4, "Username of the channel you want the bot to join.\r\n\r\nNote: Don\'t enter the link t" +
                    "o the stream, just the name of the streamer.");
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(279, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Payout Interval:";
            this.toolTip.SetToolTip(this.label5, "How often you want currency to be given out.");
            // 
            // intervalBox
            // 
            this.intervalBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.intervalBox.FormatString = "N0";
            this.intervalBox.FormattingEnabled = true;
            this.intervalBox.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "5",
            "10",
            "15",
            "20",
            "30",
            "60"});
            this.intervalBox.Location = new System.Drawing.Point(367, 5);
            this.intervalBox.Name = "intervalBox";
            this.intervalBox.Size = new System.Drawing.Size(88, 21);
            this.intervalBox.TabIndex = 5;
            this.toolTip.SetToolTip(this.intervalBox, "How often you want currency to be given out");
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(279, 35);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Payout Amount:";
            this.toolTip.SetToolTip(this.label6, "How much currency to give out during handouts.\r\n\r\nSubscribers get double this amo" +
                    "unt.");
            // 
            // payoutBox
            // 
            this.payoutBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.payoutBox.FormatString = "N0";
            this.payoutBox.FormattingEnabled = true;
            this.payoutBox.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "15",
            "20",
            "25",
            "30",
            "35",
            "40",
            "45",
            "50",
            "60",
            "70",
            "75",
            "80",
            "90",
            "100"});
            this.payoutBox.Location = new System.Drawing.Point(367, 32);
            this.payoutBox.Name = "payoutBox";
            this.payoutBox.Size = new System.Drawing.Size(88, 21);
            this.payoutBox.TabIndex = 6;
            this.toolTip.SetToolTip(this.payoutBox, "How much currency to give out during handouts.\r\n\r\nSubscribers get double this amo" +
                    "unt.");
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(-1, 86);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Sub Spreadsheet";
            this.toolTip.SetToolTip(this.label7, resources.GetString("label7.ToolTip"));
            // 
            // subBox
            // 
            this.subBox.Location = new System.Drawing.Point(94, 83);
            this.subBox.Name = "subBox";
            this.subBox.Size = new System.Drawing.Size(196, 20);
            this.subBox.TabIndex = 4;
            this.subBox.Text = global::ModBot.Properties.Settings.Default.subUrl;
            this.toolTip.SetToolTip(this.subBox, resources.GetString("subBox.ToolTip"));
            // 
            // passwordBox
            // 
            this.passwordBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ModBot.Properties.Settings.Default, "password", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.passwordBox.Location = new System.Drawing.Point(94, 31);
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.PasswordChar = '*';
            this.passwordBox.Size = new System.Drawing.Size(121, 20);
            this.passwordBox.TabIndex = 2;
            this.passwordBox.Text = global::ModBot.Properties.Settings.Default.password;
            this.toolTip.SetToolTip(this.passwordBox, "Your bot account\'s password.");
            // 
            // currencyBox
            // 
            this.currencyBox.Location = new System.Drawing.Point(367, 57);
            this.currencyBox.Name = "currencyBox";
            this.currencyBox.Size = new System.Drawing.Size(88, 20);
            this.currencyBox.TabIndex = 7;
            this.currencyBox.Text = global::ModBot.Properties.Settings.Default.currency;
            this.toolTip.SetToolTip(this.currencyBox, "The name of your channel\'s currency.\r\n\r\nChanging it does not cause old currency t" +
                    "o be lost.");
            // 
            // channelBox
            // 
            this.channelBox.Location = new System.Drawing.Point(94, 57);
            this.channelBox.Name = "channelBox";
            this.channelBox.Size = new System.Drawing.Size(121, 20);
            this.channelBox.TabIndex = 3;
            this.channelBox.Text = global::ModBot.Properties.Settings.Default.channel;
            this.toolTip.SetToolTip(this.channelBox, "Username of the channel you want the bot to join.\r\n\r\nNote: Don\'t enter the link t" +
                    "o the stream, just the name of the streamer.");
            // 
            // botNameBox
            // 
            this.botNameBox.Location = new System.Drawing.Point(94, 5);
            this.botNameBox.Name = "botNameBox";
            this.botNameBox.Size = new System.Drawing.Size(121, 20);
            this.botNameBox.TabIndex = 1;
            this.botNameBox.Text = global::ModBot.Properties.Settings.Default.name;
            this.toolTip.SetToolTip(this.botNameBox, "Your bot account\'s username.\r\n\r\nIt\'s a good idea to use a separate bot account fr" +
                    "om your streaming account.");
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(296, 79);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(78, 27);
            this.startButton.TabIndex = 8;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // aboutButton
            // 
            this.aboutButton.Location = new System.Drawing.Point(380, 79);
            this.aboutButton.Name = "aboutButton";
            this.aboutButton.Size = new System.Drawing.Size(75, 27);
            this.aboutButton.TabIndex = 10;
            this.aboutButton.Text = "About";
            this.aboutButton.UseVisualStyleBackColor = true;
            this.aboutButton.Click += new System.EventHandler(this.aboutButton_Click);
            // 
            // SettingsDialog
            // 
            this.AcceptButton = this.startButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(478, 117);
            this.Controls.Add(this.aboutButton);
            this.Controls.Add(this.subBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.passwordBox);
            this.Controls.Add(this.payoutBox);
            this.Controls.Add(this.intervalBox);
            this.Controls.Add(this.currencyBox);
            this.Controls.Add(this.channelBox);
            this.Controls.Add(this.botNameBox);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "SettingsDialog";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox botNameBox;
        private System.Windows.Forms.TextBox channelBox;
        private System.Windows.Forms.TextBox currencyBox;
        private System.Windows.Forms.ComboBox intervalBox;
        private System.Windows.Forms.MaskedTextBox passwordBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox payoutBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox subBox;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button aboutButton;
    }
}

