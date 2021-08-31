using PurchasingSystem.ORM.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchasingSystem.DBSouce
{
    public class UserInfoManager
    {
        public static UserInfo GETUserInfoAccount(string account)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.UserInfoes
                         where item.Account == account
                         select item);

                    var list = query.FirstOrDefault();
                    return list;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }
        /// <summary>
        /// 判斷此帳號是否已經被使用
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public static bool IsAccountCreated(string account)
        {

            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.UserInfoes
                         where item.Account == account
                         select item);

                    var list = query.FirstOrDefault();
                    if (list is null)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return false;
            }
        }
        /// <summary>
        /// 判斷此mail是否已經被使用
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsMailCreated(string email)
        {

            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.UserInfoes
                         where item.Email == email
                         select item);

                    var list = query.FirstOrDefault();
                    if (list is null)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return false;
            }
        }
        /// <summary>
        /// 判斷此手機是否已經被使用
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public static bool IsPhoneCreated(string phone)
        {

            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.UserInfoes
                         where item.MobilePhone == phone
                         select item);

                    var list = query.FirstOrDefault();
                    if (list is null)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return false;
            }
        }
        //建立使用者
        public static void CreateUser(UserInfo user)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    context.UserInfoes.Add(user);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);

            }
        }

        public static void UpdateUser(UserInfo user)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.UserInfoes
                         where item.Account == user.Account
                         select item);

                    var list = query.FirstOrDefault();
                    if(list!= null)
                    {                       
                        list.Name = user.Name;
                        list.MobilePhone = user.MobilePhone;
                        list.Email = user.Email;
                        list.Address = user.Address;
                        list.PaymentType = user.PaymentType;
                        list.PaymentProfile = user.PaymentProfile;
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);

            }
        }

        public static void UpdateUserPassword(string account, string pwd)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.UserInfoes
                         where item.Account == account
                         select item);

                    var list = query.FirstOrDefault();
                    if (list != null)
                    {
                        list.PWD = pwd;
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);

            }
        }
    }
}
