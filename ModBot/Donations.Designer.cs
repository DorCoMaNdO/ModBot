namespace ModBot
{
    partial class Donations
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
            this.Donations_List = new System.Windows.Forms.DataGridView();
            this.UpdateRecentDonorsCheckBox = new System.Windows.Forms.CheckBox();
            this.UpdateLastDonorCheckBox = new System.Windows.Forms.CheckBox();
            this.UpdateTopDonorsCheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TopDonorsLimit = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.RecentDonorsLimit = new System.Windows.Forms.NumericUpDown();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Donor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Notes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IncludeRecent = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IncludeLatest = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IncludeTop = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IncludeTopDonor = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.Donations_List)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TopDonorsLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RecentDonorsLimit)).BeginInit();
            this.SuspendLayout();
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
            this.Donations_List.Location = new System.Drawing.Point(7, 82);
            this.Donations_List.MultiSelect = false;
            this.Donations_List.Name = "Donations_List";
            this.Donations_List.RowHeadersVisible = false;
            this.Donations_List.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.Donations_List.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Donations_List.Size = new System.Drawing.Size(813, 266);
            this.Donations_List.TabIndex = 12;
            this.Donations_List.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Donations_List_CellValueChanged);
            this.Donations_List.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Donations_List_CellValueChanged);
            this.Donations_List.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.Donations_List_CellValueChanged);
            this.Donations_List.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.Donations_List_SortCompare);
            // 
            // UpdateRecentDonorsCheckBox
            // 
            this.UpdateRecentDonorsCheckBox.AutoSize = true;
            this.UpdateRecentDonorsCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UpdateRecentDonorsCheckBox.Location = new System.Drawing.Point(326, 35);
            this.UpdateRecentDonorsCheckBox.Name = "UpdateRecentDonorsCheckBox";
            this.UpdateRecentDonorsCheckBox.Size = new System.Drawing.Size(149, 17);
            this.UpdateRecentDonorsCheckBox.TabIndex = 13;
            this.UpdateRecentDonorsCheckBox.Text = "Auto-update recent donors";
            this.UpdateRecentDonorsCheckBox.UseVisualStyleBackColor = true;
            this.UpdateRecentDonorsCheckBox.CheckedChanged += new System.EventHandler(this.UpdateRecentDonorsCheckBox_CheckedChanged);
            // 
            // UpdateLastDonorCheckBox
            // 
            this.UpdateLastDonorCheckBox.AutoSize = true;
            this.UpdateLastDonorCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UpdateLastDonorCheckBox.Location = new System.Drawing.Point(683, 36);
            this.UpdateLastDonorCheckBox.Name = "UpdateLastDonorCheckBox";
            this.UpdateLastDonorCheckBox.Size = new System.Drawing.Size(130, 17);
            this.UpdateLastDonorCheckBox.TabIndex = 14;
            this.UpdateLastDonorCheckBox.Text = "Auto-update last donor";
            this.UpdateLastDonorCheckBox.UseVisualStyleBackColor = true;
            this.UpdateLastDonorCheckBox.CheckedChanged += new System.EventHandler(this.UpdateLastDonorCheckBox_CheckedChanged);
            // 
            // UpdateTopDonorsCheckBox
            // 
            this.UpdateTopDonorsCheckBox.AutoSize = true;
            this.UpdateTopDonorsCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UpdateTopDonorsCheckBox.Location = new System.Drawing.Point(14, 36);
            this.UpdateTopDonorsCheckBox.Name = "UpdateTopDonorsCheckBox";
            this.UpdateTopDonorsCheckBox.Size = new System.Drawing.Size(134, 17);
            this.UpdateTopDonorsCheckBox.TabIndex = 15;
            this.UpdateTopDonorsCheckBox.Text = "Auto-update top donors";
            this.UpdateTopDonorsCheckBox.UseVisualStyleBackColor = true;
            this.UpdateTopDonorsCheckBox.CheckedChanged += new System.EventHandler(this.UpdateTopDonorsCheckBox_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Show up to              top donors";
            // 
            // TopDonorsLimit
            // 
            this.TopDonorsLimit.Location = new System.Drawing.Point(70, 54);
            this.TopDonorsLimit.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.TopDonorsLimit.Name = "TopDonorsLimit";
            this.TopDonorsLimit.Size = new System.Drawing.Size(36, 20);
            this.TopDonorsLimit.TabIndex = 17;
            this.TopDonorsLimit.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.TopDonorsLimit.ValueChanged += new System.EventHandler(this.TopDonorsLimit_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(323, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(168, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Show up to              recent donors";
            // 
            // RecentDonorsLimit
            // 
            this.RecentDonorsLimit.Location = new System.Drawing.Point(382, 54);
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
            this.RecentDonorsLimit.TabIndex = 19;
            this.RecentDonorsLimit.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.RecentDonorsLimit.ValueChanged += new System.EventHandler(this.RecentDonorsLimit_ValueChanged);
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
            this.ID.Width = 43;
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
            this.IncludeTopDonor.Width = 64;
            // 
            // Donations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(827, 354);
            this.Controls.Add(this.RecentDonorsLimit);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TopDonorsLimit);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.UpdateTopDonorsCheckBox);
            this.Controls.Add(this.UpdateLastDonorCheckBox);
            this.Controls.Add(this.UpdateRecentDonorsCheckBox);
            this.Controls.Add(this.Donations_List);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Donations";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Donations";
            this.Controls.SetChildIndex(this.Donations_List, 0);
            this.Controls.SetChildIndex(this.UpdateRecentDonorsCheckBox, 0);
            this.Controls.SetChildIndex(this.UpdateLastDonorCheckBox, 0);
            this.Controls.SetChildIndex(this.UpdateTopDonorsCheckBox, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.TopDonorsLimit, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.RecentDonorsLimit, 0);
            ((System.ComponentModel.ISupportInitialize)(this.Donations_List)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TopDonorsLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RecentDonorsLimit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.DataGridView Donations_List;
        public System.Windows.Forms.CheckBox UpdateRecentDonorsCheckBox;
        public System.Windows.Forms.CheckBox UpdateLastDonorCheckBox;
        public System.Windows.Forms.CheckBox UpdateTopDonorsCheckBox;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.NumericUpDown TopDonorsLimit;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.NumericUpDown RecentDonorsLimit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn Donor;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Notes;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IncludeRecent;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IncludeLatest;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IncludeTop;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IncludeTopDonor;
    }
}