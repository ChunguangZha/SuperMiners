using SuperMinersWPF.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SuperMinersWPF.Views
{
    /// <summary>
    /// Interaction logic for StonesMarketUserControl.xaml
    /// </summary>
    public partial class StonesMarketUserControl : UserControl
    {
        private System.Threading.SynchronizationContext _syn;

        public StonesMarketUserControl()
        {
            InitializeComponent();
            _syn = System.Threading.SynchronizationContext.Current;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.listboxSellOrders.ItemsSource = App.StoneOrderVMObject.AllNotFinishStonesOrder;
            App.StoneOrderVMObject.AsyncGetOrderLockedBySelf();
            App.StoneOrderVMObject.AsyncGetAllNotFinishedSellOrders();
            App.StoneOrderVMObject.LockOrderSucceed += StoneOrderVMObject_LockOrderSucceed;
            
            Binding bind = new Binding()
            {
                Source = App.StoneOrderVMObject
            };
            this.expBuyOrders.SetBinding(Expander.DataContextProperty, bind);
        }

        void StoneOrderVMObject_LockOrderSucceed(LockSellStonesOrderUIModel obj)
        {
            this._syn.Post(o =>
            {
                BuyStonesWindow win = new BuyStonesWindow(obj);
                if (win.ShowDialog() == true)
                {

                }
                else
                {

                }
            }, null);
        }

        private void numBuyStones_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void btnAutoMatchOrder_Click(object sender, RoutedEventArgs e)
        {
            App.StoneOrderVMObject.AsyncAutoMatchStonesOrder((int)this.numBuyStones.Value);
        }

        private void btnPay_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAppeal_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
