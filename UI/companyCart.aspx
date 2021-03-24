<%@ Page Title="" Language="C#" MasterPageFile="~/OliveOverlord.Master" AutoEventWireup="true" CodeBehind="companyCart.aspx.cs" Inherits="UI.companyCart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlCart" runat="server" Visible="true">
        <asp:Label ID="lblCart" runat="server" Text="Oh no! Cart is empty (sad face)"></asp:Label>
        <asp:GridView ID="gvCart" runat="server" AutoGenerateColumns="false" OnRowCommand="gvCart_RowCommand" 
            OnRowDeleted="gvCart_RowDeleted" OnRowDeleting="gvCart_RowDeleting">
            <Columns>
                <asp:BoundField DataField="FarmerName" HeaderText="farmers user name" />
                <asp:BoundField DataField="OliveName" HeaderText="olive type" />
                <asp:BoundField DataField="SaleWeight" HeaderText="weight in kg per 1 in stock" />
                <asp:BoundField DataField="SalePrice" HeaderText="price in dolar per 1 in stock" />
                <asp:BoundField DataField="InStock" HeaderText="stocks you are buying" />    
                <asp:BoundField DataField="DateSaleAdded" HeaderText="date added" />    
                <asp:ButtonField HeaderText="Delete sale" ButtonType="Button" CommandName="delete" />
            </Columns>
        </asp:GridView>
        <br />
        <asp:Button ID="btnPaymant" runat="server" Text="move to payment" OnClick="btnPaymant_Click" />
    </asp:Panel>
    <asp:Panel ID="pnlPayment" runat="server">
        <asp:Label ID="lblSalesInfo" runat="server" Text=""></asp:Label>
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
        <asp:Button ID="btnOrder" runat="server" Text="Order" OnClick="btnOrder_Click" />
        <br />
        <asp:Label ID="lblOrderFailed" runat="server" Text="Something went wrong! are you sure your credit details are correct?" Visible="false"></asp:Label>
    </asp:Panel>

   <%-- <asp:Panel ID="pnlOrderSale" runat="server" Visible="false">
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
    </asp:Panel>  --%>

</asp:Content>
