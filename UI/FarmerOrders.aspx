<%@ Page Title="" Language="C#" MasterPageFile="~/OliveOverlord.Master" AutoEventWireup="true" CodeBehind="FarmerOrders.aspx.cs" Inherits="UI.FarmerOrders" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="noSale" runat="server" Text="It seems you dont have any orders. You can add one just below!"></asp:Label>
    <asp:GridView ID="Sales" runat="server" OnRowCommand="ordersForSale_RowCommand" OnRowDeleting="Sales_RowDeleting" OnRowDeleted="Sales_RowDeleted">
        <Columns>
            <asp:BoundField DataField="OliveName" HeaderText="olive type" />
            <asp:BoundField DataField="SaleWeight" HeaderText="weight" />
            <asp:BoundField DataField="SalePrice" HeaderText="price" />
            <asp:BoundField DataField="InStock" HeaderText="stock" />    
            <asp:ButtonField HeaderText="Increase stock" CausesValidation="false" CommandName="increase" ButtonType="Button"/>
            <asp:ButtonField HeaderText="Delete order" CausesValidation="false" CommandName="remove" ButtonType="Button" />
        </Columns>
    </asp:GridView>
    <br />
    <br />
    <br />
    <asp:Label ID="increaseOrDelete" runat="server" Text=""></asp:Label>
    <br />
    <asp:Label ID="lblOliveName" runat="server" Text="choose the olives type"></asp:Label>
    <br />
    <asp:DropDownList ID="ddlOliveTypes" runat="server"></asp:DropDownList>   
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
    <asp:Button ID="btnNewSale" runat="server" Text="Submit the new sale" OnClick="btnNewSale_Click"/>
    <br />
    <asp:Label ID="lblError" runat="server" Text="" ></asp:Label>
</asp:Content>
 

<%--Make sure gridview has diffrent pages.
    Also maybe add option to delete orders and increse InStock
    Maybe also dont allow to make diffrent orders of the same olive type?
    Also allow to change most details of the order.--%>