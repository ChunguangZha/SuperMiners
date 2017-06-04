using MetaData;
using MetaData.SystemConfig;
using MetaData.Trade;
using MetaData.User;
using SuperMinersServerApplication.WebServiceToWeb.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace XunLinMineRemoteControlWeb.Wcf
{
    public class SuperMinersClient : ClientBase<IServiceToWeb>, IServiceToWeb
    {
        public SuperMinersClient()
        {

        }

        public SuperMinersClient(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
        }

        public SuperMinersClient(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public SuperMinersClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public SuperMinersClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
        }

        public bool Active()
        {
            try
            {
                return base.Channel.Active();
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// RESULTCODE_REGISTER_USERNAME_LENGTH_SHORT; RESULTCODE_FAILED; RESULTCODE_REGISTER_USERNAME_EXIST; RESULTCODE_TRUE; RESULTCODE_EXCEPTION
        /// </summary>
        /// <param name="clientIP"></param>
        /// <param name="userLoginName"></param>
        /// <param name="password"></param>
        /// <param name="alipayAccount"></param>
        /// <param name="alipayRealName"></param>
        /// <param name="invitationCode"></param>
        /// <returns></returns>
        public int RegisterUser(string clientIP, string userLoginName, string userName, string password, string alipayAccount, string alipayRealName, string IDCardNo, string email, string qq, string invitationCode)
        {
            try
            {
                return base.Channel.RegisterUser(clientIP, userLoginName, userName, password, alipayAccount, alipayRealName, IDCardNo, email, qq, invitationCode);
            }
            catch (Exception)
            {
                return OperResult.RESULTCODE_EXCEPTION;
            }
        }

        public int RegisterUserByAgent(string clientIP, string userLoginName, string userName, string password, string alipayAccount, string alipayRealName, string IDCardNo, string email, string qq, string invitationCode)
        {
            try
            {
                return base.Channel.RegisterUserByAgent(clientIP, userLoginName, userName, password, alipayAccount, alipayRealName, IDCardNo, email, qq, invitationCode);
            }
            catch (Exception)
            {
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
                return base.Channel.CheckUserNameExist(userName);
            }
            catch (Exception)
            {
                return OperResult.RESULTCODE_EXCEPTION;
            }
        }

        /// <summary>
        /// RESULTCODE_PARAM_INVALID; RESULTCODE_TRUE; RESULTCODE_FALSE; RESULTCODE_EXCEPTION
        /// </summary>
        /// <param name="userLoginName"></param>
        /// <returns></returns>
        public int CheckUserLoginNameExist(string userLoginName)
        {
            try
            {
                return base.Channel.CheckUserLoginNameExist(userLoginName);
            }
            catch (Exception)
            {
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
                return base.Channel.CheckUserAlipayAccountExist(alipayAccount);
            }
            catch (Exception)
            {
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
                return base.Channel.CheckUserAlipayRealNameExist(alipayRealName);
            }
            catch (Exception)
            {
                return OperResult.RESULTCODE_EXCEPTION;
            }
        }

        public int CheckUserIDCardNoExist(string IDCardNo)
        {
            try
            {
                return base.Channel.CheckUserIDCardNoExist(IDCardNo);
            }
            catch (Exception)
            {
                return OperResult.RESULTCODE_EXCEPTION;
            }
        }

        /// <summary>
        /// -RESULTCODE_PARAM_INVALID; RESULTCODE_TRUE; RESULTCODE_FALSE; RESULTCODE_EXCEPTION
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public int CheckEmailExist(string email)
        {
            try
            {
                return base.Channel.CheckEmailExist(email);
            }
            catch (Exception)
            {
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
                return base.Channel.CheckRegisterIP(clientIP);
            }
            catch (Exception)
            {
                return OperResult.RESULTCODE_EXCEPTION;
            }
        }

        public GameConfig GetGameConfig()
        {
            try
            {
                return base.Channel.GetGameConfig();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int AlipayCallback(string userName, string out_trade_no, string alipay_trade_no, decimal total_fee, string buyer_email, string pay_time)
        {
            try
            {
                return base.Channel.AlipayCallback(userName, out_trade_no, alipay_trade_no, total_fee, buyer_email, pay_time);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
                return OperResult.RESULTCODE_EXCEPTION;
            }
        }

        public int CheckAlipayOrderBeHandled(string userName, string out_trade_no, string alipay_trade_no, decimal total_fee, string buyer_email, string pay_time)
        {
            try
            {
                return base.Channel.CheckAlipayOrderBeHandled(userName, out_trade_no, alipay_trade_no, total_fee, buyer_email, pay_time);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
                return OperResult.RESULTCODE_EXCEPTION;
            }
        }



        public OperResultObject Login(string clientIP, string userLoginName, string password)
        {
            try
            {
                return base.Channel.Login(clientIP, userLoginName, password);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
                return new OperResultObject() { OperResultCode = OperResult.RESULTCODE_EXCEPTION };
            }
        }

        public WebPlayerInfo GetPlayerInfo(string token, string userLoginName, string clientIP)
        {
            try
            {
                return base.Channel.GetPlayerInfo(token, userLoginName, clientIP);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
                return null;
            }
        }

        public UserRemoteServerItem[] GetUserRemoteServerItems(string token, string userName)
        {
            try
            {
                return base.Channel.GetUserRemoteServerItems(token, userName);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
                return null;
            }
        }

        public bool CheckOnceRemoveServiceCanBuyable(string token, string userName)
        {
            try
            {
                return base.Channel.CheckOnceRemoveServiceCanBuyable(token, userName);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
                return false;
            }
        }

        public string CreateBuyRemoteServerAlipayLink(string token, string userName, RemoteServerType serverType)
        {
            try
            {
                return base.Channel.CreateBuyRemoteServerAlipayLink(token, userName, serverType);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
                return null;
            }
        }



        public int TransferOldUser(string userLoginName, string password, string alipayAccount, string alipayRealName, string email, string newServerUserLoginName, string newServerPassword)
        {
            throw new NotImplementedException();
        }
    }
}