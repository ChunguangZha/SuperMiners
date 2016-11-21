using SuperMinersWeb.Utility;
using SuperMinersWeb.WeiXin.Controller;
using SuperMinersWeb.WeiXin.Model;
using SuperMinersWeb.WeiXin.WeiXinCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SuperMinersWeb.WeiXin
{
    public partial class WeiXinResponse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //code说明 ： code作为换取access_token的票据，每次用户授权带上的code将不一样，code只能使用一次，5分钟未被使用自动过期。
                string code = Request["code"];
                string state = Request["state"];

                LogHelper.Instance.AddInfoLog("code:" + code + "; state: " + state );

                if (state == Config.state)
                {
                    //WeiXinHandler.GetUserInfoSucceed += WeiXinHandler_GetUserInfoSucceed;
                    //WeiXinHandler.AccessWeiXinServerException += WeiXinHandler_AccessWeiXinServerException;
                    //WeiXinHandler.AccessWeiXinServerReturnError += WeiXinHandler_AccessWeiXinServerReturnError;
                    bool isOK = WeiXinHandler.SynGetUserAccessToken(code);
                    if (!isOK)
                    {
                        Server.Transfer("ErrorPage.aspx");
                    }
                    else
                    {
                        this.lblMsg.Text = "欢迎进入迅灵矿场";
                    }
                }
                else
                {

                }
            }
        }

        void WeiXinHandler_AccessWeiXinServerReturnError(string arg1, ErrorModel arg2)
        {
            TokenController.ErrorObj = arg2;
            lblMsg.Text = "在调用接口" + arg1 + "时，微信服务器返回错误。信息为：" + arg2;
        }

        void WeiXinHandler_AccessWeiXinServerException(string obj)
        {
            lblMsg.Text = "调用信息服务器异常。" + obj;
        }

        void WeiXinHandler_GetUserInfoSucceed()
        {
            lblMsg.Text = "欢迎 " + TokenController.WeiXinUserObj.nickname + " 进入矿场，OpenID : " + TokenController.WeiXinUserObj.openid;
        }

        protected void btnRedirect_Click(object sender, EventArgs e)
        {
            Server.Transfer("ErrorPage.aspx");
        }

    }
}