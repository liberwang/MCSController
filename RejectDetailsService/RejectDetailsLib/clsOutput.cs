using RejectDetailsLib.Clients;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Security.AccessControl;
using System.Text;

namespace RejectDetailsLib
{
    public class clsOutput
    {
        //public string ProductName { get; set; }
        //public string Description { get; set; }

        public List<(string, string)> m_tagValueList { get; set; }

        public IDictionary<int, clsTagValue> m_tagValueDictionary {  get; set; }    

        public string m_serialNumber { get; set; }

        public int m_serialNumberId { get; set; }
        public string m_ipAddress { get; set; }

        public int m_controllerId { get; set; }

        protected static List<int> m_OutputTagIdList = null;

        public static clsOutput GetOutputByProduceName()
        {
            if (SystemKeys.IsHondaBulkHead())
            {
                return new clsHBHOutput();
            }
            else
            {
                return new clsOutput();
            }
        } 

        public clsOutput()
        {
            //TODO
            //m_OutputTagIdList = this.GetOutputTagListByOrder();
        }

        public void SaveToFileAndDatabase(List<(string, string)> tagValue, string serialNumber, string ipAddress)
        {
            m_tagValueList = tagValue;
            m_serialNumber = serialNumber;
            m_ipAddress = ipAddress;

            //clsLog.addLog($@"enter savetofileanddatabase...{m_tagValueList.Count}, {m_serialNumber}, {m_ipAddress}");
            this.SaveToFileAndDatabase();
        }

        public void SaveToFileAndDatabase(IDictionary<int, clsTagValue> tagValue, int serialNumberId, string ipAddress, int controllerId)
        {
            m_tagValueDictionary = tagValue;
            m_serialNumberId = serialNumberId;
            m_ipAddress = ipAddress;
            m_controllerId = controllerId;
                      
            // TODO
            this.SaveToFileAndDatabase();
        }

        public void SaveToFileAndDatabase()
        {
            if (SystemKeys.SAVE_TO_FILE)
            {
                this.SaveToFile();
            }
            if (SystemKeys.SAVE_TO_DB)
            {
                this.SaveToDatabase();
            }
        }
        
        public virtual void CopyFileToTarget()
        {
            if (SystemKeys.SAVE_TO_FILE)
            {
                string lsSource = SystemKeys.getFullFileName();
                if (File.Exists(lsSource))
                {
                    File.Copy(lsSource, SystemKeys.getCopyFileName(), true);
                }
            }
        }

        protected virtual void SaveToFile()
        {
            // default output for all clients
            if (m_tagValueList != null && m_tagValueList.Count > 0)
            {
                string fileName = SystemKeys.getFullFileName();

                SaveToFile(fileName, m_tagValueList, true);
            }
            //if (!File.Exists(fileName))
            //{
            //    // if csv file is not in the path, create a header to it first.
            //    StringBuilder sbField = new StringBuilder();

            //    foreach ((string, string) tv in m_tagValueList)
            //    {
            //        sbField.Append(tv.Item2).Append(",");
            //    }
            //    sbField.Append("TimeStamp");

            //    using (StreamWriter sw = File.AppendText(fileName))
            //    {
            //        sw.WriteLine(sbField.ToString());
            //    }
            //}

            //StringBuilder sb = new StringBuilder();
            //foreach ((string, string) tv in m_tagValueList)
            //{
            //    sb.Append(tv.Item1).Append(",");
            //}
            //sb.Append(SystemKeys.getCurrentDateTime());

            //using (StreamWriter sw = File.AppendText(fileName))
            //{
            //    sw.WriteLine(sb.ToString());
            //}
        }

        public void SaveToFile(string fileName, List<(string, string)> tagValueList, bool bAppendTimeStamp )
        {
            if (!File.Exists(fileName))
            {
                // if csv file is not in the path, create a header to it first.
                StringBuilder sbField = new StringBuilder();

                foreach ((string, string) tv in tagValueList)
                {
                    sbField.Append(tv.Item2).Append(",");
                }

                if (bAppendTimeStamp)
                {
                    sbField.Append("TimeStamp");
                } 
                else
                {
                    sbField.Remove(sbField.Length - 1, 1);  
                }

                using (StreamWriter sw = File.AppendText(fileName))
                {
                    sw.WriteLine(sbField.ToString());
                }
            }

            StringBuilder sb = new StringBuilder();
            foreach ((string, string) tv in tagValueList)
            {
                sb.Append(tv.Item1).Append(",");
            }
            if (bAppendTimeStamp)
            {
                sb.Append(SystemKeys.getCurrentDateTime());
            } else
            {
                sb.Remove(sb.Length - 1, 1);
            }

            using (StreamWriter sw = File.AppendText(fileName))
            {
                sw.WriteLine(sb.ToString());
            }
        }

        protected virtual void SaveToDatabase()
        {
            if (m_tagValueList?.Count > 0)
            {
                //clsLog.addLog("send to database...");
                new Database().SetContent(m_tagValueList, m_ipAddress, m_serialNumber);
            }
        }

        protected virtual List<int> GetOutputTagListByOrder()
        {
            return new Database().GetSelectedTagIdOutput(this.m_controllerId);
        }

        protected virtual string GetOutputTagName(clsTagValue tagValueObject)
        {
            return tagValueObject.GetOutputTitle();
        }
    }
}
