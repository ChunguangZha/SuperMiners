using MetaData.Trade;
using SuperMinersWeiXin.Wcf.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SuperMinersWeiXin.View
{
    public partial class StoneMarket : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.SellStoneOrderListSource.SelectParameters.Add(new Parameter("userName", System.Data.DbType.String, Context.User.Identity.Name));
            }

        }

    }
}