using DataBaseProvider;
using MetaData;
using MetaData.Trade;
using MetaData.User;
using SuperMinersServerApplication.UIModel;
using SuperMinersServerApplication.Utility;
using SuperMinersServerApplication.WebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.Controller
{
    public class OrderController
    {
        #region Single Stone

        private static OrderController _instance = new OrderController();

        public static OrderController Instance
        {
            get { return _instance; }
        }

        private OrderController()
        {

        }

        #endregion

        object _lockListSellOrders = new object();
        /// <summary>
        /// Key为订单号
        /// </summary>
        private Dictionary<string, OrderRunnable> dicSellOrders = new Dictionary<string, OrderRunnable>();

        private List<BuyStonesOrder> listBuyStonesOrderLast20 = new List<BuyStonesOrder>();

        public bool Init()
        {
            try
            {
                dicSellOrders.Clear();
                DateTime endTime = DateTime.Now;
                DateTime beginTime = endTime.AddDays(-7);
                var waitOrderDBObjects = DBProvider.OrderDBProvider.GetSellOrderList(new int[] { (int)SellOrderState.Wait }, "", beginTime, endTime);
                foreach (var item in waitOrderDBObjects)
                {
                    var runnable = new OrderRunnable(item);
                    dicSellOrders.Add(item.OrderNumber, new OrderRunnable(item));
                }

                var lockedOrderDBObjects = DBProvider.OrderDBProvider.GetLockSellStonesOrderList("");
                foreach (var item in lockedOrderDBObjects)
                {
                    TimeSpan span = DateTime.Now - item.LockedTime;
                    if (span.TotalSeconds > GlobalConfig.GameConfig.BuyOrderLockTimeMinutes * 60)
                    {
                        var runnable = new OrderRunnable(item);
                        runnable.ReleaseLock();
                        //解除锁定后，继续加到集合中
                        dicSellOrders.Add(item.StonesOrder.OrderNumber, runnable);
                    }
                    else
                    {
                        item.OrderLockedTimeSpan = (int)span.TotalSeconds;
                        var runnable = new OrderRunnable(item);
                        dicSellOrders.Add(item.StonesOrder.OrderNumber, runnable);
                    }
                }

                beginTime = endTime.AddDays(-1);
                var buyOrderRecords = DBProvider.OrderDBProvider.GetBuyStonesOrderList("", beginTime, endTime);
                if (buyOrderRecords == null)
                {
                    this.listBuyStonesOrderLast20 = new List<BuyStonesOrder>();
                }
                else
                {
                    this.listBuyStonesOrderLast20 = new List<BuyStonesOrder>(buyOrderRecords);
                }
                return true;
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("Init OrderController Error", exc);
                return false;
            }
        }

        /// <summary>
        /// 检查该玩家是否存在未支付的订单
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool CheckUserHasNotPayOrder(string userName)
        {
            lock (_lockListSellOrders)
            {
                foreach (var item in dicSellOrders.Values)
                {
                    if (item.CheckBuyerName(userName))
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public LockSellStonesOrder AutoMatchLockSellStone(string userName, int stoneCount)
        {
            lock (_lockListSellOrders)
            {
                OrderRunnable runnable = null;
                foreach (var item in dicSellOrders.Values)
                {
                    if (item.OrderState == SellOrderState.Wait && item.StoneCount <= stoneCount)
                    {
                        if (runnable == null)
                        {
                            runnable = item;
                        }
                        else
                        {
                            if (item.StoneCount > runnable.StoneCount)
                            {
                                runnable = item;
                            }
                        }

                        if (runnable != null && runnable.StoneCount == stoneCount)
                        {
                            break;
                        }
                    }
                }

                if (runnable == null)
                {
                    return null;
                }

                return runnable.Lock(userName);
            }
        }

        public LockSellStonesOrder GetLockedOrderByUserName(string userName)
        {
            lock (_lockListSellOrders)
            {
                LockSellStonesOrder order = null;
                foreach (var item in dicSellOrders.Values)
                {
                    if (item.CheckBuyerName(userName))
                    {
                        if (!item.CheckOrderLockedIsTimeOut())
                        {
                            order = item.GetLockedOrder();
                        }
                    }
                }

                return order;
            }
        }

        public SellStonesOrder[] GetSellOrders()
        {
            lock (_lockListSellOrders)
            {
                List<SellStonesOrder> orders = new List<SellStonesOrder>();
                foreach (var item in dicSellOrders.Values)
                {
                    if (item.OrderState != SellOrderState.Finish)
                    {
                        orders.Add(item.GetSellOrder());
                    }
                }

                return orders.ToArray();
            }
        }

        private float GetExpense(float valueRMB)
        {
            float expense = valueRMB * GlobalConfig.GameConfig.ExchangeExpensePercent / 100;
            if (expense < GlobalConfig.GameConfig.ExchangeExpenseMinNumber)
            {
                expense = GlobalConfig.GameConfig.ExchangeExpenseMinNumber;
            }
            return expense;
        }

        private string CreateOrderNumber(string userName, DateTime time)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(time.Year.ToString("0000"));
            builder.Append(time.Month.ToString("00"));
            builder.Append(time.Day.ToString("00"));
            builder.Append(time.Hour.ToString("00"));
            builder.Append(time.Minute.ToString("00"));
            builder.Append(time.Second.ToString("00"));
            builder.Append(time.Millisecond.ToString("0000"));
            builder.Append((int)TradeType.StoneTrade);//表示此为矿石交易，对应还有矿山交易等
            builder.Append(Math.Abs(userName.GetHashCode()));
            builder.Append((new Random()).Next(1000, 9999));
            return builder.ToString();
        }

        public int GetTradeType(string orderNumber)
        {
            if (orderNumber.Length < 20)
            {
                return -1;
            }

            string typestring = orderNumber.Substring(18, 2);
            int typeResult = -1;
            int.TryParse(typestring, out typeResult);
            return typeResult;
        }

        public void ClearSellStonesOrder(SellStonesOrder order)
        {
            lock (this._lockListSellOrders)
            {
                dicSellOrders.Remove(order.OrderNumber);
            }
        }

        /// <summary>
        /// 如果事务提交失败，则需调用ClearSellStonesOrder方法从集合中清除该方法返回的订单
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="sellStonesCount"></param>
        /// <param name="myTrans"></param>
        /// <returns></returns>
        public SellStonesOrder CreateSellOrder(string userName, int sellStonesCount)
        {
            float valueRMB = sellStonesCount / GlobalConfig.GameConfig.Stones_RMB;
            DateTime time = DateTime.Now;
            SellStonesOrder order = new SellStonesOrder()
            {
                OrderNumber = CreateOrderNumber(userName, time),
                SellStonesCount = sellStonesCount,
                OrderState = SellOrderState.Wait,
                SellerUserName = userName,
                ValueRMB = valueRMB,
                Expense = GetExpense(valueRMB),
                SellTime = time,
            };
            
            return order;
        }

        public void AddSellOrder(SellStonesOrder order, CustomerMySqlTransaction myTrans)
        {
            lock (this._lockListSellOrders)
            {
                DBProvider.OrderDBProvider.AddSellOrder(order, myTrans);
                dicSellOrders.Add(order.OrderNumber, new OrderRunnable(order));
            }
        }

        //public bool LockSellOrder(string sellOrderNumber, string buyerUserName)
        //{
        //    OrderRunnable order = null;
        //    lock (this._lockListSellOrders)
        //    {
        //        this.dicSellOrders.TryGetValue(sellOrderNumber, out order);
        //    }

        //    if (order == null)
        //    {
        //        return false;
        //    }

        //    return order.Lock(buyerUserName);
        //}

        public bool ReleaseLockSellOrder(string orderNumber)
        {
            OrderRunnable order = null;
            lock (this._lockListSellOrders)
            {
                this.dicSellOrders.TryGetValue(orderNumber, out order);
            }

            if (order == null)
            {
                return false;
            }

            return order.ReleaseLock();
        }

        public BuyStonesOrder Pay(string orderNumber, string buyerUserName, float rmb, CustomerMySqlTransaction trans)
        {
            OrderRunnable runnable = null;
            lock (this._lockListSellOrders)
            {
                if (this.dicSellOrders.ContainsKey(orderNumber))
                {
                    runnable = this.dicSellOrders[orderNumber];
                }
            }
            
            if (runnable == null)
            {
                LogHelper.Instance.AddInfoLog("支付订单时错误，没有找到订单。 orderNumber: " + orderNumber + "。 buyerUserName: " + buyerUserName + "。 rmb: " + rmb.ToString());
                return null;
            }
            if (!runnable.CheckBuyerName(buyerUserName))
            {
                LogHelper.Instance.AddInfoLog("支付订单时错误，此订单不是被当前玩家锁定。 orderNumber: " + orderNumber + "。 buyerUserName: " + buyerUserName + "。 rmb: " + rmb.ToString());
                return null;
            }
            if (rmb < runnable.ValueRMB)
            {
                LogHelper.Instance.AddInfoLog("支付订单时错误，玩家支付的灵币不足，无法完成订单。 orderNumber: " + orderNumber + "。 buyerUserName: " + buyerUserName + "。 rmb: " + rmb.ToString());
                return null;
            }

            return runnable.Pay(rmb, trans);
        }

        private OrderRunnable FindRunnableByBuyUserName(string buyerUserName)
        {
            lock (this._lockListSellOrders)
            {
                foreach (var item in this.dicSellOrders.Values)
                {
                    if (item.CheckBuyerName(buyerUserName))
                    {
                        return item;
                    }
                }

                return null;
            }
        }

        public bool PayStoneTrade(PlayerInfo player, string orderNumber, bool rmbPay, float rmb)
        {
            var trans = MyDBHelper.Instance.CreateTrans();
            try
            {
                var buyOrder = this.Pay(orderNumber, player.SimpleInfo.UserName, rmb, trans);
                if (buyOrder == null)
                {
                    trans.Rollback();
                    return false;
                }

                bool isOK = PlayerController.Instance.PayStoneOrder(player, buyOrder, rmbPay, trans);
                if (!isOK)
                {
                    trans.Rollback();
                    return false;
                }

                trans.Commit();

                this.dicSellOrders.Remove(buyOrder.StonesOrder.OrderNumber);

                PlayerActionController.Instance.AddLog(buyOrder.BuyerUserName, MetaData.ActionLog.ActionType.BuyStone, buyOrder.StonesOrder.SellStonesCount);

                string tokenBuyer = ClientManager.GetToken(player.SimpleInfo.UserName);
                if (!string.IsNullOrEmpty(tokenBuyer))
                {
                    if (StoneOrderPaySucceedNotifyBuyer != null)
                    {
                        StoneOrderPaySucceedNotifyBuyer(tokenBuyer, orderNumber);
                    }
                }
                string tokenSeller = ClientManager.GetToken(buyOrder.StonesOrder.SellerUserName);
                if (!string.IsNullOrEmpty(tokenSeller))
                {
                    if (StoneOrderPaySucceedNotifySeller != null)
                    {
                        StoneOrderPaySucceedNotifySeller(tokenSeller, orderNumber);
                    }
                }

                return true;
            }
            catch (Exception exc)
            {
                trans.Rollback();
                LogHelper.Instance.AddErrorLog("PayStoneTrade Exception. OrderNumber: " + orderNumber, exc);
                return false;
            }
            finally
            {
                if (trans != null)
                {
                    trans.Dispose();
                }
            }
        }

        //public void PayStoneTradeByAlipay(PlayerInfo player, string orderNumber, float rmb)
        //{
        //    var trans = MyDBHelper.Instance.CreateTrans();
        //    try
        //    {
        //        var buyOrder = this.Pay(orderNumber, player.SimpleInfo.UserName, rmb, trans);
        //        if (buyOrder == null)
        //        {
        //            trans.Rollback();
        //            return;
        //        }
        //        //从Web页面回来的，肯定是支付宝支付
        //        bool isOK = PlayerController.Instance.PayStoneOrder(player, buyOrder, false, trans);
        //        if (!isOK)
        //        {
        //            trans.Rollback();
        //            return;
        //        }

        //        trans.Commit();
        //        PlayerActionController.Instance.AddLog(buyOrder.BuyerUserName, MetaData.ActionLog.ActionType.BuyStone, buyOrder.StonesOrder.SellStonesCount);

        //    }
        //    catch (Exception exc)
        //    {
        //        trans.Rollback();
        //        LogHelper.Instance.AddErrorLog("PayStoneTradeByAlipay Exception. OrderNumber: " + orderNumber, exc);
        //    }
        //    finally
        //    {
        //        if (trans != null)
        //        {
        //            trans.Dispose();
        //        }
        //    }
        //}

        /// <summary>
        /// p1: token;  p2: orderNumber
        /// </summary>
        public event Action<string, string> StoneOrderPaySucceedNotifyBuyer;
        /// <summary>
        /// p1: token;  p2: orderNumber
        /// </summary>
        public event Action<string, string> StoneOrderPaySucceedNotifySeller;
    }
}
