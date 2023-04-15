using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountBook.DAL.Core
{
    public class SqlHelper
    {
        public static string ConnectionString { get; set; } = "server=.;database=AccountBook;uid=sa;pwd=Aa426124716";

        public static DataTable ExecuteTable(string cmdText, params SqlParameter[] sqlParameters)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.Parameters.AddRange(sqlParameters);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                return ds.Tables[0];
            }
        }

        public static int ExecuteNonQuery(string cmdText, params SqlParameter[] sqlParameters)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.Parameters.AddRange(sqlParameters);
                return cmd.ExecuteNonQuery();
            }
        }
    }
}
