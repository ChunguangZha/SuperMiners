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
    class ServiceToWeb : IServiceToWeb
    {
        public bool Active()
        {
            return true;
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
        public int RegisterUser(string clientIP, string userName, string nickName, string password,
            string alipayAccount, string alipayRealName, string IDCardNo, string email, string qq, string invitationCode)
        {
            try
            {
                if (string.IsNullOrEmpty(userName) || userName.Length < 3)
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
                return PlayerController.Instance.RegisterUser(clientIP, userName, nickName, password, alipayAccount, alipayRealName, IDCardNo, email, qq, invitationCode);
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("RegisterUser Exception. clientIP:" + clientIP + ",userName: " + userName + ",password: " + password
                                    + "alipayAccount:" + alipayAccount + "alipayRealName:" + alipayRealName + ",IDCardNo:" + IDCardNo + ",email: " + email + ",qq: " + qq, exc);

                return OperResult.RESULTCODE_EXCEPTION;
            }
        }

        /// <summary>
        /// RESULTCODE_REGISTER_USERNAME_LENGTH_SHORT; RESULTCODE_FALSE; RESULTCODE_REGISTER_USERNAME_EXIST; RESULTCODE_TRUE; RESULTCODE_EXCEPTION
        /// </summary>
        /// <param name="clientIP"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <param name="qq"></param>
        /// <param name="agentUserName"></param>
        /// <returns></returns>
        public int RegisterUserByAgent(string clientIP, string userName, string nickName, string password,
            string alipayAccount, string alipayRealName, string IDCardNo, string email, string qq, string agentUserName)
        {
            try
            {
                if (string.IsNullOrEmpty(userName) || userName.Length < 3)
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

                var agent = PlayerController.Instance.GetPlayerInfo(agentUserName);
                if (agent == null)
                {
                    return OperResult.RESULTCODE_FALSE;
                }

                return PlayerController.Instance.RegisterUser(clientIP, userName, nickName, password, alipayAccount, alipayRealName, IDCardNo, email, qq, agent.SimpleInfo.InvitationCode);
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("RegisterUser Exception. clientIP:" + clientIP + ",userName: " + userName + ",password: " + password
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
        public int CheckNickNameExist(string nickName)
        {
            try
            {
                if (string.IsNullOrEmpty(nickName))
                {
                    return OperResult.RESULTCODE_PARAM_INVALID;
                }
                int count = DBProvider.UserDBProvider.GetPlayerCountByNickName(nickName);
                if (count == 0)
                {
                    //不存在
                    return OperResult.RESULTCODE_FALSE;
                }

                return OperResult.RESULTCODE_TRUE;
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("CheckNickNameExist Exception. nickName: " + nickName, exc);

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

                LogHelper.Instance.AddInfoLog("玩家[" + userName + "] ---- alipay_trade_no: " + alipay_trade_no + "; out_trade_no:" + out_trade_no + "; fee: " + total_fee);
                if (record.value_rmb <= 0)
                {
                    return OperResult.RESULTCODE_FALSE;
                }

                bool isOK = OrderController.Instance.AlipayCallback(record);
                return isOK ? OperResult.RESULTCODE_TRUE : OperResult.RESULTCODE_FALSE;
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
    }
}
