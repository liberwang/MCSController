using System.Collections.Generic;

namespace RejectDetailsLib {
    public class clsController {
        public int Id { get; set; }

        public string IpAddress { get; set; }

        public string Description { get; set; }

        public int CpuTypeId { get; set; }

        public bool isEnabled {
            get;
            set;
        }

        public List<clsStation> StationList { get; set; }

        public void GetStationList() {
            if(this.Id > 0) {
                if(SystemKeys.GET_DATA_FROM_XML)
                    this.StationList = new DataXML().GetStations(Id);
                else
                    this.StationList = new Database().GetStations(Id);
            }
        }
    }
}
