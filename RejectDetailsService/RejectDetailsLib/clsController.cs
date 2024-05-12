using LibplctagWrapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;

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

        public static List<clsController> GetControllerList(bool bRefresh = false, bool bEnabled = true)
        {
            if (ControllerList == null || bRefresh)
            {
                ControllerList = GetAllControllerList(); //.Where(x => x.IsEnabled && ! x.IsStatistics).ToList();   //new Database().GetControllerList(true);
            }

            return ControllerList.Where( x => x.IsEnabled == bEnabled).ToList();
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
            new Database().SetIPAddress(this.Id, this.IpAddress, this.Description,(int)CpuType.LGX, this.IsEnabled ? 1 : 0, this.IsStatistics ? 1 : 0);
        }

        public bool IsDuplicate()
        {
            return new Database().CheckIPAddressDuplicate(this.Id, this.IpAddress, this.Description);
        }

        public string GetIPAddressAndDescription()
        {
            return this.IpAddress + ":" + this.Description;
        }
    }
}
