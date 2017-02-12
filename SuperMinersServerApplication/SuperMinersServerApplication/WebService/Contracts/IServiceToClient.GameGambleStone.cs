using MetaData.Game.GambleStone;
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
        [WebInvoke(UriTemplate = "/WebService/GambleStoneBetIn",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        GambleStonePlayerBetInResult GambleStoneBetIn(string token, GambleStoneItemColor color, int stoneCount, int gravelCount);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/GetGambleStoneRoundInning",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        GambleStoneRound_InningInfo GetGambleStoneRoundInning(string token);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/GetGambleStoneRoundInfo",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        GambleStoneRoundInfo GetGambleStoneRoundInfo(string token);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/GetGambleStoneInningInfo",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        GambleStoneInningInfo GetGambleStoneInningInfo(string token);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/GetLastMonthGambleStonePlayerBetRecord",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        GambleStonePlayerBetRecord[] GetLastMonthGambleStonePlayerBetRecord(string token);
    }
}
