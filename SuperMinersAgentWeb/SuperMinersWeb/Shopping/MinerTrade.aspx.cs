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

        }
    }
}