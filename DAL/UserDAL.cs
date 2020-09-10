using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
namespace DAL
{
    /// <summary>
    /// A class for users in the database.
    /// </summary>
    public class UserDAL
    {
        public static string provider = @"Microsoft.ACE.OLEDB.12.0";
        public static string source = "OlivesDB.accdb";
        /// <summary>
        /// The method recives the email and password of a user trying to log in, and return the datarow of the users details if he exists. Otherwise, it will return null.
        /// </summary>
        /// <param name="email">The users email</param>
        /// <param name="pass">the users password</param>
        /// <returns>Either the users details in the form of a datarow, or null if he does not exist/an error has accoured.</returns>
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
