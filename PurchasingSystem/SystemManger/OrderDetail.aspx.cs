using PurchasingSystem.Auth;
using PurchasingSystem.DBSouce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PurchasingSystem.ORM.DBModels;
using System.Net.Mail;

namespace PurchasingSystem.SystemManger
{
    /// <summary>
    /// 訂單、商品資料寫入和修改
    /// </summary>
    public partial class OrderDetail : System.Web.UI.Page
    {
   
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AuthManger.ManagerIsLogined())//檢查登入
            {
                Response.Redirect("/SystemManger/Login.aspx");
                return;
            }
            var cUser = AuthManger.GetCurrentManager();//讀取該管理員資訊
            if (cUser == null)
            {
                this.Session["ManagerLoginInfo"] = null;
                Response.Redirect("/SystemManger/Login.aspx");
                return;

            }
            if (cUser.Level < 1)//一般管理員以上才能進此頁面
            {
                Response.Redirect("/SystemManger/ManagerInfo.aspx");
                return;
            }
            if (!IsPostBack)
            {
                if (this.Request.QueryString["ID"] != null)//依訂單ID抓取該訂單資訊
                {
                    var strID = this.Request.QueryString["ID"].ToString();
                    int id;
                    if (int.TryParse(strID, out id))
                    {
                        var list = CommodityManager.GETCommodityInfo(id);
                        var order = OrderManager.GETOrderInfoToList(id);
                        
                        if (order != null)
                        {
                            if(order[0].OrderStatus == 3 && cUser.Level < 2)//如果是以成立的訂單，就只有高級管理員以上才能做修改
                            {
                                Response.Redirect("/SystemManger/OrderListManager.aspx");
                                return;
                            }
                            
                            this.GridView2.DataSource = order;
                            this.GridView2.DataBind();
                        }
                        if (list.Count > 0)
                        {
                            //該訂單的商品列表
                            this.GridView1.DataSource = list;
                            this.GridView1.DataBind();
                            for ( int i =0; i< list.Count; i++)
                            {
                                var isBuy = (DropDownList)this.GridView1.Rows[i].FindControl("IsBuyDDList");                               
                                if (isBuy.SelectedValue == "-1")
                                {
                                    var txtprice = (TextBox)this.GridView2.Rows[0].FindControl("txtPrice");
                                    txtprice.Enabled = true;
                                }
                            }
                        }

                       
                    }
                }
                else
                {
                    Response.Redirect("/SystemManger/OrderListManager.aspx");
                }
            }
        }
        /// <summary>
        /// 儲存更新或寫入的資訊
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveCommodity_Click(object sender, EventArgs e)
        {
            if (this.GridView1.Rows.Count > 0)
            {
                for (int i = 0; i < this.GridView1.Rows.Count; i++)
                {
                    var txtPrice = (TextBox)this.GridView1.Rows[i].FindControl("txtPrice");
                    var type = (TextBox)this.GridView1.Rows[i].FindControl("txtType");
                    var id = (HiddenField)this.GridView1.Rows[i].FindControl("HiddenField1");
                    var isBuy = (DropDownList)this.GridView1.Rows[i].FindControl("IsBuyDDList");
                    int price = 0;
                    if (!string.IsNullOrWhiteSpace(txtPrice.Text))
                    {
                        price = Convert.ToInt32(txtPrice.Text);
                    }

                    CommodityManager.UpdateCommodity(price, type.Text, Convert.ToInt32(isBuy.SelectedValue), Convert.ToInt32(id.Value));
                }
            }

            if (this.GridView2.Rows.Count == 1 && this.Request.QueryString["ID"] != null)
            {
                var txtprice = (TextBox)this.GridView2.Rows[0].FindControl("txtPrice");
                var txtamount = (TextBox)this.GridView2.Rows[0].FindControl("txtAmount");
                var txtShippingFee = (TextBox)this.GridView2.Rows[0].FindControl("txtShippingFee");
                // var isBuyOrder = (DropDownList)this.GridView2.Rows[0].FindControl("IsBuyOrderDDList");
               // var isBuyOrder = (Literal)this.GridView2.Rows[0].FindControl("litOrderIsbuy");
                var isSentOrder = (DropDownList)this.GridView2.Rows[0].FindControl("IsSentOrderDDList");
                var orderStatus = (DropDownList)this.GridView2.Rows[0].FindControl("OrderStatusDDList");
                int amount = 0, shippingFee = 0;
                var priceText = Convert.ToDecimal(txtprice.Text);
                if (!string.IsNullOrWhiteSpace(txtamount.Text) && txtamount.Text != "0" )//有付款後才檢查
                {
                    var amountTest = Convert.ToDecimal(txtamount.Text);                  
                    var pricetxt = Decimal.Multiply(priceText, (decimal)0.9);//付款金額不得小於總金額，但是只跳警告，後續要做什麼處理由管理員決定
                    if (pricetxt > amountTest)
                    {
                        Response.Write("<Script language='JavaScript'>alert('付款金額不正確');  </Script>");
                        return;
                    }
                }
                if (!string.IsNullOrWhiteSpace(txtamount.Text))
                {
                    amount = Convert.ToInt32(txtamount.Text);
                }
                if (!string.IsNullOrWhiteSpace(txtShippingFee.Text))
                {
                    shippingFee = Convert.ToInt32(txtShippingFee.Text);
                }
                Order orderUpdate = new Order()
                {
                    PriceSum = priceText,
                    ID = Convert.ToInt32(this.Request.QueryString["ID"].ToString()),
                    Amount = amount,
                    ShippingFee = shippingFee,
                    // IsBuy = Convert.ToInt32(isBuyOrder.SelectedValue),
                    IsBuy = OrderIsBuyValue(),
                    IsSent = Convert.ToInt32(isSentOrder.SelectedValue),
                    OrderStatus = Convert.ToInt32(orderStatus.SelectedValue)
                };
                int emailid = Convert.ToInt32(this.Request.QueryString["ID"].ToString());
                var order = OrderManager.GETOrderInfo(emailid);
                var emailstr = UserInfoManager.GETUserInfoEmail(order.UserID);
                string body = Emailbody();
                if (body != null)
                    sendGmail(emailstr, body);//自動發MAIL

                OrderManager.UpdateOrderByManager(orderUpdate);

            }
            
                
            Response.Redirect("/SystemManger/OrderListManager.aspx");
            return;
            
        }

        protected void btnCancelCommodity_Click(object sender, EventArgs e)
        {
            Response.Redirect("/SystemManger/OrderListManager.aspx");
            return;
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    DropDownList DropDownList1 = (e.Row.FindControl("IsBuyDDList") as DropDownList);
            //    if (this.Request.QueryString["ID"].ToString() != null)
            //    {
            //        var strID = this.Request.QueryString["ID"].ToString();
            //        int id;
            //        if (int.TryParse(strID, out id))
            //        {
            //            var list = CommodityManager.GETCommodityInfo(id);
            //            if (list.Count > 0)
            //            {
            //                DropDownList1.DataSource = list;
            //               // DropDownList1.DataTextField = "IsBuy";
            //                DropDownList1.DataValueField = "IsBuy";
            //                DropDownList1.DataBind();

            //            }
            //        }


            //    }

            //  }
        }

        protected void IsBuyDDList_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (this.GridView1.Rows.Count > 0)
            {
                int j = this.GridView1.Rows.Count;
                for (int i = 0; i < this.GridView1.Rows.Count; i++)
                {
                   
                    var isBuy = (DropDownList)this.GridView1.Rows[i].FindControl("IsBuyDDList");
                    if (isBuy.SelectedValue == "1" || isBuy.SelectedValue == "-1")
                        j--;
                    if(isBuy.SelectedValue == "-1")//商品有任何一筆是缺貨，則總金額能做修改(詢問顧客後，此訂單還要繼續的情況)
                    {
                        var txtprice = (TextBox)this.GridView2.Rows[0].FindControl("txtPrice");
                        txtprice.Enabled = true;
                    }
                        

                }
                //所有商品皆為已購買(包含其中有缺貨)，訂單顯示文字為已購買
                var isBuyOrder = (Literal)this.GridView2.Rows[0].FindControl("litOrderIsbuy");
                if (j == 0)                 
                    isBuyOrder.Text = "已購買";
          
                else
                    isBuyOrder.Text = "尚未購買";

            }
        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //訂單是否為已購買的顯示文字
            var row = e.Row;
            if (row.RowType == DataControlRowType.DataRow)
            {
                Literal lbl = row.FindControl("litOrderIsbuy") as Literal;
                var dr = row.DataItem as Order;
                int orderStatus = dr.IsBuy;

                if (orderStatus == 1)
                {
                    lbl.Text = "已購買";
                }

                else 
                {

                    lbl.Text = "尚未購買";
                }


            }
        }
        /// <summary>
        /// 判斷此訂單是否為已購買
        /// </summary>
        /// <returns></returns>
        private int OrderIsBuyValue()
        {
            var isBuyOrder = (Literal)this.GridView2.Rows[0].FindControl("litOrderIsbuy");
            if (isBuyOrder.Text == "已購買")
                return 1;
            else
                return 0;
            
        }

        private void Cost()
        {
            var isBuyOrder = (Literal)this.GridView2.Rows[0].FindControl("litOrderIsbuy");
         //   if (isBuyOrder.Text == "已購買")



        }
        /// <summary>
        /// 商品金額加總計算後是否與使用者填的金額相符
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCalculation_Click(object sender, EventArgs e)
        {
            var costDataList = OrderManager.GetCostData();
            var cashrate = costDataList[0].Value;
            var purchasingcost = (int)costDataList[1].Value;
            if (this.GridView1.Rows.Count > 0)
            {
                decimal priceSum = 0;          
                for (int i = 0; i < this.GridView1.Rows.Count; i++)
                {

                    var txtPrice = (TextBox)this.GridView1.Rows[i].FindControl("txtPrice");
                    if (txtPrice.Text == null || txtPrice.Text =="0")//金額不為0或尚未填就跳過不做此判斷
                        return;
                    else
                        priceSum += Convert.ToDecimal(txtPrice.Text);

                }
                var priceTW = (int)(priceSum * cashrate) + purchasingcost;
                this.txtPrice.Text = priceTW.ToString();
                var strID = this.Request.QueryString["ID"].ToString();
                int id;
                if (int.TryParse(strID, out id))
                {                    
                    var order = OrderManager.GETOrderInfo(id);
                    if (priceTW != order.PriceSum)//只跳警告，後續要做什麼處理由管理員決定
                        Response.Write("<Script language='JavaScript'>alert('訂單金額不正確');  </Script>");
                }
                   

            }

        }
        /// <summary>
        /// 自動發MAIL
        /// </summary>
        /// <param name="emailstr"></param>
        /// <param name="body"></param>
        public void sendGmail( string emailstr, string body)
        {
            MailMessage mail = new MailMessage();
            //前面是發信email後面是顯示的名稱
            mail.From = new MailAddress("imoutonotamenara@gmail.com", "二次元代購系統通知");

            //收信者email
            mail.To.Add(emailstr);

            ////設定優先權
            //mail.Priority = MailPriority.Normal;

            //標題
            mail.Subject = "本系統為自動發送，請勿回復";

            //內容
            mail.Body = body;

            //內容使用html
            mail.IsBodyHtml = true;

            //設定gmail的smtp (這是google的)
            SmtpClient MySmtp = new SmtpClient("smtp.gmail.com", 587);

            //您在gmail的帳號密碼
            MySmtp.Credentials = new System.Net.NetworkCredential("imoutonotamenara", "ImoutoSaikou");

            //開啟ssl
            MySmtp.EnableSsl = true;

            //發送郵件
            MySmtp.Send(mail);

            //放掉宣告出來的MySmtp
            MySmtp = null;

            //放掉宣告出來的mail
            mail.Dispose();
        }
        /// <summary>
        /// MAIL內文
        /// </summary>
        /// <returns></returns>
        private string Emailbody()
        {
            var strID = this.Request.QueryString["ID"].ToString();
            int id;
            string body = null;
            if (int.TryParse(strID, out id))
            {
                var order = OrderManager.GETOrderInfo(id);
                var orderStatus = (DropDownList)this.GridView2.Rows[0].FindControl("OrderStatusDDList");
                int status = Convert.ToInt32(orderStatus.SelectedValue);
                if (order.OrderStatus <= status && order.OrderStatus != -1 && status != -1)
                {
                    if(status == 1 && order.OrderStatus < status)
                    {
                        body = "感謝使用本系統作代購服務，已經確認貴客的訂單，請盡快付款";
                    }
                    else if (status == 2 && order.IsBuy == 0 && OrderIsBuyValue() == 1)
                    {
                        body = "感謝使用本系統作代購服務，委託代購的商品已購買，商品到台後會另做通知";
                    }
                    else if (status == 2 && order.OrderStatus < status)
                    {
                        body = "感謝使用本系統作代購服務，已經確認貴客的款項，會盡快處理您的訂單";
                    }
                }
                if(status == -1)//不成立用，尚未完成
                {

                }

            }
            return body;
        }
    }
}