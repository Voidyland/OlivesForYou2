using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
namespace DAL
{
    public class UserDAL
    {
        public static string provider = @"Microsoft.ACE.OLEDB.12.0";
        public static string source = "OlivesDB.accdb";
        public static DataRow Login(string email, string pass)
        {
            string sql = $"SELECT * FROM Users WHERE Email = '{email}', Pass = '{pass}';";
            DBHelper db = new DBHelper(provider, source);
            DataTable dt = new DataTable(sql);
            if (dt == null || dt.Rows.Count != 1)
            {
                return null;
            }
            return dt.Rows[0];
        }
    }
}
