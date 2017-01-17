using MetaData.Game.GambleStone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            _timer = new Timer(OpenWinTimeSeconds * 1000);
            this._timer.Elapsed += _timer_Elapsed;
        }

        #endregion

        private int OpenWinTimeSeconds = 40;
        private Timer _timer = null;

        private GambleStoneRoundRunner RoundRunner = null;

        public void Init()
        {
            this._timer.Start();
        }

        public void StopService()
        {
            this._timer.Stop();
        }

        void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void LoadFromDB()
        {

        }

        public void CreateNewRound()
        {
            GambleStoneRoundInfo round = new GambleStoneRoundInfo()
            {
                StartTime = new MetaData.MyDateTime(DateTime.Now),
            };
            DBProvider.GambleStoneDBProvider.AddGambleStoneRoundInfo(round);
            round = DBProvider.GambleStoneDBProvider.GetLastGambleStoneRoundInfo();

            this.RoundRunner = new GambleStoneRoundRunner(round);
        }

        public bool BetIn(GambleStoneItemColor color, int stoneCount, int userID)
        {
            return this.RoundRunner.BetIn(color, stoneCount, userID);
        }

    }

    public class GambleStoneRoundRunner
    {
        public GambleStoneRoundInfo RoundInfo = null;

        //完成时再保存到数据库
        public GambleStoneInningInfo InningInfo = null;

        private Dictionary<int, int> _dicPlayerBetRedStone = new Dictionary<int, int>();
        private Dictionary<int, int> _dicPlayerBetGreenStone = new Dictionary<int, int>();
        private Dictionary<int, int> _dicPlayerBetBlueStone = new Dictionary<int, int>();
        private Dictionary<int, int> _dicPlayerBetPurpleStone = new Dictionary<int, int>();

        private bool _isRandomOpen = false;

        private Random _random = new Random();

        public GambleStoneRoundRunner(GambleStoneRoundInfo round)
        {
            this.RoundInfo = round;
        }

        public bool StartNewInning()
        {
            this.InningInfo = new GambleStoneInningInfo()
            {
                ID = Guid.NewGuid().ToString(),
                RoundID = this.RoundInfo.ID
            };

            return true;
        }

        public bool BetIn(GambleStoneItemColor color, int stoneCount, int userID)
        {
            switch (color)
            {
                case GambleStoneItemColor.Red:
                    this.InningInfo.BetRedStone += stoneCount;
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
                    this.InningInfo.BetGreenStone += stoneCount;
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
                    this.InningInfo.BetBlueStone += stoneCount;
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
                    this.InningInfo.BetPurpleStone += stoneCount;
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

            return true;
        }

        public bool FinishInning()
        {
            this.InningInfo.EndTime = new MetaData.MyDateTime(DateTime.Now);
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
                    winnedStoneCount = this.InningInfo.BetRedStone * GlobalConfig.GameConfig.GambleStoneRedColorWinTimes;
                    winnedTimes = GlobalConfig.GameConfig.GambleStoneRedColorWinTimes;
                }
                else if (randomRed < randomRed + randomGreen)
                {
                    winnedColor = GambleStoneItemColor.Green;
                    winnedStoneCount = this.InningInfo.BetGreenStone * GlobalConfig.GameConfig.GambleStoneGreenColorWinTimes;
                    winnedTimes = GlobalConfig.GameConfig.GambleStoneGreenColorWinTimes;
                }
                else if (randomRed < randomRed + randomGreen + randomBlue)
                {
                    winnedColor = GambleStoneItemColor.Blue;
                    winnedStoneCount = this.InningInfo.BetBlueStone * GlobalConfig.GameConfig.GambleStoneBlueWinTimes;
                    winnedTimes = GlobalConfig.GameConfig.GambleStoneBlueWinTimes;
                }
                else
                {
                    winnedColor = GambleStoneItemColor.Purple;
                    winnedStoneCount = this.InningInfo.BetPurpleStone * GlobalConfig.GameConfig.GambleStonePurpleWinTimes;
                    winnedTimes = GlobalConfig.GameConfig.GambleStonePurpleWinTimes;
                }
            }
            else
            {
                int redWinnedStone = this.InningInfo.BetRedStone * GlobalConfig.GameConfig.GambleStoneRedColorWinTimes;
                int greenWinnedStone = this.InningInfo.BetGreenStone * GlobalConfig.GameConfig.GambleStoneGreenColorWinTimes;
                int blueWinnedStone = this.InningInfo.BetBlueStone * GlobalConfig.GameConfig.GambleStoneBlueWinTimes;
                int purpleWinnedStone = this.InningInfo.BetPurpleStone * GlobalConfig.GameConfig.GambleStonePurpleWinTimes;

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

            this._dicPlayerBetRedStone.Clear();
            this._dicPlayerBetGreenStone.Clear();
            this._dicPlayerBetBlueStone.Clear();
            this._dicPlayerBetPurpleStone.Clear();

            this.InningInfo.WinnedColor = winnedColor;
            this.InningInfo.WinnedOutStone = winnedStoneCount;
            this.InningInfo.WinnedTimes = winnedTimes;

            this.RoundInfo.AllBetInStone += (this.InningInfo.BetRedStone + this.InningInfo.BetGreenStone + this.InningInfo.BetBlueStone + this.InningInfo.BetPurpleStone);
            this.RoundInfo.AllWinedOutStone += winnedStoneCount;
            this.RoundInfo.FinishedInningCount++;

            NotifyWinnedPlayer();
            return DBProvider.GambleStoneDBProvider.AddGambleStoneInningInfo(this.RoundInfo, this.InningInfo);
        }

        private void NotifyWinnedPlayer()
        {
            switch (this.InningInfo.WinnedColor)
            {
                case GambleStoneItemColor.Red:
                    break;
                case GambleStoneItemColor.Green:
                    break;
                case GambleStoneItemColor.Blue:
                    break;
                case GambleStoneItemColor.Purple:
                    break;
                default:
                    break;
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
