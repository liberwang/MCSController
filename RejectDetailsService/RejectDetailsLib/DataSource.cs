using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RejectDetailsLib {
    public interface DataSource {
        List<clsController> GetController();

        List<clsStation> GetStations(int ControllerID);

        //List<clsTag> GetTagInformation(int StationID, string StationName);

        //Dictionary<string, string> GetReadWriteTag();
    }
}
