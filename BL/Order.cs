using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Order
    {
        private int saleID;
        private int farmerID;
        private string oliveType;
        private double orderWeight;
        private double orderPrice;
        private int inStock;

        public Order(int saleID, int farmerID, string oliveType, double orderWeight, double orderPrice, int inStock)
        {
            this.saleID = saleID;
            this.farmerID = farmerID;
            this.oliveType = oliveType;
            this.orderWeight = orderWeight;
            this.orderPrice = orderPrice;
            this.inStock = inStock;
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
        public string OliveType
        {
            get
            {
                return oliveType;
            }
            set
            {
                oliveType = value;
            }
        }
        public double OrderWieght
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
                return InStock;
            }
            set
            {
                inStock = value;
            }
        }
    }
}
