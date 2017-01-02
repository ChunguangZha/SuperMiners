using MetaData;
using MetaData.Game.RaideroftheLostArk;
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
        [WebInvoke(UriTemplate = "/WebService/GetCurrentRaiderRoundInfo",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RaiderRoundMetaDataInfo GetCurrentRaiderRoundInfo(string token);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/JoinRaider",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        OperResultObject JoinRaider(string token, string userName, int roundID, int betStoneCount);

        /// <summary>
        /// roundID : -1表示全部
        /// </summary>
        /// <param name="token"></param>
        /// <param name="roundID"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/GetPlayerselfBetInfo",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PlayerBetInfo[] GetPlayerselfBetInfo(string token, int roundID, int pageItemCount, int pageIndex);
    }
}
