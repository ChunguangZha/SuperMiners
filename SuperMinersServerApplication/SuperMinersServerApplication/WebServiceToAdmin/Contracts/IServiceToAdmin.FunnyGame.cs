using MetaData;
using MetaData.Game.Roulette;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.WebServiceToAdmin.Contracts
{
    public partial interface IServiceToAdmin
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/GetAllAwardItems",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RouletteAwardItem[] GetAllAwardItems(string token);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/AddAwardItem",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        int AddAwardItem(string token, RouletteAwardItem item);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/UpdateAwardItem",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        int UpdateAwardItem(string token, RouletteAwardItem item);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/DeleteAwardItem",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        int DeleteAwardItem(string token, RouletteAwardItem item);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/GetCurrentAwardItems",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RouletteAwardItem[] GetCurrentAwardItems(string token);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/SetCurrentAwardItems",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        bool SetCurrentAwardItems(string token, RouletteAwardItem[] items);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/GetNotPayWinAwardRecords",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RouletteWinnerRecord[] GetNotPayWinAwardRecords(string token);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/GetAllPayWinAwardRecords",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RouletteWinnerRecord[] GetAllPayWinAwardRecords(string token, string UserName, int RouletteAwardItemID, MyDateTime BeginWinTime, MyDateTime EndWinTime, int IsGot, int IsPay, int pageItemCount, int pageIndex);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/PayAward",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        int PayAward(string token, string adminUserName, string playerUserName, int recordID);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/GetAllRouletteRoundInfo",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RouletteRoundInfo[] GetAllRouletteRoundInfo(string token);
    }
}
