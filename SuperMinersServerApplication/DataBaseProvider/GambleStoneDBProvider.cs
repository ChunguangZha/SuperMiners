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
        private string sqlCreateGambleStoneInningInfoTable = "CREATE TABLE `superminers`.`gamblestoneinninginfo{0}` (" +
                                                            " `id` INT UNSIGNED NOT NULL AUTO_INCREMENT," +
                                                            " `InningIndex` INT NOT NULL," +
                                                            " `RoundID` INT UNSIGNED NOT NULL," +
                                                            " `CountDownSeconds` INT UNSIGNED NOT NULL DEFAULT 0," +
                                                            " `BetRedStone` INT UNSIGNED NOT NULL DEFAULT 0," +
                                                            " `BetGreenStone` INT UNSIGNED NOT NULL DEFAULT 0," +
                                                            " `BetBlueStone` INT UNSIGNED NOT NULL DEFAULT 0," +
                                                            " `BetPurpleStone` INT UNSIGNED NOT NULL DEFAULT 0," +
                                                            " `WinnedColor` INT UNSIGNED NOT NULL," +
                                                            " `WinnedTimes` INT UNSIGNED NOT NULL," +
                                                            " `WinnedOutStone` INT UNSIGNED NOT NULL," +
                                                            " PRIMARY KEY (`id`)," +
                                                            " UNIQUE INDEX `id_UNIQUE` (`id` ASC)," +
                                                            " INDEX `GambleStoneInningInfo{0}_FK_GambleRoundID_idx` (`RoundID` ASC)," +
                                                            " CONSTRAINT `GambleStoneInningInfo201701_FK_GambleRoundID`" +
                                                            "   FOREIGN KEY (`RoundID`)" +
                                                            "   REFERENCES `superminers`.`gamblestoneroundinfo` (`id`)" +
                                                            "   ON DELETE NO ACTION" +
                                                            "   ON UPDATE NO ACTION);";


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
