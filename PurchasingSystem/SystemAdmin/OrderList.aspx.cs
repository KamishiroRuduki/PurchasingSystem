using PurchasingSystem.Auth;
using PurchasingSystem.DBSouce;
using PurchasingSystem.ORM.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PurchasingSystem.SystemAdmin
{
    public partial class OrderList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AuthManger.IsLogined())
            {
                Response.Redirect("/login.aspx");
                return;
            }
            var cUser = AuthManger.GetCurrentUser();
            if (cUser == null)
            {
                this.Session["UserLoginInfo"] = null;
                Response.Redirect("/Login.aspx");
                return;

            }
            var costDataList = OrderManager.GetCostData();
            this.HF2.Value = costDataList[0].Value.ToString();
            var integer = (int)costDataList[1].Value;
            this.HF3.Value = integer.ToString();

            var list = OrderManager.GETAllOrderInfo(cUser.UserGuid);
            if(list.Count > 0 )
            {
                this.OrderListView.DataSource = list;
                this.OrderListView.DataBind();
            }

        }

        protected void OrderListView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var row = e.Row;
            if (row.RowType == DataControlRowType.DataRow)
            {
                Label lbl = row.FindControl("lblstatus") as Label;
                var dr = row.DataItem as Order;
                int orderStatus = dr.OrderStatus;

                if (orderStatus == 0)
                {
                    // ltl.Text = "支出";
                    lbl.Text = "未處理";
                }

                else if (orderStatus == 1)
                {
                    //   ltl.Text = "收入";
                    lbl.Text = "未付款";
                }

                else if (orderStatus == 2)
                {
                    //   ltl.Text = "收入";
                    lbl.Text = "處理中";
                }

                else if (orderStatus == 3)
                {
                    //   ltl.Text = "收入";
                    lbl.Text = "已結案";
                }
                else if (orderStatus == -1)
                {
                    //   ltl.Text = "收入";
                    lbl.Text = "此訂單不成立";
                }
            }
        }
    }
}