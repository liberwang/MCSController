﻿using RejectDetailsLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace RejectDetailsWin
{
    public partial class frmTags : Form
    {
        private Database db = new Database();

        // for dragdrop of output 
        private int indexOfItemUnderMouseToDrag;
        private int indexOfItemUnderMouseToDrop;
        private Rectangle dragBoxFromMouseDown;

        public frmTags()
        {
            InitializeComponent();
        }


        private void frmTags_Load(object sender, EventArgs e)
        {

            setIPAddressDatasource();
            //this.dgvIPAddress.AutoGenerateColumns = true;

            //if (!SystemKeys.SAVE_TO_FILE)
            //    this.tabControl.TabPages.RemoveAt(2);

            this.btnUp.Text = "\u25B2";
            this.btnDown.Text = "\u25BC";

            this.btnRight.Text = "\u25B6";
            this.btnLeft.Text = "\u25C0";

            // Double buffering can make DGV slow in remote desktop
            if (!System.Windows.Forms.SystemInformation.TerminalServerSession)
            {
                Type dgvType = dgvTags.GetType();
                PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
                  BindingFlags.Instance | BindingFlags.NonPublic);
                pi.SetValue(dgvTags, true, null);
            }
        }


        private void setIPAddressDatasource()
        {
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
                short iOutput = (short)dgvTags.CurrentRow.Cells["colTagOutput"].Value;
                string sTitle = dgvTags.CurrentRow.Cells["colTagTitle"].Value.ToString();

                if (colindex > -1)
                {
                    if (dgvTags.Columns[colindex].Name == "colTagEdit")
                    {
                        frmTagModify tagModify = new frmTagModify(controllerId, id, ipAddress, sTagName, iTagType, iRead, iWrite, sDescription, iOutput, sTitle);
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

        private void btnAddController_Click(object sender, EventArgs e)
        {
            frmIPModify mod = new frmIPModify();
            mod.ShowDialog();

            if (mod.DialogResult == DialogResult.OK)
            {
                setIPAddressDatasource();
            }
            mod.Close();
        }

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
            if (this.cboIPAddress.SelectedIndex >= 0)
            {
                int controllerId = (int)cboIPAddress.SelectedValue;
                string ipAddress = cboIPAddress.Text;

                frmTagModify tagform = new frmTagModify(controllerId, -1, ipAddress);
                tagform.ShowDialog();

                if (tagform.DialogResult == DialogResult.OK)
                {
                    refreshTags();
                }
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
            if (lstOutput.SelectedIndices.Count > 0)
            {
                lstOutput.BeginUpdate();
                List<int> listIndex = lstOutput.SelectedIndices.Cast<int>().ToList();

                if (listIndex[0] > 0)
                {
                    foreach (int ind in listIndex)
                    {
                        object obj = lstOutput.Items[ind];

                        lstOutput.Items.RemoveAt(ind);
                        lstOutput.Items.Insert(ind - 1, obj);
                        lstOutput.SetSelected(ind - 1, true);
                    }
                }
                lstOutput.EndUpdate();
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if( lstOutput.SelectedIndices.Count > 0 )
            {
                lstOutput.BeginUpdate();
                List<int> listIndex = lstOutput.SelectedIndices.Cast<int>().ToList();

                int lastIndex = lstOutput.Items.Count - 1;

                if (listIndex[listIndex.Count - 1 ] < lastIndex)
                {
                    for( int i = listIndex.Count - 1; i >= 0; -- i )
                    {
                        int ind = listIndex[i];
                        object obj = lstOutput.Items[ind];
                        lstOutput.Items.RemoveAt(ind);
                        lstOutput.Items.Insert(ind + 1, obj);
                        lstOutput.SetSelected(ind+1, true);
                    }
                }
                lstOutput.EndUpdate();
            }
        }

        private void btnSaveOutput_Click(object sender, EventArgs e)
        {
            if (this.cboOutputIP.SelectedIndex >= 0)
            {
                StringBuilder sb = new StringBuilder();

                foreach (clsTag obj in lstOutput.Items)
                {
                    sb.Append(obj.TagId).Append(",");
                }

                string idList = sb.ToString();
                int controllerId = (int)this.cboOutputIP.SelectedValue;

                db.SetSelectedOutput(controllerId, idList);

                MessageBox.Show("Output setting saves successfully!", "Output Setting", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {

            if (cboIPAddress.SelectedIndex >= 0 && MessageBox.Show("Are you sure to delete all tags?", "Clear up", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int controllerId = (int)this.cboIPAddress.SelectedValue;

                db.CleanUpFallTag(controllerId);

                refreshTags();
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (cboIPAddress.SelectedIndex >= 0)
            {
                this.openFileDialog.Filter = "txt files (*.txt)|*.txt|csv files (*.csv)|*.csv|All files (*.*)|*.*";
                this.openFileDialog.FilterIndex = 2;
                this.openFileDialog.RestoreDirectory = true;
                this.openFileDialog.FileName = "";

                if (this.openFileDialog.ShowDialog() == DialogResult.OK && this.openFileDialog.FileName != "")
                {
                    string sFileName = this.openFileDialog.FileName;

                    Stream fileStream = openFileDialog.OpenFile();
                    List<string[]> insertList = new List<string[]>();
                    Dictionary<string, int> dicType = db.GetTagTypeDictionary();

                    using (StreamReader sr = new StreamReader(fileStream))
                    {
                        int lineNo = 0;
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            ++lineNo;
                            line = line.Trim();


                            // ignore comment or empty line
                            if (line.StartsWith("--") || string.IsNullOrWhiteSpace(line))
                                continue;

                            // tag name,tagType,description,read,write,title
                            string[] strField = line.Split(new char[] { ',' });
                            if (strField.Length != 6)
                            {
                                MessageBox.Show($"(#{lineNo}) Field in the file is not corrent. Fields are Name (string), Type (int), Description (string), Read (int), Write (int), and Title (string).", "Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            // 0: tag Name
                            if (string.IsNullOrWhiteSpace(strField[0]))
                            {
                                MessageBox.Show($"(#{lineNo}) The name of Tag can not be empty!", "Upload error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            else
                            {
                                strField[0] = "'" + strField[0].Trim() + "'";
                            }

                            // 1: tag Type
                            strField[1] = strField[1].Trim();
                            if (!dicType.ContainsKey(strField[1]))
                            {
                                MessageBox.Show($"(#{lineNo}) The type of Tag is not found!", "Upload error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            else
                            {
                                strField[1] = dicType[strField[1]].ToString();
                            }

                            // 2: tag Description 
                            strField[2] = "'" + strField[2].Trim() + "'";

                            // 3: read tag
                            if (int.TryParse(strField[3].Trim(), out int iRead))
                            {
                                strField[3] = iRead > 0 ? "1" : "0";
                            }
                            else
                            {
                                strField[3] = "0";
                            }

                            // 4: write tag 
                            if (int.TryParse(strField[4].Trim(), out int iWrite))
                            {
                                strField[4] = iWrite > 0 ? "1" : "0";
                            }
                            else
                            {
                                strField[4] = "0";
                            }

                            // 5: tag title 
                            strField[5] = "'" + strField[5].Trim() + "'";

                            insertList.Add(strField);
                        }
                    }

                    if (insertList.Count > 0)
                    {
                        string returnMsg = db.UploadFullTag((int)cboIPAddress.SelectedValue, insertList);
                        if (string.IsNullOrWhiteSpace(returnMsg))
                        {
                            MessageBox.Show($@"Upload {sFileName} is successful.", "Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            this.refreshTags();
                        }
                        else
                        {
                            MessageBox.Show($@"Error: {returnMsg}", "Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    else
                    {
                        MessageBox.Show($@"{sFileName} is empty.", "Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }

                }

            }
        }

        private void lstTags_MouseDown(object sender, MouseEventArgs e)
        {
            // Get the index of the item the mouse is below.
            indexOfItemUnderMouseToDrag = lstTags.IndexFromPoint(e.X, e.Y);

            if (indexOfItemUnderMouseToDrag != ListBox.NoMatches)
            {
                // Remember the point where the mouse down occurred. The DragSize indicates
                // the size that the mouse can move before a drag event should be started.
                Size dragSize = SystemInformation.DragSize;

                // Create a rectangle using the DragSize, with the mouse position being
                // at the center of the rectangle.
                dragBoxFromMouseDown = new Rectangle(
                    new Point(e.X - (dragSize.Width / 2),
                              e.Y - (dragSize.Height / 2)),
                    dragSize);
            }
            else
            {
                // Reset the rectangle if the mouse is not over an item in the ListBox.
                dragBoxFromMouseDown = Rectangle.Empty;
            }
        }

        private void lstTags_MouseUp(object sender, MouseEventArgs e)
        {
            // Reset the drag rectangle when the mouse button is raised.
            dragBoxFromMouseDown = Rectangle.Empty;
        }

        private void lstTags_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                // If the mouse moves outside the rectangle, start the drag.
                if (dragBoxFromMouseDown != Rectangle.Empty &&
                    !dragBoxFromMouseDown.Contains(e.X, e.Y))
                {
                    // Create custom cursors for the drag-and-drop operation.
                    // The screenOffset is used to account for any desktop bands 
                    // that may be at the top or left side of the screen when 
                    // determining when to cancel the drag drop operation.
                    //screenOffset = SystemInformation.WorkingArea.Location;

                    // Proceed with the drag-and-drop, passing in the list item.
                    DragDropEffects dropEffect = lstTags.DoDragDrop(lstTags.Items[indexOfItemUnderMouseToDrag], DragDropEffects.Move);

                    // If the drag operation was a move then remove the item.
                    if (dropEffect == DragDropEffects.Move)
                    {
                        lstTags.Items.RemoveAt(indexOfItemUnderMouseToDrag);

                        // Selects the previous item in the list as long as the list has an item.
                        if (indexOfItemUnderMouseToDrag > 0)
                            lstTags.SelectedIndex = indexOfItemUnderMouseToDrag - 1;

                        else if (lstTags.Items.Count > 0)
                            // Selects the first item.
                            lstTags.SelectedIndex = 0;
                    }
                }

            }
        }

        private void lstTags_DoubleClick(object sender, EventArgs e)
        {
            if (indexOfItemUnderMouseToDrag != ListBox.NoMatches)
            {
                this.btnRight_Click(lstTags, null);
            }
        }

        private void lstOutput_DragOver(object sender, DragEventArgs e)
        {
            // Determine whether string data exists in the drop data. If not, then
            // the drop effect reflects that the drop cannot occur.
            if (!e.Data.GetDataPresent(typeof(clsTag)))
            {
                e.Effect = DragDropEffects.None;
                return;
            }

            e.Effect = DragDropEffects.Move;

            // Get the index of the item the mouse is below. 

            // The mouse locations are relative to the screen, so they must be 
            // converted to client coordinates.
            indexOfItemUnderMouseToDrop =
                lstOutput.IndexFromPoint(lstOutput.PointToClient(new Point(e.X, e.Y)));
        }

        private void lstOutput_DragDrop(object sender, DragEventArgs e)
        {
            // Ensure that the list item index is contained in the data.
            if (e.Data.GetDataPresent(typeof(clsTag)))
            {
                Object item = e.Data.GetData(typeof(clsTag));

                // Perform drag-and-drop, depending upon the effect.
                if (e.Effect == DragDropEffects.Move)
                {
                    // Insert the item.
                    if (indexOfItemUnderMouseToDrop != ListBox.NoMatches)
                        lstOutput.Items.Insert(indexOfItemUnderMouseToDrop, item);
                    else
                        lstOutput.Items.Add(item);
                }
            }
        }
    }
}
