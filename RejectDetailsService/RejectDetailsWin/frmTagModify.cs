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
        private readonly int ControllerId;
        private Database db;
        private readonly int TagId = -1;
        private int ParentTagId = -1;

        public clsTag TagClass
        {
            get;set;
        }

        public frmTagModify() {
            InitializeComponent();

            db = new Database();
        }

        public frmTagModify(int conId, int tagId, string ipAdd, string name="", int iType = 1, int iRead = 0, int iWrite = 0,string description = "", int iOutput = 1, string sTitle = "", int parentTagId = -1) : this() {
            this.ControllerId = conId;
            this.TagId = tagId;

            this.ParentTagId = parentTagId;
            this.ucTags.LoadTag(conId, tagId, ipAdd, name, iType, iRead, iWrite, description, sTitle);

        }

        public frmTagModify(int controllerId, string parentName, int parentId, clsTag tagClass = null) : this()
        {
            this.ParentTagId = parentId;
            this.ControllerId = controllerId;
            this.ucTags.LoadTag(tagClass, parentName);
            this.TagClass = tagClass;
            if (tagClass != null)
            {
                this.TagId = tagClass.TagId;
            }
        }

        private void frmTagModify_Load(object sender, EventArgs e) {
            this.ucTags.EditStatus = true;
        }

        private void btnSave_Click(object sender, EventArgs e) {
            string sName = this.ucTags.tagName;
            string sDescription = this.ucTags.tagDescription;
            int iType = this.ucTags.tagTypeId;
            string TypeName = this.ucTags.tagTypeName;
            int iRead = this.ucTags.tagRead;
            int iWrite = this.ucTags.tagWrite;
            string sTitle = this.ucTags.tagTitle;

            if ( string.IsNullOrWhiteSpace( sName)) {
                MessageBox.Show("Please input tag name.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            db.SetFullTags(this.ControllerId, this.TagId, sName, iType, iRead, iWrite, sDescription, 0, sTitle, this.ParentTagId);

            if (this.TagClass is null)
            {
                this.TagClass = db.GetOneTag(this.ControllerId, sName);
            }
            else
            {
                this.TagClass.TagName = sName;
                this.TagClass.Description = sDescription;
                this.TagClass.TagTypeId = iType;
                this.TagClass.TagType = TypeName;
                this.TagClass.Read = iRead;
                this.TagClass.Write = iWrite;
                this.TagClass.TagTitle = sTitle;
            }
            this.DialogResult = DialogResult.OK;
            this.Hide();
        }
    }
}
