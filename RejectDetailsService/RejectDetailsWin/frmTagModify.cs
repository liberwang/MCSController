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
    public partial class frmTagModify : Form {
        private int controllerId;
        private string ipAddress;
        private Database db;

        public frmTagModify() {
            InitializeComponent();
        }

        public frmTagModify(int conId, string ipAdd, string name="", int iType = 1, int iRW = 0, string description = "" ) : this() {
            this.controllerId = conId;
            this.ipAddress = ipAdd;

            this.txtTagName.Text = name;
            this.cboType.SelectedValue = iType;
            this.txtDescription.Text = description;
            if(iRW == 0) {
                this.optRegular.Checked = true;
            } else if(iRW == 1)
                this.optRead.Checked = true;
            else
                this.optWrite.Checked = true;
        }

        private void frmTagModify_Load(object sender, EventArgs e) {
            db = new Database();
            this.txtIPAddress.Text = this.ipAddress;

            this.cboType.DataSource = db.GetTagTypeDataSet().Tables[0];
            this.cboType.DisplayMember = "typeName";
            this.cboType.ValueMember = "typeid";
        }

        private void btnSave_Click(object sender, EventArgs e) {
            string sName = this.txtTagName.Text.Trim();
            string sDescription = this.txtDescription.Text.Trim();
            int iType = (int)this.cboType.SelectedValue;
            int iRW = this.optRegular.Checked ? 0 : (this.optRead.Checked ? 1 : -1);

            if ( string.IsNullOrWhiteSpace( sName)) {
                MessageBox.Show("Please input tag name.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            db.SetFullTags(this.controllerId, sName, iType, iRW, sDescription);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
