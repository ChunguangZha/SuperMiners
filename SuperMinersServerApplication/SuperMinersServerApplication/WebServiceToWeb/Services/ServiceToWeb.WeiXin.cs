using DataBaseProvider;
using MetaData;
using MetaData.Trade;
using MetaData.User;
using SuperMinersServerApplication.Controller;
using SuperMinersServerApplication.Controller.Trade;
using SuperMinersServerApplication.Utility;
using SuperMinersServerApplication.WebServiceToWeb.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.WebServiceToWeb.Services
{
    public partial class ServiceToWeb : IServiceToWeb
    {

        public string GetAccessToken()
        {
            try
            {
                return App.TokenController.GetWeiXinAccessToken();
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("ServiceToWeb.WeiXin.GetAccessToken Exception. ", exc);

                return "";
            }
        }

        /// <summary>
        /// 绑定加登录
        /// </summary>
        /// <param name="wxUserOpenID"></param>
        /// <param name="wxUserName"></param>
        /// <param name="xlUserName"></param>
        /// <param name="xlUserPassword"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        public OperResultObject BindWeiXinUser(string wxUserOpenID, string wxUserName, string xlUserName, string xlUserPassword, string ip)
        {
            OperResultObject resultObj = new OperResultObject();
            try
            {
                int userID = DBProvider.UserDBProvider.JudgeWeiXinOpenIDorXLUserName_Binded(wxUserOpenID, xlUserName);
                if (userID > 0)
                {
                    resultObj.OperResultCode = OperResult.RESULTCODE_WEIXIN_USERBINDEDBYOTHER;
                    return resultObj;
                }

                return InnerBindWeixinUser(wxUserOpenID, wxUserName, xlUserName, xlUserPassword, ip);
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("微信端。绑定玩家信息异常。 wxUserOpenID: " + wxUserOpenID + "，绑定用户：[" + xlUserName + "]失败.", exc);
                resultObj.OperResultCode = OperResult.RESULTCODE_EXCEPTION;
                return resultObj;
            }
        }

        private OperResultObject InnerBindWeixinUser(string wxUserOpenID, string wxUserName, string xlUserName, string xlUserPassword, string ip)
        {
            OperResultObject resultObj = new OperResultObject();

            PlayerInfo player = DBProvider.UserDBProvider.GetPlayer(xlUserName);
            if (player == null)
            {
                resultObj.OperResultCode = OperResult.RESULTCODE_USERNAME_PASSWORD_ERROR;
                return resultObj;
            }
            if (xlUserPassword != player.SimpleInfo.Password)
            {
                resultObj.OperResultCode = OperResult.RESULTCODE_USERNAME_PASSWORD_ERROR;
                return resultObj;
            }


            int result = PlayerController.Instance.BindWeiXinUser(wxUserOpenID, player);
            if (result == OperResult.RESULTCODE_TRUE)
            {
                LogHelper.Instance.AddInfoLog("wxUserOpenID: " + wxUserOpenID + "，成功绑定用户：" + xlUserName);
                resultObj = PlayerController.Instance.CheckPlayerIsLocked(player.SimpleInfo.UserID, player.SimpleInfo.UserName);
                if (resultObj.OperResultCode != OperResult.RESULTCODE_TRUE)
                {
                    return resultObj;
                }
                
                string mac = "weixin";
                player.SimpleInfo.LastLoginIP = ip;
                player.SimpleInfo.LastLoginMac = mac;
                PlayerController.Instance.WeiXinLoginPlayer(wxUserOpenID, player);
                LogHelper.Instance.AddInfoLog("微信玩家 [" + wxUserName + "] 登录用户[" + player.SimpleInfo.UserName + "]成功, IP=" + ip + ", Mac=" + mac);
                result = OperResult.RESULTCODE_TRUE;
            }

            resultObj.OperResultCode = result;
            return resultObj;
        }

        /// <summary>
        /// 注册加绑定  RESULTCODE_REGISTER_USERNAME_LENGTH_SHORT; RESULTCODE_FALSE; RESULTCODE_REGISTER_USERNAME_EXIST; RESULTCODE_TRUE; RESULTCODE_EXCEPTION
        /// </summary>
        /// <param name="clientIP"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <param name="qq"></param>
        /// <param name="invitationCode"></param>
        /// <returns></returns>
        public int RegisterUserFromWeiXin(string wxUserOpenID, string wxUserName, string clientIP, string userName, string nickName, string password,
            string alipayAccount, string alipayRealName, string IDCardNo, string email, string qq, string invitationCode)
        {
            try
            {
                if (string.IsNullOrEmpty(wxUserOpenID))
                {
                    return OperResult.RESULTCODE_PARAM_INVALID;
                }
                if (string.IsNullOrEmpty(wxUserName))
                {
                    return OperResult.RESULTCODE_PARAM_INVALID;
                }

                var player = DBProvider.UserDBProvider.GetPlayerByWeiXinOpenID(wxUserOpenID);
                if (player != null)
                {
                    return OperResult.RESULTCODE_WEXIN_REGISTER_OPENID_EXIST;
                }

                if (string.IsNullOrEmpty(userName) || userName.Length < 3)
                {
                    return OperResult.RESULTCODE_REGISTER_USERNAME_LENGTH_SHORT;
                }
                if (string.IsNullOrEmpty(password))
                {
                    return OperResult.RESULTCODE_PARAM_INVALID;
                }
                //if (string.IsNullOrEmpty(alipayAccount))
                //{
                //    return OperResult.RESULTCODE_PARAM_INVALID;
                //}
                //if (string.IsNullOrEmpty(alipayRealName))
                //{
                //    return OperResult.RESULTCODE_PARAM_INVALID;
                //}
                //if (string.IsNullOrEmpty(IDCardNo))
                //{
                //    return OperResult.RESULTCODE_PARAM_INVALID;
                //}
                //if (string.IsNullOrEmpty(email))
                //{
                //    return OperResult.RESULTCODE_PARAM_INVALID;
                //}
                int result = PlayerController.Instance.RegisterUser(clientIP, userName, nickName, password, alipayAccount, alipayRealName, IDCardNo, email, qq, invitationCode);
                if (result == OperResult.RESULTCODE_TRUE)
                {
                    OperResultObject resultObj = InnerBindWeixinUser(wxUserOpenID, wxUserName, userName, password, clientIP);
                    result = resultObj.OperResultCode;
                }

                return result;
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("WeiXinRegisterUser Exception. wxUserOpenID: " + wxUserOpenID + ";wxUserName:"+ wxUserName + ";clientIP:" + clientIP + ";userName: " + userName + ";password: " + password
                                    + "alipayAccount:" + alipayAccount + ";alipayRealName:" + alipayRealName + ";IDCardNo:" + IDCardNo + ";email: " + email + ";qq: " + qq, exc);

                return OperResult.RESULTCODE_EXCEPTION;
            }
        }

        public OperResultObject WeiXinLogin(string wxUserOpenID, string wxUserName, string ip)
        {
            OperResultObject resultObj = new OperResultObject();
            string mac = "weixin";
            try
            {
                PlayerInfo player = DBProvider.UserDBProvider.GetPlayerByWeiXinOpenID(wxUserOpenID);
                if (player == null)
                {
                    resultObj.OperResultCode = OperResult.RESULTCODE_USER_NOT_EXIST;
                    return resultObj;
                }
                resultObj = PlayerController.Instance.CheckPlayerIsLocked(player.SimpleInfo.UserID, player.SimpleInfo.UserName);
                if (resultObj.OperResultCode != OperResult.RESULTCODE_TRUE)
                {
                    return resultObj;
                }
                
                player.SimpleInfo.LastLoginIP = ip;
                player.SimpleInfo.LastLoginMac = mac;
                PlayerController.Instance.WeiXinLoginPlayer(wxUserOpenID, player);

                LogHelper.Instance.AddInfoLog("微信玩家 [" + wxUserName + "] 登录用户[" + player .SimpleInfo.UserName+ "]成功, IP=" + ip + ", Mac=" + mac);

                resultObj.OperResultCode = OperResult.RESULTCODE_TRUE;
                return resultObj;
            }
            catch (Exception ex)
            {
                LogHelper.Instance.AddErrorLog("微信玩家 [" + wxUserName + "] 登录失败, IP=" + ip + ", Mac=" + mac, ex);
                resultObj.OperResultCode = OperResult.RESULTCODE_EXCEPTION;
                return resultObj;
            }
        }


        public MetaData.User.PlayerInfo GetPlayerByWeiXinOpenID(string openid)
        {
            try
            {
                return PlayerController.Instance.GetPlayerByWeiXinOpenID(openid);
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("微信端。ServiceToWeb.WeiXin.GetPlayerByWeiXinOpenID Exception openid=" + openid, exc);
                return null;
            }
        }

        public MetaData.User.PlayerInfo GetPlayerByXLUserName(string xlUserName)
        {
            try
            {
                return PlayerController.Instance.GetPlayerInfo(xlUserName);
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("微信端。ServiceToWeb.WeiXin.GetPlayerByXLUserName Exception xlUserName=" + xlUserName, exc);
                return null;
            }
        }

        public GatherTempOutputStoneResult GatherStones(string userName, decimal stones)
        {
            try
            {
                return PlayerController.Instance.GatherStones(userName, stones);
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("微信端。玩家[" + userName + "] 收取矿石异常，矿石数为:" + stones, exc);
                return null;
            }
        }

        public int BuyMiner(string userName, int minersCount)
        {
            try
            {
                return PlayerController.Instance.BuyMiner(userName, minersCount);
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("微信端。玩家[" + userName + "] 购买矿工异常，购买矿工数为:" + minersCount, exc);
                return 0;
            }
        }

        public TradeOperResult BuyMine(string userName, int minesCount, PayType payType)
        {
            TradeOperResult result = new TradeOperResult();
            try
            {
                result.PayType = (int)payType;
                if (minesCount <= 0)
                {
                    result.ResultCode = OperResult.RESULTCODE_PARAM_INVALID;
                    return result;
                }

                return OrderController.Instance.MineOrderController.BuyMine(userName, minesCount, (int)payType);
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("微信端。玩家[" + userName + "] 购买矿山异常，购买矿山数为:" + minesCount + ",支付类型为:" + ((PayType)payType).ToString(), exc);
                result.ResultCode = OperResult.RESULTCODE_EXCEPTION;
                return result;
            }
        }

        public TradeOperResult RechargeGoldCoin(string userName, int goldCoinCount, PayType payType)
        {
            try
            {
                TradeOperResult result = new TradeOperResult();
                result.PayType = (int)payType;
                result.TradeType = (int)AlipayTradeInType.BuyGoldCoin;

                int valueRMB = (int)Math.Ceiling(goldCoinCount / GlobalConfig.GameConfig.RMB_GoldCoin);
                return OrderController.Instance.GoldCoinOrderController.RechargeGoldCoin(userName, valueRMB, goldCoinCount, (int)payType);
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("微信端。玩家[" + userName + "] 金币充值异常，充值金币数为:" + goldCoinCount + ",支付类型为:" + (payType).ToString(), exc);
                return null;
            }
        }

        public OperResultObject WithdrawRMB(string userName, int getRMBCount)
        {
            OperResultObject resultObj = new OperResultObject();
            try
            {
                if (getRMBCount <= 0)
                {
                    resultObj.OperResultCode = OperResult.RESULTCODE_FALSE;
                }

                resultObj = PlayerController.Instance.CreateWithdrawRMB(userName, getRMBCount);
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("微信端。玩家[" + userName + "] 灵币提现异常，提现灵币为:" + getRMBCount, exc);
                resultObj.OperResultCode = OperResult.RESULTCODE_EXCEPTION;
            }

            return resultObj;
        }

        public int SellStones(string userName, int stoneCount)
        {
            if (stoneCount <= 0)
            {
                return OperResult.RESULTCODE_FALSE;
            }

            SellStonesOrder order = null;
            CustomerMySqlTransaction trans = null;
            try
            {
                trans = MyDBHelper.Instance.CreateTrans();
                PlayerRunnable playerrun = PlayerController.Instance.GetRunnable(userName);
                if (playerrun == null)
                {
                    return OperResult.RESULTCODE_USER_NOT_EXIST;
                }

                bool needUseQuan = OrderController.Instance.StoneOrderController.CheckNeedStoneSellQuan(playerrun.BasePlayer.SimpleInfo.UserID);

                order = OrderController.Instance.StoneOrderController.CreateSellOrder(userName, (int)playerrun.BasePlayer.FortuneInfo.Exp, playerrun.BasePlayer.FortuneInfo.CreditValue, stoneCount);
                if (order.ValueRMB <= 0)
                {
                    return OperResult.RESULTCODE_USER_OFFLINE;
                }

                int result = playerrun.SellStones(order, needUseQuan, trans);
                if (result != OperResult.RESULTCODE_TRUE)
                {
                    trans.Rollback();
                    return result;
                }
                OrderController.Instance.StoneOrderController.AddSellOrder(order, playerrun.BasePlayer.SimpleInfo.UserID, trans);
                trans.Commit();
                PlayerActionController.Instance.AddLog(userName, MetaData.ActionLog.ActionType.SellStone, stoneCount);

                return result;
            }
            catch (Exception exc)
            {
                string errMessage = "微信端。玩家[" + userName + "] 出售矿石异常。";
                if (order != null)
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

        public MinesBuyRecord[] GetBuyMineFinishedRecordList(string userName, int pageItemCount, int pageIndex)
        {
            try
            {
                return DBProvider.MineRecordDBProvider.GetAllMineTradeRecords(userName, null, null, pageItemCount, pageIndex);
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("微信端，玩家[" + userName + "] 获取所有矿山勘探记录异常", exc);
                return null;
            }
        }

        public MinersBuyRecord[] GetBuyMinerFinishedRecordList(string userName, int pageItemCount, int pageIndex)
        {
            try
            {
                return DBProvider.BuyMinerRecordDBProvider.GetFinishedBuyMinerRecordList(userName, null, null, pageItemCount, pageIndex);
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("微信端，玩家[" + userName + "] 获取所有矿工购买记录异常", exc);
                return null;
            }
        }

        public GoldCoinRechargeRecord[] GetFinishedGoldCoinRechargeRecordList(string userName, int pageItemCount, int pageIndex)
        {
            try
            {
                return DBProvider.GoldCoinRecordDBProvider.GetFinishedGoldCoinRechargeRecordList(userName, null, null, null, pageItemCount, pageIndex);
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("微信端，玩家[" + userName + "] 获取所有金币充值记录异常", exc);
                return null;
            }
        }

        public WithdrawRMBRecord[] GetWithdrawRMBRecordList(string userName, int pageItemCount, int pageIndex)
        {
            try
            {
                return DBProvider.WithdrawRMBRecordDBProvider.GetWithdrawRMBRecordList(-1, userName, null, null, null, null, null, pageItemCount, pageIndex);
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("微信端，玩家[" + userName + "] 获取所有灵币提现记录异常", exc);
                return null;
            }
        }

        public SellStonesOrder[] GetUserSellStoneOrders(string sellerUserName, int pageItemCount, int pageIndex)
        {
            try
            {
                return DBProvider.StoneOrderDBProvider.GetSellOrderList(sellerUserName, null, -1, null, null, pageItemCount, pageIndex);
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("微信端，玩家[" + sellerUserName + "] 查询矿石出售历史订单异常", exc);
                return null;
            }
        }

        public BuyStonesOrder[] GetUserBuyStoneOrders(string buyUserName, int pageItemCount, int pageIndex)
        {
            try
            {
                return DBProvider.StoneOrderDBProvider.GetBuyStonesOrderList("", "", buyUserName, -1, null, null, null, null, pageItemCount, pageIndex);
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("微信端，玩家[" + buyUserName + "] 查询矿石购买历史订单异常", exc);
                return null;
            }
        }

        public SellStonesOrder[] GetAllNotFinishedSellOrders(string userName)
        {
            try
            {
                return OrderController.Instance.StoneOrderController.GetSellOrders((int)SellOrderState.Wait);
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("微信端，玩家[" + userName + "] 获取所有未完成的矿石订单异常", exc);
                return null;
            }
        }

        public int BuyStone(string userName, string stoneOrderNumber)
        {
            try
            {
                var lockedOrder = OrderController.Instance.StoneOrderController.LockSellStone(userName, stoneOrderNumber);
                if (lockedOrder == null)
                {
                    return OperResult.RESULTCODE_ORDER_BE_LOCKED_BY_OTHER;
                }

                int result = OrderController.Instance.StoneOrderController.PayStoneOrderByRMB(userName, stoneOrderNumber, lockedOrder.StonesOrder.ValueRMB);
                if (result != OperResult.RESULTCODE_TRUE)
                {
                    OrderController.Instance.StoneOrderController.ReleaseLockSellOrder(stoneOrderNumber);
                }

                return result;
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("微信端，玩家[" + userName + "] 获取所有未完成的矿石订单异常", exc);
                return OperResult.RESULTCODE_EXCEPTION;
            }
        }
    }
}
