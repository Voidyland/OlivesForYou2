using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UI
{
    public partial class OliveOverlord : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] == null)
            {
                //AddButton("Main Page", "MainPage.aspx");
                AddButton("Login", "Login.aspx");
                AddButton("Register", "Register.aspx");
                btnLogout.Visible = false;
            }
            else
            {
                btnLogout.Visible = true;
                switch (((User)Session["User"]).UserType)
                {
                    case 1:

                        break;
                    case 2:
                        AddButton("View and create orders", "FarmerOrders.aspx");
                        break;
                    case 3:

                        break;
                }
            }
        }
        
        protected void logOut_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }
        protected void RedirectAnywhere(object sender, EventArgs e)
        {
            Button s = (Button)sender;
            Response.Redirect(s.CommandArgument);
        }
        protected void AddButton(string ButtonText, string ButtonCommandArgument)
        {
            Button b = new Button();
            b.Text = ButtonText;
            b.CommandArgument = ButtonCommandArgument;
            b.Click += RedirectAnywhere;
            b.CausesValidation = false;
            pnlTopBar.Controls.Add(b);
        }
    }
}