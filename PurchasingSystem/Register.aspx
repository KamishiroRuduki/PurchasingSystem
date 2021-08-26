<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAndControl/Main.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="PurchasingSystem.Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="entry-content">
        帳號:<asp:TextBox ID="txtAccount" runat="server" Width="300px"></asp:TextBox><br />
        密碼:<asp:TextBox ID="txtPassword" TextMode="Password" runat="server" Width="300px"></asp:TextBox><br />
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
            <asp:Button ID="btnSave" runat="server" Text="建立使用者" OnClick="btnSave_Click" /><br/>
            <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
        </div>
    </div>
    
</asp:Content>
