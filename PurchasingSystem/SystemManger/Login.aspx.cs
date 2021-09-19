using PurchasingSystem.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PurchasingSystem.SystemManger
{
    /// <summary>
    /// 管理員登入頁面
    /// </summary>
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["ManagerLoginInfo"] != null)//檢查登入
            {
                this.plcLogin.Visible = false;
                Response.Redirect("/SystemManger/ManagerInfo.aspx");
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {


            string inp_Account = this.txtAccount.Text;
            string inp_PWD = this.txtPassword.Text;
            string Msg;
            if (!AuthManger.TryManagerLogin(inp_Account, inp_PWD, out Msg))//管理員登入
            {
                this.ltMsg.Text = Msg;
                return;
            }
            Response.Redirect("/SystemManger/ManagerInfo.aspx");

        }
    }
}