using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class GeneralDAL
    {
        public static DataTable  GetOliveTypes ()
        {
            string sql = "SELECT * FROM Olives;";
            DBHelper db = new DBHelper(DALHelper.PROVIDER,DALHelper.SOURCE);
            return db.GetDataTable(sql);
        }
        public static DataTable GetCountrys ()
        {
            string sql =  "SELECT * FROM Countrys;";
            DBHelper db = new DBHelper(DALHelper.PROVIDER, DALHelper.SOURCE);
            return db.GetDataTable(sql);
        }
    }
}
