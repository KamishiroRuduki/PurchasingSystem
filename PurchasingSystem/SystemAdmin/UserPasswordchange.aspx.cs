using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PurchasingSystem.Auth;
using PurchasingSystem.DBSouce;
using PurchasingSystem.ORM.DBModels;

namespace PurchasingSystem.SystemAdmin
{
    public partial class UserPasswordchange : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!AuthManger.IsLogined())
            //{
            //    Response.Redirect("/Login.aspx");
            //    return;
            //}
            //var cUser = AuthManger.GetCurrentUser();
            //if (cUser == null)
            //{
            //    this.Session["UserLoginInfo"] = null;
            //    Response.Redirect("/Login.aspx");
            //    return;

            //}
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            List<string> msgList = new List<string>();
            if (!this.CheckInput(out msgList))
            {
                this.ltMsg.Text = string.Join("<br/>", msgList);
                return;
            }

            UserInfoModel currentUser = AuthManger.GetCurrentUser();
            if (currentUser == null)
            {
                Response.Redirect("/Login.aspx");
                return;
            }


            var user = UserInfoManager.GETUserInfoAccount(this.Session["UserLoginInfo"].ToString());

            string pwd = user.PWD;
            string NowPwd = this.txtPWD.Text;
            string CurrentPwd = this.txtCurretPWD.Text;
            string NewPwd = this.txtNewPWD.Text;

            if(string.Compare(NowPwd, pwd) != 0)
            {
                this.ltMsg.Text = "原密碼不正確";
                return;
            }


            if (string.Compare(NewPwd, CurrentPwd) == 0)
            {
                if (string.Compare(NewPwd, pwd) == 0 )
                {
                    this.ltMsg.Text = "新密碼不能和原本的密碼相同";
                    return;
                }
                else
                {
                    UserInfoManager.UpdateUserPassword(this.Session["UserLoginInfo"].ToString(), NewPwd);
                    Response.Redirect($"/SystemAdmin/UserDetail.aspx");
                    return;

                }

            }
            else
            {
                this.ltMsg.Text = "新密碼和確認密碼不一致";
                return;
            }
        }

        protected void btcCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect($"/SystemAdmin/UserDetail.aspx");
            return;
        }

        private bool CheckInput(out List<string> errorMsgList)
        {
            List<string> msgList = new List<string>();
            if (string.IsNullOrWhiteSpace(this.txtPWD.Text))
            {
                msgList.Add("原密碼不能為空");
            }

            if (string.IsNullOrWhiteSpace(this.txtCurretPWD.Text))
            {
                msgList.Add("再次確認新密碼不能為空");
            }

            if (string.IsNullOrWhiteSpace(this.txtNewPWD.Text))
            {
                msgList.Add("新密碼不能為空");
            }

            if (this.txtNewPWD.Text.Length < 8 || this.txtNewPWD.Text.Length > 16)
            {
                msgList.Add("新密碼長度不能小於8或大於16");
            }
            errorMsgList = msgList;
            if (msgList.Count == 0)
                return true;
            else
                return false;
        }
    }
}