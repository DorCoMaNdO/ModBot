namespace ModBotUpdater
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
            this.ChangelogNotes = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // ChangelogNotes
            // 
            this.ChangelogNotes.BackColor = System.Drawing.Color.White;
            this.ChangelogNotes.Location = new System.Drawing.Point(6, 28);
            this.ChangelogNotes.Name = "ChangelogNotes";
            this.ChangelogNotes.ReadOnly = true;
            this.ChangelogNotes.Size = new System.Drawing.Size(503, 503);
            this.ChangelogNotes.TabIndex = 12;
            this.ChangelogNotes.Text = "";
            // 
            // Changelog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 538);
            this.Controls.Add(this.ChangelogNotes);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Changelog";
            this.Text = "Changelog";
            this.Controls.SetChildIndex(this.ChangelogNotes, 0);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.RichTextBox ChangelogNotes;
    }
}