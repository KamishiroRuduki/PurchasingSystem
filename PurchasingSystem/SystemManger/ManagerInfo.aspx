<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAndControl/Manager.Master" AutoEventWireup="true" CodeBehind="ManagerInfo.aspx.cs" Inherits="PurchasingSystem.SystemManger.ManagerInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="entry-content">
            <a class="nav-link active" aria-current="page" href="/SystemManger/ManagerInfo.aspx">管理員首頁</a><br />
        <a class="nav-link active" aria-current="page" href="/SystemManger/OrderListManager.aspx">訂單資料</a><br />
        <a class="nav-link active" aria-current="page" href="/SystemManger/UserList.aspx">使用者資料</a><br />
        <a class="nav-link active" aria-current="page" href="/SystemManger/ManagerList.aspx">管理員清單</a><br />

    </div>
</asp:Content>
