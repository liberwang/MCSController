using RejectDetailsLib;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace RejectDetailsWin
{
    public partial class ucTags : UserControl
    {
        private clsTag TagClass {  get; set; }
        private int TagId {  get; set; }    

        private int ControllerId { get; set; }

        public string tagParent
        {
            get { return this.txtParent.Text; }
            set { this.txtParent.Text = value;}
        }

        public string tagName {  
            get { return this.txtName.Text.Trim(); } 
            set { this.txtName.Text = value;}
        }

        public string tagDescription
        {
            get { return this.txtDescription.Text.Trim(); }
            set { this.txtDescription.Text = value; }
        }

        public string tagTitle
        {
            get { return txtTitle.Text.Trim(); }
            set { this.txtTitle.Text = value; }
        }

        public int tagTypeId
        {
            get { return this.cboType.Items.Count > 0 ? (int)this.cboType.SelectedValue : -1; }
            set
            {
                if (this.cboType.Items.Count > 0)
                {
                    this.cboType.SelectedValue = value;
                }
            }
        }

        public string tagTypeName
        {
            get { return this.cboType.SelectedText; }
        }

        public int tagRead
        {
            get { return this.chkRead.Checked ? 1 : 0; } 
            set { this.chkRead.Checked = value == 1 ; }
        }

        public int tagWrite
        {
            get { return this.chkWrite.Checked ? 1 : 0; }
            set { this.chkWrite.Checked = value == 1; }
        }

        public bool EditStatus
        {
            set
            {
                RefreshStatus(value);
            }
        }


        public ucTags()
        {
            InitializeComponent();
            
        }

        public void LoadTag(clsTag tag, string parentName = "")
        {
            this.InitializeTagTypeList();

            this.lblParent.Text = "Parent:";

            if (tag == null || tag.TagId == -1)
            {
                CleanUp();
            }
            else
            {
                tagName = tag.TagName;
                tagDescription = tag.Description;
                tagTitle = tag.TagTitle;
                tagTypeId = tag.TagTypeId;
                tagRead = tag.Read;
                tagWrite = tag.Write;

                TagClass = tag;
            }
            tagParent = parentName;
        }

        public void LoadTag(int conId, int tagId, string ipAdd, string sName = "", int iTagType = 1, int iRead = 0, int iWrite = 0, string sDescription = "", string sTitle = "")
        {
            this.InitializeTagTypeList();

            this.lblParent.Text = "Controller Name:";
            
            ControllerId = conId;
            TagId = tagId;

            tagParent = ipAdd;
            tagName = sName;
            tagDescription = sDescription;
            tagTitle = sTitle;
            tagTypeId = iTagType;
            tagRead = iRead;
            tagWrite = iWrite;
        }

        private void InitializeTagTypeList()
        {
            this.cboType.DataSource = new Database().GetTagTypeDataSet().Tables[0];
            this.cboType.DisplayMember = "typeName";
            this.cboType.ValueMember = "typeId";

            this.cboType.SelectedIndex = 0;
        }

        private void CleanUp()
        {
            tagParent = "";
            tagName = "";
            tagDescription = "";
            tagTitle = "";
            tagTypeId = -1;
            tagRead = 0;
            tagWrite = 0;
        }

        private void RefreshStatus( bool editStatus)
        {
            this.txtName.ReadOnly = !editStatus;
            this.txtDescription.ReadOnly = !editStatus;
            this.txtTitle.ReadOnly = !editStatus;
            this.cboType.Enabled = editStatus;
            this.chkRead.Enabled = editStatus;
            this.chkWrite.Enabled = editStatus;
        }
    }
}
