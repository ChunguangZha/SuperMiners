using MetaData.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XunLinMineRemoteControlWeb.Wcf;

namespace XunLinMineRemoteControlWeb.Core
{
    public class Controller
    {
        public static bool GetPlayerInfo(string token, string userLoginName, string clientIP, HttpContext context, out string message)
        {
            message = "";
            WebPlayerInfo userinfo = WcfClient.Instance.GetPlayerInfo(token, userLoginName, clientIP);
            if (userinfo == null)
            {
                message = "登录失败。";
                return false;
            }
            if (userinfo.IsLocked)
            {
                message = "您的账户已经被锁定，无法登录，请联系管理员解决。";
                return false;
            }

            WebLoginUserInfo webloginPlayer = WebLoginUserInfo.FromWebPlayerInfo(userinfo);

            // 登录状态100分钟内有效
            MyFormsPrincipal<WebLoginUserInfo>.SignIn(webloginPlayer.UserLoginName, webloginPlayer, 30);
            MyFormsPrincipal<WebLoginUserInfo>.TrySetUserInfo(context);

            return true;
        }
    }
}