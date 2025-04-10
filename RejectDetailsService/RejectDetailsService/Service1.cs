using System;
using System.IO;
using System.ServiceProcess;
using System.Timers;
using RejectDetailsLib;

namespace RejectDetailsService {
    public partial class Service1 : ServiceBase {
        public Service1() {
            try
            {
                InitializeComponent();
                SystemKeys.initializeKey();
            } catch (Exception ex)
            {
                clsLog.addLog( ex.Message );
                throw ex;
            }
        }

        protected override void OnStart(string[] args) {
            clsLog.addLog("Tag Service Start...");
            try {
                Timer timer = new Timer();
                timer.Interval = SystemKeys.VISIT_INTERVAL; // 500; // 0.5 seconds
                timer.Elapsed += new ElapsedEventHandler(this.OnTimer);
                timer.Start();
            } catch(Exception e) {
                clsLog.addLog($"Onstart Error: {e.Message}, {e.StackTrace}");
                throw e;
            }

            if (SystemKeys.SAVE_TO_FILE)
            {
                try
                {
                    Timer timer = new Timer();
                    timer.Interval = SystemKeys.COPY_INTERVAL; //  31000; // 60 seconds
                    timer.Elapsed += new ElapsedEventHandler(this.OnTimerCopy);
                    timer.Start();
                }
                catch (Exception e)
                {
                    clsLog.addLog(e.Message);
                    throw e;
                }
            }

            if (SystemKeys.HEARTBEAT_SERVICE_ENABLE)
            {
                clsLog.addLog("HartBeat service is starting...");
                try
                {
                    Timer timer = new Timer();
                    timer.Interval = SystemKeys.HEARTBEAT_SERVICE_INTERVAL; //  31000; // 60 seconds
                    timer.Elapsed += new ElapsedEventHandler(this.OnTimerHeartBeat);
                    timer.Start();
                }
                catch (Exception e)
                {
                    clsLog.addLog(e.Message);
                    throw e;
                }
            }

            if (SystemKeys.STATISTICS_SERVICE_ENABLE)
            {
                clsLog.addLog("Statistics service is starting...");
                try
                {
                    Timer timer = new Timer();
                    timer.Interval = SystemKeys.STATISTICS_SERVICE_INTERVAL; // 500; // 0.5 seconds
                    timer.Elapsed += new ElapsedEventHandler(this.OnTimerStatistics);
                    timer.Start();
                }
                catch (Exception e)
                {
                    clsLog.addLog(e.Message);
                    throw e;
                }
            }
        }

        protected override void OnStop() {
            clsLog.addLog("Tag Service Stop...");
        }

        public void OnTimer(object sender, ElapsedEventArgs args) {
            try {
                RejectDetails.Instance.Start();
            } catch(Exception e) {
                clsLog.addLog(e.Message);
            }
        }

        private void OnTimerCopy(object sender, ElapsedEventArgs args) {
            try {
                clsOutput op = clsOutput.GetOutputByProduceName();
                if (op != null)
                {
                    op.CopyFileToTarget();
                } else
                {
                    clsLog.addLog("clsOutput is null when copying file!");
                }

            } catch(Exception e) {
                clsLog.addLog(e.Message);
            }
        }

        private void OnTimerHeartBeat(object sender, ElapsedEventArgs args )
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

        private void OnTimerStatistics(object sender, ElapsedEventArgs args)
        {
            try
            {
                StatisticsDetails.Instance.Start();
            } 
            catch (Exception e)
            {
                clsLog.addLog(e.Message);
            }
        }
    }
}

