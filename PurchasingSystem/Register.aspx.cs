using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PurchasingSystem.ORM.DBModels;
using PurchasingSystem.DBSouce;

namespace PurchasingSystem
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            List<string> msgList = new List<string>();
            if (!this.CheckInput(out msgList))
            {
                this.ltMsg.Text = string.Join("<br/>", msgList);
                return;
            }

            string accText = this.txtAccount.Text;
            string phoneText = this.txtPhone.Text;
            string mailText = this.txtMail.Text;
            if (!UserInfoManager.IsAccountCreated(accText))
            {
                this.ltMsg.Text = "此帳號已經被使用過了";
                return;
            }
            if (!UserInfoManager.IsMailCreated(mailText))
            {
                this.ltMsg.Text = "此信箱已經被使用過了";
                return;
            }
            if (!UserInfoManager.IsPhoneCreated(phoneText))
            {
                this.ltMsg.Text = "此手機已經被使用過了";
                return;
            }
            string PaymentTypeText = this.payType.SelectedValue;
            int PaymentType = Convert.ToInt32(PaymentTypeText);
            UserInfo newUser = new UserInfo()
            {

            UserID = Guid.NewGuid(),
            Account = this.txtAccount.Text,
            PWD = this.txtPassword.Text,
            Name = this.txtName.Text,
            MobilePhone = this.txtPhone.Text,
            Email = this.txtMail.Text,
            Address = this.txtAddress.Text,
            PaymentProfile = this.txtPaymentProfile.Text,
            PaymentType = PaymentType,
            CreateDate = DateTime.Now,
            BlackList = 0

            };
            UserInfoManager.CreateUser(newUser);
            Response.Redirect("Default1.aspx");
        }

        private bool CheckInput(out List<string> errorMsgList)
        {
            List<string> msgList = new List<string>();
            if (this.payType.SelectedValue != "0" && this.payType.SelectedValue != "1")
            {
                msgList.Add("Type必須是0或1");
            }

            if (string.IsNullOrWhiteSpace(this.txtAccount.Text))
            {
                msgList.Add("請輸入帳號");
            }
            if (string.IsNullOrWhiteSpace(this.txtPassword.Text))
            {
                msgList.Add("請輸入密碼");
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
    }
}