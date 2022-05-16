using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RejectDetailsService {
    public class clsTag {
        public int StationId { get; set; }

        public string StationName { get; set; }
        public int TagId { get; set; }
        
        public string TagName { get; set; }

        //public string SubName { get; set; }

        //public string ProcessTag { get; set; }

        public string TagType { get; set; }

        public string Comment { get; set; }

        public int ReadWrite { get; set; }

        //public string IpAddress { get; set; }

        public string getTagPath() {
            return $@"Station0{StationName}.{TagName}";
        }
    }
}
