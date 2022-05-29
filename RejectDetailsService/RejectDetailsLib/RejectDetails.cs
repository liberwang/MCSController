using LibplctagWrapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace RejectDetailsLib {
    public class RejectDetails {
        const int DataTimeout = 5000;
        //private static DateTime lastDT = DateTime.Now;
        //public static bool is_running = true;

        private static object lockobject = new object();

        private static RejectDetails instance;

        private static List<int> listStations = new List<int>();

        private static Dictionary<int, List<Tag>> dictStationTag = new Dictionary<int, List<Tag>>();

        private static Dictionary<string, clsTag> dictTagInfo = new Dictionary<string, clsTag>();

        //private List<(Tag, float)> listTag = new List<(Tag, float)>();

        private List<clsController> listController = new List<clsController>();

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
        }

        private void initialize() {
            this.listController = Database.GetController();

            foreach( clsController clsCon in this.listController ) {
                clsCon.GetStationList();

                foreach( clsStation clsStat in clsCon.StationList) {
                    clsStat.GetTagList();
                }
            }

            foreach(clsController clsCon in listController) {
                foreach(clsStation clsSta in clsCon.StationList) {
                    List<Tag> tagList = new List<Tag>();

                    foreach(clsTag tag in clsSta.TagList) {
                        string tagName = tag.getTagPath();

                        int dataType = DataType.INT;
                        if(tag.TagType == "Bool") {
                            dataType = DataType.SINT;
                        } else if(tag.TagType == "Real") {
                            dataType = DataType.REAL;
                        } else if(tag.TagType == "String") {
                            dataType = DataType.String;
                        } else if(tag.TagType == "Int") {
                            dataType = DataType.INT;
                        }

                        dictTagInfo.Add(tagName, tag);

                        Tag newTag = new Tag(clsCon.IpAddress, "1,0", CpuType.LGX, tagName, dataType, 1);
                        tagList.Add(newTag);
                    }
                    dictStationTag.Add(clsSta.Id, tagList);
                }
            }

            //listStations = Database.GetStations();

            // get all tags for every station
            //foreach(int stationId in listStations) {
            //List<clsTag> clsTagList = Database.GetTagInformation();
            //List<Tag> tagList = new List<Tag>();

            //foreach(clsTag tag in clsTagList) {
            //    string tagName = tag.getTagPath();

            //    int dataType = DataType.INT;
            //    if(tag.TagType == "Bool") {
            //        dataType = DataType.SINT;
            //    } else if(tag.TagType == "Real") {
            //        dataType = DataType.REAL;
            //    } else if(tag.TagType == "String") {
            //        dataType = DataType.String;
            //    } else if(tag.TagType == "Int") {
            //        dataType = DataType.INT;
            //    }

            //    dictTagInfo.Add(tagName, tag);

            //    Tag newTag = new Tag(SystemKeys.IP_ADDRESS_THIS, "1,0", CpuType.LGX, tagName, dataType, 1);
            //    tagList.Add(newTag);
            //}
            //dictTag.Add(stationId, tagList);
            //}
        }

        //Read String
        private object GetStringValue(Tag Red_Msg, Libplctag client) {

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

        public void Start() {

            foreach(int iStation in dictStationTag.Keys) {

                using(var client = new Libplctag()) {
                    foreach(Tag tag in dictStationTag[iStation]) {
                        client.AddTag(tag);
                    }
                    bool isOK = true;
                    bool DBRequest = false;
                    List<(string, int)> listReadValues = new List<(string, int)>();
                    Tag tagWrite = null;

                    foreach(Tag tag in dictStationTag[iStation]) {
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

                        var dataType = dictTagInfo[tag.Name].TagType;
                        object readValue;
                        if(dataType == "Bool") {
                            readValue = client.GetBitValue(tag, -1, DataTimeout);
                        } else if(dataType == "String") {
                            readValue = GetStringValue(tag, client);

                        } else if(dataType == "Real") {
                            readValue = client.GetFloat32Value(tag, 0 * tag.ElementSize);
                        } else { // int
                            readValue = client.GetInt32Value(tag, 0 * tag.ElementSize);
                        }

                        listReadValues.Add((readValue.ToString(), dictTagInfo[tag.Name].StationTagId));

                        if(dictTagInfo[tag.Name].ReadWrite == 1) {
                            DBRequest = (bool)readValue;
                        } else if(dictTagInfo[tag.Name].ReadWrite == -1) {
                            tagWrite = tag;
                        }
                    }

                    if(!isOK)
                        break;

                    if(DBRequest) {
                        foreach( var tagValue in listReadValues) {
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
