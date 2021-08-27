<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAndControl/Main.Master" AutoEventWireup="true" CodeBehind="OrderList.aspx.cs" Inherits="PurchasingSystem.SystemAdmin.OrderList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script>
        $(function () {
            $("#ordercontent").hide();
            $(".btncontent").click(function () {
                $("#ordercontent").empty();
               // var ID = $("#ContentPlaceHolder1_OrderListView_HF1_1").val();
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
                            var htmlText =
                                `<tr>
                                <td>${obj.Type}</td>
                                <td>${obj.Name}</td>
                                <td>${obj.Quantity}</td>
                                <td>${obj.Price}</td>
                            </tr>`;
                            table += htmlText;
                        }
                        table += `<tr> <td></td> <td></td> <td>日幣匯率</td> <td>0.296</th> </tr>
                                  <tr> <td></td> <td></td> <td>代購費</td> <td>200</th> </tr>
                                  </table>`;
                        $("#ordercontent").append(table);
                    }
                });

                $("#ordercontent").show(300);
            });



        });

    </script>
    <div class="entry-content">
        <div id="ordercontent"></div>

        <asp:GridView ID="OrderListView" runat="server" AutoGenerateColumns="False" OnRowDataBound="OrderListView_RowDataBound" CellPadding="20">
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
                        <asp:Label ID="lblstatus" runat="server" Text="Label"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>




                <asp:TemplateField HeaderText="訂單詳細">
                    <ItemTemplate>                       
                        <button type="button" id="btncontent" Value='<%# Eval("ID") %>' class="btncontent"></button>
                    </ItemTemplate>
                </asp:TemplateField>


            </Columns>
        </asp:GridView>

        <asp:HiddenField ID="HF1" runat="server"/>
        <asp:HiddenField ID="HF2" runat="server" />
    </div>
</asp:Content>
