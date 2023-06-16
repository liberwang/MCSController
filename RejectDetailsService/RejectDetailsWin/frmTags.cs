﻿using RejectDetailsLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RejectDetailsWin
{
    public partial class frmTags : Form
    {
        private Database db = new Database();
        public frmTags()
        {
            InitializeComponent();
        }


        private void frmTags_Load(object sender, EventArgs e)
        {

            setIPAddressDatasource();
            //this.dgvIPAddress.AutoGenerateColumns = true;

            if (!SystemKeys.SAVE_TO_FILE)
                this.tabControl.TabPages.RemoveAt(2);

            this.btnUp.Text = "\u25B2";
            this.btnDown.Text = "\u25BC";

            this.btnRight.Text = "\u25B6";
            this.btnLeft.Text = "\u25C0";
        }


        private void setIPAddressDatasource()
        {
            //DataSet controller = db.GetIPAddressDataSet();
            //this.bindingSource1.DataSource = controller.Tables[0];

            //DataView dv = new DataView(controller.Tables[0]);
            //dv.RowFilter = "isEnabled = True";

            //this.bindingSource2.DataSource = dv;
            //this.cboIPAddress.DisplayMember = "description";
            //this.cboIPAddress.ValueMember = "id";

            //this.cboOutputIP.DisplayMember = "description";
            //this.cboOutputIP.ValueMember = "id";

            clsController.RefreshController();

            this.bindingSourceIPAddress.DataSource = clsController.GetControllerDataTable();
            //this.bindingSourceIPdropdown.DataSource = clsController.GetControllerDataSource();

            this.cboIPAddress.DataSource = clsController.GetControllerItemDataSource(false);
            //this.cboIPAddress.DisplayMember = "Text";
            //this.cboIPAddress.ValueMember = "Value";

            this.cboOutputIP.DataSource = clsController.GetControllerItemDataSource(false);
            //this.cboOutputIP.DisplayMember = "Text";
            //this.cboOutputIP.ValueMember = "Value";

            this.cboIPAddress.SelectedIndex = -1;
            this.cboOutputIP.SelectedIndex = -1;

            this.refreshTags();
        }

        private void dgvIPAddress_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                int colindex = e.ColumnIndex;
                int id = (int)(dgvIPAddress.CurrentRow.Cells["colID"].Value);
                string ipAddress = dgvIPAddress.CurrentRow.Cells["colIP"].Value.ToString();
                string description = dgvIPAddress.CurrentRow.Cells["colDescription"].Value.ToString();
                bool enabled = dgvIPAddress.CurrentRow.Cells["colEnabled"].Value.ToString() == bool.TrueString;

                if (colindex > -1)
                {
                    if (dgvIPAddress.Columns[colindex].Name == "colEdit")
                    {
                        frmIPModify mod = new frmIPModify(ipAddress, description, enabled);
                        mod.ShowDialog();

                        if (mod.DialogResult == DialogResult.OK)
                        {
                            setIPAddressDatasource();
                        }
                        mod.Close();
                    }
                    else if (dgvIPAddress.Columns[colindex].Name == "colDelete")
                    {
                        if (MessageBox.Show($"Are you sure to delete IP Address and its tags: {ipAddress}?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            db.DeleteIPAddress(id);
                            setIPAddressDatasource();
                        }
                    }
                }
            }
        }

        private void dgvTags_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
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

                if (colindex > -1)
                {
                    if (dgvTags.Columns[colindex].Name == "colTagEdit")
                    {
                        frmTagModify tagModify = new frmTagModify(controllerId, ipAddress, sTagName, iTagType, iRead, iWrite, sDescription);
                        tagModify.ShowDialog();

                        if (tagModify.DialogResult == DialogResult.OK)
                        {
                            refreshTags();
                        }
                        tagModify.Close();
                    }
                    else if (dgvTags.Columns[colindex].Name == "colTagDelete")
                    {
                        if (MessageBox.Show($"Are you sure to delete tag: {sTagName}?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            db.DeleteFullTag(id);
                            refreshTags();
                        }
                    }
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmIPModify mod = new frmIPModify();
            mod.ShowDialog();

            if (mod.DialogResult == DialogResult.OK)
            {
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

        private void refreshTags()
        {
            if (cboIPAddress.SelectedIndex < 0)
            {
                this.bindingSource3.DataSource = null;
                return;
            }

            int controllerId = (int)cboIPAddress.SelectedValue;
            //string ipAddress = cboIPAddress.SelectedText;

            this.bindingSource3.DataSource = db.GetFullTags(controllerId).Tables[0];

        }

        private void btnAddTag_Click(object sender, EventArgs e)
        {
            int controllerId = (int)cboIPAddress.SelectedValue;
            string ipAddress = cboIPAddress.Text;

            frmTagModify tagform = new frmTagModify(controllerId, ipAddress);
            tagform.ShowDialog();

            if (tagform.DialogResult == DialogResult.OK)
            {
                refreshTags();
            }

            //tagform.Close();
        }

        private void cboIPAddress_TextChanged(object sender, EventArgs e)
        {
            refreshTags();
        }

        private void cboOutputIP_TextChanged(object sender, EventArgs e)
        {
            this.lstTags.Items.Clear();
            this.lstOutput.Items.Clear();

            if (cboOutputIP.SelectedIndex >= 0)
            {
                int controllerId = (int)cboOutputIP.SelectedValue;

                List<clsTag> listUnselect = db.GetUnselectedTags(controllerId);

                if (listUnselect != null && listUnselect.Any())
                {
                    lstTags.Items.AddRange(listUnselect.ToArray());
                }

                List<clsTag> listSelected = db.GetSelectedOutput(controllerId);

                if (listSelected.Any())
                {
                    lstOutput.Items.AddRange(listSelected.ToArray());
                }
            }
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            foreach (object obj in lstTags.SelectedItems)
            {
                this.lstOutput.Items.Add(obj);
            }

            for (int i = lstTags.Items.Count - 1; i >= 0; --i)
            {
                if (lstTags.SelectedItems.Contains(lstTags.Items[i]))
                {
                    lstTags.Items.Remove(lstTags.Items[i]);
                }
            }
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            foreach (object obj in lstOutput.SelectedItems)
            {
                this.lstTags.Items.Add(obj);
            }

            for (int i = lstOutput.Items.Count - 1; i >= 0; --i)
            {
                if (lstOutput.SelectedItems.Contains(lstOutput.Items[i]))
                {
                    lstOutput.Items.Remove(lstOutput.Items[i]);
                }
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            if (lstOutput.SelectedItems.Count > 0)
            {               
                int ind = lstOutput.SelectedIndex;
                if (ind > 0)
                {
                    object obj = lstOutput.SelectedItem;

                    lstOutput.Items.RemoveAt(ind);
                    lstOutput.Items.Insert(ind - 1, obj);
                    lstOutput.SelectedIndex = ind - 1;
                }
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if ( lstOutput.SelectedItems.Count > 0)
            {
                int ind = lstOutput.SelectedIndex;
                if ( ind < lstOutput.Items.Count -1 )
                {
                    object obj = lstOutput.SelectedItem;

                    lstOutput.Items.RemoveAt(ind);
                    lstOutput.Items.Insert(ind + 1, obj);
                    lstOutput.SelectedIndex = ind + 1;
                }
            }
        }

        private void btnSaveOutput_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            foreach( clsTag obj in lstOutput.Items )
            {
                sb.Append(obj.TagId).Append(",");
            }

            string idList = sb.ToString();
            int controllerId = (int)this.cboOutputIP.SelectedValue;

            db.SetSelectedOutput(controllerId, idList);

        }
    }
}
