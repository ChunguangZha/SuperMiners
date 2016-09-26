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
        [WebInvoke(UriTemplate = "/WebServiceAdmin/GetAwardItems",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RouletteAwardItem[] GetAwardItems(string token);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/SetAwardItems",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        bool SetAwardItems(string token, RouletteAwardItem[] items);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/GetNotPayWinAwardRecords",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RouletteWinnerRecord[] GetNotPayWinAwardRecords(string token);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/PayAward",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        int PayAward(string token, string adminUserName, string playerUserName, int recordID);
    }
}
