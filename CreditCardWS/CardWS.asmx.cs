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
        /// <summary>
        /// Creates a new credit card.
        /// </summary>
        /// <param name="ownerFirstName"></param>
        /// <param name="ownerLastName"></param>
        /// <returns>The new cards number</returns>
        [WebMethod]
        public string CreateNewCard (int ownerID, string ownerFirstName, string ownerLastName)
        {
            return "";
        }
    }
}
