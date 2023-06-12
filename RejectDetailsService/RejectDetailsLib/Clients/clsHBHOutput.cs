using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RejectDetailsLib.Clients
{
    internal class clsHBHOutput : clsOutput
    {
        private const string fileNamePre = "RejectDetails-";
        private string[] titles = new string[] { "DATE", "TIME", "REJECT#", "PARTTYPE", "CAVITY", "STN#", "NUT#", "LIMITS", "HI/LOW", "VALUE" };

        private string[] partType = new string[] { "", "CIVIC", "ILX", "ILX-S" };
        private string rejectField = "RejectDetail1";
        private const char DELIMITER = ';';
        private const int PARTTYPE_POS = 3;
        private const int PART_NUMBER_POS = 2;
        private const string PART_NUMBER_START = "part";
        private const int STATION_NO_POS = 5;
        private const string STATION_NO_START = "STN ";

        private static string m_lastSerialNumber = "";
        private static string m_lastRejectDetail = "";

        protected override void SaveToFile()
        {
            base.SaveToFile();

            //clsLog.addLog("honda-buldhead: save to file!");

            if (!this.m_serialNumber.StartsWith("Reject"))
            {
                return;
            }

            string svalue = "";

            foreach ((string, string) tagValue in this.m_tagValueList)
            {
                if (tagValue.Item2.EndsWith(rejectField))
                {
                    svalue = tagValue.Item1;
                    break;
                }
            }

            //if (!string.IsNullOrWhiteSpace(svalue) && ( m_serialNumber != m_lastSerialNumber || svalue != m_lastRejectDetail ))
            if (!string.IsNullOrWhiteSpace(svalue))

            {
                clsLog.addLog($@"rejectvalue : {svalue}");

                m_lastRejectDetail = svalue;
                m_lastSerialNumber = m_serialNumber;

                List<(string, string)> list = new List<(string, string)>();
                string[] valueArry = svalue.Split(DELIMITER);
                for (int i = 0; i < valueArry.Length; ++i)
                {
                    string rejectValue = valueArry[i];
                    if (i < titles.Length)
                    {

                        if (i == PARTTYPE_POS)
                        {
                            if (int.TryParse(rejectValue, out int val) && val < partType.Length)
                            {
                                list.Add((partType[val], titles[i]));
                            }
                            else
                            {
                                list.Add((partType[0], titles[i]));
                            }
                        }
                        else if (i == PART_NUMBER_POS)
                        {
                            if (rejectValue.StartsWith(PART_NUMBER_START, StringComparison.CurrentCultureIgnoreCase))
                            {
                                rejectValue = rejectValue.Substring(PART_NUMBER_START.Length);
                            }
                            list.Add((rejectValue, titles[i]));
                        }
                        else if (i == STATION_NO_POS)
                        {
                            if (rejectValue.StartsWith(STATION_NO_START, StringComparison.CurrentCultureIgnoreCase))
                            {
                                rejectValue = rejectValue.Substring(STATION_NO_START.Length);
                            }
                            list.Add((rejectValue, titles[i]));
                        }
                        else
                        {
                            list.Add((rejectValue, titles[i]));
                        }
                    }
                    else
                    {
                        list.Add((rejectValue, $@"temp{i}"));
                    }
                }

                this.SaveToFile(GetFileName(), list, false);
            }

        }

        public override void CopyFileToTarget()
        {
            base.CopyFileToTarget();

            string lsSource = this.GetFileName();
            //clsLog.addLog($@"source file: {lsSource}");

            if (File.Exists(lsSource))
            {
                File.Copy(lsSource, SystemKeys.getCopyFileName(fileNamePre), true);
            }
        }


        private string GetFileName()
        {
            return SystemKeys.getFullFileName(fileNamePre);
        }
    }
}
