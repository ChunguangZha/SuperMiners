using MetaData;
using MetaData.Game.RaideroftheLostArk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        private List<PlayerBetInfo> listPlayerBetInfos = new List<PlayerBetInfo>();

        public RaiderRoundLogicRunner(RaiderRoundMetaDataInfo parent)
        {
            this.ParentObject = parent;
        }


    }

    public class RaidersofLastArkController
    {
        private RaiderRoundLogicRunner _currentRoundRunner;

        private int _lastRoundID = 0;

        public void Init()
        {
            //服务器停掉时，不好处理

            _currentRoundRunner = new RaiderRoundLogicRunner(new RaiderRoundMetaDataInfo()
            {
                ID = ++_lastRoundID,
                StartTime = MetaData.MyDateTime.FromDateTime(DateTime.Now),
            });


        }

        public OperResultObject Join(MetaData.User.PlayerInfo player, int stones)
        {
            return null;
        }
    }
}
