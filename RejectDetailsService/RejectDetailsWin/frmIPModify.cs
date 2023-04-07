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
        public frmIPModify() {
            InitializeComponent();
        }

        public frmIPModify( string ipAddress, string desc, bool enabled ) : this() {
            this.txtIPAddress.Text = ipAddress;
            this.txtDescription.Text = desc;
            this.chkEnable.Checked = enabled;
        }

        private void btnSave_Click(object sender, EventArgs e) {
            string ipAddressReg = @"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";
            string sIPAddress = this.txtIPAddress.Text.Trim();
            string sDescription = this.txtDescription.Text.Trim();
            bool isEnabled = this.chkEnable.Checked;

            if ( string.IsNullOrWhiteSpace(sIPAddress)) {
                MessageBox.Show("Please input IP Address.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Match m = Regex.Match(sIPAddress, ipAddressReg, RegexOptions.IgnoreCase);
            if (!m.Success) {
                MessageBox.Show("The format of IP Address is not valid!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Database db = new Database();
            db.SetIPAddress(sIPAddress, sDescription, isEnabled ? 1 : 0);

            this.DialogResult = DialogResult.OK;
            this.Visible = false;
        }
    }
}
