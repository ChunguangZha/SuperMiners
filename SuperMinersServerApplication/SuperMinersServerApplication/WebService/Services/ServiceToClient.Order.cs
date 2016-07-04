using DataBaseProvider;
using MetaData;
using MetaData.Trade;
using MetaData.User;
using SuperMinersServerApplication.Controller;
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
            if (RSAProvider.LoadRSA(token))
            {
                if (ClientManager.GetClientUserName(token) != userName)
                {
                    return -1;
                }
                if (sellStonesCount <= 0)
                {
                    return -3;
                }

                SellStonesOrder order = null;
                CustomerMySqlTransaction trans = MyDBHelper.Instance.CreateTrans();
                try
                {
                    order = OrderController.Instance.CreateSellOrder(userName, sellStonesCount);
                    if (order.ValueRMB <= 0)
                    {
                        return -2;
                    }

                    int result = PlayerController.Instance.SellStones(order, trans);
                    if (result != 0)
                    {
                        trans.Rollback();
                        return result;
                    }
                    OrderController.Instance.AddSellOrder(order, trans);
                    trans.Commit();
                    PlayerActionController.Instance.AddLog(userName, MetaData.ActionLog.ActionType.SellStone, sellStonesCount);

                    return result;
                }
                catch (Exception exc)
                {
                    string errMessage = "Add Sell Order Exception: ";
                    if (order == null)
                    {
                        errMessage += " (User Error) UserName:" + userName;
                    }
                    else
                    {
                        errMessage += " (Order Error) Order:" + order.ToString();
                    }

                    try
                    {
                        trans.Rollback();
                        PlayerController.Instance.RollbackUserFromDB(userName);
                        if (order != null)
                        {
                            OrderController.Instance.ClearSellStonesOrder(order);
                        }
                        LogHelper.Instance.AddErrorLog(errMessage, exc);
                    }
                    catch (Exception ee)
                    {
                        LogHelper.Instance.AddErrorLog("Add Sell Order Rollback Exception: " + errMessage, ee);
                    }

                    return -3;
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

        public LockSellStonesOrder GetNotFinishedStonesOrder(string token)
        {
            if (RSAProvider.LoadRSA(token))
            {
                string userName = ClientManager.GetClientUserName(token);
                return OrderController.Instance.GetLockedOrderByUserName(userName);
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
            if (RSAProvider.LoadRSA(token))
            {
                if (ClientManager.GetClientUserName(token) != userName)
                {
                    return null;
                }

                if (OrderController.Instance.CheckUserHasNotPayOrder(userName))
                {
                    return null;
                }

                return OrderController.Instance.AutoMatchLockSellStone(userName, buyStonesCount);
            }
            else
            {
                throw new Exception();
            }
        }

        public bool CheckUserHasNotPayOrder(string token)
        {
            if (RSAProvider.LoadRSA(token))
            {
                string userName = ClientManager.GetClientUserName(token);
                return OrderController.Instance.CheckUserHasNotPayOrder(userName);
            }
            else
            {
                throw new Exception();
            }
        }

        public bool ReleaseLockOrder(string token, string orderNumber)
        {
            if (RSAProvider.LoadRSA(token))
            {
                string userName = ClientManager.GetClientUserName(token);
                return OrderController.Instance.ReleaseLockSellOrder(orderNumber);
            }
            else
            {
                throw new Exception();
            }
        }

        public bool PayOrderByRMB(string token, string orderNumber, float rmb)
        {
            if (RSAProvider.LoadRSA(token))
            {
                string userName = ClientManager.GetClientUserName(token);
                if (!string.IsNullOrEmpty(userName))
                {
                    return false;
                }

                PlayerInfo player = PlayerController.Instance.GetOnlinePlayerInfo(userName);
                if (player == null)
                {
                    player = DBProvider.UserDBProvider.GetPlayer(userName);
                }
                if (player == null)
                {
                    return false;
                }

                return OrderController.Instance.PayStoneTrade(player, orderNumber, true, rmb);
            }
            else
            {
                throw new Exception();
            }
        }

        ///// <summary>
        ///// 返回生成的网址
        ///// </summary>
        ///// <param name="token"></param>
        ///// <param name="orderNumber"></param>
        ///// <returns></returns>
        //public string PayOrderByAlipay(string token, string orderNumber, float rmb)
        //{
        //    if (RSAProvider.LoadRSA(token))
        //    {
        //        string userName = ClientManager.GetClientUserName(token);
        //        float money = rmb / GlobalConfig.GameConfig.Yuan_RMB;
        //        float money_1 = (float)Math.Round(money, 1);
        //        if (money_1 < money)//说明刚才是四舍了，要把他加回来
        //        {
        //            money_1 += 0.1f;
        //        }

        //        //取到角级
        //        string suburl = @"Alipay/AlipayDefault.aspx?on=" + orderNumber + "&sn=矿石&mn=" + money_1.ToString();
        //        return suburl;
        //    }
        //    else
        //    {
        //        throw new Exception();
        //    }
        //}

        #endregion

    }
}
