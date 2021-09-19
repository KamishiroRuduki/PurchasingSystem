using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PurchasingSystem.ORM.DBModels;
using PurchasingSystem.DBSouce;
using BankJsonData;
namespace PurchasingSystem
{
    /// <summary>
    /// 使用者註冊
    /// </summary>
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //----------------抓銀行代號並放進DropDownList------------------------
            var list = BankJsonData.BankJsonData.ReadData();
            bankcodeDDList.DataSource = list;
            bankcodeDDList.DataBind();

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
            string paymentProfile = string.Empty;
            if (this.payType.SelectedValue == "1")//銀行轉帳的話，抓取下拉選單選到的銀行資訊跟顧客的銀行帳號
            {
                paymentProfile = bankcodeDDList.SelectedValue + " " + this.txtPaymentProfile.Text;
            }
            else//信用卡只抓卡號
            {
                paymentProfile = this.txtPaymentProfile.Text;
            }
            if (!UserInfoManager.IsPaymentCreated(paymentProfile))
            {
                this.ltMsg.Text = "此卡號或銀行帳號已經被使用過了";
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
            PaymentProfile = paymentProfile,
            PaymentType = PaymentType,
            CreateDate = DateTime.Now,
            BlackList = 0

            };
            UserInfoManager.CreateUser(newUser);
            Response.Write("<Script language='JavaScript'>alert('帳號註冊成功，請從登入頁面登入'); location.href='Login.aspx'; </Script>");
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
            if (this.txtPassword.Text.Length < 8 || this.txtPassword.Text.Length > 16)
            {
                msgList.Add("密碼長度不能小於8或大於16");
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
            if (this.txtPaymentProfile.Text.Length < 12 )
            {
                msgList.Add("卡號或銀行帳號有誤");
            }


            errorMsgList = msgList;
            if (msgList.Count == 0)
                return true;
            else
                return false;
        }

        protected void payType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //------------------選到銀行帳號才顯示--------------------
            if (this.payType.SelectedValue == "1")
            {
                bankcodeDDList.Visible = true;
            }
            else
            {
                bankcodeDDList.Visible = false;
            }
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}