
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageIPAddress = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.dgvIPAddress = new System.Windows.Forms.DataGridView();
            this.colID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEnabled = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colEdit = new System.Windows.Forms.DataGridViewImageColumn();
            this.colDelete = new System.Windows.Forms.DataGridViewImageColumn();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.tabPageTags = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.dgvTags = new System.Windows.Forms.DataGridView();
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
            this.bindingSource3 = new System.Windows.Forms.BindingSource(this.components);
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.btnAddTag = new System.Windows.Forms.Button();
            this.cboIPAddress = new System.Windows.Forms.ComboBox();
            this.bindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.tabPageOutput = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.btnRight = new System.Windows.Forms.Button();
            this.btnLeft = new System.Windows.Forms.Button();
            this.lstTags = new System.Windows.Forms.ListBox();
            this.btnSaveOutput = new System.Windows.Forms.Button();
            this.lstOutput = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.cboOutputIP = new System.Windows.Forms.ComboBox();
            this.tabControl.SuspendLayout();
            this.tabPageIPAddress.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIPAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.tabPageTags.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTags)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource3)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource2)).BeginInit();
            this.tabPageOutput.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageIPAddress);
            this.tabControl.Controls.Add(this.tabPageTags);
            this.tabControl.Controls.Add(this.tabPageOutput);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(4, 5);
            this.tabControl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1810, 1230);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageIPAddress
            // 
            this.tabPageIPAddress.Controls.Add(this.tableLayoutPanel2);
            this.tabPageIPAddress.Location = new System.Drawing.Point(8, 39);
            this.tabPageIPAddress.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageIPAddress.Name = "tabPageIPAddress";
            this.tabPageIPAddress.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageIPAddress.Size = new System.Drawing.Size(1794, 1183);
            this.tabPageIPAddress.TabIndex = 0;
            this.tabPageIPAddress.Text = "IP Address";
            this.tabPageIPAddress.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.btnAdd, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.dgvIPAddress, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(4, 5);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1786, 1173);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnAdd.Location = new System.Drawing.Point(1602, 19);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(180, 62);
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
            this.dgvIPAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvIPAddress.Location = new System.Drawing.Point(4, 105);
            this.dgvIPAddress.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvIPAddress.Name = "dgvIPAddress";
            this.dgvIPAddress.RowHeadersWidth = 25;
            this.dgvIPAddress.RowTemplate.Height = 24;
            this.dgvIPAddress.Size = new System.Drawing.Size(1778, 1063);
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
            // tabPageTags
            // 
            this.tabPageTags.Controls.Add(this.tableLayoutPanel3);
            this.tabPageTags.Location = new System.Drawing.Point(8, 39);
            this.tabPageTags.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageTags.Name = "tabPageTags";
            this.tabPageTags.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageTags.Size = new System.Drawing.Size(1794, 1183);
            this.tabPageTags.TabIndex = 1;
            this.tabPageTags.Text = "Tags";
            this.tabPageTags.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.dgvTags, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(4, 5);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1786, 1173);
            this.tableLayoutPanel3.TabIndex = 4;
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
            this.dgvTags.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTags.Location = new System.Drawing.Point(4, 105);
            this.dgvTags.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvTags.Name = "dgvTags";
            this.dgvTags.RowHeadersWidth = 25;
            this.dgvTags.RowTemplate.Height = 24;
            this.dgvTags.Size = new System.Drawing.Size(1778, 1063);
            this.dgvTags.TabIndex = 2;
            this.dgvTags.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTags_CellClick);
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
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.btnAddTag, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.cboIPAddress, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1780, 94);
            this.tableLayoutPanel4.TabIndex = 3;
            // 
            // btnAddTag
            // 
            this.btnAddTag.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnAddTag.Location = new System.Drawing.Point(1596, 20);
            this.btnAddTag.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAddTag.Name = "btnAddTag";
            this.btnAddTag.Size = new System.Drawing.Size(180, 54);
            this.btnAddTag.TabIndex = 3;
            this.btnAddTag.Text = "Add...";
            this.btnAddTag.UseVisualStyleBackColor = true;
            this.btnAddTag.Click += new System.EventHandler(this.btnAddTag_Click);
            // 
            // cboIPAddress
            // 
            this.cboIPAddress.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cboIPAddress.DataSource = this.bindingSource2;
            this.cboIPAddress.DisplayMember = "id";
            this.cboIPAddress.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboIPAddress.FormattingEnabled = true;
            this.cboIPAddress.Location = new System.Drawing.Point(204, 30);
            this.cboIPAddress.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cboIPAddress.Name = "cboIPAddress";
            this.cboIPAddress.Size = new System.Drawing.Size(360, 33);
            this.cboIPAddress.TabIndex = 1;
            this.cboIPAddress.TextChanged += new System.EventHandler(this.cboIPAddress_TextChanged);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(74, 34);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP Address:";
            // 
            // tabPageOutput
            // 
            this.tabPageOutput.Controls.Add(this.tableLayoutPanel8);
            this.tabPageOutput.Location = new System.Drawing.Point(8, 39);
            this.tabPageOutput.Name = "tabPageOutput";
            this.tabPageOutput.Size = new System.Drawing.Size(1794, 1183);
            this.tabPageOutput.TabIndex = 2;
            this.tabPageOutput.Text = "Output";
            this.tabPageOutput.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 1;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Controls.Add(this.tableLayoutPanel5, 0, 1);
            this.tableLayoutPanel8.Controls.Add(this.tableLayoutPanel9, 0, 0);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 2;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(1794, 1183);
            this.tableLayoutPanel8.TabIndex = 1;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 4;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 101F));
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel6, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.lstTags, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.btnSaveOutput, 2, 1);
            this.tableLayoutPanel5.Controls.Add(this.lstOutput, 2, 0);
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel7, 3, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 103);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(1788, 1077);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Controls.Add(this.btnRight, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.btnLeft, 0, 2);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(746, 3);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 4;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.00001F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(194, 971);
            this.tableLayoutPanel6.TabIndex = 0;
            // 
            // btnRight
            // 
            this.btnRight.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRight.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRight.Location = new System.Drawing.Point(10, 395);
            this.btnRight.Margin = new System.Windows.Forms.Padding(10);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(174, 80);
            this.btnRight.TabIndex = 0;
            this.btnRight.Text = ">>";
            this.btnRight.UseVisualStyleBackColor = true;
            // 
            // btnLeft
            // 
            this.btnLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLeft.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLeft.Location = new System.Drawing.Point(10, 495);
            this.btnLeft.Margin = new System.Windows.Forms.Padding(10);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(174, 80);
            this.btnLeft.TabIndex = 1;
            this.btnLeft.Text = "<<";
            this.btnLeft.UseVisualStyleBackColor = true;
            // 
            // lstTags
            // 
            this.lstTags.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstTags.FormattingEnabled = true;
            this.lstTags.ItemHeight = 25;
            this.lstTags.Location = new System.Drawing.Point(30, 30);
            this.lstTags.Margin = new System.Windows.Forms.Padding(30);
            this.lstTags.Name = "lstTags";
            this.lstTags.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstTags.Size = new System.Drawing.Size(683, 917);
            this.lstTags.TabIndex = 1;
            // 
            // btnSaveOutput
            // 
            this.btnSaveOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveOutput.Location = new System.Drawing.Point(1503, 1014);
            this.btnSaveOutput.Name = "btnSaveOutput";
            this.btnSaveOutput.Size = new System.Drawing.Size(180, 60);
            this.btnSaveOutput.TabIndex = 2;
            this.btnSaveOutput.Text = "Save Output";
            this.btnSaveOutput.UseVisualStyleBackColor = true;
            // 
            // lstOutput
            // 
            this.lstOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstOutput.FormattingEnabled = true;
            this.lstOutput.ItemHeight = 25;
            this.lstOutput.Location = new System.Drawing.Point(973, 30);
            this.lstOutput.Margin = new System.Windows.Forms.Padding(30);
            this.lstOutput.Name = "lstOutput";
            this.lstOutput.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstOutput.Size = new System.Drawing.Size(683, 917);
            this.lstOutput.TabIndex = 3;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 1;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Controls.Add(this.btnUp, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.btnDown, 0, 1);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(1689, 30);
            this.tableLayoutPanel7.Margin = new System.Windows.Forms.Padding(3, 30, 3, 3);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 3;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(96, 944);
            this.tableLayoutPanel7.TabIndex = 4;
            // 
            // btnUp
            // 
            this.btnUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnUp.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnUp.Location = new System.Drawing.Point(3, 3);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(90, 74);
            this.btnUp.TabIndex = 0;
            this.btnUp.Text = "Up";
            this.btnUp.UseVisualStyleBackColor = true;
            // 
            // btnDown
            // 
            this.btnDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDown.Location = new System.Drawing.Point(3, 83);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(90, 74);
            this.btnDown.TabIndex = 1;
            this.btnDown.Text = "Down";
            this.btnDown.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(1634, 1259);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(180, 62);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.btnClose, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tabControl, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1818, 1340);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 2;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.94589F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 82.05412F));
            this.tableLayoutPanel9.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel9.Controls.Add(this.cboOutputIP, 1, 0);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 1;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(1788, 94);
            this.tableLayoutPanel9.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(195, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 25);
            this.label2.TabIndex = 0;
            this.label2.Text = "IP Address:";
            // 
            // cboOutputIP
            // 
            this.cboOutputIP.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cboOutputIP.DataSource = this.bindingSource2;
            this.cboOutputIP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOutputIP.FormattingEnabled = true;
            this.cboOutputIP.Location = new System.Drawing.Point(323, 30);
            this.cboOutputIP.Name = "cboOutputIP";
            this.cboOutputIP.Size = new System.Drawing.Size(343, 33);
            this.cboOutputIP.TabIndex = 1;
            this.cboOutputIP.SelectedIndexChanged += new System.EventHandler(this.cboOutputIP_SelectedIndexChanged);
            // 
            // frmTags
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(1818, 1340);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTags";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tags Setting";
            this.Load += new System.EventHandler(this.frmTags_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPageIPAddress.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvIPAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.tabPageTags.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTags)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource3)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource2)).EndInit();
            this.tabPageOutput.ResumeLayout(false);
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel9.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageIPAddress;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridView dgvIPAddress;
        private System.Windows.Forms.TabPage tabPageTags;
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
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TabPage tabPageOutput;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.ListBox lstTags;
        private System.Windows.Forms.Button btnSaveOutput;
        private System.Windows.Forms.ListBox lstOutput;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboOutputIP;
    }
}