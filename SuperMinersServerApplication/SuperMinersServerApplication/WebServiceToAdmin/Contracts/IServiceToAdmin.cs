using MetaData;
using MetaData.SystemConfig;
using MetaData.Trade;
using MetaData.User;
using SuperMinersServerApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.WebServiceToAdmin.Contracts
{
    [ServiceContract]
    public partial interface IServiceToAdmin
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/LoginAdmin",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string LoginAdmin(string adminName, string password, string mac, string key);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/GetAdminInfo",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        AdminInfo GetAdminInfo(string token);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/LogoutAdmin",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        bool LogoutAdmin(string token);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/GetGameConfig",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        SystemConfigin1 GetGameConfig(string token);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="isOnline">1表示在线，0表示不在线，-1表示全部</param>
        /// <param name="isLocked">1表示被锁定，0表示没被锁定，-1表示全部</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/GetPlayers",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PlayerInfoLoginWrap[] GetPlayers(string token);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/ChangePlayer",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        bool ChangePlayer(string token, string actionPassword, PlayerInfoLoginWrap player);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/DeletePlayers",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        DeleteResultInfo DeletePlayers(string token, string actionPassword, string[] playerUserNames);

        //[OperationContract]
        //[WebInvoke(UriTemplate = "/WebServiceAdmin/LogOutPlayers",
        //    Method = "POST",
        //    ResponseFormat = WebMessageFormat.Json,
        //    RequestFormat = WebMessageFormat.Json,
        //    BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //bool LogOutPlayers(string token, string actionPassword, string[] playerUserNames);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/LockPlayer",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        bool LockPlayer(string token, string actionPassword, string playerUserName);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/UnlockPlayer",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        bool UnlockPlayer(string token, string actionPassword, string playerUserName);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/UpdatePlayerFortuneInfo",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        bool UpdatePlayerFortuneInfo(string token, string actionPassword, PlayerFortuneInfo fortuneInfo);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/ChangePlayerPassword",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        bool ChangePlayerPassword(string token, string actionPassword, string playerUserName, string newPassword);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/GetNotices",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        NoticeInfo[] GetNotices(string token);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/CreateNotice",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        bool CreateNotice(string token, NoticeInfo notice);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/GetSellStonesOrderList",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        SellStonesOrder[] GetSellStonesOrderList(string token, string sellerUserName, int sellOrderState, MyDateTime startDate, MyDateTime endDate);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/GetLockedStonesOrderList",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        LockSellStonesOrder[] GetLockedStonesOrderList(string token, string buyerUserName);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/GetBuyStonesOrderList",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        BuyStonesOrder[] GetBuyStonesOrderList(string token, string buyerUserName, MyDateTime startDate, MyDateTime endDate);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/GetBuyMinesFinishedRecordList",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        MinesBuyRecord[] GetBuyMinesFinishedRecordList(string token, string buyerUserName, MyDateTime startDate, MyDateTime endDate);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/GetBuyMinesNotFinishedRecordList",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        MinesBuyRecord[] GetBuyMinesNotFinishedRecordList(string token, string buyerUserName);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/HandleExceptionStoneOrderSucceed",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        int HandleExceptionStoneOrderSucceed(string token, AlipayRechargeRecord alipayRecord);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/HandleExceptionStoneOrderFailed",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        int HandleExceptionStoneOrderFailed(string token, string orderNumber);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/PayWithdrawRMBRecord",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        int PayWithdrawRMBRecord(string token, WithdrawRMBRecord record);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/GetWithdrawRMBRecordList",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        WithdrawRMBRecord[] GetWithdrawRMBRecordList(string token, bool isPayed, string playerUserName, MyDateTime beginCreateTime, MyDateTime endCreateTime, string adminUserName, MyDateTime beginPayTime, MyDateTime endPayTime, int pageItemCount, int pageIndex);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/GetFinishedGoldCoinRechargeRecordList",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        GoldCoinRechargeRecord[] GetFinishedGoldCoinRechargeRecordList(string token, string playerUserName, string orderNumber, MyDateTime beginCreateTime, MyDateTime endCreateTime, int pageItemCount, int pageIndex);
    }
}
