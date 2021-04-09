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
                List<User> allNonAdmins = BL.General.AllNonAdmins();
                LoadPNLGeneralStatistics(allNonAdmins);
                LoadAllUsersDDL(allNonAdmins);
            }
            else
            {
                User userForDetails = (User)Session["userForDetails"];
                if (userForDetails != null)
                {
                    pnlUserStats.Visible = true;
                    pnlGeneralStatistics.Visible = false;
                    findUser.Visible = false;
                    LoadPNLUserStats(userForDetails);
                }
                else
                {
                    pnlUserStats.Visible = false;
                    pnlGeneralStatistics.Visible = true;
                    findUser.Visible = true;
                    List<User> allNonAdmins = BL.General.AllNonAdmins();
                    LoadPNLGeneralStatistics(allNonAdmins);
                    LoadAllUsersDDL(allNonAdmins);
                }
            }

            
        }

        private void LoadPNLGeneralStatistics (List<User> allNonAdmins)
        {            
            int numOfFarmers, numOfCompanys;
            numOfFarmers = BL.General.NumOfUserType(allNonAdmins, 2);//A farmers user type is 2.
            numOfCompanys = allNonAdmins.Count - numOfFarmers;
            lblNumOfUsers.Text = $"There are currently {numOfFarmers} registered farmers and {numOfCompanys} registered companys.";
            List<OrderOrdered> allOrdersOrdered = BL.General.AllOrdersOrdered();
            double moneyExchanged = BL.General.MoneyExchangedInOrdersOrdered(allOrdersOrdered);
            int ordersSentNotArrived = BL.General.NumOfOrdersOnTheirWayInOrdersOrdered(allOrdersOrdered);
            OrderOrdered latestOrderOrdered = BL.General.LatestOrderInOrdersOrdered(allOrdersOrdered);
            lblMoneyExchanged.Text = $"{allOrdersOrdered}$ have been exchanged so far between farmers and companys.";
            lblNumOfOrdersOnTheirWay.Text = $"{ordersSentNotArrived} orders have been sent but have not yet arrived.";
            lblLatestOrder.Text = $"The latest order to be ordered is: {latestOrderOrdered.ToString()}";
        }

        private void LoadPNLUserStats (User userForDetails)
        {
            lblUserBasicDetails.Text = userForDetails.ToString();
            switch (userForDetails.UserType)
            {
                case 2:
                    List<OrderOrdered> orderedFromFarmer = userForDetails.AllOrdersOrdered();
                    int stocksSold = 0;
                    double moneyEarned = 0;
                    foreach (OrderOrdered oo in orderedFromFarmer)
                    {
                        stocksSold += oo.Stocks;
                        moneyEarned += oo.OrderPrice * oo.Stocks;
                    }
                    lblStocksBoughtOrSold.Text = $"This farmer has sold {stocksSold} total stocks over all its sales.";
                    lblMoneyEarnedOrSpent.Text = $"This farmer has earned {moneyEarned}$.";
                    lblAvgMoneyPerStock.Text = $"This farmer has earned an avrage of {moneyEarned / stocksSold}$ per stock.";
                    break;
                case 3:
                    List<OrderOrdered> ordersOrderedByCompany = userForDetails.AllPreviousOrders();
                    int stocksBought = 0;
                    double moneySpent = 0;
                    foreach (OrderOrdered oo in ordersOrderedByCompany)
                    {
                        stocksBought += oo.Stocks;
                        moneySpent += oo.OrderPrice * oo.Stocks;
                    }
                    lblStocksBoughtOrSold.Text = $"This company has bought {stocksBought} total stocks over all its orders.";
                    lblMoneyEarnedOrSpent.Text = $"This company has spent {moneySpent}$.";
                    lblAvgMoneyPerStock.Text = $"This farmer has spent an avrage of {moneySpent / stocksBought}$ per stock.";
                    break;
            }
        }
        /// <summary>
        /// Loads all non admin users into the drop down list of all searchable users.
        /// </summary>
        private void LoadAllUsersDDL (List<User> allNonAdmins)
        {
            
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
            pnlGeneralStatistics.Visible = false;
            findUser.Visible = false;
        }
    }
}