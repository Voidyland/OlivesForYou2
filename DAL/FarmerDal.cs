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
        public static int NewOrderForSale (int farmerID, int oliveID, double orderWeight, double orderPrice , int inStock)
        {
            string sql = $"INSERT INTO OrdersForSale (FarmerID, OliveID,  OrderWeight, OrderPrice, InStock)" +
                $" VALUES {farmerID}, {oliveID}, {orderWeight}, {orderPrice}, {inStock};";
            DBHelper db = new DBHelper(DALHelper.PROVIDER, DALHelper.SOURCE);
            int id = db.InsertWithAutoNumKey(sql);            
            return id;
        }  
        public static DataTable AllOrdersForSale (int farmerID)
        {
            string sql = $"SELECT * FROM OrdersForSale WHERE FarmerID = {farmerID};";
            DBHelper db = new DBHelper(DALHelper.PROVIDER, DALHelper.SOURCE);
            return db.GetDataTable(sql);
        }
        
    }
}
