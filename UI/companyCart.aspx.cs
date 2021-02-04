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
            }

        }
    }
}