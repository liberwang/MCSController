using RejectDetailsLib;
using System;
using System.IO;
using System.Windows.Forms;

namespace RejectDetailsWin {
    public partial class frmSettings : Form {
        private System.Timers.Timer timer = new System.Timers.Timer();
        public frmSettings() {
            InitializeComponent();
        }

        private void frmSettings_Load(object sender, EventArgs e) {
            this.txtOutputFileFolder.Text = SystemKeys.FILE_FOLDER;
            this.txtOutputFileName.Text = SystemKeys.FILE_NAME;
            this.txtOutputFilePrefix.Text = SystemKeys.FILE_NAME_PREFIX;
            this.txtOutputFileExt.Text = SystemKeys.FILE_NAME_EXT;

            this.txtCopyFileFolder.Text = SystemKeys.COPY_FOLDER;
            this.txtCopyFilePrefix.Text = SystemKeys.COPY_FILE_PREFIX;
            this.txtCopyFileExt.Text = SystemKeys.COPY_FILE_EXT;

            this.nudCopyInterval.Value = SystemKeys.COPY_INTERVAL;
            this.nudVisitInterval.Value = SystemKeys.VISIT_INTERVAL;

            this.txtLogFolder.Text = SystemKeys.LOG_FILE;
            //this.txtDBConnect.Text = SystemKeys.DB_CONNECT;

            this.chkSaveToDB.Checked = SystemKeys.SAVE_TO_DB;
            this.chkSaveToFile.Checked = SystemKeys.SAVE_TO_FILE;

            
        }

        private void btnOutputFileFolder_Click(object sender, EventArgs e) {
            string curFolder = this.txtOutputFileFolder.Text.Trim();

            string newFolder = this.getSelectPath(curFolder);
            if(!string.IsNullOrWhiteSpace(newFolder)) {
                this.txtOutputFileFolder.Text = newFolder;
            }
        }

        private void btnCopyFileFolder_Click(object sender, EventArgs e) {
            string newFolder = this.getSelectPath(this.txtCopyFileFolder.Text.Trim());
            if(!string.IsNullOrWhiteSpace(newFolder)) {
                this.txtCopyFileFolder.Text = newFolder;
            }
        }

        private void btnLogFileFolder_Click(object sender, EventArgs e) {
            string newFolder = this.getSelectPath(this.txtLogFolder.Text.Trim());
            if(!string.IsNullOrWhiteSpace(newFolder)) {
                this.txtLogFolder.Text = newFolder;
            }
        }

        private string getSelectPath(string curPath) {
            using(var folder = new FolderBrowserDialog()) {

                if(!string.IsNullOrWhiteSpace(curPath) && Directory.Exists(curPath)) {
                    folder.SelectedPath = curPath;
                }
                DialogResult result = folder.ShowDialog();

                if(result == DialogResult.OK && !string.IsNullOrWhiteSpace(folder.SelectedPath)) {
                    return folder.SelectedPath;
                }
            }

            return string.Empty;
        }

        private void btnSave_Click(object sender, EventArgs e) {
            string sOutputFileFolder = this.txtOutputFileFolder.Text.Trim();
            string sOutputFileName = this.txtOutputFileName.Text.Trim();
            string sOutputFilePrefix = this.txtOutputFilePrefix.Text.Trim();
            string sOutputFileExt = this.txtOutputFileExt.Text.Trim();

            string sCopyFileFolder = this.txtCopyFileFolder.Text.Trim();
            string sCopyFilePrefix = this.txtCopyFilePrefix.Text.Trim();
            string sCopyFileExt = this.txtCopyFileExt.Text.Trim();

            int nCopyInterval = (int)this.nudCopyInterval.Value;
            int nVisitInterval = (int)this.nudVisitInterval.Value;

            string sLogFolder = this.txtLogFolder.Text.Trim();
            //string sDBConnect = this.txtDBConnect.Text.Trim();

            bool sSaveToDB = this.chkSaveToDB.Checked;
            bool sSaveToFile = this.chkSaveToFile.Checked;

            if(sOutputFileFolder != SystemKeys.FILE_FOLDER) {
                SystemKeys.setKey(SystemKeys.FILE_FOLDER_KEY, sOutputFileFolder);
            }
            if(sOutputFileName != SystemKeys.FILE_NAME) {
                SystemKeys.setKey(SystemKeys.FILE_NAME_KEY, sOutputFileName);
            }
            if(sOutputFilePrefix != SystemKeys.FILE_NAME_PREFIX) {
                SystemKeys.setKey(SystemKeys.FILE_NAME_PREFIX_KEY, sOutputFilePrefix);
            }
            if(sOutputFileExt != SystemKeys.FILE_NAME_EXT) {
                SystemKeys.setKey(SystemKeys.FILE_NAME_EXT_KEY, sOutputFileExt);
            }
            if(sCopyFileFolder != SystemKeys.COPY_FOLDER) {
                SystemKeys.setKey(SystemKeys.COPY_FOLDER_KEY, sCopyFileFolder);
            }
            if(sCopyFilePrefix != SystemKeys.COPY_FILE_PREFIX) {
                SystemKeys.setKey(SystemKeys.COPY_FILE_PREFIX_KEY, sCopyFilePrefix);
            }
            if(sCopyFileExt != SystemKeys.COPY_FILE_EXT) {
                SystemKeys.setKey(SystemKeys.COPY_FILE_EXT_KEY, sCopyFileExt);
            }
            if(nCopyInterval != SystemKeys.COPY_INTERVAL) {
                SystemKeys.setKey(SystemKeys.COPY_INTERVAL_KEY, nCopyInterval.ToString());
            }
            if(nVisitInterval != SystemKeys.VISIT_INTERVAL) {
                SystemKeys.setKey(SystemKeys.VISIT_INTERVAL_KEY, nVisitInterval.ToString());
            }
            if(sLogFolder != SystemKeys.LOG_FILE) {
                SystemKeys.setKey(SystemKeys.LOG_FILE_KEY, sLogFolder);
            }
            if(sSaveToDB != SystemKeys.SAVE_TO_DB) {
                SystemKeys.setKey(SystemKeys.SAVE_TO_DB_KEY, sSaveToDB.ToString());
            }
            if(sSaveToFile != SystemKeys.SAVE_TO_FILE) {
                SystemKeys.setKey(SystemKeys.SAVE_TO_FILE_KEY, sSaveToFile.ToString());
            }

            SystemKeys.initializeKey();
            this.Close();
        }
    }
}
