using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;

namespace UI
{
    public partial class companyCart : System.Web.UI.Page
    {
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
                LoadCart();
            }
        }
        private void LoadCart ()
        {
            List<Sale> cart = (List<Sale>)Session["saleBeingBought"];
            if (cart != null)
            {
                gvCart.DataSource = cart;
                gvCart.DataBind();
                lblCart.Visible = false;
                gvCart.Visible = true;
            }
            else
            {
                lblCart.Visible = true;
                gvCart.Visible = false;
            }
        }
        protected void gvCart_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}