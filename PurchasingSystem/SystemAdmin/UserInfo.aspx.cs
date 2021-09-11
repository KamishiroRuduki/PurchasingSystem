using PurchasingSystem.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PurchasingSystem.SystemAdmin
{
    public partial class UserInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AuthManger.IsLogined())
            {
                Response.Redirect("/Login.aspx");
                return;
            }
            var cUser = AuthManger.GetCurrentUser();
            if (cUser == null)
            {
                this.Session["UserLoginInfo"] = null;
                Response.Redirect("/Login.aspx");
                return;

            }
            if(cUser.BlackList != 0 )
            {
                AuthManger.Logout();
                Response.Write("<Script language='JavaScript'>alert('此帳號在黑名單內！'); location.href='/default.aspx'; </Script>");
               // Response.Redirect("/SystemAdmin/UserInfo.aspx");
            }
        }
    }
}