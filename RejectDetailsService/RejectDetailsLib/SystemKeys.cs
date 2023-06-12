using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.NetworkInformation;

namespace RejectDetailsLib {
    public class SystemKeys {
        public static string DB_CONNECT = @"Server=.\SQLExpress;Database=MCS;User Id=mcs;Password=mcs";
        public static readonly string DB_REMOTE = @"Server={0},1433\SQLExpress;Database=MCS;User Id=mcs;Password=mcs";
        //public static readonly string DB_LOCAL = @"Server=.\SQLExpress;Database=MCS;User Id=mcs;Password=mcs";

        public const string PRODUCE_NAME_HONDA_BULKHEAD = "HONDA-BULKHEAD";

        public static string PRODUCT_NAME;
        public static string FILE_FOLDER;
        public static string FILE_NAME_PREFIX;
        public static string FILE_NAME;
        public static string FILE_NAME_EXT;
        public static string COPY_FOLDER;
        public static string COPY_FILE_EXT;
        public static string COPY_FILE_PREFIX;
        public static int VISIT_INTERVAL;
        public static int COPY_INTERVAL;
        public static string LOG_FILE;
        public static string IP_ADDRESS_THIS;
        public static bool SAVE_TO_FILE;
        public static bool SAVE_TO_DB;
        public static bool GET_DATA_FROM_XML;

        public const string PRODUCT_NAME_KEY = "ProductName";
        public const string FILE_FOLDER_KEY = "OutputFileFolder";
        public const string FILE_NAME_PREFIX_KEY = "OutputFileNamePrefix";
        public const string FILE_NAME_KEY = "OutputFileName";
        public const string FILE_NAME_EXT_KEY = "OutputFileNameExt";
        public const string COPY_FOLDER_KEY = "CopyFolder";
        public const string COPY_FILE_EXT_KEY = "CopyFileExt";
        public const string COPY_FILE_PREFIX_KEY = "CopyFilePrefix";
        public const string VISIT_INTERVAL_KEY = "VisitInterval";
        public const string COPY_INTERVAL_KEY = "CopyInterval";
        public const string LOG_FILE_KEY = "LogFileFolder";
        public const string DB_CONNECT_STRING_KEY = "DBConnect";
        public const string IP_ADDRESS_THIS_KEY = "ThisIP";
        public const string SAVE_TO_FILE_KEY = "SaveToFile";
        public const string SAVE_TO_DB_KEY = "SaveToDB";
        public const string GET_DATA_FROM_XML_KEY = "GetDataFromXML";

        //static SystemKeys() {
        //    initializeKey();
        //}

        public static void SetDBConnect(string sConnect)
        {
            DB_CONNECT = sConnect;
            initializeKey(); 
        }

        public static void initializeKey() {
            Database db = new Database();
            Dictionary<string, string> keys = db.GetSystemSettings();

            PRODUCT_NAME = keys.ContainsKey(PRODUCT_NAME_KEY) ? keys[PRODUCT_NAME_KEY] : "HONDA-BULKHEAD";
            FILE_FOLDER = keys.ContainsKey(FILE_FOLDER_KEY) ? keys[FILE_FOLDER_KEY] : @"c:\temp";
            FILE_NAME_PREFIX = keys.ContainsKey(FILE_NAME_PREFIX_KEY) ? keys[FILE_NAME_PREFIX_KEY] : @"tag-";
            FILE_NAME = keys.ContainsKey(FILE_NAME_KEY) ? keys[FILE_NAME_KEY] : @"yyy-MM-dd";
            FILE_NAME_EXT = keys.ContainsKey(FILE_NAME_EXT_KEY) ? keys[FILE_NAME_EXT_KEY] : "csv";
            COPY_FOLDER = keys.ContainsKey(COPY_FOLDER_KEY) ? keys[COPY_FOLDER_KEY] : @"c:\temp";
            COPY_FILE_PREFIX = keys.ContainsKey(COPY_FILE_PREFIX_KEY) ? keys[COPY_FILE_PREFIX_KEY] : @"RejectDetails-tag-";
            COPY_FILE_EXT = keys.ContainsKey(COPY_FILE_EXT_KEY) ? keys[COPY_FILE_EXT_KEY] : @"csv";
            VISIT_INTERVAL = keys.ContainsKey(VISIT_INTERVAL_KEY) ? int.Parse(keys[VISIT_INTERVAL_KEY]) : 500;
            COPY_INTERVAL = keys.ContainsKey(COPY_INTERVAL_KEY) ? int.Parse(keys[COPY_INTERVAL_KEY]) : 31000;
            LOG_FILE = keys.ContainsKey(LOG_FILE_KEY) ? keys[LOG_FILE_KEY] : @"c:\temp\log";

            if(keys.ContainsKey(SAVE_TO_FILE_KEY) && bool.TryParse(keys[SAVE_TO_FILE_KEY], out bool result1))
                SAVE_TO_FILE = result1;

            if(keys.ContainsKey(SAVE_TO_DB_KEY) && bool.TryParse(keys[SAVE_TO_DB_KEY], out bool result2)) {
                SAVE_TO_DB = result2;
            } else {
                SAVE_TO_DB = true;
            }
        }

        public static void setKey(string appKey, string appValue) {
            //var appSetings = ConfigurationManager.AppSettings;
            //appSetings[appKey] = appValue;
            Database db = new Database();
            db.SetSystemSetting(appKey, appValue);
        }


        public static bool IsHondaBulkHead()
        {
            return PRODUCT_NAME == PRODUCE_NAME_HONDA_BULKHEAD;
        }

        private static string getFileNameDateString() {
            return DateTime.Now.ToString(FILE_NAME);
        }
        private static string getFileName() {
            return FILE_NAME_PREFIX + getFileNameDateString() + (string.IsNullOrEmpty(FILE_NAME_EXT) ? "" : ("." + FILE_NAME_EXT));
        }

        public static string getFullFileName() {
            return Path.Combine(FILE_FOLDER, getFileName());
        }

        public static string getFullFileName(string sFilePrefix)
        {
            if ( string.IsNullOrEmpty(sFilePrefix) )
            {
                return getFullFileName();
            } else
            {
                string fileName = sFilePrefix + getFileNameDateString() + (string.IsNullOrEmpty(FILE_NAME_EXT) ? "" : ("." + FILE_NAME_EXT));
                return Path.Combine (FILE_FOLDER, fileName);
            }
        }

        public static string getCopyFileName(string fileNamePrex = null) {
            if (string.IsNullOrWhiteSpace(fileNamePrex))
            {
                return Path.Combine(COPY_FOLDER, COPY_FILE_PREFIX + getFileNameDateString() + "." + COPY_FILE_EXT);
            } else
            {
                return Path.Combine(COPY_FOLDER, fileNamePrex + getFileNameDateString() + "." + COPY_FILE_EXT);
            }
        }

        public static string getCurrentDateTime() {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string getLogName() {
            if(String.IsNullOrWhiteSpace(Path.GetExtension(LOG_FILE))) {
                return LOG_FILE + getFileNameDateString() + ".txt";
            } else {
                return LOG_FILE.Substring(0, LOG_FILE.Length - 4) + getFileNameDateString() + LOG_FILE.Substring(LOG_FILE.Length - 4);
            }
        }
    }
}
