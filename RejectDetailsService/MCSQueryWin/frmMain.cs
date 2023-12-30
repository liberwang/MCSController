using RejectDetailsLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
//using static System.Net.WebRequestMethods;

namespace MCSQueryWin
{
    public partial class frmMain : Form
    {
        private DataTable m_dtSource = null;
        private HashSet<string> fixedColumns = new HashSet<string>(new string[] { "SerialNumber", "startTime", "endTime" });
        private Dictionary<string, string> tagTitleNameDict;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            LoadDatabase();

            this.dataGridView.AutoGenerateColumns = true;

            // Double buffering can make DGV slow in remote desktop
            if (!System.Windows.Forms.SystemInformation.TerminalServerSession)
            {
                Type dgvType = dataGridView.GetType();
                PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                pi.SetValue(dataGridView, true, null);
            }

            this.treeViewStation.ExpandAll();

            this.dtpStart.Value = DateTime.Now.AddHours(-12);
        }

        private void LoadStationToTreeView()
        {
            int controllerId = ((clsController)this.cboIPAddress.SelectedItem).Id;

            this.treeViewStation.BeginUpdate();
            bool bChecked = this.treeViewStation.Nodes[0].Checked;

            this.treeViewStation.Nodes[0].Nodes.Clear();
            List<string> listFirstLevel = clsStation.GetStationList(controllerId);
            foreach (string level in listFirstLevel)
            {
                TreeNode tn = new TreeNode(level);
                tn.Checked = bChecked;
                this.treeViewStation.Nodes[0].Nodes.Add(tn);
            }
            this.treeViewStation.EndUpdate();
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

            this.tagTitleNameDict = clsTag.GetTagNameTitlePair();
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

            this.cboIPAddress.DataSource = clsController.GetControllerItemDataSource(withAllOption: false, bRefresh: true);
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
            this.bindingSource.DataSource = null;

            DataSet ds = new Database().GetContent(start, end, ipAddress, tagName, tagValue, serialNo);
            this.m_dtSource = ds.Tables[0];

            this.bindingSource.DataSource = this.m_dtSource;

            this.lblTotal.Text = $@"Total: {ds.Tables[0].Rows.Count} records.";
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
                    if (!string.IsNullOrWhiteSpace(fileName))
                    {
                        if (File.Exists(fileName))
                        {
                            if (MessageBox.Show($"File {fileName} is existing. Do you want to delete this file, then create a new one?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                File.Delete(fileName);
                            }
                        }

                        clsOutput clsop = clsOutput.GetOutputByProduceName();
                        foreach (DataRow dr in this.m_dtSource.Rows)
                        {
                            List<(string, string)> listValues = new List<(string, string)>();
                            for (int i = 0; i < this.m_dtSource.Columns.Count; ++i)
                            {
                                listValues.Add((dr[i].ToString(), this.m_dtSource.Columns[i].ColumnName));
                            }
                            clsop.SaveToFile(fileName, listValues, false);
                        }
                        MessageBox.Show($@"Exporting data to {fileName} is successful!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void cboIPAddress_TextChanged(object sender, EventArgs e)
        {
            LoadStationToTreeView();
        }

        private void treeViewStation_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {
                if (e.Node.Nodes.Count > 0)
                {
                    foreach (TreeNode tn in e.Node.Nodes)
                    {
                        tn.Checked = e.Node.Checked;
                    }
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            HashSet<string> stationHs = new HashSet<string>();
            foreach (TreeNode tn in this.treeViewStation.Nodes[0].Nodes)
            {
                if (tn.Checked)
                {
                    stationHs.Add(tn.Text);
                }
            }

            foreach (DataGridViewColumn col in this.dataGridView.Columns)
            {
                string columnName = col.HeaderText;
                if (fixedColumns.Contains(columnName))
                    continue;

                bool bfound = false;

                if (this.tagTitleNameDict.TryGetValue(columnName, out string val))
                {
                    columnName = val;
                }

                foreach (string filter in stationHs)
                {
                    if (columnName.StartsWith(filter))
                    {
                        bfound = true;
                        break;
                    }
                }
                col.Visible = bfound;
            }

        }
    }
}
