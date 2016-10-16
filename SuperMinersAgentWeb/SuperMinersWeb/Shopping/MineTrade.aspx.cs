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
            string subUrl = Tools.CreateAlipayLink("20160813000000000", "矿山", GlobalData.GameConfig.RMB_Mine, "");
            Response.Redirect(subUrl);
        }
    }
}