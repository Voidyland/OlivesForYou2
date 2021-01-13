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
            List<Sale> allAvailableSales = ((User)Session["User"]).AllAvailableSales();
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
    }
}