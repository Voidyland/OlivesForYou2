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
    /// A general class for the BL lair's static methods.
    /// </summary>
    public class General
    {
        /// <summary>
        /// The method recives the email and password of a user trying to log in, and if he exists it creates an object of a user with all of his details. Otherwise it returns null.
        /// </summary>
        /// <param name="Email"></param>
        /// <param name="Pass"></param>
        /// <returns></returns>
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
    }
}
