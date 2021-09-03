<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAndControl/Manager.Master" AutoEventWireup="true" CodeBehind="OrderListManager.aspx.cs" Inherits="PurchasingSystem.SystemManger.OrderListManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="entry-content">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowDataBound="OrderListView_RowDataBound" CellPadding="10">
            <Columns>
                    <asp:HyperLinkField  HeaderText="顧客姓名" DataTextField="Name" DataNavigateUrlFormatString="\SystemManger\UserList.aspx?ID={0}" DataNavigateUrlFields="UserID" />
                <asp:TemplateField HeaderText="訂單狀態">
                    <ItemTemplate>
                        <asp:HiddenField ID="HiddenField1" runat="server" />
                        <asp:Label ID="lblstatus" runat="server" Text="Label"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="CreateDate" HeaderText="下單日期" />


                <asp:BoundField DataField="PriceSum" HeaderText="總價格" />
                <asp:BoundField DataField="Amount" HeaderText="付款金額" />
                <asp:TemplateField HeaderText="購買狀況">
                    <ItemTemplate>
                        <%# ((int)Eval("IsBuy") == 0) ? "未購買" : "已購買" %>
                    </ItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField HeaderText="寄送狀況">
                    <ItemTemplate>
                        <%# ((int)Eval("IsSent") == 0) ? "未寄出" : "已寄出" %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ShippingFee" HeaderText="運費" />
                <asp:BoundField DataField="Remarks" HeaderText="備註" />
                <asp:TemplateField HeaderText="訂單處理">
                    <ItemTemplate>
                        <a href="/SystemManger/OrderDetail.aspx?ID=<%# Eval("ID") %>">Edit</a>
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>

        </asp:GridView>
    </div>
</asp:Content>
