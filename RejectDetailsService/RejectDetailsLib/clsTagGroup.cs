using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibplctagWrapper;

namespace RejectDetailsLib {
    public class clsTagGroup {

        //private List<clsTag> m_listTags;
        //private clsTag m_tagRead;

        public clsTag tagRead {
            get;
            set;
        }

        public List<clsTag> listTags {
            get;
            set;
        }

        public List<clsTag> tagWrite {
            get;
            set;
        }

        public int controllerId {
            get;
            internal set;
        }

        public Libplctag tagClass {
            get;
            set;
        }

        private clsTagGroup() {
            tagClass = new Libplctag();
        }

        private clsTagGroup(clsTag readTag, int controlId) : this() {
            tagRead = readTag;
            controllerId = controlId;
        }

        public void GetTags(string IpAddress) {

            if(tagRead != null && !string.IsNullOrWhiteSpace(tagRead.TagName)) {
                tagRead.GenerateTag(IpAddress);
                this.tagClass.AddTag(tagRead.plcTag);

                string prefix = "";
                if(tagRead.TagName.IndexOf('.') >= 0) {
                    int lastIndex = tagRead.TagName.IndexOf('.');
                    prefix = tagRead.TagName.Substring(0, lastIndex + 1);
                }
                List<clsTag> tagList = new Database().GetTagGroup(prefix, this.controllerId);

                tagWrite = new List<clsTag>();
                listTags = new List<clsTag>();

                foreach(clsTag tag in tagList) {
                    tag.GenerateTag(IpAddress);
                    listTags.Add(tag);

                    if(tag.Write == 1) {
                        tagWrite.Add(tag);
                    }

                    this.tagClass.AddTag(tag.plcTag);
                }
            }
        }

        public void GetTagsNoStation(string IpAddress)
        {
            if (tagRead != null && !string.IsNullOrWhiteSpace(tagRead.TagName))
            {
                tagRead.GenerateTag(IpAddress);
                this.tagClass.AddTag(tagRead.plcTag);

                List<clsTag> tagList = new Database().GetTagGroup(this.controllerId);

                tagWrite = new List<clsTag>();
                listTags = new List<clsTag>();

                foreach (clsTag tag in tagList)
                {
                    tag.GenerateTag(IpAddress);
                    listTags.Add(tag);

                    if (tag.Write == 1)
                    {
                        tagWrite.Add(tag);
                    }

                    this.tagClass.AddTag(tag.plcTag);
                }
            } 
        }
        public static List<clsTagGroup> GetGroup(int ControllerID, string IpAddress) {
            List<clsTagGroup> list = new List<clsTagGroup>();

            List<clsTag> listTags = new Database().GetReadTags(ControllerID);

            foreach(clsTag readTag in listTags) {
                clsTagGroup group = new clsTagGroup(readTag, ControllerID);
                group.GetTags(IpAddress);

                list.Add(group);
            }

            return list;
        }

        public static List<clsTagGroup> GetGroup(int ControllerID, clsController clsCon)
        {
            List<clsTagGroup> list = new List<clsTagGroup>();

            List<clsTag> listReadTags = new Database().GetReadTags(ControllerID);

            if (listReadTags != null && listReadTags.Any())
            {

                if (clsCon.IsStatistics)
                {
                    foreach (clsTag readTag in listReadTags)
                    {
                        clsTagGroup group = new clsTagGroup(readTag, ControllerID);
                        group.GetTagsNoStation(clsCon.IpAddress);

                        list.Add(group);
                    }
                }
                else
                {
                    foreach (clsTag readTag in listReadTags)
                    {
                        clsTagGroup group = new clsTagGroup(readTag, ControllerID);
                        group.GetTags(clsCon.IpAddress);

                        list.Add(group);
                    }
                }
            }
            else
            {
                clsLog.addLog("no read tag setup.......");
            }
            return list;
        }
    }
}
