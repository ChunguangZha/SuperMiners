using MetaData;
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
        /// <param name="sellStonesCount"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/SellStone",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        int SellStone(string token, string userName, int sellStonesCount);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/CancelSellStone",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        int CancelSellStone(string token, string userName, string orderNumber);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/GetAllNotFinishedSellOrders",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        SellStonesOrder[] GetAllNotFinishedSellOrders(string token);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/GetOrderLockedBySelf",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        LockSellStonesOrder GetOrderLockedBySelf(string token);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/AutoMatchLockSellStone",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        LockSellStonesOrder AutoMatchLockSellStone(string token, string userName, int buyStonesCount);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/LockSellStone",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        LockSellStonesOrder LockSellStone(string token, string userName, string orderNumber);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/CheckUserHasNotPayOrder",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        bool CheckUserHasNotPayOrder(string token);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/ReleaseLockOrder",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        bool ReleaseLockOrder(string token, string orderNumber);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/PayStoneOrderByRMB",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        int PayStoneOrderByRMB(string token, string orderNumber, decimal rmb);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/StoneOrderSetException",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        int SetStoneOrderPayException(string token, string orderNumber);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/SearchUserSellStoneOrders",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        SellStonesOrder[] SearchUserSellStoneOrders(string token, string sellerUserName, string orderNumber, int orderState, MyDateTime myBeginCreateTime, MyDateTime myEndCreateTime, int pageItemCount, int pageIndex);
        
        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/SearchUserBuyStoneOrders",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        BuyStonesOrder[] SearchUserBuyStoneOrders(string token, string sellerUserName, string orderNumber, string buyUserName, int orderState, MyDateTime myBeginCreateTime, MyDateTime myEndCreateTime, MyDateTime myBeginBuyTime, MyDateTime myEndBuyTime, int pageItemCount, int pageIndex);

    }
}
