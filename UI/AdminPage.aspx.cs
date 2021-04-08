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
                loadAllUsersDDL();
            }
        }
        /// <summary>
        /// Loads all non admin users into the drop down list of all searchable users.
        /// </summary>
        private void loadAllUsersDDL ()
        {
            List<User> allNonAdmins = BL.General.AllNonAdmins();//Admin cannot search for an admin.
            List<ListItem> allUserNames = new List<ListItem>();
            foreach (User user in allNonAdmins) //Show only names
            {
                allUserNames.Add(new ListItem(user.UserName, user.UserName));
            }
            ddlAllUsers.DataSource = allUserNames;
            ddlAllUsers.DataBind();
        }
        protected void btnFindBy_Click(object sender, EventArgs e)
        {
            string userNameOrEmail = txtFindBy.Text;
            User userForDetail = null;
            switch (int.Parse(ddlFindBy.SelectedValue))
            {
                case 1:
                    if (userNameOrEmail == "")
                    {
                        lblFindingError.Text = "It seems you did not enter a userName.";
                        return;
                    }
                    lblFindingError.Text = "";
                    userForDetail = BL.General.FindUserByUserName(userNameOrEmail);
                    break;
                case 2:
                    if (userNameOrEmail == "")
                    {
                        lblFindingError.Text = "It seems you did not enter an email.";
                        return;
                    }
                    lblFindingError.Text = "";
                    userForDetail = BL.General.FindUserByEmail(userNameOrEmail);
                    break;
                case 3:
                    userForDetail = BL.General.FindUserByUserName(ddlAllUsers.SelectedValue);
                    break;
                default:
                    lblFindingError.Text = "It seems something went wrong...";
                    return;                    
            }
            if (userForDetail == null)
            {
                lblFindingError.Text = "It seems something went wrong, the user might not exist. Are you sure you entered the right details?";
                return;
            }
            Session["userForDetail"] = userForDetail;
            pnlUserStats.Visible = true;
            findUser.Visible = false;
        }
    }
}