<%@ Page Title="" Language="C#" MasterPageFile="~/OliveOverlord.Master" AutoEventWireup="true" CodeBehind="CompanyPage.aspx.cs" Inherits="UI.CompanyPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">  
    <asp:Panel ID="pnlFromCart" runat="server" Visible="false" EnableViewState="false">
        <asp:Label ID="lblFromCart" runat="server" Text="Ordered all of cart! you were redirected to the main comapny page."></asp:Label> <%--consider making this bigger in css--%>
        <br />
        <br />
        <br />
        <br />
    </asp:Panel>
    
    <asp:Panel ID="pnlOptions" runat="server">
        <asp:Button ID="btnAvailableSales" runat="server" Text="View available sales" OnClick="btnChangePNL_Click" CommandArgument="availSales" />
        <asp:Button ID="btnPreviousOrders" runat="server" Text="View all previous orders" OnClick="btnChangePNL_Click" CommandArgument="prevOrd" />
    </asp:Panel>
    <asp:Panel ID="pnlAvailableSales" runat="server">
        <asp:Label ID="lblAvailableSales" runat="server" Text="View all available sales"></asp:Label>       
        <asp:Label ID="lblNoAvailaleSales" runat="server" Text="It seems there are no available sales for you right now" Visible="false"></asp:Label>
        <br />
        <asp:Label ID="lblSaleBought" runat="server" Text="" Visible="false"></asp:Label>
        <asp:GridView ID="gvAvailableSales" runat="server" AutoGenerateColumns="false" OnRowCommand="gvAvailableSales_RowCommand" EnableViewState="true" OnRowDataBound="gvAvailableSales_RowDataBound">
            <Columns>
                <asp:BoundField DataField="FarmerName" HeaderText="farmers user name" />
                <asp:BoundField DataField="OliveName" HeaderText="olive type" />
                <asp:BoundField DataField="SaleWeight" HeaderText="weight in kg per 1 in stock" />
                <asp:BoundField DataField="SalePrice" HeaderText="price in dolar per 1 in stock" />
                <asp:BoundField DataField="InStock" HeaderText="in stock" />    
                <asp:BoundField DataField="DateSaleAdded" HeaderText="date added" />
                <asp:ButtonField HeaderText="order now" ButtonType="Button" CommandName="orderNow"/>
                <asp:ButtonField HeaderText="Add to cart" ButtonType="Button" CommandName="addToCart" />
            </Columns>
        </asp:GridView>
    </asp:Panel>
    <asp:Panel ID="pnlPreviousOrders" runat="server" Visible="false">
        <asp:Label ID="lblPreviousOrders" runat="server" Text="Here are all the orders you have ordered untill now."></asp:Label>
        <br />
        <asp:Label ID="lblNoPrevOrders" runat="server" Text="Either an error has accoured or you simply never ordered anything." Visible="false"></asp:Label>   
        <asp:GridView ID="gvPreviousOrders" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvPreviousOrders_RowDataBound">
            <Columns>
                <asp:BoundField DataField="FarmerName" HeaderText="sellers name" />
                <asp:BoundField DataField="OliveName" HeaderText="olive type" />
                <asp:BoundField DataField="OrderWeight" HeaderText="weight per stock" />
                <asp:BoundField DataField="OrderPrice" HeaderText="price per stock" />
                <asp:BoundField DataField="Stocks" HeaderText="stocks bought" />
                <asp:BoundField DataField="DateOrderOrdered" HeaderText="date ordered" />
                <asp:BoundField DataField="DateOrderSent" HeaderText="sent status" />
                <asp:BoundField DataField="DateOrderArrived" HeaderText="arrived status" />
                <asp:ButtonField HeaderText="confirm order arrived" ButtonType="Button" />
            </Columns>
        </asp:GridView>
    </asp:Panel>
    <asp:Panel ID="pnlOrderSale" runat="server" Visible="false">
        <asp:Label ID="lblSaleDetails" runat="server" Text=""></asp:Label>
        <br />
        <asp:Label ID="lblCreditNumber" runat="server" Text="credit card number."></asp:Label>
        <br />
        <asp:TextBox ID="txtCreditNumber" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="requiredCreditNumber" ControlToValidate="txtCreditNumber" runat="server" ErrorMessage="You must enter a credit card number"></asp:RequiredFieldValidator>
        <br /> 
        <asp:Label ID="lblExpirationDate" runat="server" Text="credit card expiration date and security code."></asp:Label>
        <br />
        <asp:DropDownList ID="ddlMonths" runat="server"></asp:DropDownList>
        <asp:DropDownList ID="ddlYears" runat="server"></asp:DropDownList>
        <asp:TextBox ID="txtCode" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="requiredCode" ControlToValidate="txtCode" runat="server" ErrorMessage="You must enter a sequrity code."></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="rangeCode" ControlToValidate="txtCode" MinimumValue="100" MaximumValue="999" runat="server" ErrorMessage="Your security code is a number between 100-999 on the back of the card."></asp:RangeValidator>
        <br />
        <asp:Label ID="lblStockBought" runat="server" Text="how many stocks would you like to buy"></asp:Label>
        <br />
        <asp:DropDownList ID="ddlStockBought" runat="server"></asp:DropDownList>
        <br />
        <asp:Button ID="btnOrder" runat="server" Text="Order" OnClick="btnOrder_Click" />
        <br />
        <asp:Label ID="lblOrderFailed" runat="server" Text="Something went wrong! are you sure your credit details are correct?" Visible="false"></asp:Label>
    </asp:Panel>    

    <asp:Panel ID="pnlAddToCart" runat="server" Visible="false">
        <asp:Label ID="lblCartSaleDetails" runat="server" Text=""></asp:Label>
        <br />
        <asp:Label ID="lblCartStocksBeingBought" runat="server" Text="how many stocks would you like add to cart"></asp:Label>
        <br />
        <asp:DropDownList ID="ddlCartStocksBeingBought" runat="server"></asp:DropDownList>
        <br />
        <asp:Button ID="btnAddToCart" runat="server" Text="add to cart" OnClick="btnAddToCart_Click" />
        <br />
        <asp:Label ID="lblInfo" runat="server" EnableViewState="false" Text="Added to cart! Please notice your cart is temporary and will be lost if you remain inactive for a while or close this site." Visible="false"></asp:Label>
        <br />
    </asp:Panel>        
</asp:Content>
