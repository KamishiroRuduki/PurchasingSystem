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
    /// <summary>
    /// 使用者資訊變更
    /// </summary>
    public partial class UserDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AuthManger.IsLogined())
            {
                Response.Redirect("/login.aspx");
                return;
            }
            var cUser = AuthManger.GetCurrentUser();
            if (cUser == null)
            {
                this.Session["UserLoginInfo"] = null;
                Response.Redirect("/Login.aspx");
                return;

            }
            if (!this.IsPostBack) {
            this.lblAccount.Text = cUser.Account;
            this.txtName.Text = cUser.Name;
            this.txtPhone.Text = cUser.MobilePhone;
            this.txtMail.Text = cUser.Email;
            this.txtAddress.Text = cUser.Address;
            this.payType.SelectedValue = cUser.PaymentType.ToString();
            this.txtPaymentProfile.Text = cUser.PaymentProfile;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            List<string> msgList = new List<string>();
            if (!this.CheckInput(out msgList))
            {
                this.ltMsg.Text = string.Join("<br/>", msgList);
                return;
            }
            var user = UserInfoManager.GETUserInfoAccount(this.Session["UserLoginInfo"].ToString());
            var inteager = Convert.ToInt32(this.payType.SelectedValue);
            if (!UserInfoManager.IsMailCreated(this.txtMail.Text))
            {
                this.ltMsg.Text = "此信箱已經被使用過了";
                return;
            }
            if (!UserInfoManager.IsPhoneCreated(this.txtPhone.Text))
            {
                this.ltMsg.Text = "此手機已經被使用過了";
                return;
            }
            ORM.DBModels.UserInfo userchange = new ORM.DBModels.UserInfo()
            {
                Account = lblAccount.Text,
                Name = this.txtName.Text,
                MobilePhone = this.txtPhone.Text,
                Email = this.txtMail.Text,
                Address = this.txtAddress.Text,
                PaymentType = inteager,
                PaymentProfile = this.txtPaymentProfile.Text
            };
            UserInfoManager.UpdateUser(userchange);

        }

        private bool CheckInput(out List<string> errorMsgList)
        {
            List<string> msgList = new List<string>();
            if (this.payType.SelectedValue != "0" && this.payType.SelectedValue != "1")
            {
                msgList.Add("Type必須是0或1");
            }

            if (string.IsNullOrWhiteSpace(this.txtPhone.Text))
            {
                msgList.Add("請輸入手機號碼");
            }
            else
            {
                int tempInt;
                if (!int.TryParse(this.txtPhone.Text, out tempInt))
                {
                    msgList.Add("手機號碼有誤");
                }
            }
            if (string.IsNullOrWhiteSpace(this.txtName.Text))
            {
                msgList.Add("請輸入姓名");
            }
            if (string.IsNullOrWhiteSpace(this.txtAddress.Text))
            {
                msgList.Add("請輸入地址");
            }
            if (string.IsNullOrWhiteSpace(this.txtMail.Text))
            {
                msgList.Add("請輸入Email");
            }
            if (string.IsNullOrWhiteSpace(this.txtPaymentProfile.Text))
            {
                msgList.Add("請輸入卡號或銀行帳號");
            }


            errorMsgList = msgList;
            if (msgList.Count == 0)
                return true;
            else
                return false;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("/SystemAdmin/UserInfo.aspx");
            return;
        }

        protected void btnPwd_Click(object sender, EventArgs e)
        {
            Response.Redirect($"/SystemAdmin/UserPasswordchange.aspx");
            return;
        }
    }
}