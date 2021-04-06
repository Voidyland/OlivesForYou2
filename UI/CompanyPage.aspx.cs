using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;
namespace UI
{
    public partial class CompanyPage : System.Web.UI.Page
    {
        private List<Sale> allAvailableSales = null;
        private Sale saleBeingBought = null;
        private int salesBought = -1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Page.Request.QueryString["cart"] == "All cart orderd")
            {
                pnlFromCart.Visible = true;
            }
            if (!Page.IsPostBack)
            {
                if (Session["User"] == null)
                {
                    Response.Redirect("MainPage.aspx");
                }
                if (((User)Session["User"]).UserType != 3)
                {
                    Response.Redirect("MainPage.aspx");
                }
                LoadAvailableSales();
            }
        }
        private void LoadAvailableSales ()
        {
            allAvailableSales = ((User)Session["User"]).AllAvailableSales();
            Session["allAvailableSales"] = allAvailableSales;
            if (allAvailableSales == null)
            {
                gvAvailableSales.Visible = false;
                lblAvailableSales.Visible = false;
                lblNoAvailaleSales.Visible = true;
            }
            else
            {
                lblNoAvailaleSales.Visible = false;
                gvAvailableSales.Visible = true;
                lblAvailableSales.Visible = true;
                gvAvailableSales.DataSource = allAvailableSales;
                gvAvailableSales.DataBind();
            }
            if (Session["saleBeingBought"] != null)
            {
                saleBeingBought = (Sale)Session["saleBeingBought"];
                salesBought = (int)Session["salesBought"];
                lblSaleBought.Text = $"Last sale bought was: {saleBeingBought}. {salesBought} stocks were bought.";
            }
        }

        protected void gvAvailableSales_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string commandArgument = ((Button)sender).CommandArgument;
            pnlAvailableSales.Visible = false;
            allAvailableSales = (List<Sale>)Session["allAvailableSales"];
            saleBeingBought = allAvailableSales[int.Parse(e.CommandArgument.ToString())];
            Session["saleBeingBought"] = saleBeingBought;
            if (commandArgument == "orderNow")   //Command argument for the order now button in the available sales grid view.
            {                
                pnlOrderSale.Visible = true;                
                lblSaleDetails.Text = saleBeingBought.ToString();
                CreateMonthsDDL();
                CreateYearsDDL();
                CreateStocksDDL(saleBeingBought.InStock);
            }
            else
            {
                pnlAddToCart.Visible = true;
                lblCartSaleDetails.Text = saleBeingBought.ToString();
                CreateCartStocksBeingBoughtDDL(saleBeingBought.InStock);
            }
        }

        private void CreateMonthsDDL ()
        {
            List<int> months = new List<int>();
            for (int i = 1; i <=12; i++)
            {
                months.Add(i);
            }
            ddlMonths.DataSource = months;
            ddlMonths.DataBind();
        }
        private void CreateYearsDDL ()
        {
            List<int> years = new List<int>();
            const int YEARS_AHEAD = 30;
            int thisYear = DateTime.Now.Year;
            for (int i = thisYear; i <= thisYear + YEARS_AHEAD; i++)
            {
                years.Add(i);
            }
            ddlYears.DataSource = years;
            ddlYears.DataBind();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stockNumber">recives the number of stocks available.</param>
        private void CreateStocksDDL (int stockNumber)
        {
            List<int> stocks = new List<int>();
            for (int i = 1; i <= stockNumber; i++)
            {
                stocks.Add(i);
            }
            ddlStockBought.DataSource = stocks;
            ddlStockBought.DataBind();
        }
        private void CreateCartStocksBeingBoughtDDL(int stockNumber) //might want to merge this and CreateStocksDDL
        {
            List<int> stocks = new List<int>();
            for (int i = 1; i <= stockNumber; i++)
            {
                stocks.Add(i);
            }
            ddlCartStocksBeingBought.DataSource = stocks;
            ddlCartStocksBeingBought.DataBind();
        }

        protected void btnOrder_Click(object sender, EventArgs e)
        {
            if (true) //placeholder for thec credit card check
            {
                if (Session["saleBeingBought"] != null) 
                {
                    saleBeingBought = (Sale)Session["saleBeingBought"];
                    int userID = ((User)Session["User"]).UserID;                    
                    salesBought = int.Parse(ddlStockBought.SelectedValue);
                    Session["salesBought"] = salesBought;
                    if (!saleBeingBought.CreateNewOrder(userID, salesBought)) 
                    {
                        lblOrderFailed.Visible = true;                    
                    }
                    else
                    {
                        pnlAvailableSales.Visible = true;
                        pnlOrderSale.Visible = false;
                        LoadAvailableSales();
                    }
                }
            }
        }
        private void LoadPreviousOrders ()
        {
            User user = (User)Session["User"];
            List<OrderOrdered> allPreviousOrders = user.AllPreviousOrders();
            if (allPreviousOrders == null)
            {
                gvPreviousOrders.Visible = false;
                lblPreviousOrders.Visible = false;
                lblNoPrevOrders.Visible = true;
            }
            else
            {
                gvPreviousOrders.DataSource = allPreviousOrders;
                gvPreviousOrders.DataBind();
                gvPreviousOrders.Visible = true;
                lblPreviousOrders.Visible = true;
                lblNoPrevOrders.Visible = false;
            }
            
        }
        protected void btnChangePNL_Click(object sender, EventArgs e)
        {
            string comArg = ((Button)sender).CommandArgument;
            switch (comArg)
            {
                case "availSales":
                    pnlAvailableSales.Visible = true;
                    LoadAvailableSales();
                    pnlOrderSale.Visible = false;
                    pnlPreviousOrders.Visible = false;
                    break;
                case "prevOrd":
                    pnlAvailableSales.Visible = false;
                    pnlOrderSale.Visible = false;
                    pnlPreviousOrders.Visible = true;
                    LoadPreviousOrders();
                    break;                    
            }
        }
        
        protected void gvPreviousOrders_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex > -1)
            {
                OrderOrdered orderOrdered = (OrderOrdered)e.Row.DataItem;
                e.Row.Cells[5].Text = orderOrdered.DateOrderOrdered.ToShortDateString();
                if (orderOrdered.DateOrderSent == DateTime.MinValue)
                {
                    e.Row.Cells[6].Text = "Not sent";
                    e.Row.Cells[7].Text = "Not arrived";
                    e.Row.Cells[8].Text = "not arrived.";                    
                }
                else if (orderOrdered.DateOrderArrived == DateTime.MinValue)
                {
                    e.Row.Cells[6].Text = orderOrdered.DateOrderSent.ToShortDateString();
                    e.Row.Cells[7].Text = "Not arrived";
                }
                else
                {
                    e.Row.Cells[6].Text = orderOrdered.DateOrderSent.ToShortDateString();
                    e.Row.Cells[7].Text = orderOrdered.DateOrderArrived.ToShortDateString();
                    e.Row.Cells[8].Text = "Order arrived, no need to confirm";
                }
            }
        }

        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            List<Sale> cart = (List<Sale>)Session["cart"];
            if (cart == null) cart = new List<Sale>();
            Sale addToCart = new Sale((Sale)Session["saleBeingBought"]);
            addToCart.InStock = int.Parse(ddlCartStocksBeingBought.SelectedValue);
            cart.Add(addToCart);
            Session["saleBeingBought"] = addToCart;
            lblInfo.Visible = true;
        }
        /// <summary>
        /// change the date to only show the date not the time.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvAvailableSales_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Sale sale = (Sale)e.Row.DataItem;
            e.Row.Cells[5].Text = sale.DateSaleAdded.ToShortDateString();
        }
    }
}
//if (e.Row.RowIndex > -1)
//{
//    OrderOrdered orderOrdered = (OrderOrdered)e.Row.DataItem;
//    if (orderOrdered.DateOrderSent == DateTime.MinValue)
//    {
//        e.Row.Cells[5].Text = "Not sent.";
//        e.Row.Cells[6].Text = "Not sent or arrived.";
//    }
//    else if (orderOrdered.DateOrderArrived == DateTime.MinValue)
//    {
//        e.Row.Cells[6].Text = "Not arrived.";
//        e.Row.Cells[7].Text = "Order sent succesfully!";
//    }
//    else
//    {
//        e.Row.Cells[7].Text = "Order sent succesfully!";
//    }
//}