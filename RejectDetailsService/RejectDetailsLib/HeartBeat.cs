using LibplctagWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RejectDetailsLib
{
    public class HeartBeat
    {
        private const int DataTimeout = 5000;
        private static bool IsRunning = false;

        private static object lockobject = new object();

        private static HeartBeat instance = null;

        private List<clsTag> listTag = new List<clsTag>();

        private HeartBeat() { }

        public static HeartBeat Instance
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
                                instance = new HeartBeat();
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
            List<clsController> listController = clsController.GetControllerList().Where(x => !x.IsStatistics).ToList();

            if (listController == null || listController.Count == 0)
            {
                throw new Exception("Can not find enabled ip address for Heartbeat..........");
            }
            else
            {
                foreach (clsController clsCon in listController)
                {
                    clsTag hbTag = new clsTag() { TagName = SystemKeys.HEARTBEAT_TAG_NAME, TagType = clsTag.BOOL_TYPE_STR };
                    hbTag.GenerateTag(clsCon.IpAddress);
                    this.listTag.Add(hbTag);
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

        private void Process()
        {
            try
            {
                foreach (clsTag tag in this.listTag)
                {
                    Tag tagHeartBeat = tag.plcTag;

                    Libplctag client = new Libplctag();
                    client.AddTag(tagHeartBeat);    // note, it has to add the related into client, otherwise raise error.

                    if (SystemKeys.IN_DEBUGING)
                    {
                        clsLog.addLog($@"HeartBeat of {tag.IPAddress} is adding.");
                    }

                    int pendingCounter = 100;
                    while (client.GetStatus(tagHeartBeat) == Libplctag.PLCTAG_STATUS_PENDING && pendingCounter > 0)
                    {
                        Thread.Sleep(100);
                        --pendingCounter;
                    }

                    if (pendingCounter <= 0) {
                        clsLog.addLog($@"HeartBeat of {tag.IPAddress} is always pending, and not connect to PLC.");
                        continue;
                    }

                    int tagStatus = client.GetStatus(tagHeartBeat);

                    if (tagStatus == Libplctag.PLCTAG_STATUS_OK)
                    {
                        var tagValue = client.ReadTag(tagHeartBeat, DataTimeout);

                        if (tagValue != Libplctag.PLCTAG_STATUS_OK)
                        {
                            clsLog.addLog($"HeartBeat Unable to read the data! Got error code {tagValue}: {client.DecodeError(tagValue)}");
                            continue;
                        }

                        bool readValue = client.GetBitValue(tagHeartBeat, -1, DataTimeout);

                        if (SystemKeys.IN_DEBUGING)
                        {
                            clsLog.addLog($@"HeartBeat of {tag.IPAddress} gets value {readValue}.");
                        }

                        if (readValue)
                        {
                            if (SystemKeys.IN_DEBUGING)
                            {
                                clsLog.addLog($@"HeartBeat of {tag.IPAddress} sets off value back.");
                            }
                            client.SetBitValue(tagHeartBeat, 0, Convert.ToBoolean(0), DataTimeout);
                        }
                    } else
                    {
                        if (SystemKeys.IN_DEBUGING)
                        {
                            clsLog.addLog($@"HeartBeat of {tag.IPAddress} is not OK status: {tagStatus}.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
