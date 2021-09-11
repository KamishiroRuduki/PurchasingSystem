<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAndControl/Manager.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PurchasingSystem.SystemManger.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="entry-content">
            <div class="jumbotron text-center">
           <asp:PlaceHolder ID="plcLogin" runat="server" Visible="true">
        帳號:<asp:TextBox ID="txtAccount" runat="server"></asp:TextBox><br />
        密碼:<asp:TextBox ID="txtPassword" TextMode="Password" runat="server"></asp:TextBox><br />
        <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />&nbsp
               
        <asp:Literal ID="ltMsg" runat="server"></asp:Literal><br />
                </asp:PlaceHolder>
                </div>
            </div>
</asp:Content>
