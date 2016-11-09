using SuperMinersWeb.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SuperMinersWeb.Shopping
{
    public partial class MineTrade : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblPrice.Text = GlobalData.MinePrice.ToString();
        }

        protected void btnPay_Click(object sender, EventArgs e)
        {
            if (this.txtCount.Text == "")
            {
                Response.Write("<script>alert('请输入购买数量')</script>");
                return;
            }
            int count = Convert.ToInt32(this.txtCount.Text);
            decimal valueRMB = count * GlobalData.GameConfig.RMB_Mine;
            string subUrl = Tools.CreateAlipayLink("WEB", MetaData.Trade.AlipayTradeInType.BuyMine, "WEB矿山", valueRMB, "");
            Response.Redirect(subUrl);
        }
    }
}