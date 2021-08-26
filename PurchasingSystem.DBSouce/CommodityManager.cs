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
    }
}
