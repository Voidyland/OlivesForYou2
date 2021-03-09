<%@ Page Title="" Language="C#" MasterPageFile="~/OliveOverlord.Master" AutoEventWireup="true" CodeBehind="FarmerOrders.aspx.cs" Inherits="UI.FarmerOrders" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <asp:Panel ID="pnlViewStuff" runat="server">        
        <asp:Button ID="btnViewSales" runat="server" Text="View your sales" OnClick="btnPastOrPresent_Click" CommandArgument="viewSales" CausesValidation="false" />
        <asp:Button ID="btnViewOrdersOrdered" runat="server" Text="View orders ordered from you" OnClick="btnPastOrPresent_Click" CausesValidation="false" />
        <asp:Button ID="btnAddSalePanel" runat="server" Text="Create a new sale" OnClick="btnPastOrPresent_Click" CommandArgument="newSale" CausesValidation="false" />
    </asp:Panel>
    <asp:Label ID="noSale" runat="server" Text="It seems you dont have any orders. You can add one just below!"></asp:Label>
    <asp:Label ID="lblSales" runat="server" Text="Here is a table of all the sales you are offering!"></asp:Label>
    <br />
    <asp:GridView ID="Sales" runat="server" OnRowCommand="ordersForSale_RowCommand" AutoGenerateColumns="false"
         OnRowDeleting="Sales_RowDeleting" OnRowDeleted="Sales_RowDeleted" Visible="true" >
        <Columns>
            <asp:BoundField DataField="OliveName" HeaderText="olive type" />
            <asp:BoundField DataField="SaleWeight" HeaderText="weight in kg per 1 in stock" />
            <asp:BoundField DataField="SalePrice" HeaderText="price in dolar per 1 in stock" />
            <asp:BoundField DataField="InStock" HeaderText="in stock" />    
            <asp:BoundField DataField="DateSaleAdded" HeaderText="date added" />
            <asp:ButtonField HeaderText="Update Sale" Text="update" CausesValidation="false" CommandName="change" ButtonType="Button"/>
            <asp:ButtonField HeaderText="Delete Sale" Text="delete" CausesValidation="false" CommandName="remove" ButtonType="Button" />
        </Columns>
    </asp:GridView>
    <asp:Label ID="lblOrdersOrdered" runat="server" Text="Here is a table of all orders ordered from you!" Visible="false"></asp:Label>
    <br />
    <asp:GridView ID="gvOrdersOrdered" runat="server"   OnRowDataBound="gvOrdersOrdered_RowDataBound"
         AllowPaging="true" PageSize="10" Visible="false" OnPageIndexChanging="gvOrdersOrdered_PageIndexChanging"
        OnRowCommand="gvOrdersOrdered_RowCommand" AutoGenerateColumns="false" EnableViewState="true">
        <Columns>
            <asp:BoundField DataField="CompanyName" HeaderText="buyer" /> 
            <asp:BoundField DataField="CompanyEmail" HeaderText="buyers email" />
            <asp:BoundField DataField="OliveName" HeaderText="olive type" />
            <asp:BoundField DataField="OrderWeight" HeaderText="total weight" />
            <asp:BoundField DataField="OrderPrice" HeaderText="total price" />
            <asp:BoundField DataField="CountryName" HeaderText="country" />            
            <asp:BoundField DataField="DateOrderOrdered" HeaderText="date ordered" />
            <asp:BoundField DataField="DateOrderSent" HeaderText="sent status" />
            <asp:BoundField DataField="DateOrderArrived" HeaderText="arrived status" />
            <asp:ButtonField ButtonType="Button" Text="press" 
                HeaderText="confrim or unconfirm the sending of an order" />
        </Columns>
    </asp:GridView>
    <asp:Panel ID="pnlConfirmOrDeny" runat="server" Visible="false">
        <asp:Label ID="lblOrderToConfirmOrDeny" runat="server" Text=""></asp:Label>
        <asp:Button ID="btnConfirmSent" runat="server" Text="Confirm order was sent." OnClick="btnConfirmOrDeny_Click" CommandArgument="confirm" />
        <asp:Button ID="btnDenySending" runat="server" Text="Deny sending the order to the company. This will cancel the order." OnClick="btnConfirmOrDeny_Click" CommandArgument="deny" />
        <asp:Label ID="lblConfirmOrDenyEnd" runat="server" Text=""></asp:Label>
    </asp:Panel>
    <br />
    <asp:Label ID="ConfirmSentError" runat="server" Text="An error has accoured" Visible = "false"></asp:Label>
    <br />
    <asp:Panel ID="pnlFindOne" runat="server" Visible="false">
        <asp:Label ID="lblSearchByX" runat="server" Text="Search by:"></asp:Label>
        <br />
        <asp:Label ID="lblFindOrdersFromName" runat="server" Text="name"></asp:Label>
        <br />
        <asp:DropDownList ID="ddlFindOrdersFromName" runat="server"></asp:DropDownList>
        <br />
        <asp:Button ID="btnFindOrdersFromName" runat="server" Text="search" OnClick="btnFindOrders_Click" CommandArgument="findName" />
        <br />
        <asp:Label ID="lblFindUnsentOrders" runat="server" Text="unsent orders"></asp:Label>
        <br />
        <asp:Button ID="btnFindUnsentOrders" runat="server" Text="search" OnClick="btnFindOrders_Click" CommandArgument="findUnsent" />
        <br />
        <asp:Label ID="lblFindUnrecivedOrders" runat="server" Text="unrecived orders"></asp:Label>
        <br />
        <asp:Button ID="btnFindUnrecivedOrders" runat="server" Text="search" OnClick="btnFindOrders_Click" CommandArgument="findUnrecived" />
        <br />

    </asp:Panel>
    <br />
    <asp:Panel ID="pnlOrderMethod" runat="server" Visible="false">
      <%-- <asp:Label ID="lblOrderByName" runat="server" Text="If you would like to view a buyer, 
          enter their name then press the button bellow."></asp:Label>
       <br />
       <asp:TextBox ID="txtOrderByName" runat="server"></asp:TextBox>      
       <asp:RequiredFieldValidator ID="requiredOrderByName" ControlToValidate="txtOrderByName" ValidationGroup="vldOrderByName" runat="server" ErrorMessage="You must enter a name."></asp:RequiredFieldValidator>
       <br />
        
        <br />

        <asp:Label ID="lblOrderByNameSuccess" runat="server" Text="To view all orders once again, simply choose one of the other ordering methods"></asp:Label>
        <br />
       <asp:Button ID="btnOrderByName" runat="server" Text="Button" ValidationGroup="vldOrderByName" 
           OnClick="btnReorder_Click" CommandArgument="name" />
       <br />
        <asp:Label ID="lblNameNotFound" runat="server" Visible="false" Text="I'm afraid no such company exist's. Are you sure you entered the right username? remember, capital letters and spaces matter."></asp:Label>
        <br />--%>
        <asp:Label ID="lblOrderByWhat" runat="server" Text="Order by:"></asp:Label>

        <%--<asp:Label ID="lblOrderByDateOrdered" runat="server" Text="date ordered?"></asp:Label>
        <br />--%>
        <asp:Button ID="btnOrderByDateOrdered" runat="server" Text="date ordered" CommandArgument="whenOrdered" OnClick="btnReorder_Click" />
        <br />
        <%--<asp:Label ID="lblOrderByUnhandled" runat="server" Text="Would you like to order by orders that were not sent yet?"></asp:Label>
        <br />--%>
        <%--<asp:Button ID="btnOrderByUnhandled" runat="server" Text="unsent orders" OnClick="btnReorder_Click"
            CommandArgument="handled" />
        <br />
        <%--<asp:Label ID="lblOrderByDateRecived" runat="server" Text="Would you like to order by orders that were not recived yet?"></asp:Label>
        <br />
        <asp:Button ID="btnOrderByDateRecived" runat="server" Text="unrecived orders" CommandArgument="whenRecived" />
        <br />--%>
        <asp:Label ID="lblOrderOfOrdering" runat="server" Text="Choose the ordering of first to last 
            (only relevent to the previous three storting methods)"></asp:Label>
        <br />
        <asp:RadioButtonList ID="rblOrderOfOrdering" runat="server">
            <asp:ListItem Selected="True">oldest to youngest</asp:ListItem>
            <asp:ListItem>youngest to oldest</asp:ListItem>
        </asp:RadioButtonList>
    </asp:Panel>
    <asp:Label ID="increaseOrDelete" runat="server" Text=""></asp:Label>
    <asp:Panel ID="pnlAddOrder" runat="server" Visible="false" EnableViewState="true">   
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
        <asp:Label ID="lblSaleToUpdate" runat="server" Text=""></asp:Label>
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
        <asp:Label ID="lblReturnToSales" runat="server" Text="Please note that pressing the 'Update the sale' button will return you to the sales grid."></asp:Label>
        <br />
    </asp:Panel>
     <br />    
    <asp:Label ID="lblError" runat="server" Text="" ></asp:Label>
    <asp:Label ID="lblGeneralSuccess" runat="server" Text=""></asp:Label>
    
    <asp:Panel ID="pnlOrginize" runat="server">

    </asp:Panel>
</asp:Content>
 

