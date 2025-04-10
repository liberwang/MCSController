using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RejectDetailsLib {
    public class clsLog {

        private const string TAG_LOG_STRING = "Tag";
        private const string ALARM_LOG_STRING = "Alarm";

        public static void addLog(string slog) {
            //string logFile = SystemKeys.getLogName();

            //using(StreamWriter sw = File.AppendText(logFile)) {
            //    sw.WriteLine(SystemKeys.getCurrentDateTime() + " " + slog);
            //}
            addLog(slog, "");
        }

        private static void addLog(string slog, string prefix) 
        {
            string logFile = SystemKeys.getLogName(prefix);

            using (StreamWriter sw = File.AppendText(logFile))
            {
                sw.WriteLine(SystemKeys.getCurrentDateTime() + " " + slog);
            }
        }

        public static void addAlarmLog(string slog)
        {
            addLog(slog, ALARM_LOG_STRING);
        }

        public static void addTagLog(string slog)
        {
            addLog(slog, TAG_LOG_STRING);
        }
    }
}