<%@ Page Title="" Language="C#" MasterPageFile="~/OliveOverlord.Master" AutoEventWireup="true" CodeBehind="FarmerOrders.aspx.cs" Inherits="UI.FarmerOrders" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:GridView ID="ordersForSale" runat="server">
        <Columns>
            <asp:BoundField DataField="OliveName" HeaderText="olive type" />
            <asp:BoundField DataField="OrderWeight" HeaderText="weight" />
            <asp:BoundField DataField="OrderPrice" HeaderText="price" />
            <asp:BoundField DataField="InStock" HeaderText="stock" />
        </Columns>
    </asp:GridView>
    <%--make sure gridview has diffrent pages.--%>
    <asp:Button ID="btnNewOrder" runat="server" Text="Create new order" />
</asp:Content>
 
