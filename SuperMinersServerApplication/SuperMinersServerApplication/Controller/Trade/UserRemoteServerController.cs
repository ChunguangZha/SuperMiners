using DataBaseProvider;
using MetaData;
using MetaData.Trade;
using SuperMinersServerApplication.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.Controller.Trade
{
    public class UserRemoteServerController
    {
        #region Single

        private static UserRemoteServerController _instance = new UserRemoteServerController();

        public static UserRemoteServerController Instance
        {
            get
            {
                return _instance;
            }
        }

        private UserRemoteServerController()
        {
        }

        #endregion

        private UserRemoteServerItem[] _serverItems = null;

        public void Init()
        {
            GetUserRemoteServerItems();
        }

        public UserRemoteServerItem[] GetUserRemoteServerItems()
        {
            if (_serverItems == null)
            {
                _serverItems = DBProvider.UserRemoteServerDBProvider.GetUserRemoteServerItems();
            }

            return _serverItems;
        }

        public UserRemoteServerItem GetUserRemoteServerItem(RemoteServerType serverType)
        {
            if (_serverItems == null)
            {
                return null;
            }

            for (int i = 0; i < this._serverItems.Length; i++)
            {
                if (_serverItems[i].ServerType == serverType)
                {
                    return _serverItems[i];
                }
            }

            return null;
        }

        public int AlipayCallback(AlipayRechargeRecord alipay, RemoteServerType serverType)
        {
            var playerRunner = PlayerController.Instance.GetRunnable(alipay.user_name);
            if (playerRunner == null)
            {
                return OperResult.RESULTCODE_USER_NOT_EXIST;
            }

            int result = OperResult.RESULTCODE_FALSE;

            MyDBHelper.Instance.TransactionDataBaseOper(myTrans =>
            {
                result = playerRunner.BuyRemoteServer(alipay, serverType, myTrans);
                if (result != OperResult.RESULTCODE_TRUE)
                {
                    return result;
                }
                UserRemoteServerBuyRecord buyRecord = new UserRemoteServerBuyRecord()
                {
                    UserID = playerRunner.BasePlayer.SimpleInfo.UserID,
                    UserName = playerRunner.BasePlayer.SimpleInfo.UserName,
                    OrderNumber = alipay.out_trade_no,
                    BuyRemoteServerTime = new MyDateTime(DateTime.Now),
                    ServerType = serverType,
                    PayMoneyYuan = (int)alipay.total_fee,
                    GetShoppingCredits = (int)alipay.total_fee * GlobalConfig.GameConfig.RemoteServerRechargeReturnShoppingCreditsTimes
                };
                DBProvider.UserRemoteServerDBProvider.SaveUserRemoteServerBuyRecord(buyRecord, myTrans);

                DBProvider.AlipayRecordDBProvider.SaveAlipayRechargeRecord(alipay, myTrans);

                LogHelper.Instance.AddInfoLog("玩家 [" + alipay.user_name + "] 成功购买远程协助服务，" + serverType.ToString() + ", Alipay:" + alipay.ToString());
                result = OperResult.RESULTCODE_TRUE;
                return result;
            },
            exc =>
            {
                LogHelper.Instance.AddErrorLog("远程协助服务付款回调异常，AlipayInfo: " + alipay.ToString() + "; serverType: " + serverType.ToString(), exc);
                result = OperResult.RESULTCODE_EXCEPTION;
            });

            return result;
        }
    }
}
