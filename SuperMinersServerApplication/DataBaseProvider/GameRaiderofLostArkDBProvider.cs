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
        public RaiderRoundMetaDataInfo GetLastRaiderRoundMetaDataInfo()
        {
            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                string sqlText = "SELECT * FROM superminers.raiderroundmetadatainfo order by id desc limit 1; ";
                mycmd = myconn.CreateCommand();
                mycmd.CommandText = sqlText;
                myconn.Open();

                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(table);
                var roundInfos = MetaDBAdapter<RaiderRoundMetaDataInfo>.GetRaiderRoundMetaDataInfoFromDataTable(table);

                adapter.Dispose();

                if (roundInfos.Length == 1)
                {
                    return roundInfos[0];
                }

                return null;
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
                string sqlText = "UPDATE superminers.raiderroundmetadatainfo " + 
                    " set `State` = @State, `StartTime` = @StartTime, `AwardPoolSumStones` = @AwardPoolSumStones, `WinnerUserName`=@WinnerUserName,`WinStones`=@WinStones,`EndTime`=@EndTime where `id` = @id ; ";

                mycmd = myTrans.CreateCommand();
                mycmd.CommandText = sqlText;
                mycmd.Parameters.AddWithValue("@State", (int)roundInfo.State);
                mycmd.Parameters.AddWithValue("@StartTime", roundInfo.StartTime == null ? DBNull.Value : (object)roundInfo.StartTime.ToDateTime());
                mycmd.Parameters.AddWithValue("@AwardPoolSumStones", roundInfo.AwardPoolSumStones);
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
                string sqlTextA = "insert into superminers.raiderroundmetadatainfo (`State`) values ( @State) ;";

                mycmd = myconn.CreateCommand();
                mycmd.CommandText = sqlTextA;
                mycmd.Parameters.AddWithValue("@State", (int)roundInfo.State);
                mycmd.ExecuteNonQuery();
                mycmd.Dispose();

                //2. Select from DB
                string sqlTextB = "SELECT * FROM superminers.raiderroundmetadatainfo order by id desc limit 1; ";
                mycmd = myconn.CreateCommand();
                mycmd.CommandText = sqlTextB;

                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(table);
                var roundInfos = MetaDBAdapter<RaiderRoundMetaDataInfo>.GetRaiderRoundMetaDataInfoFromDataTable(table);

                adapter.Dispose();

                if (roundInfos.Length == 1)
                {
                    return roundInfos[0];
                }

                return null;
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

        public PlayerBetInfo[] GetPlayerBetInfoByRoundID(int roundID, int pageItemCount, int pageIndex)
        {
            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                mycmd = myconn.CreateCommand();

                string sqlTextA = "SELECT * FROM superminers.raiderplayerbetinfo ";

                string sqlWhere = "";
                if (roundID > 0)
                {
                    sqlWhere = " where RaiderRoundID = @RaiderRoundID ";
                    mycmd.Parameters.AddWithValue("@RaiderRoundID", roundID);
                }

                string sqlOrderLimit = " order by id desc ";
                if (pageItemCount > 0)
                {
                    int start = pageIndex <= 0 ? 0 : (pageIndex - 1) * pageItemCount;
                    sqlOrderLimit += " limit " + start.ToString() + ", " + pageItemCount;
                }

                string sqlAllText = sqlTextA + sqlWhere + sqlOrderLimit;

                mycmd.CommandText = sqlAllText;
                myconn.Open();

                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(table);
                adapter.Dispose();

                return MetaDBAdapter<PlayerBetInfo>.GetPlayerBetInfoFromDataTable(table);
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

        public bool SavePlayerBetInfo(PlayerBetInfo betInfo)
        {
            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();
                //1. Save to DB
                string sqlTextA = "insert into superminers.raiderplayerbetinfo (`RaiderRoundID`,`UserName`,`BetStones`,`Time`) values ( @RaiderRoundID,@UserName,@BetStones,@Time) ;";

                mycmd = myconn.CreateCommand();
                mycmd.CommandText = sqlTextA;
                mycmd.Parameters.AddWithValue("@RaiderRoundID", betInfo.RaiderRoundID);
                mycmd.Parameters.AddWithValue("@UserName", DESEncrypt.EncryptDES(betInfo.UserName));
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
