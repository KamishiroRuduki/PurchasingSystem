using PurchasingSystem.Auth;
using PurchasingSystem.DBSouce;
using PurchasingSystem.ORM.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PurchasingSystem.SystemManger
{
    /// <summary>
    /// 管理員清單
    /// </summary>
    public partial class ManagerList : System.Web.UI.Page
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
            if (cUser.Level < 2)//高階管理員以上才能進此頁面
            {
                Response.Redirect("/SystemManger/ManagerInfo.aspx");
                return;
            }

            if (!IsPostBack)
            {
               
                    //建立管理員清單
                    var list = ManagerInfoManager.GETManagerInfoToList();
                    if (list.Count > 0)
                    {
                        this.GridView1.DataSource = list;
                        this.GridView1.DataBind();
                    }
                    //額外款項資訊(匯率、代購費...等)
                var costdata = ManagerInfoManager.GETCostData();
                this.txtCashRate.Text = costdata[0].Value.ToString();
                var purchasingcostStr= costdata[1].Value.ToString();
                this.txtPurchasingCost.Text = (Convert.ToDouble(purchasingcostStr)).ToString();

            }
            }

        protected void OrderListView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //變更權限項顯示的文字
            var row = e.Row;
            if (row.RowType == DataControlRowType.DataRow)
            {
                Label lbl = row.FindControl("lbl") as Label;
                var dr = row.DataItem as Manager;
                int level = dr.Level;

                if (level == 0)
                {
                    lbl.Text = "客服人員";
                }

                else if( level == 1)
                {
                    lbl.Text = "一般管理員";
                }

                else if (level >= 2)
                {
                    lbl.Text = "高級管理員";
                }


            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //儲存新的額外款項資訊(匯率、代購費...等)
            if ( !string.IsNullOrWhiteSpace(txtCashRate.Text) && !string.IsNullOrWhiteSpace(txtPurchasingCost.Text))
            {
                decimal cashrate = 0;
                if (Decimal.TryParse(txtCashRate.Text, out cashrate))
                { 

                
                //var newCashRate = Convert.ToDecimal(txtCashRate.Text);
                var newPurchasingCost = Convert.ToDecimal(txtPurchasingCost.Text);
                ManagerInfoManager.UpdateCostData(cashrate, newPurchasingCost);
                }

            }
            Response.Redirect("/SystemManger/ManagerList.aspx");

        }

        protected void btnCancel_Click(object sender, EventArgs e)//取消按鈕
        {
            Response.Redirect("/SystemManger/ManagerList.aspx");
        }
    }
}