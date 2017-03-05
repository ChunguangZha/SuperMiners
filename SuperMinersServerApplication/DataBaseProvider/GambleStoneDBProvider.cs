using MetaData;
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
        private static readonly string GambleStoneInningInfoTableNamePrefix = "gamblestoneinninginfo";
        private string sqlCreateGambleStoneInningInfoTable = "CREATE TABLE `" + GambleStoneInningInfoTableNamePrefix + "{0}` (" +
                                                            " `id` VARCHAR(40) NOT NULL," +
                                                            " `InningIndex` INT NOT NULL," +
                                                            " `RoundID` INT UNSIGNED NOT NULL," +
                                                            " `CountDownSeconds` INT UNSIGNED NOT NULL DEFAULT 0," +
                                                            " `State` TINYINT(1) NOT NULL DEFAULT 9 COMMENT 'Readying=0;BetInWaiting=1;Opening=2;Finished=9;'," +
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
                                                            "   REFERENCES `gamblestoneroundinfo` (`id`)" +
                                                            "   ON DELETE CASCADE" +
                                                            "   ON UPDATE CASCADE);";

//        ALTER TABLE `gamblestoneinninginfo201702` 
        //ADD COLUMN `State` TINYINT(1) NOT NULL DEFAULT 9 COMMENT 'Readying=0;BetInWaiting=1;Opening=2;Finished=9;' AFTER `WinnedOutStone`;

        private static readonly string GambleStonePlayerBetRecordTableNamePrefix = "gamblestoneplayerbetrecord";
        private string sqlCreateGambleStonePlayerBetRecordTable = "CREATE TABLE `" + GambleStonePlayerBetRecordTableNamePrefix + "{0}` ( " +
                                                                 " `id` INT UNSIGNED NOT NULL AUTO_INCREMENT, " +
                                                                 " `UserID` INT UNSIGNED NOT NULL, " +
                                                                 " `Time` DATETIME NOT NULL, " +
                                                                 " `RoundID` INT UNSIGNED NOT NULL, " +
                                                                 " `InningID` VARCHAR(40) NULL, " +
                                                                 " `InningIndex` INT NOT NULL DEFAULT 0, " +
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
                                                                 "   REFERENCES `playersimpleinfo` (`id`) " +
                                                                 "   ON DELETE CASCADE " +
                                                                 "   ON UPDATE CASCADE, " +
                                                                 " CONSTRAINT `GambleStonePlayerBetRecord{0}_FK_GambleStoneRoundID` " +
                                                                 "   FOREIGN KEY (`RoundID`) " +
                                                                 "   REFERENCES `gamblestoneroundinfo` (`id`) " +
                                                                 "   ON DELETE CASCADE " +
                                                                 "   ON UPDATE CASCADE, " +
                                                                 " CONSTRAINT `GambleStonePlayerBetRecord{0}_FK_GambleStoneInningID` " +
                                                                 "   FOREIGN KEY (`InningID`) " +
                                                                 "   REFERENCES `gamblestoneinninginfo{0}` (`id`) " +
                                                                 "   ON DELETE CASCADE " +
                                                                 "   ON UPDATE CASCADE); ";


//        ALTER TABLE `gamblestoneplayerbetrecord201702` 
//ADD COLUMN `InningIndex` INT NOT NULL DEFAULT 0 AFTER `InningID`;



        public bool AddGambleStoneRoundInfo(GambleStoneRoundInfo round)
        {
            return MyDBHelper.Instance.ConnectionCommandExecuteNonQuery(mycmd =>
            {
                string sqlText = "insert into gamblestoneroundinfo (`StartTime`,`FinishedInningCount`,`EndTime`,`CurrentWinRedCount`,`CurrentWinGreenCount`,`CurrentWinBlueCount`,`CurrentWinPurpleCount`,`LastWinRedCount`,`LastWinGreenCount`,`LastWinBlueCount`,`LastWinPurpleCount`,`AllBetInStone`,`AllWinnedOutStone`,`WinColorItems`,`TableName`) " +
                                 " values ( @StartTime,@FinishedInningCount,@EndTime,@CurrentWinRedCount,@CurrentWinGreenCount,@CurrentWinBlueCount,@CurrentWinPurpleCount,@LastWinRedCount,@LastWinGreenCount,@LastWinBlueCount,@LastWinPurpleCount,@AllBetInStone,@AllWinnedOutStone,@WinColorItems,@TableName ) ";
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
                mycmd.Parameters.AddWithValue("@WinColorItems", BytesConverter.ConvertByteArrayToBytes(round.WinColorItems));
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
                    "`LastWinGreenCount`=@LastWinGreenCount,`LastWinBlueCount`=@LastWinBlueCount,`LastWinPurpleCount`=@LastWinPurpleCount,`AllBetInStone`=@AllBetInStone,"+
                    "`AllWinnedOutStone`=@AllWinnedOutStone,`WinColorItems`=@WinColorItems,`TableName`=@TableName " +
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
                mycmd.Parameters.AddWithValue("@WinColorItems", BytesConverter.ConvertByteArrayToBytes(round.WinColorItems));
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
                GambleStoneRoundInfo[] rounds = MetaDBAdapter<GambleStoneRoundInfo>.GetGambleStoneRoundInfoFromDataTable(table);
                if (rounds != null && rounds.Length == 1)
                {
                    lastRound = rounds[0];
                }
                table.Clear();
                table.Dispose();
                adapter.Dispose();
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
                GambleStoneDailyScheme[] schemes = MetaDBAdapter<GambleStoneDailyScheme>.GetGambleStoneDailySchemeFromDataTable(table);
                if (schemes != null && schemes.Length == 1)
                {
                    lastScheme = schemes[0];
                }
                table.Clear();
                table.Dispose();
                adapter.Dispose();
            });

            return lastScheme;
        }

        public bool AddGambleStoneDailyScheme(GambleStoneDailyScheme dailyScheme)
        {
            return MyDBHelper.Instance.ConnectionCommandExecuteNonQuery(mycmd =>
            {
                string sqlText = "insert into gamblestonedailyscheme (`Date`,`ProfitStoneObjective`,`AllBetInStone`,`AllWinnedOutStone` ) " +
                                 " values ( @Date,@ProfitStoneObjective,@AllBetInStone,@AllWinnedOutStone ) ";
                mycmd.CommandText = sqlText;
                mycmd.Parameters.AddWithValue("@Date", dailyScheme.Date.ToDateTime());
                mycmd.Parameters.AddWithValue("@ProfitStoneObjective", dailyScheme.ProfitStoneObjective);
                mycmd.Parameters.AddWithValue("@AllBetInStone", dailyScheme.AllBetInStone);
                mycmd.Parameters.AddWithValue("@AllWinnedOutStone", dailyScheme.AllWinnedOutStone);

                mycmd.ExecuteNonQuery();

            });
        }

        public bool UpdateGambleStoneDailyScheme(GambleStoneDailyScheme dailyScheme)
        {
            return MyDBHelper.Instance.ConnectionCommandExecuteNonQuery(mycmd =>
            {
                string sqlText = "update gamblestonedailyscheme set `Date`=@Date,`ProfitStoneObjective`=@ProfitStoneObjective,`AllBetInStone`=@AllBetInStone,`AllWinnedOutStone`=@AllWinnedOutStone " +
                                 " where id = @ID ";
                mycmd.CommandText = sqlText;
                mycmd.Parameters.AddWithValue("@Date", dailyScheme.Date.ToDateTime());
                mycmd.Parameters.AddWithValue("@ProfitStoneObjective", dailyScheme.ProfitStoneObjective);
                mycmd.Parameters.AddWithValue("@AllBetInStone", dailyScheme.AllBetInStone);
                mycmd.Parameters.AddWithValue("@AllWinnedOutStone", dailyScheme.AllWinnedOutStone);
                mycmd.Parameters.AddWithValue("@ID", dailyScheme.ID);

                mycmd.ExecuteNonQuery();

            });
        }

        public bool AddGambleStoneInningInfo(GambleStoneRoundInfo round, GambleStoneInningInfo inning, CustomerMySqlTransaction myTrans)
        {
            MySqlCommand mycmd = null;
            try
            {
                string tableName = GambleStoneInningInfoTableNamePrefix + round.TableName;
                mycmd = myTrans.CreateCommand();
                string sqlSelectTableText = "show tables where Tables_in_" + MyDBHelper.DataBaseServerName + " = @tableName ";
                mycmd.CommandText = sqlSelectTableText;
                mycmd.Parameters.AddWithValue("@tableName", tableName);
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(table);
                adapter.Dispose();
                if (table.Rows.Count == 0)
                {
                    //表不存在，则创建
                    mycmd.CommandText = string.Format(this.sqlCreateGambleStoneInningInfoTable, round.TableName);
                    mycmd.ExecuteNonQuery();
                }

                string sqlText = "insert into " + tableName + " (`id`,`InningIndex`,`RoundID`,`CountDownSeconds`,`State`,`BetRedStone`,`BetGreenStone`,`BetBlueStone`,`BetPurpleStone`,`WinnedColor`,`WinnedTimes`,`WinnedOutStone`) " +
                                " values (@ID,@InningIndex,@RoundID,@CountDownSeconds,@State,@BetRedStone,@BetGreenStone,@BetBlueStone,@BetPurpleStone,@WinnedColor,@WinnedTimes,@WinnedOutStone ) ";
                mycmd.CommandText = sqlText;
                mycmd.Parameters.AddWithValue("@ID", inning.ID);
                mycmd.Parameters.AddWithValue("@InningIndex", inning.InningIndex);
                mycmd.Parameters.AddWithValue("@RoundID", inning.RoundID);
                mycmd.Parameters.AddWithValue("@CountDownSeconds", inning.CountDownSeconds);
                mycmd.Parameters.AddWithValue("@State", (byte)inning.State);
                mycmd.Parameters.AddWithValue("@BetRedStone", inning.BetRedStone);
                mycmd.Parameters.AddWithValue("@BetGreenStone", inning.BetGreenStone);
                mycmd.Parameters.AddWithValue("@BetBlueStone", inning.BetBlueStone);
                mycmd.Parameters.AddWithValue("@BetPurpleStone", inning.BetPurpleStone);
                mycmd.Parameters.AddWithValue("@WinnedColor", inning.WinnedColor);
                mycmd.Parameters.AddWithValue("@WinnedTimes", inning.WinnedTimes);
                mycmd.Parameters.AddWithValue("@WinnedOutStone", inning.WinnedOutStone);
                mycmd.ExecuteNonQuery();

                return true;
            }
            finally
            {
                mycmd.Dispose();
            }
        }

        public bool AddGambleStonePlayerBetRecord(GambleStonePlayerBetRecord playerBetRecord, string tableNameSuffix, CustomerMySqlTransaction myTrans)
        {
            MySqlCommand mycmd = null;
            try
            {
                string tableName = GambleStonePlayerBetRecordTableNamePrefix + tableNameSuffix;
                mycmd = myTrans.CreateCommand();
                string sqlSelectTableText = "show tables where Tables_in_" + MyDBHelper.DataBaseServerName + " = @tableName ";
                mycmd.CommandText = sqlSelectTableText;
                mycmd.Parameters.AddWithValue("@tableName", tableName);
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(table);
                adapter.Dispose();
                if (table.Rows.Count == 0)
                {
                    //表不存在，则创建
                    mycmd.CommandText = string.Format(this.sqlCreateGambleStonePlayerBetRecordTable, tableNameSuffix);
                    mycmd.ExecuteNonQuery();
                }

                string sqlText = "insert into " + tableName + " (`UserID`,`Time`,`RoundID`,`InningID`,`InningIndex`,`BetRedStone`,`BetGreenStone`,`BetBlueStone`,`BetPurpleStone`,`WinnedStone`) " +
                                " values (@UserID,@Time,@RoundID,@InningID,@InningIndex,@BetRedStone,@BetGreenStone,@BetBlueStone,@BetPurpleStone,@WinnedStone ) ";
                mycmd.CommandText = sqlText;
                mycmd.Parameters.AddWithValue("@UserID", playerBetRecord.UserID);
                mycmd.Parameters.AddWithValue("@Time", playerBetRecord.Time.ToDateTime());
                mycmd.Parameters.AddWithValue("@RoundID", playerBetRecord.RoundID);
                mycmd.Parameters.AddWithValue("@InningID", playerBetRecord.InningID);
                mycmd.Parameters.AddWithValue("@InningIndex", playerBetRecord.InningIndex);
                mycmd.Parameters.AddWithValue("@BetRedStone", playerBetRecord.BetRedStone);
                mycmd.Parameters.AddWithValue("@BetGreenStone", playerBetRecord.BetGreenStone);
                mycmd.Parameters.AddWithValue("@BetBlueStone", playerBetRecord.BetBlueStone);
                mycmd.Parameters.AddWithValue("@BetPurpleStone", playerBetRecord.BetPurpleStone);
                mycmd.Parameters.AddWithValue("@WinnedStone", playerBetRecord.WinnedStone);
                mycmd.ExecuteNonQuery();

                return true;
            }
            finally
            {
                mycmd.Dispose();
            }
        }

        public GambleStonePlayerBetRecord[] GetLastMonthGambleStonePlayerBetRecord(int userID)
        {
            DateTime todayMaxTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            DateTime lastMonthTime = todayMaxTime.AddMonths(-1);

            GambleStonePlayerBetRecord[] currentMonth = GetMonthGambleStonePlayerBetRecord(userID, todayMaxTime.Year, todayMaxTime.Month, null);
            GambleStonePlayerBetRecord[] lastMonth = GetMonthGambleStonePlayerBetRecord(userID, lastMonthTime.Year, lastMonthTime.Month, new MyDateTime(lastMonthTime));

            List<GambleStonePlayerBetRecord> listRecords = new List<GambleStonePlayerBetRecord>();
            if (currentMonth != null)
            {
                listRecords.AddRange(currentMonth);
            }
            if (lastMonth != null)
            {
                listRecords.AddRange(lastMonth);
            }

            return listRecords.ToArray();
        }

        public GambleStonePlayerBetRecord[] GetMonthGambleStonePlayerBetRecord(int userID, int year, int month, MyDateTime beginTime)
        {
            GambleStonePlayerBetRecord[] records = null;

            MyDBHelper.Instance.ConnectionCommandSelect(mycmd =>
            {
                string tableName = GambleStonePlayerBetRecordTableNamePrefix + year.ToString("0000") + month.ToString("00");

                string sqlSelectTableName = "show tables where Tables_in_" + MyDBHelper.DataBaseServerName + " = '" + tableName + "'";
                mycmd.CommandText = sqlSelectTableName;
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(table);
                adapter.Dispose();
                if (table.Rows.Count == 0)
                {
                    return;
                }

                
                string sqlText = "select * from " + tableName + " where UserID = @UserID";
                if (beginTime != null && beginTime.Year == year && beginTime.Month == month)
                {
                    sqlText += " and Time >= @BeginTime ";
                    mycmd.Parameters.AddWithValue("@BeginTime", beginTime);
                }
                sqlText += " order by Time desc";
                mycmd.CommandText = sqlText;
                mycmd.Parameters.AddWithValue("@UserID", userID);
                table.Clear();
                adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(table);
                records = MetaDBAdapter<GambleStonePlayerBetRecord>.GetGambleStonePlayerBetRecordFromDataTable(table);

                table.Clear();
                table.Dispose();
                adapter.Dispose();
            });

            return records;
        }
    }
}
