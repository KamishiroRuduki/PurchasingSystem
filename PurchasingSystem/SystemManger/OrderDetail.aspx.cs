using PurchasingSystem.Auth;
using PurchasingSystem.DBSouce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PurchasingSystem.ORM.DBModels;

namespace PurchasingSystem.SystemManger
{
    
    public partial class OrderDetail : System.Web.UI.Page
    {
   
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AuthManger.ManagerIsLogined())
            {
                Response.Redirect("/SystemManger/Login.aspx");
                return;
            }
            var cUser = AuthManger.GetCurrentManager();
            if (cUser == null)
            {
                this.Session["ManagerLoginInfo"] = null;
                Response.Redirect("/SystemManger/Login.aspx");
                return;

            }
            if (cUser.Level < 1)
            {
                Response.Redirect("/SystemManger/ManagerInfo.aspx");
                return;
            }
            if (!IsPostBack)
            {
                if (this.Request.QueryString["ID"] != null)
                {
                    var strID = this.Request.QueryString["ID"].ToString();
                    int id;
                    if (int.TryParse(strID, out id))
                    {
                        var list = CommodityManager.GETCommodityInfo(id);
                        var order = OrderManager.GETOrderInfoToList(id);
                        if (list.Count > 0)
                        {
                            this.GridView1.DataSource = list;
                            this.GridView1.DataBind();
                        }
                        if (order != null)
                        {

                            this.GridView2.DataSource = order;
                            this.GridView2.DataBind();
                        }
                    }
                }
                else
                {
                    Response.Redirect("/SystemManger/OrderListManager.aspx");
                }
            }
        }

        protected void btnSaveCommodity_Click(object sender, EventArgs e)
        {
            if (this.GridView1.Rows.Count > 0)
            {
                for (int i = 0; i < this.GridView1.Rows.Count; i++)
                {
                    var txtPrice = (TextBox)this.GridView1.Rows[i].FindControl("txtPrice");
                    var type = (TextBox)this.GridView1.Rows[i].FindControl("txtType");
                    var id = (HiddenField)this.GridView1.Rows[i].FindControl("HiddenField1");
                    var isBuy = (DropDownList)this.GridView1.Rows[i].FindControl("IsBuyDDList");
                    int price = 0;
                    if (!string.IsNullOrWhiteSpace(txtPrice.Text))
                    {
                        price = Convert.ToInt32(txtPrice.Text);
                    }

                    CommodityManager.UpdateCommodity(price, type.Text, Convert.ToInt32(isBuy.SelectedValue), Convert.ToInt32(id.Value));
                }
            }

            if (this.GridView2.Rows.Count == 1 && this.Request.QueryString["ID"] != null)
            {
                var txtprice = (TextBox)this.GridView2.Rows[0].FindControl("txtPrice");
                var txtamount = (TextBox)this.GridView2.Rows[0].FindControl("txtAmount");
                var txtShippingFee = (TextBox)this.GridView2.Rows[0].FindControl("txtShippingFee");
                // var isBuyOrder = (DropDownList)this.GridView2.Rows[0].FindControl("IsBuyOrderDDList");
               // var isBuyOrder = (Literal)this.GridView2.Rows[0].FindControl("litOrderIsbuy");
                var isSentOrder = (DropDownList)this.GridView2.Rows[0].FindControl("IsSentOrderDDList");
                var orderStatus = (DropDownList)this.GridView2.Rows[0].FindControl("OrderStatusDDList");
                int amount = 0, shippingFee = 0;
                if(!string.IsNullOrWhiteSpace(txtamount.Text) && txtamount.Text != "0" )//有付款後才檢查
                {
                    var amountTest = Convert.ToDecimal(txtamount.Text);
                    var priceText = Convert.ToDecimal(txtprice.Text);
                    priceText = Decimal.Multiply(priceText, (decimal)0.9);
                    if (priceText> amountTest)
                    {
                        Response.Write("<Script language='JavaScript'>alert('付款金額不正確');  </Script>");
                        return;
                    }
                }
                if (!string.IsNullOrWhiteSpace(txtamount.Text))
                {
                    amount = Convert.ToInt32(txtamount.Text);
                }
                if (!string.IsNullOrWhiteSpace(txtShippingFee.Text))
                {
                    shippingFee = Convert.ToInt32(txtShippingFee.Text);
                }
                Order orderUpdate = new Order()
                {
                    ID = Convert.ToInt32(this.Request.QueryString["ID"].ToString()),
                    Amount = amount,
                    ShippingFee = shippingFee,
                    // IsBuy = Convert.ToInt32(isBuyOrder.SelectedValue),
                    IsBuy = OrderIsBuyValue(),
                    IsSent = Convert.ToInt32(isSentOrder.SelectedValue),
                    OrderStatus = Convert.ToInt32(orderStatus.SelectedValue)
                };

                OrderManager.UpdateOrderByManager(orderUpdate);

            }
            Response.Redirect("/SystemManger/OrderListManager.aspx");
            return;

        }

        protected void btnCancelCommodity_Click(object sender, EventArgs e)
        {
            Response.Redirect("/SystemManger/OrderListManager.aspx");
            return;
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    DropDownList DropDownList1 = (e.Row.FindControl("IsBuyDDList") as DropDownList);
            //    if (this.Request.QueryString["ID"].ToString() != null)
            //    {
            //        var strID = this.Request.QueryString["ID"].ToString();
            //        int id;
            //        if (int.TryParse(strID, out id))
            //        {
            //            var list = CommodityManager.GETCommodityInfo(id);
            //            if (list.Count > 0)
            //            {
            //                DropDownList1.DataSource = list;
            //               // DropDownList1.DataTextField = "IsBuy";
            //                DropDownList1.DataValueField = "IsBuy";
            //                DropDownList1.DataBind();

            //            }
            //        }


            //    }

            //  }
        }

        protected void IsBuyDDList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.GridView1.Rows.Count > 0)
            {
                int j = this.GridView1.Rows.Count;
                for (int i = 0; i < this.GridView1.Rows.Count; i++)
                {
                   
                    var isBuy = (DropDownList)this.GridView1.Rows[i].FindControl("IsBuyDDList");
                    if (isBuy.SelectedValue == "1")
                        j--;

                }
                var isBuyOrder = (Literal)this.GridView2.Rows[0].FindControl("litOrderIsbuy");
                if (j == 0)                 
                    isBuyOrder.Text = "已購買";
                else
                    isBuyOrder.Text = "尚未購買";

            }
        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var row = e.Row;
            if (row.RowType == DataControlRowType.DataRow)
            {
                Literal lbl = row.FindControl("litOrderIsbuy") as Literal;
                var dr = row.DataItem as Order;
                int orderStatus = dr.IsBuy;

                if (orderStatus == 1)
                {
                    lbl.Text = "已購買";
                }

                else 
                {

                    lbl.Text = "尚未購買";
                }


            }
        }
        private int OrderIsBuyValue()
        {
            var isBuyOrder = (Literal)this.GridView2.Rows[0].FindControl("litOrderIsbuy");
            if (isBuyOrder.Text == "已購買")
                return 1;
            else
                return 0;
            
        }

        private void Cost()
        {
            var isBuyOrder = (Literal)this.GridView2.Rows[0].FindControl("litOrderIsbuy");
         //   if (isBuyOrder.Text == "已購買")



        }

        protected void btnCalculation_Click(object sender, EventArgs e)
        {
            var costDataList = OrderManager.GetCostData();
            var cashrate = costDataList[0].Value;
            var purchasingcost = (int)costDataList[1].Value;
            if (this.GridView1.Rows.Count > 0)
            {
                decimal priceSum = 0;          
                for (int i = 0; i < this.GridView1.Rows.Count; i++)
                {

                    var txtPrice = (TextBox)this.GridView1.Rows[i].FindControl("txtPrice");
                    if (txtPrice.Text == null || txtPrice.Text =="0")
                        return;
                    else
                        priceSum += Convert.ToDecimal(txtPrice.Text);

                }
                var priceTW = (int)(priceSum * cashrate) + purchasingcost;
                this.txtPrice.Text = priceTW.ToString();
                var strID = this.Request.QueryString["ID"].ToString();
                int id;
                if (int.TryParse(strID, out id))
                {                    
                    var order = OrderManager.GETOrderInfo(id);
                    if (priceTW != order.PriceSum)
                        Response.Write("<Script language='JavaScript'>alert('訂單金額不正確');  </Script>");
                }
                   

            }

        }
    }
}