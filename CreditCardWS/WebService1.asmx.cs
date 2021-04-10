using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.OleDb;
namespace CreditCardWS
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        public const int CARD_LENGTH = 8;
        public Random rnd = new Random();
        /// <summary>
        /// Creates a new credit card.
        /// </summary>
        /// <param name="ownerEmail"></param>
        /// <param name="ownerFirstName"></param>
        /// <param name="ownerLastName"></param>
        /// <returns>The new cards number</returns>
        [WebMethod]
        public string CreateNewCard (string ownerEmail, string ownerFirstName, string ownerLastName)
        {
            string cardNumber = CreateCardNumber();
            if (cardNumber == "") return "";
            int expirationYear = DateTime.Now.Year % 100 + 5; //Expires five years from now.
            bool success = DAL.CreateCard(cardNumber, rnd.Next(100, 1000), rnd.Next(1, 13), expirationYear, ownerEmail, ownerFirstName, ownerLastName, 0);
            if (success) return cardNumber;
            return "";
        }
        /// <summary>
        /// Returns all transactions by a given card in a given month and year.
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        [WebMethod]
        public List<Transaction> allCardTransactionInMonth(string cardNumber, int month, int year)
        {
            if (cardNumber.Length != CARD_LENGTH || month < 1 || month > 12) return null;
            DataTable dt = DAL.allTransactions();
            List<Transaction> allCardTransactionsInMonth = new List<Transaction>();
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["SendingCard"].ToString() == cardNumber || dr["RecivingCard"].ToString() == cardNumber)
                {
                    DateTime transactionDate = (DateTime)dr["TransactionDate"];
                    if (transactionDate.Year == year && transactionDate.Month == month) allCardTransactionsInMonth.Add(new Transaction(dr));
                }
            }
            if (allCardTransactionsInMonth.Count == 0) return null;
            return allCardTransactionsInMonth;
        }
        /// <summary>
        /// Returns all transactions by a given card.
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <returns></returns>
        [WebMethod]
        public List<Transaction> allCardTransaction(string cardNumber)
        {
            if (cardNumber.Length != CARD_LENGTH) return null;
            DataTable dt = DAL.allTransactions();
            List<Transaction> allCardTransactionsInMonth = new List<Transaction>();
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["SendingCard"].ToString() == cardNumber || dr["RecivingCard"].ToString() == cardNumber)
                {
                    allCardTransactionsInMonth.Add(new Transaction(dr));
                }
            }
            if (allCardTransactionsInMonth.Count == 0) return null;
            return allCardTransactionsInMonth;
        }
        /// <summary>
        /// Verifys that a card with all those details exist.
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <param name="cardCCV"></param>
        /// <param name="cardExpirationMonth"></param>
        /// <param name="cardExpirationYear"></param>
        /// <returns></returns>
        [WebMethod]
        public bool VerifyDetails(string cardNumber, int cardCCV, int cardExpirationMonth, int cardExpirationYear)
        {
            DataRow dr = DAL.FindCard(cardNumber);
            if (dr == null) return false;
            CreditCard card = new CreditCard(dr);
            if (card.CardCCV != cardCCV || card.CardExpirationMonth != cardExpirationMonth || card.CardExpirationYear != cardExpirationYear)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Verifys a cards details vs given details.
        /// </summary>
        /// <param name="cardCCV"></param>
        /// <param name="cardExpirationMonth"></param>
        /// <param name="cardExpirationYear"></param>
        /// <param name="card"></param>
        /// <returns></returns>
        private bool VerifyDetails(int cardCCV, int cardExpirationMonth, int cardExpirationYear, CreditCard card)
        {
            if (card.CardCCV != cardCCV || card.CardExpirationMonth != cardExpirationMonth || card.CardExpirationYear != cardExpirationYear)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Pays someone and returns the transaction.
        /// </summary>
        /// <param name="payingCardNumber"></param>
        /// <param name="payingCardCCV"></param>
        /// <param name="payingCardExpirationMonth"></param>
        /// <param name="payingCardExpirationYear"></param>
        /// <param name="recivingCard"></param>
        /// <returns></returns>
        [WebMethod]
        public Transaction PaySomeone (string payingCardNumber, int payingCardCCV, int payingCardExpirationMonth, int payingCardExpirationYear, string recivingCard, double payingHowMuch)
        {
            DataRow dr = DAL.FindCard(payingCardNumber);
            if (dr == null) return null;
            CreditCard card = new CreditCard(dr);
            if (!VerifyDetails(payingCardCCV, payingCardExpirationMonth, payingCardExpirationYear, card)) return null;
            DataRow dr2 = DAL.PaySomeone(payingCardNumber, recivingCard, payingHowMuch);
            if (dr2 == null) return null;
            return new Transaction(dr2);
        }
        /// <summary>
        /// Creates a random card number until succesfully making a unique one.
        /// </summary>
        /// <returns></returns>
        private string CreateCardNumber()
        {
            try
            {                
                string cardNumber = "";
                
                for (int i = 0; i < CARD_LENGTH; i++)
                {
                    cardNumber += rnd.Next(1, 10);
                }
                if (DAL.IsTaken(cardNumber)) return CreateCardNumber(); //If the card exists try again.
                return cardNumber;
            }
            catch (Exception e)
            {
                return "";
            }
        }
        /// <summary>
        /// Returns all credit cards.
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public List<CreditCard> AllCreditCards ()
        {
            DataTable dt = DAL.AllCreditCards();
            if (dt == null) return null;
            List<CreditCard> cards = new List<CreditCard>();
            foreach (DataRow dr in dt.Rows)
            {
                cards.Add(new CreditCard(dr));
            }
            return cards;
        }
    }
}
