namespace ModBot
{
    partial class MainWindow
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
            this.aboutButton = new System.Windows.Forms.Button();
            this.ChannelLabel = new System.Windows.Forms.Label();
            this.Giveaway_MustFollowCheckBox = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ChannelTitleTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ChannelGameTextBox = new System.Windows.Forms.TextBox();
            this.Giveaway_MinCurrencyCheckBox = new System.Windows.Forms.CheckBox();
            this.Giveaway_MinCurrency = new System.Windows.Forms.NumericUpDown();
            this.Giveaway_StartButton = new System.Windows.Forms.Button();
            this.Giveaway_RerollButton = new System.Windows.Forms.Button();
            this.Giveaway_StopButton = new System.Windows.Forms.Button();
            this.Giveaway_WinnerStatusLabel = new System.Windows.Forms.Label();
            this.Giveaway_WinnerLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.Giveaway_ActiveUserTime = new System.Windows.Forms.NumericUpDown();
            this.Giveaway_AnnounceWinnerButton = new System.Windows.Forms.Button();
            this.Giveaway_BanListListBox = new System.Windows.Forms.ListBox();
            this.Giveaway_BanButton = new System.Windows.Forms.Button();
            this.Giveaway_UnbanButton = new System.Windows.Forms.Button();
            this.Giveaway_AddBanTextBox = new System.Windows.Forms.TextBox();
            this.Giveaway_AutoBanWinnerCheckBox = new System.Windows.Forms.CheckBox();
            this.Misc_LockCurrencyCmdCheckBox = new System.Windows.Forms.CheckBox();
            this.Giveaway_CopyWinnerButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.Giveaway_WinnerTimerLabel = new System.Windows.Forms.Label();
            this.Giveaway_WinnerTimer = new System.Windows.Forms.Timer(this.components);
            this.Giveaway_WinnerChat = new System.Windows.Forms.RichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SettingsPresents = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.Giveaway_WinTimeLabel = new System.Windows.Forms.Label();
            this.Donations_ManageButton = new System.Windows.Forms.Button();
            this.GiveawaySpacer = new System.Windows.Forms.GroupBox();
            this.GiveawayLabel = new System.Windows.Forms.Label();
            this.CurrencyLabel = new System.Windows.Forms.Label();
            this.CurrencySpacer = new System.Windows.Forms.GroupBox();
            this.Currency_HandoutEveryone = new System.Windows.Forms.RadioButton();
            this.Currency_HandoutActiveStream = new System.Windows.Forms.RadioButton();
            this.Currency_HandoutLabel = new System.Windows.Forms.Label();
            this.Currency_HandoutActiveTime = new System.Windows.Forms.RadioButton();
            this.Currency_HandoutLastActive = new System.Windows.Forms.NumericUpDown();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.Giveaway_MinCurrency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Giveaway_ActiveUserTime)).BeginInit();
            this.SettingsPresents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Currency_HandoutLastActive)).BeginInit();
            this.SuspendLayout();
            // 
            // aboutButton
            // 
            this.aboutButton.BackColor = System.Drawing.Color.White;
            this.aboutButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.aboutButton.Location = new System.Drawing.Point(452, 538);
            this.aboutButton.Name = "aboutButton";
            this.aboutButton.Size = new System.Drawing.Size(103, 23);
            this.aboutButton.TabIndex = 11;
            this.aboutButton.Text = "About";
            this.aboutButton.UseVisualStyleBackColor = false;
            this.aboutButton.Click += new System.EventHandler(this.aboutButton_Click);
            // 
            // ChannelLabel
            // 
            this.ChannelLabel.Font = new System.Drawing.Font("Comic Sans MS", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChannelLabel.Location = new System.Drawing.Point(8, 32);
            this.ChannelLabel.Name = "ChannelLabel";
            this.ChannelLabel.Size = new System.Drawing.Size(553, 19);
            this.ChannelLabel.TabIndex = 12;
            this.ChannelLabel.Text = "Loading...";
            this.ChannelLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Giveaway_MustFollowCheckBox
            // 
            this.Giveaway_MustFollowCheckBox.AutoSize = true;
            this.Giveaway_MustFollowCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Giveaway_MustFollowCheckBox.Location = new System.Drawing.Point(14, 236);
            this.Giveaway_MustFollowCheckBox.Name = "Giveaway_MustFollowCheckBox";
            this.Giveaway_MustFollowCheckBox.Size = new System.Drawing.Size(109, 17);
            this.Giveaway_MustFollowCheckBox.TabIndex = 14;
            this.Giveaway_MustFollowCheckBox.Text = "Must be a follower";
            this.Giveaway_MustFollowCheckBox.UseVisualStyleBackColor = true;
            this.Giveaway_MustFollowCheckBox.CheckedChanged += new System.EventHandler(this.Giveaway_MustFollowCheckBox_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(20, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 19);
            this.label2.TabIndex = 16;
            this.label2.Text = "Title :";
            // 
            // ChannelTitleTextBox
            // 
            this.ChannelTitleTextBox.BackColor = System.Drawing.Color.White;
            this.ChannelTitleTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ChannelTitleTextBox.Location = new System.Drawing.Point(62, 51);
            this.ChannelTitleTextBox.Name = "ChannelTitleTextBox";
            this.ChannelTitleTextBox.ReadOnly = true;
            this.ChannelTitleTextBox.Size = new System.Drawing.Size(493, 20);
            this.ChannelTitleTextBox.TabIndex = 17;
            this.ChannelTitleTextBox.Text = "Loading...";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 19);
            this.label3.TabIndex = 18;
            this.label3.Text = "Game :";
            // 
            // ChannelGameTextBox
            // 
            this.ChannelGameTextBox.BackColor = System.Drawing.Color.White;
            this.ChannelGameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ChannelGameTextBox.Location = new System.Drawing.Point(62, 73);
            this.ChannelGameTextBox.Name = "ChannelGameTextBox";
            this.ChannelGameTextBox.ReadOnly = true;
            this.ChannelGameTextBox.Size = new System.Drawing.Size(493, 20);
            this.ChannelGameTextBox.TabIndex = 19;
            this.ChannelGameTextBox.Text = "Loading...";
            // 
            // Giveaway_MinCurrencyCheckBox
            // 
            this.Giveaway_MinCurrencyCheckBox.AutoSize = true;
            this.Giveaway_MinCurrencyCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Giveaway_MinCurrencyCheckBox.Location = new System.Drawing.Point(14, 260);
            this.Giveaway_MinCurrencyCheckBox.Name = "Giveaway_MinCurrencyCheckBox";
            this.Giveaway_MinCurrencyCheckBox.Size = new System.Drawing.Size(88, 17);
            this.Giveaway_MinCurrencyCheckBox.TabIndex = 20;
            this.Giveaway_MinCurrencyCheckBox.Text = "Min. Currency";
            this.Giveaway_MinCurrencyCheckBox.UseVisualStyleBackColor = true;
            this.Giveaway_MinCurrencyCheckBox.CheckedChanged += new System.EventHandler(this.Giveaway_MinCurrencyCheckBox_CheckedChanged);
            // 
            // Giveaway_MinCurrency
            // 
            this.Giveaway_MinCurrency.Enabled = false;
            this.Giveaway_MinCurrency.Location = new System.Drawing.Point(122, 260);
            this.Giveaway_MinCurrency.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.Giveaway_MinCurrency.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Giveaway_MinCurrency.Name = "Giveaway_MinCurrency";
            this.Giveaway_MinCurrency.Size = new System.Drawing.Size(62, 20);
            this.Giveaway_MinCurrency.TabIndex = 21;
            this.Giveaway_MinCurrency.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Giveaway_MinCurrency.ValueChanged += new System.EventHandler(this.Giveaway_MinCurrency_ValueChanged);
            // 
            // Giveaway_StartButton
            // 
            this.Giveaway_StartButton.BackColor = System.Drawing.Color.White;
            this.Giveaway_StartButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Giveaway_StartButton.Location = new System.Drawing.Point(14, 538);
            this.Giveaway_StartButton.Name = "Giveaway_StartButton";
            this.Giveaway_StartButton.Size = new System.Drawing.Size(104, 23);
            this.Giveaway_StartButton.TabIndex = 22;
            this.Giveaway_StartButton.Text = "Start";
            this.Giveaway_StartButton.UseVisualStyleBackColor = false;
            this.Giveaway_StartButton.Click += new System.EventHandler(this.Giveaway_StartButton_Click);
            // 
            // Giveaway_RerollButton
            // 
            this.Giveaway_RerollButton.BackColor = System.Drawing.Color.White;
            this.Giveaway_RerollButton.Enabled = false;
            this.Giveaway_RerollButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Giveaway_RerollButton.Location = new System.Drawing.Point(124, 538);
            this.Giveaway_RerollButton.Name = "Giveaway_RerollButton";
            this.Giveaway_RerollButton.Size = new System.Drawing.Size(104, 23);
            this.Giveaway_RerollButton.TabIndex = 23;
            this.Giveaway_RerollButton.Text = "Roll";
            this.Giveaway_RerollButton.UseVisualStyleBackColor = false;
            this.Giveaway_RerollButton.Click += new System.EventHandler(this.Giveaway_RerollButton_Click);
            // 
            // Giveaway_StopButton
            // 
            this.Giveaway_StopButton.BackColor = System.Drawing.Color.White;
            this.Giveaway_StopButton.Enabled = false;
            this.Giveaway_StopButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Giveaway_StopButton.Location = new System.Drawing.Point(343, 538);
            this.Giveaway_StopButton.Name = "Giveaway_StopButton";
            this.Giveaway_StopButton.Size = new System.Drawing.Size(103, 23);
            this.Giveaway_StopButton.TabIndex = 24;
            this.Giveaway_StopButton.Text = "Stop";
            this.Giveaway_StopButton.UseVisualStyleBackColor = false;
            this.Giveaway_StopButton.Click += new System.EventHandler(this.Giveaway_StopButton_Click);
            // 
            // Giveaway_WinnerStatusLabel
            // 
            this.Giveaway_WinnerStatusLabel.Font = new System.Drawing.Font("Segoe Print", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Giveaway_WinnerStatusLabel.Location = new System.Drawing.Point(14, 461);
            this.Giveaway_WinnerStatusLabel.Name = "Giveaway_WinnerStatusLabel";
            this.Giveaway_WinnerStatusLabel.Size = new System.Drawing.Size(541, 21);
            this.Giveaway_WinnerStatusLabel.TabIndex = 25;
            this.Giveaway_WinnerStatusLabel.Text = "Winner :";
            this.Giveaway_WinnerStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Giveaway_WinnerLabel
            // 
            this.Giveaway_WinnerLabel.Font = new System.Drawing.Font("Segoe Print", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Giveaway_WinnerLabel.ForeColor = System.Drawing.Color.Blue;
            this.Giveaway_WinnerLabel.Location = new System.Drawing.Point(14, 472);
            this.Giveaway_WinnerLabel.Name = "Giveaway_WinnerLabel";
            this.Giveaway_WinnerLabel.Size = new System.Drawing.Size(541, 43);
            this.Giveaway_WinnerLabel.TabIndex = 26;
            this.Giveaway_WinnerLabel.Text = "CoMaNdO ;)";
            this.Giveaway_WinnerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(187, 238);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(206, 13);
            this.label5.TabIndex = 27;
            this.label5.Text = "Last active less than               minutes ago";
            // 
            // Giveaway_ActiveUserTime
            // 
            this.Giveaway_ActiveUserTime.Location = new System.Drawing.Point(290, 236);
            this.Giveaway_ActiveUserTime.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.Giveaway_ActiveUserTime.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.Giveaway_ActiveUserTime.Name = "Giveaway_ActiveUserTime";
            this.Giveaway_ActiveUserTime.Size = new System.Drawing.Size(38, 20);
            this.Giveaway_ActiveUserTime.TabIndex = 28;
            this.Giveaway_ActiveUserTime.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.Giveaway_ActiveUserTime.ValueChanged += new System.EventHandler(this.Giveaway_ActiveUserTime_ValueChanged);
            // 
            // Giveaway_AnnounceWinnerButton
            // 
            this.Giveaway_AnnounceWinnerButton.BackColor = System.Drawing.Color.White;
            this.Giveaway_AnnounceWinnerButton.Enabled = false;
            this.Giveaway_AnnounceWinnerButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Giveaway_AnnounceWinnerButton.Location = new System.Drawing.Point(234, 538);
            this.Giveaway_AnnounceWinnerButton.Name = "Giveaway_AnnounceWinnerButton";
            this.Giveaway_AnnounceWinnerButton.Size = new System.Drawing.Size(103, 23);
            this.Giveaway_AnnounceWinnerButton.TabIndex = 29;
            this.Giveaway_AnnounceWinnerButton.Text = "Announce";
            this.Giveaway_AnnounceWinnerButton.UseVisualStyleBackColor = false;
            this.Giveaway_AnnounceWinnerButton.Click += new System.EventHandler(this.Giveaway_AnnounceWinnerButton_Click);
            // 
            // Giveaway_BanListListBox
            // 
            this.Giveaway_BanListListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Giveaway_BanListListBox.FormattingEnabled = true;
            this.Giveaway_BanListListBox.Location = new System.Drawing.Point(396, 236);
            this.Giveaway_BanListListBox.Name = "Giveaway_BanListListBox";
            this.Giveaway_BanListListBox.Size = new System.Drawing.Size(159, 93);
            this.Giveaway_BanListListBox.TabIndex = 30;
            this.Giveaway_BanListListBox.SelectedIndexChanged += new System.EventHandler(this.Giveaway_BanListListBox_SelectedIndexChanged);
            // 
            // Giveaway_BanButton
            // 
            this.Giveaway_BanButton.BackColor = System.Drawing.Color.White;
            this.Giveaway_BanButton.Enabled = false;
            this.Giveaway_BanButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Giveaway_BanButton.Location = new System.Drawing.Point(190, 306);
            this.Giveaway_BanButton.Name = "Giveaway_BanButton";
            this.Giveaway_BanButton.Size = new System.Drawing.Size(97, 23);
            this.Giveaway_BanButton.TabIndex = 31;
            this.Giveaway_BanButton.Text = "Ban";
            this.Giveaway_BanButton.UseVisualStyleBackColor = false;
            this.Giveaway_BanButton.Click += new System.EventHandler(this.Giveaway_BanButton_Click);
            // 
            // Giveaway_UnbanButton
            // 
            this.Giveaway_UnbanButton.BackColor = System.Drawing.Color.White;
            this.Giveaway_UnbanButton.Enabled = false;
            this.Giveaway_UnbanButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Giveaway_UnbanButton.Location = new System.Drawing.Point(293, 306);
            this.Giveaway_UnbanButton.Name = "Giveaway_UnbanButton";
            this.Giveaway_UnbanButton.Size = new System.Drawing.Size(97, 23);
            this.Giveaway_UnbanButton.TabIndex = 32;
            this.Giveaway_UnbanButton.Text = "Unban";
            this.Giveaway_UnbanButton.UseVisualStyleBackColor = false;
            this.Giveaway_UnbanButton.Click += new System.EventHandler(this.Giveaway_UnbanButton_Click);
            // 
            // Giveaway_AddBanTextBox
            // 
            this.Giveaway_AddBanTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Giveaway_AddBanTextBox.Location = new System.Drawing.Point(190, 280);
            this.Giveaway_AddBanTextBox.Name = "Giveaway_AddBanTextBox";
            this.Giveaway_AddBanTextBox.Size = new System.Drawing.Size(200, 20);
            this.Giveaway_AddBanTextBox.TabIndex = 33;
            this.Giveaway_AddBanTextBox.TextChanged += new System.EventHandler(this.Giveaway_AddBanTextBox_TextChanged);
            // 
            // Giveaway_AutoBanWinnerCheckBox
            // 
            this.Giveaway_AutoBanWinnerCheckBox.AutoSize = true;
            this.Giveaway_AutoBanWinnerCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Giveaway_AutoBanWinnerCheckBox.Location = new System.Drawing.Point(14, 283);
            this.Giveaway_AutoBanWinnerCheckBox.Name = "Giveaway_AutoBanWinnerCheckBox";
            this.Giveaway_AutoBanWinnerCheckBox.Size = new System.Drawing.Size(140, 17);
            this.Giveaway_AutoBanWinnerCheckBox.TabIndex = 34;
            this.Giveaway_AutoBanWinnerCheckBox.Text = "Automatically ban winner";
            this.Giveaway_AutoBanWinnerCheckBox.UseVisualStyleBackColor = true;
            this.Giveaway_AutoBanWinnerCheckBox.CheckedChanged += new System.EventHandler(this.Giveaway_AutoBanWinnerCheckBox_CheckedChanged);
            // 
            // Misc_LockCurrencyCmdCheckBox
            // 
            this.Misc_LockCurrencyCmdCheckBox.AutoSize = true;
            this.Misc_LockCurrencyCmdCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Misc_LockCurrencyCmdCheckBox.Location = new System.Drawing.Point(13, 304);
            this.Misc_LockCurrencyCmdCheckBox.Name = "Misc_LockCurrencyCmdCheckBox";
            this.Misc_LockCurrencyCmdCheckBox.Size = new System.Drawing.Size(140, 17);
            this.Misc_LockCurrencyCmdCheckBox.TabIndex = 35;
            this.Misc_LockCurrencyCmdCheckBox.Text = "Lock currency command";
            this.Misc_LockCurrencyCmdCheckBox.UseVisualStyleBackColor = true;
            this.Misc_LockCurrencyCmdCheckBox.CheckedChanged += new System.EventHandler(this.Misc_LockCurrencyCmdCheckBox_CheckedChanged);
            // 
            // Giveaway_CopyWinnerButton
            // 
            this.Giveaway_CopyWinnerButton.BackColor = System.Drawing.Color.White;
            this.Giveaway_CopyWinnerButton.Enabled = false;
            this.Giveaway_CopyWinnerButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Giveaway_CopyWinnerButton.Location = new System.Drawing.Point(14, 509);
            this.Giveaway_CopyWinnerButton.Name = "Giveaway_CopyWinnerButton";
            this.Giveaway_CopyWinnerButton.Size = new System.Drawing.Size(541, 23);
            this.Giveaway_CopyWinnerButton.TabIndex = 36;
            this.Giveaway_CopyWinnerButton.Text = "Copy to clipboard";
            this.Giveaway_CopyWinnerButton.UseVisualStyleBackColor = false;
            this.Giveaway_CopyWinnerButton.Click += new System.EventHandler(this.Giveaway_CopyWinnerButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(190, 259);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 19);
            this.label4.TabIndex = 37;
            this.label4.Text = "Giveaway ban :";
            // 
            // Giveaway_WinnerTimerLabel
            // 
            this.Giveaway_WinnerTimerLabel.AutoSize = true;
            this.Giveaway_WinnerTimerLabel.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Giveaway_WinnerTimerLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Giveaway_WinnerTimerLabel.Location = new System.Drawing.Point(13, 326);
            this.Giveaway_WinnerTimerLabel.Name = "Giveaway_WinnerTimerLabel";
            this.Giveaway_WinnerTimerLabel.Size = new System.Drawing.Size(36, 19);
            this.Giveaway_WinnerTimerLabel.TabIndex = 40;
            this.Giveaway_WinnerTimerLabel.Text = "0:00";
            // 
            // Giveaway_WinnerTimer
            // 
            this.Giveaway_WinnerTimer.Enabled = true;
            this.Giveaway_WinnerTimer.Tick += new System.EventHandler(this.Giveaway_WinnerTimer_Tick);
            // 
            // Giveaway_WinnerChat
            // 
            this.Giveaway_WinnerChat.BackColor = System.Drawing.Color.White;
            this.Giveaway_WinnerChat.Location = new System.Drawing.Point(14, 344);
            this.Giveaway_WinnerChat.Name = "Giveaway_WinnerChat";
            this.Giveaway_WinnerChat.ReadOnly = true;
            this.Giveaway_WinnerChat.Size = new System.Drawing.Size(541, 119);
            this.Giveaway_WinnerChat.TabIndex = 41;
            this.Giveaway_WinnerChat.Text = "";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(14, 324);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(541, 23);
            this.label6.TabIndex = 42;
            this.label6.Text = "Winner chat";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SettingsPresents
            // 
            this.SettingsPresents.Controls.Add(this.tabPage1);
            this.SettingsPresents.Location = new System.Drawing.Point(7, 198);
            this.SettingsPresents.Name = "SettingsPresents";
            this.SettingsPresents.SelectedIndex = 0;
            this.SettingsPresents.Size = new System.Drawing.Size(556, 22);
            this.SettingsPresents.TabIndex = 43;
            this.SettingsPresents.SelectedIndexChanged += new System.EventHandler(this.SettingsPresents_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(548, 0);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Default";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // Giveaway_WinTimeLabel
            // 
            this.Giveaway_WinTimeLabel.AutoSize = true;
            this.Giveaway_WinTimeLabel.Font = new System.Drawing.Font("Segoe Print", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Giveaway_WinTimeLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Giveaway_WinTimeLabel.Location = new System.Drawing.Point(10, 482);
            this.Giveaway_WinTimeLabel.Name = "Giveaway_WinTimeLabel";
            this.Giveaway_WinTimeLabel.Size = new System.Drawing.Size(44, 24);
            this.Giveaway_WinTimeLabel.TabIndex = 44;
            this.Giveaway_WinTimeLabel.Text = "0:00";
            // 
            // Donations_ManageButton
            // 
            this.Donations_ManageButton.BackColor = System.Drawing.Color.White;
            this.Donations_ManageButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Donations_ManageButton.Location = new System.Drawing.Point(14, 95);
            this.Donations_ManageButton.Name = "Donations_ManageButton";
            this.Donations_ManageButton.Size = new System.Drawing.Size(541, 23);
            this.Donations_ManageButton.TabIndex = 45;
            this.Donations_ManageButton.Text = "Manage Donations";
            this.Donations_ManageButton.UseVisualStyleBackColor = false;
            this.Donations_ManageButton.Click += new System.EventHandler(this.Donations_ManageButton_Click);
            // 
            // GiveawaySpacer
            // 
            this.GiveawaySpacer.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Bold);
            this.GiveawaySpacer.Location = new System.Drawing.Point(8, 216);
            this.GiveawaySpacer.Name = "GiveawaySpacer";
            this.GiveawaySpacer.Size = new System.Drawing.Size(553, 11);
            this.GiveawaySpacer.TabIndex = 47;
            this.GiveawaySpacer.TabStop = false;
            // 
            // GiveawayLabel
            // 
            this.GiveawayLabel.AutoSize = true;
            this.GiveawayLabel.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GiveawayLabel.Location = new System.Drawing.Point(252, 217);
            this.GiveawayLabel.Name = "GiveawayLabel";
            this.GiveawayLabel.Size = new System.Drawing.Size(66, 17);
            this.GiveawayLabel.TabIndex = 13;
            this.GiveawayLabel.Text = "Giveaway";
            this.GiveawayLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CurrencyLabel
            // 
            this.CurrencyLabel.AutoSize = true;
            this.CurrencyLabel.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrencyLabel.Location = new System.Drawing.Point(254, 116);
            this.CurrencyLabel.Name = "CurrencyLabel";
            this.CurrencyLabel.Size = new System.Drawing.Size(62, 17);
            this.CurrencyLabel.TabIndex = 48;
            this.CurrencyLabel.Text = "Currency";
            this.CurrencyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CurrencySpacer
            // 
            this.CurrencySpacer.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Bold);
            this.CurrencySpacer.Location = new System.Drawing.Point(8, 115);
            this.CurrencySpacer.Name = "CurrencySpacer";
            this.CurrencySpacer.Size = new System.Drawing.Size(553, 11);
            this.CurrencySpacer.TabIndex = 49;
            this.CurrencySpacer.TabStop = false;
            // 
            // Currency_HandoutEveryone
            // 
            this.Currency_HandoutEveryone.AutoSize = true;
            this.Currency_HandoutEveryone.Checked = true;
            this.Currency_HandoutEveryone.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Currency_HandoutEveryone.Location = new System.Drawing.Point(13, 146);
            this.Currency_HandoutEveryone.Name = "Currency_HandoutEveryone";
            this.Currency_HandoutEveryone.Size = new System.Drawing.Size(132, 17);
            this.Currency_HandoutEveryone.TabIndex = 51;
            this.Currency_HandoutEveryone.TabStop = true;
            this.Currency_HandoutEveryone.Text = "Everyone in the stream";
            this.Currency_HandoutEveryone.UseVisualStyleBackColor = true;
            this.Currency_HandoutEveryone.CheckedChanged += new System.EventHandler(this.Currency_HandoutEveryone_CheckedChanged);
            // 
            // Currency_HandoutActiveStream
            // 
            this.Currency_HandoutActiveStream.AutoSize = true;
            this.Currency_HandoutActiveStream.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Currency_HandoutActiveStream.Location = new System.Drawing.Point(13, 161);
            this.Currency_HandoutActiveStream.Name = "Currency_HandoutActiveStream";
            this.Currency_HandoutActiveStream.Size = new System.Drawing.Size(342, 17);
            this.Currency_HandoutActiveStream.TabIndex = 52;
            this.Currency_HandoutActiveStream.Text = "Anyone in the stream who joined / said something during the stream";
            this.Currency_HandoutActiveStream.UseVisualStyleBackColor = true;
            this.Currency_HandoutActiveStream.CheckedChanged += new System.EventHandler(this.Currency_HandoutActiveStream_CheckedChanged);
            // 
            // Currency_HandoutLabel
            // 
            this.Currency_HandoutLabel.AutoSize = true;
            this.Currency_HandoutLabel.Font = new System.Drawing.Font("Segoe Print", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Currency_HandoutLabel.Location = new System.Drawing.Point(13, 130);
            this.Currency_HandoutLabel.Name = "Currency_HandoutLabel";
            this.Currency_HandoutLabel.Size = new System.Drawing.Size(137, 17);
            this.Currency_HandoutLabel.TabIndex = 53;
            this.Currency_HandoutLabel.Text = "Handout CURRENCY to :";
            // 
            // Currency_HandoutActiveTime
            // 
            this.Currency_HandoutActiveTime.AutoSize = true;
            this.Currency_HandoutActiveTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Currency_HandoutActiveTime.Location = new System.Drawing.Point(13, 176);
            this.Currency_HandoutActiveTime.Name = "Currency_HandoutActiveTime";
            this.Currency_HandoutActiveTime.Size = new System.Drawing.Size(393, 17);
            this.Currency_HandoutActiveTime.TabIndex = 54;
            this.Currency_HandoutActiveTime.Text = "Anyone in the stream who joined / said something in the last                 minu" +
    "tes";
            this.Currency_HandoutActiveTime.UseVisualStyleBackColor = true;
            this.Currency_HandoutActiveTime.CheckedChanged += new System.EventHandler(this.Currency_HandoutActiveTime_CheckedChanged);
            // 
            // Currency_HandoutLastActive
            // 
            this.Currency_HandoutLastActive.Enabled = false;
            this.Currency_HandoutLastActive.Location = new System.Drawing.Point(316, 176);
            this.Currency_HandoutLastActive.Maximum = new decimal(new int[] {
            2880,
            0,
            0,
            0});
            this.Currency_HandoutLastActive.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.Currency_HandoutLastActive.Name = "Currency_HandoutLastActive";
            this.Currency_HandoutLastActive.Size = new System.Drawing.Size(44, 20);
            this.Currency_HandoutLastActive.TabIndex = 55;
            this.Currency_HandoutLastActive.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.Currency_HandoutLastActive.ValueChanged += new System.EventHandler(this.Currency_HandoutLastActive_ValueChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox1.FlatAppearance.BorderSize = 0;
            this.checkBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBox1.Location = new System.Drawing.Point(446, 139);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(71, 23);
            this.checkBox1.TabIndex = 56;
            this.checkBox1.Text = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Visible = false;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.WindowChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox2.FlatAppearance.BorderSize = 0;
            this.checkBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBox2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBox2.Location = new System.Drawing.Point(446, 162);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(71, 23);
            this.checkBox2.TabIndex = 57;
            this.checkBox2.Text = "checkBox2";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.Visible = false;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.WindowChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Location = new System.Drawing.Point(521, 139);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(34, 22);
            this.panel1.TabIndex = 58;
            this.panel1.Visible = this.checkBox1.Checked;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Location = new System.Drawing.Point(521, 163);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(34, 22);
            this.panel2.TabIndex = 59;
            this.panel2.Visible = false;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 575);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.Giveaway_AddBanTextBox);
            this.Controls.Add(this.SettingsPresents);
            this.Controls.Add(this.Currency_HandoutLastActive);
            this.Controls.Add(this.Currency_HandoutActiveTime);
            this.Controls.Add(this.Currency_HandoutActiveStream);
            this.Controls.Add(this.Currency_HandoutLabel);
            this.Controls.Add(this.Currency_HandoutEveryone);
            this.Controls.Add(this.Donations_ManageButton);
            this.Controls.Add(this.Giveaway_WinnerChat);
            this.Controls.Add(this.Giveaway_WinnerTimerLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Giveaway_CopyWinnerButton);
            this.Controls.Add(this.Misc_LockCurrencyCmdCheckBox);
            this.Controls.Add(this.Giveaway_AutoBanWinnerCheckBox);
            this.Controls.Add(this.Giveaway_UnbanButton);
            this.Controls.Add(this.Giveaway_BanButton);
            this.Controls.Add(this.Giveaway_BanListListBox);
            this.Controls.Add(this.ChannelLabel);
            this.Controls.Add(this.Giveaway_AnnounceWinnerButton);
            this.Controls.Add(this.Giveaway_ActiveUserTime);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Giveaway_StopButton);
            this.Controls.Add(this.Giveaway_RerollButton);
            this.Controls.Add(this.Giveaway_StartButton);
            this.Controls.Add(this.Giveaway_MinCurrency);
            this.Controls.Add(this.Giveaway_MinCurrencyCheckBox);
            this.Controls.Add(this.ChannelGameTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ChannelTitleTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Giveaway_MustFollowCheckBox);
            this.Controls.Add(this.aboutButton);
            this.Controls.Add(this.Giveaway_WinnerStatusLabel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Giveaway_WinTimeLabel);
            this.Controls.Add(this.Giveaway_WinnerLabel);
            this.Controls.Add(this.GiveawayLabel);
            this.Controls.Add(this.GiveawaySpacer);
            this.Controls.Add(this.CurrencyLabel);
            this.Controls.Add(this.CurrencySpacer);
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.Text = "ModBot";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.Controls.SetChildIndex(this.CurrencySpacer, 0);
            this.Controls.SetChildIndex(this.CurrencyLabel, 0);
            this.Controls.SetChildIndex(this.GiveawaySpacer, 0);
            this.Controls.SetChildIndex(this.GiveawayLabel, 0);
            this.Controls.SetChildIndex(this.Giveaway_WinnerLabel, 0);
            this.Controls.SetChildIndex(this.Giveaway_WinTimeLabel, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.Giveaway_WinnerStatusLabel, 0);
            this.Controls.SetChildIndex(this.aboutButton, 0);
            this.Controls.SetChildIndex(this.Giveaway_MustFollowCheckBox, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.ChannelTitleTextBox, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.ChannelGameTextBox, 0);
            this.Controls.SetChildIndex(this.Giveaway_MinCurrencyCheckBox, 0);
            this.Controls.SetChildIndex(this.Giveaway_MinCurrency, 0);
            this.Controls.SetChildIndex(this.Giveaway_StartButton, 0);
            this.Controls.SetChildIndex(this.Giveaway_RerollButton, 0);
            this.Controls.SetChildIndex(this.Giveaway_StopButton, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.Giveaway_ActiveUserTime, 0);
            this.Controls.SetChildIndex(this.Giveaway_AnnounceWinnerButton, 0);
            this.Controls.SetChildIndex(this.ChannelLabel, 0);
            this.Controls.SetChildIndex(this.Giveaway_BanListListBox, 0);
            this.Controls.SetChildIndex(this.Giveaway_BanButton, 0);
            this.Controls.SetChildIndex(this.Giveaway_UnbanButton, 0);
            this.Controls.SetChildIndex(this.Giveaway_AutoBanWinnerCheckBox, 0);
            this.Controls.SetChildIndex(this.Misc_LockCurrencyCmdCheckBox, 0);
            this.Controls.SetChildIndex(this.Giveaway_CopyWinnerButton, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.Giveaway_WinnerTimerLabel, 0);
            this.Controls.SetChildIndex(this.Giveaway_WinnerChat, 0);
            this.Controls.SetChildIndex(this.Donations_ManageButton, 0);
            this.Controls.SetChildIndex(this.Currency_HandoutEveryone, 0);
            this.Controls.SetChildIndex(this.Currency_HandoutLabel, 0);
            this.Controls.SetChildIndex(this.Currency_HandoutActiveStream, 0);
            this.Controls.SetChildIndex(this.Currency_HandoutActiveTime, 0);
            this.Controls.SetChildIndex(this.Currency_HandoutLastActive, 0);
            this.Controls.SetChildIndex(this.SettingsPresents, 0);
            this.Controls.SetChildIndex(this.Giveaway_AddBanTextBox, 0);
            this.Controls.SetChildIndex(this.checkBox1, 0);
            this.Controls.SetChildIndex(this.checkBox2, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            ((System.ComponentModel.ISupportInitialize)(this.Giveaway_MinCurrency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Giveaway_ActiveUserTime)).EndInit();
            this.SettingsPresents.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Currency_HandoutLastActive)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button aboutButton;
        public System.Windows.Forms.Label ChannelLabel;
        public System.Windows.Forms.CheckBox Giveaway_MustFollowCheckBox;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox ChannelTitleTextBox;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox ChannelGameTextBox;
        public System.Windows.Forms.CheckBox Giveaway_MinCurrencyCheckBox;
        public System.Windows.Forms.NumericUpDown Giveaway_MinCurrency;
        public System.Windows.Forms.Button Giveaway_StartButton;
        public System.Windows.Forms.Button Giveaway_RerollButton;
        public System.Windows.Forms.Button Giveaway_StopButton;
        public System.Windows.Forms.Label Giveaway_WinnerStatusLabel;
        public System.Windows.Forms.Label Giveaway_WinnerLabel;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.NumericUpDown Giveaway_ActiveUserTime;
        public System.Windows.Forms.Button Giveaway_AnnounceWinnerButton;
        public System.Windows.Forms.ListBox Giveaway_BanListListBox;
        public System.Windows.Forms.Button Giveaway_BanButton;
        public System.Windows.Forms.Button Giveaway_UnbanButton;
        public System.Windows.Forms.TextBox Giveaway_AddBanTextBox;
        public System.Windows.Forms.CheckBox Giveaway_AutoBanWinnerCheckBox;
        public System.Windows.Forms.CheckBox Misc_LockCurrencyCmdCheckBox;
        public System.Windows.Forms.Button Giveaway_CopyWinnerButton;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.Label Giveaway_WinnerTimerLabel;
        public System.Windows.Forms.Timer Giveaway_WinnerTimer;
        public System.Windows.Forms.RichTextBox Giveaway_WinnerChat;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TabControl SettingsPresents;
        private System.Windows.Forms.TabPage tabPage1;
        public System.Windows.Forms.Label Giveaway_WinTimeLabel;
        public System.Windows.Forms.Button Donations_ManageButton;
        private System.Windows.Forms.GroupBox GiveawaySpacer;
        private System.Windows.Forms.Label GiveawayLabel;
        private System.Windows.Forms.Label CurrencyLabel;
        private System.Windows.Forms.GroupBox CurrencySpacer;
        public System.Windows.Forms.RadioButton Currency_HandoutEveryone;
        public System.Windows.Forms.RadioButton Currency_HandoutActiveStream;
        private System.Windows.Forms.Label Currency_HandoutLabel;
        public System.Windows.Forms.RadioButton Currency_HandoutActiveTime;
        public System.Windows.Forms.NumericUpDown Currency_HandoutLastActive;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}