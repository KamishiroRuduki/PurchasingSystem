<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAndControl/Main.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PurchasingSystem.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="entry-content">
           <asp:PlaceHolder ID="plcLogin" runat="server" Visible="false">
        帳號:<asp:TextBox ID="txtAccount" runat="server"></asp:TextBox><br />
        密碼:<asp:TextBox ID="txtPassword" TextMode="Password" runat="server"></asp:TextBox><br />
        <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />&nbsp
               <a href="Register.aspx">註冊</a><br />
        <asp:Literal ID="ltMsg" runat="server"></asp:Literal><br />
            </asp:PlaceHolder>
        </div>
</asp:Content>
