namespace ModBotUpdater
{
    partial class Extensions
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
            this.ExtensionsDataGrid = new System.Windows.Forms.DataGridView();
            this.ExtensionName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Author = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UniqueID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Version = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Updated = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DownloadButton = new System.Windows.Forms.Button();
            this.ExtensionLabel = new System.Windows.Forms.Label();
            this.DownloadProgressBar = new ModBotUpdater.CustomProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.ExtensionsDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // ExtensionsDataGrid
            // 
            this.ExtensionsDataGrid.AllowUserToAddRows = false;
            this.ExtensionsDataGrid.AllowUserToDeleteRows = false;
            this.ExtensionsDataGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.ExtensionsDataGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ExtensionsDataGrid.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.ExtensionsDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ExtensionsDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ExtensionName,
            this.Author,
            this.Description,
            this.UniqueID,
            this.FileName,
            this.Version,
            this.Updated});
            this.ExtensionsDataGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.ExtensionsDataGrid.Location = new System.Drawing.Point(7, 30);
            this.ExtensionsDataGrid.MultiSelect = false;
            this.ExtensionsDataGrid.Name = "ExtensionsDataGrid";
            this.ExtensionsDataGrid.ReadOnly = true;
            this.ExtensionsDataGrid.RowHeadersVisible = false;
            this.ExtensionsDataGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.ExtensionsDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ExtensionsDataGrid.Size = new System.Drawing.Size(886, 218);
            this.ExtensionsDataGrid.TabIndex = 62;
            this.ExtensionsDataGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ExtensionSelected);
            this.ExtensionsDataGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ExtensionSelected);
            // 
            // ExtensionName
            // 
            this.ExtensionName.HeaderText = "Name";
            this.ExtensionName.Name = "ExtensionName";
            this.ExtensionName.ReadOnly = true;
            this.ExtensionName.Width = 150;
            // 
            // Author
            // 
            this.Author.HeaderText = "Author";
            this.Author.Name = "Author";
            this.Author.ReadOnly = true;
            this.Author.Width = 150;
            // 
            // Description
            // 
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.Width = 352;
            // 
            // UniqueID
            // 
            this.UniqueID.HeaderText = "UniqueID";
            this.UniqueID.Name = "UniqueID";
            this.UniqueID.ReadOnly = true;
            this.UniqueID.Visible = false;
            // 
            // FileName
            // 
            this.FileName.HeaderText = "FileName";
            this.FileName.Name = "FileName";
            this.FileName.ReadOnly = true;
            this.FileName.Visible = false;
            // 
            // Version
            // 
            this.Version.HeaderText = "Latest Version";
            this.Version.Name = "Version";
            this.Version.ReadOnly = true;
            // 
            // Updated
            // 
            this.Updated.HeaderText = "Last Updated";
            this.Updated.Name = "Updated";
            this.Updated.ReadOnly = true;
            this.Updated.Width = 116;
            // 
            // DownloadButton
            // 
            this.DownloadButton.BackColor = System.Drawing.Color.White;
            this.DownloadButton.Enabled = false;
            this.DownloadButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DownloadButton.Location = new System.Drawing.Point(14, 302);
            this.DownloadButton.Name = "DownloadButton";
            this.DownloadButton.Size = new System.Drawing.Size(872, 24);
            this.DownloadButton.TabIndex = 99;
            this.DownloadButton.Text = "Download";
            this.DownloadButton.UseVisualStyleBackColor = false;
            this.DownloadButton.Click += new System.EventHandler(this.DownloadButton_Click);
            // 
            // ExtensionLabel
            // 
            this.ExtensionLabel.AutoSize = true;
            this.ExtensionLabel.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExtensionLabel.Location = new System.Drawing.Point(10, 251);
            this.ExtensionLabel.Name = "ExtensionLabel";
            this.ExtensionLabel.Size = new System.Drawing.Size(115, 19);
            this.ExtensionLabel.TabIndex = 100;
            this.ExtensionLabel.Text = "Selected Extension:";
            // 
            // DownloadProgressBar
            // 
            this.DownloadProgressBar.Location = new System.Drawing.Point(14, 273);
            this.DownloadProgressBar.Name = "DownloadProgressBar";
            this.DownloadProgressBar.Size = new System.Drawing.Size(872, 23);
            this.DownloadProgressBar.TabIndex = 101;
            // 
            // Extensions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 340);
            this.Controls.Add(this.DownloadProgressBar);
            this.Controls.Add(this.ExtensionLabel);
            this.Controls.Add(this.ExtensionsDataGrid);
            this.Controls.Add(this.DownloadButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Extensions";
            this.Text = "Extensions";
            this.Controls.SetChildIndex(this.DownloadButton, 0);
            this.Controls.SetChildIndex(this.ExtensionsDataGrid, 0);
            this.Controls.SetChildIndex(this.ExtensionLabel, 0);
            this.Controls.SetChildIndex(this.DownloadProgressBar, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ExtensionsDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.DataGridView ExtensionsDataGrid;
        public System.Windows.Forms.Button DownloadButton;
        private System.Windows.Forms.Label ExtensionLabel;
        private CustomProgressBar DownloadProgressBar;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExtensionName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Author;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn UniqueID;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Version;
        private System.Windows.Forms.DataGridViewTextBoxColumn Updated;

    }
}