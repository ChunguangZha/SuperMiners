using MetaData;
using SuperMinersCustomServiceSystem.Uility;
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

namespace SuperMinersCustomServiceSystem.View.Controls.StoneFactory
{
    /// <summary>
    /// Interaction logic for StoneFactorySetYesterdayProfitWindow.xaml
    /// </summary>
    public partial class StoneFactorySetYesterdayProfitWindow : Window
    {
        private int YesterdaySumValidStoneStackCount = 0;
        private decimal ProfitRate = 0;

        public StoneFactorySetYesterdayProfitWindow(int yesterdaySumStoneStack)
        {
            InitializeComponent();

            this.YesterdaySumValidStoneStackCount = yesterdaySumStoneStack;
            this.txtTotalValidStoneCount.Text = yesterdaySumStoneStack.ToString();
            GlobalData.Client.AdminSetStoneFactoryProfitRateCompleted += Client_AdminSetStoneFactoryProfitRateCompleted;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            ProfitRate = Math.Round(((decimal)this.numSetProfitYuan.Value * GlobalData.GameConfig.Yuan_RMB), 2);

            App.BusyToken.ShowBusyWindow("正在保存工厂昨日收益");
            GlobalData.Client.AdminSetStoneFactoryProfitRate(ProfitRate);
        }

        void Client_AdminSetStoneFactoryProfitRateCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<int> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("保存工厂昨日收益失败。原因为：" + e.Error.Message);
                    return;
                }

                if (e.Result == OperResult.RESULTCODE_TRUE)
                {
                    MyMessageBox.ShowInfo("保存成功");
                    this.Close();
                }
                else
                {
                    MyMessageBox.ShowInfo("保存失败，原因为：" + OperResult.GetMsg(e.Result));
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("保存工厂昨日收益返回处理异常1。原因为：" + exc.Message);
            }
        }

        private void numSetProfitYuan_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.numSetProfitYuan != null)
            {
                ProfitRate = Math.Round(((decimal)this.numSetProfitYuan.Value * GlobalData.GameConfig.Yuan_RMB), 2);
                this.txtProfitRMB.Text = ProfitRate.ToString();
            }
        }
    }
}
