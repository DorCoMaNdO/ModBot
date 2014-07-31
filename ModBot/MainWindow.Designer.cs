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
            this.ChannelStatusLabel = new System.Windows.Forms.Label();
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
            this.Giveaway_ActiveUserTime = new System.Windows.Forms.NumericUpDown();
            this.Giveaway_AnnounceWinnerButton = new System.Windows.Forms.Button();
            this.Giveaway_BanListListBox = new System.Windows.Forms.ListBox();
            this.Giveaway_BanButton = new System.Windows.Forms.Button();
            this.Giveaway_UnbanButton = new System.Windows.Forms.Button();
            this.Giveaway_AddBanTextBox = new System.Windows.Forms.TextBox();
            this.Giveaway_AutoBanWinnerCheckBox = new System.Windows.Forms.CheckBox();
            this.Currency_DisableCommandCheckBox = new System.Windows.Forms.CheckBox();
            this.Giveaway_CopyWinnerButton = new System.Windows.Forms.Button();
            this.Giveaway_WinnerTimerLabel = new System.Windows.Forms.Label();
            this.Giveaway_WinnerTimer = new System.Windows.Forms.Timer(this.components);
            this.Giveaway_WinnerChat = new System.Windows.Forms.RichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SettingsPresents = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.Giveaway_WinTimeLabel = new System.Windows.Forms.Label();
            this.GiveawayTypeSpacer = new System.Windows.Forms.GroupBox();
            this.GiveawayTypeLabel = new System.Windows.Forms.Label();
            this.HandoutLabel = new System.Windows.Forms.Label();
            this.HandoutSpacer = new System.Windows.Forms.GroupBox();
            this.Currency_HandoutEveryone = new System.Windows.Forms.RadioButton();
            this.Currency_HandoutActiveStream = new System.Windows.Forms.RadioButton();
            this.Currency_HandoutLabel = new System.Windows.Forms.Label();
            this.Currency_HandoutActiveTime = new System.Windows.Forms.RadioButton();
            this.Currency_HandoutLastActive = new System.Windows.Forms.NumericUpDown();
            this.SettingsWindowButton = new System.Windows.Forms.CheckBox();
            this.DonationsWindowButton = new System.Windows.Forms.CheckBox();
            this.SettingsWindow = new System.Windows.Forms.Panel();
            this.GenerateTokenButton = new System.Windows.Forms.Button();
            this.label21 = new System.Windows.Forms.Label();
            this.CurrencyCommandBox = new System.Windows.Forms.TextBox();
            this.SettingsErrorLabel = new System.Windows.Forms.Label();
            this.DisconnectButton = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.DonationsLabel = new System.Windows.Forms.Label();
            this.SubscribersLabel = new System.Windows.Forms.Label();
            this.CurrencyHandoutAmount = new System.Windows.Forms.NumericUpDown();
            this.DonationsSpacer = new System.Windows.Forms.GroupBox();
            this.CurrencyHandoutInterval = new System.Windows.Forms.NumericUpDown();
            this.CurrencyLabel = new System.Windows.Forms.Label();
            this.ConnectionLabel = new System.Windows.Forms.Label();
            this.CurrencySpacer = new System.Windows.Forms.GroupBox();
            this.SubscribersSpacer = new System.Windows.Forms.GroupBox();
            this.ConnectionSpacer = new System.Windows.Forms.GroupBox();
            this.DonationsKeyBox = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.SubLinkBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.BotPasswordBox = new System.Windows.Forms.TextBox();
            this.BotNameBox = new System.Windows.Forms.TextBox();
            this.ChannelBox = new System.Windows.Forms.TextBox();
            this.CurrencyNameBox = new System.Windows.Forms.TextBox();
            this.DonationsWindow = new System.Windows.Forms.Panel();
            this.RecentDonorsLimit = new System.Windows.Forms.NumericUpDown();
            this.UpdateTopDonorsCheckBox = new System.Windows.Forms.CheckBox();
            this.label15 = new System.Windows.Forms.Label();
            this.Donations_List = new System.Windows.Forms.DataGridView();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Donor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Notes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IncludeRecent = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IncludeLatest = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IncludeTop = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IncludeTopDonor = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.TopDonorsLimit = new System.Windows.Forms.NumericUpDown();
            this.UpdateRecentDonorsCheckBox = new System.Windows.Forms.CheckBox();
            this.label16 = new System.Windows.Forms.Label();
            this.UpdateLastDonorCheckBox = new System.Windows.Forms.CheckBox();
            this.AboutWindowButton = new System.Windows.Forms.CheckBox();
            this.AboutWindow = new System.Windows.Forms.Panel();
            this.SupportLinkLabel = new System.Windows.Forms.LinkLabel();
            this.EmailLinkLabel = new System.Windows.Forms.LinkLabel();
            this.label17 = new System.Windows.Forms.Label();
            this.WebsiteLinkLabel = new System.Windows.Forms.LinkLabel();
            this.label18 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.VersionLabel = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.AuthenticationBrowser = new System.Windows.Forms.WebBrowser();
            this.ChannelWindowButton = new System.Windows.Forms.CheckBox();
            this.ChannelWindow = new System.Windows.Forms.Panel();
            this.CurrencyWindow = new System.Windows.Forms.Panel();
            this.CurrencyWindowButton = new System.Windows.Forms.CheckBox();
            this.GiveawayWindowButton = new System.Windows.Forms.CheckBox();
            this.GiveawayWindow = new System.Windows.Forms.Panel();
            this.GiveawayBansLabel = new System.Windows.Forms.Label();
            this.GiveawayBansSpacer = new System.Windows.Forms.GroupBox();
            this.GiveawayRulesLabel = new System.Windows.Forms.Label();
            this.GiveawayRulesSpacer = new System.Windows.Forms.GroupBox();
            this.Giveaway_TypeTickets = new System.Windows.Forms.RadioButton();
            this.Giveaway_TypeKeyword = new System.Windows.Forms.RadioButton();
            this.Giveaway_TypeActive = new System.Windows.Forms.RadioButton();
            this.AuthenticationLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Giveaway_MinCurrency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Giveaway_ActiveUserTime)).BeginInit();
            this.SettingsPresents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Currency_HandoutLastActive)).BeginInit();
            this.SettingsWindow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CurrencyHandoutAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CurrencyHandoutInterval)).BeginInit();
            this.DonationsWindow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RecentDonorsLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Donations_List)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TopDonorsLimit)).BeginInit();
            this.AboutWindow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.ChannelWindow.SuspendLayout();
            this.CurrencyWindow.SuspendLayout();
            this.GiveawayWindow.SuspendLayout();
            this.SuspendLayout();
            // 
            // ChannelStatusLabel
            // 
            this.ChannelStatusLabel.Font = new System.Drawing.Font("Segoe Print", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChannelStatusLabel.ForeColor = System.Drawing.Color.Red;
            this.ChannelStatusLabel.Location = new System.Drawing.Point(0, 0);
            this.ChannelStatusLabel.Name = "ChannelStatusLabel";
            this.ChannelStatusLabel.Size = new System.Drawing.Size(814, 24);
            this.ChannelStatusLabel.TabIndex = 12;
            this.ChannelStatusLabel.Text = "DISCONNECTED";
            this.ChannelStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Giveaway_MustFollowCheckBox
            // 
            this.Giveaway_MustFollowCheckBox.AutoSize = true;
            this.Giveaway_MustFollowCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Giveaway_MustFollowCheckBox.Location = new System.Drawing.Point(6, 145);
            this.Giveaway_MustFollowCheckBox.Name = "Giveaway_MustFollowCheckBox";
            this.Giveaway_MustFollowCheckBox.Size = new System.Drawing.Size(109, 17);
            this.Giveaway_MustFollowCheckBox.TabIndex = 14;
            this.Giveaway_MustFollowCheckBox.Text = "Must be a follower";
            this.Giveaway_MustFollowCheckBox.UseVisualStyleBackColor = true;
            this.Giveaway_MustFollowCheckBox.CheckedChanged += new System.EventHandler(this.Giveaway_Settings_Changed);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Title :";
            // 
            // ChannelTitleTextBox
            // 
            this.ChannelTitleTextBox.BackColor = System.Drawing.Color.White;
            this.ChannelTitleTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ChannelTitleTextBox.Location = new System.Drawing.Point(6, 41);
            this.ChannelTitleTextBox.Name = "ChannelTitleTextBox";
            this.ChannelTitleTextBox.ReadOnly = true;
            this.ChannelTitleTextBox.Size = new System.Drawing.Size(802, 20);
            this.ChannelTitleTextBox.TabIndex = 17;
            this.ChannelTitleTextBox.Text = "Loading...";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Game :";
            // 
            // ChannelGameTextBox
            // 
            this.ChannelGameTextBox.BackColor = System.Drawing.Color.White;
            this.ChannelGameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ChannelGameTextBox.Location = new System.Drawing.Point(6, 80);
            this.ChannelGameTextBox.Name = "ChannelGameTextBox";
            this.ChannelGameTextBox.ReadOnly = true;
            this.ChannelGameTextBox.Size = new System.Drawing.Size(802, 20);
            this.ChannelGameTextBox.TabIndex = 19;
            this.ChannelGameTextBox.Text = "Loading...";
            // 
            // Giveaway_MinCurrencyCheckBox
            // 
            this.Giveaway_MinCurrencyCheckBox.AutoSize = true;
            this.Giveaway_MinCurrencyCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Giveaway_MinCurrencyCheckBox.Location = new System.Drawing.Point(6, 162);
            this.Giveaway_MinCurrencyCheckBox.Name = "Giveaway_MinCurrencyCheckBox";
            this.Giveaway_MinCurrencyCheckBox.Size = new System.Drawing.Size(239, 17);
            this.Giveaway_MinCurrencyCheckBox.TabIndex = 20;
            this.Giveaway_MinCurrencyCheckBox.Text = "Must have at least                       CURRENCY";
            this.Giveaway_MinCurrencyCheckBox.UseVisualStyleBackColor = true;
            this.Giveaway_MinCurrencyCheckBox.CheckedChanged += new System.EventHandler(this.Giveaway_MinCurrencyCheckBox_CheckedChanged);
            // 
            // Giveaway_MinCurrency
            // 
            this.Giveaway_MinCurrency.Enabled = false;
            this.Giveaway_MinCurrency.Location = new System.Drawing.Point(113, 162);
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
            this.Giveaway_MinCurrency.ValueChanged += new System.EventHandler(this.Giveaway_Settings_Changed);
            // 
            // Giveaway_StartButton
            // 
            this.Giveaway_StartButton.BackColor = System.Drawing.Color.White;
            this.Giveaway_StartButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Giveaway_StartButton.Location = new System.Drawing.Point(6, 533);
            this.Giveaway_StartButton.Name = "Giveaway_StartButton";
            this.Giveaway_StartButton.Size = new System.Drawing.Size(196, 23);
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
            this.Giveaway_RerollButton.Location = new System.Drawing.Point(208, 533);
            this.Giveaway_RerollButton.Name = "Giveaway_RerollButton";
            this.Giveaway_RerollButton.Size = new System.Drawing.Size(196, 23);
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
            this.Giveaway_StopButton.Location = new System.Drawing.Point(612, 533);
            this.Giveaway_StopButton.Name = "Giveaway_StopButton";
            this.Giveaway_StopButton.Size = new System.Drawing.Size(196, 23);
            this.Giveaway_StopButton.TabIndex = 24;
            this.Giveaway_StopButton.Text = "Stop";
            this.Giveaway_StopButton.UseVisualStyleBackColor = false;
            this.Giveaway_StopButton.Click += new System.EventHandler(this.Giveaway_StopButton_Click);
            // 
            // Giveaway_WinnerStatusLabel
            // 
            this.Giveaway_WinnerStatusLabel.Font = new System.Drawing.Font("Segoe Print", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Giveaway_WinnerStatusLabel.Location = new System.Drawing.Point(6, 459);
            this.Giveaway_WinnerStatusLabel.Name = "Giveaway_WinnerStatusLabel";
            this.Giveaway_WinnerStatusLabel.Size = new System.Drawing.Size(802, 21);
            this.Giveaway_WinnerStatusLabel.TabIndex = 25;
            this.Giveaway_WinnerStatusLabel.Text = "Winner :";
            this.Giveaway_WinnerStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Giveaway_WinnerLabel
            // 
            this.Giveaway_WinnerLabel.Font = new System.Drawing.Font("Segoe Print", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Giveaway_WinnerLabel.ForeColor = System.Drawing.Color.Blue;
            this.Giveaway_WinnerLabel.Location = new System.Drawing.Point(6, 470);
            this.Giveaway_WinnerLabel.Name = "Giveaway_WinnerLabel";
            this.Giveaway_WinnerLabel.Size = new System.Drawing.Size(802, 43);
            this.Giveaway_WinnerLabel.TabIndex = 26;
            this.Giveaway_WinnerLabel.Text = "CoMaNdO ;)";
            this.Giveaway_WinnerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Giveaway_ActiveUserTime
            // 
            this.Giveaway_ActiveUserTime.Location = new System.Drawing.Point(124, 46);
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
            this.Giveaway_ActiveUserTime.ValueChanged += new System.EventHandler(this.Giveaway_Settings_Changed);
            // 
            // Giveaway_AnnounceWinnerButton
            // 
            this.Giveaway_AnnounceWinnerButton.BackColor = System.Drawing.Color.White;
            this.Giveaway_AnnounceWinnerButton.Enabled = false;
            this.Giveaway_AnnounceWinnerButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Giveaway_AnnounceWinnerButton.Location = new System.Drawing.Point(410, 533);
            this.Giveaway_AnnounceWinnerButton.Name = "Giveaway_AnnounceWinnerButton";
            this.Giveaway_AnnounceWinnerButton.Size = new System.Drawing.Size(196, 23);
            this.Giveaway_AnnounceWinnerButton.TabIndex = 29;
            this.Giveaway_AnnounceWinnerButton.Text = "Announce";
            this.Giveaway_AnnounceWinnerButton.UseVisualStyleBackColor = false;
            this.Giveaway_AnnounceWinnerButton.Click += new System.EventHandler(this.Giveaway_AnnounceWinnerButton_Click);
            // 
            // Giveaway_BanListListBox
            // 
            this.Giveaway_BanListListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Giveaway_BanListListBox.FormattingEnabled = true;
            this.Giveaway_BanListListBox.Location = new System.Drawing.Point(324, 46);
            this.Giveaway_BanListListBox.Name = "Giveaway_BanListListBox";
            this.Giveaway_BanListListBox.Size = new System.Drawing.Size(200, 93);
            this.Giveaway_BanListListBox.TabIndex = 30;
            this.Giveaway_BanListListBox.SelectedIndexChanged += new System.EventHandler(this.Giveaway_BanListListBox_SelectedIndexChanged);
            // 
            // Giveaway_BanButton
            // 
            this.Giveaway_BanButton.BackColor = System.Drawing.Color.White;
            this.Giveaway_BanButton.Enabled = false;
            this.Giveaway_BanButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Giveaway_BanButton.Location = new System.Drawing.Point(324, 170);
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
            this.Giveaway_UnbanButton.Location = new System.Drawing.Point(427, 170);
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
            this.Giveaway_AddBanTextBox.Location = new System.Drawing.Point(324, 144);
            this.Giveaway_AddBanTextBox.Name = "Giveaway_AddBanTextBox";
            this.Giveaway_AddBanTextBox.Size = new System.Drawing.Size(200, 20);
            this.Giveaway_AddBanTextBox.TabIndex = 33;
            this.Giveaway_AddBanTextBox.TextChanged += new System.EventHandler(this.Giveaway_AddBanTextBox_TextChanged);
            // 
            // Giveaway_AutoBanWinnerCheckBox
            // 
            this.Giveaway_AutoBanWinnerCheckBox.AutoSize = true;
            this.Giveaway_AutoBanWinnerCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Giveaway_AutoBanWinnerCheckBox.Location = new System.Drawing.Point(6, 179);
            this.Giveaway_AutoBanWinnerCheckBox.Name = "Giveaway_AutoBanWinnerCheckBox";
            this.Giveaway_AutoBanWinnerCheckBox.Size = new System.Drawing.Size(140, 17);
            this.Giveaway_AutoBanWinnerCheckBox.TabIndex = 34;
            this.Giveaway_AutoBanWinnerCheckBox.Text = "Automatically ban winner";
            this.Giveaway_AutoBanWinnerCheckBox.UseVisualStyleBackColor = true;
            this.Giveaway_AutoBanWinnerCheckBox.CheckedChanged += new System.EventHandler(this.Giveaway_Settings_Changed);
            // 
            // Currency_DisableCommandCheckBox
            // 
            this.Currency_DisableCommandCheckBox.AutoSize = true;
            this.Currency_DisableCommandCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Currency_DisableCommandCheckBox.Location = new System.Drawing.Point(6, 84);
            this.Currency_DisableCommandCheckBox.Name = "Currency_DisableCommandCheckBox";
            this.Currency_DisableCommandCheckBox.Size = new System.Drawing.Size(151, 17);
            this.Currency_DisableCommandCheckBox.TabIndex = 35;
            this.Currency_DisableCommandCheckBox.Text = "Disable currency command";
            this.Currency_DisableCommandCheckBox.UseVisualStyleBackColor = true;
            this.Currency_DisableCommandCheckBox.CheckedChanged += new System.EventHandler(this.Currency_DisableCommandCheckBox_CheckedChanged);
            // 
            // Giveaway_CopyWinnerButton
            // 
            this.Giveaway_CopyWinnerButton.BackColor = System.Drawing.Color.White;
            this.Giveaway_CopyWinnerButton.Enabled = false;
            this.Giveaway_CopyWinnerButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Giveaway_CopyWinnerButton.Location = new System.Drawing.Point(6, 507);
            this.Giveaway_CopyWinnerButton.Name = "Giveaway_CopyWinnerButton";
            this.Giveaway_CopyWinnerButton.Size = new System.Drawing.Size(802, 23);
            this.Giveaway_CopyWinnerButton.TabIndex = 36;
            this.Giveaway_CopyWinnerButton.Text = "Copy to clipboard";
            this.Giveaway_CopyWinnerButton.UseVisualStyleBackColor = false;
            this.Giveaway_CopyWinnerButton.Click += new System.EventHandler(this.Giveaway_CopyWinnerButton_Click);
            // 
            // Giveaway_WinnerTimerLabel
            // 
            this.Giveaway_WinnerTimerLabel.AutoSize = true;
            this.Giveaway_WinnerTimerLabel.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Giveaway_WinnerTimerLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Giveaway_WinnerTimerLabel.Location = new System.Drawing.Point(2, 326);
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
            this.Giveaway_WinnerChat.Location = new System.Drawing.Point(6, 343);
            this.Giveaway_WinnerChat.Name = "Giveaway_WinnerChat";
            this.Giveaway_WinnerChat.ReadOnly = true;
            this.Giveaway_WinnerChat.Size = new System.Drawing.Size(802, 119);
            this.Giveaway_WinnerChat.TabIndex = 41;
            this.Giveaway_WinnerChat.Text = "";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 324);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(802, 23);
            this.label6.TabIndex = 42;
            this.label6.Text = "Winner chat";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SettingsPresents
            // 
            this.SettingsPresents.Controls.Add(this.tabPage1);
            this.SettingsPresents.Location = new System.Drawing.Point(-1, -1);
            this.SettingsPresents.Name = "SettingsPresents";
            this.SettingsPresents.SelectedIndex = 0;
            this.SettingsPresents.Size = new System.Drawing.Size(816, 22);
            this.SettingsPresents.TabIndex = 43;
            this.SettingsPresents.SelectedIndexChanged += new System.EventHandler(this.SettingsPresents_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(808, 0);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Default";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // Giveaway_WinTimeLabel
            // 
            this.Giveaway_WinTimeLabel.AutoSize = true;
            this.Giveaway_WinTimeLabel.Font = new System.Drawing.Font("Segoe Print", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Giveaway_WinTimeLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Giveaway_WinTimeLabel.Location = new System.Drawing.Point(2, 480);
            this.Giveaway_WinTimeLabel.Name = "Giveaway_WinTimeLabel";
            this.Giveaway_WinTimeLabel.Size = new System.Drawing.Size(44, 24);
            this.Giveaway_WinTimeLabel.TabIndex = 44;
            this.Giveaway_WinTimeLabel.Text = "0:00";
            // 
            // GiveawayTypeSpacer
            // 
            this.GiveawayTypeSpacer.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Bold);
            this.GiveawayTypeSpacer.Location = new System.Drawing.Point(-1, 25);
            this.GiveawayTypeSpacer.Name = "GiveawayTypeSpacer";
            this.GiveawayTypeSpacer.Size = new System.Drawing.Size(320, 11);
            this.GiveawayTypeSpacer.TabIndex = 47;
            this.GiveawayTypeSpacer.TabStop = false;
            // 
            // GiveawayTypeLabel
            // 
            this.GiveawayTypeLabel.AutoSize = true;
            this.GiveawayTypeLabel.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GiveawayTypeLabel.Location = new System.Drawing.Point(58, 25);
            this.GiveawayTypeLabel.Name = "GiveawayTypeLabel";
            this.GiveawayTypeLabel.Size = new System.Drawing.Size(89, 19);
            this.GiveawayTypeLabel.TabIndex = 13;
            this.GiveawayTypeLabel.Text = "Giveaway type";
            this.GiveawayTypeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // HandoutLabel
            // 
            this.HandoutLabel.AutoSize = true;
            this.HandoutLabel.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HandoutLabel.Location = new System.Drawing.Point(246, 0);
            this.HandoutLabel.Name = "HandoutLabel";
            this.HandoutLabel.Size = new System.Drawing.Size(56, 19);
            this.HandoutLabel.TabIndex = 48;
            this.HandoutLabel.Text = "Handout";
            this.HandoutLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // HandoutSpacer
            // 
            this.HandoutSpacer.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Bold);
            this.HandoutSpacer.Location = new System.Drawing.Point(-1, 0);
            this.HandoutSpacer.Name = "HandoutSpacer";
            this.HandoutSpacer.Size = new System.Drawing.Size(816, 11);
            this.HandoutSpacer.TabIndex = 49;
            this.HandoutSpacer.TabStop = false;
            // 
            // Currency_HandoutEveryone
            // 
            this.Currency_HandoutEveryone.AutoSize = true;
            this.Currency_HandoutEveryone.Checked = true;
            this.Currency_HandoutEveryone.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Currency_HandoutEveryone.Location = new System.Drawing.Point(5, 34);
            this.Currency_HandoutEveryone.Name = "Currency_HandoutEveryone";
            this.Currency_HandoutEveryone.Size = new System.Drawing.Size(132, 17);
            this.Currency_HandoutEveryone.TabIndex = 51;
            this.Currency_HandoutEveryone.TabStop = true;
            this.Currency_HandoutEveryone.Text = "Everyone in the stream";
            this.Currency_HandoutEveryone.UseVisualStyleBackColor = true;
            this.Currency_HandoutEveryone.CheckedChanged += new System.EventHandler(this.Currency_HandoutType_Changed);
            // 
            // Currency_HandoutActiveStream
            // 
            this.Currency_HandoutActiveStream.AutoSize = true;
            this.Currency_HandoutActiveStream.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Currency_HandoutActiveStream.Location = new System.Drawing.Point(5, 49);
            this.Currency_HandoutActiveStream.Name = "Currency_HandoutActiveStream";
            this.Currency_HandoutActiveStream.Size = new System.Drawing.Size(342, 17);
            this.Currency_HandoutActiveStream.TabIndex = 52;
            this.Currency_HandoutActiveStream.Text = "Anyone in the stream who joined / said something during the stream";
            this.Currency_HandoutActiveStream.UseVisualStyleBackColor = true;
            this.Currency_HandoutActiveStream.CheckedChanged += new System.EventHandler(this.Currency_HandoutType_Changed);
            // 
            // Currency_HandoutLabel
            // 
            this.Currency_HandoutLabel.AutoSize = true;
            this.Currency_HandoutLabel.Font = new System.Drawing.Font("Segoe Print", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Currency_HandoutLabel.Location = new System.Drawing.Point(5, 18);
            this.Currency_HandoutLabel.Name = "Currency_HandoutLabel";
            this.Currency_HandoutLabel.Size = new System.Drawing.Size(137, 17);
            this.Currency_HandoutLabel.TabIndex = 53;
            this.Currency_HandoutLabel.Text = "Handout CURRENCY to :";
            // 
            // Currency_HandoutActiveTime
            // 
            this.Currency_HandoutActiveTime.AutoSize = true;
            this.Currency_HandoutActiveTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Currency_HandoutActiveTime.Location = new System.Drawing.Point(5, 64);
            this.Currency_HandoutActiveTime.Name = "Currency_HandoutActiveTime";
            this.Currency_HandoutActiveTime.Size = new System.Drawing.Size(393, 17);
            this.Currency_HandoutActiveTime.TabIndex = 54;
            this.Currency_HandoutActiveTime.Text = "Anyone in the stream who joined / said something in the last                 minu" +
    "tes";
            this.Currency_HandoutActiveTime.UseVisualStyleBackColor = true;
            this.Currency_HandoutActiveTime.CheckedChanged += new System.EventHandler(this.Currency_HandoutType_Changed);
            // 
            // Currency_HandoutLastActive
            // 
            this.Currency_HandoutLastActive.Enabled = false;
            this.Currency_HandoutLastActive.Location = new System.Drawing.Point(308, 64);
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
            // SettingsWindowButton
            // 
            this.SettingsWindowButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.SettingsWindowButton.Checked = true;
            this.SettingsWindowButton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SettingsWindowButton.FlatAppearance.BorderSize = 0;
            this.SettingsWindowButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.SettingsWindowButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SettingsWindowButton.Font = new System.Drawing.Font("Segoe Print", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SettingsWindowButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.SettingsWindowButton.Location = new System.Drawing.Point(8, 30);
            this.SettingsWindowButton.Name = "SettingsWindowButton";
            this.SettingsWindowButton.Size = new System.Drawing.Size(100, 46);
            this.SettingsWindowButton.TabIndex = 56;
            this.SettingsWindowButton.TabStop = false;
            this.SettingsWindowButton.Text = "Settings";
            this.SettingsWindowButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.SettingsWindowButton.UseVisualStyleBackColor = true;
            this.SettingsWindowButton.CheckedChanged += new System.EventHandler(this.WindowChanged);
            // 
            // DonationsWindowButton
            // 
            this.DonationsWindowButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.DonationsWindowButton.Enabled = false;
            this.DonationsWindowButton.FlatAppearance.BorderSize = 0;
            this.DonationsWindowButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.DonationsWindowButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DonationsWindowButton.Font = new System.Drawing.Font("Segoe Print", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DonationsWindowButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.DonationsWindowButton.Location = new System.Drawing.Point(8, 194);
            this.DonationsWindowButton.Name = "DonationsWindowButton";
            this.DonationsWindowButton.Size = new System.Drawing.Size(100, 46);
            this.DonationsWindowButton.TabIndex = 57;
            this.DonationsWindowButton.TabStop = false;
            this.DonationsWindowButton.Text = "Donations";
            this.DonationsWindowButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.DonationsWindowButton.UseVisualStyleBackColor = true;
            this.DonationsWindowButton.CheckedChanged += new System.EventHandler(this.WindowChanged);
            // 
            // SettingsWindow
            // 
            this.SettingsWindow.BackColor = System.Drawing.Color.White;
            this.SettingsWindow.Controls.Add(this.GenerateTokenButton);
            this.SettingsWindow.Controls.Add(this.label21);
            this.SettingsWindow.Controls.Add(this.CurrencyCommandBox);
            this.SettingsWindow.Controls.Add(this.SettingsErrorLabel);
            this.SettingsWindow.Controls.Add(this.DisconnectButton);
            this.SettingsWindow.Controls.Add(this.label14);
            this.SettingsWindow.Controls.Add(this.DonationsLabel);
            this.SettingsWindow.Controls.Add(this.SubscribersLabel);
            this.SettingsWindow.Controls.Add(this.CurrencyHandoutAmount);
            this.SettingsWindow.Controls.Add(this.DonationsSpacer);
            this.SettingsWindow.Controls.Add(this.CurrencyHandoutInterval);
            this.SettingsWindow.Controls.Add(this.CurrencyLabel);
            this.SettingsWindow.Controls.Add(this.ConnectionLabel);
            this.SettingsWindow.Controls.Add(this.CurrencySpacer);
            this.SettingsWindow.Controls.Add(this.SubscribersSpacer);
            this.SettingsWindow.Controls.Add(this.ConnectionSpacer);
            this.SettingsWindow.Controls.Add(this.DonationsKeyBox);
            this.SettingsWindow.Controls.Add(this.label13);
            this.SettingsWindow.Controls.Add(this.label8);
            this.SettingsWindow.Controls.Add(this.label12);
            this.SettingsWindow.Controls.Add(this.label11);
            this.SettingsWindow.Controls.Add(this.SubLinkBox);
            this.SettingsWindow.Controls.Add(this.label10);
            this.SettingsWindow.Controls.Add(this.label7);
            this.SettingsWindow.Controls.Add(this.label9);
            this.SettingsWindow.Controls.Add(this.label1);
            this.SettingsWindow.Controls.Add(this.ConnectButton);
            this.SettingsWindow.Controls.Add(this.BotPasswordBox);
            this.SettingsWindow.Controls.Add(this.BotNameBox);
            this.SettingsWindow.Controls.Add(this.ChannelBox);
            this.SettingsWindow.Controls.Add(this.CurrencyNameBox);
            this.SettingsWindow.Location = new System.Drawing.Point(108, 30);
            this.SettingsWindow.Name = "SettingsWindow";
            this.SettingsWindow.Size = new System.Drawing.Size(814, 562);
            this.SettingsWindow.TabIndex = 58;
            this.SettingsWindow.Visible = this.SettingsWindowButton.Checked;
            // 
            // GenerateTokenButton
            // 
            this.GenerateTokenButton.BackColor = System.Drawing.Color.White;
            this.GenerateTokenButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GenerateTokenButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GenerateTokenButton.Location = new System.Drawing.Point(629, 72);
            this.GenerateTokenButton.Name = "GenerateTokenButton";
            this.GenerateTokenButton.Size = new System.Drawing.Size(178, 22);
            this.GenerateTokenButton.TabIndex = 87;
            this.GenerateTokenButton.Text = "Generate";
            this.GenerateTokenButton.UseVisualStyleBackColor = false;
            this.GenerateTokenButton.Click += new System.EventHandler(this.GenerateTokenButton_Click);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(3, 269);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(164, 13);
            this.label21.TabIndex = 85;
            this.label21.Text = "Currency Command (starts with !):";
            // 
            // CurrencyCommandBox
            // 
            this.CurrencyCommandBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CurrencyCommandBox.Location = new System.Drawing.Point(6, 285);
            this.CurrencyCommandBox.Name = "CurrencyCommandBox";
            this.CurrencyCommandBox.Size = new System.Drawing.Size(801, 20);
            this.CurrencyCommandBox.TabIndex = 86;
            this.CurrencyCommandBox.Text = "ModCoins";
            // 
            // SettingsErrorLabel
            // 
            this.SettingsErrorLabel.AutoSize = true;
            this.SettingsErrorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SettingsErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.SettingsErrorLabel.Location = new System.Drawing.Point(3, 447);
            this.SettingsErrorLabel.Name = "SettingsErrorLabel";
            this.SettingsErrorLabel.Size = new System.Drawing.Size(34, 13);
            this.SettingsErrorLabel.TabIndex = 84;
            this.SettingsErrorLabel.Text = "Error";
            // 
            // DisconnectButton
            // 
            this.DisconnectButton.BackColor = System.Drawing.Color.White;
            this.DisconnectButton.Enabled = false;
            this.DisconnectButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DisconnectButton.Location = new System.Drawing.Point(491, 506);
            this.DisconnectButton.Name = "DisconnectButton";
            this.DisconnectButton.Size = new System.Drawing.Size(316, 50);
            this.DisconnectButton.TabIndex = 83;
            this.DisconnectButton.Text = "Disconnect";
            this.DisconnectButton.UseVisualStyleBackColor = false;
            this.DisconnectButton.Click += new System.EventHandler(this.DisconnectButton_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(3, 420);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(620, 26);
            this.label14.TabIndex = 82;
            this.label14.Text = "NOTE : Currently only StreamDonations is supported. The old data is available on " +
    "their servers, although they moved to StreamTip.\r\n             StreamTip will be" +
    " supported soon.";
            // 
            // DonationsLabel
            // 
            this.DonationsLabel.AutoSize = true;
            this.DonationsLabel.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DonationsLabel.Location = new System.Drawing.Point(290, 367);
            this.DonationsLabel.Name = "DonationsLabel";
            this.DonationsLabel.Size = new System.Drawing.Size(63, 19);
            this.DonationsLabel.TabIndex = 81;
            this.DonationsLabel.Text = "Donations";
            this.DonationsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SubscribersLabel
            // 
            this.SubscribersLabel.AutoSize = true;
            this.SubscribersLabel.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SubscribersLabel.Location = new System.Drawing.Point(292, 311);
            this.SubscribersLabel.Name = "SubscribersLabel";
            this.SubscribersLabel.Size = new System.Drawing.Size(72, 19);
            this.SubscribersLabel.TabIndex = 64;
            this.SubscribersLabel.Text = "Subscribers";
            this.SubscribersLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CurrencyHandoutAmount
            // 
            this.CurrencyHandoutAmount.Location = new System.Drawing.Point(6, 207);
            this.CurrencyHandoutAmount.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.CurrencyHandoutAmount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.CurrencyHandoutAmount.Name = "CurrencyHandoutAmount";
            this.CurrencyHandoutAmount.Size = new System.Drawing.Size(49, 20);
            this.CurrencyHandoutAmount.TabIndex = 79;
            this.CurrencyHandoutAmount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // DonationsSpacer
            // 
            this.DonationsSpacer.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Bold);
            this.DonationsSpacer.Location = new System.Drawing.Point(-1, 367);
            this.DonationsSpacer.Name = "DonationsSpacer";
            this.DonationsSpacer.Size = new System.Drawing.Size(816, 11);
            this.DonationsSpacer.TabIndex = 80;
            this.DonationsSpacer.TabStop = false;
            // 
            // CurrencyHandoutInterval
            // 
            this.CurrencyHandoutInterval.Location = new System.Drawing.Point(6, 168);
            this.CurrencyHandoutInterval.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.CurrencyHandoutInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.CurrencyHandoutInterval.Name = "CurrencyHandoutInterval";
            this.CurrencyHandoutInterval.Size = new System.Drawing.Size(49, 20);
            this.CurrencyHandoutInterval.TabIndex = 78;
            this.CurrencyHandoutInterval.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // CurrencyLabel
            // 
            this.CurrencyLabel.AutoSize = true;
            this.CurrencyLabel.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrencyLabel.Location = new System.Drawing.Point(289, 138);
            this.CurrencyLabel.Name = "CurrencyLabel";
            this.CurrencyLabel.Size = new System.Drawing.Size(61, 19);
            this.CurrencyLabel.TabIndex = 62;
            this.CurrencyLabel.Text = "Currency";
            this.CurrencyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ConnectionLabel
            // 
            this.ConnectionLabel.AutoSize = true;
            this.ConnectionLabel.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Bold);
            this.ConnectionLabel.Location = new System.Drawing.Point(289, 2);
            this.ConnectionLabel.Name = "ConnectionLabel";
            this.ConnectionLabel.Size = new System.Drawing.Size(70, 19);
            this.ConnectionLabel.TabIndex = 60;
            this.ConnectionLabel.Text = "Connection";
            this.ConnectionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CurrencySpacer
            // 
            this.CurrencySpacer.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Bold);
            this.CurrencySpacer.Location = new System.Drawing.Point(-1, 138);
            this.CurrencySpacer.Name = "CurrencySpacer";
            this.CurrencySpacer.Size = new System.Drawing.Size(816, 11);
            this.CurrencySpacer.TabIndex = 61;
            this.CurrencySpacer.TabStop = false;
            // 
            // SubscribersSpacer
            // 
            this.SubscribersSpacer.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Bold);
            this.SubscribersSpacer.Location = new System.Drawing.Point(-1, 311);
            this.SubscribersSpacer.Name = "SubscribersSpacer";
            this.SubscribersSpacer.Size = new System.Drawing.Size(816, 11);
            this.SubscribersSpacer.TabIndex = 63;
            this.SubscribersSpacer.TabStop = false;
            // 
            // ConnectionSpacer
            // 
            this.ConnectionSpacer.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Bold);
            this.ConnectionSpacer.Location = new System.Drawing.Point(-1, 0);
            this.ConnectionSpacer.Name = "ConnectionSpacer";
            this.ConnectionSpacer.Size = new System.Drawing.Size(816, 11);
            this.ConnectionSpacer.TabIndex = 50;
            this.ConnectionSpacer.TabStop = false;
            // 
            // DonationsKeyBox
            // 
            this.DonationsKeyBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DonationsKeyBox.Location = new System.Drawing.Point(6, 397);
            this.DonationsKeyBox.Name = "DonationsKeyBox";
            this.DonationsKeyBox.PasswordChar = '*';
            this.DonationsKeyBox.Size = new System.Drawing.Size(801, 20);
            this.DonationsKeyBox.TabIndex = 76;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(3, 18);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(57, 13);
            this.label13.TabIndex = 60;
            this.label13.Text = "Bot Name:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 381);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(132, 13);
            this.label8.TabIndex = 77;
            this.label8.Text = "StreamDonations API Key:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 57);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(105, 13);
            this.label12.TabIndex = 65;
            this.label12.Text = "Bot\'s Access Token:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 230);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(83, 13);
            this.label11.TabIndex = 61;
            this.label11.Text = "Currency Name:";
            // 
            // SubLinkBox
            // 
            this.SubLinkBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SubLinkBox.Location = new System.Drawing.Point(6, 341);
            this.SubLinkBox.Name = "SubLinkBox";
            this.SubLinkBox.Size = new System.Drawing.Size(801, 20);
            this.SubLinkBox.TabIndex = 68;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 152);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(137, 13);
            this.label10.TabIndex = 62;
            this.label10.Text = "Payout Interval (in minutes):";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 325);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 13);
            this.label7.TabIndex = 74;
            this.label7.Text = "Sub Spreadsheet:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 96);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(49, 13);
            this.label9.TabIndex = 63;
            this.label9.Text = "Channel:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 191);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 72;
            this.label1.Text = "Payout Amount:";
            // 
            // ConnectButton
            // 
            this.ConnectButton.BackColor = System.Drawing.Color.White;
            this.ConnectButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ConnectButton.Location = new System.Drawing.Point(6, 506);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(316, 50);
            this.ConnectButton.TabIndex = 0;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = false;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // BotPasswordBox
            // 
            this.BotPasswordBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BotPasswordBox.Location = new System.Drawing.Point(6, 73);
            this.BotPasswordBox.Name = "BotPasswordBox";
            this.BotPasswordBox.PasswordChar = '*';
            this.BotPasswordBox.Size = new System.Drawing.Size(617, 20);
            this.BotPasswordBox.TabIndex = 66;
            this.BotPasswordBox.TextChanged += new System.EventHandler(this.ConnectionDetailsChanged);
            // 
            // BotNameBox
            // 
            this.BotNameBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BotNameBox.Location = new System.Drawing.Point(6, 34);
            this.BotNameBox.Name = "BotNameBox";
            this.BotNameBox.Size = new System.Drawing.Size(801, 20);
            this.BotNameBox.TabIndex = 64;
            this.BotNameBox.Text = "ModBot";
            this.BotNameBox.TextChanged += new System.EventHandler(this.ConnectionDetailsChanged);
            // 
            // ChannelBox
            // 
            this.ChannelBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ChannelBox.Location = new System.Drawing.Point(6, 112);
            this.ChannelBox.Name = "ChannelBox";
            this.ChannelBox.Size = new System.Drawing.Size(801, 20);
            this.ChannelBox.TabIndex = 67;
            this.ChannelBox.Text = "ModChannel";
            this.ChannelBox.TextChanged += new System.EventHandler(this.ConnectionDetailsChanged);
            // 
            // CurrencyNameBox
            // 
            this.CurrencyNameBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CurrencyNameBox.Location = new System.Drawing.Point(6, 246);
            this.CurrencyNameBox.Name = "CurrencyNameBox";
            this.CurrencyNameBox.Size = new System.Drawing.Size(801, 20);
            this.CurrencyNameBox.TabIndex = 71;
            this.CurrencyNameBox.Text = "Mod Coins";
            this.CurrencyNameBox.TextChanged += new System.EventHandler(this.ConnectionDetailsChanged);
            // 
            // DonationsWindow
            // 
            this.DonationsWindow.BackColor = System.Drawing.Color.White;
            this.DonationsWindow.Controls.Add(this.RecentDonorsLimit);
            this.DonationsWindow.Controls.Add(this.UpdateTopDonorsCheckBox);
            this.DonationsWindow.Controls.Add(this.label15);
            this.DonationsWindow.Controls.Add(this.Donations_List);
            this.DonationsWindow.Controls.Add(this.TopDonorsLimit);
            this.DonationsWindow.Controls.Add(this.UpdateRecentDonorsCheckBox);
            this.DonationsWindow.Controls.Add(this.label16);
            this.DonationsWindow.Controls.Add(this.UpdateLastDonorCheckBox);
            this.DonationsWindow.Location = new System.Drawing.Point(108, 30);
            this.DonationsWindow.Name = "DonationsWindow";
            this.DonationsWindow.Size = new System.Drawing.Size(814, 562);
            this.DonationsWindow.TabIndex = 59;
            // 
            // RecentDonorsLimit
            // 
            this.RecentDonorsLimit.Location = new System.Drawing.Point(374, 23);
            this.RecentDonorsLimit.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.RecentDonorsLimit.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.RecentDonorsLimit.Name = "RecentDonorsLimit";
            this.RecentDonorsLimit.Size = new System.Drawing.Size(36, 20);
            this.RecentDonorsLimit.TabIndex = 67;
            this.RecentDonorsLimit.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.RecentDonorsLimit.ValueChanged += new System.EventHandler(this.RecentDonorsLimit_ValueChanged);
            // 
            // UpdateTopDonorsCheckBox
            // 
            this.UpdateTopDonorsCheckBox.AutoSize = true;
            this.UpdateTopDonorsCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UpdateTopDonorsCheckBox.Location = new System.Drawing.Point(6, 5);
            this.UpdateTopDonorsCheckBox.Name = "UpdateTopDonorsCheckBox";
            this.UpdateTopDonorsCheckBox.Size = new System.Drawing.Size(134, 17);
            this.UpdateTopDonorsCheckBox.TabIndex = 63;
            this.UpdateTopDonorsCheckBox.Text = "Auto-update top donors";
            this.UpdateTopDonorsCheckBox.UseVisualStyleBackColor = true;
            this.UpdateTopDonorsCheckBox.CheckedChanged += new System.EventHandler(this.UpdateTopDonorsCheckBox_CheckedChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(315, 25);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(168, 13);
            this.label15.TabIndex = 66;
            this.label15.Text = "Show up to              recent donors";
            // 
            // Donations_List
            // 
            this.Donations_List.AllowUserToAddRows = false;
            this.Donations_List.AllowUserToDeleteRows = false;
            this.Donations_List.AllowUserToResizeColumns = false;
            this.Donations_List.AllowUserToResizeRows = false;
            this.Donations_List.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Donations_List.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Date,
            this.Donor,
            this.Amount,
            this.ID,
            this.Notes,
            this.IncludeRecent,
            this.IncludeLatest,
            this.IncludeTop,
            this.IncludeTopDonor});
            this.Donations_List.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.Donations_List.Location = new System.Drawing.Point(0, 51);
            this.Donations_List.MultiSelect = false;
            this.Donations_List.Name = "Donations_List";
            this.Donations_List.RowHeadersVisible = false;
            this.Donations_List.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.Donations_List.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Donations_List.Size = new System.Drawing.Size(814, 266);
            this.Donations_List.TabIndex = 60;
            this.Donations_List.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.Donations_List_CellValueChanged);
            this.Donations_List.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.Donations_List_SortCompare);
            // 
            // Date
            // 
            this.Date.HeaderText = "Date";
            this.Date.Name = "Date";
            this.Date.ReadOnly = true;
            this.Date.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Date.Width = 110;
            // 
            // Donor
            // 
            this.Donor.HeaderText = "Donor";
            this.Donor.Name = "Donor";
            this.Donor.ReadOnly = true;
            this.Donor.Width = 130;
            // 
            // Amount
            // 
            this.Amount.HeaderText = "Amount";
            this.Amount.Name = "Amount";
            this.Amount.ReadOnly = true;
            this.Amount.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Amount.Width = 66;
            // 
            // ID
            // 
            this.ID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ID.Visible = false;
            // 
            // Notes
            // 
            this.Notes.HeaderText = "Notes";
            this.Notes.Name = "Notes";
            this.Notes.ReadOnly = true;
            this.Notes.Width = 300;
            // 
            // IncludeRecent
            // 
            this.IncludeRecent.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.IncludeRecent.HeaderText = "Recent";
            this.IncludeRecent.Name = "IncludeRecent";
            this.IncludeRecent.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.IncludeRecent.Width = 48;
            // 
            // IncludeLatest
            // 
            this.IncludeLatest.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.IncludeLatest.HeaderText = "Latest";
            this.IncludeLatest.Name = "IncludeLatest";
            this.IncludeLatest.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.IncludeLatest.Width = 42;
            // 
            // IncludeTop
            // 
            this.IncludeTop.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.IncludeTop.HeaderText = "Top";
            this.IncludeTop.Name = "IncludeTop";
            this.IncludeTop.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.IncludeTop.Width = 32;
            // 
            // IncludeTopDonor
            // 
            this.IncludeTopDonor.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.IncludeTopDonor.HeaderText = "Top Donor";
            this.IncludeTopDonor.Name = "IncludeTopDonor";
            this.IncludeTopDonor.Visible = false;
            // 
            // TopDonorsLimit
            // 
            this.TopDonorsLimit.Location = new System.Drawing.Point(62, 23);
            this.TopDonorsLimit.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.TopDonorsLimit.Name = "TopDonorsLimit";
            this.TopDonorsLimit.Size = new System.Drawing.Size(36, 20);
            this.TopDonorsLimit.TabIndex = 65;
            this.TopDonorsLimit.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.TopDonorsLimit.ValueChanged += new System.EventHandler(this.TopDonorsLimit_ValueChanged);
            // 
            // UpdateRecentDonorsCheckBox
            // 
            this.UpdateRecentDonorsCheckBox.AutoSize = true;
            this.UpdateRecentDonorsCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UpdateRecentDonorsCheckBox.Location = new System.Drawing.Point(318, 4);
            this.UpdateRecentDonorsCheckBox.Name = "UpdateRecentDonorsCheckBox";
            this.UpdateRecentDonorsCheckBox.Size = new System.Drawing.Size(149, 17);
            this.UpdateRecentDonorsCheckBox.TabIndex = 61;
            this.UpdateRecentDonorsCheckBox.Text = "Auto-update recent donors";
            this.UpdateRecentDonorsCheckBox.UseVisualStyleBackColor = true;
            this.UpdateRecentDonorsCheckBox.CheckedChanged += new System.EventHandler(this.UpdateRecentDonorsCheckBox_CheckedChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(3, 25);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(153, 13);
            this.label16.TabIndex = 64;
            this.label16.Text = "Show up to              top donors";
            // 
            // UpdateLastDonorCheckBox
            // 
            this.UpdateLastDonorCheckBox.AutoSize = true;
            this.UpdateLastDonorCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UpdateLastDonorCheckBox.Location = new System.Drawing.Point(675, 5);
            this.UpdateLastDonorCheckBox.Name = "UpdateLastDonorCheckBox";
            this.UpdateLastDonorCheckBox.Size = new System.Drawing.Size(130, 17);
            this.UpdateLastDonorCheckBox.TabIndex = 62;
            this.UpdateLastDonorCheckBox.Text = "Auto-update last donor";
            this.UpdateLastDonorCheckBox.UseVisualStyleBackColor = true;
            this.UpdateLastDonorCheckBox.CheckedChanged += new System.EventHandler(this.UpdateLastDonorCheckBox_CheckedChanged);
            // 
            // AboutWindowButton
            // 
            this.AboutWindowButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.AboutWindowButton.FlatAppearance.BorderSize = 0;
            this.AboutWindowButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.AboutWindowButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AboutWindowButton.Font = new System.Drawing.Font("Segoe Print", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AboutWindowButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.AboutWindowButton.Location = new System.Drawing.Point(8, 243);
            this.AboutWindowButton.Name = "AboutWindowButton";
            this.AboutWindowButton.Size = new System.Drawing.Size(100, 46);
            this.AboutWindowButton.TabIndex = 60;
            this.AboutWindowButton.TabStop = false;
            this.AboutWindowButton.Text = "About";
            this.AboutWindowButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.AboutWindowButton.UseVisualStyleBackColor = true;
            this.AboutWindowButton.CheckedChanged += new System.EventHandler(this.WindowChanged);
            // 
            // AboutWindow
            // 
            this.AboutWindow.BackColor = System.Drawing.Color.White;
            this.AboutWindow.Controls.Add(this.SupportLinkLabel);
            this.AboutWindow.Controls.Add(this.EmailLinkLabel);
            this.AboutWindow.Controls.Add(this.label17);
            this.AboutWindow.Controls.Add(this.WebsiteLinkLabel);
            this.AboutWindow.Controls.Add(this.label18);
            this.AboutWindow.Controls.Add(this.pictureBox1);
            this.AboutWindow.Controls.Add(this.VersionLabel);
            this.AboutWindow.Controls.Add(this.label19);
            this.AboutWindow.Controls.Add(this.label20);
            this.AboutWindow.Location = new System.Drawing.Point(108, 30);
            this.AboutWindow.Name = "AboutWindow";
            this.AboutWindow.Size = new System.Drawing.Size(814, 562);
            this.AboutWindow.TabIndex = 61;
            // 
            // SupportLinkLabel
            // 
            this.SupportLinkLabel.AutoSize = true;
            this.SupportLinkLabel.Location = new System.Drawing.Point(232, 178);
            this.SupportLinkLabel.Name = "SupportLinkLabel";
            this.SupportLinkLabel.Size = new System.Drawing.Size(28, 13);
            this.SupportLinkLabel.TabIndex = 23;
            this.SupportLinkLabel.TabStop = true;
            this.SupportLinkLabel.Text = "here";
            this.SupportLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.SupportLinkLabel_LinkClicked);
            // 
            // EmailLinkLabel
            // 
            this.EmailLinkLabel.AutoSize = true;
            this.EmailLinkLabel.Location = new System.Drawing.Point(176, 178);
            this.EmailLinkLabel.Name = "EmailLinkLabel";
            this.EmailLinkLabel.Size = new System.Drawing.Size(32, 13);
            this.EmailLinkLabel.TabIndex = 22;
            this.EmailLinkLabel.TabStop = true;
            this.EmailLinkLabel.Text = "Email";
            this.EmailLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.EmailLinkLabel_LinkClicked);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(3, 178);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(233, 13);
            this.label17.TabIndex = 21;
            this.label17.Text = "For help/support/feedback send an           or go";
            // 
            // WebsiteLinkLabel
            // 
            this.WebsiteLinkLabel.AutoSize = true;
            this.WebsiteLinkLabel.Location = new System.Drawing.Point(166, 230);
            this.WebsiteLinkLabel.Name = "WebsiteLinkLabel";
            this.WebsiteLinkLabel.Size = new System.Drawing.Size(28, 13);
            this.WebsiteLinkLabel.TabIndex = 20;
            this.WebsiteLinkLabel.TabStop = true;
            this.WebsiteLinkLabel.Text = "here";
            this.WebsiteLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.WebsiteLinkLabel_LinkClicked);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(3, 230);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(282, 13);
            this.label18.TabIndex = 19;
            this.label18.Text = "New versions and notes available          and in the updater";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.Image = global::ModBot.Properties.Resources.AboutImage;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(814, 83);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 18;
            this.pictureBox1.TabStop = false;
            // 
            // VersionLabel
            // 
            this.VersionLabel.AutoSize = true;
            this.VersionLabel.Location = new System.Drawing.Point(45, 135);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(36, 13);
            this.VersionLabel.TabIndex = 17;
            this.VersionLabel.Text = "x.x.x.x";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(3, 135);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(45, 13);
            this.label19.TabIndex = 16;
            this.label19.Text = "Version:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(3, 86);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(250, 26);
            this.label20.TabIndex = 15;
            this.label20.Text = "ModBot © Jonathan \"Keirathi\" Smith, 2013\r\nModified by CoMaNdO";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Location = new System.Drawing.Point(108, 30);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(814, 562);
            this.panel2.TabIndex = 62;
            // 
            // AuthenticationBrowser
            // 
            this.AuthenticationBrowser.Location = new System.Drawing.Point(108, 30);
            this.AuthenticationBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.AuthenticationBrowser.Name = "AuthenticationBrowser";
            this.AuthenticationBrowser.ScriptErrorsSuppressed = true;
            this.AuthenticationBrowser.ScrollBarsEnabled = false;
            this.AuthenticationBrowser.Size = new System.Drawing.Size(814, 562);
            this.AuthenticationBrowser.TabIndex = 0;
            this.AuthenticationBrowser.WebBrowserShortcutsEnabled = false;
            this.AuthenticationBrowser.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.AuthenticationBrowser_Navigated);
            // 
            // ChannelWindowButton
            // 
            this.ChannelWindowButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.ChannelWindowButton.Enabled = false;
            this.ChannelWindowButton.FlatAppearance.BorderSize = 0;
            this.ChannelWindowButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.ChannelWindowButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ChannelWindowButton.Font = new System.Drawing.Font("Segoe Print", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChannelWindowButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ChannelWindowButton.Location = new System.Drawing.Point(8, 73);
            this.ChannelWindowButton.Name = "ChannelWindowButton";
            this.ChannelWindowButton.Size = new System.Drawing.Size(100, 46);
            this.ChannelWindowButton.TabIndex = 63;
            this.ChannelWindowButton.TabStop = false;
            this.ChannelWindowButton.Text = "Channel";
            this.ChannelWindowButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ChannelWindowButton.UseVisualStyleBackColor = true;
            this.ChannelWindowButton.CheckedChanged += new System.EventHandler(this.WindowChanged);
            // 
            // ChannelWindow
            // 
            this.ChannelWindow.BackColor = System.Drawing.Color.White;
            this.ChannelWindow.Controls.Add(this.ChannelStatusLabel);
            this.ChannelWindow.Controls.Add(this.label2);
            this.ChannelWindow.Controls.Add(this.ChannelTitleTextBox);
            this.ChannelWindow.Controls.Add(this.label3);
            this.ChannelWindow.Controls.Add(this.ChannelGameTextBox);
            this.ChannelWindow.Location = new System.Drawing.Point(108, 30);
            this.ChannelWindow.Name = "ChannelWindow";
            this.ChannelWindow.Size = new System.Drawing.Size(814, 562);
            this.ChannelWindow.TabIndex = 63;
            // 
            // CurrencyWindow
            // 
            this.CurrencyWindow.BackColor = System.Drawing.Color.White;
            this.CurrencyWindow.Controls.Add(this.HandoutLabel);
            this.CurrencyWindow.Controls.Add(this.HandoutSpacer);
            this.CurrencyWindow.Controls.Add(this.Currency_HandoutLastActive);
            this.CurrencyWindow.Controls.Add(this.Currency_HandoutActiveTime);
            this.CurrencyWindow.Controls.Add(this.Currency_HandoutActiveStream);
            this.CurrencyWindow.Controls.Add(this.Currency_HandoutEveryone);
            this.CurrencyWindow.Controls.Add(this.Currency_HandoutLabel);
            this.CurrencyWindow.Controls.Add(this.Currency_DisableCommandCheckBox);
            this.CurrencyWindow.Location = new System.Drawing.Point(108, 30);
            this.CurrencyWindow.Name = "CurrencyWindow";
            this.CurrencyWindow.Size = new System.Drawing.Size(814, 562);
            this.CurrencyWindow.TabIndex = 63;
            // 
            // CurrencyWindowButton
            // 
            this.CurrencyWindowButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.CurrencyWindowButton.Enabled = false;
            this.CurrencyWindowButton.FlatAppearance.BorderSize = 0;
            this.CurrencyWindowButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.CurrencyWindowButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CurrencyWindowButton.Font = new System.Drawing.Font("Segoe Print", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrencyWindowButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.CurrencyWindowButton.Location = new System.Drawing.Point(8, 110);
            this.CurrencyWindowButton.Name = "CurrencyWindowButton";
            this.CurrencyWindowButton.Size = new System.Drawing.Size(100, 46);
            this.CurrencyWindowButton.TabIndex = 64;
            this.CurrencyWindowButton.TabStop = false;
            this.CurrencyWindowButton.Text = "Currency";
            this.CurrencyWindowButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CurrencyWindowButton.UseVisualStyleBackColor = true;
            this.CurrencyWindowButton.CheckedChanged += new System.EventHandler(this.WindowChanged);
            // 
            // GiveawayWindowButton
            // 
            this.GiveawayWindowButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.GiveawayWindowButton.Enabled = false;
            this.GiveawayWindowButton.FlatAppearance.BorderSize = 0;
            this.GiveawayWindowButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.GiveawayWindowButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GiveawayWindowButton.Font = new System.Drawing.Font("Segoe Print", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GiveawayWindowButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.GiveawayWindowButton.Location = new System.Drawing.Point(8, 149);
            this.GiveawayWindowButton.Name = "GiveawayWindowButton";
            this.GiveawayWindowButton.Size = new System.Drawing.Size(100, 46);
            this.GiveawayWindowButton.TabIndex = 65;
            this.GiveawayWindowButton.TabStop = false;
            this.GiveawayWindowButton.Text = "Giveaway";
            this.GiveawayWindowButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.GiveawayWindowButton.UseVisualStyleBackColor = true;
            this.GiveawayWindowButton.CheckedChanged += new System.EventHandler(this.WindowChanged);
            // 
            // GiveawayWindow
            // 
            this.GiveawayWindow.BackColor = System.Drawing.Color.White;
            this.GiveawayWindow.Controls.Add(this.GiveawayBansLabel);
            this.GiveawayWindow.Controls.Add(this.GiveawayBansSpacer);
            this.GiveawayWindow.Controls.Add(this.GiveawayRulesLabel);
            this.GiveawayWindow.Controls.Add(this.GiveawayRulesSpacer);
            this.GiveawayWindow.Controls.Add(this.Giveaway_TypeTickets);
            this.GiveawayWindow.Controls.Add(this.Giveaway_TypeKeyword);
            this.GiveawayWindow.Controls.Add(this.Giveaway_MinCurrency);
            this.GiveawayWindow.Controls.Add(this.SettingsPresents);
            this.GiveawayWindow.Controls.Add(this.Giveaway_WinTimeLabel);
            this.GiveawayWindow.Controls.Add(this.Giveaway_AddBanTextBox);
            this.GiveawayWindow.Controls.Add(this.Giveaway_MustFollowCheckBox);
            this.GiveawayWindow.Controls.Add(this.Giveaway_WinnerChat);
            this.GiveawayWindow.Controls.Add(this.Giveaway_WinnerTimerLabel);
            this.GiveawayWindow.Controls.Add(this.Giveaway_StartButton);
            this.GiveawayWindow.Controls.Add(this.Giveaway_CopyWinnerButton);
            this.GiveawayWindow.Controls.Add(this.Giveaway_RerollButton);
            this.GiveawayWindow.Controls.Add(this.Giveaway_StopButton);
            this.GiveawayWindow.Controls.Add(this.Giveaway_AutoBanWinnerCheckBox);
            this.GiveawayWindow.Controls.Add(this.Giveaway_UnbanButton);
            this.GiveawayWindow.Controls.Add(this.Giveaway_ActiveUserTime);
            this.GiveawayWindow.Controls.Add(this.Giveaway_BanButton);
            this.GiveawayWindow.Controls.Add(this.Giveaway_AnnounceWinnerButton);
            this.GiveawayWindow.Controls.Add(this.Giveaway_BanListListBox);
            this.GiveawayWindow.Controls.Add(this.label6);
            this.GiveawayWindow.Controls.Add(this.Giveaway_WinnerStatusLabel);
            this.GiveawayWindow.Controls.Add(this.Giveaway_WinnerLabel);
            this.GiveawayWindow.Controls.Add(this.GiveawayTypeLabel);
            this.GiveawayWindow.Controls.Add(this.GiveawayTypeSpacer);
            this.GiveawayWindow.Controls.Add(this.Giveaway_MinCurrencyCheckBox);
            this.GiveawayWindow.Controls.Add(this.Giveaway_TypeActive);
            this.GiveawayWindow.Location = new System.Drawing.Point(108, 30);
            this.GiveawayWindow.Name = "GiveawayWindow";
            this.GiveawayWindow.Size = new System.Drawing.Size(814, 562);
            this.GiveawayWindow.TabIndex = 63;
            // 
            // GiveawayBansLabel
            // 
            this.GiveawayBansLabel.AutoSize = true;
            this.GiveawayBansLabel.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GiveawayBansLabel.Location = new System.Drawing.Point(381, 25);
            this.GiveawayBansLabel.Name = "GiveawayBansLabel";
            this.GiveawayBansLabel.Size = new System.Drawing.Size(89, 19);
            this.GiveawayBansLabel.TabIndex = 53;
            this.GiveawayBansLabel.Text = "Giveaway bans";
            this.GiveawayBansLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GiveawayBansSpacer
            // 
            this.GiveawayBansSpacer.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Bold);
            this.GiveawayBansSpacer.Location = new System.Drawing.Point(318, 25);
            this.GiveawayBansSpacer.Name = "GiveawayBansSpacer";
            this.GiveawayBansSpacer.Size = new System.Drawing.Size(497, 11);
            this.GiveawayBansSpacer.TabIndex = 54;
            this.GiveawayBansSpacer.TabStop = false;
            // 
            // GiveawayRulesLabel
            // 
            this.GiveawayRulesLabel.AutoSize = true;
            this.GiveawayRulesLabel.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GiveawayRulesLabel.Location = new System.Drawing.Point(58, 124);
            this.GiveawayRulesLabel.Name = "GiveawayRulesLabel";
            this.GiveawayRulesLabel.Size = new System.Drawing.Size(92, 19);
            this.GiveawayRulesLabel.TabIndex = 51;
            this.GiveawayRulesLabel.Text = "Giveaway rules";
            this.GiveawayRulesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GiveawayRulesSpacer
            // 
            this.GiveawayRulesSpacer.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Bold);
            this.GiveawayRulesSpacer.Location = new System.Drawing.Point(-1, 124);
            this.GiveawayRulesSpacer.Name = "GiveawayRulesSpacer";
            this.GiveawayRulesSpacer.Size = new System.Drawing.Size(320, 11);
            this.GiveawayRulesSpacer.TabIndex = 52;
            this.GiveawayRulesSpacer.TabStop = false;
            // 
            // Giveaway_TypeTickets
            // 
            this.Giveaway_TypeTickets.AutoSize = true;
            this.Giveaway_TypeTickets.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Giveaway_TypeTickets.Location = new System.Drawing.Point(6, 76);
            this.Giveaway_TypeTickets.Name = "Giveaway_TypeTickets";
            this.Giveaway_TypeTickets.Size = new System.Drawing.Size(59, 17);
            this.Giveaway_TypeTickets.TabIndex = 50;
            this.Giveaway_TypeTickets.Text = "Tickets";
            this.Giveaway_TypeTickets.UseVisualStyleBackColor = true;
            this.Giveaway_TypeTickets.CheckedChanged += new System.EventHandler(this.Giveaway_TypeTickets_CheckedChanged);
            // 
            // Giveaway_TypeKeyword
            // 
            this.Giveaway_TypeKeyword.AutoSize = true;
            this.Giveaway_TypeKeyword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Giveaway_TypeKeyword.Location = new System.Drawing.Point(6, 61);
            this.Giveaway_TypeKeyword.Name = "Giveaway_TypeKeyword";
            this.Giveaway_TypeKeyword.Size = new System.Drawing.Size(65, 17);
            this.Giveaway_TypeKeyword.TabIndex = 49;
            this.Giveaway_TypeKeyword.Text = "Keyword";
            this.Giveaway_TypeKeyword.UseVisualStyleBackColor = true;
            this.Giveaway_TypeKeyword.CheckedChanged += new System.EventHandler(this.Giveaway_Settings_Changed);
            // 
            // Giveaway_TypeActive
            // 
            this.Giveaway_TypeActive.AutoSize = true;
            this.Giveaway_TypeActive.Checked = true;
            this.Giveaway_TypeActive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Giveaway_TypeActive.Location = new System.Drawing.Point(6, 46);
            this.Giveaway_TypeActive.Name = "Giveaway_TypeActive";
            this.Giveaway_TypeActive.Size = new System.Drawing.Size(223, 17);
            this.Giveaway_TypeActive.TabIndex = 48;
            this.Giveaway_TypeActive.TabStop = true;
            this.Giveaway_TypeActive.Text = "Last active less than               minutes ago";
            this.Giveaway_TypeActive.UseVisualStyleBackColor = true;
            this.Giveaway_TypeActive.CheckedChanged += new System.EventHandler(this.Giveaway_TypeActive_CheckedChanged);
            // 
            // AuthenticationLabel
            // 
            this.AuthenticationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AuthenticationLabel.Location = new System.Drawing.Point(108, 30);
            this.AuthenticationLabel.Name = "AuthenticationLabel";
            this.AuthenticationLabel.Size = new System.Drawing.Size(814, 31);
            this.AuthenticationLabel.TabIndex = 66;
            this.AuthenticationLabel.Text = "Connect to the bot\'s account";
            this.AuthenticationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(930, 600);
            this.Controls.Add(this.GiveawayWindowButton);
            this.Controls.Add(this.CurrencyWindowButton);
            this.Controls.Add(this.ChannelWindowButton);
            this.Controls.Add(this.AboutWindowButton);
            this.Controls.Add(this.DonationsWindowButton);
            this.Controls.Add(this.SettingsWindowButton);
            this.Controls.Add(this.SettingsWindow);
            this.Controls.Add(this.DonationsWindow);
            this.Controls.Add(this.ChannelWindow);
            this.Controls.Add(this.CurrencyWindow);
            this.Controls.Add(this.GiveawayWindow);
            this.Controls.Add(this.AuthenticationBrowser);
            this.Controls.Add(this.AuthenticationLabel);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.AboutWindow);
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.Text = "ModBot";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.Controls.SetChildIndex(this.AboutWindow, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.Controls.SetChildIndex(this.AuthenticationLabel, 0);
            this.Controls.SetChildIndex(this.AuthenticationBrowser, 0);
            this.Controls.SetChildIndex(this.GiveawayWindow, 0);
            this.Controls.SetChildIndex(this.CurrencyWindow, 0);
            this.Controls.SetChildIndex(this.ChannelWindow, 0);
            this.Controls.SetChildIndex(this.DonationsWindow, 0);
            this.Controls.SetChildIndex(this.SettingsWindow, 0);
            this.Controls.SetChildIndex(this.SettingsWindowButton, 0);
            this.Controls.SetChildIndex(this.DonationsWindowButton, 0);
            this.Controls.SetChildIndex(this.AboutWindowButton, 0);
            this.Controls.SetChildIndex(this.ChannelWindowButton, 0);
            this.Controls.SetChildIndex(this.CurrencyWindowButton, 0);
            this.Controls.SetChildIndex(this.GiveawayWindowButton, 0);
            ((System.ComponentModel.ISupportInitialize)(this.Giveaway_MinCurrency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Giveaway_ActiveUserTime)).EndInit();
            this.SettingsPresents.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Currency_HandoutLastActive)).EndInit();
            this.SettingsWindow.ResumeLayout(false);
            this.SettingsWindow.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CurrencyHandoutAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CurrencyHandoutInterval)).EndInit();
            this.DonationsWindow.ResumeLayout(false);
            this.DonationsWindow.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RecentDonorsLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Donations_List)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TopDonorsLimit)).EndInit();
            this.AboutWindow.ResumeLayout(false);
            this.AboutWindow.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ChannelWindow.ResumeLayout(false);
            this.ChannelWindow.PerformLayout();
            this.CurrencyWindow.ResumeLayout(false);
            this.CurrencyWindow.PerformLayout();
            this.GiveawayWindow.ResumeLayout(false);
            this.GiveawayWindow.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label ChannelStatusLabel;
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
        public System.Windows.Forms.NumericUpDown Giveaway_ActiveUserTime;
        public System.Windows.Forms.Button Giveaway_AnnounceWinnerButton;
        public System.Windows.Forms.ListBox Giveaway_BanListListBox;
        public System.Windows.Forms.Button Giveaway_BanButton;
        public System.Windows.Forms.Button Giveaway_UnbanButton;
        public System.Windows.Forms.TextBox Giveaway_AddBanTextBox;
        public System.Windows.Forms.CheckBox Giveaway_AutoBanWinnerCheckBox;
        public System.Windows.Forms.CheckBox Currency_DisableCommandCheckBox;
        public System.Windows.Forms.Button Giveaway_CopyWinnerButton;
        public System.Windows.Forms.Label Giveaway_WinnerTimerLabel;
        public System.Windows.Forms.Timer Giveaway_WinnerTimer;
        public System.Windows.Forms.RichTextBox Giveaway_WinnerChat;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TabControl SettingsPresents;
        private System.Windows.Forms.TabPage tabPage1;
        public System.Windows.Forms.Label Giveaway_WinTimeLabel;
        private System.Windows.Forms.GroupBox GiveawayTypeSpacer;
        private System.Windows.Forms.Label GiveawayTypeLabel;
        private System.Windows.Forms.Label HandoutLabel;
        private System.Windows.Forms.GroupBox HandoutSpacer;
        public System.Windows.Forms.RadioButton Currency_HandoutEveryone;
        public System.Windows.Forms.RadioButton Currency_HandoutActiveStream;
        public System.Windows.Forms.Label Currency_HandoutLabel;
        public System.Windows.Forms.RadioButton Currency_HandoutActiveTime;
        public System.Windows.Forms.NumericUpDown Currency_HandoutLastActive;
        public System.Windows.Forms.CheckBox SettingsWindowButton;
        public System.Windows.Forms.CheckBox DonationsWindowButton;
        public System.Windows.Forms.Panel SettingsWindow;
        public System.Windows.Forms.Panel DonationsWindow;
        private System.Windows.Forms.GroupBox ConnectionSpacer;
        private System.Windows.Forms.Label ConnectionLabel;
        private System.Windows.Forms.TextBox DonationsKeyBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox SubLinkBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.TextBox BotPasswordBox;
        private System.Windows.Forms.TextBox BotNameBox;
        private System.Windows.Forms.TextBox ChannelBox;
        private System.Windows.Forms.TextBox CurrencyNameBox;
        private System.Windows.Forms.Label CurrencyLabel;
        private System.Windows.Forms.GroupBox CurrencySpacer;
        public System.Windows.Forms.NumericUpDown CurrencyHandoutInterval;
        public System.Windows.Forms.NumericUpDown CurrencyHandoutAmount;
        private System.Windows.Forms.Label DonationsLabel;
        private System.Windows.Forms.Label SubscribersLabel;
        private System.Windows.Forms.GroupBox DonationsSpacer;
        private System.Windows.Forms.GroupBox SubscribersSpacer;
        public System.Windows.Forms.Button DisconnectButton;
        private System.Windows.Forms.Label label14;
        public System.Windows.Forms.NumericUpDown RecentDonorsLimit;
        public System.Windows.Forms.CheckBox UpdateTopDonorsCheckBox;
        private System.Windows.Forms.Label label15;
        public System.Windows.Forms.DataGridView Donations_List;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn Donor;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Notes;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IncludeRecent;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IncludeLatest;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IncludeTop;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IncludeTopDonor;
        public System.Windows.Forms.NumericUpDown TopDonorsLimit;
        public System.Windows.Forms.CheckBox UpdateRecentDonorsCheckBox;
        private System.Windows.Forms.Label label16;
        public System.Windows.Forms.CheckBox UpdateLastDonorCheckBox;
        public System.Windows.Forms.CheckBox AboutWindowButton;
        private System.Windows.Forms.Panel AboutWindow;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.LinkLabel SupportLinkLabel;
        private System.Windows.Forms.LinkLabel EmailLinkLabel;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.LinkLabel WebsiteLinkLabel;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label VersionLabel;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        public System.Windows.Forms.Label SettingsErrorLabel;
        public System.Windows.Forms.CheckBox ChannelWindowButton;
        private System.Windows.Forms.Panel ChannelWindow;
        private System.Windows.Forms.Panel CurrencyWindow;
        public System.Windows.Forms.CheckBox CurrencyWindowButton;
        public System.Windows.Forms.CheckBox GiveawayWindowButton;
        public System.Windows.Forms.Panel GiveawayWindow;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox CurrencyCommandBox;
        public System.Windows.Forms.RadioButton Giveaway_TypeActive;
        public System.Windows.Forms.RadioButton Giveaway_TypeKeyword;
        public System.Windows.Forms.RadioButton Giveaway_TypeTickets;
        private System.Windows.Forms.Label GiveawayRulesLabel;
        private System.Windows.Forms.GroupBox GiveawayRulesSpacer;
        private System.Windows.Forms.Label GiveawayBansLabel;
        private System.Windows.Forms.GroupBox GiveawayBansSpacer;
        public System.Windows.Forms.Button GenerateTokenButton;
        private System.Windows.Forms.WebBrowser AuthenticationBrowser;
        private System.Windows.Forms.Label AuthenticationLabel;
    }
}