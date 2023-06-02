using System.Collections.Generic;

namespace RejectDetailsLib {
    public class clsStation {
        public int Id { get; set; }
        public string Name { get; set; }

        public int ControllerId { get; set; }

        public List<clsTag> TagList { get; set; }

        //public void GetTagList() {
        //    if(this.Id > 0) {
        //        if(SystemKeys.GET_DATA_FROM_XML)
        //            TagList = new DataXML().GetTagInformation(Id, Name);
        //        else
        //            TagList = new Database().GetTagInformation(Id, Name);
        //    }
        //}
    }
}
