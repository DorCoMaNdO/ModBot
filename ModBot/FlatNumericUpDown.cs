using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace ModBot
{
    public class FlatNumericUpDown : NumericUpDown // Taken from http://www.rsdn.ru/forum/src/397418.1, modified by me
    {
        FieldInfo buttonState;

        public static void SetStyleHack(Control control, ControlStyles flag)
        {
            control.GetType().GetMethod("SetStyle", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(control, new object[] { flag, true });
        }

        public FlatNumericUpDown() : base()
        {
            Controls[0].Paint += new PaintEventHandler(UpDownButtons_Paint);
            SetStyle(ControlStyles.UserPaint, true);
            buttonState = Controls[0].GetType().GetField("pushed", BindingFlags.NonPublic | BindingFlags.Instance);
            SetStyleHack(Controls[0], ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(100, 100, 100)), 0, 0, ClientSize.Width, ClientSize.Height);
            if (Enabled)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.White), Controls[1].ClientRectangle.X + 1, Controls[1].ClientRectangle.Y + 1, Controls[1].ClientRectangle.Width + 2, Controls[1].ClientRectangle.Height + 2);
            }
            else
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(240, 240, 240)), Controls[1].ClientRectangle.X + 1, Controls[1].ClientRectangle.Y + 1, Controls[1].ClientRectangle.Width + 2, Controls[1].ClientRectangle.Height + 2);
            }
        }

        private void UpDownButtons_Paint(object sender, PaintEventArgs e)
        {
            int height = ClientSize.Height / 2;
            ControlPaint.DrawScrollButton(e.Graphics, new Rectangle(1, -1, 16, height), ScrollButton.Up, ButtonState.Flat | ((int)buttonState.GetValue(Controls[0]) == 1 ? ButtonState.Pushed : 0));
            ControlPaint.DrawScrollButton(e.Graphics, new Rectangle(1, height -1, 16, height), ScrollButton.Down, ButtonState.Flat | ((int)buttonState.GetValue(Controls[0]) == 2 ? ButtonState.Pushed : 0));
            //e.Graphics.DrawRectangle(new Pen(Color.FromArgb(100, 100, 100)), 0, 0, 1, ClientSize.Height);
            //e.Graphics.DrawRectangle(new Pen(Color.White), -1, 0, 1, ClientSize.Height);
        }
    }
}
