using MetaData;
using MetaData.Trade;
using SuperMinersServerApplication.Controller;
using SuperMinersServerApplication.Encoder;
using SuperMinersServerApplication.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.WebService.Services
{
    public partial class ServiceToClient
    {
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

    }
}
