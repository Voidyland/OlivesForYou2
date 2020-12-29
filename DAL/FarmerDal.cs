using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;

namespace DAL
{
    public class FarmerDal
    {
        /// <summary>
        /// Adds a new sale from a certain farmer to the database.
        /// </summary>
        /// <param name="farmerID">The farmers ID</param>
        /// <param name="oliveID">The ID of the olive in the sale</param>
        /// <param name="saleWeight">The weight of one stock of the sale</param>
        /// <param name="salePrice">The price of one stock of the sale</param>
        /// <param name="inStock">The amout of stocks of the sale available for buying</param>
        /// <param name="DateSaleAdded">The date the sale was added to the database</param>
        /// <returns>The ID of the new sale. -1/WRITEDATA_ERROR if an error has occured</returns>
        public static int NewSale (int farmerID, int oliveID, double saleWeight, double salePrice , int inStock, DateTime DateSaleAdded)
        {
            try
            {
                string sql = $"INSERT INTO Sales (FarmerID, OliveID,  SaleWeight, SalePrice, InStock, DateSaleAdded)" +
                    $" VALUES ({farmerID}, {oliveID}, {saleWeight}, {salePrice}, {inStock}, {DateSaleAdded.ToOADate()});";
                DBHelper db = new DBHelper(DALHelper.PROVIDER, DALHelper.SOURCE);
                int id = db.InsertWithAutoNumKey(sql);
                return id;
            }
            catch (Exception e)
            {
                return DALHelper.WRITEDATA_ERROR;
            }
        }  
        /// <summary>
        /// Returns all sales a farmer has posted, so long as those sales are not sold out.
        /// </summary>
        /// <param name="farmerID">The id of the farmer</param>
        /// <returns>The sales of the farmer. If none exist returns null. </returns>
        public static DataTable AllSales (int farmerID)
        {
            string sql = $"SELECT * FROM Sales WHERE FarmerID = {farmerID};";
            DBHelper db = new DBHelper(DALHelper.PROVIDER, DALHelper.SOURCE);
            DataTable allSales = db.GetDataTable(sql);
            if (allSales.Rows.Count != 0)
                return allSales;
            return null;
        }
        /// <summary>
        /// Deletes a given sale from the database.
        /// </summary>
        /// <param name="saleID">The orders ID</param>
        /// <returns>-1 aka WRITEDATA_ERROR if it failed, the sales ID otherwise</returns>
        public static int DeleteSale (int saleID)
        {
            string sql = $"DELETE * FROM Sales WHERE SaleID = {saleID}";
            DBHelper db = new DBHelper(DALHelper.PROVIDER, DALHelper.SOURCE);
            if (db.WriteData(sql) == DALHelper.WRITEDATA_ERROR) return DALHelper.WRITEDATA_ERROR;
            return saleID;
        }
        /// <summary>
        /// Updates a sale a farmer made with new details.
        /// </summary>
        /// <param name="saleID">The ID of the sale</param>
        /// <param name="newOliveID">The new ID of the olive</param>
        /// <param name="newWeight">The new weight of the order (weight per one stock)</param>
        /// <param name="newPrice">The new price of the order (price per one stock)</param>
        /// <param name="newInStock">The new amout of stocks available for purchace</param>
        /// <returns>WRITEDATAERROR (aka 1) if fails, returns the sales ID otherwise otherwise.</returns>
        public static int UpdateSale (int saleID,int newOliveID, double newWeight, double newPrice, int newInStock)
        {
            string sql = $"UPDATE Sales SET OliveID ={newOliveID}, SaleWeight = {newWeight}, SalePrice = {newPrice}," +
                $" InStock = {newInStock} WHERE SaleID = {saleID}";
            DBHelper db = new DBHelper(DALHelper.PROVIDER, DALHelper.SOURCE);
            if (db.WriteData(sql) == DALHelper.WRITEDATA_ERROR) return DALHelper.WRITEDATA_ERROR;
            return saleID;
        }
        /// <summary>
        /// Finds and returns all orders that were ordered from a certain farmer.
        /// </summary>
        /// <param name="farmerID">The farmers ID</param>
        /// <returns>The orders ordered</returns>
        public static DataTable OrderedFromFarmer (int farmerID)
        {
            string sql = $"SELECT * FROM OrdersOrdered WHERE FarmerID = {farmerID}";
            DBHelper db = new DBHelper(DALHelper.PROVIDER, DALHelper.SOURCE);
            DataTable dt = db.GetDataTable(sql);
            return dt;
        }
        /// <summary>
        /// Confirms a certain order was sent to the company, and sets the time it was sent to the time the method was called.
        /// </summary>
        /// <param name="orderID">The ID of the order that was sent</param>
        /// <returns>-1 if it failed, the orders ID otherwise</returns>
        public static int ConfirmOrderSent (int orderID)
        {
            string sql = $"UPDATE OrdersOrdered SET DateOrderSent = #{DateTime.Now.ToOADate()}# WHERE OrderID = {orderID}";
            DBHelper db = new DBHelper(DALHelper.PROVIDER, DALHelper.SOURCE);
            if (db.WriteData(sql) == DALHelper.WRITEDATA_ERROR) return DALHelper.WRITEDATA_ERROR;
            return orderID;
        }
    }
}
