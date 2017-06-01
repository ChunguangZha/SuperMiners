using DataBaseProvider;
using MetaData;
using MetaData.StoneFactory;
using MetaData.SystemConfig;
using MetaData.User;
using SuperMinersServerApplication.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SuperMinersServerApplication.Controller
{
    /// <summary>
    /// 10000矿石为一股投入到加工厂（存储必须1万的倍数），加工厂凝练每天分配给相应比例的灵币，
    /// 18天后可以转入灵币账户进行提现或消费操作，凝练10000矿石需要100矿工，需把矿工转入加工厂不可转回，
    /// 加工厂的矿工为苦力，每48小时需要喂食一次食物（积分商城卖食物），超过48小时苦力将会死亡。。。
    /// </summary>
    public class StoneFactoryController
    {
        #region Single

        private static StoneFactoryController _instance = new StoneFactoryController();

        internal static StoneFactoryController Instance
        {
            get
            {
                return _instance;
            }
        }

        #endregion

        public void Init()
        {
            SchedulerTaskController.Instance.JoinTask(new DailyTimerTask()
            {
                DailyTime = new DateTime(2000, 1, 1, 0, 16, 0),
                Task = DailyCheckFactoryState
            });

            SchedulerTaskController.Instance.JoinTask(new DailyTimerTask()
            {
                DailyTime = new DateTime(2000, 1, 1, 14, 0, 0),
                Task = DailySetProfit
            });

        }

        //凝练按天计算，每天0点计算前一天数值，为了简化计算，当前投入的矿石，矿工，食物，都是以第二天0时开始计算。
        //每天23：50分到次到1点禁止充值矿石、矿工、食物。

        public PlayerStoneFactoryAccountInfo GetPlayerStoneFactoryAccountInfo(int userID)
        {
            PlayerStoneFactoryAccountInfo account = DBProvider.PlayerStoneFactoryDBProvider.GetPlayerStoneFactoryAccountInfo(userID);
            if (account.SlaveLiveDiscountms <= 0 && account.EnableSlavesGroupCount > 0)
            {
                int oldSlaveCount = account.EnableSlavesGroupCount;
                account.SlaveLiveDiscountms = 0;
                account.EnableSlavesGroupCount = 0;

                int result = MyDBHelper.Instance.TransactionDataBaseOper(myTrans =>
                {
                    DBProvider.PlayerStoneFactoryDBProvider.SavePlayerStoneFactoryAccountInfo(account, myTrans);
                    return OperResult.RESULTCODE_TRUE;
                },
                exc =>
                {
                    LogHelper.Instance.AddErrorLog("矿石工厂信息：玩家ID：[" + userID + "] 由于投喂不及时，将矿石工厂中" + oldSlaveCount + "苦力饿死。保存工厂信息异常，", exc);
                });

                if (result == OperResult.RESULTCODE_TRUE)
                {
                    LogHelper.Instance.AddInfoLog("矿石工厂信息：玩家ID：[" + userID + "] 由于投喂不及时，将矿石工厂中" + oldSlaveCount + "苦力饿死。");
                }
            }

            return account;
        }

        public StoneFactoryProfitRMBChangedRecord[] GetProfitRecords(int userID, MyDateTime beginBuyTime, MyDateTime endBuyTime, int pageItemCount, int pageIndex)
        {
            return DBProvider.PlayerStoneFactoryDBProvider.GetProfitRecords(userID, beginBuyTime, endBuyTime, pageItemCount, pageIndex);
        }

        public int OpenFactory(int userID, CustomerMySqlTransaction myTrans)
        {
            //工厂开启状态。开启一次 1000积分。72小时 没有存入矿石和苦力 就 在关闭
            bool isOK = DBProvider.PlayerStoneFactoryDBProvider.OpenFactory(userID, myTrans);
            return isOK ? OperResult.RESULTCODE_TRUE : OperResult.RESULTCODE_FALSE;
        }

        private int CheckTime()
        {
            DateTime time = DateTime.Now;
            if (time.Hour == 23)
            {
                if (new DateTime(time.Year, time.Month, time.Day, 23, 50, 0) < time)
                {
                    return OperResult.RESULTCODE_STONEFACTORYISCLEARING;
                }
            }
            if (time.Hour == 0)
            {
                if (time < new DateTime(time.Year, time.Month, time.Day, 1, 0, 0))
                {
                    return OperResult.RESULTCODE_STONEFACTORYISCLEARING;
                }
            }

            return OperResult.RESULTCODE_TRUE;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="userName"></param>
        /// <param name="stoneStackCount">矿石股数（一万矿石为一股）</param>
        /// <returns></returns>
        public int AddStoneToFactory(int userID, string userName, int stoneStackCount)
        {
            int result = CheckTime();
            if (result != OperResult.RESULTCODE_TRUE)
            {
                return result;
            }

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

            result = MyDBHelper.Instance.TransactionDataBaseOper(myTrans =>
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

            if (result == OperResult.RESULTCODE_TRUE)
            {
                LogHelper.Instance.AddInfoLog("矿石工厂，玩家ID[" + userID + "] 添加" + stoneStackCount + "0000矿石");
            }
            return result;
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
            
            if (result == OperResult.RESULTCODE_TRUE)
            {
                LogHelper.Instance.AddInfoLog("矿石工厂，玩家ID[" + userID + "] 取出" + stoneStackCount + "矿石");
            }
            return OperResult.RESULTCODE_FALSE;
        }

        /// <summary>
        /// 100个矿工为一组奴隶
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="userName"></param>
        /// <param name="minersGroupCount">100个矿工为一组奴隶</param>
        /// <returns></returns>
        public int AddMinersToFactory(int userID, string userName, int minersGroupCount)
        {
            int result = CheckTime();
            if (result != OperResult.RESULTCODE_TRUE)
            {
                return result;
            }

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

            result = MyDBHelper.Instance.TransactionDataBaseOper(myTrans =>
            {
                result = playerrunner.TransferMinersToFactory(minersGroupCount * StoneFactoryConfig.OneGroupSlaveHasMiners, myTrans);
                if (result == OperResult.RESULTCODE_TRUE)
                {
                    factoryAccount.FreezingSlaveGroupCount += minersGroupCount;
                    //factoryAccount.LastFeedSlaveTime = new MyDateTime(DateTime.Now);

                    bool isOK = DBProvider.PlayerStoneFactoryDBProvider.SavePlayerStoneFactoryAccountInfo(factoryAccount, myTrans);
                    return isOK ? OperResult.RESULTCODE_TRUE : OperResult.RESULTCODE_FALSE;
                }

                return result;
            },
            exc =>
            {
                PlayerController.Instance.RollbackUserFromDB(userName);
            });

            if (result == OperResult.RESULTCODE_TRUE)
            {
                LogHelper.Instance.AddInfoLog("矿石工厂，玩家ID[" + userID + "] 增加" + minersGroupCount + "00苦力");
            }
            return result;
        }

        public int FeedSlave(int userID)
        {
            int result = CheckTime();
            if (result != OperResult.RESULTCODE_TRUE)
            {
                return result;
            }

            PlayerStoneFactoryAccountInfo factoryAccount = DBProvider.PlayerStoneFactoryDBProvider.GetPlayerStoneFactoryAccountInfo(userID);
            if (factoryAccount == null || !factoryAccount.FactoryIsOpening || factoryAccount.FactoryLiveDays <= 0)
            {
                return OperResult.RESULTCODE_STONEFACTORYISCLOSED;
            }

            if (factoryAccount.Food < StoneFactoryConfig.SlaveDefaultLiveDays)
            {
                return OperResult.RESULTCODE_FACTORY_FOOD_LACKOFBALANCE;
            }

            factoryAccount.Food -= StoneFactoryConfig.SlaveDefaultLiveDays;
            factoryAccount.LastFeedSlaveTime = new MyDateTime(DateTime.Now);

            result = MyDBHelper.Instance.TransactionDataBaseOper(myTrans =>
            {
                bool isOK = DBProvider.PlayerStoneFactoryDBProvider.SavePlayerStoneFactoryAccountInfo(factoryAccount, myTrans);
                return isOK ? OperResult.RESULTCODE_TRUE : OperResult.RESULTCODE_FALSE;
            }
            , exc =>
            {
                LogHelper.Instance.AddErrorLog("矿石工厂，玩家ID[" + userID + "] 投喂苦力异常", exc);
            });

            if (result == OperResult.RESULTCODE_TRUE)
            {
                LogHelper.Instance.AddInfoLog("矿石工厂，玩家ID[" + userID + "] 投喂苦力");
            }
            return result;
        }

        public int AddFoods(int userID, int foodsCount, CustomerMySqlTransaction myTrans)
        {
            int result = CheckTime();
            if (result != OperResult.RESULTCODE_TRUE)
            {
                return result;
            }

            PlayerStoneFactoryAccountInfo factoryAccount = DBProvider.PlayerStoneFactoryDBProvider.GetPlayerStoneFactoryAccountInfo(userID);
            if (factoryAccount == null || !factoryAccount.FactoryIsOpening || factoryAccount.FactoryLiveDays <= 0)
            {
                return OperResult.RESULTCODE_STONEFACTORYISCLOSED;
            }

            factoryAccount.Food += foodsCount;

            bool isOK = DBProvider.PlayerStoneFactoryDBProvider.SavePlayerStoneFactoryAccountInfo(factoryAccount, myTrans);
            return isOK ? OperResult.RESULTCODE_TRUE : OperResult.RESULTCODE_FALSE;
        }

        public int WithdrawOutputRMB(int userID, string userName, decimal withdrawRMBCount)
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

            if (result == OperResult.RESULTCODE_TRUE)
            {
                LogHelper.Instance.AddInfoLog("矿石工厂，玩家ID[" + userID + "] 提取" + withdrawRMBCount + "灵币");
            }

            return result;
        }

        private DateTime _lastExeCheckFactoryStateTime = DateTime.MinValue;
        private object _lockDailyCheckFactoryState = new object();

        public void DailyCheckFactoryState()
        {
            try
            {
                lock (_lockDailyCheckFactoryState)
                {
                    if ((DateTime.Now - _lastExeCheckFactoryStateTime).TotalHours < 20)
                    {
                        return;
                    }
                    _lastExeCheckFactoryStateTime = DateTime.Now;
                }

                LogHelper.Instance.AddInfoLog("开始执行         DailyCheckFactoryState");

                PlayerStoneFactoryAccountInfo[] listAllFactories = DBProvider.PlayerStoneFactoryDBProvider.GetAllPlayerStoneFactoryAccountInfos();

                int result = MyDBHelper.Instance.TransactionDataBaseOper(myTrans =>
                {
                    foreach (var factory in listAllFactories)
                    {
                        if (factory.FactoryIsOpening && factory.FactoryLiveDays > 0)
                        {
                            //1份食物：1组奴隶：1股矿石
                            //计算前一天存活的奴隶
                            if (factory.SlaveLiveDiscountms <= 0)
                            {
                                factory.EnableSlavesGroupCount = 0;
                                factory.SlaveLiveDiscountms = 0;
                            }
                            //int workableGroupSlaveCount = factory.EnableSlavesGroupCount < factory.Food ? factory.EnableSlavesGroupCount : factory.Food;
                            //int deadGroupSlaveCount = factory.EnableSlavesGroupCount - workableGroupSlaveCount;
                            //计算前一天有效矿石。
                            factory.LastDayValidStoneStack = factory.EnableSlavesGroupCount < factory.TotalStackCount ? factory.EnableSlavesGroupCount : factory.TotalStackCount;

                            //减去前一天没有食物死掉的奴隶
                            //factory.EnableSlavesGroupCount -= deadGroupSlaveCount;
                            //将前一天存入的冻结中的奴隶转成可用
                            if (factory.FreezingSlaveGroupCount > 0)
                            {
                                factory.EnableSlavesGroupCount += factory.FreezingSlaveGroupCount;
                                factory.FreezingSlaveGroupCount = 0;
                                factory.LastFeedSlaveTime = new MyDateTime(DateTime.Now);
                            }

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

                LogHelper.Instance.AddInfoLog("矿石工厂零时处理执行完毕         DailyCheckFactoryState");

            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("矿石加工厂DailyCheckFactoryState异常", exc);
            }
        }

        private bool SaveYesterdayFactoryProfitRate(decimal profitRate)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                XmlNode root = doc.CreateElement("root");
                XmlElement node = doc.CreateElement("Profit");
                XmlElement nodeRate = doc.CreateElement("Rate");
                nodeRate.InnerText = profitRate.ToString();
                XmlElement nodeDate = doc.CreateElement("Date");
                nodeDate.InnerText = DateTime.Now.ToString();
                node.AppendChild(nodeRate);
                node.AppendChild(nodeDate);
                
                root.AppendChild(node);
                doc.AppendChild(root);
                doc.Save(GlobalData.StoneFactoryYesterdayProfitRateConfigFile);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private StoneFactorySystemDailyProfit LoadYesterdayFactoryProfitRate()
        {
            StoneFactorySystemDailyProfit profit = null;

            try
            {
                if (File.Exists(GlobalData.StoneFactoryYesterdayProfitRateConfigFile))
                {
                    using (FileStream stream = File.Open(GlobalData.StoneFactoryYesterdayProfitRateConfigFile, FileMode.Open))
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.Load(stream);
                        XmlNode root = doc.SelectSingleNode("root");
                        if (root == null)
                        {
                            return null;
                        }
                        XmlNode node = root.SelectSingleNode("Profit");
                        if (node == null)
                        {
                            return null;
                        }
                        XmlNode nodeRate = node.SelectSingleNode("Rate");
                        if (nodeRate == null)
                        {
                            return null;
                        }
                        XmlNode nodeDate = node.SelectSingleNode("Date");
                        if (nodeDate == null)
                        {
                            return null;
                        }

                        profit = new StoneFactorySystemDailyProfit();
                        profit.profitRate = Convert.ToDecimal(nodeRate.InnerText);
                        DateTime date = Convert.ToDateTime(nodeDate.InnerText);
                        profit.Day = new MyDateTime(date);
                        return profit;
                    }
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public StoneFactorySystemDailyProfit GetYesterdayFactoryProfitRate()
        {
            return LoadYesterdayFactoryProfitRate();
        }

        public int AdminSetProfitRate(decimal profitRate)
        {
            bool isOK = SaveYesterdayFactoryProfitRate(profitRate);
            return isOK ? OperResult.RESULTCODE_TRUE : OperResult.RESULTCODE_FALSE;
        }

        private object _lockDailySetProfit = new object();
        private DateTime _lastExeSetProfit = DateTime.MinValue;

        public void DailySetProfit()
        {
            try
            {
                lock (_lockDailySetProfit)
                {
                    if ((DateTime.Now - _lastExeSetProfit).TotalHours < 20)
                    {
                        return;
                    }
                    _lastExeSetProfit = DateTime.Now;
                }

                decimal profitRate = StoneFactoryConfig.DailyMinProfit;
                StoneFactorySystemDailyProfit yesterdayProfit = LoadYesterdayFactoryProfitRate();
                if(yesterdayProfit != null && yesterdayProfit.profitRate != 0 && (DateTime.Now.Date - yesterdayProfit.Day.ToDateTime().Date).Days == 0)
                {
                    //如当天保存了收益率，则使用当天收益率，否则使用默认收益率。
                    profitRate = yesterdayProfit.profitRate;
                }

                DBProvider.PlayerStoneFactoryDBProvider.AddStoneFactorySystemDailyProfit(new StoneFactorySystemDailyProfit() { Day = new MyDateTime(DateTime.Now), profitRate = profitRate });

                PlayerStoneFactoryAccountInfo[] listAllFactories = DBProvider.PlayerStoneFactoryDBProvider.GetAllPlayerStoneFactoryAccountInfos();
                foreach (var factory in listAllFactories)
                {
                    if (!factory.FactoryIsOpening || factory.FactoryLiveDays <= 0 || factory.LastDayValidStoneStack == 0)
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
                            var level1FactoryAccount = listAllFactories.FirstOrDefault(f => f.UserID == Level1ReferPlayerInfo.SimpleInfo.UserID);
                            if (level1FactoryAccount != null && level1FactoryAccount.FactoryIsOpening && level1FactoryAccount.FactoryLiveDays > 0)
                            {
                                int minStoneStack = level1FactoryAccount.TotalStackCount < factory.TotalStackCount ? level1FactoryAccount.TotalStackCount : factory.TotalStackCount;
                                if (minStoneStack != 0)
                                {
                                    isOK = DBProvider.PlayerStoneFactoryDBProvider.AddProfitRMBChangedRecord(new StoneFactoryProfitRMBChangedRecord()
                                    {
                                        UserID = Level1ReferPlayerInfo.SimpleInfo.UserID,
                                        ProfitType = FactoryProfitOperType.Level1ReferenceAward,
                                        OperRMB = minStoneStack * profitRate * StoneFactoryConfig.FactoryOutputProfitAwardRMBConfig[0],
                                        OperTime = new MyDateTime(DateTime.Now)
                                    }, myTrans);
                                    if (!isOK)
                                    {
                                        return OperResult.RESULTCODE_FALSE;
                                    }
                                }
                            }
                        }
                        if (Level2ReferPlayerInfo != null)
                        {
                            var level2FactoryAccount = listAllFactories.FirstOrDefault(f => f.UserID == Level2ReferPlayerInfo.SimpleInfo.UserID);
                            if (level2FactoryAccount != null && level2FactoryAccount.FactoryIsOpening && level2FactoryAccount.FactoryLiveDays > 0)
                            {
                                int minStoneStack = level2FactoryAccount.TotalStackCount < factory.TotalStackCount ? level2FactoryAccount.TotalStackCount : factory.TotalStackCount;
                                if (minStoneStack != 0)
                                {
                                    isOK = DBProvider.PlayerStoneFactoryDBProvider.AddProfitRMBChangedRecord(new StoneFactoryProfitRMBChangedRecord()
                                    {
                                        UserID = Level2ReferPlayerInfo.SimpleInfo.UserID,
                                        ProfitType = FactoryProfitOperType.Level2ReferenceAward,
                                        OperRMB = minStoneStack * profitRate * StoneFactoryConfig.FactoryOutputProfitAwardRMBConfig[1],
                                        OperTime = new MyDateTime(DateTime.Now)
                                    }, myTrans);
                                    if (!isOK)
                                    {
                                        return OperResult.RESULTCODE_FALSE;
                                    }
                                }
                            }
                        }
                        if (Level3ReferPlayerInfo != null)
                        {
                            var level3FactoryAccount = listAllFactories.FirstOrDefault(f => f.UserID == Level3ReferPlayerInfo.SimpleInfo.UserID);
                            if (level3FactoryAccount != null && level3FactoryAccount.FactoryIsOpening && level3FactoryAccount.FactoryLiveDays > 0)
                            {
                                int minStoneStack = level3FactoryAccount.TotalStackCount < factory.TotalStackCount ? level3FactoryAccount.TotalStackCount : factory.TotalStackCount;
                                if (minStoneStack != 0)
                                {
                                    isOK = DBProvider.PlayerStoneFactoryDBProvider.AddProfitRMBChangedRecord(new StoneFactoryProfitRMBChangedRecord()
                                    {
                                        UserID = Level3ReferPlayerInfo.SimpleInfo.UserID,
                                        ProfitType = FactoryProfitOperType.Level3ReferenceAward,
                                        OperRMB = minStoneStack * profitRate * StoneFactoryConfig.FactoryOutputProfitAwardRMBConfig[2],
                                        OperTime = new MyDateTime(DateTime.Now)
                                    }, myTrans);
                                    if (!isOK)
                                    {
                                        return OperResult.RESULTCODE_FALSE;
                                    }
                                }
                            }
                        }

                        return OperResult.RESULTCODE_TRUE;
                    },
                    exc =>
                    {
                        LogHelper.Instance.AddErrorLog("管理员设置工厂收益异常，玩家ID：" + factory.UserID, exc);
                    });

                }
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("矿石加工厂DailySetProfit异常", exc);
            }
        }

    }
}
