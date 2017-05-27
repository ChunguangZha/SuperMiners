using MetaData;
using MetaData.StoneFactory;
using MetaData.SystemConfig;
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
    public class PlayerStoneFactoryDBProvider
    {
        public bool OpenFactory(int userID, CustomerMySqlTransaction myTrans)
        {
            //需先检查数据库中是否存在工厂信息，如果没有则添加
            bool isOK = MyDBHelper.Instance.ConnectionCommandExecuteNonQuery(mycmd =>
            {
                string sqlText = "select count(ID) from playerstonefactoryaccountinfo where `UserID`=@UserID ";
                mycmd.Parameters.AddWithValue("@UserID", userID);
                mycmd.CommandText = sqlText;
                object objResult = Convert.ToInt32(mycmd.ExecuteScalar());
                if (objResult == DBNull.Value || Convert.ToInt32(objResult) == 0)
                {
                    //添加新记录
                    sqlText = "insert into playerstonefactoryaccountinfo " +
                        "(`UserID`,`FactoryIsOpening`,`FactoryLiveDays`,`Food`,`LastDayValidStoneStack`,`FreezingSlavesCount`,`SlavesCount`) " +
                        " values (@UserID,@FactoryIsOpening,@FactoryLiveDays,@Food,@LastDayValidStoneStack,@FreezingSlavesCount,@SlavesCount) ";
                    mycmd.CommandText = sqlText;
                    //mycmd.Parameters.Clear();
                    //mycmd.Parameters.AddWithValue("@UserID", userID);
                    mycmd.Parameters.AddWithValue("@FactoryIsOpening", true);
                    mycmd.Parameters.AddWithValue("@FactoryLiveDays", StoneFactoryConfig.FactoryLiveDays);
                    mycmd.Parameters.AddWithValue("@Food", 0);
                    mycmd.Parameters.AddWithValue("@LastDayValidStoneStack", 0);
                    mycmd.Parameters.AddWithValue("@FreezingSlavesCount", 0);
                    mycmd.Parameters.AddWithValue("@SlavesCount", 0);
                    mycmd.ExecuteNonQuery();
                }
                else
                {
                    //修改原记录
                    sqlText = "update playerstonefactoryaccountinfo set `FactoryIsOpening`=@FactoryIsOpening,`FactoryLiveDays`=@FactoryLiveDays where `UserID`=@UserID ";
                    mycmd.CommandText = sqlText;
                    //mycmd.Parameters.Clear();
                    //mycmd.Parameters.AddWithValue("@UserID", userID);
                    mycmd.Parameters.AddWithValue("@FactoryIsOpening", true);
                    mycmd.Parameters.AddWithValue("@FactoryLiveDays", StoneFactoryConfig.FactoryLiveDays);
                    mycmd.ExecuteNonQuery();

                }

            });
            
            return isOK;
        }

        public bool SavePlayerStoneFactoryAccountInfo(PlayerStoneFactoryAccountInfo account, CustomerMySqlTransaction myTrans)
        {
            MySqlCommand mycmd = null;
            try
            {
                mycmd = myTrans.CreateCommand();

                string sqlText = "update playerstonefactoryaccountinfo set `FactoryIsOpening`=@FactoryIsOpening,`FactoryLiveDays`=@FactoryLiveDays,`Food`=@Food,`LastDayValidStoneStack`=@LastDayValidStoneStack,`FreezingSlaveGroupCount`=@FreezingSlaveGroupCount,`EnableSlavesGroupCount`=@EnableSlavesGroupCount where `ID`=@ID ";
                mycmd.CommandText = sqlText;
                mycmd.Parameters.AddWithValue("@ID", account.ID);
                mycmd.Parameters.AddWithValue("@FactoryIsOpening", account.FactoryIsOpening);
                mycmd.Parameters.AddWithValue("@FactoryLiveDays", account.FactoryLiveDays);
                mycmd.Parameters.AddWithValue("@Food", account.Food);
                mycmd.Parameters.AddWithValue("@LastDayValidStoneStack", account.LastDayValidStoneStack);
                mycmd.Parameters.AddWithValue("@FreezingSlaveGroupCount", account.FreezingSlaveGroupCount);
                mycmd.Parameters.AddWithValue("@EnableSlavesGroupCount", account.EnableSlavesGroupCount);
                mycmd.ExecuteNonQuery();
            }
            finally
            {
                if (mycmd != null)
                {
                    mycmd.Dispose();
                }
            }
            return true;
        }

        public PlayerStoneFactoryAccountInfo[] GetAllPlayerStoneFactoryAccountInfos()
        {
            List<PlayerStoneFactoryAccountInfo> listFactories = new List<PlayerStoneFactoryAccountInfo>();
            MyDBHelper.Instance.ConnectionCommandSelect(mycmd =>
            {
                string sqlText = "select * from playerstonefactoryaccountinfo";
                mycmd.CommandText = sqlText;
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(table);

                var items = MetaDBAdapter<PlayerStoneFactoryAccountInfo>.GetPlayerStoneFactoryAccountInfoItemFromDataTable(table);
                table.Dispose();
                adapter.Dispose();

                if (items == null || items.Length == 0)
                {
                    return;
                }
                PlayerStoneFactoryAccountInfo account = items[0];

                SumUserAccountStoneStackCount(account, mycmd);
                SumUserAccountProfitRMBCount(account, mycmd);

                listFactories.Add(account);
            });

            return listFactories.ToArray();
        }

        public PlayerStoneFactoryAccountInfo GetPlayerStoneFactoryAccountInfo(int userID)
        {
            PlayerStoneFactoryAccountInfo account = null;
            MyDBHelper.Instance.ConnectionCommandSelect(mycmd =>
            {
                string sqlText = "select * from playerstonefactoryaccountinfo where `UserID`=@UserID ";
                mycmd.Parameters.AddWithValue("@UserID", userID);
                mycmd.CommandText = sqlText;
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(table);

                var items = MetaDBAdapter<PlayerStoneFactoryAccountInfo>.GetPlayerStoneFactoryAccountInfoItemFromDataTable(table);
                table.Dispose();
                adapter.Dispose();

                if (items == null || items.Length == 0)
                {
                    return;
                }
                account = items[0];

                SumUserAccountStoneStackCount(account, mycmd);
                SumUserAccountProfitRMBCount(account, mycmd);

            });

            return account;
        }

        private void SumUserAccountProfitRMBCount(PlayerStoneFactoryAccountInfo account, MySqlCommand mycmd)
        {
            string sqlText = "select * from stonefactoryprofitrmbchangedrecord where `UserID`=@UserID ";
            mycmd.CommandText = sqlText;
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
            adapter.Fill(table);

            StoneFactoryProfitRMBChangedRecord[] items = MetaDBAdapter<StoneFactoryProfitRMBChangedRecord>.GetStoneFactoryProfitRMBChangedRecordItemFromDataTable(table);
            table.Dispose();
            adapter.Dispose();

            decimal sumProfitRMB = 0;
            decimal sumWithdrawableProfitRMB = 0;

            //已经提现的收益灵币（该值为负数）
            decimal sumWithdrawedProfitRMB = 0;

            if (items != null && items.Length != 0)
            {
                DateTime timeNow = DateTime.Now;
                foreach (var item in items)
                {
                    if (item.ProfitType == FactoryProfitOperType.WithdrawRMB)
                    {
                        sumWithdrawedProfitRMB += item.OperRMB;
                    }
                    else
                    {
                        sumProfitRMB += item.OperRMB;
                        if ((timeNow - item.OperTime.ToDateTime()).TotalDays > StoneFactoryConfig.ProfitRMBWithdrawLimitDays)
                        {
                            sumWithdrawableProfitRMB += item.OperRMB;
                        }
                    }
                }
            }

            account.TotalProfitRMB = sumProfitRMB;
            account.WithdrawableProfitRMB = sumWithdrawableProfitRMB + sumWithdrawedProfitRMB;
        }

        private void SumUserAccountStoneStackCount(PlayerStoneFactoryAccountInfo account, MySqlCommand mycmd)
        {
            string sqlText = "select * from stonefactorystackchangerecord where `UserID`=@UserID ";
            mycmd.CommandText = sqlText;
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
            adapter.Fill(table);

            StoneFactoryStackChangeRecord[] items = MetaDBAdapter<StoneFactoryStackChangeRecord>.GetStoneFactoryStackChangeRecordItemFromDataTable(table);
            table.Dispose();
            adapter.Dispose();

            int sumEnableStoneStack = 0;
            int sumFreezingStoneStack = 0;
            int sumWithdrawableStoneStack = 0;
            int sumWithdrawedStoneStack = 0;

            if (items != null && items.Length != 0)
            {
                DateTime timeNow = DateTime.Now;
                foreach (var item in items)
                {
                    if ((timeNow - item.Time.ToDateTime()).TotalHours > StoneFactoryConfig.StoneFactoryStoneFreezingHours)
                    {
                        //可用矿石
                        sumEnableStoneStack += item.JoinStoneStackCount;
                    }
                    else
                    {
                        sumFreezingStoneStack += item.JoinStoneStackCount;
                    }
                    if (item.JoinStoneStackCount > 0 && (timeNow - item.Time.ToDateTime()).TotalDays > StoneFactoryConfig.StoneStackWithdrawLimitDays)
                    {
                        //可提现的矿石（没有减去已经提走的灵币）
                        sumWithdrawableStoneStack += item.JoinStoneStackCount;
                    }
                    if (item.JoinStoneStackCount < 0)
                    {
                        //已提取的灵币
                        sumWithdrawedStoneStack += item.JoinStoneStackCount;
                    }
                }
            }

            account.TotalStackCount = sumEnableStoneStack;
            account.WithdrawableStackCount = sumWithdrawableStoneStack - sumWithdrawedStoneStack;
            account.FreezingStackCount = sumFreezingStoneStack;
        }

        public StoneFactoryProfitRMBChangedRecord[] GetProfitRecords(int userID, MyDateTime beginTime, MyDateTime endTime, int pageItemCount, int pageIndex)
        {
            return null;
        }

        public StoneFactoryStackChangeRecord[] GetFactoryStackChangedRecord(int userID, MyDateTime beginTime, MyDateTime endTime, int pageItemCount, int pageIndex)
        {
            return null;
        }

        public StoneFactoryOneGroupSlave[] GetFactorySlaveGroupInfos(int userID, MyDateTime beginTime, MyDateTime endTime, int pageItemCount, int pageIndex)
        {
            return null;
        }

        public StoneFactorySystemDailyProfit[] GetFactorySystemDailyProfitRecords(int pageItemCount, int pageIndex)
        {
            return null;
        }

        public bool AddStoneFactorySystemDailyProfit(StoneFactorySystemDailyProfit profit)
        {
            bool isOK = MyDBHelper.Instance.ConnectionCommandExecuteNonQuery(mycmd =>
            {
                string sqlText = "insert into stonefactorysystemdailyprofit " +
                        "(`profitRate`,`Day`) " +
                        " values (@profitRate,@Day) ";
                mycmd.CommandText = sqlText;
                mycmd.Parameters.AddWithValue("@profitRate", profit.profitRate);
                mycmd.Parameters.AddWithValue("@Day", profit.Day.ToDateTime());
                mycmd.ExecuteNonQuery();
            });

            return isOK;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="isJoinIn">true表示投入；false表示提取</param>
        /// <param name="stoneStackCount"></param>
        /// <param name="myTrans"></param>
        /// <returns></returns>
        public bool AddNewStackChangeRecord(StoneFactoryStackChangeRecord record, CustomerMySqlTransaction myTrans)
        {
            MySqlCommand mycmd = null;
            try
            {
                mycmd = myTrans.CreateCommand();
                string sqlText = "insert into stonefactorystackchangerecord " +
                        "(`UserID`,`JoinStoneStackCount`,`Time`) " +
                        " values (@UserID,@JoinStoneStackCount,@Time) ";
                mycmd.CommandText = sqlText;
                mycmd.Parameters.AddWithValue("@UserID", record.UserID);
                mycmd.Parameters.AddWithValue("@JoinStoneStackCount", record.JoinStoneStackCount);
                mycmd.Parameters.AddWithValue("@Time", record.Time.ToDateTime());
                mycmd.ExecuteNonQuery();
            }
            finally
            {
                if (mycmd != null)
                {
                    mycmd.Dispose();
                }
            }

            return true;
        }

        //public bool AddMiners(int userID, int minersCount, CustomerMySqlTransaction myTrans)
        //{
        //    StoneFactoryOneGroupSlave slave = new StoneFactoryOneGroupSlave()
        //    {
        //        UserID = userID,
        //        ChargeTime = new MyDateTime(),
        //        JoinInSlaveCount = minersCount,
        //        isLive = true,
        //        LifeDays = 2,
        //        LiveSlaveCount = minersCount
        //    };

        //    return false;
        //}

        //public bool AddFoods(int userID, int foodsCount, CustomerMySqlTransaction myTrans)
        //{
        //    //直接修改字段值
        //    return false;
        //}

        public bool AddProfitRMBChangedRecord(StoneFactoryProfitRMBChangedRecord record, CustomerMySqlTransaction myTrans)
        {
            return false;
        }

    }
}
