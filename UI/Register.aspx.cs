using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;
namespace UI
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;
            string email = txtEmail.Text;
            string pass = txtPass.Text;
            int userType = 2; //2=farmer
            if (radioCompany.Checked) userType = 3; //3=company
            string country = txtCountry.Text;
            string phoneNumber = txtPhoneNumber.Text;
            
            try
            {
                Session["User"] = BL.General.Register(userName, pass, email, userType, country, phoneNumber);
                Response.Redirect("MainPage.aspx");
            }
            catch
            {                
                Error.Visible = true;
            }
            
        }
    }
}