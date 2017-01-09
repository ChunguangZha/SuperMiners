using MetaData;
using MetaData.Game.StoneStack;
using MetaData.Trade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.WebService.Contracts
{
    public partial interface IServiceToClient
    {
        /// <summary>
        /// 0表示成功；-1表示查询不到该用户; -2表示该用户不在线；-3表示异常；1表示本次出售的矿石数超出可出售的矿石数；2表示本次出售的矿石不足支付最低手续费；
        /// </summary>
        /// <param name="token"></param>
        /// <param name="userName"></param>
        /// <param name="sellStoneHandsCount"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/DelegateSellStone",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        int DelegateSellStone(string token, int sellStoneHandsCount, decimal price);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/CancelDelegateSellStone",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        int CancelDelegateSellStone(string token, StoneDelegateSellOrderInfo sellOrder);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/GetNotFinishedDelegateSellStoneOrders",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        StoneDelegateSellOrderInfo[] GetNotFinishedDelegateSellStoneOrders(string token);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/GetFinishedDelegateSellStoneOrders",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        StoneDelegateSellOrderInfo[] GetFinishedDelegateSellStoneOrders(string token, MyDateTime myBeginCreateTime, MyDateTime myEndCreateTime, int pageItemCount, int pageIndex);


        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/DelegateBuyStone",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        OperResultObject DelegateBuyStone(string token, int buyStoneHandsCount, decimal price, PayType paytype);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/CancelDelegateBuyStone",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        int CancelDelegateBuyStone(string token, StoneDelegateBuyOrderInfo buyOrder);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/GetNotFinishedDelegateBuyStoneOrders",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        StoneDelegateBuyOrderInfo[] GetNotFinishedDelegateBuyStoneOrders(string token);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/GetFinishedDelegateBuyStoneOrders",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        StoneDelegateBuyOrderInfo[] GetFinishedDelegateBuyStoneOrders(string token, MyDateTime myBeginCreateTime, MyDateTime myEndCreateTime, int pageItemCount, int pageIndex);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/GetTodayStoneStackInfo",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        TodayStoneStackTradeRecordInfo GetTodayStoneStackInfo(string token);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/GetTodayRealTimeTradeRecords",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        StoneStackDailyRecordInfo[] GetTodayRealTimeTradeRecords(string token);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/GetAllStoneStackDailyRecordInfo",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        StoneStackDailyRecordInfo[] GetAllStoneStackDailyRecordInfo(string token);
    }
}
