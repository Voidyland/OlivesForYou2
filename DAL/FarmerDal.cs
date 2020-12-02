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
        public static DataTable AllSales (int farmerID)
        {
            string sql = $"SELECT * FROM Sales WHERE FarmerID = {farmerID};";
            DBHelper db = new DBHelper(DALHelper.PROVIDER, DALHelper.SOURCE);
            DataTable allSales = db.GetDataTable(sql);
            if (allSales.Rows.Count != 0)
                return db.GetDataTable(sql);
            return null;
        }
        public static int DeleteSale (int saleID)
        {
            string sql = $"DELETE * FROM Sales WHERE SaleID = {saleID}";
            DBHelper db = new DBHelper(DALHelper.PROVIDER, DALHelper.SOURCE);
            return db.WriteData(sql);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="farmerID"></param>
        /// <param name="newOliveID"></param>
        /// <param name="newWeight"></param>
        /// <param name="newPrice"></param>
        /// <param name="newInStock"></param>
        /// <returns>WRITEDATAERROR (aka 1) if fails, returns the sales ID otherwise otherwise.</returns>
        public static int UpdateSale (int saleID,int newOliveID, double newWeight, double newPrice, int newInStock)
        {
            string sql = $"UPDATE Sales SET OliveID ={newOliveID}, SaleWeight = {newWeight}, SalePrice = {newPrice}," +
                $" InStock = {newInStock} WHERE SaleID = {saleID}";
            DBHelper db = new DBHelper(DALHelper.PROVIDER, DALHelper.SOURCE);
            if (db.WriteData(sql) == -1) return -1;
            return saleID;
        }
    }
}
