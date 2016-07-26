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

namespace SuperMinersWPF.Views
{
    /// <summary>
    /// Interaction logic for ListAllNoticesWindow.xaml
    /// </summary>
    public partial class ListAllNoticesWindow : Window
    {
        public ListAllNoticesWindow()
        {
            InitializeComponent();
            BindUI();
        }

        private void BindUI()
        {
            Binding bind = new Binding()
            {
                Source = App.NoticeVMObject.ListNotices
            };
            this.listboxAllNotices.SetBinding(ListBox.ItemsSourceProperty, bind);
        }

        public void SetCurrentNotice(NoticeInfo notice)
        {
            if (notice == null)
            {
                return;
            }
            this.listboxAllNotices.Visibility = System.Windows.Visibility.Collapsed;
            this.panelViewSingleNotice.Visibility = System.Windows.Visibility.Visible;
            this.panelViewSingleNotice.DataContext = notice;
            this.btnBack.Visibility = System.Windows.Visibility.Visible;
            this.txtTitle.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void ListBoxItem_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            ListBoxItem item = sender as ListBoxItem;
            NoticeInfo notice = item.DataContext as NoticeInfo;
            SetCurrentNotice(notice);
        }  

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.listboxAllNotices.Visibility = System.Windows.Visibility.Visible;
            this.panelViewSingleNotice.Visibility = System.Windows.Visibility.Collapsed;
            this.btnBack.Visibility = System.Windows.Visibility.Collapsed;
            this.txtTitle.Visibility = System.Windows.Visibility.Visible;
        }
    }
}
