
namespace RejectDetailsWin {
    partial class frmTagModify {
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
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.optRegular = new System.Windows.Forms.RadioButton();
            this.optRead = new System.Windows.Forms.RadioButton();
            this.txtIPAddress = new System.Windows.Forms.TextBox();
            this.txtTagName = new System.Windows.Forms.TextBox();
            this.cboType = new System.Windows.Forms.ComboBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.chkWriteBack = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(519, 514);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(180, 62);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(732, 514);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(180, 62);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(92, 64);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "IP Address:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(92, 147);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "Tag Name:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(102, 230);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 25);
            this.label3.TabIndex = 4;
            this.label3.Text = "Tag Type:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(84, 312);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(126, 25);
            this.label4.TabIndex = 5;
            this.label4.Text = "Description:";
            // 
            // optRegular
            // 
            this.optRegular.AutoSize = true;
            this.optRegular.Checked = true;
            this.optRegular.Location = new System.Drawing.Point(222, 392);
            this.optRegular.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.optRegular.Name = "optRegular";
            this.optRegular.Size = new System.Drawing.Size(118, 29);
            this.optRegular.TabIndex = 6;
            this.optRegular.TabStop = true;
            this.optRegular.Text = "Regular";
            this.optRegular.UseVisualStyleBackColor = true;
            // 
            // optRead
            // 
            this.optRead.AutoSize = true;
            this.optRead.Location = new System.Drawing.Point(222, 434);
            this.optRead.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.optRead.Name = "optRead";
            this.optRead.Size = new System.Drawing.Size(142, 29);
            this.optRead.TabIndex = 7;
            this.optRead.Text = "Read Flag";
            this.optRead.UseVisualStyleBackColor = true;
            // 
            // txtIPAddress
            // 
            this.txtIPAddress.Location = new System.Drawing.Point(222, 64);
            this.txtIPAddress.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtIPAddress.Name = "txtIPAddress";
            this.txtIPAddress.ReadOnly = true;
            this.txtIPAddress.Size = new System.Drawing.Size(628, 31);
            this.txtIPAddress.TabIndex = 9;
            // 
            // txtTagName
            // 
            this.txtTagName.Location = new System.Drawing.Point(222, 142);
            this.txtTagName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtTagName.Name = "txtTagName";
            this.txtTagName.Size = new System.Drawing.Size(628, 31);
            this.txtTagName.TabIndex = 10;
            // 
            // cboType
            // 
            this.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboType.FormattingEnabled = true;
            this.cboType.Location = new System.Drawing.Point(222, 225);
            this.cboType.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(628, 33);
            this.cboType.TabIndex = 11;
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(222, 308);
            this.txtDescription.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(628, 31);
            this.txtDescription.TabIndex = 12;
            // 
            // chkWriteBack
            // 
            this.chkWriteBack.AutoSize = true;
            this.chkWriteBack.Location = new System.Drawing.Point(486, 415);
            this.chkWriteBack.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkWriteBack.Name = "chkWriteBack";
            this.chkWriteBack.Size = new System.Drawing.Size(146, 29);
            this.chkWriteBack.TabIndex = 13;
            this.chkWriteBack.Text = "Write back";
            this.chkWriteBack.UseVisualStyleBackColor = true;
            // 
            // frmTagModify
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(984, 631);
            this.Controls.Add(this.chkWriteBack);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.cboType);
            this.Controls.Add(this.txtTagName);
            this.Controls.Add(this.txtIPAddress);
            this.Controls.Add(this.optRead);
            this.Controls.Add(this.optRegular);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTagModify";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Modify/New Tags";
            this.Load += new System.EventHandler(this.frmTagModify_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton optRegular;
        private System.Windows.Forms.RadioButton optRead;
        private System.Windows.Forms.TextBox txtIPAddress;
        private System.Windows.Forms.TextBox txtTagName;
        private System.Windows.Forms.ComboBox cboType;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.CheckBox chkWriteBack;
    }
}