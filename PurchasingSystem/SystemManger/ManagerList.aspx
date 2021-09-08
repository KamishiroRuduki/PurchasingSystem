<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAndControl/Manager.Master" AutoEventWireup="true" CodeBehind="ManagerList.aspx.cs" Inherits="PurchasingSystem.SystemManger.ManagerList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="entry-content">
        <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowDataBound="OrderListView_RowDataBound" CellPadding="10">
            <Columns>
                <asp:BoundField DataField="Name" HeaderText="姓名" />


                <asp:BoundField DataField="Account" HeaderText="帳號" />

                <asp:TemplateField HeaderText="管理員等級">
                    <ItemTemplate>
                        <asp:Label ID="lbl" runat="server" Text="Label"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="帳號管理">
                    <ItemTemplate>
                        <a href="/SystemManger/ManagerDetail.aspx?ID=<%# Eval("UserID") %>">Edit</a>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>

        </asp:GridView>
            <a href="/SystemManger/ManagerDetail.aspx">新增管理員</a>
            </div>
        <div>
            <h2>匯率、代購費變更</h2>
            匯率:&nbsp&nbsp&nbsp<asp:TextBox ID="txtCashRate" runat="server"  ></asp:TextBox><br />
            代購費:<asp:TextBox ID="txtPurchasingCost" runat="server" onkeyup="this.value=this.value.replace(/\D/g,'')"
onafterpaste="this.value=this.value.replace(/\D/g,'')"></asp:TextBox><br />
            <asp:Button ID="btnSave" runat="server" Text="儲存變更" OnClick="btnSave_Click" />&nbsp
            <asp:Button ID="btnCancel" runat="server" Text="取消" OnClick="btnCancel_Click" />

        </div>
    </div>
</asp:Content>
