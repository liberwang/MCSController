using LibplctagWrapper;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RejectDetailsLib
{
    public class StatisticsDetails
    {
        private const int DataTimeout = 5000;
        private static bool IsRunning = false;

        private static object lockobject = new object();

        private static StatisticsDetails instance = null;

        private Dictionary<string,List<clsTagGroup>> dicTagGroup = new Dictionary<string, List<clsTagGroup>>();
        private StatisticsDetails() { }

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

        public static StatisticsDetails Instance
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
                                instance = new StatisticsDetails();
                                instance.initialize();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    clsLog.addLog(ex.Message);
                    throw;
                }
                return instance;
            }
        }

        private void initialize()
        {
            List<clsController> listController = clsController.GetAllControllerList().Where(x => x.IsStatistics && x.IsEnabled).ToList();

            if (listController == null || listController.Count == 0)
            {
                throw new Exception("Can not find enabled ip address for statistics..........");
            }
            else
            {
                foreach (clsController clsCon in listController)
                {
                    this.dicTagGroup.Add(clsCon.IpAddress, clsTagGroup.GetGroup(clsCon.Id, clsCon));
                }
            }
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
            }
        }

        public void Process()
        {
            foreach (string ipAddress in dicTagGroup.Keys)
            {
                List<clsTagGroup> groupList = dicTagGroup[ipAddress];

                foreach (clsTagGroup tagGroup in groupList)
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
                                //int SerialNoTagId = -99;
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
                                        //if (tag.Name.EndsWith(clsTag.SERIAL_NUMBER_STR))
                                        //{
                                        //    lock (lockObj)
                                        //    {
                                        //        SerialNoTagId = tagClass.TagId;
                                        //    }
                                        //}
                                    });
                                }
                                catch (OperationCanceledException e)
                                {
                                    Console.WriteLine(e.Message);
                                    isOK = false;
                                }

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
                                            clsLog.addLog($@"{tagWrite.plcTag.Name} is writing back.");
                                        }

                                        client.SetBitValue(tagWrite.plcTag, 0, Convert.ToBoolean(1), DataTimeout);
                                    }

                                    new Thread(() =>
                                    {
                                        SaveToFile(ReadValuesDictionary, ipAddress);
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
                            clsLog.addLog($@"Tag group does not have tags.");
                        }
                    }
                }
            }

        }

        private void SaveToFile(IDictionary<int, clsTagValue> tagValue, string ipAddress)
        {
            clsOutput op = new clsOutputStatistics();

            op.SaveToFileAndDatabase(tagValue, -99, ipAddress, 0);
        }
    }
}
