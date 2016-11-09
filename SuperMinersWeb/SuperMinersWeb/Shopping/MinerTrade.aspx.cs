using SuperMinersWeb.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SuperMinersWeb.Shopping
{
    public partial class MinerTrade : System.Web.UI.Page
    {
        public float Price { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblPrice.Text = GlobalData.MinerPrice.ToString();
        }

        protected void btnPay_Click(object sender, EventArgs e)
        {
            if (this.txtCount.Text == "")
            {
                Response.Write("<script>alert('请输入购买数量')</script>");
                return;
            }
            int count = Convert.ToInt32(this.txtCount.Text);
            decimal valueRMB = count * GlobalData.GameConfig.GoldCoin_Miner / GlobalData.GameConfig.RMB_GoldCoin;
            string subUrl = Tools.CreateAlipayLink("WEB", MetaData.Trade.AlipayTradeInType.BuyMiner, "WEB矿工", valueRMB, "");
            Response.Redirect(subUrl);
        }
    }
}