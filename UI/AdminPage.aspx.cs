﻿using System;
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
        private void loadAllUsersDDL ()
        {
            List<User> allNonAdmins = BL.General.AllNonAdmins();
            ddlAllUsers.DataSource = allNonAdmins;//Admin cannot search for an admin.
            ddlAllUsers.DataBind();
        }
        protected void btnFindBy_Click(object sender, EventArgs e)
        {
            string userNameOrEmail = txtFindBy.Text;
            switch (int.Parse(ddlFindBy.SelectedValue))
            {
                case 1:
                    if (userNameOrEmail == "")
                    {
                        lblFindingError.Text = "It seems you did not enter a userName.";
                        return;
                    }
                    User userForDetail = BL.General.FindUserByUserName(userNameOrEmail);
                    break;
            }
        }
    }
}