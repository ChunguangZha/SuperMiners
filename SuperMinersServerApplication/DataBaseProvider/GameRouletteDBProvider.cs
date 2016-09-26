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
    public class GameRouletteDBProvider
    {
        public bool SaveRouletteAwardItems(RouletteAwardItem[] items)
        {
            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();

                string sqlDeleteText = "delete from rouletteawarditem;";
                mycmd = myconn.CreateCommand();
                mycmd.CommandText = sqlDeleteText;
                mycmd.ExecuteNonQuery();
                mycmd.Dispose();

                foreach (var item in items)
                {
                    string sqlInsertText = "insert into rouletteawarditem " +
                        " (`AwardName`, `AwardNumber`, `RouletteAwardType`, `ValueMoneyYuan`, `IsLargeAward`, `IsRealAward`, `WinProbability`) " +
                        " values (@AwardName, @AwardNumber, @RouletteAwardType, @ValueMoneyYuan, @IsLargeAward, @IsRealAward, @WinProbability)";

                    mycmd = myconn.CreateCommand();
                    mycmd.CommandText = sqlInsertText;
                    mycmd.Parameters.AddWithValue("@AwardName", item.AwardName);
                    mycmd.Parameters.AddWithValue("@AwardNumber", item.AwardNumber);
                    mycmd.Parameters.AddWithValue("@RouletteAwardType", (int)item.RouletteAwardType);
                    mycmd.Parameters.AddWithValue("@ValueMoneyYuan", item.ValueMoneyYuan);
                    mycmd.Parameters.AddWithValue("@IsLargeAward", item.IsLargeAward);
                    mycmd.Parameters.AddWithValue("@IsRealAward", item.IsRealAward);
                    mycmd.Parameters.AddWithValue("@WinProbability", item.WinProbability);

                    mycmd.ExecuteNonQuery();
                    mycmd.Dispose();
                }

                mycmd = null;

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

        public RouletteAwardItem[] GetRouletteAwardItems()
        {
            RouletteAwardItem[] items = null;
            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();
                string sqlText = "select * from rouletteawarditem";
                mycmd = myconn.CreateCommand();
                mycmd.CommandText = sqlText;
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);

                DataTable table = new DataTable();
                adapter.Fill(table);
                items = MetaDBAdapter<RouletteAwardItem>.GetRouletteAwardItemFromDataTable(table);
                return items;
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

        public bool AddRouletteWinnerRecord(RouletteWinnerRecord record)
        {
            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();

                string sqlInsertText = "insert into roulettewinnerrecord " +
                    " (`UserID`, `AwardItemID`, `WinTime`, `IsGot`, `GotTime`, `IsPay`, `PayTime`, `GotInfo1`, `GotInfo2`) " +
                    " values (@UserID, @AwardItemID, @WinTime, @IsGot, @GotTime, @IsPay, @PayTime, @GotInfo1, @GotInfo2)";

                mycmd = myconn.CreateCommand();
                mycmd.CommandText = sqlInsertText;
                mycmd.Parameters.AddWithValue("@UserID", record.UserID);
                mycmd.Parameters.AddWithValue("@AwardItemID", record.AwardItem.ID);
                mycmd.Parameters.AddWithValue("@WinTime", record.WinTime);
                mycmd.Parameters.AddWithValue("@IsGot", record.IsGot);
                if (record.GotTime == null)
                {
                    mycmd.Parameters.AddWithValue("@GotTime", DBNull.Value);
                }
                else
                {
                    mycmd.Parameters.AddWithValue("@GotTime", record.GotTime);
                }
                mycmd.Parameters.AddWithValue("@IsPay", record.IsPay);
                if (record.PayTime == null)
                {
                    mycmd.Parameters.AddWithValue("@PayTime", DBNull.Value);
                }
                else
                {
                    mycmd.Parameters.AddWithValue("@PayTime", record.PayTime);
                }
                mycmd.Parameters.AddWithValue("@GotInfo1", DESEncrypt.EncryptDES(record.GotInfo1));
                mycmd.Parameters.AddWithValue("@GotInfo2", DESEncrypt.EncryptDES(record.GotInfo2));

                mycmd.ExecuteNonQuery();
                mycmd.Dispose();
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

        public bool SetWinnerRecordGot(RouletteWinnerRecord record)
        {
            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();

                string sqlInsertText = "update roulettewinnerrecord " +
                    " set `IsGot` = @IsGot, `GotTime` = @GotTime, `GotInfo1` = @GotInfo1, `GotInfo2` = @GotInfo2 where `id` = @ID ";

                mycmd = myconn.CreateCommand();
                mycmd.CommandText = sqlInsertText;
                mycmd.Parameters.AddWithValue("@IsGot", record.IsGot);
                mycmd.Parameters.AddWithValue("@GotTime", record.GotTime);
                mycmd.Parameters.AddWithValue("@GotInfo1", DESEncrypt.EncryptDES(record.GotInfo1));
                mycmd.Parameters.AddWithValue("@GotInfo2", DESEncrypt.EncryptDES(record.GotInfo2));
                mycmd.Parameters.AddWithValue("@ID", record.RecordID);

                mycmd.ExecuteNonQuery();
                mycmd.Dispose();
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

        public bool SetWinnerRecordPay(RouletteWinnerRecord record)
        {
            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();

                string sqlInsertText = "update roulettewinnerrecord " +
                    " set `IsPay` = @IsPay, `PayTime` = @PayTime where `id` = @ID ";

                mycmd = myconn.CreateCommand();
                mycmd.CommandText = sqlInsertText;
                mycmd.Parameters.AddWithValue("@IsPay", record.IsPay);
                mycmd.Parameters.AddWithValue("@PayTime", record.PayTime);
                mycmd.Parameters.AddWithValue("@ID", record.RecordID);

                mycmd.ExecuteNonQuery();
                mycmd.Dispose();
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

        ///// <summary>
        ///// 没有填充AwardItem属性
        ///// </summary>
        ///// <returns></returns>
        //public RouletteWinnerRecord[] GetNotTokeWinAwardRecords()
        //{
        //    RouletteWinnerRecord[] records = null;
        //    MySqlConnection myconn = null;
        //    MySqlCommand mycmd = null;
        //    try
        //    {
        //        myconn = MyDBHelper.Instance.CreateConnection();
        //        myconn.Open();
        //        string sqlText = "select  r.*, s.UserName, s.NickName from roulettewinnerrecord r left join playersimpleinfo s on r.UserID = s.id where r.IsGot = @IsGot";
        //        mycmd = myconn.CreateCommand();
        //        mycmd.CommandText = sqlText;
        //        mycmd.Parameters.AddWithValue("@IsGot", false);
        //        MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);

        //        DataTable table = new DataTable();
        //        adapter.Fill(table);
        //        records = MetaDBAdapter<RouletteWinnerRecord>.GetRouletteWinnerRecordFromDataTable(table);
        //        return records;
        //    }
        //    finally
        //    {
        //        if (mycmd != null)
        //        {
        //            mycmd.Dispose();
        //        }
        //        if (myconn != null)
        //        {
        //            myconn.Close();
        //            myconn.Dispose();
        //        }
        //    }
        //}

        /// <summary>
        /// 没有填充AwardItem属性
        /// </summary>
        /// <returns></returns>
        public RouletteWinnerRecord[] GetNotPayWinAwardRecords()
        {
            RouletteWinnerRecord[] records = null;
            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;
            try
            {
                myconn = MyDBHelper.Instance.CreateConnection();
                myconn.Open();
                string sqlText = "select  r.*, s.UserName, s.NickName from roulettewinnerrecord r left join playersimpleinfo s on r.UserID = s.id where r.IsPay = @IsPay";
                mycmd = myconn.CreateCommand();
                mycmd.CommandText = sqlText;
                mycmd.Parameters.AddWithValue("@IsPay", false);
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);

                DataTable table = new DataTable();
                adapter.Fill(table);
                records = MetaDBAdapter<RouletteWinnerRecord>.GetRouletteWinnerRecordFromDataTable(table);
                return records;
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
    }
}
