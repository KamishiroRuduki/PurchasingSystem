using PurchasingSystem.Auth;
using PurchasingSystem.DBSouce;
using PurchasingSystem.ORM.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PurchasingSystem.SystemManger
{
    public partial class OrderListManager : System.Web.UI.Page
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

                var list = OrderManager.GETOrderInfoByManager();
                if (list.Count > 0)
                {
                    this.GridView1.DataSource = list;
                    this.GridView1.DataBind();
                }
            }
        }
        protected void OrderListView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var row = e.Row;
            if (row.RowType == DataControlRowType.DataRow)
            {
                Label lbl = row.FindControl("lblstatus") as Label;
                var dr = row.DataItem as OrderModel;
                int orderStatus = dr.OrderStatus;

                if (orderStatus == 0)
                {
                    
                    lbl.Text = "未處理";
                }

                else if (orderStatus == 1)
                {
                    
                    lbl.Text = "未付款";
                }

                else if (orderStatus == 2)
                {
                    
                    lbl.Text = "處理中";
                }

                else if (orderStatus == 3)
                {
                    
                    lbl.Text = "已結案";
                }
                else if (orderStatus == -1)
                {
                   
                    lbl.Text = "此訂單不成立";
                }
            }
        }

        protected void statusDDList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.statusDDList.SelectedValue == "0")
            {
                int status = Convert.ToInt32(this.statusDDList.SelectedValue);
                var list = OrderManager.GETOrderInfoByManager(status);
                if (list.Count > 0)
                {
                    this.GridView1.DataSource = list;
                    this.GridView1.DataBind();
                }
            }
            else if (this.statusDDList.SelectedValue == "1")
            {
                int status = Convert.ToInt32(this.statusDDList.SelectedValue);
                var list = OrderManager.GETOrderInfoByManager(status);
                if (list.Count > 0)
                {
                    this.GridView1.DataSource = list;
                    this.GridView1.DataBind();
                }
            }
            else if (this.statusDDList.SelectedValue == "2")
            {
                int status = Convert.ToInt32(this.statusDDList.SelectedValue);
                var list = OrderManager.GETOrderInfoByManager(status);
                if (list.Count > 0)
                {
                    this.GridView1.DataSource = list;
                    this.GridView1.DataBind();
                }
            }
            else if (this.statusDDList.SelectedValue == "3")
            {
                int status = Convert.ToInt32(this.statusDDList.SelectedValue);
                var list = OrderManager.GETOrderInfoByManager(status);
                if (list.Count > 0)
                {
                    this.GridView1.DataSource = list;
                    this.GridView1.DataBind();
                }
            }
            else if (this.statusDDList.SelectedValue == "-1")
            {
                int status = Convert.ToInt32(this.statusDDList.SelectedValue);
                var list = OrderManager.GETOrderInfoByManager(status);
                if (list.Count > 0)
                {
                    this.GridView1.DataSource = list;
                    this.GridView1.DataBind();
                }
            }
            else
            {
                var list = OrderManager.GETOrderInfoByManager();
                if (list.Count > 0)
                {
                    this.GridView1.DataSource = list;
                    this.GridView1.DataBind();
                }
            }

        }
    }
}