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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.ChannelStatusLabel = new System.Windows.Forms.Label();
            this.Giveaway_MustFollow = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Channel_Title = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Channel_Game = new System.Windows.Forms.TextBox();
            this.Giveaway_MinCurrency = new System.Windows.Forms.CheckBox();
            this.Giveaway_MinCurrencyBox = new ModBot.FlatNumericUpDown();
            this.Giveaway_StartButton = new System.Windows.Forms.Button();
            this.Giveaway_RerollButton = new System.Windows.Forms.Button();
            this.Giveaway_StopButton = new System.Windows.Forms.Button();
            this.Giveaway_WinnerStatusLabel = new System.Windows.Forms.Label();
            this.Giveaway_WinnerLabel = new System.Windows.Forms.Label();
            this.Giveaway_ActiveUserTime = new ModBot.FlatNumericUpDown();
            this.Giveaway_AnnounceWinnerButton = new System.Windows.Forms.Button();
            this.Giveaway_BanListListBox = new System.Windows.Forms.ListBox();
            this.Giveaway_BanButton = new System.Windows.Forms.Button();
            this.Giveaway_UnbanButton = new System.Windows.Forms.Button();
            this.Giveaway_AddBanTextBox = new System.Windows.Forms.TextBox();
            this.Giveaway_AutoBanWinner = new System.Windows.Forms.CheckBox();
            this.Currency_DisableCommand = new System.Windows.Forms.CheckBox();
            this.Giveaway_CopyWinnerButton = new System.Windows.Forms.Button();
            this.Giveaway_WinnerTimerLabel = new System.Windows.Forms.Label();
            this.Giveaway_WinnerTimer = new System.Windows.Forms.Timer(this.components);
            this.Giveaway_WinnerChat = new System.Windows.Forms.RichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Giveaway_SettingsPresents = new System.Windows.Forms.TabControl();
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
            this.Currency_HandoutLastActive = new ModBot.FlatNumericUpDown();
            this.SettingsWindow = new System.Windows.Forms.Panel();
            this.SettingsErrorLabel = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.Database_Table = new System.Windows.Forms.TextBox();
            this.MySQL_Port = new ModBot.FlatNumericUpDown();
            this.label25 = new System.Windows.Forms.Label();
            this.MySQL_Password = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.MySQL_Username = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.MySQL_Database = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.MySQL_Host = new System.Windows.Forms.TextBox();
            this.MySQLLabel = new System.Windows.Forms.Label();
            this.MySQLSpacer = new System.Windows.Forms.GroupBox();
            this.Misc_ShowConsole = new System.Windows.Forms.CheckBox();
            this.label19 = new System.Windows.Forms.Label();
            this.Currency_SubHandoutAmount = new ModBot.FlatNumericUpDown();
            this.Donations_ST_Token = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Channel_TokenButton = new System.Windows.Forms.Button();
            this.Channel_Token = new System.Windows.Forms.TextBox();
            this.Bot_TokenButton = new System.Windows.Forms.Button();
            this.label21 = new System.Windows.Forms.Label();
            this.Currency_Command = new System.Windows.Forms.TextBox();
            this.DisconnectButton = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.DonationsLabel = new System.Windows.Forms.Label();
            this.SubscribersLabel = new System.Windows.Forms.Label();
            this.Currency_HandoutAmount = new ModBot.FlatNumericUpDown();
            this.DonationsSpacer = new System.Windows.Forms.GroupBox();
            this.Currency_HandoutInterval = new ModBot.FlatNumericUpDown();
            this.CurrencyLabel = new System.Windows.Forms.Label();
            this.ConnectionLabel = new System.Windows.Forms.Label();
            this.CurrencySpacer = new System.Windows.Forms.GroupBox();
            this.SubscribersSpacer = new System.Windows.Forms.GroupBox();
            this.ConnectionSpacer = new System.Windows.Forms.GroupBox();
            this.Donations_ST_ClientId = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.Subscribers_Spreadsheet = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.Bot_Token = new System.Windows.Forms.TextBox();
            this.Bot_Name = new System.Windows.Forms.TextBox();
            this.Channel_Name = new System.Windows.Forms.TextBox();
            this.Currency_Name = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.DonationsWindow = new System.Windows.Forms.Panel();
            this.RecentDonorsLimit = new ModBot.FlatNumericUpDown();
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
            this.TopDonorsLimit = new ModBot.FlatNumericUpDown();
            this.UpdateRecentDonorsCheckBox = new System.Windows.Forms.CheckBox();
            this.label16 = new System.Windows.Forms.Label();
            this.UpdateLastDonorCheckBox = new System.Windows.Forms.CheckBox();
            this.AboutWindow = new System.Windows.Forms.Panel();
            this.CopyrightLabel = new System.Windows.Forms.LinkLabel();
            this.WebsiteLinkLabel = new System.Windows.Forms.LinkLabel();
            this.About_Users = new System.Windows.Forms.DataGridView();
            this.Channel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Bot = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Game = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Viewers = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Version = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Updated = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DonateImage = new System.Windows.Forms.PictureBox();
            this.SupportLinkLabel = new System.Windows.Forms.LinkLabel();
            this.EmailLinkLabel = new System.Windows.Forms.LinkLabel();
            this.label17 = new System.Windows.Forms.Label();
            this.AboutImage = new System.Windows.Forms.PictureBox();
            this.VersionLabel = new System.Windows.Forms.Label();
            this.About_UsersLabel = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ChannelWindow = new System.Windows.Forms.Panel();
            this.label18 = new System.Windows.Forms.Label();
            this.Channel_WelcomeSubMessage = new System.Windows.Forms.TextBox();
            this.Channel_ViewersChangeMessage = new System.Windows.Forms.TextBox();
            this.Channel_ViewersChangeRate = new ModBot.FlatNumericUpDown();
            this.Channel_ViewersChangeInterval = new ModBot.FlatNumericUpDown();
            this.Channel_ViewersChange = new System.Windows.Forms.CheckBox();
            this.MessageTimersLabel = new System.Windows.Forms.Label();
            this.MessageTimersSpacer = new System.Windows.Forms.GroupBox();
            this.Channel_SubscriptionsDate = new System.Windows.Forms.DateTimePicker();
            this.SubscriptionsLabel = new System.Windows.Forms.Label();
            this.Channel_SubscriptionRewards = new System.Windows.Forms.CheckBox();
            this.SubscriptionsSpacer = new System.Windows.Forms.GroupBox();
            this.Channel_SteamID64 = new System.Windows.Forms.TextBox();
            this.Channel_UseSteam = new System.Windows.Forms.CheckBox();
            this.Channel_UpdateTitleGame = new System.Windows.Forms.Button();
            this.Channel_WelcomeSub = new System.Windows.Forms.CheckBox();
            this.Channel_SubscriptionRewardsList = new System.Windows.Forms.DataGridView();
            this.Reward = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Instructions = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CurrencyWindow = new System.Windows.Forms.Panel();
            this.GiveawayWindow = new System.Windows.Forms.Panel();
            this.Giveaway_SubscribersWinMultiplierAmount = new ModBot.FlatNumericUpDown();
            this.Giveaway_UserList = new System.Windows.Forms.ListBox();
            this.GiveawayUsersLabel = new System.Windows.Forms.Label();
            this.GiveawayUsersSpacer = new System.Windows.Forms.GroupBox();
            this.Giveaway_CustomKeyword = new System.Windows.Forms.TextBox();
            this.Giveaway_AnnounceWarnedEntries = new System.Windows.Forms.CheckBox();
            this.Giveaway_WarnFalseEntries = new System.Windows.Forms.CheckBox();
            this.Giveaway_MustWatchMinutes = new ModBot.FlatNumericUpDown();
            this.Giveaway_MustWatchHours = new ModBot.FlatNumericUpDown();
            this.Giveaway_MustWatchDays = new ModBot.FlatNumericUpDown();
            this.Giveaway_MustWatch = new System.Windows.Forms.CheckBox();
            this.Giveaway_MustSubscribe = new System.Windows.Forms.CheckBox();
            this.Giveaway_MaxTickets = new ModBot.FlatNumericUpDown();
            this.Giveaway_TicketCost = new ModBot.FlatNumericUpDown();
            this.Giveaway_CancelButton = new System.Windows.Forms.Button();
            this.Giveaway_CloseButton = new System.Windows.Forms.Button();
            this.Giveaway_OpenButton = new System.Windows.Forms.Button();
            this.GiveawayBansLabel = new System.Windows.Forms.Label();
            this.GiveawayBansSpacer = new System.Windows.Forms.GroupBox();
            this.GiveawayRulesLabel = new System.Windows.Forms.Label();
            this.GiveawayRulesSpacer = new System.Windows.Forms.GroupBox();
            this.Giveaway_TypeTickets = new System.Windows.Forms.RadioButton();
            this.Giveaway_TypeKeyword = new System.Windows.Forms.RadioButton();
            this.Giveaway_TypeActive = new System.Windows.Forms.RadioButton();
            this.Giveaway_UserCount = new System.Windows.Forms.Label();
            this.Giveaway_SubscribersWinMultiplier = new System.Windows.Forms.CheckBox();
            this.AuthenticationLabel = new System.Windows.Forms.Label();
            this.SpamFilterWindow = new System.Windows.Forms.Panel();
            this.Spam_CWLAnnounceTimeouts = new System.Windows.Forms.CheckBox();
            this.Spam_CWLBox = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.Spam_CWL = new System.Windows.Forms.CheckBox();
            this.Spam_CWLLabel = new System.Windows.Forms.Label();
            this.Spam_CWLSpacer = new System.Windows.Forms.GroupBox();
            this.SongRequestPlayer = new System.Windows.Forms.WebBrowser();
            this.AuthenticationBrowser = new System.Windows.Forms.WebBrowser();
            ((System.ComponentModel.ISupportInitialize)(this.Giveaway_MinCurrencyBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Giveaway_ActiveUserTime)).BeginInit();
            this.Giveaway_SettingsPresents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Currency_HandoutLastActive)).BeginInit();
            this.SettingsWindow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MySQL_Port)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Currency_SubHandoutAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Currency_HandoutAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Currency_HandoutInterval)).BeginInit();
            this.DonationsWindow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RecentDonorsLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Donations_List)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TopDonorsLimit)).BeginInit();
            this.AboutWindow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.About_Users)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DonateImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AboutImage)).BeginInit();
            this.ChannelWindow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Channel_ViewersChangeRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Channel_ViewersChangeInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Channel_SubscriptionRewardsList)).BeginInit();
            this.CurrencyWindow.SuspendLayout();
            this.GiveawayWindow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Giveaway_SubscribersWinMultiplierAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Giveaway_MustWatchMinutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Giveaway_MustWatchHours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Giveaway_MustWatchDays)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Giveaway_MaxTickets)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Giveaway_TicketCost)).BeginInit();
            this.SpamFilterWindow.SuspendLayout();
            this.SuspendLayout();
            // 
            // ChannelStatusLabel
            // 
            this.ChannelStatusLabel.Font = new System.Drawing.Font("Segoe Print", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChannelStatusLabel.ForeColor = System.Drawing.Color.Red;
            this.ChannelStatusLabel.Location = new System.Drawing.Point(0, 0);
            this.ChannelStatusLabel.Name = "ChannelStatusLabel";
            this.ChannelStatusLabel.Size = new System.Drawing.Size(1024, 24);
            this.ChannelStatusLabel.TabIndex = 12;
            this.ChannelStatusLabel.Text = "DISCONNECTED";
            this.ChannelStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Giveaway_MustFollow
            // 
            this.Giveaway_MustFollow.AutoSize = true;
            this.Giveaway_MustFollow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Giveaway_MustFollow.Location = new System.Drawing.Point(6, 145);
            this.Giveaway_MustFollow.Name = "Giveaway_MustFollow";
            this.Giveaway_MustFollow.Size = new System.Drawing.Size(109, 17);
            this.Giveaway_MustFollow.TabIndex = 14;
            this.Giveaway_MustFollow.Text = "Must be a follower";
            this.Giveaway_MustFollow.UseVisualStyleBackColor = true;
            this.Giveaway_MustFollow.CheckedChanged += new System.EventHandler(this.Giveaway_Settings_Changed);
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
            // Channel_Title
            // 
            this.Channel_Title.BackColor = System.Drawing.Color.White;
            this.Channel_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Channel_Title.Location = new System.Drawing.Point(6, 41);
            this.Channel_Title.Name = "Channel_Title";
            this.Channel_Title.Size = new System.Drawing.Size(864, 20);
            this.Channel_Title.TabIndex = 17;
            this.Channel_Title.Text = "Loading...";
            this.Channel_Title.TextChanged += new System.EventHandler(this.TitleGame_Modified);
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
            // Channel_Game
            // 
            this.Channel_Game.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.Channel_Game.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.Channel_Game.BackColor = System.Drawing.Color.White;
            this.Channel_Game.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Channel_Game.Location = new System.Drawing.Point(6, 80);
            this.Channel_Game.Name = "Channel_Game";
            this.Channel_Game.Size = new System.Drawing.Size(864, 20);
            this.Channel_Game.TabIndex = 19;
            this.Channel_Game.Text = "Loading...";
            this.Channel_Game.TextChanged += new System.EventHandler(this.TitleGame_Modified);
            // 
            // Giveaway_MinCurrency
            // 
            this.Giveaway_MinCurrency.AutoSize = true;
            this.Giveaway_MinCurrency.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Giveaway_MinCurrency.Location = new System.Drawing.Point(6, 168);
            this.Giveaway_MinCurrency.Name = "Giveaway_MinCurrency";
            this.Giveaway_MinCurrency.Size = new System.Drawing.Size(239, 17);
            this.Giveaway_MinCurrency.TabIndex = 20;
            this.Giveaway_MinCurrency.Text = "Must have at least                       CURRENCY";
            this.Giveaway_MinCurrency.UseVisualStyleBackColor = true;
            this.Giveaway_MinCurrency.CheckedChanged += new System.EventHandler(this.Giveaway_Settings_Changed);
            // 
            // Giveaway_MinCurrencyBox
            // 
            this.Giveaway_MinCurrencyBox.Enabled = false;
            this.Giveaway_MinCurrencyBox.Location = new System.Drawing.Point(113, 168);
            this.Giveaway_MinCurrencyBox.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.Giveaway_MinCurrencyBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Giveaway_MinCurrencyBox.Name = "Giveaway_MinCurrencyBox";
            this.Giveaway_MinCurrencyBox.Size = new System.Drawing.Size(62, 20);
            this.Giveaway_MinCurrencyBox.TabIndex = 21;
            this.Giveaway_MinCurrencyBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Giveaway_MinCurrencyBox.ValueChanged += new System.EventHandler(this.Giveaway_Settings_Changed);
            // 
            // Giveaway_StartButton
            // 
            this.Giveaway_StartButton.BackColor = System.Drawing.Color.White;
            this.Giveaway_StartButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Giveaway_StartButton.Location = new System.Drawing.Point(6, 533);
            this.Giveaway_StartButton.Name = "Giveaway_StartButton";
            this.Giveaway_StartButton.Size = new System.Drawing.Size(112, 23);
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
            this.Giveaway_RerollButton.Location = new System.Drawing.Point(124, 533);
            this.Giveaway_RerollButton.Name = "Giveaway_RerollButton";
            this.Giveaway_RerollButton.Size = new System.Drawing.Size(109, 23);
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
            this.Giveaway_StopButton.Location = new System.Drawing.Point(584, 533);
            this.Giveaway_StopButton.Name = "Giveaway_StopButton";
            this.Giveaway_StopButton.Size = new System.Drawing.Size(109, 23);
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
            this.Giveaway_AnnounceWinnerButton.Location = new System.Drawing.Point(469, 533);
            this.Giveaway_AnnounceWinnerButton.Name = "Giveaway_AnnounceWinnerButton";
            this.Giveaway_AnnounceWinnerButton.Size = new System.Drawing.Size(109, 23);
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
            this.Giveaway_BanListListBox.Size = new System.Drawing.Size(484, 93);
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
            this.Giveaway_BanButton.Size = new System.Drawing.Size(239, 23);
            this.Giveaway_BanButton.TabIndex = 31;
            this.Giveaway_BanButton.Text = "Ban";
            this.Giveaway_BanButton.UseVisualStyleBackColor = false;
            this.Giveaway_BanButton.Click += new System.EventHandler(this.Giveaway_Settings_Changed);
            // 
            // Giveaway_UnbanButton
            // 
            this.Giveaway_UnbanButton.BackColor = System.Drawing.Color.White;
            this.Giveaway_UnbanButton.Enabled = false;
            this.Giveaway_UnbanButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Giveaway_UnbanButton.Location = new System.Drawing.Point(569, 170);
            this.Giveaway_UnbanButton.Name = "Giveaway_UnbanButton";
            this.Giveaway_UnbanButton.Size = new System.Drawing.Size(239, 23);
            this.Giveaway_UnbanButton.TabIndex = 32;
            this.Giveaway_UnbanButton.Text = "Unban";
            this.Giveaway_UnbanButton.UseVisualStyleBackColor = false;
            this.Giveaway_UnbanButton.Click += new System.EventHandler(this.Giveaway_Settings_Changed);
            // 
            // Giveaway_AddBanTextBox
            // 
            this.Giveaway_AddBanTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Giveaway_AddBanTextBox.Location = new System.Drawing.Point(324, 144);
            this.Giveaway_AddBanTextBox.Name = "Giveaway_AddBanTextBox";
            this.Giveaway_AddBanTextBox.Size = new System.Drawing.Size(484, 20);
            this.Giveaway_AddBanTextBox.TabIndex = 33;
            this.Giveaway_AddBanTextBox.TextChanged += new System.EventHandler(this.Giveaway_AddBanTextBox_TextChanged);
            // 
            // Giveaway_AutoBanWinner
            // 
            this.Giveaway_AutoBanWinner.AutoSize = true;
            this.Giveaway_AutoBanWinner.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Giveaway_AutoBanWinner.Location = new System.Drawing.Point(6, 237);
            this.Giveaway_AutoBanWinner.Name = "Giveaway_AutoBanWinner";
            this.Giveaway_AutoBanWinner.Size = new System.Drawing.Size(140, 17);
            this.Giveaway_AutoBanWinner.TabIndex = 34;
            this.Giveaway_AutoBanWinner.Text = "Automatically ban winner";
            this.Giveaway_AutoBanWinner.UseVisualStyleBackColor = true;
            this.Giveaway_AutoBanWinner.CheckedChanged += new System.EventHandler(this.Giveaway_Settings_Changed);
            // 
            // Currency_DisableCommand
            // 
            this.Currency_DisableCommand.AutoSize = true;
            this.Currency_DisableCommand.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Currency_DisableCommand.Location = new System.Drawing.Point(5, 103);
            this.Currency_DisableCommand.Name = "Currency_DisableCommand";
            this.Currency_DisableCommand.Size = new System.Drawing.Size(151, 17);
            this.Currency_DisableCommand.TabIndex = 35;
            this.Currency_DisableCommand.Text = "Disable currency command";
            this.Currency_DisableCommand.UseVisualStyleBackColor = true;
            this.Currency_DisableCommand.CheckedChanged += new System.EventHandler(this.Settings_Changed);
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
            // Giveaway_SettingsPresents
            // 
            this.Giveaway_SettingsPresents.Controls.Add(this.tabPage1);
            this.Giveaway_SettingsPresents.Location = new System.Drawing.Point(-1, -1);
            this.Giveaway_SettingsPresents.Name = "Giveaway_SettingsPresents";
            this.Giveaway_SettingsPresents.SelectedIndex = 0;
            this.Giveaway_SettingsPresents.Size = new System.Drawing.Size(1026, 22);
            this.Giveaway_SettingsPresents.TabIndex = 43;
            this.Giveaway_SettingsPresents.SelectedIndexChanged += new System.EventHandler(this.SettingsPresents_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1018, 0);
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
            this.GiveawayTypeLabel.Size = new System.Drawing.Size(35, 19);
            this.GiveawayTypeLabel.TabIndex = 13;
            this.GiveawayTypeLabel.Text = "Type";
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
            this.HandoutSpacer.Size = new System.Drawing.Size(1026, 11);
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
            this.Currency_HandoutActiveStream.Location = new System.Drawing.Point(5, 57);
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
            this.Currency_HandoutActiveTime.Location = new System.Drawing.Point(5, 80);
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
            this.Currency_HandoutLastActive.Location = new System.Drawing.Point(308, 80);
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
            // SettingsWindow
            // 
            this.SettingsWindow.BackColor = System.Drawing.Color.White;
            this.SettingsWindow.Controls.Add(this.SettingsErrorLabel);
            this.SettingsWindow.Controls.Add(this.label23);
            this.SettingsWindow.Controls.Add(this.Database_Table);
            this.SettingsWindow.Controls.Add(this.MySQL_Port);
            this.SettingsWindow.Controls.Add(this.label25);
            this.SettingsWindow.Controls.Add(this.MySQL_Password);
            this.SettingsWindow.Controls.Add(this.label26);
            this.SettingsWindow.Controls.Add(this.MySQL_Username);
            this.SettingsWindow.Controls.Add(this.label27);
            this.SettingsWindow.Controls.Add(this.MySQL_Database);
            this.SettingsWindow.Controls.Add(this.label24);
            this.SettingsWindow.Controls.Add(this.MySQL_Host);
            this.SettingsWindow.Controls.Add(this.MySQLLabel);
            this.SettingsWindow.Controls.Add(this.MySQLSpacer);
            this.SettingsWindow.Controls.Add(this.Misc_ShowConsole);
            this.SettingsWindow.Controls.Add(this.label19);
            this.SettingsWindow.Controls.Add(this.Currency_SubHandoutAmount);
            this.SettingsWindow.Controls.Add(this.Donations_ST_Token);
            this.SettingsWindow.Controls.Add(this.label4);
            this.SettingsWindow.Controls.Add(this.Channel_TokenButton);
            this.SettingsWindow.Controls.Add(this.Channel_Token);
            this.SettingsWindow.Controls.Add(this.Bot_TokenButton);
            this.SettingsWindow.Controls.Add(this.label21);
            this.SettingsWindow.Controls.Add(this.Currency_Command);
            this.SettingsWindow.Controls.Add(this.DisconnectButton);
            this.SettingsWindow.Controls.Add(this.label14);
            this.SettingsWindow.Controls.Add(this.DonationsLabel);
            this.SettingsWindow.Controls.Add(this.SubscribersLabel);
            this.SettingsWindow.Controls.Add(this.Currency_HandoutAmount);
            this.SettingsWindow.Controls.Add(this.DonationsSpacer);
            this.SettingsWindow.Controls.Add(this.Currency_HandoutInterval);
            this.SettingsWindow.Controls.Add(this.CurrencyLabel);
            this.SettingsWindow.Controls.Add(this.ConnectionLabel);
            this.SettingsWindow.Controls.Add(this.CurrencySpacer);
            this.SettingsWindow.Controls.Add(this.SubscribersSpacer);
            this.SettingsWindow.Controls.Add(this.ConnectionSpacer);
            this.SettingsWindow.Controls.Add(this.Donations_ST_ClientId);
            this.SettingsWindow.Controls.Add(this.label13);
            this.SettingsWindow.Controls.Add(this.label8);
            this.SettingsWindow.Controls.Add(this.label12);
            this.SettingsWindow.Controls.Add(this.label11);
            this.SettingsWindow.Controls.Add(this.Subscribers_Spreadsheet);
            this.SettingsWindow.Controls.Add(this.label10);
            this.SettingsWindow.Controls.Add(this.label7);
            this.SettingsWindow.Controls.Add(this.label9);
            this.SettingsWindow.Controls.Add(this.label1);
            this.SettingsWindow.Controls.Add(this.ConnectButton);
            this.SettingsWindow.Controls.Add(this.Bot_Token);
            this.SettingsWindow.Controls.Add(this.Bot_Name);
            this.SettingsWindow.Controls.Add(this.Channel_Name);
            this.SettingsWindow.Controls.Add(this.Currency_Name);
            this.SettingsWindow.Controls.Add(this.label5);
            this.SettingsWindow.Location = new System.Drawing.Point(108, 30);
            this.SettingsWindow.Name = "SettingsWindow";
            this.SettingsWindow.Size = new System.Drawing.Size(1024, 562);
            this.SettingsWindow.TabIndex = 58;
            // 
            // SettingsErrorLabel
            // 
            this.SettingsErrorLabel.AutoSize = true;
            this.SettingsErrorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SettingsErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.SettingsErrorLabel.Location = new System.Drawing.Point(3, 382);
            this.SettingsErrorLabel.Name = "SettingsErrorLabel";
            this.SettingsErrorLabel.Size = new System.Drawing.Size(34, 13);
            this.SettingsErrorLabel.TabIndex = 84;
            this.SettingsErrorLabel.Text = "Error";
            this.SettingsErrorLabel.TextChanged += new System.EventHandler(this.SettingsErrorLabel_TextChanged);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(681, 446);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(279, 13);
            this.label23.TabIndex = 110;
            this.label23.Text = "Table name (if kept blank the channel name will be used):";
            // 
            // Database_Table
            // 
            this.Database_Table.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Database_Table.Location = new System.Drawing.Point(684, 462);
            this.Database_Table.MaxLength = 128;
            this.Database_Table.Name = "Database_Table";
            this.Database_Table.Size = new System.Drawing.Size(334, 20);
            this.Database_Table.TabIndex = 111;
            this.Database_Table.TextChanged += new System.EventHandler(this.Settings_Changed);
            // 
            // MySQL_Port
            // 
            this.MySQL_Port.Location = new System.Drawing.Point(798, 384);
            this.MySQL_Port.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.MySQL_Port.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MySQL_Port.Name = "MySQL_Port";
            this.MySQL_Port.Size = new System.Drawing.Size(50, 20);
            this.MySQL_Port.TabIndex = 109;
            this.MySQL_Port.Value = new decimal(new int[] {
            3306,
            0,
            0,
            0});
            this.MySQL_Port.ValueChanged += new System.EventHandler(this.Settings_Changed);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(851, 407);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(56, 13);
            this.label25.TabIndex = 107;
            this.label25.Text = "Password:";
            // 
            // MySQL_Password
            // 
            this.MySQL_Password.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MySQL_Password.Location = new System.Drawing.Point(854, 423);
            this.MySQL_Password.MaxLength = 512;
            this.MySQL_Password.Name = "MySQL_Password";
            this.MySQL_Password.PasswordChar = '*';
            this.MySQL_Password.Size = new System.Drawing.Size(164, 20);
            this.MySQL_Password.TabIndex = 106;
            this.MySQL_Password.TextChanged += new System.EventHandler(this.Settings_Changed);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(681, 407);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(58, 13);
            this.label26.TabIndex = 104;
            this.label26.Text = "Username:";
            // 
            // MySQL_Username
            // 
            this.MySQL_Username.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MySQL_Username.Location = new System.Drawing.Point(684, 423);
            this.MySQL_Username.MaxLength = 128;
            this.MySQL_Username.Name = "MySQL_Username";
            this.MySQL_Username.Size = new System.Drawing.Size(164, 20);
            this.MySQL_Username.TabIndex = 105;
            this.MySQL_Username.TextChanged += new System.EventHandler(this.Settings_Changed);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(851, 368);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(56, 13);
            this.label27.TabIndex = 103;
            this.label27.Text = "Database:";
            // 
            // MySQL_Database
            // 
            this.MySQL_Database.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MySQL_Database.Location = new System.Drawing.Point(854, 384);
            this.MySQL_Database.MaxLength = 128;
            this.MySQL_Database.Name = "MySQL_Database";
            this.MySQL_Database.Size = new System.Drawing.Size(164, 20);
            this.MySQL_Database.TabIndex = 102;
            this.MySQL_Database.TextChanged += new System.EventHandler(this.Settings_Changed);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(681, 368);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(32, 13);
            this.label24.TabIndex = 98;
            this.label24.Text = "Host:";
            // 
            // MySQL_Host
            // 
            this.MySQL_Host.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MySQL_Host.Location = new System.Drawing.Point(684, 384);
            this.MySQL_Host.MaxLength = 128;
            this.MySQL_Host.Name = "MySQL_Host";
            this.MySQL_Host.Size = new System.Drawing.Size(108, 20);
            this.MySQL_Host.TabIndex = 99;
            this.MySQL_Host.TextChanged += new System.EventHandler(this.Settings_Changed);
            // 
            // MySQLLabel
            // 
            this.MySQLLabel.AutoSize = true;
            this.MySQLLabel.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MySQLLabel.Location = new System.Drawing.Point(691, 349);
            this.MySQLLabel.Name = "MySQLLabel";
            this.MySQLLabel.Size = new System.Drawing.Size(318, 19);
            this.MySQLLabel.TabIndex = 97;
            this.MySQLLabel.Text = "MySQL Database (Optional, leave blank for local storage)";
            this.MySQLLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MySQLSpacer
            // 
            this.MySQLSpacer.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Bold);
            this.MySQLSpacer.Location = new System.Drawing.Point(676, 349);
            this.MySQLSpacer.Name = "MySQLSpacer";
            this.MySQLSpacer.Size = new System.Drawing.Size(348, 11);
            this.MySQLSpacer.TabIndex = 96;
            this.MySQLSpacer.TabStop = false;
            // 
            // Misc_ShowConsole
            // 
            this.Misc_ShowConsole.AutoSize = true;
            this.Misc_ShowConsole.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Misc_ShowConsole.Location = new System.Drawing.Point(6, 362);
            this.Misc_ShowConsole.Name = "Misc_ShowConsole";
            this.Misc_ShowConsole.Size = new System.Drawing.Size(90, 17);
            this.Misc_ShowConsole.TabIndex = 95;
            this.Misc_ShowConsole.Text = "Show console";
            this.Misc_ShowConsole.UseVisualStyleBackColor = true;
            this.Misc_ShowConsole.CheckedChanged += new System.EventHandler(this.Settings_Changed);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(872, 247);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(142, 13);
            this.label19.TabIndex = 94;
            this.label19.Text = "Subscribers\' Payout Amount:";
            // 
            // Currency_SubHandoutAmount
            // 
            this.Currency_SubHandoutAmount.Location = new System.Drawing.Point(875, 263);
            this.Currency_SubHandoutAmount.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.Currency_SubHandoutAmount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Currency_SubHandoutAmount.Name = "Currency_SubHandoutAmount";
            this.Currency_SubHandoutAmount.Size = new System.Drawing.Size(143, 20);
            this.Currency_SubHandoutAmount.TabIndex = 93;
            this.Currency_SubHandoutAmount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Donations_ST_Token
            // 
            this.Donations_ST_Token.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Donations_ST_Token.Location = new System.Drawing.Point(515, 323);
            this.Donations_ST_Token.Name = "Donations_ST_Token";
            this.Donations_ST_Token.PasswordChar = '*';
            this.Donations_ST_Token.Size = new System.Drawing.Size(503, 20);
            this.Donations_ST_Token.TabIndex = 91;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(409, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 13);
            this.label4.TabIndex = 90;
            this.label4.Text = "Channel\'s Access Token:";
            // 
            // Channel_TokenButton
            // 
            this.Channel_TokenButton.BackColor = System.Drawing.Color.White;
            this.Channel_TokenButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Channel_TokenButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Channel_TokenButton.Location = new System.Drawing.Point(808, 72);
            this.Channel_TokenButton.Name = "Channel_TokenButton";
            this.Channel_TokenButton.Size = new System.Drawing.Size(210, 22);
            this.Channel_TokenButton.TabIndex = 89;
            this.Channel_TokenButton.Text = "Generate";
            this.Channel_TokenButton.UseVisualStyleBackColor = false;
            this.Channel_TokenButton.Click += new System.EventHandler(this.GenerateToken_Request);
            // 
            // Channel_Token
            // 
            this.Channel_Token.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Channel_Token.Location = new System.Drawing.Point(412, 73);
            this.Channel_Token.Name = "Channel_Token";
            this.Channel_Token.PasswordChar = '*';
            this.Channel_Token.Size = new System.Drawing.Size(390, 20);
            this.Channel_Token.TabIndex = 88;
            this.Channel_Token.TextChanged += new System.EventHandler(this.ConnectionDetailsChanged);
            // 
            // Bot_TokenButton
            // 
            this.Bot_TokenButton.BackColor = System.Drawing.Color.White;
            this.Bot_TokenButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Bot_TokenButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Bot_TokenButton.Location = new System.Drawing.Point(808, 33);
            this.Bot_TokenButton.Name = "Bot_TokenButton";
            this.Bot_TokenButton.Size = new System.Drawing.Size(210, 22);
            this.Bot_TokenButton.TabIndex = 87;
            this.Bot_TokenButton.Text = "Generate";
            this.Bot_TokenButton.UseVisualStyleBackColor = false;
            this.Bot_TokenButton.Click += new System.EventHandler(this.GenerateToken_Request);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(512, 191);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(164, 13);
            this.label21.TabIndex = 85;
            this.label21.Text = "Currency Command (starts with !):";
            // 
            // Currency_Command
            // 
            this.Currency_Command.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Currency_Command.Location = new System.Drawing.Point(515, 207);
            this.Currency_Command.MaxLength = 64;
            this.Currency_Command.Name = "Currency_Command";
            this.Currency_Command.Size = new System.Drawing.Size(503, 20);
            this.Currency_Command.TabIndex = 86;
            this.Currency_Command.Text = "ModCoins";
            this.Currency_Command.TextChanged += new System.EventHandler(this.ConnectionDetailsChanged);
            // 
            // DisconnectButton
            // 
            this.DisconnectButton.BackColor = System.Drawing.Color.White;
            this.DisconnectButton.Enabled = false;
            this.DisconnectButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DisconnectButton.Location = new System.Drawing.Point(598, 506);
            this.DisconnectButton.Name = "DisconnectButton";
            this.DisconnectButton.Size = new System.Drawing.Size(420, 50);
            this.DisconnectButton.TabIndex = 83;
            this.DisconnectButton.Text = "Disconnect";
            this.DisconnectButton.UseVisualStyleBackColor = false;
            this.DisconnectButton.Click += new System.EventHandler(this.DisconnectButton_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(3, 346);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(406, 13);
            this.label14.TabIndex = 82;
            this.label14.Text = "NOTE : The client id and token are both available on your account information pag" +
    "e.";
            // 
            // DonationsLabel
            // 
            this.DonationsLabel.AutoSize = true;
            this.DonationsLabel.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DonationsLabel.Location = new System.Drawing.Point(290, 289);
            this.DonationsLabel.Name = "DonationsLabel";
            this.DonationsLabel.Size = new System.Drawing.Size(121, 19);
            this.DonationsLabel.TabIndex = 81;
            this.DonationsLabel.Text = "Donations (Optional)";
            this.DonationsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SubscribersLabel
            // 
            this.SubscribersLabel.AutoSize = true;
            this.SubscribersLabel.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SubscribersLabel.Location = new System.Drawing.Point(292, 233);
            this.SubscribersLabel.Name = "SubscribersLabel";
            this.SubscribersLabel.Size = new System.Drawing.Size(72, 19);
            this.SubscribersLabel.TabIndex = 64;
            this.SubscribersLabel.Text = "Subscribers";
            this.SubscribersLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Currency_HandoutAmount
            // 
            this.Currency_HandoutAmount.Location = new System.Drawing.Point(6, 168);
            this.Currency_HandoutAmount.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.Currency_HandoutAmount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Currency_HandoutAmount.Name = "Currency_HandoutAmount";
            this.Currency_HandoutAmount.Size = new System.Drawing.Size(50, 20);
            this.Currency_HandoutAmount.TabIndex = 79;
            this.Currency_HandoutAmount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // DonationsSpacer
            // 
            this.DonationsSpacer.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Bold);
            this.DonationsSpacer.Location = new System.Drawing.Point(-1, 289);
            this.DonationsSpacer.Name = "DonationsSpacer";
            this.DonationsSpacer.Size = new System.Drawing.Size(1026, 11);
            this.DonationsSpacer.TabIndex = 80;
            this.DonationsSpacer.TabStop = false;
            // 
            // Currency_HandoutInterval
            // 
            this.Currency_HandoutInterval.Location = new System.Drawing.Point(6, 129);
            this.Currency_HandoutInterval.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.Currency_HandoutInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Currency_HandoutInterval.Name = "Currency_HandoutInterval";
            this.Currency_HandoutInterval.Size = new System.Drawing.Size(50, 20);
            this.Currency_HandoutInterval.TabIndex = 78;
            this.Currency_HandoutInterval.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // CurrencyLabel
            // 
            this.CurrencyLabel.AutoSize = true;
            this.CurrencyLabel.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrencyLabel.Location = new System.Drawing.Point(289, 99);
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
            this.CurrencySpacer.Location = new System.Drawing.Point(-1, 99);
            this.CurrencySpacer.Name = "CurrencySpacer";
            this.CurrencySpacer.Size = new System.Drawing.Size(1026, 11);
            this.CurrencySpacer.TabIndex = 61;
            this.CurrencySpacer.TabStop = false;
            // 
            // SubscribersSpacer
            // 
            this.SubscribersSpacer.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Bold);
            this.SubscribersSpacer.Location = new System.Drawing.Point(-1, 233);
            this.SubscribersSpacer.Name = "SubscribersSpacer";
            this.SubscribersSpacer.Size = new System.Drawing.Size(1026, 11);
            this.SubscribersSpacer.TabIndex = 63;
            this.SubscribersSpacer.TabStop = false;
            // 
            // ConnectionSpacer
            // 
            this.ConnectionSpacer.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Bold);
            this.ConnectionSpacer.Location = new System.Drawing.Point(-1, 0);
            this.ConnectionSpacer.Name = "ConnectionSpacer";
            this.ConnectionSpacer.Size = new System.Drawing.Size(1026, 11);
            this.ConnectionSpacer.TabIndex = 50;
            this.ConnectionSpacer.TabStop = false;
            // 
            // Donations_ST_ClientId
            // 
            this.Donations_ST_ClientId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Donations_ST_ClientId.Location = new System.Drawing.Point(6, 323);
            this.Donations_ST_ClientId.Name = "Donations_ST_ClientId";
            this.Donations_ST_ClientId.PasswordChar = '*';
            this.Donations_ST_ClientId.Size = new System.Drawing.Size(503, 20);
            this.Donations_ST_ClientId.TabIndex = 76;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(3, 18);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(26, 13);
            this.label13.TabIndex = 60;
            this.label13.Text = "Bot:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 307);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(104, 13);
            this.label8.TabIndex = 77;
            this.label8.Text = "Stream Tip Client ID:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(409, 20);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(105, 13);
            this.label12.TabIndex = 65;
            this.label12.Text = "Bot\'s Access Token:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 191);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(83, 13);
            this.label11.TabIndex = 61;
            this.label11.Text = "Currency Name:";
            // 
            // Subscribers_Spreadsheet
            // 
            this.Subscribers_Spreadsheet.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Subscribers_Spreadsheet.Location = new System.Drawing.Point(6, 263);
            this.Subscribers_Spreadsheet.Name = "Subscribers_Spreadsheet";
            this.Subscribers_Spreadsheet.Size = new System.Drawing.Size(863, 20);
            this.Subscribers_Spreadsheet.TabIndex = 68;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 113);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(137, 13);
            this.label10.TabIndex = 62;
            this.label10.Text = "Payout Interval (in minutes):";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 247);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(140, 13);
            this.label7.TabIndex = 74;
            this.label7.Text = "Sub Spreadsheet (Optional):";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 57);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(49, 13);
            this.label9.TabIndex = 63;
            this.label9.Text = "Channel:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 152);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 72;
            this.label1.Text = "Payout Amount:";
            // 
            // ConnectButton
            // 
            this.ConnectButton.BackColor = System.Drawing.Color.White;
            this.ConnectButton.Enabled = false;
            this.ConnectButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ConnectButton.Location = new System.Drawing.Point(6, 506);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(420, 50);
            this.ConnectButton.TabIndex = 0;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = false;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // Bot_Token
            // 
            this.Bot_Token.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Bot_Token.Location = new System.Drawing.Point(412, 34);
            this.Bot_Token.Name = "Bot_Token";
            this.Bot_Token.PasswordChar = '*';
            this.Bot_Token.Size = new System.Drawing.Size(390, 20);
            this.Bot_Token.TabIndex = 66;
            this.Bot_Token.TextChanged += new System.EventHandler(this.ConnectionDetailsChanged);
            // 
            // Bot_Name
            // 
            this.Bot_Name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Bot_Name.Location = new System.Drawing.Point(6, 34);
            this.Bot_Name.MaxLength = 64;
            this.Bot_Name.Name = "Bot_Name";
            this.Bot_Name.Size = new System.Drawing.Size(400, 20);
            this.Bot_Name.TabIndex = 64;
            this.Bot_Name.Text = "ModBot";
            this.Bot_Name.TextChanged += new System.EventHandler(this.ConnectionDetailsChanged);
            // 
            // Channel_Name
            // 
            this.Channel_Name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Channel_Name.Location = new System.Drawing.Point(6, 73);
            this.Channel_Name.MaxLength = 64;
            this.Channel_Name.Name = "Channel_Name";
            this.Channel_Name.Size = new System.Drawing.Size(400, 20);
            this.Channel_Name.TabIndex = 67;
            this.Channel_Name.Text = "ModChannel";
            this.Channel_Name.TextChanged += new System.EventHandler(this.ConnectionDetailsChanged);
            // 
            // Currency_Name
            // 
            this.Currency_Name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Currency_Name.Location = new System.Drawing.Point(6, 207);
            this.Currency_Name.MaxLength = 64;
            this.Currency_Name.Name = "Currency_Name";
            this.Currency_Name.Size = new System.Drawing.Size(503, 20);
            this.Currency_Name.TabIndex = 71;
            this.Currency_Name.Text = "Mod Coins";
            this.Currency_Name.TextChanged += new System.EventHandler(this.ConnectionDetailsChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(512, 307);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 13);
            this.label5.TabIndex = 92;
            this.label5.Text = "Stream Tip Token:";
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
            this.DonationsWindow.Size = new System.Drawing.Size(1024, 562);
            this.DonationsWindow.TabIndex = 59;
            // 
            // RecentDonorsLimit
            // 
            this.RecentDonorsLimit.Location = new System.Drawing.Point(487, 23);
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
            this.UpdateTopDonorsCheckBox.CheckedChanged += new System.EventHandler(this.Settings_Changed);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(428, 25);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(168, 13);
            this.label15.TabIndex = 66;
            this.label15.Text = "Show up to              recent donors";
            // 
            // Donations_List
            // 
            this.Donations_List.AllowUserToAddRows = false;
            this.Donations_List.AllowUserToDeleteRows = false;
            this.Donations_List.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.Donations_List.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Donations_List.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
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
            this.Donations_List.Location = new System.Drawing.Point(-1, 51);
            this.Donations_List.MultiSelect = false;
            this.Donations_List.Name = "Donations_List";
            this.Donations_List.RowHeadersVisible = false;
            this.Donations_List.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.Donations_List.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Donations_List.Size = new System.Drawing.Size(1025, 267);
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
            this.Date.Width = 112;
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
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Notes.DefaultCellStyle = dataGridViewCellStyle1;
            this.Notes.HeaderText = "Notes";
            this.Notes.Name = "Notes";
            this.Notes.ReadOnly = true;
            this.Notes.Width = 510;
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
            this.UpdateRecentDonorsCheckBox.Location = new System.Drawing.Point(431, 5);
            this.UpdateRecentDonorsCheckBox.Name = "UpdateRecentDonorsCheckBox";
            this.UpdateRecentDonorsCheckBox.Size = new System.Drawing.Size(149, 17);
            this.UpdateRecentDonorsCheckBox.TabIndex = 61;
            this.UpdateRecentDonorsCheckBox.Text = "Auto-update recent donors";
            this.UpdateRecentDonorsCheckBox.UseVisualStyleBackColor = true;
            this.UpdateRecentDonorsCheckBox.CheckedChanged += new System.EventHandler(this.Settings_Changed);
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
            this.UpdateLastDonorCheckBox.Location = new System.Drawing.Point(888, 5);
            this.UpdateLastDonorCheckBox.Name = "UpdateLastDonorCheckBox";
            this.UpdateLastDonorCheckBox.Size = new System.Drawing.Size(130, 17);
            this.UpdateLastDonorCheckBox.TabIndex = 62;
            this.UpdateLastDonorCheckBox.Text = "Auto-update last donor";
            this.UpdateLastDonorCheckBox.UseVisualStyleBackColor = true;
            this.UpdateLastDonorCheckBox.CheckedChanged += new System.EventHandler(this.Settings_Changed);
            // 
            // AboutWindow
            // 
            this.AboutWindow.BackColor = System.Drawing.Color.White;
            this.AboutWindow.Controls.Add(this.CopyrightLabel);
            this.AboutWindow.Controls.Add(this.WebsiteLinkLabel);
            this.AboutWindow.Controls.Add(this.About_Users);
            this.AboutWindow.Controls.Add(this.DonateImage);
            this.AboutWindow.Controls.Add(this.SupportLinkLabel);
            this.AboutWindow.Controls.Add(this.EmailLinkLabel);
            this.AboutWindow.Controls.Add(this.label17);
            this.AboutWindow.Controls.Add(this.AboutImage);
            this.AboutWindow.Controls.Add(this.VersionLabel);
            this.AboutWindow.Controls.Add(this.About_UsersLabel);
            this.AboutWindow.Location = new System.Drawing.Point(108, 30);
            this.AboutWindow.Name = "AboutWindow";
            this.AboutWindow.Size = new System.Drawing.Size(1024, 562);
            this.AboutWindow.TabIndex = 61;
            // 
            // CopyrightLabel
            // 
            this.CopyrightLabel.AutoSize = true;
            this.CopyrightLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.CopyrightLabel.Location = new System.Drawing.Point(3, 116);
            this.CopyrightLabel.Name = "CopyrightLabel";
            this.CopyrightLabel.Size = new System.Drawing.Size(250, 26);
            this.CopyrightLabel.TabIndex = 63;
            this.CopyrightLabel.TabStop = true;
            this.CopyrightLabel.Text = "ModBot © Jonathan \"Keirathi\" Smith, 2013\r\nModified by CoMaNdO\r\n";
            this.CopyrightLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.CopyrightLabel_LinkClicked);
            // 
            // WebsiteLinkLabel
            // 
            this.WebsiteLinkLabel.AutoSize = true;
            this.WebsiteLinkLabel.Location = new System.Drawing.Point(237, 246);
            this.WebsiteLinkLabel.Name = "WebsiteLinkLabel";
            this.WebsiteLinkLabel.Size = new System.Drawing.Size(27, 13);
            this.WebsiteLinkLabel.TabIndex = 20;
            this.WebsiteLinkLabel.TabStop = true;
            this.WebsiteLinkLabel.Text = "blog";
            this.WebsiteLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.WebsiteLinkLabel_LinkClicked);
            // 
            // About_Users
            // 
            this.About_Users.AllowUserToAddRows = false;
            this.About_Users.AllowUserToDeleteRows = false;
            this.About_Users.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.About_Users.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.About_Users.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.About_Users.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.About_Users.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Channel,
            this.Bot,
            this.Title,
            this.Game,
            this.Status,
            this.Viewers,
            this.Version,
            this.Updated});
            this.About_Users.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.About_Users.Location = new System.Drawing.Point(-1, 344);
            this.About_Users.MultiSelect = false;
            this.About_Users.Name = "About_Users";
            this.About_Users.ReadOnly = true;
            this.About_Users.RowHeadersVisible = false;
            this.About_Users.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.About_Users.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.About_Users.Size = new System.Drawing.Size(1025, 218);
            this.About_Users.TabIndex = 61;
            this.About_Users.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.About_Users_SortCompare);
            // 
            // Channel
            // 
            this.Channel.HeaderText = "Channel";
            this.Channel.Name = "Channel";
            this.Channel.ReadOnly = true;
            // 
            // Bot
            // 
            this.Bot.HeaderText = "Bot";
            this.Bot.Name = "Bot";
            this.Bot.ReadOnly = true;
            // 
            // Title
            // 
            this.Title.HeaderText = "Title";
            this.Title.Name = "Title";
            this.Title.ReadOnly = true;
            this.Title.Width = 281;
            // 
            // Game
            // 
            this.Game.HeaderText = "Game";
            this.Game.Name = "Game";
            this.Game.ReadOnly = true;
            this.Game.Width = 200;
            // 
            // Status
            // 
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Width = 76;
            // 
            // Viewers
            // 
            this.Viewers.HeaderText = "Viewers";
            this.Viewers.Name = "Viewers";
            this.Viewers.ReadOnly = true;
            this.Viewers.Width = 50;
            // 
            // Version
            // 
            this.Version.HeaderText = "Version";
            this.Version.Name = "Version";
            this.Version.ReadOnly = true;
            this.Version.Width = 84;
            // 
            // Updated
            // 
            this.Updated.HeaderText = "Updated";
            this.Updated.Name = "Updated";
            this.Updated.ReadOnly = true;
            this.Updated.Width = 116;
            // 
            // DonateImage
            // 
            this.DonateImage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DonateImage.Image = global::ModBot.Properties.Resources.DonateImage;
            this.DonateImage.Location = new System.Drawing.Point(101, 281);
            this.DonateImage.Name = "DonateImage";
            this.DonateImage.Size = new System.Drawing.Size(74, 21);
            this.DonateImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.DonateImage.TabIndex = 0;
            this.DonateImage.TabStop = false;
            this.DonateImage.Click += new System.EventHandler(this.DonateImage_Click);
            // 
            // SupportLinkLabel
            // 
            this.SupportLinkLabel.AutoSize = true;
            this.SupportLinkLabel.Location = new System.Drawing.Point(220, 207);
            this.SupportLinkLabel.Name = "SupportLinkLabel";
            this.SupportLinkLabel.Size = new System.Drawing.Size(27, 13);
            this.SupportLinkLabel.TabIndex = 23;
            this.SupportLinkLabel.TabStop = true;
            this.SupportLinkLabel.Text = "blog";
            this.SupportLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.SupportLinkLabel_LinkClicked);
            // 
            // EmailLinkLabel
            // 
            this.EmailLinkLabel.AutoSize = true;
            this.EmailLinkLabel.Location = new System.Drawing.Point(151, 207);
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
            this.label17.Location = new System.Drawing.Point(3, 207);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(561, 52);
            this.label17.TabIndex = 21;
            this.label17.Text = resources.GetString("label17.Text");
            // 
            // AboutImage
            // 
            this.AboutImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.AboutImage.Image = global::ModBot.Properties.Resources.ModBot;
            this.AboutImage.Location = new System.Drawing.Point(0, 0);
            this.AboutImage.Name = "AboutImage";
            this.AboutImage.Size = new System.Drawing.Size(1024, 114);
            this.AboutImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.AboutImage.TabIndex = 18;
            this.AboutImage.TabStop = false;
            // 
            // VersionLabel
            // 
            this.VersionLabel.AutoSize = true;
            this.VersionLabel.Location = new System.Drawing.Point(3, 164);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(45, 13);
            this.VersionLabel.TabIndex = 16;
            this.VersionLabel.Text = "Version:";
            // 
            // About_UsersLabel
            // 
            this.About_UsersLabel.AutoSize = true;
            this.About_UsersLabel.Location = new System.Drawing.Point(3, 332);
            this.About_UsersLabel.Name = "About_UsersLabel";
            this.About_UsersLabel.Size = new System.Drawing.Size(64, 13);
            this.About_UsersLabel.TabIndex = 62;
            this.About_UsersLabel.Text = "Other users:";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Location = new System.Drawing.Point(108, 30);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1024, 562);
            this.panel2.TabIndex = 62;
            // 
            // ChannelWindow
            // 
            this.ChannelWindow.BackColor = System.Drawing.Color.White;
            this.ChannelWindow.Controls.Add(this.label18);
            this.ChannelWindow.Controls.Add(this.Channel_WelcomeSubMessage);
            this.ChannelWindow.Controls.Add(this.Channel_ViewersChangeMessage);
            this.ChannelWindow.Controls.Add(this.Channel_ViewersChangeRate);
            this.ChannelWindow.Controls.Add(this.Channel_ViewersChangeInterval);
            this.ChannelWindow.Controls.Add(this.Channel_ViewersChange);
            this.ChannelWindow.Controls.Add(this.MessageTimersLabel);
            this.ChannelWindow.Controls.Add(this.MessageTimersSpacer);
            this.ChannelWindow.Controls.Add(this.Channel_SubscriptionsDate);
            this.ChannelWindow.Controls.Add(this.SubscriptionsLabel);
            this.ChannelWindow.Controls.Add(this.Channel_SubscriptionRewards);
            this.ChannelWindow.Controls.Add(this.SubscriptionsSpacer);
            this.ChannelWindow.Controls.Add(this.Channel_SteamID64);
            this.ChannelWindow.Controls.Add(this.Channel_UseSteam);
            this.ChannelWindow.Controls.Add(this.Channel_UpdateTitleGame);
            this.ChannelWindow.Controls.Add(this.ChannelStatusLabel);
            this.ChannelWindow.Controls.Add(this.label2);
            this.ChannelWindow.Controls.Add(this.Channel_Title);
            this.ChannelWindow.Controls.Add(this.label3);
            this.ChannelWindow.Controls.Add(this.Channel_Game);
            this.ChannelWindow.Controls.Add(this.Channel_WelcomeSub);
            this.ChannelWindow.Controls.Add(this.Channel_SubscriptionRewardsList);
            this.ChannelWindow.Location = new System.Drawing.Point(108, 30);
            this.ChannelWindow.Name = "ChannelWindow";
            this.ChannelWindow.Size = new System.Drawing.Size(1024, 562);
            this.ChannelWindow.TabIndex = 63;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(817, 336);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(201, 13);
            this.label18.TabIndex = 71;
            this.label18.Text = "\"@user\" is replaced with the user\'s name";
            // 
            // Channel_WelcomeSubMessage
            // 
            this.Channel_WelcomeSubMessage.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.Channel_WelcomeSubMessage.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.Channel_WelcomeSubMessage.BackColor = System.Drawing.Color.White;
            this.Channel_WelcomeSubMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Channel_WelcomeSubMessage.Enabled = false;
            this.Channel_WelcomeSubMessage.Location = new System.Drawing.Point(281, 334);
            this.Channel_WelcomeSubMessage.Name = "Channel_WelcomeSubMessage";
            this.Channel_WelcomeSubMessage.Size = new System.Drawing.Size(533, 20);
            this.Channel_WelcomeSubMessage.TabIndex = 69;
            this.Channel_WelcomeSubMessage.Text = "Welcome to the team @user!";
            this.Channel_WelcomeSubMessage.TextChanged += new System.EventHandler(this.Settings_Changed);
            // 
            // Channel_ViewersChangeMessage
            // 
            this.Channel_ViewersChangeMessage.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.Channel_ViewersChangeMessage.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.Channel_ViewersChangeMessage.BackColor = System.Drawing.Color.White;
            this.Channel_ViewersChangeMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Channel_ViewersChangeMessage.Enabled = false;
            this.Channel_ViewersChangeMessage.Location = new System.Drawing.Point(501, 146);
            this.Channel_ViewersChangeMessage.Name = "Channel_ViewersChangeMessage";
            this.Channel_ViewersChangeMessage.Size = new System.Drawing.Size(517, 20);
            this.Channel_ViewersChangeMessage.TabIndex = 66;
            this.Channel_ViewersChangeMessage.Text = "New viewers remember to follow the channel!";
            this.Channel_ViewersChangeMessage.TextChanged += new System.EventHandler(this.Settings_Changed);
            // 
            // Channel_ViewersChangeRate
            // 
            this.Channel_ViewersChangeRate.Enabled = false;
            this.Channel_ViewersChangeRate.Location = new System.Drawing.Point(239, 146);
            this.Channel_ViewersChangeRate.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Channel_ViewersChangeRate.Name = "Channel_ViewersChangeRate";
            this.Channel_ViewersChangeRate.Size = new System.Drawing.Size(38, 20);
            this.Channel_ViewersChangeRate.TabIndex = 65;
            this.Channel_ViewersChangeRate.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.Channel_ViewersChangeRate.ValueChanged += new System.EventHandler(this.Settings_Changed);
            // 
            // Channel_ViewersChangeInterval
            // 
            this.Channel_ViewersChangeInterval.Enabled = false;
            this.Channel_ViewersChangeInterval.Location = new System.Drawing.Point(53, 146);
            this.Channel_ViewersChangeInterval.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.Channel_ViewersChangeInterval.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.Channel_ViewersChangeInterval.Name = "Channel_ViewersChangeInterval";
            this.Channel_ViewersChangeInterval.Size = new System.Drawing.Size(34, 20);
            this.Channel_ViewersChangeInterval.TabIndex = 64;
            this.Channel_ViewersChangeInterval.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.Channel_ViewersChangeInterval.ValueChanged += new System.EventHandler(this.Settings_Changed);
            // 
            // Channel_ViewersChange
            // 
            this.Channel_ViewersChange.AutoSize = true;
            this.Channel_ViewersChange.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Channel_ViewersChange.Location = new System.Drawing.Point(6, 146);
            this.Channel_ViewersChange.Name = "Channel_ViewersChange";
            this.Channel_ViewersChange.Size = new System.Drawing.Size(1009, 17);
            this.Channel_ViewersChange.TabIndex = 63;
            this.Channel_ViewersChange.Text = resources.GetString("Channel_ViewersChange.Text");
            this.Channel_ViewersChange.UseVisualStyleBackColor = true;
            this.Channel_ViewersChange.CheckedChanged += new System.EventHandler(this.Settings_Changed);
            // 
            // MessageTimersLabel
            // 
            this.MessageTimersLabel.AutoSize = true;
            this.MessageTimersLabel.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MessageTimersLabel.Location = new System.Drawing.Point(246, 129);
            this.MessageTimersLabel.Name = "MessageTimersLabel";
            this.MessageTimersLabel.Size = new System.Drawing.Size(95, 19);
            this.MessageTimersLabel.TabIndex = 67;
            this.MessageTimersLabel.Text = "Message Timers";
            this.MessageTimersLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MessageTimersSpacer
            // 
            this.MessageTimersSpacer.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Bold);
            this.MessageTimersSpacer.Location = new System.Drawing.Point(-1, 129);
            this.MessageTimersSpacer.Name = "MessageTimersSpacer";
            this.MessageTimersSpacer.Size = new System.Drawing.Size(1026, 11);
            this.MessageTimersSpacer.TabIndex = 68;
            this.MessageTimersSpacer.TabStop = false;
            // 
            // Channel_SubscriptionsDate
            // 
            this.Channel_SubscriptionsDate.CustomFormat = "MM/dd/yyyy H:mm:ss";
            this.Channel_SubscriptionsDate.Enabled = false;
            this.Channel_SubscriptionsDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Channel_SubscriptionsDate.Location = new System.Drawing.Point(254, 356);
            this.Channel_SubscriptionsDate.MaxDate = new System.DateTime(2019, 12, 31, 0, 0, 0, 0);
            this.Channel_SubscriptionsDate.MinDate = new System.DateTime(2014, 1, 1, 0, 0, 0, 0);
            this.Channel_SubscriptionsDate.Name = "Channel_SubscriptionsDate";
            this.Channel_SubscriptionsDate.Size = new System.Drawing.Size(149, 20);
            this.Channel_SubscriptionsDate.TabIndex = 52;
            this.Channel_SubscriptionsDate.Value = new System.DateTime(2014, 8, 29, 1, 51, 17, 0);
            // 
            // SubscriptionsLabel
            // 
            this.SubscriptionsLabel.AutoSize = true;
            this.SubscriptionsLabel.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SubscriptionsLabel.Location = new System.Drawing.Point(246, 317);
            this.SubscriptionsLabel.Name = "SubscriptionsLabel";
            this.SubscriptionsLabel.Size = new System.Drawing.Size(82, 19);
            this.SubscriptionsLabel.TabIndex = 50;
            this.SubscriptionsLabel.Text = "Subscriptions";
            this.SubscriptionsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Channel_SubscriptionRewards
            // 
            this.Channel_SubscriptionRewards.AutoSize = true;
            this.Channel_SubscriptionRewards.Enabled = false;
            this.Channel_SubscriptionRewards.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Channel_SubscriptionRewards.Location = new System.Drawing.Point(6, 357);
            this.Channel_SubscriptionRewards.Name = "Channel_SubscriptionRewards";
            this.Channel_SubscriptionRewards.Size = new System.Drawing.Size(394, 17);
            this.Channel_SubscriptionRewards.TabIndex = 54;
            this.Channel_SubscriptionRewards.Text = "Subscription rewards for all subscriptions starting                              " +
    "                  ";
            this.Channel_SubscriptionRewards.UseVisualStyleBackColor = true;
            this.Channel_SubscriptionRewards.CheckedChanged += new System.EventHandler(this.Settings_Changed);
            // 
            // SubscriptionsSpacer
            // 
            this.SubscriptionsSpacer.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Bold);
            this.SubscriptionsSpacer.Location = new System.Drawing.Point(-1, 317);
            this.SubscriptionsSpacer.Name = "SubscriptionsSpacer";
            this.SubscriptionsSpacer.Size = new System.Drawing.Size(1026, 11);
            this.SubscriptionsSpacer.TabIndex = 51;
            this.SubscriptionsSpacer.TabStop = false;
            // 
            // Channel_SteamID64
            // 
            this.Channel_SteamID64.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.Channel_SteamID64.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.Channel_SteamID64.BackColor = System.Drawing.Color.White;
            this.Channel_SteamID64.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Channel_SteamID64.Enabled = false;
            this.Channel_SteamID64.Location = new System.Drawing.Point(146, 106);
            this.Channel_SteamID64.MaxLength = 32;
            this.Channel_SteamID64.Name = "Channel_SteamID64";
            this.Channel_SteamID64.Size = new System.Drawing.Size(137, 20);
            this.Channel_SteamID64.TabIndex = 37;
            this.Channel_SteamID64.Text = "SteamID64";
            this.Channel_SteamID64.TextChanged += new System.EventHandler(this.Channel_SteamID64_TextChanged);
            // 
            // Channel_UseSteam
            // 
            this.Channel_UseSteam.AutoSize = true;
            this.Channel_UseSteam.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Channel_UseSteam.Location = new System.Drawing.Point(6, 106);
            this.Channel_UseSteam.Name = "Channel_UseSteam";
            this.Channel_UseSteam.Size = new System.Drawing.Size(461, 17);
            this.Channel_UseSteam.TabIndex = 36;
            this.Channel_UseSteam.Text = "Update game from Steam                                                (Profile pr" +
    "ivacy must be set as public)";
            this.Channel_UseSteam.UseVisualStyleBackColor = true;
            this.Channel_UseSteam.CheckedChanged += new System.EventHandler(this.Settings_Changed);
            // 
            // Channel_UpdateTitleGame
            // 
            this.Channel_UpdateTitleGame.BackColor = System.Drawing.Color.White;
            this.Channel_UpdateTitleGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Channel_UpdateTitleGame.Location = new System.Drawing.Point(876, 41);
            this.Channel_UpdateTitleGame.Name = "Channel_UpdateTitleGame";
            this.Channel_UpdateTitleGame.Size = new System.Drawing.Size(142, 59);
            this.Channel_UpdateTitleGame.TabIndex = 32;
            this.Channel_UpdateTitleGame.Text = "Update";
            this.Channel_UpdateTitleGame.UseVisualStyleBackColor = false;
            this.Channel_UpdateTitleGame.Click += new System.EventHandler(this.UpdateTitleGameButton_Click);
            // 
            // Channel_WelcomeSub
            // 
            this.Channel_WelcomeSub.AutoSize = true;
            this.Channel_WelcomeSub.Enabled = false;
            this.Channel_WelcomeSub.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Channel_WelcomeSub.Location = new System.Drawing.Point(6, 334);
            this.Channel_WelcomeSub.Name = "Channel_WelcomeSub";
            this.Channel_WelcomeSub.Size = new System.Drawing.Size(807, 17);
            this.Channel_WelcomeSub.TabIndex = 70;
            this.Channel_WelcomeSub.Text = resources.GetString("Channel_WelcomeSub.Text");
            this.Channel_WelcomeSub.UseVisualStyleBackColor = true;
            this.Channel_WelcomeSub.CheckedChanged += new System.EventHandler(this.Settings_Changed);
            // 
            // Channel_SubscriptionRewardsList
            // 
            this.Channel_SubscriptionRewardsList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.Channel_SubscriptionRewardsList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Channel_SubscriptionRewardsList.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.Channel_SubscriptionRewardsList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Channel_SubscriptionRewardsList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Reward,
            this.Instructions});
            this.Channel_SubscriptionRewardsList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.Channel_SubscriptionRewardsList.Enabled = false;
            this.Channel_SubscriptionRewardsList.Location = new System.Drawing.Point(-1, 382);
            this.Channel_SubscriptionRewardsList.MultiSelect = false;
            this.Channel_SubscriptionRewardsList.Name = "Channel_SubscriptionRewardsList";
            this.Channel_SubscriptionRewardsList.RowHeadersVisible = false;
            this.Channel_SubscriptionRewardsList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.Channel_SubscriptionRewardsList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.Channel_SubscriptionRewardsList.Size = new System.Drawing.Size(1025, 180);
            this.Channel_SubscriptionRewardsList.TabIndex = 62;
            // 
            // Reward
            // 
            this.Reward.HeaderText = "Reward";
            this.Reward.Name = "Reward";
            this.Reward.Width = 400;
            // 
            // Instructions
            // 
            this.Instructions.HeaderText = "Instructions";
            this.Instructions.Name = "Instructions";
            this.Instructions.Width = 607;
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
            this.CurrencyWindow.Controls.Add(this.Currency_DisableCommand);
            this.CurrencyWindow.Location = new System.Drawing.Point(108, 30);
            this.CurrencyWindow.Name = "CurrencyWindow";
            this.CurrencyWindow.Size = new System.Drawing.Size(1024, 562);
            this.CurrencyWindow.TabIndex = 63;
            // 
            // GiveawayWindow
            // 
            this.GiveawayWindow.BackColor = System.Drawing.Color.White;
            this.GiveawayWindow.Controls.Add(this.Giveaway_SubscribersWinMultiplierAmount);
            this.GiveawayWindow.Controls.Add(this.Giveaway_UserList);
            this.GiveawayWindow.Controls.Add(this.GiveawayUsersLabel);
            this.GiveawayWindow.Controls.Add(this.GiveawayUsersSpacer);
            this.GiveawayWindow.Controls.Add(this.Giveaway_CustomKeyword);
            this.GiveawayWindow.Controls.Add(this.Giveaway_AnnounceWarnedEntries);
            this.GiveawayWindow.Controls.Add(this.Giveaway_WarnFalseEntries);
            this.GiveawayWindow.Controls.Add(this.Giveaway_MustWatchMinutes);
            this.GiveawayWindow.Controls.Add(this.Giveaway_MustWatchHours);
            this.GiveawayWindow.Controls.Add(this.Giveaway_MustWatchDays);
            this.GiveawayWindow.Controls.Add(this.Giveaway_MustWatch);
            this.GiveawayWindow.Controls.Add(this.Giveaway_MustSubscribe);
            this.GiveawayWindow.Controls.Add(this.Giveaway_MaxTickets);
            this.GiveawayWindow.Controls.Add(this.Giveaway_TicketCost);
            this.GiveawayWindow.Controls.Add(this.Giveaway_CancelButton);
            this.GiveawayWindow.Controls.Add(this.Giveaway_CloseButton);
            this.GiveawayWindow.Controls.Add(this.Giveaway_OpenButton);
            this.GiveawayWindow.Controls.Add(this.GiveawayBansLabel);
            this.GiveawayWindow.Controls.Add(this.GiveawayBansSpacer);
            this.GiveawayWindow.Controls.Add(this.GiveawayRulesLabel);
            this.GiveawayWindow.Controls.Add(this.GiveawayRulesSpacer);
            this.GiveawayWindow.Controls.Add(this.Giveaway_TypeTickets);
            this.GiveawayWindow.Controls.Add(this.Giveaway_TypeKeyword);
            this.GiveawayWindow.Controls.Add(this.Giveaway_MinCurrencyBox);
            this.GiveawayWindow.Controls.Add(this.Giveaway_SettingsPresents);
            this.GiveawayWindow.Controls.Add(this.Giveaway_WinTimeLabel);
            this.GiveawayWindow.Controls.Add(this.Giveaway_AddBanTextBox);
            this.GiveawayWindow.Controls.Add(this.Giveaway_MustFollow);
            this.GiveawayWindow.Controls.Add(this.Giveaway_WinnerChat);
            this.GiveawayWindow.Controls.Add(this.Giveaway_WinnerTimerLabel);
            this.GiveawayWindow.Controls.Add(this.Giveaway_StartButton);
            this.GiveawayWindow.Controls.Add(this.Giveaway_CopyWinnerButton);
            this.GiveawayWindow.Controls.Add(this.Giveaway_RerollButton);
            this.GiveawayWindow.Controls.Add(this.Giveaway_StopButton);
            this.GiveawayWindow.Controls.Add(this.Giveaway_AutoBanWinner);
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
            this.GiveawayWindow.Controls.Add(this.Giveaway_MinCurrency);
            this.GiveawayWindow.Controls.Add(this.Giveaway_TypeActive);
            this.GiveawayWindow.Controls.Add(this.Giveaway_UserCount);
            this.GiveawayWindow.Controls.Add(this.Giveaway_SubscribersWinMultiplier);
            this.GiveawayWindow.Location = new System.Drawing.Point(108, 30);
            this.GiveawayWindow.Name = "GiveawayWindow";
            this.GiveawayWindow.Size = new System.Drawing.Size(1024, 562);
            this.GiveawayWindow.TabIndex = 63;
            // 
            // Giveaway_SubscribersWinMultiplierAmount
            // 
            this.Giveaway_SubscribersWinMultiplierAmount.Enabled = false;
            this.Giveaway_SubscribersWinMultiplierAmount.Location = new System.Drawing.Point(144, 283);
            this.Giveaway_SubscribersWinMultiplierAmount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Giveaway_SubscribersWinMultiplierAmount.Name = "Giveaway_SubscribersWinMultiplierAmount";
            this.Giveaway_SubscribersWinMultiplierAmount.Size = new System.Drawing.Size(38, 20);
            this.Giveaway_SubscribersWinMultiplierAmount.TabIndex = 73;
            this.Giveaway_SubscribersWinMultiplierAmount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Giveaway_SubscribersWinMultiplierAmount.ValueChanged += new System.EventHandler(this.Giveaway_Settings_Changed);
            // 
            // Giveaway_UserList
            // 
            this.Giveaway_UserList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Giveaway_UserList.FormattingEnabled = true;
            this.Giveaway_UserList.Location = new System.Drawing.Point(814, 60);
            this.Giveaway_UserList.Name = "Giveaway_UserList";
            this.Giveaway_UserList.Size = new System.Drawing.Size(202, 496);
            this.Giveaway_UserList.TabIndex = 70;
            // 
            // GiveawayUsersLabel
            // 
            this.GiveawayUsersLabel.AutoSize = true;
            this.GiveawayUsersLabel.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GiveawayUsersLabel.Location = new System.Drawing.Point(887, 25);
            this.GiveawayUsersLabel.Name = "GiveawayUsersLabel";
            this.GiveawayUsersLabel.Size = new System.Drawing.Size(97, 19);
            this.GiveawayUsersLabel.TabIndex = 68;
            this.GiveawayUsersLabel.Text = "Users (Disabled)";
            this.GiveawayUsersLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GiveawayUsersSpacer
            // 
            this.GiveawayUsersSpacer.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Bold);
            this.GiveawayUsersSpacer.Location = new System.Drawing.Point(813, 25);
            this.GiveawayUsersSpacer.Name = "GiveawayUsersSpacer";
            this.GiveawayUsersSpacer.Size = new System.Drawing.Size(212, 11);
            this.GiveawayUsersSpacer.TabIndex = 69;
            this.GiveawayUsersSpacer.TabStop = false;
            // 
            // Giveaway_CustomKeyword
            // 
            this.Giveaway_CustomKeyword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Giveaway_CustomKeyword.Enabled = false;
            this.Giveaway_CustomKeyword.Location = new System.Drawing.Point(124, 69);
            this.Giveaway_CustomKeyword.MaxLength = 32;
            this.Giveaway_CustomKeyword.Name = "Giveaway_CustomKeyword";
            this.Giveaway_CustomKeyword.Size = new System.Drawing.Size(125, 20);
            this.Giveaway_CustomKeyword.TabIndex = 67;
            this.Giveaway_CustomKeyword.TextChanged += new System.EventHandler(this.Giveaway_Settings_Changed);
            // 
            // Giveaway_AnnounceWarnedEntries
            // 
            this.Giveaway_AnnounceWarnedEntries.AutoSize = true;
            this.Giveaway_AnnounceWarnedEntries.Enabled = false;
            this.Giveaway_AnnounceWarnedEntries.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Giveaway_AnnounceWarnedEntries.Location = new System.Drawing.Point(171, 260);
            this.Giveaway_AnnounceWarnedEntries.Name = "Giveaway_AnnounceWarnedEntries";
            this.Giveaway_AnnounceWarnedEntries.Size = new System.Drawing.Size(114, 17);
            this.Giveaway_AnnounceWarnedEntries.TabIndex = 66;
            this.Giveaway_AnnounceWarnedEntries.Text = "Announce timeouts";
            this.Giveaway_AnnounceWarnedEntries.UseVisualStyleBackColor = true;
            this.Giveaway_AnnounceWarnedEntries.CheckedChanged += new System.EventHandler(this.Giveaway_Settings_Changed);
            // 
            // Giveaway_WarnFalseEntries
            // 
            this.Giveaway_WarnFalseEntries.AutoSize = true;
            this.Giveaway_WarnFalseEntries.Enabled = false;
            this.Giveaway_WarnFalseEntries.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Giveaway_WarnFalseEntries.Location = new System.Drawing.Point(6, 260);
            this.Giveaway_WarnFalseEntries.Name = "Giveaway_WarnFalseEntries";
            this.Giveaway_WarnFalseEntries.Size = new System.Drawing.Size(166, 17);
            this.Giveaway_WarnFalseEntries.TabIndex = 65;
            this.Giveaway_WarnFalseEntries.Text = "Warn and timeout false entries";
            this.Giveaway_WarnFalseEntries.UseVisualStyleBackColor = true;
            this.Giveaway_WarnFalseEntries.CheckedChanged += new System.EventHandler(this.Giveaway_Settings_Changed);
            // 
            // Giveaway_MustWatchMinutes
            // 
            this.Giveaway_MustWatchMinutes.Enabled = false;
            this.Giveaway_MustWatchMinutes.Location = new System.Drawing.Point(355, 214);
            this.Giveaway_MustWatchMinutes.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.Giveaway_MustWatchMinutes.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.Giveaway_MustWatchMinutes.Name = "Giveaway_MustWatchMinutes";
            this.Giveaway_MustWatchMinutes.Size = new System.Drawing.Size(32, 20);
            this.Giveaway_MustWatchMinutes.TabIndex = 64;
            this.Giveaway_MustWatchMinutes.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Giveaway_MustWatchMinutes.ValueChanged += new System.EventHandler(this.Giveaway_Settings_Changed);
            // 
            // Giveaway_MustWatchHours
            // 
            this.Giveaway_MustWatchHours.Enabled = false;
            this.Giveaway_MustWatchHours.Location = new System.Drawing.Point(269, 214);
            this.Giveaway_MustWatchHours.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.Giveaway_MustWatchHours.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.Giveaway_MustWatchHours.Name = "Giveaway_MustWatchHours";
            this.Giveaway_MustWatchHours.Size = new System.Drawing.Size(32, 20);
            this.Giveaway_MustWatchHours.TabIndex = 63;
            this.Giveaway_MustWatchHours.ValueChanged += new System.EventHandler(this.Giveaway_Settings_Changed);
            // 
            // Giveaway_MustWatchDays
            // 
            this.Giveaway_MustWatchDays.Enabled = false;
            this.Giveaway_MustWatchDays.Location = new System.Drawing.Point(193, 214);
            this.Giveaway_MustWatchDays.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.Giveaway_MustWatchDays.Name = "Giveaway_MustWatchDays";
            this.Giveaway_MustWatchDays.Size = new System.Drawing.Size(44, 20);
            this.Giveaway_MustWatchDays.TabIndex = 62;
            this.Giveaway_MustWatchDays.ValueChanged += new System.EventHandler(this.Giveaway_Settings_Changed);
            // 
            // Giveaway_MustWatch
            // 
            this.Giveaway_MustWatch.AutoSize = true;
            this.Giveaway_MustWatch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Giveaway_MustWatch.Location = new System.Drawing.Point(6, 214);
            this.Giveaway_MustWatch.Name = "Giveaway_MustWatch";
            this.Giveaway_MustWatch.Size = new System.Drawing.Size(427, 17);
            this.Giveaway_MustWatch.TabIndex = 61;
            this.Giveaway_MustWatch.Text = "Has watched the stream for at least                 days,             hours and  " +
    "           minutes";
            this.Giveaway_MustWatch.UseVisualStyleBackColor = true;
            this.Giveaway_MustWatch.CheckedChanged += new System.EventHandler(this.Giveaway_Settings_Changed);
            // 
            // Giveaway_MustSubscribe
            // 
            this.Giveaway_MustSubscribe.AutoSize = true;
            this.Giveaway_MustSubscribe.Enabled = false;
            this.Giveaway_MustSubscribe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Giveaway_MustSubscribe.Location = new System.Drawing.Point(6, 191);
            this.Giveaway_MustSubscribe.Name = "Giveaway_MustSubscribe";
            this.Giveaway_MustSubscribe.Size = new System.Drawing.Size(121, 17);
            this.Giveaway_MustSubscribe.TabIndex = 60;
            this.Giveaway_MustSubscribe.Text = "Must be a subscriber";
            this.Giveaway_MustSubscribe.UseVisualStyleBackColor = true;
            this.Giveaway_MustSubscribe.CheckedChanged += new System.EventHandler(this.Giveaway_Settings_Changed);
            // 
            // Giveaway_MaxTickets
            // 
            this.Giveaway_MaxTickets.Enabled = false;
            this.Giveaway_MaxTickets.Location = new System.Drawing.Point(205, 92);
            this.Giveaway_MaxTickets.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.Giveaway_MaxTickets.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Giveaway_MaxTickets.Name = "Giveaway_MaxTickets";
            this.Giveaway_MaxTickets.Size = new System.Drawing.Size(44, 20);
            this.Giveaway_MaxTickets.TabIndex = 59;
            this.Giveaway_MaxTickets.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.Giveaway_MaxTickets.ValueChanged += new System.EventHandler(this.Giveaway_Settings_Changed);
            // 
            // Giveaway_TicketCost
            // 
            this.Giveaway_TicketCost.Enabled = false;
            this.Giveaway_TicketCost.Location = new System.Drawing.Point(110, 92);
            this.Giveaway_TicketCost.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.Giveaway_TicketCost.Name = "Giveaway_TicketCost";
            this.Giveaway_TicketCost.Size = new System.Drawing.Size(62, 20);
            this.Giveaway_TicketCost.TabIndex = 58;
            this.Giveaway_TicketCost.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.Giveaway_TicketCost.ValueChanged += new System.EventHandler(this.Giveaway_Settings_Changed);
            // 
            // Giveaway_CancelButton
            // 
            this.Giveaway_CancelButton.BackColor = System.Drawing.Color.White;
            this.Giveaway_CancelButton.Enabled = false;
            this.Giveaway_CancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Giveaway_CancelButton.Location = new System.Drawing.Point(699, 533);
            this.Giveaway_CancelButton.Name = "Giveaway_CancelButton";
            this.Giveaway_CancelButton.Size = new System.Drawing.Size(109, 23);
            this.Giveaway_CancelButton.TabIndex = 57;
            this.Giveaway_CancelButton.Text = "Cancel";
            this.Giveaway_CancelButton.UseVisualStyleBackColor = false;
            this.Giveaway_CancelButton.Click += new System.EventHandler(this.Giveaway_CancelButton_Click);
            // 
            // Giveaway_CloseButton
            // 
            this.Giveaway_CloseButton.BackColor = System.Drawing.Color.White;
            this.Giveaway_CloseButton.Enabled = false;
            this.Giveaway_CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Giveaway_CloseButton.Location = new System.Drawing.Point(354, 533);
            this.Giveaway_CloseButton.Name = "Giveaway_CloseButton";
            this.Giveaway_CloseButton.Size = new System.Drawing.Size(109, 23);
            this.Giveaway_CloseButton.TabIndex = 56;
            this.Giveaway_CloseButton.Text = "Close";
            this.Giveaway_CloseButton.UseVisualStyleBackColor = false;
            this.Giveaway_CloseButton.Click += new System.EventHandler(this.Giveaway_CloseButton_Click);
            // 
            // Giveaway_OpenButton
            // 
            this.Giveaway_OpenButton.BackColor = System.Drawing.Color.White;
            this.Giveaway_OpenButton.Enabled = false;
            this.Giveaway_OpenButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Giveaway_OpenButton.Location = new System.Drawing.Point(239, 533);
            this.Giveaway_OpenButton.Name = "Giveaway_OpenButton";
            this.Giveaway_OpenButton.Size = new System.Drawing.Size(109, 23);
            this.Giveaway_OpenButton.TabIndex = 55;
            this.Giveaway_OpenButton.Text = "Open";
            this.Giveaway_OpenButton.UseVisualStyleBackColor = false;
            this.Giveaway_OpenButton.Click += new System.EventHandler(this.Giveaway_OpenButton_Click);
            // 
            // GiveawayBansLabel
            // 
            this.GiveawayBansLabel.AutoSize = true;
            this.GiveawayBansLabel.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GiveawayBansLabel.Location = new System.Drawing.Point(381, 25);
            this.GiveawayBansLabel.Name = "GiveawayBansLabel";
            this.GiveawayBansLabel.Size = new System.Drawing.Size(35, 19);
            this.GiveawayBansLabel.TabIndex = 53;
            this.GiveawayBansLabel.Text = "Bans";
            this.GiveawayBansLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GiveawayBansSpacer
            // 
            this.GiveawayBansSpacer.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Bold);
            this.GiveawayBansSpacer.Location = new System.Drawing.Point(318, 25);
            this.GiveawayBansSpacer.Name = "GiveawayBansSpacer";
            this.GiveawayBansSpacer.Size = new System.Drawing.Size(496, 11);
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
            this.Giveaway_TypeTickets.Location = new System.Drawing.Point(6, 92);
            this.Giveaway_TypeTickets.Name = "Giveaway_TypeTickets";
            this.Giveaway_TypeTickets.Size = new System.Drawing.Size(241, 17);
            this.Giveaway_TypeTickets.TabIndex = 50;
            this.Giveaway_TypeTickets.Text = "Tickets        Cost:                        Max:             ";
            this.Giveaway_TypeTickets.UseVisualStyleBackColor = true;
            this.Giveaway_TypeTickets.CheckedChanged += new System.EventHandler(this.Giveaway_Settings_Changed);
            // 
            // Giveaway_TypeKeyword
            // 
            this.Giveaway_TypeKeyword.AutoSize = true;
            this.Giveaway_TypeKeyword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Giveaway_TypeKeyword.Location = new System.Drawing.Point(6, 69);
            this.Giveaway_TypeKeyword.Name = "Giveaway_TypeKeyword";
            this.Giveaway_TypeKeyword.Size = new System.Drawing.Size(241, 17);
            this.Giveaway_TypeKeyword.TabIndex = 49;
            this.Giveaway_TypeKeyword.Text = "Keyword      Custom:                                        ";
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
            this.Giveaway_TypeActive.CheckedChanged += new System.EventHandler(this.Giveaway_Settings_Changed);
            // 
            // Giveaway_UserCount
            // 
            this.Giveaway_UserCount.AutoSize = true;
            this.Giveaway_UserCount.Location = new System.Drawing.Point(811, 46);
            this.Giveaway_UserCount.Name = "Giveaway_UserCount";
            this.Giveaway_UserCount.Size = new System.Drawing.Size(46, 13);
            this.Giveaway_UserCount.TabIndex = 71;
            this.Giveaway_UserCount.Text = "Users: 0";
            // 
            // Giveaway_SubscribersWinMultiplier
            // 
            this.Giveaway_SubscribersWinMultiplier.AutoSize = true;
            this.Giveaway_SubscribersWinMultiplier.Enabled = false;
            this.Giveaway_SubscribersWinMultiplier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Giveaway_SubscribersWinMultiplier.Location = new System.Drawing.Point(6, 283);
            this.Giveaway_SubscribersWinMultiplier.Name = "Giveaway_SubscribersWinMultiplier";
            this.Giveaway_SubscribersWinMultiplier.Size = new System.Drawing.Size(173, 17);
            this.Giveaway_SubscribersWinMultiplier.TabIndex = 72;
            this.Giveaway_SubscribersWinMultiplier.Text = "Subscribers win multiplier           ";
            this.Giveaway_SubscribersWinMultiplier.UseVisualStyleBackColor = true;
            this.Giveaway_SubscribersWinMultiplier.CheckedChanged += new System.EventHandler(this.Giveaway_Settings_Changed);
            // 
            // AuthenticationLabel
            // 
            this.AuthenticationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AuthenticationLabel.Location = new System.Drawing.Point(108, 30);
            this.AuthenticationLabel.Name = "AuthenticationLabel";
            this.AuthenticationLabel.Size = new System.Drawing.Size(1024, 31);
            this.AuthenticationLabel.TabIndex = 66;
            this.AuthenticationLabel.Text = "Connect to the bot\'s account";
            this.AuthenticationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SpamFilterWindow
            // 
            this.SpamFilterWindow.BackColor = System.Drawing.Color.White;
            this.SpamFilterWindow.Controls.Add(this.Spam_CWLAnnounceTimeouts);
            this.SpamFilterWindow.Controls.Add(this.Spam_CWLBox);
            this.SpamFilterWindow.Controls.Add(this.label22);
            this.SpamFilterWindow.Controls.Add(this.Spam_CWL);
            this.SpamFilterWindow.Controls.Add(this.Spam_CWLLabel);
            this.SpamFilterWindow.Controls.Add(this.Spam_CWLSpacer);
            this.SpamFilterWindow.Location = new System.Drawing.Point(108, 30);
            this.SpamFilterWindow.Name = "SpamFilterWindow";
            this.SpamFilterWindow.Size = new System.Drawing.Size(1024, 562);
            this.SpamFilterWindow.TabIndex = 68;
            // 
            // Spam_CWLAnnounceTimeouts
            // 
            this.Spam_CWLAnnounceTimeouts.AutoSize = true;
            this.Spam_CWLAnnounceTimeouts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Spam_CWLAnnounceTimeouts.Location = new System.Drawing.Point(74, 49);
            this.Spam_CWLAnnounceTimeouts.Name = "Spam_CWLAnnounceTimeouts";
            this.Spam_CWLAnnounceTimeouts.Size = new System.Drawing.Size(114, 17);
            this.Spam_CWLAnnounceTimeouts.TabIndex = 69;
            this.Spam_CWLAnnounceTimeouts.Text = "Announce timeouts";
            this.Spam_CWLAnnounceTimeouts.UseVisualStyleBackColor = true;
            this.Spam_CWLAnnounceTimeouts.CheckedChanged += new System.EventHandler(this.Settings_Changed);
            // 
            // Spam_CWLBox
            // 
            this.Spam_CWLBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Spam_CWLBox.Location = new System.Drawing.Point(6, 69);
            this.Spam_CWLBox.MaxLength = 64;
            this.Spam_CWLBox.Name = "Spam_CWLBox";
            this.Spam_CWLBox.Size = new System.Drawing.Size(240, 20);
            this.Spam_CWLBox.TabIndex = 68;
            this.Spam_CWLBox.Text = "abcdefghijklmnopqrstuvwxyz0123456789";
            this.Spam_CWLBox.TextChanged += new System.EventHandler(this.Spam_CWLBox_TextChanged);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(3, 21);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(245, 26);
            this.label22.TabIndex = 66;
            this.label22.Text = "Blocks all characters but the ones entered below\r\n* Added by default: `~!@#$%^&*(" +
    ")-_=+\'\"\\/.,?[]{}<>|:;";
            // 
            // Spam_CWL
            // 
            this.Spam_CWL.AutoSize = true;
            this.Spam_CWL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Spam_CWL.Location = new System.Drawing.Point(6, 49);
            this.Spam_CWL.Name = "Spam_CWL";
            this.Spam_CWL.Size = new System.Drawing.Size(62, 17);
            this.Spam_CWL.TabIndex = 65;
            this.Spam_CWL.Text = "Enabled";
            this.Spam_CWL.UseVisualStyleBackColor = true;
            this.Spam_CWL.CheckedChanged += new System.EventHandler(this.Settings_Changed);
            // 
            // Spam_CWLLabel
            // 
            this.Spam_CWLLabel.AutoSize = true;
            this.Spam_CWLLabel.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Spam_CWLLabel.Location = new System.Drawing.Point(71, 3);
            this.Spam_CWLLabel.Name = "Spam_CWLLabel";
            this.Spam_CWLLabel.Size = new System.Drawing.Size(125, 19);
            this.Spam_CWLLabel.TabIndex = 64;
            this.Spam_CWLLabel.Text = "Characters white list";
            this.Spam_CWLLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Spam_CWLSpacer
            // 
            this.Spam_CWLSpacer.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Bold);
            this.Spam_CWLSpacer.Location = new System.Drawing.Point(-1, 3);
            this.Spam_CWLSpacer.Name = "Spam_CWLSpacer";
            this.Spam_CWLSpacer.Size = new System.Drawing.Size(254, 12);
            this.Spam_CWLSpacer.TabIndex = 63;
            this.Spam_CWLSpacer.TabStop = false;
            // 
            // SongRequestPlayer
            // 
            this.SongRequestPlayer.Location = new System.Drawing.Point(108, 30);
            this.SongRequestPlayer.MinimumSize = new System.Drawing.Size(20, 20);
            this.SongRequestPlayer.Name = "SongRequestPlayer";
            this.SongRequestPlayer.ScriptErrorsSuppressed = true;
            this.SongRequestPlayer.ScrollBarsEnabled = false;
            this.SongRequestPlayer.Size = new System.Drawing.Size(1024, 562);
            this.SongRequestPlayer.TabIndex = 69;
            this.SongRequestPlayer.Visible = false;
            this.SongRequestPlayer.WebBrowserShortcutsEnabled = false;
            // 
            // AuthenticationBrowser
            // 
            this.AuthenticationBrowser.Location = new System.Drawing.Point(108, 30);
            this.AuthenticationBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.AuthenticationBrowser.Name = "AuthenticationBrowser";
            this.AuthenticationBrowser.ScriptErrorsSuppressed = true;
            this.AuthenticationBrowser.ScrollBarsEnabled = false;
            this.AuthenticationBrowser.Size = new System.Drawing.Size(1024, 562);
            this.AuthenticationBrowser.TabIndex = 0;
            this.AuthenticationBrowser.WebBrowserShortcutsEnabled = false;
            this.AuthenticationBrowser.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.AuthenticationBrowser_Navigated);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1140, 600);
            this.Controls.Add(this.AboutWindow);
            this.Controls.Add(this.SettingsWindow);
            this.Controls.Add(this.DonationsWindow);
            this.Controls.Add(this.ChannelWindow);
            this.Controls.Add(this.CurrencyWindow);
            this.Controls.Add(this.GiveawayWindow);
            this.Controls.Add(this.AuthenticationLabel);
            this.Controls.Add(this.AuthenticationBrowser);
            this.Controls.Add(this.SpamFilterWindow);
            this.Controls.Add(this.SongRequestPlayer);
            this.Controls.Add(this.panel2);
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.Text = "ModBot";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.Controls.SetChildIndex(this.SongRequestPlayer, 0);
            this.Controls.SetChildIndex(this.SpamFilterWindow, 0);
            this.Controls.SetChildIndex(this.AuthenticationBrowser, 0);
            this.Controls.SetChildIndex(this.AuthenticationLabel, 0);
            this.Controls.SetChildIndex(this.GiveawayWindow, 0);
            this.Controls.SetChildIndex(this.CurrencyWindow, 0);
            this.Controls.SetChildIndex(this.ChannelWindow, 0);
            this.Controls.SetChildIndex(this.DonationsWindow, 0);
            this.Controls.SetChildIndex(this.SettingsWindow, 0);
            this.Controls.SetChildIndex(this.AboutWindow, 0);
            ((System.ComponentModel.ISupportInitialize)(this.Giveaway_MinCurrencyBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Giveaway_ActiveUserTime)).EndInit();
            this.Giveaway_SettingsPresents.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Currency_HandoutLastActive)).EndInit();
            this.SettingsWindow.ResumeLayout(false);
            this.SettingsWindow.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MySQL_Port)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Currency_SubHandoutAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Currency_HandoutAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Currency_HandoutInterval)).EndInit();
            this.DonationsWindow.ResumeLayout(false);
            this.DonationsWindow.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RecentDonorsLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Donations_List)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TopDonorsLimit)).EndInit();
            this.AboutWindow.ResumeLayout(false);
            this.AboutWindow.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.About_Users)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DonateImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AboutImage)).EndInit();
            this.ChannelWindow.ResumeLayout(false);
            this.ChannelWindow.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Channel_ViewersChangeRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Channel_ViewersChangeInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Channel_SubscriptionRewardsList)).EndInit();
            this.CurrencyWindow.ResumeLayout(false);
            this.CurrencyWindow.PerformLayout();
            this.GiveawayWindow.ResumeLayout(false);
            this.GiveawayWindow.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Giveaway_SubscribersWinMultiplierAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Giveaway_MustWatchMinutes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Giveaway_MustWatchHours)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Giveaway_MustWatchDays)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Giveaway_MaxTickets)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Giveaway_TicketCost)).EndInit();
            this.SpamFilterWindow.ResumeLayout(false);
            this.SpamFilterWindow.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label ChannelStatusLabel;
        public System.Windows.Forms.CheckBox Giveaway_MustFollow;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox Channel_Title;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox Channel_Game;
        public System.Windows.Forms.CheckBox Giveaway_MinCurrency;
        public FlatNumericUpDown Giveaway_MinCurrencyBox;
        public System.Windows.Forms.Button Giveaway_StartButton;
        public System.Windows.Forms.Button Giveaway_RerollButton;
        public System.Windows.Forms.Button Giveaway_StopButton;
        public System.Windows.Forms.Label Giveaway_WinnerStatusLabel;
        public System.Windows.Forms.Label Giveaway_WinnerLabel;
        public FlatNumericUpDown Giveaway_ActiveUserTime;
        public System.Windows.Forms.Button Giveaway_AnnounceWinnerButton;
        public System.Windows.Forms.ListBox Giveaway_BanListListBox;
        public System.Windows.Forms.Button Giveaway_BanButton;
        public System.Windows.Forms.Button Giveaway_UnbanButton;
        public System.Windows.Forms.TextBox Giveaway_AddBanTextBox;
        public System.Windows.Forms.CheckBox Giveaway_AutoBanWinner;
        public System.Windows.Forms.CheckBox Currency_DisableCommand;
        public System.Windows.Forms.Button Giveaway_CopyWinnerButton;
        public System.Windows.Forms.Label Giveaway_WinnerTimerLabel;
        public System.Windows.Forms.Timer Giveaway_WinnerTimer;
        public System.Windows.Forms.RichTextBox Giveaway_WinnerChat;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.TabControl Giveaway_SettingsPresents;
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
        public FlatNumericUpDown Currency_HandoutLastActive;
        public System.Windows.Forms.Panel SettingsWindow;
        public System.Windows.Forms.Panel DonationsWindow;
        private System.Windows.Forms.GroupBox ConnectionSpacer;
        private System.Windows.Forms.Label ConnectionLabel;
        private System.Windows.Forms.TextBox Donations_ST_ClientId;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox Subscribers_Spreadsheet;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Button ConnectButton;
        public System.Windows.Forms.TextBox Bot_Token;
        private System.Windows.Forms.TextBox Bot_Name;
        private System.Windows.Forms.TextBox Channel_Name;
        private System.Windows.Forms.TextBox Currency_Name;
        private System.Windows.Forms.Label CurrencyLabel;
        private System.Windows.Forms.GroupBox CurrencySpacer;
        public FlatNumericUpDown Currency_HandoutInterval;
        public FlatNumericUpDown Currency_HandoutAmount;
        private System.Windows.Forms.Label DonationsLabel;
        private System.Windows.Forms.Label SubscribersLabel;
        private System.Windows.Forms.GroupBox DonationsSpacer;
        private System.Windows.Forms.GroupBox SubscribersSpacer;
        public System.Windows.Forms.Button DisconnectButton;
        public FlatNumericUpDown RecentDonorsLimit;
        public System.Windows.Forms.CheckBox UpdateTopDonorsCheckBox;
        private System.Windows.Forms.Label label15;
        public System.Windows.Forms.DataGridView Donations_List;
        public FlatNumericUpDown TopDonorsLimit;
        public System.Windows.Forms.CheckBox UpdateRecentDonorsCheckBox;
        private System.Windows.Forms.Label label16;
        public System.Windows.Forms.CheckBox UpdateLastDonorCheckBox;
        public System.Windows.Forms.Panel AboutWindow;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.LinkLabel SupportLinkLabel;
        private System.Windows.Forms.LinkLabel EmailLinkLabel;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.LinkLabel WebsiteLinkLabel;
        private System.Windows.Forms.PictureBox AboutImage;
        private System.Windows.Forms.Label VersionLabel;
        public System.Windows.Forms.Label SettingsErrorLabel;
        public System.Windows.Forms.Panel ChannelWindow;
        public System.Windows.Forms.Panel CurrencyWindow;
        public System.Windows.Forms.Panel GiveawayWindow;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox Currency_Command;
        public System.Windows.Forms.RadioButton Giveaway_TypeActive;
        public System.Windows.Forms.RadioButton Giveaway_TypeKeyword;
        public System.Windows.Forms.RadioButton Giveaway_TypeTickets;
        private System.Windows.Forms.Label GiveawayRulesLabel;
        private System.Windows.Forms.GroupBox GiveawayRulesSpacer;
        private System.Windows.Forms.Label GiveawayBansLabel;
        private System.Windows.Forms.GroupBox GiveawayBansSpacer;
        public System.Windows.Forms.Button Bot_TokenButton;
        private System.Windows.Forms.Label AuthenticationLabel;
        public System.Windows.Forms.Button Channel_TokenButton;
        public System.Windows.Forms.TextBox Channel_Token;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox Donations_ST_Token;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label14;
        public System.Windows.Forms.Button Channel_UpdateTitleGame;
        private System.Windows.Forms.Label label19;
        public FlatNumericUpDown Currency_SubHandoutAmount;
        private System.Windows.Forms.PictureBox DonateImage;
        public System.Windows.Forms.Panel SpamFilterWindow;
        private System.Windows.Forms.Label Spam_CWLLabel;
        private System.Windows.Forms.GroupBox Spam_CWLSpacer;
        public System.Windows.Forms.CheckBox Spam_CWL;
        public System.Windows.Forms.TextBox Spam_CWLBox;
        private System.Windows.Forms.Label label22;
        public System.Windows.Forms.Button Giveaway_CancelButton;
        public System.Windows.Forms.Button Giveaway_CloseButton;
        public System.Windows.Forms.Button Giveaway_OpenButton;
        public FlatNumericUpDown Giveaway_MaxTickets;
        public FlatNumericUpDown Giveaway_TicketCost;
        public System.Windows.Forms.TextBox Channel_SteamID64;
        public System.Windows.Forms.CheckBox Channel_UseSteam;
        public FlatNumericUpDown Giveaway_MustWatchMinutes;
        public FlatNumericUpDown Giveaway_MustWatchHours;
        public FlatNumericUpDown Giveaway_MustWatchDays;
        public System.Windows.Forms.CheckBox Giveaway_MustWatch;
        public System.Windows.Forms.CheckBox Giveaway_MustSubscribe;
        public System.Windows.Forms.DataGridView About_Users;
        private System.Windows.Forms.Label About_UsersLabel;
        public System.Windows.Forms.WebBrowser SongRequestPlayer;
        public System.Windows.Forms.CheckBox Misc_ShowConsole;
        public System.Windows.Forms.CheckBox Giveaway_WarnFalseEntries;
        public System.Windows.Forms.CheckBox Giveaway_AnnounceWarnedEntries;
        public System.Windows.Forms.TextBox Giveaway_CustomKeyword;
        private System.Windows.Forms.Label label25;
        public System.Windows.Forms.TextBox MySQL_Password;
        private System.Windows.Forms.Label label26;
        public System.Windows.Forms.TextBox MySQL_Username;
        private System.Windows.Forms.Label label27;
        public System.Windows.Forms.TextBox MySQL_Database;
        private System.Windows.Forms.Label label24;
        public System.Windows.Forms.TextBox MySQL_Host;
        private System.Windows.Forms.Label MySQLLabel;
        private System.Windows.Forms.GroupBox MySQLSpacer;
        public FlatNumericUpDown MySQL_Port;
        private System.Windows.Forms.Label label23;
        public System.Windows.Forms.TextBox Database_Table;
        public System.Windows.Forms.CheckBox Spam_CWLAnnounceTimeouts;
        private System.Windows.Forms.Label SubscriptionsLabel;
        private System.Windows.Forms.GroupBox SubscriptionsSpacer;
        public System.Windows.Forms.DateTimePicker Channel_SubscriptionsDate;
        public System.Windows.Forms.CheckBox Channel_SubscriptionRewards;
        public System.Windows.Forms.DataGridView Channel_SubscriptionRewardsList;
        public System.Windows.Forms.ListBox Giveaway_UserList;
        private System.Windows.Forms.Label GiveawayUsersLabel;
        private System.Windows.Forms.GroupBox GiveawayUsersSpacer;
        public System.Windows.Forms.Label Giveaway_UserCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn Donor;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Notes;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IncludeRecent;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IncludeLatest;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IncludeTop;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IncludeTopDonor;
        public FlatNumericUpDown Giveaway_SubscribersWinMultiplierAmount;
        public System.Windows.Forms.CheckBox Giveaway_SubscribersWinMultiplier;
        public System.Windows.Forms.TextBox Channel_ViewersChangeMessage;
        public FlatNumericUpDown Channel_ViewersChangeRate;
        public FlatNumericUpDown Channel_ViewersChangeInterval;
        public System.Windows.Forms.CheckBox Channel_ViewersChange;
        private System.Windows.Forms.Label MessageTimersLabel;
        private System.Windows.Forms.GroupBox MessageTimersSpacer;
        public System.Windows.Forms.TextBox Channel_WelcomeSubMessage;
        public System.Windows.Forms.CheckBox Channel_WelcomeSub;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.LinkLabel CopyrightLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn Channel;
        private System.Windows.Forms.DataGridViewTextBoxColumn Bot;
        private System.Windows.Forms.DataGridViewTextBoxColumn Title;
        private System.Windows.Forms.DataGridViewTextBoxColumn Game;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn Viewers;
        private System.Windows.Forms.DataGridViewTextBoxColumn Version;
        private System.Windows.Forms.DataGridViewTextBoxColumn Updated;
        private System.Windows.Forms.DataGridViewTextBoxColumn Reward;
        private System.Windows.Forms.DataGridViewTextBoxColumn Instructions;
        public System.Windows.Forms.WebBrowser AuthenticationBrowser;
    }
}