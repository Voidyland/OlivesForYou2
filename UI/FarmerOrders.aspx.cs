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
            if (allFarmerSales != null)
            {
                BindSales(allFarmerSales);
            }
            else
            {
                Sales.Visible = false;
                noSale.Visible = true;
            }
            List<Olive> allOliveTypes = BL.General.AllOlives();
            List<ListItem> allListItems = new List<ListItem>();
            bool inSale = false;
            foreach (Olive olive in allOliveTypes)
            {
                foreach (Sale sale in allFarmerSales)
                {
                    if (sale.OliveID == olive.OliveID)
                    {
                        inSale = true;
                        break;
                    }
                    inSale = false;
                }
                if (!inSale)
                {
                    allListItems.Add(new ListItem(olive.OliveName, olive.OliveID.ToString()));
                }
            }
            int saleID = -1;
            if (int.TryParse(Request.QueryString["SI"], out saleID)) 
            {
                //Find the order that needs to be updated.
                Sale saleToUpdate = null;
                foreach (Sale sale in allFarmerSales)
                {
                    if (sale.SaleID == saleID)
                    {
                        saleToUpdate = sale;
                        break;
                    }
                }
                //Make the update panle visible and the order panel invisible.
                pnlAddOrder.Visible = false;
                pnlUpdateOrder.Visible = true;
                //Bind the olive type dropdown list.
                ddlUpdateOliveTypes.DataSource = allListItems;
                ddlUpdateOliveTypes.DataValueField = "Value";
                ddlUpdateOliveTypes.DataTextField = "Text";
                ddlUpdateOliveTypes.DataBind();
                //Make the defult values the previus values.
                txtUpdateWeight.Text = saleToUpdate.SalePrice.ToString();
                txtUpdatePrice.Text = saleToUpdate.SalePrice.ToString();                
                txtUpdateStock.Text = saleToUpdate.InStock.ToString();


            }
            else
            {
                pnlAddOrder.Visible = true;
                pnlUpdateOrder.Visible = false;
                ddlOliveTypes.DataSource = allListItems;
                ddlOliveTypes.DataValueField = "Value";
                ddlOliveTypes.DataTextField = "Text";
                ddlOliveTypes.DataBind();
            }
        }
        
        protected void btnNewSale_Click(object sender, EventArgs e)
        {
            Sale newSale =  ((User)Session["User"]).NewSale(int.Parse(ddlOliveTypes.SelectedValue), ddlOliveTypes.Text, 
                double.Parse(txtWeight.Text), double.Parse(txtPrice.Text), int.Parse(txtStock.Text));
            if (newSale == null) lblError.Text = "Something went wrong...";            
            else lblError.Text = "Great! Everything worked!";
            if (noSale.Visible == true)
            {
                List<Sale> allFarmerSales = ((User)Session["User"]).AllSales();
                BindSales(allFarmerSales);
            }
        }

        protected void ordersForSale_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                
                int index = int.Parse(e.CommandArgument.ToString());
                List<Sale> allFarmerSales = ((User)Session["User"]).AllSales();
                Sale chosenSale = allFarmerSales[index];
                if (e.CommandName == "change")
                {
                    Response.Redirect("FarmerOrders.aspx?SI=" + chosenSale.SaleID.ToString()); //SI = SaleIndex
                    return;
                }
                else
                {
                    Sales.DeleteRow(index);
                    allFarmerSales.Remove(chosenSale);
                    chosenSale.DeleteThis();
                    BindSales(allFarmerSales);
                    increaseOrDelete.Text = $"The following order has been deleted: Olive name = " +
                        $"{chosenSale.OliveName}, sale weight = {chosenSale.SaleWeight}, sale price = " +
                        $"{chosenSale.SalePrice}, stock at the time of deletion: {chosenSale.InStock}";
                    
                }
            }
            catch (Exception exeption)
            {
                increaseOrDelete.Text = "An error occurred.";
            }
        }

        protected void Sales_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {

        }

        protected void Sales_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
        public void BindSales(List<Sale> allFarmerSales)
        {
            Sales.DataSource = allFarmerSales;
            Sales.AutoGenerateColumns = false;
            Sales.DataBind();
            Sales.Visible = true;
            noSale.Visible = false;
        }

        protected void btnUpdateSale_Click(object sender, EventArgs e)
        {

        }
    }
}