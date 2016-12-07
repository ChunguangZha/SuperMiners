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
            }
        }

        public static MetaData.Trade.SellStonesOrder[] GetNotFinishedSellorders()
        {
            if (!WcfClient.IsReady)
            {
                return null;
            }

            string userName = Context.User.Identity.Name;
            return WcfClient.Instance.GetAllNotFinishedSellOrders(userName);
        }

        public void Insert(MetaData.Trade.SellStonesOrder order)
        {

        }

        public SuperMinersWeiXin.App_Code.TestModel[] GetModels()
        {
            return new App_Code.TestModel[] { };
        }
    }
}