using MetaData.Game.GambleStone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseProvider
{
    public class GambleStoneDBProvider
    {
        public bool AddGambleStoneRoundInfo(GambleStoneRoundInfo round)
        {
            return true;
        }

        public bool UpdateGambleStoneRoundInfo(GambleStoneRoundInfo round)
        {
            return false;
        }

        public GambleStoneRoundInfo GetLastGambleStoneRoundInfo()
        {
            return null;
        }

        public GambleStoneDailyScheme GetLastGambleStoneDailyScheme()
        {
            return null;
        }

        public bool AddGambleStoneDailyScheme(GambleStoneDailyScheme dailyScheme)
        {
            return true;
        }

        public bool UpdateGambleStoneDailyScheme(GambleStoneDailyScheme dailyScheme)
        {
            return true;
        }

        public bool AddGambleStoneInningInfo(GambleStoneRoundInfo round, GambleStoneInningInfo inning, CustomerMySqlTransaction myTrans)
        {
            return true;
        }


    }
}
