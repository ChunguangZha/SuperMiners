using DataBaseProvider;
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

namespace SuperMinersServerApplication.Controller.Game
{
    public class RaidersofLostArkController
    {
        #region Single

        private static RaidersofLostArkController _instance = new RaidersofLostArkController();

        public static RaidersofLostArkController Instance
        {
            get
            {
                return _instance;
            }
        }

        private RaidersofLostArkController()
        {
            _timer = new Timer(OpenWinTimeMinutes * 60 * 1000);
            this._timer.Elapsed += FinishRound;
        }

        #endregion

       
        private object _lockJoin = new object();

        private object _lockRoundInfo = new object();
        private RaiderRoundMetaDataInfo _currentRoundInfo;

        private int OpenWinTimeMinutes = 1;
        private Timer _timer = null;

        private Random _ran = new Random();


        public RaiderRoundState RaiderRoundState
        {
            get
            {
                return this._currentRoundInfo.State;
            }
        }

        public RaiderRoundMetaDataInfo CurrentRoundInfo
        {
            get
            {
                if (this._currentRoundInfo.State == MetaData.Game.RaideroftheLostArk.RaiderRoundState.Started)
                {

                    this._currentRoundInfo.CountDownTotalSecond = (int)(this._currentRoundInfo.StartTime.ToDateTime().AddMinutes(OpenWinTimeMinutes) - DateTime.Now).TotalSeconds;
                }
                return this._currentRoundInfo;
            }
        }

        private List<PlayerBetInfo> listPlayerBetInfos = new List<PlayerBetInfo>();

        public void Init()
        {
            ResetRoundInfo();
        }

        public void CreateNewRound()
        {
            RaiderRoundMetaDataInfo lastRoundInfo = new RaiderRoundMetaDataInfo();
            lastRoundInfo = DBProvider.GameRaiderofLostArkDBProvider.AddNewRaiderRoundMetaDataInfo(lastRoundInfo);

            this._currentRoundInfo = lastRoundInfo;
            this.listPlayerBetInfos.Clear();
        }

        private void ResetRoundInfo()
        {
            RaiderRoundMetaDataInfo lastRoundInfo = DBProvider.GameRaiderofLostArkDBProvider.GetLastRaiderRoundMetaDataInfo();
            if (lastRoundInfo == null || lastRoundInfo.State == RaiderRoundState.Finished)
            {
                lastRoundInfo = new RaiderRoundMetaDataInfo();
                lastRoundInfo = DBProvider.GameRaiderofLostArkDBProvider.AddNewRaiderRoundMetaDataInfo(lastRoundInfo);
            }

            this._currentRoundInfo = lastRoundInfo;
            this.listPlayerBetInfos.Clear();
        }

        public int Join(int userID, string userName, int roundID, int stonesCount)
        {
            lock (_lockJoin)
            {
                if (roundID != this._currentRoundInfo.ID)
                {
                    return OperResult.RESULTCODE_FALSE;
                }
                if (this._currentRoundInfo.State == RaiderRoundState.Finished)
                {
                    return OperResult.RESULTCODE_GAME_RAIDER_ROUNDFINISHED;
                }

                if (this._currentRoundInfo.State == RaiderRoundState.Waiting)
                {
                    StartRound();
                }
            }

            var betInfo = new PlayerBetInfo()
            {
                UserName = userName,
                RaiderRoundID = _currentRoundInfo.ID,
                BetStones = stonesCount,
                Time = MyDateTime.FromDateTime(DateTime.Now)
            };

            //如果保存数据库不成功，将异常抛到外层
            DBProvider.GameRaiderofLostArkDBProvider.SavePlayerBetInfo(betInfo);
            listPlayerBetInfos.Add(betInfo);

            this._currentRoundInfo.AwardPoolSumStones += stonesCount;

            return OperResult.RESULTCODE_TRUE;
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
                    var winnerPersonAllBetCount = this.listPlayerBetInfos.Where(b => b.UserName == winnerBetInfo.UserName).Sum(b => b.BetStones);
                    int expense = (int)Math.Ceiling(this._currentRoundInfo.AwardPoolSumStones * GlobalConfig.GameConfig.RaiderExpense);
                    int winnerGainBetCount = this._currentRoundInfo.AwardPoolSumStones - expense;
                    if (winnerGainBetCount < winnerPersonAllBetCount)
                    {
                        winnerGainBetCount = winnerPersonAllBetCount;
                    }

                    this._currentRoundInfo.EndTime = new MyDateTime(DateTime.Now);
                    this._currentRoundInfo.WinnerUserName = winnerBetInfo.UserName;
                    this._currentRoundInfo.WinStones = winnerGainBetCount;

                    bool isOK = false;
                    CustomerMySqlTransaction myTrans = null;
                    try
                    {
                        myTrans = MyDBHelper.Instance.CreateTrans();
                        DBProvider.GameRaiderofLostArkDBProvider.UpdateRaiderRoundMetaDataInfo(this._currentRoundInfo, myTrans);
                        PlayerController.Instance.WinRaiderGetAward(winnerBetInfo.UserName, winnerGainBetCount, myTrans);

                        myTrans.Commit();
                        isOK = true;
                    }
                    catch (Exception exc)
                    {
                        myTrans.Rollback();
                        isOK = false;
                        LogHelper.Instance.AddErrorLog("RaiderofLostArk Finish Round SaveTo DB Exception. Round Info: " + this._currentRoundInfo.ToString(), exc);
                    }
                    finally
                    {
                        if (myTrans != null)
                        {
                            myTrans.Dispose();
                        }
                    }

                    if (isOK)
                    {
                        if (NotifyAllPlayerRaiderWinnerEvent != null)
                        {
                            NotifyAllPlayerRaiderWinnerEvent(this._currentRoundInfo);
                        }
                        CreateNewRound();
                    }
                }
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("RaiderofLostArk Finish Round Exception. Round Info: " + this._currentRoundInfo.ToString(), exc);
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

        public event Action<RaiderRoundMetaDataInfo> NotifyAllPlayerRaiderWinnerEvent;
    }
}
