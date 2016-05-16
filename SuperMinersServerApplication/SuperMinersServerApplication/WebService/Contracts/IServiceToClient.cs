using SuperMinersServerApplication.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.WebService.Contracts
{
#if !SILVERLIGHT
    [ServiceContract]
#endif
    public partial interface IServiceToClient
    {
#if !SILVERLIGHT
        //[OperationContract]
        //[WebGet(UriTemplate = "/clientaccesspolicy.xml")]
        //Stream GetClientAccessPolicy();

        //[OperationContract]
        //[WebGet(UriTemplate = "/crossdomain.xml")]
        //Stream GetCrossDomain();
#endif

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/Callback",
          Method = "POST",
          ResponseFormat = WebMessageFormat.Json,
          RequestFormat = WebMessageFormat.Json,
          BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        CallbackInfo Callback(string token);

        //[OperationContract]
        //[WebInvoke(UriTemplate = "/WebService/GetTimeZone",
        //  Method = "POST",
        //  ResponseFormat = WebMessageFormat.Json,
        //  RequestFormat = WebMessageFormat.Json,
        //  BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //string GetTimeZone(string token);
    }
}
