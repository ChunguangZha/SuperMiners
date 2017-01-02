using MetaData;
using MetaData.Game.RaideroftheLostArk;
using MetaData.User;
using SuperMinersServerApplication.Utility;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
//using System.Threading;
//using System.Threading.Tasks;

namespace SuperMinersServerApplication.Controller.Game
{
    public class RaiderRoundLogicRunner
    {
        private object _lockJoin = new object();

        private object _lockRoundInfo = new object();
        private RaiderRoundMetaDataInfo _currentRoundInfo;

        private Timer _timer = new Timer(5 * 60 * 1000);

        private Random _ran = new Random();


        public RaiderRoundState RaiderRoundState
        {
            get
            {
                return this._currentRoundInfo.State;
            }
        }

        private List<PlayerBetInfo> listPlayerBetInfos = new List<PlayerBetInfo>();

        private RaiderRoundLogicRunner(RaiderRoundMetaDataInfo parent)
        {
            this._currentRoundInfo = parent;
            this._timer.Elapsed += FinishRound;
        }

        public static RaiderRoundLogicRunner Init()
        {
            RaiderRoundMetaDataInfo currentRoundInfo = ResetRoundInfo();
            return new RaiderRoundLogicRunner(currentRoundInfo);
        }

        public static RaiderRoundLogicRunner CreateNewRound()
        {
            RaiderRoundMetaDataInfo lastRoundInfo = new RaiderRoundMetaDataInfo();
            DBProvider.GameRaiderofLostArkDBProvider.SaveRaiderRoundMetaDataInfo(lastRoundInfo);
            lastRoundInfo = DBProvider.GameRaiderofLostArkDBProvider.GetLastRaiderRoundMetaDataInfo();

            return new RaiderRoundLogicRunner(lastRoundInfo);
        }

        private static RaiderRoundMetaDataInfo ResetRoundInfo()
        {
            RaiderRoundMetaDataInfo lastRoundInfo = DBProvider.GameRaiderofLostArkDBProvider.GetLastRaiderRoundMetaDataInfo();
            if (lastRoundInfo == null || lastRoundInfo.State == RaiderRoundState.Finished)
            {
                lastRoundInfo = new RaiderRoundMetaDataInfo();
                DBProvider.GameRaiderofLostArkDBProvider.SaveRaiderRoundMetaDataInfo(lastRoundInfo);
                lastRoundInfo = DBProvider.GameRaiderofLostArkDBProvider.GetLastRaiderRoundMetaDataInfo();
            }

            return lastRoundInfo;
        }

        public OperResultObject Join(PlayerInfo player, int stonesCount)
        {
            OperResultObject resultObj = new OperResultObject();

            lock (_lockJoin)
            {
                if (this._currentRoundInfo.State == RaiderRoundState.Finished)
                {
                    resultObj.OperResultCode = OperResult.RESULTCODE_GAME_RAIDER_ROUNDFINISHED;
                    return resultObj;
                }

                if (this._currentRoundInfo.State == RaiderRoundState.Waiting)
                {
                    StartRound();
                }
            }

            var betInfo = new PlayerBetInfo()
            {
                UserID = player.SimpleInfo.UserID,
                RaiderRoundID = _currentRoundInfo.ID,
                BetStones = stonesCount,
                Time = MyDateTime.FromDateTime(DateTime.Now)
            };

            //如果保存数据库不成功，将异常抛到外层
            DBProvider.GameRaiderofLostArkDBProvider.SavePlayerBetInfo(betInfo);
            listPlayerBetInfos.Add(betInfo);

            this._currentRoundInfo.AwardPoolSumStones += stonesCount;

            resultObj.OperResultCode = OperResult.RESULTCODE_TRUE;
            return resultObj;
        }

        private void StartRound()
        {
            this._currentRoundInfo.State = RaiderRoundState.Started;
            this._currentRoundInfo.StartTime = new MyDateTime(DateTime.Now);

            this._timer.Start();
        }

        void FinishRound(object sender, ElapsedEventArgs e)
        {
            this._timer.Stop();

            try
            {
                lock (_lockJoin)
                {
                    this._currentRoundInfo.State = RaiderRoundState.Finished;
                    if (this.listPlayerBetInfos.Count == 0)
                    {
                        return;
                    }

                    PlayerBetInfo winnerBetInfo = FindWinner();
                    var winnerPersonAllBetCount = this.listPlayerBetInfos.Where(b => b.UserID == winnerBetInfo.UserID).Sum(b => b.BetStones);
                    int expense = (int)Math.Ceiling(this._currentRoundInfo.AwardPoolSumStones * GlobalConfig.GameConfig.RaiderExpense);
                    int winnerGainBetCount = this._currentRoundInfo.AwardPoolSumStones - expense;
                    if (winnerGainBetCount < winnerPersonAllBetCount)
                    {
                        winnerGainBetCount = winnerPersonAllBetCount;
                    }

                    this._currentRoundInfo.EndTime = new MyDateTime(DateTime.Now);
                    this._currentRoundInfo.WinnerUserID = winnerBetInfo.UserID;
                    this._currentRoundInfo.WinStones = winnerGainBetCount;

                    DBProvider.GameRaiderofLostArkDBProvider.SaveRaiderRoundMetaDataInfo(this._currentRoundInfo);

                }
            }
            catch (Exception exc)
            {

            }
        }

        public PlayerBetInfo FindWinner()
        {
            int ranCount = _ran.Next(2, 10);

            int winnerNumber = 0;
            for (int i = 0; i < ranCount; i++)
            {
                winnerNumber = _ran.Next(this._currentRoundInfo.AwardPoolSumStones);
            }

            PlayerBetInfo winnerBetInfo = null;

            int sum = 0;
            for (int i = 0; i < this.listPlayerBetInfos.Count; i++)
            {
                if (winnerNumber <= sum + this.listPlayerBetInfos[i].BetStones)
                {
                    winnerBetInfo = this.listPlayerBetInfos[i];
                    break;
                }
                sum += this.listPlayerBetInfos[i].BetStones;
            }

            if (winnerBetInfo == null)
            {
                winnerBetInfo = this.listPlayerBetInfos[this.listPlayerBetInfos.Count - 1];
            }

            return winnerBetInfo;
        }
    }

    public class RaidersofLostArkController
    {
        private object _lockCurrentRoundInfo = new object();
        private RaiderRoundMetaDataInfo _currentRoundInfo = null;

        private RaiderRoundLogicRunner _currentRoundRunner;

        private List<PlayerBetInfo> _listCurrentRoundPlayerBetInfos = new List<PlayerBetInfo>();

        private ConcurrentQueue<RaiderRoundLogicRunner> _queueRunner = new ConcurrentQueue<RaiderRoundLogicRunner>();

        //private Thread _thrRound = null;

        private object _lockJoin = new object();

        public RaiderRoundState RaiderRoundState
        {
            get
            {
                if (_currentRoundInfo == null)
                {
                    return MetaData.Game.RaideroftheLostArk.RaiderRoundState.Waiting;
                }
                return this._currentRoundInfo.State;
            }
        }
        
        public void Init()
        {
            //服务器停掉时，不好处理
        }

        public OperResultObject Join(MetaData.User.PlayerInfo player, int stones)
        {
            OperResultObject resultObj = new OperResultObject();

            //lock (_lockJoin)
            //{
            //    if (this.RaiderRoundInfo.State == RaiderRoundState.Finished)
            //    {
            //        resultObj.OperResultCode = OperResult.RESULTCODE_GAME_RAIDER_ROUNDFINISHED;
            //        return resultObj;
            //    }

            //    if (this.RaiderRoundInfo.State == RaiderRoundState.Waiting)
            //    {
            //        StartRound();
            //    }

            //}

            //var betInfo = new PlayerBetInfo()
            //{
            //    UserID = player.SimpleInfo.UserID,
            //    RaiderRoundID = RaiderRoundInfo.ID,
            //    BetStones = stones,
            //    Time = MyDateTime.FromDateTime(DateTime.Now)
            //};

            ////如果保存数据库不成功，将异常抛到外层
            //DBProvider.GameRaiderofLostArkDBProvider.SavePlayerBetInfo(betInfo);
            //_listCurrentRoundPlayerBetInfos.Add(betInfo);

            //this.RaiderRoundInfo.AwardPoolSumStones += stones;

            resultObj.OperResultCode = OperResult.RESULTCODE_TRUE;
            return resultObj;
        }

    }
}
