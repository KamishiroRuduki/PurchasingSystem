<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAndControl/Main.Master" AutoEventWireup="true" CodeBehind="OrderList.aspx.cs" Inherits="PurchasingSystem.SystemAdmin.OrderList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="entry-content">
        <asp:GridView ID="OrderListView" runat="server" AutoGenerateColumns="False" OnRowDataBound="OrderListView_RowDataBound" CellPadding="4">
            <Columns>
                <asp:BoundField DataField="CreateDate" HeaderText="下單日期" />


                <asp:BoundField DataField="PriceSum" HeaderText="總價格" />
                <asp:BoundField DataField="Amount" HeaderText="付款金額" />


                <asp:TemplateField HeaderText="購買狀況"></asp:TemplateField>



                <asp:TemplateField HeaderText="寄送狀況"></asp:TemplateField>



               <asp:BoundField DataField="ShippingFee" HeaderText="運費" />
                <asp:BoundField DataField="Remarks" HeaderText="備註" />



            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
