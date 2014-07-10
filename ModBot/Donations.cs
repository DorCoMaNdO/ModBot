using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ModBot
{
    public partial class Donations : CustomForm
    {
        private iniUtil ini = Program.ini;
        public Donations()
        {
            InitializeComponent();
            ini.SetValue("Settings", "Donations_UpdateTop", (UpdateTopDonorsCheckBox.Checked = (ini.GetValue("Settings", "Donations_UpdateTop", "0") == "1")) ? "1" : "0");
            ini.SetValue("Settings", "Donations_Top_Limit", (TopDonorsLimit.Value = Convert.ToInt32(ini.GetValue("Settings", "Donations_Top_Limit", "20"))).ToString());
            ini.SetValue("Settings", "Donations_UpdateRecent", (UpdateRecentDonorsCheckBox.Checked = (ini.GetValue("Settings", "Donations_UpdateRecent", "0") == "1")) ? "1" : "0");
            ini.SetValue("Settings", "Donations_Recent_Limit", (RecentDonorsLimit.Value = Convert.ToInt32(ini.GetValue("Settings", "Donations_Recent_Limit", "5"))).ToString());
            ini.SetValue("Settings", "Donations_UpdateLast", (UpdateLastDonorCheckBox.Checked = (ini.GetValue("Settings", "Donations_UpdateLast", "0") == "1")) ? "1" : "0");
        }

        private void Donations_List_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > 4)
            {
                string sDonationsIgnoreRecent = "";
                string sDonationsIgnoreLatest = "";
                string sDonationsIgnoreTop = "";

                foreach (DataGridViewRow row in Donations_List.Rows)
                {
                    string sId = row.Cells["ID"].Value.ToString();
                    if (row.Cells["IncludeRecent"].Value.ToString().Equals("False"))
                    {
                        sDonationsIgnoreRecent += sId + ",";
                    }
                    if (row.Cells["IncludeLatest"].Value.ToString().Equals("False"))
                    {
                        sDonationsIgnoreLatest += sId + ",";
                    }
                    if (row.Cells["IncludeTop"].Value.ToString().Equals("False"))
                    {
                        sDonationsIgnoreTop += sId + ",";
                    }
                }
                ini.SetValue("Settings", "Donations_Ignore_Recent", sDonationsIgnoreRecent);
                ini.SetValue("Settings", "Donations_Ignore_Latest", sDonationsIgnoreLatest);
                ini.SetValue("Settings", "Donations_Ignore_Top", sDonationsIgnoreTop);
            }
        }

        private void UpdateTopDonorsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ini.SetValue("Settings", "Donations_UpdateTop", UpdateTopDonorsCheckBox.Checked ? "1" : "0");
            TopDonorsLimit.Enabled = UpdateTopDonorsCheckBox.Checked;
        }

        private void UpdateRecentDonorsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ini.SetValue("Settings", "Donations_UpdateRecent", UpdateRecentDonorsCheckBox.Checked ? "1" : "0");
            RecentDonorsLimit.Enabled = UpdateRecentDonorsCheckBox.Checked;
        }

        private void UpdateLastDonorCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ini.SetValue("Settings", "Donations_UpdateLast", UpdateLastDonorCheckBox.Checked ? "1" : "0");
        }

        private void RecentDonorsLimit_ValueChanged(object sender, EventArgs e)
        {
            ini.SetValue("Settings", "Donations_Recent_Limit", RecentDonorsLimit.Value.ToString());
        }

        private void TopDonorsLimit_ValueChanged(object sender, EventArgs e)
        {
            ini.SetValue("Settings", "Donations_Top_Limit", TopDonorsLimit.Value.ToString());
        }

        private void Donations_List_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            if (e.Column.Name == "Amount")
            {
                e.SortResult = float.Parse(e.CellValue1.ToString()).CompareTo(float.Parse(e.CellValue2.ToString()));
                e.Handled = true;
            }
        }
    }
}
