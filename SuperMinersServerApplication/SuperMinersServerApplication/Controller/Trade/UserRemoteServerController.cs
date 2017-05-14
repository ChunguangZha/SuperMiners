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
            GetUserRemoteServerItems();
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
            int result = OperResult.RESULTCODE_FALSE;

            MyDBHelper.Instance.TransactionDataBaseOper(myTrans =>
            {
                DBProvider.AlipayRecordDBProvider.SaveAlipayRechargeRecord(alipay, myTrans);

                LogHelper.Instance.AddInfoLog("玩家 [" + alipay.user_name + "] 支付宝充值购买远程协助服务，" + serverType.ToString() + ", Alipay:" + alipay.ToString());
                result = OperResult.RESULTCODE_TRUE;
                return result;
            },
            exc =>
            {
                LogHelper.Instance.AddErrorLog("远程协助1服务付款回调异常，AlipayInfo: " + alipay.ToString() + "; serverType: " + serverType.ToString(), exc);
                result = OperResult.RESULTCODE_EXCEPTION;
            });
            if (result != OperResult.RESULTCODE_TRUE)
            {
                return result;
            }

            var serverItem = this.GetUserRemoteServerItem(serverType);
            if (serverItem == null)
            {
                LogHelper.Instance.AddInfoLog("玩家 [" + alipay.user_name + "] 支付宝充值购买远程协助服务失败1，原因为：" + OperResult.GetMsg(OperResult.RESULTCODE_BUYREMOTESERVER_FAILED_SERVERTYPEERROR));
                return OperResult.RESULTCODE_BUYREMOTESERVER_FAILED_SERVERTYPEERROR;
            }
            if (alipay.total_fee != serverItem.PayMoneyYuan)
            {
                LogHelper.Instance.AddInfoLog("玩家 [" + alipay.user_name + "] 支付宝充值购买远程协助服务失败2，原因为：" + OperResult.GetMsg(OperResult.RESULTCODE_BUYREMOTESERVER_FAILED_PAYEDMONEYERROR));
                return OperResult.RESULTCODE_BUYREMOTESERVER_FAILED_PAYEDMONEYERROR;
            }

            var playerRunner = PlayerController.Instance.GetRunnable(alipay.user_name);
            if (playerRunner == null)
            {
                LogHelper.Instance.AddInfoLog("玩家 [" + alipay.user_name + "] 支付宝充值购买远程协助服务失败3，原因为：" + OperResult.GetMsg(OperResult.RESULTCODE_USER_NOT_EXIST));
                return OperResult.RESULTCODE_USER_NOT_EXIST;
            }

            int getShoppingCredits = (int)alipay.total_fee * GlobalConfig.GameConfig.RemoteServerRechargeReturnShoppingCreditsTimes;

            MyDBHelper.Instance.TransactionDataBaseOper(myTrans =>
            {
                result = playerRunner.BuyRemoteServer(alipay, serverType, myTrans);
                if (result != OperResult.RESULTCODE_TRUE)
                {
                    LogHelper.Instance.AddInfoLog("玩家 [" + alipay.user_name + "] 支付宝充值购买远程协助服务失败4，原因为：" + OperResult.GetMsg(result));
                    return result;
                }

                //玩家购买远程服务充值三层返利，
                if (!string.IsNullOrEmpty(playerRunner.BasePlayer.SimpleInfo.ReferrerUserName))
                {
                    //上一级
                    var parent1PlayerRunner = PlayerController.Instance.GetRunnable(playerRunner.BasePlayer.SimpleInfo.ReferrerUserName);
                    if (parent1PlayerRunner != null)
                    {
                        parent1PlayerRunner.BuyShoppingCreditAwardParent(getShoppingCredits * GlobalConfig.BuyRemoteServiceAwardRMBConfig[0], myTrans);

                        //上二级
                        if (!string.IsNullOrEmpty(parent1PlayerRunner.BasePlayer.SimpleInfo.ReferrerUserName))
                        {
                            var parent2PlayerRunner = PlayerController.Instance.GetRunnable(parent1PlayerRunner.BasePlayer.SimpleInfo.ReferrerUserName);
                            if (parent2PlayerRunner != null)
                            {
                                parent2PlayerRunner.BuyShoppingCreditAwardParent(getShoppingCredits * GlobalConfig.BuyRemoteServiceAwardRMBConfig[1], myTrans);

                                if (!string.IsNullOrEmpty(parent2PlayerRunner.BasePlayer.SimpleInfo.ReferrerUserName))
                                {
                                    var parent3PlayerRunner = PlayerController.Instance.GetRunnable(parent2PlayerRunner.BasePlayer.SimpleInfo.ReferrerUserName);
                                    if (parent3PlayerRunner != null)
                                    {
                                        parent3PlayerRunner.BuyShoppingCreditAwardParent(getShoppingCredits * GlobalConfig.BuyRemoteServiceAwardRMBConfig[2], myTrans);

                                    }
                                }
                            }
                        }
                    }
                }

                UserRemoteServerBuyRecord buyRecord = new UserRemoteServerBuyRecord()
                {
                    UserID = playerRunner.BasePlayer.SimpleInfo.UserID,
                    UserName = playerRunner.BasePlayer.SimpleInfo.UserLoginName,
                    OrderNumber = alipay.out_trade_no,
                    BuyRemoteServerTime = new MyDateTime(DateTime.Now),
                    ServerType = serverType,
                    PayMoneyYuan = (int)alipay.total_fee,
                    GetShoppingCredits = getShoppingCredits
                };
                DBProvider.UserRemoteServerDBProvider.SaveUserRemoteServerBuyRecord(buyRecord, myTrans);

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

        public int HandlePlayerRemoteService(string adminUserName, string playerUserName, string serviceContent, MyDateTime serviceTime, string engineerName)
        {
            var player = PlayerController.Instance.GetRunnable(playerUserName);
            if (player == null)
            {
                return OperResult.RESULTCODE_USER_NOT_EXIST;
            }
            UserRemoteHandleServiceRecord record = new UserRemoteHandleServiceRecord()
            {
                AdminUserName = adminUserName,
                ServiceContent = serviceContent,
                ServiceTime = serviceTime,
                UserName = playerUserName,
                UserID = player.BasePlayer.SimpleInfo.UserID,
                WorkerName = engineerName
            };

            int result = MyDBHelper.Instance.TransactionDataBaseOper(myTrans =>
            {
                int innerResult = player.HandlePlayerRemoteService(serviceTime, myTrans);
                if (innerResult != OperResult.RESULTCODE_TRUE)
                {
                    return innerResult;
                }

                DBProvider.UserRemoteServerDBProvider.AddUserRemoteHandleServiceRecord(record, myTrans);
                return OperResult.RESULTCODE_TRUE;
            },
            exc =>
            {
                LogHelper.Instance.AddErrorLog("HandlePlayerRemoteService Exception. adminUserName:" + adminUserName + ";playerUserName:" + playerUserName + ";serviceContent:" + serviceContent + ";serviceTime:" + serviceTime.ToString() + ";engineerName:" + engineerName, exc);
            });

            return result;
        }
    }
}
