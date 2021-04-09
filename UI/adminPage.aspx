<%@ Page Title="" Language="C#" MasterPageFile="~/OliveOverlord.Master" AutoEventWireup="true" CodeBehind="AdminPage.aspx.cs" Inherits="UI.adminPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="lblHello" runat="server" Text="" EnableViewState="true"></asp:Label>
    
     <asp:Panel ID="pnlGeneralStatistics" runat="server">
         <br />  
         <asp:Label ID="lblNumOfUsers" runat="server" Text=""></asp:Label>
         <br />
         <asp:Label ID="lblMoneyExchanged" runat="server" Text=""></asp:Label>
         <br />
         <asp:Label ID="lblNumOfOrdersOnTheirWay" runat="server" Text=""></asp:Label>
         <br />
         <asp:Label ID="lblLatestOrder" runat="server" Text=""></asp:Label>
         <br />
    </asp:Panel>
   
    <asp:Panel ID="findUser" runat="server">
        <asp:Label ID="lblFindBy" runat="server" Text="find user by:"></asp:Label>
        <br />
        <asp:DropDownList ID="ddlFindBy" runat="server">
            <asp:ListItem Value="1">user name</asp:ListItem>
            <asp:ListItem Value="2">email</asp:ListItem>   
            <asp:ListItem Value="3">drop down list</asp:ListItem>
        </asp:DropDownList>
        <br />
        <asp:DropDownList ID="ddlAllUsers" runat="server"></asp:DropDownList>
        <br />
        <asp:TextBox ID="txtFindBy" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="requiredFindBy" runat="server" ControlToValidate="txtFindBy" ErrorMessage="You must enter something"></asp:RequiredFieldValidator>
        <br />
        <asp:Button ID="btnFindBy" runat="server" Text="Find user" OnClick="btnFindBy_Click" />
        <br />
        <asp:Label ID="lblFindingError" runat="server" Text="" EnableViewState="false"></asp:Label>
    </asp:Panel>
    
    <asp:Panel ID="pnlUserStats" runat="server">
        <br />
        <asp:Label ID="lblUserBasicDetails" runat="server" Text=""></asp:Label>
        <br />
        <asp:Label ID="lblStocksBoughtOrSold" runat="server" Text=""></asp:Label>
        <br />
        <asp:Label ID="lblMoneyEarnedOrSpent" runat="server" Text=""></asp:Label>
        <br />
        <asp:Label ID="lblAvgMoneyPerStock" runat="server" Text=""></asp:Label>
    </asp:Panel>
   
</asp:Content>
