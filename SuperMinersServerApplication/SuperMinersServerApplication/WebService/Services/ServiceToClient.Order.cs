using DataBaseProvider;
using MetaData;
using MetaData.Trade;
using MetaData.User;
using SuperMinersServerApplication.Controller;
using SuperMinersServerApplication.Controller.Trade;
using SuperMinersServerApplication.Encoder;
using SuperMinersServerApplication.Utility;
using SuperMinersServerApplication.WebService.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.WebService.Services
{
    public partial class ServiceToClient : IServiceToClient
    {
        #region IServiceToClient Members
        
        /// <summary>
        /// 0表示成功；-1表示查询不到该用户; -2表示该用户不在线；-3表示异常；1表示本次出售的矿石数超出可出售的矿石数；2表示本次出售的矿石不足支付最低手续费；
        /// </summary>
        /// <param name="token"></param>
        /// <param name="userName"></param>
        /// <param name="sellStonesCount"></param>
        /// <returns></returns>
        public int SellStone(string token, string userName, int sellStonesCount)
        {
#if Delay

            Thread.Sleep(5000);

#endif

            if (RSAProvider.LoadRSA(token))
            {
                if (ClientManager.GetClientUserName(token) != userName)
                {
                    return OperResult.RESULTCODE_USER_NOT_EXIST;
                }
                if (sellStonesCount <= 0)
                {
                    return OperResult.RESULTCODE_EXCEPTION;
                }

                SellStonesOrder order = null;
                CustomerMySqlTransaction trans = MyDBHelper.Instance.CreateTrans();
                try
                {
                    order = OrderController.Instance.StoneOrderController.CreateSellOrder(userName, sellStonesCount);
                    if (order.ValueRMB <= 0)
                    {
                        return OperResult.RESULTCODE_USER_OFFLINE;
                    }

                    int result = PlayerController.Instance.SellStones(order, trans);
                    if (result != OperResult.RESULTCODE_TRUE)
                    {
                        trans.Rollback();
                        return result;
                    }
                    OrderController.Instance.StoneOrderController.AddSellOrder(order, trans);
                    trans.Commit();
                    PlayerActionController.Instance.AddLog(userName, MetaData.ActionLog.ActionType.SellStone, sellStonesCount);

                    return result;
                }
                catch (Exception exc)
                {
                    string errMessage = "玩家提交矿石销量订单异常。";
                    if (order == null)
                    {
                        errMessage += " 玩家信息:" + userName;
                    }
                    else
                    {
                        errMessage += " 订单信息:" + order.ToString();
                    }

                    try
                    {
                        trans.Rollback();
                        PlayerController.Instance.RollbackUserFromDB(userName);
                        if (order != null)
                        {
                            OrderController.Instance.StoneOrderController.ClearSellStonesOrder(order);
                        }
                        LogHelper.Instance.AddErrorLog(errMessage, exc);
                    }
                    catch (Exception ee)
                    {
                        LogHelper.Instance.AddErrorLog("A玩家提交矿石销量订单异常。 回滚异常: " + errMessage, ee);
                    }

                    return OperResult.RESULTCODE_EXCEPTION;
                }
                finally
                {
                    if (trans != null)
                    {
                        trans.Dispose();
                    }
                }
            }
            else
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// RESULTCODE_ORDER_NOT_EXIST; RESULTCODE_USER_NOT_EXIST; RESULTCODE_EXCEPTION; RESULTCODE_ORDER_NOT_EXIST; RESULTCODE_ORDER_NOT_BELONE_CURRENT_PLAYER; RESULTCODE_ORDER_BE_LOCKED; RESULTCODE_TRUE; RESULTCODE_FALSE
        /// </summary>
        /// <param name="token"></param>
        /// <param name="userName"></param>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public int CancelSellStone(string token, string userName, string orderNumber)
        {
#if Delay

            Thread.Sleep(5000);

#endif

            if (RSAProvider.LoadRSA(token))
            {
                if (string.IsNullOrEmpty(orderNumber))
                {
                    return OperResult.RESULTCODE_ORDER_NOT_EXIST;
                }
                if (ClientManager.GetClientUserName(token) != userName)
                {
                    return OperResult.RESULTCODE_USER_NOT_EXIST;
                }

                try
                {
                    return OrderController.Instance.StoneOrderController.CancelSellOrder(userName, orderNumber);
                }
                catch (Exception exc)
                {
                    string errMessage = "玩家: " + userName + " 取消矿石出售订单: " + orderNumber + " 异常.";
                    LogHelper.Instance.AddErrorLog(errMessage, exc);
                    return OperResult.RESULTCODE_EXCEPTION;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public LockSellStonesOrder GetOrderLockedBySelf(string token)
        {
#if Delay

            Thread.Sleep(5000);

#endif

            if (RSAProvider.LoadRSA(token))
            {
                string userName = "";
                try
                {
                    userName = ClientManager.GetClientUserName(token);
                    return OrderController.Instance.StoneOrderController.GetLockedOrderByUserName(userName);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("玩家[" + userName + "] 获取自己锁定订单异常", exc);
                    return null;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public SellStonesOrder[] GetAllNotFinishedSellOrders(string token)
        {
#if Delay

            Thread.Sleep(5000);

#endif

            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    return OrderController.Instance.StoneOrderController.GetSellOrders();
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("获取所有未完成的矿石订单异常", exc);
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
        /// <param name="userName"></param>
        /// <param name="buyStonesCount"></param>
        /// <returns></returns>
        public LockSellStonesOrder AutoMatchLockSellStone(string token, string userName, int buyStonesCount)
        {
#if Delay

            Thread.Sleep(5000);

#endif

            if (RSAProvider.LoadRSA(token))
            {
                if (ClientManager.GetClientUserName(token) != userName)
                {
                    return null;
                }

                if (OrderController.Instance.StoneOrderController.CheckUserHasNotPayOrder(userName))
                {
                    return null;
                }

                return OrderController.Instance.StoneOrderController.AutoMatchLockSellStone(userName, buyStonesCount);
            }
            else
            {
                throw new Exception();
            }
        }

        public LockSellStonesOrder LockSellStone(string token, string userName, string orderNumber)
        {
#if Delay

            Thread.Sleep(5000);

#endif

            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    if (ClientManager.GetClientUserName(token) != userName)
                    {
                        return null;
                    }

                    if (OrderController.Instance.StoneOrderController.CheckUserHasNotPayOrder(userName))
                    {
                        return null;
                    }

                    return OrderController.Instance.StoneOrderController.LockSellStone(userName, orderNumber);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("玩家[" + userName + "] 锁定矿石订单异常", exc);
                    return null;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public bool CheckUserHasNotPayOrder(string token)
        {
#if Delay

            Thread.Sleep(5000);

#endif

            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    string userName = ClientManager.GetClientUserName(token);
                    return OrderController.Instance.StoneOrderController.CheckUserHasNotPayOrder(userName);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("检查玩家是否存在未支付的订单异常", exc);
                    return true;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public bool ReleaseLockOrder(string token, string orderNumber)
        {
#if Delay

            Thread.Sleep(5000);

#endif

            if (RSAProvider.LoadRSA(token))
            {
                string userName = "";
                try
                {
                    userName = ClientManager.GetClientUserName(token);
                    return OrderController.Instance.StoneOrderController.ReleaseLockSellOrder(orderNumber);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("玩家[" + userName + "] 解除锁定订单" + orderNumber + "，异常", exc);
                    return false;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// 此处只需处理RMB支付。Alipay支付的情况，在锁定订单时已经将支付链接返回，客户端可直接链接支付。
        /// </summary>
        /// <param name="token"></param>
        /// <param name="orderNumber"></param>
        /// <param name="rmb"></param>
        /// <returns></returns>
        public int PayStoneOrderByRMB(string token, string orderNumber, decimal rmb)
        {
#if Delay

            Thread.Sleep(5000);

#endif

            if (RSAProvider.LoadRSA(token))
            {
                string userName = "";
                try
                {
                    userName = ClientManager.GetClientUserName(token);
                    if (string.IsNullOrEmpty(userName))
                    {
                        return OperResult.RESULTCODE_USER_NOT_EXIST;
                    }

                    PlayerInfo player = PlayerController.Instance.GetPlayerInfo(userName);
                    if (player == null)
                    {
                        return OperResult.RESULTCODE_USER_NOT_EXIST;
                    }
                    if (player.FortuneInfo.RMB < rmb)
                    {
                        return OperResult.RESULTCODE_LACK_OF_BALANCE;
                    }

                    return OrderController.Instance.StoneOrderController.PayStoneOrderByRMB(player.SimpleInfo.UserName, orderNumber, rmb);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("玩家[" + userName + "] 灵币支付矿石订单" + orderNumber + "，异常", exc);
                    return OperResult.RESULTCODE_EXCEPTION;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// RESULTCODE_USER_NOT_EXIST; RESULTCODE_ORDER_NOT_EXIST; RESULTCODE_EXCEPTION; RESULTCODE_ORDER_NOT_BE_LOCKED; RESULTCODE_ORDER_NOT_BELONE_CURRENT_PLAYER; RESULTCODE_TRUE; RESULTCODE_FALSE;
        /// </summary>
        /// <param name="token"></param>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public int SetStoneOrderPayException(string token, string orderNumber)
        {
#if Delay

            Thread.Sleep(5000);

#endif

            if (RSAProvider.LoadRSA(token))
            {
                string userName = "";
                try
                {
                    userName = ClientManager.GetClientUserName(token);
                    if (string.IsNullOrEmpty(userName))
                    {
                        return OperResult.RESULTCODE_USER_NOT_EXIST;
                    }

                    return OrderController.Instance.StoneOrderController.SetStoneOrderPayException(userName, orderNumber);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("玩家[" + userName + "] 申诉矿石订单" + orderNumber + "，异常", exc);
                    return OperResult.RESULTCODE_EXCEPTION;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public SellStonesOrder[] SearchUserSellStoneOrders(string token, string userName, MyDateTime myBeginTime, MyDateTime myEndTime)
        {
#if Delay

            Thread.Sleep(5000);

#endif

            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    if (ClientManager.GetClientUserName(token) != userName)
                    {
                        return null;
                    }

                    return DBProvider.StoneOrderDBProvider.GetSellOrderList(userName, "", 0, myBeginTime, myEndTime, 0, 0);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("玩家[" + userName + "] 查询矿石出售历史订单异常", exc);
                    return null;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public BuyStonesOrder[] SearchUserBuyStoneOrders(string token, string userName, MyDateTime myBeginTime, MyDateTime myEndTime)
        {
#if Delay

            Thread.Sleep(5000);

#endif

            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    if (ClientManager.GetClientUserName(token) != userName)
                    {
                        return null;
                    }

                    return DBProvider.StoneOrderDBProvider.GetBuyStonesOrderList("", "", userName, 0, myBeginTime, myEndTime, null, null, 1000, 0);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("玩家[" + userName + "] 查询矿石购买历史订单异常", exc);
                    return null;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        #endregion
    }
}
