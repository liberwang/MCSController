﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace RejectDetailsLib {
    public class SystemKeys {
        public static readonly string DB_CONNECT= @"Server=.\SQLExpress;Database=MCS;User Id=mcs;Password=mcs";

        public static readonly string FILE_FOLDER;
        public static readonly string FILE_NAME_PREFIX;
        public static readonly string FILE_NAME;
        public static readonly string FILE_NAME_EXT;
        public static readonly string COPY_FOLDER;
        public static readonly string COPY_FILE_EXT;
        public static readonly string COPY_FILE_PREFIX;
        public static readonly int VISIT_INTERVAL;
        public static readonly int COPY_INTERVAL;
        public static readonly string LOG_FILE;
        public static readonly string IP_ADDRESS_THIS;
        public static readonly bool SAVE_TO_FILE;
        public static readonly bool SAVE_TO_DB;
        public static readonly bool GET_DATA_FROM_XML;

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

        static SystemKeys() {
            /*var appSetings = ConfigurationManager.AppSettings;

            FILE_FOLDER = appSetings[FILE_FOLDER_KEY] ?? @"c:\temp";
            FILE_NAME_PREFIX = appSetings[FILE_NAME_PREFIX_KEY] ?? @"tag-";
            FILE_NAME = appSetings[FILE_NAME_KEY] ?? @"yyy-MM-dd";
            FILE_NAME_EXT = appSetings[FILE_NAME_EXT_KEY] ?? "";
            COPY_FOLDER = appSetings[COPY_FOLDER_KEY] ?? @"c:\temp";
            COPY_FILE_PREFIX = appSetings[COPY_FILE_PREFIX_KEY] ?? @"RejectDetails-tag-";
            COPY_FILE_EXT = appSetings[COPY_FILE_EXT_KEY] ?? @"csv";
            VISIT_INTERVAL = int.Parse(appSetings[VISIT_INTERVAL_KEY] ?? @"500");
            COPY_INTERVAL = int.Parse(appSetings[COPY_INTERVAL_KEY] ?? @"31000");
            LOG_FILE = appSetings[LOG_FILE_KEY] ?? @"c:\temp\log";
            DB_CONNECT = (appSetings[DB_CONNECT_STRING_KEY] ?? @"Server=.\SQLExpress;Database=MCS;User Id=mcs;Password=") + "mcs";
            IP_ADDRESS_THIS = appSetings[IP_ADDRESS_THIS_KEY] ?? @"192.168.0.100";
            if (bool.TryParse(appSetings[SAVE_TO_FILE_KEY] ?? "false", out bool result1))
                SAVE_TO_FILE = result1;

            if(bool.TryParse(appSetings[SAVE_TO_DB_KEY] ?? "false", out bool result2))
                SAVE_TO_DB = result2;

            if (bool.TryParse(appSetings[GET_DATA_FROM_XML_KEY] ?? "false", out bool result3)) {
                GET_DATA_FROM_XML = result3;
            }
            */
            Database db = new Database();
            Dictionary<string, string> keys = db.GetSystemSettings();

            FILE_FOLDER = keys.ContainsKey(FILE_FOLDER_KEY) ? keys[FILE_FOLDER_KEY] : @"c:\temp";
            FILE_NAME_PREFIX = keys.ContainsKey(FILE_NAME_PREFIX_KEY) ? keys[FILE_NAME_PREFIX_KEY] : @"tag-";
            FILE_NAME = keys.ContainsKey(FILE_NAME_KEY) ? keys[FILE_NAME_KEY] : @"yyy-MM-dd";
            FILE_NAME_EXT = keys.ContainsKey(FILE_NAME_EXT_KEY) ? keys[FILE_NAME_EXT_KEY] : "csv";
            COPY_FOLDER = keys.ContainsKey(COPY_FOLDER_KEY) ? keys[COPY_FOLDER_KEY] : @"c:\temp";
            COPY_FILE_PREFIX = keys.ContainsKey(COPY_FILE_PREFIX_KEY) ?  keys[COPY_FILE_PREFIX_KEY] : @"RejectDetails-tag-";
            COPY_FILE_EXT = keys.ContainsKey(COPY_FILE_EXT_KEY) ? keys[COPY_FILE_EXT_KEY] : @"csv";
            VISIT_INTERVAL = keys.ContainsKey(VISIT_INTERVAL_KEY) ? int.Parse(keys[VISIT_INTERVAL_KEY]) : 500;
            COPY_INTERVAL = keys.ContainsKey(COPY_INTERVAL_KEY) ? int.Parse(keys[COPY_INTERVAL_KEY]) :31000;
            LOG_FILE = keys[LOG_FILE_KEY] ?? @"c:\temp\log";

            if(keys.ContainsKey(SAVE_TO_FILE_KEY) && bool.TryParse(keys[SAVE_TO_FILE_KEY], out bool result1))
                SAVE_TO_FILE = result1;

            if(keys.ContainsKey(SAVE_TO_DB_KEY) && bool.TryParse(keys[SAVE_TO_DB_KEY], out bool result2))
                SAVE_TO_DB = result2;
        }

        public static void setKey(string appKey, string appValue) {
            //var appSetings = ConfigurationManager.AppSettings;
            //appSetings[appKey] = appValue;
            Database db = new Database();
            db.SetSystemSetting(appKey, appValue);
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

        public static string getCopyFileName() {
            return Path.Combine(COPY_FOLDER, COPY_FILE_PREFIX + getFileNameDateString() + "." + COPY_FILE_EXT);
        }

        public static string getCurrentDateTime() {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string getLogName() {
            if(String.IsNullOrWhiteSpace(Path.GetExtension(LOG_FILE))) {
                return LOG_FILE + getFileNameDateString() + ".txt";
            } else {
                return LOG_FILE.Substring(0, LOG_FILE.Length - 4 ) + getFileNameDateString() + LOG_FILE.Substring( LOG_FILE.Length - 4 );
            }
        }
    }
}
