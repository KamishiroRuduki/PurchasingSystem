using PurchasingSystem.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PurchasingSystem.SystemAdmin
{
    /// <summary>
    /// 會員首頁
    /// </summary>
    public partial class UserInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AuthManger.IsLogined())//檢查登入
            {
                Response.Redirect("/Login.aspx");
                return;
            }
            var cUser = AuthManger.GetCurrentUser();//讀取該使用者資訊
            if (cUser == null)
            {
                this.Session["UserLoginInfo"] = null;
                Response.Redirect("/Login.aspx");
                return;

            }
            if(cUser.BlackList != 0 )//如果此帳號為黑名單，將此帳號登出並跳至系統首頁
            {
                AuthManger.Logout();
                Response.Write("<Script language='JavaScript'>alert('此帳號在黑名單內！'); location.href='/default.aspx'; </Script>");
               // Response.Redirect("/SystemAdmin/UserInfo.aspx");
            }
        }
    }
}