using MetaData;
using MetaData.Game.RaideroftheLostArk;
using MetaData.User;
using SuperMinersServerApplication.Utility;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.Controller.Game
{
    public class RaiderRoundLogicRunner
    {
        private RaiderRoundMetaDataInfo _parentObject;

        public RaiderRoundMetaDataInfo ParentObject
        {
            get { return _parentObject; }
            private set { _parentObject = value; }
        }

        private ConcurrentQueue<PlayerBetInfo> queuePlayerBetInfos = new ConcurrentQueue<PlayerBetInfo>();

        public RaiderRoundLogicRunner(RaiderRoundMetaDataInfo parent)
        {
            this.ParentObject = parent;
        }

        public bool Join(PlayerInfo player, int stonesCount)
        {
            queuePlayerBetInfos.Enqueue(new PlayerBetInfo()
            {
                UserID = player.SimpleInfo.UserID,
                RaiderRoundID = ParentObject.ID,
                BetStones = stonesCount,
                Time = MyDateTime.FromDateTime(DateTime.Now)
            });
            return true;
        }
    }

    public class RaidersofLastArkController
    {
        private RaiderRoundLogicRunner _currentRoundRunner;

        private ConcurrentQueue<RaiderRoundLogicRunner> _queueRunner = new ConcurrentQueue<RaiderRoundLogicRunner>();

        private Thread _thrRound = null;

        private int _lastRoundID = 0;

        public void Init()
        {
            //服务器停掉时，不好处理

            _currentRoundRunner = new RaiderRoundLogicRunner(new RaiderRoundMetaDataInfo()
            {
                ID = ++_lastRoundID,
                StartTime = MetaData.MyDateTime.FromDateTime(DateTime.Now),
            });

            _thrRound = new Thread(new ThreadStart(FinishRound));
            _thrRound.IsBackground = true;
            _thrRound.Name = "RaidersofLastArkController";
            _thrRound.Start();

        }

        private void FinishRound()
        {
            while (true)
            {
                try
                {
                    if (_queueRunner.Count == 0)
                    {
                        Thread.Sleep(100);
                        continue;
                    }


                }
                catch (Exception exc)
                {
                    LogHelper.Instance.AddErrorLog("RaidersofLastArkController Thread Round Exception", exc);
                }
            }
        }

        public OperResultObject Join(MetaData.User.PlayerInfo player, int stones)
        {
            return null;
        }
    }
}
