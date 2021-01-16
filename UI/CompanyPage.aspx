<%@ Page Title="" Language="C#" MasterPageFile="~/OliveOverlord.Master" AutoEventWireup="true" CodeBehind="CompanyPage.aspx.cs" Inherits="UI.CompanyPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="lblAvailableSales" runat="server" Text="View all available sales"></asp:Label>
    <br />
    <asp:Panel ID="pnlAvailableSales" runat="server">
        <asp:Label ID="lblNoAvailaleSales" runat="server" Text="It seems there are no available sales for you right now" Visible="false"></asp:Label>
        <asp:GridView ID="gvAvailableSales" runat="server" AutoGenerateColumns="false">
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
    <asp:Panel ID="pnlOrderSale" runat="server">
        <asp:Label ID="lblSaleDetails" runat="server" Text=""></asp:Label>
        <br />
        <asp:Label ID="lblCreditNumber" runat="server" Text="Enter your credit card number."></asp:Label>
        <br />
        <asp:TextBox ID="txtCreditNumber" runat="server"></asp:TextBox>
        <br /> 
        <asp:Label ID="lblExpirationDate" runat="server" Text="Enter your credit card's expiration date and security code."></asp:Label>
        <br />
        <%--copy from steam--%>
        <br />

        <br />

        <br />


    </asp:Panel>
</asp:Content>
