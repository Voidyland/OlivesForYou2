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
            LoadCountrys();
        }
        private void LoadCountrys ()
        {
            List<string> countrys = BL.General.AllCountrys();
            ddlCountrys.DataSource = countrys;
            ddlCountrys.DataBind();
        }
        protected void Submit_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;
            string email = txtEmail.Text;
            string pass = txtPass.Text;
            int userType = 2; //2=farmer
            if (radioCompany.Checked) userType = 3; //3=company
            string country = ddlCountrys.SelectedValue;
            string phoneNumber = txtPhoneNumber.Text;
            List<User> prevUsers = BL.General.AllUsers();
            foreach (User user in prevUsers)
            {
                if (user.UserName == userName || user.Email == email || user.PhoneNumber == phoneNumber)
                {
                    Error.Text = "It seems you entered identifying details that belong to another user. Identifying details are username, email and phone number.";
                    Error.Visible = true;
                    return;
                }
            }
            try
            {
                Session["User"] = BL.General.Register(userName, pass, email, userType, country, phoneNumber);
                Error.Visible = false;
                Response.Redirect("MainPage.aspx");
            }
            catch
            {                
                Error.Visible = true;
            }
            
        }
    }
}