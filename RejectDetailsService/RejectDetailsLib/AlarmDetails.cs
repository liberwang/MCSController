using LibplctagWrapper;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RejectDetailsLib
{
    public class AlarmDetails
    {
        private const int DataTimeout = 5000;
        private static bool IsRunning = false;

        private static object LockObject = new object();

        private static AlarmDetails instance = null;

        private List<clsHierarchyTag>[] listTagGroup;
        private List<clsController> listController = null;
        private AlarmDetails() { }

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

        public static AlarmDetails Instance
        {
            get
            {
                try
                {
                    if (instance == null)
                    {
                        lock (LockObject)
                        {
                            if (instance == null)
                            {
                                instance = new AlarmDetails();
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
            listController = clsController.GetControllerAlarmList();

            if (listController == null || listController.Count == 0)
            {
                throw new Exception("Can not find enabled ip address for alarm..........");
            }
            else
            {
                this.listTagGroup = new List<clsHierarchyTag>[listController.Count];
                for (int i = 0; i < listController.Count; ++i)
                {
                    listTagGroup[i] = clsHierarchyTag.GenerateHierarchyTags(listController[i].Id, listController[i].IpAddress);
                }
            }
        }


        public void Start()
        {
            if (!IsRunning)
            {
                lock (LockObject)
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

        private void Process()
        {
            for (int i = 0; i < listTagGroup.Length; ++i)
            {
                if (listTagGroup[i].Count == 0)
                {
                    continue;
                }

                int controllerId = listController[i].Id;
                foreach (clsHierarchyTag clsTag in listTagGroup[i])
                {
                   // new Thread(() =>
                   //{
                        ProcessChildren(clsTag, new Libplctag(), controllerId);
                    //}).Start();
                }


            }
        }

        private void ProcessChildren(clsHierarchyTag tag, Libplctag client, int controllerId)
        {
            ConcurrentDictionary<int, clsTagValue> ReadValuesDictionary = new ConcurrentDictionary<int, clsTagValue>();
            if (ReadTag(tag, client, ReadValuesDictionary))
            {
                new Thread(() =>
                {
                    SaveToFile(ReadValuesDictionary, controllerId);
                }).Start();
            }
        }

        private bool ReadTag(clsHierarchyTag tag, Libplctag client, ConcurrentDictionary<int, clsTagValue> ReadValuesDictionary)
        {
            client.AddTag(tag.plcTag);
            bool isOK = true;
            if (tag.Read == 1)
            {
                int counter = 0;
                while (client.GetStatus(tag.plcTag) == Libplctag.PLCTAG_STATUS_PENDING && counter ++ < 100)
                {
                    Thread.Sleep(100);
                }

                if (client.GetStatus(tag.plcTag) == Libplctag.PLCTAG_STATUS_OK)
                {
                    bool DBRequest = client.GetBitValue(tag.plcTag, -1, DataTimeout);

                    if (DBRequest)
                    {
                        if (tag.ChildrenTags != null && tag.ChildrenTags.Count > 0)
                        {

                            object UpdateOK = new object();

                            CancellationTokenSource cts = new CancellationTokenSource();
                            ParallelOptions po = new ParallelOptions
                            {
                                CancellationToken = cts.Token,
                                MaxDegreeOfParallelism = System.Environment.ProcessorCount,
                            };

                            try
                            {
                                Parallel.ForEach(tag.ChildrenTags, po, (tagClass) =>
                                {
                                    if (!ReadTag(tagClass, client, ReadValuesDictionary))
                                    {
                                        lock (UpdateOK)
                                        {
                                            isOK = false;
                                        }
                                    }

                                });
                            }
                            catch (Exception ex)
                            {
                                clsLog.addLog($"AlarmDetails.ReadTag MultiThread Error: {ex.Message}");
                                isOK = false;
                            }
                        }
                    }
                    else
                    {
                        if (SystemKeys.IN_DEBUGING)
                        {
                            clsLog.addLog($@"{tag.TagName} does not get request value: {DBRequest}.");
                        }
                        return false;
                    }
                }
                else
                {
                    if (SystemKeys.IN_DEBUGING)
                    {
                        clsLog.addLog($@"{tag.TagName} is in Pending status and not ready.");
                    }
                    return false;
                }
            }
            else
            {
                clsTagValue tv = RetrieveTagValue(client, tag);
                if (tv != null)
                {
                    if (!ReadValuesDictionary.ContainsKey(tag.TagId))
                    {
                        ReadValuesDictionary.TryAdd(tag.TagId, tv);
                    }
                }
            }

            if (isOK && tag.Write == 1)
            {
                if (SystemKeys.IN_DEBUGING)
                {
                    clsLog.addLog($@"{tag.TagName} is writing back.");
                }

                client.SetBitValue(tag.plcTag, 0, Convert.ToBoolean(0), DataTimeout);
            }
            return isOK;
        }

        private clsTagValue RetrieveTagValue(Libplctag client, clsHierarchyTag tagClass)
        {
            Tag tag = tagClass.plcTag;

            int counter = 0;
            while (client.GetStatus(tag) == Libplctag.PLCTAG_STATUS_PENDING && counter ++ < 100)
            {
                Thread.Sleep(100);
            }

            if (client.GetStatus(tag) == Libplctag.PLCTAG_STATUS_PENDING)
            {
                if (SystemKeys.IN_DEBUGING)
                {
                    clsLog.addLog($@"{tagClass.TagName} is Pending status.");
                }
                return null;
            }

            int tagStatus = client.GetStatus(tag);
            if (tagStatus != Libplctag.PLCTAG_STATUS_OK)
            {
                clsLog.addLog($"[{tagClass.TagName}] error: setting up tag internal state: {client.DecodeError(tagStatus)}");
                return null;
            }

            var tagValue = client.ReadTag(tag, DataTimeout);

            if (tagValue != Libplctag.PLCTAG_STATUS_OK)
            {
                clsLog.addLog($"[{tagClass.TagName}] error: Unable to read the data! Got error code {tagValue}: {client.DecodeError(tagValue)}");
                return null;
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

            return new clsTagValue(tagClass, readValue);
        }

        private void SaveToFile(IDictionary<int, clsTagValue> tagValue, int controllerId)
        {
            clsOutput op = new clsOutputAlarm();
            string ipAddress = string.Empty;
            foreach (clsController clsCon in listController)
            {
                if (clsCon.Id == controllerId)
                {
                    ipAddress = clsCon.GetIPAddressAndDescription();
                    break;
                }
            }
            if (string.IsNullOrWhiteSpace(ipAddress))
            {
                clsLog.addLog($"AlarmDetails.SaveToFile: Cannot find ipAddress of {controllerId}");
            }
            else
            {
                op.SaveToFileAndDatabase(tagValue, -99, ipAddress, 0);
            }
        }
    }
}
