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

        public clsController() { }

        public clsController(int id, string IpAddr, string desc ) : this()
        {
            Id = id;
            IpAddress = IpAddr;
            Description = desc;
        }

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


        public static List<clsController> GetControllerItemDataSource(bool withAllOption = true)
        {
            List<clsController> list = GetControllerList();
            if (withAllOption)
            {
                list.Insert(0, new clsController (-1, "All", "All" ));
            }


            return list;
        }

        public void SaveController()
        {
            new Database().SetIPAddress(this.IpAddress, this.Description,(int)CpuType.LGX, this.IsEnabled ? 1 : 0);
        }
    }
}
