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
        public static int ConvertCountryToInt (string country)
        {
            DataTable dt = DAL.GeneralDAL.GetCountrys();
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["countryName"].ToString() == country) return (int)dr["countryNumber"];
            }
            return -1;
        }
        public static string CountryToString (int countryNumber)
        {
            DataTable dt = DAL.GeneralDAL.GetCountrys();
            return dt.Rows[countryNumber - 1].ToString();
        }
    }
}
