using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;
namespace UI
{
    public partial class FarmerOrders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] == null)
            {
                Response.Redirect("MainPage.aspx");
            }
            if (((User)Session["User"]).UserType != 2)
            {
                Response.Redirect("MainPage.aspx");
            }            
        }
        protected void Page_OnPreRender(object sender, EventArgs e)
        {
            List<Order> allFarmerOrders = ((User)Session["User"]).AllOrdersForSale();
            ordersForSale.DataSource = allFarmerOrders;
            ordersForSale.DataBind();
        }

    }
}