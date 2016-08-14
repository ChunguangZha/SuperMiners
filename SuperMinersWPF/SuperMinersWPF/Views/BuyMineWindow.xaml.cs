using MetaData;
using MetaData.Trade;
using SuperMinersWPF.StringResources;
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
            this.txtRMB_Mine.Text = GlobalData.GameConfig.RMB_Mine.ToString();
        }

        void Client_BuyMineCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<TradeOperResult> e)
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

            TradeOperResult result = e.Result;
            if (result.ResultCode != OperResult.RESULTCODE_TRUE)
            {
                MyMessageBox.ShowInfo("勘探矿山失败。原因：" + ResultCodeMsg.GetMsg(result.ResultCode));
                return;
            }
            if (result.PayType == (int)PayType.Alipay)
            {
                MyWebPage.ShowMyWebPage(result.AlipayLink);
                MyMessageBox.ShowInfo("请在弹出的网页中，登录支付宝进行付款。");
                //return;
            }

            App.UserVMObject.AsyncGetPlayerInfo();

            this.DialogResult = true;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            int count = (int)this.numMinesCount.Value;
            if (count == 0)
            {
                MyMessageBox.ShowInfo("请输入有效" + Strings.Mine + "数");
                return;
            }

            int payType = (int)PayType.Alipay;
            if (this.chkPayType.IsChecked == false)
            {
                payType = (int)PayType.RMB;
                float money = count * GlobalData.GameConfig.RMB_Mine;
                this.txtNeedMoney.Text = money.ToString();
                if (money > GlobalData.CurrentUser.RMB)
                {
                    MyMessageBox.ShowInfo("账户余额不足，请充值。");
                    return;
                }
            }
            GlobalData.Client.BuyMine(count, payType);
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
            }
            else
            {
                this.txtError.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void chkPayType_Checked(object sender, RoutedEventArgs e)
        {
            this.chkPayType.Content = "支付宝支付";
        }

        private void chkPayType_Unchecked(object sender, RoutedEventArgs e)
        {
            this.chkPayType.Content = "灵币支付";
        }
    }
}
