using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;

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
        private int userType;
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
        //public List<Order> AllOrders ()
        //{
        //    List<Order> orders = new List<Order>();
            
        //}
        
    }
}
