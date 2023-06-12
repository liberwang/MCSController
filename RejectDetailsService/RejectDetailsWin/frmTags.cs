using RejectDetailsLib;
using System;
using System.Data;
using System.Windows.Forms;

namespace RejectDetailsWin {
    public partial class frmTags : Form {
        private Database db = new Database();
        public frmTags() {
            InitializeComponent();
        }


        private void frmTags_Load(object sender, EventArgs e) {

            setIPAddressDatasource();
            //this.dgvIPAddress.AutoGenerateColumns = true;

            if (! SystemKeys.SAVE_TO_FILE)
                this.tabControl.TabPages.RemoveAt(2);

            this.btnUp.Text = "\u25B2";
            this.btnDown.Text = "\u25BC";

            this.btnRight.Text = "\u25B6";
            this.btnLeft.Text = "\u25C0";
        }


        private void setIPAddressDatasource() {
            DataSet controller = db.GetIPAddressDataSet();
            this.bindingSource1.DataSource = controller.Tables[0];

            DataView dv = new DataView(controller.Tables[0]);
            dv.RowFilter = "isEnabled = True";

            this.bindingSource2.DataSource = dv;
            this.cboIPAddress.DisplayMember = "description";
            this.cboIPAddress.ValueMember = "id";

            this.cboOutputIP.DisplayMember = "description";
            this.cboOutputIP.ValueMember = "id";

            this.cboIPAddress.SelectedIndex = -1;
            this.cboOutputIP.SelectedIndex = -1;

            this.refreshTags();
        }

        private void dgvIPAddress_CellClick(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex != -1 ) {
                int colindex = e.ColumnIndex;
                int id = (int)(dgvIPAddress.CurrentRow.Cells["colID"].Value);
                string ipAddress = dgvIPAddress.CurrentRow.Cells["colIP"].Value.ToString();
                string description = dgvIPAddress.CurrentRow.Cells["colDescription"].Value.ToString();
                bool enabled = dgvIPAddress.CurrentRow.Cells["colEnabled"].Value.ToString() == bool.TrueString;

                if ( dgvIPAddress.Columns[colindex].Name == "colEdit" ) {
                    frmIPModify mod = new frmIPModify(ipAddress, description, enabled);
                    mod.ShowDialog();

                    if(mod.DialogResult == DialogResult.OK) {
                        setIPAddressDatasource();
                    }
                    mod.Close();
                } else if (dgvIPAddress.Columns[colindex].Name == "colDelete") {
                    if ( MessageBox.Show( $"Are you sure to delete IP Address and its tags: {ipAddress}?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                        db.DeleteIPAddress(id);
                        setIPAddressDatasource();
                    }
                }
            }
        }

        private void dgvTags_CellClick(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex != -1) {
                int colindex = e.ColumnIndex;

                int controllerId = (int)cboIPAddress.SelectedValue;
                string ipAddress = cboIPAddress.Text;

                int id = (int)dgvTags.CurrentRow.Cells["colTagId"].Value;
                string sTagName = dgvTags.CurrentRow.Cells["colTagName"].Value.ToString();
                int iTagType = (int)dgvTags.CurrentRow.Cells["colTypeId"].Value;
                string sDescription = dgvTags.CurrentRow.Cells["colTagDesc"].Value.ToString();
                short iRead = (short)dgvTags.CurrentRow.Cells["colTagRead"].Value;
                short iWrite = (short)dgvTags.CurrentRow.Cells["colTagWrite"].Value;
                //short iOutput = (short)dgvTags.CurrentRow.Cells["colTagOutput"].Value;

                if ( dgvTags.Columns[colindex].Name == "colTagEdit") {
                    frmTagModify tagModify = new frmTagModify(controllerId, ipAddress, sTagName,iTagType, iRead, iWrite, sDescription);
                    tagModify.ShowDialog();

                    if (tagModify.DialogResult == DialogResult.OK) {
                        refreshTags();
                    }
                    tagModify.Close();
                } else if (dgvTags.Columns[colindex].Name == "colTagDelete") {
                    if(MessageBox.Show($"Are you sure to delete tag: {sTagName}?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                        db.DeleteFullTag(id);
                        refreshTags();
                    }
                }

            }
        }

        private void btnAdd_Click(object sender, EventArgs e) {
            frmIPModify mod = new frmIPModify();
            mod.ShowDialog();

            if (mod.DialogResult == DialogResult.OK) {
                setIPAddressDatasource();
            }
            mod.Close();
        }

        //private void cboIPAddress_SelectedIndexChanged(object sender, EventArgs e) {
        //    //this.refreshTags();
        //    System.Diagnostics.Debug.WriteLine("selectedindexchanged");
        //}

        //private void cboIPAddress_SelectionChangeCommitted(object sender, EventArgs e) {
        //    //this.refreshTags();
        //    System.Diagnostics.Debug.WriteLine("selectioinchangedcommited");
        //}

        private void refreshTags() {
            if(cboIPAddress.SelectedIndex < 0) {
                this.bindingSource3.DataSource = null;
                return;
            }
                
            int controllerId = (int)cboIPAddress.SelectedValue;
            //string ipAddress = cboIPAddress.SelectedText;

            this.bindingSource3.DataSource = db.GetFullTags(controllerId).Tables[0];
        }

        private void btnAddTag_Click(object sender, EventArgs e) {
            int controllerId = (int)cboIPAddress.SelectedValue;
            string ipAddress = cboIPAddress.Text;

            frmTagModify tagform = new frmTagModify(controllerId, ipAddress);
            tagform.ShowDialog();

            if (tagform.DialogResult == DialogResult.OK) {
                refreshTags();
            }

            //tagform.Close();
        }

        private void cboIPAddress_TextChanged(object sender, EventArgs e) {
            //System.Diagnostics.Debug.WriteLine("textChanged");
            refreshTags();
        }

        private void cboOutputIP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboOutputIP.SelectedIndex >= 0 )
            {
                int controllerId = (int)cboOutputIP.SelectedValue;
                DataSet dsUnselected = db.GetUnselectedTags(controllerId);

                if (dsUnselected != null && dsUnselected.Tables.Count > 0)
                {
                    this.lstTags.Items.Clear();
                    this.lstTags.DataSource = dsUnselected.Tables[0];

                    this.lstTags.DisplayMember = "tagName";
                    this.lstTags.ValueMember = "tagId";
               }

                DataSet dsSelect = db.GetSelectedOutput(controllerId);

                if (dsSelect?.Tables?.Count > 0)
                {
                    this.lstOutput.Items.Clear();
                    this.lstOutput.DataSource = dsSelect.Tables[0];
                    
                    this.lstOutput.DisplayMember = "tagName";
                    this.lstOutput.ValueMember = "tagId";

                }
            }

        }
    }
}
