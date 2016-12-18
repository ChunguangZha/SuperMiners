using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChangPlayerLockedInfoTableFixPackage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal static readonly string CONNECTIONSTRING = "server=localhost;port=13344; uid=superminersDBA;pwd=dba!@#123;database=superminers;charset=utf8; pooling=false; Keep Alive=5; Allow User Variables=True;";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            ChangeLockedInfoTable();
        }

        private void RejectAllSellStoneOrders()
        {
            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;
            try
            {
                DataTable dtSellStoneOrders = new DataTable();

                myconn = new MySqlConnection(CONNECTIONSTRING);
                myconn.Open();
                string cmdText = "SELECT * FROM superminers.sellstonesorder where OrderState = 1;";
                mycmd = new MySqlCommand(cmdText, myconn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(dtSellStoneOrders);
                mycmd.Dispose();
                adapter.Dispose();

                if (dtSellStoneOrders != null)
                {
                    for (int i = 0; i < dtSellStoneOrders.Rows.Count; i++)
                    {
                        string sqlSelectFortune = "select `FreezingStones` from  `playerfortuneinfo`  " +
                            " WHERE `UserID`=(SELECT b.id FROM playersimpleinfo b where b.UserName = @UserName);";

                        string userName = dtSellStoneOrders.Rows[i]["SellerUserName"].ToString();
                        int stoneCount = Convert.ToInt32(dtSellStoneOrders.Rows[i]["SellStonesCount"]);

                        mycmd = myconn.CreateCommand();
                        string sqlText = "UPDATE `playerfortuneinfo` SET  `FreezingStones`=@FreezingStones " + 
                            " WHERE `UserID`=(SELECT b.id FROM playersimpleinfo b where b.UserName = @UserName);";
                        mycmd.CommandText = sqlText;
                        mycmd.Parameters.AddWithValue("@UserName", 100);
                        mycmd.Parameters.AddWithValue("@FreezingStones", 100);
                        mycmd.ExecuteNonQuery();
                        mycmd.Dispose();
                    }
                }

                this.lblMsg.Text = "修复成功";

            }
            catch (Exception exc)
            {
                this.lblMsg.Text = exc.Message;
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

        private void ChangeLockedInfoTable()
        {
            MySqlConnection myconn = null;
            MySqlCommand mycmd = null;
            try
            {
                DataTable dt = new DataTable();

                myconn = new MySqlConnection(CONNECTIONSTRING);
                myconn.Open();
                string cmdText = "select * from playersimpleinfo ";
                mycmd = new MySqlCommand(cmdText, myconn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(mycmd);
                adapter.Fill(dt);
                mycmd.Dispose();
                adapter.Dispose();

                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        bool isLocked = Convert.ToBoolean(dt.Rows[i]["LockedLogin"]);
                        if (isLocked)
                        {
                            int userID = Convert.ToInt32(dt.Rows[i]["id"]);
                            DateTime time = Convert.ToDateTime(dt.Rows[i]["LockedLoginTime"]);
                            mycmd = myconn.CreateCommand();
                            string sqlText = "delete from playerlockedinfo where `UserID` = @UserID; " +
                                            "insert into playerlockedinfo (`UserID`, `LockedLogin`, `LockedLoginTime`, `ExpireDays`) " +
                                            "values (@UserID, @LockedLogin, @LockedLoginTime, @ExpireDays); ";
                            mycmd.CommandText = sqlText;
                            mycmd.Parameters.AddWithValue("@UserID", dt.Rows[i]["id"]);
                            mycmd.Parameters.AddWithValue("@LockedLogin", true);
                            mycmd.Parameters.AddWithValue("@LockedLoginTime", time);
                            mycmd.Parameters.AddWithValue("@ExpireDays", 100);
                            mycmd.ExecuteNonQuery();
                            mycmd.Dispose();
                        }
                    }
                }

                this.lblMsg.Text = "修复成功";

            }
            catch (Exception exc)
            {
                this.lblMsg.Text = exc.Message;
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
