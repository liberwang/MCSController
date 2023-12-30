using System.Collections.Generic;

namespace RejectDetailsLib {
    public class clsStation
    {
        public static List<string> GetStationList(int ControllerID)
        {
            if (ControllerID < 0)
            {
                return new List<string>();
            }
            else
            {
                return new Database().GetFirstLevelGroup(ControllerID);
            }
        }
    }
}
