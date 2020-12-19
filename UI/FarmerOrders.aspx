<%@ Page Title="" Language="C#" MasterPageFile="~/OliveOverlord.Master" AutoEventWireup="true" CodeBehind="FarmerOrders.aspx.cs" Inherits="UI.FarmerOrders" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="noSale" runat="server" Text="It seems you dont have any orders. You can add one just below!"></asp:Label>
    <asp:GridView ID="Sales" runat="server" OnRowCommand="ordersForSale_RowCommand"
         OnRowDeleting="Sales_RowDeleting" OnRowDeleted="Sales_RowDeleted" Visible="true" >
        <Columns>
            <asp:BoundField DataField="OliveName" HeaderText="olive type" />
            <asp:BoundField DataField="SaleWeight" HeaderText="weight per 1 in stock" />
            <asp:BoundField DataField="SalePrice" HeaderText="price per 1 in stock" />
            <asp:BoundField DataField="InStock" HeaderText="in stock" />    
            <asp:BoundField DataField="DateSaleAdded" HeaderText="date added" />
            <asp:ButtonField HeaderText="Update Sale" CausesValidation="false" CommandName="change" ButtonType="Button"/>
            <asp:ButtonField HeaderText="Delete Sale" CausesValidation="false" CommandName="remove" ButtonType="Button" />
        </Columns>
    </asp:GridView>
    <br />
    <asp:GridView ID="gvOrdersOrdered" runat="server"   OnRowDataBound="gvOrdersOrdered_RowDataBound"
         AllowPaging="true" PageSize="10" Visible="false" OnPageIndexChanging="gvOrdersOrdered_PageIndexChanging"
        OnRowCommand="gvOrdersOrdered_RowCommand" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="CompanyName" HeaderText="buyer" /> 
            <asp:BoundField DataField="OliveName" HeaderText="olive type" />
            <asp:BoundField DataField="OrderWeight" HeaderText="total weight" />
            <asp:BoundField DataField="OrderPrice" HeaderText="total price" />
            <asp:BoundField DataField="DateOrderOrdered" HeaderText="date ordered" />
            <asp:BoundField DataField="DateOrderSent" HeaderText="date you sent the shipment" />
            <asp:BoundField DataField="DateOrderArrived" HeaderText="date shipment was recived by company" />
            <asp:ButtonField ButtonType="Button" Text="Press me to confirm the order was sent" 
                HeaderText="confrim or unconfirm the sending of an order" />
        </Columns>
    </asp:GridView>
    <br />
    <asp:Label ID="lblOrderByName" runat="server" Text="If you would like to order by a company's name, 
        enter it then press the button bellow"></asp:Label>
    <br />
    <asp:TextBox ID="txtOrderByName" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="requiredOrderByName" ControlToValidate="txtOrderByName" ValidationGroup="vldOrderByName" runat="server" ErrorMessage="You must enter a name."></asp:RequiredFieldValidator>
    <br />
    <asp:Button ID="btnOrderByName" runat="server" Text="Button" ValidationGroup="vldOrderByName" OnClick="btnOrderByName_Click" />
    <br />
    <asp:Label ID="lblPastOrPresent" runat="server" Text="Would you like to view all orders being ordered/completed?"></asp:Label>
    <br />
    <asp:Button ID="btnPastOrPresent" runat="server" Text="Watch sales bought" OnClick="btnPastOrPresent_Click" CausesValidation="false" />
    <br />
    <asp:Label ID="increaseOrDelete" runat="server" Text=""></asp:Label>
    <asp:Panel ID="pnlAddOrder" runat="server" Visible="true">   
    <br />
    <asp:Label ID="lblOliveName" runat="server" Text="Choose the olives type"></asp:Label>
    <br />
    <asp:DropDownList ID="ddlOliveTypes" runat="server"></asp:DropDownList>   
    <br />
    <asp:Label ID="lblWeight" runat="server" Text="Enter the weight of the order (the weight of one item from the stock, not the combined total)"></asp:Label>
    <br />
    <asp:TextBox ID="txtWeight" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="requiredWeight" ValidationGroup="vldNewSale" runat="server" ControlToValidate="txtWeight" ErrorMessage="You must enter the weight"></asp:RequiredFieldValidator>
    <asp:RangeValidator ID="rangeWeight" runat="server" ValidationGroup="vldNewSale" Type="Double" MinimumValue="1" MaximumValue="1000" ControlToValidate="txtWeight" ErrorMessage="You must enter a weight between 1 and 1,000"></asp:RangeValidator>
    <br />
    <asp:Label ID="lblPrice" runat="server" Text="Enter the price of the order (the price of one item from the stock)"></asp:Label>
    <br />
    <asp:TextBox ID="txtPrice" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="requiredPrice" ValidationGroup="vldNewSale" runat="server" ControlToValidate="txtPrice" ErrorMessage="You must enter the price"></asp:RequiredFieldValidator>    
    <asp:RangeValidator ID="rangePrice" runat="server" ValidationGroup="vldNewSale" Type="Double" MinimumValue="1" MaximumValue="1000000" ControlToValidate="txtPrice" ErrorMessage="You must enter a price between 1 and 1,000,000"></asp:RangeValidator>
    <br />
    <asp:Label ID="lblStock" runat="server" Text="Enter the amout of items in stock"></asp:Label>
    <br />
    <asp:TextBox ID="txtStock" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="requiredStock" ValidationGroup="vldNewSale" runat="server" ControlToValidate="txtStock" ErrorMessage="You must enter the stock number"></asp:RequiredFieldValidator>
    <asp:RangeValidator ID="rangeStock" ValidationGroup="vldNewSale" runat="server" Type="Integer" MinimumValue="1" MaximumValue="50" ControlToValidate="txtStock" ErrorMessage="You must enter a number between 1 and 50"></asp:RangeValidator>
    <br />
    <asp:Button ID="btnNewSale" ValidationGroup="vldNewSale" runat="server" Text="Submit the new sale" OnClick="btnNewSale_Click"/>
    <br />
    </asp:Panel>
    <asp:Panel ID="pnlUpdateOrder" runat="server" Visible="false">
        <asp:Label ID="lblOrderToUpdate" runat="server" Text=""></asp:Label>
        <br />
        <asp:Label ID="lblUpdateOliveName" runat="server" Text="Choose new the olives type"></asp:Label>
    <br />
    <asp:DropDownList ID="ddlUpdateOliveTypes" runat="server"></asp:DropDownList>   
    <br />
    <asp:Label ID="lblUpdateWeight" runat="server" Text="Enter the new weight of the order (the weight of one item from the stock, not the combined total)"></asp:Label>
    <br />
    <asp:TextBox ID="txtUpdateWeight" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="requiredUpdateWeight" ValidationGroup="vldUpdateSale" runat="server" ControlToValidate="txtUpdateWeight" ErrorMessage="You must enter the weight"></asp:RequiredFieldValidator>
    <asp:RangeValidator ID="rangeUpdateWeight" ValidationGroup="vldUpdateSale" runat="server" Type="Double" MinimumValue="1" MaximumValue="1000" ControlToValidate="txtUpdateWeight" ErrorMessage="You must enter a weight between 1 and 1,000"></asp:RangeValidator>
    <br />
    <asp:Label ID="lblUpdatePrice" runat="server" Text="Enter the new price of the order (the price of one item from the stock)"></asp:Label>
    <br />
    <asp:TextBox ID="txtUpdatePrice" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="requiredUpdatePrice" ValidationGroup="vldUpdateSale" runat="server" ControlToValidate="txtUpdatePrice" ErrorMessage="You must enter the price"></asp:RequiredFieldValidator>    
    <asp:RangeValidator ID="rangeUpdatePrice" ValidationGroup="vldUpdateSale" runat="server" Type="Double" MinimumValue="1" MaximumValue="1000000" ControlToValidate="txtUpdatePrice" ErrorMessage="You must enter a price between 1 and 1,000,000"></asp:RangeValidator>
    <br />
    <asp:Label ID="lblUpdateStock" runat="server" Text="Enter the new amout of items in stock"></asp:Label>
    <br />
    <asp:TextBox ID="txtUpdateStock" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="requiredUpdateStock" ValidationGroup="vldUpdateSale" runat="server" ControlToValidate="txtUpdateStock" ErrorMessage="You must enter the stock number"></asp:RequiredFieldValidator>
    <asp:RangeValidator ID="rangeUpdateStock" ValidationGroup="vldUpdateSale" runat="server" Type="Integer" MinimumValue="1" MaximumValue="50" ControlToValidate="txtUpdateStock" ErrorMessage="You must enter a number between 1 and 50"></asp:RangeValidator>
    <br />
    <asp:Button ID="btnUpdateSale" runat="server" ValidationGroup="vldUpdateSale" Text="Update the sale" OnClick="btnUpdateSale_Click"/>
    <br />
    </asp:Panel>
    <asp:Label ID="lblError" runat="server" Text="" ></asp:Label>
</asp:Content>
 

