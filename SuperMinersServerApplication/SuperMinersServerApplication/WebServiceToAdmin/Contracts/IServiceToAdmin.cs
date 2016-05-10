using MetaData.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.WebServiceToAdmin.Contracts
{
    [ServiceContract]
    interface IServiceToAdmin
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/LoginAdmin",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string LoginAdmin(string adminName, string password, string key);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/LogoutAdmin",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        bool LogoutAdmin(string token);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="isOnline">1表示在线，0表示不在线，-1表示全部</param>
        /// <param name="isLocked">1表示被锁定，0表示没被锁定，-1表示全部</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/GetPlayers",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Model.UserInfo[] GetPlayers(string token, int isOnline, int isLocked);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/DeletePlayers",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        void DeletePlayers(string token, string actionPassword, string[] userNames);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/LogOutPlayers",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        bool LogOutPlayers(string token, string actionPassword, string[] userNames);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/LockPlayers",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        bool LockPlayers(string token, string actionPassword, string[] userNames);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebServiceAdmin/UpdatePlayerInfo",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        bool UpdatePlayerInfo(string token, string actionPassword, PlayerSimpleInfo simpleInfo, PlayerFortuneInfo fortuneInfo);


    }
}
