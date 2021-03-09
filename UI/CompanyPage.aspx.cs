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
            pnlAvailableSales.Visible = false;
            pnlOrderSale.Visible = true;
            allAvailableSales = (List<Sale>)Session["allAvailableSales"];
            saleBeingBought = allAvailableSales[int.Parse(e.CommandArgument.ToString())];
            Session["saleBeingBought"] = saleBeingBought;
            lblSaleDetails.Text = saleBeingBought.ToString();
            CreateMonthsDDL();
            CreateYearsDDL();
            CreateStocksDDL(saleBeingBought.InStock);
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
                if (orderOrdered.DateOrderSent == DateTime.MinValue)
                {
                    e.Row.Cells[5].Text = "Not sent";
                    e.Row.Cells[6].Text = "Not arrived";
                    e.Row.Cells[7].Text = "not arrived.";
                }
                else if (orderOrdered.DateOrderArrived == DateTime.MinValue)
                {
                    e.Row.Cells[6].Text = "Not arrived";
                }
                else
                {
                    e.Row.Cells[7].Text = "Order arrived, no need to confirm";
                }
            }
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