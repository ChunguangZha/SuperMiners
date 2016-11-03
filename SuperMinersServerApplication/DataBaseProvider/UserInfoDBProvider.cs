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
                        "(`UserName`, `NickName`, `Password`, `GroupType`,`IsAgentReferred`, `AgentReferredLevel`, `AgentUserID`, `Alipay`, `AlipayRealName`,`Email`, `QQ`, `RegisterIP`, `InvitationCode`, `RegisterTime`) values " +
                        " (@UserName, @NickName, @Password, @GroupType, @IsAgentReferred, @AgentReferredLevel, @AgentUserID, @Alipay, @AlipayRealName, @Email, @QQ, @RegisterIP, @InvitationCode, @RegisterTime); ";
                }
                else
                {
                    cmdTextA = "insert into playersimpleinfo " +
                        "(`UserName`, `NickName`, `Password`, `GroupType`, `IsAgentReferred`, `AgentReferredLevel`, `AgentUserID`, `Alipay`, `AlipayRealName`, `Email`, `QQ`, `RegisterIP`, `InvitationCode`, `RegisterTime`, `ReferrerUserID`) values " +
                        " (@UserName, @NickName, @Password, @GroupType, @IsAgentReferred, @AgentReferredLevel, @AgentUserID, @Alipay, @AlipayRealName, @Email, @QQ, @RegisterIP, @InvitationCode, @RegisterTime, (select b.id from playersimpleinfo b where b.UserName = @ReferrerUserName)); ";
                    mycmd.Parameters.AddWithValue("@ReferrerUserName", DESEncrypt.EncryptDES(player.SimpleInfo.ReferrerUserName));
                }
                mycmd.CommandText = cmdTextA;
                mycmd.Parameters.AddWithValue("@UserName", DESEncrypt.EncryptDES(player.SimpleInfo.UserName));
                mycmd.Parameters.AddWithValue("@NickName", DESEncrypt.EncryptDES(player.SimpleInfo.NickName));
                mycmd.Parameters.AddWithValue("@Password", DESEncrypt.EncryptDES(player.SimpleInfo.Password));
                mycmd.Parameters.AddWithValue("@GroupType", (int)player.SimpleInfo.GroupType);
                mycmd.Parameters.AddWithValue("@IsAgentReferred", player.SimpleInfo.IsAgentReferred);
                mycmd.Parameters.AddWithValue("@AgentReferredLevel", player.SimpleInfo.AgentReferredLevel);
                mycmd.Parameters.AddWithValue("@AgentUserID", player.SimpleInfo.AgentUserID);
                mycmd.Parameters.AddWithValue("@Alipay", string.IsNullOrEmpty(player.SimpleInfo.Alipay) ? null : DESEncrypt.EncryptDES(player.SimpleInfo.Alipay));
                mycmd.Parameters.AddWithValue("@AlipayRealName", string.IsNullOrEmpty(player.SimpleInfo.AlipayRealName) ? null : DESEncrypt.EncryptDES(player.SimpleInfo.AlipayRealName));
                mycmd.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(player.SimpleInfo.AlipayRealName) ? null : DESEncrypt.EncryptDES(player.SimpleInfo.Email));
                mycmd.Parameters.AddWithValue("@QQ", string.IsNullOrEmpty(player.SimpleInfo.QQ) ? null : DESEncrypt.EncryptDES(player.SimpleInfo.QQ));
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
                    " `FreezingStones`, `StockOfDiamonds`, `FreezingDiamonds`, `FirstRechargeGoldCoinAward` ) values " +
                    " (@userId, @Exp, @RMB, @GoldCoin, @MinesCount, @StonesReserves, @TotalProducedStonesCount, @MinersCount, @StockOfStones, " +
                    " @FreezingStones, @StockOfDiamonds,@FreezingDiamonds, @FirstRechargeGoldCoinAward) ";
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
                mycmd.Parameters.AddWithValue("@FirstRechargeGoldCoinAward", player.FortuneInfo.FirstRechargeGoldCoinAward);

                mycmd.ExecuteNonQuery();

                return true;
            }
            finally
            {
                mycmd.Dispose();
            }
        }

        public bool DeletePlayer(string userName)
        {
            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();
                string textCmd = "delete from playersimpleinfo where `UserName` = @UserName;";
                mycmd = new MySqlCommand(textCmd, myconn);
                mycmd.Parameters.AddWithValue("@UserName", DESEncrypt.EncryptDES(userName));

                mycmd.ExecuteNonQuery();

                return true;
            }
            finally
            {
                mycmd.Dispose();
                myconn.Close();
            }
        }

        public bool UpdatePlayerGroupType(int userID, PlayerGroupType type, CustomerMySqlTransaction trans)
        {
            MySqlCommand mycmd = null;
            try
            {
                string cmdTextB = "UPDATE `playersimpleinfo` SET `GroupType`=@GroupType WHERE `id`=@UserID ;";

                mycmd = trans.CreateCommand();
                mycmd.CommandText = cmdTextB;

                mycmd.Parameters.AddWithValue("@GroupType", (int)type);
                mycmd.Parameters.AddWithValue("@UserID", userID);

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


        public bool SavePlayerLastGatherTime(int userID, DateTime? lastGatherStoneTime)
        {
            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;
            try
            {
                string cmdTextB = "UPDATE `playerfortuneinfo` SET `TempOutputStonesStartTime`=@TempOutputStonesStartTime WHERE `UserID`=@UserID ;";

                myconn = MyDBHelper.Instance.CreateConnection();
                mycmd = myconn.CreateCommand();
                myconn.Open();
                mycmd.CommandText = cmdTextB;

                mycmd.Parameters.AddWithValue("@TempOutputStonesStartTime", lastGatherStoneTime);
                mycmd.Parameters.AddWithValue("@UserID", userID);

                mycmd.ExecuteNonQuery();

                return true;
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

        public bool SavePlayerFortuneInfo(PlayerFortuneInfo playerFortune, CustomerMySqlTransaction trans)
        {
            MySqlCommand mycmd = null;
            try
            {
                string cmdTextA = "UPDATE `playerfortuneinfo` SET "
                    + " `Exp`=@Exp, `RMB`=@RMB, `FreezingRMB`=@FreezingRMB, `GoldCoin`=@GoldCoin, `MinesCount`=@MinesCount, `StonesReserves`=@StonesReserves, `TotalProducedStonesCount`=@TotalProducedStonesCount, "
                    + " `MinersCount`=@MinersCount, `StockOfStones`=@StockOfStones,";

                string cmdTextB = "";
                if (playerFortune.TempOutputStonesStartTime != null)
                {
                    cmdTextB = " `TempOutputStonesStartTime`=@TempOutputStonesStartTime,";
                }

                cmdTextB += " `TempOutputStones`=@TempOutputStones,"
                    + " `FreezingStones`=@FreezingStones, `StockOfDiamonds`=@StockOfDiamonds, `FreezingDiamonds`=@FreezingDiamonds, `FirstRechargeGoldCoinAward`=@FirstRechargeGoldCoinAward "
                    + " WHERE `UserID`=(SELECT b.id FROM playersimpleinfo b where b.UserName = @UserName);";

                mycmd = trans.CreateCommand();
                mycmd.CommandText = cmdTextA + cmdTextB;

                mycmd.Parameters.AddWithValue("@Exp", playerFortune.Exp);
                mycmd.Parameters.AddWithValue("@RMB", playerFortune.RMB);
                mycmd.Parameters.AddWithValue("@FreezingRMB", playerFortune.FreezingRMB);
                mycmd.Parameters.AddWithValue("@GoldCoin", playerFortune.GoldCoin);
                mycmd.Parameters.AddWithValue("@MinesCount", playerFortune.MinesCount);
                mycmd.Parameters.AddWithValue("@StonesReserves", playerFortune.StonesReserves);
                mycmd.Parameters.AddWithValue("@TotalProducedStonesCount", playerFortune.TotalProducedStonesCount);
                mycmd.Parameters.AddWithValue("@MinersCount", playerFortune.MinersCount);
                mycmd.Parameters.AddWithValue("@StockOfStones", playerFortune.StockOfStones);

                if (playerFortune.TempOutputStonesStartTime != null)
                {
                    mycmd.Parameters.AddWithValue("@TempOutputStonesStartTime", playerFortune.TempOutputStonesStartTime);
                }

                mycmd.Parameters.AddWithValue("@TempOutputStones", playerFortune.TempOutputStones);
                mycmd.Parameters.AddWithValue("@FreezingStones", playerFortune.FreezingStones);
                mycmd.Parameters.AddWithValue("@StockOfDiamonds", playerFortune.StockOfDiamonds);
                mycmd.Parameters.AddWithValue("@FreezingDiamonds", playerFortune.FreezingDiamonds);
                mycmd.Parameters.AddWithValue("@FirstRechargeGoldCoinAward", playerFortune.FirstRechargeGoldCoinAward);
                mycmd.Parameters.AddWithValue("@UserName", DESEncrypt.EncryptDES(playerFortune.UserName));

                mycmd.ExecuteNonQuery();

                return true;
            }
            finally
            {
                mycmd.Dispose();
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

        public bool SavePlayerLoginTime(PlayerSimpleInfo playerSimpleInfo)
        {
            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();
                string textCmd = "update playersimpleinfo set `LastLoginTime` = @LastLoginTime where `id` = @id;";
                mycmd = myconn.CreateCommand();
                mycmd.CommandText = textCmd;
                mycmd.Parameters.AddWithValue("@LastLoginTime", playerSimpleInfo.LastLoginTime.Value);
                mycmd.Parameters.AddWithValue("@id", playerSimpleInfo.UserID);

                mycmd.ExecuteNonQuery();
                return true;
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

        public bool LogoutPlayer(PlayerSimpleInfo playerSimpleInfo, PlayerFortuneInfo playerFortuneInfo)
        {
            CustomerMySqlTransaction trans = null;
            MySqlCommand mycmd = null;
            try
            {
                trans = MyDBHelper.Instance.CreateTrans();
                string textCmd = "update playersimpleinfo set `LastLoginTime` = @LastLoginTime, `LastLogOutTime` = @LastLogOutTime where `UserName` = @UserName;" +
                                " update playerfortuneinfo a set a.`TempOutputStonesStartTime` = @TempOutputStonesStartTime where a.userId = (select b.id from playersimpleinfo b where b.UserName = @UserName); ";
                mycmd = trans.CreateCommand();
                mycmd.CommandText = textCmd;
                mycmd.Parameters.AddWithValue("@LastLoginTime", playerSimpleInfo.LastLoginTime.Value);
                mycmd.Parameters.AddWithValue("@LastLogOutTime", playerSimpleInfo.LastLogOutTime.Value);
                mycmd.Parameters.AddWithValue("@TempOutputStonesStartTime", playerFortuneInfo.TempOutputStonesStartTime.Value);
                mycmd.Parameters.AddWithValue("@UserName", DESEncrypt.EncryptDES(playerSimpleInfo.UserName));

                mycmd.ExecuteNonQuery();

                trans.Commit();
                return true;
            }
            catch (Exception exc)
            {
                trans.Rollback();
                throw exc;
            }
            finally
            {
                mycmd.Dispose();
                trans.Dispose();
            }
        }

        public bool UpdatePlayerPassword(string userName, string password)
        {
            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();
                string textCmd = "update playersimpleinfo set `Password` = @Password where `UserName` = @UserName;";
                mycmd = new MySqlCommand(textCmd, myconn);
                mycmd.Parameters.AddWithValue("@Password", DESEncrypt.EncryptDES(password));
                mycmd.Parameters.AddWithValue("@UserName", DESEncrypt.EncryptDES(userName));

                mycmd.ExecuteNonQuery();

                return true;
            }
            finally
            {
                mycmd.Dispose();
                myconn.Close();
            }
        }

        public bool UpdatePlayerLockedState(string userName, bool isLock, DateTime? time)
        {
            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();
                string textCmd = "update playersimpleinfo set `LockedLogin` = @LockedLogin, `LockedLoginTime` = @LockedLoginTime where `UserName` = @UserName;";
                mycmd = new MySqlCommand(textCmd, myconn);
                mycmd.Parameters.AddWithValue("@LockedLogin", isLock);
                mycmd.Parameters.AddWithValue("@LockedLoginTime", time);
                mycmd.Parameters.AddWithValue("@UserName", DESEncrypt.EncryptDES(userName));

                mycmd.ExecuteNonQuery();

                return true;
            }
            finally
            {
                mycmd.Dispose();
                myconn.Close();
            }
        }

        public bool UpdatePlayerSimpleInfo(string userName, string nickName, string alipayAccount, string alipayRealName, string email, string qq)
        {
            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();
                string textCmd = "update playersimpleinfo set `NickName`=@NickName, `Alipay` = @Alipay, `AlipayRealName` = @AlipayRealName, `Email`=@Email, `QQ`=@QQ where `UserName` = @UserName;";
                mycmd = new MySqlCommand(textCmd, myconn);
                mycmd.Parameters.AddWithValue("@NickName", DESEncrypt.EncryptDES(nickName));
                mycmd.Parameters.AddWithValue("@Alipay", string.IsNullOrEmpty(alipayAccount) ? DBNull.Value : (object)DESEncrypt.EncryptDES(alipayAccount));
                mycmd.Parameters.AddWithValue("@AlipayRealName", string.IsNullOrEmpty(alipayAccount) ? DBNull.Value : (object)DESEncrypt.EncryptDES(alipayRealName));
                mycmd.Parameters.AddWithValue("@UserName", DESEncrypt.EncryptDES(userName));
                mycmd.Parameters.AddWithValue("@Email", DESEncrypt.EncryptDES(email));
                mycmd.Parameters.AddWithValue("@QQ", DESEncrypt.EncryptDES(qq));

                mycmd.ExecuteNonQuery();

                return true;
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

        public PlayerFortuneInfo GetPlayerFortuneInfo(string userName)
        {
            var player = GetPlayer(userName);
            if (player == null)
            {
                return null;
            }

            return player.FortuneInfo;
        }

        public PlayerInfo[] GetAllPlayers()
        {
            PlayerInfo[] players = null;
            MySqlConnection myconn = null;
            try
            {
                DataTable dt = new DataTable();

                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();
                string cmdText = "select a.*, c.UserName as ReferrerUserName, b.* from playersimpleinfo a left join playersimpleinfo c on a.ReferrerUserID = c.id left join playerfortuneinfo b on a.id = b.userId";
                MySqlCommand mycmd = new MySqlCommand(cmdText, myconn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(dt);
                players = MetaDBAdapter<PlayerInfo>.GetPlayerInfoFromDataTable(dt);

                mycmd.Dispose();

                return players;
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

        public PlayerInfo GetPlayerByUserID(int userID)
        {
            PlayerInfo player = null;
            MySqlConnection myconn = null;
            try
            {
                DataTable dt = new DataTable();

                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();
                string cmdText = "select a.*, c.UserName as ReferrerUserName, b.* from playersimpleinfo a left join playersimpleinfo c on a.ReferrerUserID = c.id left join playerfortuneinfo b on a.id = b.userId where a.id = @userID";
                MySqlCommand mycmd = new MySqlCommand(cmdText, myconn);
                mycmd.Parameters.AddWithValue("@userID", userID);
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    player = MetaDBAdapter<PlayerInfo>.GetPlayerInfoFromDataTable(dt)[0];
                }
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
                if (dt.Rows.Count > 0)
                {
                    player = MetaDBAdapter<PlayerInfo>.GetPlayerInfoFromDataTable(dt)[0];
                }
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
                if (dt.Rows.Count > 0)
                {
                    player = MetaDBAdapter<PlayerInfo>.GetPlayerInfoFromDataTable(dt)[0];
                }

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

        public int GetPlayerCountByNickName(string nickName)
        {
            MySqlConnection myconn = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();

                string cmdText = "select count(NickName) from playersimpleinfo where NickName = @NickName";
                MySqlCommand mycmd = new MySqlCommand(cmdText, myconn);
                mycmd.Parameters.AddWithValue("@NickName", DESEncrypt.EncryptDES(nickName));
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

        public int GetAllPlayerCount()
        {
            MySqlConnection myconn = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();

                string cmdText = "select count(id) from playersimpleinfo;";
                MySqlCommand mycmd = new MySqlCommand(cmdText, myconn);
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

        public decimal GetAllMinersCount()
        {
            MySqlConnection myconn = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();

                string cmdText = "SELECT sum(MinersCount) FROM playerfortuneinfo;;";
                MySqlCommand mycmd = new MySqlCommand(cmdText, myconn);
                object objResult = mycmd.ExecuteScalar();
                mycmd.Dispose();

                if (objResult == DBNull.Value)
                {
                    return 0;
                }
                return Convert.ToDecimal(objResult);
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

        public decimal GetAllOutputStonesCount()
        {
            MySqlConnection myconn = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();

                string cmdText = "SELECT sum(TotalProducedStonesCount) FROM playerfortuneinfo;;";
                MySqlCommand mycmd = new MySqlCommand(cmdText, myconn);
                object objResult = mycmd.ExecuteScalar();
                mycmd.Dispose();

                if (objResult == DBNull.Value)
                {
                    return 0;
                }
                return Convert.ToDecimal(objResult);
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

        public int GetPlayerCountByEmail(string email)
        {
            MySqlConnection myconn = null;
            try
            {
                myconn = new MySqlConnection(MyDBHelper.CONNECTIONSTRING);
                myconn.Open();

                string cmdText = "select count(UserName) from playersimpleinfo where Email = @Email ";
                MySqlCommand mycmd = new MySqlCommand(cmdText, myconn);
                mycmd.Parameters.AddWithValue("@Email", DESEncrypt.EncryptDES(email));
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="alipayAccount"></param>
        /// <param name="alipayRealName"></param>
        /// <returns></returns>
        public int GetPlayerCountByAlipayAccount(string alipayAccount)
        {
            MySqlConnection myconn = null;
            try
            {
                myconn = new MySqlConnection(MyDBHelper.CONNECTIONSTRING);
                myconn.Open();

                string cmdText = "select count(id) from playersimpleinfo where Alipay = @Alipay";
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="alipayAccount"></param>
        /// <param name="alipayRealName"></param>
        /// <returns></returns>
        public int GetPlayerCountByAlipayRealName(string alipayRealName)
        {
            MySqlConnection myconn = null;
            try
            {
                myconn = new MySqlConnection(MyDBHelper.CONNECTIONSTRING);
                myconn.Open();

                string cmdText = "select count(id) from playersimpleinfo where AlipayRealName = @AlipayRealName";
                MySqlCommand mycmd = new MySqlCommand(cmdText, myconn);
                mycmd.Parameters.AddWithValue("@AlipayRealName", DESEncrypt.EncryptDES(alipayRealName));
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

        public PlayerInfo GetPlayerByAlipay(string alipayAccount)
        {
            PlayerInfo player = null;
            MySqlConnection myconn = null;
            try
            {
                DataTable dt = new DataTable();

                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();
                string cmdText = "select a.*, c.UserName as ReferrerUserName, b.* from playersimpleinfo a left join playersimpleinfo c on a.ReferrerUserID = c.id left join playerfortuneinfo b on a.id = b.userId where a.Alipay = @Alipay";
                MySqlCommand mycmd = new MySqlCommand(cmdText, myconn);
                mycmd.Parameters.AddWithValue("@Alipay", DESEncrypt.EncryptDES(alipayAccount));
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    player = MetaDBAdapter<PlayerInfo>.GetPlayerInfoFromDataTable(dt)[0];
                }

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

        public PlayerInfo GetPlayerByAlipayRealName(string alipayRealName)
        {
            PlayerInfo player = null;
            MySqlConnection myconn = null;
            try
            {
                DataTable dt = new DataTable();

                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();
                string cmdText = "select a.*, c.UserName as ReferrerUserName, b.* from playersimpleinfo a left join playersimpleinfo c on a.ReferrerUserID = c.id left join playerfortuneinfo b on a.id = b.userId where a.AlipayRealName = @AlipayRealName";
                MySqlCommand mycmd = new MySqlCommand(cmdText, myconn);
                mycmd.Parameters.AddWithValue("@AlipayRealName", DESEncrypt.EncryptDES(alipayRealName));
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    player = MetaDBAdapter<PlayerInfo>.GetPlayerInfoFromDataTable(dt)[0];
                }

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

        /// <summary>
        /// 取下线
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public UserReferrerTreeItem[] GetUserReferrerDownTree(string userName)
        {
            UserReferrerTreeItem[] players = null;

            MySqlConnection myconn = null;
            try
            {
                DataTable dt = new DataTable();

                myconn = new MySqlConnection(MyDBHelper.CONNECTIONSTRING);
                myconn.Open();

                string cmdText = "SELECT a.`UserName`, a.`NickName`,a.`RegisterIP`,a.`RegisterTime` FROM superminers.playersimpleinfo a " +
                                " where a.`ReferrerUserID` = (select b.`id` from playersimpleinfo b where b.`UserName` = @UserName);";
                MySqlCommand mycmd = new MySqlCommand(cmdText, myconn);
                mycmd.Parameters.AddWithValue("@UserName", DESEncrypt.EncryptDES(userName));
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    players = MetaDBAdapter<UserReferrerTreeItem>.GetUserReferrerTreeItem(dt);
                }
                mycmd.Dispose();

                return players;
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

        /// <summary>
        /// 取上线
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public UserReferrerTreeItem GetUserReferrerUpTree(string userName)
        {
            UserReferrerTreeItem player = null;

            MySqlConnection myconn = null;
            try
            {
                DataTable dt = new DataTable();

                myconn = new MySqlConnection(MyDBHelper.CONNECTIONSTRING);
                myconn.Open();

                string cmdText = "SELECT a.`UserName`, a.`NickName`,a.`RegisterIP`,a.`RegisterTime` FROM superminers.playersimpleinfo a " + 
                                " where a.`id` = (select b.`ReferrerUserID` from playersimpleinfo b where b.`UserName` = @UserName);";
                MySqlCommand mycmd = new MySqlCommand(cmdText, myconn);
                mycmd.Parameters.AddWithValue("@UserName", DESEncrypt.EncryptDES(userName));
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    player = MetaDBAdapter<UserReferrerTreeItem>.GetUserReferrerTreeItem(dt)[0];
                }
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
        
        public TopListInfo[] GetExpTopList()
        {
            return GetTopList("Exp");
        }

        public TopListInfo[] GetStoneTopList()
        {
            return GetTopList("StockOfStones");
        }

        public TopListInfo[] GetMinerTopList()
        {
            return GetTopList("MinersCount");
        }

        public TopListInfo[] GetGoldCoinTopList()
        {
            return GetTopList("GoldCoin");
        }

        public TopListInfo[] GetReferrerTopList()
        {
            TopListInfo[] toplistInfos = null;
            MySqlConnection myconn = null;
            try
            {
                string valueType = "RefrerCount";
                DataTable dt = new DataTable();

                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();
                string cmdText = "select a.UserName, a.NickName, (select count(b.id) from playersimpleinfo b where b.ReferrerUserID = a.id) as " + valueType + " from playersimpleinfo a order by " + valueType + " desc limit 20;";
                MySqlCommand mycmd = new MySqlCommand(cmdText, myconn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    toplistInfos = MetaDBAdapter<TopListInfo>.GetTopListInfo(valueType, dt);
                }

                mycmd.Dispose();

                return toplistInfos;
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

        private TopListInfo[] GetTopList(string valueType)
        {
            TopListInfo[] toplistInfos = null;
            MySqlConnection myconn = null;
            try
            {
                DataTable dt = new DataTable();

                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();
                string cmdText = "SELECT s.UserName, s.NickName , f." + valueType + " FROM playersimpleinfo s left join playerfortuneinfo f on s.id=f.userId order by f." + valueType + " desc limit 20;";
                MySqlCommand mycmd = new MySqlCommand(cmdText, myconn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    toplistInfos = MetaDBAdapter<TopListInfo>.GetTopListInfo(valueType, dt);
                }

                mycmd.Dispose();

                return toplistInfos;
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
