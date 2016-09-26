using MetaData.Game.Roulette;
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
        [WebInvoke(UriTemplate = "/WebService/GetAwardItems",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RouletteAwardItem[] GetAwardItems(string token);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/StartRoulette",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RouletteWinAwardResult StartRoulette(string token);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/FinishRoulette",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RouletteWinnerRecord FinishRoulette(string token, int winAwardNumber);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/GetRouletteAward",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        int GetRouletteAward(string token, int recordID, string info1, string info2);
    }
}
