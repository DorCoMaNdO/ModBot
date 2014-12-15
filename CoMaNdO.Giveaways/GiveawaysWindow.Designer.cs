using ModBot;
namespace CoMaNdO.Giveaways
{
    partial class GiveawaysWindow
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
            this.Giveaway_UserList = new System.Windows.Forms.ListBox();
            this.GiveawayUsersLabel = new System.Windows.Forms.Label();
            this.GiveawayUsersSpacer = new System.Windows.Forms.GroupBox();
            this.Giveaway_CustomKeyword = new System.Windows.Forms.TextBox();
            this.Giveaway_AnnounceWarnedEntries = new System.Windows.Forms.CheckBox();
            this.Giveaway_WarnFalseEntries = new System.Windows.Forms.CheckBox();
            this.Giveaway_MustWatch = new System.Windows.Forms.CheckBox();
            this.Giveaway_MustSubscribe = new System.Windows.Forms.CheckBox();
            this.Giveaway_CancelButton = new System.Windows.Forms.Button();
            this.Giveaway_CloseButton = new System.Windows.Forms.Button();
            this.Giveaway_OpenButton = new System.Windows.Forms.Button();
            this.GiveawayBansLabel = new System.Windows.Forms.Label();
            this.GiveawayBansSpacer = new System.Windows.Forms.GroupBox();
            this.GiveawaySettingsLabel = new System.Windows.Forms.Label();
            this.GiveawaySettingsSpacer = new System.Windows.Forms.GroupBox();
            this.Giveaway_TypeTickets = new System.Windows.Forms.RadioButton();
            this.Giveaway_TypeKeyword = new System.Windows.Forms.RadioButton();
            this.Giveaway_SettingsPresents = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.Giveaway_SettingsPresentsContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.Giveaway_WinTimeLabel = new System.Windows.Forms.Label();
            this.Giveaway_AddBanTextBox = new System.Windows.Forms.TextBox();
            this.Giveaway_MustFollow = new System.Windows.Forms.CheckBox();
            this.Giveaway_WinnerChat = new System.Windows.Forms.RichTextBox();
            this.Giveaway_WinnerTimerLabel = new System.Windows.Forms.Label();
            this.Giveaway_StartButton = new System.Windows.Forms.Button();
            this.Giveaway_CopyWinnerButton = new System.Windows.Forms.Button();
            this.Giveaway_RerollButton = new System.Windows.Forms.Button();
            this.Giveaway_StopButton = new System.Windows.Forms.Button();
            this.Giveaway_AutoBanWinner = new System.Windows.Forms.CheckBox();
            this.Giveaway_UnbanButton = new System.Windows.Forms.Button();
            this.Giveaway_BanButton = new System.Windows.Forms.Button();
            this.Giveaway_AnnounceWinnerButton = new System.Windows.Forms.Button();
            this.Giveaway_BanListListBox = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Giveaway_WinnerStatusLabel = new System.Windows.Forms.Label();
            this.Giveaway_WinnerLabel = new System.Windows.Forms.Label();
            this.GiveawayTypeLabel = new System.Windows.Forms.Label();
            this.GiveawayTypeSpacer = new System.Windows.Forms.GroupBox();
            this.Giveaway_MinCurrency = new System.Windows.Forms.CheckBox();
            this.Giveaway_TypeActive = new System.Windows.Forms.RadioButton();
            this.Giveaway_UserCount = new System.Windows.Forms.Label();
            this.Giveaway_SubscribersWinMultiplier = new System.Windows.Forms.CheckBox();
            this.Giveaway_WinnerTimer = new System.Windows.Forms.Timer(this.components);
            this.Giveaway_AnnounceFalseEntries = new System.Windows.Forms.CheckBox();
            this.Giveaway_SubscribersWinMultiplierAmount = new ModBot.FlatNumericUpDown();
            this.Giveaway_MustWatchMinutes = new ModBot.FlatNumericUpDown();
            this.Giveaway_MustWatchHours = new ModBot.FlatNumericUpDown();
            this.Giveaway_MaxTickets = new ModBot.FlatNumericUpDown();
            this.Giveaway_TicketCost = new ModBot.FlatNumericUpDown();
            this.Giveaway_ActiveUserTime = new ModBot.FlatNumericUpDown();
            this.Giveaway_MinCurrencyBox = new ModBot.FlatNumericUpDown();
            this.Giveaway_SettingsPresents.SuspendLayout();
            this.Giveaway_SettingsPresentsContextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Giveaway_SubscribersWinMultiplierAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Giveaway_MustWatchMinutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Giveaway_MustWatchHours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Giveaway_MaxTickets)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Giveaway_TicketCost)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Giveaway_ActiveUserTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Giveaway_MinCurrencyBox)).BeginInit();
            this.SuspendLayout();
            // 
            // Giveaway_UserList
            // 
            this.Giveaway_UserList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Giveaway_UserList.FormattingEnabled = true;
            this.Giveaway_UserList.Location = new System.Drawing.Point(814, 60);
            this.Giveaway_UserList.Name = "Giveaway_UserList";
            this.Giveaway_UserList.Size = new System.Drawing.Size(202, 496);
            this.Giveaway_UserList.Sorted = true;
            this.Giveaway_UserList.TabIndex = 70;
            // 
            // GiveawayUsersLabel
            // 
            this.GiveawayUsersLabel.AutoSize = true;
            this.GiveawayUsersLabel.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GiveawayUsersLabel.Location = new System.Drawing.Point(887, 27);
            this.GiveawayUsersLabel.Name = "GiveawayUsersLabel";
            this.GiveawayUsersLabel.Size = new System.Drawing.Size(40, 19);
            this.GiveawayUsersLabel.TabIndex = 68;
            this.GiveawayUsersLabel.Text = "Users";
            this.GiveawayUsersLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GiveawayUsersSpacer
            // 
            this.GiveawayUsersSpacer.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Bold);
            this.GiveawayUsersSpacer.Location = new System.Drawing.Point(813, 27);
            this.GiveawayUsersSpacer.Name = "GiveawayUsersSpacer";
            this.GiveawayUsersSpacer.Size = new System.Drawing.Size(212, 11);
            this.GiveawayUsersSpacer.TabIndex = 69;
            this.GiveawayUsersSpacer.TabStop = false;
            // 
            // Giveaway_CustomKeyword
            // 
            this.Giveaway_CustomKeyword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Giveaway_CustomKeyword.Enabled = false;
            this.Giveaway_CustomKeyword.Location = new System.Drawing.Point(124, 76);
            this.Giveaway_CustomKeyword.MaxLength = 32;
            this.Giveaway_CustomKeyword.Name = "Giveaway_CustomKeyword";
            this.Giveaway_CustomKeyword.Size = new System.Drawing.Size(140, 20);
            this.Giveaway_CustomKeyword.TabIndex = 67;
            this.Giveaway_CustomKeyword.TextChanged += new System.EventHandler(this.Giveaway_Settings_Changed);
            // 
            // Giveaway_AnnounceWarnedEntries
            // 
            this.Giveaway_AnnounceWarnedEntries.AutoSize = true;
            this.Giveaway_AnnounceWarnedEntries.Enabled = false;
            this.Giveaway_AnnounceWarnedEntries.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Giveaway_AnnounceWarnedEntries.Location = new System.Drawing.Point(306, 290);
            this.Giveaway_AnnounceWarnedEntries.Name = "Giveaway_AnnounceWarnedEntries";
            this.Giveaway_AnnounceWarnedEntries.Size = new System.Drawing.Size(115, 17);
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
            this.Giveaway_WarnFalseEntries.Location = new System.Drawing.Point(137, 290);
            this.Giveaway_WarnFalseEntries.Name = "Giveaway_WarnFalseEntries";
            this.Giveaway_WarnFalseEntries.Size = new System.Drawing.Size(171, 17);
            this.Giveaway_WarnFalseEntries.TabIndex = 65;
            this.Giveaway_WarnFalseEntries.Text = "Warn and timeout false entries";
            this.Giveaway_WarnFalseEntries.UseVisualStyleBackColor = true;
            this.Giveaway_WarnFalseEntries.CheckedChanged += new System.EventHandler(this.Giveaway_Settings_Changed);
            // 
            // Giveaway_MustWatch
            // 
            this.Giveaway_MustWatch.AutoSize = true;
            this.Giveaway_MustWatch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Giveaway_MustWatch.Location = new System.Drawing.Point(6, 244);
            this.Giveaway_MustWatch.Name = "Giveaway_MustWatch";
            this.Giveaway_MustWatch.Size = new System.Drawing.Size(377, 17);
            this.Giveaway_MustWatch.TabIndex = 61;
            this.Giveaway_MustWatch.Text = "Has watched the stream for at least                   hours and             minut" +
    "es";
            this.Giveaway_MustWatch.UseVisualStyleBackColor = true;
            this.Giveaway_MustWatch.CheckedChanged += new System.EventHandler(this.Giveaway_Settings_Changed);
            // 
            // Giveaway_MustSubscribe
            // 
            this.Giveaway_MustSubscribe.AutoSize = true;
            this.Giveaway_MustSubscribe.Enabled = false;
            this.Giveaway_MustSubscribe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Giveaway_MustSubscribe.Location = new System.Drawing.Point(6, 175);
            this.Giveaway_MustSubscribe.Name = "Giveaway_MustSubscribe";
            this.Giveaway_MustSubscribe.Size = new System.Drawing.Size(122, 17);
            this.Giveaway_MustSubscribe.TabIndex = 60;
            this.Giveaway_MustSubscribe.Text = "Must be a subscriber";
            this.Giveaway_MustSubscribe.UseVisualStyleBackColor = true;
            this.Giveaway_MustSubscribe.CheckedChanged += new System.EventHandler(this.Giveaway_Settings_Changed);
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
            this.GiveawayBansLabel.Location = new System.Drawing.Point(381, 27);
            this.GiveawayBansLabel.Name = "GiveawayBansLabel";
            this.GiveawayBansLabel.Size = new System.Drawing.Size(35, 19);
            this.GiveawayBansLabel.TabIndex = 53;
            this.GiveawayBansLabel.Text = "Bans";
            this.GiveawayBansLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GiveawayBansSpacer
            // 
            this.GiveawayBansSpacer.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Bold);
            this.GiveawayBansSpacer.Location = new System.Drawing.Point(318, 27);
            this.GiveawayBansSpacer.Name = "GiveawayBansSpacer";
            this.GiveawayBansSpacer.Size = new System.Drawing.Size(496, 11);
            this.GiveawayBansSpacer.TabIndex = 54;
            this.GiveawayBansSpacer.TabStop = false;
            // 
            // GiveawaySettingsLabel
            // 
            this.GiveawaySettingsLabel.AutoSize = true;
            this.GiveawaySettingsLabel.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GiveawaySettingsLabel.Location = new System.Drawing.Point(58, 126);
            this.GiveawaySettingsLabel.Name = "GiveawaySettingsLabel";
            this.GiveawaySettingsLabel.Size = new System.Drawing.Size(53, 19);
            this.GiveawaySettingsLabel.TabIndex = 51;
            this.GiveawaySettingsLabel.Text = "Settings";
            this.GiveawaySettingsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GiveawaySettingsSpacer
            // 
            this.GiveawaySettingsSpacer.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Bold);
            this.GiveawaySettingsSpacer.Location = new System.Drawing.Point(-1, 126);
            this.GiveawaySettingsSpacer.Name = "GiveawaySettingsSpacer";
            this.GiveawaySettingsSpacer.Size = new System.Drawing.Size(320, 11);
            this.GiveawaySettingsSpacer.TabIndex = 52;
            this.GiveawaySettingsSpacer.TabStop = false;
            // 
            // Giveaway_TypeTickets
            // 
            this.Giveaway_TypeTickets.AutoSize = true;
            this.Giveaway_TypeTickets.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Giveaway_TypeTickets.Location = new System.Drawing.Point(6, 99);
            this.Giveaway_TypeTickets.Name = "Giveaway_TypeTickets";
            this.Giveaway_TypeTickets.Size = new System.Drawing.Size(254, 17);
            this.Giveaway_TypeTickets.TabIndex = 50;
            this.Giveaway_TypeTickets.Text = "Tickets          Cost:                       Max:                ";
            this.Giveaway_TypeTickets.UseVisualStyleBackColor = true;
            this.Giveaway_TypeTickets.CheckedChanged += new System.EventHandler(this.Giveaway_Settings_Changed);
            // 
            // Giveaway_TypeKeyword
            // 
            this.Giveaway_TypeKeyword.AutoSize = true;
            this.Giveaway_TypeKeyword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Giveaway_TypeKeyword.Location = new System.Drawing.Point(6, 76);
            this.Giveaway_TypeKeyword.Name = "Giveaway_TypeKeyword";
            this.Giveaway_TypeKeyword.Size = new System.Drawing.Size(256, 17);
            this.Giveaway_TypeKeyword.TabIndex = 49;
            this.Giveaway_TypeKeyword.Text = "Keyword     Custom:                                             ";
            this.Giveaway_TypeKeyword.UseVisualStyleBackColor = true;
            this.Giveaway_TypeKeyword.CheckedChanged += new System.EventHandler(this.Giveaway_Settings_Changed);
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
            this.Giveaway_SettingsPresents.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Giveaway_SettingsPresents_MouseClick);
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
            // Giveaway_SettingsPresentsContextMenu
            // 
            this.Giveaway_SettingsPresentsContextMenu.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Giveaway_SettingsPresentsContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.removeToolStripMenuItem,
            this.toolStripSeparator1,
            this.toolStripTextBox1});
            this.Giveaway_SettingsPresentsContextMenu.Name = "Giveaway_SettingsPresentsContextMenu";
            this.Giveaway_SettingsPresentsContextMenu.ShowImageMargin = false;
            this.Giveaway_SettingsPresentsContextMenu.Size = new System.Drawing.Size(136, 79);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.addToolStripMenuItem.Text = "Add";
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Enabled = false;
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(132, 6);
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(100, 23);
            this.toolStripTextBox1.Text = "Default";
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
            // Giveaway_AddBanTextBox
            // 
            this.Giveaway_AddBanTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Giveaway_AddBanTextBox.Location = new System.Drawing.Point(324, 146);
            this.Giveaway_AddBanTextBox.Name = "Giveaway_AddBanTextBox";
            this.Giveaway_AddBanTextBox.Size = new System.Drawing.Size(484, 20);
            this.Giveaway_AddBanTextBox.TabIndex = 33;
            this.Giveaway_AddBanTextBox.TextChanged += new System.EventHandler(this.Giveaway_AddBanTextBox_TextChanged);
            // 
            // Giveaway_MustFollow
            // 
            this.Giveaway_MustFollow.AutoSize = true;
            this.Giveaway_MustFollow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Giveaway_MustFollow.Location = new System.Drawing.Point(6, 152);
            this.Giveaway_MustFollow.Name = "Giveaway_MustFollow";
            this.Giveaway_MustFollow.Size = new System.Drawing.Size(111, 17);
            this.Giveaway_MustFollow.TabIndex = 14;
            this.Giveaway_MustFollow.Text = "Must be a follower";
            this.Giveaway_MustFollow.UseVisualStyleBackColor = true;
            this.Giveaway_MustFollow.CheckedChanged += new System.EventHandler(this.Giveaway_Settings_Changed);
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
            // Giveaway_AutoBanWinner
            // 
            this.Giveaway_AutoBanWinner.AutoSize = true;
            this.Giveaway_AutoBanWinner.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Giveaway_AutoBanWinner.Location = new System.Drawing.Point(6, 267);
            this.Giveaway_AutoBanWinner.Name = "Giveaway_AutoBanWinner";
            this.Giveaway_AutoBanWinner.Size = new System.Drawing.Size(143, 17);
            this.Giveaway_AutoBanWinner.TabIndex = 34;
            this.Giveaway_AutoBanWinner.Text = "Automatically ban winner";
            this.Giveaway_AutoBanWinner.UseVisualStyleBackColor = true;
            this.Giveaway_AutoBanWinner.CheckedChanged += new System.EventHandler(this.Giveaway_Settings_Changed);
            // 
            // Giveaway_UnbanButton
            // 
            this.Giveaway_UnbanButton.BackColor = System.Drawing.Color.White;
            this.Giveaway_UnbanButton.Enabled = false;
            this.Giveaway_UnbanButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Giveaway_UnbanButton.Location = new System.Drawing.Point(569, 172);
            this.Giveaway_UnbanButton.Name = "Giveaway_UnbanButton";
            this.Giveaway_UnbanButton.Size = new System.Drawing.Size(239, 23);
            this.Giveaway_UnbanButton.TabIndex = 32;
            this.Giveaway_UnbanButton.Text = "Unban";
            this.Giveaway_UnbanButton.UseVisualStyleBackColor = false;
            this.Giveaway_UnbanButton.Click += new System.EventHandler(this.Giveaway_Settings_Changed);
            // 
            // Giveaway_BanButton
            // 
            this.Giveaway_BanButton.BackColor = System.Drawing.Color.White;
            this.Giveaway_BanButton.Enabled = false;
            this.Giveaway_BanButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Giveaway_BanButton.Location = new System.Drawing.Point(324, 172);
            this.Giveaway_BanButton.Name = "Giveaway_BanButton";
            this.Giveaway_BanButton.Size = new System.Drawing.Size(239, 23);
            this.Giveaway_BanButton.TabIndex = 31;
            this.Giveaway_BanButton.Text = "Ban";
            this.Giveaway_BanButton.UseVisualStyleBackColor = false;
            this.Giveaway_BanButton.Click += new System.EventHandler(this.Giveaway_Settings_Changed);
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
            this.Giveaway_BanListListBox.Location = new System.Drawing.Point(324, 48);
            this.Giveaway_BanListListBox.Name = "Giveaway_BanListListBox";
            this.Giveaway_BanListListBox.Size = new System.Drawing.Size(484, 93);
            this.Giveaway_BanListListBox.TabIndex = 30;
            this.Giveaway_BanListListBox.SelectedIndexChanged += new System.EventHandler(this.Giveaway_BanListListBox_SelectedIndexChanged);
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
            this.Giveaway_WinnerLabel.Location = new System.Drawing.Point(6, 479);
            this.Giveaway_WinnerLabel.Name = "Giveaway_WinnerLabel";
            this.Giveaway_WinnerLabel.Size = new System.Drawing.Size(802, 24);
            this.Giveaway_WinnerLabel.TabIndex = 26;
            this.Giveaway_WinnerLabel.Text = "CoMaNdO ;)";
            this.Giveaway_WinnerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GiveawayTypeLabel
            // 
            this.GiveawayTypeLabel.AutoSize = true;
            this.GiveawayTypeLabel.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GiveawayTypeLabel.Location = new System.Drawing.Point(58, 27);
            this.GiveawayTypeLabel.Name = "GiveawayTypeLabel";
            this.GiveawayTypeLabel.Size = new System.Drawing.Size(35, 19);
            this.GiveawayTypeLabel.TabIndex = 13;
            this.GiveawayTypeLabel.Text = "Type";
            this.GiveawayTypeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GiveawayTypeSpacer
            // 
            this.GiveawayTypeSpacer.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Bold);
            this.GiveawayTypeSpacer.Location = new System.Drawing.Point(-1, 27);
            this.GiveawayTypeSpacer.Name = "GiveawayTypeSpacer";
            this.GiveawayTypeSpacer.Size = new System.Drawing.Size(320, 11);
            this.GiveawayTypeSpacer.TabIndex = 47;
            this.GiveawayTypeSpacer.TabStop = false;
            // 
            // Giveaway_MinCurrency
            // 
            this.Giveaway_MinCurrency.AutoSize = true;
            this.Giveaway_MinCurrency.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Giveaway_MinCurrency.Location = new System.Drawing.Point(6, 221);
            this.Giveaway_MinCurrency.Name = "Giveaway_MinCurrency";
            this.Giveaway_MinCurrency.Size = new System.Drawing.Size(235, 17);
            this.Giveaway_MinCurrency.TabIndex = 20;
            this.Giveaway_MinCurrency.Text = "Must have at least                       CURRENCY";
            this.Giveaway_MinCurrency.UseVisualStyleBackColor = true;
            this.Giveaway_MinCurrency.CheckedChanged += new System.EventHandler(this.Giveaway_Settings_Changed);
            // 
            // Giveaway_TypeActive
            // 
            this.Giveaway_TypeActive.AutoSize = true;
            this.Giveaway_TypeActive.Checked = true;
            this.Giveaway_TypeActive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Giveaway_TypeActive.Location = new System.Drawing.Point(6, 53);
            this.Giveaway_TypeActive.Name = "Giveaway_TypeActive";
            this.Giveaway_TypeActive.Size = new System.Drawing.Size(225, 17);
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
            this.Giveaway_UserCount.Size = new System.Drawing.Size(49, 13);
            this.Giveaway_UserCount.TabIndex = 71;
            this.Giveaway_UserCount.Text = "Count: 0";
            // 
            // Giveaway_SubscribersWinMultiplier
            // 
            this.Giveaway_SubscribersWinMultiplier.AutoSize = true;
            this.Giveaway_SubscribersWinMultiplier.Enabled = false;
            this.Giveaway_SubscribersWinMultiplier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Giveaway_SubscribersWinMultiplier.Location = new System.Drawing.Point(6, 198);
            this.Giveaway_SubscribersWinMultiplier.Name = "Giveaway_SubscribersWinMultiplier";
            this.Giveaway_SubscribersWinMultiplier.Size = new System.Drawing.Size(175, 17);
            this.Giveaway_SubscribersWinMultiplier.TabIndex = 72;
            this.Giveaway_SubscribersWinMultiplier.Text = "Subscribers win multiplier           ";
            this.Giveaway_SubscribersWinMultiplier.UseVisualStyleBackColor = true;
            this.Giveaway_SubscribersWinMultiplier.CheckedChanged += new System.EventHandler(this.Giveaway_Settings_Changed);
            // 
            // Giveaway_WinnerTimer
            // 
            this.Giveaway_WinnerTimer.Enabled = true;
            this.Giveaway_WinnerTimer.Tick += new System.EventHandler(this.Giveaway_WinnerTimer_Tick);
            // 
            // Giveaway_AnnounceFalseEntries
            // 
            this.Giveaway_AnnounceFalseEntries.AutoSize = true;
            this.Giveaway_AnnounceFalseEntries.Enabled = false;
            this.Giveaway_AnnounceFalseEntries.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Giveaway_AnnounceFalseEntries.Location = new System.Drawing.Point(6, 290);
            this.Giveaway_AnnounceFalseEntries.Name = "Giveaway_AnnounceFalseEntries";
            this.Giveaway_AnnounceFalseEntries.Size = new System.Drawing.Size(133, 17);
            this.Giveaway_AnnounceFalseEntries.TabIndex = 74;
            this.Giveaway_AnnounceFalseEntries.Text = "Announce false entries";
            this.Giveaway_AnnounceFalseEntries.UseVisualStyleBackColor = true;
            this.Giveaway_AnnounceFalseEntries.CheckedChanged += new System.EventHandler(this.Giveaway_Settings_Changed);
            // 
            // Giveaway_SubscribersWinMultiplierAmount
            // 
            this.Giveaway_SubscribersWinMultiplierAmount.Enabled = false;
            this.Giveaway_SubscribersWinMultiplierAmount.Location = new System.Drawing.Point(144, 198);
            this.Giveaway_SubscribersWinMultiplierAmount.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.Giveaway_SubscribersWinMultiplierAmount.Name = "Giveaway_SubscribersWinMultiplierAmount";
            this.Giveaway_SubscribersWinMultiplierAmount.Size = new System.Drawing.Size(38, 20);
            this.Giveaway_SubscribersWinMultiplierAmount.TabIndex = 73;
            this.Giveaway_SubscribersWinMultiplierAmount.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.Giveaway_SubscribersWinMultiplierAmount.ValueChanged += new System.EventHandler(this.Giveaway_Settings_Changed);
            // 
            // Giveaway_MustWatchMinutes
            // 
            this.Giveaway_MustWatchMinutes.Enabled = false;
            this.Giveaway_MustWatchMinutes.Location = new System.Drawing.Point(304, 244);
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
            this.Giveaway_MustWatchHours.Location = new System.Drawing.Point(200, 244);
            this.Giveaway_MustWatchHours.Maximum = new decimal(new int[] {
            24000,
            0,
            0,
            0});
            this.Giveaway_MustWatchHours.Name = "Giveaway_MustWatchHours";
            this.Giveaway_MustWatchHours.Size = new System.Drawing.Size(49, 20);
            this.Giveaway_MustWatchHours.TabIndex = 63;
            this.Giveaway_MustWatchHours.ValueChanged += new System.EventHandler(this.Giveaway_Settings_Changed);
            // 
            // Giveaway_MaxTickets
            // 
            this.Giveaway_MaxTickets.Enabled = false;
            this.Giveaway_MaxTickets.Location = new System.Drawing.Point(208, 99);
            this.Giveaway_MaxTickets.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.Giveaway_MaxTickets.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Giveaway_MaxTickets.Name = "Giveaway_MaxTickets";
            this.Giveaway_MaxTickets.Size = new System.Drawing.Size(56, 20);
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
            this.Giveaway_TicketCost.Location = new System.Drawing.Point(115, 99);
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
            // Giveaway_ActiveUserTime
            // 
            this.Giveaway_ActiveUserTime.Location = new System.Drawing.Point(125, 53);
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
            // Giveaway_MinCurrencyBox
            // 
            this.Giveaway_MinCurrencyBox.Enabled = false;
            this.Giveaway_MinCurrencyBox.Location = new System.Drawing.Point(115, 221);
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
            // GiveawaysWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1024, 562);
            this.Controls.Add(this.Giveaway_SubscribersWinMultiplierAmount);
            this.Controls.Add(this.Giveaway_UserList);
            this.Controls.Add(this.Giveaway_SettingsPresents);
            this.Controls.Add(this.GiveawayUsersLabel);
            this.Controls.Add(this.Giveaway_UserCount);
            this.Controls.Add(this.Giveaway_CustomKeyword);
            this.Controls.Add(this.Giveaway_AnnounceWarnedEntries);
            this.Controls.Add(this.Giveaway_MustWatchMinutes);
            this.Controls.Add(this.GiveawayTypeLabel);
            this.Controls.Add(this.Giveaway_MustWatchHours);
            this.Controls.Add(this.Giveaway_BanListListBox);
            this.Controls.Add(this.Giveaway_MaxTickets);
            this.Controls.Add(this.Giveaway_AnnounceWinnerButton);
            this.Controls.Add(this.Giveaway_TicketCost);
            this.Controls.Add(this.Giveaway_BanButton);
            this.Controls.Add(this.Giveaway_CancelButton);
            this.Controls.Add(this.Giveaway_ActiveUserTime);
            this.Controls.Add(this.Giveaway_CloseButton);
            this.Controls.Add(this.Giveaway_UnbanButton);
            this.Controls.Add(this.Giveaway_OpenButton);
            this.Controls.Add(this.GiveawayBansLabel);
            this.Controls.Add(this.Giveaway_StopButton);
            this.Controls.Add(this.Giveaway_RerollButton);
            this.Controls.Add(this.GiveawaySettingsLabel);
            this.Controls.Add(this.Giveaway_CopyWinnerButton);
            this.Controls.Add(this.GiveawaySettingsSpacer);
            this.Controls.Add(this.Giveaway_StartButton);
            this.Controls.Add(this.Giveaway_WinnerChat);
            this.Controls.Add(this.Giveaway_MinCurrencyBox);
            this.Controls.Add(this.Giveaway_AddBanTextBox);
            this.Controls.Add(this.Giveaway_WinTimeLabel);
            this.Controls.Add(this.Giveaway_SubscribersWinMultiplier);
            this.Controls.Add(this.Giveaway_WarnFalseEntries);
            this.Controls.Add(this.Giveaway_MustWatch);
            this.Controls.Add(this.Giveaway_MustSubscribe);
            this.Controls.Add(this.Giveaway_AutoBanWinner);
            this.Controls.Add(this.Giveaway_MustFollow);
            this.Controls.Add(this.Giveaway_MinCurrency);
            this.Controls.Add(this.Giveaway_TypeActive);
            this.Controls.Add(this.Giveaway_TypeTickets);
            this.Controls.Add(this.Giveaway_TypeKeyword);
            this.Controls.Add(this.GiveawayTypeSpacer);
            this.Controls.Add(this.GiveawayBansSpacer);
            this.Controls.Add(this.GiveawayUsersSpacer);
            this.Controls.Add(this.Giveaway_WinnerTimerLabel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Giveaway_WinnerStatusLabel);
            this.Controls.Add(this.Giveaway_AnnounceFalseEntries);
            this.Controls.Add(this.Giveaway_WinnerLabel);
            this.Font = new System.Drawing.Font("Tahoma", 8F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "GiveawaysWindow";
            this.Text = "Example Window";
            this.Giveaway_SettingsPresents.ResumeLayout(false);
            this.Giveaway_SettingsPresentsContextMenu.ResumeLayout(false);
            this.Giveaway_SettingsPresentsContextMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Giveaway_SubscribersWinMultiplierAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Giveaway_MustWatchMinutes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Giveaway_MustWatchHours)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Giveaway_MaxTickets)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Giveaway_TicketCost)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Giveaway_ActiveUserTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Giveaway_MinCurrencyBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public FlatNumericUpDown Giveaway_SubscribersWinMultiplierAmount;
        public System.Windows.Forms.ListBox Giveaway_UserList;
        public System.Windows.Forms.TextBox Giveaway_CustomKeyword;
        public System.Windows.Forms.CheckBox Giveaway_AnnounceWarnedEntries;
        public System.Windows.Forms.CheckBox Giveaway_WarnFalseEntries;
        public FlatNumericUpDown Giveaway_MustWatchMinutes;
        public FlatNumericUpDown Giveaway_MustWatchHours;
        public System.Windows.Forms.CheckBox Giveaway_MustWatch;
        public System.Windows.Forms.CheckBox Giveaway_MustSubscribe;
        public FlatNumericUpDown Giveaway_MaxTickets;
        public FlatNumericUpDown Giveaway_TicketCost;
        public System.Windows.Forms.Button Giveaway_CancelButton;
        public System.Windows.Forms.Button Giveaway_CloseButton;
        public System.Windows.Forms.Button Giveaway_OpenButton;
        public System.Windows.Forms.RadioButton Giveaway_TypeTickets;
        public System.Windows.Forms.RadioButton Giveaway_TypeKeyword;
        public FlatNumericUpDown Giveaway_MinCurrencyBox;
        public System.Windows.Forms.TabControl Giveaway_SettingsPresents;
        private System.Windows.Forms.TabPage tabPage1;
        public System.Windows.Forms.Label Giveaway_WinTimeLabel;
        public System.Windows.Forms.TextBox Giveaway_AddBanTextBox;
        public System.Windows.Forms.CheckBox Giveaway_MustFollow;
        public System.Windows.Forms.RichTextBox Giveaway_WinnerChat;
        public System.Windows.Forms.Label Giveaway_WinnerTimerLabel;
        public System.Windows.Forms.Button Giveaway_StartButton;
        public System.Windows.Forms.Button Giveaway_CopyWinnerButton;
        public System.Windows.Forms.Button Giveaway_RerollButton;
        public System.Windows.Forms.Button Giveaway_StopButton;
        public System.Windows.Forms.CheckBox Giveaway_AutoBanWinner;
        public System.Windows.Forms.Button Giveaway_UnbanButton;
        public FlatNumericUpDown Giveaway_ActiveUserTime;
        public System.Windows.Forms.Button Giveaway_BanButton;
        public System.Windows.Forms.Button Giveaway_AnnounceWinnerButton;
        public System.Windows.Forms.ListBox Giveaway_BanListListBox;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.Label Giveaway_WinnerStatusLabel;
        public System.Windows.Forms.Label Giveaway_WinnerLabel;
        public System.Windows.Forms.CheckBox Giveaway_MinCurrency;
        public System.Windows.Forms.RadioButton Giveaway_TypeActive;
        public System.Windows.Forms.Label Giveaway_UserCount;
        public System.Windows.Forms.CheckBox Giveaway_SubscribersWinMultiplier;
        public System.Windows.Forms.Timer Giveaway_WinnerTimer;
        public System.Windows.Forms.Label GiveawayUsersLabel;
        public System.Windows.Forms.GroupBox GiveawayUsersSpacer;
        public System.Windows.Forms.Label GiveawayBansLabel;
        public System.Windows.Forms.GroupBox GiveawayBansSpacer;
        public System.Windows.Forms.Label GiveawaySettingsLabel;
        public System.Windows.Forms.GroupBox GiveawaySettingsSpacer;
        public System.Windows.Forms.Label GiveawayTypeLabel;
        public System.Windows.Forms.GroupBox GiveawayTypeSpacer;
        public System.Windows.Forms.CheckBox Giveaway_AnnounceFalseEntries;
        public System.Windows.Forms.ContextMenuStrip Giveaway_SettingsPresentsContextMenu;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;



    }
}