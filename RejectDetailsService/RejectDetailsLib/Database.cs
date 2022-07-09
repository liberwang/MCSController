using System.Collections.Generic;
using System.Data.SqlClient;

namespace RejectDetailsLib {
    public class Database {
        public static void SetContent(string psContent, int stationTagId, int serialNumber) {
            
            using(SqlConnection conn = new SqlConnection(SystemKeys.DB_CONNECT)) {
                using(SqlCommand com = conn.CreateCommand()) {
                    conn.Open();
                    com.CommandText = $@"INSERT INTO dbo.tblTagContent (tag_cont, stationtag_id, serial_number) VALUES ('{psContent}', {stationTagId}, {serialNumber})";
                    com.ExecuteNonQuery();
                }
            }
        }

        public static List<clsController> GetController() {
            List<clsController> listCont = new List<clsController>();

            using(SqlConnection conn = new SqlConnection(SystemKeys.DB_CONNECT)) {
                using(SqlCommand com = conn.CreateCommand()) {
                    conn.Open();
                    com.CommandText = $@"SELECT id, ip_address, description, cpuTypeId FROM tblController";
                    SqlDataReader dr = com.ExecuteReader();
                    
                    while( dr.Read() ) {
                        clsController clsCon = new clsController() {
                            Id = dr.GetInt32(0),
                            IpAddress = dr.GetString(1),
                            Description = dr.GetString(2),
                            CpuTypeId = dr.GetInt32(3)
                        };
                        listCont.Add(clsCon);
                    }
                    dr.Close();
                }
            }

            return listCont;
        }

        public static List<clsStation> GetStations(int ControllerID) {
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

        public static List<clsTag> GetTagInformation(int StationID, string StationName) {
            List<clsTag> listTags = new List<clsTag>();

            using(SqlConnection conn = new SqlConnection(SystemKeys.DB_CONNECT)) {
                using(SqlCommand com = conn.CreateCommand()) {
                    conn.Open();
                    com.CommandText = $@"
SELECT a.id, a.tagName, a.tagType, a.Comment, 
case when rw1.id is not null then 1 when rw2.id is not null then -1 else 0 end AS ReadWrite,
b.Id as StationTagId
FROM tbltags a
join tblStationTag b on a.id = b.TagId 
left join tblTagReadWrite rw1 on a.id = rw1.readTagId
left join tblTagReadWrite rw2 on a.id = rw2.writeTagId
where b.StationId = {StationID} ";
                    SqlDataReader dr = com.ExecuteReader();

                    while(dr.Read()) {
                        clsTag tag = new clsTag() {
                            StationId = StationID,
                            StationName = StationName,
                            TagId = dr.GetInt32(0),
                            TagName = dr.GetString(1),
                            TagType = dr.IsDBNull(2) ? string.Empty : dr.GetString(2),
                            Comment = dr.IsDBNull(3) ? string.Empty : dr.GetString(3),
                            ReadWrite = dr.IsDBNull(4) ? 0 : dr.GetInt32(4),
                            StationTagId = dr.GetInt32(5),
                        };
                        listTags.Add(tag);
                    }
                    dr.Close();
                }
            }
            return listTags;
        }

        public static Dictionary<string, string> GetReadWriteTag() {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            using(SqlConnection conn = new SqlConnection(SystemKeys.DB_CONNECT)) {
                using(SqlCommand com = conn.CreateCommand()) {
                    conn.Open();
                    com.CommandText = $@"select tg1.tagName As ReadTagName, tg2.tagName As WriteTagName from tblTagReadWrite rw 
  join tbltags tg1 on rw.readTagId = tg1.id 
  join tblTags tg2 on rw.writeTagId = tg2.id ";
                    SqlDataReader dr = com.ExecuteReader();

                    while(dr.Read()) {
                        dic.Add(dr.GetString(0), dr.GetString(1));
                    }
                    dr.Close();
                }
            }
            return dic;
        }

//        public static List<clsTag> GetTagInformation(int stationId ) {
//            List<clsTag> listTags= new List<clsTag>();

//            using(SqlConnection conn = new SqlConnection(SystemKeys.DB_CONNECT)) {
//                using(SqlCommand com = conn.CreateCommand()) {
//                    conn.Open();
//                    com.CommandText = $@"
//SELECT statag.StationId, sta.Name as StationName, statag.TagId, tags.tagName, sub.subName, pro.processTag, pro.tagType, pro.comment, pro.ReadWrite
//FROM tblStationTag statag 
//JOIN tblStation sta ON statag.StationId = sta.id AND sta.Id = {stationId} 
//JOIN tblTags tags ON statag.TagId = tags.Id 
//JOIN tblTagSub sub ON sub.tagId = statag.TagId
//JOIN tblTagProcess pro ON pro.tagSubId = sub.id 
//ORDER BY 3, 4, 5, 6";
//                    SqlDataReader dr = com.ExecuteReader();

//                    while(dr.Read()) {
//                        clsTag tag = new clsTag() {
//                            StationId = dr.GetInt32(0),
//                            StationName = dr.GetString(1),
//                            TagId = dr.GetInt32(2),
//                            TagName = dr.GetString(3),
//                            SubName = dr.GetString(4),
//                            ProcessTag = dr.GetString(5),
//                            TagType = dr.GetString(6),
//                            Comment = dr.GetString(7),
//                            ReadWrite = dr.GetInt16(8),
//                        };
//                        listTags.Add(tag);
//                    }
//                    dr.Close();
//                }
//            }
//            return listTags;
//        }
    }
}
