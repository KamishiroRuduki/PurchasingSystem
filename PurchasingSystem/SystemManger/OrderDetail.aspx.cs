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
            if (!IsPostBack)
            {
                if (this.Request.QueryString["ID"].ToString() != null)
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

                var txtamount = (TextBox)this.GridView2.Rows[0].FindControl("txtAmount");
                var txtShippingFee = (TextBox)this.GridView2.Rows[0].FindControl("txtShippingFee");               
                var isBuyOrder = (DropDownList)this.GridView2.Rows[0].FindControl("IsBuyOrderDDList");
                var isSentOrder = (DropDownList)this.GridView2.Rows[0].FindControl("IsSentOrderDDList");
                var orderStatus = (DropDownList)this.GridView2.Rows[0].FindControl("OrderStatusDDList");
                int amount = 0, shippingFee = 0;
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
                    IsBuy = Convert.ToInt32(isBuyOrder.SelectedValue),
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
    }
}