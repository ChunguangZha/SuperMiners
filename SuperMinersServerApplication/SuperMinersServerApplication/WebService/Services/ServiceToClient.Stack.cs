using DataBaseProvider;
using MetaData;
using MetaData.Game.StoneStack;
using SuperMinersServerApplication.Controller;
using SuperMinersServerApplication.Controller.Trade;
using SuperMinersServerApplication.Encoder;
using SuperMinersServerApplication.Utility;
using SuperMinersServerApplication.WebService.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.WebService.Services
{
    public partial class ServiceToClient : IServiceToClient
    {

        public int DelegateSellStone(string token, int sellStoneHandsCount, decimal price)
        {
            if (RSAProvider.LoadRSA(token))
            {
                string userName = "";
                try
                {
                    if (sellStoneHandsCount <= 0)
                    {
                        return OperResult.RESULTCODE_PARAM_INVALID;
                    }

                    userName = ClientManager.GetClientUserName(token);
                    var playerRunner = PlayerController.Instance.GetRunnable(userName);
                    if (playerRunner == null)
                    {
                        return OperResult.RESULTCODE_USER_NOT_EXIST;
                    }

                    int sellStoneCount = sellStoneHandsCount * GlobalConfig.GameConfig.HandStoneCount;
                    int feeNeedStoneCount = (int)(sellStoneHandsCount * GlobalConfig.GameConfig.HandStoneCount * (GlobalConfig.GameConfig.ExchangeExpensePercent / 100));

                    if (playerRunner.SellableStonesCount < sellStoneCount + feeNeedStoneCount)
                    {
                        return OperResult.RESULTCODE_ORDER_SELLABLE_STONE_LACK;
                    }

                    DateTime timenow = DateTime.Now;
                    StoneDelegateSellOrderInfo sellOrder = new StoneDelegateSellOrderInfo()
                    {
                        UserID = playerRunner.BasePlayer.SimpleInfo.UserID,
                        UserName = playerRunner.BasePlayer.SimpleInfo.UserName,
                        DelegateTime = new MyDateTime(timenow),
                        OrderNumber = OrderController.Instance.CreateOrderNumber(userName, timenow, MetaData.Trade.AlipayTradeInType.StackStoneSell),
                        SellState = StoneDelegateSellState.Waiting,
                        IsSubOrder = false,
                        SellUnit = new StackTradeUnit()
                        {
                            Price = price,
                            TradeStoneHandCount = sellStoneHandsCount
                        }
                    };

                    CustomerMySqlTransaction myTrans = MyDBHelper.Instance.CreateTrans();
                    try
                    {
                        var resultObj = OrderController.Instance.StoneStackController.PlayerDelegateSellStone(sellOrder, myTrans);
                        if (resultObj.OperResultCode != OperResult.RESULTCODE_TRUE)
                        {
                            //卖单提交失败，不再进行数据库操作
                            return resultObj.OperResultCode;
                        }

                        playerRunner.AddNewSellStonesByDelegate(sellStoneCount, feeNeedStoneCount, myTrans);

                        myTrans.Commit();

                        //PlayerActionController.Instance.AddLog(userName, MetaData.ActionLog.ActionType.DelegateSellStone, sellStoneHandsCount, "");
                        LogHelper.Instance.AddInfoLog("玩家[" + userName + "] 挂单委托出售 " + sellStoneHandsCount + " 手矿石，Price：" + price);
                        return OperResult.RESULTCODE_TRUE;
                    }
                    catch (Exception exc)
                    {
                        myTrans.Rollback();
                        LogHelper.Instance.AddErrorLog("ServiceToClient.DelegateSellStone Exception userName: " + userName, exc);
                        return OperResult.RESULTCODE_FALSE;
                    }
                    finally
                    {
                        myTrans.Dispose();
                    }
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("玩家[" + userName + "] DelegateSellStone Exception", exc);
                    return OperResult.RESULTCODE_EXCEPTION;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public int CancelDelegateSellStone(string token, MetaData.Game.StoneStack.StoneDelegateSellOrderInfo sellOrder)
        {
            if (RSAProvider.LoadRSA(token))
            {
                string userName = "";
                try
                {
                    userName = ClientManager.GetClientUserName(token);
                    var playerRunner = PlayerController.Instance.GetRunnable(userName);
                    if (playerRunner == null)
                    {
                        return OperResult.RESULTCODE_USER_NOT_EXIST;
                    }

                    CustomerMySqlTransaction myTrans = MyDBHelper.Instance.CreateTrans();
                    try
                    {
                        StoneDelegateSellOrderInfo canceledSellOrder = null;
                        var resultObj = OrderController.Instance.StoneStackController.PlayerCancelSellStone(sellOrder.OrderNumber, sellOrder.SellUnit.Price, myTrans, out canceledSellOrder);
                        if (resultObj.OperResultCode != OperResult.RESULTCODE_TRUE)
                        {
                            return resultObj.OperResultCode;
                        }
                        if (canceledSellOrder == null)
                        {
                            return OperResult.RESULTCODE_FALSE;
                        }
                        playerRunner.CancelDelegateSellStoneOrder(canceledSellOrder, myTrans);

                        myTrans.Commit();

                        LogHelper.Instance.AddInfoLog("玩家[" + userName + "], 撤消矿石委托卖单。Order: " + canceledSellOrder.ToString());
                        return OperResult.RESULTCODE_TRUE;
                    }
                    catch (Exception exc)
                    {
                        myTrans.Rollback();
                        LogHelper.Instance.AddErrorLog("ServiceToClient.CancelDelegateSellStone Exception userName: " + userName, exc);
                        return OperResult.RESULTCODE_FALSE;
                    }
                    finally
                    {
                        myTrans.Dispose();
                    }
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("玩家[" + userName + "] CancelDelegateSellStone Exception", exc);
                    return OperResult.RESULTCODE_EXCEPTION;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public MetaData.Game.StoneStack.StoneDelegateSellOrderInfo[] GetNotFinishedDelegateSellStoneOrders(string token)
        {
            if (RSAProvider.LoadRSA(token))
            {
                string userName = "";
                try
                {
                    userName = ClientManager.GetClientUserName(token);
                    var playerInfo = PlayerController.Instance.GetPlayerInfoByUserName(userName);
                    if (playerInfo == null)
                    {
                        return null;
                    }
                    return DBProvider.StoneStackDBProvider.GetNotFinishedStoneDelegateSellOrderInfoByPlayer(playerInfo.SimpleInfo.UserID);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("玩家[" + userName + "] GetFinishedDelegateBuyStoneOrders Exception", exc);
                    return null;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public MetaData.Game.StoneStack.StoneDelegateSellOrderInfo[] GetFinishedDelegateSellStoneOrders(string token, MetaData.MyDateTime myBeginFinishedTime, MetaData.MyDateTime myEndFinishedTime, int pageItemCount, int pageIndex)
        {
            if (RSAProvider.LoadRSA(token))
            {
                string userName = "";
                try
                {
                    userName = ClientManager.GetClientUserName(token);
                    if (string.IsNullOrEmpty(userName))
                    {
                        return null;
                    }
                    return DBProvider.StoneStackDBProvider.GetAllFinishedStoneDelegateSellOrderInfoByPlayer(userName, myBeginFinishedTime, myEndFinishedTime, pageItemCount, pageIndex);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("玩家[" + userName + "] GetFinishedDelegateBuyStoneOrders Exception", exc);
                    return null;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="buyStoneHandsCount"></param>
        /// <param name="price"></param>
        /// <param name="paytype"></param>
        /// <returns></returns>
        public OperResultObject DelegateBuyStone(string token, int buyStoneHandsCount, decimal price, MetaData.Trade.PayType paytype)
        {
            if (RSAProvider.LoadRSA(token))
            {
                OperResultObject resultObj = new OperResultObject();
                string userName = "";
                try
                {
                    if (buyStoneHandsCount <= 0)
                    {
                        resultObj.OperResultCode = OperResult.RESULTCODE_PARAM_INVALID;
                        return resultObj;
                    }

                    userName = ClientManager.GetClientUserName(token);
                    var playerRunner = PlayerController.Instance.GetRunnable(userName);
                    if (playerRunner == null)
                    {
                        resultObj.OperResultCode = OperResult.RESULTCODE_USER_NOT_EXIST;
                        return resultObj;
                    }

                    decimal allNeedRMB = buyStoneHandsCount * price;
                    if (paytype == MetaData.Trade.PayType.RMB)
                    {
                        if (playerRunner.BasePlayer.FortuneInfo.RMB < allNeedRMB)
                        {
                            resultObj.OperResultCode = OperResult.RESULTCODE_LACK_OF_BALANCE;
                            return resultObj;
                        }
                    }
                    else if (paytype == MetaData.Trade.PayType.Diamand)
                    {
                        if (playerRunner.BasePlayer.FortuneInfo.StockOfDiamonds < allNeedRMB * GlobalConfig.GameConfig.Diamonds_RMB)
                        {
                            resultObj.OperResultCode = OperResult.RESULTCODE_LACK_OF_BALANCE;
                            return resultObj;
                        }
                    }

                    DateTime timenow = DateTime.Now;
                    StoneDelegateBuyOrderInfo buyOrder = new StoneDelegateBuyOrderInfo()
                    {
                        UserID = playerRunner.BasePlayer.SimpleInfo.UserID,
                        UserName = playerRunner.BasePlayer.SimpleInfo.UserName,
                        DelegateTime = new MyDateTime(timenow),
                        OrderNumber = OrderController.Instance.CreateOrderNumber(userName, timenow, MetaData.Trade.AlipayTradeInType.StackStoneBuy),
                        BuyState = StoneDelegateBuyState.Waiting,
                        IsSubOrder = false,
                        PayType = paytype,
                        BuyUnit = new StackTradeUnit()
                        {
                            Price = price,
                            TradeStoneHandCount = buyStoneHandsCount
                        }
                    };

                    if (paytype == MetaData.Trade.PayType.Alipay)
                    {
                        string payLink = OrderController.Instance.CreateAlipayLink(userName, buyOrder.OrderNumber, "矿石", allNeedRMB, "迅灵矿石");
                        buyOrder.AlipayLink = payLink;
                    }

                    CustomerMySqlTransaction myTrans = MyDBHelper.Instance.CreateTrans();
                    try
                    {
                        resultObj = OrderController.Instance.StoneStackController.PlayerDelegateBuyStone(buyOrder, myTrans);
                        if (resultObj.OperResultCode != OperResult.RESULTCODE_TRUE)
                        {
                            //卖单提交失败，不再进行数据库操作
                            return resultObj;
                        }

                        playerRunner.AddNewBuyStonesByDelegate(buyOrder, myTrans);

                        myTrans.Commit();

                        if (paytype != MetaData.Trade.PayType.Alipay)
                        {
                            //PlayerActionController.Instance.AddLog(userName, MetaData.ActionLog.ActionType.DelegateBuyStone, buyStoneHandsCount, "");
                        }
                        LogHelper.Instance.AddInfoLog("玩家[" + userName + "] 挂单委托买入 " + buyStoneHandsCount + " 手矿石：" + buyOrder.ToString());

                        resultObj.OperResultCode = OperResult.RESULTCODE_TRUE;
                        resultObj.Message = buyOrder.AlipayLink;

                        return resultObj;
                    }
                    catch (Exception exc)
                    {
                        myTrans.Rollback();
                        LogHelper.Instance.AddErrorLog("ServiceToClient.DelegateBuyStone Exception userName: " + userName, exc);
                        resultObj.OperResultCode = OperResult.RESULTCODE_FALSE;
                        return resultObj;
                    }
                    finally
                    {
                        myTrans.Dispose();
                    }
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("玩家[" + userName + "] DelegateBuyStone Exception", exc);
                    resultObj.OperResultCode = OperResult.RESULTCODE_EXCEPTION;
                    return resultObj;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public int CancelDelegateBuyStone(string token, MetaData.Game.StoneStack.StoneDelegateBuyOrderInfo buyOrder)
        {
            if (RSAProvider.LoadRSA(token))
            {
                string userName = "";
                try
                {
                    userName = ClientManager.GetClientUserName(token);
                    var playerRunner = PlayerController.Instance.GetRunnable(userName);
                    if (playerRunner == null)
                    {
                        return OperResult.RESULTCODE_USER_NOT_EXIST;
                    }

                    StoneDelegateBuyOrderInfo canceledBuyOrder = null;
                    CustomerMySqlTransaction myTrans = MyDBHelper.Instance.CreateTrans();
                    try
                    {
                        var resultObj = OrderController.Instance.StoneStackController.PlayerCancelBuyStone(buyOrder.OrderNumber, buyOrder.BuyUnit.Price, myTrans, out canceledBuyOrder);
                        if (resultObj.OperResultCode != OperResult.RESULTCODE_TRUE)
                        {
                            return resultObj.OperResultCode;
                        }
                        if (canceledBuyOrder == null)
                        {
                            return OperResult.RESULTCODE_FALSE;
                        }
                        playerRunner.CancelDelegateBuyStoneOrder(canceledBuyOrder, myTrans);

                        myTrans.Commit();

                        LogHelper.Instance.AddInfoLog("玩家["+userName+"], 撤消矿石委托买单。Order: " + canceledBuyOrder.ToString());
                        return OperResult.RESULTCODE_TRUE;
                    }
                    catch (Exception exc)
                    {
                        myTrans.Rollback();
                        string errorMsg = "ServiceToClient.CancelDelegateBuyStone Exception userName: "
                            + userName + ", buyOrder : " + buyOrder.ToString();
                        LogHelper.Instance.AddErrorLog(errorMsg, exc);
                        return OperResult.RESULTCODE_FALSE;
                    }
                    finally
                    {
                        myTrans.Dispose();
                    }
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("玩家[" + userName + "] DelegateSellStone Exception", exc);
                    return OperResult.RESULTCODE_EXCEPTION;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public MetaData.Game.StoneStack.StoneDelegateBuyOrderInfo[] GetNotFinishedDelegateBuyStoneOrders(string token)
        {
            if (RSAProvider.LoadRSA(token))
            {
                string userName = "";
                try
                {
                    userName = ClientManager.GetClientUserName(token);
                    var playerInfo = PlayerController.Instance.GetPlayerInfoByUserName(userName);
                    if (playerInfo == null)
                    {
                        return null;
                    }
                    return DBProvider.StoneStackDBProvider.GetNotFinishedStoneDelegateBuyOrderInfoByPlayer(playerInfo.SimpleInfo.UserID);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("玩家[" + userName + "] GetFinishedDelegateBuyStoneOrders Exception", exc);
                    return null;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public MetaData.Game.StoneStack.StoneDelegateBuyOrderInfo[] GetFinishedDelegateBuyStoneOrders(string token, MetaData.MyDateTime myBeginCreateTime, MetaData.MyDateTime myEndCreateTime, int pageItemCount, int pageIndex)
        {
            if (RSAProvider.LoadRSA(token))
            {
                string userName = "";
                try
                {
                    userName = ClientManager.GetClientUserName(token);
                    if (string.IsNullOrEmpty(userName))
                    {
                        return null;
                    }
                    return DBProvider.StoneStackDBProvider.GetAllFinishedStoneDelegateBuyOrderInfoByPlayer(userName, myBeginCreateTime, myEndCreateTime, pageItemCount, pageIndex);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("玩家[" + userName + "] GetFinishedDelegateBuyStoneOrders Exception", exc);
                    return null;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public TodayStoneStackTradeRecordInfo GetTodayStoneStackInfo(string token)
        {
            if (RSAProvider.LoadRSA(token))
            {
                string userName = "";
                try
                {
                    userName = ClientManager.GetClientUserName(token);
                    return OrderController.Instance.StoneStackController.GetTodayStackInfo();
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("玩家[" + userName + "] GetFinishedDelegateBuyStoneOrders Exception", exc);
                    return null;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public StoneStackDailyRecordInfo[] GetTodayRealTimeTradeRecords(string token)
        {
            if (RSAProvider.LoadRSA(token))
            {
                string userName = "";
                try
                {
                    userName = ClientManager.GetClientUserName(token);
                    return OrderController.Instance.StoneStackController.GetTodayRealTimeTradeRecords();
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("玩家[" + userName + "] GetTodayRealTimeTradeRecords Exception", exc);
                    return null;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public StoneStackDailyRecordInfo[] GetAllStoneStackDailyRecordInfo(string token)
        {
            if (RSAProvider.LoadRSA(token))
            {
                string userName = "";
                try
                {
                    userName = ClientManager.GetClientUserName(token);
                    return DBProvider.StoneStackDBProvider.GetAllStoneStackDailyRecordInfo();
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("玩家[" + userName + "] GetAllStoneStackDailyRecordInfo Exception", exc);
                    return null;
                }
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
