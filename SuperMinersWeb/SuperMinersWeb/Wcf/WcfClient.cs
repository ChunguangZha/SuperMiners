using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace SuperMinersWeb.Wcf
{
    public static class WcfClient
    {
        #region Events

        private static bool _disconnected = true;
        public static event EventHandler Error;
        //public static event EventHandler Created;

        #endregion

        private static SuperMinersClient _client = null;

        public static SuperMinersClient Instance
        {
            get
            {
                return _client;
            }
        }

        public static bool IsReady
        {
            get
            {
                if (_disconnected)
                {
                    return Init();
                }
                return true;
            }
        }

        private static void InnerChannel_Faulted(object sender, EventArgs e)
        {
            _disconnected = true;
        }

        public static bool Init()
        {
            try
            {
                if (null != _client)
                {
                    try
                    {
                        _client.InnerChannel.Faulted -= new EventHandler(InnerChannel_Faulted);
                        _client.Close();
                        _client = null;
                    }
                    catch { }
                }

                NetTcpBinding binding = new NetTcpBinding();
                binding.Security.Mode = SecurityMode.Transport;
                binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
                EndpointAddress address = new EndpointAddress("net.tcp://localhost:4510/ServiceToWeb");

                _client = new SuperMinersClient(binding, address);

                _client.InnerChannel.Faulted += new EventHandler(InnerChannel_Faulted);

                _disconnected = false;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}