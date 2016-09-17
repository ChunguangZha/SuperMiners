using MetaData;
using SuperMinersWPF.Utility;
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
using System.Windows.Shapes;

namespace SuperMinersWPF.Views.Windows
{
    /// <summary>
    /// Interaction logic for WithdrawRMBWindow.xaml
    /// </summary>
    public partial class WithdrawRMBWindow : Window
    {
        private SynchronizationContext _syn;
        public WithdrawRMBWindow()
        {
            InitializeComponent();
            _syn = SynchronizationContext.Current;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GlobalData.Client.WithdrawRMBCompleted += Client_WithdrawRMBCompleted;

            if (this.txtRMB != null)
            {
                this.txtRMB.Text = GlobalData.CurrentUser.EnbleRMB.ToString();
                this.txtYuan_RMB.Text = GlobalData.GameConfig.Yuan_RMB.ToString();
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            GlobalData.Client.WithdrawRMBCompleted -= Client_WithdrawRMBCompleted;
        }

        void Client_WithdrawRMBCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<int> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("提交提现申请失败。" + e.Error.Message);
                    return;
                }

                if (e.Result == OperResult.RESULTCODE_TRUE)
                {
                    MyMessageBox.ShowInfo("提交提现申请成功！");
                    App.UserVMObject.AsyncGetPlayerInfo();
                    _syn.Post(p =>
                    {
                        this.DialogResult = true;
                    }, null);
                }
                else
                {
                    MyMessageBox.ShowInfo("提交提现申请失败。原因为：" + OperResult.GetMsg(e.Result));
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("提交提现申请失败。原因为：" + exc.Message);
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (GlobalData.CurrentUser.Exp < 50)
            {
                MyMessageBox.ShowInfo("贡献值必须大于等于50，才能提现。");
                return;
            }
            if (string.IsNullOrEmpty(GlobalData.CurrentUser.Alipay) || string.IsNullOrEmpty(GlobalData.CurrentUser.AlipayRealName))
            {
                MyMessageBox.ShowInfo("请先绑定支付宝账户和真实姓名，然后再进行提现。提现钱将直接转到绑定的支付宝账户。");
                return;
            }

            int withdrawRMBCount = (int)Math.Floor(this.numWithdrawRMB.Value);
            if (withdrawRMBCount < (int)(5 * GlobalData.GameConfig.Yuan_RMB))
            {
                MyMessageBox.ShowInfo("一次至少要提现价值5元人民币的灵币。");
                return;
            }
            int valueYuanCount = (int)Math.Floor((decimal)this.numWithdrawRMB.Value / GlobalData.GameConfig.Yuan_RMB);
            if (valueYuanCount != (decimal)this.numWithdrawRMB.Value / GlobalData.GameConfig.Yuan_RMB)
            {
                MyMessageBox.ShowInfo("提现金额必须为人民币的整数。");
                return;
            }

            System.Windows.Forms.DialogResult digResult = MyMessageBox.ShowQuestionOKCancel("您的提现将直接转到您绑定的支付宝账户：" + GlobalData.CurrentUser.Alipay + ", 实名认证为：" + GlobalData.CurrentUser.AlipayRealName + " ,的账户里，请确保信息正确，由此带来的提现失败，平台概不负责。");
            if (digResult != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }

            App.BusyToken.ShowBusyWindow("正在提交提现申请...");
            GlobalData.Client.WithdrawRMB(withdrawRMBCount, null);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void numRechargeRMB_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.numGainYuan.Value = (double)((decimal)this.numWithdrawRMB.Value / GlobalData.GameConfig.Yuan_RMB);
        }
    }
}
