using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ms_tech_clientes
{
    public class Sql1
    {
        //static string cnn_string = "data source=NYC;initial catalog=ms_tech;user id=sa;password=12345;";
        static string cnn_string = "data source=nb-nico2\\sqlexpress;initial catalog=ms_tech;user id=sa;password=12345;";
        public static DataTable Execute(string sql)
        {
            DataTable dt = new DataTable();

            using (SqlDataAdapter ad = new SqlDataAdapter(sql, cnn_string))
            {
                ad.Fill(dt);
            }
            return dt;
        }
    }
}