using DataBaseProvider;
using MetaData;
using MetaData.StoneFactory;
using MetaData.SystemConfig;
using MetaData.User;
using SuperMinersServerApplication.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.Controller
{
    /// <summary>
    /// 10000矿石为一股投入到加工厂（存储必须1万的倍数），加工厂凝练每天分配给相应比例的灵币，
    /// 18天后可以转入灵币账户进行提现或消费操作，凝练10000矿石需要100矿工，需把矿工转入加工厂不可转回，
    /// 加工厂的矿工为苦力，每48小时需要喂食一次食物（积分商城卖食物），超过48小时苦力将会死亡。。。
    /// </summary>
    public class StoneFactoryController
    {
        //凝练按天计算，每天0点计算前一天数值，为了简化计算，当前投入的矿石，矿工，食物，都是以第二天0时开始计算。
        //每天23：50分到次到1点禁止充值矿石、矿工、食物。

        public PlayerStoneFactoryAccountInfo GetPlayerStoneFactoryAccountInfo(int userID)
        {
            return DBProvider.PlayerStoneFactoryDBProvider.GetPlayerStoneFactoryAccountInfo(userID);
        }

        public StoneFactoryProfitRMBChangedRecord[] GetProfitRecords(int userID, MyDateTime beginBuyTime, MyDateTime endBuyTime, int pageItemCount, int pageIndex)
        {
            return DBProvider.PlayerStoneFactoryDBProvider.GetProfitRecords(userID, beginBuyTime, endBuyTime, pageItemCount, pageIndex);
        }

        public int OpenFactory(int userID, string userName)
        {
            //工厂开启状态。开启一次 1000积分。72小时 没有存入矿石和苦力 就 在关闭
            int result = MyDBHelper.Instance.TransactionDataBaseOper(myTrans =>
            {
                int operResult = PlayerController.Instance.OpenFactory(userID, userName, myTrans);
                if (operResult == OperResult.RESULTCODE_TRUE)
                {
                    bool isOK = DBProvider.PlayerStoneFactoryDBProvider.OpenFactory(userID, myTrans);
                    return isOK ? OperResult.RESULTCODE_TRUE : OperResult.RESULTCODE_FALSE;
                }
                return operResult;
            }, exc =>
            {
                PlayerController.Instance.RollbackUserFromDB(userName);
            });

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="userName"></param>
        /// <param name="stoneStackCount">矿石股数（一万矿石为一股）</param>
        /// <returns></returns>
        public int AddStone(int userID, string userName, int stoneStackCount)
        {
            //1万矿石可以投入一股，30天后，可撤回到玩家矿石账户
            PlayerRunnable playerrunner = PlayerController.Instance.GetRunnable(userName);
            if (playerrunner == null)
            {
                return OperResult.RESULTCODE_USER_NOT_EXIST;
            }
            if (playerrunner.BasePlayer.FortuneInfo.StockOfStones - playerrunner.BasePlayer.FortuneInfo.FreezingStones < (stoneStackCount * StoneFactoryConfig.StoneFactoryStone_Stack))
            {
                return OperResult.RESULTCODE_LACK_OF_BALANCE;
            }

            int result = MyDBHelper.Instance.TransactionDataBaseOper(myTrans =>
            {
                result = playerrunner.JoinStoneToFactory(stoneStackCount, myTrans);
                if (result == OperResult.RESULTCODE_TRUE)
                {
                    //只需要添加矿石存入记录。
                    StoneFactoryStackChangeRecord record = new StoneFactoryStackChangeRecord()
                    {
                        UserID = userID,
                        JoinStoneStackCount = stoneStackCount,
                        Time = new MyDateTime(DateTime.Now)
                    };
                    bool isOK = DBProvider.PlayerStoneFactoryDBProvider.AddNewStackChangeRecord(record, myTrans);
                    if (isOK)
                    {
                        return OperResult.RESULTCODE_TRUE;
                    }
                    return OperResult.RESULTCODE_FALSE;
                }
                return result;
            },
            exc =>
            {
                PlayerController.Instance.RollbackUserFromDB(userName);
            });

            return OperResult.RESULTCODE_FALSE;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="stoneStackCount">矿石股数（一万矿石为一股）</param>
        /// <returns></returns>
        public int WithdrawStone(int userID, string userName, int stoneStackCount)
        {
            PlayerRunnable playerrunner = PlayerController.Instance.GetRunnable(userName);
            if (playerrunner == null)
            {
                return OperResult.RESULTCODE_USER_NOT_EXIST;
            }
            var playerFactoryAccountInfo = this.GetPlayerStoneFactoryAccountInfo(userID);
            if (playerFactoryAccountInfo == null)
            {
                return OperResult.RESULTCODE_STONEFACTORYISCLOSED;
            }

            if (playerFactoryAccountInfo.WithdrawableStackCount < stoneStackCount)
            {
                return OperResult.RESULTCODE_LACK_OF_BALANCE;
            }

            int result = MyDBHelper.Instance.TransactionDataBaseOper(myTrans =>
            {
                playerFactoryAccountInfo.WithdrawableStackCount -= stoneStackCount;
                playerFactoryAccountInfo.TotalStackCount -= stoneStackCount;

                result = playerrunner.WithdrawStoneFromFactory(stoneStackCount, myTrans);
                if (result == OperResult.RESULTCODE_TRUE)
                {
                    //添加矿石取出记录
                    StoneFactoryStackChangeRecord record = new StoneFactoryStackChangeRecord()
                    {
                        UserID = userID,
                        JoinStoneStackCount = -stoneStackCount,
                        Time = new MyDateTime(DateTime.Now)
                    };
                    bool isOK = DBProvider.PlayerStoneFactoryDBProvider.AddNewStackChangeRecord(record, myTrans);
                    if (isOK)
                    {
                        return OperResult.RESULTCODE_TRUE;
                    }
                    return OperResult.RESULTCODE_FALSE;
                }
                return result;
            },
            exc =>
            {
                PlayerController.Instance.RollbackUserFromDB(userName);
            });


            return OperResult.RESULTCODE_FALSE;
        }

        /// <summary>
        /// 100个矿工为一组奴隶
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="userName"></param>
        /// <param name="minersGroupCount">100个矿工为一组奴隶</param>
        /// <returns></returns>
        public int AddMiners(int userID, string userName, int minersGroupCount)
        {
            if (minersGroupCount < 1)
            {
                return OperResult.RESULTCODE_PARAM_INVALID;
            }
            PlayerRunnable playerrunner = PlayerController.Instance.GetRunnable(userName);
            if (playerrunner == null)
            {
                return OperResult.RESULTCODE_USER_NOT_EXIST;
            }
            if (playerrunner.BasePlayer.FortuneInfo.MinersCount <= minersGroupCount * StoneFactoryConfig.OneGroupSlaveHasMiners)
            {
                return OperResult.RESULTCODE_MINERS_LACK_OF_BALANCE;
            }

            PlayerStoneFactoryAccountInfo factoryAccount = DBProvider.PlayerStoneFactoryDBProvider.GetPlayerStoneFactoryAccountInfo(userID);
            if (factoryAccount == null || !factoryAccount.FactoryIsOpening || factoryAccount.FactoryLiveDays <= 0)
            {
                return OperResult.RESULTCODE_STONEFACTORYISCLOSED;
            }

            int result = MyDBHelper.Instance.TransactionDataBaseOper(myTrans =>
            {
                result = playerrunner.TransferMinersToFactory(minersGroupCount * StoneFactoryConfig.OneGroupSlaveHasMiners, myTrans);
                if (result == OperResult.RESULTCODE_TRUE)
                {
                    factoryAccount.FreezingSlaveGroupCount += minersGroupCount;
                    //新增奴隶自带2天食物
                    factoryAccount.Food += (minersGroupCount * StoneFactoryConfig.SlaveDefaultLiveDays);

                    bool isOK = DBProvider.PlayerStoneFactoryDBProvider.SavePlayerStoneFactoryAccountInfo(factoryAccount, myTrans);
                    return isOK ? OperResult.RESULTCODE_TRUE : OperResult.RESULTCODE_FALSE;
                }

                return result;
            },
            exc =>
            {
                PlayerController.Instance.RollbackUserFromDB(userName);
            });

            return result;
        }

        public int AddFoods(int userID, int foodsCount, CustomerMySqlTransaction myTrans)
        {
            PlayerStoneFactoryAccountInfo factoryAccount = DBProvider.PlayerStoneFactoryDBProvider.GetPlayerStoneFactoryAccountInfo(userID);
            if (factoryAccount == null || !factoryAccount.FactoryIsOpening || factoryAccount.FactoryLiveDays <= 0)
            {
                return OperResult.RESULTCODE_STONEFACTORYISCLOSED;
            }

            factoryAccount.Food += foodsCount;

            bool isOK = DBProvider.PlayerStoneFactoryDBProvider.SavePlayerStoneFactoryAccountInfo(factoryAccount, myTrans);
            return isOK ? OperResult.RESULTCODE_TRUE : OperResult.RESULTCODE_FALSE;
        }

        public int WithdrawOutputRMB(int userID, string userName, int withdrawRMBCount)
        {
            PlayerRunnable playerrunner = PlayerController.Instance.GetRunnable(userName);
            if (playerrunner == null)
            {
                return OperResult.RESULTCODE_USER_NOT_EXIST;
            }
            var playerFactoryAccountInfo = this.GetPlayerStoneFactoryAccountInfo(userID);
            if (playerFactoryAccountInfo == null)
            {
                return OperResult.RESULTCODE_STONEFACTORYISCLOSED;
            }
            if (playerFactoryAccountInfo.WithdrawableProfitRMB < withdrawRMBCount)
            {
                return OperResult.RESULTCODE_LACK_OF_BALANCE;
            }

            int result = MyDBHelper.Instance.TransactionDataBaseOper(myTrans =>
            {
                //添加提现记录
                StoneFactoryProfitRMBChangedRecord record = new StoneFactoryProfitRMBChangedRecord()
                {
                    UserID = userID,
                    OperRMB = -withdrawRMBCount,
                    OperTime = new MyDateTime(DateTime.Now),
                    ProfitType = FactoryProfitOperType.WithdrawRMB
                };

                bool isOK = DBProvider.PlayerStoneFactoryDBProvider.AddProfitRMBChangedRecord(record, myTrans);
                if (isOK)
                {
                    return playerrunner.WithdrawRMBFromFactory(withdrawRMBCount, myTrans);
                }

                return OperResult.RESULTCODE_FALSE;
            },
            exc =>
            {
                PlayerController.Instance.RollbackUserFromDB(userName);
            });


            return result;
        }

        public void DailyCheck()
        {
            PlayerStoneFactoryAccountInfo[] listAllFactories = DBProvider.PlayerStoneFactoryDBProvider.GetAllPlayerStoneFactoryAccountInfos();

            int result = MyDBHelper.Instance.TransactionDataBaseOper(myTrans =>
            {
                foreach (var factory in listAllFactories)
                {
                    if (factory.FactoryIsOpening && factory.FactoryLiveDays > 0)
                    {
                        //1份食物：1组奴隶：1股矿石
                        //计算前一天存活的奴隶
                        int workableGroupSlaveCount = factory.EnableSlavesGroupCount < factory.Food ? factory.EnableSlavesGroupCount : factory.Food;
                        int deadGroupSlaveCount = factory.EnableSlavesGroupCount - workableGroupSlaveCount;
                        //计算前一天有效矿石。
                        factory.LastDayValidStoneStack = workableGroupSlaveCount < factory.TotalStackCount ? workableGroupSlaveCount : factory.TotalStackCount;

                        //减去前一天没有食物死掉的奴隶
                        factory.EnableSlavesGroupCount -= deadGroupSlaveCount;
                        //将前一天存入的冻结中的奴隶转成可用
                        factory.EnableSlavesGroupCount += factory.FreezingSlaveGroupCount;
                        factory.FreezingSlaveGroupCount = 0;
                        factory.Food -= workableGroupSlaveCount;

                        //检查工厂状态，如果没有奴隶和矿工则工厂生存天数自减1，如果生存天数为0，则工厂关闭。
                        if (factory.EnableSlavesGroupCount == 0 && factory.FreezingSlaveGroupCount == 0 && factory.TotalStackCount == 0 && factory.FreezingStackCount == 0)
                        {
                            factory.FactoryLiveDays--;
                            if (factory.FactoryLiveDays <= 0)
                            {
                                factory.FactoryIsOpening = false;
                            }
                        }
                        else
                        {
                            factory.FactoryLiveDays = StoneFactoryConfig.FactoryLiveDays;
                        }

                        DBProvider.PlayerStoneFactoryDBProvider.SavePlayerStoneFactoryAccountInfo(factory, myTrans);
                    }
                }

                return OperResult.RESULTCODE_TRUE;
            },
            exc =>
            {
                LogHelper.Instance.AddErrorLog("矿石工厂零时处理异常", exc);
            });

        }

        public int AdminSetProfitRate(decimal profitRate)
        {
            DBProvider.PlayerStoneFactoryDBProvider.AddStoneFactorySystemDailyProfit(new StoneFactorySystemDailyProfit() { Day = new MyDateTime(DateTime.Now), profitRate = profitRate });

            PlayerStoneFactoryAccountInfo[] listAllFactories = DBProvider.PlayerStoneFactoryDBProvider.GetAllPlayerStoneFactoryAccountInfos();
            foreach (var factory in listAllFactories)
            {
                if (!factory.FactoryIsOpening || factory.FactoryLiveDays <= 0)
                {
                    continue;
                }

                PlayerInfo currentPlayer = DBProvider.UserDBProvider.GetPlayerByUserID(factory.UserID);
                PlayerInfo Level1ReferPlayerInfo = null;
                PlayerInfo Level2ReferPlayerInfo = null;
                PlayerInfo Level3ReferPlayerInfo = null;
                if (!string.IsNullOrEmpty(currentPlayer.SimpleInfo.ReferrerUserName))
                {
                    Level1ReferPlayerInfo = DBProvider.UserDBProvider.GetPlayerByUserName(currentPlayer.SimpleInfo.ReferrerUserName);
                    if (Level1ReferPlayerInfo != null && !string.IsNullOrEmpty(Level1ReferPlayerInfo.SimpleInfo.ReferrerUserName))
                    {
                        Level2ReferPlayerInfo = DBProvider.UserDBProvider.GetPlayerByUserName(Level1ReferPlayerInfo.SimpleInfo.ReferrerUserName);
                        if (Level2ReferPlayerInfo != null && !string.IsNullOrEmpty(Level2ReferPlayerInfo.SimpleInfo.ReferrerUserName))
                        {
                            Level3ReferPlayerInfo = DBProvider.UserDBProvider.GetPlayerByUserName(Level2ReferPlayerInfo.SimpleInfo.ReferrerUserName);
                        }
                    }
                }

                decimal profitRMB = factory.LastDayValidStoneStack * profitRate;

                MyDBHelper.Instance.TransactionDataBaseOper(myTrans =>
                {
                    bool isOK = DBProvider.PlayerStoneFactoryDBProvider.AddProfitRMBChangedRecord(new StoneFactoryProfitRMBChangedRecord()
                    {
                        UserID = factory.UserID,
                        ProfitType = FactoryProfitOperType.FactoryOutput,
                        OperRMB = profitRMB,
                        OperTime = new MyDateTime(DateTime.Now)
                    }, myTrans);
                    if (!isOK)
                    {
                        return OperResult.RESULTCODE_FALSE;
                    }

                    if (Level1ReferPlayerInfo != null)
                    {
                        isOK = DBProvider.PlayerStoneFactoryDBProvider.AddProfitRMBChangedRecord(new StoneFactoryProfitRMBChangedRecord()
                        {
                            UserID = Level1ReferPlayerInfo.SimpleInfo.UserID,
                            ProfitType = FactoryProfitOperType.ReferenceAward,
                            OperRMB = profitRMB * StoneFactoryConfig.FactoryOutputProfitAwardRMBConfig[0],
                            OperTime = new MyDateTime(DateTime.Now)
                        }, myTrans);
                        if (!isOK)
                        {
                            return OperResult.RESULTCODE_FALSE;
                        }
                    }
                    if (Level2ReferPlayerInfo != null)
                    {
                        isOK = DBProvider.PlayerStoneFactoryDBProvider.AddProfitRMBChangedRecord(new StoneFactoryProfitRMBChangedRecord()
                        {
                            UserID = Level2ReferPlayerInfo.SimpleInfo.UserID,
                            ProfitType = FactoryProfitOperType.ReferenceAward,
                            OperRMB = profitRMB * StoneFactoryConfig.FactoryOutputProfitAwardRMBConfig[1],
                            OperTime = new MyDateTime(DateTime.Now)
                        }, myTrans);
                        if (!isOK)
                        {
                            return OperResult.RESULTCODE_FALSE;
                        }
                    }
                    if (Level3ReferPlayerInfo != null)
                    {
                        isOK = DBProvider.PlayerStoneFactoryDBProvider.AddProfitRMBChangedRecord(new StoneFactoryProfitRMBChangedRecord()
                        {
                            UserID = Level3ReferPlayerInfo.SimpleInfo.UserID,
                            ProfitType = FactoryProfitOperType.ReferenceAward,
                            OperRMB = profitRMB * StoneFactoryConfig.FactoryOutputProfitAwardRMBConfig[2],
                            OperTime = new MyDateTime(DateTime.Now)
                        }, myTrans);
                        if (!isOK)
                        {
                            return OperResult.RESULTCODE_FALSE;
                        }
                    }

                    return OperResult.RESULTCODE_TRUE;
                },
                exc =>
                {
                    LogHelper.Instance.AddErrorLog("管理员设置工厂收益异常，玩家ID：" + factory.UserID, exc);
                });

            }

            return OperResult.RESULTCODE_TRUE;
        }

    }
}
