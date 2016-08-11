using DataBaseProvider;
using MetaData.SystemConfig;
using MetaData.Trade;
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

namespace SuperMinersServerApplication.WebServiceToWeb.Services
{
    class ServiceToWeb : IServiceToWeb
    {
        /// <summary>
        /// 0：成功；1：用户名已经存在；2：同一IP注册用户数超限；3：注册失败; 4: 用户名长度不够
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
                    return 4;
                }
                return PlayerController.Instance.RegisterUser(clientIP, userName, nickName, password, email, qq, invitationCode);
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("RegisterUser Exception. clientIP:" + clientIP + ",userName: " + userName + ",password: " + password
                                    + ",email: " + email + ",qq: " + qq, exc);

                return 3;
            }
        }

        /// <summary>
        /// -2表示参数无效，-1表示异常，0,表示不存在，1表示存在
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public int CheckUserNameExist(string userName)
        {
            try
            {
                if (string.IsNullOrEmpty(userName))
                {
                    return -2;
                }
                int count = DBProvider.UserDBProvider.GetPlayerCountByUserName(userName);
                return count;
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("CheckUserNameExist Exception. userName: " + userName, exc);

                return -1;
            }
        }

        /// <summary>
        /// -2表示参数无效，-1表示异常，0,表示不存在，1表示存在
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
                    return -2;
                }
                int count = DBProvider.UserDBProvider.GetPlayerCountByAlipay(alipayAccount, alipayRealName);
                return count;
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("CheckUserNameExist Exception. alipayAccount: " + alipayAccount 
                    + ", alipayRealName: " + alipayRealName, exc);

                return -1;
            }
        }

        /// <summary>
        /// -2表示参数无效，-1表示异常，0,表示不存在，1表示存在
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public int CheckEmailExist(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                {
                    return -2;
                }
                int count = DBProvider.UserDBProvider.GetPlayerCountByEmail(email);
                return count;
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("CheckEmailExist Exception. email: " + email, exc);

                return -1;
            }
        }

        /// <summary>
        /// -2表示参数无效，-1表示异常，0,表示可以注册，1表示已经超出限制，不可以注册
        /// </summary>
        /// <param name="clientIP"></param>
        /// <returns></returns>
        public int CheckRegisterIP(string clientIP)
        {
            try
            {
                if (string.IsNullOrEmpty(clientIP))
                {
                    return -2;
                }
                int count = DBProvider.UserDBProvider.GetPlayerCountByRegisterIP(clientIP);
                if (count < GlobalConfig.RegisterPlayerConfig.UserCountCreateByOneIP)
                {
                    return 0;
                }

                return 1;
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("CheckRegisterIP Exception. clientIP: " + clientIP, exc);

                return -1;
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
        public void AlipayCallback(string out_trade_no, string alipay_trade_no, float total_fee, string buyer_email, string pay_time)
        {
            try
            {
                AlipayRechargeRecord record = new AlipayRechargeRecord()
                {
                    alipay_trade_no = alipay_trade_no,
                    buyer_email = buyer_email,
                    out_trade_no = out_trade_no,
                    pay_time = Convert.ToDateTime(pay_time),
                    total_fee = total_fee
                };
                OrderController.Instance.AlipayCallback(record);
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("PayCompleted Exception. " +
                                            " orderNumber: " + out_trade_no + ";" +
                                            " money: " + total_fee.ToString() + ";" +
                                            " payAlipayAccount: " + buyer_email, exc);

            }
        }
    }
}
