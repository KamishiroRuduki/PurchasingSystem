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

        public static List<Commodity> GETCommodityInfo(int orderID)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.Commodities
                         where item.OrderID == orderID
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
