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
        public static void SetSourceAndProvider(string source, string provider)
        {
            DalHelper.SetProvider(provider);
            DalHelper.SetSource(source);
        }
        public static void CreateDBHelperInDalHelper(string source, string provider)
        {
            SetSourceAndProvider(source, provider);
            DalHelper.CreateDBHelper();
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
        public static User Register (string userName, string pass, string email, int userType, string country, int phoneNumber)
        {
            int id = DAL.UserDAL.Register(userName, pass, email, userType, country, phoneNumber);
            return new User(id, userName, pass, email, userType, country, phoneNumber);
        }
        public static bool NewOrderForSale (int farmerID, int oliveID, int orderWeight, double orderPrice, int inStock)
        {
            return DAL.FarmerDal.NewOrderForSale(farmerID, oliveID, orderWeight, orderPrice, inStock);
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
        
    }
}
