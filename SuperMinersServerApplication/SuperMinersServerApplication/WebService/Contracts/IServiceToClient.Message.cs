using MetaData;
using MetaData.ActionLog;
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
        [WebInvoke(UriTemplate = "/WebService/GetPlayerAction",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PlayerActionLog[] GetPlayerAction(string token, int year, int month, int day, int hour, int minute, int second);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/GetNotices",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        NoticeInfo[] GetNotices(string token, int year, int month, int day, int hour, int minute, int second);
    }
}
