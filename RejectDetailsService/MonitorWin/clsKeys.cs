using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace MonitorWin
{
    internal class clsKeys
    {
        private const char DB_DELIMITER = ';';
        private const char NAME_DELIMTER = '|';

        public static readonly List<List<string>> DB_LIST;

        public const string LOCAL_HOST_STRING = "Localhost";
        private const string REMOTE_DB_LIST_KEY = "RemoteDBList";

        static clsKeys()
        {
            var appSetings = ConfigurationManager.AppSettings;

            string db_str = appSetings[REMOTE_DB_LIST_KEY];
            if (db_str[db_str.Length-1] == DB_DELIMITER)
                db_str = db_str.Substring(0, db_str.Length - 1);

            string[] db_array = db_str.Split(DB_DELIMITER);

            DB_LIST = new List<List<string>>();

            foreach( string dbname in db_array )
            {
                DB_LIST.Add(dbname.Split(NAME_DELIMTER).ToList());  
            }
        }
    }
}
