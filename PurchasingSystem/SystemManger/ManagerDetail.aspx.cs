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
    public partial class ManagerDetail : System.Web.UI.Page
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
            if (cUser.Level < 2)
            {
                Response.Redirect("/SystemManger/ManagerInfo.aspx");
                return;
            }
            if (!IsPostBack)
            {
                if (this.Request.QueryString["ID"] != null)
                {
                    var userID = this.Request.QueryString["ID"].ToString();
                    var manager = ManagerInfoManager.GETManagerInfoAccountByUserID(userID.ToGuid());
                    this.lblAccount.Text = manager.Account;
                    this.lblName.Text = manager.Name;
                    this.lvDDList.SelectedValue = manager.Level.ToString();

                }
                else
                {
                    this.txtAccount.Visible = true;
                    this.lblAccount.Visible = false;
                    this.txtName.Visible = true;
                    this.lblName.Visible = false;
                    this.txtPWD.Visible = false;
                    this.lblPWD.Visible = true;
                    this.btnDel.Visible = false;
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

           // string idtext = this.Request.QueryString["ID"];
            if (this.Request.QueryString["ID"] != null)
            {
                if(string.IsNullOrWhiteSpace(this.txtPWD.Text) )
                {
                    this.ltMsg.Text = "請輸入新密碼";
                    return;
                }
                ManagerInfoManager.UpdateManager(this.lblAccount.Text, this.txtPWD.Text, Convert.ToInt32(this.lvDDList.SelectedValue));
               
            }
            else
            {
                if(! ManagerInfoManager.IsAccountCreated(this.txtAccount.Text))
                {
                    this.ltMsg.Text = "此帳號已經被使用過了";
                    return;
                }
                if (string.IsNullOrWhiteSpace(this.txtName.Text))
                {
                    this.ltMsg.Text = "請輸入姓名";
                    return;
                }
                Manager newManager = new Manager()
                {
                    UserID = Guid.NewGuid(),
                    Account = this.txtAccount.Text,
                    Name = this.txtName.Text,
                    Password = "123",
                    Level = Convert.ToInt32(this.lvDDList.SelectedValue),

                };
                ManagerInfoManager.CreateManager(newManager);
               
            }
            Response.Redirect("/SystemManger/ManagerList.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("/SystemManger/ManagerList.aspx");
            return;
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            ManagerInfoManager.DelManager(this.lblAccount.Text);
            Response.Redirect("/SystemManger/ManagerList.aspx");
        }
    }
}
