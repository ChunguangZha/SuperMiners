using MetaData.Game.Roulette;
using SuperMinersWPF.Models;
using SuperMinersWPF.Utility;
using SuperMinersWPF.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for GameRouletteControl.xaml
    /// </summary>
    public partial class GameRouletteControl : UserControl
    {
        private SynchronizationContext _syn;

        Thread _thrRoulette = null;

        private Color _selectItemColor = Color.FromArgb(255, 180, 252, 247);
        private Color _normalItemColor = Color.FromArgb(255, 255, 220, 21);
        //private RouletteWinAwardResult _winedAwardResult = null;
        private int _winedAwardItemID = 0;
        int _startIndex;
        int _downSpeedStartIndex = 3 * 12;
        int _endIndex;
        int _endTickIndex;

        public GameRouletteControl()
        {
            InitializeComponent();
            _syn = SynchronizationContext.Current;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            BindUI();
            App.GameRouletteVMObject.AsyncGetAllAwardItems();
            App.GameRouletteVMObject.AsyncGetAllAwardRecord(-1, null, null, -1, -1, 10, 1);
        }

        private void BindUI()
        {
            Binding bind = new Binding();
            bind.Source = App.GameRouletteVMObject.ListActiveWinAwardInfos;
            this.listAwardRecords.SetBinding(ListView.ItemsSourceProperty, bind);
        }

        public void AddEventHandlers()
        {
            App.GameRouletteVMObject.AwardItemsListChanged += GameRouletteVMObject_AwardItemsListChanged;
            GlobalData.Client.StartRouletteCompleted += Client_StartRouletteCompleted;
            GlobalData.Client.FinishRouletteCompleted += Client_FinishRouletteCompleted;
        }

        public void RemoveEventHandlers()
        {
            App.GameRouletteVMObject.AwardItemsListChanged -= GameRouletteVMObject_AwardItemsListChanged;
            GlobalData.Client.StartRouletteCompleted -= Client_StartRouletteCompleted;
            GlobalData.Client.FinishRouletteCompleted += Client_FinishRouletteCompleted;
        }

        void GameRouletteVMObject_AwardItemsListChanged()
        {
            _syn.Post(o =>
            {
                BindAwardItems();
            }, null);
        }

        private void BindAwardItems()
        {
            if (App.GameRouletteVMObject.ListAwardItems.Count == 0)
            {
                this.panelRoulette.Visibility = System.Windows.Visibility.Collapsed;
                this.panelNotOpen.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                this.awardItem1.DataContext = App.GameRouletteVMObject.ListAwardItems[0];
                this.awardItem2.DataContext = App.GameRouletteVMObject.ListAwardItems[1];
                this.awardItem3.DataContext = App.GameRouletteVMObject.ListAwardItems[2];
                this.awardItem4.DataContext = App.GameRouletteVMObject.ListAwardItems[3];
                this.awardItem5.DataContext = App.GameRouletteVMObject.ListAwardItems[4];
                this.awardItem6.DataContext = App.GameRouletteVMObject.ListAwardItems[5];
                this.awardItem7.DataContext = App.GameRouletteVMObject.ListAwardItems[6];
                this.awardItem8.DataContext = App.GameRouletteVMObject.ListAwardItems[7];
                this.awardItem9.DataContext = App.GameRouletteVMObject.ListAwardItems[8];
                this.awardItem10.DataContext = App.GameRouletteVMObject.ListAwardItems[9];
                this.awardItem11.DataContext = App.GameRouletteVMObject.ListAwardItems[10];
                this.awardItem12.DataContext = App.GameRouletteVMObject.ListAwardItems[11];

                this.panelRoulette.Visibility = System.Windows.Visibility.Visible;
                this.panelNotOpen.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private int FindAwardIDIndex(int awardItemID)
        {
            int index = 11;
            for (int i = 0; i < App.GameRouletteVMObject.ListAwardItems.Count; i++)
            {
                if (App.GameRouletteVMObject.ListAwardItems[i].ID == awardItemID)
                {
                    index = i;
                    break;
                }
            }

            return index;
        }

        void Client_StartRouletteCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<MetaData.Game.Roulette.RouletteWinAwardResult> e)
        {
            try
            {
                if (e.Error != null || e.Result == null)
                {
                    MyMessageBox.ShowInfo("连接服务器失败。");
                    return;
                }

                this._winedAwardItemID = e.Result.WinAwardItemID;

                _startIndex = new Random(1).Next(0, 11);
                _endIndex = FindAwardIDIndex(e.Result.WinAwardItemID);
                _endTickIndex = 4 * 12 + _endIndex;
                _downSpeedStartIndex = 3 * 12 + _endIndex;

                //MessageBox.Show(App.GameRouletteVMObject.ListAwardItems[e.Result.WinAwardItemIndex].AwardName);
                CreateRouletteThread();
            }
            catch (Exception exc)
            {
                this.btnStart.IsEnabled = true;
                MyMessageBox.ShowInfo(exc.Message);
            }
        }

        void Client_FinishRouletteCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<RouletteWinnerRecord> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("提交服务器异常，请联系管理员。");
                    this.btnStart.IsEnabled = true;
                    return;
                }
                if (e.Result == null)
                {
                    MyMessageBox.ShowInfo("提交服务器失败，请联系管理员。");
                    this.btnStart.IsEnabled = true;
                    return;
                }
                _syn.Post(o =>
                {
                    RouletteAwardItemUIModel awardItem = new RouletteAwardItemUIModel(e.Result.AwardItem);
                    this.imgWinedAwardItem.Source = awardItem.Icon;
                    this.txtWinedAwardItem.Text = awardItem.AwardName;
                    this.panelWinedAwardItem.Visibility = System.Windows.Visibility.Visible;
                    App.GameRouletteVMObject.ListMyWinAwardRecords.Add(new SuperMinersCustomServiceSystem.Model.RouletteWinnerRecordUIModel(e.Result));

                    //RouletteWinAwardAlertWindow win = new RouletteWinAwardAlertWindow(e.Result);
                    //win.ShowDialog();

                    ResetItemBackground();
                }, null);
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo(exc.Message);
            }
        }

        private void CreateRouletteThread()
        {
            this._thrRoulette = new Thread(new ThreadStart(DoThreadRoulette));
            this._thrRoulette.IsBackground = true;
            this._thrRoulette.Name = "ThreadRoulette";
            this._thrRoulette.Start();
        }

        private void DoThreadRoulette()
        {
            try
            {
                for (int i = _startIndex; i <= _endTickIndex; i++)
                {
                    int index = i % 12;
                    int lastIndex = (i - 1) % 12;
                    _syn.Post(p =>
                    {
                        App.GameRouletteVMObject.ListAwardItems[index].Background = new SolidColorBrush(_selectItemColor);
                        App.GameRouletteVMObject.ListAwardItems[lastIndex].Background = new SolidColorBrush(_normalItemColor);
                    }, null);
                    if (i < _downSpeedStartIndex)
                    {
                        Thread.Sleep(50);
                    }
                    else
                    {
                        Thread.Sleep((i - _downSpeedStartIndex + 1) * 50);
                    }
                }

                _syn.Post(o =>
                {
                    App.BusyToken.ShowBusyWindow("正在处理中...");
                    GlobalData.Client.FinishRoulette(this._winedAwardItemID, null);
                }, null);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
                
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            if (GlobalData.CurrentUser.SellableStones < 100)
            {
                MyMessageBox.ShowInfo("您的矿石不足100，无法抽奖。");
                return;
            }

            this.btnStart.IsEnabled = false;
            GlobalData.Client.StartRoulette(null);
        }

        private void ResetItemBackground()
        {
            for (int i = 0; i < App.GameRouletteVMObject.ListAwardItems.Count; i++)
            {
                App.GameRouletteVMObject.ListAwardItems[i].Background = new SolidColorBrush(_normalItemColor);
            }
        }

        private void btnViewMyWinAwardRecord_Click(object sender, RoutedEventArgs e)
        {
            RouletteMyWinnedRecordsWindow win = new RouletteMyWinnedRecordsWindow();
            win.ShowDialog();
        }

        private void panelWinedAlardItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.panelWinedAwardItem.Visibility = System.Windows.Visibility.Collapsed;
            this.btnStart.IsEnabled = true;
        }
    }
}
