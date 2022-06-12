using LibplctagWrapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace RejectDetailsLib {
    public class RejectDetails {
        const int DataTimeout = 5000;

        private static object lockobject = new object();

        private static RejectDetails instance = null;

        //private static Dictionary<int, List<Tag>> dictStationTag = new Dictionary<int, List<Tag>>();

        private static Dictionary<string, clsTag> dictTagInfo = new Dictionary<string, clsTag>();

        private List<clsController> listController = new List<clsController>();

        private static Dictionary<string, string> dictReadWrite = new Dictionary<string, string>();

        private static List<(clsStation, Libplctag)> ListStationLibplctag = new List<(clsStation, Libplctag)>();

        private RejectDetails() {
        }

        public static RejectDetails Instance {
            get {
                try {
                    if(instance == null) {
                        lock(lockobject) {
                            if(instance == null) {
                                instance = new RejectDetails();
                                instance.initialize();
                            }
                        }
                    }
                } catch(Exception ex) {
                    throw;
                }
                return instance;
            }
            set {
                instance = value;
            }
        }

        //Read String
        private object GetStringValue(Tag Red_Msg, Libplctag client) {
            try {
                int size = client.GetInt32Value(Red_Msg, 0);
                int offset = DataType.Int32;
                string output = string.Empty;

                for(int i = 0; i < Red_Msg.ElementCount; i++) {
                    var sb = new StringBuilder();

                    for(int j = 0; j < size; j++) {
                        sb.Append((char)client.GetUint8Value(Red_Msg, (i * Red_Msg.ElementSize) + offset + j));
                    }

                    output = sb.ToString();
                }
                return output;
            } catch(Exception ex) {
                clsLog.addLog(ex.ToString());
                throw;
            }
        }

        //Write String
        private void SetStringValue(Tag Wrt_Msg, Libplctag client, string value) {
            //First setting the size
            client.SetInt32Value(Wrt_Msg, 0, value.Length);

            //Setting string value
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(value);

            int offset = DataType.Int32; //the first 4 bytes are used to store the string size
            int j = 0;

            for(j = 0; j < bytes.Length; j++) {
                int off = offset + j;
                client.SetUint8Value(Wrt_Msg, off, bytes[j]);
            }
        }

        private void initialize() {
            this.listController = Database.GetController();

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

            //foreach(clsController clsCon in listController) {
            //    foreach(clsStation clsSta in clsCon.StationList) {
            //        List<Tag> tagList = new List<Tag>();

            //        foreach(clsTag tag in clsSta.TagList) {
            //            tag.GenerateTag(clsCon.IpAddress);
            //            dictTagInfo.Add(tag.TagFullName, tag);
            //            tagList.Add(tag.plcTag);
            //        }
            //        dictStationTag.Add(clsSta.Id, tagList);
            //    }
            //}

            dictReadWrite = Database.GetReadWriteTag();
        }

        public void Start() {

            foreach((clsStation, Libplctag) stationTag in ListStationLibplctag) {

                //using(var client = new Libplctag()) {
                //    foreach(Tag tag in dictStationTag[iStation]) {
                //        client.AddTag(tag);
                //    }
                Libplctag client = stationTag.Item2;

                bool isOK = true;
                bool DBRequest = false;
                List<(string, int)> listReadValues = new List<(string, int)>();
                Tag tagWrite = null;

                foreach(clsTag tagClass in stationTag.Item1.TagList) {
                    Tag tag = tagClass.plcTag;

                    while(client.GetStatus(tag) == Libplctag.PLCTAG_STATUS_PENDING) {
                        Thread.Sleep(100);
                    }

                    int tagStatus = client.GetStatus(tag);
                    if(tagStatus != Libplctag.PLCTAG_STATUS_OK) {
                        Console.WriteLine($"Error setting up tag internal state. Error{ client.DecodeError(tagStatus)}");
                        isOK = false;
                        break;
                    }

                    var tagValue = client.ReadTag(tag, DataTimeout);

                    if(tagValue != Libplctag.PLCTAG_STATUS_OK) {
                        Console.WriteLine($"ERROR: Unable to read the data! Got error code {tagValue}: {client.DecodeError(tagValue)}");
                        isOK = false;
                        break;
                    }

                    //var dataType = tagClass.TagType; // dictTagInfo[tag.Name].TagType;
                    object readValue;
                    if(tagClass.TagType == "Bool") {
                        readValue = client.GetBitValue(tag, -1, DataTimeout);
                    } else if(tagClass.TagType == "String") {
                        readValue = GetStringValue(tag, client);
                    } else if(tagClass.TagType == "Real") {
                        readValue = client.GetFloat32Value(tag, 0 * tag.ElementSize);
                    } else { // int
                        readValue = client.GetInt16Value(tag, 0 * tag.ElementSize);
                    }

                    listReadValues.Add((readValue.ToString(), tagClass.StationTagId));

                    if(tagClass.ReadWrite == 1) {
                        DBRequest = (bool)readValue;
                    } else if(tagClass.ReadWrite == -1) {
                        tagWrite = tag;
                    } 
                }

                if(!isOK)
                    break;

                if(DBRequest) {
                    foreach(var tagValue in listReadValues) {
                        SaveToFile(tagValue);
                    }
                }

                if(tagWrite != null)
                    if(DBRequest)
                        client.SetBitValue(tagWrite, 0, Convert.ToBoolean(1), DataTimeout);
                    else
                        client.SetBitValue(tagWrite, 0, Convert.ToBoolean(0), DataTimeout);
            }
        }


        public void SaveToFile((string, int) tagValue, string Station = "30", bool saveToFile = false) {
            //string lsFileName = getFileName();
            if(saveToFile) {
                using(StreamWriter sw = File.AppendText(SystemKeys.getFullFileName())) {
                    sw.WriteLine(tagValue.Item1);
                }
            }
            Database.SetContent(tagValue.Item1, tagValue.Item2);
        }

        public void CopyFile() {
            string lsSource = SystemKeys.getFullFileName();
            if(File.Exists(lsSource)) {
                File.Copy(lsSource, SystemKeys.getCopyFileName(), true);
            }
        }
    }
}
