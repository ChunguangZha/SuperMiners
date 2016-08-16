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
        public int RegisterUser(string clientIP, string userName, string nickName, string password, string email, string qq, string invitationCode)
        {
            try
            {
                if (string.IsNullOrEmpty(userName) || userName.Length < 3)
                {
                    return OperResult.RESULTCODE_REGISTER_USERNAME_LENGTH_SHORT;
                }
                return PlayerController.Instance.RegisterUser(clientIP, userName, nickName, password, email, qq, invitationCode);
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("RegisterUser Exception. clientIP:" + clientIP + ",userName: " + userName + ",password: " + password
                                    + ",email: " + email + ",qq: " + qq, exc);

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
        /// <param name="alipayAccount"></param>
        /// <param name="alipayRealName"></param>
        /// <returns></returns>
        public int CheckUserAlipayExist(string alipayAccount, string alipayRealName)
        {
            try
            {
                if (string.IsNullOrEmpty(alipayAccount) || string.IsNullOrEmpty(alipayRealName))
                {
                    return OperResult.RESULTCODE_PARAM_INVALID;
                }
                int count = DBProvider.UserDBProvider.GetPlayerCountByAlipay(alipayAccount, alipayRealName);
                if (count == 0)
                {
                    //不存在
                    return OperResult.RESULTCODE_FALSE;
                }

                return OperResult.RESULTCODE_TRUE;
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("CheckUserNameExist Exception. alipayAccount: " + alipayAccount 
                    + ", alipayRealName: " + alipayRealName, exc);

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
        public void AlipayCallback(string out_trade_no, string alipay_trade_no, decimal total_fee, string buyer_email, string pay_time)
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
                    value_rmb = total_fee * GlobalConfig.GameConfig.Yuan_RMB
                };

                LogHelper.Instance.AddInfoLog("alipay_trade_no: " + alipay_trade_no);
                OrderController.Instance.AlipayCallback(record);
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("AlipayCallback Exception. " +
                                            " orderNumber: " + out_trade_no + ";" +
                                            " money: " + total_fee.ToString() + ";" +
                                            " payAlipayAccount: " + buyer_email, exc);

            }
        }
    }
}
