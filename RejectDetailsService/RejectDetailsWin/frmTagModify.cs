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
        private readonly int controllerId;
        //private string ipAddress;
        private Database db;
        private readonly int TagId = -1;

        public frmTagModify() {
            InitializeComponent();

            db = new Database();

            this.cboType.DataSource = db.GetTagTypeDataSet().Tables[0];
            this.cboType.DisplayMember = "typeName";
            this.cboType.ValueMember = "typeid";
        }

        public frmTagModify(int conId, int tagId, string ipAdd, string name="", int iType = 1, int iRead = 0, int iWrite = 0,string description = "", int iOutput = 1, string sTitle = "") : this() {
            this.controllerId = conId;
            this.TagId = tagId;

            this.txtIPAddress.Text = ipAdd;
            this.txtTagName.Text = name;
            this.cboType.SelectedValue = iType;
            this.txtDescription.Text = description;
            this.optRegular.Checked = iRead == 0;
            this.optRead.Checked = iRead == 1;
            this.chkWriteBack.Checked = iWrite == 1;

            this.txtTitle.Text = sTitle;
        }

        private void frmTagModify_Load(object sender, EventArgs e) {

        }

        private void btnSave_Click(object sender, EventArgs e) {
            string sName = this.txtTagName.Text.Trim();
            string sDescription = this.txtDescription.Text.Trim();
            int iType = (int)this.cboType.SelectedValue;
            int iRead = this.optRegular.Checked ? 0 : 1;
            int iWrite = this.chkWriteBack.Checked ? 1 : 0;
            //int iOutput = this.chkOutput.Checked ? 1 : 0;
            string sTitle = this.txtTitle.Text.Trim();

            if ( string.IsNullOrWhiteSpace( sName)) {
                MessageBox.Show("Please input tag name.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            db.SetFullTags(this.controllerId, this.TagId, sName, iType, iRead, iWrite, sDescription, 0, sTitle);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
