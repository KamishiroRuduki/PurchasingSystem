<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAndControl/Main.Master" AutoEventWireup="true" CodeBehind="UserDetail.aspx.cs" Inherits="PurchasingSystem.SystemAdmin.UserDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="entry-content">
        帳號:<asp:Label ID="lblAccount" runat="server" ></asp:Label><br />
        姓名:<asp:TextBox ID="txtName" runat="server"></asp:TextBox><br />
        手機:<asp:TextBox ID="txtPhone" TextMode="Phone" runat="server"></asp:TextBox><br />
        Email:<asp:TextBox ID="txtMail" TextMode="Email" runat="server" Width="300px"></asp:TextBox><br />
        地址:<asp:TextBox ID="txtAddress" runat="server" Width="800px"></asp:TextBox><br />
        付款方式:<asp:DropDownList ID="payType" runat="server">
            <asp:ListItem Value="0">信用卡</asp:ListItem>
            <asp:ListItem Value="1">銀行轉帳</asp:ListItem>
        </asp:DropDownList><br />
        卡號/銀行帳號<asp:TextBox ID="txtPaymentProfile" runat="server" Width="300px"></asp:TextBox><br />
        <div>
            <asp:Button ID="btnSave" runat="server" Text="儲存變更" OnClick="btnSave_Click" />&nbsp
            <asp:Button ID="btnCancel" runat="server" Text="取消" OnClick="btnCancel_Click" />&nbsp
            <asp:Button ID="btnPwd" runat="server" Text="前往變更密碼" OnClick="btnPwd_Click" /><br/>
            <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
        </div>
    </div>
</asp:Content>
