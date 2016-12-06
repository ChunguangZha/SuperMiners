using MetaData;
using MetaData.Trade;
using MetaData.User;
using SuperMinersServerApplication.WebServiceToWeb.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace SuperMinersWeiXin.Wcf.Services
{
    public partial class SuperMinersClient : ClientBase<IServiceToWeb>, IServiceToWeb
    {

        public string GetAccessToken()
        {
            try
            {
                return base.Channel.GetAccessToken();
            }
            catch (Exception)
            {
                return "";
            }
        }

        public int BindWeiXinUser(string wxUserOpenID, string wxUserName, string xlUserName, string xlUserPassword, string ip)
        {
            try
            {
                return base.Channel.BindWeiXinUser(wxUserOpenID, wxUserName, xlUserName, xlUserPassword, ip);
            }
            catch (Exception)
            {
                return OperResult.RESULTCODE_EXCEPTION;
            }
        }

        /// <summary>
        /// RESULTCODE_REGISTER_USERNAME_LENGTH_SHORT; RESULTCODE_FAILED; RESULTCODE_REGISTER_USERNAME_EXIST; RESULTCODE_TRUE; RESULTCODE_EXCEPTION
        /// </summary>
        /// <param name="clientIP"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="alipayAccount"></param>
        /// <param name="alipayRealName"></param>
        /// <param name="invitationCode"></param>
        /// <returns></returns>
        public int RegisterUserFromWeiXin(string wxUserOpenID, string wxUserName, string clientIP, string userName, string nickName, string password, string alipayAccount, string alipayRealName, string IDCardNo, string email, string qq, string invitationCode)
        {
            try
            {
                return base.Channel.RegisterUserFromWeiXin(wxUserOpenID, wxUserName, clientIP, userName, nickName, password, alipayAccount, alipayRealName, IDCardNo, email, qq, invitationCode);
            }
            catch (Exception)
            {
                return OperResult.RESULTCODE_EXCEPTION;
            }
        }

        public int WeiXinLogin(string wxUserOpenID, string wxUserName, string ip)
        {
            try
            {
                return base.Channel.WeiXinLogin(wxUserOpenID, wxUserName, ip);
            }
            catch (Exception)
            {
                return OperResult.RESULTCODE_EXCEPTION;
            }
        }

        public PlayerInfo GetPlayerByWeiXinOpenID(string openid)
        {
            try
            {
                return base.Channel.GetPlayerByWeiXinOpenID(openid);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public PlayerInfo GetPlayerByXLUserName(string xlUserName)
        {
            try
            {
                return base.Channel.GetPlayerByXLUserName(xlUserName);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public GatherTempOutputStoneResult GatherStones(string userName, decimal stones)
        {
            try
            {
                return base.Channel.GatherStones(userName, stones);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
                return null;
            }
        }

        public int BuyMiner(string userName, int minersCount)
        {
            try
            {
                return base.Channel.BuyMiner(userName, minersCount);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
                return OperResult.RESULTCODE_EXCEPTION;
            }
        }

        public TradeOperResult BuyMine(string userName, int minesCount, PayType payType)
        {
            try
            {
                return base.Channel.BuyMine(userName, minesCount, payType);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
                return null;
            }
        }

        public TradeOperResult RechargeGoldCoin(string userName, int goldCoinCount, PayType payType)
        {
            try
            {
                return base.Channel.RechargeGoldCoin(userName, goldCoinCount, payType);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
                return null;
            }
        }

        public int WithdrawRMB(string userName, int getRMBCount)
        {
            try
            {
                return base.Channel.WithdrawRMB(userName, getRMBCount);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
                return OperResult.RESULTCODE_EXCEPTION;
            }
        }

        public int SellStones(string userName, int stoneCount)
        {
            try
            {
                return base.Channel.SellStones(userName, stoneCount);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
                return OperResult.RESULTCODE_EXCEPTION;
            }
        }
    }
}