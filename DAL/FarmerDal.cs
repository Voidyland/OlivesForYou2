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
        public static int NewSale (int farmerID, int oliveID, double saleWeight, double salePrice , int inStock)
        {
            string sql = $"INSERT INTO Sales (FarmerID, OliveID,  SaleWeight, SalePrice, InStock)" +
                $" VALUES ({farmerID}, {oliveID}, {saleWeight}, {salePrice}, {inStock});";
            DBHelper db = new DBHelper(DALHelper.PROVIDER, DALHelper.SOURCE);
            int id = db.InsertWithAutoNumKey(sql);            
            return id;
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
        /// <returns>WRITEDATAERROR (aka 1) if fails</returns>
        public static int UpdateSale (int farmerID,int newOliveID, double newWeight, double newPrice, int newInStock)
        {
            string sql = $"UPDATE Sales SET OliveID ={newOliveID}, SaleWeight = {newWeight}, SalePrice = {newPrice}," +
                $" InStock = {newInStock}";
            DBHelper db = new DBHelper(DALHelper.PROVIDER, DALHelper.SOURCE);
            return db.WriteData(sql);

        }
    }
}
