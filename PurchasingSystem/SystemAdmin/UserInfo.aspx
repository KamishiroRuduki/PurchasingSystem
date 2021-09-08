<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAndControl/Main.Master" AutoEventWireup="true" CodeBehind="UserInfo.aspx.cs" Inherits="PurchasingSystem.SystemAdmin.UserInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="entry-content">
        <a class="nav-link active" aria-current="page" href="/SystemAdmin/UserInfo.aspx">會員首頁</a><br />
        <a class="nav-link active" aria-current="page" href="/SystemAdmin/CreateOrder.aspx">委託代購</a><br />
        <a class="nav-link active" aria-current="page" href="/SystemAdmin/OrderList.aspx">訂單查詢</a><br />
        <a class="nav-link active" aria-current="page" href="/SystemAdmin/UserDetail.aspx">資料修改</a><br />

    </div>
</asp:Content>
