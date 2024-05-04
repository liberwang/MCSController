﻿using System;
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
            }
        }

        protected override void OnStart(string[] args) {
            clsLog.addLog("Start...");
            try {
                Timer timer = new Timer();
                timer.Interval = SystemKeys.VISIT_INTERVAL; // 500; // 0.5 seconds
                timer.Elapsed += new ElapsedEventHandler(this.OnTimer);
                timer.Start();
            } catch(Exception e) {
                clsLog.addLog(e.Message);
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
                }
            }
            try
            {
                Timer timer = new Timer();
                timer.Interval = SystemKeys.HEARTBEAT_INTERVAL; //  31000; // 60 seconds
                timer.Elapsed += new ElapsedEventHandler(this.OnTimerHeartBeat);
                timer.Start();
            }
            catch (Exception e)
            {
                clsLog.addLog(e.Message);
            }
            try
            {
                Timer timer = new Timer();
                timer.Interval = SystemKeys.VISIT_INTERVAL; // 500; // 0.5 seconds
                timer.Elapsed += new ElapsedEventHandler(this.OnTimerStatistics);
                timer.Start();
            }
            catch (Exception e)
            {
                clsLog.addLog(e.Message);
            }
        }

        protected override void OnStop() {
            clsLog.addLog("Stop...");
        }

        public void OnTimer(object sender, ElapsedEventArgs args) {
            try {
                RejectDetails.Instance.Start();
            } catch(Exception e) {
                clsLog.addLog(e.Message);
            }
        }

        public void OnTimerCopy(object sender, ElapsedEventArgs args) {
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

        public void OnTimerHeartBeat(object sender, ElapsedEventArgs args )
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

        public void OnTimerStatistics(object sender, ElapsedEventArgs args)
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

