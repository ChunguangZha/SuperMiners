using MetaData.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.WebServiceToWeb.Contracts
{
    public partial interface IServiceToWeb
    {
        [OperationContract]
        string GetAccessToken();

        [OperationContract]
        int BindWeiXinUser(string wxUserOpenID, string wxUserName, string xlUserName, string xlUserPassword, string ip);

        [OperationContract]
        int WeiXinLogin(string wxUserOpenID, string wxUserName, string ip);

        [OperationContract]
        PlayerInfo GetPlayerByWeiXinOpenID(string openid);
    }
}
