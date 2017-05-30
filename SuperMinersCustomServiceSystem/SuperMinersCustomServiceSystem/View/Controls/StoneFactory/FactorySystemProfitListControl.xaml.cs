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

namespace SuperMinersCustomServiceSystem.View.Controls.StoneFactory
{
    /// <summary>
    /// Interaction logic for FactorySystemProfitListControl.xaml
    /// </summary>
    public partial class FactorySystemProfitListControl : UserControl
    {
        private System.Threading.SynchronizationContext _syn;

        public FactorySystemProfitListControl()
        {
            InitializeComponent();
            _syn = System.Threading.SynchronizationContext.Current;

            this.DataContext = App.StoneFactoryVMObject;

            GlobalData.Client.GetSumLastDayValidStoneStackCompleted += Client_GetSumLastDayValidStoneStackCompleted;
        }

        void Client_GetSumLastDayValidStoneStackCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<int> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("获取工厂昨日总有数矿石机数失败。原因为：" + e.Error.Message);
                    return;
                }

                _syn.Post(o =>
                {
                    StoneFactorySetYesterdayProfitWindow win = new StoneFactorySetYesterdayProfitWindow(Convert.ToInt32(o));
                    win.ShowDialog();

                    App.StoneFactoryVMObject.AsyncGetStoneFactorySystemDailyProfitList(GlobalData.PageItemsCount, 1);
                }, e.Result);
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("获取工厂昨日总有数矿石机数返回处理异常1。原因为：" + exc.Message);
            }
        }

        private void btnSetYesterdayProfit_Click(object sender, RoutedEventArgs e)
        {
            App.BusyToken.ShowBusyWindow("加载信息");
            GlobalData.Client.GetSumLastDayValidStoneStack();
        }
    }
}
