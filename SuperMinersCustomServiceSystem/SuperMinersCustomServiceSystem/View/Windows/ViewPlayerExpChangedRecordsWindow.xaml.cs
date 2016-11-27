using SuperMinersCustomServiceSystem.Model;
using SuperMinersCustomServiceSystem.Uility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace SuperMinersCustomServiceSystem.View.Windows
{
    /// <summary>
    /// Interaction logic for ViewPlayerExpChangedRecordsWindow.xaml
    /// </summary>
    public partial class ViewPlayerExpChangedRecordsWindow : Window
    {
        private int _userID;
        public ObservableCollection<ExpChangeRecordUIModel> List = new ObservableCollection<ExpChangeRecordUIModel>();

        public ViewPlayerExpChangedRecordsWindow(int userID)
        {
            InitializeComponent();

            _userID = userID;
            this.datagrid.ItemsSource = List;
            GlobalData.Client.GetExpChangeRecordCompleted += Client_GetExpChangeRecordCompleted;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (GlobalData.Client.IsConnected)
            {
                App.BusyToken.ShowBusyWindow("正在加载数据...");
                GlobalData.Client.GetExpChangeRecord(_userID);
            }
        }

        void Client_GetExpChangeRecordCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<MetaData.Trade.ExpChangeRecord[]> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("查寻玩家贡献值增长记录失败。信息为：" + e.Error.Message);
                    return;
                }

                if (e.Result != null)
                {
                    foreach (var item in e.Result)
                    {
                        List.Add(new ExpChangeRecordUIModel(item));
                    }
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("查寻玩家贡献值增长记录，回调处理异常。信息为：" + exc.Message);
            }
        }
    }
}
