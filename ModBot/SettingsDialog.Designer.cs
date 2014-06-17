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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.intervalBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.payoutBox = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.startButton = new System.Windows.Forms.Button();
            this.aboutButton = new System.Windows.Forms.Button();
            this.subBox = new System.Windows.Forms.TextBox();
            this.passwordBox = new System.Windows.Forms.TextBox();
            this.currencyBox = new System.Windows.Forms.TextBox();
            this.channelBox = new System.Windows.Forms.TextBox();
            this.botNameBox = new System.Windows.Forms.TextBox();
            this.DonationsKeyBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Bot Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Bot Password:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(337, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Currency: ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(53, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Channel:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(309, 39);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Payout Interval:";
            // 
            // intervalBox
            // 
            this.intervalBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.intervalBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.intervalBox.FormatString = "N0";
            this.intervalBox.FormattingEnabled = true;
            this.intervalBox.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "10",
            "12",
            "15",
            "20",
            "30",
            "60"});
            this.intervalBox.Location = new System.Drawing.Point(392, 36);
            this.intervalBox.Name = "intervalBox";
            this.intervalBox.Size = new System.Drawing.Size(88, 21);
            this.intervalBox.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(308, 66);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Payout Amount:";
            // 
            // payoutBox
            // 
            this.payoutBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.payoutBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
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
            this.payoutBox.Location = new System.Drawing.Point(392, 63);
            this.payoutBox.Name = "payoutBox";
            this.payoutBox.Size = new System.Drawing.Size(88, 21);
            this.payoutBox.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 117);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Sub Spreadsheet";
            // 
            // startButton
            // 
            this.startButton.BackColor = System.Drawing.Color.White;
            this.startButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.startButton.Location = new System.Drawing.Point(321, 140);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(78, 24);
            this.startButton.TabIndex = 8;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = false;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // aboutButton
            // 
            this.aboutButton.BackColor = System.Drawing.Color.White;
            this.aboutButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.aboutButton.Location = new System.Drawing.Point(405, 140);
            this.aboutButton.Name = "aboutButton";
            this.aboutButton.Size = new System.Drawing.Size(75, 24);
            this.aboutButton.TabIndex = 10;
            this.aboutButton.Text = "About";
            this.aboutButton.UseVisualStyleBackColor = false;
            this.aboutButton.Click += new System.EventHandler(this.aboutButton_Click);
            // 
            // subBox
            // 
            this.subBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.subBox.Location = new System.Drawing.Point(102, 114);
            this.subBox.Name = "subBox";
            this.subBox.Size = new System.Drawing.Size(378, 20);
            this.subBox.TabIndex = 4;
            // 
            // passwordBox
            // 
            this.passwordBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.passwordBox.Location = new System.Drawing.Point(102, 62);
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.PasswordChar = '*';
            this.passwordBox.Size = new System.Drawing.Size(121, 20);
            this.passwordBox.TabIndex = 2;
            // 
            // currencyBox
            // 
            this.currencyBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.currencyBox.Location = new System.Drawing.Point(392, 88);
            this.currencyBox.MaxLength = 15;
            this.currencyBox.Name = "currencyBox";
            this.currencyBox.Size = new System.Drawing.Size(88, 20);
            this.currencyBox.TabIndex = 7;
            this.currencyBox.Text = "ModCoins";
            // 
            // channelBox
            // 
            this.channelBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.channelBox.Location = new System.Drawing.Point(102, 88);
            this.channelBox.Name = "channelBox";
            this.channelBox.Size = new System.Drawing.Size(121, 20);
            this.channelBox.TabIndex = 3;
            this.channelBox.Text = "ModChannel";
            // 
            // botNameBox
            // 
            this.botNameBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.botNameBox.Location = new System.Drawing.Point(102, 36);
            this.botNameBox.Name = "botNameBox";
            this.botNameBox.Size = new System.Drawing.Size(121, 20);
            this.botNameBox.TabIndex = 1;
            this.botNameBox.Text = "ModBot";
            // 
            // DonationsKeyBox
            // 
            this.DonationsKeyBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DonationsKeyBox.Location = new System.Drawing.Point(102, 140);
            this.DonationsKeyBox.Name = "DonationsKeyBox";
            this.DonationsKeyBox.PasswordChar = '*';
            this.DonationsKeyBox.Size = new System.Drawing.Size(213, 20);
            this.DonationsKeyBox.TabIndex = 12;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 143);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(96, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "Donations API Key";
            // 
            // SettingsDialog
            // 
            this.AcceptButton = this.startButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 178);
            this.Controls.Add(this.DonationsKeyBox);
            this.Controls.Add(this.label8);
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
            this.MaximizeBox = false;
            this.Name = "SettingsDialog";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ModBot - Settings";
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.startButton, 0);
            this.Controls.SetChildIndex(this.botNameBox, 0);
            this.Controls.SetChildIndex(this.channelBox, 0);
            this.Controls.SetChildIndex(this.currencyBox, 0);
            this.Controls.SetChildIndex(this.intervalBox, 0);
            this.Controls.SetChildIndex(this.payoutBox, 0);
            this.Controls.SetChildIndex(this.passwordBox, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.subBox, 0);
            this.Controls.SetChildIndex(this.aboutButton, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.DonationsKeyBox, 0);
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
        private System.Windows.Forms.TextBox passwordBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox payoutBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox subBox;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button aboutButton;
        private System.Windows.Forms.TextBox DonationsKeyBox;
        private System.Windows.Forms.Label label8;
    }
}

