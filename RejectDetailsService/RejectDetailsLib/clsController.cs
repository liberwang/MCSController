using System.Collections.Generic;

namespace RejectDetailsLib {
    public class clsController {
        public int Id { get; set; }

        public string IpAddress { get; set; }

        public string Description { get; set; }

        public int CpuTypeId { get; set; }

        public List<clsStation> StationList { get; set; }

        public void GetStationList() {
            if (this.Id > 0 )
                this.StationList = Database.GetStations(Id);
        }
    }
}
