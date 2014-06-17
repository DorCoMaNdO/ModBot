using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ModBotUpdater
{
    public partial class Changelog : CustomForm
    {
        Updater updater;
        public Changelog(Updater updater)
        {
            InitializeComponent();
            this.updater = updater;
        }
    }
}
