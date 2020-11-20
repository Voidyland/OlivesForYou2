using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BL
{
    public class Sale
    {
        private int saleID;
        private int farmerID;
        private int oliveID;
        private string oliveName;
        private double saleWeight;
        private double salePrice;
        private int inStock;

        public Sale(int saleID, int farmerID, int oliveID, string oliveName, double saleWeight, double salePrice, int inStock)
        {
            this.saleID = saleID;
            this.farmerID = farmerID;
            this.oliveID = oliveID;
            this.oliveName = oliveName;
            this.saleWeight = saleWeight;
            this.salePrice = salePrice;
            this.inStock = inStock;
        }
        public Sale (DataRow dr)
        {
            this.saleID = (int)dr["SaleID"];
            this.farmerID = (int)dr["FarmerID"];
            this.oliveID = (int)dr["OliveID"];
            this.oliveName = "";
            this.saleWeight = (double)dr["SaleWeight"];
            this.salePrice = (double)dr["SalePrice"];
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
        public double SaleWeight
        {
            get
            {
                return saleWeight;
            }
            set
            {
                saleWeight = value;
            }
        }
        public double SalePrice
        {
            get
            {
                return salePrice;
            }
            set
            {
                salePrice = value;
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
        public int DeleteThis ()
        {
            DAL.FarmerDal.DeleteSale(this.saleID);
            return saleID;
        }
    }
}
