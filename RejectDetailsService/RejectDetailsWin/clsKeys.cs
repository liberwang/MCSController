using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace RejectDetailsWin {
    public class clsKeys {
        public static readonly string DB_STR;
        public static readonly string[] DB_LIST;

        //public const string LOCAL_HOST_STRING = "Localhost";
        private const string REMOTE_DB_LIST_KEY = "RemoteDBList";

        static clsKeys() {
            var appSetings = ConfigurationManager.AppSettings;

            DB_STR = appSetings[REMOTE_DB_LIST_KEY]??"";

            if (! string.IsNullOrWhiteSpace(DB_STR))
            {
                if (DB_STR.EndsWith(";"))
                {
                    DB_LIST = DB_STR.Substring(0, DB_STR.Length - 1).Split(';');
                }
                else
                {
                    DB_LIST = DB_STR.Split(';');
                }
            }
        }
    }
}
