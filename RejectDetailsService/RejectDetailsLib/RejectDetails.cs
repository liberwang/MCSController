using LibplctagWrapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;

namespace RejectDetailsLib
{
    public class RejectDetails
    {
        const int DataTimeout = 5000;

        private static bool IsRunning = false;

        private static object lockobject = new object();

        private static RejectDetails instance = null;

        private static string lastSerialNumber = string.Empty;

        private DataSource ds = null;

        private List<clsController> listController = new List<clsController>();
        private Dictionary<int, List<clsTagGroup>> dicTagGroup = new Dictionary<int, List<clsTagGroup>>();

        private RejectDetails()
        {
        }

        public static RejectDetails Instance
        {
            get
            {
                try
                {
                    if (instance == null)
                    {
                        lock (lockobject)
                        {
                            if (instance == null)
                            {
                                instance = new RejectDetails();
                                instance.initialize();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    clsLog.addLog(ex.ToString());
                    throw;
                }
                return instance;
            }
            set
            {
                instance = value;
            }
        }

        //Read String
        private object GetStringValue(Tag Red_Msg, Libplctag client)
        {
            try
            {
                int size = client.GetInt32Value(Red_Msg, 0);
                int offset = DataType.Int32;
                string output = string.Empty;

                for (int i = 0; i < Red_Msg.ElementCount; i++)
                {
                    var sb = new StringBuilder();

                    for (int j = 0; j < size; j++)
                    {
                        sb.Append((char)client.GetUint8Value(Red_Msg, (i * Red_Msg.ElementSize) + offset + j));
                    }

                    output = sb.ToString();
                }
                return output;
            }
            catch (Exception ex)
            {
                clsLog.addLog(ex.ToString());
                throw;
            }
        }

        //Write String
        private void SetStringValue(Tag Wrt_Msg, Libplctag client, string value)
        {
            //First setting the size
            client.SetInt32Value(Wrt_Msg, 0, value.Length);

            //Setting string value
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(value);

            int offset = DataType.Int32; //the first 4 bytes are used to store the string size
            int j = 0;

            for (j = 0; j < bytes.Length; j++)
            {
                int off = offset + j;
                client.SetUint8Value(Wrt_Msg, off, bytes[j]);
            }
        }

        private void initialize()
        {
            if (SystemKeys.GET_DATA_FROM_XML)
            {
                this.ds = new DataXML();
            }
            else
            {
                this.ds = new Database();
            }

            /*
            this.listController = ds.GetController();

            // 192.168.1.10 -> 10, 20, 30, 40
            foreach(clsController clsCon in this.listController) {
                clsCon.GetStationList();

                // 10 ->  Header.State.OK, Header.CycleStart
                foreach(clsStation clsStat in clsCon.StationList) {
                    clsStat.GetTagList();

                    Libplctag tagClass = new Libplctag();

                    foreach(clsTag tag in clsStat.TagList) {
                        tag.GenerateTag(clsCon.IpAddress);

                        if(!dictTagInfo.ContainsKey(tag.TagFullName)) {
                            dictTagInfo.Add(tag.TagFullName, tag);
                            tagClass.AddTag(tag.plcTag);
                        }
                    }

                    ListStationLibplctag.Add((clsStat, tagClass));
                }
            }

            dictReadWrite = ds.GetReadWriteTag();
            */
            this.listController = ds.GetController();

            foreach (clsController clsCon in this.listController)
            {
                if (clsCon.isEnabled)
                {
                    this.dicTagGroup.Add(clsCon.Id, clsTagGroup.GetGroup(clsCon.Id, clsCon.IpAddress));
                }
            }
            //this.listTagGroup = clsTagGroup.GetAllGroups();
        }

        public void Start()
        {
            if (!IsRunning)
            {
                IsRunning = true;

                try
                {
                    Process();
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    IsRunning = false;
                }
            }
        }
        private void Process()
        {
            // get controller first for ip address 
            foreach (clsController clsCon in this.listController)
            {
                if (this.dicTagGroup.ContainsKey(clsCon.Id))
                {
                    // get all tags under current ip address
                    foreach (clsTagGroup tagGroup in this.dicTagGroup[clsCon.Id])
                    {
                        // no tag for this ip address 
                        if (tagGroup.listTags.Count == 0)
                            continue;

                        Libplctag client = tagGroup.tagClass;

                        // check read tag first, every group has one read tag only.
                        int counter = 0;
                        while (counter < 100 && client.GetStatus(tagGroup.tagRead.plcTag) == Libplctag.PLCTAG_STATUS_PENDING)
                        {
                            Thread.Sleep(100);
                            ++counter;
                        }

                        if (counter >= 100)
                            continue;

                        // if read tag is ready, read all tags
                        if (client.GetStatus(tagGroup.tagRead.plcTag) == Libplctag.PLCTAG_STATUS_OK)
                        {
                            bool DBRequest = client.GetBitValue(tagGroup.tagRead.plcTag, -1, DataTimeout);

                            if (DBRequest)
                            {
                                List<(string, string)> listReadValues = new List<(string, string)>();
                                string tagSerialNoValue = string.Empty;
                                bool isOK = true;

                                foreach (clsTag tagClass in tagGroup.listTags)
                                {
                                    Tag tag = tagClass.plcTag;

                                    while (client.GetStatus(tag) == Libplctag.PLCTAG_STATUS_PENDING)
                                    {
                                        Thread.Sleep(100);
                                    }

                                    int tagStatus = client.GetStatus(tag);
                                    if (tagStatus != Libplctag.PLCTAG_STATUS_OK)
                                    {
                                        Console.WriteLine($"Error setting up tag internal state. Error{client.DecodeError(tagStatus)}");
                                        isOK = false;
                                        break;
                                    }

                                    var tagValue = client.ReadTag(tag, DataTimeout);

                                    if (tagValue != Libplctag.PLCTAG_STATUS_OK)
                                    {
                                        Console.WriteLine($"ERROR: Unable to read the data! Got error code {tagValue}: {client.DecodeError(tagValue)}");
                                        isOK = false;
                                        break;
                                    }

                                    object readValue;
                                    if (tagClass.TagType == "Bool")
                                    {
                                        readValue = client.GetBitValue(tag, -1, DataTimeout);
                                    }
                                    else if (tagClass.TagType == "String")
                                    {
                                        readValue = GetStringValue(tag, client);
                                    }
                                    else if (tagClass.TagType == "Real")
                                    {
                                        readValue = client.GetFloat32Value(tag, 0 * tag.ElementSize);
                                    }
                                    else
                                    { // int
                                        readValue = client.GetInt16Value(tag, 0 * tag.ElementSize);
                                    }

                                    if (tagClass.Output == 1)
                                        listReadValues.Add((readValue.ToString(), tagClass.TagName));

                                    if (tag.Name.EndsWith("SerialNumber"))
                                    {
                                        tagSerialNoValue = readValue.ToString();
                                    }
                                }

                                if (isOK)
                                {
                                    // ignore duplicated serialnumber 
                                    //if (tagSerialNoValue != string.Empty && tagSerialNoValue != lastSerialNumber)
                                    //{
                                    //    lastSerialNumber = tagSerialNoValue;

                                        // set read flag back first;
                                        if (tagGroup.tagRead.Write == 1)
                                        {
                                            client.SetBitValue(tagGroup.tagRead.plcTag, 0, Convert.ToBoolean(0), DataTimeout);
                                        }

                                        // set back to write tags. 
                                        foreach (clsTag tagWrite in tagGroup.tagWrite)
                                        {
                                            // todo write value back;
                                            // TODO
                                            //clsLog.addLog($@"tagwrite: {tagWrite.plcTag.Name}");
                                            client.SetBitValue(tagWrite.plcTag, 0, Convert.ToBoolean(1), DataTimeout);
                                        }

                                        SaveToFile(listReadValues, tagSerialNoValue, clsCon.IpAddress);
                                    //}
                                }
                            }
                        }
                    }
                }
            }
        }

        //private void Process() {
        //    foreach((clsStation, Libplctag) stationTag in ListStationLibplctag) {

        //        Libplctag client = stationTag.Item2;

        //        bool isOK = true;
        //        bool DBRequest = false;
        //        List<(string, int)> listReadValues = new List<(string, int)>();
        //        Tag tagWrite = null;
        //        int tagSerialNoValue = 0;

        //        foreach(clsTag tagClass in stationTag.Item1.TagList) {
        //            Tag tag = tagClass.plcTag;

        //            while(client.GetStatus(tag) == Libplctag.PLCTAG_STATUS_PENDING) {
        //                Thread.Sleep(100);
        //            }

        //            int tagStatus = client.GetStatus(tag);
        //            if(tagStatus != Libplctag.PLCTAG_STATUS_OK) {
        //                Console.WriteLine($"Error setting up tag internal state. Error{ client.DecodeError(tagStatus)}");
        //                isOK = false;
        //                break;
        //            }

        //            var tagValue = client.ReadTag(tag, DataTimeout);

        //            if(tagValue != Libplctag.PLCTAG_STATUS_OK) {
        //                Console.WriteLine($"ERROR: Unable to read the data! Got error code {tagValue}: {client.DecodeError(tagValue)}");
        //                isOK = false;
        //                break;
        //            }

        //            //var dataType = tagClass.TagType; // dictTagInfo[tag.Name].TagType;
        //            object readValue;
        //            if(tagClass.TagType == "Bool") {
        //                readValue = client.GetBitValue(tag, -1, DataTimeout);
        //            } else if(tagClass.TagType == "String") {
        //                readValue = GetStringValue(tag, client);
        //            } else if(tagClass.TagType == "Real") {
        //                readValue = client.GetFloat32Value(tag, 0 * tag.ElementSize);
        //            } else { // int
        //                readValue = client.GetInt16Value(tag, 0 * tag.ElementSize);
        //            }

        //            listReadValues.Add((readValue.ToString(), tagClass.StationTagId));

        //            if(tagClass.ReadWrite == 1) {
        //                DBRequest = (bool)readValue;
        //            } else if(tagClass.ReadWrite == -1) {
        //                tagWrite = tag;
        //            }

        //            if(tag.Name.EndsWith("SerialNumber")) {
        //                tagSerialNoValue = (int)readValue;
        //            }
        //        }

        //        if(!isOK)
        //            break;

        //        if(DBRequest) {
        //            foreach(var tagValue in listReadValues) {
        //                SaveToFile(tagValue, tagSerialNoValue);
        //            }
        //        }

        //        if(tagWrite != null) {
        //            if(DBRequest) {
        //                client.SetBitValue(tagWrite, 0, Convert.ToBoolean(1), DataTimeout);
        //            } else {
        //                client.SetBitValue(tagWrite, 0, Convert.ToBoolean(0), DataTimeout);
        //            }
        //        }
        //    }

        //}

        private void SaveToFile(List<(string, string)> tagValue, string serialNumber, string ipAddress)
        {
            clsOutput op = clsOutput.GetOutputByProduceName();
  
            op.SaveToFileAndDatabase(tagValue, serialNumber, ipAddress);

            //if (SystemKeys.SAVE_TO_FILE)
            //{
            //    string fileName = SystemKeys.getFullFileName();

            //    if (!File.Exists(fileName))
            //    {
            //        // if csv file is not in the path, create a header to it first.
            //        StringBuilder sbField = new StringBuilder();

            //        foreach ((string, string) tv in tagValue)
            //        {
            //            sbField.Append(tv.Item2).Append(",");
            //        }
            //        sbField.Append("TimeStamp");

            //        using (StreamWriter sw = File.AppendText(fileName))
            //        {
            //            sw.WriteLine(sbField.ToString());
            //        }
            //    }

            //    StringBuilder sb = new StringBuilder();
            //    foreach ((string, string) tv in tagValue)
            //    {
            //        sb.Append(tv.Item1).Append(",");
            //    }
            //    sb.Append(SystemKeys.getCurrentDateTime());

            //    using (StreamWriter sw = File.AppendText(fileName))
            //    {
            //        sw.WriteLine(sb.ToString());
            //    }
            //}
            //if (SystemKeys.SAVE_TO_DB)
            //{
            //    Database.SetContent(tagValue, ipAddress, serialNumber);
            //}
        }
    }
}
