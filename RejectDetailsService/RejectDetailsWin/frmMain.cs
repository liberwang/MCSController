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

namespace RejectDetailsWin
{
    public partial class frmMain : Form
    {


        public frmMain()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            frmTest test = new frmTest();
            test.ShowDialog();
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            frmSettings settings = new frmSettings();
            settings.ShowDialog();
        }

        private void btnTags_Click(object sender, EventArgs e)
        {
            frmTags tag = new frmTags();
            tag.ShowDialog();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            frmQuery frmQry = new frmQuery();
            frmQry.ShowDialog();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            if (clsKeys.DB_LIST != null && clsKeys.DB_LIST.Length > 0)
                this.cboDB.Items.AddRange(clsKeys.DB_LIST);

            if (this.cboDB.Items.Count > 0)
                this.cboDB.SelectedIndex = 0;
            else
            {
                this.EnabledFunctions();
            }
        }

        private void EnabledFunctions(bool enabled = false)
        {
            this.btnChart.Enabled = enabled;
            this.btnQuery.Enabled = enabled;
            this.btnSetting.Enabled = enabled;
            this.btnTags.Enabled = enabled;
            this.btnTest.Enabled = enabled;
        }

        private void cboDB_SelectedIndexChanged(object sender, EventArgs e)
        {
            string dbName = this.cboDB.SelectedItem.ToString();

            try
            {
                SystemKeys.SetDBConnect(string.Format(SystemKeys.DB_REMOTE, dbName));
                EnabledFunctions(true);
            } catch (Exception ex )
            {
                MessageBox.Show( $@"Cannot connect to database '{dbName}'. Please double check if this database is running!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error );
                EnabledFunctions(false);
            }

        }
    }
}
