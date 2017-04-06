using MetaData.Game.RaideroftheLostArk;
using MetaData.Game.Roulette;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseProvider
{
    public class GameRaiderofLostArkDBProvider
    {
        public PlayerRaiderRoundHistoryRecordInfo[] GetPlayerRaiderRoundHistoryRecordInfo(int userID, int pageItemCount, int pageIndex)
        {
            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();

                string sqlOrderLimit = " order by b.RaiderRoundID desc ";
                if (pageItemCount > 0)
                {
                    int start = pageIndex <= 0 ? 0 : (pageIndex - 1) * pageItemCount;
                    sqlOrderLimit += " limit " + start.ToString() + ", " + pageItemCount;
                }

                string sqlTextA = "select ttt.*, r.* from " +
                                    " (SELECT b.RaiderRoundID, sum(b.BetStones) as AllBetStones " + 
                                    " FROM  raiderplayerbetinfo b " +  
                                    " where b.UserID = @UserID " +
                                    " group by b.RaiderRoundID  " + sqlOrderLimit + " ) ttt " +
                                    "  left join   raiderroundmetadatainfo r  on ttt.RaiderRoundID = r.id ";

                mycmd = myconn.CreateCommand();
                mycmd.CommandText = sqlTextA;
                mycmd.Parameters.AddWithValue("@UserID", userID);
                myconn.Open();

                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(table);

                var lists = MetaDBAdapter<PlayerRaiderRoundHistoryRecordInfo>.GetPlayerRaiderRoundHistoryRecordInfoFromDataTable(table);

                table.Clear();
                table.Dispose();
                adapter.Dispose();

                return lists;
            }
            finally
            {
                if (mycmd != null)
                {
                    mycmd.Dispose();
                }
                if (myconn != null)
                {
                    myconn.Close();
                    myconn.Dispose();
                }
            }
        }

        public RaiderRoundMetaDataInfo[] GetHistoryRaiderRoundRecords(int pageItemCount, int pageIndex)
        {
            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                string sqlTextA = "SELECT * FROM  raiderroundmetadatainfo where State = @State ";

                string sqlOrderLimit = " order by id desc ";
                if (pageItemCount > 0)
                {
                    int start = pageIndex <= 0 ? 0 : (pageIndex - 1) * pageItemCount;
                    sqlOrderLimit += " limit " + start.ToString() + ", " + pageItemCount;
                }

                string sqlAllText = sqlTextA + sqlOrderLimit;

                mycmd = myconn.CreateCommand();
                mycmd.CommandText = sqlAllText;
                mycmd.Parameters.AddWithValue("@State", (int)RaiderRoundState.Finished);
                myconn.Open();

                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(table);

                var lists = MetaDBAdapter<RaiderRoundMetaDataInfo>.GetRaiderRoundMetaDataInfoFromDataTable(table);

                table.Clear();
                table.Dispose();
                adapter.Dispose();
                return lists;
            }
            finally
            {
                if (mycmd != null)
                {
                    mycmd.Dispose();
                }
                if (myconn != null)
                {
                    myconn.Close();
                    myconn.Dispose();
                }
            }
        }

        public RaiderRoundMetaDataInfo GetLastRaiderRoundMetaDataInfo()
        {
            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                string sqlText = "SELECT * FROM  raiderroundmetadatainfo order by id desc limit 1; ";
                mycmd = myconn.CreateCommand();
                mycmd.CommandText = sqlText;
                myconn.Open();

                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(table);

                var roundInfos = MetaDBAdapter<RaiderRoundMetaDataInfo>.GetRaiderRoundMetaDataInfoFromDataTable(table);

                table.Clear();
                table.Dispose();
                adapter.Dispose();
                if (roundInfos == null || roundInfos.Length == 0)
                {
                    return null;
                }

                return roundInfos[0];
            }
            finally
            {
                if (mycmd != null)
                {
                    mycmd.Dispose();
                }
                if (myconn != null)
                {
                    myconn.Close();
                    myconn.Dispose();
                }
            }
        }

        public bool UpdateRaiderRoundMetaDataInfo(RaiderRoundMetaDataInfo roundInfo, CustomerMySqlTransaction myTrans)
        {
            MySqlCommand mycmd = null;
            try
            {
                string sqlText = "UPDATE  raiderroundmetadatainfo " +
                    " set `State` = @State, `StartTime` = @StartTime, `AwardPoolSumStones` = @AwardPoolSumStones, `JoinedPlayerCount`=@JoinedPlayerCount, `WinnerUserName`=@WinnerUserName,`WinStones`=@WinStones,`EndTime`=@EndTime where `id` = @id ; ";

                mycmd = myTrans.CreateCommand();
                mycmd.CommandText = sqlText;
                mycmd.Parameters.AddWithValue("@State", (int)roundInfo.State);
                mycmd.Parameters.AddWithValue("@StartTime", roundInfo.StartTime == null ? DBNull.Value : (object)roundInfo.StartTime.ToDateTime());
                mycmd.Parameters.AddWithValue("@AwardPoolSumStones", roundInfo.AwardPoolSumStones);
                mycmd.Parameters.AddWithValue("@JoinedPlayerCount", roundInfo.JoinedPlayerCount);
                mycmd.Parameters.AddWithValue("@WinnerUserName", string.IsNullOrEmpty(roundInfo.WinnerUserName) ? DBNull.Value : (object)DESEncrypt.EncryptDES(roundInfo.WinnerUserName));
                mycmd.Parameters.AddWithValue("@WinStones", roundInfo.WinStones);
                mycmd.Parameters.AddWithValue("@EndTime", roundInfo.EndTime == null ? DBNull.Value : (object)roundInfo.EndTime.ToDateTime());
                mycmd.Parameters.AddWithValue("@id", roundInfo.ID);

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

        public RaiderRoundMetaDataInfo AddNewRaiderRoundMetaDataInfo(RaiderRoundMetaDataInfo roundInfo)
        {
            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();
                //1. Save to DB
                string sqlTextA = "insert into  raiderroundmetadatainfo (`State`) values ( @State) ;";

                mycmd = myconn.CreateCommand();
                mycmd.CommandText = sqlTextA;
                mycmd.Parameters.AddWithValue("@State", (int)roundInfo.State);
                mycmd.ExecuteNonQuery();
                mycmd.Dispose();

                //2. Select from DB
                string sqlTextB = "SELECT * FROM  raiderroundmetadatainfo order by id desc limit 1; ";
                mycmd = myconn.CreateCommand();
                mycmd.CommandText = sqlTextB;

                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(table);

                var roundInfos = MetaDBAdapter<RaiderRoundMetaDataInfo>.GetRaiderRoundMetaDataInfoFromDataTable(table);
                table.Clear();
                table.Dispose();
                adapter.Dispose();

                if (roundInfos == null || roundInfos.Length == 0)
                {
                    return null;
                }

                return roundInfos[0];
            }
            finally
            {
                if (mycmd != null)
                {
                    mycmd.Dispose();
                }
                MyDBHelper.Instance.DisposeConnection(myconn);
            }
        }

        public RaiderPlayerBetInfo[] GetPlayerBetInfoByRoundID(int roundID, string userName, int pageItemCount, int pageIndex)
        {
            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                mycmd = myconn.CreateCommand();

                string sqlTextA = "SELECT * FROM  raiderplayerbetinfo ";

                string sqlWhere = "";
                if (roundID > 0)
                {
                    sqlWhere = " where RaiderRoundID = @RaiderRoundID ";
                    mycmd.Parameters.AddWithValue("@RaiderRoundID", roundID);
                }
                if (!string.IsNullOrEmpty(userName))
                {
                    if (sqlWhere.Length == 0)
                    {
                        sqlWhere = " where ";
                    }
                    else
                    {
                        sqlWhere += " and ";
                    }

                    sqlWhere += " UserID = ( select id from   playersimpleinfo where UserName = @UserName ) ";
                    mycmd.Parameters.AddWithValue("@UserName", DESEncrypt.EncryptDES(userName));
                }

                string sqlOrderLimit = " order by id desc ";
                if (pageItemCount > 0)
                {
                    int start = pageIndex <= 0 ? 0 : (pageIndex - 1) * pageItemCount;
                    sqlOrderLimit += " limit " + start.ToString() + ", " + pageItemCount;
                }

                string sqlAllText = "select ttt.*, s.UserName as UserName from " +
                                    " ( " + sqlTextA + sqlWhere + sqlOrderLimit +
                                    " ) ttt " +
                                    "  left join   playersimpleinfo s  on ttt.UserID = s.id ";

                mycmd.CommandText = sqlAllText;
                myconn.Open();

                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(table);

                var lists = MetaDBAdapter<RaiderPlayerBetInfo>.GetPlayerBetInfoFromDataTable(table);

                table.Clear();
                table.Dispose();
                adapter.Dispose();
                return lists;
            }
            finally
            {
                if (mycmd != null)
                {
                    mycmd.Dispose();
                }
                if (myconn != null)
                {
                    myconn.Close();
                    myconn.Dispose();
                }
            }
        }

        public bool SavePlayerBetInfo(RaiderPlayerBetInfo betInfo)
        {
            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();
                //1. Save to DB

                string sqlTextA = "";

#if V1
                sqlTextA = "insert into  raiderplayerbetinfo (`RaiderRoundID`,`UserID`,`UserName`,`BetStones`,`Time`) values ( @RaiderRoundID,@UserID,@UserName,@BetStones,@Time) ;";

#else
                sqlTextA = "insert into  raiderplayerbetinfo (`RaiderRoundID`,`UserID`,`BetStones`,`Time`) values ( @RaiderRoundID,@UserID,@BetStones,@Time) ;";

#endif

                mycmd = myconn.CreateCommand();
                mycmd.CommandText = sqlTextA;
                mycmd.Parameters.AddWithValue("@RaiderRoundID", betInfo.RaiderRoundID);
                mycmd.Parameters.AddWithValue("@UserID", betInfo.UserID);
#if V1
                mycmd.Parameters.AddWithValue("@UserName", DESEncrypt.EncryptDES(betInfo.UserName));
#endif
                mycmd.Parameters.AddWithValue("@BetStones", betInfo.BetStones);
                mycmd.Parameters.AddWithValue("@Time", betInfo.Time.ToDateTime());
                mycmd.ExecuteNonQuery();
                return true;
            }
            finally
            {
                if (mycmd != null)
                {
                    mycmd.Dispose();
                }
                MyDBHelper.Instance.DisposeConnection(myconn);
            }
        }


    }
}
