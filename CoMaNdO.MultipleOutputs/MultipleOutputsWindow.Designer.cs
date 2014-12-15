namespace CoMaNdO.MultipleOutputs
{
    partial class MultipleOutputsWindow
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
            this.CommandsDataGrid = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Command = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Data = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Output = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.CommandsDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // CommandsDataGrid
            // 
            this.CommandsDataGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.CommandsDataGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CommandsDataGrid.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.CommandsDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CommandsDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Command,
            this.Data,
            this.Output});
            this.CommandsDataGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.CommandsDataGrid.Location = new System.Drawing.Point(-1, 32);
            this.CommandsDataGrid.MultiSelect = false;
            this.CommandsDataGrid.Name = "CommandsDataGrid";
            this.CommandsDataGrid.RowHeadersVisible = false;
            this.CommandsDataGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.CommandsDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.CommandsDataGrid.Size = new System.Drawing.Size(1025, 618);
            this.CommandsDataGrid.TabIndex = 62;
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            // 
            // Command
            // 
            this.Command.HeaderText = "Command";
            this.Command.Name = "Command";
            // 
            // Data
            // 
            this.Data.HeaderText = "Data";
            this.Data.Name = "Data";
            this.Data.Width = 200;
            // 
            // Output
            // 
            this.Output.HeaderText = "Output";
            this.Output.Name = "Output";
            this.Output.Width = 607;
            // 
            // MultipleOutputsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1024, 562);
            this.Controls.Add(this.CommandsDataGrid);
            this.Font = new System.Drawing.Font("Tahoma", 8F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MultipleOutputsWindow";
            this.Text = "Multiple Outputs Window";
            ((System.ComponentModel.ISupportInitialize)(this.CommandsDataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView CommandsDataGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Command;
        private System.Windows.Forms.DataGridViewTextBoxColumn Data;
        private System.Windows.Forms.DataGridViewTextBoxColumn Output;



    }
}