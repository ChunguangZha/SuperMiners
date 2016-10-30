using MetaData;
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
        RouletteAwardItem[] GetAwardItems(string token, string userName);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/StartRoulette",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RouletteWinAwardResult StartRoulette(string token, string userName);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/FinishRoulette",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RouletteWinnerRecord FinishRoulette(string token, string userName, int winAwardID);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/GetRouletteAward",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        int TakeRouletteAward(string token, string userName, int recordID, string info1, string info2);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/GetAllWinAwardRecords",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RouletteWinnerRecord[] GetAllWinAwardRecords(string token, string userName, int RouletteAwardItemID, MyDateTime BeginWinTime, MyDateTime EndWinTime, int IsGot, int IsPay, int pageItemCount, int pageIndex);

    }
}
