using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace ModBotUpdater
{
    public partial class ImageWindow : Form
    {
        private int Step = 0, Steps = 10;
        public ImageWindow(Image img, bool showprogress = false, int steps = 10, int step = 0)
        {
            InitializeComponent();
            BackgroundImage = img;
            Progress.Visible = showprogress;
            Size = BackgroundImage.Size;
            Progress.Location = new Point(12, Size.Height - 35);
            Progress.Size = new Size(Size.Width - 24, 23);
            Step = step;
            Steps = steps;
        }

        public new void Show()
        {
            if (!IsHandleCreated)
            {
                base.Show();
                return;
            }

            BeginInvoke((MethodInvoker)delegate
            {
                base.Show();
            });
        }

        public new void Hide()
        {
            if (!IsHandleCreated)
            {
                base.Hide();
                return;
            }

            BeginInvoke((MethodInvoker)delegate
            {
                base.Hide();
            });
        }

        public void ShowProgress(bool show)
        {
            if (!IsHandleCreated)
            {
                Progress.Visible = show;
                return;
            }

            BeginInvoke((MethodInvoker)delegate
            {
                Progress.Visible = show;
            });
        }

        public void SetProgress(int step, int steps)
        {
            Step = step;
            Steps = steps;

            if (step >= steps)
            {
                step = steps;
                return;
            }

            if (!IsHandleCreated)
            {
                Progress.Value = step / steps * 100;
                return;
            }

            BeginInvoke((MethodInvoker)delegate
            {
                Progress.Value = step / steps * 100;
            });
        }

        public void AddProgress()
        {
            if (Step >= Steps)
            {
                Step = Steps;
                return;
            }

            SetProgress(Step + 1, Steps);
        }
    }
}
