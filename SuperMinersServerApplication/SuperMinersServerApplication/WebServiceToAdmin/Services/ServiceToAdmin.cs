using MetaData;
using MetaData.SystemConfig;
using MetaData.Trade;
using MetaData.User;
using SuperMinersServerApplication.Controller;
using SuperMinersServerApplication.Controller.Trade;
using SuperMinersServerApplication.Encoder;
using SuperMinersServerApplication.Model;
using SuperMinersServerApplication.Utility;
using SuperMinersServerApplication.WebService;
using SuperMinersServerApplication.WebServiceToAdmin.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace SuperMinersServerApplication.WebServiceToAdmin.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    public partial class ServiceToAdmin : IServiceToAdmin, IDisposable
    {
        private Dictionary<string, Queue<CallbackInfo>> _callbackDic = new Dictionary<string, Queue<CallbackInfo>>();
        private readonly object _callbackDicLocker = new object();

        private System.Timers.Timer _userStateCheck = new System.Timers.Timer(10000);

        public ServiceToAdmin()
        {
            PlayerController.Instance.SomebodyWithdrawRMB += Instance_SomebodyWithdrawRMB;
        }

        void Instance_SomebodyWithdrawRMB(WithdrawRMBRecord record)
        {
            new Thread(new ParameterizedThreadStart(o =>
            {
                string[] tokens = AdminManager.GetAllOnlineAdministratorTokens();
                if (tokens != null)
                {
                    foreach (var token in tokens)
                    {
                        this.SomebodyWithdrawRMB(token, record);
                    }
                }
            })).Start();

        }

        private void _userStateCheck_Elapsed(object sender, ElapsedEventArgs e)
        {
            this._userStateCheck.Stop();
            try
            {
                string[] tokens = AdminManager.GetInvalidClients();
                if (null != tokens)
                {
                    foreach (var token in tokens)
                    {
                        //PlayerController.Instance.LogoutPlayer(ClientManager.GetClientUserName(token));
                        RSAProvider.RemoveRSA(token);
                        AdminManager.RemoveClient(token);
                        lock (this._callbackDicLocker)
                        {
                            this._callbackDic.Remove(token);
                        }
                    }
                }
            }
            catch
            {
            }
            finally
            {
                if (App.ServiceToRun.IsStarted)
                {
                    this._userStateCheck.Start();
                }
            }
        }

        public CallbackInfo Callback(string token)
        {
            if (RSAProvider.LoadRSA(token))
            {
                bool valid = false;
                Queue<CallbackInfo> queue = null;
                DateTime start = DateTime.Now;
                while (!valid)
                {
                    if (!App.ServiceToRun.IsStarted)
                    {
                        throw new Exception();
                    }

                    lock (this._callbackDicLocker)
                    {
                        if (this._callbackDic.TryGetValue(token, out queue))
                        {
                            lock (queue)
                            {
                                valid = queue.Count > 0;
                            }
                        }
                        else
                        {
                            throw new Exception();
                        }
                    }

                    if (!valid)
                    {
                        if ((DateTime.Now - start).TotalSeconds >= GlobalData.KeepAliveTimeSeconds)
                        {
                            return new CallbackInfo()
                            {
                                MethodName = String.Empty
                            };
                        }

                        Thread.Sleep(100);
                    }
                }

                if (!valid)
                {
                    throw new Exception();
                }

                lock (queue)
                {
                    return queue.Dequeue();
                }
            }
            else
            {
                throw new Exception();
            }
        }

        private void InvokeCallback(string token, string methodName, params object[] paras)
        {
            Queue<CallbackInfo> queue;
            lock (this._callbackDicLocker)
            {
                if (!this._callbackDic.TryGetValue(token, out queue))
                {
                    return;
                }
            }

            lock (queue)
            {
                queue.Enqueue(new CallbackInfo()
                {
                    MethodName = methodName,
                    Parameters = (paras == null) ? null : paras.Select(p =>
                    {
                        Type type = p.GetType();
                        DataContractJsonSerializer s = new DataContractJsonSerializer(type, GetKnownTypes(type));
                        using (MemoryStream ms = new MemoryStream())
                        {
                            s.WriteObject(ms, p);
                            return Encoding.UTF8.GetString(ms.ToArray()).Trim();
                        }
                    }).ToArray()
                });
            }
        }

        private static Type[] GetKnownTypes(Type type)
        {
            Func<MemberInfo, Type, bool> hasAttribute = (info, attrType) =>
            {
                var typeAttrs = info.GetCustomAttributes(attrType, false);
                if ((typeAttrs != null) && (typeAttrs.Length > 0))
                {
                    return true;
                }

                return false;
            };

            List<Type> typeList = new List<Type>();

            PropertyInfo[] props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in props)
            {
                if (hasAttribute(prop, typeof(DataMemberAttribute)))
                {
                    if (!typeList.Contains(prop.PropertyType))
                    {
                        typeList.Add(prop.PropertyType);
                    }
                }
            }

            return typeList.Count > 0 ? typeList.ToArray() : null;
        }

        public string LoginAdmin(string adminName, string password, string mac, string key)
        {
            string token = string.Empty;
            if (string.IsNullOrEmpty(adminName) || String.IsNullOrEmpty(password) || String.IsNullOrEmpty(mac) || String.IsNullOrEmpty(key))
            {
                return token;
            }
            try
            {
                AdminInfo admin = DBProvider.AdminDBProvider.GetAdmin(adminName);
                if (null == admin)
                {
                    return token;
                }
                if (admin.LoginPassword != password)
                {
                    return token;
                }

                if (admin.Macs == null || admin.Macs.Length == 0)
                {
                    if (admin.Macs.FirstOrDefault(s => s.ToLower() == mac.ToLower()) == null)
                    {
                        return token;
                    }
                }

                token = AdminManager.GetToken(adminName);
                if (!string.IsNullOrEmpty(token))
                {
                    new Thread(new ParameterizedThreadStart(o =>
                    {
                        this.KickoutByUser(o.ToString());
                    })).Start(token);

                    //return "ISLOGGED";
                }

                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(key);

                token = Guid.NewGuid().ToString();
                RSAProvider.SetRSA(token, rsa);
                RSAProvider.LoadRSA(token);

                AdminManager.AddClient(adminName, admin.ActionPassword, token);
                lock (this._callbackDicLocker)
                {
                    this._callbackDic[token] = new Queue<CallbackInfo>();
                }

                LogHelper.Instance.AddInfoLog("Admin login, userId=" + adminName + ", remoteIP=" + AdminManager.GetClientIP(token));

            }
            catch (Exception ex)
            {
                LogHelper.Instance.AddErrorLog("Admin login failed, userId=" + adminName + ", remoteIP=" + AdminManager.GetClientIP(token), ex);
            }

            return token;
        }

        public AdminInfo GetAdminInfo(string token)
        {
            if (RSAProvider.LoadRSA(token))
            {
                var adminUserName = AdminManager.GetClientUserName(token);
                if (!string.IsNullOrEmpty(adminUserName))
                {
                    return DBProvider.AdminDBProvider.GetAdmin(adminUserName);
                }

                return null;
            }
            else
            {
                throw new Exception();
            }
        }

        public bool LogoutAdmin(string token)
        {
            if (RSAProvider.LoadRSA(token))
            {
                RSAProvider.RemoveRSA(token);
                AdminManager.RemoveClient(token);
                lock (this._callbackDicLocker)
                {
                    this._callbackDic.Remove(token);
                }
                //if (!string.IsNullOrEmpty(token))
                //{
                //    new Thread(new ParameterizedThreadStart(o =>
                //    {
                //        this.LogedOut(o.ToString());
                //    })).Start(token);
                //}
                return true;
            }
            else
            {
                throw new Exception();
            }
        }

        public SystemConfigin1 GetGameConfig(string token)
        {
            if (RSAProvider.LoadRSA(token))
            {
                SystemConfigin1 config = new SystemConfigin1()
                {
                    GameConfig = GlobalConfig.GameConfig,
                    RegisterUserConfig = GlobalConfig.RegisterPlayerConfig,
                    AwardReferrerConfigList = GlobalConfig.AwardReferrerLevelConfig.GetListAward().ToArray()
                };

                return config;
            }
            else
            {
                throw new Exception();
            }
        }

        public PlayerInfoLoginWrap[] GetPlayers(string token)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    string adminUserName = AdminManager.GetClientUserName(token);
                    if (string.IsNullOrEmpty(adminUserName))
                    {
                        return null;
                    }

                    PlayerInfo[] players = DBProvider.UserDBProvider.GetAllPlayers();
                    if (players == null)
                    {
                        return null;
                    }
                    PlayerInfoLoginWrap[] users = new PlayerInfoLoginWrap[players.Length];
                    for (int i = 0; i < players.Length; i++)
                    {
                        users[i] = new PlayerInfoLoginWrap();
                        users[i].SimpleInfo = players[i].SimpleInfo;
                        users[i].FortuneInfo = players[i].FortuneInfo;
                        users[i].isOnline = ClientManager.IsExistUserName(players[i].SimpleInfo.UserName);
                        if (users[i].isOnline)
                        {
                            users[i].LoginIP = ClientManager.GetClientIP(ClientManager.GetToken(players[i].SimpleInfo.UserName));
                        }
                    }

                    return users;
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("ServiceToAdmin.GetPlayers Exception", exc);
                    return null;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public bool ChangePlayer(string token, string actionPassword, PlayerInfoLoginWrap player)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    var admin = AdminManager.GetClient(token);
                    if (admin == null)
                    {
                        return false;
                    }
                    if (admin.ActionPassword != actionPassword)
                    {
                        return false;
                    }

                    bool isOK = PlayerController.Instance.ChangePlayerSimpleInfo(player.SimpleInfo.UserName, player.SimpleInfo.NickName, player.SimpleInfo.Alipay, player.SimpleInfo.AlipayRealName, player.SimpleInfo.Email, player.SimpleInfo.QQ);
                    if (!isOK)
                    {
                        return false;
                    }
                    isOK = PlayerController.Instance.ChangePlayerFortuneInfo(player.FortuneInfo);
                    return isOK;
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("ServiceToAdmin.ChangePlayer Exception", exc);
                    return false;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public DeleteResultInfo DeletePlayers(string token, string actionPassword, string[] playerUserNames)
        {
            if (RSAProvider.LoadRSA(token))
            {
                DeleteResultInfo result = new DeleteResultInfo();
                try
                {
                    var admin = AdminManager.GetClient(token);
                    if (admin == null)
                    {
                        return result;
                    }
                    if (admin.ActionPassword != actionPassword)
                    {
                        return result;
                    }

                    if (playerUserNames == null)
                    {
                        return result;
                    }

                    List<string> listSucceed = new List<string>();
                    List<string> listFailed = new List<string>();

                    foreach (var name in playerUserNames)
                    {
                        bool isOK = PlayerController.Instance.DeletePlayer(name);
                        if (isOK)
                        {
                            listSucceed.Add(name);
                        }
                        else
                        {
                            listFailed.Add(name);
                        }
                    }

                    if (listFailed.Count == 0)
                    {
                        result.AllSucceed = true;
                    }

                    result.SucceedList = listSucceed.ToArray();
                    result.FailedList = listFailed.ToArray();
                    return result;
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("ServiceToAdmin.ChangePlayer Exception", exc);
                    return result;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public bool LockPlayer(string token, string actionPassword, string playerUserName)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    var admin = AdminManager.GetClient(token);
                    if (admin == null)
                    {
                        return false;
                    }
                    if (admin.ActionPassword != actionPassword)
                    {
                        return false;
                    }

                    return PlayerController.Instance.LockPlayer(playerUserName);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("ServiceToAdmin.LockPlayer", exc);
                    return false;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public bool UnlockPlayer(string token, string actionPassword, string playerUserName)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    var admin = AdminManager.GetClient(token);
                    if (admin == null)
                    {
                        return false;
                    }
                    if (admin.ActionPassword != actionPassword)
                    {
                        return false;
                    }

                    return PlayerController.Instance.UnlockPlayer(playerUserName);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("ServiceToAdmin.UnlockPlayer", exc);
                    return false;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public bool UpdatePlayerFortuneInfo(string token, string actionPassword, MetaData.User.PlayerFortuneInfo fortuneInfo)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    var admin = AdminManager.GetClient(token);
                    if (admin == null)
                    {
                        return false;
                    }
                    if (admin.ActionPassword != actionPassword)
                    {
                        return false;
                    }

                    return PlayerController.Instance.ChangePlayerFortuneInfo(fortuneInfo);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("ServiceToAdmin.UpdatePlayerFortuneInfo", exc);
                    return false;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public bool ChangePlayerPassword(string token, string actionPassword, string playerUserName, string newPassword)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    var admin = AdminManager.GetClient(token);
                    if (admin == null)
                    {
                        return false;
                    }
                    if (admin.ActionPassword != actionPassword)
                    {
                        return false;
                    }

                    return PlayerController.Instance.ChangePasswordByAdmin(playerUserName, newPassword);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("ServiceToAdmin.ChangePlayerPassword", exc);
                    return false;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public MetaData.NoticeInfo[] GetNotices(string token)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    return NoticeController.Instance.GetAllNotices();
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("GetNotices Exception. ClientIP=" + ClientManager.GetClientIP(token), exc);
                    return null;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public bool CreateNotice(string token, MetaData.NoticeInfo notice)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    return NoticeController.Instance.CreateNotice(notice);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("GetNotices Exception. ClientIP=" + ClientManager.GetClientIP(token), exc);
                    return false;
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
        /// <param name="sellerUserName"></param>
        /// <param name="sellOrderState">小于0表示全部类型</param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public SellStonesOrder[] GetSellStonesOrderList(string token, string sellerUserName, string orderNumber, int orderType, MyDateTime myBeginCreateTime, MyDateTime myEndCreateTime, int pageItemCount, int pageIndex)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    return DBProvider.StoneOrderDBProvider.GetSellOrderList(sellerUserName, orderNumber, orderType, myBeginCreateTime, myEndCreateTime, pageItemCount, pageIndex);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("GetSellStonesOrderList Exception. ClientIP=" + ClientManager.GetClientIP(token), exc);
                    return null;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public LockSellStonesOrder[] GetLockedStonesOrderList(string token, string buyerUserName)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    return DBProvider.StoneOrderDBProvider.GetLockSellStonesOrderList(buyerUserName);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("GetLockedStonesOrderList Exception. ClientIP=" + ClientManager.GetClientIP(token), exc);
                    return null;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public BuyStonesOrder[] GetBuyStonesOrderList(string token, string sellerUserName, string orderNumber, string buyUserName, int orderType, MyDateTime myBeginCreateTime, MyDateTime myEndCreateTime, MyDateTime myBeginBuyTime, MyDateTime myEndBuyTime, int pageItemCount, int pageIndex)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    return DBProvider.StoneOrderDBProvider.GetBuyStonesOrderList(sellerUserName, orderNumber, buyUserName, orderType, myBeginCreateTime, myEndCreateTime, myBeginBuyTime, myEndBuyTime, pageItemCount, pageIndex);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("GetBuyStonesOrderList Exception. ClientIP=" + ClientManager.GetClientIP(token), exc);
                    return null;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public int HandleExceptionStoneOrderSucceed(string token, AlipayRechargeRecord alipayRecord)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    return OrderController.Instance.StoneOrderController.HandleExceptionOrderSucceed(alipayRecord);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("HandleExceptionStoneOrderSucceed Exception. ClientIP=" + ClientManager.GetClientIP(token), exc);
                    return OperResult.RESULTCODE_EXCEPTION;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public int HandleExceptionStoneOrderFailed(string token, string orderNumber)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    return OrderController.Instance.StoneOrderController.HandleExceptionOrderCancel(orderNumber);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("HandleExceptionStoneOrderFailed Exception. ClientIP=" + ClientManager.GetClientIP(token), exc);
                    return OperResult.RESULTCODE_EXCEPTION;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public int PayWithdrawRMBRecord(string token, WithdrawRMBRecord record)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    return PlayerController.Instance.PayWithdrawRMB(record);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("PayWithdrawRMBRecord Exception. ClientIP=" + ClientManager.GetClientIP(token), exc);
                    return OperResult.RESULTCODE_EXCEPTION;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public WithdrawRMBRecord[] GetWithdrawRMBRecordList(string token, bool isPayed, string playerUserName, MyDateTime beginCreateTime, MyDateTime endCreateTime, string adminUserName, MyDateTime beginPayTime, MyDateTime endPayTime, int pageItemCount, int pageIndex)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    return DBProvider.WithdrawRMBRecordDBProvider.GetWithdrawRMBRecordList(isPayed, playerUserName, beginCreateTime, endCreateTime, adminUserName, beginPayTime, endPayTime, pageItemCount, pageIndex);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("GetWithdrawRMBRecordList Exception. ClientIP=" + ClientManager.GetClientIP(token), exc);
                    return null;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public GoldCoinRechargeRecord[] GetFinishedGoldCoinRechargeRecordList(string token, string playerUserName, string orderNumber, MyDateTime beginCreateTime, MyDateTime endCreateTime, int pageItemCount, int pageIndex)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    return DBProvider.GoldCoinRecordDBProvider.GetFinishedGoldCoinRechargeRecordList(playerUserName, orderNumber, beginCreateTime, endCreateTime, pageItemCount, pageIndex);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("GetFinishedGoldCoinRechargeRecordList Exception. ClientIP=" + ClientManager.GetClientIP(token), exc);
                    return null;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public AlipayRechargeRecord[] GetAllExceptionAlipayRechargeRecords(string token)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    return DBProvider.AlipayRecordDBProvider.GetAllExceptionAlipayRechargeRecords();
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("GetFinishedGoldCoinRechargeRecordList Exception. ClientIP=" + ClientManager.GetClientIP(token), exc);
                    return null;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public int HandleExceptionAlipayRechargeRecord(string token, AlipayRechargeRecord exceptionRecord)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    bool isOK = OrderController.Instance.AlipayCallback(exceptionRecord);
                    if (isOK)
                    {
                        isOK = DBProvider.AlipayRecordDBProvider.DeleteExceptionAlipayRecord(exceptionRecord.alipay_trade_no, exceptionRecord.out_trade_no);
                    }

                    return isOK ? OperResult.RESULTCODE_TRUE : OperResult.RESULTCODE_FALSE;
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("HandleExceptionAlipayRechargeRecord Exception. ClientIP=" + ClientManager.GetClientIP(token), exc);
                    return OperResult.RESULTCODE_EXCEPTION;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public MinersBuyRecord[] GetBuyMinerFinishedRecordList(string token, string playerUserName, MyDateTime beginCreateTime, MyDateTime endCreateTime, int pageItemCount, int pageIndex)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    return DBProvider.BuyMinerRecordDBProvider.GetFinishedBuyMinerRecordList(playerUserName, beginCreateTime, endCreateTime, pageItemCount, pageIndex);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("GetBuyMinerFinishedRecordList Exception. ClientIP=" + ClientManager.GetClientIP(token), exc);
                    return null;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public MinesBuyRecord[] GetBuyMineFinishedRecordList(string token, string playerUserName, MyDateTime beginCreateTime, MyDateTime endCreateTime, int pageItemCount, int pageIndex)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    return DBProvider.MineRecordDBProvider.GetAllMineTradeRecords(playerUserName, beginCreateTime, endCreateTime, pageItemCount, pageIndex);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("GetBuyMineFinishedRecordList Exception. ClientIP=" + ClientManager.GetClientIP(token), exc);
                    return null;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            this._userStateCheck.Dispose();
        }

        #endregion

    }
}
