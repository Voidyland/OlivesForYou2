using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;

namespace BL
{
    public class User
    {
        private int userID;
        private string userName;
        private string email;
        private string pass;
        private bool isAdmin;
        private bool isFarmer;
        private bool certefiedUser;
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
        public bool IsAdmin
        {
            get
            {
                return isAdmin;
            }
            set
            {
                isAdmin = value;
            }
        }
        public bool IsFarmer
        {
            get
            {
                return isFarmer;
            }
            set
            {
                isFarmer = value;
            }
        }
        public bool CertefiedUser
        {
            get
            {
                return certefiedUser;
            }
            set
            {
                certefiedUser = value;
            }
        }
        public User(DataRow dr)
        {
            this.UserID = int.Parse(dr["UserID"].ToString());
            this.UserName = dr["UserName"].ToString();
            this.Email = dr["Email"].ToString();
            this.Pass = dr["Pass"].ToString();
            this.IsAdmin = (bool)dr["IsAdmin"];
            this.IsFarmer = (bool)dr["IsFarmer"];
            this.CertefiedUser = (bool)dr["CertefiedUser"];
        }
    }
}
