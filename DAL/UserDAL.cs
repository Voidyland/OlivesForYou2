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
            string sql = $"SELECT * FROM Users WHERE Email = '{email}' AND Pass = '{pass}';";
            DBHelper db = new DBHelper(DALHelper.PROVIDER, DALHelper.SOURCE);
            DataTable dt = db.GetDataTable(sql);
            if (dt == null || dt.Rows.Count != 1)
            {
                return null;
            }
            return dt.Rows[0];
        }
        /// <summary>
        /// A method which registers a new user and enters it's details to the database.  
        /// </summary>
        /// <param name="userName">The user's alliasis aka user name</param>
        /// <param name="pass">The user's password</param>
        /// <param name="email">The user's email adress</param>
        /// <param name="userType">The users type: 1 - manager, 2 - farmer, 3 - company</param>
        /// <param name="countryNumber">The ID of the country the user is from</param>
        /// <param name="phoneNumber">The user's phone number</param>
        /// <returns>Returns the new ID if the user was created. If the method fails throws an exeption.</returns>
        public static int Register (string userName, string pass, string email, int userType, int countryNumber, string phoneNumber)
        {
            string sql = $"INSERT INTO Users (UserName, Pass, Email, UserType, CountryNumber, PhoneNumber) " +
                $"VALUES ('{userName}', '{pass}', '{email}', {userType}, '{countryNumber}', '{phoneNumber}');";
            DBHelper db = new DBHelper(DALHelper.PROVIDER, DALHelper.SOURCE);
            int newID = db.InsertWithAutoNumKey(sql);
            if (newID == DBHelper.WRITEDATA_ERROR) throw new Exception();
            return newID;
        }
        /// <summary>
        /// Finds and returns a user, using his userID to search.
        /// </summary>
        /// <param name="userID">The ID of the user being seached</param>
        /// <returns>The user if he exists, null if there was an error\he does not exist</returns>
        public static DataRow FindUserByID(int userID)
        {
            string sql = $"SELECT * FROM Users WHERE UserID = {userID}";
            DBHelper db = new DBHelper(DALHelper.PROVIDER, DALHelper.SOURCE);
            DataTable dt = db.GetDataTable(sql);
            if (dt == null || dt.Rows.Count != 1)
            {
                return null;
            }
            return dt.Rows[0];
        }
    }
}
