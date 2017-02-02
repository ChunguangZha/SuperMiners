using MetaData.SystemConfig;
using SuperMinersServerApplication.Controller;
using SuperMinersServerApplication.Controller.Game;
using SuperMinersServerApplication.Controller.Trade;
using SuperMinersServerApplication.Encoder;
using SuperMinersServerApplication.Utility;
using SuperMinersServerApplication.WebService.Contracts;
using SuperMinersServerApplication.WebService.Services;
using SuperMinersServerApplication.WebServiceToAdmin.Contracts;
using SuperMinersServerApplication.WebServiceToAdmin.Services;
using SuperMinersServerApplication.WebServiceToWeb.Contracts;
using SuperMinersServerApplication.WebServiceToWeb.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersServerApplication
{
    public partial class SuperMinersService : ServiceBase
    {
        public event EventHandler ServiceStateChanged;

        private bool _isStarted = false;

        public bool IsStarted
        {
            get { return this._isStarted; }
            private set
            {
                this._isStarted = value;
                ServiceStateChanged(this, null);
            }
        }

        private ServiceHost _serviceToClientHost;
        private ServiceHost _serviceToAdminHost;
        private ServiceHost _serviceToWebHost;

        public SuperMinersService()
        {
            InitializeComponent();
        }

        internal void StartByApplication()
        {
            if (!InitService())
            {
                StopService();
            }
        }

        protected override void OnStart(string[] args)
        {
            if (!InitService())
            {
                this.Stop();
            }
        }

        private bool InitService()
        {
            try
            {
                if (!CreateFolder())
                {
                    return false;
                }
                LogHelper.Instance.Init();

                SchedulerTaskController.Instance.Init();
                GameSystemConfigController.Instance.Init();
                PlayerController.Instance.Init();
                OrderController.Instance.Init();
                NoticeController.Instance.Init();
                RouletteAwardController.Instance.Init();
                RaidersofLostArkController.Instance.Init();
                GravelController.Instance.Init();
                GambleStoneController.Instance.Init();

                if (!InitServiceToClient())
                {
                    return false;
                }
                if (!InitServiceToWeb())
                {
                    return false;
                }
                if (!InitServiceToAdmin())
                {
                    return false;
                }

                IsStarted = true;

                return true;
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("启动服务失败。", exc);
                IsStarted = false;
                return false;
            }
        }

        private bool CreateFolder()
        {
            try
            {
                if (!Directory.Exists(GlobalData.LogFolder))
                {
                    Directory.CreateDirectory(GlobalData.LogFolder);
                }
                if (!Directory.Exists(GlobalData.UserActionFolder))
                {
                    Directory.CreateDirectory(GlobalData.UserActionFolder);
                }
                if (!Directory.Exists(GlobalData.UserWithdrawRMBImagesFolder))
                {
                    Directory.CreateDirectory(GlobalData.UserWithdrawRMBImagesFolder);
                }
                if (!Directory.Exists(GlobalData.ConfigFolder))
                {
                    Directory.CreateDirectory(GlobalData.ConfigFolder);
                }
                if (!Directory.Exists(GlobalData.NoticeFolder))
                {
                    Directory.CreateDirectory(GlobalData.NoticeFolder);
                }

                return true;
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("Init Service Create Folder failed.", exc);
                return false;
            }
        }

        private bool InitServiceToWeb()
        {
            try
            {
                List<BindingElement> bindingElements = new List<BindingElement>();

                NetTcpBinding tcpBind = new NetTcpBinding();
                tcpBind.Security.Mode = SecurityMode.Transport;
                tcpBind.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;

                Uri baseAddress = new Uri("net.tcp://localhost:" + GlobalData.ServiceToWebPort + "/ServiceToWeb");
                this._serviceToWebHost = new ServiceHost(typeof(ServiceToWeb), baseAddress);

                this._serviceToWebHost.Description.Behaviors.Add(new ServiceThrottlingBehavior()
                {
                    MaxConcurrentCalls = 1024,
                    MaxConcurrentInstances = 512,
                    MaxConcurrentSessions = 512,
                });

                this._serviceToWebHost.Description.Behaviors.Add(new ServiceMetadataBehavior());
                var servieEP = this._serviceToWebHost.AddServiceEndpoint(typeof(IServiceToWeb), tcpBind, baseAddress);

                foreach (var item in servieEP.Contract.Operations)
                {
                    DataContractSerializerOperationBehavior dc = item.Behaviors.Find<DataContractSerializerOperationBehavior>();
                    if (dc == null)
                    {
                        dc = new DataContractSerializerOperationBehavior(item);
                        item.Behaviors.Add(dc);
                    }

                    // Change the settings of the behavior.
                    dc.MaxItemsInObjectGraph = Int32.MaxValue;
                }

                this._serviceToWebHost.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexTcpBinding(), "mex");

                this._serviceToWebHost.Open();

                return true;
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("Init Service To Web Failed。", exc);
                return false;
            }
        }

        private bool InitServiceToClient()
        {
            try
            {
                List<BindingElement> bindingElements = new List<BindingElement>();

                WebMessageEncodingBindingElement webCncoding = new WebMessageEncodingBindingElement();
                webCncoding.ReaderQuotas.MaxArrayLength = 1024 * 1024 * 5;
                webCncoding.ReaderQuotas.MaxBytesPerRead = 1024 * 1024 * 3;
                webCncoding.ReaderQuotas.MaxStringContentLength = 1024 * 1024 * 5;
                CryptMessageEncoderElement encoding = new CryptMessageEncoderElement(webCncoding);

                HttpTransportBindingElement transport = new HttpTransportBindingElement();
                transport.ManualAddressing = true;
                transport.MaxReceivedMessageSize = Int32.MaxValue;
                transport.MaxBufferSize = Int32.MaxValue;
                transport.MaxBufferPoolSize = 1024 * 1024 * 5;

                bindingElements.Add(encoding);
                bindingElements.Add(transport);

                CustomBinding bind = new CustomBinding(bindingElements);

                var baseAddress = new Uri("http://localhost:" + GlobalData.ServiceToClientPort);
                this._serviceToClientHost = new ServiceHost(typeof(ServiceToClient), baseAddress);
                this._serviceToClientHost.Description.Behaviors.Add(new ServiceThrottlingBehavior()
                {
                    MaxConcurrentCalls = 1024,
                    MaxConcurrentInstances = 512,
                    MaxConcurrentSessions = 512,
                });

                var servieEP = this._serviceToClientHost.AddServiceEndpoint(typeof(IServiceToClient), bind, baseAddress);

                servieEP.Behaviors.Add(new WebHttpBehavior());

                foreach (var item in servieEP.Contract.Operations)
                {
                    DataContractSerializerOperationBehavior dc = item.Behaviors.Find<DataContractSerializerOperationBehavior>();
                    if (dc == null)
                    {
                        dc = new DataContractSerializerOperationBehavior(item);
                        item.Behaviors.Add(dc);
                    }

                    // Change the settings of the behavior.
                    dc.MaxItemsInObjectGraph = Int32.MaxValue;
                }

                this._serviceToClientHost.Open();
                return true;
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("Init Service To Client failed.", exc);
                return false;
            }
        }

        private bool InitServiceToAdmin()
        {
            try
            {
                List<BindingElement> bindingElements = new List<BindingElement>();

                WebMessageEncodingBindingElement webCncoding = new WebMessageEncodingBindingElement();
                webCncoding.ReaderQuotas.MaxArrayLength = 1024 * 1024 * 5;
                webCncoding.ReaderQuotas.MaxBytesPerRead = 1024 * 1024 * 3;
                webCncoding.ReaderQuotas.MaxStringContentLength = 1024 * 1024 * 5;
                CryptMessageEncoderElement encoding = new CryptMessageEncoderElement(webCncoding);

                HttpTransportBindingElement transport = new HttpTransportBindingElement();
                transport.ManualAddressing = true;
                transport.MaxReceivedMessageSize = Int32.MaxValue;
                transport.MaxBufferSize = Int32.MaxValue;
                transport.MaxBufferPoolSize = 1024 * 1024 * 5;

                bindingElements.Add(encoding);
                bindingElements.Add(transport);

                CustomBinding bind = new CustomBinding(bindingElements);

                var baseAddress = new Uri("http://localhost:" + GlobalData.ServiceToAdministrator);
                this._serviceToAdminHost = new ServiceHost(typeof(ServiceToAdmin), baseAddress);
                this._serviceToAdminHost.Description.Behaviors.Add(new ServiceThrottlingBehavior()
                {
                    MaxConcurrentCalls = 1024,
                    MaxConcurrentInstances = 512,
                    MaxConcurrentSessions = 512,
                });

                var servieEP = this._serviceToAdminHost.AddServiceEndpoint(typeof(IServiceToAdmin), bind, baseAddress);

                servieEP.Behaviors.Add(new WebHttpBehavior());

                foreach (var item in servieEP.Contract.Operations)
                {
                    DataContractSerializerOperationBehavior dc = item.Behaviors.Find<DataContractSerializerOperationBehavior>();
                    if (dc == null)
                    {
                        dc = new DataContractSerializerOperationBehavior(item);
                        item.Behaviors.Add(dc);
                    }

                    // Change the settings of the behavior.
                    dc.MaxItemsInObjectGraph = Int32.MaxValue;
                }

                this._serviceToAdminHost.Open();
                return true;
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("Init Service To Admin failed.", exc);
                return false;
            }
        }

        private bool StopService()
        {
            try
            {
                if (_serviceToClientHost != null)
                {
                    _serviceToClientHost.Close();
                }
                if (_serviceToWebHost != null)
                {
                    _serviceToWebHost.Close();
                }
                if (_serviceToAdminHost != null)
                {
                    _serviceToAdminHost.Close();
                }
                IsStarted = false;
                LogHelper.Instance.Stop();
                RaidersofLostArkController.Instance.StopService();

                return true;
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("停止服务失败。", exc);
                return false;
            }
        }

        protected override void OnStop()
        {
            StopService();
        }
    }
}
