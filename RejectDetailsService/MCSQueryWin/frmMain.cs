using RejectDetailsLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace MCSQueryWin
{
    public partial class frmMain : Form
    {
        private DataTable m_dtSource = null;

        public frmMain()
        {
            InitializeComponent();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            DateTime startTime = this.dtpStart.Value;
            DateTime endtime = this.dtpEnd.Value;
            string start = startTime.ToString("yyyy-MM-dd HH:mm:ss");
            string end = endtime.ToString("yyyy-MM-dd HH:mm:ss");

            string ipAddress = ((clsController)this.cboIPAddress.SelectedItem).IpAddress;
            string tagName = this.txtTagName.Text.Trim();
            string tagValue = this.txtTagValue.Text.Trim();
            string serialNo = this.txtSerialNumber.Text.Trim();

            if (start.CompareTo(end) > 0)
            {
                MessageBox.Show("Start time should be early than end time.", "Query", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }


            DataSet ds = new Database().GetContent(start, end, ipAddress, tagName, tagValue, serialNo);
            this.m_dtSource = ds.Tables[0];

            this.bindingSource.DataSource = this.m_dtSource;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            LoadDatabase();



            this.dataGridView.AutoGenerateColumns = true;
        }

        private void LoadDatabase()
        {
            if (clsKeys.DB_LIST != null && clsKeys.DB_LIST.Length > 0)
                this.cboDatabase.Items.AddRange(clsKeys.DB_LIST);

            if (this.cboDatabase.Items.Count > 0)
                this.cboDatabase.SelectedIndex = 0;
            else
            {
                this.EnabledFunctions();
            }
        }

        private void EnabledFunctions(bool bEnabled = false)
        {
            this.btnExport.Enabled = bEnabled;
            this.btnQuery.Enabled = bEnabled;
        }

        private void LoadIpAddress()
        {
            this.cboIPAddress.DisplayMember = "Description";
            this.cboIPAddress.ValueMember = "IpAddress";

            this.cboIPAddress.DataSource = clsController.GetControllerItemDataSource(bRefresh: true);
            this.cboIPAddress.SelectedIndex = 0;
        }

        private void cboDatabase_SelectedIndexChanged(object sender, EventArgs e)
        {
            string dbName = this.cboDatabase.SelectedItem.ToString();

            try
            {
                SystemKeys.SetDBConnect(string.Format(SystemKeys.DB_REMOTE, dbName));
                EnabledFunctions(true);
                LoadIpAddress();
            }
            catch (Exception ex)
            {
                MessageBox.Show($@"Cannot connect to database '{dbName}'. Please double check if this database is running!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                EnabledFunctions(false);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (this.bindingSource.Count > 0)
            {
                this.saveFileDialog.CheckPathExists = true;
                this.saveFileDialog.AddExtension = true;
                this.saveFileDialog.DefaultExt = "csv";
                this.saveFileDialog.OverwritePrompt = false;
                this.saveFileDialog.CheckPathExists = true;
                this.saveFileDialog.FileName = SystemKeys.getFullFileName();
                if (this.saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName = this.saveFileDialog.FileName;
                    clsOutput clsop = clsOutput.GetOutputByProduceName();
                    foreach( DataRow dr in this.m_dtSource.Rows )
                    {
                        List<(string, string)> listValues = new List<(string, string)>();
                        for( int i = 0; i < this.m_dtSource.Columns.Count; ++i )
                        {
                            listValues.Add((dr[i].ToString(), this.m_dtSource.Columns[i].ColumnName));
                        }
                        clsop.SaveToFile(fileName, listValues, false);
                    }
                    MessageBox.Show($@"Export to {fileName} is done!", "MCS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
