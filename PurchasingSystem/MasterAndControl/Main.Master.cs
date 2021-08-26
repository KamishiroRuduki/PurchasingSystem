using PurchasingSystem.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PurchasingSystem.MasterAndControl
{
    public partial class Main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.litLogin.Text = "<a href='Login.aspx'>註冊/登入</a>";
            if (AuthManger.IsLogined())
            {
                this.litLogin.Visible = false;
                this.btnLogout.Visible = true;

            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            AuthManger.Logout();
            Response.Redirect("/Default1.aspx");
        }
    }
}