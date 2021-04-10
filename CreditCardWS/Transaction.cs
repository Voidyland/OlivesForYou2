using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;
namespace CreditCardWS
{
    public class Transaction
    {
        private int transactionID;
        private string sendingCard;
        private string recivingCard;
        private double transactionAmount;
        private DateTime transactionDate;
        public Transaction ()
        {

        }
        public Transaction(int transactionID, string sendingCard, string recivingCard, double transactionAmount, DateTime transactionDate)
        {
            this.transactionID = transactionID;
            this.sendingCard = sendingCard;
            this.recivingCard = recivingCard;
            this.transactionAmount = transactionAmount;
            this.transactionDate = transactionDate;
        }
        public Transaction (DataRow dr)
        {
            this.transactionID = int.Parse(dr["TransactionID"].ToString());
            this.sendingCard = dr["SendingCard"].ToString();
            this.recivingCard = dr["RecivingCard"].ToString();
            this.transactionAmount = double.Parse(dr["TransactionAmount"].ToString());
            this.transactionDate = (DateTime)dr["TransactionDate"];
        }
        public int TransactionID { get => transactionID;}
        public string SendingCard { get => sendingCard;}
        public string RecivingCard { get => recivingCard;}
        public double TransactionAmount { get => transactionAmount;}
        public DateTime TransactionDate { get => transactionDate;}
        
        
    }
}