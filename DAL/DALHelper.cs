using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// A class for constants and for the source and provider. 
    /// </summary>
    public class DALHelper
    {
        public static int WRITEDATA_ERROR = -1;
        public static string SOURCE = "";
        public static string PROVIDER = "";        
        public static void SetSource(string source)
        {
            SOURCE = source;
        }
        public static void SetProvider (string provider)
        {
            PROVIDER = provider;
        }
        
    }
}
