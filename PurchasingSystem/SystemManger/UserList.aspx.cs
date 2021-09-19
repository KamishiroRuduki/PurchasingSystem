using PurchasingSystem.Auth;
using PurchasingSystem.DBSouce;
using PurchasingSystem.Extensions;
using PurchasingSystem.ORM.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PurchasingSystem.SystemManger
{
    /// <summary>
    /// 使用者清單
    /// </summary>
    public partial class UserList : System.Web.UI.Page
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
                if (this.Request.QueryString["ID"] == null)//有ID就顯示該使用者的資訊，沒有就顯示所有使用者
                {
                    var list = UserInfoManager.GETUserInfoToList();
                    if (list.Count > 0)
                    {
                        this.GridView1.DataSource = list;
                        this.GridView1.DataBind();
                    }

                    if (cUser.Level >= 2)//高級管理員以上才能使用黑名單功能
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            var test = (Button)this.GridView1.Rows[i].FindControl("btnBlackList");
                            test.Visible = true;
                        }
                    }
                }
                else
                {
                    string idtext = this.Request.QueryString["ID"];
                    var list = UserInfoManager.GETUserInfoAccount(idtext.ToGuid());
                    if (list.Count > 0)
                    {
                        this.GridView1.DataSource = list;
                        this.GridView1.DataBind();
                    }

                    if (cUser.Level >= 2)//高級管理員以上才能使用黑名單功能
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            var test = (Button)this.GridView1.Rows[i].FindControl("btnBlackList");
                            test.Visible = true;
                        }
                    }
                }
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //付款資訊的顯示文字
            var row = e.Row;
            if (row.RowType == DataControlRowType.DataRow)
            {
                Label lbl = row.FindControl("lblType") as Label;
                //Literal ltl = row.FindControl("ltlActType") as Literal;
                var dr = row.DataItem as UserInfo;
                int paymentType = dr.PaymentType;

                if (paymentType == 0)
                {
                    lbl.Text = "信用卡";
                }

                else
                {
                    lbl.Text = "銀行轉帳";
                }



            }
        }

        protected void btnBlackList_Click(object sender, EventArgs e)
        {

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var cUser = AuthManger.GetCurrentManager();
            if (cUser.Level >= 2)
            {


                if (e.CommandName == "BlackList")
                {

                    var custAccount = e.CommandArgument.ToString();
                    // var thisOrder = UserInfoManager.GETUserInfoAccount(custAccount);

                    UserInfoManager.UpdateUserToBlackList(custAccount);
                    Response.Redirect("/SystemManger/UserList.aspx");


                }
            }
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
    }
}