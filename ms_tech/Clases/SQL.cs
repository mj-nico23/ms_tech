using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ms_tech.Models;

namespace ms_tech.Clases
{
    public class SQL
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(db.Database.Connection.ConnectionString);
        }

        public static DataTable Execute(string strSql)
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter adap = new SqlDataAdapter(strSql, GetConnection()))
            {
                adap.Fill(dt);
            }

            return dt;
        }
    }
}