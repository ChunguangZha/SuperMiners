using SuperMinersWeb.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SuperMinersWeb.Shopping
{
    public partial class StoneTrade : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblPrice.Text = GlobalData.StonePrice.ToString();
        }

        protected void btnPay_Click(object sender, EventArgs e)
        {
            string subUrl = Tools.CreateAlipayLink("201608131111111111", "矿石", 1, "");
            Response.Redirect(subUrl);
        }
    }
}