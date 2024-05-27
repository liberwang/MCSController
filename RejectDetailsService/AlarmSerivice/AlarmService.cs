using RejectDetailsLib;
using System;
using System.ServiceProcess;
using System.Timers;

namespace AlarmService
{
    public partial class AlarmService : ServiceBase
    {
        public AlarmService()
        {
            try
            {
                InitializeComponent();
                SystemKeys.initializeKey();
            }
            catch (Exception ex)
            {
                clsLog.addLog(ex.Message);
                throw ex;
            }
        }

        protected override void OnStart(string[] args)
        {
            clsLog.addLog("Alarm Service Start..."); 
            try
            {
                Timer timer = new Timer();
                timer.Interval = SystemKeys.ALARM_SERVICE_INTERVAL; // 500; // 0.5 seconds
                timer.Elapsed += new ElapsedEventHandler(this.OnTimerAlarm);
                timer.Start();
            }
            catch (Exception e)
            {
                clsLog.addLog(e.Message);
            }
        }

        protected override void OnStop()
        {
            clsLog.addLog("Alarm Service Stop...");
        }

        public void OnTimerAlarm(object sender, EventArgs e)
        {
            try
            {
                AlarmDetails.Instance.Start();
            }
            catch (Exception ex)
            {
                clsLog.addLog(ex.Message);
            }
        }
    }
}
