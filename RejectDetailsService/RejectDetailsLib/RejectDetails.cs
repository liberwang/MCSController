using LibplctagWrapper;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RejectDetailsLib
{
    public class RejectDetails
    {
        const int DataTimeout = 5000;

        private static bool IsRunning = false;

        private static object lockobject = new object();

        private static RejectDetails instance = null;

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
            this.listController = clsController.GetControllerList();

            foreach (clsController clsCon in this.listController)
            {
                if (clsCon.IsEnabled)
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
                lock (lockobject)
                {
                    if (!IsRunning)
                    {
                        IsRunning = true;

                        try
                        {
                            if (SystemKeys.USE_MULTITHREADING_SERVICE)
                            {
                                ProcessThread();
                            }
                            else
                            {
                                Process();
                            }
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
                        {
                            if (SystemKeys.IN_DEBUGING)
                            {
                                clsLog.addLog($@"{tagGroup.tagRead.TagName} count is 0.");
                            }
                            continue;
                        }

                        Libplctag client = tagGroup.tagClass;

                        // check read tag first, every group has one read tag only.
                        int counter = 0;
                        while (counter < 100 && client.GetStatus(tagGroup.tagRead.plcTag) == Libplctag.PLCTAG_STATUS_PENDING)
                        {
                            Thread.Sleep(100);
                            ++counter;
                        }

                        if (counter >= 100)
                        {
                            if (SystemKeys.IN_DEBUGING)
                            {
                                clsLog.addLog($@"{tagGroup.tagRead.TagName} is pending status and not connect to PLC.");
                            }
                            continue;
                        }

                        // if read tag is ready, read all tags
                        if (client.GetStatus(tagGroup.tagRead.plcTag) == Libplctag.PLCTAG_STATUS_OK)
                        {
                            bool DBRequest = client.GetBitValue(tagGroup.tagRead.plcTag, -1, DataTimeout);

                            if (SystemKeys.IN_DEBUGING)
                            {
                                clsLog.addLog($@"{tagGroup.tagRead.TagName} status is OK. ");
                            }

                            if (DBRequest)
                            {
                                clsLog.addLog($@"start dbrequest .... {DateTime.Now}");
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
                                        clsLog.addLog($"[{tagClass.TagName}] Error setting up tag internal state: {client.DecodeError(tagStatus)}");
                                        isOK = false;
                                        break;
                                    }

                                    var tagValue = client.ReadTag(tag, DataTimeout);

                                    if (tagValue != Libplctag.PLCTAG_STATUS_OK)
                                    {
                                        clsLog.addLog($"[{tagClass.TagName}]: Unable to read the data! Got error code {tagValue}: {client.DecodeError(tagValue)}");
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
                                clsLog.addLog($@"end dbrequest .... {DateTime.Now}");

                                if (isOK)
                                {
                                    if (SystemKeys.IN_DEBUGING)
                                    {
                                        clsLog.addLog($@"{tagGroup.tagRead.TagName} group is OK.");
                                    }
                                    // set read flag back first;
                                    if (tagGroup.tagRead.Write == 1)
                                    {
                                        if (SystemKeys.IN_DEBUGING)
                                        {
                                            clsLog.addLog($@"{tagGroup.tagRead.TagName} is writing back.");
                                        }

                                        client.SetBitValue(tagGroup.tagRead.plcTag, 0, Convert.ToBoolean(0), DataTimeout);
                                    }

                                    // set back to write tags. 
                                    foreach (clsTag tagWrite in tagGroup.tagWrite)
                                    {
                                        if (SystemKeys.IN_DEBUGING)
                                        {
                                            clsLog.addLog($@"tagwrite:{tagWrite.plcTag.Name}.");
                                        }
                                        client.SetBitValue(tagWrite.plcTag, 0, Convert.ToBoolean(1), DataTimeout);
                                    }

                                    new Thread( () =>
                                    {
                                        SaveToFile(listReadValues, tagSerialNoValue, clsCon.IpAddress);
                                    }).Start();
                                } else
                                {
                                    if (SystemKeys.IN_DEBUGING)
                                    {
                                        clsLog.addLog($@"{tagGroup.tagRead.TagName} group is failed.");
                                    }
                                }
                                clsLog.addLog($@"end ok .... {DateTime.Now}");
                            } else
                            {
                                if (SystemKeys.IN_DEBUGING)
                                {
                                    clsLog.addLog($@"{tagGroup.tagRead.TagName} is not ready for DBRequest.");
                                }
                            }
                        }
                    }
                }
            }
        }

        private void ProcessThread()
        {
            foreach (clsController clsCon in this.listController)
            {
                if (this.dicTagGroup.ContainsKey(clsCon.Id))
                {
                    Parallel.ForEach(this.dicTagGroup[clsCon.Id], (tagGroup) =>
                    {
                        // no tag for this ip address 
                        if (tagGroup.listTags.Count > 0)
                        {
                            Libplctag client = tagGroup.tagClass;

                            // if read tag is ready, read all tags
                            if (client.GetStatus(tagGroup.tagRead.plcTag) == Libplctag.PLCTAG_STATUS_OK)
                            {
                                bool DBRequest = client.GetBitValue(tagGroup.tagRead.plcTag, -1, DataTimeout);

                                if (DBRequest)
                                {
                                    ConcurrentDictionary<int, clsTagValue> ReadValuesDictionary = new ConcurrentDictionary<int, clsTagValue>();
                                    //string tagSerialNoValue = string.Empty;
                                    //clsTagValue tagSerialNoClass = null;
                                    int SerialNoTagId = -99;
                                    bool isOK = true;

                                    CancellationTokenSource cts = new CancellationTokenSource();
                                    ParallelOptions po = new ParallelOptions
                                    {
                                        CancellationToken = cts.Token,
                                        MaxDegreeOfParallelism = System.Environment.ProcessorCount,
                                    };

                                    object lockObj = new object();
                                    try
                                    {
                                        Parallel.ForEach<clsTag>(tagGroup.listTags, po, (tagClass) =>
                                        {
                                            Tag tag = tagClass.plcTag;

                                            if (client.GetStatus(tag) == Libplctag.PLCTAG_STATUS_PENDING)
                                            {
                                                if (SystemKeys.IN_DEBUGING)
                                                {
                                                    clsLog.addLog($@"{tagClass.TagName} is Pending status.");
                                                }
                                                cts.Cancel(true);
                                            }

                                            int tagStatus = client.GetStatus(tag);
                                            if (tagStatus != Libplctag.PLCTAG_STATUS_OK)
                                            {
                                                clsLog.addLog($"[{tagClass.TagName}] error: setting up tag internal state: {client.DecodeError(tagStatus)}");
                                                cts.Cancel(true);
                                            }

                                            var tagValue = client.ReadTag(tag, DataTimeout);

                                            if (tagValue != Libplctag.PLCTAG_STATUS_OK)
                                            {
                                                clsLog.addLog($"[{tagClass.TagName}] error: Unable to read the data! Got error code {tagValue}: {client.DecodeError(tagValue)}");
                                                cts.Cancel(true);
                                            }

                                            object readValue;
                                            if (tagClass.TagType == clsTag.BOOL_TYPE_STR) // "Bool"
                                            {
                                                readValue = client.GetBitValue(tag, -1, DataTimeout);
                                            }
                                            else if (tagClass.TagType == clsTag.STRING_TYPE_STR) //"String"
                                            {
                                                readValue = GetStringValue(tag, client);
                                            }
                                            else if (tagClass.TagType == clsTag.REAL_TYPE_STR) //"Real"
                                            {
                                                readValue = client.GetFloat32Value(tag, 0 * tag.ElementSize);
                                            }
                                            else if (tagClass.TagType == clsTag.LINT_TYPE_STR)  // long int
                                            {
                                                readValue = client.GetInt64Value(tag, 0 * tag.ElementSize);
                                            }
                                            else if (tagClass.TagType == clsTag.DINT_TYPE_STR) // 32 int 
                                            {
                                                readValue = client.GetInt32Value(tag, 0 * tag.ElementSize);
                                            }
                                            else  // short int
                                            {
                                                readValue = client.GetInt16Value(tag, 0 * tag.ElementSize);
                                            }

                                            ReadValuesDictionary.TryAdd(tagClass.TagId, new clsTagValue(tagClass, readValue));

                                            // remember serial number tag 
                                            if (tag.Name.EndsWith(clsTag.SERIAL_NUMBER_STR))
                                            {
                                                lock (lockObj)
                                                {
                                                    SerialNoTagId = tagClass.TagId;
                                                }
                                            }
                                        });
                                    }
                                    catch (OperationCanceledException e)
                                    {
                                        Console.WriteLine(e.Message);
                                        isOK = false;
                                    }

                                    if (isOK)
                                    {
                                        // set read flag back first;
                                        if (tagGroup.tagRead.Write == 1)
                                        {
                                            client.SetBitValue(tagGroup.tagRead.plcTag, 0, Convert.ToBoolean(0), DataTimeout);
                                        }

                                        // set back to write tags. 
                                        foreach (clsTag tagWrite in tagGroup.tagWrite)
                                        {
                                            client.SetBitValue(tagWrite.plcTag, 0, Convert.ToBoolean(1), DataTimeout);
                                        }

                                        new Thread(() =>
                                        {
                                            SaveToFile(ReadValuesDictionary, SerialNoTagId, clsCon.IpAddress, clsCon.Id);
                                        }).Start();
                                    }
                                }
                                else
                                {
                                    if (SystemKeys.IN_DEBUGING)
                                    {
                                        clsLog.addLog($@"{tagGroup.tagRead.TagName} does not get request value: {DBRequest}.");
                                    }
                                }
                            }
                            else
                            {
                                if (SystemKeys.IN_DEBUGING)
                                {
                                    clsLog.addLog($@"{tagGroup.tagRead.TagName} is not ready.");
                                }
                            }
                        } else
                        {
                            if (SystemKeys.IN_DEBUGING)
                            {
                                clsLog.addLog($@"{clsCon.IpAddress} does not have tags.");
                            }
                        }
                    });
                }
            }
        }

        private void SaveToFile(List<(string, string)> tagValue, string serialNumber, string ipAddress)
        {
            if (SystemKeys.IN_DEBUGING)
            {
                clsLog.addLog($@"{serialNumber} is in SaveToFile process.");
            }
            clsOutput op = clsOutput.GetOutputByProduceName();

            op.SaveToFileAndDatabase(tagValue, serialNumber, ipAddress);
        }

        private void SaveToFile(IDictionary<int, clsTagValue> tagValue, int serialNumberTagId, string ipAddress, int controllerId)
        {
            clsOutput op = clsOutput.GetOutputByProduceName();

            op.SaveToFileAndDatabase(tagValue, serialNumberTagId, ipAddress, controllerId);
        }
    }
}
