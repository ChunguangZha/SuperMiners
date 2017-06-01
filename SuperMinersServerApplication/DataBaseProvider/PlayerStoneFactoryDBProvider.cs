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
                        "(`UserID`,`FactoryIsOpening`,`FactoryLiveDays`,`Food`,`LastDayValidStoneStack`,`FreezingSlaveGroupCount`,`EnableSlavesGroupCount`) " +
                        " values (@UserID,@FactoryIsOpening,@FactoryLiveDays,@Food,@LastDayValidStoneStack,@FreezingSlaveGroupCount,@EnableSlavesGroupCount) ";
                    mycmd.CommandText = sqlText;
                    //mycmd.Parameters.Clear();
                    //mycmd.Parameters.AddWithValue("@UserID", userID);
                    mycmd.Parameters.AddWithValue("@FactoryIsOpening", true);
                    mycmd.Parameters.AddWithValue("@FactoryLiveDays", StoneFactoryConfig.FactoryLiveDays);
                    mycmd.Parameters.AddWithValue("@Food", 0);
                    mycmd.Parameters.AddWithValue("@LastDayValidStoneStack", 0);
                    mycmd.Parameters.AddWithValue("@FreezingSlaveGroupCount", 0);
                    mycmd.Parameters.AddWithValue("@EnableSlavesGroupCount", 0);
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

                string sqlText = "update playerstonefactoryaccountinfo set `FactoryIsOpening`=@FactoryIsOpening,`FactoryLiveDays`=@FactoryLiveDays,`Food`=@Food,`LastFeedSlaveTime`=@LastFeedSlaveTime,`LastDayValidStoneStack`=@LastDayValidStoneStack,`FreezingSlaveGroupCount`=@FreezingSlaveGroupCount,`EnableSlavesGroupCount`=@EnableSlavesGroupCount where `ID`=@ID ";
                mycmd.CommandText = sqlText;
                mycmd.Parameters.AddWithValue("@ID", account.ID);
                mycmd.Parameters.AddWithValue("@FactoryIsOpening", account.FactoryIsOpening);
                mycmd.Parameters.AddWithValue("@FactoryLiveDays", account.FactoryLiveDays);
                mycmd.Parameters.AddWithValue("@Food", account.Food);
                mycmd.Parameters.AddWithValue("@LastFeedSlaveTime", account.LastFeedSlaveTime == null? DBNull.Value: (object)account.LastFeedSlaveTime.ToDateTime());
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
                string sqlText = "select f.*, s.UserName from playerstonefactoryaccountinfo f left join playersimpleinfo s on f.UserID = s.id ";
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

                foreach (var account in items)
                {
                    if (account.LastFeedSlaveTime != null)
                    {
                        account.SlaveLiveDiscountms = StoneFactoryConfig.OnceFeedFoodSlaveCanLivems - (int)(DateTime.Now - account.LastFeedSlaveTime.ToDateTime()).TotalSeconds;
                    }
                    else
                    {
                        account.SlaveLiveDiscountms = 0;
                    }

                    SumUserAccountStoneStackCount(account, mycmd);
                    SumUserAccountProfitRMBCount(account, mycmd);

                    listFactories.Add(account);
                }

            });

            return listFactories.ToArray();
        }

        public PlayerStoneFactoryAccountInfo GetPlayerStoneFactoryAccountInfo(int userID)
        {
            PlayerStoneFactoryAccountInfo account = null;
            MyDBHelper.Instance.ConnectionCommandSelect(mycmd =>
            {
                string sqlText = "select ttt.*, s.UserName from "+
                                    " (select f.* from playerstonefactoryaccountinfo f where f.`UserID`=@UserID ) ttt "+
                                    " left join playersimpleinfo s on ttt.UserID = s.id  ";
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

                if (account.LastFeedSlaveTime != null)
                {
                    account.SlaveLiveDiscountms = StoneFactoryConfig.OnceFeedFoodSlaveCanLivems - (int)(DateTime.Now - account.LastFeedSlaveTime.ToDateTime()).TotalSeconds;
                }
                else
                {
                    account.SlaveLiveDiscountms = 0;
                }
                SumUserAccountStoneStackCount(account, mycmd);
                SumUserAccountProfitRMBCount(account, mycmd);

            });

            return account;
        }

        public int GetSumLastDayValidStoneStack()
        {
            int result = 0;
            MyDBHelper.Instance.ConnectionCommandExecuteNonQuery(mycmd =>
            {
                string sqlText = "select sum(LastDayValidStoneStack) as sumLastDayValidStoneStack from playerstonefactoryaccountinfo ";
                mycmd.CommandText = sqlText;
                object objResult = mycmd.ExecuteScalar();
                if (objResult != DBNull.Value)
                {
                    result = Convert.ToInt32(objResult);
                }
            });

            return result;
        }

        private void SumUserAccountProfitRMBCount(PlayerStoneFactoryAccountInfo account, MySqlCommand mycmd)
        {
            string sqlText = "select * from stonefactoryprofitrmbchangedrecord where `UserID`=@UserID ";
            mycmd.CommandText = sqlText;
            mycmd.Parameters.Clear();
            mycmd.Parameters.AddWithValue("@UserID", account.UserID);
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

            decimal sumYesterdayProfitRMB = 0;

            //按时间顺序
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
                        DateTime itemOperTime = item.OperTime.ToDateTime();
                        if ((timeNow.Date - itemOperTime.Date).Days >= StoneFactoryConfig.ProfitRMBWithdrawLimitDays)
                        {
                            sumWithdrawableProfitRMB += item.OperRMB;
                        }
                        else
                        {
                            if (timeNow.Hour < 14)
                            {
                                //14点之前只能取到前天的记录。
                                if ((timeNow.Date - itemOperTime.Date).Days == 2)
                                {
                                    sumYesterdayProfitRMB += item.OperRMB;
                                }
                            }
                            else
                            {
                                //14点以后可以取到昨天记录
                                if ((timeNow.Date - itemOperTime.Date).Days == 1)
                                {
                                    sumYesterdayProfitRMB += item.OperRMB;
                                }
                            }

                        }
                    }
                }

            }

            account.YesterdayTotalProfitRMB = sumYesterdayProfitRMB;
            account.TotalProfitRMB = sumProfitRMB;
            account.WithdrawableProfitRMB = sumWithdrawableProfitRMB + sumWithdrawedProfitRMB;
        }

        private void SumUserAccountStoneStackCount(PlayerStoneFactoryAccountInfo account, MySqlCommand mycmd)
        {
            string sqlText = "select * from stonefactorystackchangerecord where `UserID`=@UserID ";
            mycmd.CommandText = sqlText;
            mycmd.Parameters.Clear();
            mycmd.Parameters.AddWithValue("@UserID", account.UserID);
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
                    if ((timeNow.Date - item.Time.ToDateTime().Date).Days >= StoneFactoryConfig.StoneFactoryStoneFreezingDays)
                    {
                        //可用矿石
                        sumEnableStoneStack += item.JoinStoneStackCount;
                    }
                    else
                    {
                        sumFreezingStoneStack += item.JoinStoneStackCount;
                    }
                    if (item.JoinStoneStackCount > 0 && (timeNow.Date - item.Time.ToDateTime().Date).Days >= StoneFactoryConfig.StoneStackWithdrawLimitDays)
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
            StoneFactoryProfitRMBChangedRecord[] items = null;
            bool isOK = MyDBHelper.Instance.ConnectionCommandSelect(mycmd =>
            {
                string sqlTextA = "SELECT n.* FROM  stonefactoryprofitrmbchangedrecord n ";

                StringBuilder builder = new StringBuilder();
                if (userID > 0)
                {
                    builder.Append(" n.UserID = @UserID ");
                    mycmd.Parameters.AddWithValue("@UserID", userID);
                }
                if (beginTime != null && !beginTime.IsNull && endTime != null && !endTime.IsNull)
                {
                    if (builder.Length != 0)
                    {
                        builder.Append(" and ");
                    }
                    DateTime bTime = beginTime.ToDateTime();
                    DateTime eTime = endTime.ToDateTime();
                    if (bTime >= eTime)
                    {
                        return;
                    }
                    builder.Append(" n.OperTime >= @beginTime and n.OperTime < @endTime ");
                    mycmd.Parameters.AddWithValue("@beginTime", bTime);
                    mycmd.Parameters.AddWithValue("@endTime", eTime);
                }
                string sqlWhere = "";
                if (builder.Length > 0)
                {
                    sqlWhere = " where " + builder.ToString();
                }

                string sqlOrderLimit = " order by n.ID desc ";
                if (pageItemCount > 0)
                {
                    int start = pageIndex <= 0 ? 0 : (pageIndex - 1) * pageItemCount;
                    sqlOrderLimit += " limit " + start.ToString() + ", " + pageItemCount;
                }

                string sqlAllText = sqlTextA + sqlWhere + sqlOrderLimit;

                mycmd.CommandText = sqlAllText;
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(table);
                items = MetaDBAdapter<StoneFactoryProfitRMBChangedRecord>.GetStoneFactoryProfitRMBChangedRecordItemFromDataTable(table);
                table.Dispose();
                adapter.Dispose();
            });

            return items;
        }

        //public StoneFactoryStackChangeRecord[] GetFactoryStackChangedRecord(int userID, MyDateTime beginTime, MyDateTime endTime, int pageItemCount, int pageIndex)
        //{
        //    return null;
        //}

        //public StoneFactoryOneGroupSlave[] GetFactorySlaveGroupInfos(int userID, MyDateTime beginTime, MyDateTime endTime, int pageItemCount, int pageIndex)
        //{
        //    return null;
        //}

        public StoneFactorySystemDailyProfit[] GetFactorySystemDailyProfitRecords(int pageItemCount, int pageIndex)
        {
            StoneFactorySystemDailyProfit[] items = null;
            bool isOK = MyDBHelper.Instance.ConnectionCommandSelect(mycmd =>
            {
                string sqlTextA = "SELECT n.* FROM  stonefactorysystemdailyprofit n ";

                StringBuilder builder = new StringBuilder();

                string sqlOrderLimit = " order by n.ID desc ";
                if (pageItemCount > 0)
                {
                    int start = pageIndex <= 0 ? 0 : (pageIndex - 1) * pageItemCount;
                    sqlOrderLimit += " limit " + start.ToString() + ", " + pageItemCount;
                }

                string sqlAllText = sqlTextA + sqlOrderLimit;

                mycmd.CommandText = sqlAllText;
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(table);
                items = MetaDBAdapter<StoneFactorySystemDailyProfit>.GetStoneFactorySystemDailyProfitItemFromDataTable(table);
                table.Dispose();
                adapter.Dispose();
            });

            return items;
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
            MySqlCommand mycmd = null;
            try
            {
                mycmd = myTrans.CreateCommand();
                string sqlText = "insert into stonefactoryprofitrmbchangedrecord " +
                        "(`UserID`,`OperRMB`,`ProfitType`,`OperTime`) " +
                        " values (@UserID,@OperRMB,@ProfitType,@OperTime) ";
                mycmd.CommandText = sqlText;
                mycmd.Parameters.AddWithValue("@UserID", record.UserID);
                mycmd.Parameters.AddWithValue("@OperRMB", record.OperRMB);
                mycmd.Parameters.AddWithValue("@ProfitType", (int)record.ProfitType);
                mycmd.Parameters.AddWithValue("@OperTime", record.OperTime.ToDateTime());
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

    }
}
