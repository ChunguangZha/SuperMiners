using MetaData;
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
    public class UserInfoDBProvider
    {
        public bool AddPlayer(PlayerInfo player, CustomerMySqlTransaction trans)
        {
            MySqlCommand mycmd = null;
            try
            {
                mycmd = trans.CreateCommand();

                string cmdTextA;
                if (string.IsNullOrEmpty(player.SimpleInfo.ReferrerUserName))
                {
                    cmdTextA = "insert into playersimpleinfo " +
                        "(`UserName`, `Password`, `Alipay`, `AlipayRealName`, `RegisterIP`, `InvitationCode`, `RegisterTime`) values " +
                        " (@UserName, @Password, @Alipay, @AlipayRealName, @RegisterIP, @InvitationCode, @RegisterTime); ";
                }
                else
                {
                    cmdTextA = "insert into playersimpleinfo " +
                        "(`UserName`, `Password`, `Alipay`, `AlipayRealName`, `RegisterIP`, `InvitationCode`, `RegisterTime`, `ReferrerUserID`) values " +
                        " (@UserName, @Password, @Alipay, @AlipayRealName, @RegisterIP, @InvitationCode, @RegisterTime, (select b.id from playersimpleinfo b where b.UserName = @ReferrerUserName)); ";
                    mycmd.Parameters.AddWithValue("@ReferrerUserName", DESEncrypt.EncryptDES(player.SimpleInfo.ReferrerUserName));
                }
                mycmd.CommandText = cmdTextA;
                mycmd.Parameters.AddWithValue("@UserName", DESEncrypt.EncryptDES(player.SimpleInfo.UserName));
                mycmd.Parameters.AddWithValue("@Password", DESEncrypt.EncryptDES(player.SimpleInfo.Password));
                mycmd.Parameters.AddWithValue("@Alipay", string.IsNullOrEmpty(player.SimpleInfo.Alipay) ? null : DESEncrypt.EncryptDES(player.SimpleInfo.Alipay));
                mycmd.Parameters.AddWithValue("@AlipayRealName", string.IsNullOrEmpty(player.SimpleInfo.AlipayRealName) ? null : DESEncrypt.EncryptDES(player.SimpleInfo.AlipayRealName));
                mycmd.Parameters.AddWithValue("@RegisterIP", player.SimpleInfo.RegisterIP);
                mycmd.Parameters.AddWithValue("@InvitationCode", DESEncrypt.EncryptDES(player.SimpleInfo.InvitationCode));
                mycmd.Parameters.AddWithValue("@RegisterTime", player.SimpleInfo.RegisterTime);

                mycmd.ExecuteNonQuery();

                string cmdTextS = "select id from playersimpleinfo where UserName = @UserName";
                mycmd.CommandText = cmdTextS;
                object result = mycmd.ExecuteScalar();
                int userId = Convert.ToInt32(result);

                string cmdTextB = "insert into playerfortuneinfo " +
                    "(`userId`, `Exp`, `RMB`, `GoldCoin`, `MinesCount`, `StonesReserves`, `TotalProducedStonesCount`, `MinersCount`, `StockOfStones`, " +
                    " `FreezingStones`, `StockOfDiamonds`, `FreezingDiamonds`) values " +
                    " (@userId, @Exp, @RMB, @GoldCoin, @MinesCount, @StonesReserves, @TotalProducedStonesCount, @MinersCount, @StockOfStones, " +
                    " @FreezingStones, @StockOfDiamonds,@FreezingDiamonds)";
                mycmd.CommandText = cmdTextB;
                mycmd.Parameters.AddWithValue("@userId", userId);
                mycmd.Parameters.AddWithValue("@Exp", player.FortuneInfo.Exp);
                mycmd.Parameters.AddWithValue("@RMB", player.FortuneInfo.RMB);
                mycmd.Parameters.AddWithValue("@GoldCoin", player.FortuneInfo.GoldCoin);
                mycmd.Parameters.AddWithValue("@MinesCount", player.FortuneInfo.MinesCount);
                mycmd.Parameters.AddWithValue("@StonesReserves", player.FortuneInfo.StonesReserves);
                mycmd.Parameters.AddWithValue("@TotalProducedStonesCount", player.FortuneInfo.TotalProducedStonesCount);
                mycmd.Parameters.AddWithValue("@MinersCount", player.FortuneInfo.MinersCount);
                mycmd.Parameters.AddWithValue("@StockOfStones", player.FortuneInfo.StockOfStones);
                mycmd.Parameters.AddWithValue("@FreezingStones", player.FortuneInfo.FreezingStones);
                mycmd.Parameters.AddWithValue("@StockOfDiamonds", player.FortuneInfo.StockOfDiamonds);
                mycmd.Parameters.AddWithValue("@FreezingDiamonds", player.FortuneInfo.FreezingDiamonds);

                mycmd.ExecuteNonQuery();

                return true;
            }
            finally
            {
                mycmd.Dispose();
            }
        }

        public bool SavePlayerFortuneInfo(PlayerFortuneInfo playerFortune, CustomerMySqlTransaction trans)
        {
            MySqlCommand mycmd = null;
            try
            {
                string cmdTextB = "UPDATE `playerfortuneinfo` SET "
                    + " `Exp`=@Exp, `RMB`=@RMB, `GoldCoin`=@GoldCoin, `MinesCount`=@MinesCount, `StonesReserves`=@StonesReserves, `TotalProducedStonesCount`=@TotalProducedStonesCount, "
                    + " `MinersCount`=@MinersCount, `StockOfStones`=@StockOfStones,`TempOutputStonesStartTime`=@TempOutputStonesStartTime,`TempOutputStones`=@TempOutputStones,"
                    + " `FreezingStones`=@FreezingStones, `StockOfDiamonds`=@StockOfDiamonds, `FreezingDiamonds`=@FreezingDiamonds "
                    + " WHERE `UserID`=(SELECT b.id FROM playersimpleinfo b where b.UserName = @UserName);";

                mycmd = trans.CreateCommand();
                mycmd.CommandText = cmdTextB;

                mycmd.Parameters.AddWithValue("@Exp", playerFortune.Exp);
                mycmd.Parameters.AddWithValue("@RMB", playerFortune.RMB);
                mycmd.Parameters.AddWithValue("@GoldCoin", playerFortune.GoldCoin);
                mycmd.Parameters.AddWithValue("@MinesCount", playerFortune.MinesCount);
                mycmd.Parameters.AddWithValue("@StonesReserves", playerFortune.StonesReserves);
                mycmd.Parameters.AddWithValue("@TotalProducedStonesCount", playerFortune.TotalProducedStonesCount);
                mycmd.Parameters.AddWithValue("@MinersCount", playerFortune.MinersCount);
                mycmd.Parameters.AddWithValue("@StockOfStones", playerFortune.StockOfStones);
                mycmd.Parameters.AddWithValue("@TempOutputStonesStartTime", playerFortune.TempOutputStonesStartTime);
                mycmd.Parameters.AddWithValue("@TempOutputStones", playerFortune.TempOutputStones);
                mycmd.Parameters.AddWithValue("@FreezingStones", playerFortune.FreezingStones);
                mycmd.Parameters.AddWithValue("@StockOfDiamonds", playerFortune.StockOfDiamonds);
                mycmd.Parameters.AddWithValue("@FreezingDiamonds", playerFortune.FreezingDiamonds);
                mycmd.Parameters.AddWithValue("@UserName", DESEncrypt.EncryptDES(playerFortune.UserName));

                mycmd.ExecuteNonQuery();
                mycmd.Dispose();

                return true;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public bool SavePlayerFortuneInfo(PlayerFortuneInfo playerFortune)
        {
            var trans = MyDBHelper.Instance.CreateTrans();
            try
            {
                bool isOK = SavePlayerFortuneInfo(playerFortune, trans);
                trans.Commit();
                return isOK;
            }
            catch (Exception exc)
            {
                trans.Rollback();
                throw exc;
            }
            finally
            {
                trans.Dispose();
            }
        }

        public bool SavePlayerSimpleInfo(PlayerSimpleInfo playerSimpleInfo)
        {
            return false;
        }

        public PlayerFortuneInfo GetPlayerFortuneInfo(string userName)
        {
            var player = GetPlayer(userName);
            if (player == null)
            {
                return null;
            }

            return player.FortuneInfo;
        }

        public PlayerInfo GetPlayer(string userName)
        {
            PlayerInfo player = null;
            MySqlConnection myconn = null;
            try
            {
                DataTable dt = new DataTable();

                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();
                string cmdText = "select a.*, c.UserName as ReferrerUserName, b.* from playersimpleinfo a left join playersimpleinfo c on a.ReferrerUserID = c.id left join playerfortuneinfo b on a.id = b.userId where a.UserName = @UserName";
                MySqlCommand mycmd = new MySqlCommand(cmdText, myconn);
                mycmd.Parameters.AddWithValue("@UserName", DESEncrypt.EncryptDES(userName));
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(dt);
                player = MetaDBAdapter<PlayerInfo>.GetPlayerInfoFromDataTable(dt);

                mycmd.Dispose();

                return player;
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

        public PlayerInfo GetPlayerByInvitationCode(string invitationCode)
        {
            PlayerInfo player = null;
            MySqlConnection myconn = null;
            try
            {
                DataTable dt = new DataTable();

                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();
                string cmdText = "select a.*, c.UserName as ReferrerUserName, b.* from playersimpleinfo a left join playersimpleinfo c on a.ReferrerUserID = c.id left join playerfortuneinfo b on a.id = b.userId where a.InvitationCode = @InvitationCode";
                MySqlCommand mycmd = new MySqlCommand(cmdText, myconn);
                mycmd.Parameters.AddWithValue("@InvitationCode", DESEncrypt.EncryptDES(invitationCode));
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(dt);
                player = MetaDBAdapter<PlayerInfo>.GetPlayerInfoFromDataTable(dt);

                mycmd.Dispose();

                return player;
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

        public int GetPlayerCountByUserName(string userName)
        {
            MySqlConnection myconn = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();

                string cmdText = "select count(UserName) from playersimpleinfo where UserName = @UserName";
                MySqlCommand mycmd = new MySqlCommand(cmdText, myconn);
                mycmd.Parameters.AddWithValue("@UserName", DESEncrypt.EncryptDES(userName));
                object objResult = mycmd.ExecuteScalar();
                int result = Convert.ToInt32(objResult);

                mycmd.Dispose();

                return result;
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

        public int GetAllPlayerCount()
        {
            MySqlConnection myconn = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();

                string cmdText = "select count(UserName) from playersimpleinfo;";
                MySqlCommand mycmd = new MySqlCommand(cmdText, myconn);
                object objResult = mycmd.ExecuteScalar();
                int result = Convert.ToInt32(objResult);

                mycmd.Dispose();

                return result;
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

        public float GetAllMinersCount()
        {
            MySqlConnection myconn = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();

                string cmdText = "SELECT count(MinersCount) FROM playerfortuneinfo;;";
                MySqlCommand mycmd = new MySqlCommand(cmdText, myconn);
                object objResult = mycmd.ExecuteScalar();
                float result = Convert.ToSingle(objResult);

                mycmd.Dispose();

                return result;
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

        public float GetAllOutputStonesCount()
        {
            MySqlConnection myconn = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();

                string cmdText = "SELECT count(TotalProducedStonesCount) FROM playerfortuneinfo;;";
                MySqlCommand mycmd = new MySqlCommand(cmdText, myconn);
                object objResult = mycmd.ExecuteScalar();
                float result = Convert.ToSingle(objResult);

                mycmd.Dispose();

                return result;
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

        public int GetPlayerCountByAlipay(string alipayAccount, string alipayRealName)
        {
            MySqlConnection myconn = null;
            try
            {
                myconn = new MySqlConnection(MyDBHelper.CONNECTIONSTRING);
                myconn.Open();

                string cmdText = "select count(UserName) from playersimpleinfo where Alipay = @Alipay  or AlipayRealName = @AlipayRealName";
                MySqlCommand mycmd = new MySqlCommand(cmdText, myconn);
                mycmd.Parameters.AddWithValue("@Alipay", DESEncrypt.EncryptDES(alipayAccount));
                mycmd.Parameters.AddWithValue("@AlipayRealName", DESEncrypt.EncryptDES(alipayRealName));
                object objResult = mycmd.ExecuteScalar();
                int result = Convert.ToInt32(objResult);

                mycmd.Dispose();

                return result;
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

        public PlayerInfo GetPlayerByAlipay(string alipayAccount, string alipayRealName)
        {
            PlayerInfo player = null;
            MySqlConnection myconn = null;
            try
            {
                DataTable dt = new DataTable();

                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();
                string cmdText = "select a.*, b.* from playersimpleinfo a left join playerfortuneinfo b on a.id = b.userId where a.Alipay = @Alipay and a.AlipayRealName = @AlipayRealName";
                MySqlCommand mycmd = new MySqlCommand(cmdText, myconn);
                mycmd.Parameters.AddWithValue("@Alipay", DESEncrypt.EncryptDES(alipayAccount));
                mycmd.Parameters.AddWithValue("@AlipayRealName", DESEncrypt.EncryptDES(alipayRealName));
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(dt);
                player = MetaDBAdapter<PlayerInfo>.GetPlayerInfoFromDataTable(dt);

                mycmd.Dispose();

                return player;
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

        public int GetPlayerCountByRegisterIP(string ip)
        {
            MySqlConnection myconn = null;
            try
            {
                myconn = new MySqlConnection(MyDBHelper.CONNECTIONSTRING);
                myconn.Open();

                string cmdText = "select count(RegisterIP) from playersimpleinfo where RegisterIP = @ip";
                MySqlCommand mycmd = new MySqlCommand(cmdText, myconn);
                mycmd.Parameters.AddWithValue("@ip", ip);
                object objResult = mycmd.ExecuteScalar();
                int result = Convert.ToInt32(objResult);

                mycmd.Dispose();

                return result;
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
