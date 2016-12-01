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

        public int GatherStones(string userName, decimal stones)
        {
            try
            {
                return base.Channel.GatherStones(userName, stones);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
                return OperResult.RESULTCODE_EXCEPTION;
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

    }
}