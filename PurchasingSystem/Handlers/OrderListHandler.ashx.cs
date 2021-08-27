using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using PurchasingSystem.Auth;
using PurchasingSystem.DBSouce;
using PurchasingSystem.ORM.DBModels;

namespace PurchasingSystem.Handlers
{
    /// <summary>
    /// OrderListHandler の概要の説明です
    /// </summary>
    public class OrderListHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string ID = context.Request.QueryString["OrderID"];
            if (string.IsNullOrEmpty(ID))
            {
                context.Response.StatusCode = 400;
                context.Response.ContentType = "text/plain";
                context.Response.Write("required");
                context.Response.End();
            }
            int orderID = Convert.ToInt32(ID);
            List<Commodity> list = CommodityManager.GETCommodityInfo(orderID);
            string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(list);
            context.Response.ContentType = "application/json";
            context.Response.Write(jsonText);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}