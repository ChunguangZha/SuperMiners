using MetaData.SystemConfig;
using SuperMinersServerApplication.WebServiceToWeb.Contracts;
using SuperMinersWeb.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Web;

namespace SuperMinersWeb.Wcf
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

        /// <summary>
        /// 0：成功；1：用户名已经存在；2：同一IP注册用户数超限；3：注册失败
        /// </summary>
        /// <param name="clientIP"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="alipayAccount"></param>
        /// <param name="alipayRealName"></param>
        /// <param name="invitationCode"></param>
        /// <returns></returns>
        public int RegisterUser(string clientIP, string userName, string nickName, string password, string alipayAccount, string alipayRealName, string invitationCode)
        {
            try
            {
                return base.Channel.RegisterUser(clientIP, userName, nickName, password, alipayAccount, alipayRealName, invitationCode);
            }
            catch (Exception)
            {
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
                return base.Channel.CheckUserNameExist(userName);
            }
            catch (Exception)
            {
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
                return base.Channel.CheckUserAlipayExist(alipayAccount, alipayRealName);
            }
            catch (Exception)
            {
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
                return base.Channel.CheckEmailExist(email);
            }
            catch (Exception)
            {
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
                return base.Channel.CheckRegisterIP(clientIP);
            }
            catch (Exception)
            {
                return -1;
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

        public void PayCompleted(string orderNumber, float money, string payAlipayAccount, bool succeed)
        {
            try
            {
                base.Channel.PayCompleted(orderNumber, money, payAlipayAccount, succeed);
            }
            catch (Exception)
            {

            }
        }

    }
}