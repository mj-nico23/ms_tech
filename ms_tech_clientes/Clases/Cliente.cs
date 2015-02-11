using System;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace ms_tech_clientes
{
    public class Cliente
    {
        public static bool ValidarUsuario(string usuario, string pass, out string msj, out string id)
        {
            msj = "";
            id = "";
            string sql = "select * from Clientes where mail= '" + usuario + "'";

            using (DataTable dt = Sql1.Execute(sql))
            {
                if (dt.Rows.Count == 0)
                {
                    msj = "Usuario o Password incorrectos.";
                    return false;
                }

                if (!(bool)dt.Rows[0]["Activo"])
                {
                    msj = "El usuarios no se encuentra activo para ingresar al sistema.";
                    return false;
                }

                if (dt.Rows[0]["Password"].ToString() != getHash(pass))
                {
                    msj = "Usuario o Password incorrectos.";
                    return false;
                }

                id = dt.Rows[0]["IdCliente"].ToString();
                msj = dt.Rows[0]["Nombre"].ToString() + " " + dt.Rows[0]["Apellido"].ToString();
            }

           
            return true;
        }

        private static string getHash(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }
       
    }
}