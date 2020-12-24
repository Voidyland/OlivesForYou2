using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using DAL;
namespace BL
{
    public class OrderOrdered
    {
        private int orderID;
        private int companyID;
        private string companyName;
        private int farmerID;
        private string farmerName;
        private int oliveID;
        private string oliveName;
        private double orderWeight;
        private double orderPrice;
        private DateTime dateOrderOrdered;
        private DateTime dateOrderSent;
        private DateTime dateOrderArrived;
        public OrderOrdered (DataRow orderOrdered)
        {
            orderID = int.Parse(orderOrdered["OrderID"].ToString());
            companyID = int.Parse(orderOrdered["CompanyID"].ToString());
            companyName = "";
            farmerID = int.Parse(orderOrdered["FarmerID"].ToString());
            farmerName = "";
            oliveID = int.Parse(orderOrdered["OliveID"].ToString());
            oliveName = "";
            orderWeight = double.Parse(orderOrdered["Weight"].ToString());
            orderPrice = double.Parse(orderOrdered["Price"].ToString());
            dateOrderOrdered = (DateTime)orderOrdered["DateOrderOrdered"];
            if (!orderOrdered.IsNull("DateOrderSent"))
                dateOrderSent = (DateTime)orderOrdered["DateOrderSent"];
            else
                dateOrderSent = DateTime.MinValue;
            if (!orderOrdered.IsNull("DateOrderArrived"))
                dateOrderArrived = (DateTime)orderOrdered["DateOrderArrived"];
            else
                dateOrderArrived = DateTime.MinValue;
        }
        public int OrderID
        {
            get
            {
                return orderID;
            }
        }
        public int CompanyID
        {
            get
            {
                return companyID;
            }
        }
        public string CompanyName
        {
            get
            {
                if (companyName == "")
                    companyName = DAL.UserDAL.FindUserByID(companyID)["UserName"].ToString();
                return companyName;
            }
        }
        public int FarmerID
        {
            get
            {
                return farmerID;
            }
        }
        public string FarmerName
        {
            get
            {
                if (farmerName == "") 
                    farmerName =  DAL.UserDAL.FindUserByID(farmerID)["UserName"].ToString();
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
        public DateTime DateOrderOrdered
        {
            get
            {
                return dateOrderOrdered;
            }
            set
            {
                dateOrderOrdered = value;
            }
        }
        public DateTime DateOrderSent
        {
            get
            {
                return dateOrderSent;
            }
            set
            {
                dateOrderSent = value;
            }
        }
        public DateTime DateOrderArrived
        {
            get
            {
                return dateOrderArrived;
            }
            set
            {
                dateOrderArrived = value;
            }
        }
        public bool ConfirmOrder ()
        {
            if (DAL.FarmerDal.ConfirmOrderSent(this.orderID) != DAL.DALHelper.WRITEDATA_ERROR) return true;
            return false;
        } 
    }
}
