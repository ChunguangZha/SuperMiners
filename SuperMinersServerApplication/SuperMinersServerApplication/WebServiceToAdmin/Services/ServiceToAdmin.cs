using MetaData;
using MetaData.Shopping;
using MetaData.SystemConfig;
using MetaData.Trade;
using MetaData.User;
using SuperMinersServerApplication.Controller;
using SuperMinersServerApplication.Controller.Shopping;
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
                    AwardReferrerConfigList = GlobalConfig.AwardReferrerLevelConfig.GetListAward().ToArray(),
                    RouletteConfig = GlobalConfig.RouletteConfig
                    
                };

                return config;
            }
            else
            {
                throw new Exception();
            }
        }

        public PlayerInfoLoginWrap GetPlayer(string token, string userName)
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

                    PlayerInfo player = PlayerController.Instance.GetPlayerInfoByUserName(userName);
                    if (player == null)
                    {
                        return null;
                    }
                    PlayerInfoLoginWrap user = new PlayerInfoLoginWrap();
                    user.SimpleInfo = player.SimpleInfo;
                    user.FortuneInfo = player.FortuneInfo;
                    user.isOnline = ClientManager.IsExistUserName(player.SimpleInfo.UserName);
                    user.LockedInfo = DBProvider.PlayerLockedInfoDBProvider.GetPlayerLockedInfo(player.SimpleInfo.UserID);

                    return user;
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("ServiceToAdmin.GetPlayer Exception", exc);
                    return null;
                }
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
                    PlayerLockedInfo[] playerLockedInfos = DBProvider.PlayerLockedInfoDBProvider.GetAllPlayerLockedInfo();
                    PlayerInfoLoginWrap[] users = new PlayerInfoLoginWrap[players.Length];
                    for (int i = 0; i < players.Length; i++)
                    {
                        PlayerInfoLoginWrap userWrap = new PlayerInfoLoginWrap();
                        userWrap.SimpleInfo = players[i].SimpleInfo;
                        userWrap.FortuneInfo = players[i].FortuneInfo;
                        userWrap.isOnline = ClientManager.IsExistUserName(userWrap.SimpleInfo.UserName);
                        if (playerLockedInfos != null)
                        {
                            userWrap.LockedInfo = playerLockedInfos.FirstOrDefault(p => p.UserID == userWrap.SimpleInfo.UserID);
                        }

                        users[i] = userWrap;
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

        public int ChangePlayer(string token, string actionPassword, PlayerInfoLoginWrap player)
        {
            if (RSAProvider.LoadRSA(token))
            {
                AdminLoginnedInfo admin = null;
                try
                {
                    admin = AdminManager.GetClient(token);
                    if (admin == null)
                    {
                        return OperResult.RESULTCODE_FALSE;
                    }
                    if (admin.ActionPassword != actionPassword)
                    {
                        return OperResult.RESULTCODE_FALSE;
                    }

                    int result = PlayerController.Instance.ChangePlayerSimpleInfo(player.SimpleInfo.UserName, player.SimpleInfo.Alipay, player.SimpleInfo.AlipayRealName, player.SimpleInfo.IDCardNo, player.SimpleInfo.Email, player.SimpleInfo.QQ);
                    if (result != OperResult.RESULTCODE_TRUE)
                    {
                        return result;
                    }
                    var isOK = PlayerController.Instance.ChangePlayerFortuneInfo(player.FortuneInfo);
                    if (isOK)
                    {
                        LogHelper.Instance.AddInfoLog("管理员[" + admin.UserName + "]修改了玩家[" + player .SimpleInfo.UserName + "]信息");
                    }
                    return isOK ? OperResult.RESULTCODE_TRUE : OperResult.RESULTCODE_FALSE;
                }
                catch (Exception exc)
                {
                    if (admin != null)
                    {
                        LogHelper.Instance.AddErrorLog("管理员["+admin.UserName+"]修改玩家信息异常", exc);
                    }
                    else
                    {
                        LogHelper.Instance.AddErrorLog("ServiceToAdmin.ChangePlayer Exception", exc);
                    }
                    return OperResult.RESULTCODE_FALSE;
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

        public bool LockPlayer(string token, string actionPassword, string playerUserName, int expireDays)
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

                    return PlayerController.Instance.LockPlayer(playerUserName, expireDays);
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

        public bool SaveNotice(string token, MetaData.NoticeInfo notice, bool isAdd)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    if (isAdd)
                    {
                        notice.Time = DateTime.Now;
                    }
                    return NoticeController.Instance.SaveNotice(notice, isAdd);
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
        public SellStonesOrder[] GetSellStonesOrderList(string token, string sellerUserName, string orderNumber, int orderState, MyDateTime myBeginCreateTime, MyDateTime myEndCreateTime, int pageItemCount, int pageIndex)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    return DBProvider.StoneOrderDBProvider.GetSellOrderList(sellerUserName, orderNumber, orderState, myBeginCreateTime, myEndCreateTime, pageItemCount, pageIndex);
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

        public LockSellStonesOrder[] GetLockedStonesOrderList(string token, string sellerUserName, string orderNumber, string buyUserName, int orderState)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    return DBProvider.StoneOrderDBProvider.GetLockSellStonesOrderList(sellerUserName, orderNumber, buyUserName, orderState);
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

        public BuyStonesOrder[] GetBuyStonesOrderList(string token, string sellerUserName, string orderNumber, string buyUserName, int orderState, MyDateTime myBeginCreateTime, MyDateTime myEndCreateTime, MyDateTime myBeginBuyTime, MyDateTime myEndBuyTime, int pageItemCount, int pageIndex)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    return DBProvider.StoneOrderDBProvider.GetBuyStonesOrderList(sellerUserName, orderNumber, buyUserName, orderState, myBeginCreateTime, myEndCreateTime, myBeginBuyTime, myEndBuyTime, pageItemCount, pageIndex);
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

        public int HandleExceptionAlipayRechargeRecord(string token, AlipayRechargeRecord exceptionRecord)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    int result = OrderController.Instance.AlipayCallback(exceptionRecord);
                    if (result == OperResult.RESULTCODE_TRUE || result == OperResult.RESULTCODE_ORDER_BUY_SUCCEED)
                    {
                        bool isOK = DBProvider.AlipayRecordDBProvider.DeleteExceptionAlipayRecord(exceptionRecord.alipay_trade_no, exceptionRecord.out_trade_no);
                    }

                    return result;
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

        // 被 HandleExceptionAlipayRechargeRecord 替代
        //public int AgreeExceptionStoneOrder(string token, AlipayRechargeRecord alipayRecord)
        //{
        //    if (RSAProvider.LoadRSA(token))
        //    {
        //        try
        //        {
        //            return OrderController.Instance.StoneOrderController.AgreeExceptionStoneOrder(alipayRecord);
        //        }
        //        catch (Exception exc)
        //        {
        //            LogHelper.Instance.AddErrorLog("AgreeExceptionStoneOrder Exception. ClientIP=" + ClientManager.GetClientIP(token), exc);
        //            return OperResult.RESULTCODE_EXCEPTION;
        //        }
        //    }
        //    else
        //    {
        //        throw new Exception();
        //    }
        //}

        public int RejectExceptionStoneOrder(string token, string orderNumber)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    return OrderController.Instance.StoneOrderController.RejectExceptionStoneOrder(orderNumber);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("RejectExceptionStoneOrder Exception. ClientIP=" + ClientManager.GetClientIP(token), exc);
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

        public WithdrawRMBRecord[] GetWithdrawRMBRecordList(string token, int state, string playerUserName, MyDateTime beginCreateTime, MyDateTime endCreateTime, string adminUserName, MyDateTime beginPayTime, MyDateTime endPayTime, int pageItemCount, int pageIndex)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    return DBProvider.WithdrawRMBRecordDBProvider.GetWithdrawRMBRecordList(state, playerUserName, beginCreateTime, endCreateTime, adminUserName, beginPayTime, endPayTime, pageItemCount, pageIndex);
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

        public AlipayRechargeRecord SearchExceptionAlipayRechargeRecord(string token, string orderNumber)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    return DBProvider.AlipayRecordDBProvider.SearchExceptionAlipayRechargeRecord(orderNumber);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("SearchExceptionAlipayRechargeRecord Exception. ClientIP=" + ClientManager.GetClientIP(token), exc);
                    return null;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public AlipayRechargeRecord[] GetAllAlipayRechargeRecords(string token, string orderNumber, string alipayOrderNumber, string payEmail, string playerUserName, MyDateTime beginPayTime, MyDateTime endPayTime, int pageItemCount, int pageIndex)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    return DBProvider.AlipayRecordDBProvider.GetAllAlipayRechargeRecords(orderNumber, alipayOrderNumber, payEmail, playerUserName, beginPayTime, endPayTime, pageItemCount, pageIndex);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("GetAllAlipayRechargeRecords Exception. ClientIP=" + ClientManager.GetClientIP(token), exc);
                    return null;
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

        public int SetPlayerAsAgent(string token, int userID, string userName, string agentReferURL)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    return PlayerController.Instance.SetPlayerAsAgent(userID, userName, agentReferURL);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("管理员 SetPlayerAsAgent 异常", exc);
                    return OperResult.RESULTCODE_EXCEPTION;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        //public PlayerLoginInfo[] GetUserLoginLog(string token, int userID)
        //{
        //    if (RSAProvider.LoadRSA(token))
        //    {
        //        return PlayerController.Instance.GetUserLoginLog(userID);
        //    }
        //    else
        //    {
        //        throw new Exception();
        //    }
        //}

        public ExpChangeRecord[] GetExpChangeRecord(string token, int userID)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    return DBProvider.ExpChangeRecordDBProvider.GetExpChangeRecord(userID);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("管理员 GetExpChangeRecord 异常", exc);
                    return null;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public PlayerInfo[] GetDeletedPlayers(string token)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    return DBProvider.DeletedPlayerInfoDBProvider.GetDeletedPlayers();
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("管理员 GetDeletedPlayers 异常", exc);
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


        #region IServiceToAdmin Members


        public OldPlayerTransferRegisterInfo[] GetPlayerTransferRecords(string token)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    return DBProvider.OldPlayerTransferDBProvider.GetAllPlayerTransferRecords();
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("管理员 GetPlayerTransferRecords 异常", exc);
                    return null;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// 一区服务器处理
        /// </summary>
        /// <param name="token"></param>
        /// <param name="recordID"></param>
        /// <param name="userName"></param>
        /// <param name="adminUserName"></param>
        /// <returns></returns>
        public int TransferPlayerFrom(string token, int recordID, string userName, string adminUserName)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    var playerRunner = PlayerController.Instance.GetRunnable(userName);
                    if (playerRunner == null)
                    {
                        return OperResult.RESULTCODE_USER_NOT_EXIST;
                    }
                    playerRunner.LockPlayer(1000);
                    bool IsOK = DBProvider.OldPlayerTransferDBProvider.TransferPlayer(recordID, adminUserName);
                    return OperResult.RESULTCODE_TRUE;
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("管理员 TransferPlayerFrom 异常", exc);
                    return OperResult.RESULTCODE_EXCEPTION;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// 二区服务器处理
        /// </summary>
        /// <param name="adminUserName"></param>
        /// <param name="simpleInfo"></param>
        /// <param name="fortuneInfo"></param>
        /// <returns></returns>
        public int TransferPlayerTo(string adminUserName, PlayerSimpleInfo simpleInfo, PlayerFortuneInfo fortuneInfo, string newUserLoginName, string newPassword)
        {
            int result = OperResult.RESULTCODE_FALSE;
            try
            {
                if (string.IsNullOrEmpty(newUserLoginName) || string.IsNullOrEmpty(newPassword))
                {
                    result = PlayerController.Instance.RegisterUser(simpleInfo.RegisterIP, simpleInfo.UserLoginName, simpleInfo.UserName, simpleInfo.Password, simpleInfo.Alipay,
                        simpleInfo.AlipayRealName, simpleInfo.IDCardNo, simpleInfo.Email, simpleInfo.QQ, "");
                    if (result != OperResult.RESULTCODE_TRUE)
                    {
                        result = PlayerController.Instance.RegisterUser(simpleInfo.RegisterIP, simpleInfo.UserLoginName + "xl", simpleInfo.UserName + "xl", simpleInfo.Password, simpleInfo.Alipay,
                            simpleInfo.AlipayRealName, simpleInfo.IDCardNo, simpleInfo.Email, simpleInfo.QQ, "");
                        if (result != OperResult.RESULTCODE_TRUE)
                        {
                            return result;
                        }
                    }

                    newUserLoginName = simpleInfo.UserName;
                }
                var playerRunner = PlayerController.Instance.GetRunnableByUserLoginName(newUserLoginName);
                if (playerRunner == null || playerRunner.BasePlayer.SimpleInfo.Password != newPassword)
                {                    
                    result = OperResult.RESULTCODE_USER_NOT_EXIST;
                    return result;
                }

                bool isOK = playerRunner.ChangePlayerSimpleInfo(simpleInfo.Alipay, simpleInfo.AlipayRealName, simpleInfo.IDCardNo, simpleInfo.Email, simpleInfo.QQ);
                isOK = playerRunner.SetFortuneInfo(fortuneInfo);

                result = isOK ? OperResult.RESULTCODE_TRUE : OperResult.RESULTCODE_FALSE;
                return result;
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("管理员 TransferPlayerTo 异常", exc);
                result = OperResult.RESULTCODE_EXCEPTION;
                return result;
            }
            finally
            {
                LogHelper.Instance.AddInfoLog("管理员[" + adminUserName + "] 从一区转入玩家[" + simpleInfo.UserLoginName + "] " + "，操作结果为：" + OperResult.GetMsg(result) + "。 newUserLoginName=" + newUserLoginName + "; newPassword=" + newPassword + "; fortuneInfo=" + fortuneInfo.ToString());
            }

        }

        #endregion


        public MetaData.Game.StoneStack.StoneDelegateBuyOrderInfo[] GetStoneDelegateBuyOrderInfo(string token, string playerUserName, MyDateTime beginCreateTime, MyDateTime endCreateTime, int pageItemCount, int pageIndex)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    return DBProvider.StoneStackDBProvider.GetAllFinishedStoneDelegateBuyOrderInfoByPlayer(playerUserName, beginCreateTime, endCreateTime, pageItemCount, pageIndex);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("GetStoneDelegateBuyOrderInfo Exception. ClientIP=" + ClientManager.GetClientIP(token), exc);
                    return null;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public MetaData.Game.StoneStack.StoneDelegateSellOrderInfo[] GetStoneDelegateSellOrderInfo(string token, string playerUserName, MyDateTime beginFinishedTime, MyDateTime endFinishedTime, int pageItemCount, int pageIndex)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    return DBProvider.StoneStackDBProvider.GetAllFinishedStoneDelegateSellOrderInfoByPlayer(playerUserName, beginFinishedTime, endFinishedTime, pageItemCount, pageIndex);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("GetStoneDelegateSellOrderInfo Exception. ClientIP=" + ClientManager.GetClientIP(token), exc);
                    return null;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public int HandlePlayerRemoteService(string token, string actionPassword, string playerUserName, string serviceContent, MyDateTime serviceTime, string engineerName)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    var admin = AdminManager.GetClient(token);
                    if (admin == null)
                    {
                        return OperResult.RESULTCODE_ADMIN_USER_NOT_EXIST;
                    }
                    if (admin.ActionPassword != actionPassword)
                    {
                        return OperResult.RESULTCODE_ADMIN_ACTIONPASSWORD_ERROR;
                    }

                    int result = UserRemoteServerController.Instance.HandlePlayerRemoteService(admin.UserName, playerUserName, serviceContent, serviceTime, engineerName);
                    if (result == OperResult.RESULTCODE_TRUE)
                    {
                        LogHelper.Instance.AddInfoLog("玩家[" + playerUserName + "] 完成一次远程协助服务，服务内容：" + serviceContent + "。服务时间：" + serviceTime.ToString() + "。工程师：" + engineerName + "。管理员：" + admin.UserName);
                    }

                    return result;
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("HandlePlayerRemoteService Exception. ClientIP=" + ClientManager.GetClientIP(token), exc);
                    return OperResult.RESULTCODE_EXCEPTION;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public UserRemoteServerBuyRecord[] GetUserRemoteServerBuyRecords(string token, string playerUserName, MyDateTime beginCreateTime, MyDateTime endCreateTime, int pageItemCount, int pageIndex)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    return DBProvider.UserRemoteServerDBProvider.GetUserRemoteServerBuyRecords(playerUserName, beginCreateTime, endCreateTime, pageItemCount, pageIndex);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("GetUserRemoteServerBuyRecords Exception. ClientIP=" + ClientManager.GetClientIP(token), exc);
                    return null;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public UserRemoteHandleServiceRecord[] GetUserRemoteHandleServiceRecords(string token, string playerUserName, MyDateTime beginCreateTime, MyDateTime endCreateTime, int pageItemCount, int pageIndex)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    return DBProvider.UserRemoteServerDBProvider.GetUserRemoteHandleServiceRecords(playerUserName, beginCreateTime, endCreateTime, pageItemCount, pageIndex);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("GetUserRemoteHandleServiceRecords Exception. ClientIP=" + ClientManager.GetClientIP(token), exc);
                    return null;
                }
            }
            else
            {
                throw new Exception();
            }
        }


        public int AddVirtualShoppingItem(string token, string actionPassword, MetaData.Shopping.VirtualShoppingItem item)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    var admin = AdminManager.GetClient(token);
                    if (admin == null)
                    {
                        return OperResult.RESULTCODE_ADMIN_USER_NOT_EXIST;
                    }
                    if (admin.ActionPassword != actionPassword)
                    {
                        return OperResult.RESULTCODE_ADMIN_ACTIONPASSWORD_ERROR;
                    }

                    LogHelper.Instance.AddInfoLog("管理员["+admin.UserName+"]在虚拟商城里添加一项虚拟商品：" + item.Name);
                    return VirtualShoppingController.Instance.AddVirtualShoppingItem(item);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("AddVirtualShoppingItem Exception. ClientIP=" + ClientManager.GetClientIP(token), exc);
                    return OperResult.RESULTCODE_EXCEPTION;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public int UpdateVirtualShoppingItem(string token, string actionPassword, MetaData.Shopping.VirtualShoppingItem item)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    var admin = AdminManager.GetClient(token);
                    if (admin == null)
                    {
                        return OperResult.RESULTCODE_ADMIN_USER_NOT_EXIST;
                    }
                    if (admin.ActionPassword != actionPassword)
                    {
                        return OperResult.RESULTCODE_ADMIN_ACTIONPASSWORD_ERROR;
                    }

                    LogHelper.Instance.AddInfoLog("管理员[" + admin.UserName + "]修改了虚拟商品：" + item.Name);
                    return VirtualShoppingController.Instance.UpdateVirtualShoppingItem(item);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("UpdateVirtualShoppingItem Exception. ClientIP=" + ClientManager.GetClientIP(token), exc);
                    return OperResult.RESULTCODE_EXCEPTION;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public MetaData.Shopping.VirtualShoppingItem[] GetVirtualShoppingItems(string token, bool getAllItem, MetaData.Shopping.SellState state)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    return VirtualShoppingController.Instance.GetVirtualShoppingItems(getAllItem, state);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("GetVirtualShoppingItems Exception. ClientIP=" + ClientManager.GetClientIP(token), exc);
                    return null;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public MetaData.Shopping.PlayerBuyVirtualShoppingItemRecord[] GetPlayerBuyVirtualShoppingItemRecord(string token, string playerUserName, string shoppingItemName, MyDateTime beginBuyTime, MyDateTime endBuyTime, int pageItemCount, int pageIndex)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    return VirtualShoppingController.Instance.GetPlayerBuyVirtualShoppingItemRecordByName(playerUserName, shoppingItemName, beginBuyTime, endBuyTime, pageItemCount, pageIndex);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("GetPlayerBuyVirtualShoppingItemRecord Exception. ClientIP=" + ClientManager.GetClientIP(token), exc);
                    return null;
                }
            }
            else
            {
                throw new Exception();
            }
        }
        
        public int AddDiamondShoppingItem(string token, string actionPassword, MetaData.Shopping.DiamondShoppingItem item)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    var admin = AdminManager.GetClient(token);
                    if (admin == null)
                    {
                        return OperResult.RESULTCODE_ADMIN_USER_NOT_EXIST;
                    }
                    if (admin.ActionPassword != actionPassword)
                    {
                        return OperResult.RESULTCODE_ADMIN_ACTIONPASSWORD_ERROR;
                    }

                    LogHelper.Instance.AddInfoLog("管理员[" + admin.UserName + "]在钻石商城里添加一项商品：" + item.Name);
                    return DiamondShoppingController.Instance.AddDiamondShoppingItem(item);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("AddDiamondShoppingItem Exception. ClientIP=" + ClientManager.GetClientIP(token), exc);
                    return OperResult.RESULTCODE_EXCEPTION;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public int UpdateDiamondShoppingItem(string token, string actionPassword, MetaData.Shopping.DiamondShoppingItem item)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    var admin = AdminManager.GetClient(token);
                    if (admin == null)
                    {
                        return OperResult.RESULTCODE_ADMIN_USER_NOT_EXIST;
                    }
                    if (admin.ActionPassword != actionPassword)
                    {
                        return OperResult.RESULTCODE_ADMIN_ACTIONPASSWORD_ERROR;
                    }

                    LogHelper.Instance.AddInfoLog("管理员[" + admin.UserName + "]修改了钻石商城里的商品：" + item.Name);
                    return DiamondShoppingController.Instance.UpdateDiamondShoppingItem(item);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("UpdateDiamondShoppingItem Exception. ClientIP=" + ClientManager.GetClientIP(token), exc);
                    return OperResult.RESULTCODE_EXCEPTION;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public MetaData.Shopping.DiamondShoppingItem[] GetDiamondShoppingItems(string token, bool getAllItem, MetaData.Shopping.SellState state)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    return DiamondShoppingController.Instance.GetDiamondShoppingItems(getAllItem, state);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("GetDiamondShoppingItems Exception. ClientIP=" + ClientManager.GetClientIP(token), exc);
                    return null;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public int HandleBuyDiamondShopping(string token, string actionPassword, PlayerBuyDiamondShoppingItemRecord record)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    var admin = AdminManager.GetClient(token);
                    if (admin == null)
                    {
                        return OperResult.RESULTCODE_ADMIN_USER_NOT_EXIST;
                    }
                    if (admin.ActionPassword != actionPassword)
                    {
                        return OperResult.RESULTCODE_ADMIN_ACTIONPASSWORD_ERROR;
                    }

                    LogHelper.Instance.AddInfoLog("管理员[" + admin.UserName + "]处理了钻石商城订单，订单号为：" + record.OrderNumber);
                    return DiamondShoppingController.Instance.HandleBuyDiamondShopping(record);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("HandleBuyDiamondShopping Exception. ClientIP=" + ClientManager.GetClientIP(token), exc);
                    return OperResult.RESULTCODE_EXCEPTION;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public MetaData.Shopping.PlayerBuyDiamondShoppingItemRecord[] GetPlayerBuyDiamondShoppingItemRecordByName(string token, string playerUserName, string shoppingItemName, int shoppingStateInt, MyDateTime beginBuyTime, MyDateTime endBuyTime, int pageItemCount, int pageIndex)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    return DiamondShoppingController.Instance.GetPlayerBuyDiamondShoppingItemRecordByName(playerUserName, shoppingItemName, shoppingStateInt, beginBuyTime, endBuyTime, pageItemCount, pageIndex);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("GetPlayerBuyDiamondShoppingItemRecordByName Exception. ClientIP=" + ClientManager.GetClientIP(token), exc);
                    return null;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public PostAddress[] GetPlayerPostAddressList(string token, int userID)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    return DBProvider.UserDBProvider.GetPlayerPostAddressList(userID);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("GetPlayerPostAddressList Exception. ClientIP=" + ClientManager.GetClientIP(token), exc);
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
