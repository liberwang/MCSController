using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RejectDetailsLib {
    public class clsStation {
        public int Id { get; set; }
        public string Name { get; set; }

        public int ControllerId { get; set; }

        public List<clsTag> TagList { get; set; }

        public void GetTagList() {
            if (this.Id > 0) {
                TagList = Database.GetTagInformation(Id, Name);
            }
        }
    }
}
