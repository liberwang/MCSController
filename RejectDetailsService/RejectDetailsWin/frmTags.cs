﻿using RejectDetailsLib;
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
        }


        private void setIPAddressDatasource() {
            DataSet controller = db.GetIPAddressDataSet();
            this.bindingSource1.DataSource = controller.Tables[0];

            DataView dv = new DataView(controller.Tables[0]);
            dv.RowFilter = "isEnabled = True";

            this.bindingSource2.DataSource = dv;
            this.cboIPAddress.DisplayMember = "ip_address";
            this.cboIPAddress.ValueMember = "id";

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
                short iRW = (short)dgvTags.CurrentRow.Cells["colTagRW"].Value;
                

                if ( dgvTags.Columns[colindex].Name == "colTagEdit") {
                    frmTagModify tagModify = new frmTagModify(controllerId, ipAddress, sTagName,iTagType, iRW, sDescription);
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


    }
}
