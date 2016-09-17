using SuperMinersCustomServiceSystem.Model;
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
    /// Interaction logic for OrderRecordListBuyControl.xaml
    /// </summary>
    public partial class OrderRecordListBuyControl : UserControl
    {
        private List<BuyStonesOrderUIModel> _listBuyStonesOrder = new List<BuyStonesOrderUIModel>();

        public List<BuyStonesOrderUIModel> ListBuyStonesOrder
        {
            get { return _listBuyStonesOrder; }
            set
            {
                _listBuyStonesOrder = value;
                this.listboxBuyOrder.ItemsSource = ListBuyStonesOrder;

                int itemsCount = 0;
                if (ListBuyStonesOrder != null)
                {
                    itemsCount = ListBuyStonesOrder.Count;
                }
                this.txtItemsCount.Text = itemsCount.ToString();
            }
        }

        public int ItemsCount
        {
            get
            {
                if (ListBuyStonesOrder == null)
                {
                    return 0;
                }

                return ListBuyStonesOrder.Count;
            }
        }

        public OrderRecordListBuyControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //Binding bind = new Binding()
            //{
            //    Source = this.ListBuyStonesOrder
            //};
            //this.listboxBuyOrder.SetBinding(ListBox.ItemsSourceProperty, bind);
            //bind = new Binding()
            //{
            //    Source = this.ItemsCount
            //};
            //this.txtItemsCount.SetBinding(TextBlock.TextProperty, bind);
        }

        //#region INotifyPropertyChanged Members

        //public event PropertyChangedEventHandler PropertyChanged;

        //#endregion
    }
}
