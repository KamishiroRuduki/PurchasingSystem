using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PurchasingSystem.ORM.DBModels;
using PurchasingSystem.DBSouce;
using PurchasingSystem.Auth;

namespace PurchasingSystem.SystemAdmin
{
    /// <summary>
    /// 委託代購頁面
    /// </summary>
    public partial class CreateOrder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AuthManger.IsLogined())//檢查登入
            {
                Response.Redirect("/Login.aspx");
                return;
            }
            var cUser = AuthManger.GetCurrentUser();//讀取該使用者資訊
            if (cUser == null)
            {
                this.Session["UserLoginInfo"] = null;
                Response.Redirect("/Login.aspx");
                return;

            }
            var costDataList = OrderManager.GetCostData();
            this.HF2.Value = costDataList[0].Value.ToString();
            var integer = (int)costDataList[1].Value;
            this.HF3.Value = integer.ToString();
            this.ltlCalcu.Text = $"計算公式，商品金額加總*匯率:{costDataList[0].Value.ToString()}+代購費:{integer.ToString()}";
             

        }

        protected void btnsave_Click(object sender, EventArgs e)
        {

            //String[] keys = Request.Form.AllKeys;
            //for (int i = 0; i < keys.Length; i++)
            //{
            //    Response.Write(keys[i] + ": " + Request.Form[keys[i]] + "<br>");
            //}

            //判斷是否沒新增商品就送出
            if (this.Request.Form["txtURL"] is null || this.Request.Form["txtName"] is null || this.Request.Form["txtQuantity"] is null)
            {
                ltmsg.Text = ("請輸入商品資料");
                return;

            }

            var strURL = this.Request.Form["txtURL"].ToString();
            var strName = this.Request.Form["txtName"].ToString();
            var strQuantity = this.Request.Form["txtQuantity"].ToString();
          //  var strPrice = this.Request.Form["txtPrice"].ToString();
            var strPrice = this.HiddenField1.Value.ToString();
            //判斷是否甚麼都沒輸入就送出
            if (string.IsNullOrEmpty(strURL)|| string.IsNullOrEmpty(strName) || string.IsNullOrEmpty(strQuantity))
            {
                ltmsg.Text = ("請輸入商品資料");
                return;
            }
            char[] delimiterChars = {','};
            string[] ssURL = strURL.Split(delimiterChars);
            string[] ssName = strName.Split(delimiterChars);
            string[] ssQuantity = strQuantity.Split(delimiterChars);
            if(!CheckInput(ssURL,ssName,ssQuantity ))
            {
                ltmsg.Text = ("商品資料有漏填，請檢查");
                return;
            }

            if (string.IsNullOrWhiteSpace(strPrice))
            {
                ltmsg.Text = ("請先從試算金額計算價錢");
                return;
            }
            if(Convert.ToInt32(strPrice) > 500000 || Convert.ToInt32(strPrice)< 0 )
            {
                ltmsg.Text = ("金額不正確");
                return;
            }
            for (int i = 0; i < ssQuantity.Length; i++)
            {
                if(Convert.ToInt32(ssQuantity[i]) > 3)
                {
                    ltmsg.Text = ("同一件商品一人最多只能代購3個");
                    return;
                }

            }
                var user = UserInfoManager.GETUserInfoAccount(this.Session["UserLoginInfo"].ToString());
            Order neworder = new Order()
            {
                UserID = user.UserID,
                PriceSum = Convert.ToDecimal(strPrice),
                CreateDate = DateTime.Now,
                IsBuy = 0,
                IsSent = 0,
                Remarks = this.txtRemarks.Text,
                OrderStatus = 0,
                CashRate = this.HF2.Value,
                PurchasingCost = this.HF3.Value

            };
            OrderManager.CreateOrder(neworder);
            //-----------------訂單成立後先把訂單資料寫入DB，再讀取訂單ID讓該訂單的所有商品有訂單ID後再將商品資訊寫入DB
            var order = OrderManager.GETOrderInfo(user.UserID);
            // if (txt != null)
            for (int i = 0; i < ssURL.Length; i++)
            {
                Commodity newCommodity = new Commodity()
                {
                    OrderID = order.ID,
                    Name = ssName[i],
                    URL = ssURL[i],
                    Quantity = Convert.ToInt32(ssQuantity[i]),
                    IsBuy = 0
                };
                CommodityManager.CreateCommodity(newCommodity);

            }
            Response.Write("<Script language='JavaScript'>alert('訂單已成功送出！'); location.href='/UserInfo.aspx'; </Script>");
        }
        /// <summary>
        /// 檢查是否有未填寫的
        /// </summary>
        /// <param name="ssURL"></param>
        /// <param name="ssName"></param>
        /// <param name="ssQuantity"></param>
        /// <returns></returns>
        private bool CheckInput(string[] ssURL, string[] ssName, string[] ssQuantity)
        {
            for( int i = 0; i < ssName.Length;i++)
            { 
            if (string.IsNullOrEmpty(ssURL[i]) || string.IsNullOrEmpty(ssName[i]) || string.IsNullOrEmpty(ssQuantity[i]))
                return false;
            }
            
                return true;
            
        }
    }
}