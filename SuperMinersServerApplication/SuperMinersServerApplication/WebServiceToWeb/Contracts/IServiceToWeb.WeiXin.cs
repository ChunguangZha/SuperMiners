using MetaData.Trade;
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

        [OperationContract]
        int GatherStones(string userName, decimal stones);

        [OperationContract]
        int BuyMiner(string userName, int minersCount);

        [OperationContract]
        TradeOperResult BuyMine(string userName, int minesCount, int payType);

        [OperationContract]
        TradeOperResult RechargeGoldCoin(string userName, int goldCoinCount, int payType);

        [OperationContract]
        int WithdrawRMB(string userName, int getRMBCount);
    }
}
