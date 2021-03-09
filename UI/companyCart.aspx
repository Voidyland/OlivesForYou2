<%@ Page Title="" Language="C#" MasterPageFile="~/OliveOverlord.Master" AutoEventWireup="true" CodeBehind="companyCart.aspx.cs" Inherits="UI.companyCart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="FarmerName" HeaderText="farmers user name" />
            <asp:BoundField DataField="OliveName" HeaderText="olive type" />
            <asp:BoundField DataField="SaleWeight" HeaderText="weight in kg per 1 in stock" />
            <asp:BoundField DataField="SalePrice" HeaderText="price in dolar per 1 in stock" />
            <asp:BoundField DataField="InStock" HeaderText="stocks you are buying" />    
            <asp:BoundField DataField="DateSaleAdded" HeaderText="date added" />            
        </Columns>
    </asp:GridView>






</asp:Content>
