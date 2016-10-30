using SuperMinersCustomServiceSystem.Model;
using SuperMinersCustomServiceSystem.Uility;
using SuperMinersCustomServiceSystem.View.Windows;
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

namespace SuperMinersCustomServiceSystem.View.Controls.GameFunny
{
    /// <summary>
    /// Interaction logic for RouletteAwardItemListControl.xaml
    /// </summary>
    public partial class RouletteAwardItemListControl : UserControl
    {
        public RouletteAwardItemListControl()
        {
            InitializeComponent();
            this.dgRecords.ItemsSource = App.GameRouletteVMObject.ListAllRouletteAwardItems;
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            App.GameRouletteVMObject.AsyncGetCurrentAwardItems();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            RouletteAwardItemUIModel awarditem = btn.DataContext as RouletteAwardItemUIModel;
            if (awarditem != null)
            {
                EditRouletteAwardItemWindow win = new EditRouletteAwardItemWindow(awarditem);
                win.ShowDialog();
                if (win.IsOK)
                {
                    App.GameRouletteVMObject.AsyncGetAllAwardItems();
                }
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            EditRouletteAwardItemWindow win = new EditRouletteAwardItemWindow(App.GameRouletteVMObject.ListAllRouletteAwardItems.Count);
            win.ShowDialog();
            if (win.IsOK)
            {
                App.GameRouletteVMObject.AsyncGetAllAwardItems();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MyMessageBox.ShowQuestionOKCancel("请确认要删除该奖项？如果该奖项被使用则会删除失败。") == System.Windows.Forms.DialogResult.OK)
            {
                Button btn = sender as Button;
                RouletteAwardItemUIModel awarditem = btn.DataContext as RouletteAwardItemUIModel;
                if (awarditem != null)
                {
                    App.GameRouletteVMObject.AsyncDeleteAwardItem(awarditem.ParentObject);
                }
            }
        }
    }
}
