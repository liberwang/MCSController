
namespace RejectDetailsWin {
    partial class frmTags {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTags));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnAdd = new System.Windows.Forms.Button();
            this.dgvIPAddress = new System.Windows.Forms.DataGridView();
            this.colID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEnabled = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colEdit = new System.Windows.Forms.DataGridViewImageColumn();
            this.colDelete = new System.Windows.Forms.DataGridViewImageColumn();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnAddTag = new System.Windows.Forms.Button();
            this.dgvTags = new System.Windows.Forms.DataGridView();
            this.bindingSource3 = new System.Windows.Forms.BindingSource(this.components);
            this.cboIPAddress = new System.Windows.Forms.ComboBox();
            this.bindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.colTagId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTagName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTypeId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTypeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTagDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTagRWText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTagRead = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTagWriteText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTagWrite = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTagOutputText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTagOutput = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTagEdit = new System.Windows.Forms.DataGridViewImageColumn();
            this.colTagDelete = new System.Windows.Forms.DataGridViewImageColumn();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIPAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTags)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource2)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1032, 696);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnAdd);
            this.tabPage1.Controls.Add(this.dgvIPAddress);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1024, 667);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "IP Address";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(833, 16);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(120, 40);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "Add...";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // dgvIPAddress
            // 
            this.dgvIPAddress.AllowUserToAddRows = false;
            this.dgvIPAddress.AllowUserToDeleteRows = false;
            this.dgvIPAddress.AllowUserToOrderColumns = true;
            this.dgvIPAddress.AutoGenerateColumns = false;
            this.dgvIPAddress.ColumnHeadersHeight = 29;
            this.dgvIPAddress.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colID,
            this.colIP,
            this.colDescription,
            this.colEnabled,
            this.colEdit,
            this.colDelete});
            this.dgvIPAddress.DataSource = this.bindingSource1;
            this.dgvIPAddress.Location = new System.Drawing.Point(6, 66);
            this.dgvIPAddress.Name = "dgvIPAddress";
            this.dgvIPAddress.RowHeadersWidth = 25;
            this.dgvIPAddress.RowTemplate.Height = 24;
            this.dgvIPAddress.Size = new System.Drawing.Size(1012, 595);
            this.dgvIPAddress.TabIndex = 0;
            this.dgvIPAddress.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvIPAddress_CellClick);
            // 
            // colID
            // 
            this.colID.DataPropertyName = "id";
            this.colID.HeaderText = "ID";
            this.colID.MinimumWidth = 6;
            this.colID.Name = "colID";
            this.colID.ReadOnly = true;
            this.colID.Visible = false;
            this.colID.Width = 125;
            // 
            // colIP
            // 
            this.colIP.DataPropertyName = "ip_address";
            this.colIP.HeaderText = "IP Address";
            this.colIP.MinimumWidth = 6;
            this.colIP.Name = "colIP";
            this.colIP.ReadOnly = true;
            this.colIP.Width = 125;
            // 
            // colDescription
            // 
            this.colDescription.DataPropertyName = "description";
            this.colDescription.HeaderText = "Description";
            this.colDescription.MinimumWidth = 6;
            this.colDescription.Name = "colDescription";
            this.colDescription.ReadOnly = true;
            this.colDescription.Width = 250;
            // 
            // colEnabled
            // 
            this.colEnabled.DataPropertyName = "isEnabled";
            this.colEnabled.HeaderText = "Enabled";
            this.colEnabled.MinimumWidth = 6;
            this.colEnabled.Name = "colEnabled";
            this.colEnabled.ReadOnly = true;
            this.colEnabled.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colEnabled.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colEnabled.Width = 50;
            // 
            // colEdit
            // 
            this.colEdit.HeaderText = "Edit";
            this.colEdit.Image = ((System.Drawing.Image)(resources.GetObject("colEdit.Image")));
            this.colEdit.MinimumWidth = 6;
            this.colEdit.Name = "colEdit";
            this.colEdit.ReadOnly = true;
            this.colEdit.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colEdit.ToolTipText = "Edit current IP address";
            this.colEdit.Width = 40;
            // 
            // colDelete
            // 
            this.colDelete.HeaderText = "Delete";
            this.colDelete.Image = ((System.Drawing.Image)(resources.GetObject("colDelete.Image")));
            this.colDelete.MinimumWidth = 6;
            this.colDelete.Name = "colDelete";
            this.colDelete.ReadOnly = true;
            this.colDelete.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colDelete.ToolTipText = "Delete current IP address";
            this.colDelete.Width = 40;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnAddTag);
            this.tabPage2.Controls.Add(this.dgvTags);
            this.tabPage2.Controls.Add(this.cboIPAddress);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1024, 667);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Tags";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnAddTag
            // 
            this.btnAddTag.Location = new System.Drawing.Point(843, 10);
            this.btnAddTag.Name = "btnAddTag";
            this.btnAddTag.Size = new System.Drawing.Size(120, 40);
            this.btnAddTag.TabIndex = 3;
            this.btnAddTag.Text = "Add...";
            this.btnAddTag.UseVisualStyleBackColor = true;
            this.btnAddTag.Click += new System.EventHandler(this.btnAddTag_Click);
            // 
            // dgvTags
            // 
            this.dgvTags.AllowUserToAddRows = false;
            this.dgvTags.AllowUserToDeleteRows = false;
            this.dgvTags.AllowUserToOrderColumns = true;
            this.dgvTags.AutoGenerateColumns = false;
            this.dgvTags.ColumnHeadersHeight = 29;
            this.dgvTags.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTagId,
            this.colTagName,
            this.colTypeId,
            this.colTypeName,
            this.colTagDesc,
            this.colTagRWText,
            this.colTagRead,
            this.colTagWriteText,
            this.colTagWrite,
            this.colTagOutputText,
            this.colTagOutput,
            this.colTagEdit,
            this.colTagDelete});
            this.dgvTags.DataSource = this.bindingSource3;
            this.dgvTags.Location = new System.Drawing.Point(6, 68);
            this.dgvTags.Name = "dgvTags";
            this.dgvTags.RowHeadersWidth = 25;
            this.dgvTags.RowTemplate.Height = 24;
            this.dgvTags.Size = new System.Drawing.Size(1012, 593);
            this.dgvTags.TabIndex = 2;
            this.dgvTags.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTags_CellClick);
            // 
            // cboIPAddress
            // 
            this.cboIPAddress.DataSource = this.bindingSource2;
            this.cboIPAddress.DisplayMember = "id";
            this.cboIPAddress.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboIPAddress.FormattingEnabled = true;
            this.cboIPAddress.Location = new System.Drawing.Point(124, 19);
            this.cboIPAddress.Name = "cboIPAddress";
            this.cboIPAddress.Size = new System.Drawing.Size(241, 24);
            this.cboIPAddress.TabIndex = 1;
            this.cboIPAddress.TextChanged += new System.EventHandler(this.cboIPAddress_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP Address:";
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(869, 714);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(120, 40);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // colTagId
            // 
            this.colTagId.DataPropertyName = "tagId";
            this.colTagId.HeaderText = "Id";
            this.colTagId.MinimumWidth = 6;
            this.colTagId.Name = "colTagId";
            this.colTagId.ReadOnly = true;
            this.colTagId.Visible = false;
            this.colTagId.Width = 125;
            // 
            // colTagName
            // 
            this.colTagName.DataPropertyName = "tagName";
            this.colTagName.HeaderText = "Name";
            this.colTagName.MinimumWidth = 6;
            this.colTagName.Name = "colTagName";
            this.colTagName.ReadOnly = true;
            this.colTagName.Width = 180;
            // 
            // colTypeId
            // 
            this.colTypeId.DataPropertyName = "tagType";
            this.colTypeId.HeaderText = "typeId";
            this.colTypeId.MinimumWidth = 6;
            this.colTypeId.Name = "colTypeId";
            this.colTypeId.Visible = false;
            this.colTypeId.Width = 125;
            // 
            // colTypeName
            // 
            this.colTypeName.DataPropertyName = "typeName";
            this.colTypeName.HeaderText = "Type";
            this.colTypeName.MinimumWidth = 6;
            this.colTypeName.Name = "colTypeName";
            this.colTypeName.ReadOnly = true;
            this.colTypeName.Width = 50;
            // 
            // colTagDesc
            // 
            this.colTagDesc.DataPropertyName = "tagDescription";
            this.colTagDesc.HeaderText = "Description";
            this.colTagDesc.MinimumWidth = 6;
            this.colTagDesc.Name = "colTagDesc";
            this.colTagDesc.ReadOnly = true;
            this.colTagDesc.Width = 260;
            // 
            // colTagRWText
            // 
            this.colTagRWText.DataPropertyName = "tagReadText";
            this.colTagRWText.HeaderText = "Read";
            this.colTagRWText.MinimumWidth = 6;
            this.colTagRWText.Name = "colTagRWText";
            this.colTagRWText.ReadOnly = true;
            this.colTagRWText.Width = 50;
            // 
            // colTagRead
            // 
            this.colTagRead.DataPropertyName = "tagRead";
            this.colTagRead.HeaderText = "Column1";
            this.colTagRead.MinimumWidth = 6;
            this.colTagRead.Name = "colTagRead";
            this.colTagRead.ReadOnly = true;
            this.colTagRead.Visible = false;
            this.colTagRead.Width = 125;
            // 
            // colTagWriteText
            // 
            this.colTagWriteText.DataPropertyName = "tagWriteText";
            this.colTagWriteText.HeaderText = "Write";
            this.colTagWriteText.MinimumWidth = 6;
            this.colTagWriteText.Name = "colTagWriteText";
            this.colTagWriteText.ReadOnly = true;
            this.colTagWriteText.Width = 50;
            // 
            // colTagWrite
            // 
            this.colTagWrite.DataPropertyName = "tagWrite";
            this.colTagWrite.HeaderText = "Column1";
            this.colTagWrite.MinimumWidth = 6;
            this.colTagWrite.Name = "colTagWrite";
            this.colTagWrite.ReadOnly = true;
            this.colTagWrite.Visible = false;
            this.colTagWrite.Width = 125;
            // 
            // colTagOutputText
            // 
            this.colTagOutputText.DataPropertyName = "tagOutputText";
            this.colTagOutputText.HeaderText = "Output";
            this.colTagOutputText.MinimumWidth = 6;
            this.colTagOutputText.Name = "colTagOutputText";
            this.colTagOutputText.Width = 50;
            // 
            // colTagOutput
            // 
            this.colTagOutput.DataPropertyName = "tagOutput";
            this.colTagOutput.HeaderText = "Column1";
            this.colTagOutput.MinimumWidth = 6;
            this.colTagOutput.Name = "colTagOutput";
            this.colTagOutput.ReadOnly = true;
            this.colTagOutput.Visible = false;
            this.colTagOutput.Width = 125;
            // 
            // colTagEdit
            // 
            this.colTagEdit.HeaderText = "Edit";
            this.colTagEdit.Image = ((System.Drawing.Image)(resources.GetObject("colTagEdit.Image")));
            this.colTagEdit.MinimumWidth = 6;
            this.colTagEdit.Name = "colTagEdit";
            this.colTagEdit.ReadOnly = true;
            this.colTagEdit.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colTagEdit.ToolTipText = "Edit current Tag";
            this.colTagEdit.Width = 40;
            // 
            // colTagDelete
            // 
            this.colTagDelete.HeaderText = "Delete";
            this.colTagDelete.Image = ((System.Drawing.Image)(resources.GetObject("colTagDelete.Image")));
            this.colTagDelete.MinimumWidth = 6;
            this.colTagDelete.Name = "colTagDelete";
            this.colTagDelete.ReadOnly = true;
            this.colTagDelete.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colTagDelete.ToolTipText = "Delete current Tag";
            this.colTagDelete.Width = 40;
            // 
            // frmTags
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(1049, 766);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTags";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tags Setting";
            this.Load += new System.EventHandler(this.frmTags_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvIPAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTags)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridView dgvIPAddress;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIP;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescription;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colEnabled;
        private System.Windows.Forms.DataGridViewImageColumn colEdit;
        private System.Windows.Forms.DataGridViewImageColumn colDelete;
        private System.Windows.Forms.Button btnAddTag;
        private System.Windows.Forms.DataGridView dgvTags;
        private System.Windows.Forms.ComboBox cboIPAddress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.BindingSource bindingSource2;
        private System.Windows.Forms.BindingSource bindingSource3;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTagId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTagName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTypeId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTypeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTagDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTagRWText;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTagRead;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTagWriteText;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTagWrite;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTagOutputText;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTagOutput;
        private System.Windows.Forms.DataGridViewImageColumn colTagEdit;
        private System.Windows.Forms.DataGridViewImageColumn colTagDelete;
    }
}