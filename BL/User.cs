using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using DAL;

namespace BL
{
    /// <summary>
    /// A class representing a user.
    /// </summary>
    public class User
    {
        private int userID;
        private string userName;
        private string email;
        private string pass;
        private int userType; //Manager = 1, Farmer = 2, Company = 3.
        private string country;
        private string phoneNumber;
        private string profileDescription;
        private string profilePicture;
        public int UserID
        {
            get
            {
                return userID;
            }
            set
            {
                userID = value;
            }
        }
        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
            }
        }
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
            }
        }
        public string Pass
        {
            get
            {
                return pass;
            }
            set
            {
                pass = value;
            }
        }
        public int UserType
        {
            get
            {
                return userType;
            }
            set
            {
                userType = value;
            }
        }
       
        public string Country
        {
            get
            {
                return country;
            }
            set
            {
                country = value;
            }
        }
        public string PhoneNumber
        {
            get
            {
                return phoneNumber;
            }
            set
            {
                phoneNumber = value;
            }
        }
        public string ProfileDescription
        {
            get
            {
                return profileDescription;
            }
            set
            {
                profileDescription = value;
            }
        }
        public string ProfilePicture
        {
            get
            {
                return profilePicture;
            }
            set
            {
                profilePicture = value;
            }
        }
        /// <summary>
        /// Constructor for the user class. Recives the users details from a datarow.
        /// </summary>
        /// <param name="dr">The users details in the form of a datarow</param>
        public User(DataRow dr)
        {
            this.userID = (int)dr["UserID"];
            this.userName = dr["UserName"].ToString();
            this.email = dr["Email"].ToString();
            this.pass = dr["Pass"].ToString();
            this.userType = (int)dr["UserType"];
            this.country = General.CountryToString((int)dr["CountryNumber"]);
            this.phoneNumber = dr["PhoneNumber"].ToString();
            this.profileDescription = dr["ProfileDescription"].ToString();
            this.profilePicture = dr["ProfilePicture"].ToString();
        }

        public User(int userID, string userName, string email, string pass, int userType, string country, string phoneNumber)
        {
            this.userID = userID;
            this.userName = userName;
            this.email = email;
            this.pass = pass;
            this.userType = userType;
            this.country = country;
            this.phoneNumber = phoneNumber;
        }
        /// <summary>
        /// A method that returns all sales of the chosen farmer
        /// </summary>
        /// <param name="soldOut">True if you want to view all sales that sold out, false otherwise</param>
        /// <returns></returns>
        public List<Sale> AllSales(bool soldOut)
        {
            List<Sale> sales = new List<Sale>();
            DataTable dt = FarmerDal.AllSales(this.userID);
            if (dt == null) return null;
            if (soldOut)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (int.Parse(dr["InStock"].ToString()) == 0)
                    {
                        sales.Add(new Sale(dr));
                    }
                }
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (int.Parse(dr["InStock"].ToString()) != 0)
                    {
                        sales.Add(new Sale(dr));
                    }
                }
            }
            return sales;
        }
        /// <summary>
        /// Gets all orders ordered from the farmer and sorts them by whether or not they were handeled yet.
        /// </summary>
        /// <returns>The orders ordered</returns>
        public List<OrderOrdered> AllOrdersOrdered ()
        {
            List<OrderOrdered> ordersOrdered = new List<OrderOrdered>();
            DataTable dt = DAL.FarmerDal.OrderedFromFarmer(userID);
            foreach (DataRow dr in dt.Rows)
            {
                ordersOrdered.Add(new OrderOrdered(dr));
            }
            List<OrderOrdered> sortedByNotSent = ordersOrdered.OrderByDescending
                (o => o.DateOrderSent).ToList(); //Places the orders that were not handled first. Orders that were 
                                                                    //not sent yet have their DateOrderSent set to DateTime.MinValue.
            return sortedByNotSent;
        }
        /// <summary>
        /// Adds a new sale to the database.
        /// </summary>
        /// <param name="saleID"></param>
        /// <param name="oliveName"></param>
        /// <param name="saleWeight"></param>
        /// <param name="salePrice"></param>
        /// <param name="inStock"></param>
        /// <param name="dateSaleAdded"></param>
        /// <returns></returns>
        public Sale NewSale (int saleID, string oliveName, double saleWeight, double salePrice, int inStock, DateTime dateSaleAdded)
        {
            int orderID = DAL.FarmerDal.NewSale(this.userID ,saleID, saleWeight, salePrice, inStock, dateSaleAdded);
            if (orderID == DAL.DALHelper.WRITEDATA_ERROR) return null;
            return new Sale(this.UserID, orderID, saleID, oliveName, saleWeight, salePrice, inStock, dateSaleAdded);
        }
        /// <summary>
        /// Finds and returns all available Sales (a sale is available if it has any items in stock).
        /// </summary>
        /// <returns></returns>
        public List<Sale> allAvailableSales ()
        {
            DataTable dt = DAL.CompanyDAL.FindAllSales();
            if (dt == null) return null;
            List<Sale> sales = new List<Sale>();
            foreach (DataRow dr in dt.Rows)
            {
                if (int.Parse(dr["InStock"].ToString()) > 0) sales.Add(new Sale(dr)); 
            }
            if (sales.Count < 1) return null;
            return sales;
        }
    }
}
