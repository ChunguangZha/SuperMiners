using MetaData;
using MetaData.User;
using SuperMinersServerApplication.Controller;
using SuperMinersServerApplication.Utility;
using SuperMinersServerApplication.WebServiceToWeb.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.WebServiceToWeb.Services
{
    public partial class ServiceToWeb : IServiceToWeb
    {

        public string GetAccessToken()
        {
            try
            {
                return App.TokenController.GetWeiXinAccessToken();
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("GetAccessToken Exception. ", exc);

                return "";
            }
        }

        public int BindWeiXinUser(string wxUserOpenID, string wxUserName, string xlUserName, string xlUserPassword, string ip)
        {
            try
            {
                int userID = DBProvider.UserDBProvider.GetUserIDByWeiXinOpenID(wxUserOpenID);
                if (userID > 0)
                {
                    return OperResult.RESULTCODE_WEIXIN_USERBINDEDBYOTHER;
                }

                PlayerInfo player = DBProvider.UserDBProvider.GetPlayer(xlUserName);
                if (player == null)
                {
                    return OperResult.RESULTCODE_USER_NOT_EXIST;
                }
                if (xlUserPassword != player.SimpleInfo.Password)
                {
                    return OperResult.RESULTCODE_USER_NOT_EXIST;
                }

                int result = PlayerController.Instance.BindWeiXinUser(wxUserOpenID, player);
                if (result == OperResult.RESULTCODE_TRUE)
                {
                    LogHelper.Instance.AddInfoLog("wxUserOpenID: " + wxUserOpenID + "，成功绑定用户：" + xlUserName);
                    if (player.SimpleInfo.LockedLogin)
                    {
                        return OperResult.RESULTCODE_USER_IS_LOCKED;
                    }

                    string mac = "weixin";
                    player.SimpleInfo.LastLoginIP = ip;
                    player.SimpleInfo.LastLoginMac = mac;
                    PlayerController.Instance.WeiXinLoginPlayer(wxUserOpenID, player);
                    LogHelper.Instance.AddInfoLog("微信玩家 [" + wxUserName + "] 登录用户[" + player.SimpleInfo.UserName + "]成功, IP=" + ip + ", Mac=" + mac);

                }

                return result;
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("wxUserOpenID: " + wxUserOpenID + "，绑定用户：[" + xlUserName + "]失败.", exc);
                return OperResult.RESULTCODE_EXCEPTION;
            }
        }

        public int WeiXinLogin(string wxUserOpenID, string wxUserName, string ip)
        {
            string mac = "weixin";
            try
            {
                PlayerInfo player = DBProvider.UserDBProvider.GetPlayerByWeiXinOpenID(wxUserOpenID);
                if (player == null)
                {
                    return OperResult.RESULTCODE_USER_NOT_EXIST;
                }
                if (player.SimpleInfo.LockedLogin)
                {
                    return OperResult.RESULTCODE_USER_IS_LOCKED;
                }
                
                player.SimpleInfo.LastLoginIP = ip;
                player.SimpleInfo.LastLoginMac = mac;
                PlayerController.Instance.WeiXinLoginPlayer(wxUserOpenID, player);

                //RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                //rsa.FromXmlString(key);

                //token = Guid.NewGuid().ToString();
                //RSAProvider.SetRSA(token, rsa);
                //RSAProvider.LoadRSA(token);

                //ClientManager.AddClient(userName, token);
                //lock (this._callbackDicLocker)
                //{
                //    this._callbackDic[token] = new Queue<CallbackInfo>();
                //}

                LogHelper.Instance.AddInfoLog("微信玩家 [" + wxUserName + "] 登录用户[" + player .SimpleInfo.UserName+ "]成功, IP=" + ip + ", Mac=" + mac);

                return OperResult.RESULTCODE_TRUE;
            }
            catch (Exception ex)
            {
                LogHelper.Instance.AddErrorLog("微信玩家 [" + wxUserName + "] 登录失败, IP=" + ip + ", Mac=" + mac, ex);
                return OperResult.RESULTCODE_EXCEPTION;
            }
            //if (!string.IsNullOrEmpty(token))
            //{
            //    PlayerActionController.Instance.AddLog(userName, MetaData.ActionLog.ActionType.Login, 1);
            //    new Thread(new ParameterizedThreadStart(o =>
            //    {
            //        this.LogedIn(o.ToString());
            //    })).Start(token);
            //}

            //return token;
        }


        public MetaData.User.PlayerInfo GetPlayerByWeiXinOpenID(string openid)
        {
            try
            {
                return PlayerController.Instance.GetPlayerByWeiXinOpenID(openid);
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("GetPlayerByWeiXinOpenID Exception openid=" + openid , exc);
                return null;
            }
        }
    }
}
