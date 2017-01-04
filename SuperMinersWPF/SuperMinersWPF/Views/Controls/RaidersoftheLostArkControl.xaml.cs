using SuperMinersWPF.Utility;
using SuperMinersWPF.Views.Windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SuperMinersWPF.Views.Controls
{
    /// <summary>
    /// Interaction logic for RaidersoftheLostArkControl.xaml
    /// </summary>
    public partial class RaidersoftheLostArkControl : UserControl
    {
        public RaidersoftheLostArkControl()
        {
            InitializeComponent();
            BindUI();
        }

        private void BindUI()
        {
            this.DataContext = App.GameRaiderofLostArkVMObject.CurrentRaiderRound;
            this.listSelfCurrentBetRecords.ItemsSource = App.GameRaiderofLostArkVMObject.ListSelfBetRecords;
            this.listRaiderHistoryRecords.ItemsSource = App.GameRaiderofLostArkVMObject.ListHistoryRaiderRoundRecords;
        }

        private void btnSubtract_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnJoin_Click(object sender, RoutedEventArgs e)
        {
            if (App.GameRaiderofLostArkVMObject.CurrentRaiderRound == null || App.GameRaiderofLostArkVMObject.CurrentRaiderRound.ID == 0)
            {
                MyMessageBox.ShowInfo("游戏还未开始，请稍候再试");
                return;
            }
                        
            int betStoneCount = (int)this.numBetStoneCount.Value;
            if (GlobalData.CurrentUser.SellableStones < betStoneCount)
            {
                MyMessageBox.ShowInfo("抱歉，您没有足够的矿石下注");
                return;
            }
            App.GameRaiderofLostArkVMObject.AsyncJoinRaider(App.GameRaiderofLostArkVMObject.CurrentRaiderRound.ID, betStoneCount);
        }

        private void btnViewMyRaiderRecord_Click(object sender, RoutedEventArgs e)
        {
            MyRaiderBetRecordsWindow win = new MyRaiderBetRecordsWindow();
            win.ShowDialog();
        }
    }
}
