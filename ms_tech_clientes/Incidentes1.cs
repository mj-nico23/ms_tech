using System.Data;

namespace ms_tech_clientes
{
    public class Incidentes1
    {

        public static DataTable RecuperarIncidente(string nroIncidente)
        {
            string sql = "select * from vIncidentes where idIncidente = " + nroIncidente;

            return Sql1.Execute(sql);
        }

        public static DataTable RecuperarIncidentes(string cliente)
        {
            string sql = "select * from vIncidentes where cliente = '" + cliente + "'";

            return Sql1.Execute(sql);
        }
    }
}
