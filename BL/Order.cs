using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BL
{
    public class Order
    {
        private int saleID;
        private int farmerID;
        private int oliveID;
        private string oliveName;
        private double orderWeight;
        private double orderPrice;
        private int inStock;

        public Order(int saleID, int farmerID, int oliveID, string oliveName, double orderWeight, double orderPrice, int inStock)
        {
            this.saleID = saleID;
            this.farmerID = farmerID;
            this.oliveID = oliveID;
            this.oliveName = oliveName;
            this.orderWeight = orderWeight;
            this.orderPrice = orderPrice;
            this.inStock = inStock;
        }
        public Order (DataRow dr)
        {
            this.saleID = (int)dr["SaleID"];
            this.farmerID = (int)dr["FarmerID"];
            this.oliveID = (int)dr["OliveID"];
            this.oliveName = "";
            this.orderWeight = (double)dr["OrderWeight"];
            this.orderPrice = (double)dr["OrderPrice"];
            this.inStock = (int)dr["InStock"];
        }
        public int SaleID
        {
            get
            {
                return saleID;
            }
            set
            {
                saleID = value;
            }
        }
        public int FarmerID
        {
            get
            {
                return farmerID;
            }
            set
            {
                farmerID = value;
            }
        }
        public int OliveID
        {
            get
            {
                return oliveID;
            }
            set
            {
                oliveID = value;
            }
        }
        public string OliveName
        {
            get
            {
                if (oliveName != "") return oliveName;
                DataTable dt = DAL.GeneralDAL.GetOliveTypes();
                foreach (DataRow dr in dt.Rows)
                {
                    if ((int)dr["OliveID"] == oliveID) oliveName = dr["OliveName"].ToString();
                }
                return oliveName;
            }
            set
            {
                oliveName = value;
            }
        }
        public double OrderWeight
        {
            get
            {
                return orderWeight;
            }
            set
            {
                orderWeight = value;
            }
        }
        public double OrderPrice
        {
            get
            {
                return orderPrice;
            }
            set
            {
                orderPrice = value;
            }
        }
        public int InStock
        {
            get
            {
                return inStock;
            }
            set
            {
                inStock = value;
            }
        }
    }
}
