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
            DBHelper db = new DBHelper();
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
            DBHelper db = new DBHelper();
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
            DBHelper db = new DBHelper();
            DataTable dt = db.GetDataTable(sql);
            if (dt == null || dt.Rows.Count != 1)
            {
                return null;
            }
            return dt.Rows[0];
        }
        /// <summary>
        /// Finds and returns a user, using his userName to search.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>The user if he exists, null if there was an error\he does not exist</returns>
        public static DataRow FindUserByName (string userName)
        {
            string sql = $"SELECT * FROM Users WHERE UserName = {userName}";
            DBHelper db = new DBHelper();
            DataTable dt = db.GetDataTable(sql);
            if (dt == null || dt.Rows.Count != 1)
            {
                return null;
            }
            return dt.Rows[0];
        }
        /// <summary>
        /// Finds and returns a user, using his email to search.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>The user if he exists, null if there was an error\he does not exist</returns>
        public static DataRow FindUserByEmail (string email)
        {
            string sql = $"SELECT * FROM Users WHERE Email = {email}";
            DBHelper db = new DBHelper();
            DataTable dt = db.GetDataTable(sql);
            if (dt == null || dt.Rows.Count != 1)
            {
                return null;
            }
            return dt.Rows[0];
        }
        /// <summary>
        /// At the time of writing this method is ment for farmer only but it could very well be used for company so I'm putting it in the users class. 
        /// Also the method deletes a given order what a supprise.
        /// </summary>
        /// <param name="orderID">The orders ID</param>
        /// <returns>false if error, otherwise true.</returns>
        public static bool DeleteOrderOrderer (int orderID)
        {
            string sql = $"DELETE FROM OrdersOrdered WHERE OrderID = {orderID}";
            DBHelper db = new DBHelper();
            int result = db.WriteData(sql);
            if (result == DBHelper.WRITEDATA_ERROR) return false;
            return true;
        } 
    }
}
