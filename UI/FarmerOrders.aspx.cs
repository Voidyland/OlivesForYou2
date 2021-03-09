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
        //All of those are just for convinience. They repet in diffrent methods so just for consistensy I gave them a permenet name.
        public Sale saleToUpdate = null;

        private List<OrderOrdered> allOrdersOrdered = null;

        private List<OrderOrdered> ordersToView = null;

        private int ddlOliveID = 0;

        private OrderOrdered orderToConfirmOrDeny = null;

        /// <summary>
        /// Gets a list of all of the farmers sales and returns a list of all of the olives NOT in any sale, 
        /// aka olives available for new sales.
        /// </summary>
        /// <param name="allFarmerSales"></param>
        /// <returns></returns>
        public List<ListItem> allAvailableOliveTypes(List<Sale> allFarmerSales)
        {
            List<Olive> allOliveTypes = BL.General.AllOlives();
            List<ListItem> allListItems = new List<ListItem>();
            bool inSale = false;
            if (allFarmerSales == null || allFarmerSales.Count == 0)
            {
                foreach (Olive olive in allOliveTypes)
                {
                    allListItems.Add(new ListItem(olive.OliveName, olive.OliveID.ToString()));
                }
                return allListItems;
            }
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
                lblSales.Visible = false;
                Sales.Visible = false;
                noSale.Visible = true;
            }

            int saleID = -1;
            if (int.TryParse(Request.QueryString["SI"], out saleID))
            {
                lblSales.Visible = false;
                Sales.Visible = false;
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
                lblSaleToUpdate.Text = saleToUpdate.ToString();
                allListItems.Insert(0, new ListItem(saleToUpdate.OliveName, saleToUpdate.OliveID.ToString()));
                if (txtUpdatePrice.Text != "" && txtUpdateWeight.Text != "" && txtUpdateStock.Text != "")
                {
                    ddlOliveID = int.Parse(ddlUpdateOliveTypes.SelectedValue);
                    int indexToRemove = ddlUpdateOliveTypes.SelectedIndex;
                    allListItems.RemoveAt(indexToRemove);
                }
                ddlUpdateOliveTypes.DataSource = allListItems;
                ddlUpdateOliveTypes.DataValueField = "Value";
                ddlUpdateOliveTypes.DataTextField = "Text";
                ddlUpdateOliveTypes.DataBind();

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
                List<ListItem> allListItems = allAvailableOliveTypes(allFarmerSales);
                if (txtPrice.Text != "" && txtWeight.Text != "" && txtStock.Text != "")
                {
                    ddlOliveID = int.Parse(ddlOliveTypes.SelectedValue);
                    int indexToRemove = ddlOliveTypes.SelectedIndex;
                    allListItems.RemoveAt(indexToRemove);
                }
                ddlOliveTypes.DataSource = allListItems;
                ddlOliveTypes.DataValueField = "Value";
                ddlOliveTypes.DataTextField = "Text";
                ddlOliveTypes.DataBind();
                lblError.Text = Request.QueryString["error"];
                pnlUpdateOrder.Visible = false;
                if (gvOrdersOrdered.Visible)
                {
                    lblOrdersOrdered.Visible = true;
                    pnlAddOrder.Visible = false;
                    lblSales.Visible = false;
                    Sales.Visible = false;
                    noSale.Visible = false; //Error messege only relevent when trying to present the sales gridview.
                    ordersToView = (List<OrderOrdered>)Session["ordersToView"];
                    if (ordersToView == null)
                    {
                        allOrdersOrdered = ((User)Session["User"]).AllOrdersOrdered();
                        gvOrdersOrdered.DataSource = allOrdersOrdered;
                    }
                    else
                    {
                        gvOrdersOrdered.DataSource = ordersToView;
                    }
                    gvOrdersOrdered.DataBind();
                }

            }

        }

        protected void btnNewSale_Click(object sender, EventArgs e)
        {
            Sale newSale = ((User)Session["User"]).NewSale(ddlOliveID, ddlOliveTypes.Text,
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
                    increaseOrDelete.Text = $"The following order has been deleted: " + chosenSale.ToString();
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
            lblSales.Visible = true;
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
            string commandArgument = ((Button)sender).CommandArgument;
            if (commandArgument == btnAddSalePanel.CommandArgument)
            {
                lblOrdersOrdered.Visible = false;
                gvOrdersOrdered.Visible = false;
                Session["ordersFromGivenCompany"] = null;
                lblSales.Visible = false;
                Sales.Visible = false;
                pnlOrderMethod.Visible = false;
                pnlAddOrder.Visible = true;
                pnlFindOne.Visible = false;
            }
            else if (commandArgument == btnViewOrdersOrdered.CommandArgument)
            {
                
                lblSales.Visible = false;
                Sales.Visible = false;
                pnlOrderMethod.Visible = true;
                pnlFindOne.Visible = true;
                pnlAddOrder.Visible = false;
                Session["ordersFromGivenCompany"] = null;
                allOrdersOrdered = ((User)Session["User"]).AllOrdersOrdered();
                if (allOrdersOrdered == null || allOrdersOrdered.Count == 0)
                {
                    lblError.Text = "It seems you have no orders ordered from you.";
                }
                else
                {
                    lblError.Text = "";
                    lblOrdersOrdered.Visible = true;
                    gvOrdersOrdered.Visible = true;
                    Session["allOrdersOrdered"] = allOrdersOrdered;
                    DDLNamesDataBind();
                    gvOrdersOrdered.DataSource = allOrdersOrdered;
                    gvOrdersOrdered.DataBind();
                }                              
            }
            else
            {
                lblOrdersOrdered.Visible = false;
                gvOrdersOrdered.Visible = false;
                Session["ordersFromGivenCompany"] = null;                
                lblSales.Visible = true;
                Sales.Visible = true;
                pnlOrderMethod.Visible = false;
                pnlAddOrder.Visible = false;
                pnlFindOne.Visible = false;
                BindSales(((User)Session["User"]).AllSales(false));
            }
        }

        protected void gvOrdersOrdered_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex > -1)
            {
                OrderOrdered orderOrdered = (OrderOrdered)e.Row.DataItem;
                if (orderOrdered.DateOrderSent == DateTime.MinValue)
                {
                    e.Row.Cells[7].Text = "Not sent.";
                    e.Row.Cells[8].Text = "Not sent or arrived.";
                }
                else if (orderOrdered.DateOrderArrived == DateTime.MinValue)
                {
                    e.Row.Cells[8].Text = "Not arrived.";
                    e.Row.Cells[9].Text = "Order sent succesfully!";
                }
                else
                {
                    e.Row.Cells[9].Text = "Order sent succesfully!";
                }
            }
        }
        protected void gvOrdersOrdered_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvOrdersOrdered.PageIndex = e.NewPageIndex;
            ordersToView = (List<OrderOrdered>)Session["ordersToView"];
            if (ordersToView == null)
                gvOrdersOrdered.DataSource = (List<OrderOrdered>)Session["allOrdersOrdered"];
            else
                gvOrdersOrdered.DataSource = ordersToView;
            gvOrdersOrdered.DataBind();
        }
        /// <summary>
        /// Not working
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvOrdersOrdered_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowNum = int.Parse(e.CommandArgument.ToString());
            int index = rowNum + gvOrdersOrdered.PageSize * gvOrdersOrdered.PageIndex;
            List<OrderOrdered> dataSource = null;
            ordersToView = (List<OrderOrdered>)Session["ordersToView"];
            if (ordersToView == null)
                dataSource = (List<OrderOrdered>)Session["allOrdersOrdered"];
            else
                dataSource = ordersToView;
            orderToConfirmOrDeny = dataSource[index];
            Session["OrderToConfirmOrDeny"] = orderToConfirmOrDeny;
            pnlConfirmOrDeny.Visible = true;
            lblOrderToConfirmOrDeny.Text = orderToConfirmOrDeny.ToString();
            //if (!orderToConfirmOrDeny.ConfirmOrder()) ConfirmSentError.Visible = true;
            //else
            //{
            //    ConfirmSentError.Visible = false;
            //    if (ordersFromGivenCompany == null)
            //    {
            //        allOrdersOrdered = ((User)Session["User"]).AllOrdersOrdered();
            //        gvOrdersOrdered.DataSource = allOrdersOrdered;
            //    }
            //    else
            //    {
            //        gvOrdersOrdered.DataSource = ordersFromGivenCompany;
            //    }
            //    gvOrdersOrdered.DataBind();
            //}
        }

        protected void btnReorder_Click(object sender, EventArgs e)
        {
            string commandArgument = ((Button)sender).CommandArgument;
            allOrdersOrdered = (List<OrderOrdered>)Session["allOrdersOrdered"];
            //if (commandArgument == btnOrderByName.CommandArgument)
            //{
            //    List<OrderOrdered> orderedByCompanyX = new List<OrderOrdered>();
            //    //List<OrderOrdered> notOrderedByCompanyX = new List<OrderOrdered>();
            //    string name = ddlFindOrdersFromName.SelectedValue;
            //    foreach (OrderOrdered orderOrdered in allOrdersOrdered)
            //    {
            //        if (orderOrdered.CompanyName == name) orderedByCompanyX.Add(orderOrdered);
            //        //else notOrderedByCompanyX.Add(orderOrdered);
            //    }
            //    orderedByCompanyX = orderedByCompanyX.OrderByDescending(o => o.DateOrderSent).ToList();
            //    //notOrderedByCompanyX = notOrderedByCompanyX.OrderByDescending(o => o.DateOrderSent).ToList();
            //    //foreach (OrderOrdered orderOrdered in notOrderedByCompanyX)
            //    //{
            //    //    orderedByCompanyX.Add(orderOrdered);
            //    //}
            //    ordersFromGivenCompany = orderedByCompanyX;
            //    Session["ordersFromGivenCompany"] = ordersFromGivenCompany;
            //    gvOrdersOrdered.DataSource = ordersFromGivenCompany;
            //    gvOrdersOrdered.DataBind();
            //    lblNameNotFound.Visible = false;                
            //}
            //else
            //{
                //if (commandArgument == btnOrderByUnhandled.CommandArgument)
                //{
                //    if (rblOrderOfOrdering.SelectedIndex == 0)
                //    {
                //        allOrdersOrdered = allOrdersOrdered.OrderByDescending(o => o.DateOrderSent).ToList();
                //    }
                //    else
                //    {
                //        allOrdersOrdered = allOrdersOrdered.OrderBy(o => o.DateOrderSent).ToList();
                //    }
                //}
                if (commandArgument == btnOrderByDateOrdered.CommandArgument)
                {
                    if (rblOrderOfOrdering.SelectedIndex == 0)
                    {
                        allOrdersOrdered = allOrdersOrdered.OrderByDescending(o => o.DateOrderOrdered).ToList();
                    }
                    else
                    {
                        allOrdersOrdered = allOrdersOrdered.OrderBy(o => o.DateOrderOrdered).ToList();
                    }
                }
                //else if (commandArgument == btnOrderByDateRecived.CommandArgument)
                //{
                //    if (rblOrderOfOrdering.SelectedIndex == 0)
                //    {
                //        allOrdersOrdered = allOrdersOrdered.OrderByDescending(o => o.DateOrderArrived).ToList();
                //    }
                //    else
                //    {
                //        allOrdersOrdered = allOrdersOrdered.OrderBy(o => o.DateOrderArrived).ToList();
                //    }
                //}
                ordersToView = null;
                Session["ordersToView"] = null;
                Session["allOrdersOrdered"] = allOrdersOrdered;
                gvOrdersOrdered.DataSource = allOrdersOrdered;
                gvOrdersOrdered.DataBind();
            //}
        }
        /// <summary>
        /// Adds the datasource for the DropDownList used for finding a certain company.
        /// </summary>
        private void DDLNamesDataBind()
        {

            allOrdersOrdered = (List<OrderOrdered>)Session["allOrdersOrdered"];
            List<string> allCompanyNames = new List<string>();
            foreach (OrderOrdered oo in allOrdersOrdered) 
            {
                allCompanyNames.Add(oo.CompanyName);
            }
            ddlFindOrdersFromName.DataSource = allCompanyNames;
            ddlFindOrdersFromName.DataBind();
            
        }

        protected void btnFindOrders_Click(object sender, EventArgs e)
        {
            string commandArgument = ((Button)sender).CommandArgument;
            allOrdersOrdered = (List<OrderOrdered>)Session["allOrdersOrdered"];
            ordersToView = new List<OrderOrdered>();
            if (commandArgument == btnFindOrdersFromName.CommandArgument)
            {
                string companyName = ddlFindOrdersFromName.SelectedValue;                
                foreach (OrderOrdered orderOrdered in allOrdersOrdered)
                {
                    if (orderOrdered.CompanyName == companyName) ordersToView.Add(orderOrdered);
                }
                //ordersFromGivenCompany = ordersFromGivenCompany.OrderByDescending(o => o.DateOrderSent).ToList(); 
                //Session["ordersFromGivenCompany"] = ordersFromGivenCompany;
                //gvOrdersOrdered.DataSource = ordersFromGivenCompany;
                //gvOrdersOrdered.DataBind();
            }
            else if (commandArgument == btnFindUnsentOrders.CommandArgument)
            {
                foreach (OrderOrdered orderOrdered in allOrdersOrdered)
                {
                    if (orderOrdered.DateOrderSent == DateTime.MinValue) ordersToView.Add(orderOrdered);
                }                
            }
            else
            {
                foreach (OrderOrdered orderOrdered in allOrdersOrdered)
                {
                    if (orderOrdered.DateOrderArrived == DateTime.MinValue) ordersToView.Add(orderOrdered);
                }
            }
            Session["ordersToView"] = ordersToView;
            gvOrdersOrdered.DataSource = ordersToView;
            gvOrdersOrdered.DataBind();
        }

        protected void btnConfirmOrDeny_Click(object sender, EventArgs e)
        {
            string commandArgument = ((Button)sender).CommandArgument;
            orderToConfirmOrDeny = (OrderOrdered)Session["OrderToConfirmOrDeny"];
            if (orderToConfirmOrDeny == null)
            {
                lblError.Text = "An error has accoured. Perhaps you waited to long to click the button? Try pressing the button in the gridview again.";
                lblGeneralSuccess.Text = "";
                btnConfirmSent.Visible = false;
                btnDenySending.Visible = false;
            }
            else if (commandArgument == btnConfirmSent.CommandArgument)
            {
                bool flag = orderToConfirmOrDeny.ConfirmOrder();
                if (!flag) 
                { 
                    lblError.Text = "An error has accoured. Please try again at a later time.";
                    lblGeneralSuccess.Text = "";
                }
                else
                {
                    lblGeneralSuccess.Text = "Confirmed order was sent successfully!";
                    lblError.Text = "";
                }
            }
            else //if (commandArgument == btnDenySending.CommandArgument) commented because currently unessesery, if I were to add more buttons then it would be neccesery.
            {
                if (!orderToConfirmOrDeny.DeleteOrder())
                {
                    lblError.Text = "An error has accoured. Please try again at a later date.";
                    lblGeneralSuccess.Text = "";
                }
                else
                {
                    lblGeneralSuccess.Text = "Order deleted succussfully!";
                    lblError.Text = "";                    
                }                
            }
        }
    }
}