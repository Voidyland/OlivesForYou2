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
                btnPaymant.Visible = true;
            }
            else
            {
                lblCart.Visible = true;
                gvCart.Visible = false;
                btnPaymant.Visible = false;
            }
        }
        protected void gvCart_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "delete")
                {
                    int index = int.Parse(e.CommandArgument.ToString());
                    List<Sale> cart = (List<Sale>)Session["saleBeingBought"];
                    cart.RemoveAt(index);
                    gvCart.DeleteRow(index);
                    LoadCart();
                }
            }
            catch (Exception exeption)
            {

            }
        }

        protected void gvCart_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {

        }

        protected void gvCart_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void btnPaymant_Click(object sender, EventArgs e)
        {
            pnlCart.Visible = false;
            pnlPayment.Visible = true;
        }

        protected void btnOrder_Click(object sender, EventArgs e)
        {
            try
            {
                if (true) // placeholder for the webservies
                {
                    List<Sale> cart = (List<Sale>)Session["saleBeingBought"];
                    int companyID = ((User)Session["User"]).UserID;
                    foreach (Sale sale in cart)
                    {
                        if (!sale.CreateNewOrder(companyID, sale.InStock)) //In this senario, InStock represents the amout of stocks being bought and not the amout of stocks available.
                        { //Remember, only charge if order was successfull! Charge somewhere here, in this if.
                            lblOrderFailed.Visible = true;
                            lblOrderFailed.Text = "Oh oh! Something went wrong... Its possible that some of your purchuses were successfull and others not." +
                                " Return to your company page to see if any new orders apear. You will only be charged for orders that successfully went through.";
                            return;
                        }
                        else
                        {
                            //charge em
                        }
                    }
                    Response.Redirect("CompanyPage?cart=All cart ordered");
                }
            }
            catch (Exception exeption)
            {
                lblOrderFailed.Visible = true;
            }
        }
    }
}