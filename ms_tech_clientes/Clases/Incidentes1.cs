using System;
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

        public static void CrearIncidente(string idProblema, string descripcion, string cliente)
        {
            string sql = "INSERT INTO dbo.Incidentes ( IdUsuario,IdCliente, IdProblema,Fecha,Descripcion, IdPrioridad)";

            sql += string.Format(" values ({0}, {1}, {2}, '{3}', '{4}', 1)", "1", cliente, idProblema, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), descripcion);

            sql += "select @@IDENTITY";

            string idInc = Sql1.Execute(sql).Rows[0][0].ToString();

            sql = "INSERT INTO [incidentesestados]([IdEstado],[IdIncidente],[IdUsuario],[FechaActualizacion],[Observacion])";
            sql += string.Format("  VALUES(1,{0},1,'{1}','')", idInc, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Sql1.Execute(sql);
        }
    }
}
