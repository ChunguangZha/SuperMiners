using MetaData;
using SuperMinersWeiXin.Controller;
using SuperMinersWeiXin.Utility;
using SuperMinersWeiXin.Wcf.Services;
using SuperMinersWeiXin.WeiXinCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SuperMinersWeiXin
{
    public partial class WeiXinResponse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    //code说明 ： code作为换取access_token的票据，每次用户授权带上的code将不一样，code只能使用一次，5分钟未被使用自动过期。
                    string code = Request["code"];
                    string state = Request["state"];

                    LogHelper.Instance.AddInfoLog("code:" + code + "; state: " + state);

                    if (state == Config.state)
                    {
                        bool isOK = WeiXinHandler.SynGetUserAccessToken(code);
                        if (!isOK)
                        {
                            Server.Transfer("ErrorPage.aspx");
                        }
                        else
                        {
                            Session["wxuserinfo"] = TokenController.WeiXinUserObj;
                            string ip = System.Web.HttpContext.Current.Request.UserHostAddress;

                            int result = WcfClient.Instance.WeiXinLogin(TokenController.WeiXinUserObj.openid, TokenController.WeiXinUserObj.nickname, ip);
                            if (result == OperResult.RESULTCODE_TRUE)
                            {
                                var player = WcfClient.Instance.GetPlayerByWeiXinOpenID(TokenController.WeiXinUserObj.openid);
                                MainController.Player = player;
                                Server.Transfer("~/Index.aspx");
                            }
                            else if (result == OperResult.RESULTCODE_USER_NOT_EXIST)
                            {
                                Server.Transfer("Login.aspx");
                            }
                            else
                            {
                                Response.Write("<script>alert('登录迅灵矿场失败, 原因为：" + OperResult.GetMsg(result) + "')</script>");
                            }
                        }
                    }
                    else
                    {

                    }
                }
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("WeiXinResponse Exception", exc);
            }
        }
    }
}