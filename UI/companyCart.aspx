<%@ Page Title="" Language="C#" MasterPageFile="~/OliveOverlord.Master" AutoEventWireup="true" CodeBehind="companyCart.aspx.cs" Inherits="UI.companyCart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="lblCart" runat="server" Text="Oh no! Cart is empty (sad face)"></asp:Label>
    <asp:GridView ID="gvCart" runat="server" AutoGenerateColumns="false" OnRowCommand="gvCart_RowCommand">
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






</asp:Content>
