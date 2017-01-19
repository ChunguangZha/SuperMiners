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

        public bool UpdateGambleStoneRoundInfo(GambleStoneRoundInfo round, CustomerMySqlTransaction myTrans)
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

        public bool AddGambleStonePlayerBetRecord(GambleStonePlayerBetRecord playerBetRecord, string tableNamePrefix, CustomerMySqlTransaction myTrans)
        {
            return true;
        }
    }
}
