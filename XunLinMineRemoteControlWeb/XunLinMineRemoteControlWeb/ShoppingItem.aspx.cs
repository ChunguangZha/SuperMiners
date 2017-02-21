using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using XunLinMineRemoteControlWeb.Core;

namespace XunLinMineRemoteControlWeb
{
    public partial class ShoppingItem : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void btnPay_Click(object sender, EventArgs e)
        {
            MyFormsPrincipal<WebLoginUserInfo> principal = Context.User as MyFormsPrincipal<WebLoginUserInfo>;
            if (principal == null || principal.UserData == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            int index = this.serverTypeSelect.SelectedIndex;

            if (index < 0)
            {
                Response.Write("<script>alert('请选择要购买的服务类型')</script>");
                return;
            }

            MetaData.Trade.RemoteServerType serverType;
            if (index == 0)
            {
                serverType = MetaData.Trade.RemoteServerType.Once;
            }
            else if (index == 1)
            {
                serverType = MetaData.Trade.RemoteServerType.OneMonth;
            }
            else if (index == 2)
            {
                serverType = MetaData.Trade.RemoteServerType.HalfYear;
            }
            else if (index == 3)
            {
                serverType = MetaData.Trade.RemoteServerType.OneYear;
            }
            else
            {
                Response.Write("<script>alert('请选择要购买的服务类型')</script>");
                return;
            }

            Wcf.WcfClient.Instance.CreateBuyRemoteServerAlipayLink(principal.UserData.Token, principal.UserData.UserName, serverType);
        }
    }
}