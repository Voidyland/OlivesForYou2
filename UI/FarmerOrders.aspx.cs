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
            List<Order> allFarmerOrders = ((User)Session["User"]).AllOrdersForSale();
            ordersForSale.DataSource = allFarmerOrders;
            ordersForSale.AutoGenerateColumns = false;
            ordersForSale.DataBind();
            List<Olive> allOliveTypes = BL.General.AllOlives();
            List<ListItem> allListItems = new List<ListItem>();
            foreach (Olive olive in allOliveTypes)
            {
                allListItems.Add(new ListItem(olive.OliveName, olive.OliveID.ToString()));
            }
            ddlOliveTypes.DataSource = allListItems;
            ddlOliveTypes.DataValueField = "Value";
            ddlOliveTypes.DataTextField = "Text";
            ddlOliveTypes.DataBind();
        }
        
        protected void btnNewOrder_Click(object sender, EventArgs e)
        {
            Order newOrder =  ((User)Session["User"]).NewOrderForSale(int.Parse(ddlOliveTypes.SelectedValue), ddlOliveTypes.Text, 
                double.Parse(txtWeight.Text), double.Parse(txtPrice.Text), int.Parse(txtStock.Text));
            if (newOrder == null) lblError.Text = "Something went wrong...";            
            else lblError.Text = "Great! Everything worked!";
        }
    }
}