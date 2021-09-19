using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PurchasingSystem.ORM.DBModels;

namespace PurchasingSystem.DBSouce
{
    public class ManagerInfoManager
    {
        /// <summary>
        /// 用帳號抓取該管理員資訊
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public static Manager GETManagerInfoAccount(string account)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.Managers
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
        /// 用GUID抓取該管理員資訊
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static Manager GETManagerInfoAccountByUserID(Guid userID)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.Managers
                         where item.UserID == userID
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
        /// 讀取所有管理員的資訊
        /// </summary>
        /// <returns></returns>
        public static List<Manager> GETManagerInfoToList()
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.Managers
                         select item);

                    var list = query.ToList();
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
        /// 確認帳號是否被使用
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
                        (from item in context.Managers
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
        /// 新增管理員
        /// </summary>
        /// <param name="newManager"></param>
        public static void CreateManager(Manager newManager)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    context.Managers.Add(newManager);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);

            }
        }
        /// <summary>
        /// 修改管理員資料
        /// </summary>
        /// <param name="account"></param>
        /// <param name="pwd"></param>
        /// <param name="level"></param>
        public static void UpdateManager(string account, string pwd, int level)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.Managers
                         where item.Account == account
                         select item);

                    var list = query.FirstOrDefault();
                    if (list != null)
                    {
                        list.Password = pwd;
                        list.Level = level;
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);

            }
        }
        /// <summary>
        /// 刪除管理員
        /// </summary>
        /// <param name="account"></param>
        public static void DelManager(string account)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                       (from item in context.Managers
                        where item.Account == account
                          select item);

                    var list = query.FirstOrDefault();
                    if (list != null)
                    {
                        context.Managers.Remove(list);
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);

            }
        }
        /// <summary>
        /// 讀取額外款項資訊(匯率、代購費...等)
        /// </summary>
        /// <returns></returns>
        public static List<CostData> GETCostData()
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.CostDatas
                         select item);

                    var list = query.ToList();
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
        /// 更新額外款項資訊(匯率、代購費...等)
        /// </summary>
        /// <param name="newCashRate"></param>
        /// <param name="newPurchasingCost"></param>
        public static void UpdateCostData(Decimal newCashRate, Decimal newPurchasingCost)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.CostDatas
                         where item.ID == 1
                         select item);
                    var query2 =
                        (from item in context.CostDatas
                         where item.ID == 2
                         select item);

                    var list = query.FirstOrDefault();
                    var list2 = query2.FirstOrDefault();
                    if (list != null)
                    {
                        list.Value = newCashRate;
                        
                    }
                    if (list2 != null)
                    {
                        list2.Value = newPurchasingCost;
                       
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
