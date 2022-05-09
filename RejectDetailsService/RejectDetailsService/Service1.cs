﻿using System;
using System.IO;
using System.ServiceProcess;
using System.Timers;

namespace RejectDetailsService {
    public partial class Service1 : ServiceBase
    {
        //private RejectDetails rejectClass = new RejectDetails();

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            addLog("Start...");
            try
            {
                Timer timer = new Timer();
                timer.Interval = SystemKeys.VISIT_INTERVAL; // 500; // 0.5 seconds
                timer.Elapsed += new ElapsedEventHandler(this.OnTimer);
                timer.Start();
            }
            catch (Exception e)
            {
                addLog(e.Message);
            }
            try
            {
                //addLog("START COPY" + DateTime.Now.ToShortTimeString());
                Timer timer = new Timer();
                timer.Interval = SystemKeys.COPY_INTERVAL; //  31000; // 60 seconds
                timer.Elapsed += new ElapsedEventHandler(this.OnTimerCopy);
                timer.Start();
            }
            catch (Exception e)
            {
                addLog(e.Message);
            }


        }

        protected override void OnStop()
        {
            addLog("Stop...");
        }

        public static void addLog(string slog)
        {
            string logFile = SystemKeys.getLogName();

            using (StreamWriter sw = File.AppendText(logFile))
            {
                sw.WriteLine(SystemKeys.getCurrentDateTime() + " " + slog);
            }
        }

        public void OnTimer(object sender, ElapsedEventArgs args)
        {
            try {
                RejectDetails.Instance.Start();
            } catch ( Exception e ) {
                addLog(e.Message);
            }
        }

        public void OnTimerCopy(object sender, ElapsedEventArgs args)
        {
            try {
                RejectDetails.Instance.CopyFile();
            } catch ( Exception e ) {
                addLog(e.Message);
            }
        }

    }
}
