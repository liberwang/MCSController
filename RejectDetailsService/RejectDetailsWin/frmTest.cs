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
    public partial class frmTest : Form {
        private System.Timers.Timer timer = new System.Timers.Timer();

        public frmTest() {
            InitializeComponent();
            RejectDetails.Instance = null;
        }

        private void btnStart_Click(object sender, EventArgs e) {
            try {
                timer.Interval = 500;
                timer.Elapsed += new ElapsedEventHandler(this.OnTimer);
                timer.Start();
            } catch(Exception ex) {
                clsLog.addLog(ex.ToString());
                MessageBox.Show(ex.ToString(), "MCS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void OnTimer(object sender, ElapsedEventArgs args) {
            try {
                RejectDetails.Instance.Start();
            } catch(Exception e) {
                clsLog.addLog(e.Message);
            }
        }

        private void btnSingle_Click(object sender, EventArgs e) {
            RejectDetails.Instance.Start();
        }
    }
}
