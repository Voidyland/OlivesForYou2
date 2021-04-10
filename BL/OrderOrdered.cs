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
        private const int MISSING_INT = -1;
        private int orderID;
        private int companyID;
        private string companyName;
        private string companyEmail;
        private int farmerID;
        private string farmerName;
        private int countryID;
        private string countryName; // country to ship to
        private int oliveID;
        private string oliveName;
        private double orderWeight;
        private double orderPrice;
        private int stocks;
        private DateTime dateOrderOrdered;
        private DateTime dateOrderSent;
        private DateTime dateOrderArrived;
        /// <summary>
        /// Constructor for OrderOrdered. Recives parameters from datarow.
        /// </summary>
        /// <param name="orderOrdered"></param>
        public OrderOrdered (DataRow orderOrdered)
        {
            orderID = int.Parse(orderOrdered["OrderID"].ToString());
            companyID = int.Parse(orderOrdered["CompanyID"].ToString());
            companyName = "";
            companyEmail = "";
            farmerID = int.Parse(orderOrdered["FarmerID"].ToString());
            farmerName = "";
            countryID = MISSING_INT;
            countryName = "";
            oliveID = int.Parse(orderOrdered["OliveID"].ToString());
            oliveName = "";
            orderWeight = double.Parse(orderOrdered["Weight"].ToString());
            orderPrice = double.Parse(orderOrdered["Price"].ToString());
            stocks = int.Parse(orderOrdered["Stocks"].ToString());
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
                {
                    DataRow company = DAL.UserDAL.FindUserByID(companyID);
                    companyName = company["UserName"].ToString();
                    if (countryID == MISSING_INT) // Doing this because might as well, adds 2 lines of code but could save alot down the line. All three of these details come from the same user after all.
                        countryID = int.Parse(company["CountryNumber"].ToString());
                    if (companyEmail == "") //Same here, costs little but can bennefit alot.
                        companyEmail = company["Email"].ToString();
                }
                return companyName;
            }
        }
        public string CompanyEmail
        {
            get
            {
                if (companyEmail == "")
                {
                    DataRow company = DAL.UserDAL.FindUserByID(companyID);
                    companyEmail = company["Email"].ToString();
                    if (countryID == MISSING_INT)// Doing this because might as well, adds 2 lines of code but could save alot down the line. All three of these details come from the same user after all.
                        countryID = int.Parse(company["CountryNumber"].ToString());
                    if (companyName == "") //Same here, costs little but can bennefit alot.
                        companyName = company["UserName"].ToString();
                }
                return companyEmail;
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
        public int CountryID
        {
            get
            {
                if (countryID == MISSING_INT)
                {
                    DataRow company = DAL.UserDAL.FindUserByID(companyID);
                    countryID = int.Parse(company["CountryNumber"].ToString());
                    if (companyName == "") // Doing this because might as well, adds 2 lines of code but could save alot down the line. All three of these details come from the same user after all.
                        companyName = company["UserName"].ToString();
                    if (companyEmail == "") //Same here, costs little but can bennefit alot.
                        companyEmail = company["Email"].ToString();
                }
                return countryID;
            }
        }
        public string CountryName
        {
            get
            {
                if (countryName != "") return countryName;
                if (countryID == MISSING_INT)
                {
                    DataRow company = DAL.UserDAL.FindUserByID(companyID);                                       
                    countryID = int.Parse(company["CountryNumber"].ToString());
                    if (companyName == "") // Doing this because might as well, adds 2 lines of code but could save alot down the line. All three of these details come from the same user after all.
                        companyName = company["UserName"].ToString();
                    if (companyEmail == "") //Same here, costs little but can bennefit alot.
                        companyEmail = company["Email"].ToString();
                }
                countryName = General.CountryToString(countryID);
                return countryName;
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
        public int Stocks
        {
            get
            {
                return stocks;
            }
            set
            {
                stocks = value;
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
        /// <summary>
        /// Confirms this order was sent to the company.
        /// </summary>
        /// <returns>Whether or not the confirmation succeded or not</returns>
        public bool ConfirmOrder ()
        {
            if (DAL.FarmerDal.ConfirmOrderSent(this.orderID) != DAL.DALHelper.WRITEDATA_ERROR) return true;
            return false;
        } 
        /// <summary>
        /// Deletes this order from the database
        /// </summary>
        /// <returns>Whether the process was a success of not</returns>
        public bool DeleteOrder ()
        {
            return DAL.UserDAL.DeleteOrderOrderer(this.orderID);
        }
        public override string ToString()
        {
            return $"Ordered by: {this.CompanyName} from: {this.FarmerName} at a weight of: {this.orderWeight}kg and a price of: {this.orderPrice}$ per stock, with:" +
                $" {this.stocks} ordered. Olive type: {this.OliveName}. Destination: {this.countryName}. Ordered on: {this.dateOrderOrdered.Date}. " +
                $"Sent: {(this.dateOrderSent == DateTime.MinValue ? "no" : $"on {this.dateOrderSent.Date}")}. " +
                $"Arrived: {(this.dateOrderArrived == DateTime.MinValue ? "no" : $"on {this.dateOrderArrived.Date}")}";
        }
    }
}
