<%@ Page Title="" Language="C#" MasterPageFile="~/OliveOverlord.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="UI.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="lblUserName" runat="server" Text="User Name"></asp:Label>
    <br />
    <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="requiredUserName" runat="server" ControlToValidate="txtUserName" ErrorMessage="You must enter a user name"></asp:RequiredFieldValidator>
    <br />
    <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label>
    <br />
    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="requiredEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="You must enter an email adress"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="regularEmail" runat="server" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="You must enter a valid email adress"></asp:RegularExpressionValidator>
    <br />
    <asp:Label ID="lblPass" runat="server" Text="Password"></asp:Label>
    <br />
    <asp:TextBox ID="txtPass" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="requiredPass" runat="server" ControlToValidate="txtPass" ErrorMessage="You must enter a password"></asp:RequiredFieldValidator>
    <br />
    <asp:Label ID="lblUserType" runat="server" Text="User Type"></asp:Label>
    <br />
    <asp:RadioButton ID="radioFarmer" Text="Farmer" GroupName="userType" runat="server"  Checked="true"/>    
    <br />
    <asp:RadioButton ID="radioCompany" Text="Company" GroupName="userType" runat="server" />    
    <br />
    <asp:Label ID="lblCountry" runat="server" Text="Label"></asp:Label>    
    <br />
    <asp:TextBox ID="txtCountry" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="requiredCountry" runat="server" ControlToValidate="txtCountry" ErrorMessage="You must enter a country"></asp:RequiredFieldValidator>
    <br />
    <asp:Label ID="lblPhoneNumber" runat="server" Text="Label"></asp:Label>
    <br />
    <asp:TextBox ID="txtPhoneNumber" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="requiredPhoneNumber" runat="server" ControlToValidate="txtPhoneNumber" ErrorMessage="You must enter your phone number"></asp:RequiredFieldValidator>
    <asp:Button ID="Submit" runat="server" Text="Submit" OnClick="Submit_Click" />
    <asp:Label ID="Error" runat="server" Visible="false" Text="An error has acoured. Please make sure all details are correct and that this is the first time you have signed up with this paricular email adress."></asp:Label>
</asp:Content>
