<%@ Page Title="" Language="C#" MasterPageFile="~/OliveOverlord.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="UI.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="lblEmail" runat="server" Text="Please enter your Email"></asp:Label>
    <br />
    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="You must enter an Email adress"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="RegularExpressionEmail" runat="server" ControlToValidate="txtEmail" ValidationExpression="^[_A-Za-z0-9-\\+]+(\\.[_A-Za-z0-9-]+)*
      @[A-Za-z0-9-]+(\\.[A-Za-z0-9]+)*(\\.[A-Za-z]{2,})$" ErrorMessage="You must enter text in the form of an Email adress"></asp:RegularExpressionValidator>
    <br />
    <asp:Label ID="lblPass" runat="server" Text="Please enter your Password"></asp:Label>
    <br />
    <asp:TextBox ID="txtPass" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredPass" runat="server" ControlToValidate="txtPass" ErrorMessage="You must enter a password"></asp:RequiredFieldValidator>
    <br />
    <asp:Button ID="btnLogin" runat="server" Text="Submit" OnClick="btnLogin_Click" />
</asp:Content>
