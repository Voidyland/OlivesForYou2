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
        private List<Sale> allAvailableSales;
        private Sale saleBeingBought;
        protected void Page_Load(object sender, EventArgs e)
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
        private void LoadAvailableSales ()
        {
            allAvailableSales = ((User)Session["User"]).AllAvailableSales();
            if (allAvailableSales == null)
            {
                gvAvailableSales.Visible = false;
                lblNoAvailaleSales.Visible = true;
            }
            else
            {
                lblNoAvailaleSales.Visible = false;
                gvAvailableSales.Visible = true;
                gvAvailableSales.DataSource = allAvailableSales;
                gvAvailableSales.DataBind();
            }
          
        }

        protected void gvAvailableSales_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            pnlAvailableSales.Visible = false;
            pnlOrderSale.Visible = true;
            saleBeingBought = allAvailableSales[gvAvailableSales.SelectedIndex];
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
                int userID = ((User)Session["User"]).UserID;
                int salesBought = int.Parse(ddlStockBought.SelectedValue);
                if (!saleBeingBought.CreateNewOrder(userID,salesBought))
                {
                    lblOrderFailed.Visible = true;
                }
            }
        }
    }
}