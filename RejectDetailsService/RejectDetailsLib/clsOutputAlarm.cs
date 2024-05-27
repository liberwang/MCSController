using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RejectDetailsLib
{
    internal class clsOutputAlarm : clsOutputStatistics
    {
        protected override void SaveContentToDatabase(List<(string, string)> list)
        {
            new Database().SetAlarmContent(list, m_ipAddress);
        }
    }
}
