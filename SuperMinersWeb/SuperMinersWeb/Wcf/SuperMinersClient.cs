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
        //#region Single

        //private static SuperMinersClient client = new SuperMinersClient();

        //public static SuperMinersClient Instance
        //{
        //    get
        //    {
        //        return client;
        //    }
        //}

        //private SuperMinersClient()
        //{

        //}

        //#endregion

        //ChannelFactory<IServiceToWeb> factory = null;
        //IServiceToWeb channel;

        //public void Init()
        //{
        //    try
        //    {
        //        NetTcpBinding binding = new NetTcpBinding();
        //        binding.Security.Mode = SecurityMode.Transport;
        //        binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
        //        EndpointAddress address = new EndpointAddress("net.tcp://localhost:4510/ServiceToWeb");
        //        factory = new ChannelFactory<IServiceToWeb>(binding, address);
        //        factory.Faulted += Factory_Faulted;
        //        channel = factory.CreateChannel();

        //    }
        //    catch (Exception exc)
        //    {
        //        if (factory != null)
        //        {
        //            factory.Abort();
        //        }
        //        LogHelper.Instance.AddErrorLog("Init Client Failed。", exc);
        //    }
        //}

        //void Factory_Faulted(object sender, EventArgs e)
        //{
        //    factory.Close();
        //    Init();
        //}

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
    }
}