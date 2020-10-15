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
            DBHelper db = new DBHelper(Constantinopal.PROVIDER, Constantinopal.SOURCE);
            DataTable dt = new DataTable(sql);
            if (dt == null || dt.Rows.Count != 1)
            {
                return null;
            }
            return dt.Rows[0];
        }
        /// <summary>
        /// A method which registers a new user and enters it's details to the database.  
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pass"></param>
        /// <param name="email"></param>
        /// <param name="userType"></param>
        /// <param name="country"></param>
        /// <param name="phoneNumber"></param>
        /// <returns>Returns the new ID if the user was created. If the method fails throws an exeption.</returns>
        public static int Register (string userName, string pass, string email, int userType, string country, int phoneNumber)
        {
            string sql = $"INSERT INTO Users (UserName, Pass, Email, UserType, Country, PhoneNumber) " +
                $"VALUES ({userName}, {pass}, {email}, {userType}, {country}, {phoneNumber});";
            DBHelper db = new DBHelper(Constantinopal.PROVIDER, Constantinopal.SOURCE);
            int newID = db.InsertWithAutoNumKey(sql);
            if (newID == DBHelper.WRITEDATA_ERROR) throw new Exception();
            return newID;
        }
    }
}
