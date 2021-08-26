using PurchasingSystem.Auth;
using PurchasingSystem.DBSouce;
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

            var list = OrderManager.GETAllOrderInfo(cUser.UserGuid);
            if(list.Count > 0 )
            {
                this.OrderListView.DataSource = list;
                this.OrderListView.DataBind();
            }

        }

        protected void OrderListView_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
    }
}