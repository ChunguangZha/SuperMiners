using MetaData;
using SuperMinersWeiXin.Controller;
using SuperMinersWeiXin.Core;
using SuperMinersWeiXin.Model;
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

                    this.lblMsg.Text = "code:" + code + "; state: " + state;

                    LogHelper.Instance.AddInfoLog("code:" + code + "; state: " + state);

                    if (state == Config.state)
                    {
                        HttpGetReturnModel resultValue = WeiXinHandler.SynGetUserAccessToken(code);
                        if (resultValue.Exception != null)
                        {
                            this.lblMsg.Text = "登录异常，请联系迅灵矿场管理员";
                            return;
                        }

                        if (resultValue.ResponseError != null)
                        {
                            Session[Config.SESSIONKEY_RESPONSEERROR] = resultValue.ResponseError;
                            Server.Transfer("ErrorPage.aspx");
                            return;
                        }

                        AuthorizeResponseModel authObj = resultValue.ResponseResult as AuthorizeResponseModel;
                        if (authObj != null)
                        {
                            this.lblMsg.Text = "authObj OK";
                            Session[Config.SESSIONKEY_AUTHORIZEOBJ] = authObj;
                            resultValue = WeiXinHandler.SyncGetUserInfo(authObj.access_token, authObj.openid);
                        }
                        if (resultValue.Exception != null)
                        {
                            this.lblMsg.Text = "登录异常，请联系迅灵矿场管理员";
                            return;
                        }

                        if (resultValue.ResponseError != null)
                        {
                            Session[Config.SESSIONKEY_RESPONSEERROR] = resultValue.ResponseError;
                            Server.Transfer("ErrorPage.aspx");
                            return;
                        }

                        WeiXinUserInfoModel userObj = resultValue.ResponseResult as WeiXinUserInfoModel;
                        Session[Config.SESSIONKEY_WXUSERINFO] = userObj;
                        string ip = System.Web.HttpContext.Current.Request.UserHostAddress;
                        this.lblMsg.Text = "欢迎  " + userObj.nickname + "  进入迅灵矿场";

                        int result = WcfClient.Instance.WeiXinLogin(userObj.openid, userObj.nickname, ip);

                        this.lblMsg.Text = "登录迅灵矿场，结果为：" + OperResult.GetMsg(result);
                        if (result == OperResult.RESULTCODE_TRUE)
                        {
                            this.lblMsg.Text = "WeiXinLogin OK";
                            var player = WcfClient.Instance.GetPlayerByWeiXinOpenID(userObj.openid);

                            this.lblMsg.Text = "player OK";
                            WebUserInfo userinfo = new WebUserInfo();
                            userinfo.xlUserID = player.SimpleInfo.UserID;
                            userinfo.xlUserName = player.SimpleInfo.UserName;
                            userinfo.wxOpenID = userObj.openid;

                            // 登录状态100分钟内有效
                            MyFormsPrincipal<WebUserInfo>.SignIn(userinfo.xlUserName, userinfo, 100);
                            Session[userinfo.xlUserName] = player;

                            Server.Transfer("View/Index.aspx");
                        }
                        else if (result == OperResult.RESULTCODE_USER_NOT_EXIST)
                        {
                            Server.Transfer("LoginPage.aspx");
                        }
                        else
                        {
                            Response.Write("<script>alert('登录迅灵矿场失败, 原因为：" + OperResult.GetMsg(result) + "')</script>");
                        }
                    }
                    else
                    {

                    }
                }
            }
            catch (Exception exc)
            {
                this.lblMsg.Text = "WeiXinResponse Exception. " + exc.Message;
                LogHelper.Instance.AddErrorLog("WeiXinResponse Exception", exc);
            }
        }
    }
}