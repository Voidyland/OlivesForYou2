using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
namespace DAL
{
    public class FarmerDal
    {
        public static bool NewOrderForSale (int farmerID, int oliveID, int orderWeight, double orderPrice , int inStock)
        {
            string sql = $"INSERT INTO OrdersForSale (FarmerID, OliveID,  OrderWeight, OrderPrice, InStock)" +
                $" VALUES {farmerID}, {oliveID}, {orderWeight}, {orderPrice}, {inStock};";
            DBHelper db = new DBHelper(Constantinopal.PROVIDER, Constantinopal.SOURCE);
            if (db.InsertWithAutoNumKey(sql) == Constantinopal.WRITEDATA_ERROR) return false;
            return true;
        }  
        
    }
}
