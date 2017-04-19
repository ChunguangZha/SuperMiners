using MetaData;
using MetaData.Game.StoneStack;
using MetaData.Shopping;
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
        [WebInvoke(UriTemplate = "/WebServiceAdmin/GetGameConfig",
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
        [WebInvoke(UriTemplate = "/WebServiceAdmin/GetPlayer",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PlayerInfoLoginWrap GetPlayer(string token, string userName);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/ChangePlayer",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        int ChangePlayer(string token, string actionPassword, PlayerInfoLoginWrap player);

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
        bool LockPlayer(string token, string actionPassword, string playerUserName, int expireDays);

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
        [WebInvoke(UriTemplate = "/WebServiceAdmin/GetNotices",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        NoticeInfo[] GetNotices(string token);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/SaveNotice",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        bool SaveNotice(string token, NoticeInfo notice, bool isAdd);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/GetSellStonesOrderList",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        SellStonesOrder[] GetSellStonesOrderList(string token, string sellerUserName, string orderNumber, int orderState, MyDateTime myBeginCreateTime, MyDateTime myEndCreateTime, int pageItemCount, int pageIndex);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/GetLockedStonesOrderList",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        LockSellStonesOrder[] GetLockedStonesOrderList(string token, string sellerUserName, string orderNumber, string buyUserName, int orderState);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/GetBuyStonesOrderList",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        BuyStonesOrder[] GetBuyStonesOrderList(string token, string sellerUserName, string orderNumber, string buyUserName, int orderState, MyDateTime myBeginCreateTime, MyDateTime myEndCreateTime, MyDateTime myBeginBuyTime, MyDateTime myEndBuyTime, int pageItemCount, int pageIndex);

        //[OperationContract]
        //[WebInvoke(UriTemplate = "/WebServiceAdmin/AgreeExceptionStoneOrder",
        //    Method = "POST",
        //    ResponseFormat = WebMessageFormat.Json,
        //    RequestFormat = WebMessageFormat.Json,
        //    BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //int AgreeExceptionStoneOrder(string token, AlipayRechargeRecord alipayRecord);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/RejectExceptionStoneOrder",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        int RejectExceptionStoneOrder(string token, string orderNumber);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/PayWithdrawRMBRecord",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        int PayWithdrawRMBRecord(string token, WithdrawRMBRecord record);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/GetWithdrawRMBRecordList",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        WithdrawRMBRecord[] GetWithdrawRMBRecordList(string token, int state, string playerUserName, MyDateTime beginCreateTime, MyDateTime endCreateTime, string adminUserName, MyDateTime beginPayTime, MyDateTime endPayTime, int pageItemCount, int pageIndex);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/GetFinishedGoldCoinRechargeRecordList",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        GoldCoinRechargeRecord[] GetFinishedGoldCoinRechargeRecordList(string token, string playerUserName, string orderNumber, MyDateTime beginCreateTime, MyDateTime endCreateTime, int pageItemCount, int pageIndex);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/GetAllExceptionAlipayRechargeRecords",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        AlipayRechargeRecord[] GetAllExceptionAlipayRechargeRecords(string token);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/SearchExceptionAlipayRechargeRecord",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        AlipayRechargeRecord SearchExceptionAlipayRechargeRecord(string token, string orderNumber);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/HandleExceptionAlipayRechargeRecord",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        int HandleExceptionAlipayRechargeRecord(string token, AlipayRechargeRecord exceptionRecord);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/GetAllAlipayRechargeRecords",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        AlipayRechargeRecord[] GetAllAlipayRechargeRecords(string token, string orderNumber, string alipayOrderNumber, string payEmail, string playerUserName, MyDateTime beginPayTime, MyDateTime endPayTime, int pageItemCount, int pageIndex);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/GetBuyMinerFinishedRecordList",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        MinersBuyRecord[] GetBuyMinerFinishedRecordList(string token, string playerUserName, MyDateTime beginCreateTime, MyDateTime endCreateTime, int pageItemCount, int pageIndex);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/GetBuyMineFinishedRecordList",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        MinesBuyRecord[] GetBuyMineFinishedRecordList(string token, string playerUserName, MyDateTime beginCreateTime, MyDateTime endCreateTime, int pageItemCount, int pageIndex);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/SetPlayerAsAgent",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        int SetPlayerAsAgent(string token, int userID, string userName, string agentReferURL);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/GetExpChangeRecord",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ExpChangeRecord[] GetExpChangeRecord(string token, int userID);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/GetDeletedPlayers",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PlayerInfo[] GetDeletedPlayers(string token);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/GetPlayerTransferRecords",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        OldPlayerTransferRegisterInfo[] GetPlayerTransferRecords(string token);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/TransferPlayerFrom",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        int TransferPlayerFrom(string token, int recordID, string userName, string adminUserName);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/TransferPlayerTo",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        int TransferPlayerTo(string adminUserName, PlayerSimpleInfo simpleInfo, PlayerFortuneInfo fortuneInfo, string newUserLoginName, string newPassword);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/GetStoneDelegateBuyOrderInfo",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        StoneDelegateBuyOrderInfo[] GetStoneDelegateBuyOrderInfo(string token, string playerUserName, MyDateTime beginCreateTime, MyDateTime endCreateTime, int pageItemCount, int pageIndex);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/GetStoneDelegateSellOrderInfo",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        StoneDelegateSellOrderInfo[] GetStoneDelegateSellOrderInfo(string token, string playerUserName, MyDateTime beginFinishedTime, MyDateTime endFinishedTime, int pageItemCount, int pageIndex);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/HandlePlayerRemoteService",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        int HandlePlayerRemoteService(string token, string actionPassword, string playerUserName, string serviceContent, MyDateTime serviceTime, string engineerName);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/GetUserRemoteServerBuyRecords",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        UserRemoteServerBuyRecord[] GetUserRemoteServerBuyRecords(string token, string playerUserName, MyDateTime beginCreateTime, MyDateTime endCreateTime, int pageItemCount, int pageIndex);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/GetUserRemoteHandleServiceRecords",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        UserRemoteHandleServiceRecord[] GetUserRemoteHandleServiceRecords(string token, string playerUserName, MyDateTime beginCreateTime, MyDateTime endCreateTime, int pageItemCount, int pageIndex);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/AddVirtualShoppingItem",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        int AddVirtualShoppingItem(string token, string actionPassword, VirtualShoppingItem item);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/UpdateVirtualShoppingItem",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        int UpdateVirtualShoppingItem(string token, string actionPassword, VirtualShoppingItem item);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/GetVirtualShoppingItems",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        VirtualShoppingItem[] GetVirtualShoppingItems(string token, bool getAllItem, SellState state);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/GetPlayerBuyVirtualShoppingItemRecord",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PlayerBuyVirtualShoppingItemRecord[] GetPlayerBuyVirtualShoppingItemRecord(string token, string playerUserName, string shoppingItemName, MyDateTime beginBuyTime, MyDateTime endBuyTime, int pageItemCount, int pageIndex);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/AddDiamondShoppingItem",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        int AddDiamondShoppingItem(string token, string actionPassword, DiamondShoppingItem item);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/UpdateDiamondShoppingItem",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        int UpdateDiamondShoppingItem(string token, string actionPassword, DiamondShoppingItem item);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/GetDiamondShoppingItems",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        DiamondShoppingItem[] GetDiamondShoppingItems(string token, bool getAllItem, SellState state);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/HandleBuyDiamondShopping",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        int HandleBuyDiamondShopping(string token, string actionPassword, PlayerBuyDiamondShoppingItemRecord record);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/GetPlayerBuyDiamondShoppingItemRecordByName",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PlayerBuyDiamondShoppingItemRecord[] GetPlayerBuyDiamondShoppingItemRecordByName(string token, string playerUserName, string shoppingItemName, int shoppingStateInt, MyDateTime beginBuyTime, MyDateTime endBuyTime, int pageItemCount, int pageIndex);
    }
}
