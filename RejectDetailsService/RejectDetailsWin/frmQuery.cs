using RejectDetailsLib;
using System;
using System.Data;
using System.Windows.Forms;

namespace RejectDetailsWin {
    public partial class frmQuery : Form {
        public frmQuery() {
            InitializeComponent();
        }

        private void btnQuery_Click(object sender, EventArgs e) {
            DateTime startTime = this.dtpStart.Value;
            DateTime endtime = this.dtpEnd.Value;
            string start = startTime.ToString("yyyy-MM-dd HH:mm:ss");
            string end = endtime.ToString("yyyy-MM-dd HH:mm:ss");

            string ipAddress = (this.cboIPAddress.SelectedItem as dynamic).Value;
            string tagName = this.txtTagName.Text.Trim();
            string tagValue = this.txtTagValue.Text.Trim();
            string serialNo = this.txtSerialNumber.Text.Trim();

            if ( start.CompareTo(end) > 0 ) {
                MessageBox.Show("Start time should be less than end time.", "Query", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }


            DataSet ds = new Database().GetContent(start, end, ipAddress, tagName, tagValue, serialNo);

            this.bindingSource.DataSource = ds.Tables[0];
        }

        private void frmQuery_Load(object sender, EventArgs e) {
            this.cboIPAddress.DisplayMember = "Text";
            this.cboIPAddress.ValueMember = "Value";

            this.cboIPAddress.Items.Add(new { Text = "All", Value = "All" });
            DataSet ds = new Database().GetIPAddressDataSet();
            foreach( DataRow dr in ds.Tables[0].Rows ) {
                this.cboIPAddress.Items.Add(new { Text = dr[2].ToString(), Value = dr[1].ToString() });
            }

            this.cboIPAddress.SelectedIndex = 0;

            this.dataGridView.AutoGenerateColumns = true;
        }
    }
}
