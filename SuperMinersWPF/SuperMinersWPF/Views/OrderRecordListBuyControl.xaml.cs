﻿using System;
using System.Collections;
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
    /// Interaction logic for OrderRecordListBuyControl.xaml
    /// </summary>
    public partial class OrderRecordListBuyControl : UserControl
    {
        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemSourceProperty); }
            set { SetValue(ItemSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(OrderRecordListBuyControl), new PropertyMetadata(null));
        

        public OrderRecordListBuyControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Binding bind = new Binding()
            {
                Source = this.ItemsSource
            };
            this.listboxBuyOrder.SetBinding(ListBox.ItemsSourceProperty, bind);
        }
    }
}
