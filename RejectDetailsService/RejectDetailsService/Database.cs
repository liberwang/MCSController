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
                    com.CommandText = $@"SELECT id, name, conrollerId FROM tblStation WHERE controller = {ControllerID}";
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
                    com.CommandText = $@"SELECT a.id, a.tagName, a.tagType, a.Comment, a.ReadWrite FROM tbltags a, tblStationTag b WHERE a.id = b.TagId and b.StationId = {StationID} ";
                    SqlDataReader dr = com.ExecuteReader();

                    while(dr.Read()) {
                        clsTag tag = new clsTag() {
                            StationId = StationID,
                            StationName = StationName,
                            TagId = dr.GetInt32(0),
                            TagName = dr.GetString(1),
                            TagType = dr.GetString(2),
                            Comment = dr.GetString(3),
                            ReadWrite = dr.GetInt16(4),
                        };
                        listTags.Add(tag);
                    }
                    dr.Close();
                }
            }
            return listTags;
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
