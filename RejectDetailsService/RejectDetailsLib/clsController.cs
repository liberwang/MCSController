using LibplctagWrapper;
using System.Collections.Generic;
using System.Data;

namespace RejectDetailsLib
{
    public class clsController
    {
        public const string ALL_STRING_CONTROLLER = "All";
        public const int ALL_VALUE_CONTROLLER = -1;

        public int Id { get; set; }

        public string IpAddress { get; set; }

        public string Description { get; set; }

        public int CpuTypeId { get; set; }

        public bool IsEnabled { get; set; }

        public bool IsStatistics { get; set; }

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

        public static List<clsController> GetControllerList(bool bRefresh = false)
        {
            if (ControllerList == null || bRefresh)
            {
                ControllerList = new Database().GetControllerList(true);
            }
            return ControllerList;
        }

        public static List<clsController> GetAllControllerList()
        {
            return new Database().GetAllControllerList();
        }

        public static DataTable GetControllerDataTable()
        {
            return new Database().GetControllerDataSet().Tables[0];
        }


        public static List<clsController> GetControllerItemDataSource(bool withAllOption = true, bool bRefresh = false)
        {
            List<clsController> list = GetControllerList(bRefresh);
            if (withAllOption)
            {
                list.Insert(0, new clsController (ALL_VALUE_CONTROLLER, ALL_STRING_CONTROLLER, ALL_STRING_CONTROLLER));
            }


            return list;
        }

        public void SaveController()
        {
            new Database().SetIPAddress(this.IpAddress, this.Description,(int)CpuType.LGX, this.IsEnabled ? 1 : 0);
        }
    }
}
