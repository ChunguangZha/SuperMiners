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

namespace SuperMinersWPF.Views
{
    /// <summary>
    /// Interaction logic for BuyMineWindow.xaml
    /// </summary>
    public partial class BuyMineWindow : Window
    {
        public BuyMineWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            GlobalData.Client.BuyMineCompleted += Client_BuyMineCompleted;
            this.txtRMB.Text = GlobalData.CurrentUser.RMB.ToString();
            this.txtGoldCoin.Text = GlobalData.CurrentUser.GoldCoin.ToString();
            this.txtGoldCoin_Mine.Text = GlobalData.GameConfig.RMB_Mine.ToString();
        }

        void Client_BuyMineCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<int> e)
        {
            if (e.Cancelled)
            {
                return;
            }

            if (e.Error != null)
            {
                MyMessageBox.ShowInfo("访问服务器失败。");
                return;
            }

            int result = e.Result;
            if (result < 0)
            {
                MyMessageBox.ShowInfo("服务器不存在当前用户，请联系平台客服。");
                return;
            }
            if (result == 0)
            {
                MyMessageBox.ShowInfo("购买失败。");
                return;
            }

            App.UserVMObject.AsyncGetPlayerInfo();

            this.DialogResult = true;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            int count = (int)this.numMinesCount.Value;
            float money = count * GlobalData.GameConfig.RMB_Mine;
            this.txtNeedMoney.Text = money.ToString();
            if (money > GlobalData.CurrentUser.RMB)
            {
                MyMessageBox.ShowInfo("账户余额不足，请充值。");
                return;
            }
            GlobalData.Client.BuyMine(count);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void numMinersCount_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int count = (int)this.numMinesCount.Value;
            float money = count * GlobalData.GameConfig.RMB_Mine;
            this.txtNeedMoney.Text = money.ToString();
            if (money > GlobalData.CurrentUser.RMB)
            {
                this.txtError.Visibility = System.Windows.Visibility.Visible;
                return;
            }
        }
    }
}
