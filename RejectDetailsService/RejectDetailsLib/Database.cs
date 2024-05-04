using LibplctagWrapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Dynamic;

namespace RejectDetailsLib
{
    public class Database : DataSource
    {

        #region Tag Content 
        public static void SetContent(string psContent, string psTagName, string ipaddress, string serialNumber)
        {
            using (SqlConnection conn = new SqlConnection(SystemKeys.DB_CONNECT))
            {
                using (SqlCommand com = conn.CreateCommand())
                {
                    conn.Open();
                    com.CommandText = $@"INSERT INTO dbo.tblTagContent (tag_cont, tag_name, controller_ip, serial_number) VALUES ('{psContent}', '{psTagName}','{ipaddress}', '{serialNumber}')";
                    com.ExecuteNonQuery();
                }
            }
        }

        public void SetContent(List<(string, string)> tagValue, string ipaddress, string serialNumber)
        {
            using (SqlConnection conn = new SqlConnection(SystemKeys.DB_CONNECT))
            {
                using (SqlCommand com = conn.CreateCommand())
                {
                    conn.Open();
                    string sScript = $@"INSERT INTO dbo.tblTagContent (tag_cont, tag_name, controller_ip, serial_number) VALUES";
                    foreach ((string, string) tv in tagValue)
                    {
                        sScript += $"('{tv.Item1}', '{tv.Item2}','{ipaddress}', '{serialNumber}'),";
                    }
                    com.CommandText = sScript.Substring(0, sScript.Length - 1);
                    com.ExecuteNonQuery();
                }
            }
        }

        public void SetStatisticsContent( List<(string, string)> tagValue, string ipAddress )
        {
            using (SqlConnection conn = new SqlConnection(SystemKeys.DB_CONNECT))
            {
                using (SqlCommand com = conn.CreateCommand())
                {
                    conn.Open();
                    string sScript = $@"INSERT INTO dbo.tblStatisticsContent (tag_cont, tag_name, controller_ip) VALUES";
                    foreach ((string, string) tv in tagValue)
                    {
                        sScript += $"('{tv.Item1}', '{tv.Item2}','{ipAddress}'),";
                    }
                    com.CommandText = sScript.Substring(0, sScript.Length - 1);
                    com.ExecuteNonQuery();
                }
            }
        }

        public DataSet GetContent(string startTime, string endTime, string ipAddress, string tagName, string tagValue, string serialNo)
        {
            using (SqlConnection conn = new SqlConnection(SystemKeys.DB_CONNECT))
            {
                using (SqlCommand com = conn.CreateCommand())
                {
                    conn.Open();
                    //                    string sqlString = $@"SELECT Serial_number as SerialNumber, tag_add_dt AS tagTime, tag_name AS tagName, tag_cont AS tagValue, isnull( co.description, tc.controller_ip) AS IPAddress  
                    //FROM tblTagContent tc WITH(NOLOCK)
                    //LEFT JOIN tblController co WITH(NOLOCK) on tc.controller_ip = co.ip_address";
                    //                    sqlString += $@" WHERE tag_add_dt between '{startTime}' AND '{endTime}'";
                    //                    if (ipAddress != "All")
                    //                    {
                    //                        sqlString += $@" AND controller_ip = '{ipAddress}'";
                    //                    }
                    //                    if (!string.IsNullOrWhiteSpace(tagName))
                    //                    {
                    //                        sqlString += $@" AND tag_name LIKE '%{tagName}%'";
                    //                    }
                    //                    if (!string.IsNullOrWhiteSpace(tagValue))
                    //                    {
                    //                        sqlString += $@" AND tag_cont LIKE '%{tagValue}%'";
                    //                    }
                    //                    if (!string.IsNullOrWhiteSpace(serialNo))
                    //                    {
                    //                        sqlString += $@" AND serial_number LIKE '%{serialNo}%'";
                    //                    }
                    //                    com.CommandText = sqlString;

                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandText = "dbo.spGetTagQuery";

                    com.Parameters.Add(new SqlParameter("@pStartTime", startTime));
                    com.Parameters.Add(new SqlParameter("@pEndTime", endTime));
                    if (ipAddress != clsController.ALL_STRING_CONTROLLER)
                        com.Parameters.Add(new SqlParameter("@pipAddress", ipAddress));
                    if (!string.IsNullOrWhiteSpace(tagName)) 
                        com.Parameters.Add(new SqlParameter("@ptagName", tagName));
                    if (!string.IsNullOrWhiteSpace (tagValue))
                        com.Parameters.Add(new SqlParameter("@ptagValue", tagValue));
                    if (!string.IsNullOrWhiteSpace(serialNo)) 
                        com.Parameters.Add(new SqlParameter("@pserialNumber", serialNo));

                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = com;

                    DataSet queryDS = new DataSet();
                    adapter.Fill(queryDS, "query");

                    return queryDS;
                }
            }
        }
        #endregion

        #region Controller
        /*  ----  controller/ip adress -------------- */
        public List<clsController> GetControllerList(bool OnlyEnabled)
        {
            List<clsController> listCont = new List<clsController>();

            using (SqlConnection conn = new SqlConnection(SystemKeys.DB_CONNECT))
            {
                using (SqlCommand com = conn.CreateCommand())
                {
                    conn.Open();
                    string sqlString = $@"SELECT id, ip_address, description, cpuTypeId, isEnabled FROM tblController WITH(NOLOCK) WHERE isStatistics = 0";
                    if (OnlyEnabled)
                    {
                        sqlString += " AND isEnabled = 1";
                    }
                    sqlString += $" ORDER BY description";

                    com.CommandText = sqlString;
                    SqlDataReader dr = com.ExecuteReader();

                    while (dr.Read())
                    {
                        clsController clsCon = new clsController()
                        {
                            Id = dr.GetInt32(0),
                            IpAddress = dr.GetString(1),
                            Description = dr.GetString(2),
                            CpuTypeId = dr.GetInt32(3),
                            IsEnabled = dr.GetBoolean(4),
                        };
                        listCont.Add(clsCon);
                    }
                    dr.Close();
                }
            }

            return listCont;
        }

        public List<clsController> GetAllControllerList()
        {
            List<clsController> listCont = new List<clsController>();

            using (SqlConnection conn = new SqlConnection(SystemKeys.DB_CONNECT))
            {
                using (SqlCommand com = conn.CreateCommand())
                {
                    conn.Open();
                    string sqlString = $@"SELECT id, ip_address, description, cpuTypeId, isEnabled, isStatistics FROM tblController WITH(NOLOCK) ORDER BY description";

                    com.CommandText = sqlString;
                    SqlDataReader dr = com.ExecuteReader();

                    while (dr.Read())
                    {
                        clsController clsCon = new clsController()
                        {
                            Id = dr.GetInt32(0),
                            IpAddress = dr.GetString(1),
                            Description = dr.GetString(2),
                            CpuTypeId = dr.GetInt32(3),
                            IsEnabled = dr.GetBoolean(4),
                            IsStatistics = dr.GetBoolean(5)
                        };
                        listCont.Add(clsCon);
                    }
                    dr.Close();
                }
            }

            return listCont;
        }

        //public List<clsController> GetStatisticsControllerList(bool OnlyEnabled)
        //{
        //    List<clsController> listCont = new List<clsController>();

        //    using (SqlConnection conn = new SqlConnection(SystemKeys.DB_CONNECT))
        //    {
        //        using (SqlCommand com = conn.CreateCommand())
        //        {
        //            conn.Open();
        //            string sqlString = $@"SELECT id, ip_address, description, cpuTypeId, isEnabled, isStatistics FROM tblController WITH(NOLOCK) WHERE isStatistics = 1";
        //            if (OnlyEnabled)
        //            {
        //                sqlString += " AND isEnabled = 1";
        //            }
        //            sqlString += $" ORDER BY description";

        //            com.CommandText = sqlString;
        //            SqlDataReader dr = com.ExecuteReader();

        //            while (dr.Read())
        //            {
        //                clsController clsCon = new clsController()
        //                {
        //                    Id = dr.GetInt32(0),
        //                    IpAddress = dr.GetString(1),
        //                    Description = dr.GetString(2),
        //                    CpuTypeId = dr.GetInt32(3),
        //                    IsEnabled = dr.GetBoolean(4),
        //                    IsStatistics = dr.GetBoolean(5)
        //                };
        //                listCont.Add(clsCon);
        //            }
        //            dr.Close();
        //        }
        //    }

        //    return listCont;
        //}

        public DataSet GetControllerDataSet()
        {
            using (SqlConnection conn = new SqlConnection(SystemKeys.DB_CONNECT))
            {
                using (SqlCommand com = conn.CreateCommand())
                {
                    conn.Open();
                    com.CommandText = $@"SELECT id, ip_address, description, cpuTypeId, isEnabled, isStatistics FROM tblController WITH(NOLOCK)";

                    SqlDataAdapter adapter = new SqlDataAdapter(com);
                    DataSet controller = new DataSet();
                    adapter.Fill(controller, "controller");

                    return controller;
                }
            }
        }

        public void DeleteIPAddress(int id)
        {
            using (SqlConnection conn = new SqlConnection(SystemKeys.DB_CONNECT))
            {
                using (SqlCommand com = conn.CreateCommand())
                {
                    conn.Open();
                    com.CommandText = $@"
BEGIN TRANSACTION
    DELETE FROM tblFullTag WHERE controllerId = {id}
    DELETE FROM tblController WHERE ID = {id} 
COMMIT TRANSACTION;
";
                    com.ExecuteNonQuery();
                }
            }
        }

        public void SetIPAddress(string ipAddress, string description, int cupTypeId, int isEnabled)
        {
            using (SqlConnection conn = new SqlConnection(SystemKeys.DB_CONNECT))
            {
                using (SqlCommand com = conn.CreateCommand())
                {
                    conn.Open();
                    com.CommandText = $@"
IF EXISTS( SELECT 1 FROM tblController WITH(NOLOCK) WHERE ip_address = '{ipAddress}' ) 
    UPDATE tblController SET description = '{description}', isEnabled = {isEnabled} WHERE ip_address = '{ipAddress}' 
ELSE 
    INSERT INTO tblController (ip_address, description, cpuTypeId, isEnabled) VALUES ( '{ipAddress}', '{description}',{cupTypeId}, {isEnabled} );
";
                    com.ExecuteNonQuery();
                }
            }
        }

        #endregion

        #region Tag Info
        public List<clsTag> GetTagGroup(string prefix, int ControllerID)
        {
            List<clsTag> listTags = new List<clsTag>();
            using (SqlConnection conn = new SqlConnection(SystemKeys.DB_CONNECT))
            {
                using (SqlCommand com = conn.CreateCommand())
                {
                    conn.Open();
                    string strSql = $@"
SELECT ft.tagId, ft.tagName, tt.typeName, ft.tagRead, ft.tagDescription, ISNULL(ft.tagWrite, 0 ) AS tagWrite, CASE WHEN op.id IS NULL THEN 0 ELSE 1 END AS tagOutput, tagTitle
FROM dbo.tblFullTag ft WITH(NOLOCK) 
JOIN dbo.tblTagType tt WITH(NOLOCK) ON ft.tagType = tt.typeId
LEFT JOIN dbo.tblOutput op WITH(NOLOCK) ON ft.tagId = op.tagId
WHERE (ft.tagRead IS NULL OR ft.tagRead != 1)
AND controllerId = {ControllerID}
";

                    if (string.IsNullOrWhiteSpace(prefix))
                    {
                        strSql += $@"AND CHARINDEX('.', ft.tagName) = 0";

                    }
                    else
                    {
                        strSql += $@"AND LEFT(ft.tagName, {prefix.Length}) = '{prefix}'";
                    }

                    com.CommandText = strSql;

                    SqlDataReader dr = com.ExecuteReader();

                    while (dr.Read())
                    {
                        clsTag tag = new clsTag()
                        {
                            TagId = dr.GetInt32(0),
                            TagName = dr.GetString(1),
                            TagType = dr.GetString(2),
                            Read = dr.IsDBNull(3) ? 0 : dr.GetInt16(3),
                            Description = dr.IsDBNull(4) ? string.Empty : dr.GetString(4),
                            Write = System.Convert.ToInt32(dr.GetValue(5)),
                            Output = System.Convert.ToInt32(dr.GetValue(6)),
                            TagTitle = dr.GetString(7),
                        };
                        listTags.Add(tag);
                    }

                    dr.Close();
                }
            }
            return listTags;
        }

        public List<clsTag> GetTagGroup(int ControllerID)
        {
            List<clsTag> listTags = new List<clsTag>();
            using (SqlConnection conn = new SqlConnection(SystemKeys.DB_CONNECT))
            {
                using (SqlCommand com = conn.CreateCommand())
                {
                    conn.Open();
                    string strSql = $@"
SELECT ft.tagId, ft.tagName, tt.typeName, ft.tagRead, ft.tagDescription, ISNULL(ft.tagWrite, 0 ) AS tagWrite, CASE WHEN op.id IS NULL THEN 0 ELSE 1 END AS tagOutput, tagTitle
FROM dbo.tblFullTag ft WITH(NOLOCK) 
JOIN dbo.tblTagType tt WITH(NOLOCK) ON ft.tagType = tt.typeId
LEFT JOIN dbo.tblOutput op WITH(NOLOCK) ON ft.tagId = op.tagId
WHERE (ft.tagRead IS NULL OR ft.tagRead != 1)
AND controllerId = {ControllerID}
";

                    com.CommandText = strSql;

                    SqlDataReader dr = com.ExecuteReader();

                    while (dr.Read())
                    {
                        clsTag tag = new clsTag()
                        {
                            TagId = dr.GetInt32(0),
                            TagName = dr.GetString(1),
                            TagType = dr.GetString(2),
                            Read = dr.IsDBNull(3) ? 0 : dr.GetInt16(3),
                            Description = dr.IsDBNull(4) ? string.Empty : dr.GetString(4),
                            Write = System.Convert.ToInt32(dr.GetValue(5)),
                            Output = System.Convert.ToInt32(dr.GetValue(6)),
                            TagTitle = dr.GetString(7),
                        };
                        listTags.Add(tag);
                    }

                    dr.Close();
                }
            }
            return listTags;
        }

        public List<string> GetFirstLevelGroup(int ControllerID)
        {
            List<string> listGroup = new List<string>();
            using (SqlConnection conn = new SqlConnection(SystemKeys.DB_CONNECT))
            {
                using (SqlCommand com = conn.CreateCommand())
                {
                    conn.Open();
                    com.CommandText = 
$@"SELECT DISTINCT LEFT( tagName, IIF(position > 0, position -1, LEN( tagName)) ) AS StationName
FROM tblFullTag ft WITH(NOLOCK)
JOIN (SELECT tagId, CHARINDEX( '.', tagName) AS position FROM tblFullTag WITH(NOLOCK)) tmp ON ft.tagid = tmp.tagId 
WHERE controllerId = {ControllerID}
ORDER BY 1 ";

                    SqlDataReader dr = com.ExecuteReader();

                    while (dr.Read())
                    {
                        listGroup.Add(dr.GetString(0));
                    }

                    dr.Close();
                }
            }
            return listGroup;
        }

        public Dictionary<string, string> GetTagOutputNamePair()
        {
            Dictionary<string, string> dicTagOutputPair = new Dictionary<string, string>();
            using (SqlConnection conn = new SqlConnection(SystemKeys.DB_CONNECT))
            {
                using (SqlCommand com = conn.CreateCommand())
                {
                    conn.Open();
                    com.CommandText = "SELECT tagName, ISNULL(tagTitle, tagName) AS tagTitle FROM tblFullTag WITH(NOLOCK)";

                    SqlDataReader dr = com.ExecuteReader();

                    while (dr.Read())
                    {
                        dicTagOutputPair.Add( dr.GetString(1), dr.GetString(0));
                    }

                    dr.Close();
                }
            }
            return dicTagOutputPair;
        }

        public List<clsTag> GetReadTags(int ControllerID)
        {
            List<clsTag> listTags = new List<clsTag>();
            using (SqlConnection conn = new SqlConnection(SystemKeys.DB_CONNECT))
            {
                using (SqlCommand com = conn.CreateCommand())
                {
                    conn.Open();
                    com.CommandText = $@"
SELECT ft.tagId, ft.tagName, tt.typeName, ft.tagRead, ft.tagDescription, ISNULL(ft.tagWrite, 0) AS tagWrite, CASE WHEN op.id IS NULL THEN 0 ELSE 1 END AS tagOutput, tagTitle
FROM dbo.tblFullTag ft WITH(NOLOCK) 
JOIN dbo.tblTagType tt WITH(NOLOCK) ON ft.tagType = tt.typeId
LEFT JOIN dbo.tblOutput op WITH(NOLOCK) ON ft.tagId = op.tagId
WHERE ft.tagRead = 1 AND controllerId = {ControllerID}";

                    SqlDataReader dr = com.ExecuteReader();

                    while (dr.Read())
                    {
                        clsTag tag = new clsTag()
                        {
                            TagId = dr.GetInt32(0),
                            TagName = dr.GetString(1),
                            TagType = dr.GetString(2),
                            Read = dr.GetInt16(3),
                            Description = dr.IsDBNull(4) ? string.Empty : dr.GetString(4),
                            Write = System.Convert.ToInt32(dr.GetValue(5)),
                            Output = System.Convert.ToInt32(dr.GetValue(6)),
                            TagTitle = dr.GetString(7),
                        };
                        listTags.Add(tag);
                    }

                    dr.Close();
                }
            }
            return listTags;
        }

        public DataSet GetFullTags(int ControllerID)
        {
            // read : 1, write : -1;
            using (SqlConnection conn = new SqlConnection(SystemKeys.DB_CONNECT))
            {
                using (SqlCommand com = conn.CreateCommand())
                {
                    conn.Open();
                    com.CommandText = $@"SELECT ft.tagId, ft.tagName, ft.tagType, tt.typeName, 
ISNULL(ft.tagRead, 0) tagRead, ISNULL(ft.tagWrite, 0) AS tagWrite, ft.tagDescription, ISNULL(ft.tagOutput, 0) AS tagOutput,
CASE WHEN ft.tagRead = 1 THEN 'Read' ELSE '' END as tagReadText,
CASE WHEN ft.tagWrite = 1 THEN 'Write' ELSE '' END as tagWriteText,
CASE WHEN ft.tagOutput = 1 THEN 'Output' ELSE '' END as tagOutputText,
tagTitle 
FROM dbo.tblFullTag ft WITH(NOLOCK) JOIN dbo.tblTagType tt WITH(NOLOCK) ON ft.tagType = tt.typeId
WHERE controllerId = {ControllerID}";

                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = com;

                    DataSet tags = new DataSet();
                    adapter.Fill(tags, "tags");

                    return tags;
                }
            }
        }

        public void SetFullTags(int ControllerId, int tagId, string tagName, int tagType, int tagRead, int tagWrite, string description, int tagOutput, string tagTitle)
        {
            using (SqlConnection conn = new SqlConnection(SystemKeys.DB_CONNECT))
            {
                using (SqlCommand com = conn.CreateCommand())
                {
                    conn.Open();
                    com.CommandText = $@"
IF EXISTS( SELECT 1 FROM tblFullTag WHERE controllerId = {ControllerId} AND tagId = {tagId} )
    UPDATE tblFullTag 
    SET tagName = '{tagName}', tagType = {tagType}, tagDescription = '{description}', tagRead = {tagRead}, tagWrite = {tagWrite}, tagOutput = {tagOutput}, tagTitle = '{tagTitle}'
    WHERE controllerId = {ControllerId} AND tagId = {tagId}
ELSE 
    INSERT INTO tblFullTag (controllerId, tagName, tagDescription, tagType, tagRead, tagWrite, tagOutput, tagTitle) VALUES 
    ({ControllerId}, '{tagName}', '{description}', {tagType}, {tagRead}, {tagWrite}, {tagOutput}, '{tagTitle}' )
";
                    com.ExecuteNonQuery();
                }
            }
        }

        public void DeleteFullTag(int tagId)
        {
            using (SqlConnection conn = new SqlConnection(SystemKeys.DB_CONNECT))
            {
                using (SqlCommand com = conn.CreateCommand())
                {
                    conn.Open();
                    com.CommandText = $@"DELETE FROM tblOutput WHERE tagId = {tagId}; DELETE FROM tblFullTag WHERE tagId = {tagId}";
                    com.ExecuteNonQuery();
                }
            }
        }

        public void CleanUpFallTag(int ipAddressID)
        {
            using (SqlConnection conn = new SqlConnection(SystemKeys.DB_CONNECT))
            {
                using (SqlCommand com = conn.CreateCommand())
                {
                    conn.Open();
                    com.CommandText = $@"DELETE FROM tblOutput WHERE tagId IN (SELECT tagID FROM tblFullTag WITH(NOLOCK) WHERE controllerID = {ipAddressID}); DELETE FROM tblFullTag WHERE controllerID = {ipAddressID}";
                    com.ExecuteNonQuery();
                }
            }
        }

        public string UploadFullTag(int ipAddressID, List<string[]> insertList)
        {
            string returnStr = "";
            try
            {
                string insertStr = string.Empty;
                foreach (string[] il in insertList)
                {
                    insertStr += $"({ipAddressID}," + string.Join(",", il) + "),";
                }

                insertStr = insertStr.Substring(0, insertStr.Length - 1);

                using (SqlConnection conn = new SqlConnection(SystemKeys.DB_CONNECT))
                {
                    using (SqlCommand com = conn.CreateCommand())
                    {
                        conn.Open();
                        com.CommandText = $@"INSERT INTO [dbo].[tblFullTag] ([controllerId],[tagName],[tagType],[tagDescription],[tagRead],[tagWrite],[tagTitle]) VALUES {insertStr}";
                        com.ExecuteNonQuery();
                    }
                }
            } catch (Exception e )
            {
                clsLog.addLog(e.Message);
                returnStr = e.Message;
            } 
            return returnStr;
        }
        #endregion

        #region System Keys
        public Dictionary<string, string> GetSystemSettings()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            using (SqlConnection conn = new SqlConnection(SystemKeys.DB_CONNECT))
            {
                using (SqlCommand com = conn.CreateCommand())
                {
                    conn.Open();
                    com.CommandText = $@"SELECT keyName, keyValue FROM tblSystemSettings WITH(NOLOCK)";
                    SqlDataReader dr = com.ExecuteReader();

                    while (dr.Read())
                    {
                        dic.Add(dr.GetString(0), dr.GetString(1));
                    }
                    dr.Close();
                }
            }

            return dic;
        }

        public void SetSystemSetting(string keyName, string keyValue)
        {
            using (SqlConnection conn = new SqlConnection(SystemKeys.DB_CONNECT))
            {
                using (SqlCommand com = conn.CreateCommand())
                {
                    conn.Open();
                    com.CommandText = $@"
IF EXISTS( SELECT 1 FROM tblSystemSettings WITH(NOLOCK) WHERE keyName = '{keyName}' ) 
    UPDATE tblSystemSettings SET keyValue = '{keyValue}' WHERE keyName = '{keyName}' 
ELSE 
    INSERT INTO tblSystemSettings (keyName, keyValue ) VALUES ( '{keyName}', '{keyValue}' );
";
                    com.ExecuteNonQuery();
                }
            }
        }
        #endregion

        public DataSet GetTagTypeDataSet()
        {
            using (SqlConnection conn = new SqlConnection(SystemKeys.DB_CONNECT))
            {
                using (SqlCommand com = conn.CreateCommand())
                {
                    conn.Open();
                    com.CommandText = $@"SELECT typeid, typeName FROM tblTagType WITH(NOLOCK)";

                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = com;

                    DataSet controller = new DataSet();
                    adapter.Fill(controller, "TagTypes");

                    return controller;
                }
            }
        }

        public Dictionary<string, int> GetTagTypeDictionary()
        {
            Dictionary<string, int> dic = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            using (SqlConnection conn = new SqlConnection(SystemKeys.DB_CONNECT))
            {
                using (SqlCommand com = conn.CreateCommand())
                {
                    conn.Open();
                    com.CommandText = $@"SELECT typeName, typeId FROM tblTagType WITH(NOLOCK)";

                    SqlDataReader dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        dic.Add(dr.GetString(0), dr.GetInt32(1));
                    }
                    dr.Close();

                }
            }
            return dic;
        }

        #region Tag Output 
        public List<clsTag> GetSelectedOutput(int controllerId)
        {
            using (SqlConnection conn = new SqlConnection(SystemKeys.DB_CONNECT))
            {
                using (SqlCommand com = conn.CreateCommand())
                {
                    conn.Open();
                    com.CommandText = $@"SELECT tag.tagId, tag.tagName 
FROM tblOutput op WITH(NOLOCK)
JOIN tblFullTag tag WITH(NOLOCK) ON op.tagId = tag.tagId AND controllerId = {controllerId}
ORDER BY byOrder ";

                    SqlDataReader dr = com.ExecuteReader();
                    List<clsTag> list = new List<clsTag>();
                    while (dr.Read())
                    {
                        list.Add(new clsTag(dr.GetInt32(0), dr.GetString(1)));
                    }
                    dr.Close();

                    return list;
                }
            }
        }

        public List<int> GetSelectedTagIdOutput(int controllerId)
        {
            return this.GetSelectedTagIdHelper($@"SELECT tag.tagId
FROM tblOutput op WITH(NOLOCK)
JOIN tblFullTag tag WITH(NOLOCK) ON op.tagId = tag.tagId AND controllerId = {controllerId}
ORDER BY byOrder ");
        }

        public List<int> GetSelectedTagIdOutputOriginal(int controllerId)
        {
            return this.GetSelectedTagIdHelper($@"SELECT tagId FROM tblFullTag WITH(NOLOCK) WHERE tagOutput = 1 AND controllerId = {controllerId} ORDER BY tagId; ");
        }


        private List<int> GetSelectedTagIdHelper(string sqlScript)
        {
            using (SqlConnection conn = new SqlConnection(SystemKeys.DB_CONNECT))
            {
                using (SqlCommand com = conn.CreateCommand())
                {
                    conn.Open();
                    com.CommandText = sqlScript;

                    SqlDataReader dr = com.ExecuteReader();
                    List<int> list = new List<int>();
                    while (dr.Read())
                    {
                        list.Add(dr.GetInt32(0));
                    }
                    dr.Close();

                    return list;
                }
            }
        }

        public void SetSelectedOutput(int controllerId, string outputIdList)
        {
            using (SqlConnection conn = new SqlConnection(SystemKeys.DB_CONNECT))
            {
                using (SqlCommand com = conn.CreateCommand())
                {
                    conn.Open();
                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandText = "dbo.spSetSelectedOutputs";

                    com.Parameters.Add(new SqlParameter("@controllerId", controllerId));
                    com.Parameters.Add(new SqlParameter("@tagIdList", outputIdList));

                    com.ExecuteNonQuery();
                }
            }
        }

        public List<clsTag> GetUnselectedTags(int controllerId)
        {
            using (SqlConnection conn = new SqlConnection(SystemKeys.DB_CONNECT))
            {
                using (SqlCommand com = conn.CreateCommand())
                {
                    conn.Open();
                    com.CommandText = $@"SELECT tag.tagId, tag.tagName 
FROM tblFullTag tag WITH(NOLOCK) 
LEFT JOIN tblOutput op WITH(NOLOCK) ON tag.tagId = op.tagId
WHERE controllerId = {controllerId}
AND op.tagId IS NULL
ORDER BY tag.tagName;";

                    SqlDataReader dr = com.ExecuteReader();
                    List<clsTag> list = new List<clsTag>();

                    while (dr.Read())
                    {
                        list.Add(new clsTag(dr.GetInt32(0), dr.GetString(1)));
                    }
                    dr.Close();

                    return list;
                }
            }
        }

        #endregion
    }
}
