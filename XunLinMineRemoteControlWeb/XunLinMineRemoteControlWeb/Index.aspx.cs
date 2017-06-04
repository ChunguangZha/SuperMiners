using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using XunLinMineRemoteControlWeb.Core;

namespace XunLinMineRemoteControlWeb
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnBuyOnce_Click(object sender, EventArgs e)
        {
            MyFormsPrincipal<WebLoginUserInfo> principal = Context.User as MyFormsPrincipal<WebLoginUserInfo>;
            if (principal == null || principal.UserData == null)
            {
                Response.Redirect("/Login.aspx");
                return;
            }

            bool isBuyable = Wcf.WcfClient.Instance.CheckOnceRemoveServiceCanBuyable(principal.UserData.Token, principal.UserData.UserName);
            if (!isBuyable)
            {
                Response.Write("<script>alert('24小时内只能购买一次')</script>");
                return;
            }

            string alipayLink = Wcf.WcfClient.Instance.CreateBuyRemoteServerAlipayLink(principal.UserData.Token, principal.UserData.UserName, MetaData.Trade.RemoteServerType.Once);
            if (!string.IsNullOrEmpty(alipayLink))
            {
                Response.Redirect(alipayLink);
            }

        }

        protected void btnBuyOneMonth_Click(object sender, EventArgs e)
        {
            MyFormsPrincipal<WebLoginUserInfo> principal = Context.User as MyFormsPrincipal<WebLoginUserInfo>;
            if (principal == null || principal.UserData == null)
            {
                Response.Redirect("/Login.aspx");
                return;
            }

            string alipayLink = Wcf.WcfClient.Instance.CreateBuyRemoteServerAlipayLink(principal.UserData.Token, principal.UserData.UserName, MetaData.Trade.RemoteServerType.OneMonth);
            if (!string.IsNullOrEmpty(alipayLink))
            {
                Response.Redirect(alipayLink);
            }

        }

        protected void btnBuyThreeMonth_Click(object sender, EventArgs e)
        {
            MyFormsPrincipal<WebLoginUserInfo> principal = Context.User as MyFormsPrincipal<WebLoginUserInfo>;
            if (principal == null || principal.UserData == null)
            {
                Response.Redirect("/Login.aspx");
                return;
            }

            string alipayLink = Wcf.WcfClient.Instance.CreateBuyRemoteServerAlipayLink(principal.UserData.Token, principal.UserData.UserName, MetaData.Trade.RemoteServerType.ThreeMonth);
            if (!string.IsNullOrEmpty(alipayLink))
            {
                Response.Redirect(alipayLink);
            }

        }

        protected void btnBuyOneYear_Click(object sender, EventArgs e)
        {
            MyFormsPrincipal<WebLoginUserInfo> principal = Context.User as MyFormsPrincipal<WebLoginUserInfo>;
            if (principal == null || principal.UserData == null)
            {
                Response.Redirect("/Login.aspx");
                return;
            }

            string alipayLink = Wcf.WcfClient.Instance.CreateBuyRemoteServerAlipayLink(principal.UserData.Token, principal.UserData.UserName, MetaData.Trade.RemoteServerType.OneYear);
            if (!string.IsNullOrEmpty(alipayLink))
            {
                Response.Redirect(alipayLink);
            }

        }
    }
}