using PurchasingSystem.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PurchasingSystem.MasterAndControl
{
    public partial class Manager : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            this.litManagerInfo.Text = "<a href='/SystemManger/ManagerInfo.aspx'>管理員首頁</a>";
            if (AuthManger.ManagerIsLogined())
            {
                this.litLogin.Visible = false;
                this.btnLogout.Visible = true;

            }
        }
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            AuthManger.ManagerLogout();
            Response.Redirect("/SystemManger/Login.aspx");
        }

    }
}