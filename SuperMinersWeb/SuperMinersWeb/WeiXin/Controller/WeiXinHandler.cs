using SuperMinersWeb.WeiXin.Model;
using SuperMinersWeb.WeiXin.WeiXinCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperMinersWeb.WeiXin.Controller
{
    public class WeiXinHandler
    {
        public static event Action<string> AccessWeiXinServerException;
        public static event Action<string, ErrorModel> AccessWeiXinServerReturnError;
        public static event Action GetUserInfoSucceed;

        public static string CreateGetCodeUrl()
        {
            string baseUrl = "https://open.weixin.qq.com/connect/oauth2/authorize?";
            string redirectUriValue = "http://www.xlore.net/WeiXin/WeiXinResponse.aspx";
            string responseTypeValue = "code";
            string scopeValue = "snsapi_userinfo";
            string url = baseUrl + "appid=" + Config.appid + "&redirect_uri=" + System.Web.HttpUtility.UrlEncode(redirectUriValue) + "&response_type=" + responseTypeValue + "&scope=" + scopeValue + "&state=" + Config.state + "#wechat_redirect";
            return url;
        }

        public static void AsyncGetUserCode()
        {
            string url = CreateGetCodeUrl();
            SuperMinersWeb.Utility.LogHelper.Instance.AddInfoLog("Start AsyncGetUserCode");
            HttpHandler.AsyncGet(url);
            //return isOK;
        }

        public static bool AsynGetUserAccessToken(string code)
        {
            string baseurl = "https://api.weixin.qq.com/sns/oauth2/access_token?";
            string url = baseurl + "appid=" + Config.appid + "&secret=" + Config.appSecret + "&code=" + code + "&grant_type=authorization_code";

            SuperMinersWeb.Utility.LogHelper.Instance.AddInfoLog("Start AsynGetUserAccessToken");
            bool isOK = HttpHandler.AsyncGet<AuthorizeResponseModel>(url, result =>
            {
                if (result.Exception != null)
                {
                    if (AccessWeiXinServerException != null)
                    {
                        AccessWeiXinServerException(result.Exception.Message);
                    }
                    return;
                }

                if (result.ResponseError != null)
                {
                    if (AccessWeiXinServerReturnError != null)
                    {
                        AccessWeiXinServerReturnError("AsynGetUserAccessToken", result.ResponseError);
                    }
                    //Response.Write("<script>alert('微信服务器返回错误。错误码为：" + obj.ResponseError.errcode + " ; 错误信息为：" + obj.ResponseError.errmsg + "')</script>");
                    return;
                }

                TokenController.AuthorizeObj = result.ResponseResult as AuthorizeResponseModel;
                if (TokenController.AuthorizeObj != null)
                {
                    AsyncGetUserInfo(TokenController.AuthorizeObj.access_token, TokenController.AuthorizeObj.openid);
                }
            });
            return isOK;
        }

        public static bool AsyncRefreshUserAccessToken(string refresh_token)
        {
            string baseurl = "https://api.weixin.qq.com/sns/oauth2/refresh_token?";
            string url = baseurl + "appid=" + Config.appid + "&grant_type=refresh_token&refresh_token=" + refresh_token;

            SuperMinersWeb.Utility.LogHelper.Instance.AddInfoLog("Start AsyncRefreshUserAccessToken");
            bool isOK = HttpHandler.AsyncGet<AuthorizeResponseModel>(url, result =>
            {
                if (result.Exception != null)
                {
                    if (AccessWeiXinServerException != null)
                    {
                        AccessWeiXinServerException(result.Exception.Message);
                    }
                    return;
                }

                if (result.ResponseError != null)
                {
                    if (AccessWeiXinServerReturnError != null)
                    {
                        AccessWeiXinServerReturnError("AsyncRefreshUserAccessToken", result.ResponseError);
                    }
                    return;
                }
            });
            return isOK;
        }

        public static bool AsyncGetUserInfo(string access_token, string openid)
        {
            string baseurl = "https://api.weixin.qq.com/sns/userinfo?";
            string url = baseurl + "appid=" + Config.appid + "&access_token=" + access_token + "&openid=" + openid + "&lang=zh_CN";

            SuperMinersWeb.Utility.LogHelper.Instance.AddInfoLog("Start AsyncGetUserInfo");
            bool isOK = HttpHandler.AsyncGet<WeiXinUserInfoModel>(url, result =>
            {
                if (result.Exception != null)
                {
                    if (AccessWeiXinServerException != null)
                    {
                        AccessWeiXinServerException(result.Exception.Message);
                    }
                    return;
                }

                if (result.ResponseError != null)
                {
                    if (AccessWeiXinServerReturnError != null)
                    {
                        AccessWeiXinServerReturnError("AsyncGetUserInfo", result.ResponseError);
                    }
                    return;
                }

                TokenController.WeiXinUserObj = result.ResponseResult as WeiXinUserInfoModel;
                if (GetUserInfoSucceed != null)
                {
                    GetUserInfoSucceed();
                }
            });
            return isOK;
        }

        public static bool AsyncJudgeAccessTokenValid(string access_token, string openid)
        {
            string baseurl = "https://api.weixin.qq.com/sns/auth?";
            string url = baseurl + "access_token=" + access_token + "&openid=" + openid;

            SuperMinersWeb.Utility.LogHelper.Instance.AddInfoLog("Start AsyncJudgeAccessTokenValid");
            bool isOK = HttpHandler.AsyncGet<ErrorModel>(url, result =>
            {
                if (result.Exception != null)
                {
                    if (AccessWeiXinServerException != null)
                    {
                        AccessWeiXinServerException(result.Exception.Message);
                    }
                    return;
                }

                if (result.ResponseError != null)
                {
                    if (AccessWeiXinServerReturnError != null)
                    {
                        AccessWeiXinServerReturnError("AsyncJudgeAccessTokenValid", result.ResponseError);
                    }
                    return;
                }
            });
            return isOK;
        }
    }
}