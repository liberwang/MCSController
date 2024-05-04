using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RejectDetailsLib
{
    public class clsOutputStatistics : clsOutput 
    {

        protected override void SaveToDatabase()
        {
            if (m_tagValueDictionary != null && m_tagValueDictionary.Any())
            {
                List<(string, string)> list = new List<(string, string)>();
                foreach (int key in m_tagValueDictionary.Keys)
                {
                    clsTagValue tv = m_tagValueDictionary[key];
                    list.Add((tv.ToString(), tv.GetTagName()));
                }
                new Database().SetStatisticsContent(list, m_ipAddress);
            }
        }
    }
}
