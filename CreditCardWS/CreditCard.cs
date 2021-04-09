using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;
namespace CreditCardWS
{
    public class CreditCard
    {
        private string cardNumber;
        private int cardCCV;
        private int cardExpirationMonth;
        private int cardExpirationYear;
        private int ownerID;
        private string ownerFirstName;
        private string ownerLastName;
        private double cardBalance;

        public CreditCard ()
        {
            this.cardNumber = "";
            this.cardCCV = -1;
            this.cardExpirationMonth = -1;
            this.cardExpirationYear = -1;
            this.ownerID = -1;
            this.ownerFirstName = "";
            this.ownerLastName = "";
            this.cardBalance = -1;
        }
        public CreditCard(string cardNumber, int cardCCV, int cardExpirationMonth, int cardExpirationYear, int ownerID, string ownerFirstName, string ownerLastName, double cardBalance)
        {
            this.cardNumber = cardNumber;
            this.cardCCV = cardCCV;
            this.cardExpirationMonth = cardExpirationMonth;
            this.cardExpirationYear = cardExpirationYear;
            this.ownerID = ownerID;
            this.ownerFirstName = ownerFirstName;
            this.ownerLastName = ownerLastName;
            this.cardBalance = cardBalance;
        }
        public CreditCard(DataRow dr)
        {
            this.cardNumber = dr["CardNumber"].ToString();
            this.cardCCV = int.Parse(dr["CardCCV"].ToString());
            this.cardExpirationMonth = int.Parse(dr["CardExpirationMonth"].ToString());
            this.cardExpirationYear = int.Parse(dr["CardExpirationYear"].ToString());
            this.ownerID = int.Parse(dr["OwnerID"].ToString());
            this.ownerFirstName = dr["OwnerFirstName"].ToString();
            this.ownerLastName = dr["OwnerLastName"].ToString();
            this.cardBalance = int.Parse(dr["CardBalance"].ToString());
        }
        public string CardNumber
        {
            get
            {
                return cardNumber;
            }
        }
        public int CardCCV
        {
            get
            {
                return cardCCV;
            }
        }
        public int CardExpirationMonth
{
            get
            {
                return cardExpirationMonth;
            }
        }
        public int CardExpirationYear
        {
            get
            {
                return cardExpirationYear;
            }
        }
        public int OwnerID
        {
            get
            {
                return ownerID;
            }
        }
        public string OwnerFirstName
        {
            get
            {
                return ownerFirstName;
            }
        }
        public string OwnerLastName
        {
            get
            {
                return ownerLastName;
            }
        }
        public double CardBalance
        {
            get
            {
                return cardBalance;
            }
        }
        public void IncreseBalance (double increaseBy)
        {
            this.cardBalance += increaseBy;
        }
    }
}