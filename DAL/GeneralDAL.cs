using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// A general class for methods that comunicate with the database but dont realy fit under their own catagory.
    /// </summary>
    public class GeneralDAL
    {
        /// <summary>
        /// Returns all olives in the database.
        /// </summary>
        /// <returns>If an error has occored returns null. Otherwise returns the olives</returns>
        public static DataTable  GetOliveTypes ()
        {
            string sql = "SELECT * FROM Olives;";
            DBHelper db = new DBHelper();
            return db.GetDataTable(sql);
        }
        /// <summary>
        /// Returns all countrys in the datebase.
        /// </summary>
        /// <returns>If an error has occored returns null. Otherwise returns the countrys</returns>
        public static DataTable GetCountrys ()
        {
            string sql =  "SELECT * FROM Countrys;";
            DBHelper db = new DBHelper();
            return db.GetDataTable(sql);
        }
        /// <summary>
        /// Returns all users in the database.
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllUsers ()
        {
            string sql = "SELECT * FROM Users;";
            DBHelper db = new DBHelper();
            return db.GetDataTable(sql);
        }
        /// <summary>
        /// Returns all orders ordered in the database.
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllOrdersOrdered ()
        {
            string sql = "SELECT * FROM OrdersOrdered";
            DBHelper db = new DBHelper();
            return db.GetDataTable(sql);
        }
    }
}
