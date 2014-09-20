using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ModBot
{
    public class CustomProgressBar : ProgressBar
    {
        public CustomProgressBar()
        {
            Text = "";
            TextColor = Brushes.Black;
            Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rect = ClientRectangle;
            Graphics g = e.Graphics;

            ProgressBarRenderer.DrawHorizontalBar(g, rect);
            rect.Inflate(-1, -1);
            if (Value > 0)
            {
                ProgressBarRenderer.DrawHorizontalChunks(g, new Rectangle(rect.X, rect.Y, (int)Math.Round(((float)Value / Maximum) * rect.Width), rect.Height));
            }

            string text = Text;
            if (Text == "")
            {
                text = Value.ToString() + "%";
            }
            else if (Text == " ")
            {
                text = "";
            }
            text = text.Replace("PRECENTAGE", Value.ToString() + "%");

            SizeF len = g.MeasureString(text, Font);
            g.DrawString(text, Font, TextColor, Convert.ToInt32((rect.Width / 2) - (len.Width / 2)), Convert.ToInt32((rect.Height / 2) - (len.Height / 2)));
        }

        [Category("Appearance")]
        [Description("Gets or sets the color for the text displayed above the control.")]
        public Brush TextColor
        {
            get;
            set;
        }

        [Category("Appearance")]
        [Description("Gets or sets the text displayed above the control.")]
        [BrowsableAttribute(true)]
        public new string Text
        {
            get;
            set;
        }

        [Category("Appearance")]
        [Description("The font used to display text in the control.")]
        [BrowsableAttribute(true)]
        public new Font Font
        {
            get;
            set;
        }
    }
}
