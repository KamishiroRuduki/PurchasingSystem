<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAndControl/Manager.Master" AutoEventWireup="true" CodeBehind="ManagerDetail.aspx.cs" Inherits="PurchasingSystem.SystemManger.ManagerDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
            <div class="entry-content">
        帳號:<asp:Label ID="lblAccount" runat="server" ></asp:Label>
             <asp:TextBox ID="txtAccount" runat="server" Visible="false"></asp:TextBox><br />
        姓名:<asp:Label ID="lblName" runat="server" ></asp:Label>
              <asp:TextBox ID="txtName" runat="server" Visible="false"></asp:TextBox><br />
        密碼:<asp:Label ID="lblPWD" runat="server" Text="預設為123" Visible="false"></asp:Label>
                <asp:TextBox ID="txtPWD" TextMode="Password" runat="server"></asp:TextBox><br />
        管理員等級:<asp:DropDownList ID="lvDDList" runat="server">
            <asp:ListItem Value="0">客服人員</asp:ListItem>
            <asp:ListItem Value="1">一般管理員</asp:ListItem>
            <asp:ListItem Value="2">高級管理員</asp:ListItem>
        </asp:DropDownList><br />
        <div>
            <asp:Button ID="btnSave" runat="server" Text="儲存變更" OnClick="btnSave_Click" />&nbsp
            <asp:Button ID="btnCancel" runat="server" Text="取消" OnClick="btnCancel_Click" />&nbsp
            <asp:Button ID="btnDel" runat="server" Text="刪除此管理員" OnClick="btnDel_Click" />
            <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
        </div>
    </div>
</asp:Content>
