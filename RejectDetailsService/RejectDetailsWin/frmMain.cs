using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using RejectDetailsLib;

namespace RejectDetailsWin {
    public partial class frmMain : Form {


        public frmMain() {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void btnTest_Click(object sender, EventArgs e) {
            frmTest test = new frmTest();
            test.ShowDialog();
        }

        private void btnSetting_Click(object sender, EventArgs e) {
            frmSettings settings = new frmSettings();
            settings.ShowDialog();
        }

        private void btnTags_Click(object sender, EventArgs e) {
            frmTags tag = new frmTags();
            tag.ShowDialog();
        }

        private void frmMain_Load(object sender, EventArgs e) {
            this.cboDB.Items.Add(clsKeys.LOCAL_HOST_STRING);
            this.cboDB.Items.AddRange(clsKeys.DB_LIST);

            this.cboDB.SelectedIndex = 0;
        }

        private void cboDB_SelectedIndexChanged(object sender, EventArgs e) {
            string dbName = this.cboDB.SelectedItem.ToString();

            if(dbName == clsKeys.LOCAL_HOST_STRING) {
                SystemKeys.DB_CONNECT = SystemKeys.DB_LOCAL;
            } else {
                SystemKeys.DB_CONNECT = string.Format( SystemKeys.DB_REMOTE, dbName );
            }

            SystemKeys.initializeKey();
        }

        private void btnQuery_Click(object sender, EventArgs e) {
            frmQuery frmQry = new frmQuery();
            frmQry.ShowDialog();
        }
    }
}
