using LibplctagWrapper;
using System.Collections.Generic;
using System.Data;

namespace RejectDetailsLib
{
    public class clsController
    {
        public int Id { get; set; }

        public string IpAddress { get; set; }

        public string Description { get; set; }

        public int CpuTypeId { get; set; }

        public bool IsEnabled { get; set; }

        private static List<clsController> ControllerList { get; set; }

        public static void RefreshController()
        {
            ControllerList = null;
        }

        public static List<clsController> GetControllerList()
        {
            if (ControllerList == null)
            {
                ControllerList = new Database().GetControllerList(true);
            }
            return ControllerList;
        }

        public static DataTable GetControllerDataTable()
        {
            return new Database().GetControllerDataSet().Tables[0];
        }


        public static List<object> GetControllerItemDataSource(bool withAllOption = true)
        {
            List<object> list = new List<object>();
            if ( withAllOption)
            {
                list.Add(new { Text = "All", Value = -1 });
            }
            foreach( clsController contr in GetControllerList() ) {
                list.Add(new { Text = contr.Description, Value = contr.Id });
            }

            return list;
        }

        public void SaveController()
        {
            new Database().SetIPAddress(this.IpAddress, this.Description,(int)CpuType.LGX, this.IsEnabled ? 1 : 0);
        }
    }
}
