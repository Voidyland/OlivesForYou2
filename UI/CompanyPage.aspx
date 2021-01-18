﻿<%@ Page Title="" Language="C#" MasterPageFile="~/OliveOverlord.Master" AutoEventWireup="true" CodeBehind="CompanyPage.aspx.cs" Inherits="UI.CompanyPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="lblAvailableSales" runat="server" Text="View all available sales"></asp:Label>
    <br />
    <asp:Panel ID="pnlAvailableSales" runat="server">
        <asp:Label ID="lblNoAvailaleSales" runat="server" Text="It seems there are no available sales for you right now" Visible="false"></asp:Label>
        <asp:GridView ID="gvAvailableSales" runat="server" AutoGenerateColumns="false" OnRowCommand="gvAvailableSales_RowCommand">
            <Columns>
                <asp:BoundField DataField="FarmerName" HeaderText="farmers user name" />
                <asp:BoundField DataField="OliveName" HeaderText="olive type" />
                <asp:BoundField DataField="SaleWeight" HeaderText="weight in kg per 1 in stock" />
                <asp:BoundField DataField="SalePrice" HeaderText="price in dolar per 1 in stock" />
                <asp:BoundField DataField="InStock" HeaderText="in stock" />    
                <asp:BoundField DataField="DateSaleAdded" HeaderText="date added" />
                <asp:ButtonField HeaderText="order sale" ButtonType="Button"/>
            </Columns>
        </asp:GridView>
    </asp:Panel>
    <asp:Panel ID="pnlOrderSale" runat="server" Visible="false">
        <asp:Label ID="lblSaleDetails" runat="server" Text=""></asp:Label>
        <br />
        <asp:Label ID="lblCreditNumber" runat="server" Text="Enter your credit card number."></asp:Label>
        <br />
        <asp:TextBox ID="txtCreditNumber" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="requiredCreditNumber" ControlToValidate="txtCreditNumber" runat="server" ErrorMessage="You must enter a credit card number"></asp:RequiredFieldValidator>
        <br /> 
        <asp:Label ID="lblExpirationDate" runat="server" Text="Enter your credit card's expiration date and security code."></asp:Label>
        <br />
        <asp:DropDownList ID="ddlMonths" runat="server"></asp:DropDownList>
        <asp:DropDownList ID="ddlYears" runat="server"></asp:DropDownList>
        <asp:TextBox ID="txtCode" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="requiredCode" ControlToValidate="txtCode" runat="server" ErrorMessage="You must enter a sequrity code."></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="rangeCode" ControlToValidate="txtCode" MinimumValue="100" MaximumValue="999" runat="server" ErrorMessage="Your security code is a number between 100-999 on the back of the card."></asp:RangeValidator>
        <br />
        <asp:Label ID="lblStockBought" runat="server" Text="Please choose how many stocks you would like to buy"></asp:Label>
        <br />
        <asp:DropDownList ID="ddlStockBought" runat="server"></asp:DropDownList>
        <br />
        <asp:Button ID="btnOrder" runat="server" Text="Order" OnClick="btnOrder_Click" />
        <br />
        <asp:Label ID="lblOrderFailed" runat="server" Text="Something went wrong! are you sure your credit details are correct?" Visible="false"></asp:Label>
    </asp:Panel>
</asp:Content>