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
        private HashSet<string> rejectFields = new HashSet<string>(new string[] { "RejectDetail1", "RejectDetail2", "RejectDetail3", "RejectDetail4", "RejectDetail5", "RejectDetail6", "RejectDetail7", "RejectDetail8", "RejectDetail9", "RejectDetail10" });
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

            if (!this.m_serialNumber.StartsWith("Reject"))
            {
                return;
            }

            foreach ((string, string) tagValue in this.m_tagValueList)
            {
                int index = tagValue.Item2.LastIndexOf('.');
                string rejectName = index < 0 ? tagValue.Item2 : tagValue.Item2.Substring(index + 1);

                if (rejectFields.Contains(rejectName) && !string.IsNullOrWhiteSpace(tagValue.Item1))
                {
                    this.SaveRejectInfo(tagValue.Item1);
                }
            }
        }

        private void SaveRejectInfo(string rejecrValue)
        {
            //if (!string.IsNullOrWhiteSpace(svalue) && ( m_serialNumber != m_lastSerialNumber || svalue != m_lastRejectDetail ))
            clsLog.addLog($@"rejectvalue : {rejecrValue}");

            //m_lastRejectDetail = rejecrValue;
            //m_lastSerialNumber = m_serialNumber;

            List<(string, string)> list = new List<(string, string)>();
            string[] valueArry = rejecrValue.Split(DELIMITER);
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

        protected override List<int> GetOutputTagListByOrder()
        {
            return new Database().GetSelectedTagIdOutputOriginal(this.m_controllerId);
        }

        protected override string GetOutputTagName(string sTagName)
        {
            return clsTagValue.GetTagOutputName(sTagName);
        }

        private string GetFileName() 
        {
            return SystemKeys.getFullFileName(fileNamePre);
        }
    }
}
