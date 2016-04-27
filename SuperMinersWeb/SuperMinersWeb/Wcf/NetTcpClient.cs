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
    public class NetTcpClient
    {
        #region Single

        private static NetTcpClient client = new NetTcpClient();

        public static NetTcpClient Instance
        {
            get
            {
                return client;
            }
        }

        private NetTcpClient()
        {
            
        }

        #endregion

        ChannelFactory<IServiceToWeb> factory = null;
        IServiceToWeb channel;

        public void Init()
        {
            try
            {
                NetTcpBinding binding = new NetTcpBinding();
                binding.Security.Mode = SecurityMode.Transport;
                binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
                EndpointAddress address = new EndpointAddress("net.tcp://localhost:4510/ServiceToWeb");
                factory = new ChannelFactory<IServiceToWeb>(binding, address);
                factory.Faulted += Factory_Faulted;
                channel = factory.CreateChannel();
                
            }
            catch (Exception exc)
            {
                if (factory != null)
                {
                    factory.Abort();
                }
                LogHelper.Instance.AddErrorLog("Init Client Failed。", exc);
            }
        }

        void Factory_Faulted(object sender, EventArgs e)
        {
            factory.Close();
            Init();
        }

        public bool RegisterUser(string clientIP, string userName, string password, string alipayAccount, string alipayRealName, string invitationCode)
        {
            if (channel == null)
            {
                return false;
            }

            try
            {
                return channel.RegisterUser(clientIP, userName, password, alipayAccount, alipayRealName, invitationCode);
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        /// <summary>
        /// -2表示参数无效，-1表示异常，0,表示不存在，1表示存在
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public int CheckUserNameExist(string userName)
        {
            if (channel == null)
            {
                return -1;
            }

            try
            {
                return channel.CheckUserNameExist(userName);
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
            if (channel == null)
            {
                return -1;
            }

            try
            {
                return channel.CheckUserAlipayExist(alipayAccount, alipayRealName);
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}