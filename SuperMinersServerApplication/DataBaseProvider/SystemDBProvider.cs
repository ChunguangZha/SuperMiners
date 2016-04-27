using MetaData.SystemConfig;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseProvider
{
    public class SystemDBProvider
    {
        #region GameConfig

        public GameConfig GetGameConfig()
        {
            GameConfig config = null;
            MySqlConnection myconn = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();

                DataTable dt = new DataTable();

                string cmdText = "SELECT * FROM gameconfig";
                MySqlCommand mycmd = new MySqlCommand(cmdText, myconn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(dt);
                if (dt.Rows.Count != 0)
                {
                    config = new GameConfig();
                    config.Yuan_RMB = Convert.ToSingle(dt.Rows[0]["Yuan_RMB"]);
                    config.RMB_GoldCoin = Convert.ToSingle(dt.Rows[0]["RMB_GoldCoin"]);
                    config.GoldCoin_Mine = Convert.ToSingle(dt.Rows[0]["GoldCoin_Mine"]);
                    config.GoldCoin_Miner = Convert.ToSingle(dt.Rows[0]["GoldCoin_Miner"]);
                    config.Stones_RMB = Convert.ToSingle(dt.Rows[0]["Stones_RMB"]);
                    config.Diamonds_RMB = Convert.ToSingle(dt.Rows[0]["Diamonds_RMB"]);
                    config.StoneBuyerAwardGoldCoinMultiple = Convert.ToSingle(dt.Rows[0]["StoneBuyerAwardGoldCoinMultiple"]);
                    config.OutputStonesPerHour = Convert.ToSingle(dt.Rows[0]["OutputStonesPerHour"]);
                    config.StonesReservesPerMines = Convert.ToSingle(dt.Rows[0]["StonesReservesPerMines"]);
                    config.ExchangeExpensePercent = Convert.ToSingle(dt.Rows[0]["ExchangeExpensePercent"]);
                    config.ExchangeExpenseMinNumber = Convert.ToSingle(dt.Rows[0]["ExchangeExpenseMinNumber"]);

                    dt.Dispose();
                }

                mycmd.Dispose();

                return config;
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

        public bool SaveGameConfig(GameConfig config, CustomerMySqlTransaction trans)
        {
            MySqlCommand mycmd = trans.CreateCommand();
            string cmdText = "delete from gameconfig; " +
                "insert into gameconfig (Yuan_RMB, RMB_GoldCoin, GoldCoin_Mine, GoldCoin_Miner, Stones_RMB, Diamonds_RMB, StoneBuyerAwardGoldCoinMultiple, OutputStonesPerHour, StonesReservesPerMines,  ExchangeExpensePercent, ExchangeExpenseMinNumber) values " +
                                    " (@Yuan_RMB, @RMB_GoldCoin, @GoldCoin_Mine, @GoldCoin_Miner, @Stones_RMB, @Diamonds_RMB, @StoneBuyerAwardGoldCoinMultiple, @OutputStonesPerHour, @StonesReservesPerMines, @ExchangeExpensePercent, @ExchangeExpenseMinNumber)";
            mycmd.CommandText = cmdText;
            mycmd.Parameters.AddWithValue("@Yuan_RMB", config.Yuan_RMB);
            mycmd.Parameters.AddWithValue("@RMB_GoldCoin", config.RMB_GoldCoin);
            mycmd.Parameters.AddWithValue("@GoldCoin_Mine", config.GoldCoin_Mine);
            mycmd.Parameters.AddWithValue("@GoldCoin_Miner", config.GoldCoin_Miner);
            mycmd.Parameters.AddWithValue("@Stones_RMB", config.Stones_RMB);
            mycmd.Parameters.AddWithValue("@Diamonds_RMB", config.Diamonds_RMB);
            mycmd.Parameters.AddWithValue("@StoneBuyerAwardGoldCoinMultiple", config.StoneBuyerAwardGoldCoinMultiple);
            mycmd.Parameters.AddWithValue("@OutputStonesPerHour", config.OutputStonesPerHour);
            mycmd.Parameters.AddWithValue("@StonesReservesPerMines", config.StonesReservesPerMines);
            mycmd.Parameters.AddWithValue("@ExchangeExpensePercent", config.ExchangeExpensePercent);
            mycmd.Parameters.AddWithValue("@ExchangeExpenseMinNumber", config.ExchangeExpenseMinNumber);

            mycmd.ExecuteNonQuery();
            //mycmd.Dispose();

            return true;
        }

        #endregion

        #region IncomeMoneyAccount

        public IncomeMoneyAccount GetIncomeMoneyAccountConfig()
        {
            IncomeMoneyAccount config = null;
            MySqlConnection myconn = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();

                DataTable dt = new DataTable();

                string cmdText = "SELECT * FROM incomemoneyaccount";
                MySqlCommand mycmd = new MySqlCommand(cmdText, myconn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(dt);
                if (dt.Rows.Count != 0)
                {
                    config = new IncomeMoneyAccount();
                    config.IncomeMoneyAlipay = Convert.ToString(dt.Rows[0]["IncomeMoneyAlipay"]);
                    config.IncomeMoneyAlipayRealName = Convert.ToString(dt.Rows[0]["IncomeMoneyAlipayRealName"]);
                    config.Alipay2DCode = (byte[])dt.Rows[0]["Alipay2DCode"];
                    dt.Dispose();
                }

                mycmd.Dispose();

                return config;
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

        public bool SaveIncomeMoneyAccountConfig(IncomeMoneyAccount config)
        {
            MySqlConnection myconn = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();

                string cmdText = "delete from incomemoneyaccount; " +
                    "insert into incomemoneyaccount (IncomeMoneyAlipay, IncomeMoneyAlipayRealName, Alipay2DCode) values " +
                                        " (@IncomeMoneyAlipay, @IncomeMoneyAlipayRealName, @Alipay2DCode)";
                MySqlCommand mycmd = new MySqlCommand(cmdText, myconn);
                mycmd.Parameters.AddWithValue("@IncomeMoneyAlipay", config.IncomeMoneyAlipay);
                mycmd.Parameters.AddWithValue("@IncomeMoneyAlipayRealName", config.IncomeMoneyAlipayRealName);
                mycmd.Parameters.AddWithValue("@Alipay2DCode", config.Alipay2DCode);
                
                mycmd.ExecuteNonQuery();
                mycmd.Dispose();

                return true;
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

        #endregion

        #region RegisterUserConfig

        public RegisterUserConfig GetRegisterUserConfig()
        {
            RegisterUserConfig config = null;
            MySqlConnection myconn = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();

                DataTable dt = new DataTable();

                string cmdText = "SELECT * FROM registeruserconfig";
                MySqlCommand mycmd = new MySqlCommand(cmdText, myconn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(dt);
                if (dt.Rows.Count != 0)
                {
                    config = new RegisterUserConfig();
                    config.UserCountCreateByOneIP = Convert.ToInt32(dt.Rows[0]["UserCountCreateByOneIP"]);
                    config.GiveToNewUserExp = Convert.ToSingle(dt.Rows[0]["GiveToNewUserExp"]);
                    config.GiveToNewUserGoldCoin = Convert.ToSingle(dt.Rows[0]["GiveToNewUserGoldCoin"]);
                    config.GiveToNewUserMines = Convert.ToInt32(dt.Rows[0]["GiveToNewUserMines"]);
                    config.GiveToNewUserMiners = Convert.ToInt32(dt.Rows[0]["GiveToNewUserMiners"]);
                    config.GiveToNewUserStones = Convert.ToSingle(dt.Rows[0]["GiveToNewUserStones"]);

                    dt.Dispose();
                }

                mycmd.Dispose();

                return config;
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

        public bool SaveRegisterUserConfig(RegisterUserConfig config, CustomerMySqlTransaction trans)
        {
            MySqlCommand mycmd = trans.CreateCommand();
            string cmdText = "delete from registeruserconfig; " +
                "insert into registeruserconfig (UserCountCreateByOneIP, GiveToNewUserExp, GiveToNewUserGoldCoin, GiveToNewUserMines, GiveToNewUserMiners, GiveToNewUserStones ) values " +
                                    " (@UserCountCreateByOneIP, @GiveToNewUserExp, @GiveToNewUserGoldCoin, @GiveToNewUserMines, @GiveToNewUserMiners, @GiveToNewUserStones )";
            mycmd.CommandText = cmdText;
            mycmd.Parameters.AddWithValue("@UserCountCreateByOneIP", config.UserCountCreateByOneIP);
            mycmd.Parameters.AddWithValue("@GiveToNewUserExp", config.GiveToNewUserExp);
            mycmd.Parameters.AddWithValue("@GiveToNewUserGoldCoin", config.GiveToNewUserGoldCoin);
            mycmd.Parameters.AddWithValue("@GiveToNewUserMines", config.GiveToNewUserMines);
            mycmd.Parameters.AddWithValue("@GiveToNewUserMiners", config.GiveToNewUserMiners);
            mycmd.Parameters.AddWithValue("@GiveToNewUserStones", config.GiveToNewUserStones);

            mycmd.ExecuteNonQuery();
            //mycmd.Dispose();

            return true;
        }

        #endregion

        #region AwardReferrerConfig

        public List<AwardReferrerConfig> GetAwardReferrerConfig()
        {
            List<AwardReferrerConfig> listAwardConfig = new List<AwardReferrerConfig>();
            MySqlConnection myconn = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();

                DataTable dt = new DataTable();

                string cmdText = "SELECT * FROM awardreferrerconfig";
                MySqlCommand mycmd = new MySqlCommand(cmdText, myconn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    AwardReferrerConfig config = new AwardReferrerConfig();
                    config.ReferLevel = Convert.ToInt32(dt.Rows[i]["ReferLevel"]);
                    config.AwardReferrerExp = Convert.ToSingle(dt.Rows[i]["AwardReferrerExp"]);
                    config.AwardReferrerGoldCoin = Convert.ToSingle(dt.Rows[i]["AwardReferrerGoldCoin"]);
                    config.AwardReferrerMines = Convert.ToSingle(dt.Rows[i]["AwardReferrerMines"]);
                    config.AwardReferrerMiners = Convert.ToInt32(dt.Rows[i]["AwardReferrerMiners"]);
                    config.AwardReferrerStones = Convert.ToSingle(dt.Rows[i]["AwardReferrerStones"]);
                    config.AwardReferrerDiamond = Convert.ToSingle(dt.Rows[i]["AwardReferrerDiamond"]);
                    listAwardConfig.Add(config);
                }

                dt.Dispose();
                mycmd.Dispose();

                return listAwardConfig;
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

        public bool SaveAwardReferrerConfig(List<AwardReferrerConfig> ListConfig, CustomerMySqlTransaction trans)
        {
            MySqlCommand mycmd = trans.CreateCommand();
            mycmd.CommandText = "delete from awardreferrerconfig; ";
            mycmd.ExecuteNonQuery();

            mycmd.CommandText = "insert into awardreferrerconfig (ReferLevel, AwardReferrerExp, AwardReferrerGoldCoin, AwardReferrerMines, AwardReferrerMiners, AwardReferrerStones, AwardReferrerDiamond ) values " +
                " (@ReferLevel, @AwardReferrerExp, @AwardReferrerGoldCoin, @AwardReferrerMines, @AwardReferrerMiners, @AwardReferrerStones, @AwardReferrerDiamond )";

            mycmd.Parameters.Add("@ReferLevel", MySqlDbType.Int32);
            mycmd.Parameters.AddWithValue("@AwardReferrerExp", MySqlDbType.Float);
            mycmd.Parameters.AddWithValue("@AwardReferrerGoldCoin", MySqlDbType.Float);
            mycmd.Parameters.AddWithValue("@AwardReferrerMines", MySqlDbType.Float);
            mycmd.Parameters.AddWithValue("@AwardReferrerMiners", MySqlDbType.Int32);
            mycmd.Parameters.AddWithValue("@AwardReferrerStones", MySqlDbType.Float);
            mycmd.Parameters.AddWithValue("@AwardReferrerDiamond", MySqlDbType.Float);

            foreach (var config in ListConfig)
            {
                mycmd.Parameters["@ReferLevel"].Value = config.ReferLevel;
                mycmd.Parameters["@AwardReferrerExp"].Value = config.AwardReferrerExp;
                mycmd.Parameters["@AwardReferrerGoldCoin"].Value = config.AwardReferrerGoldCoin;
                mycmd.Parameters["@AwardReferrerMines"].Value = config.AwardReferrerMines;
                mycmd.Parameters["@AwardReferrerMiners"].Value = config.AwardReferrerMiners;
                mycmd.Parameters["@AwardReferrerStones"].Value = config.AwardReferrerStones;
                mycmd.Parameters["@AwardReferrerDiamond"].Value = config.AwardReferrerDiamond;
                mycmd.ExecuteNonQuery();
            }

            //mycmd.Dispose();
            return true;
        }

        #endregion
    }
}
