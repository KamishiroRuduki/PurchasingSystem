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
        public static Order GETOrderInfo(int orderID)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.Orders
                         where item.ID == orderID
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
        public static List<Order> GETOrderInfoToList(int orderID)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.Orders
                         where item.ID == orderID
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

        public static void UpdateOrderStatus(int ID, int status)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.Orders
                         where item.ID == ID
                         select item);

                    var list = query.FirstOrDefault();
                    if (list != null)
                    {
                        list.OrderStatus = -1;
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);

            }
        }
        //-----------------------------------------------------以下是管理員使用的method-------------------------------------------------
        public static List<Order> GETOrderInfoByManager()
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.Orders
                         where item.UserID == item.UserID && item.OrderStatus >=0
                      //   orderby item.OrderStatus
                         select item);
                    var query2 = query.OrderBy(obj => obj.OrderStatus).ThenByDescending(obj => obj.ID);

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
        public static void UpdateOrderByManager(Order order)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.Orders
                         where item.ID == order.ID
                         select item);

                    var list = query.FirstOrDefault();
                    if (list != null)
                    {
                        list.IsBuy = order.IsBuy;
                        list.IsSent = order.IsSent;
                        list.Amount = order.Amount;
                        list.ShippingFee = order.ShippingFee;
                        list.OrderStatus = order.OrderStatus;
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
