using PurchasingSystem.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PurchasingSystem
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["UserLoginInfo"] != null)
            {
                this.plcLogin.Visible = false;
                Response.Redirect("/SystemAdmin/UserInfo.aspx");
            }

            else
                this.plcLogin.Visible = true;
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {


            string inp_Account = this.txtAccount.Text;
            string inp_PWD = this.txtPassword.Text;
            string Msg;
            if (!AuthManger.TryLogin(inp_Account, inp_PWD, out Msg))
            {
                this.ltMsg.Text = Msg;
                return;
            }
            Response.Redirect("/SystemAdmin/UserInfo.aspx");

        }
    }
}