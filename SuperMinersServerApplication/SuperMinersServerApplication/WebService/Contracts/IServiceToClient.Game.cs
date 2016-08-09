using MetaData.User;
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
        [WebInvoke(UriTemplate = "/WebService/BuyMiner",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        int BuyMiner(string token, string userName, int minersCount, int tradeType);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/BuyMine",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        int BuyMine(string token, string userName, int minesCount, int tradeType);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/GoldCoinRecharge",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        int GoldCoinRecharge(string token, string userName, int goldCoinCount, int tradeType);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/GatherStones",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        int GatherStones(string token, string userName, float stones);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/GetExpTopList",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        TopListInfo[] GetExpTopList(string token);
            
        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/GetStoneTopList",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        TopListInfo[] GetStoneTopList(string token);
            
        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/GetMinerTopList",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        TopListInfo[] GetMinerTopList(string token);
            
        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/GetGoldCoinTopList",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        TopListInfo[] GetGoldCoinTopList(string token);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/GetReferrerTopList",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        TopListInfo[] GetReferrerTopList(string token);
    }
}
