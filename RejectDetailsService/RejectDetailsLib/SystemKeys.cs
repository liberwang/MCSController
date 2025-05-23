﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.NetworkInformation;

namespace RejectDetailsLib {
    public class SystemKeys {
        public static string DB_CONNECT = @"Server=.\SQLExpress;Database=MCS;User Id=mcs;Password=mcs";
        public static readonly string DB_REMOTE = @"Server={0},1433\SQLExpress;Database=MCS;User Id=mcs;Password=mcs";

        public const string PRODUCE_NAME_HONDA_BULKHEAD = "HONDA-BULKHEAD";

        public static string PRODUCT_NAME;

        public static string FILE_FOLDER;
        public static string FILE_NAME_PREFIX;
        public static string FILE_NAME;
        public static string FILE_NAME_EXT;
        public static string COPY_FOLDER;
        public static string COPY_FILE_EXT;
        public static string COPY_FILE_PREFIX;
        public static string REJECT_FILE_PREFIX;

        public static int COPY_INTERVAL;
        public static string LOG_FILE = @"c:\temp\MCSLog";
        public static string IP_ADDRESS_THIS;
        public static bool SAVE_TO_FILE;
        public static bool SAVE_TO_DB;
        public static string GENERATE_OUTPUT_FILE_TIME;
        public static bool GET_DATA_FROM_XML;


        public static int VISIT_INTERVAL;
        public static bool STATISTICS_SERVICE_ENABLE;
        public static int STATISTICS_SERVICE_INTERVAL;
        public static bool HEARTBEAT_SERVICE_ENABLE;
        public static int HEARTBEAT_SERVICE_INTERVAL;
        public static string HEARTBEAT_SERVICE_TAGNAME;
        public static bool ALARM_SERVICE_ENABLE;
        public static int ALARM_SERVICE_INTERVAL;
        public static bool USE_MULTITHREADING_SERVICE;
        public static bool IN_DEBUGING;

        public const string PRODUCT_NAME_KEY = "ProductName";

        public const string FILE_FOLDER_KEY = "OutputFileFolder";
        public const string FILE_NAME_PREFIX_KEY = "OutputFileNamePrefix";
        public const string FILE_NAME_KEY = "OutputFileName";
        public const string FILE_NAME_EXT_KEY = "OutputFileNameExt";
        public const string COPY_FOLDER_KEY = "CopyFolder";
        public const string COPY_FILE_EXT_KEY = "CopyFileExt";
        public const string COPY_FILE_PREFIX_KEY = "CopyFilePrefix";
        public const string REJECT_FILE_PREFIX_KEY = "RejectFilePrefix";
        public const string COPY_INTERVAL_KEY = "CopyInterval";
        public const string LOG_FILE_KEY = "LogFileFolder";
        public const string DB_CONNECT_STRING_KEY = "DBConnect";
        public const string IP_ADDRESS_THIS_KEY = "ThisIP";
        public const string SAVE_TO_FILE_KEY = "SaveToFile";
        public const string SAVE_TO_DB_KEY = "SaveToDB";
        public const string GENERATE_OUTPUT_FILE_TIME_KEY = "GenerateOutputFileFrom";
        public const string GET_DATA_FROM_XML_KEY = "GetDataFromXML";

        public const string VISIT_INTERVAL_KEY = "VisitInterval";
        public const string STATISTICS_SERVICE_ENABLE_KEY = "StatisticsServiceEnable";
        public static string STATISTICS_SERVICE_INTERVAL_KEY = "StatisticsServiceInterval";
        public static string HEARTBEAT_SERVICE_ENABLE_KEY = "HeartBeatServiceEnable";
        public static string HEARTBEAT_SERVICE_INTERVAL_KEY = "HeartBeatServiceInterval";
        public static string HEARTBEAT_SERVICE_TAGNAME_KEY = "HeartBeatServiceTagName";
        public static string ALARM_SERVICE_ENABLE_KEY = "AlarmServiceEnable";
        public static string ALARM_SERVICE_INTERVAL_KEY = "AlarmServiceInterval";

        public const string USE_MULTITHREADING_SERVICE_KEY = "UseMultiTreading";
        public const string IN_DEBUGING_KEY = "InDebuging";

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
            FILE_NAME_PREFIX = keys.ContainsKey(FILE_NAME_PREFIX_KEY) ? keys[FILE_NAME_PREFIX_KEY] : @"TagData-";
            FILE_NAME = keys.ContainsKey(FILE_NAME_KEY) ? keys[FILE_NAME_KEY] : @"yyy-MM-dd";
            FILE_NAME_EXT = keys.ContainsKey(FILE_NAME_EXT_KEY) ? keys[FILE_NAME_EXT_KEY] : "csv";
            COPY_FOLDER = keys.ContainsKey(COPY_FOLDER_KEY) ? keys[COPY_FOLDER_KEY] : @"c:\temp";
            COPY_FILE_PREFIX = keys.ContainsKey(COPY_FILE_PREFIX_KEY) ? keys[COPY_FILE_PREFIX_KEY] : @"RejectDetails-";
            COPY_FILE_EXT = keys.ContainsKey(COPY_FILE_EXT_KEY) ? keys[COPY_FILE_EXT_KEY] : @"csv";
            REJECT_FILE_PREFIX = keys.ContainsKey(REJECT_FILE_PREFIX_KEY) ? keys[REJECT_FILE_PREFIX_KEY] : "RejectDetails-";
            VISIT_INTERVAL = keys.ContainsKey(VISIT_INTERVAL_KEY) ? int.Parse(keys[VISIT_INTERVAL_KEY]) : 500;
            COPY_INTERVAL = keys.ContainsKey(COPY_INTERVAL_KEY) ? int.Parse(keys[COPY_INTERVAL_KEY]) : 31000;
            LOG_FILE = keys.ContainsKey(LOG_FILE_KEY) ? keys[LOG_FILE_KEY] : @"c:\temp\MCSLog";

            if (keys.ContainsKey(SAVE_TO_FILE_KEY) && bool.TryParse(keys[SAVE_TO_FILE_KEY], out bool result1))
            {
                SAVE_TO_FILE = result1;
            } else
            {
                SAVE_TO_FILE = false;
            }

            if(keys.ContainsKey(SAVE_TO_DB_KEY) && bool.TryParse(keys[SAVE_TO_DB_KEY], out bool result2)) {
                SAVE_TO_DB = result2;
            } else {
                SAVE_TO_DB = true;
            }
            GENERATE_OUTPUT_FILE_TIME = keys.ContainsKey(GENERATE_OUTPUT_FILE_TIME_KEY) ? keys[GENERATE_OUTPUT_FILE_TIME_KEY] : "00";
            if(keys.ContainsKey(USE_MULTITHREADING_SERVICE_KEY) && bool.TryParse(keys[USE_MULTITHREADING_SERVICE_KEY], out bool result3)) {
                USE_MULTITHREADING_SERVICE = result3;
            } else {
                USE_MULTITHREADING_SERVICE = false;
            }

            if(keys.ContainsKey(IN_DEBUGING_KEY) && bool.TryParse(keys[IN_DEBUGING_KEY], out bool debugResult))
            {
                IN_DEBUGING = debugResult;
            } else
            {
                IN_DEBUGING = false;
            }

            // Heart Beat
            if(keys.ContainsKey(HEARTBEAT_SERVICE_ENABLE_KEY) && bool.TryParse(keys[HEARTBEAT_SERVICE_ENABLE_KEY], out bool heartBeatEnable))
            {
                HEARTBEAT_SERVICE_ENABLE = heartBeatEnable;
            } else
            {
                HEARTBEAT_SERVICE_ENABLE = false;
            }

            HEARTBEAT_SERVICE_INTERVAL = keys.ContainsKey(HEARTBEAT_SERVICE_INTERVAL_KEY) ? int.Parse(keys[HEARTBEAT_SERVICE_INTERVAL_KEY]) : 500;
            HEARTBEAT_SERVICE_TAGNAME = keys.ContainsKey(HEARTBEAT_SERVICE_TAGNAME_KEY) ? keys[HEARTBEAT_SERVICE_TAGNAME_KEY] : "dbconnection";

            // Statistics 
            if (keys.ContainsKey(STATISTICS_SERVICE_ENABLE_KEY) && bool.TryParse(keys[STATISTICS_SERVICE_ENABLE_KEY], out bool StatisticsEnable))
            {
                STATISTICS_SERVICE_ENABLE = StatisticsEnable;
            }
            else
            {
                STATISTICS_SERVICE_ENABLE = false;
            }

            STATISTICS_SERVICE_INTERVAL = keys.ContainsKey(STATISTICS_SERVICE_INTERVAL_KEY) ? int.Parse(keys[STATISTICS_SERVICE_INTERVAL_KEY]) : 500;

            // alarm service
            if (keys.ContainsKey(ALARM_SERVICE_ENABLE_KEY) && bool.TryParse(keys[ALARM_SERVICE_ENABLE_KEY], out bool AlarmEnable))
            {
                ALARM_SERVICE_ENABLE = AlarmEnable;
            }
            else
            {
                ALARM_SERVICE_ENABLE = false;
            }

            ALARM_SERVICE_INTERVAL = keys.ContainsKey(ALARM_SERVICE_INTERVAL_KEY) ? int.Parse(keys[ALARM_SERVICE_INTERVAL_KEY]) : 500;
        }

        public static void setKey(string appKey, string appValue) {
            Database db = new Database();
            db.SetSystemSetting(appKey, appValue);
        }

        public static bool IsHondaBulkHead()
        {
            return PRODUCT_NAME == PRODUCE_NAME_HONDA_BULKHEAD;
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
                string fileName = sFilePrefix + GetFileNameDateString() + (string.IsNullOrEmpty(FILE_NAME_EXT) ? "" : ("." + FILE_NAME_EXT));
                return Path.Combine (FILE_FOLDER, fileName);
            }
        }

        public static string getCopyFileName(string fileNamePrex = null) {
            if (string.IsNullOrWhiteSpace(fileNamePrex))
            {
                return Path.Combine(COPY_FOLDER, COPY_FILE_PREFIX + GetFileNameDateString() + "." + COPY_FILE_EXT);
            } else
            {
                return Path.Combine(COPY_FOLDER, fileNamePrex + GetFileNameDateString() + "." + COPY_FILE_EXT);
            }
        }

        public static string getCurrentDateTime() {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string getLogName(string prefix = "") {
            if ( ! Directory.Exists(LOG_FILE))
            {
                Directory.CreateDirectory(LOG_FILE);
            }

            if (! string.IsNullOrWhiteSpace(prefix) && ! prefix.EndsWith("."))
            {
                prefix += ".";
            }

            return Path.Combine(LOG_FILE, prefix + getLogFileNameDateString() + ".txt");
            /*if(String.IsNullOrWhiteSpace(Path.GetExtension(LOG_FILE))) {
                return LOG_FILE + getLogFileNameDateString() + ".txt";
            } else {
                return LOG_FILE.Substring(0, LOG_FILE.Length - 4) + getLogFileNameDateString() + LOG_FILE.Substring(LOG_FILE.Length - 4);
            }*/
        }


        #region Private Methods
        private static string getLogFileNameDateString()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }

        private static string getFileName()
        {
            return FILE_NAME_PREFIX + GetFileNameDateString() + (string.IsNullOrWhiteSpace(FILE_NAME_EXT) ? "" : ("." + FILE_NAME_EXT));
        }

        private static string GetFileNameDateString()
        {
            DateTime now = DateTime.Now;
            if (! int.TryParse(GENERATE_OUTPUT_FILE_TIME, out int hoursDelimt) )
            {
                hoursDelimt = 0;
            }

            DateTime dt = new DateTime(now.Year, now.Month, now.Day, hoursDelimt, 0, 0);

            if (now < dt)
            {
                return now.ToString(FILE_NAME);
            }
            else
            {
                return dt.AddDays(1).ToString(FILE_NAME);
            }
        }
        #endregion
    }
}
