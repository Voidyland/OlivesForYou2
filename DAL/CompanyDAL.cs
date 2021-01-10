using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
namespace DAL
{
    public class CompanyDAL
    {
        /// <summary>
        /// Finds all sales in the database. If there are no sales or if an error has accoured then it returns null.
        /// </summary>
        /// <returns></returns>
        public static DataTable FindAllSales ()
        {
            string sql = "SELECT * FROM Sales";
            DBHelper db = new DBHelper();
            DataTable dt = db.GetDataTable(sql);
            if (dt == null) return null;
            if (dt.Rows.Count < 1) return null;
            return dt; 
        }
        /// <summary>
        /// Finds all the favorite farmers of a given company
        /// </summary>
        /// <param name="companyID"></param>
        /// <returns></returns>
        public static DataTable AllFavoriteFarmers (int companyID)
        {
            string sql = $"SELECT * FROM FavoriteFarmer WHERE CompanyID = {companyID}";
            DBHelper db = new DBHelper();
            DataTable dt = db.GetDataTable(sql);
            if (dt == null) return null;
            if (dt.Rows.Count < 1) return null;
            return dt;
        }
    }
}
