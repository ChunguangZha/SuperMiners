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
            CreateNavigateUrl();
            
        }

        private void CreateNavigateUrl()
        {
            string baseUrl = "https://open.weixin.qq.com/connect/oauth2/authorize?";
            string redirectUriValue = "https://www.xlore.net/WeiXin/WeiXinResponse.aspx";
            string responseTypeValue = "code";
            string scopeValue = "snsapi_userinfo";
            string stateValue = "xunlin";
            this.link.NavigateUrl = baseUrl + "appid=" + Config.appid + "&redirect_uri=" + System.Web.HttpUtility.UrlEncode(redirectUriValue) + "&response_type=" + responseTypeValue + "&scope=" + scopeValue + "&state=" + stateValue + "#wechat_redirect";
            
        }
    }
}