using MetaData.Trade;
using SuperMinersWPF.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for OrderRecordListSellControl.xaml
    /// </summary>
    public partial class OrderRecordListSellControl : UserControl, INotifyPropertyChanged
    {
        private List<SellStonesOrderUIModel> _listSellStonesOrder;

        public List<SellStonesOrderUIModel> ListSellStonesOrder
        {
            get { return _listSellStonesOrder; }
            set
            {
                _listSellStonesOrder = value;

                SetItemsSource();
            }
        }

        private IEnumerable<SellStonesOrderUIModel> _itemsSource = new List<SellStonesOrderUIModel>();

        public IEnumerable<SellStonesOrderUIModel> ItemsSource
        {
            get { return _itemsSource; }
            set
            {
                _itemsSource = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ItemsSource"));
                    PropertyChanged(this, new PropertyChangedEventArgs("ItemsCount"));
                }
            }
        }

        public int ItemsCount
        {
            get
            {
                if (ItemsSource == null)
                {
                    return 0;
                }

                return ItemsSource.Count();
            }
        }

        public OrderRecordListSellControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.txtItemsCount != null)
            {
                Binding bind = new Binding()
                {
                    Source = this.ItemsSource
                };
                this.listboxSellOrder.SetBinding(ListBox.ItemsSourceProperty, bind);
                bind = new Binding()
                {
                    Source = this.ItemsCount
                };
                this.txtItemsCount.SetBinding(TextBlock.TextProperty, bind);
            }
        }

        private void cmbOrderState_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetItemsSource();
        }

        private void SetItemsSource()
        {
            if (cmbOrderState != null && this.ListSellStonesOrder != null)
            {
                if (this.cmbOrderState.SelectedIndex <= 0)
                {
                    ItemsSource = this.ListSellStonesOrder;
                }
                else
                {
                    //SellOrderState state = (SellOrderState)this.cmbOrderState.SelectedIndex;
                    ItemsSource = this.ListSellStonesOrder.Where(s => (int)s.OrderState == this.cmbOrderState.SelectedIndex);
                }

                //this.listboxSellOrder.ItemsSource = ItemsSource;
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
