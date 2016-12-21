using MetaData.User;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseProvider
{
    public class DeletedPlayerInfoDBProvider
    {
        public bool AddDeletedPlayer(PlayerInfo player, DateTime time, CustomerMySqlTransaction trans)
        {
            MySqlCommand mycmd = null;
            try
            {
                mycmd = trans.CreateCommand();

                string cmdTextA;
                cmdTextA = "insert into deletedplayerinfo " +
                    "(`UserName`, `NickName`, `Password`, `GroupType`, `IsAgentReferred`, `AgentReferredLevel`, `AgentUserID`, `Alipay`, `AlipayRealName`, `IDCardNo`, `Email`, `QQ`, `RegisterIP`, `InvitationCode`, `RegisterTime`, `LastLoginTime`, `LastLogOutTime`, `ReferrerUserName`, `LastLoginIP`, `LastLoginMac`, " +
                    " `Exp`, `CreditValue`, `RMB`, `FreezingRMB`, `GoldCoin`, `MinesCount`, `StonesReserves`, `TotalProducedStonesCount`, `MinersCount`, `StockOfStones`, `TempOutputStonesStartTime`, `TempOutputStones`, " +
                    " `FreezingStones`, `StockOfDiamonds`, `FreezingDiamonds`, `StoneSellQuan`, `FirstRechargeGoldCoinAward`, `DeleteTime` ) values " +
                    " (@UserName, @NickName, @Password, @GroupType, @IsAgentReferred, @AgentReferredLevel, @AgentUserID, @Alipay, @AlipayRealName, @IDCardNo, @Email, @QQ, @RegisterIP, @InvitationCode, @RegisterTime, @LastLoginTime, @LastLogOutTime, @ReferrerUserName, @LastLoginIP, @LastLoginMac, " +
                    " @Exp, @CreditValue, @RMB, @FreezingRMB, @GoldCoin, @MinesCount, @StonesReserves, @TotalProducedStonesCount, @MinersCount, @StockOfStones, @TempOutputStonesStartTime, @TempOutputStones, " +
                    " @FreezingStones, @StockOfDiamonds,@FreezingDiamonds, @StoneSellQuan, @FirstRechargeGoldCoinAward, @DeleteTime ); ";

                mycmd.Parameters.AddWithValue("@UserName", DESEncrypt.EncryptDES(player.SimpleInfo.UserName));
                mycmd.Parameters.AddWithValue("@NickName", DESEncrypt.EncryptDES(player.SimpleInfo.NickName));
                mycmd.Parameters.AddWithValue("@Password", DESEncrypt.EncryptDES(player.SimpleInfo.Password));
                mycmd.Parameters.AddWithValue("@GroupType", (int)player.SimpleInfo.GroupType);
                mycmd.Parameters.AddWithValue("@IsAgentReferred", player.SimpleInfo.IsAgentReferred);
                mycmd.Parameters.AddWithValue("@AgentReferredLevel", player.SimpleInfo.AgentReferredLevel);
                mycmd.Parameters.AddWithValue("@AgentUserID", player.SimpleInfo.AgentUserID);
                mycmd.Parameters.AddWithValue("@Alipay", string.IsNullOrEmpty(player.SimpleInfo.Alipay) ? null : DESEncrypt.EncryptDES(player.SimpleInfo.Alipay));
                mycmd.Parameters.AddWithValue("@AlipayRealName", string.IsNullOrEmpty(player.SimpleInfo.AlipayRealName) ? null : DESEncrypt.EncryptDES(player.SimpleInfo.AlipayRealName));
                mycmd.Parameters.AddWithValue("@IDCardNo", string.IsNullOrEmpty(player.SimpleInfo.IDCardNo) ? DBNull.Value : (object)player.SimpleInfo.IDCardNo);
                mycmd.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(player.SimpleInfo.AlipayRealName) ? null : DESEncrypt.EncryptDES(player.SimpleInfo.Email));
                mycmd.Parameters.AddWithValue("@QQ", string.IsNullOrEmpty(player.SimpleInfo.QQ) ? null : DESEncrypt.EncryptDES(player.SimpleInfo.QQ));
                mycmd.Parameters.AddWithValue("@RegisterIP", player.SimpleInfo.RegisterIP);
                mycmd.Parameters.AddWithValue("@InvitationCode", DESEncrypt.EncryptDES(player.SimpleInfo.InvitationCode));
                mycmd.Parameters.AddWithValue("@RegisterTime", player.SimpleInfo.RegisterTime);
                mycmd.Parameters.AddWithValue("@LastLoginTime", player.SimpleInfo.LastLoginTime == null ? DBNull.Value : (object)player.SimpleInfo.LastLoginTime);
                mycmd.Parameters.AddWithValue("@LastLogOutTime", player.SimpleInfo.LastLogOutTime == null ? DBNull.Value : (object)player.SimpleInfo.LastLogOutTime);
                mycmd.Parameters.AddWithValue("@ReferrerUserName", DESEncrypt.EncryptDES(player.SimpleInfo.ReferrerUserName));
                mycmd.Parameters.AddWithValue("@LastLoginIP", player.SimpleInfo.LastLoginIP == null ? DBNull.Value : (object)player.SimpleInfo.LastLoginIP);
                mycmd.Parameters.AddWithValue("@LastLoginMac", player.SimpleInfo.LastLoginMac == null ? DBNull.Value : (object)player.SimpleInfo.LastLoginMac);
                mycmd.Parameters.AddWithValue("@Exp", player.FortuneInfo.Exp);
                mycmd.Parameters.AddWithValue("@CreditValue", player.FortuneInfo.CreditValue);
                mycmd.Parameters.AddWithValue("@RMB", player.FortuneInfo.RMB);
                mycmd.Parameters.AddWithValue("@FreezingRMB", player.FortuneInfo.FreezingRMB);
                mycmd.Parameters.AddWithValue("@GoldCoin", player.FortuneInfo.GoldCoin);
                mycmd.Parameters.AddWithValue("@MinesCount", player.FortuneInfo.MinesCount);
                mycmd.Parameters.AddWithValue("@StonesReserves", player.FortuneInfo.StonesReserves);
                mycmd.Parameters.AddWithValue("@TotalProducedStonesCount", player.FortuneInfo.TotalProducedStonesCount);
                mycmd.Parameters.AddWithValue("@MinersCount", player.FortuneInfo.MinersCount);
                mycmd.Parameters.AddWithValue("@StockOfStones", player.FortuneInfo.StockOfStones);
                mycmd.Parameters.AddWithValue("@TempOutputStonesStartTime", player.FortuneInfo.TempOutputStonesStartTime);
                mycmd.Parameters.AddWithValue("@TempOutputStones", player.FortuneInfo.TempOutputStones);
                mycmd.Parameters.AddWithValue("@FreezingStones", player.FortuneInfo.FreezingStones);
                mycmd.Parameters.AddWithValue("@StockOfDiamonds", player.FortuneInfo.StockOfDiamonds);
                mycmd.Parameters.AddWithValue("@FreezingDiamonds", player.FortuneInfo.FreezingDiamonds);
                mycmd.Parameters.AddWithValue("@StoneSellQuan", player.FortuneInfo.StoneSellQuan);
                mycmd.Parameters.AddWithValue("@FirstRechargeGoldCoinAward", player.FortuneInfo.FirstRechargeGoldCoinAward);
                mycmd.Parameters.AddWithValue("@DeleteTime", time);

                mycmd.CommandText = cmdTextA;
                mycmd.ExecuteNonQuery();

                return true;
            }
            finally
            {
                mycmd.Dispose();
            }
        }

        public void AddDeletedPlayer(List<PlayerInfo> players)
        {
            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();
                mycmd = myconn.CreateCommand();

                StringBuilder builder = new StringBuilder();
                builder.Append("insert into deletedplayerinfo values ");

                for (int i = 0; i < players.Count; i++)
                {
                    builder.Append("(");



                    builder.Append(")");

                    if (i != players.Count)
                    {
                        builder.Append(" , ");
                    }
                }
                mycmd.CommandText = builder.ToString();

                mycmd.ExecuteNonQuery();

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

        public PlayerInfo[] GetDeletedPlayers()
        {
            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();
                mycmd = myconn.CreateCommand();

                string sqlText = "select * from deletedplayerinfo ";
                mycmd.CommandText = sqlText;
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(table);
                return MetaDBAdapter<PlayerInfo>.GetPlayerInfoFromDataTable(table);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="alipayAccount"></param>
        /// <param name="alipayRealName"></param>
        /// <returns></returns>
        public int GetDeletedPlayerCountByAlipayAccount(string alipayAccount)
        {
            MySqlConnection myconn = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();

                string cmdText = "select count(id) from deletedplayerinfo where Alipay = @Alipay";
                MySqlCommand mycmd = new MySqlCommand(cmdText, myconn);
                mycmd.Parameters.AddWithValue("@Alipay", DESEncrypt.EncryptDES(alipayAccount));
                object objResult = mycmd.ExecuteScalar();
                mycmd.Dispose();

                if (objResult == DBNull.Value)
                {
                    return 0;
                }
                return Convert.ToInt32(objResult);
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                MyDBHelper.Instance.DisposeConnection(myconn);
            }
        }

    }
}
