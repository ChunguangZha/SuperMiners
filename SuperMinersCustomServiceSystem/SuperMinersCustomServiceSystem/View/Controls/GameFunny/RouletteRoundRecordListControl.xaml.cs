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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SuperMinersCustomServiceSystem.View.Controls.GameFunny
{
    /// <summary>
    /// Interaction logic for RouletteRoundRecordListControl.xaml
    /// </summary>
    public partial class RouletteRoundRecordListControl : UserControl
    {
        public RouletteRoundRecordListControl()
        {
            InitializeComponent();

            this.dgRecords.ItemsSource = App.GameRouletteVMObject.ListRoundRecords;

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (GlobalData.RouletteConfig != null)
            {
                this.numMultiple.Value = (double)GlobalData.RouletteConfig.RouletteLargeWinMultiple;
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            App.GameRouletteVMObject.AsyncGetAllRouletteRoundInfo();
        }

        private void btnSaveMultiple_Click(object sender, RoutedEventArgs e)
        {
            decimal multiple = (decimal)numMultiple.Value;
            App.BusyToken.ShowBusyWindow("正在提交数据...");
            GlobalData.Client.SaveRouletteLargeWinMultipleCompleted += Client_SaveRouletteLargeWinMultipleCompleted;
            GlobalData.Client.SaveRouletteLargeWinMultiple(multiple);
        }

        void Client_SaveRouletteLargeWinMultipleCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<bool> e)
        {
            App.BusyToken.CloseBusyWindow();
            GlobalData.Client.SaveRouletteLargeWinMultipleCompleted -= Client_SaveRouletteLargeWinMultipleCompleted;
            try
            {
                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("保存大奖中奖倍数，服务器返回异常。信息为：" + e.Error);
                    return;
                }

                if (e.Result)
                {
                    MyMessageBox.ShowInfo("保存大奖中奖倍数成功");
                    GlobalData.RouletteConfig.RouletteLargeWinMultiple = (decimal)numMultiple.Value;
                }
                else
                {
                    MyMessageBox.ShowInfo("保存大奖中奖倍数失败。");
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("保存大奖中奖倍数，回调处理异常。信息为：" + exc.Message);
            }
        }
    }
}
