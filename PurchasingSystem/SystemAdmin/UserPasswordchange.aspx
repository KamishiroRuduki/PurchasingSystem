<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAndControl/Main.Master" AutoEventWireup="true" CodeBehind="UserPasswordchange.aspx.cs" Inherits="PurchasingSystem.SystemAdmin.UserPasswordchange" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="entry-content">
        <table>
            <tr>
                <th>原密碼:</th>
                
                <td>
                    <asp:TextBox ID="txtPWD" TextMode="Password" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <th>新密碼:</th>
                
                <td>
                    <asp:TextBox ID="txtNewPWD" TextMode="Password" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <th>再次確認新密碼:</th>
                
                <td>
                    <asp:TextBox ID="txtCurretPWD" TextMode="Password" runat="server"></asp:TextBox></td>
            </tr>



        </table>
            <asp:Button ID="btnSave" runat="server" Text="儲存變更"  OnClick="btnSave_Click" OnClientClick="javascript:return confirm('確定變更密碼？');" />
            <asp:Button ID="btcCancel" runat="server" Text="取消" OnClick="btcCancel_Click" /><br/>
        <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
    </div>
</asp:Content>
