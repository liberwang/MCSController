
namespace RejectDetailsWin {
    partial class frmSettings {
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
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtOutputFileFolder = new System.Windows.Forms.TextBox();
            this.txtOutputFilePrefix = new System.Windows.Forms.TextBox();
            this.btnOutputFileFolder = new System.Windows.Forms.Button();
            this.txtOutputFileName = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtOutputFileExt = new System.Windows.Forms.TextBox();
            this.txtCopyFileFolder = new System.Windows.Forms.TextBox();
            this.txtCopyFilePrefix = new System.Windows.Forms.TextBox();
            this.txtCopyFileExt = new System.Windows.Forms.TextBox();
            this.nudVisitInterval = new System.Windows.Forms.NumericUpDown();
            this.nudCopyInterval = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnCopyFileFolder = new System.Windows.Forms.Button();
            this.chkSaveToFile = new System.Windows.Forms.CheckBox();
            this.chkSaveToDB = new System.Windows.Forms.CheckBox();
            this.txtLogFolder = new System.Windows.Forms.TextBox();
            this.btnLogFileFolder = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.nudVisitInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCopyInterval)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(1172, 941);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(180, 62);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(981, 941);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(180, 62);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(66, 53);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(190, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Output File Folder:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(74, 117);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(184, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "Output File Prefix:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(70, 181);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(185, 25);
            this.label3.TabIndex = 4;
            this.label3.Text = "Output File Name:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(90, 244);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(160, 25);
            this.label4.TabIndex = 5;
            this.label4.Text = "Output File Ext:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(78, 48);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(176, 25);
            this.label5.TabIndex = 6;
            this.label5.Text = "Copy File Folder:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(84, 114);
            this.label6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(170, 25);
            this.label6.TabIndex = 7;
            this.label6.Text = "Copy File Prefix:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(106, 175);
            this.label7.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(146, 25);
            this.label7.TabIndex = 8;
            this.label7.Text = "Copy File Ext:";
            // 
            // txtOutputFileFolder
            // 
            this.txtOutputFileFolder.Location = new System.Drawing.Point(262, 48);
            this.txtOutputFileFolder.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtOutputFileFolder.Name = "txtOutputFileFolder";
            this.txtOutputFileFolder.Size = new System.Drawing.Size(794, 31);
            this.txtOutputFileFolder.TabIndex = 9;
            // 
            // txtOutputFilePrefix
            // 
            this.txtOutputFilePrefix.Location = new System.Drawing.Point(260, 111);
            this.txtOutputFilePrefix.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtOutputFilePrefix.Name = "txtOutputFilePrefix";
            this.txtOutputFilePrefix.Size = new System.Drawing.Size(794, 31);
            this.txtOutputFilePrefix.TabIndex = 10;
            // 
            // btnOutputFileFolder
            // 
            this.btnOutputFileFolder.Location = new System.Drawing.Point(1072, 42);
            this.btnOutputFileFolder.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnOutputFileFolder.Name = "btnOutputFileFolder";
            this.btnOutputFileFolder.Size = new System.Drawing.Size(48, 44);
            this.btnOutputFileFolder.TabIndex = 11;
            this.btnOutputFileFolder.Text = "...";
            this.btnOutputFileFolder.UseVisualStyleBackColor = true;
            this.btnOutputFileFolder.Click += new System.EventHandler(this.btnOutputFileFolder_Click);
            // 
            // txtOutputFileName
            // 
            this.txtOutputFileName.Location = new System.Drawing.Point(260, 175);
            this.txtOutputFileName.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtOutputFileName.Name = "txtOutputFileName";
            this.txtOutputFileName.Size = new System.Drawing.Size(792, 31);
            this.txtOutputFileName.TabIndex = 12;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(190, 680);
            this.label8.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(135, 25);
            this.label8.TabIndex = 13;
            this.label8.Text = "Visit Interval:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(740, 680);
            this.label9.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(144, 25);
            this.label9.TabIndex = 14;
            this.label9.Text = "Copy Interval:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(166, 759);
            this.label10.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(162, 25);
            this.label10.TabIndex = 15;
            this.label10.Text = "Log File Folder:";
            // 
            // txtOutputFileExt
            // 
            this.txtOutputFileExt.Location = new System.Drawing.Point(260, 239);
            this.txtOutputFileExt.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtOutputFileExt.Name = "txtOutputFileExt";
            this.txtOutputFileExt.Size = new System.Drawing.Size(792, 31);
            this.txtOutputFileExt.TabIndex = 19;
            // 
            // txtCopyFileFolder
            // 
            this.txtCopyFileFolder.Location = new System.Drawing.Point(260, 42);
            this.txtCopyFileFolder.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtCopyFileFolder.Name = "txtCopyFileFolder";
            this.txtCopyFileFolder.Size = new System.Drawing.Size(792, 31);
            this.txtCopyFileFolder.TabIndex = 20;
            // 
            // txtCopyFilePrefix
            // 
            this.txtCopyFilePrefix.Location = new System.Drawing.Point(260, 108);
            this.txtCopyFilePrefix.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtCopyFilePrefix.Name = "txtCopyFilePrefix";
            this.txtCopyFilePrefix.Size = new System.Drawing.Size(792, 31);
            this.txtCopyFilePrefix.TabIndex = 21;
            // 
            // txtCopyFileExt
            // 
            this.txtCopyFileExt.Location = new System.Drawing.Point(260, 169);
            this.txtCopyFileExt.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtCopyFileExt.Name = "txtCopyFileExt";
            this.txtCopyFileExt.Size = new System.Drawing.Size(792, 31);
            this.txtCopyFileExt.TabIndex = 22;
            // 
            // nudVisitInterval
            // 
            this.nudVisitInterval.Location = new System.Drawing.Point(338, 675);
            this.nudVisitInterval.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.nudVisitInterval.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudVisitInterval.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nudVisitInterval.Name = "nudVisitInterval";
            this.nudVisitInterval.Size = new System.Drawing.Size(240, 31);
            this.nudVisitInterval.TabIndex = 23;
            this.nudVisitInterval.Value = new decimal(new int[] {
            1500,
            0,
            0,
            0});
            // 
            // nudCopyInterval
            // 
            this.nudCopyInterval.Location = new System.Drawing.Point(896, 675);
            this.nudCopyInterval.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.nudCopyInterval.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudCopyInterval.Minimum = new decimal(new int[] {
            31000,
            0,
            0,
            0});
            this.nudCopyInterval.Name = "nudCopyInterval";
            this.nudCopyInterval.Size = new System.Drawing.Size(240, 31);
            this.nudCopyInterval.TabIndex = 24;
            this.nudCopyInterval.Value = new decimal(new int[] {
            31000,
            0,
            0,
            0});
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtOutputFileName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtOutputFileFolder);
            this.groupBox1.Controls.Add(this.txtOutputFileExt);
            this.groupBox1.Controls.Add(this.txtOutputFilePrefix);
            this.groupBox1.Controls.Add(this.btnOutputFileFolder);
            this.groupBox1.Location = new System.Drawing.Point(78, 36);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox1.Size = new System.Drawing.Size(1214, 325);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Output";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnCopyFileFolder);
            this.groupBox2.Controls.Add(this.txtCopyFileFolder);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtCopyFilePrefix);
            this.groupBox2.Controls.Add(this.txtCopyFileExt);
            this.groupBox2.Location = new System.Drawing.Point(78, 381);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox2.Size = new System.Drawing.Size(1214, 258);
            this.groupBox2.TabIndex = 26;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Copy";
            // 
            // btnCopyFileFolder
            // 
            this.btnCopyFileFolder.Location = new System.Drawing.Point(1068, 36);
            this.btnCopyFileFolder.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnCopyFileFolder.Name = "btnCopyFileFolder";
            this.btnCopyFileFolder.Size = new System.Drawing.Size(48, 44);
            this.btnCopyFileFolder.TabIndex = 20;
            this.btnCopyFileFolder.Text = "...";
            this.btnCopyFileFolder.UseVisualStyleBackColor = true;
            this.btnCopyFileFolder.Click += new System.EventHandler(this.btnCopyFileFolder_Click);
            // 
            // chkSaveToFile
            // 
            this.chkSaveToFile.AutoSize = true;
            this.chkSaveToFile.Location = new System.Drawing.Point(338, 831);
            this.chkSaveToFile.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.chkSaveToFile.Name = "chkSaveToFile";
            this.chkSaveToFile.Size = new System.Drawing.Size(158, 29);
            this.chkSaveToFile.TabIndex = 27;
            this.chkSaveToFile.Text = "Save to File";
            this.chkSaveToFile.UseVisualStyleBackColor = true;
            // 
            // chkSaveToDB
            // 
            this.chkSaveToDB.AutoSize = true;
            this.chkSaveToDB.Checked = true;
            this.chkSaveToDB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSaveToDB.Location = new System.Drawing.Point(542, 831);
            this.chkSaveToDB.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.chkSaveToDB.Name = "chkSaveToDB";
            this.chkSaveToDB.Size = new System.Drawing.Size(152, 29);
            this.chkSaveToDB.TabIndex = 28;
            this.chkSaveToDB.Text = "Save to DB";
            this.chkSaveToDB.UseVisualStyleBackColor = true;
            // 
            // txtLogFolder
            // 
            this.txtLogFolder.Location = new System.Drawing.Point(338, 750);
            this.txtLogFolder.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtLogFolder.Name = "txtLogFolder";
            this.txtLogFolder.Size = new System.Drawing.Size(792, 31);
            this.txtLogFolder.TabIndex = 29;
            // 
            // btnLogFileFolder
            // 
            this.btnLogFileFolder.Location = new System.Drawing.Point(1149, 747);
            this.btnLogFileFolder.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnLogFileFolder.Name = "btnLogFileFolder";
            this.btnLogFileFolder.Size = new System.Drawing.Size(48, 44);
            this.btnLogFileFolder.TabIndex = 23;
            this.btnLogFileFolder.Text = "...";
            this.btnLogFileFolder.UseVisualStyleBackColor = true;
            this.btnLogFileFolder.Click += new System.EventHandler(this.btnLogFileFolder_Click);
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(1380, 1033);
            this.Controls.Add(this.btnLogFileFolder);
            this.Controls.Add(this.txtLogFolder);
            this.Controls.Add(this.chkSaveToDB);
            this.Controls.Add(this.chkSaveToFile);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.nudCopyInterval);
            this.Controls.Add(this.nudVisitInterval);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "System Settings";
            this.Load += new System.EventHandler(this.frmSettings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudVisitInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCopyInterval)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtOutputFileFolder;
        private System.Windows.Forms.TextBox txtOutputFilePrefix;
        private System.Windows.Forms.Button btnOutputFileFolder;
        private System.Windows.Forms.TextBox txtOutputFileName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtOutputFileExt;
        private System.Windows.Forms.TextBox txtCopyFileFolder;
        private System.Windows.Forms.TextBox txtCopyFilePrefix;
        private System.Windows.Forms.TextBox txtCopyFileExt;
        private System.Windows.Forms.NumericUpDown nudVisitInterval;
        private System.Windows.Forms.NumericUpDown nudCopyInterval;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnCopyFileFolder;
        private System.Windows.Forms.CheckBox chkSaveToFile;
        private System.Windows.Forms.CheckBox chkSaveToDB;
        private System.Windows.Forms.TextBox txtLogFolder;
        private System.Windows.Forms.Button btnLogFileFolder;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
    }
}

