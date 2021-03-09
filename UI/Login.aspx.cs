using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;
namespace UI
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string pass = txtPass.Text;
            User user = BL.General.Login(email, pass);            
            if (user != null)
            {
                Session["User"] = user;
                Response.Redirect("MainPage.aspx");
            }
            lblError.Visible = true;
        }
    }
}