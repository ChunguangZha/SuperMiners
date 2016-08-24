using MetaData;
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

namespace SuperMinersCustomServiceSystem.View
{
    /// <summary>
    /// Interaction logic for ViewPlayerSellStoneRecordWindow.xaml
    /// </summary>
    public partial class ViewPlayerSellStoneRecordWindow : Window
    {
        public ViewPlayerSellStoneRecordWindow()
        {
            InitializeComponent();
            GlobalData.Client.GetSellStonesOrderListCompleted += Client_GetSellStonesOrderListCompleted;
        }

        void Client_GetSellStonesOrderListCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<MetaData.Trade.SellStonesOrder[]> e)
        {
            throw new NotImplementedException();
        }

        public void SetUser(string seller)
        {
            this.Title += "  ----" + seller;
            GlobalData.Client.GetSellStonesOrderList(seller, new MyDateTime(), new MyDateTime());
        }
    }
}
