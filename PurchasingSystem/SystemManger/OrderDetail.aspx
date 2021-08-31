<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAndControl/Manager.Master" AutoEventWireup="true" CodeBehind="OrderDetail.aspx.cs" Inherits="PurchasingSystem.SystemManger.OrderDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="entry-content">
        <div>
            <h3>商品資料修改</h3>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="10" OnRowDataBound="GridView1_RowDataBound">
                <Columns>

                    <asp:BoundField DataField="Name" HeaderText="商品名" />


                    <asp:HyperLinkField DataNavigateUrlFields="URL" HeaderText="商品網址" DataTextField="URL" />


                    <asp:TemplateField HeaderText="商品價格">
                        <ItemTemplate>
                            <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("commodityID") %>' />
                            <asp:TextBox ID="txtPrice" runat="server" Text='<%# Bind("Price") %>' Width="150px" onkeyup="this.value=this.value.replace(/\D/g,'')"
                                onafterpaste="this.value=this.value.replace(/\D/g,'')"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="種類">
                        <ItemTemplate>
                            <asp:TextBox ID="txtType" runat="server" Text='<%# Bind("Type") %>' Width="150px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Quantity" HeaderText="數量" />
                    <asp:TemplateField HeaderText="購買狀況">
                        <ItemTemplate>
                            <asp:DropDownList ID="IsBuyDDList" runat="server" SelectedValue='<%# Eval("IsBuy") %>'>
                                <asp:ListItem Value="0">尚未購買</asp:ListItem>
                                <asp:ListItem Value="1">已購買</asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div>
            <h3>訂單資料修改</h3>
            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CellPadding="6">
                <Columns>
                    <asp:BoundField DataField="PriceSum" HeaderText="總價格" />
                    <asp:TemplateField HeaderText="付款金額">
                        <ItemTemplate>
                            <asp:TextBox ID="txtAmount" runat="server" Text='<%# Bind("Amount") %>' Width="150px" onkeyup="this.value=this.value.replace(/\D/g,'')"
                                onafterpaste="this.value=this.value.replace(/\D/g,'')"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="運費">
                        <ItemTemplate>
                            <asp:TextBox ID="txtShippingFee" runat="server" Text='<%# Bind("ShippingFee") %>' Width="150px" onkeyup="this.value=this.value.replace(/\D/g,'')"
                                onafterpaste="this.value=this.value.replace(/\D/g,'')"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="購買狀況">
                        <ItemTemplate>
                            <asp:DropDownList ID="IsBuyOrderDDList" runat="server" SelectedValue='<%# Eval("IsBuy") %>'>
                                <asp:ListItem Value="0">尚未購買</asp:ListItem>
                                <asp:ListItem Value="1">已購買</asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="寄送狀況">
                        <ItemTemplate>
                            <asp:DropDownList ID="IsSentOrderDDList" runat="server" SelectedValue='<%# Eval("IsSent") %>'>
                                <asp:ListItem Value="0">尚未寄送</asp:ListItem>
                                <asp:ListItem Value="1">已寄出</asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="訂單狀態">
                        <ItemTemplate>
                            <asp:DropDownList ID="OrderStatusDDList" runat="server" SelectedValue='<%# Eval("OrderStatus") %>'>
                                <asp:ListItem Value="0">未處理</asp:ListItem>
                                <asp:ListItem Value="1">未付款</asp:ListItem>
                                <asp:ListItem Value="2">處理中</asp:ListItem>
                                <asp:ListItem Value="3">已結案</asp:ListItem>
                                <asp:ListItem Value="-1">此訂單不成立</asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>

            </asp:GridView>
        </div>
              <asp:Button ID="btnSaveCommodity" runat="server" Text="儲存" OnClick="btnSaveCommodity_Click" />&nbsp
            <asp:Button ID="btnCancelCommodity" runat="server" Text="取消" OnClick="btnCancelCommodity_Click" />
    </div>

</asp:Content>
