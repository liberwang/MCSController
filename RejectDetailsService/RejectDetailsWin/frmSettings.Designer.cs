
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
            this.txtOutputFileFolder = new System.Windows.Forms.TextBox();
            this.txtOutputFilePrefix = new System.Windows.Forms.TextBox();
            this.btnOutputFileFolder = new System.Windows.Forms.Button();
            this.txtOutputFileName = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtOutputFileExt = new System.Windows.Forms.TextBox();
            this.txtCopyFileFolder = new System.Windows.Forms.TextBox();
            this.txtRejectFilePrefix = new System.Windows.Forms.TextBox();
            this.nudVisitInterval = new System.Windows.Forms.NumericUpDown();
            this.btnCopyFileFolder = new System.Windows.Forms.Button();
            this.chkSaveToFile = new System.Windows.Forms.CheckBox();
            this.chkSaveToDB = new System.Windows.Forms.CheckBox();
            this.txtLogFolder = new System.Windows.Forms.TextBox();
            this.btnLogFileFolder = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageProd = new System.Windows.Forms.TabPage();
            this.txtProductName = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tabPageService = new System.Windows.Forms.TabPage();
            this.chkDebug = new System.Windows.Forms.CheckBox();
            this.chkMultithread = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tabPageSaveFile = new System.Windows.Forms.TabPage();
            this.label15 = new System.Windows.Forms.Label();
            this.opnExcel = new System.Windows.Forms.RadioButton();
            this.opnCSV = new System.Windows.Forms.RadioButton();
            this.label14 = new System.Windows.Forms.Label();
            this.cboTimeOfOutputFile = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tabPageLog = new System.Windows.Forms.TabPage();
            this.chkHeartBeat = new System.Windows.Forms.CheckBox();
            this.chkStatistics = new System.Windows.Forms.CheckBox();
            this.nudStatistics = new System.Windows.Forms.NumericUpDown();
            this.label16 = new System.Windows.Forms.Label();
            this.nudHeartBeat = new System.Windows.Forms.NumericUpDown();
            this.label17 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.nudCopyInterval = new System.Windows.Forms.NumericUpDown();
            this.label18 = new System.Windows.Forms.Label();
            this.nudAlarm = new System.Windows.Forms.NumericUpDown();
            this.chkAlarm = new System.Windows.Forms.CheckBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtHeartBeatTag = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudVisitInterval)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabPageProd.SuspendLayout();
            this.tabPageService.SuspendLayout();
            this.tabPageSaveFile.SuspendLayout();
            this.tabPageLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudStatistics)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeartBeat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCopyInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAlarm)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(1180, 817);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(180, 62);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(989, 817);
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
            this.label1.Location = new System.Drawing.Point(118, 88);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(190, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Output File Folder:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(126, 152);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(184, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "Output File Prefix:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(123, 216);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(185, 25);
            this.label3.TabIndex = 4;
            this.label3.Text = "Output File Name:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(150, 280);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(160, 25);
            this.label4.TabIndex = 5;
            this.label4.Text = "Output File Ext:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(134, 344);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(176, 25);
            this.label5.TabIndex = 6;
            this.label5.Text = "Copy File Folder:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(129, 408);
            this.label6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(181, 25);
            this.label6.TabIndex = 7;
            this.label6.Text = "Reject File Prefix:";
            // 
            // txtOutputFileFolder
            // 
            this.txtOutputFileFolder.Location = new System.Drawing.Point(314, 83);
            this.txtOutputFileFolder.Margin = new System.Windows.Forms.Padding(6);
            this.txtOutputFileFolder.Name = "txtOutputFileFolder";
            this.txtOutputFileFolder.Size = new System.Drawing.Size(794, 31);
            this.txtOutputFileFolder.TabIndex = 9;
            // 
            // txtOutputFilePrefix
            // 
            this.txtOutputFilePrefix.Location = new System.Drawing.Point(312, 146);
            this.txtOutputFilePrefix.Margin = new System.Windows.Forms.Padding(6);
            this.txtOutputFilePrefix.Name = "txtOutputFilePrefix";
            this.txtOutputFilePrefix.Size = new System.Drawing.Size(794, 31);
            this.txtOutputFilePrefix.TabIndex = 10;
            // 
            // btnOutputFileFolder
            // 
            this.btnOutputFileFolder.Location = new System.Drawing.Point(1124, 77);
            this.btnOutputFileFolder.Margin = new System.Windows.Forms.Padding(6);
            this.btnOutputFileFolder.Name = "btnOutputFileFolder";
            this.btnOutputFileFolder.Size = new System.Drawing.Size(48, 44);
            this.btnOutputFileFolder.TabIndex = 11;
            this.btnOutputFileFolder.Text = "...";
            this.btnOutputFileFolder.UseVisualStyleBackColor = true;
            this.btnOutputFileFolder.Click += new System.EventHandler(this.btnOutputFileFolder_Click);
            // 
            // txtOutputFileName
            // 
            this.txtOutputFileName.Location = new System.Drawing.Point(312, 210);
            this.txtOutputFileName.Margin = new System.Windows.Forms.Padding(6);
            this.txtOutputFileName.Name = "txtOutputFileName";
            this.txtOutputFileName.Size = new System.Drawing.Size(792, 31);
            this.txtOutputFileName.TabIndex = 12;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(56, 59);
            this.label8.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(275, 25);
            this.label8.TabIndex = 13;
            this.label8.Text = "Production Service Interval:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(101, 78);
            this.label10.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(162, 25);
            this.label10.TabIndex = 15;
            this.label10.Text = "Log File Folder:";
            // 
            // txtOutputFileExt
            // 
            this.txtOutputFileExt.Location = new System.Drawing.Point(312, 274);
            this.txtOutputFileExt.Margin = new System.Windows.Forms.Padding(6);
            this.txtOutputFileExt.Name = "txtOutputFileExt";
            this.txtOutputFileExt.Size = new System.Drawing.Size(792, 31);
            this.txtOutputFileExt.TabIndex = 19;
            // 
            // txtCopyFileFolder
            // 
            this.txtCopyFileFolder.Location = new System.Drawing.Point(314, 338);
            this.txtCopyFileFolder.Margin = new System.Windows.Forms.Padding(6);
            this.txtCopyFileFolder.Name = "txtCopyFileFolder";
            this.txtCopyFileFolder.Size = new System.Drawing.Size(792, 31);
            this.txtCopyFileFolder.TabIndex = 20;
            // 
            // txtRejectFilePrefix
            // 
            this.txtRejectFilePrefix.Enabled = false;
            this.txtRejectFilePrefix.Location = new System.Drawing.Point(312, 402);
            this.txtRejectFilePrefix.Margin = new System.Windows.Forms.Padding(6);
            this.txtRejectFilePrefix.Name = "txtRejectFilePrefix";
            this.txtRejectFilePrefix.Size = new System.Drawing.Size(792, 31);
            this.txtRejectFilePrefix.TabIndex = 21;
            // 
            // nudVisitInterval
            // 
            this.nudVisitInterval.Location = new System.Drawing.Point(381, 57);
            this.nudVisitInterval.Margin = new System.Windows.Forms.Padding(6);
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
            this.nudVisitInterval.Size = new System.Drawing.Size(164, 31);
            this.nudVisitInterval.TabIndex = 23;
            this.nudVisitInterval.Value = new decimal(new int[] {
            1500,
            0,
            0,
            0});
            // 
            // btnCopyFileFolder
            // 
            this.btnCopyFileFolder.Location = new System.Drawing.Point(1122, 332);
            this.btnCopyFileFolder.Margin = new System.Windows.Forms.Padding(6);
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
            this.chkSaveToFile.Location = new System.Drawing.Point(34, 36);
            this.chkSaveToFile.Margin = new System.Windows.Forms.Padding(6);
            this.chkSaveToFile.Name = "chkSaveToFile";
            this.chkSaveToFile.Size = new System.Drawing.Size(158, 29);
            this.chkSaveToFile.TabIndex = 27;
            this.chkSaveToFile.Text = "Save to File";
            this.chkSaveToFile.UseVisualStyleBackColor = true;
            this.chkSaveToFile.CheckedChanged += new System.EventHandler(this.chkSaveToFile_CheckedChanged);
            // 
            // chkSaveToDB
            // 
            this.chkSaveToDB.AutoSize = true;
            this.chkSaveToDB.Checked = true;
            this.chkSaveToDB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSaveToDB.Location = new System.Drawing.Point(34, 600);
            this.chkSaveToDB.Margin = new System.Windows.Forms.Padding(6);
            this.chkSaveToDB.Name = "chkSaveToDB";
            this.chkSaveToDB.Size = new System.Drawing.Size(152, 29);
            this.chkSaveToDB.TabIndex = 28;
            this.chkSaveToDB.Text = "Save to DB";
            this.chkSaveToDB.UseVisualStyleBackColor = true;
            // 
            // txtLogFolder
            // 
            this.txtLogFolder.Location = new System.Drawing.Point(273, 69);
            this.txtLogFolder.Margin = new System.Windows.Forms.Padding(6);
            this.txtLogFolder.Name = "txtLogFolder";
            this.txtLogFolder.Size = new System.Drawing.Size(792, 31);
            this.txtLogFolder.TabIndex = 29;
            // 
            // btnLogFileFolder
            // 
            this.btnLogFileFolder.Location = new System.Drawing.Point(1084, 66);
            this.btnLogFileFolder.Margin = new System.Windows.Forms.Padding(6);
            this.btnLogFileFolder.Name = "btnLogFileFolder";
            this.btnLogFileFolder.Size = new System.Drawing.Size(48, 44);
            this.btnLogFileFolder.TabIndex = 23;
            this.btnLogFileFolder.Text = "...";
            this.btnLogFileFolder.UseVisualStyleBackColor = true;
            this.btnLogFileFolder.Click += new System.EventHandler(this.btnLogFileFolder_Click);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageProd);
            this.tabControl.Controls.Add(this.tabPageService);
            this.tabControl.Controls.Add(this.tabPageSaveFile);
            this.tabControl.Controls.Add(this.tabPageLog);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1356, 778);
            this.tabControl.TabIndex = 30;
            // 
            // tabPageProd
            // 
            this.tabPageProd.Controls.Add(this.txtProductName);
            this.tabPageProd.Controls.Add(this.label12);
            this.tabPageProd.Location = new System.Drawing.Point(8, 39);
            this.tabPageProd.Name = "tabPageProd";
            this.tabPageProd.Size = new System.Drawing.Size(1340, 731);
            this.tabPageProd.TabIndex = 3;
            this.tabPageProd.Text = "Product";
            this.tabPageProd.UseVisualStyleBackColor = true;
            // 
            // txtProductName
            // 
            this.txtProductName.Location = new System.Drawing.Point(261, 52);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.Size = new System.Drawing.Size(649, 31);
            this.txtProductName.TabIndex = 1;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(55, 58);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(154, 25);
            this.label12.TabIndex = 0;
            this.label12.Text = "Product Name:";
            // 
            // tabPageService
            // 
            this.tabPageService.Controls.Add(this.txtHeartBeatTag);
            this.tabPageService.Controls.Add(this.label19);
            this.tabPageService.Controls.Add(this.label18);
            this.tabPageService.Controls.Add(this.nudAlarm);
            this.tabPageService.Controls.Add(this.chkAlarm);
            this.tabPageService.Controls.Add(this.label17);
            this.tabPageService.Controls.Add(this.nudHeartBeat);
            this.tabPageService.Controls.Add(this.label16);
            this.tabPageService.Controls.Add(this.nudStatistics);
            this.tabPageService.Controls.Add(this.chkStatistics);
            this.tabPageService.Controls.Add(this.chkHeartBeat);
            this.tabPageService.Controls.Add(this.chkDebug);
            this.tabPageService.Controls.Add(this.chkMultithread);
            this.tabPageService.Controls.Add(this.label7);
            this.tabPageService.Controls.Add(this.label8);
            this.tabPageService.Controls.Add(this.nudVisitInterval);
            this.tabPageService.Location = new System.Drawing.Point(8, 39);
            this.tabPageService.Name = "tabPageService";
            this.tabPageService.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageService.Size = new System.Drawing.Size(1340, 731);
            this.tabPageService.TabIndex = 0;
            this.tabPageService.Text = "Service";
            this.tabPageService.UseVisualStyleBackColor = true;
            // 
            // chkDebug
            // 
            this.chkDebug.AutoSize = true;
            this.chkDebug.Location = new System.Drawing.Point(61, 496);
            this.chkDebug.Name = "chkDebug";
            this.chkDebug.Size = new System.Drawing.Size(245, 29);
            this.chkDebug.TabIndex = 28;
            this.chkDebug.Text = "Set to Debug status. ";
            this.chkDebug.UseVisualStyleBackColor = true;
            // 
            // chkMultithread
            // 
            this.chkMultithread.AutoSize = true;
            this.chkMultithread.Location = new System.Drawing.Point(61, 441);
            this.chkMultithread.Name = "chkMultithread";
            this.chkMultithread.Size = new System.Drawing.Size(404, 29);
            this.chkMultithread.TabIndex = 27;
            this.chkMultithread.Text = "Use multithreading method in service.";
            this.chkMultithread.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(555, 59);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(130, 25);
            this.label7.TabIndex = 25;
            this.label7.Text = "milliseconds";
            // 
            // tabPageSaveFile
            // 
            this.tabPageSaveFile.Controls.Add(this.label11);
            this.tabPageSaveFile.Controls.Add(this.label9);
            this.tabPageSaveFile.Controls.Add(this.nudCopyInterval);
            this.tabPageSaveFile.Controls.Add(this.label15);
            this.tabPageSaveFile.Controls.Add(this.opnExcel);
            this.tabPageSaveFile.Controls.Add(this.opnCSV);
            this.tabPageSaveFile.Controls.Add(this.label14);
            this.tabPageSaveFile.Controls.Add(this.cboTimeOfOutputFile);
            this.tabPageSaveFile.Controls.Add(this.label13);
            this.tabPageSaveFile.Controls.Add(this.label6);
            this.tabPageSaveFile.Controls.Add(this.chkSaveToFile);
            this.tabPageSaveFile.Controls.Add(this.btnCopyFileFolder);
            this.tabPageSaveFile.Controls.Add(this.chkSaveToDB);
            this.tabPageSaveFile.Controls.Add(this.txtOutputFileName);
            this.tabPageSaveFile.Controls.Add(this.label4);
            this.tabPageSaveFile.Controls.Add(this.txtRejectFilePrefix);
            this.tabPageSaveFile.Controls.Add(this.btnOutputFileFolder);
            this.tabPageSaveFile.Controls.Add(this.txtCopyFileFolder);
            this.tabPageSaveFile.Controls.Add(this.txtOutputFilePrefix);
            this.tabPageSaveFile.Controls.Add(this.label1);
            this.tabPageSaveFile.Controls.Add(this.txtOutputFileExt);
            this.tabPageSaveFile.Controls.Add(this.label5);
            this.tabPageSaveFile.Controls.Add(this.txtOutputFileFolder);
            this.tabPageSaveFile.Controls.Add(this.label2);
            this.tabPageSaveFile.Controls.Add(this.label3);
            this.tabPageSaveFile.Location = new System.Drawing.Point(8, 39);
            this.tabPageSaveFile.Name = "tabPageSaveFile";
            this.tabPageSaveFile.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSaveFile.Size = new System.Drawing.Size(1340, 731);
            this.tabPageSaveFile.TabIndex = 1;
            this.tabPageSaveFile.Text = "Output";
            this.tabPageSaveFile.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(201, 466);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(107, 25);
            this.label15.TabIndex = 34;
            this.label15.Text = "File Type:";
            // 
            // opnExcel
            // 
            this.opnExcel.AutoSize = true;
            this.opnExcel.Location = new System.Drawing.Point(442, 466);
            this.opnExcel.Name = "opnExcel";
            this.opnExcel.Size = new System.Drawing.Size(96, 29);
            this.opnExcel.TabIndex = 33;
            this.opnExcel.Text = "Excel";
            this.opnExcel.UseVisualStyleBackColor = true;
            this.opnExcel.Visible = false;
            // 
            // opnCSV
            // 
            this.opnCSV.AutoSize = true;
            this.opnCSV.Checked = true;
            this.opnCSV.Location = new System.Drawing.Point(323, 466);
            this.opnCSV.Name = "opnCSV";
            this.opnCSV.Size = new System.Drawing.Size(86, 29);
            this.opnCSV.TabIndex = 32;
            this.opnCSV.TabStop = true;
            this.opnCSV.Text = "CSV";
            this.opnCSV.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(500, 666);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(72, 25);
            this.label14.TabIndex = 31;
            this.label14.Text = ":00:00";
            // 
            // cboTimeOfOutputFile
            // 
            this.cboTimeOfOutputFile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTimeOfOutputFile.FormattingEnabled = true;
            this.cboTimeOfOutputFile.Items.AddRange(new object[] {
            "00",
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23"});
            this.cboTimeOfOutputFile.Location = new System.Drawing.Point(386, 663);
            this.cboTimeOfOutputFile.Name = "cboTimeOfOutputFile";
            this.cboTimeOfOutputFile.Size = new System.Drawing.Size(107, 33);
            this.cboTimeOfOutputFile.TabIndex = 30;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(29, 666);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(318, 25);
            this.label13.TabIndex = 29;
            this.label13.Text = "Generate a new output file from:";
            // 
            // tabPageLog
            // 
            this.tabPageLog.Controls.Add(this.label10);
            this.tabPageLog.Controls.Add(this.btnLogFileFolder);
            this.tabPageLog.Controls.Add(this.txtLogFolder);
            this.tabPageLog.Location = new System.Drawing.Point(8, 39);
            this.tabPageLog.Name = "tabPageLog";
            this.tabPageLog.Size = new System.Drawing.Size(1340, 731);
            this.tabPageLog.TabIndex = 2;
            this.tabPageLog.Text = "Log";
            this.tabPageLog.UseVisualStyleBackColor = true;
            // 
            // chkHeartBeat
            // 
            this.chkHeartBeat.AutoSize = true;
            this.chkHeartBeat.Location = new System.Drawing.Point(61, 187);
            this.chkHeartBeat.Name = "chkHeartBeat";
            this.chkHeartBeat.Size = new System.Drawing.Size(306, 29);
            this.chkHeartBeat.TabIndex = 29;
            this.chkHeartBeat.Text = "Heart Beat Service Interval:";
            this.chkHeartBeat.UseVisualStyleBackColor = true;
            this.chkHeartBeat.CheckedChanged += new System.EventHandler(this.chkHeartBeat_CheckedChanged);
            // 
            // chkStatistics
            // 
            this.chkStatistics.AutoSize = true;
            this.chkStatistics.Location = new System.Drawing.Point(61, 124);
            this.chkStatistics.Name = "chkStatistics";
            this.chkStatistics.Size = new System.Drawing.Size(291, 29);
            this.chkStatistics.TabIndex = 30;
            this.chkStatistics.Text = "Statistics Service Interval:";
            this.chkStatistics.UseVisualStyleBackColor = true;
            this.chkStatistics.CheckedChanged += new System.EventHandler(this.chkStatistics_CheckedChanged);
            // 
            // nudStatistics
            // 
            this.nudStatistics.Location = new System.Drawing.Point(381, 124);
            this.nudStatistics.Margin = new System.Windows.Forms.Padding(6);
            this.nudStatistics.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudStatistics.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nudStatistics.Name = "nudStatistics";
            this.nudStatistics.Size = new System.Drawing.Size(164, 31);
            this.nudStatistics.TabIndex = 31;
            this.nudStatistics.Value = new decimal(new int[] {
            1500,
            0,
            0,
            0});
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(554, 126);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(130, 25);
            this.label16.TabIndex = 32;
            this.label16.Text = "milliseconds";
            // 
            // nudHeartBeat
            // 
            this.nudHeartBeat.Location = new System.Drawing.Point(381, 187);
            this.nudHeartBeat.Margin = new System.Windows.Forms.Padding(6);
            this.nudHeartBeat.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudHeartBeat.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nudHeartBeat.Name = "nudHeartBeat";
            this.nudHeartBeat.Size = new System.Drawing.Size(164, 31);
            this.nudHeartBeat.TabIndex = 33;
            this.nudHeartBeat.Value = new decimal(new int[] {
            1500,
            0,
            0,
            0});
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(554, 188);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(130, 25);
            this.label17.TabIndex = 34;
            this.label17.Text = "milliseconds";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(500, 529);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(130, 25);
            this.label11.TabIndex = 37;
            this.label11.Text = "milliseconds";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(164, 529);
            this.label9.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(144, 25);
            this.label9.TabIndex = 35;
            this.label9.Text = "Copy Interval:";
            // 
            // nudCopyInterval
            // 
            this.nudCopyInterval.Location = new System.Drawing.Point(323, 527);
            this.nudCopyInterval.Margin = new System.Windows.Forms.Padding(6);
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
            this.nudCopyInterval.Size = new System.Drawing.Size(164, 31);
            this.nudCopyInterval.TabIndex = 36;
            this.nudCopyInterval.Value = new decimal(new int[] {
            31000,
            0,
            0,
            0});
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(554, 304);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(130, 25);
            this.label18.TabIndex = 37;
            this.label18.Text = "milliseconds";
            // 
            // nudAlarm
            // 
            this.nudAlarm.Location = new System.Drawing.Point(381, 304);
            this.nudAlarm.Margin = new System.Windows.Forms.Padding(6);
            this.nudAlarm.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudAlarm.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nudAlarm.Name = "nudAlarm";
            this.nudAlarm.Size = new System.Drawing.Size(164, 31);
            this.nudAlarm.TabIndex = 36;
            this.nudAlarm.Value = new decimal(new int[] {
            1500,
            0,
            0,
            0});
            // 
            // chkAlarm
            // 
            this.chkAlarm.AutoSize = true;
            this.chkAlarm.Location = new System.Drawing.Point(61, 303);
            this.chkAlarm.Name = "chkAlarm";
            this.chkAlarm.Size = new System.Drawing.Size(259, 29);
            this.chkAlarm.TabIndex = 35;
            this.chkAlarm.Text = "Alarm Service Interval:";
            this.chkAlarm.UseVisualStyleBackColor = true;
            this.chkAlarm.CheckedChanged += new System.EventHandler(this.chkAlarm_CheckedChanged);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(210, 246);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(157, 25);
            this.label19.TabIndex = 38;
            this.label19.Text = "HeartBeat Tag:";
            // 
            // txtHeartBeatTag
            // 
            this.txtHeartBeatTag.Location = new System.Drawing.Point(381, 246);
            this.txtHeartBeatTag.Name = "txtHeartBeatTag";
            this.txtHeartBeatTag.Size = new System.Drawing.Size(436, 31);
            this.txtHeartBeatTag.TabIndex = 39;
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(1380, 904);
            this.Controls.Add(this.tabControl);
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
            this.tabControl.ResumeLayout(false);
            this.tabPageProd.ResumeLayout(false);
            this.tabPageProd.PerformLayout();
            this.tabPageService.ResumeLayout(false);
            this.tabPageService.PerformLayout();
            this.tabPageSaveFile.ResumeLayout(false);
            this.tabPageSaveFile.PerformLayout();
            this.tabPageLog.ResumeLayout(false);
            this.tabPageLog.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudStatistics)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeartBeat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCopyInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAlarm)).EndInit();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.TextBox txtOutputFileFolder;
        private System.Windows.Forms.TextBox txtOutputFilePrefix;
        private System.Windows.Forms.Button btnOutputFileFolder;
        private System.Windows.Forms.TextBox txtOutputFileName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtOutputFileExt;
        private System.Windows.Forms.TextBox txtCopyFileFolder;
        private System.Windows.Forms.TextBox txtRejectFilePrefix;
        private System.Windows.Forms.NumericUpDown nudVisitInterval;
        private System.Windows.Forms.Button btnCopyFileFolder;
        private System.Windows.Forms.CheckBox chkSaveToFile;
        private System.Windows.Forms.CheckBox chkSaveToDB;
        private System.Windows.Forms.TextBox txtLogFolder;
        private System.Windows.Forms.Button btnLogFileFolder;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageService;
        private System.Windows.Forms.TabPage tabPageSaveFile;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TabPage tabPageLog;
        private System.Windows.Forms.TabPage tabPageProd;
        private System.Windows.Forms.TextBox txtProductName;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cboTimeOfOutputFile;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox chkMultithread;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.RadioButton opnExcel;
        private System.Windows.Forms.RadioButton opnCSV;
        private System.Windows.Forms.CheckBox chkDebug;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.NumericUpDown nudHeartBeat;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.NumericUpDown nudStatistics;
        private System.Windows.Forms.CheckBox chkStatistics;
        private System.Windows.Forms.CheckBox chkHeartBeat;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown nudCopyInterval;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.NumericUpDown nudAlarm;
        private System.Windows.Forms.CheckBox chkAlarm;
        private System.Windows.Forms.TextBox txtHeartBeatTag;
        private System.Windows.Forms.Label label19;
    }
}

