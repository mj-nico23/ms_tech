using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ms_tech.Clases
{
    public static class MyFunctions
    {
        public static string Left(string dato, int lenght)
        {
            if (dato.Length > lenght)
                return dato.Substring(0, lenght);

            return dato;
        }
    }
}