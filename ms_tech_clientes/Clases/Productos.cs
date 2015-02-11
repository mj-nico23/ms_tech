using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ms_tech_clientes
{
    public class Productos
    {
        public static DataTable RecuperarProductos()
        {
            string sql = "select * from productos where activo=1 order by nombre";

            return Sql1.Execute(sql);
        }

        public static DataTable RecuperarProblemas(string idProducto)
        {
            string sql = "select * from problemas where activo=1 and idProducto=" + idProducto + " order by nombre";

            return Sql1.Execute(sql);
        }

        public static DataTable RecuperarProblemasProducto(string idProblema)
        {
            string sql = "SELECT p.Nombre AS problema, p2.Nombre AS producto FROM dbo.Problemas p ";
            sql += "INNER JOIN dbo.Productos p2 ON p2.IdProducto = p.IdProducto ";
            sql += "WHERE p.IdProblema=" + idProblema;

            return Sql1.Execute(sql);
        }

        public static string RecuperarSolucion(string idProblema)
        {
            try
            {
                string res = "<ul>";

                string sql = "select * from soluciones where activo=1 and idProblema=" + idProblema;

                foreach (DataRow row in Sql1.Execute(sql).Rows)
                {
                    res += "<li>" + row["descripcion"] + "</li>";
                }

                res += "</ul>";

                return res;

            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}