<%@ Page Title="" Language="C#" MasterPageFile="~/OliveOverlord.Master" AutoEventWireup="true" CodeBehind="AdminPage.aspx.cs" Inherits="UI.adminPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="lblHello" runat="server" Text="" EnableViewState="true"></asp:Label>
    <asp:Panel ID="findUser" runat="server">
        <asp:Label ID="lblFindBy" runat="server" Text="find user by:"></asp:Label>
        <asp:DropDownList ID="ddlFindBy" runat="server">
            <asp:ListItem Value="1">user name</asp:ListItem>
            <asp:ListItem Value="2">email</asp:ListItem>            
        </asp:DropDownList>
        <asp:TextBox ID="txtFindBy" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="requiredFindBy" runat="server" ControlToValidate="txtFindBy" ErrorMessage="You must enter something"></asp:RequiredFieldValidator>
        <asp:Button ID="btnFindBy" runat="server" Text="Find user" OnClick="btnFindBy_Click" />
    </asp:Panel>
</asp:Content>
