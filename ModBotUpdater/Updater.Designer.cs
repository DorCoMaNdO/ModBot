namespace ModBotUpdater
{
    partial class Updater
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
            this.DownloadProgressBar = new ModBotUpdater.CustomProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.CheckUpdatesButton = new System.Windows.Forms.Button();
            this.UpdateButton = new System.Windows.Forms.Button();
            this.CurrentVersionLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LatestVersionLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.StateLabel = new System.Windows.Forms.Label();
            this.ChangelogButton = new System.Windows.Forms.Button();
            this.BetaUpdates = new System.Windows.Forms.CheckBox();
            this.DevUpdates = new System.Windows.Forms.CheckBox();
            this.ExtensionsButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // DownloadProgressBar
            // 
            this.DownloadProgressBar.Location = new System.Drawing.Point(14, 108);
            this.DownloadProgressBar.Name = "DownloadProgressBar";
            this.DownloadProgressBar.Size = new System.Drawing.Size(504, 23);
            this.DownloadProgressBar.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 19);
            this.label1.TabIndex = 13;
            this.label1.Text = "Current ModBot Version: ";
            // 
            // CheckUpdatesButton
            // 
            this.CheckUpdatesButton.BackColor = System.Drawing.Color.White;
            this.CheckUpdatesButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CheckUpdatesButton.Location = new System.Drawing.Point(141, 137);
            this.CheckUpdatesButton.Name = "CheckUpdatesButton";
            this.CheckUpdatesButton.Size = new System.Drawing.Size(122, 23);
            this.CheckUpdatesButton.TabIndex = 46;
            this.CheckUpdatesButton.Text = "Check for Updates";
            this.CheckUpdatesButton.UseVisualStyleBackColor = false;
            this.CheckUpdatesButton.Click += new System.EventHandler(this.CheckUpdatesButton_Click);
            // 
            // UpdateButton
            // 
            this.UpdateButton.BackColor = System.Drawing.Color.White;
            this.UpdateButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UpdateButton.Location = new System.Drawing.Point(396, 137);
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(122, 23);
            this.UpdateButton.TabIndex = 47;
            this.UpdateButton.Text = "Update ModBot";
            this.UpdateButton.UseVisualStyleBackColor = false;
            this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // CurrentVersionLabel
            // 
            this.CurrentVersionLabel.AutoSize = true;
            this.CurrentVersionLabel.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrentVersionLabel.ForeColor = System.Drawing.Color.Red;
            this.CurrentVersionLabel.Location = new System.Drawing.Point(154, 32);
            this.CurrentVersionLabel.Name = "CurrentVersionLabel";
            this.CurrentVersionLabel.Size = new System.Drawing.Size(67, 19);
            this.CurrentVersionLabel.TabIndex = 48;
            this.CurrentVersionLabel.Text = "Not Found";
            this.CurrentVersionLabel.TextChanged += new System.EventHandler(this.CurrentVersionLabel_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(10, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(141, 19);
            this.label2.TabIndex = 49;
            this.label2.Text = "Latest ModBot Version: ";
            // 
            // LatestVersionLabel
            // 
            this.LatestVersionLabel.AutoSize = true;
            this.LatestVersionLabel.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LatestVersionLabel.ForeColor = System.Drawing.Color.Red;
            this.LatestVersionLabel.Location = new System.Drawing.Point(143, 51);
            this.LatestVersionLabel.Name = "LatestVersionLabel";
            this.LatestVersionLabel.Size = new System.Drawing.Size(67, 19);
            this.LatestVersionLabel.TabIndex = 50;
            this.LatestVersionLabel.Text = "Not Found";
            this.LatestVersionLabel.TextChanged += new System.EventHandler(this.LatestVersionLabel_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(10, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 19);
            this.label3.TabIndex = 51;
            this.label3.Text = "State:";
            // 
            // StateLabel
            // 
            this.StateLabel.AutoSize = true;
            this.StateLabel.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StateLabel.Location = new System.Drawing.Point(47, 70);
            this.StateLabel.Name = "StateLabel";
            this.StateLabel.Size = new System.Drawing.Size(61, 19);
            this.StateLabel.TabIndex = 52;
            this.StateLabel.Text = "Unknown";
            this.StateLabel.TextChanged += new System.EventHandler(this.StateLabel_TextChanged);
            // 
            // ChangelogButton
            // 
            this.ChangelogButton.BackColor = System.Drawing.Color.White;
            this.ChangelogButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ChangelogButton.Location = new System.Drawing.Point(269, 137);
            this.ChangelogButton.Name = "ChangelogButton";
            this.ChangelogButton.Size = new System.Drawing.Size(121, 23);
            this.ChangelogButton.TabIndex = 53;
            this.ChangelogButton.Text = "Changelog";
            this.ChangelogButton.UseVisualStyleBackColor = false;
            this.ChangelogButton.Click += new System.EventHandler(this.ChangelogButton_Click);
            // 
            // BetaUpdates
            // 
            this.BetaUpdates.AutoSize = true;
            this.BetaUpdates.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BetaUpdates.Location = new System.Drawing.Point(384, 35);
            this.BetaUpdates.Name = "BetaUpdates";
            this.BetaUpdates.Size = new System.Drawing.Size(134, 17);
            this.BetaUpdates.TabIndex = 96;
            this.BetaUpdates.Text = "Check for beta updates";
            this.BetaUpdates.UseVisualStyleBackColor = true;
            this.BetaUpdates.CheckedChanged += new System.EventHandler(this.UpdateChecks_CheckedChanged);
            // 
            // DevUpdates
            // 
            this.DevUpdates.AutoSize = true;
            this.DevUpdates.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DevUpdates.Location = new System.Drawing.Point(384, 58);
            this.DevUpdates.Name = "DevUpdates";
            this.DevUpdates.Size = new System.Drawing.Size(131, 17);
            this.DevUpdates.TabIndex = 97;
            this.DevUpdates.Text = "Check for dev updates";
            this.DevUpdates.UseVisualStyleBackColor = true;
            this.DevUpdates.CheckedChanged += new System.EventHandler(this.UpdateChecks_CheckedChanged);
            // 
            // ExtensionsButton
            // 
            this.ExtensionsButton.BackColor = System.Drawing.Color.White;
            this.ExtensionsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExtensionsButton.Location = new System.Drawing.Point(14, 137);
            this.ExtensionsButton.Name = "ExtensionsButton";
            this.ExtensionsButton.Size = new System.Drawing.Size(121, 23);
            this.ExtensionsButton.TabIndex = 98;
            this.ExtensionsButton.Text = "Extensions";
            this.ExtensionsButton.UseVisualStyleBackColor = false;
            this.ExtensionsButton.Click += new System.EventHandler(this.ExtensionsButton_Click);
            // 
            // Updater
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(532, 174);
            this.Controls.Add(this.ExtensionsButton);
            this.Controls.Add(this.DevUpdates);
            this.Controls.Add(this.BetaUpdates);
            this.Controls.Add(this.ChangelogButton);
            this.Controls.Add(this.StateLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.LatestVersionLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CurrentVersionLabel);
            this.Controls.Add(this.UpdateButton);
            this.Controls.Add(this.CheckUpdatesButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DownloadProgressBar);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Updater";
            this.Text = "ModBot - Updater";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Updater_FormClosing);
            this.Controls.SetChildIndex(this.DownloadProgressBar, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.CheckUpdatesButton, 0);
            this.Controls.SetChildIndex(this.UpdateButton, 0);
            this.Controls.SetChildIndex(this.CurrentVersionLabel, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.LatestVersionLabel, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.StateLabel, 0);
            this.Controls.SetChildIndex(this.ChangelogButton, 0);
            this.Controls.SetChildIndex(this.BetaUpdates, 0);
            this.Controls.SetChildIndex(this.DevUpdates, 0);
            this.Controls.SetChildIndex(this.ExtensionsButton, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomProgressBar DownloadProgressBar;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Button CheckUpdatesButton;
        public System.Windows.Forms.Button UpdateButton;
        private System.Windows.Forms.Label CurrentVersionLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label LatestVersionLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label StateLabel;
        public System.Windows.Forms.Button ChangelogButton;
        public System.Windows.Forms.CheckBox BetaUpdates;
        public System.Windows.Forms.CheckBox DevUpdates;
        public System.Windows.Forms.Button ExtensionsButton;
    }
}

