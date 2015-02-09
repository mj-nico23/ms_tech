using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ms_tech_clientes
{
    public class Incidentes1
    {

        public static DataTable RecuperarIncidente(string nroIncidente)
        {
            string sql = "select * from vIncidentes where idIncidente = " + nroIncidente;

            return Sql1.Execute(sql);
        }
    }
}
