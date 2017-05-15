using DataBaseProvider;
using MetaData;
using MetaData.Game.GambleStone;
using SuperMinersServerApplication.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;

namespace SuperMinersServerApplication.Controller.Game
{
    public class GambleStoneController
    {
        #region Single

        private static GambleStoneController _instance = new GambleStoneController();

        public static GambleStoneController Instance
        {
            get
            {
                return _instance;
            }
        }

        private GambleStoneController()
        {
            _thrGamble = new Thread(ThreadWork);
            _thrGamble.Name = "thrGambleStone";
            _thrGamble.IsBackground = true;
        }

        #endregion

        /// <summary>
        /// 设定阶段性总赢亏目标
        /// </summary>
        public static int StageSumWinnedStoneCountGoal = 0;
        public static int StageSumLosedStoneCountGoal = 0;
        
        public static readonly int WaitBetInTimeSeconds = 40;
        public static readonly int ReadyTimeSeconds = 5;
        public static readonly int OpenPriceTimeSeconds = 3;

        private Thread _thrGamble = null;
        private bool isListening = false;
        private bool isRunning = false;

        ManualResetEvent StopEventX = new ManualResetEvent(false);

        private object _lockDailyScheme = new object();
        private GambleStoneDailyScheme _dailyScheme = null;
        private GambleStoneDailyScheme DailyScheme
        {
            get
            {
                lock (_lockDailyScheme)
                {
                    return this._dailyScheme;
                }
            }
            set
            {
                lock (_lockDailyScheme)
                {
                    this._dailyScheme = value;
                }
            }
        }
        private GambleStoneRoundInfo RoundInfo = null;

        private GambleStoneInningRunner CurrentInningRunner = null;

        /// <summary>
        /// 不保存数据库，每次重新服务器重新创建
        /// </summary>
        private SuperMinersServerApplication.Controller.Game.GambleStoneInningRunner.ReferBetInInningInfo ReferBetInInning = new GambleStoneInningRunner.ReferBetInInningInfo();

        public bool Init()
        {
            try
            {
                LoadFromDB();
                this.CreateNewInning();

                LogHelper.Instance.AddInfoLog("赌石娱乐 Init Succeed");
                this.isListening = true;
                this.isRunning = true;
                _thrGamble.Start();
                return true;
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("赌石娱乐 Init Failed", exc);
                return false;
            }
        }

        public void StopService()
        {
            this.isRunning = false;
            StopEventX.WaitOne();
            LogHelper.Instance.AddInfoLog("赌石娱乐停止");
        }

        public GambleStoneRound_InningInfo GetCurrentInningInfo()
        {
            GambleStoneRound_InningInfo resultInfo = new GambleStoneRound_InningInfo()
            {
                roundInfo = this.RoundInfo,
                inningInfo = this.CurrentInningRunner.InningInfo
            };
            return resultInfo;
        }

        public GambleStoneRoundInfo GetGambleStoneRoundInfo()
        {
            return this.RoundInfo;
        }

        public GambleStoneInningInfo GetGambleStoneInningInfo()
        {
            return this.CurrentInningRunner.InningInfo;
        }

        private void ThreadWork()
        {
            while (this.isListening)
            {
                Thread.Sleep(1000);
                try
                {
                    if (this.CurrentInningRunner == null)
                    {
                        continue;
                    }

                    bool isFinished = this.CurrentInningRunner.CountDownDecrease();
                    if (this.CurrentInningRunner.InningInfo.State == GambleStoneInningStatusType.Finished)
                    {
                        if (!this.isRunning)
                        {
                            break;
                        }

                        if (this.RoundInfo.FinishedInningCount >= GlobalConfig.GameConfig.GambleStone_Round_InningCount)
                        {
                            this.RoundInfo = this.CreateNewRound(this.RoundInfo);
                        }

                        this.CreateNewInning();
                    }
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("", exc);
                }
            }

            StopEventX.Set();
            LogHelper.Instance.AddInfoLog("赌石娱乐已退出");
        }

        private void CreateNewDailyScheme(GambleStoneDailyScheme lastDailyScheme)
        {
            if (lastDailyScheme != null)
            {
                int allProfit = lastDailyScheme.AllWinnedOutStone - lastDailyScheme.AllBetInStone;
            }
            DateTime nowDate = DateTime.Now;
            GambleStoneDailyScheme newDailyScheme = new GambleStoneDailyScheme()
            {
                Date = new MyDateTime(nowDate),
                ProfitStoneObjective = GlobalConfig.GameConfig.GambleStone_DailyProfitStoneObjective,
            };
            DBProvider.GambleStoneDBProvider.AddGambleStoneDailyScheme(newDailyScheme);
            this.DailyScheme = newDailyScheme;
        }
        
        public void LoadFromDB()
        {
            DateTime nowDate = DateTime.Now;
            GambleStoneDailyScheme lastDailyScheme = DBProvider.GambleStoneDBProvider.GetLastGambleStoneDailyScheme();
            if (lastDailyScheme != null && lastDailyScheme.Date != null && lastDailyScheme.Date.Year == nowDate.Year && lastDailyScheme.Date.Month == nowDate.Month && lastDailyScheme.Date.Day == nowDate.Day)
            {
                this.DailyScheme = lastDailyScheme;
            }
            else
            {
                CreateNewDailyScheme(lastDailyScheme);
            }

            GambleStoneRoundInfo lastRound = DBProvider.GambleStoneDBProvider.GetLastGambleStoneRoundInfo();
            if (lastRound != null && lastRound.FinishedInningCount < GlobalConfig.GameConfig.GambleStone_Round_InningCount)
            {
                this.RoundInfo = lastRound;
            }
            else
            {
                this.RoundInfo = CreateNewRound(lastRound);
            }

        }

        public GambleStoneRoundInfo CreateNewRound(GambleStoneRoundInfo lastRoundInfo)
        {
            GambleStoneRoundInfo round = new GambleStoneRoundInfo()
            {
                StartTime = new MetaData.MyDateTime(DateTime.Now),
                LastWinRedCount = lastRoundInfo == null? 0 : lastRoundInfo.CurrentWinRedCount,
                LastWinGreenCount = lastRoundInfo == null ? 0 : lastRoundInfo.CurrentWinGreenCount,
                LastWinBlueCount = lastRoundInfo == null ? 0 : lastRoundInfo.CurrentWinBlueCount,
                LastWinPurpleCount = lastRoundInfo == null ? 0 : lastRoundInfo.CurrentWinPurpleCount,
                TableName = DateTime.Now.ToString("yyyyMM")
            };
            DBProvider.GambleStoneDBProvider.AddGambleStoneRoundInfo(round);
            round = DBProvider.GambleStoneDBProvider.GetLastGambleStoneRoundInfo();
            return round;
        }

        public void CreateNewInning()
        {
            GambleStoneInningInfo inning = new GambleStoneInningInfo()
            {
                ID = Guid.NewGuid().ToString(),
                InningIndex = this.RoundInfo.FinishedInningCount + 1,
                RoundID = this.RoundInfo.ID,
                State = GambleStoneInningStatusType.Readying,
                CountDownSeconds = ReadyTimeSeconds,
            };
            this.CurrentInningRunner = new GambleStoneInningRunner(this.RoundInfo, inning, this.DailyScheme, this.ReferBetInInning);
            this.CurrentInningRunner.GambleStoneInningWinnedNotifyAllClient += CurrentInningRunner_GambleStoneInningWinnedNotifyAllClient;
        }

        void CurrentInningRunner_GambleStoneInningWinnedNotifyAllClient(GambleStoneRoundInfo roundInfo, GambleStoneInningInfo inningInfo, GambleStonePlayerBetRecord maxWinner)
        {
            if (this.GambleStoneInningWinnedNotifyAllClient != null)
            {
                GambleStoneInningWinnedNotifyAllClient(roundInfo, inningInfo, maxWinner);
            }
        }

        public bool CheckBetInable()
        {
            if (!this.isListening || this.CurrentInningRunner == null)
                return false;
            if (this.CurrentInningRunner.InningInfo.State != GambleStoneInningStatusType.BetInWaiting && this.CurrentInningRunner.InningInfo.State != GambleStoneInningStatusType.Opening)
            {
                return false;
            }

            return true;
        }

        public GambleStonePlayerBetInResult BetIn(GambleStoneItemColor color, int stoneCount, int userID, string userName)
        {
            GambleStonePlayerBetInResult result = new GambleStonePlayerBetInResult();
            if (!this.isListening || this.CurrentInningRunner == null)
            {
                result.ResultCode = OperResult.RESULTCODE_GAME_GAMBLE_INNINGFINISHED;
                return result;
            }
            return this.CurrentInningRunner.BetIn(this.RoundInfo.ID, color, stoneCount, userID, userName);
        }

        public event Action<GambleStoneRoundInfo, GambleStoneInningInfo, GambleStonePlayerBetRecord> GambleStoneInningWinnedNotifyAllClient;
    }

    public class GambleStoneInningRunner
    {
        //完成时再保存到数据库
        private GambleStoneInningInfo _inningInfo = null;

        private GambleStoneRoundInfo _roundInfo = null;

        private GambleStoneDailyScheme _dailyScheme = null;
        private ReferBetInInningInfo _referInfo = null;

        private Dictionary<int, int> _dicPlayerBetRedStone = new Dictionary<int, int>();
        private Dictionary<int, int> _dicPlayerBetGreenStone = new Dictionary<int, int>();
        private Dictionary<int, int> _dicPlayerBetBlueStone = new Dictionary<int, int>();
        private Dictionary<int, int> _dicPlayerBetPurpleStone = new Dictionary<int, int>();

        private Dictionary<int, GambleStonePlayerBetRecord> _dicPlayerBetRecord = new Dictionary<int, GambleStonePlayerBetRecord>();

        private object _lockBetIn = new object();

        private bool _isRandomOpen = false;

        private Random _random = new Random();

        public GambleStoneInningRunner(GambleStoneRoundInfo roundInfo, GambleStoneInningInfo inning, GambleStoneDailyScheme dailyScheme, ReferBetInInningInfo referInfo)
        {
            this._roundInfo = roundInfo;
            this._inningInfo = inning;
            this._dailyScheme = dailyScheme;
            this._referInfo = referInfo;
        }

        public GambleStoneInningInfo InningInfo
        {
            get
            {
                return this._inningInfo;
            }
        }

        public GambleStonePlayerBetInResult BetIn(int roundID, GambleStoneItemColor color, int stoneCount, int userID, string userName)
        {
            lock (_lockBetIn)
            {
                GambleStonePlayerBetInResult result = new GambleStonePlayerBetInResult();

                //客户端倒计时5秒内，不再允许下游。以防止计算错误
                if (this._inningInfo.State != GambleStoneInningStatusType.BetInWaiting)
                {
                    LogHelper.Instance.AddInfoLog("State: " + this._inningInfo.State.ToString());
                    result.ResultCode = OperResult.RESULTCODE_GAME_GAMBLE_INNINGFINISHED;
                    return result;
                }

                GambleStonePlayerBetRecord betrecord = null;
                if (this._dicPlayerBetRecord.ContainsKey(userID))
                {
                    betrecord = this._dicPlayerBetRecord[userID];
                }
                else
                {
                    betrecord = new GambleStonePlayerBetRecord()
                    {
                        UserID = userID,
                        UserName = userName,
                        InningID = this._inningInfo.ID,
                        InningIndex = this._inningInfo.InningIndex,
                        RoundID = roundID,
                        Time = new MyDateTime(DateTime.Now)
                    };
                    this._dicPlayerBetRecord.Add(userID, betrecord);
                }

                switch (color)
                {
                    case GambleStoneItemColor.Red:
                        betrecord.BetRedStone += stoneCount;
                        this._inningInfo.BetRedStone += stoneCount;
                        if (this._dicPlayerBetRedStone.ContainsKey(userID))
                        {
                            this._dicPlayerBetRedStone[userID] += stoneCount;
                        }
                        else
                        {
                            this._dicPlayerBetRedStone.Add(userID, stoneCount);
                        }
                        break;
                    case GambleStoneItemColor.Green:
                        betrecord.BetGreenStone += stoneCount;
                        this._inningInfo.BetGreenStone += stoneCount;
                        if (this._dicPlayerBetGreenStone.ContainsKey(userID))
                        {
                            this._dicPlayerBetGreenStone[userID] += stoneCount;
                        }
                        else
                        {
                            this._dicPlayerBetGreenStone.Add(userID, stoneCount);
                        }
                        break;
                    case GambleStoneItemColor.Blue:
                        betrecord.BetBlueStone += stoneCount;
                        this._inningInfo.BetBlueStone += stoneCount;
                        if (this._dicPlayerBetBlueStone.ContainsKey(userID))
                        {
                            this._dicPlayerBetBlueStone[userID] += stoneCount;
                        }
                        else
                        {
                            this._dicPlayerBetBlueStone.Add(userID, stoneCount);
                        }
                        break;
                    case GambleStoneItemColor.Purple:
                        betrecord.BetPurpleStone += stoneCount;
                        this._inningInfo.BetPurpleStone += stoneCount;
                        if (this._dicPlayerBetPurpleStone.ContainsKey(userID))
                        {
                            this._dicPlayerBetPurpleStone[userID] += stoneCount;
                        }
                        else
                        {
                            this._dicPlayerBetPurpleStone.Add(userID, stoneCount);
                        }
                        break;
                    default:
                        break;
                }

                result.PlayerBetRecord = betrecord;
                result.ResultCode = OperResult.RESULTCODE_TRUE;
                return result;
            }
        }

        public bool CountDownDecrease()
        {
            this._inningInfo.CountDownSeconds--;
            if (this._inningInfo.CountDownSeconds == 0)
            {
                //LogHelper.Instance.AddInfoLog("CountDownDecrease. State: " + this._inningInfo.State.ToString());
                if (this._inningInfo.State == GambleStoneInningStatusType.Readying)
                {
                    this._inningInfo.State = GambleStoneInningStatusType.BetInWaiting;
                    this._inningInfo.CountDownSeconds = GambleStoneController.WaitBetInTimeSeconds;
                }
                else if (this._inningInfo.State == GambleStoneInningStatusType.BetInWaiting)
                {
                    this._inningInfo.State = GambleStoneInningStatusType.Opening;
                    this._inningInfo.CountDownSeconds = GambleStoneController.OpenPriceTimeSeconds;
                    FinishInning();
                    SaveInningInfoToDB();
                }
                else if (this._inningInfo.State == GambleStoneInningStatusType.Opening)
                {
                    this._inningInfo.State = GambleStoneInningStatusType.Finished;
                    return true;
                }
            }

            ////为了防止临截止时玩家下注，延迟2秒开奖
            //if (this._inningInfo.State == GambleStoneInningStatusType.Opening && this._inningInfo.CountDownSeconds == 5)
            //{
            //    lock (_lockBetIn)
            //    {
            //        FinishInning();
            //        SaveInningInfoToDB();
            //    }
            //}

            return false;
        }

        private bool FinishInning()
        {
            //this._inningInfo.EndTime = new MetaData.MyDateTime(DateTime.Now);
            GambleStoneItemColor winnedColor;
            int winnedStoneCount = 0;
            int winnedTimes = 0;
            
            //默认以3倍开奖
            int extraFactorTimes = 3;
            if (this._referInfo.ListHistoryTenInningBetIn.Count >= 10 && GambleStoneController.StageSumWinnedStoneCountGoal > this._referInfo.AllBetInCount)
            {
                extraFactorTimes = 1;
                GambleStoneController.StageSumLosedStoneCountGoal = this._referInfo.AllBetInCount / 3;
            }
            winnedColor = ComputeWinnedColor(extraFactorTimes, out winnedStoneCount, out winnedTimes);
            int currentInningProfit = this._inningInfo.AllBetInStone - winnedStoneCount;
            GambleStoneController.StageSumLosedStoneCountGoal += currentInningProfit;
            if (GambleStoneController.StageSumLosedStoneCountGoal <= 0)
            {
                this._referInfo.Clear();
                GambleStoneController.StageSumWinnedStoneCountGoal = 0;
                GambleStoneController.StageSumLosedStoneCountGoal = 0;
                extraFactorTimes = 3;
                winnedColor = ComputeWinnedColor(extraFactorTimes, out winnedStoneCount, out winnedTimes);
            }

            this._inningInfo.WinnedColor = winnedColor;
            this._inningInfo.WinnedOutStone = winnedStoneCount;
            this._inningInfo.WinnedTimes = winnedTimes;

            this._roundInfo.AllWinnedOutStone += winnedStoneCount;
            this._roundInfo.AllBetInStone += this._inningInfo.AllBetInStone;
            this._roundInfo.WinColorItems[this._roundInfo.FinishedInningCount] = (byte)winnedColor;
            this._roundInfo.FinishedInningCount++;
            switch (winnedColor)
            {
                case GambleStoneItemColor.Red:
                    this._roundInfo.CurrentWinRedCount++;
                    break;
                case GambleStoneItemColor.Green:
                    this._roundInfo.CurrentWinGreenCount++;
                    break;
                case GambleStoneItemColor.Blue:
                    this._roundInfo.CurrentWinBlueCount++;
                    break;
                case GambleStoneItemColor.Purple:
                    this._roundInfo.CurrentWinPurpleCount++;
                    break;
                default:
                    break;
            }

            this._referInfo.AddHistoryBetIn(this._inningInfo.AllBetInStone);
            GambleStoneController.StageSumWinnedStoneCountGoal += this._inningInfo.StoneProfit;

            LogHelper.Instance.AddInfoLog("StageSumWinnedStoneCountGoal: " + GambleStoneController.StageSumWinnedStoneCountGoal + ";StageSumLosedStoneCountGoal: " + GambleStoneController.StageSumLosedStoneCountGoal);
            return true;
        }

        private GambleStoneItemColor ComputeWinnedColor(int extraFactorTimes, out int winnedStoneCount, out int winnedTimes)
        {
            GambleStoneItemColor winnedColor;

            int randomPurple = 300 / (GlobalConfig.GameConfig.GambleStonePurpleWinTimes * 2);
            int randomBlue = 300 / GlobalConfig.GameConfig.GambleStoneBlueWinTimes;
            int randomGreen = 300 / GlobalConfig.GameConfig.GambleStoneGreenColorWinTimes;
            int randomRed = 300 / GlobalConfig.GameConfig.GambleStoneRedColorWinTimes;

            int redOutput = this._inningInfo.BetRedStone * GlobalConfig.GameConfig.GambleStoneRedColorWinTimes;
            int greenOutput = this._inningInfo.BetGreenStone * GlobalConfig.GameConfig.GambleStoneGreenColorWinTimes;
            int blueOutput = this._inningInfo.BetBlueStone * GlobalConfig.GameConfig.GambleStoneBlueWinTimes;
            int purpleOutput = this._inningInfo.BetPurpleStone * GlobalConfig.GameConfig.GambleStonePurpleWinTimes;

            List<Tuple<int, GambleStoneItemColor>> listOutput = new List<Tuple<int, GambleStoneItemColor>>();
            listOutput.Add(new Tuple<int, GambleStoneItemColor>(redOutput, GambleStoneItemColor.Red));
            listOutput.Add(new Tuple<int, GambleStoneItemColor>(greenOutput, GambleStoneItemColor.Green));
            listOutput.Add(new Tuple<int, GambleStoneItemColor>(blueOutput, GambleStoneItemColor.Blue));
            listOutput.Add(new Tuple<int, GambleStoneItemColor>(purpleOutput, GambleStoneItemColor.Purple));

            if (redOutput + greenOutput + blueOutput + purpleOutput == 0)
            {
                //没人下注时，按1倍随机开奖
                extraFactorTimes = 1;
            }
            else
            {
                Tuple<int, GambleStoneItemColor> maxOutputItem = null;
                int maxOutputValue = -1;
                foreach (var item in listOutput)
                {
                    if (item.Item1 > maxOutputValue)
                    {
                        maxOutputItem = item;
                    }
                }

                switch (maxOutputItem.Item2)
                {
                    case GambleStoneItemColor.Red:
                        randomRed *= extraFactorTimes;
                        break;
                    case GambleStoneItemColor.Green:
                        randomGreen *= extraFactorTimes;
                        break;
                    case GambleStoneItemColor.Blue:
                        randomBlue *= extraFactorTimes;
                        break;
                    case GambleStoneItemColor.Purple:
                        randomPurple *= extraFactorTimes;
                        break;
                    default:
                        break;
                }
            }

            winnedColor = GetRandomWinnedColor(randomRed, randomGreen, randomBlue, randomPurple);

            switch (winnedColor)
            {
                case GambleStoneItemColor.Red:
                    winnedStoneCount = redOutput;
                    winnedTimes = GlobalConfig.GameConfig.GambleStoneRedColorWinTimes;
                    break;
                case GambleStoneItemColor.Green:
                    winnedStoneCount = greenOutput;
                    winnedTimes = GlobalConfig.GameConfig.GambleStoneGreenColorWinTimes;
                    break;
                case GambleStoneItemColor.Blue:
                    winnedStoneCount = blueOutput;
                    winnedTimes = GlobalConfig.GameConfig.GambleStoneBlueWinTimes;
                    break;
                case GambleStoneItemColor.Purple:
                    winnedStoneCount = purpleOutput;
                    winnedTimes = GlobalConfig.GameConfig.GambleStonePurpleWinTimes;
                    break;
                default:
                    winnedStoneCount = 0;
                    winnedTimes = 0;
                    break;
            }
            return winnedColor;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="redFactor">因子数</param>
        /// <param name="greenFactor"></param>
        /// <param name="blueFactor"></param>
        /// <param name="purpleFactor"></param>
        /// <returns></returns>
        public GambleStoneItemColor GetRandomWinnedColor(int redFactor, int greenFactor, int blueFactor, int purpleFactor)
        {
            GambleStoneItemColor winnedColor;
            int allFactors = purpleFactor + blueFactor + greenFactor + redFactor;
            int random = GetRandom(allFactors);
            if (random < redFactor)
            {
                winnedColor = GambleStoneItemColor.Red;
            }
            else if (random < redFactor + greenFactor)
            {
                winnedColor = GambleStoneItemColor.Green;
            }
            else if (random < redFactor + greenFactor + blueFactor)
            {
                winnedColor = GambleStoneItemColor.Blue;
            }
            else
            {
                winnedColor = GambleStoneItemColor.Purple;
            }

            return winnedColor;
        }

        public int SaveInningInfoToDB()
        {
            Dictionary<int, int> dicWinnedPlayerBetStoneCount = null;
            int winnedTimes = 0;

            GambleStonePlayerBetRecord maxWinner = null;
            int result = MyDBHelper.Instance.TransactionDataBaseOper(myTrans =>
            {
                DBProvider.GambleStoneDBProvider.AddGambleStoneInningInfo(this._roundInfo, this._inningInfo, myTrans);
                switch (this._inningInfo.WinnedColor)
                {
                    case GambleStoneItemColor.Red:
                        dicWinnedPlayerBetStoneCount = this._dicPlayerBetRedStone;
                        winnedTimes = GlobalConfig.GameConfig.GambleStoneRedColorWinTimes;
                        break;
                    case GambleStoneItemColor.Green:
                        dicWinnedPlayerBetStoneCount = this._dicPlayerBetGreenStone;
                        winnedTimes = GlobalConfig.GameConfig.GambleStoneGreenColorWinTimes;
                        break;
                    case GambleStoneItemColor.Blue:
                        dicWinnedPlayerBetStoneCount = this._dicPlayerBetBlueStone;
                        winnedTimes = GlobalConfig.GameConfig.GambleStoneBlueWinTimes;
                        break;
                    case GambleStoneItemColor.Purple:
                        dicWinnedPlayerBetStoneCount = this._dicPlayerBetPurpleStone;
                        winnedTimes = GlobalConfig.GameConfig.GambleStonePurpleWinTimes;
                        break;
                    default:
                        break;
                }

                maxWinner = WinToUpdatePlayer(dicWinnedPlayerBetStoneCount, winnedTimes, myTrans);
                if (this._roundInfo.FinishedInningCount >= GlobalConfig.GameConfig.GambleStone_Round_InningCount)
                {
                    this._roundInfo.EndTime = new MyDateTime(DateTime.Now);
                }
                DBProvider.GambleStoneDBProvider.UpdateGambleStoneRoundInfo(this._roundInfo, myTrans);
                return OperResult.RESULTCODE_TRUE;
            },
            exc =>
            {
                LogHelper.Instance.AddErrorLog("赌石游戏，保存异常。局信息：" + this.InningInfo.ToString(), exc);
                if (dicWinnedPlayerBetStoneCount != null)
                {
                    foreach (var userID in dicWinnedPlayerBetStoneCount.Keys)
                    {
                        PlayerController.Instance.RefreshFortune(this._dicPlayerBetRecord[userID].UserName);
                    }
                }
            });

            if (result == OperResult.RESULTCODE_TRUE)
            {
                LogHelper.Instance.AddInfoLog("赌石游戏，保存成功。局信息：" + this.InningInfo.ToString());

                if (maxWinner == null)
                {
                    maxWinner = new GambleStonePlayerBetRecord();
                }

                if (!string.IsNullOrEmpty(maxWinner.UserName))
                {
                    PlayerActionController.Instance.AddLog(maxWinner.UserName, MetaData.ActionLog.ActionType.GambleStoneMaxWinner, maxWinner.WinnedStone);
                }
                if (this.GambleStoneInningWinnedNotifyAllClient != null)
                {
                    this.GambleStoneInningWinnedNotifyAllClient(this._roundInfo, this.InningInfo, maxWinner);
                }
                //NotifyWinnedPlayer(dicWinnedPlayerBetStoneCount);
            }
            else
            {
                RecoverPlayerFortune(dicWinnedPlayerBetStoneCount);
            }

            return result;
        }

        private void NotifyWinnedPlayer(Dictionary<int, int> dicPlayerBetStoneCount)
        {
            foreach (var userID in dicPlayerBetStoneCount.Keys)
            {
                string userName = null;
                GambleStonePlayerBetRecord playerBetRecord = null;
                playerBetRecord = this._dicPlayerBetRecord[userID];
                userName = this._dicPlayerBetRecord[userID].UserName;
                PlayerController.Instance.NotifyChanged(userName);
            }
        }

        private void RecoverPlayerFortune(Dictionary<int, int> dicPlayerBetStoneCount)
        {
            if (dicPlayerBetStoneCount == null)
            {
                return;
            }
            foreach (var userID in dicPlayerBetStoneCount.Keys)
            {
                string userName = null;
                GambleStonePlayerBetRecord playerBetRecord = null;
                playerBetRecord = this._dicPlayerBetRecord[userID];
                userName = this._dicPlayerBetRecord[userID].UserName;
                PlayerController.Instance.RefreshFortune(userName);
            }
        }

        private GambleStonePlayerBetRecord WinToUpdatePlayer(Dictionary<int, int> dicPlayerBetStoneCount, int winTimes, CustomerMySqlTransaction myTrans)
        {
            int maxWinnedUserID;
            int maxWinnedStone = 0;
            foreach (var kv in dicPlayerBetStoneCount)
            {
                int userID = kv.Key;
                int winnedStone = kv.Value * winTimes;
                if (winnedStone > maxWinnedStone)
                {
                    maxWinnedStone = winnedStone;
                    maxWinnedUserID = userID;
                }

                string userName = null;
                GambleStonePlayerBetRecord playerBetRecord = null;
                if (this._dicPlayerBetRecord.ContainsKey(userID))
                {
                    playerBetRecord = this._dicPlayerBetRecord[userID];
                    userName = this._dicPlayerBetRecord[userID].UserName;
                }
                if (string.IsNullOrEmpty(userName))
                {
                    LogHelper.Instance.AddErrorLog("Gamble NotifyWinnedPlayer Not find PlayerInfo in _dicPlayerBetRecord ", null);
                    var player = PlayerController.Instance.GetPlayerInfoByUserID(userID);
                    userName = player.SimpleInfo.UserName;
                }

                PlayerController.Instance.WinGambleStone(userName, winnedStone, myTrans);
                if (playerBetRecord != null)
                {
                    playerBetRecord.WinnedStone += winnedStone;
                }
            }

            int maxWinnedStoneCount = 0;
            GambleStonePlayerBetRecord maxWinnedBetRecord = null;

            foreach (var item in this._dicPlayerBetRecord.Values)
            {
                if (item.WinnedStone > maxWinnedStoneCount)
                {
                    maxWinnedBetRecord = item;
                    maxWinnedStoneCount = item.WinnedStone;
                }
                DBProvider.GambleStoneDBProvider.AddGambleStonePlayerBetRecord(item, this._roundInfo.TableName, myTrans);
            }

            return maxWinnedBetRecord;
        }

        private int GetRandom(int max)
        {
            int result = 0;
            int randomCount = this._random.Next(3, 20);
            for (int i = 0; i < randomCount; i++)
            {
                result = this._random.Next(0, max);
            }

            return result;
        }

        public event Action<GambleStoneRoundInfo, GambleStoneInningInfo, GambleStonePlayerBetRecord> GambleStoneInningWinnedNotifyAllClient;

        public class ReferBetInInningInfo
        {
            public List<int> ListHistoryTenInningBetIn = new List<int>();

            public int AllBetInCount
            {
                get
                {
                    return ListHistoryTenInningBetIn.Sum();
                }
            }

            public void AddHistoryBetIn(int betInCount)
            {
                if (ListHistoryTenInningBetIn.Count < 10)
                {
                    ListHistoryTenInningBetIn.Add(betInCount);
                }
            }

            public void Clear()
            {
                ListHistoryTenInningBetIn.Clear();
            }
        }
    }
}
