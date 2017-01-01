using DataBaseProvider;
using MetaData.Trade;
using MetaData.User;
using SuperMinersServerApplication.Controller;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace SuperMinersServerApplication.Views
{
    /// <summary>
    /// Interaction logic for DeleteAllStoneSellOrderWindow.xaml
    /// </summary>
    public partial class DeleteAllStoneSellOrderWindow : Window
    {
        public DeleteAllStoneSellOrderWindow()
        {
            InitializeComponent();
        }

        private void LoadDB()
        {
            var allSellOrders = DBProvider.StoneOrderDBProvider.GetSellOrderList("", "", (int)SellOrderState.Wait, null, null, -1, -1);

            Dictionary<string, PlayerInfo> dicPlayers = new Dictionary<string, PlayerInfo>();

            foreach (var item in allSellOrders)
            {
                PlayerInfo playerInfo = null;
                if (dicPlayers.ContainsKey(item.SellerUserName))
                {
                    playerInfo = dicPlayers[item.SellerUserName];
                }
                else
                {
                    playerInfo = DBProvider.UserDBProvider.GetPlayer(item.SellerUserName);
                    dicPlayers.Add(item.SellerUserName, playerInfo);
                }

                playerInfo.FortuneInfo.FreezingStones -= item.SellStonesCount;
                if (playerInfo.FortuneInfo.FreezingStones < 0)
                {
                    playerInfo.FortuneInfo.FreezingStones = 0;
                }
                playerInfo.FortuneInfo.StockOfStones -= item.SellStonesCount;
                if (playerInfo.FortuneInfo.StockOfStones < 0)
                {
                    playerInfo.FortuneInfo.StockOfStones = 0;
                }
                var valueDiamond = item.ValueRMB - item.Expense;
                if (valueDiamond < 100) { valueDiamond = 100; }
                playerInfo.FortuneInfo.StockOfDiamonds += valueDiamond;

            }

            CustomerMySqlTransaction trans = MyDBHelper.Instance.CreateTrans();
            try
            {
                foreach (var item in dicPlayers.Values)
                {
                    DBProvider.UserDBProvider.SavePlayerFortuneInfo(item.FortuneInfo, trans);
                }

                DBProvider.StoneOrderDBProvider.UpdateAllSellOrderState(SellOrderState.Wait, SellOrderState.Finish, trans);
                trans.Commit();
                MessageBox.Show("操作成功");
            }
            catch (Exception exc)
            {
                trans.Rollback();
                MessageBox.Show(exc.Message);
            }
            finally
            {
                trans.Dispose();
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            LoadDB();
        }
    }
}
