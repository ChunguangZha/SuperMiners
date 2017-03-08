using DataBaseProvider;
using MetaData.SystemConfig;
using MetaData;
using MetaData.User;
using SuperMinersServerApplication.Controller;
using SuperMinersServerApplication.Controller.Trade;
using SuperMinersServerApplication.Utility;
using SuperMinersServerApplication.WebServiceToWeb.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaData.Trade;

namespace SuperMinersServerApplication.WebServiceToWeb.Services
{
    public partial class ServiceToWeb : IServiceToWeb
    {
        public bool Active()
        {
            return true;
        }

        public OperResultObject Login(string clientIP, string userLoginName, string password)
        {
            OperResultObject resultObj = new OperResultObject();

            string token = "";
            PlayerInfo player = null;
            try
            {
                player = PlayerController.Instance.GetPlayerInfoByUserLoginName(userLoginName);
                if (player == null)
                {
                    resultObj.OperResultCode = OperResult.RESULTCODE_USERNAME_PASSWORD_ERROR;
                    return resultObj;
                }
                token = WebClientManager.GetToken(player.SimpleInfo.UserName);
                if (!string.IsNullOrEmpty(token))
                {
                    WebClientManager.RemoveClient(token);
                }
                
                if (password != player.SimpleInfo.Password)
                {
                    resultObj.OperResultCode = OperResult.RESULTCODE_USERNAME_PASSWORD_ERROR;
                    return resultObj;
                }

                resultObj = PlayerController.Instance.CheckPlayerIsLocked(player.SimpleInfo.UserID, player.SimpleInfo.UserName);
                if (resultObj.OperResultCode != OperResult.RESULTCODE_TRUE)
                {
                    return resultObj;
                }

                token = Guid.NewGuid().ToString();
                WebClientManager.AddClient(player.SimpleInfo.UserName, token, clientIP);

                LogHelper.Instance.AddInfoLog("WEB 玩家登录名 [" + userLoginName + "] 登录矿场, IP=" + clientIP);

                resultObj.OperResultCode = OperResult.RESULTCODE_TRUE;
                resultObj.Message = token;
                return resultObj;
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("WEB 玩家登录名 [" + userLoginName + "] 登录矿场失败, IP=" + clientIP, exc);
                resultObj.OperResultCode = OperResult.RESULTCODE_EXCEPTION;
                return resultObj;
            }
        }

        public WebPlayerInfo GetPlayerInfo(string token, string userLoginName, string clientIP)
        {
            try
            {
                string userName = WebClientManager.GetClientUserName(token);
                if (string.IsNullOrEmpty(userName))
                {
                    return null;
                }

                var playerInfo = PlayerController.Instance.GetPlayerInfoByUserName(userName);
                if (playerInfo.SimpleInfo.UserLoginName != userLoginName)
                {
                    return null;
                }

                WebPlayerInfo webPlayerInfo = new WebPlayerInfo()
                {
                    Token = token,
                    UserName = playerInfo.SimpleInfo.UserName,
                    UserLoginName = playerInfo.SimpleInfo.UserLoginName,
                    ShoppingCredits = playerInfo.FortuneInfo.ShoppingCreditsEnabled
                };
                if (playerInfo.FortuneInfo.UserRemoteServerValidStopTime != null)
                {
                    DateTime stopTime = playerInfo.FortuneInfo.UserRemoteServerValidStopTime.ToDateTime();
                    if (stopTime >= DateTime.Now)
                    {
                        webPlayerInfo.UserRemoteServerValidStopTime = playerInfo.FortuneInfo.UserRemoteServerValidStopTime;
                    }
                }
                webPlayerInfo.IsLongTermRemoteServiceUser = playerInfo.FortuneInfo.IsLongTermRemoteServiceUser;
                webPlayerInfo.UserRemoteServiceValidTimes = playerInfo.FortuneInfo.UserRemoteServiceValidTimes;

                return webPlayerInfo;
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("WEB 玩家登录名 [" + userLoginName + "] 登录矿场失败, IP=" + clientIP, exc);
                return null;
            }
        }

        public UserRemoteServerItem[] GetUserRemoteServerItems(string token, string userName)
        {
            try
            {
                return UserRemoteServerController.Instance.GetUserRemoteServerItems();
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("WEB 玩家用户名 [" + userName + "] GetUserRemoteServerItems失败" , exc);
                return null;
            }
        }

        public string CreateBuyRemoteServerAlipayLink(string token, string userName, RemoteServerType serverType)
        {
            AlipayTradeInType alipayType;
            switch (serverType)
            {
                case RemoteServerType.Once:
                    alipayType = AlipayTradeInType.RemoteServerOnce;
                    break;
                case RemoteServerType.OneMonth:
                    alipayType = AlipayTradeInType.RemoteServerOneMonth;
                    break;
                case RemoteServerType.ThreeMonth:
                    alipayType = AlipayTradeInType.RemoteServerThreeMonth;
                    break;
                case RemoteServerType.OneYear:
                    alipayType = AlipayTradeInType.RemoteServerOneYear;
                    break;
                default:
                    return null;
            }

            var serverItem = UserRemoteServerController.Instance.GetUserRemoteServerItem(serverType);
            if(serverItem==null){
                return null;
            }

            DateTime time = DateTime.Now;
            string orderNumber = OrderController.Instance.CreateOrderNumber(userName, time, alipayType);
            string alipayLink = OrderController.Instance.CreateAlipayLink(userName, orderNumber, serverItem.ShopName, serverItem.PayMoneyYuan * GlobalConfig.GameConfig.Yuan_RMB, serverItem.Description);
            return alipayLink;

        }



        /// <summary>
        /// RESULTCODE_REGISTER_USERNAME_LENGTH_SHORT; RESULTCODE_FALSE; RESULTCODE_REGISTER_USERNAME_EXIST; RESULTCODE_TRUE; RESULTCODE_EXCEPTION
        /// </summary>
        /// <param name="clientIP"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <param name="qq"></param>
        /// <param name="invitationCode"></param>
        /// <returns></returns>
        public int RegisterUser(string clientIP, string userLoginName, string userName, string password,
            string alipayAccount, string alipayRealName, string IDCardNo, string email, string qq, string invitationCode)
        {
            try
            {
                if (string.IsNullOrEmpty(userLoginName) || userLoginName.Length < 3)
                {
                    return OperResult.RESULTCODE_REGISTER_USERNAME_LENGTH_SHORT;
                }
                if (string.IsNullOrEmpty(password))
                {
                    return OperResult.RESULTCODE_PARAM_INVALID;
                }

#if V1
                if (string.IsNullOrEmpty(alipayAccount))
                {
                    return OperResult.RESULTCODE_PARAM_INVALID;
                }
                if (string.IsNullOrEmpty(alipayRealName))
                {
                    return OperResult.RESULTCODE_PARAM_INVALID;
                }

#endif

                if (string.IsNullOrEmpty(IDCardNo))
                {
                    return OperResult.RESULTCODE_PARAM_INVALID;
                }
                if (string.IsNullOrEmpty(email))
                {
                    return OperResult.RESULTCODE_PARAM_INVALID;
                }
                return PlayerController.Instance.RegisterUser(clientIP, userLoginName, userName, password, alipayAccount, alipayRealName, IDCardNo, email, qq, invitationCode);
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("RegisterUser Exception. clientIP:" + clientIP + ",loginUserName: " + userLoginName + ",password: " + password
                                    + "alipayAccount:" + alipayAccount + "alipayRealName:" + alipayRealName + ",IDCardNo:" + IDCardNo + ",email: " + email + ",qq: " + qq, exc);

                return OperResult.RESULTCODE_EXCEPTION;
            }
        }

        /// <summary>
        /// RESULTCODE_REGISTER_USERNAME_LENGTH_SHORT; RESULTCODE_FALSE; RESULTCODE_REGISTER_USERNAME_EXIST; RESULTCODE_TRUE; RESULTCODE_EXCEPTION
        /// </summary>
        /// <param name="clientIP"></param>
        /// <param name="userLoginName"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <param name="qq"></param>
        /// <param name="agentUserName"></param>
        /// <returns></returns>
        public int RegisterUserByAgent(string clientIP, string userLoginName, string userName, string password,
            string alipayAccount, string alipayRealName, string IDCardNo, string email, string qq, string agentUserName)
        {
            try
            {
                if (string.IsNullOrEmpty(userLoginName) || userLoginName.Length < 3)
                {
                    return OperResult.RESULTCODE_REGISTER_USERNAME_LENGTH_SHORT;
                }
                if (string.IsNullOrEmpty(password))
                {
                    return OperResult.RESULTCODE_PARAM_INVALID;
                }
                if (string.IsNullOrEmpty(alipayAccount))
                {
                    return OperResult.RESULTCODE_PARAM_INVALID;
                }
                if (string.IsNullOrEmpty(alipayRealName))
                {
                    return OperResult.RESULTCODE_PARAM_INVALID;
                }
                if (string.IsNullOrEmpty(IDCardNo))
                {
                    return OperResult.RESULTCODE_PARAM_INVALID;
                }
                if (string.IsNullOrEmpty(email))
                {
                    return OperResult.RESULTCODE_PARAM_INVALID;
                }

                var agent = PlayerController.Instance.GetPlayerInfoByUserName(agentUserName);
                if (agent == null)
                {
                    return OperResult.RESULTCODE_FALSE;
                }

                return PlayerController.Instance.RegisterUser(clientIP, userLoginName, userName, password, alipayAccount, alipayRealName, IDCardNo, email, qq, agent.SimpleInfo.InvitationCode);
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("RegisterUser Exception. clientIP:" + clientIP + ",loginUserName: " + userLoginName + ",password: " + password
                                    + "alipayAccount:" + alipayAccount + "alipayRealName:" + alipayRealName + ",IDCardNo:" + IDCardNo + ",email: " + email + ",qq: " + qq, exc);

                return OperResult.RESULTCODE_EXCEPTION;
            }
        }

        /// <summary>
        /// RESULTCODE_PARAM_INVALID; RESULTCODE_TRUE; RESULTCODE_FALSE; RESULTCODE_EXCEPTION
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public int CheckUserNameExist(string userName)
        {
            try
            {
                if (string.IsNullOrEmpty(userName))
                {
                    return OperResult.RESULTCODE_PARAM_INVALID;
                }
                int count = DBProvider.UserDBProvider.GetPlayerCountByUserName(userName);
                if (count == 0)
                {
                    //不存在
                    return OperResult.RESULTCODE_FALSE;
                }

                return OperResult.RESULTCODE_TRUE;
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("CheckUserNameExist Exception. userName: " + userName, exc);

                return OperResult.RESULTCODE_EXCEPTION;
            }
        }

        /// <summary>
        /// RESULTCODE_PARAM_INVALID; RESULTCODE_TRUE; RESULTCODE_FALSE; RESULTCODE_EXCEPTION
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public int CheckUserLoginNameExist(string userLoginName)
        {
            try
            {
                if (string.IsNullOrEmpty(userLoginName))
                {
                    return OperResult.RESULTCODE_PARAM_INVALID;
                }
                int count = DBProvider.UserDBProvider.GetPlayerCountByLoginUserName(userLoginName);
                if (count == 0)
                {
                    //不存在
                    return OperResult.RESULTCODE_FALSE;
                }

                return OperResult.RESULTCODE_TRUE;
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("CheckNickNameExist Exception. nickName: " + userLoginName, exc);

                return OperResult.RESULTCODE_EXCEPTION;
            }
        }

        /// <summary>
        /// RESULTCODE_PARAM_INVALID; RESULTCODE_TRUE; RESULTCODE_FALSE; RESULTCODE_EXCEPTION
        /// </summary>
        /// <param name="alipayAccount"></param>
        /// <param name="alipayRealName"></param>
        /// <returns></returns>
        public int CheckUserAlipayAccountExist(string alipayAccount)
        {
            try
            {
                return PlayerController.Instance.CheckUserAlipayAccountExist(alipayAccount);
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("CheckUserAlipayAccountExist Exception. alipayAccount: " + alipayAccount, exc);

                return OperResult.RESULTCODE_EXCEPTION;
            }
        }

        /// <summary>
        /// RESULTCODE_PARAM_INVALID; RESULTCODE_TRUE; RESULTCODE_FALSE; RESULTCODE_EXCEPTION
        /// </summary>
        /// <param name="alipayAccount"></param>
        /// <param name="alipayRealName"></param>
        /// <returns></returns>
        public int CheckUserAlipayRealNameExist(string alipayRealName)
        {
            try
            {
                return PlayerController.Instance.CheckUserAlipayRealNameExist(alipayRealName);
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("CheckUserAlipayRealNameExist Exception. alipayRealName: " + alipayRealName, exc);

                return OperResult.RESULTCODE_EXCEPTION;
            }
        }

        public int CheckUserIDCardNoExist(string IDCardNo)
        {
            try
            {
                return PlayerController.Instance.CheckUserIDCardNoExist(IDCardNo);
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("CheckUserIDCardNoExist Exception. IDCardNo: " + IDCardNo, exc);

                return OperResult.RESULTCODE_EXCEPTION;
            }
        }

        /// <summary>
        /// RESULTCODE_PARAM_INVALID; RESULTCODE_TRUE; RESULTCODE_FALSE; RESULTCODE_EXCEPTION
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public int CheckEmailExist(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                {
                    return OperResult.RESULTCODE_PARAM_INVALID;
                }
                int count = DBProvider.UserDBProvider.GetPlayerCountByEmail(email);
                if (count == 0)
                {
                    //不存在
                    return OperResult.RESULTCODE_FALSE;
                }

                return OperResult.RESULTCODE_TRUE;
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("CheckEmailExist Exception. email: " + email, exc);

                return OperResult.RESULTCODE_EXCEPTION;
            }
        }

        /// <summary>
        /// RESULTCODE_PARAM_INVALID; RESULTCODE_TRUE; RESULTCODE_FALSE; RESULTCODE_EXCEPTION
        /// </summary>
        /// <param name="clientIP"></param>
        /// <returns></returns>
        public int CheckRegisterIP(string clientIP)
        {
            try
            {
                if (string.IsNullOrEmpty(clientIP))
                {
                    return OperResult.RESULTCODE_PARAM_INVALID;
                }
                int count = DBProvider.UserDBProvider.GetPlayerCountByRegisterIP(clientIP);
                if (count < GlobalConfig.RegisterPlayerConfig.UserCountCreateByOneIP)
                {
                    return OperResult.RESULTCODE_TRUE;
                }

                return OperResult.RESULTCODE_FALSE;
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("CheckRegisterIP Exception. clientIP: " + clientIP, exc);

                return OperResult.RESULTCODE_EXCEPTION;
            }
        }

        public GameConfig GetGameConfig()
        {
            try
            {
                return GlobalConfig.GameConfig;
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("CheckRegisterIP Exception. ", exc);

                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="out_trade_no"></param>
        /// <param name="alipay_trade_no"></param>
        /// <param name="total_fee">人民币，需换算为灵币</param>
        /// <param name="buyer_email"></param>
        /// <param name="succeed"></param>
        public int AlipayCallback(string userName, string out_trade_no, string alipay_trade_no, decimal total_fee, string buyer_email, string pay_time)
        {
            try
            {
                var existsAlipayRecord = DBProvider.AlipayRecordDBProvider.GetAlipayRechargeRecordByOrderNumber_OR_Alipay_trade_no(out_trade_no, alipay_trade_no);
                if (existsAlipayRecord != null)
                {
                    if (existsAlipayRecord.buyer_email == buyer_email && existsAlipayRecord.total_fee == total_fee && existsAlipayRecord.user_name == userName)
                    {
                        return OperResult.RESULTCODE_TRUE;
                    }
                }
                AlipayRechargeRecord record = new AlipayRechargeRecord()
                {
                    alipay_trade_no = alipay_trade_no,
                    buyer_email = buyer_email,
                    out_trade_no = out_trade_no,
                    pay_time = Convert.ToDateTime(pay_time),
                    total_fee = total_fee,
                    value_rmb = total_fee * GlobalConfig.GameConfig.Yuan_RMB,
                    user_name = userName
                };

                if (userName == "WEB" && out_trade_no.StartsWith("0000"))
                {
                    LogHelper.Instance.AddInfoLog("WEB端直接充值 ---- alipay_trade_no: " + alipay_trade_no + "; out_trade_no:" + out_trade_no + "; fee: " + total_fee);
                    return OperResult.RESULTCODE_TRUE;
                }

                LogHelper.Instance.AddInfoLog("玩家[" + userName + "] ---- alipay_trade_no: " + alipay_trade_no + "; out_trade_no:" + out_trade_no + "; fee: " + total_fee);
                if (record.value_rmb <= 0)
                {
                    return OperResult.RESULTCODE_FALSE;
                }

                int result = OrderController.Instance.AlipayCallback(record);
                return result;
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("AlipayCallback Exception. " +
                                            " orderNumber: " + out_trade_no + ";" +
                                            " money: " + total_fee.ToString() + ";" +
                                            " payAlipayAccount: " + buyer_email, exc);

                return OperResult.RESULTCODE_FALSE;
            }
        }

        public int CheckAlipayOrderBeHandled(string userName, string out_trade_no, string alipay_trade_no, decimal total_fee, string buyer_email, string pay_time)
        {
            try
            {
                AlipayRechargeRecord record = new AlipayRechargeRecord()
                {
                    alipay_trade_no = alipay_trade_no,
                    buyer_email = buyer_email,
                    out_trade_no = out_trade_no,
                    pay_time = Convert.ToDateTime(pay_time),
                    total_fee = total_fee,
                    value_rmb = total_fee * GlobalConfig.GameConfig.Yuan_RMB,
                    user_name = userName
                };

                bool isOK = OrderController.Instance.CheckAlipayOrderBeHandled(userName, out_trade_no, alipay_trade_no, total_fee, buyer_email, pay_time);
                LogHelper.Instance.AddInfoLog("玩家[" + userName + "] ---- alipay_trade_no: " + alipay_trade_no + "; out_trade_no:" + out_trade_no + "; CheckAlipayOrderBeHandled: " + isOK.ToString()); 

                return isOK ? OperResult.RESULTCODE_TRUE : OperResult.RESULTCODE_FALSE;
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("CheckAlipayOrderBeHandled Exception. " +
                                            " orderNumber: " + out_trade_no + ";" +
                                            " money: " + total_fee.ToString() + ";" +
                                            " payAlipayAccount: " + buyer_email, exc);

                return OperResult.RESULTCODE_FALSE;
            }
        }

        public int TransferOldUser(string userLoginName, string password, string alipayAccount, string alipayRealName, string email, string newServerUserLoginName, string newServerPassword)
        {
            try
            {
                var player = PlayerController.Instance.GetPlayerInfoByUserLoginName(userLoginName);
                if (player == null)
                {
                    return OperResult.RESULTCODE_USERNAME_PASSWORD_ERROR;
                }
                if (player.SimpleInfo.Password != password)
                {
                    return OperResult.RESULTCODE_USERNAME_PASSWORD_ERROR;
                }
                if (player.SimpleInfo.Alipay != alipayAccount || player.SimpleInfo.AlipayRealName != alipayRealName)
                {
                    return OperResult.RESULTCODE_TRANSFEROLDPLAYER_FAILED_ALIPAYINFOERROR;
                }
                int count = DBProvider.OldPlayerTransferDBProvider.GetRegisteredCountByUserLoginName(userLoginName);
                if (count > 0)
                {
                    return OperResult.RESULTCODE_TRANSFEROLDPLAYER_FAILED_REGISTED;
                }

                DBProvider.OldPlayerTransferDBProvider.AddOldPlayerTransferRecord(new OldPlayerTransferRegisterInfo()
                {
                    UserLoginName = userLoginName,
                    AlipayAccount = alipayAccount,
                    AlipayRealName = alipayRealName,
                    Email = email,
                    NewServerUserLoginName = newServerUserLoginName,
                    NewServerPassword = newServerPassword,
                    isTransfered = false,
                    SubmitTime = new MyDateTime(DateTime.Now)
                });

                LogHelper.Instance.AddInfoLog("玩家 [" + userLoginName + "] 登记跨区转移账户。");
                return OperResult.RESULTCODE_TRUE;
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("TransferOldUser Exception. " +
                                            " userLoginName: " + userLoginName + ";" +
                                            " alipayAccount: " + alipayAccount + ";" +
                                            " alipayRealName: " + alipayRealName, exc);

                return OperResult.RESULTCODE_FALSE;
            }
        }
    }
}
