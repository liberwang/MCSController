using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using RejectDetailsLib;

namespace RejectDetailsWin {
    public partial class frmTest : Form {
        private System.Timers.Timer timer = new System.Timers.Timer();

        public frmTest() {
            InitializeComponent();
            RejectDetails.Instance = null;
        }

        private void btnStart_Click(object sender, EventArgs e) {
            try
            {
                //Timer timer = new Timer();
                timer.Interval = SystemKeys.HEARTBEAT_SERVICE_INTERVAL; //  31000; // 60 seconds
                timer.Elapsed += new ElapsedEventHandler(this.OnTimerHeartBeat);
                timer.Start();
            } catch (Exception ex)
            {
                clsLog.addLog(ex.ToString());
                MessageBox.Show(ex.ToString(), "MCS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void OnTimer(object sender, ElapsedEventArgs args) {
            try {
                RejectDetails.Instance.Start();
            } catch(Exception e) {
                clsLog.addLog(e.Message);
            }
        }

        private void btnSingle_Click(object sender, EventArgs e) {
            if (this.cboService.SelectedItem == null )
            {
                MessageBox.Show("Please choose type of service first!", "Service Test", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            switch (this.cboService.SelectedItem.ToString())
            {
                case "Reject Service":
                    RejectDetails.Instance.Start();
                    break;
                case "Statistics Service":
                    StatisticsDetails.Instance.Start();
                    break;
                case "Alarm Service":
                    AlarmDetails.Instance.Start();
                    break;
                case "HeartBeat Service":
                    HeartBeat.Instance.Start();
                    break;
                default:
                    break;
            }
            
        }

        public void OnTimerHeartBeat(object sender, ElapsedEventArgs args)
        {
            try
            {
                HeartBeat.Instance.Start();
            }
            catch (Exception e)
            {
                clsLog.addLog(e.Message);
            }
        }
    }
}
