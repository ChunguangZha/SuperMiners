using MetaData;
using MetaData.StoneFactory;
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
        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/GetStoneFactorySystemDailyProfitList",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        StoneFactorySystemDailyProfit[] GetStoneFactorySystemDailyProfitList(string token, int pageItemCount, int pageIndex);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/GetPlayerStoneFactoryAccountInfo",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PlayerStoneFactoryAccountInfo GetPlayerStoneFactoryAccountInfo(string token, int userID);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/GetStoneFactoryProfitRMBChangedRecordList",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        StoneFactoryProfitRMBChangedRecord[] GetStoneFactoryProfitRMBChangedRecordList(string token, int userID, MyDateTime beginTime, MyDateTime endTime, int pageItemCount, int pageIndex);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/AddStoneToFactory",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        int AddStoneToFactory(string token, int userID, string userName, int stoneStackCount);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/AddMinersToFactory",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        int AddMinersToFactory(string token, int userID, string userName, int minersGroupCount);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/WithdrawOutputRMBFromFactory",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        int WithdrawOutputRMBFromFactory(string token, int userID, string userName, decimal withdrawRMBCount);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/WithdrawStoneFromFactory",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        int WithdrawStoneFromFactory(string token, int userID, string userName, int stoneStackCount);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/FeedSlave",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        int FeedSlave(string token, int userID);

    }
}
