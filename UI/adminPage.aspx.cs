using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;
namespace UI
{
    public partial class adminPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["User"] == null) 
                {
                    Response.Redirect("MainPage.aspx");
                    return;
                }
                if (((User)Session["User"]).UserType != 1)
                {
                    Response.Redirect("MainPage.aspx");
                    return;
                }
                lblHello.Text = "Hello " + ((User)Session["User"]).UserName + "! Welcome back.";
            }
        }
    }
}