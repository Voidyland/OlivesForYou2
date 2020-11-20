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
            List<Sale> allFarmerSales = ((User)Session["User"]).AllSales();
            Sales.DataSource = allFarmerSales;
            Sales.AutoGenerateColumns = false;
            Sales.DataBind();
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
        
        protected void btnNewSale_Click(object sender, EventArgs e)
        {
            Sale newSale =  ((User)Session["User"]).NewSale(int.Parse(ddlOliveTypes.SelectedValue), ddlOliveTypes.Text, 
                double.Parse(txtWeight.Text), double.Parse(txtPrice.Text), int.Parse(txtStock.Text));
            if (newSale == null) lblError.Text = "Something went wrong...";            
            else lblError.Text = "Great! Everything worked!";
        }

        protected void ordersForSale_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                Sale chosenSale = (Sale)Sales.SelectedRow.DataItem;
                if (e.CommandName == "increase")
                {
                    // Maybe change increase to just edit sale? 
                }
                else
                {
                    chosenSale.DeleteThis();
                    increaseOrDelete.Text = $"The following order has been deleted: Olive name = " +
                        $"{chosenSale.OliveName}, sale weight = {chosenSale.SaleWeight}, sale price = " +
                        $"{chosenSale.SalePrice}, stock at the time of deletion: {chosenSale.InStock}";
                }
            }
            catch
            {

            }
        }
    }
}