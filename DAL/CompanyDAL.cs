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
        /// <summary>
        /// Orders a number of stocks from a sale.
        /// </summary>
        /// <param name="saleID">the sales ID</param>
        /// <param name="companyID">the company ordering</param>
        /// <param name="farmerID">the farmer who owns the sale</param>   
        /// <param name="oliveID">the olives type of the sale</param>
        /// <param name="weight">the weight per stock</param>
        /// <param name="price">the price per stock</param>
        /// <param name="newStock">the amout of stocks</param>
        /// <returns>the ID of the new order. returns -1 if an error has accoured</returns>
        public static int OrderSale (int saleID, int companyID,int farmerID, int oliveID, double weight, double price, int newStock)
        {
            string insertSQL = $"INSERT INTO OrdersOrdered (CompanyID, FarmerID, OliveID, Weight, Price, Stocks, DateOrderOrdered)" +
                $" VALUES ({companyID}, {farmerID}, {oliveID}, {weight}, {price}, {newStock}, '{DateTime.UtcNow}');";
            string changeStockSQL = $"UPDATE Sales SET InStock = InStock - {newStock} WHERE SaleID = {saleID};"; // changed this, dunno if it works to do InStock = InStock - new stock but hey lets hope.
            DBHelper db = new DBHelper();
            int newOrderID = db.InsertWithAutoNumKey(insertSQL);
            int didStockChange = db.WriteData(changeStockSQL);
            if (newOrderID == DBHelper.WRITEDATA_ERROR || didStockChange != 1) return DBHelper.WRITEDATA_ERROR;
            return newOrderID;
        }
        /// <summary>
        /// Returns all orders ordered by a company
        /// </summary>
        /// <param name="companyID">the company's ID</param>
        /// <returns>all the previus orders</returns>
        public static DataTable AllPreviousOrders (int companyID)
        {
            string sql = $"SELECT * FROM OrdersOrdered WHERE CompanyID = {companyID};";
            DBHelper db = new DBHelper();
            DataTable dt = db.GetDataTable(sql);
            if (dt == null)  return null;
            if (dt.Rows.Count < 1) return null;
            return dt;
        }
    }
}
