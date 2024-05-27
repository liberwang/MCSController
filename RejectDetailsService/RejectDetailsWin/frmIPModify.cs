using RejectDetailsLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RejectDetailsWin {
    public partial class frmIPModify : Form {
        private int id {  get; set; }

        public frmIPModify() {
            InitializeComponent();
        }

        public frmIPModify(int id, string ipAddress, string desc, bool enabled, bool statistics, bool alarm) : this() {
            this.id = id;
            this.txtIPAddress.Text = ipAddress;
            this.txtDescription.Text = desc;
            this.chkEnable.Checked = enabled;
            this.chkStatistics.Checked = statistics;
            this.chkAlarm.Checked = alarm;  
        }

        private void btnSave_Click(object sender, EventArgs e) {
            string ipAddressReg = @"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";
            string sIPAddress = this.txtIPAddress.Text.Trim();
            string sDescription = this.txtDescription.Text.Trim();
            bool isEnabled = this.chkEnable.Checked;
            bool isStatistics = this.chkStatistics.Checked;
            bool isAlarm = this.chkAlarm.Checked;

            if ( string.IsNullOrWhiteSpace(sIPAddress)) {
                MessageBox.Show("Please input IP Address.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtIPAddress.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(sDescription))
            {
                MessageBox.Show("Please input description.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtDescription.Focus();
                return;
            }

            Match m = Regex.Match(sIPAddress, ipAddressReg, RegexOptions.IgnoreCase);
            if (!m.Success) {
                MessageBox.Show("The format of IP Address is not valid!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            clsController con = new clsController()
            {
                Id = this.id,
                IpAddress = sIPAddress, 
                Description = sDescription,
                IsEnabled = isEnabled,
                IsStatistics = isStatistics,
                IsAlarm = isAlarm
            };
            if (con.IsDuplicate())
            {
                MessageBox.Show("The IP address and description are duplicated with another controller.");
                return;
            }
            con.SaveController();

            this.DialogResult = DialogResult.OK;
            this.Visible = false;
        }

        private void chkStatistics_CheckedChanged(object sender, EventArgs e)
        {
            if (chkStatistics.Checked)
                this.chkAlarm.Checked = false;
        }

        private void chkAlarm_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAlarm.Checked)
                this.chkStatistics.Checked = false;
        }
    }
}
