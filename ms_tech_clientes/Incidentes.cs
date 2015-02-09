using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

namespace ms_tech_clientes
{
    public class Incidentes
    {
        public static DataTable RecuperarIncidente(string nroIncidente)
        {
            string sql = "select * from vIncidentes where idIncidente = " + nroIncidente;

            return Sql.Execute(sql);
        }
    }
}