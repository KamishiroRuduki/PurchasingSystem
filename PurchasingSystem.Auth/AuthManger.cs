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
        /// <summary>
        /// 檢查登入
        /// </summary>
        /// <returns></returns>
        public static bool IsLogined()
        {
            if (HttpContext.Current.Session["UserLoginInfo"] == null)
                return false;
            else
                return true;
        }
        /// <summary>
        /// 取得使用者資訊
        /// </summary>
        /// <returns></returns>
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
            model.BlackList = user.BlackList;

            return model;
        }
        /// <summary>
        /// 登出
        /// </summary>
        public static void Logout()
        {
            HttpContext.Current.Session["UserLoginInfo"] = null; //清除登入資訊
        }
        /// <summary>
        /// 使用者登入
        /// </summary>
        /// <param name="account"></param>
        /// <param name="pwd"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 檢查管理員登入
        /// </summary>
        /// <returns></returns>
        public static bool ManagerIsLogined()
        {
            if (HttpContext.Current.Session["ManagerLoginInfo"] == null)
                return false;
            else
                return true;
        }
        /// <summary>
        /// 取得該管理員資訊
        /// </summary>
        /// <returns></returns>
        public static ManagerInfoModel GetCurrentManager()
        {
            string account = HttpContext.Current.Session["ManagerLoginInfo"] as string;
            if (account == null)
                return null;
            var manager = ManagerInfoManager.GETManagerInfoAccount(account);

            if (manager == null)
            {
                HttpContext.Current.Session["ManagerLoginInfo"] = null;
                return null;
            }

            ManagerInfoModel model = new ManagerInfoModel();
            model.ID = manager.UserID.ToString();
            model.Account = manager.Account;
            model.Name = manager.Name;
            model.Level = manager.Level;

            return model;
        }

        /// <summary>
        /// 登出(管理員用)
        /// </summary>
        public static void ManagerLogout()
        {
            HttpContext.Current.Session["ManagerLoginInfo"] = null; //清除登入資訊
        }
        /// <summary>
        /// 管理員登入
        /// </summary>
        /// <param name="account"></param>
        /// <param name="pwd"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static bool TryManagerLogin(string account, string pwd, out string errMsg)
        {
            //檢查帳號/密碼是否正確
            if (string.IsNullOrWhiteSpace(account) || string.IsNullOrWhiteSpace(pwd))
            {
                errMsg = "帳號/密碼錯誤";
                return false;
            }
            //檢查此帳號是否存在
            var user = ManagerInfoManager.GETManagerInfoAccount(account);
            if (user == null)
            {
                errMsg = $"帳號{account}不存在";
                return false;
            }
            if (string.Compare(user.Account, account, true) == 0 && string.Compare(user.Password, pwd, false) == 0)
            {
                HttpContext.Current.Session["ManagerLoginInfo"] = user.Account;
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
