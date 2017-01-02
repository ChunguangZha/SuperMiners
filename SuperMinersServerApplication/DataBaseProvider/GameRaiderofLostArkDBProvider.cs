using MetaData.Game.RaideroftheLostArk;
using MetaData.Game.Roulette;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseProvider
{
    public class GameRaiderofLostArkDBProvider
    {
        public RaiderRoundMetaDataInfo GetLastRaiderRoundMetaDataInfo()
        {
            return null;
        }

        public bool SaveRaiderRoundMetaDataInfo(RaiderRoundMetaDataInfo roundInfo)
        {
            //包含insert和update
            return true;
        }

        public PlayerBetInfo[] GetPlayerBetInfoByRoundID(int roundID)
        {
            return null;
        }

        public bool SavePlayerBetInfo(PlayerBetInfo betInfo)
        {
            return true;
        }


    }
}
