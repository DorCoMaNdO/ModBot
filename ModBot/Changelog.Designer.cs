namespace ModBot
{
    partial class Changelog
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
            this.ChangesList = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // ChangesList
            // 
            this.ChangesList.Location = new System.Drawing.Point(14, 35);
            this.ChangesList.Name = "ChangesList";
            this.ChangesList.Size = new System.Drawing.Size(121, 97);
            this.ChangesList.TabIndex = 12;
            // 
            // Changelog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 538);
            this.Controls.Add(this.ChangesList);
            this.MaximizeBox = false;
            this.Name = "Changelog";
            this.Text = "Changelog";
            this.Controls.SetChildIndex(this.ChangesList, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView ChangesList;

    }
}