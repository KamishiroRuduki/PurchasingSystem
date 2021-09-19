using PurchasingSystem.ORM.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace PurchasingSystem.DBSouce
{
    public class CommodityManager
    {
        /// <summary>
        /// 將訂單內商品寫進DB
        /// </summary>
        /// <param name="commodity"></param>
        public static void CreateCommodity(Commodity commodity)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    context.Commodities.Add(commodity);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);

            }
        }
        /// <summary>
        /// 依訂單ID讀取該訂單的所有商品資訊
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public static List<CommodityModel> GETCommodityInfo(int orderID)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.Commodities
                         where item.OrderID == orderID
                         join item2 in context.Orders on item.OrderID equals item2.ID
                         select new 
                         {
                             item.ID,
                             item.OrderID,
                             item.Name,
                             item.URL,
                             item.Quantity,
                             item.Price,
                             item.Type,
                             item.IsBuy,
                             item2.CashRate,
                             item2.PurchasingCost
                         });

                    List<CommodityModel> list = query.Select(obj=> new CommodityModel()
                    { 
                        commodityID = obj.ID,
                        orderID = obj.OrderID,
                        Name = obj.Name,
                        URL=obj.URL,
                        Price=obj.Price,
                        Quantity = obj.Quantity,
                        Type = obj.Type,
                        CashRate=obj.CashRate,
                        PurchasingCost=obj.PurchasingCost,
                        IsBuy =obj.IsBuy
                    }).ToList();

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
        /// 更新商品資訊
        /// </summary>
        /// <param name="price"></param>
        /// <param name="type"></param>
        /// <param name="isbuy"></param>
        /// <param name="id"></param>
        public static void UpdateCommodity(int price, string type, int isbuy, int id)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.Commodities
                         where item.ID == id
                         select item);

                    var list = query.FirstOrDefault();
                    if (list != null)
                    {
                        list.Price = price;
                        list.Type = type;
                        list.IsBuy = isbuy;

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
