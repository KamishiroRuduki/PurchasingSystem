<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAndControl/Main.Master" AutoEventWireup="true"  enableEventValidation="false" CodeBehind="OrderList.aspx.cs" Inherits="PurchasingSystem.SystemAdmin.OrderList"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script>
        $(function () {
            $("#ordercontent").hide();
            $(".btncontent").click(function () {
                $("#ordercontent").empty();
                var ID = this.value;
                var strURL = "http://localhost:4836/Handlers/OrderListHandler.ashx?OrderID=" + ID;
                $.ajax({
                    url: strURL,
                    type: "GET",
                    data: {},
                    success: function (result) {
                        var table = '<table class="table table-striped">';
                        table += '<tr> <th>類型</th> <th>商品名</th> <th>數量</th> <th>含稅日幣</th> </tr>';
                        for (var i = 0; i < result.length; i++) {
                            var obj = result[i];
                            if (obj.Type == null)
                                obj.Type = "";
                            if (obj.Price == null)
                                obj.Price = "";
                            var htmlText =
                                `<tr>
                                <td>${obj.Type}</td>
                                <td>${obj.Name}</td>
                                <td>${obj.Quantity}</td>
                                <td>${obj.Price}</td>
                            </tr>`;
                            table += htmlText;
                        }
                        table += `<tr> <td></td> <td></td> <td>日幣匯率</td> <td>${obj.CashRate}</td> </tr>
                                  <tr> <td></td> <td></td> <td>代購費</td> <td>${obj.PurchasingCost}</td> </tr>
                                  </table>`;
                        $("#ordercontent").append(table);
                    }
                });

                $("#ordercontent").show(300);
            });
         /*   $(".btnCacel").click(function () {
                var ID = this.CommandArgument;
                $.session.set("Cancel",ID);

            });*/
        });

    </script>
    <div class="entry-content">
        <div id="ordercontent"></div>

        <asp:GridView ID="OrderListView" runat="server" AutoGenerateColumns="False" class="table table-bordered table-condensed table-responsive table-hover"  OnRowDataBound="OrderListView_RowDataBound" CellPadding="20" OnSelectedIndexChanged="OrderListView_SelectedIndexChanged" OnRowCommand="OrderListView_RowCommand" OnRowCancelingEdit="OrderListView_RowCancelingEdit" OnRowDeleting="OrderListView_RowDeleting" OnRowEditing="OrderListView_RowEditing">
            <Columns>
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



                <asp:TemplateField HeaderText="訂單狀態">
                    <ItemTemplate>
                        <asp:HiddenField ID="HiddenField1" runat="server" />
                        <asp:Label ID="lblstatus" runat="server" Text="Label"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>




                <asp:TemplateField HeaderText="訂單詳細">
                    <ItemTemplate>                       
                        <button type="button" id="btncontent" Value='<%# Eval("ID") %>' class="btncontent">詳細</button>
                    </ItemTemplate>
                </asp:TemplateField>

            
                <asp:TemplateField HeaderText="取消訂單">
                    <ItemTemplate>
                        <asp:Button ID="btnCacel" runat="server" Text="取消" Visible='<%# (Int32.Parse(Eval("OrderStatus").ToString()) >= 0 && Int32.Parse(Eval("OrderStatus").ToString()) <= 1 && Int32.Parse(Eval("IsBuy").ToString()) == 0) %>' OnClick="btnCacel_Click1" OnClientClick="javascript:return confirm('確定取消？');"  CommandName="Cancel"  CommandArgument='<%# Eval("ID") %>'  />
                    </ItemTemplate>
                </asp:TemplateField>


            </Columns>
        </asp:GridView>
    </div>
    
</asp:Content>
