using LibplctagWrapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace RejectDetailsLib {
    public class Database : DataSource {
        public static void SetContent(string psContent, string psTagName, string ipaddress, string serialNumber) {
            using(SqlConnection conn = new SqlConnection(SystemKeys.DB_CONNECT)) {
                using(SqlCommand com = conn.CreateCommand()) {
                    conn.Open();
                    com.CommandText = $@"INSERT INTO dbo.tblTagContent (tag_cont, tag_name, controller_ip, serial_number) VALUES ('{psContent}', '{psTagName}','{ipaddress}', '{serialNumber}')";
                    com.ExecuteNonQuery();
                }
            }
        }

        public static void SetContent(List<(string, string)>tagValue, string ipaddress, string serialNumber) {
            using(SqlConnection conn = new SqlConnection(SystemKeys.DB_CONNECT)) {
                using(SqlCommand com = conn.CreateCommand()) {
                    conn.Open();
                    string sScript = $@"INSERT INTO dbo.tblTagContent (tag_cont, tag_name, controller_ip, serial_number) VALUES";
                    foreach((string, string) tv in tagValue) {
                        sScript += $"('{tv.Item1}', '{tv.Item2}','{ipaddress}', '{serialNumber}'),";
                    }
                    com.CommandText = sScript.Substring( 0, sScript.Length - 1 ) ;
                    com.ExecuteNonQuery();
                }
            }
        }

        public DataSet GetContent(string startTime, string endTime, string ipAddress, string tagName, string tagValue, string serialNo) {
            using(SqlConnection conn = new SqlConnection(SystemKeys.DB_CONNECT)) {
                using(SqlCommand com = conn.CreateCommand()) {
                    conn.Open();
                    string sqlString = $@"SELECT Serial_number as SerialNumber, tag_add_dt AS tagTime, tag_name AS tagName, tag_cont AS tagValue, isnull( co.description, tc.controller_ip) AS IPAddress  
FROM tblTagContent tc WITH(NOLOCK)
LEFT JOIN tblController co WITH(NOLOCK) on tc.controller_ip = co.ip_address";
                    sqlString += $@" WHERE tag_add_dt between '{startTime}' AND '{endTime}'";
                    if(ipAddress != "All") {
                        sqlString += $@" AND controller_ip = '{ipAddress}'";
                    }
                    if (!string.IsNullOrWhiteSpace(tagName)) {
                        sqlString += $@" AND tag_name LIKE '%{tagName}%'";
                    }
                    if (!string.IsNullOrWhiteSpace(tagValue)) {
                        sqlString += $@" AND tag_cont LIKE '%{tagValue}%'";
                    }
                    if (!string.IsNullOrWhiteSpace(serialNo)) {
                        sqlString += $@" AND serial_number LIKE '%{serialNo}%'";
                    }
                    com.CommandText = sqlString;

                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = com;

                    DataSet controller = new DataSet();
                    adapter.Fill(controller, "Customers");

                    return controller;
                }
            }
        }

        public List<clsController> GetController() {
            List<clsController> listCont = new List<clsController>();

            using(SqlConnection conn = new SqlConnection(SystemKeys.DB_CONNECT)) {
                using(SqlCommand com = conn.CreateCommand()) {
                    conn.Open();
                    com.CommandText = $@"SELECT id, ip_address, description, cpuTypeId, isEnabled FROM tblController WITH(NOLOCK) WHERE isEnabled = 1";
                    SqlDataReader dr = com.ExecuteReader();
                    
                    while( dr.Read() ) {
                        clsController clsCon = new clsController() {
                            Id = dr.GetInt32(0),
                            IpAddress = dr.GetString(1),
                            Description = dr.GetString(2),
                            CpuTypeId = dr.GetInt32(3),
                            isEnabled = dr.GetBoolean(4),
                        };
                        listCont.Add(clsCon);
                    }
                    dr.Close();
                }
            }

            return listCont;
        }

        public List<clsStation> GetStations(int ControllerID) {
            List<clsStation> listStation = new List<clsStation>();

            using(SqlConnection conn = new SqlConnection(SystemKeys.DB_CONNECT)) {
                using(SqlCommand com = conn.CreateCommand()) {
                    conn.Open();
                    com.CommandText = $@"SELECT id, name, controllerId FROM tblStation WHERE controllerId = {ControllerID}";
                    SqlDataReader dr = com.ExecuteReader();

                    while( dr.Read()) {
                        clsStation clsSta = new clsStation() {
                            Id = dr.GetInt32(0),
                            Name = dr.GetString(1),
                            ControllerId = dr.GetInt32(2),
                        };

                        listStation.Add(clsSta);
                    }
                    dr.Close();
                }
            }
            return listStation;
        }

        public List<clsTag> GetTagGroup( string prefix, int ControllerID) {
            List<clsTag> listTags = new List<clsTag>();
            using(SqlConnection conn = new SqlConnection(SystemKeys.DB_CONNECT)) {
                using(SqlCommand com = conn.CreateCommand()) {
                    conn.Open();
                    string strSql = $@"
SELECT ft.tagId, ft.tagName, tt.typeName, ft.tagRead, ft.tagDescription, ISNULL(ft.tagWrite, 0 ) AS tagWrite, ISNULL(ft.tagOutput, 0) AS tagOutput
FROM dbo.tblFullTag ft WITH(NOLOCK) 
JOIN dbo.tblTagType tt WITH(NOLOCK) ON ft.tagType = tt.typeId
WHERE (ft.tagRead IS NULL OR ft.tagRead != 1)
AND controllerId = {ControllerID}
";

                    if (string.IsNullOrWhiteSpace(prefix)) {
                        strSql += $@"AND CHARINDEX('.', ft.tagName) = 0";

                    } else {
                        strSql += $@"AND LEFT(ft.tagName, {prefix.Length}) = '{prefix}'";
                    }

                    com.CommandText = strSql;

                    SqlDataReader dr = com.ExecuteReader();

                    while(dr.Read()) {
                        clsTag tag = new clsTag() {
                            TagId = dr.GetInt32(0),
                            TagName = dr.GetString(1),
                            TagType = dr.GetString(2),
                            Read = dr.IsDBNull(3) ? 0 : dr.GetInt16(3),
                            Comment = dr.IsDBNull(4) ? string.Empty : dr.GetString(4),
                            Write = dr.GetInt16(5),
                            Output = dr.GetInt16(6)
                        };
                        listTags.Add(tag);
                    }

                    dr.Close();
                }
            }
            return listTags;
        }

        public List<clsTag> GetReadTags(int ControllerID) {
            List<clsTag> listTags = new List<clsTag>();
            using(SqlConnection conn = new SqlConnection(SystemKeys.DB_CONNECT)) {
                using(SqlCommand com = conn.CreateCommand()) {
                    conn.Open();
                    com.CommandText = $@"
SELECT ft.tagId, ft.tagName, tt.typeName, ft.tagRead, ft.tagDescription, ISNULL(ft.tagWrite, 0) AS tagWrite, ISNULL(ft.tagOutput, 0) AS tagOutput
FROM dbo.tblFullTag ft WITH(NOLOCK) 
JOIN dbo.tblTagType tt WITH(NOLOCK) ON ft.tagType = tt.typeId
WHERE ft.tagRead = 1 AND controllerId = {ControllerID}";

                    SqlDataReader dr = com.ExecuteReader();

                    while(dr.Read()) {
                        clsTag tag = new clsTag() {
                            TagId = dr.GetInt32(0),
                            TagName = dr.GetString(1),
                            TagType = dr.GetString(2),
                            Read = dr.GetInt16(3),
                            Comment = dr.IsDBNull(4) ? string.Empty : dr.GetString(4),
                            Write = dr.GetInt16(5),
                            Output = dr.GetInt16(6),
                        };
                        listTags.Add(tag);
                    }

                    dr.Close();
                }
            }
            return listTags;
        }

        public Dictionary<string, string> GetSystemSettings() {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            using(SqlConnection conn = new SqlConnection(SystemKeys.DB_CONNECT)) {
                using(SqlCommand com = conn.CreateCommand()) {
                    conn.Open();
                    com.CommandText = $@"SELECT keyName, keyValue FROM tblSystemSettings WITH(NOLOCK)";
                    SqlDataReader dr = com.ExecuteReader();

                    while(dr.Read()) {
                        dic.Add(dr.GetString(0), dr.GetString(1));
                    }
                    dr.Close();
                }
            }

            return dic;
        }

        public void SetSystemSetting( string keyName, string keyValue) {
            using(SqlConnection conn = new SqlConnection(SystemKeys.DB_CONNECT)) {
                using(SqlCommand com = conn.CreateCommand()) {
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


        public DataSet GetIPAddressDataSet(bool enabledOnly = false) {
            using(SqlConnection conn = new SqlConnection(SystemKeys.DB_CONNECT)) {
                using(SqlCommand com = conn.CreateCommand()) {
                    conn.Open();
                    string sqlString = $@"SELECT id, ip_address, description, isEnabled FROM tblController WITH(NOLOCK)";
                    if (enabledOnly) {
                        sqlString += " WHERE isEnabled = 1";
                    }
                    com.CommandText = sqlString ;

                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = com;

                    DataSet controller = new DataSet();
                    adapter.Fill(controller, "Customers");

                    return controller;
                }
            }
        }

        public void SetIPAddress( string ipAddress, string description, int isEnabled ) {
            using(SqlConnection conn = new SqlConnection(SystemKeys.DB_CONNECT)) {
                using(SqlCommand com = conn.CreateCommand()) {
                    conn.Open();
                    com.CommandText = $@"
IF EXISTS( SELECT 1 FROM tblController WITH(NOLOCK) WHERE ip_address = '{ipAddress}' ) 
    UPDATE tblController SET description = '{description}', isEnabled = {isEnabled} WHERE ip_address = '{ipAddress}' 
ELSE 
    INSERT INTO tblController (ip_address, description, isEnabled) VALUES ( '{ipAddress}', '{description}', {isEnabled} );
";
                    com.ExecuteNonQuery();
                }
            }
        }

        public void DeleteIPAddress( int id ) {
            using(SqlConnection conn = new SqlConnection(SystemKeys.DB_CONNECT)) {
                using(SqlCommand com = conn.CreateCommand()) {
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

        public DataSet GetTagTypeDataSet() {
            using(SqlConnection conn = new SqlConnection(SystemKeys.DB_CONNECT)) {
                using(SqlCommand com = conn.CreateCommand()) {
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

        public DataSet GetFullTags(int ControllerID) {
            // read : 1, write : -1;
            using(SqlConnection conn = new SqlConnection(SystemKeys.DB_CONNECT)) {
                using(SqlCommand com = conn.CreateCommand()) {
                    conn.Open();
                    com.CommandText = $@"SELECT ft.tagId, ft.tagName, ft.tagType, tt.typeName, 
ISNULL(ft.tagRead, 0) tagRead, ISNULL(ft.tagWrite, 0) AS tagWrite, ft.tagDescription, ISNULL(ft.tagOutput, 0) AS tagOutput,
CASE WHEN ft.tagRead = 1 THEN 'Read' ELSE '' END as tagReadText,
CASE WHEN ft.tagWrite = 1 THEN 'Write' ELSE '' END as tagWriteText,
CASE WHEN ft.tagOutput = 1 THEN 'Output' ELSE '' END as tagOutputText
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

        public void SetFullTags(int ControllerId, string tagName, int tagType, int tagRead, int tagWrite,string description, int tagOutput) {
            using(SqlConnection conn = new SqlConnection(SystemKeys.DB_CONNECT)) {
                using(SqlCommand com = conn.CreateCommand()) {
                    conn.Open();
                    com.CommandText = $@"
IF EXISTS( SELECT 1 FROM tblFullTag WHERE controllerId = {ControllerId} AND tagName = '{tagName}' )
    UPDATE tblFullTag 
    SET tagType = {tagType}, tagDescription = '{description}', tagRead = {tagRead}, tagWrite = {tagWrite}, tagOutput = {tagOutput}
    WHERE controllerId = {ControllerId} AND tagName = '{tagName}'
ELSE 
    INSERT INTO tblFullTag (controllerId, tagName, tagDescription, tagType, tagRead, tagWrite, tagOutput) VALUES 
    ({ControllerId}, '{tagName}', '{description}', {tagType}, {tagRead}, {tagWrite},{tagOutput} )
";
                    com.ExecuteNonQuery();
                }
            }
        }

        public void DeleteFullTag( int tagId ) {
            using(SqlConnection conn = new SqlConnection(SystemKeys.DB_CONNECT)) {
                using(SqlCommand com = conn.CreateCommand()) {
                    conn.Open();
                    com.CommandText = $@"DELETE FROM tblFullTag WHERE tagId = {tagId}";
                    com.ExecuteNonQuery();
                }
            }
        }

        public DataSet GetSelectedOutput( int controllerId )
        {
            using (SqlConnection conn = new SqlConnection(SystemKeys.DB_CONNECT))
            {
                using (SqlCommand com = conn.CreateCommand())
                {
                    conn.Open();
                    com.CommandText = $@"SELECT tag.tagId, tagName 
FROM tblOutput op WITH(NOLOCK)
JOIN tblFullTag tag WITH(NOLOCK) ON op.tagId = tag.tagId AND controllerId = {controllerId}
ORDER BY byOrder ";

                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = com;

                    DataSet outputs = new DataSet();
                    adapter.Fill(outputs, "outputs");

                    return outputs;
                }
            }
        }

        public void SetSelectedOutput(int controllerId,  string outputIdList)
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

        public DataSet GetUnselectedTags(int controllerId)
        {
            using (SqlConnection conn = new SqlConnection(SystemKeys.DB_CONNECT))
            {
                using (SqlCommand com = conn.CreateCommand())
                {
                    conn.Open();
                    com.CommandText = $@"SELECT tag.tagId, tagName 
FROM tblFullTag tag WITH(NOLOCK) 
LEFT JOIN tblOutput op WITH(NOLOCK) ON tag.tagId = op.tagId
WHERE controllerId = {controllerId}
AND op.tagId IS NULL;";

                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = com;

                    DataSet tag = new DataSet();
                    adapter.Fill(tag, "tag");

                    return tag;
                }
            }
        }
    }
}
