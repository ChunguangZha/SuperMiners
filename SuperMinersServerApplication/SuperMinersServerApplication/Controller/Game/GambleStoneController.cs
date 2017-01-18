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

        private int OpenWinTimeSeconds = 40;
        private Thread _thrGamble = null;
        private bool isListening = false;
        private bool isRunning = false;

        ManualResetEvent StopEventX = new ManualResetEvent(false);

        private GambleStoneDailyScheme dailyScheme = null;
        private GambleStoneRoundInfo RoundInfo = null;

        private GambleStoneInningRunner CurrentInningRunner = null;

        public bool Init()
        {
            try
            {
                LoadFromDB();
                this.CreateNewInning();

                LogHelper.Instance.AddInfoLog("GambleStoneController Init Succeed");
                this.isListening = true;
                this.isRunning = true;

                return true;
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("GambleStoneController Init Failed", exc);
                return false;
            }
        }

        public void StopService()
        {
            this.isRunning = false;
            StopEventX.WaitOne();
        }

        public GambleStoneInningInfo GetCurrentInningInfo()
        {
            return CurrentInningRunner.InningInfo;
        }

        private void ThreadWork()
        {
            while (this.isRunning)
            {
                Thread.Sleep(1000);
                try
                {
                    if (this.CurrentInningRunner == null)
                    {
                        continue;
                    }

                    bool isFinished = this.CurrentInningRunner.CountDownDecrease();
                    if (isFinished)
                    {

                    }
                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("", exc);
                }
            }

            StopEventX.Set();
        }

        public bool SaveToDB()
        {
            int result = MyDBHelper.Instance.TransactionDataBaseOper(myTrans =>
            {
                return this.CurrentInningRunner.SaveInningInfoToDB(this.RoundInfo, myTrans);
            },
            exc =>
            {

            });

            return true;
        }

        public void LoadFromDB()
        {
            DateTime nowDate = DateTime.Now;
            GambleStoneDailyScheme lastDailyScheme = DBProvider.GambleStoneDBProvider.GetLastGambleStoneDailyScheme();
            if (lastDailyScheme != null && lastDailyScheme.Date != null && lastDailyScheme.Date.Year == nowDate.Year && lastDailyScheme.Date.Month == nowDate.Month && lastDailyScheme.Date.Day == nowDate.Day)
            {
                this.dailyScheme = lastDailyScheme;
            }
            else
            {
                this.dailyScheme = new GambleStoneDailyScheme()
                {
                    Date = new MyDateTime(nowDate),
                    ProfitStoneObjective = GlobalConfig.GameConfig.GambleStone_DailyProfitStoneObjective,
                };
                DBProvider.GambleStoneDBProvider.AddGambleStoneDailyScheme(this.dailyScheme);
            }

            GambleStoneRoundInfo lastRound = DBProvider.GambleStoneDBProvider.GetLastGambleStoneRoundInfo();
            if (lastRound != null && lastRound.FinishedInningCount < GlobalConfig.GameConfig.GambleStone_Round_InningCount)
            {
                this.RoundInfo = lastRound;
            }
            else
            {
                this.RoundInfo = CreateNewRound();
            }

        }

        public GambleStoneRoundInfo CreateNewRound()
        {
            GambleStoneRoundInfo round = new GambleStoneRoundInfo()
            {
                StartTime = new MetaData.MyDateTime(DateTime.Now),
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
                RoundID = this.RoundInfo.ID,
                CountDownSeconds = OpenWinTimeSeconds,
            };
            this.CurrentInningRunner = new GambleStoneInningRunner(this.RoundInfo, inning);
        }

        public int BetIn(GambleStoneItemColor color, int stoneCount, int userID, string userName)
        {
            if (!this.isListening || this.CurrentInningRunner == null)
            {
                return OperResult.RESULTCODE_GAME_GAMBLE_INNINGFINISHED;
            }
            return this.CurrentInningRunner.BetIn(this.RoundInfo.ID, color, stoneCount, userID, userName);
        }

    }

    public class GambleStoneInningRunner
    {
        //完成时再保存到数据库
        private GambleStoneInningInfo _inningInfo = null;

        private GambleStoneRoundInfo _roundInfo = null;

        private Dictionary<int, int> _dicPlayerBetRedStone = new Dictionary<int, int>();
        private Dictionary<int, int> _dicPlayerBetGreenStone = new Dictionary<int, int>();
        private Dictionary<int, int> _dicPlayerBetBlueStone = new Dictionary<int, int>();
        private Dictionary<int, int> _dicPlayerBetPurpleStone = new Dictionary<int, int>();

        private Dictionary<int, GambleStonePlayerBetRecord> _dicPlayerBetRecord = new Dictionary<int, GambleStonePlayerBetRecord>();

        private bool _isRandomOpen = false;

        private Random _random = new Random();

        public GambleStoneInningRunner(GambleStoneRoundInfo roundInfo, GambleStoneInningInfo inning)
        {
            this._roundInfo = roundInfo;
            this._inningInfo = inning;
        }

        public GambleStoneInningInfo InningInfo
        {
            get
            {
                return this._inningInfo;
            }
        }

        public int BetIn(int roundID, GambleStoneItemColor color, int stoneCount, int userID, string userName)
        {
            if (this._inningInfo.CountDownSeconds == 0)
            {
                return OperResult.RESULTCODE_GAME_RAIDER_WAITINGSECONDPLAYERJOIN_TOSTART;
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

            return OperResult.RESULTCODE_TRUE;
        }

        public bool CountDownDecrease()
        {
            this._inningInfo.CountDownSeconds--;
            if (this._inningInfo.CountDownSeconds == 0)
            {
                FinishInning();
                return true;
            }

            return false;
        }

        public bool FinishInning()
        {
            this._inningInfo.EndTime = new MetaData.MyDateTime(DateTime.Now);
            GambleStoneItemColor winnedColor;
            int winnedStoneCount;
            int winnedTimes;

            if (_isRandomOpen)
            {
                int randomRed = 3000 / GlobalConfig.GameConfig.GambleStoneRedColorWinTimes;
                int randomGreen = 3000 / GlobalConfig.GameConfig.GambleStoneGreenColorWinTimes;
                int randomBlue = 3000 / GlobalConfig.GameConfig.GambleStoneBlueWinTimes;
                int randomPurple = 3000 / (GlobalConfig.GameConfig.GambleStonePurpleWinTimes * 2);
                int allRandoms = randomRed + randomGreen + randomBlue + randomPurple;
                int random = GetRandom(allRandoms);
                if (random < randomRed)
                {
                    winnedColor = GambleStoneItemColor.Red;
                    winnedStoneCount = this._inningInfo.BetRedStone * GlobalConfig.GameConfig.GambleStoneRedColorWinTimes;
                    winnedTimes = GlobalConfig.GameConfig.GambleStoneRedColorWinTimes;
                }
                else if (randomRed < randomRed + randomGreen)
                {
                    winnedColor = GambleStoneItemColor.Green;
                    winnedStoneCount = this._inningInfo.BetGreenStone * GlobalConfig.GameConfig.GambleStoneGreenColorWinTimes;
                    winnedTimes = GlobalConfig.GameConfig.GambleStoneGreenColorWinTimes;
                }
                else if (randomRed < randomRed + randomGreen + randomBlue)
                {
                    winnedColor = GambleStoneItemColor.Blue;
                    winnedStoneCount = this._inningInfo.BetBlueStone * GlobalConfig.GameConfig.GambleStoneBlueWinTimes;
                    winnedTimes = GlobalConfig.GameConfig.GambleStoneBlueWinTimes;
                }
                else
                {
                    winnedColor = GambleStoneItemColor.Purple;
                    winnedStoneCount = this._inningInfo.BetPurpleStone * GlobalConfig.GameConfig.GambleStonePurpleWinTimes;
                    winnedTimes = GlobalConfig.GameConfig.GambleStonePurpleWinTimes;
                }
            }
            else
            {
                int redWinnedStone = this._inningInfo.BetRedStone * GlobalConfig.GameConfig.GambleStoneRedColorWinTimes;
                int greenWinnedStone = this._inningInfo.BetGreenStone * GlobalConfig.GameConfig.GambleStoneGreenColorWinTimes;
                int blueWinnedStone = this._inningInfo.BetBlueStone * GlobalConfig.GameConfig.GambleStoneBlueWinTimes;
                int purpleWinnedStone = this._inningInfo.BetPurpleStone * GlobalConfig.GameConfig.GambleStonePurpleWinTimes;

                int minWinnedStone = redWinnedStone;
                winnedColor = GambleStoneItemColor.Red;
                winnedTimes = GlobalConfig.GameConfig.GambleStoneRedColorWinTimes;

                if (greenWinnedStone < minWinnedStone)
                {
                    minWinnedStone = greenWinnedStone;
                    winnedColor = GambleStoneItemColor.Green;
                    winnedTimes = GlobalConfig.GameConfig.GambleStoneGreenColorWinTimes;
                }
                if (blueWinnedStone < minWinnedStone)
                {
                    minWinnedStone = blueWinnedStone;
                    winnedColor = GambleStoneItemColor.Blue;
                    winnedTimes = GlobalConfig.GameConfig.GambleStoneBlueWinTimes;
                }
                if (purpleWinnedStone < minWinnedStone)
                {
                    minWinnedStone = purpleWinnedStone;
                    winnedColor = GambleStoneItemColor.Purple;
                    winnedTimes = GlobalConfig.GameConfig.GambleStonePurpleWinTimes;
                }
                winnedStoneCount = minWinnedStone;

                //int allBetInStone = this.InningInfo.BetRedStone + this.InningInfo.BetGreenStone + this.InningInfo.BetBlueStone + this.InningInfo.BetPurpleStone;
            }

            this._inningInfo.WinnedColor = winnedColor;
            this._inningInfo.WinnedOutStone = winnedStoneCount;
            this._inningInfo.WinnedTimes = winnedTimes;

            //this.RoundInfo.AllBetInStone += (this._inningInfo.BetRedStone + this._inningInfo.BetGreenStone + this._inningInfo.BetBlueStone + this._inningInfo.BetPurpleStone);
            //this.RoundInfo.AllWinedOutStone += winnedStoneCount;
            //this.RoundInfo.FinishedInningCount++;

            return true;
        }

        public int SaveInningInfoToDB(GambleStoneRoundInfo roundInfo, CustomerMySqlTransaction myTrans)
        {
            DBProvider.GambleStoneDBProvider.AddGambleStoneInningInfo(roundInfo, this._inningInfo, myTrans);
            switch (this._inningInfo.WinnedColor)
            {
                case GambleStoneItemColor.Red:
                    NotifyWinnedPlayer(this._dicPlayerBetRedStone, GlobalConfig.GameConfig.GambleStoneRedColorWinTimes, myTrans);
                    break;
                case GambleStoneItemColor.Green:
                    NotifyWinnedPlayer(this._dicPlayerBetGreenStone, GlobalConfig.GameConfig.GambleStoneGreenColorWinTimes, myTrans);
                    break;
                case GambleStoneItemColor.Blue:
                    NotifyWinnedPlayer(this._dicPlayerBetBlueStone, GlobalConfig.GameConfig.GambleStoneBlueWinTimes, myTrans);
                    break;
                case GambleStoneItemColor.Purple:
                    NotifyWinnedPlayer(this._dicPlayerBetPurpleStone, GlobalConfig.GameConfig.GambleStonePurpleWinTimes, myTrans);
                    break;
                default:
                    break;
            }
            return OperResult.RESULTCODE_TRUE;
        }

        public void NotifyWinnedPlayer(Dictionary<int, int> dicPlayerBetStoneCount, int winTimes, CustomerMySqlTransaction myTrans)
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

                GambleStonePlayerBetRecord playerBetRecord = null;
                string userName = null;
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
                    playerBetRecord.WinnedStone = winnedStone;
                }
            }

            foreach (var item in this._dicPlayerBetRecord.Values)
            {
                DBProvider.GambleStoneDBProvider.AddGambleStonePlayerBetRecord(item, this._roundInfo.TableName, myTrans);
            }
        }

        private int GetRandom(int max)
        {
            int result = 0;
            int randomCount = this._random.Next(10, 20);
            for (int i = 0; i < randomCount; i++)
            {
                result = this._random.Next(0, max);
            }

            return result;
        }
    }
}
