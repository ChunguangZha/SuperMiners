using MetaData.Game.GambleStone;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseProvider
{
    public class GambleStoneDBProvider
    {
        private string sqlCreateGambleStoneInningInfoTable = "CREATE TABLE `superminers`.`gamblestoneinninginfo{0}` (" +
                                                            " `id` VARCHAR(40) NOT NULL," +
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
                                                            " CONSTRAINT `GambleStoneInningInfo{0}_FK_GambleRoundID`" +
                                                            "   FOREIGN KEY (`RoundID`)" +
                                                            "   REFERENCES `superminers`.`gamblestoneroundinfo` (`id`)" +
                                                            "   ON DELETE CASCADE" +
                                                            "   ON UPDATE CASCADE);";

        private string sqlCreateGambleStonePlayerBetRecordTable = "CREATE TABLE `superminers`.`gamblestoneplayerbetrecord{0}` ( " +
                                                                 " `id` INT UNSIGNED NOT NULL AUTO_INCREMENT, " +
                                                                 " `UserID` INT UNSIGNED NOT NULL, " +
                                                                 " `Time` DATETIME NOT NULL, " +
                                                                 " `RoundID` INT UNSIGNED NOT NULL, " +
                                                                 " `InningID` VARCHAR(40) NULL, " +
                                                                 " `BetRedStone` INT UNSIGNED NOT NULL, " +
                                                                 " `BetGreenStone` INT UNSIGNED NOT NULL, " +
                                                                 " `BetBlueStone` INT UNSIGNED NOT NULL, " +
                                                                 " `BetPurpleStone` INT UNSIGNED NOT NULL, " +
                                                                 " `WinnedStone` INT UNSIGNED NOT NULL, " +
                                                                 " PRIMARY KEY (`id`), " +
                                                                 " UNIQUE INDEX `id_UNIQUE` (`id` ASC), " +
                                                                 " INDEX `GambleStonePlayerBetRecord{0}_FK_UserID_idx` (`UserID` ASC), " +
                                                                 " INDEX `GambleStonePlayerBetRecord{0}_FK_GambleRoundID_idx` (`RoundID` ASC), " +
                                                                 " CONSTRAINT `GambleStonePlayerBetRecord{0}_FK_UserID` " +
                                                                 "   FOREIGN KEY (`UserID`) " +
                                                                 "   REFERENCES `superminers`.`playersimpleinfo` (`id`) " +
                                                                 "   ON DELETE CASCADE " +
                                                                 "   ON UPDATE CASCADE, " +
                                                                 " CONSTRAINT `GambleStonePlayerBetRecord{0}_FK_GambleStoneRoundID` " +
                                                                 "   FOREIGN KEY (`RoundID`) " +
                                                                 "   REFERENCES `superminers`.`gamblestoneroundinfo` (`id`) " +
                                                                 "   ON DELETE CASCADE " +
                                                                 "   ON UPDATE CASCADE, " +
                                                                 " CONSTRAINT `GambleStonePlayerBetRecord{0}_FK_GambleStoneInningID` " +
                                                                 "   FOREIGN KEY (`InningID`) " +
                                                                 "   REFERENCES `superminers`.`gamblestoneinninginfo{0}` (`id`) " +
                                                                 "   ON DELETE CASCADE " +
                                                                 "   ON UPDATE CASCADE); ";



        public bool AddGambleStoneRoundInfo(GambleStoneRoundInfo round)
        {
            return MyDBHelper.Instance.ConnectionCommandExecuteNonQuery(mycmd =>
            {
                string sqlText = "insert info gamblestoneroundinfo (`StartTime`,`FinishedInningCount`,`EndTime`,`CurrentWinRedCount`,`CurrentWinGreenCount`,`CurrentWinBlueCount`,`CurrentWinPurpleCount`,`LastWinRedCount`,`LastWinGreenCount`,`LastWinBlueCount`,`LastWinPurpleCount`,`AllBetInStone`,`AllWinnedOutStone`,`TableName`) " +
                                 " values ( @StartTime,@FinishedInningCount,@EndTime,@CurrentWinRedCount,@CurrentWinGreenCount,@CurrentWinBlueCount,@CurrentWinPurpleCount,@LastWinRedCount,@LastWinGreenCount,@LastWinBlueCount,@LastWinPurpleCount,@AllBetInStone,@AllWinnedOutStone,@TableName ) ";
                mycmd.CommandText = sqlText;
                mycmd.Parameters.AddWithValue("@StartTime", round.StartTime.ToDateTime());
                mycmd.Parameters.AddWithValue("@FinishedInningCount", round.FinishedInningCount);
                mycmd.Parameters.AddWithValue("@EndTime", round.EndTime == null ? DBNull.Value : (object)round.EndTime.ToDateTime());
                mycmd.Parameters.AddWithValue("@CurrentWinRedCount", round.CurrentWinRedCount);
                mycmd.Parameters.AddWithValue("@CurrentWinGreenCount", round.CurrentWinGreenCount);
                mycmd.Parameters.AddWithValue("@CurrentWinBlueCount", round.CurrentWinBlueCount);
                mycmd.Parameters.AddWithValue("@CurrentWinPurpleCount", round.CurrentWinPurpleCount);
                mycmd.Parameters.AddWithValue("@LastWinRedCount", round.LastWinRedCount);
                mycmd.Parameters.AddWithValue("@LastWinGreenCount", round.LastWinGreenCount);
                mycmd.Parameters.AddWithValue("@LastWinBlueCount", round.LastWinBlueCount);
                mycmd.Parameters.AddWithValue("@LastWinPurpleCount", round.LastWinPurpleCount);
                mycmd.Parameters.AddWithValue("@AllBetInStone", round.AllBetInStone);
                mycmd.Parameters.AddWithValue("@AllWinnedOutStone", round.AllWinnedOutStone);
                mycmd.Parameters.AddWithValue("@TableName", round.TableName);

                mycmd.ExecuteNonQuery();

            });
        }

        public bool UpdateGambleStoneRoundInfo(GambleStoneRoundInfo round, CustomerMySqlTransaction myTrans)
        {
            MySqlCommand mycmd = null;
            try
            {
                mycmd = myTrans.CreateCommand();
                string sqlText = "update gamblestoneroundinfo set `StartTime`=@StartTime,`FinishedInningCount`=@FinishedInningCount,`EndTime`=@EndTime,`CurrentWinRedCount`=@CurrentWinRedCount,"+
                    "`CurrentWinGreenCount`=@CurrentWinGreenCount,`CurrentWinBlueCount`=@CurrentWinBlueCount,`CurrentWinPurpleCount`=@CurrentWinPurpleCount,`LastWinRedCount`=@LastWinRedCount,"+
                    "`LastWinGreenCount`=@LastWinGreenCount,`LastWinBlueCount`=@LastWinBlueCount,`LastWinPurpleCount`=@LastWinPurpleCount,`AllBetInStone`=@AllBetInStone,`AllWinnedOutStone`=@AllWinnedOutStone,`TableName`=@TableName " +
                                 " where `id`=@id ";
                mycmd.CommandText = sqlText;
                mycmd.Parameters.AddWithValue("@StartTime", round.StartTime.ToDateTime());
                mycmd.Parameters.AddWithValue("@FinishedInningCount", round.FinishedInningCount);
                mycmd.Parameters.AddWithValue("@EndTime", round.EndTime == null ? DBNull.Value : (object)round.EndTime.ToDateTime());
                mycmd.Parameters.AddWithValue("@CurrentWinRedCount", round.CurrentWinRedCount);
                mycmd.Parameters.AddWithValue("@CurrentWinGreenCount", round.CurrentWinGreenCount);
                mycmd.Parameters.AddWithValue("@CurrentWinBlueCount", round.CurrentWinBlueCount);
                mycmd.Parameters.AddWithValue("@CurrentWinPurpleCount", round.CurrentWinPurpleCount);
                mycmd.Parameters.AddWithValue("@LastWinRedCount", round.LastWinRedCount);
                mycmd.Parameters.AddWithValue("@LastWinGreenCount", round.LastWinGreenCount);
                mycmd.Parameters.AddWithValue("@LastWinBlueCount", round.LastWinBlueCount);
                mycmd.Parameters.AddWithValue("@LastWinPurpleCount", round.LastWinPurpleCount);
                mycmd.Parameters.AddWithValue("@AllBetInStone", round.AllBetInStone);
                mycmd.Parameters.AddWithValue("@AllWinnedOutStone", round.AllWinnedOutStone);
                mycmd.Parameters.AddWithValue("@TableName", round.TableName);
                mycmd.Parameters.AddWithValue("@id", round.ID);

                mycmd.ExecuteNonQuery();
                return true;
            }
            finally
            {
                if (mycmd != null)
                {
                    mycmd.Dispose();
                }
            }
        }

        public GambleStoneRoundInfo GetLastGambleStoneRoundInfo()
        {
            GambleStoneRoundInfo lastRound = null;
            MyDBHelper.Instance.ConnectionCommandSelect(mycmd =>
            {
                string sqlText = "select * from gamblestoneroundinfo order by id desc limit 1";
                mycmd.CommandText = sqlText;
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(table);
                adapter.Dispose();
                GambleStoneRoundInfo[] rounds = MetaDBAdapter<GambleStoneRoundInfo>.GetGambleStoneRoundInfoFromDataTable(table);
                if (rounds != null && rounds.Length == 1)
                {
                    lastRound = rounds[0];
                }
            });

            return lastRound;
        }

        public GambleStoneDailyScheme GetLastGambleStoneDailyScheme()
        {
            GambleStoneDailyScheme lastScheme = null;
            MyDBHelper.Instance.ConnectionCommandSelect(mycmd =>
            {
                string sqlText = "select * from gamblestonedailyscheme order by id desc limit 1";
                mycmd.CommandText = sqlText;
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(table);
                adapter.Dispose();
                GambleStoneDailyScheme[] schemes = MetaDBAdapter<GambleStoneDailyScheme>.GetGambleStoneDailySchemeFromDataTable(table);
                if (schemes != null && schemes.Length == 1)
                {
                    lastScheme = schemes[0];
                }
            });

            return lastScheme;
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
