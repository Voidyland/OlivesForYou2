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
    <asp:Label ID="lblOliveName" runat="server" Text="choose the olives type"></asp:Label>
    <br />
    <asp:DropDownList ID="ddlOliveTypes" runat="server"></asp:DropDownList>
    <asp:RequiredFieldValidator ID="requiredOliveType" runat="server" ControlToValidate="ddlOliveType" ErrorMessage="You must choose an olive type"></asp:RequiredFieldValidator>
    <br />
    <asp:Label ID="lblWeight" runat="server" Text="Enter the weight of the order (the weight of one item from the stock, not the combined total)"></asp:Label>
    <br />
    <asp:TextBox ID="txtWeight" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="requiredWeight" runat="server" ControlToValidate="txtWeight" ErrorMessage="You must enter the weight"></asp:RequiredFieldValidator>
    <asp:RangeValidator ID="rangeWeight" runat="server" Type="Double" MinimumValue="1" MaximumValue="1000" ControlToValidate="txtWeight" ErrorMessage="You must enter a weight between 1 and 1,000"></asp:RangeValidator>
    <br />
    <asp:Label ID="lblPrice" runat="server" Text="Enter the price of the order (the price of one item from the stock)"></asp:Label>
    <br />
    <asp:TextBox ID="txtPrice" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="requiredPrice" runat="server" ControlToValidate="txtPrice" ErrorMessage="You must enter the price"></asp:RequiredFieldValidator>    
    <asp:RangeValidator ID="rangePrice" runat="server" Type="Double" MinimumValue="1" MaximumValue="1000000" ControlToValidate="txtPrice" ErrorMessage="You must enter a price between 1 and 1,000,000"></asp:RangeValidator>
    <br />
    <asp:Label ID="lblStock" runat="server" Text="Enter the amout of items in stock"></asp:Label>
    <br />
    <asp:TextBox ID="txtStock" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="requiredStock" runat="server" ControlToValidate="txtStock" ErrorMessage="You must enter the stock number"></asp:RequiredFieldValidator>
    <asp:RangeValidator ID="rangeStock" runat="server" Type="Integer" MinimumValue="1" MaximumValue="50" ControlToValidate="txtStock" ErrorMessage="You must enter a number between 1 and 50"></asp:RangeValidator>
    <br />
    <asp:Button ID="btnNewOrder" runat="server" Text="Submit the new order" />
</asp:Content>
 

<%--Make sure gridview has diffrent pages.
    Also maybe add option to delete orders and increse InStock--%>