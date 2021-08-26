using PurchasingSystem.DBSouce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PurchasingSystem.Auth
{
    public class AuthManger
    {
        public static bool IsLogined()
        {
            if (HttpContext.Current.Session["UserLoginInfo"] == null)
                return false;
            else
                return true;
        }
        public static UserInfoModel GetCurrentUser()
        {
            string account = HttpContext.Current.Session["UserLoginInfo"] as string;
            if (account == null)
                return null;
            var user = UserInfoManager.GETUserInfoAccount(account);

            if (user == null)
            {
                HttpContext.Current.Session["UserLoginInfo"] = null;
                return null;
            }

            UserInfoModel model = new UserInfoModel();
            model.ID = user.UserID.ToString();
            model.Account = user.Account;
            model.Name = user.Name;
            model.MobilePhone = user.MobilePhone;
            model.Email = user.Email;
            model.Address = user.Address;
            model.PaymentProfile = user.PaymentProfile;
            model.PaymentType = user.PaymentType;
            model.CreteDate = user.CreateDate;

            return model;
        }

        public static void Logout()
        {
            HttpContext.Current.Session["UserLoginInfo"] = null; //清除登入資訊
        }

        public static bool TryLogin(string account, string pwd, out string errMsg)
        {
            //檢查帳號/密碼是否正確
            if (string.IsNullOrWhiteSpace(account) || string.IsNullOrWhiteSpace(pwd))
            {
                errMsg = "帳號/密碼錯誤";
                return false;
            }
            //檢查此帳號是否存在
            var user = UserInfoManager.GETUserInfoAccount(account);
            if (user == null)
            {
                errMsg = $"帳號{account}不存在";
                return false;
            }
            if (string.Compare(user.Account, account, true) == 0 && string.Compare(user.PWD, pwd, false) == 0)
            {
                HttpContext.Current.Session["UserLoginInfo"] = user.Account;
                errMsg = string.Empty;
                return true;
            }
            else
            {
                errMsg = "登入失敗，請檢查帳號或密碼是否正確";
                return false;
            }

        }


    }
}
