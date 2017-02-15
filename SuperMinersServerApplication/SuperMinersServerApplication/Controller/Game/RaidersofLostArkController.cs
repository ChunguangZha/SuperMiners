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

        private int OpenWinTimeMinutes = 5;
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

                //this._currentRoundInfo.JoinedPlayerCount = this.listPlayerUserName.Count;
                return this._currentRoundInfo;
            }
        }

        private List<RaiderPlayerBetInfo> listPlayerBetInfos = new List<RaiderPlayerBetInfo>();

        private List<string> listPlayerUserName = new List<string>();

        public void Init()
        {
            ResetRoundInfo();
        }

        public void StopService()
        {
            FinishRound(null, null);
        }

        public void CreateNewRound()
        {
            RaiderRoundMetaDataInfo lastRoundInfo = new RaiderRoundMetaDataInfo();
            lastRoundInfo = DBProvider.GameRaiderofLostArkDBProvider.AddNewRaiderRoundMetaDataInfo(lastRoundInfo);

            this._currentRoundInfo = lastRoundInfo;
            this.listPlayerBetInfos.Clear();
            this.listPlayerUserName.Clear();
        }

        private void ResetRoundInfo()
        {
            RaiderRoundMetaDataInfo lastRoundInfo = DBProvider.GameRaiderofLostArkDBProvider.GetLastRaiderRoundMetaDataInfo();
            if (lastRoundInfo == null || lastRoundInfo.State == RaiderRoundState.Finished)
            {
                lastRoundInfo = new RaiderRoundMetaDataInfo();
                lastRoundInfo = DBProvider.GameRaiderofLostArkDBProvider.AddNewRaiderRoundMetaDataInfo(lastRoundInfo);
                this._currentRoundInfo = lastRoundInfo;
                this.listPlayerBetInfos.Clear();
                this.listPlayerUserName.Clear();
            }
            else
            {
                this._currentRoundInfo = lastRoundInfo;
                this.listPlayerBetInfos.Clear();
                this.listPlayerUserName.Clear();

                int AwardPoolSumStones = 0;
                int JoinedPlayerCount = 0;

                var lastRoundJoinedBetRecords = DBProvider.GameRaiderofLostArkDBProvider.GetPlayerBetInfoByRoundID(lastRoundInfo.ID, "", -1, -1);
                if (lastRoundJoinedBetRecords != null)
                {
                    foreach (var item in lastRoundJoinedBetRecords)
                    {
                        AwardPoolSumStones += item.BetStones;
                        this.listPlayerBetInfos.Add(item);
                        if (!this.listPlayerUserName.Contains(item.UserName))
                        {
                            this.listPlayerUserName.Add(item.UserName);
                            JoinedPlayerCount++;
                        }
                    }
                    this._currentRoundInfo.AwardPoolSumStones = AwardPoolSumStones;
                    this._currentRoundInfo.JoinedPlayerCount = JoinedPlayerCount;

                    //if (this.listPlayerUserName != null && this.listPlayerUserName.Count > 0 && this.NotifyPlayerToRefreshBetRecordsEvent != null)
                    //{
                    //    this.NotifyPlayerToRefreshBetRecordsEvent(this.CurrentRoundInfo, this.listPlayerUserName);
                    //}
                }
            }

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

                if (!this.listPlayerUserName.Contains(userName))
                {
                    this.listPlayerUserName.Add(userName);
                    this._currentRoundInfo.JoinedPlayerCount++;
                }

                this._currentRoundInfo.AwardPoolSumStones += stonesCount;

                if (this._currentRoundInfo.State == RaiderRoundState.Waiting && this._currentRoundInfo.JoinedPlayerCount >= 2)
                {
                    StartRound();
                }
            }

            var betInfo = new RaiderPlayerBetInfo()
            {
                UserID = userID,
                UserName = userName,
                RaiderRoundID = _currentRoundInfo.ID,
                BetStones = stonesCount,
                Time = MyDateTime.FromDateTime(DateTime.Now)
            };

            //如果保存数据库不成功，将异常抛到外层
            DBProvider.GameRaiderofLostArkDBProvider.SavePlayerBetInfo(betInfo);
            listPlayerBetInfos.Add(betInfo);

            if (this._currentRoundInfo.JoinedPlayerCount >= 2)
            {
                return OperResult.RESULTCODE_TRUE;
            }
            else
            {
                return OperResult.RESULTCODE_GAME_RAIDER_WAITINGSECONDPLAYERJOIN_TOSTART;
            }
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
                    if (this._currentRoundInfo.State == MetaData.Game.RaideroftheLostArk.RaiderRoundState.Finished)
                    {
                        return;
                    }
                    this._currentRoundInfo.State = RaiderRoundState.Finished;
                    if (this.listPlayerBetInfos.Count == 0)
                    {
                        return;
                    }

                    RaiderPlayerBetInfo winnerBetInfo = FindWinner();
                    var winnerPersonAllBetCount = this.listPlayerBetInfos.Where(b => b.UserID == winnerBetInfo.UserID).Sum(b => b.BetStones);
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
                        int result = PlayerController.Instance.WinRaiderGetAward(winnerBetInfo.UserName, winnerGainBetCount, myTrans);
                        if (result != OperResult.RESULTCODE_TRUE)
                        {
                            LogHelper.Instance.AddErrorLog("夺宝奇兵给玩家返奖失败。" + this._currentRoundInfo.ToString(), null);
                        }
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

                        //10秒后再开始下一轮。
                        System.Threading.Thread.Sleep(10 * 1000);

                        CreateNewRound();
                    }
                }
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("RaiderofLostArk Finish Round Exception. Round Info: " + this._currentRoundInfo.ToString(), exc);
            }
        }

        public RaiderPlayerBetInfo FindWinner()
        {
            int ranCount = _ran.Next(2, 10);

            int winnerNumber = 0;
            for (int i = 0; i < ranCount; i++)
            {
                winnerNumber = _ran.Next(this._currentRoundInfo.AwardPoolSumStones);
            }

            RaiderPlayerBetInfo winnerBetInfo = null;

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
        //public event Action<RaiderRoundMetaDataInfo, List<string>> NotifyPlayerToRefreshBetRecordsEvent;
    }
}
