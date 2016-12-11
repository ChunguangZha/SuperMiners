//using MetaData.Trade;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.ServiceModel;
//using System.Text;
//using System.Threading.Tasks;

//namespace SuperMinersWeiXin.Wcf.Services
//{
//    [ServiceContract(Name = "SuperMinersServerApplication.WebServiceToWeb.Contracts.IServiceToWeb")]
//    public partial interface ISuperMinersAsyncClient
//    {
//        #region GetAllNotFinishedSellOrders

//        [OperationContractAttribute(AsyncPattern = true, Action = "http://tempuri.org/IServiceToWeb/GetAllNotFinishedSellOrders", ReplyAction = "http://tempuri.org/IServiceToWeb/GetAllNotFinishedSellOrdersResponse")]
//        IAsyncResult BeginGetAllNotFinishedSellOrders(AsyncCallback callback, object asyncState);

//        SellStonesOrder[] EndGetAllNotFinishedSellOrders(IAsyncResult result);

//        #endregion
//    }

//    public partial class SuperMinersClientChannel : System.ServiceModel.Channels.ChannelBase
//    {

//        protected override void OnAbort()
//        {
//            throw new NotImplementedException();
//        }

//        protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
//        {
//            throw new NotImplementedException();
//        }

//        protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
//        {
//            throw new NotImplementedException();
//        }

//        protected override void OnClose(TimeSpan timeout)
//        {
//            throw new NotImplementedException();
//        }

//        protected override void OnEndClose(IAsyncResult result)
//        {
//            throw new NotImplementedException();
//        }

//        protected override void OnEndOpen(IAsyncResult result)
//        {
//            throw new NotImplementedException();
//        }

//        protected override void OnOpen(TimeSpan timeout)
//        {
//            throw new NotImplementedException();
//        }
//    }

//    public partial class SuperMinersAsyncClient : ClientBase<ISuperMinersAsyncClient>, ISuperMinersAsyncClient
//    {
//        public SuperMinersAsyncClient(string endpointConfigurationName) :
//            base(endpointConfigurationName)
//        {
//        }

//        public SuperMinersAsyncClient(string endpointConfigurationName, string remoteAddress) :
//            base(endpointConfigurationName, remoteAddress)
//        {
//        }

//        public SuperMinersAsyncClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
//            base(endpointConfigurationName, remoteAddress)
//        {
//        }

//        public SuperMinersAsyncClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
//            base(binding, remoteAddress)
//        {
//        }

//        protected override ISuperMinersAsyncClient CreateChannel()
//        {
//            return base.CreateChannel();
//        }

//        public IAsyncResult BeginGetAllNotFinishedSellOrders(AsyncCallback callback, object asyncState)
//        {
//            throw new NotImplementedException();
//        }

//        public SellStonesOrder[] EndGetAllNotFinishedSellOrders(IAsyncResult result)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
