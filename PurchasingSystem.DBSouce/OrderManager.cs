using PurchasingSystem.ORM.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchasingSystem.DBSouce
{
    public class OrderManager
    {
        /// <summary>
        /// 取得該使用者的最新一筆訂單
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static Order GETOrderInfo(Guid userID)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.Orders
                         where item.UserID == userID
                         orderby item.ID descending
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
        /// 取得該使用者的所有訂單
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static List<Order> GETAllOrderInfo(Guid userID)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.Orders
                         where item.UserID == userID
                         orderby item.ID descending
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
        /// 建立新的訂單
        /// </summary>
        /// <param name="order"></param>
        public static void CreateOrder(Order order)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    context.Orders.Add(order);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);

            }
        }
        public static List<CostData> GetCostData()
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
    }
}
