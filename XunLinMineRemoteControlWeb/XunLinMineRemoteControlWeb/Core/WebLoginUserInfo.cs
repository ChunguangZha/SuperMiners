using MetaData.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace XunLinMineRemoteControlWeb.Core
{
    public class WebLoginUserInfo
    {
        public string UserLoginName;

        public string UserName;

        public string Token;

        public decimal ShoppingCredits;

        public string UserRemoteServerValidStopTimeText = "";

        public static WebLoginUserInfo FromWebPlayerInfo(WebPlayerInfo playerInfo)
        {
            WebLoginUserInfo webloginPlayer = new WebLoginUserInfo()
            {
                ShoppingCredits = playerInfo.ShoppingCredits,
                Token = playerInfo.Token,
                UserLoginName = playerInfo.UserLoginName,
                UserName = playerInfo.UserName
            };

            if (playerInfo.UserRemoteServerValidStopTime != null && playerInfo.IsLongTermRemoteServiceUser)
            {
                webloginPlayer.UserRemoteServerValidStopTimeText = playerInfo.UserRemoteServerValidStopTime.ToDateTime().ToString();
            }
            else if (playerInfo.UserRemoteServiceValidTimes > 0)
            {
                webloginPlayer.UserRemoteServerValidStopTimeText = playerInfo.UserRemoteServiceValidTimes.ToString() + "次";
            }
            else
            {
                webloginPlayer.UserRemoteServerValidStopTimeText = "无";
            }
            return webloginPlayer;
        }
    }
}