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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ChannelStatusLabel = new System.Windows.Forms.Label();
            this.Giveaway_MustFollow = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ChannelTitleBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ChannelGameBox = new System.Windows.Forms.TextBox();
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
            this.Giveaway_AutoBanWinnerCheckBox = new System.Windows.Forms.CheckBox();
            this.Currency_DisableCommandCheckBox = new System.Windows.Forms.CheckBox();
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
            this.SettingsWindowButton = new System.Windows.Forms.CheckBox();
            this.DonationsWindowButton = new System.Windows.Forms.CheckBox();
            this.SettingsWindow = new System.Windows.Forms.Panel();
            this.label19 = new System.Windows.Forms.Label();
            this.CurrencySubHandoutAmount = new ModBot.FlatNumericUpDown();
            this.DonationsTokenBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.GenerateChannelTokenButton = new System.Windows.Forms.Button();
            this.ChannelTokenBox = new System.Windows.Forms.TextBox();
            this.GenerateBotTokenButton = new System.Windows.Forms.Button();
            this.label21 = new System.Windows.Forms.Label();
            this.CurrencyCommandBox = new System.Windows.Forms.TextBox();
            this.SettingsErrorLabel = new System.Windows.Forms.Label();
            this.DisconnectButton = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.DonationsLabel = new System.Windows.Forms.Label();
            this.SubscribersLabel = new System.Windows.Forms.Label();
            this.CurrencyHandoutAmount = new ModBot.FlatNumericUpDown();
            this.DonationsSpacer = new System.Windows.Forms.GroupBox();
            this.CurrencyHandoutInterval = new ModBot.FlatNumericUpDown();
            this.CurrencyLabel = new System.Windows.Forms.Label();
            this.ConnectionLabel = new System.Windows.Forms.Label();
            this.CurrencySpacer = new System.Windows.Forms.GroupBox();
            this.SubscribersSpacer = new System.Windows.Forms.GroupBox();
            this.ConnectionSpacer = new System.Windows.Forms.GroupBox();
            this.DonationsClientIdBox = new System.Windows.Forms.TextBox();
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
            this.AboutWindowButton = new System.Windows.Forms.CheckBox();
            this.AboutWindow = new System.Windows.Forms.Panel();
            this.About_Users = new System.Windows.Forms.DataGridView();
            this.Channel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Version = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Viewers = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Updated = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DonateImage = new System.Windows.Forms.PictureBox();
            this.SupportLinkLabel = new System.Windows.Forms.LinkLabel();
            this.EmailLinkLabel = new System.Windows.Forms.LinkLabel();
            this.label17 = new System.Windows.Forms.Label();
            this.WebsiteLinkLabel = new System.Windows.Forms.LinkLabel();
            this.label18 = new System.Windows.Forms.Label();
            this.AboutImage = new System.Windows.Forms.PictureBox();
            this.VersionLabel = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.AuthenticationBrowser = new System.Windows.Forms.WebBrowser();
            this.ChannelWindowButton = new System.Windows.Forms.CheckBox();
            this.ChannelWindow = new System.Windows.Forms.Panel();
            this.Channel_SteamID64 = new System.Windows.Forms.TextBox();
            this.Channel_UseSteam = new System.Windows.Forms.CheckBox();
            this.UpdateTitleGameButton = new System.Windows.Forms.Button();
            this.CurrencyWindow = new System.Windows.Forms.Panel();
            this.CurrencyWindowButton = new System.Windows.Forms.CheckBox();
            this.GiveawayWindowButton = new System.Windows.Forms.CheckBox();
            this.GiveawayWindow = new System.Windows.Forms.Panel();
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
            this.AuthenticationLabel = new System.Windows.Forms.Label();
            this.SpamFilterWindowButton = new System.Windows.Forms.CheckBox();
            this.SpamFilterWindow = new System.Windows.Forms.Panel();
            this.Spam_CWLBox = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.Spam_CWL = new System.Windows.Forms.CheckBox();
            this.Spam_CWLLabel = new System.Windows.Forms.Label();
            this.Spam_CWLSpacer = new System.Windows.Forms.GroupBox();
            this.About_UsersLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Giveaway_MinCurrencyBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Giveaway_ActiveUserTime)).BeginInit();
            this.Giveaway_SettingsPresents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Currency_HandoutLastActive)).BeginInit();
            this.SettingsWindow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CurrencySubHandoutAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CurrencyHandoutAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CurrencyHandoutInterval)).BeginInit();
            this.DonationsWindow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RecentDonorsLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Donations_List)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TopDonorsLimit)).BeginInit();
            this.AboutWindow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.About_Users)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DonateImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AboutImage)).BeginInit();
            this.ChannelWindow.SuspendLayout();
            this.CurrencyWindow.SuspendLayout();
            this.GiveawayWindow.SuspendLayout();
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
            this.ChannelStatusLabel.Size = new System.Drawing.Size(814, 24);
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
            // ChannelTitleBox
            // 
            this.ChannelTitleBox.BackColor = System.Drawing.Color.White;
            this.ChannelTitleBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ChannelTitleBox.Location = new System.Drawing.Point(6, 41);
            this.ChannelTitleBox.Name = "ChannelTitleBox";
            this.ChannelTitleBox.ReadOnly = true;
            this.ChannelTitleBox.Size = new System.Drawing.Size(653, 20);
            this.ChannelTitleBox.TabIndex = 17;
            this.ChannelTitleBox.Text = "Loading...";
            this.ChannelTitleBox.TextChanged += new System.EventHandler(this.TitleGame_Modified);
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
            // ChannelGameBox
            // 
            this.ChannelGameBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.ChannelGameBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.ChannelGameBox.BackColor = System.Drawing.Color.White;
            this.ChannelGameBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ChannelGameBox.Location = new System.Drawing.Point(6, 80);
            this.ChannelGameBox.Name = "ChannelGameBox";
            this.ChannelGameBox.ReadOnly = true;
            this.ChannelGameBox.Size = new System.Drawing.Size(653, 20);
            this.ChannelGameBox.TabIndex = 19;
            this.ChannelGameBox.Text = "Loading...";
            this.ChannelGameBox.TextChanged += new System.EventHandler(this.TitleGame_Modified);
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
            this.Giveaway_MinCurrency.CheckedChanged += new System.EventHandler(this.Giveaway_MinCurrencyCheckBox_CheckedChanged);
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
            this.Giveaway_AutoBanWinnerCheckBox.Location = new System.Drawing.Point(6, 237);
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
            this.Currency_DisableCommandCheckBox.Location = new System.Drawing.Point(6, 85);
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
            // Giveaway_SettingsPresents
            // 
            this.Giveaway_SettingsPresents.Controls.Add(this.tabPage1);
            this.Giveaway_SettingsPresents.Location = new System.Drawing.Point(-1, -1);
            this.Giveaway_SettingsPresents.Name = "Giveaway_SettingsPresents";
            this.Giveaway_SettingsPresents.SelectedIndex = 0;
            this.Giveaway_SettingsPresents.Size = new System.Drawing.Size(816, 22);
            this.Giveaway_SettingsPresents.TabIndex = 43;
            this.Giveaway_SettingsPresents.SelectedIndexChanged += new System.EventHandler(this.SettingsPresents_SelectedIndexChanged);
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
            this.SettingsWindow.Controls.Add(this.label19);
            this.SettingsWindow.Controls.Add(this.CurrencySubHandoutAmount);
            this.SettingsWindow.Controls.Add(this.DonationsTokenBox);
            this.SettingsWindow.Controls.Add(this.label4);
            this.SettingsWindow.Controls.Add(this.GenerateChannelTokenButton);
            this.SettingsWindow.Controls.Add(this.ChannelTokenBox);
            this.SettingsWindow.Controls.Add(this.GenerateBotTokenButton);
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
            this.SettingsWindow.Controls.Add(this.DonationsClientIdBox);
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
            this.SettingsWindow.Controls.Add(this.label5);
            this.SettingsWindow.Location = new System.Drawing.Point(108, 30);
            this.SettingsWindow.Name = "SettingsWindow";
            this.SettingsWindow.Size = new System.Drawing.Size(814, 562);
            this.SettingsWindow.TabIndex = 58;
            this.SettingsWindow.Visible = this.SettingsWindowButton.Checked;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(662, 247);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(142, 13);
            this.label19.TabIndex = 94;
            this.label19.Text = "Subscribers\' Payout Amount:";
            // 
            // CurrencySubHandoutAmount
            // 
            this.CurrencySubHandoutAmount.Location = new System.Drawing.Point(665, 263);
            this.CurrencySubHandoutAmount.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.CurrencySubHandoutAmount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.CurrencySubHandoutAmount.Name = "CurrencySubHandoutAmount";
            this.CurrencySubHandoutAmount.Size = new System.Drawing.Size(143, 20);
            this.CurrencySubHandoutAmount.TabIndex = 93;
            this.CurrencySubHandoutAmount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // DonationsTokenBox
            // 
            this.DonationsTokenBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DonationsTokenBox.Location = new System.Drawing.Point(410, 323);
            this.DonationsTokenBox.Name = "DonationsTokenBox";
            this.DonationsTokenBox.PasswordChar = '*';
            this.DonationsTokenBox.Size = new System.Drawing.Size(398, 20);
            this.DonationsTokenBox.TabIndex = 91;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(309, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(249, 13);
            this.label4.TabIndex = 90;
            this.label4.Text = "Channel\'s Access Token (Optional, recommended):";
            // 
            // GenerateChannelTokenButton
            // 
            this.GenerateChannelTokenButton.BackColor = System.Drawing.Color.White;
            this.GenerateChannelTokenButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GenerateChannelTokenButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GenerateChannelTokenButton.Location = new System.Drawing.Point(708, 72);
            this.GenerateChannelTokenButton.Name = "GenerateChannelTokenButton";
            this.GenerateChannelTokenButton.Size = new System.Drawing.Size(100, 22);
            this.GenerateChannelTokenButton.TabIndex = 89;
            this.GenerateChannelTokenButton.Text = "Generate";
            this.GenerateChannelTokenButton.UseVisualStyleBackColor = false;
            this.GenerateChannelTokenButton.Click += new System.EventHandler(this.GenerateToken_Request);
            // 
            // ChannelTokenBox
            // 
            this.ChannelTokenBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ChannelTokenBox.Location = new System.Drawing.Point(312, 73);
            this.ChannelTokenBox.Name = "ChannelTokenBox";
            this.ChannelTokenBox.PasswordChar = '*';
            this.ChannelTokenBox.Size = new System.Drawing.Size(390, 20);
            this.ChannelTokenBox.TabIndex = 88;
            // 
            // GenerateBotTokenButton
            // 
            this.GenerateBotTokenButton.BackColor = System.Drawing.Color.White;
            this.GenerateBotTokenButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GenerateBotTokenButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GenerateBotTokenButton.Location = new System.Drawing.Point(708, 33);
            this.GenerateBotTokenButton.Name = "GenerateBotTokenButton";
            this.GenerateBotTokenButton.Size = new System.Drawing.Size(100, 22);
            this.GenerateBotTokenButton.TabIndex = 87;
            this.GenerateBotTokenButton.Text = "Generate";
            this.GenerateBotTokenButton.UseVisualStyleBackColor = false;
            this.GenerateBotTokenButton.Click += new System.EventHandler(this.GenerateToken_Request);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(407, 191);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(164, 13);
            this.label21.TabIndex = 85;
            this.label21.Text = "Currency Command (starts with !):";
            // 
            // CurrencyCommandBox
            // 
            this.CurrencyCommandBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CurrencyCommandBox.Location = new System.Drawing.Point(410, 207);
            this.CurrencyCommandBox.MaxLength = 64;
            this.CurrencyCommandBox.Name = "CurrencyCommandBox";
            this.CurrencyCommandBox.Size = new System.Drawing.Size(398, 20);
            this.CurrencyCommandBox.TabIndex = 86;
            this.CurrencyCommandBox.Text = "ModCoins";
            this.CurrencyCommandBox.TextChanged += new System.EventHandler(this.ConnectionDetailsChanged);
            // 
            // SettingsErrorLabel
            // 
            this.SettingsErrorLabel.AutoSize = true;
            this.SettingsErrorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SettingsErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.SettingsErrorLabel.Location = new System.Drawing.Point(3, 369);
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
            this.DisconnectButton.Location = new System.Drawing.Point(492, 506);
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
            // CurrencyHandoutAmount
            // 
            this.CurrencyHandoutAmount.Location = new System.Drawing.Point(6, 168);
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
            this.DonationsSpacer.Location = new System.Drawing.Point(-1, 289);
            this.DonationsSpacer.Name = "DonationsSpacer";
            this.DonationsSpacer.Size = new System.Drawing.Size(816, 11);
            this.DonationsSpacer.TabIndex = 80;
            this.DonationsSpacer.TabStop = false;
            // 
            // CurrencyHandoutInterval
            // 
            this.CurrencyHandoutInterval.Location = new System.Drawing.Point(6, 129);
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
            this.CurrencySpacer.Size = new System.Drawing.Size(816, 11);
            this.CurrencySpacer.TabIndex = 61;
            this.CurrencySpacer.TabStop = false;
            // 
            // SubscribersSpacer
            // 
            this.SubscribersSpacer.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Bold);
            this.SubscribersSpacer.Location = new System.Drawing.Point(-1, 233);
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
            // DonationsClientIdBox
            // 
            this.DonationsClientIdBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DonationsClientIdBox.Location = new System.Drawing.Point(6, 323);
            this.DonationsClientIdBox.Name = "DonationsClientIdBox";
            this.DonationsClientIdBox.PasswordChar = '*';
            this.DonationsClientIdBox.Size = new System.Drawing.Size(398, 20);
            this.DonationsClientIdBox.TabIndex = 76;
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
            this.label8.Location = new System.Drawing.Point(3, 307);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(104, 13);
            this.label8.TabIndex = 77;
            this.label8.Text = "Stream Tip Client ID:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(309, 20);
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
            // SubLinkBox
            // 
            this.SubLinkBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SubLinkBox.Location = new System.Drawing.Point(6, 263);
            this.SubLinkBox.Name = "SubLinkBox";
            this.SubLinkBox.Size = new System.Drawing.Size(653, 20);
            this.SubLinkBox.TabIndex = 68;
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
            this.BotPasswordBox.Location = new System.Drawing.Point(312, 34);
            this.BotPasswordBox.Name = "BotPasswordBox";
            this.BotPasswordBox.PasswordChar = '*';
            this.BotPasswordBox.Size = new System.Drawing.Size(390, 20);
            this.BotPasswordBox.TabIndex = 66;
            this.BotPasswordBox.TextChanged += new System.EventHandler(this.ConnectionDetailsChanged);
            // 
            // BotNameBox
            // 
            this.BotNameBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BotNameBox.Location = new System.Drawing.Point(6, 34);
            this.BotNameBox.MaxLength = 64;
            this.BotNameBox.Name = "BotNameBox";
            this.BotNameBox.Size = new System.Drawing.Size(300, 20);
            this.BotNameBox.TabIndex = 64;
            this.BotNameBox.Text = "ModBot";
            this.BotNameBox.TextChanged += new System.EventHandler(this.ConnectionDetailsChanged);
            // 
            // ChannelBox
            // 
            this.ChannelBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ChannelBox.Location = new System.Drawing.Point(6, 73);
            this.ChannelBox.MaxLength = 64;
            this.ChannelBox.Name = "ChannelBox";
            this.ChannelBox.Size = new System.Drawing.Size(300, 20);
            this.ChannelBox.TabIndex = 67;
            this.ChannelBox.Text = "ModChannel";
            this.ChannelBox.TextChanged += new System.EventHandler(this.ConnectionDetailsChanged);
            // 
            // CurrencyNameBox
            // 
            this.CurrencyNameBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CurrencyNameBox.Location = new System.Drawing.Point(6, 207);
            this.CurrencyNameBox.MaxLength = 64;
            this.CurrencyNameBox.Name = "CurrencyNameBox";
            this.CurrencyNameBox.Size = new System.Drawing.Size(398, 20);
            this.CurrencyNameBox.TabIndex = 71;
            this.CurrencyNameBox.Text = "Mod Coins";
            this.CurrencyNameBox.TextChanged += new System.EventHandler(this.ConnectionDetailsChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(407, 307);
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
            this.Donations_List.Location = new System.Drawing.Point(0, 51);
            this.Donations_List.MultiSelect = false;
            this.Donations_List.Name = "Donations_List";
            this.Donations_List.RowHeadersVisible = false;
            this.Donations_List.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.Donations_List.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Donations_List.Size = new System.Drawing.Size(814, 267);
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
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Notes.DefaultCellStyle = dataGridViewCellStyle5;
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
            this.AboutWindowButton.Location = new System.Drawing.Point(8, 280);
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
            this.AboutWindow.Controls.Add(this.About_UsersLabel);
            this.AboutWindow.Controls.Add(this.About_Users);
            this.AboutWindow.Controls.Add(this.DonateImage);
            this.AboutWindow.Controls.Add(this.SupportLinkLabel);
            this.AboutWindow.Controls.Add(this.EmailLinkLabel);
            this.AboutWindow.Controls.Add(this.label17);
            this.AboutWindow.Controls.Add(this.WebsiteLinkLabel);
            this.AboutWindow.Controls.Add(this.label18);
            this.AboutWindow.Controls.Add(this.AboutImage);
            this.AboutWindow.Controls.Add(this.VersionLabel);
            this.AboutWindow.Controls.Add(this.label20);
            this.AboutWindow.Location = new System.Drawing.Point(108, 30);
            this.AboutWindow.Name = "AboutWindow";
            this.AboutWindow.Size = new System.Drawing.Size(814, 562);
            this.AboutWindow.TabIndex = 61;
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
            this.Status,
            this.Version,
            this.Viewers,
            this.Updated});
            this.About_Users.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.About_Users.Location = new System.Drawing.Point(0, 343);
            this.About_Users.MultiSelect = false;
            this.About_Users.Name = "About_Users";
            this.About_Users.RowHeadersVisible = false;
            this.About_Users.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.About_Users.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.About_Users.Size = new System.Drawing.Size(814, 219);
            this.About_Users.TabIndex = 61;
            this.About_Users.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.About_Users_SortCompare);
            // 
            // Channel
            // 
            this.Channel.HeaderText = "Channel";
            this.Channel.Name = "Channel";
            this.Channel.ReadOnly = true;
            this.Channel.Width = 300;
            // 
            // Status
            // 
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            // 
            // Version
            // 
            this.Version.HeaderText = "Version";
            this.Version.Name = "Version";
            this.Version.ReadOnly = true;
            // 
            // Viewers
            // 
            this.Viewers.HeaderText = "Viewers";
            this.Viewers.Name = "Viewers";
            this.Viewers.ReadOnly = true;
            // 
            // Updated
            // 
            this.Updated.HeaderText = "Updated";
            this.Updated.Name = "Updated";
            this.Updated.ReadOnly = true;
            this.Updated.Width = 196;
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
            // AboutImage
            // 
            this.AboutImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.AboutImage.Image = global::ModBot.Properties.Resources.AboutImage;
            this.AboutImage.Location = new System.Drawing.Point(0, 0);
            this.AboutImage.Name = "AboutImage";
            this.AboutImage.Size = new System.Drawing.Size(814, 83);
            this.AboutImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.AboutImage.TabIndex = 18;
            this.AboutImage.TabStop = false;
            // 
            // VersionLabel
            // 
            this.VersionLabel.AutoSize = true;
            this.VersionLabel.Location = new System.Drawing.Point(3, 135);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(45, 13);
            this.VersionLabel.TabIndex = 16;
            this.VersionLabel.Text = "Version:";
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
            this.ChannelWindow.Controls.Add(this.Channel_SteamID64);
            this.ChannelWindow.Controls.Add(this.Channel_UseSteam);
            this.ChannelWindow.Controls.Add(this.UpdateTitleGameButton);
            this.ChannelWindow.Controls.Add(this.ChannelStatusLabel);
            this.ChannelWindow.Controls.Add(this.label2);
            this.ChannelWindow.Controls.Add(this.ChannelTitleBox);
            this.ChannelWindow.Controls.Add(this.label3);
            this.ChannelWindow.Controls.Add(this.ChannelGameBox);
            this.ChannelWindow.Location = new System.Drawing.Point(108, 30);
            this.ChannelWindow.Name = "ChannelWindow";
            this.ChannelWindow.Size = new System.Drawing.Size(814, 562);
            this.ChannelWindow.TabIndex = 63;
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
            this.Channel_UseSteam.Enabled = false;
            this.Channel_UseSteam.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Channel_UseSteam.Location = new System.Drawing.Point(6, 106);
            this.Channel_UseSteam.Name = "Channel_UseSteam";
            this.Channel_UseSteam.Size = new System.Drawing.Size(461, 17);
            this.Channel_UseSteam.TabIndex = 36;
            this.Channel_UseSteam.Text = "Update game from Steam                                                (Profile pr" +
    "ivacy must be set as public)";
            this.Channel_UseSteam.UseVisualStyleBackColor = true;
            this.Channel_UseSteam.CheckedChanged += new System.EventHandler(this.Channel_UseSteam_CheckedChanged);
            // 
            // UpdateTitleGameButton
            // 
            this.UpdateTitleGameButton.BackColor = System.Drawing.Color.White;
            this.UpdateTitleGameButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UpdateTitleGameButton.Location = new System.Drawing.Point(665, 41);
            this.UpdateTitleGameButton.Name = "UpdateTitleGameButton";
            this.UpdateTitleGameButton.Size = new System.Drawing.Size(143, 59);
            this.UpdateTitleGameButton.TabIndex = 32;
            this.UpdateTitleGameButton.Text = "Update";
            this.UpdateTitleGameButton.UseVisualStyleBackColor = false;
            this.UpdateTitleGameButton.Click += new System.EventHandler(this.UpdateTitleGameButton_Click);
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
            this.CurrencyWindowButton.Location = new System.Drawing.Point(8, 111);
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
            this.GiveawayWindowButton.Location = new System.Drawing.Point(8, 150);
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
            this.GiveawayWindow.Controls.Add(this.Giveaway_MinCurrency);
            this.GiveawayWindow.Controls.Add(this.Giveaway_TypeActive);
            this.GiveawayWindow.Location = new System.Drawing.Point(108, 30);
            this.GiveawayWindow.Name = "GiveawayWindow";
            this.GiveawayWindow.Size = new System.Drawing.Size(814, 562);
            this.GiveawayWindow.TabIndex = 63;
            // 
            // Giveaway_MustWatchMinutes
            // 
            this.Giveaway_MustWatchMinutes.Enabled = false;
            this.Giveaway_MustWatchMinutes.Location = new System.Drawing.Point(318, 214);
            this.Giveaway_MustWatchMinutes.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
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
            this.Giveaway_MustWatchHours.Location = new System.Drawing.Point(232, 214);
            this.Giveaway_MustWatchHours.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.Giveaway_MustWatchHours.Name = "Giveaway_MustWatchHours";
            this.Giveaway_MustWatchHours.Size = new System.Drawing.Size(32, 20);
            this.Giveaway_MustWatchHours.TabIndex = 63;
            this.Giveaway_MustWatchHours.ValueChanged += new System.EventHandler(this.Giveaway_Settings_Changed);
            // 
            // Giveaway_MustWatchDays
            // 
            this.Giveaway_MustWatchDays.Enabled = false;
            this.Giveaway_MustWatchDays.Location = new System.Drawing.Point(157, 214);
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
            this.Giveaway_MustWatch.Size = new System.Drawing.Size(390, 17);
            this.Giveaway_MustWatch.TabIndex = 61;
            this.Giveaway_MustWatch.Text = "Has watched the stream for                 days,             hours and           " +
    "  minutes";
            this.Giveaway_MustWatch.UseVisualStyleBackColor = true;
            this.Giveaway_MustWatch.CheckedChanged += new System.EventHandler(this.Giveaway_MustWatch_CheckedChanged);
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
            this.Giveaway_MaxTickets.Location = new System.Drawing.Point(205, 89);
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
            this.Giveaway_TicketCost.Location = new System.Drawing.Point(110, 89);
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
            this.Giveaway_TypeTickets.Location = new System.Drawing.Point(6, 92);
            this.Giveaway_TypeTickets.Name = "Giveaway_TypeTickets";
            this.Giveaway_TypeTickets.Size = new System.Drawing.Size(241, 17);
            this.Giveaway_TypeTickets.TabIndex = 50;
            this.Giveaway_TypeTickets.Text = "Tickets        Cost:                        Max:             ";
            this.Giveaway_TypeTickets.UseVisualStyleBackColor = true;
            this.Giveaway_TypeTickets.CheckedChanged += new System.EventHandler(this.Giveaway_TypeTickets_CheckedChanged);
            // 
            // Giveaway_TypeKeyword
            // 
            this.Giveaway_TypeKeyword.AutoSize = true;
            this.Giveaway_TypeKeyword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Giveaway_TypeKeyword.Location = new System.Drawing.Point(6, 69);
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
            // SpamFilterWindowButton
            // 
            this.SpamFilterWindowButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.SpamFilterWindowButton.Enabled = false;
            this.SpamFilterWindowButton.FlatAppearance.BorderSize = 0;
            this.SpamFilterWindowButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.SpamFilterWindowButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SpamFilterWindowButton.Font = new System.Drawing.Font("Segoe Print", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SpamFilterWindowButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.SpamFilterWindowButton.Location = new System.Drawing.Point(8, 235);
            this.SpamFilterWindowButton.Name = "SpamFilterWindowButton";
            this.SpamFilterWindowButton.Size = new System.Drawing.Size(100, 46);
            this.SpamFilterWindowButton.TabIndex = 67;
            this.SpamFilterWindowButton.TabStop = false;
            this.SpamFilterWindowButton.Text = "Spam Filter";
            this.SpamFilterWindowButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.SpamFilterWindowButton.UseVisualStyleBackColor = true;
            this.SpamFilterWindowButton.CheckedChanged += new System.EventHandler(this.WindowChanged);
            // 
            // SpamFilterWindow
            // 
            this.SpamFilterWindow.BackColor = System.Drawing.Color.White;
            this.SpamFilterWindow.Controls.Add(this.Spam_CWLBox);
            this.SpamFilterWindow.Controls.Add(this.label22);
            this.SpamFilterWindow.Controls.Add(this.Spam_CWL);
            this.SpamFilterWindow.Controls.Add(this.Spam_CWLLabel);
            this.SpamFilterWindow.Controls.Add(this.Spam_CWLSpacer);
            this.SpamFilterWindow.Location = new System.Drawing.Point(108, 30);
            this.SpamFilterWindow.Name = "SpamFilterWindow";
            this.SpamFilterWindow.Size = new System.Drawing.Size(814, 562);
            this.SpamFilterWindow.TabIndex = 68;
            // 
            // Spam_CWLBox
            // 
            this.Spam_CWLBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Spam_CWLBox.Location = new System.Drawing.Point(6, 69);
            this.Spam_CWLBox.MaxLength = 64;
            this.Spam_CWLBox.Name = "Spam_CWLBox";
            this.Spam_CWLBox.Size = new System.Drawing.Size(232, 20);
            this.Spam_CWLBox.TabIndex = 68;
            this.Spam_CWLBox.Text = "abcdefghijklmnopqrstuvwxyz0123456789";
            this.Spam_CWLBox.TextChanged += new System.EventHandler(this.Spam_CWLBox_TextChanged);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(3, 21);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(237, 26);
            this.label22.TabIndex = 66;
            this.label22.Text = "Blocks all characters but the ones entered below\r\n* Added by default: ()*&^%$#@!\'" +
    "\"\\/.,?[]{}+_=-<>|:;";
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
            this.Spam_CWL.CheckedChanged += new System.EventHandler(this.Spam_CWL_CheckedChanged);
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
            this.Spam_CWLSpacer.Size = new System.Drawing.Size(245, 12);
            this.Spam_CWLSpacer.TabIndex = 63;
            this.Spam_CWLSpacer.TabStop = false;
            // 
            // About_UsersLabel
            // 
            this.About_UsersLabel.AutoSize = true;
            this.About_UsersLabel.Location = new System.Drawing.Point(3, 329);
            this.About_UsersLabel.Name = "About_UsersLabel";
            this.About_UsersLabel.Size = new System.Drawing.Size(64, 13);
            this.About_UsersLabel.TabIndex = 62;
            this.About_UsersLabel.Text = "Other users:";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(930, 600);
            this.Controls.Add(this.SpamFilterWindowButton);
            this.Controls.Add(this.GiveawayWindowButton);
            this.Controls.Add(this.CurrencyWindowButton);
            this.Controls.Add(this.ChannelWindowButton);
            this.Controls.Add(this.AboutWindowButton);
            this.Controls.Add(this.DonationsWindowButton);
            this.Controls.Add(this.SettingsWindowButton);
            this.Controls.Add(this.GiveawayWindow);
            this.Controls.Add(this.AuthenticationLabel);
            this.Controls.Add(this.AuthenticationBrowser);
            this.Controls.Add(this.SpamFilterWindow);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.AboutWindow);
            this.Controls.Add(this.SettingsWindow);
            this.Controls.Add(this.DonationsWindow);
            this.Controls.Add(this.ChannelWindow);
            this.Controls.Add(this.CurrencyWindow);
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.Text = "ModBot";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.Controls.SetChildIndex(this.CurrencyWindow, 0);
            this.Controls.SetChildIndex(this.ChannelWindow, 0);
            this.Controls.SetChildIndex(this.DonationsWindow, 0);
            this.Controls.SetChildIndex(this.SettingsWindow, 0);
            this.Controls.SetChildIndex(this.AboutWindow, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.Controls.SetChildIndex(this.SpamFilterWindow, 0);
            this.Controls.SetChildIndex(this.AuthenticationBrowser, 0);
            this.Controls.SetChildIndex(this.AuthenticationLabel, 0);
            this.Controls.SetChildIndex(this.GiveawayWindow, 0);
            this.Controls.SetChildIndex(this.SettingsWindowButton, 0);
            this.Controls.SetChildIndex(this.DonationsWindowButton, 0);
            this.Controls.SetChildIndex(this.AboutWindowButton, 0);
            this.Controls.SetChildIndex(this.ChannelWindowButton, 0);
            this.Controls.SetChildIndex(this.CurrencyWindowButton, 0);
            this.Controls.SetChildIndex(this.GiveawayWindowButton, 0);
            this.Controls.SetChildIndex(this.SpamFilterWindowButton, 0);
            ((System.ComponentModel.ISupportInitialize)(this.Giveaway_MinCurrencyBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Giveaway_ActiveUserTime)).EndInit();
            this.Giveaway_SettingsPresents.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Currency_HandoutLastActive)).EndInit();
            this.SettingsWindow.ResumeLayout(false);
            this.SettingsWindow.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CurrencySubHandoutAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CurrencyHandoutAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CurrencyHandoutInterval)).EndInit();
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
            this.CurrencyWindow.ResumeLayout(false);
            this.CurrencyWindow.PerformLayout();
            this.GiveawayWindow.ResumeLayout(false);
            this.GiveawayWindow.PerformLayout();
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
        public System.Windows.Forms.TextBox ChannelTitleBox;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox ChannelGameBox;
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
        public System.Windows.Forms.CheckBox Giveaway_AutoBanWinnerCheckBox;
        public System.Windows.Forms.CheckBox Currency_DisableCommandCheckBox;
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
        public System.Windows.Forms.CheckBox SettingsWindowButton;
        public System.Windows.Forms.CheckBox DonationsWindowButton;
        public System.Windows.Forms.Panel SettingsWindow;
        public System.Windows.Forms.Panel DonationsWindow;
        private System.Windows.Forms.GroupBox ConnectionSpacer;
        private System.Windows.Forms.Label ConnectionLabel;
        private System.Windows.Forms.TextBox DonationsClientIdBox;
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
        public System.Windows.Forms.TextBox BotPasswordBox;
        private System.Windows.Forms.TextBox BotNameBox;
        private System.Windows.Forms.TextBox ChannelBox;
        private System.Windows.Forms.TextBox CurrencyNameBox;
        private System.Windows.Forms.Label CurrencyLabel;
        private System.Windows.Forms.GroupBox CurrencySpacer;
        public FlatNumericUpDown CurrencyHandoutInterval;
        public FlatNumericUpDown CurrencyHandoutAmount;
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
        public System.Windows.Forms.CheckBox AboutWindowButton;
        private System.Windows.Forms.Panel AboutWindow;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.LinkLabel SupportLinkLabel;
        private System.Windows.Forms.LinkLabel EmailLinkLabel;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.LinkLabel WebsiteLinkLabel;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.PictureBox AboutImage;
        private System.Windows.Forms.Label VersionLabel;
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
        public System.Windows.Forms.Button GenerateBotTokenButton;
        private System.Windows.Forms.WebBrowser AuthenticationBrowser;
        private System.Windows.Forms.Label AuthenticationLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn Donor;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Notes;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IncludeRecent;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IncludeLatest;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IncludeTop;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IncludeTopDonor;
        public System.Windows.Forms.Button GenerateChannelTokenButton;
        public System.Windows.Forms.TextBox ChannelTokenBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox DonationsTokenBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label14;
        public System.Windows.Forms.Button UpdateTitleGameButton;
        private System.Windows.Forms.Label label19;
        public FlatNumericUpDown CurrencySubHandoutAmount;
        private System.Windows.Forms.PictureBox DonateImage;
        public System.Windows.Forms.CheckBox SpamFilterWindowButton;
        private System.Windows.Forms.Panel SpamFilterWindow;
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
        private System.Windows.Forms.DataGridViewTextBoxColumn Channel;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn Version;
        private System.Windows.Forms.DataGridViewTextBoxColumn Viewers;
        private System.Windows.Forms.DataGridViewTextBoxColumn Updated;
        private System.Windows.Forms.Label About_UsersLabel;
    }
}