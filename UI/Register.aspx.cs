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
            int phoneNumber = int.Parse(txtPhoneNumber.Text);
            
            try
            {
                Session["User"] = BL.General.Register(userName, pass, email, userType, country, phoneNumber);
                Result.Text = "Success!";
            }
            catch
            {
                Result.Text = "An error has acoured.Please make sure all details are correct and that this is the first time you have signed up with this paricular email adress.";           
            }
            Result.Visible = true;
        }
    }
}