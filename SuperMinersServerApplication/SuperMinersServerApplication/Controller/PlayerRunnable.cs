using DataBaseProvider;
using MetaData;
using MetaData.SystemConfig;
using MetaData.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.Controller
{
    public class PlayerRunnable
    {
        public PlayerInfo BasePlayer { get; private set; }
        private object _lockSimpleAction = new object();
        private object _lockFortuneAction = new object();

        public PlayerRunnable(PlayerInfo player)
        {
            BasePlayer = player;
        }

        public float MaxTempOutput
        {
            get
            {
                return GlobalConfig.GameConfig.TempStoneOutputValidHour * BasePlayer.FortuneInfo.MinersCount * GlobalConfig.GameConfig.OutputStonesPerHour;
            }
        }

        public bool SaveSimpleInfoToDB()
        {
            return DBProvider.UserDBProvider.SavePlayerSimpleInfo(BasePlayer.SimpleInfo);
        }

        public void RefreshFortune()
        {
            BasePlayer.FortuneInfo = DBProvider.UserDBProvider.GetPlayerFortuneInfo(BasePlayer.SimpleInfo.UserName);
            ComputePlayerOfflineStoneOutput();
        }

        public void ComputePlayerOfflineStoneOutput()
        {
            if (BasePlayer.SimpleInfo.LastLogOutTime == PlayerSimpleInfo.INVALIDDATETIME)
            {
                //表示该玩家之前没有登录过
                BasePlayer.FortuneInfo.TempOutputStones = 0;
                return;
            }
            TimeSpan span = BasePlayer.SimpleInfo.LastLoginTime - BasePlayer.SimpleInfo.LastLogOutTime;
            if (span.TotalHours < 0)
            {
                return;
            }

            lock (this._lockFortuneAction)
            {
                float tempOutput = (float)span.TotalHours * BasePlayer.FortuneInfo.MinersCount * GlobalConfig.GameConfig.OutputStonesPerHour;

                if (tempOutput > MaxTempOutput)
                {
                    tempOutput = MaxTempOutput;
                }
                if (tempOutput > BasePlayer.FortuneInfo.StonesReserves - BasePlayer.FortuneInfo.TotalProducedStonesCount)
                {
                    tempOutput = BasePlayer.FortuneInfo.StonesReserves - BasePlayer.FortuneInfo.TotalProducedStonesCount;
                }
                BasePlayer.FortuneInfo.TempOutputStones = tempOutput;
            }
        }

        /// <summary>
        /// 收取生产出来的矿石
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="tempStoneOutput"></param>
        /// <returns></returns>
        public float GatherStones(float tempStoneOutput)
        {
            TimeSpan span = DateTime.Now - BasePlayer.SimpleInfo.LastLoginTime;
            if (span.TotalHours > 0)
            {
                lock (this._lockFortuneAction)
                {
                    if (tempStoneOutput > MaxTempOutput)
                    {
                        tempStoneOutput = MaxTempOutput;
                    }

                    //TimeSpan onlineTimeSpan = DateTime.Now - BasePlayer.SimpleInfo.LastLoginTime;
                    //float tempStoneOutputFromServer = 
                    //拿服务器计算出的产出值和客户端传入的产出值进行比较，如果误差在一个小时产量内，则以客户端值为准，否则以服务器端为准。
                    if (Math.Abs(BasePlayer.FortuneInfo.TempOutputStones + tempOnlineOutput - tempStoneOutput) < playerOutputPerHour)
                    {
                        BasePlayer.FortuneInfo.TempOutputStones = tempStoneOutput;
                    }
                    else
                    {
                        BasePlayer.FortuneInfo.TempOutputStones = BasePlayer.FortuneInfo.TempOutputStones + tempOnlineOutput;
                    }

                    if (BasePlayer.FortuneInfo.TotalProducedStonesCount + BasePlayer.FortuneInfo.TempOutputStones > BasePlayer.FortuneInfo.StonesReserves)
                    {
                        //已经超出矿山储量
                        BasePlayer.FortuneInfo.TempOutputStones = BasePlayer.FortuneInfo.StonesReserves - BasePlayer.FortuneInfo.TotalProducedStonesCount;
                    }

                    BasePlayer.FortuneInfo.StockOfStones += BasePlayer.FortuneInfo.TempOutputStones;
                    BasePlayer.FortuneInfo.TotalProducedStonesCount += BasePlayer.FortuneInfo.TempOutputStones;

                    float stones = BasePlayer.FortuneInfo.TempOutputStones;
                    BasePlayer.FortuneInfo.TempOutputStones = 0;
                    DBProvider.UserDBProvider.SavePlayerFortuneInfo(BasePlayer.FortuneInfo);
                    return stones;
                }
            }

            return 0;
        }

        public int BuyMiner(int minersCount)
        {
            lock (_lockFortuneAction)
            {
                float allGoldCoin = BasePlayer.FortuneInfo.GoldCoin + BasePlayer.FortuneInfo.RMB * GlobalConfig.GameConfig.RMB_GoldCoin;
                float allNeedGoldCoin = minersCount * GlobalConfig.GameConfig.GoldCoin_Miner;
                if (allNeedGoldCoin > allGoldCoin)
                {
                    return 0;
                }

                if (allNeedGoldCoin < BasePlayer.FortuneInfo.GoldCoin)
                {
                    BasePlayer.FortuneInfo.GoldCoin -= allNeedGoldCoin;
                }
                else
                {
                    float gc = allNeedGoldCoin - BasePlayer.FortuneInfo.GoldCoin;
                    int needRMB = (int)Math.Ceiling(gc / GlobalConfig.GameConfig.RMB_GoldCoin);
                    if (needRMB > BasePlayer.FortuneInfo.RMB)
                    {
                        return 0;
                    }

                    BasePlayer.FortuneInfo.RMB -= needRMB;
                    BasePlayer.FortuneInfo.GoldCoin = needRMB * GlobalConfig.GameConfig.RMB_GoldCoin - gc;
                }
                BasePlayer.FortuneInfo.MinersCount += minersCount;
                if (!DBProvider.UserDBProvider.SavePlayerFortuneInfo(BasePlayer.FortuneInfo))
                {
                    RefreshFortune();
                    return -1;
                }

                return minersCount;
            }
        }

        /// <summary>
        /// 矿山只能用RMB购买
        /// </summary>
        /// <param name="minesCount"></param>
        /// <returns></returns>
        public int BuyMine(int minesCount)
        {
            lock (_lockFortuneAction)
            {
                float needRMB = minesCount * GlobalConfig.GameConfig.RMB_Mine;
                if (needRMB > BasePlayer.FortuneInfo.RMB)
                {
                    return 0;
                }

                BasePlayer.FortuneInfo.RMB -= needRMB;
                BasePlayer.FortuneInfo.MinesCount += minesCount;
                BasePlayer.FortuneInfo.StonesReserves += minesCount * GlobalConfig.GameConfig.StonesReservesPerMines;
                if (!DBProvider.UserDBProvider.SavePlayerFortuneInfo(BasePlayer.FortuneInfo))
                {
                    RefreshFortune();
                    return -1;
                }

                return minesCount;
            }
        }

        #region 取消充值功能
        public bool RechargeGoldCoin(float yuan)
        {
            lock (_lockFortuneAction)
            {
                GoldCoinRechargeRecord record = new GoldCoinRechargeRecord()
                {
                    UserName = BasePlayer.SimpleInfo.UserName,
                    RechargeMoney = yuan,
                    GainGoldCoin = yuan * GlobalConfig.GameConfig.Yuan_RMB * GlobalConfig.GameConfig.RMB_GoldCoin,
                    Time = DateTime.Now
                };
                BasePlayer.FortuneInfo.GoldCoin += record.GainGoldCoin;

                var trans = MyDBHelper.Instance.CreateTrans();
                try
                {
                    DBProvider.UserDBProvider.SavePlayerFortuneInfo(BasePlayer.FortuneInfo, trans);
                    DBProvider.RechargeDBProvider.AddRechargeGoldCoinRecord(record, trans);
                    trans.Commit();
                    return true;
                }
                catch (Exception exc)
                {
                    trans.Rollback();
                    RefreshFortune();
                    throw exc;
                }
                finally
                {
                    trans.Dispose();
                }
            }
        }

        public bool RechargeRMB(float yuan)
        {
            lock (_lockFortuneAction)
            {
                RMBRechargeRecord record = new RMBRechargeRecord()
                {
                    UserName = BasePlayer.SimpleInfo.UserName,
                    RechargeMoney = yuan,
                    GainRMB = yuan * GlobalConfig.GameConfig.Yuan_RMB,
                    Time = DateTime.Now
                };
                BasePlayer.FortuneInfo.RMB += record.GainRMB;

                var trans = MyDBHelper.Instance.CreateTrans();
                try
                {
                    DBProvider.UserDBProvider.SavePlayerFortuneInfo(BasePlayer.FortuneInfo, trans);
                    DBProvider.RechargeDBProvider.AddRechargeRMBRecord(record, trans);
                    trans.Commit();
                    return true;
                }
                catch (Exception exc)
                {
                    trans.Rollback();
                    RefreshFortune();
                    throw exc;
                }
                finally
                {
                    trans.Dispose();
                }
            }
        }
        #endregion

        public bool ReferAward(AwardReferrerConfig awardConfig, CustomerMySqlTransaction trans)
        {
            PlayerFortuneInfo newFortuneInfo = null;
            lock (_lockFortuneAction)
            {
                //此处不直接修改内存对象，先修改数据库，如果成功，重新从数据库加载，否则不动内存！
                newFortuneInfo = BasePlayer.FortuneInfo.CopyTo();
            }

            newFortuneInfo.Exp += awardConfig.AwardReferrerExp;
            newFortuneInfo.GoldCoin += awardConfig.AwardReferrerGoldCoin;
            newFortuneInfo.MinersCount += awardConfig.AwardReferrerMiners;
            newFortuneInfo.MinesCount += awardConfig.AwardReferrerMines;
            newFortuneInfo.StockOfStones += awardConfig.AwardReferrerStones;
            return DBProvider.UserDBProvider.SavePlayerFortuneInfo(newFortuneInfo, trans);
        }
    }
}
