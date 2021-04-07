using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data;
using System.Data.OleDb;
namespace BL
{
    /// <summary>
    /// A general class for the BL lairs static methods.
    /// </summary>
    public class General
    {
        /// <summary>
        /// Sets the source and provider of the data base
        /// </summary>
        /// <param name="source"></param>
        /// <param name="provider"></param>
        public static void SetSourceAndProvider(string source, string provider)
        {
            DALHelper.SetSource(source);
            DALHelper.SetProvider(provider);
        }
        
        /// <summary>
        /// The method recives the email and password of a user trying to log in, and if he exists it creates an object of a user with all of his details. Otherwise it returns null.
        /// </summary>
        /// <param name="Email">The users email</param>
        /// <param name="Pass">The users passwird</param>
        /// <returns>Either the user as an object or null if an error occured in the DAL.</returns>
        public static User Login(string Email, string Pass)
        {
            DataRow dr = DAL.UserDAL.Login(Email, Pass);
            if (dr == null)
            {
                return null;
            }
            User user = new User(dr);
            return user;
        }
        /// <summary>
        /// A method that registers a new user and returns the new user.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pass"></param>
        /// <param name="email"></param>
        /// <param name="userType"></param>
        /// <param name="country"></param>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public static User Register (string userName, string pass, string email, int userType, string country, string phoneNumber)
        {
            int countryNumber = ConvertCountryToInt(country);
            if (countryNumber == -1) throw new Exception("Invalid country name");
            int id = DAL.UserDAL.Register(userName, pass, email, userType, countryNumber, phoneNumber);
            return new User(id, userName, pass, email, userType, country, phoneNumber);
        }
        /// <summary>
        /// Returns all possible olives.
        /// </summary>
        /// <returns></returns>
        public static List<Olive> AllOlives ()
        {
            List<Olive> olives = new List<Olive>();
            DataTable db = DAL.GeneralDAL.GetOliveTypes();
            foreach (DataRow dr in db.Rows)
            {
                olives.Add(new Olive(dr));
            }
            return olives;
        }
        /// <summary>
        /// Convert a contry to an int. The int is the ID of the contry in the database.
        /// </summary>
        /// <param name="country">The countrys name</param>
        /// <returns></returns>
        public static int ConvertCountryToInt (string country)
        {
            DataTable dt = DAL.GeneralDAL.GetCountrys();
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["countryName"].ToString() == country) return (int)dr["countryNumber"];
            }
            return -1;
        }
        /// <summary>
        /// Converts a country to string. The string is the countrys name
        /// </summary>
        /// <param name="countryNumber">the countrys ID</param>
        /// <returns></returns>
        public static string CountryToString (int countryNumber)
        {
            DataTable dt = DAL.GeneralDAL.GetCountrys();
            return dt.Rows[countryNumber - 1]["countryName"].ToString();
        }
        /// <summary>
        /// Finds all countrys (in the database), and returns them in a list.
        /// </summary>
        /// <returns>All countrys (in the database)</returns>
        public static List<string> AllCountrys ()
        {
            List<string> countrys = new List<string>();
            DataTable dt = DAL.GeneralDAL.GetCountrys();
            foreach (DataRow dr in dt.Rows)
            {
                countrys.Add(dr["countryName"].ToString());
            }
            return countrys;
        }
        /// <summary>
        /// Finds all users, and returns them in a list.
        /// </summary>
        /// <returns>All users</returns>
        public static List<User> AllUsers ()
        {
            List<User> users = new List<User>();
            DataTable dt = DAL.GeneralDAL.GetAllUsers();
            foreach (DataRow dr in dt.Rows)
            {
                users.Add(new User(dr));
            }
            return users;
        }
        /// <summary>
        /// Finds all non admin users, and returns them in a list. 
        /// </summary>
        /// <returns>All non admin users</returns>
        public static List<User> AllNonAdmins ()
        {
            List<User> allUsers = AllUsers();
            List<User> allNonAdmins = new List<User>();
            foreach(User user in allUsers)
            {
                if (user.UserType != 1) allNonAdmins.Add(user); //An admins user type is 1.
            }
            return allNonAdmins;
        }
        /// <summary>
        /// Finds a user by their userName
        /// </summary>
        /// <param name="userName">the users username</param>
        /// <returns>The user</returns>
        public static User FindUserByUserName(string userName)
        {
            DataRow dr = DAL.UserDAL.FindUserByName(userName);
            if (dr == null) return null;
            return new User(dr);
        }
        /// <summary>
        /// Finds a user by his email.
        /// </summary>
        /// <param name="email">the users email</param>
        /// <returns>The user</returns>
        public static User FindUserByEmail (string email)
        {
            DataRow dr = DAL.UserDAL.FindUserByEmail(email);
            if (dr == null) return null;
            return new User(dr);
        }        
    }
}
