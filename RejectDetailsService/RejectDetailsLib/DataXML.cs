using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RejectDetailsLib {
    public class DataXML : DataSource {
        public List<clsController> GetController() {
            List<clsController> list = new List<clsController>();

            return list;
        }

        public Dictionary<string, string> GetReadWriteTag() {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            return dic;    
        }

        public List<clsStation> GetStations(int ControllerID) {
            List<clsStation> list = new List<clsStation>();

            return list;
        }

        public List<clsTag> GetTagInformation(int StationID, string StationName) {
            List<clsTag> list = new List<clsTag>();

            return list;
        }
    }
}
