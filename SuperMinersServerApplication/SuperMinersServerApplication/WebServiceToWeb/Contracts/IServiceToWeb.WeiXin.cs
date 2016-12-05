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

        /// <summary>
        /// RESULTCODE_REGISTER_USERNAME_LENGTH_SHORT; RESULTCODE_FALSE; RESULTCODE_REGISTER_USERNAME_EXIST; RESULTCODE_SUCCEED; RESULTCODE_EXCEPTION
        /// </summary>
        /// <param name="clientIP"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <param name="qq"></param>
        /// <param name="invitationCode"></param>
        /// <returns></returns>
        [OperationContract]
        int RegisterUserFromWeiXin(string wxUserOpenID, string wxUserName, string clientIP, string userName, string nickName, string password,
            string alipayAccount, string alipayRealName, string IDCardNo, string email, string qq, string invitationCode);

        [OperationContract]
        int WeiXinLogin(string wxUserOpenID, string wxUserName, string ip);

        [OperationContract]
        PlayerInfo GetPlayerByWeiXinOpenID(string openid);

        [OperationContract]
        PlayerInfo GetPlayerByXLUserName(string xlUserName);

        [OperationContract]
        int GatherStones(string userName, decimal stones);

        [OperationContract]
        int BuyMiner(string userName, int minersCount);

        [OperationContract]
        TradeOperResult BuyMine(string userName, int minesCount, PayType payType);

        [OperationContract]
        TradeOperResult RechargeGoldCoin(string userName, int goldCoinCount, PayType payType);

        [OperationContract]
        int WithdrawRMB(string userName, int getRMBCount);
    }
}
