using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace ModBot
{
    public partial class CustomForm : Form
    {
        private bool bMove = false;
        private int iMoveX = 0;
        private int iMoveY = 0;
        private Control.ControlCollection ExcludedControls;
        public bool bActivated = false;

        public CustomForm()
        {
            CustomFormBorderStyle = FormBorderStyles.Custom1;
            FormLabelColorOptimization = true;
            BorderActiveColor = Color.White;
            BorderInactiveColor = Color.LightGray;
            FormLabelColor = Color.Black;
            InitializeComponent();
            if (CustomFormBorderStyle != FormBorderStyles.None)
            {
                FormBorderStyle = FormBorderStyle.None;
            }

            if (CustomFormBorderStyle == FormBorderStyles.Custom1)
            {
                //Size = new Size(Width + 16, Height + 38);
                ExcludedControls = new Control.ControlCollection(this);
                ExcludedControls.Add(FormCloseButton);
                ExcludedControls.Add(FormMaximizeButton);
                ExcludedControls.Add(FormMinimizeButton);
                ExcludedControls.Add(FormText);
                ExcludedControls.Add(FormIcon);
                ExcludedControls.Add(LeftBorderCleaner);
                ExcludedControls.Add(LeftBorder);
                ExcludedControls.Add(RightBorderCleaner);
                ExcludedControls.Add(LowBorderCleaner);
                ExcludedControls.Add(RightBorder);
                ExcludedControls.Add(TopBorderCleaner);
                ExcludedControls.Add(LowBorder);
                ExcludedControls.Add(TopBorder);

                FormIcon.Image = Icon.ToBitmap();
                FormIcon.Location = new Point(8, 8);

                FormText.Location = new Point(1, 4);

                FormText.Text = Text;

                SetItemsColor(BorderActiveColor);
                FixBorders();
            }
        }

        private void CustomForm_Load(object sender, EventArgs e)
        {
            FormIcon.Visible = false;
            if (CustomFormBorderStyle != FormBorderStyles.None)
            {
                FormBorderStyle = FormBorderStyle.None;
            }

            if (CustomFormBorderStyle == FormBorderStyles.Custom1)
            {
                //Size = new Size(Width + 16, Height + 38);
                /*foreach (Control ctrl in Controls)
                {
                    if (!ExcludedControls.Contains(ctrl))
                    {
                        ctrl.Location = new Point(ctrl.Location.X + 8, ctrl.Location.Y + 30);
                    }
                }*/

                FixBorders();

                if(MaximizeBox)
                {
                    TopBorder.Cursor = Cursors.SizeNS;
                    TopBorderCleaner.Cursor = Cursors.SizeNS;

                    LeftBorder.Cursor = Cursors.SizeWE;
                    LeftBorderCleaner.Cursor = Cursors.SizeWE;

                    RightBorder.Cursor = Cursors.SizeWE;
                    RightBorderCleaner.Cursor = Cursors.SizeWE;

                    LowBorder.Cursor = Cursors.SizeNS;
                    LowBorderCleaner.Cursor = Cursors.SizeNS;

                    if (!MinimizeBox)
                    {
                        FormMinimizeButton.Enabled = false;
                    }
                }
                else
                {
                    if (!MinimizeBox)
                    {
                        FormMaximizeButton.Visible = false;

                        FormMinimizeButton.Visible = false;
                    }
                    else
                    {
                        FormMaximizeButton.Enabled = false;
                    }
                }
            }
            else
            {
                TopBorder.Visible = false;
                TopBorderCleaner.Visible = false;

                LeftBorder.Visible = false;
                LeftBorderCleaner.Visible = false;

                RightBorder.Visible = false;
                RightBorderCleaner.Visible = false;

                LowBorder.Visible = false;
                LowBorderCleaner.Visible = false;

                FormIcon.Visible = false;

                FormText.Visible = false;

                FormMinimizeButton.Visible = false;

                FormMaximizeButton.Visible = false;

                FormCloseButton.Visible = false;
            }
        }

        private void FixBorders()
        {
            if (CustomFormBorderStyle == FormBorderStyles.Custom1)
            {
                TopBorder.BringToFront();
                TopBorder.Location = new Point(0, 0);
                TopBorder.Size = new Size(Width, 30);
                TopBorderCleaner.BringToFront();
                TopBorderCleaner.Location = new Point(1, 1);
                TopBorderCleaner.Size = new Size(Width - 2, 28);

                LowBorder.BringToFront();
                LowBorder.Location = new Point(0, Height - 8);
                LowBorder.Size = new Size(Width, 8);
                LowBorderCleaner.BringToFront();
                LowBorderCleaner.Location = new Point(1, Height - 7);
                LowBorderCleaner.Size = new Size(Width - 2, 6);

                LeftBorder.BringToFront();
                LeftBorder.Location = new Point(0, 30);
                LeftBorder.Size = new Size(8, Height - 38);
                LeftBorderCleaner.BringToFront();
                LeftBorderCleaner.Location = new Point(1, 29);
                LeftBorderCleaner.Size = new Size(6, Height - 36);

                RightBorder.BringToFront();
                RightBorder.Location = new Point(Width - 8, 30);
                RightBorder.Size = new Size(8, Height - 38);
                RightBorderCleaner.BringToFront();
                RightBorderCleaner.Location = new Point(Width - 7, 29);
                RightBorderCleaner.Size = new Size(6, Height - 36);

                FormIcon.BringToFront();

                FormText.BringToFront();
                FormText.Size = new Size(Width - 2, 24);

                FormCloseButton.BringToFront();
                FormCloseButton.Location = new Point(Width - 48, 1);

                FormMaximizeButton.BringToFront();
                FormMaximizeButton.Location = new Point(Width - 84, 1);

                FormMinimizeButton.BringToFront();
                FormMinimizeButton.Location = new Point(Width - 120, 1);
            }
        }

        private void CustomForm_Activated(object sender, EventArgs e)
        {
            if (CustomFormBorderStyle == FormBorderStyles.Custom1)
            {
                SetItemsColor(BorderActiveColor);

                bActivated = true;

                bMove = false;
            }
        }

        private void CustomForm_Deactivate(object sender, EventArgs e)
        {
            if (CustomFormBorderStyle == FormBorderStyles.Custom1)
            {
                SetItemsColor(BorderInactiveColor);

                bActivated = false;

                bMove = false;
            }
        }

        private void SetItemsColor(Color cColor)
        {
            TopBorder.BackColor = cColor;
            TopBorderCleaner.BackColor = cColor;

            LeftBorder.BackColor = cColor;
            LeftBorderCleaner.BackColor = cColor;

            RightBorder.BackColor = cColor;
            RightBorderCleaner.BackColor = cColor;

            LowBorder.BackColor = cColor;
            LowBorderCleaner.BackColor = cColor;

            FormText.BackColor = cColor;

            FormCloseButton.BackColor = cColor;
            FormCloseButton.FlatAppearance.BorderColor = cColor;

            FormMaximizeButton.BackColor = cColor;
            FormMaximizeButton.FlatAppearance.BorderColor = cColor;

            FormMinimizeButton.BackColor = cColor;
            FormMinimizeButton.FlatAppearance.BorderColor = cColor;

            if (cColor.R < 55 && cColor.G < 55 && cColor.B < 55)
            {
                FormCloseButton.ForeColor = Color.White;

                FormMaximizeButton.ForeColor = Color.White;

                FormMinimizeButton.ForeColor = Color.White;

                /*FormCloseButton.BackColor = Color.Gray;

                FormMaximizeButton.BackColor = Color.Gray;

                FormMinimizeButton.BackColor = Color.Gray;*/
            }
            else
            {
                FormCloseButton.ForeColor = Color.Black;

                FormMaximizeButton.ForeColor = Color.Black;

                FormMinimizeButton.ForeColor = Color.Black;

                /*FormCloseButton.BackColor = Color.FromArgb(192, 0, 0);

                FormMaximizeButton.BackColor = Color.Empty;

                FormMinimizeButton.BackColor = Color.Empty;*/
            }

            if (FormLabelColorOptimization)
            {
                if (cColor.R < 55 && cColor.G < 55 && cColor.B < 55)
                {
                    FormText.ForeColor = Color.White;
                }
                else
                {
                    FormText.ForeColor = Color.Black;
                }
            }
            else
            {
                FormText.ForeColor = FormLabelColor;
            }
        }

        private void TopBorder_MouseDown(object sender, MouseEventArgs e)
        {
            if (CustomFormBorderStyle == FormBorderStyles.Custom1)
            {
                if (e.Button == MouseButtons.Left)
                {
                    bMove = true;
                    iMoveX = e.X + 1;
                    iMoveY = e.Y + 4;
                }
            }
        }

        private void TopBorder_MouseMove(object sender, MouseEventArgs e)
        {
            if (CustomFormBorderStyle == FormBorderStyles.Custom1)
            {
                if (bMove)
                {
                    Location = new Point(MousePosition.X - iMoveX, MousePosition.Y - iMoveY);
                }
            }
        }

        private void TopBorder_MouseUp(object sender, MouseEventArgs e)
        {
            if (CustomFormBorderStyle == FormBorderStyles.Custom1)
            {
                if (e.Button == MouseButtons.Left)
                {
                    bMove = false;
                }
            }
        }

        private void FormMinimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void FormMaximizeButton_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void FormCloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        [Category("Appearance")]
        [Description("Sets or gets the color of the border while the form is in focus")]
        public Color BorderActiveColor
        {
            get;
            set;
        }

        [Category("Appearance")]
        [Description("Sets or gets the color of the border while the form is not in focus")]
        public Color BorderInactiveColor
        {
            get;
            set;
        }

        // Summary:
        //     Specifies the border styles for a form.
        //[ComVisible(true)]
        public enum FormBorderStyles
        {
            // Summary:
            //     No border.
            None = 0,
            // Summary:
            //     Custom border added after the form is loaded.
            Custom1 = 1,
        }

        [Category("Appearance")]
        public FormBorderStyles CustomFormBorderStyle
        {
            get;
            set;
        }

        [Category("Appearance")]
        [Description("Automatically changes the color of the form's label from black to white if the color of the borders is too dark")]
        public Boolean FormLabelColorOptimization
        {
            get;
            set;
        }

        [Category("Appearance")]
        [Description("Manually change the color form's label (requires FormLabelColorOptimization to be set to false)")]
        public Color FormLabelColor
        {
            get;
            set;
        }

        private void CustomForm_Resize(object sender, EventArgs e)
        {
            if (CustomFormBorderStyle == FormBorderStyles.Custom1)
            {
                FixBorders();
            }
        }

        private void CustomForm_TextChanged(object sender, EventArgs e)
        {
            if (CustomFormBorderStyle == FormBorderStyles.Custom1)
            {
                FormText.Text = Text;
            }
        }
    }
}
