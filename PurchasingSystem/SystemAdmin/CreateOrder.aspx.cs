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
    public partial class CreateOrder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AuthManger.IsLogined())
            {
                Response.Redirect("/login.aspx");
                return;
            }
            var cUser = AuthManger.GetCurrentUser();
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
            var user = UserInfoManager.GETUserInfoAccount(this.Session["UserLoginInfo"].ToString());
            Order neworder = new Order()
            {
                UserID = user.UserID,
                PriceSum = Convert.ToDecimal(strPrice),
                CreateDate = DateTime.Now,
                IsBuy = 0,
                IsSent = 0,
                Remarks = this.txtRemarks.Text,
                OrderStatus = 0

            };
            OrderManager.CreateOrder(neworder);

            var order = OrderManager.GETOrderInfo(user.UserID);
            // if (txt != null)
            for (int i = 0; i < ssURL.Length; i++)
            {
                Commodity newCommodity = new Commodity()
                {
                    OrderID = order.ID,
                    Name = ssName[i],
                    URL = ssURL[i],
                    Quantity = Convert.ToInt32(ssQuantity[i])
                };
                CommodityManager.CreateCommodity(newCommodity);

            }
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