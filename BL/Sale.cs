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
        private string farmerName;
        private int oliveID;
        private string oliveName;
        private double saleWeight;
        private double salePrice;
        private int inStock;
        private DateTime dateSaleAdded;
        public Sale(int saleID, int farmerID, int oliveID, string oliveName, double saleWeight, double salePrice, int inStock, DateTime dateSaleAdded)
        {
            this.saleID = saleID;
            this.farmerID = farmerID;
            farmerName = "";
            this.oliveID = oliveID;
            this.oliveName = oliveName;
            this.saleWeight = saleWeight;
            this.salePrice = salePrice;
            this.inStock = inStock;
            this.dateSaleAdded = dateSaleAdded;
        }
        public Sale (DataRow dr)
        {
            this.saleID = (int)dr["SaleID"];
            this.farmerID = (int)dr["FarmerID"];
            farmerName = "";
            this.oliveID = (int)dr["OliveID"];
            this.oliveName = "";
            this.saleWeight = (double)dr["SaleWeight"];
            this.salePrice = (double)dr["SalePrice"];
            this.inStock = (int)dr["InStock"];
            this.dateSaleAdded = (DateTime)dr["DateSaleAdded"];
        }
        public Sale (Sale sale)
        {
            this.saleID = sale.saleID;
            this.farmerID = sale.farmerID;
            this.farmerName = sale.farmerName;
            this.oliveID = sale.oliveID;
            this.oliveName = sale.oliveName;
            this.saleWeight = sale.saleWeight;
            this.salePrice = sale.salePrice;
            this.inStock = sale.inStock;
            this.dateSaleAdded = sale.dateSaleAdded;
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
        public string FarmerName
        {
            get
            {
                if (farmerName != "") return farmerName;
                farmerName = DAL.UserDAL.FindUserByID(this.farmerID)["UserName"].ToString();
                return farmerName;
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
        public DateTime DateSaleAdded
        {
            get
            {
                return dateSaleAdded; 
            }
            set
            {
                dateSaleAdded = value;
            }
        }
        public int DeleteThis ()
        {
            DAL.FarmerDal.DeleteSale(this.saleID);
            return saleID;
        }
        public int UpdateThis (int oliveID, string oliveName, double saleWeight, double salePrice, int inStock)
        {
            if (DAL.FarmerDal.UpdateSale(this.saleID, oliveID, saleWeight, salePrice, inStock) == -1) return -1;
            this.oliveID = oliveID;
            this.oliveName = oliveName;
            this.saleWeight = saleWeight;
            this.salePrice = salePrice;
            this.inStock = inStock;
            return this.saleID;
        }
        public override string ToString()
        {
            return $"Olive type: {this.OliveName}, sale weight: {this.saleWeight}kg, sale price: {this.salePrice}$, items in stock: {this.inStock}, date the sale was added: {this.dateSaleAdded}.";
        }
        public bool CreateNewOrder (int companyID, int stocksBought)
        {
            int succsessOrFail = DAL.CompanyDAL.OrderSale(this.saleID, companyID, this.farmerID, this.oliveID, this.saleWeight, this.SalePrice, this.inStock - stocksBought);
            if (succsessOrFail == -1) return false;
            this.inStock -= stocksBought;
            return true;
        }
    }
}

