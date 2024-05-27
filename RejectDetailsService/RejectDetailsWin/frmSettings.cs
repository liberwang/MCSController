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
            // first tab
            this.txtProductName.Text = SystemKeys.PRODUCT_NAME;

            // second tab 

            this.nudVisitInterval.Value = SystemKeys.VISIT_INTERVAL;

            this.chkHeartBeat.Checked = SystemKeys.HEARTBEAT_SERVICE_ENABLE;
            this.txtHeartBeatTag.Text = SystemKeys.HEARTBEAT_SERVICE_TAGNAME;
            this.nudHeartBeat.Value = SystemKeys.HEARTBEAT_SERVICE_INTERVAL;
            chkHeartBeat_CheckedChanged(null, null);

            this.chkStatistics.Checked = SystemKeys.STATISTICS_SERVICE_ENABLE;
            this.nudStatistics.Value = SystemKeys.STATISTICS_SERVICE_INTERVAL;
            chkStatistics_CheckedChanged(null, null);   

            this.chkAlarm.Checked = SystemKeys.ALARM_SERVICE_ENABLE;
            this.nudAlarm.Value = SystemKeys.ALARM_SERVICE_INTERVAL;
            chkAlarm_CheckedChanged(null, null);

            this.chkMultithread.Checked = SystemKeys.USE_MULTITHREADING_SERVICE;
            this.chkDebug.Checked = SystemKeys.IN_DEBUGING;

            // save file/db tab
            this.chkSaveToDB.Checked = SystemKeys.SAVE_TO_DB;
            this.chkSaveToFile.Checked = SystemKeys.SAVE_TO_FILE;
            this.txtOutputFileFolder.Text = SystemKeys.FILE_FOLDER;
            this.txtOutputFileName.Text = SystemKeys.FILE_NAME;
            this.txtOutputFilePrefix.Text = SystemKeys.FILE_NAME_PREFIX;
            this.txtOutputFileExt.Text = SystemKeys.FILE_NAME_EXT;

            this.txtCopyFileFolder.Text = SystemKeys.COPY_FOLDER;
            this.txtRejectFilePrefix.Text = SystemKeys.REJECT_FILE_PREFIX;
            this.nudCopyInterval.Value = SystemKeys.COPY_INTERVAL;
            //this.txtCopyFileExt.Text = SystemKeys.COPY_FILE_EXT;
            this.SaveToFileStatus(SystemKeys.SAVE_TO_FILE);

            this.cboTimeOfOutputFile.SelectedItem = SystemKeys.GENERATE_OUTPUT_FILE_TIME;

            // Log tab 
            this.txtLogFolder.Text = SystemKeys.LOG_FILE;

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
            string sProductName = this.txtProductName.Text.Trim();
            string sOutputFileFolder = this.txtOutputFileFolder.Text.Trim();
            string sOutputFileName = this.txtOutputFileName.Text.Trim();
            string sOutputFilePrefix = this.txtOutputFilePrefix.Text.Trim();
            string sOutputFileExt = this.txtOutputFileExt.Text.Trim();

            string sCopyFileFolder = this.txtCopyFileFolder.Text.Trim();
            //string sCopyFilePrefix = this.txtRejectFilePrefix.Text.Trim();
            //string sCopyFileExt = this.txtCopyFileExt.Text.Trim();
            string sRejectFilePrefix = this.txtRejectFilePrefix.Text.Trim();

            int nCopyInterval = (int)this.nudCopyInterval.Value;
            int nVisitInterval = (int)this.nudVisitInterval.Value;

            string sLogFolder = this.txtLogFolder.Text.Trim();
            //string sDBConnect = this.txtDBConnect.Text.Trim();

            bool sSaveToDB = this.chkSaveToDB.Checked;
            bool sSaveToFile = this.chkSaveToFile.Checked;

            string sGenerateOutputTime = this.cboTimeOfOutputFile.SelectedItem.ToString();

            bool bUseMultithreading = this.chkMultithread.Checked;
            bool isDebug = this.chkDebug.Checked;

            bool isHeartBeatEnable = this.chkHeartBeat.Checked;
            int nHeartBeatInterval = (int)this.nudHeartBeat.Value;
            string sHeartBeatTag = this.txtHeartBeatTag.Text.Trim();

            bool isStatisticsEnable = this.chkStatistics.Checked;
            int nStatisticsInterval = (int)this.nudStatistics.Value;

            bool isAlarmEnabled = this.chkAlarm.Checked;
            int nAlarmInterval =(int)this.nudAlarm.Value;
            

            if (sProductName != SystemKeys.PRODUCT_NAME)
            {
                SystemKeys.setKey(SystemKeys.PRODUCT_NAME_KEY, sProductName);
            }
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
            //if(sCopyFilePrefix != SystemKeys.COPY_FILE_PREFIX) {
            //    SystemKeys.setKey(SystemKeys.COPY_FILE_PREFIX_KEY, sCopyFilePrefix);
            //}
            //if(sCopyFileExt != SystemKeys.COPY_FILE_EXT) {
            //    SystemKeys.setKey(SystemKeys.COPY_FILE_EXT_KEY, sCopyFileExt);
            //}
            if(sRejectFilePrefix != SystemKeys.REJECT_FILE_PREFIX)
            {
                SystemKeys.setKey(SystemKeys.REJECT_FILE_PREFIX_KEY, sRejectFilePrefix);
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
            if(sGenerateOutputTime != SystemKeys.GENERATE_OUTPUT_FILE_TIME)
            {
                SystemKeys.setKey(SystemKeys.GENERATE_OUTPUT_FILE_TIME_KEY, sGenerateOutputTime);
            }
            if(bUseMultithreading != SystemKeys.USE_MULTITHREADING_SERVICE)
            {
                SystemKeys.setKey(SystemKeys.USE_MULTITHREADING_SERVICE_KEY, bUseMultithreading.ToString());
            }
            if (isDebug != SystemKeys.IN_DEBUGING)
            {
                SystemKeys.setKey(SystemKeys.IN_DEBUGING_KEY, isDebug.ToString());
            }
            // heartbeat
            if( isHeartBeatEnable != SystemKeys.HEARTBEAT_SERVICE_ENABLE)
            {
                SystemKeys.setKey(SystemKeys.HEARTBEAT_SERVICE_ENABLE_KEY, isHeartBeatEnable.ToString());
            }
            if (sHeartBeatTag != SystemKeys.HEARTBEAT_SERVICE_TAGNAME)
            {
                SystemKeys.setKey(SystemKeys.HEARTBEAT_SERVICE_TAGNAME_KEY, sHeartBeatTag);
            }
            if (nHeartBeatInterval != SystemKeys.HEARTBEAT_SERVICE_INTERVAL)
            {
                SystemKeys.setKey(SystemKeys.HEARTBEAT_SERVICE_INTERVAL_KEY, nHeartBeatInterval.ToString());
            }
            //statistics
            if (isStatisticsEnable != SystemKeys.STATISTICS_SERVICE_ENABLE)
            {
                SystemKeys.setKey(SystemKeys.STATISTICS_SERVICE_ENABLE_KEY, isStatisticsEnable.ToString());
            }
            if (nStatisticsInterval != SystemKeys.STATISTICS_SERVICE_INTERVAL)
            {
                SystemKeys.setKey(SystemKeys.STATISTICS_SERVICE_INTERVAL_KEY, nStatisticsInterval.ToString());
            }
            // alarm
            if (isAlarmEnabled != SystemKeys.ALARM_SERVICE_ENABLE)
            {
                SystemKeys.setKey(SystemKeys.ALARM_SERVICE_ENABLE_KEY, isAlarmEnabled.ToString());
            }
            if (nAlarmInterval != SystemKeys.ALARM_SERVICE_INTERVAL)
            {
                SystemKeys.setKey(SystemKeys.ALARM_SERVICE_INTERVAL_KEY, nAlarmInterval.ToString());
            }
            SystemKeys.initializeKey();
            this.Close();
        }

        private void chkSaveToFile_CheckedChanged(object sender, EventArgs e)
        {
            this.SaveToFileStatus(chkSaveToFile.Checked);
        }

        private void SaveToFileStatus(bool isEnabled )
        {
            this.txtCopyFileFolder.Enabled = isEnabled;
            this.txtOutputFileExt.Enabled = isEnabled;
            this.txtOutputFileFolder.Enabled = isEnabled;
            this.txtOutputFileName.Enabled = isEnabled;
            this.txtOutputFilePrefix.Enabled = isEnabled;
            this.txtRejectFilePrefix.Enabled = isEnabled;
            this.btnOutputFileFolder.Enabled = isEnabled;
            this.btnCopyFileFolder.Enabled = isEnabled;
            this.opnCSV.Enabled = isEnabled;
            this.opnExcel.Enabled = isEnabled;
            this.nudCopyInterval.Enabled = isEnabled;
        }

        private void chkStatistics_CheckedChanged(object sender, EventArgs e)
        {
            this.nudStatistics.Enabled = this.chkStatistics.Checked;
        }

        private void chkHeartBeat_CheckedChanged(object sender, EventArgs e)
        {
            this.nudHeartBeat.Enabled = this.chkHeartBeat.Checked;
            this.txtHeartBeatTag.Enabled = this.chkHeartBeat.Checked;
        }

        private void chkAlarm_CheckedChanged(object sender, EventArgs e)
        {
            this.nudAlarm.Enabled = this.chkAlarm.Checked;
        }
    }
}
