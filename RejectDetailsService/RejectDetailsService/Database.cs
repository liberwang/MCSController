using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RejectDetailsService {
    public class Database {
        public static void SetContent(string psContent, string ipAddress) {
            
            using(SqlConnection conn = new SqlConnection(SystemKeys.DB_CONNECT)) {
                using(SqlCommand com = conn.CreateCommand()) {
                    conn.Open();
                    com.CommandText = $@"INSERT INTO dbo.tblTagContent (tag_cont, controller_ip) VALUES ('{psContent}', '{ipAddress}')";
                    com.ExecuteNonQuery();
                }
            }
        }

        public static List<int> GetStations() {
            List<int> listStation = new List<int>();

            using(SqlConnection conn = new SqlConnection(SystemKeys.DB_CONNECT)) {
                using(SqlCommand com = conn.CreateCommand()) {
                    conn.Open();
                    com.CommandText = $@"SELECT id FROM tblStation ORDER BY name";
                    SqlDataReader dr = com.ExecuteReader();

                    while( dr.Read()) {
                        listStation.Add(dr.GetInt32(0));
                    }
                    dr.Close();
                }
            }
            return listStation;
        }

        public static List<clsTag> GetTagInformation(int stationId ) {
            List<clsTag> listTags= new List<clsTag>();

            using(SqlConnection conn = new SqlConnection(SystemKeys.DB_CONNECT)) {
                using(SqlCommand com = conn.CreateCommand()) {
                    conn.Open();
                    com.CommandText = $@"
SELECT statag.StationId, sta.Name as StationName, statag.TagId, tags.tagName, sub.subName, pro.processTag, pro.tagType, pro.comment, pro.ReadWrite
FROM tblStationTag statag 
JOIN tblStation sta ON statag.StationId = sta.id AND sta.Id = {stationId} 
JOIN tblTags tags ON statag.TagId = tags.Id 
JOIN tblTagSub sub ON sub.tagId = statag.TagId
JOIN tblTagProcess pro ON pro.tagSubId = sub.id 
ORDER BY 3, 4, 5, 6";
                    SqlDataReader dr = com.ExecuteReader();

                    while(dr.Read()) {
                        clsTag tag = new clsTag() {
                            StationId = dr.GetInt32(0),
                            StationName = dr.GetString(1),
                            TagId = dr.GetInt32(2),
                            TagName = dr.GetString(3),
                            SubName = dr.GetString(4),
                            ProcessTag = dr.GetString(5),
                            TagType = dr.GetString(6),
                            Comment = dr.GetString(7),
                            ReadWrite = dr.GetInt16(8),
                        };
                        listTags.Add(tag);
                    }
                    dr.Close();
                }
            }
            return listTags;
        }
    }
}
