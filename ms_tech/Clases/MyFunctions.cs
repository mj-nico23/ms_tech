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

    //Extension methods must be defined in a static class
    public static class StringExtension
    {
        // This is the extension method.
        // The first parameter takes the "this" modifier
        // and specifies the type for which the method is defined.
        public static string Left(this String str, int lenght)
        {
            if (str.Length <= lenght)
                return str;
            else
                return str.Substring(0, lenght);
        }
       
    }
}