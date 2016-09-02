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
    /// Interaction logic for TopListUserControl.xaml
    /// </summary>
    public partial class TopListUserControl : UserControl
    {
        public TopListUserControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (!GlobalData.IsLogined)
            {
                return;
            }

            if (datagridTopList != null)
            {
                datagridTopList.Columns[2].Header = "贡献值";
                datagridTopList.ItemsSource = App.TopListVMObject.ListExpTopList;
                if (App.TopListVMObject.ListExpTopList == null || App.TopListVMObject.ListExpTopList.Count == 0)
                {
                    App.TopListVMObject.AsyncGetExpTopList();
                }
            }
        }

        private void rbtnExpTopList_Checked(object sender, RoutedEventArgs e)
        {
            if (datagridTopList == null)
            {
                return;
            }

            datagridTopList.Columns[2].Header = "贡献值";
            datagridTopList.ItemsSource = App.TopListVMObject.ListExpTopList;
            if (App.TopListVMObject.ListExpTopList == null || App.TopListVMObject.ListExpTopList.Count == 0)
            {
                App.TopListVMObject.AsyncGetExpTopList();
            }
        }

        private void rbtnBuyTopList_Checked(object sender, RoutedEventArgs e)
        {
            if (datagridTopList == null)
            {
                return;
            }

            datagridTopList.Columns[2].Header = "交易量";
            datagridTopList.ItemsSource = null;
        }

        private void rbtnRefrerTopList_Checked(object sender, RoutedEventArgs e)
        {
            if (datagridTopList == null)
            {
                return;
            }

            datagridTopList.Columns[2].Header = "推荐人数";
            datagridTopList.ItemsSource = App.TopListVMObject.ListReferrerCountTopList;
            if (App.TopListVMObject.ListReferrerCountTopList == null || App.TopListVMObject.ListReferrerCountTopList.Count == 0)
            {
                App.TopListVMObject.AsyncGetReferrerTopList();
            }
        }

        private void rbtnMinersTopList_Checked(object sender, RoutedEventArgs e)
        {
            if (datagridTopList == null)
            {
                return;
            }

            datagridTopList.Columns[2].Header = "矿工数";
            datagridTopList.ItemsSource = App.TopListVMObject.ListMinerTopList;
            if (App.TopListVMObject.ListMinerTopList == null || App.TopListVMObject.ListMinerTopList.Count == 0)
            {
                App.TopListVMObject.AsyncGetMinerTopList();
            }
        }
    }
}
