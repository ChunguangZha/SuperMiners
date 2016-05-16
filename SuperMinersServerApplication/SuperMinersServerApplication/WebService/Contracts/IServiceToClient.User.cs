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
        [WebInvoke(UriTemplate = "/WebService/Login",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string Login(string userName, string password, string key);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/Logout",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        bool Logout(string token);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/GetUserInfo",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PlayerInfo GetPlayerInfo(string token);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/ChangePassword",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        bool ChangePassword(string token, string oldPassword, string newPassword);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/ChangeAlipay",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        bool ChangePlayerSimpleInfo(string token, string nickName, string alipayAccount, string alipayRealName);
    }
}
