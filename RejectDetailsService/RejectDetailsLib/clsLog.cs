using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RejectDetailsLib {
    public class clsLog {
        public static void addLog(string slog) {
            string logFile = SystemKeys.getLogName();

            using(StreamWriter sw = File.AppendText(logFile)) {
                sw.WriteLine(SystemKeys.getCurrentDateTime() + " " + slog);
            }
        }
    }
}