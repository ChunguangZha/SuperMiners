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

namespace SuperMinersCustomServiceSystem.View.Controls
{
    /// <summary>
    /// Interaction logic for NoticeManageControl.xaml
    /// </summary>
    public partial class NoticeManageControl : UserControl
    {
        public NoticeManageControl()
        {
            InitializeComponent();
            BindUI();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (App.NoticeVMObject == null || GlobalData.CurrentAdmin == null)
            {
                return;
            }

            App.NoticeVMObject.AsyncGetAllNotice();

            if (GlobalData.CurrentAdmin.GroupType != MetaData.User.AdminGroupType.CEO)
            {
                this.btnclearAllNotices.IsEnabled = false;
                this.btnCreateNotices.IsEnabled = false;
                this.btnDeleteNotices.IsEnabled = false;
            }
        }

        private void BindUI()
        {
            Binding bind = new Binding()
            {
                Source = App.NoticeVMObject.ListAllNotices
            };
            this.datagridNotices.SetBinding(DataGrid.ItemsSourceProperty, bind);
        }

        private void btnCreateNotices_Click(object sender, RoutedEventArgs e)
        {
            AddNoticeWindow win = new AddNoticeWindow();
            win.ShowDialog();
        }

        private void btnDeleteNotices_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnclearAllNotices_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSelectAllNotices_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
