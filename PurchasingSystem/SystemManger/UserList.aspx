<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAndControl/Manager.Master" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="PurchasingSystem.SystemManger.UserList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="entry-content">
        <div>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="5" OnRowDataBound="GridView1_RowDataBound" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowCommand="GridView1_RowCommand" OnRowDeleting="GridView1_RowDeleting" >
                <Columns>


                    <asp:BoundField DataField="Name" HeaderText="姓名" />
                    <asp:BoundField DataField="MobilePhone" HeaderText="手機" />
                    <asp:BoundField DataField="Email" HeaderText="Email" />
                    <asp:BoundField DataField="Address" HeaderText="地址" />
                    <asp:TemplateField HeaderText="付款方式">
                        <ItemTemplate>
                         <asp:Label ID="lblType" runat="server" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="PaymentProfile" HeaderText="付款資料" />
                    <asp:TemplateField HeaderText="加入黑名單">
                        <ItemTemplate>
                            <asp:Button ID="btnBlackList" runat="server" Text="Button" OnClick="btnBlackList_Click" OnClientClick="javascript:return confirm('確定將此使用者加入黑名單?？');" Visible="false" CommandName="BlackList"  CommandArgument='<%# Eval("Account") %>'/>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
            </div>
            </div>
</asp:Content>
