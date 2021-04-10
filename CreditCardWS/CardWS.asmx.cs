using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

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
    public class CardWS : System.Web.Services.WebService
    {
        public Random rnd = new Random();
        /// <summary>
        /// Creates a new credit card.
        /// </summary>
        /// <param name="ownerFirstName"></param>
        /// <param name="ownerLastName"></param>
        /// <returns>The new cards number</returns>
        [WebMethod]
        public string CreateNewCard (int ownerID, string ownerFirstName, string ownerLastName)
        {
            string cardNumber = CreateCardNumber();
            if (cardNumber == "") return "";
            int expirationYear = DateTime.Now.Year % 100 + 5; //Expires five years from now.
            bool success = DAL.CreateCard(cardNumber, rnd.Next(100, 1000), rnd.Next(1, 13), expirationYear, ownerID, ownerFirstName, ownerLastName, 0);
            if (success) return cardNumber;
            return "";
        }
        private string CreateCardNumber()
        {
            try
            {                
                string cardNumber = "";
                const int CARD_LENGTH = 9;
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
    }
}
