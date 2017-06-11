using MetaData;
using MetaData.StoneFactory;
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
        public StoneFactorySystemDailyProfit[] GetStoneFactorySystemDailyProfitList(string token, int pageItemCount, int pageIndex)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    return DBProvider.PlayerStoneFactoryDBProvider.GetFactorySystemDailyProfitRecords(pageItemCount, pageIndex);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("GetStoneFactorySystemDailyProfitList Exception. ClientIP=" + ClientManager.GetClientIP(token), exc);
                    return null;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public PlayerStoneFactoryAccountInfo GetPlayerStoneFactoryAccountInfo(string token, int userID)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    return StoneFactoryController.Instance.GetPlayerStoneFactoryAccountInfo(userID);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("GetPlayerStoneFactoryAccountInfo Exception. ClientIP=" + ClientManager.GetClientIP(token), exc);
                    return null;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public StoneFactoryProfitRMBChangedRecord[] GetStoneFactoryProfitRMBChangedRecordList(string token, int userID, MyDateTime beginTime, MyDateTime endTime, int pageItemCount, int pageIndex)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    return DBProvider.PlayerStoneFactoryDBProvider.GetProfitRecords(userID, beginTime, endTime, pageItemCount, pageIndex);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("GetStoneFactoryProfitRMBChangedRecordList Exception. ClientIP=" + ClientManager.GetClientIP(token), exc);
                    return null;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public int AddStoneToFactory(string token, int userID, string userName, int stoneStackCount)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    return StoneFactoryController.Instance.AddStoneToFactory(userID, userName, stoneStackCount);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("AddStoneToFactory Exception. ClientIP=" + ClientManager.GetClientIP(token), exc);
                    return OperResult.RESULTCODE_EXCEPTION;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public int AddMinersToFactory(string token, int userID, string userName, int minersGroupCount)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    return StoneFactoryController.Instance.AddMinersToFactory(userID, userName, minersGroupCount);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("AddMinersToFactory Exception. ClientIP=" + ClientManager.GetClientIP(token), exc);
                    return OperResult.RESULTCODE_EXCEPTION;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public int WithdrawOutputRMBFromFactory(string token, int userID, string userName, decimal withdrawRMBCount)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    return StoneFactoryController.Instance.WithdrawOutputRMB(userID, userName, withdrawRMBCount);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("WithdrawOutputRMBFromFactory Exception. ClientIP=" + ClientManager.GetClientIP(token), exc);
                    return OperResult.RESULTCODE_EXCEPTION;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public int WithdrawStoneFromFactory(string token, int userID, string userName, int stoneStackCount)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    return StoneFactoryController.Instance.WithdrawStone(userID, userName, stoneStackCount);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("WithdrawStoneFromFactory Exception. ClientIP=" + ClientManager.GetClientIP(token), exc);
                    return OperResult.RESULTCODE_EXCEPTION;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public int FeedSlave(string token, int userID)
        {
            if (RSAProvider.LoadRSA(token))
            {
                try
                {
                    return StoneFactoryController.Instance.FeedSlave(userID);
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("FeedSlave Exception. ClientIP=" + ClientManager.GetClientIP(token), exc);
                    return OperResult.RESULTCODE_EXCEPTION;
                }
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
