﻿using MetaData;
using MetaData.Trade;
using MetaData.User;
using SuperMinersServerApplication.WebServiceToWeb.Contracts;
using SuperMinersWeiXin.Core;
using SuperMinersWeiXin.Utility;
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

        public OperResultObject BindWeiXinUser(string wxUserOpenID, string wxUserName, string xlUserName, string xlUserPassword, string ip)
        {
            try
            {
                return base.Channel.BindWeiXinUser(wxUserOpenID, wxUserName, xlUserName, xlUserPassword, ip);
            }
            catch (Exception)
            {
                OperResultObject resultObj = new OperResultObject();
                resultObj.OperResultCode = OperResult.RESULTCODE_EXCEPTION;
                return resultObj;
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

        public OperResultObject WeiXinLogin(string wxUserOpenID, string wxUserName, string ip)
        {
            try
            {
                return base.Channel.WeiXinLogin(wxUserOpenID, wxUserName, ip);
            }
            catch (Exception)
            {
                return new OperResultObject() { OperResultCode = OperResult.RESULTCODE_EXCEPTION };
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

        public OperResultObject WithdrawRMB(string userName, int getRMBCount)
        {
            try
            {
                return base.Channel.WithdrawRMB(userName, getRMBCount);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
                return new OperResultObject() { OperResultCode = OperResult.RESULTCODE_EXCEPTION };
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

        public MinesBuyRecord[] GetBuyMineFinishedRecordList(string userName, int pageItemCount, int pageIndex)
        {
            try
            {
                return base.Channel.GetBuyMineFinishedRecordList(userName, pageItemCount, pageIndex);
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("GetBuyMineFinishedRecordList异常", exc);
                return null;
            }
        }

        public MinersBuyRecord[] GetBuyMinerFinishedRecordList(string userName, int pageItemCount, int pageIndex)
        {
            try
            {
                return base.Channel.GetBuyMinerFinishedRecordList(userName, pageItemCount, pageIndex);
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("GetBuyMinerFinishedRecordList异常", exc);
                return null;
            }
        }

        public GoldCoinRechargeRecord[] GetFinishedGoldCoinRechargeRecordList(string userName, int pageItemCount, int pageIndex)
        {
            try
            {
                return base.Channel.GetFinishedGoldCoinRechargeRecordList(userName, pageItemCount, pageIndex);
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("GetFinishedGoldCoinRechargeRecordList异常", exc);
                return null;
            }
        }

        public WithdrawRMBRecord[] GetWithdrawRMBRecordList(string userName, int pageItemCount, int pageIndex)
        {
            try
            {
                return base.Channel.GetWithdrawRMBRecordList(userName, pageItemCount, pageIndex);
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("GetWithdrawRMBRecordList异常", exc);
                return null;
            }
        }

        public SellStonesOrder[] GetUserSellStoneOrders(string sellerUserName, int pageItemCount, int pageIndex)
        {
            try
            {
                return base.Channel.GetUserSellStoneOrders(sellerUserName, pageItemCount, pageIndex);
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("GetUserSellStoneOrders异常", exc);
                return null;
            }
        }

        public BuyStonesOrder[] GetUserBuyStoneOrders(string buyUserName, int pageItemCount, int pageIndex)
        {
            try
            {
                return base.Channel.GetUserBuyStoneOrders(buyUserName, pageItemCount, pageIndex);
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("GetUserBuyStoneOrders异常", exc);
                return null;
            }
        }

        public SellStonesOrder[] GetAllNotFinishedSellOrders(string userName)
        {
            try
            {
                return base.Channel.GetAllNotFinishedSellOrders(userName);
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("GetAllNotFinishedSellOrders异常", exc);
                return null;
            }
        }

        public int BuyStone(string userName, string stoneOrderNumber)
        {
            try
            {
                return base.Channel.BuyStone(userName, stoneOrderNumber);
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("BuyStone异常", exc);
                return OperResult.RESULTCODE_EXCEPTION;
            }
        }

        //public void AsyncGetAllNotFinishedSellOrders(object userState, params object[] inputValues)
        //{
        //    base.InvokeAsync(new BeginOperationDelegate(BeginGetAllNotFinishedSellOrders),
        //        inputValues,
        //        new EndOperationDelegate(EndGetAllNotFinishedSellOrders),
        //        new System.Threading.SendOrPostCallback(GetAllNotFinishedSellOrdersCallback),
        //        userState);
        //}

        //private IAsyncResult BeginGetAllNotFinishedSellOrders(object[] inValues, AsyncCallback asyncCallback, object state)
        //{
        //    this.Channel
        //    MyHttpAsyncResult result = new MyHttpAsyncResult(asyncCallback, state);
        //    return result;
        //}

        //private object[] EndGetAllNotFinishedSellOrders(IAsyncResult result)
        //{
            
        //}

        //private void GetAllNotFinishedSellOrdersCallback(object state)
        //{

        //}
    }
}