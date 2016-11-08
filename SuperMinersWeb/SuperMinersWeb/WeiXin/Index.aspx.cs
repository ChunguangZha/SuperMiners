using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SuperMinersWeb.WeiXin
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string baseUrl = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=APPID&redirect_uri=REDIRECT_URI&response_type=code&scope=SCOPE&state=STATE#wechat_redirect";
            string appidValue = "wx5e9997a820875035";
            string redirectUriValue = "https://www.xlore.net/";
            string responseTypeValue = "code";
            string scopeValue = "snsapi_userinfo";
            string stateValue = "xunlin";
            this.link.NavigateUrl = baseUrl + "appid=" + appidValue + "&redirect_uri=" + System.Web.HttpUtility.HtmlEncode(redirectUriValue) + "&response_type=" + responseTypeValue + "&scope=" + scopeValue + "&state=" + stateValue + "#wechat_redirect";
            
        }
    }
}