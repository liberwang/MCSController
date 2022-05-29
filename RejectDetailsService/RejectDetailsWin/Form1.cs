using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RejectDetailsLib;

namespace RejectDetailsWin {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e) {
            try {
                RejectDetails.Instance.Start();
            } catch (Exception ex) {
                clsLog.addLog(ex.ToString());
                MessageBox.Show(ex.ToString(), "MCS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
