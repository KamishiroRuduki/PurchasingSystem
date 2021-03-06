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
    /// 管理員首頁
    /// </summary>
    public partial class ManagerInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AuthManger.ManagerIsLogined())//檢查登入
            {
                Response.Redirect("/SystemManger/Login.aspx");
                return;
            }
            var cUser = AuthManger.GetCurrentManager();//讀取該管理員資訊
            if (cUser == null)
            {
                this.Session["ManagerLoginInfo"] = null;
                Response.Redirect("/SystemManger/Login.aspx");
                return;

            }
            this.litManagerList.Text = "<a href='/SystemManger/ManagerList.aspx'>管理員清單</a>";
            if (cUser.Level >= 2)//高階管理員才能看到管理員清單
                this.litManagerList.Visible = true;
            //管理員的名字跟等級
            this.lblName.Text = "姓名:" + cUser.Name;
            this.lblLevel.Text = "管理員等級:" + LevelText(cUser.Level.ToString());
        }

        private string LevelText( string value )
        {
                      
            if (value == "0")
                return "客服人員";
            else if (value == "1")
                return "一般管理員";
            else if (Convert.ToInt32(value) >= 2)
                return "高階管理員";
            else
                return string.Empty;

        }


    }
}