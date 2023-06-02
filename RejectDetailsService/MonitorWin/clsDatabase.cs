using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace MonitorWin
{
    internal class clsDatabase
    {

        public static readonly string DB_CONNECT = @"Server={0},1433\SQLExpress;Database=MCS;User Id=mcs;Password=mcs";

        public static DataSet GetShiftSum(string dbConnect, string prodName, string startTime, string endTime)
        {
            using (SqlConnection conn = new SqlConnection(dbConnect))
            {
                using (SqlCommand com = conn.CreateCommand())
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandText = "dbo.spGetShiftCount";

                    com.Parameters.Add(new SqlParameter("@prodName", prodName));
                    com.Parameters.Add(new SqlParameter("@dtStart", startTime));
                    com.Parameters.Add(new SqlParameter("@dtEnd", endTime));

                    conn.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter
                    {
                        SelectCommand = com
                    };

                    DataSet shiftsum = new DataSet();
                    adapter.Fill(shiftsum, "Customers");

                    return shiftsum;
                }
            }
        }
    }
}
