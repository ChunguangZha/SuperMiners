using MetaData;
using MetaData.Game.Roulette;
using SuperMinersWPF.Utility;
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

namespace SuperMinersWPF.Views.Windows
{
    /// <summary>
    /// Interaction logic for RouletteWinAwardTakeWindow.xaml
    /// </summary>
    public partial class RouletteWinAwardTakeWindow : Window
    {
        public bool IsOK { get; private set; }
        RouletteWinnerRecord _record = null;
        public RouletteWinAwardTakeWindow(RouletteWinnerRecord record)
        {
            InitializeComponent();
            _record = record;
            IsOK = false;
        }

        void Client_TakeRouletteAwardCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<int> e)
        {
            try
            {
                GlobalData.Client.TakeRouletteAwardCompleted -= Client_TakeRouletteAwardCompleted;
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("领取奖品服务器操作异常，请联系管理员领取。给您带来不便，敬请谅解。");
                    return;
                }

                if (e.Result == OperResult.RESULTCODE_TRUE)
                {
                    MyMessageBox.ShowInfo("已经提交到服务器，管理员正在为您支付，请稍候。");
                    this.IsOK = true;
                    this.Close();
                }
                else
                {
                    MyMessageBox.ShowInfo("领取奖品失败。原因为：" + OperResult.GetMsg(e.Result));
                }
            }
            catch (Exception exc)
            {

            }
        }

        //private void SetUI()
        //{
        //    this.lblInfo2.Visibility = System.Windows.Visibility.Collapsed;
        //    this.txtInfo2.Visibility = System.Windows.Visibility.Collapsed;
        //    switch (_record.AwardItem.RouletteAwardType)
        //    {
        //        case RouletteAwardType.None:
        //            break;
        //        case RouletteAwardType.Stone:
        //            break;
        //        case RouletteAwardType.GoldCoin:
        //            break;
        //        case RouletteAwardType.Exp:
        //            break;
        //        case RouletteAwardType.StoneReserve:
        //            break;
        //        case RouletteAwardType.Huafei:
        //            this.lblInfo1.Text = "手机号";
        //            break;
        //        case RouletteAwardType.IQiyiOneMonth:
        //            this.lblInfo1.Text = "爱奇艺会员号";
        //            break;
        //        case RouletteAwardType.LeTV:
        //            this.lblInfo1.Text = "乐视会员号";
        //            break;
        //        case RouletteAwardType.Xunlei:
        //            this.lblInfo1.Text = "迅雷会员号";
        //            break;
        //        case RouletteAwardType.Junnet:
        //            break;
        //        default:
        //            break;
        //    }
        //}

        private void btnTake_Click(object sender, RoutedEventArgs e)
        {
            string info1 = this.txtInfo1.Text.Trim();
            string info2 = this.txtInfo2.Text.Trim();
            if (info1 == "")
            {
                MyMessageBox.ShowInfo("请填写QQ号。");
                return;
            }
            if (info2 == "")
            {
                MyMessageBox.ShowInfo("请填写手机号。");
                return;
            }

            GlobalData.Client.TakeRouletteAwardCompleted += Client_TakeRouletteAwardCompleted;
            App.BusyToken.ShowBusyWindow("正在领取...");
            GlobalData.Client.TakeRouletteAward(this._record.RecordID, info1, info2, null);
        }
    }
}
