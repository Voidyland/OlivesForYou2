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
        public Sale saleToUpdate = null;
        /// <summary>
        /// Gets a list of all of the farmers sales and returns a list of all of the olives NOT in any sale, 
        /// aka olives available for new sales.
        /// </summary>
        /// <param name="allFarmerSales"></param>
        /// <returns></returns>
        public List<ListItem> allAvailableOliveTypes (List<Sale> allFarmerSales)
        {
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
            return allListItems;
        }
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
            List<Sale> allFarmerSales = ((User)Session["User"]).AllSales(false);
            if (allFarmerSales != null)
            {
                BindSales(allFarmerSales);
            }
            else
            {
                Sales.Visible = false;
                noSale.Visible = true;
            }
            
            int saleID = -1;
            if (int.TryParse(Request.QueryString["SI"], out saleID)) 
            {
                List<ListItem> allListItems = allAvailableOliveTypes(allFarmerSales); //Gets all available olives
                //Find the order that needs to be updated.
                //saleToUpdate is null as a base.
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
                //Add the olive type of the sale that needs updating to the olive types.
                //Places the olive at the start of the dropdown to make it the defult value.
                allListItems.Insert(0,new ListItem(saleToUpdate.OliveName, saleToUpdate.OliveID.ToString()));
                //Bind the olive type dropdown list.
                if (ddlUpdateOliveTypes.DataSource == null) //Problem - This is null even in the case of trying                                                                 //to update, making it imposible to update the olive type.                
                {
                    ddlUpdateOliveTypes.DataSource = allListItems;
                    ddlUpdateOliveTypes.DataValueField = "Value";
                    ddlUpdateOliveTypes.DataTextField = "Text";
                    ddlUpdateOliveTypes.DataBind();
                }
                //Make the defult values the previus values, unless an update has accured.
                if (txtUpdateWeight.Text == "")
                txtUpdateWeight.Text = saleToUpdate.SaleWeight.ToString();
                if (txtUpdatePrice.Text == "")
                txtUpdatePrice.Text = saleToUpdate.SalePrice.ToString();                
                if (txtUpdateStock.Text == "")
                txtUpdateStock.Text = saleToUpdate.InStock.ToString();


            }
            else 
            {
                if (!Page.IsPostBack)
                {
                    List<ListItem> allListItems = allAvailableOliveTypes(allFarmerSales);
                    ddlOliveTypes.DataSource = allListItems;
                    ddlOliveTypes.DataValueField = "Value";
                    ddlOliveTypes.DataTextField = "Text";
                    ddlOliveTypes.DataBind();
                    lblError.Text = Request.QueryString["error"];
                }
                //else 
                //{

                //}
                pnlAddOrder.Visible = true;
                pnlUpdateOrder.Visible = false;
            }
        }
        
        protected void btnNewSale_Click(object sender, EventArgs e)
        {
            Sale newSale =  ((User)Session["User"]).NewSale(int.Parse(ddlOliveTypes.SelectedValue), ddlOliveTypes.Text, 
                double.Parse(txtWeight.Text), double.Parse(txtPrice.Text), int.Parse(txtStock.Text), DateTime.UtcNow);
            if (newSale == null) Response.Redirect("FarmerOrders.aspx?error=Something went wrong...");
            else Response.Redirect("FarmerOrders.aspx?error=Success!");
        }

        protected void ordersForSale_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                
                int index = int.Parse(e.CommandArgument.ToString());
                List<Sale> allFarmerSales = ((User)Session["User"]).AllSales(false);
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
                } //Note to self: add pop-up that asks the user if he REALY wants to delete the sale.
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
            try
            {
                int oliveID = int.Parse(ddlUpdateOliveTypes.SelectedValue);
                string oliveName = ddlUpdateOliveTypes.Text;
                double oliveWeight = double.Parse(txtUpdateWeight.Text);
                double olivePrice = double.Parse(txtUpdatePrice.Text);
                int inStock = int.Parse(txtUpdateStock.Text);
                saleToUpdate.UpdateThis(oliveID, oliveName, oliveWeight, olivePrice, inStock);
                lblError.Text = "Update successfull!";
                Response.Redirect("FarmerOrders.aspx");
            }
            catch (Exception exeption)
            {
                lblError.Text = "something went wrong! in particulate: " + exeption.Message;
            }
        }
        /// <summary>
        /// Shows the orders ordered. Not tested, so probably doesnt work. If I had to guss it has something to
        /// do with the fact that the datasource is summoned after pageload but who knows if thats okey.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPastOrPresent_Click(object sender, EventArgs e)
        {
            gvOrdersOrdered.Visible = true;
            Sales.Visible = false;
            List<OrderOrdered> allOrdersOrdered = ((User)Session["User"]).AllOrdersOrdered();
            gvOrdersOrdered.DataSource = allOrdersOrdered;
            gvOrdersOrdered.DataBind();

        }
    }
}