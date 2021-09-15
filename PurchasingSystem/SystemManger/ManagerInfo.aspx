<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAndControl/Manager.Master" AutoEventWireup="true" CodeBehind="ManagerInfo.aspx.cs" Inherits="PurchasingSystem.SystemManger.ManagerInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="entry-content">
        <table class="table">
            <tr>
                <td>
                    <a class="nav-link active" aria-current="page" href="/SystemManger/OrderListManager.aspx">訂單資料</a><br />
                    <a class="nav-link active" aria-current="page" href="/SystemManger/UserList.aspx">使用者資料</a><br />
                   &nbsp&nbsp&nbsp&nbsp<asp:Literal ID="litManagerList" runat="server" Visible ="false"></asp:Literal><br/>
                    
                </td>
                <td>
                    <asp:Label ID="lblName" runat="server" class="display-5" style="font-weight: bold"  ></asp:Label><br /><br />
                    <asp:Label ID="lblLevel" runat="server" class="display-5" style="font-weight: bold" ></asp:Label><br />
                    <asp:Literal ID="Literal3" runat="server"></asp:Literal><br />
                   
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
