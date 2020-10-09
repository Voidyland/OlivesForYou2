<%@ Page Title="" Language="C#" MasterPageFile="~/OliveOverlord.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="UI.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="lblUserName" runat="server" Text="User Name"></asp:Label>
    <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
    <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label>
    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
    <asp:Label ID="lblPass" runat="server" Text="Password"></asp:Label>
    <asp:TextBox ID="txtPass" runat="server"></asp:TextBox>
    <asp:Label ID="lblUserType" runat="server" Text="User Type"></asp:Label>
    <asp:RadioButton ID="radioFarmer" Text="Farmer" GroupName="userType" runat="server" />
    <asp:RadioButton ID="radioCompany" Text="Company" GroupName="userType" runat="server" />
    <asp:Label ID="lblCountry" runat="server" Text="Label"></asp:Label>
    <asp:TextBox ID="txtCountry" runat="server"></asp:TextBox>
    <asp:Label ID="lblPhoneNumber" runat="server" Text="Label"></asp:Label>
    <asp:TextBox ID="txtPhoneNumber" runat="server"></asp:TextBox>


</asp:Content>
