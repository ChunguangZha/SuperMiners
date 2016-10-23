using MetaData.AgentUser;
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
        string Login(string userName, string password, string key, string mac, string clientVersion);

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
        [WebInvoke(UriTemplate = "/WebService/ChangePlayerSimpleInfo",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        bool ChangePlayerSimpleInfo(string token, string nickName, string alipayAccount, string alipayRealName, string email, string qq);

        /// <summary>
        /// -2表示参数无效，-1表示异常，0,表示不存在，1表示存在
        /// </summary>
        /// <param name="alipayAccount"></param>
        /// <param name="alipayRealName"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/CheckUserAlipayExist",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        int CheckUserAlipayExist(string token, string alipayAccount, string alipayRealName);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/GetUserReferrerTree",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        UserReferrerTreeItem[] GetUserReferrerTree(string token, string userName);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/GetAgentUserInfo",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        AgentUserInfo GetAgentUserInfo(string token, string userName);
    }
}
