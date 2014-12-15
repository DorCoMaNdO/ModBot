namespace CoMaNdO.SongRequests
{
    partial class SongRequestsWindow
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
            this.SongRequestPlayer = new System.Windows.Forms.WebBrowser();
            this.RequestingRulesLabel = new System.Windows.Forms.Label();
            this.RequestingRulesSpacer = new System.Windows.Forms.GroupBox();
            this.RequestPrice = new ModBot.FlatNumericUpDown();
            this.ChargeRequest = new System.Windows.Forms.CheckBox();
            this.RequestsLimit = new ModBot.FlatNumericUpDown();
            this.LimitRequests = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.RequestPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RequestsLimit)).BeginInit();
            this.SuspendLayout();
            // 
            // SongRequestPlayer
            // 
            this.SongRequestPlayer.Location = new System.Drawing.Point(692, 12);
            this.SongRequestPlayer.MinimumSize = new System.Drawing.Size(20, 20);
            this.SongRequestPlayer.Name = "SongRequestPlayer";
            this.SongRequestPlayer.ScrollBarsEnabled = false;
            this.SongRequestPlayer.Size = new System.Drawing.Size(320, 240);
            this.SongRequestPlayer.TabIndex = 70;
            this.SongRequestPlayer.Url = new System.Uri("", System.UriKind.Relative);
            this.SongRequestPlayer.WebBrowserShortcutsEnabled = false;
            // 
            // RequestingRulesLabel
            // 
            this.RequestingRulesLabel.AutoSize = true;
            this.RequestingRulesLabel.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RequestingRulesLabel.Location = new System.Drawing.Point(58, 0);
            this.RequestingRulesLabel.Name = "RequestingRulesLabel";
            this.RequestingRulesLabel.Size = new System.Drawing.Size(99, 19);
            this.RequestingRulesLabel.TabIndex = 73;
            this.RequestingRulesLabel.Text = "Requesting rules";
            this.RequestingRulesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RequestingRulesSpacer
            // 
            this.RequestingRulesSpacer.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Bold);
            this.RequestingRulesSpacer.Location = new System.Drawing.Point(-1, 0);
            this.RequestingRulesSpacer.Name = "RequestingRulesSpacer";
            this.RequestingRulesSpacer.Size = new System.Drawing.Size(320, 11);
            this.RequestingRulesSpacer.TabIndex = 74;
            this.RequestingRulesSpacer.TabStop = false;
            // 
            // RequestPrice
            // 
            this.RequestPrice.Location = new System.Drawing.Point(108, 20);
            this.RequestPrice.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.RequestPrice.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.RequestPrice.Name = "RequestPrice";
            this.RequestPrice.Size = new System.Drawing.Size(62, 20);
            this.RequestPrice.TabIndex = 72;
            this.RequestPrice.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.RequestPrice.ValueChanged += new System.EventHandler(this.Settings_Changed);
            // 
            // ChargeRequest
            // 
            this.ChargeRequest.AutoSize = true;
            this.ChargeRequest.Checked = true;
            this.ChargeRequest.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChargeRequest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ChargeRequest.Location = new System.Drawing.Point(6, 20);
            this.ChargeRequest.Name = "ChargeRequest";
            this.ChargeRequest.Size = new System.Drawing.Size(228, 17);
            this.ChargeRequest.TabIndex = 71;
            this.ChargeRequest.Text = "Requesting costs                       CURRENCY";
            this.ChargeRequest.UseVisualStyleBackColor = true;
            this.ChargeRequest.CheckedChanged += new System.EventHandler(this.Settings_Changed);
            // 
            // RequestsLimit
            // 
            this.RequestsLimit.Location = new System.Drawing.Point(140, 43);
            this.RequestsLimit.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.RequestsLimit.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.RequestsLimit.Name = "RequestsLimit";
            this.RequestsLimit.Size = new System.Drawing.Size(32, 20);
            this.RequestsLimit.TabIndex = 76;
            this.RequestsLimit.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.RequestsLimit.ValueChanged += new System.EventHandler(this.Settings_Changed);
            // 
            // LimitRequests
            // 
            this.LimitRequests.AutoSize = true;
            this.LimitRequests.Checked = true;
            this.LimitRequests.CheckState = System.Windows.Forms.CheckState.Checked;
            this.LimitRequests.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LimitRequests.Location = new System.Drawing.Point(6, 43);
            this.LimitRequests.Name = "LimitRequests";
            this.LimitRequests.Size = new System.Drawing.Size(204, 17);
            this.LimitRequests.TabIndex = 75;
            this.LimitRequests.Text = "A user can request only             songs";
            this.LimitRequests.UseVisualStyleBackColor = true;
            this.LimitRequests.CheckedChanged += new System.EventHandler(this.Settings_Changed);
            // 
            // SongRequestsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1024, 562);
            this.Controls.Add(this.RequestsLimit);
            this.Controls.Add(this.LimitRequests);
            this.Controls.Add(this.RequestingRulesLabel);
            this.Controls.Add(this.RequestingRulesSpacer);
            this.Controls.Add(this.RequestPrice);
            this.Controls.Add(this.ChargeRequest);
            this.Controls.Add(this.SongRequestPlayer);
            this.Font = new System.Drawing.Font("Tahoma", 8F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SongRequestsWindow";
            this.Text = "Song Requests Window";
            this.Load += new System.EventHandler(this.SongRequestsWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.RequestPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RequestsLimit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.WebBrowser SongRequestPlayer;
        public System.Windows.Forms.Label RequestingRulesLabel;
        public System.Windows.Forms.GroupBox RequestingRulesSpacer;
        public ModBot.FlatNumericUpDown RequestPrice;
        public System.Windows.Forms.CheckBox ChargeRequest;
        public ModBot.FlatNumericUpDown RequestsLimit;
        public System.Windows.Forms.CheckBox LimitRequests;
    }
}