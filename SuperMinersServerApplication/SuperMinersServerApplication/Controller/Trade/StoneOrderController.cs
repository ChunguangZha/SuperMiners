using DataBaseProvider;
using MetaData;
using MetaData.Trade;
using MetaData.User;
using SuperMinersServerApplication.Controller.Trade;
using SuperMinersServerApplication.UIModel;
using SuperMinersServerApplication.Utility;
using SuperMinersServerApplication.WebService;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.Controller
{
    public class StoneOrderController
    {
        object _lockListSellOrders = new object();
        /// <summary>
        /// Key为订单号
        /// </summary>
        private ConcurrentDictionary<string, StoneOrderRunnable> dicSellOrders = new ConcurrentDictionary<string, StoneOrderRunnable>();

        private List<BuyStonesOrder> listBuyStonesOrderLast20 = new List<BuyStonesOrder>();

        System.Timers.Timer _timer = new System.Timers.Timer(10000);

        public void StartThread()
        {
            _timer.Elapsed += CheckOrderLockTimeoutTimer_Elapsed;
        }

        void CheckOrderLockTimeoutTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                foreach (var item in this.dicSellOrders.Values)
                {
                    item.CheckOrderLockedTimeOut();
                }
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("Check All Order List Lock Timeout Exception.", exc);
            }
        }

        public bool Init()
        {
            try
            {
                dicSellOrders.Clear();
                var waitOrderDBObjects = DBProvider.StoneOrderDBProvider.GetSellOrderList(new int[] { (int)SellOrderState.Wait }, "", null, null);
                foreach (var item in waitOrderDBObjects)
                {
                    var runnable = new StoneOrderRunnable(item);
                    dicSellOrders[item.OrderNumber] = new StoneOrderRunnable(item);
                }

                var lockedOrderDBObjects = DBProvider.StoneOrderDBProvider.GetLockSellStonesOrderList("");
                foreach (var item in lockedOrderDBObjects)
                {
                    TimeSpan span = DateTime.Now - item.LockedTime;
                    if (span.TotalSeconds > GlobalConfig.GameConfig.BuyOrderLockTimeMinutes * 60)
                    {
                        var runnable = new StoneOrderRunnable(item);
                        runnable.ReleaseLock();
                        //解除锁定后，继续加到集合中
                        dicSellOrders[item.StonesOrder.OrderNumber] = runnable;
                    }
                    else
                    {
                        item.OrderLockedTimeSpan = (int)span.TotalSeconds;
                        var runnable = new StoneOrderRunnable(item);
                        dicSellOrders[item.StonesOrder.OrderNumber] = runnable;
                    }
                }

                MyDateTime endTime = MyDateTime.FromDateTime(DateTime.Now);
                MyDateTime beginTime = MyDateTime.FromDateTime(DateTime.Now.AddDays(-1));
                var buyOrderRecords = DBProvider.StoneOrderDBProvider.GetBuyStonesOrderList("", beginTime, endTime);
                if (buyOrderRecords == null)
                {
                    this.listBuyStonesOrderLast20 = new List<BuyStonesOrder>();
                }
                else
                {
                    this.listBuyStonesOrderLast20 = new List<BuyStonesOrder>(buyOrderRecords);
                }


                StartThread();
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
                StoneOrderRunnable runnable = null;
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

        public LockSellStonesOrder LockSellStone(string userName, string orderNumber)
        {
            lock (_lockListSellOrders)
            {
                StoneOrderRunnable runnable = null;
                if (dicSellOrders.TryGetValue(orderNumber, out runnable))
                {
                    if (runnable.OrderState != SellOrderState.Wait)
                    {
                        return null;
                    }
                    return runnable.Lock(userName);
                }

                return null;
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
                        if (!item.CheckOrderLockedTimeOut())
                        {
                            order = item.GetLockedOrder();
                        }
                    }
                }

                return order;
            }
        }

        internal StoneOrderRunnable GetLockedOrderByOrderNumber(string orderNumber)
        {
            lock (_lockListSellOrders)
            {
                foreach (var item in dicSellOrders.Values)
                {
                    if (item.OrderNumber == orderNumber)
                    {
                        return item;
                    }
                }

                return null;
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

        private decimal GetExpense(decimal valueRMB)
        {
            decimal expense = valueRMB * GlobalConfig.GameConfig.ExchangeExpensePercent / 100;
            if (expense < GlobalConfig.GameConfig.ExchangeExpenseMinNumber)
            {
                expense = GlobalConfig.GameConfig.ExchangeExpenseMinNumber;
            }
            return expense;
        }

        public void ClearSellStonesOrder(SellStonesOrder order)
        {
            lock (this._lockListSellOrders)
            {
                StoneOrderRunnable runnable = null;
                dicSellOrders.TryRemove(order.OrderNumber, out runnable);
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
            decimal valueRMB = sellStonesCount / GlobalConfig.GameConfig.Stones_RMB;
            DateTime time = DateTime.Now;
            SellStonesOrder order = new SellStonesOrder()
            {
                OrderNumber = OrderController.Instance.CreateOrderNumber(userName, time, AlipayTradeInType.BuyStone),
                SellStonesCount = sellStonesCount,
                OrderState = SellOrderState.Wait,
                SellerUserName = userName,
                ValueRMB = valueRMB,
                Expense = GetExpense(valueRMB),
                SellTime = time,
            };
            
            return order;
        }

        /// <summary>
        /// RESULTCODE_ORDER_NOT_EXIST; RESULTCODE_ORDER_NOT_BELONE_CURRENT_PLAYER; RESULTCODE_ORDER_BE_LOCKED; RESULTCODE_TRUE; RESULTCODE_FALSE
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public int CancelSellOrder(string sellUserName, string orderNumber)
        {
            lock (this._lockListSellOrders)
            {
                StoneOrderRunnable runnable = null;
                this.dicSellOrders.TryGetValue(orderNumber, out runnable);
                if (runnable == null)
                {
                    return OperResult.RESULTCODE_ORDER_NOT_EXIST;
                }
                SellStonesOrder order = runnable.GetSellOrder();
                if (order.SellerUserName != sellUserName)
                {
                    return OperResult.RESULTCODE_ORDER_NOT_BELONE_CURRENT_PLAYER;
                }
                if (order.OrderState != SellOrderState.Wait)
                {
                    return OperResult.RESULTCODE_ORDER_BE_LOCKED;
                }

                if (DBProvider.StoneOrderDBProvider.CancelSellOrder(order))
                {
                    return OperResult.RESULTCODE_TRUE;
                }

                return OperResult.RESULTCODE_FALSE;
            }
        }

        public void AddSellOrder(SellStonesOrder order, CustomerMySqlTransaction myTrans)
        {
            lock (this._lockListSellOrders)
            {
                DBProvider.StoneOrderDBProvider.AddSellOrder(order, myTrans);
                dicSellOrders[order.OrderNumber] = new StoneOrderRunnable(order);
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

        /// <summary>
        /// 由客户端检查超时。
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public bool ReleaseLockSellOrder(string orderNumber)
        {
            StoneOrderRunnable order = null;
            lock (this._lockListSellOrders)
            {
                this.dicSellOrders.TryGetValue(orderNumber, out order);
            }

            if (order == null)
            {
                return false;
            }

            bool isOK = order.ReleaseLock();

            return isOK;
        }

        /// <summary>
        /// 此方法只是订单处理，不关心支付方式
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <param name="buyerUserName"></param>
        /// <param name="rmb"></param>
        /// <param name="trans"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public BuyStonesOrder Pay(string orderNumber, string buyerUserName, decimal rmb, CustomerMySqlTransaction trans, ref int result)
        {
            StoneOrderRunnable runnable = FindOrderByOrderName(orderNumber);
            if (runnable == null)
            {
                result = OperResult.RESULTCODE_ORDER_NOT_EXIST;
                LogHelper.Instance.AddInfoLog("支付订单时错误，没有找到订单。 orderNumber: " + orderNumber + "。 buyerUserName: " + buyerUserName + "。 rmb: " + rmb.ToString());
                return null;
            }
            if (!runnable.CheckBuyerName(buyerUserName))
            {
                result = OperResult.RESULTCODE_ORDER_NOT_BELONE_CURRENT_PLAYER;
                LogHelper.Instance.AddInfoLog("支付订单时错误，此订单不是被当前玩家锁定。 orderNumber: " + orderNumber + "。 buyerUserName: " + buyerUserName + "。 rmb: " + rmb.ToString());
                return null;
            }
            if (rmb < runnable.ValueRMB)
            {
                result = OperResult.RESULTCODE_PARAM_INVALID;
                LogHelper.Instance.AddInfoLog("支付订单时错误，玩家支付的灵币不足，无法完成订单。 orderNumber: " + orderNumber + "。 buyerUserName: " + buyerUserName + "。 rmb: " + rmb.ToString());
                return null;
            }

            return runnable.Pay(trans);
        }

        private StoneOrderRunnable FindOrderByOrderName(string orderNumber)
        {
            StoneOrderRunnable runnable = null;
            lock (this._lockListSellOrders)
            {
                if (this.dicSellOrders.ContainsKey(orderNumber))
                {
                    runnable = this.dicSellOrders[orderNumber];
                }
            }

            return runnable;
        }

        private StoneOrderRunnable FindRunnableByBuyUserName(string buyerUserName)
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

        /// <summary>
        /// 此处只需处理RMB支付。Alipay支付的情况，在锁定订单时已经将支付链接返回，客户端可直接链接支付。
        /// </summary>
        /// <param name="buyer"></param>
        /// <param name="orderNumber"></param>
        /// <param name="rmb"></param>
        /// <returns></returns>
        public int PayStoneOrderByRMB(string buyerUserName, string orderNumber, decimal rmb)
        {
            int result = OperResult.RESULTCODE_FALSE;
            var trans = MyDBHelper.Instance.CreateTrans();
            try
            {
                //订单处理
                var buyOrder = this.Pay(orderNumber, buyerUserName, rmb, trans, ref result);
                if (buyOrder == null)
                {
                    trans.Rollback();
                    return result;
                }

                //更新用户信息
                bool isOK = PlayerController.Instance.PayStoneOrder(false, buyerUserName, buyOrder, trans);
                if (!isOK)
                {
                    trans.Rollback();
                    result = OperResult.RESULTCODE_FALSE;
                    return result;
                }
                this.RemoveRecord(buyOrder.StonesOrder.OrderNumber);

                trans.Commit();

                AddLogNotifyPlayer(buyerUserName, orderNumber, buyOrder);

                result = OperResult.RESULTCODE_TRUE;
                return result;
            }
            catch (Exception exc)
            {
                result = OperResult.RESULTCODE_EXCEPTION;
                trans.Rollback();
                LogHelper.Instance.AddErrorLog("PayStoneTrade Exception. OrderNumber: " + orderNumber, exc);
                return result;
            }
            finally
            {
                if (trans != null)
                {
                    trans.Dispose();
                }
            }
        }

        /// <summary>
        /// RESULTCODE_ORDER_NOT_EXIST; RESULTCODE_EXCEPTION; RESULTCODE_ORDER_NOT_BE_LOCKED; RESULTCODE_ORDER_NOT_BELONE_CURRENT_PLAYER; RESULTCODE_TRUE; RESULTCODE_FALSE;
        /// </summary>
        /// <param name="buyerUserName"></param>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public int SetStoneOrderPayException(string buyerUserName, string orderNumber)
        {
            int result = OperResult.RESULTCODE_FALSE;
            var trans = MyDBHelper.Instance.CreateTrans();
            try
            {
                StoneOrderRunnable runnable = GetLockedOrderByOrderNumber(orderNumber);
                if (runnable == null)
                {
                    return OperResult.RESULTCODE_ORDER_NOT_EXIST;
                }

                return runnable.SetSellOrderPayException(buyerUserName);
            }
            catch (Exception exc)
            {
                result = OperResult.RESULTCODE_EXCEPTION;
                trans.Rollback();
                LogHelper.Instance.AddErrorLog("PayStoneTrade Exception. OrderNumber: " + orderNumber, exc);
                return result;
            }
            finally
            {
                if (trans != null)
                {
                    trans.Dispose();
                }
            }
        }

        private void AddLogNotifyPlayer(string buyerUserName, string orderNumber, BuyStonesOrder buyOrder)
        {
            PlayerActionController.Instance.AddLog(buyOrder.BuyerUserName, MetaData.ActionLog.ActionType.BuyStone, buyOrder.StonesOrder.SellStonesCount);

            string tokenBuyer = ClientManager.GetToken(buyerUserName);
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
        }

        private void RemoveRecord(string orderNumber)
        {
            lock (this._lockListSellOrders)
            {
                StoneOrderRunnable runnable = null;
                this.dicSellOrders.TryRemove(orderNumber, out runnable);
            }
        }

        public bool AlipayCallback(AlipayRechargeRecord alipayRecord, bool saveAlipayRecord)
        {
            StoneOrderRunnable runnable = FindOrderByOrderName(alipayRecord.out_trade_no);
            if (runnable == null)
            {
                LogHelper.Instance.AddInfoLog("支付宝购买矿石回调，找不到订单。支付宝信息：" + alipayRecord.ToString());
                return false;
            }
            if (alipayRecord.value_rmb < runnable.ValueRMB)
            {
                LogHelper.Instance.AddInfoLog("支付宝购买矿石回调，支付宝收款金额小于需要支付金额" + runnable.ValueRMB + "。支付宝信息：" + alipayRecord.ToString());
                return false;
            }

            if (alipayRecord.user_name != runnable.GetLockedOrder().LockedByUserName)
            {
                LogHelper.Instance.AddInfoLog("支付宝购买矿石回调，支付宝回传玩家用户名和锁定订单的用户名[" + runnable.ValueRMB + "] 不匹配。支付宝信息：" + alipayRecord.ToString());
                return false;
            }

            var lockOrder = runnable.GetLockedOrder();

            int result = OperResult.RESULTCODE_FALSE;
            var trans = MyDBHelper.Instance.CreateTrans();
            try
            {
                if (saveAlipayRecord)
                {
                    DBProvider.AlipayRecordDBProvider.SaveAlipayRechargeRecord(alipayRecord, trans);
                }

                //订单处理
                var buyOrder = this.Pay(runnable.OrderNumber, lockOrder.LockedByUserName, lockOrder.StonesOrder.ValueRMB, trans, ref result);
                if (buyOrder == null)
                {
                    trans.Rollback();
                    return false;
                }

                //更新用户信息
                bool isOK = PlayerController.Instance.PayStoneOrder(true, lockOrder.LockedByUserName, buyOrder, trans);
                if (!isOK)
                {
                    trans.Rollback();
                    result = OperResult.RESULTCODE_FALSE;
                    return false;
                }
                this.RemoveRecord(buyOrder.StonesOrder.OrderNumber);

                trans.Commit();

                AddLogNotifyPlayer(lockOrder.LockedByUserName, runnable.OrderNumber, buyOrder);

                return true;
            }
            catch (Exception exc)
            {
                result = OperResult.RESULTCODE_EXCEPTION;
                trans.Rollback();
                LogHelper.Instance.AddErrorLog("PayStoneTrade Exception. OrderNumber: " + runnable.OrderNumber, exc);
                return false;
            }
            finally
            {
                if (trans != null)
                {
                    trans.Dispose();
                }
            }

            return false;
        }

        public int HandleExceptionOrderCancel(string orderNumber)
        {
            StoneOrderRunnable order = null;
            lock (this._lockListSellOrders)
            {
                this.dicSellOrders.TryGetValue(orderNumber, out order);
            }

            if (order == null)
            {
                return OperResult.RESULTCODE_ORDER_NOT_EXIST;
            }

            string tokenBuyer = ClientManager.GetToken(order.GetLockedOrder().LockedByUserName);
            order.SetOrderState(SellOrderState.Wait);
            bool isOK = order.ReleaseLock();
            if (isOK)
            {
                if (!string.IsNullOrEmpty(tokenBuyer) && this.StoneOrderAppealFailed != null)
                {
                    this.StoneOrderAppealFailed(tokenBuyer, order.OrderNumber);
                }
                return OperResult.RESULTCODE_TRUE;
            }

            return OperResult.RESULTCODE_FALSE;
        }

        public int HandleExceptionOrderSucceed(AlipayRechargeRecord alipayRecord)
        {
            if (alipayRecord == null)
            {
                return OperResult.RESULTCODE_PARAM_INVALID;
            }

            StoneOrderRunnable runnable = this.GetLockedOrderByOrderNumber(alipayRecord.out_trade_no);
            if (runnable == null)
            {
                return OperResult.RESULTCODE_ORDER_NOT_EXIST;
            }

            runnable.SetOrderState(SellOrderState.Lock);

            var oldRecord = DBProvider.AlipayRecordDBProvider.GetAlipayRechargeRecordByOrderNumber_OR_Alipay_trade_no(alipayRecord.out_trade_no, alipayRecord.alipay_trade_no);

            if (this.AlipayCallback(alipayRecord, oldRecord == null))
            {
                return OperResult.RESULTCODE_TRUE;
            }

            return OperResult.RESULTCODE_FALSE;
        }

        /// <summary>
        /// p1: token;  p2: orderNumber
        /// </summary>
        public event Action<string, string> StoneOrderPaySucceedNotifyBuyer;
        /// <summary>
        /// p1: token;  p2: orderNumber
        /// </summary>
        public event Action<string, string> StoneOrderPaySucceedNotifySeller;
        /// <summary>
        /// p1: token;  p2: orderNumber
        /// </summary>
        public event Action<string, string> StoneOrderAppealFailed;
    }
}
