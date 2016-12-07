using SuperMinersWeiXin.Model;
using SuperMinersWeiXin.Utility;
using SuperMinersWeiXin.WeiXinCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperMinersWeiXin.Controller
{
    public class WeiXinHandler
    {
        //public static event Action<string> AccessWeiXinServerException;
        //public static event Action<string, ErrorModel> AccessWeiXinServerReturnError;
        //public static event Action GetUserInfoSucceed;

        public static string CreateGetCodeUrl()
        {
            string baseUrl = "https://open.weixin.qq.com/connect/oauth2/authorize?";
            string redirectUriValue = "http://weixin.xlore.net/WeiXinResponse.aspx";
            string responseTypeValue = "code";
            string scopeValue = "snsapi_userinfo";
            string url = baseUrl + "appid=" + Config.appid + "&redirect_uri=" + System.Web.HttpUtility.UrlEncode(redirectUriValue) + "&response_type=" + responseTypeValue + "&scope=" + scopeValue + "&state=" + Config.state + "#wechat_redirect";
            return url;
        }

        public static string CreateCreateMenuUrl(string access_token)
        {
            string baseUrl = "https://api.weixin.qq.com/cgi-bin/menu/create?";
            string url = baseUrl + "access_token=" + access_token;
            return url;
        }

        public static void AsyncGetUserCode()
        {
            string url = CreateGetCodeUrl();
            LogHelper.Instance.AddInfoLog("Start AsyncGetUserCode");
            HttpHandler.AsyncGet(url);
            //return isOK;
        }

        public static HttpGetReturnModel SynGetUserAccessToken(string code)
        {
            string baseurl = "https://api.weixin.qq.com/sns/oauth2/access_token?";
            string url = baseurl + "appid=" + Config.appid + "&secret=" + Config.appSecret + "&code=" + code + "&grant_type=authorization_code";

            LogHelper.Instance.AddInfoLog("Start AsynGetUserAccessToken code: " + code);
            return HttpHandler.SyncGet<AuthorizeResponseModel>(url);
            //if (result.Exception != null)
            //{
            //    return false;
            //}

            //if (result.ResponseError != null)
            //{
            //    TokenController.ErrorObj = result.ResponseError;
            //    return false;
            //}

            //TokenController.AuthorizeObj = result.ResponseResult as AuthorizeResponseModel;
            //if (TokenController.AuthorizeObj != null)
            //{
            //    SyncGetUserInfo(TokenController.AuthorizeObj.access_token, TokenController.AuthorizeObj.openid);
            //}
            //return true;
        }

        public static HttpGetReturnModel SyncRefreshUserAccessToken(string refresh_token)
        {
            string baseurl = "https://api.weixin.qq.com/sns/oauth2/refresh_token?";
            string url = baseurl + "appid=" + Config.appid + "&grant_type=refresh_token&refresh_token=" + refresh_token;

            LogHelper.Instance.AddInfoLog("Start AsyncRefreshUserAccessToken");
            return HttpHandler.SyncGet<AuthorizeResponseModel>(url);
            //if (result.Exception != null)
            //{
            //    if (AccessWeiXinServerException != null)
            //    {
            //        AccessWeiXinServerException(result.Exception.Message);
            //    }
            //    return false;
            //}

            //if (result.ResponseError != null)
            //{
            //    //if (AccessWeiXinServerReturnError != null)
            //    //{
            //    //    AccessWeiXinServerReturnError("AsyncRefreshUserAccessToken", result.ResponseError);
            //    //}
            //    return false;
            //}

            //return true;
        }

        public static HttpGetReturnModel SyncGetUserInfo(string access_token, string openid)
        {
            string baseurl = "https://api.weixin.qq.com/sns/userinfo?";
            string url = baseurl + "appid=" + Config.appid + "&access_token=" + access_token + "&openid=" + openid + "&lang=zh_CN";

            LogHelper.Instance.AddInfoLog("Start AsyncGetUserInfo");
            return HttpHandler.SyncGet<WeiXinUserInfoModel>(url);
            //if (result.Exception != null)
            //{
            //    if (AccessWeiXinServerException != null)
            //    {
            //        AccessWeiXinServerException(result.Exception.Message);
            //    }
            //    return false;
            //}

            //if (result.ResponseError != null)
            //{
            //    if (AccessWeiXinServerReturnError != null)
            //    {
            //        AccessWeiXinServerReturnError("AsyncGetUserInfo", result.ResponseError);
            //    }
            //    return false;
            //}

            //TokenController.WeiXinUserObj = result.ResponseResult as WeiXinUserInfoModel;
            ////if (GetUserInfoSucceed != null)
            ////{
            ////    GetUserInfoSucceed();
            ////}

            //return true;
        }

        public static bool AsyncJudgeAccessTokenValid(string access_token, string openid)
        {
            string baseurl = "https://api.weixin.qq.com/sns/auth?";
            string url = baseurl + "access_token=" + access_token + "&openid=" + openid;

            LogHelper.Instance.AddInfoLog("Start AsyncJudgeAccessTokenValid");
            HttpGetReturnModel result = HttpHandler.SyncGet<ErrorModel>(url);
            if (result.Exception != null)
            {
                //if (AccessWeiXinServerException != null)
                //{
                //    AccessWeiXinServerException(result.Exception.Message);
                //}
                return false;
            }

            if (result.ResponseError != null)
            {
                //if (AccessWeiXinServerReturnError != null)
                //{
                //    AccessWeiXinServerReturnError("AsyncJudgeAccessTokenValid", result.ResponseError);
                //}
                return false;
            }
            return true;
        }
    }
}