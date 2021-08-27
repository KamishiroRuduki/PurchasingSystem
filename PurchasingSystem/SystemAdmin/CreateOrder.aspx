<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAndControl/Main.Master" AutoEventWireup="true" CodeBehind="CreateOrder.aspx.cs" Inherits="PurchasingSystem.SystemAdmin.CreateOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 87px;
            height: 13px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script>
        $(function () {
            $("#divCalculation").hide();
            $("#addcommod").click(function () {
                var htmltext =
            `<br/>商品名:<input type="Text" ID="txtName1" name="txtName" />
            數量:<input type="number" name="txtQuantity" ID="txtQuantity1"  onkeyup="this.value=this.value.replace(/\D/g,'')"
onafterpaste="this.value=this.value.replace(/\D/g,'')"  size="10" /><br/>
            商品網址:<input type="Text" name ="txtURL" ID="txtURL1"/><br/>`;
                $("#commoditys").append(htmltext);

            })
            $("#btn1").click(function () {
                $("#divCalculation").show(300);
            })
            $("#btn2").click(function () {
                var amount = $("#sum").val();
                var exRate = $("#ContentPlaceHolder1_HF2").val();
                var purchaseCost = parseInt($("#ContentPlaceHolder1_HF3").val()) ;
                var dec = Decimal.mul(amount, exRate);
                var num = parseInt(dec.toFixed(), 10) + purchaseCost;
                $("#ContentPlaceHolder1_txtPrice").val(num);
                $("#ContentPlaceHolder1_HiddenField1").val(num);
                $("#divCalculation").hide(300);
            })

        });

    </script>
    <div class="entry-content">
        <div id="commoditys">
            <!--   商品名:<asp:TextBox ID="txtName" runat="server"></asp:TextBox>
            數量:<asp:TextBox ID="txtQuantity" TextMode="Number" runat="server" Width="50px"></asp:TextBox><br/>
            商品網址:<asp:TextBox ID="txtURL" runat="server"></asp:TextBox><br/>-->
        </div>
        <button type="button" id="addcommod">新增商品</button>
        <div>
            總價格:<asp:HiddenField ID="HiddenField1" runat="server" />
            <asp:TextBox  ID="txtPrice" runat="server" Enabled="False"></asp:TextBox>
            <button type="button" id="btn1">價格試算</button>
        </div>
        <div id="divCalculation">
            全部商品價格加總後金額(日幣)<input type="number" id="sum" onkeyup="this.value=this.value.replace(/\D/g,'')"
                onafterpaste="this.value=this.value.replace(/\D/g,'')" />
            <button type="button" id="btn2">確定</button><br />
        </div>
         備註:<asp:TextBox ID="txtRemarks" runat="server" Height="70px" Width="420px"></asp:TextBox><br />
        <asp:Button ID="btnsave" runat="server" Text="送出" OnClick="btnsave_Click" /><br />
        <asp:Literal ID="ltmsg" runat="server"></asp:Literal>
        <asp:HiddenField ID="HF2" runat="server" />
        <asp:HiddenField ID="HF3" runat="server" />
    </div>
</asp:Content>
