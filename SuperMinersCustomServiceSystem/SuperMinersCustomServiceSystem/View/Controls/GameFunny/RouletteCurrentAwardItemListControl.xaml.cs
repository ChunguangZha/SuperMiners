using MetaData.Game.Roulette;
using SuperMinersCustomServiceSystem.Model;
using SuperMinersCustomServiceSystem.Uility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for RouletteCurrentAwardItemListControl.xaml
    /// </summary>
    public partial class RouletteCurrentAwardItemListControl : UserControl
    {
        public RouletteCurrentAwardItemListControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.cmb1 != null)
            {
                this.cmb1.ItemsSource = App.GameRouletteVMObject.ListAllRouletteAwardItems;
            }
            if (this.cmb2 != null)
            {
                this.cmb2.ItemsSource = App.GameRouletteVMObject.ListAllRouletteAwardItems;
            }
            if (this.cmb3 != null)
            {
                this.cmb3.ItemsSource = App.GameRouletteVMObject.ListAllRouletteAwardItems;
            }
            if (this.cmb4 != null)
            {
                this.cmb4.ItemsSource = App.GameRouletteVMObject.ListAllRouletteAwardItems;
            }
            if (this.cmb5 != null)
            {
                this.cmb5.ItemsSource = App.GameRouletteVMObject.ListAllRouletteAwardItems;
            }
            if (this.cmb6 != null)
            {
                this.cmb6.ItemsSource = App.GameRouletteVMObject.ListAllRouletteAwardItems;
            }
            if (this.cmb7 != null)
            {
                this.cmb7.ItemsSource = App.GameRouletteVMObject.ListAllRouletteAwardItems;
            }
            if (this.cmb8 != null)
            {
                this.cmb8.ItemsSource = App.GameRouletteVMObject.ListAllRouletteAwardItems;
            }
            if (this.cmb9 != null)
            {
                this.cmb9.ItemsSource = App.GameRouletteVMObject.ListAllRouletteAwardItems;
            }
            if (this.cmb10 != null)
            {
                this.cmb10.ItemsSource = App.GameRouletteVMObject.ListAllRouletteAwardItems;
            }
            if (this.cmb11 != null)
            {
                this.cmb11.ItemsSource = App.GameRouletteVMObject.ListAllRouletteAwardItems;
            }
            if (this.cmb12 != null)
            {
                this.cmb12.ItemsSource = App.GameRouletteVMObject.ListAllRouletteAwardItems;
            }
        }

        public void RegisterEvents()
        {
            App.GameRouletteVMObject.GetAllAwardItemCompleted += GameRouletteVMObject_GetAllAwardItemCompleted;
            App.GameRouletteVMObject.GetCurrentAwardItemCompleted += GameRouletteVMObject_GetCurrentAwardItemCompleted;
        }

        public void RemoveEvents()
        {
            App.GameRouletteVMObject.GetAllAwardItemCompleted -= GameRouletteVMObject_GetAllAwardItemCompleted;
            App.GameRouletteVMObject.GetCurrentAwardItemCompleted -= GameRouletteVMObject_GetCurrentAwardItemCompleted;
        }

        void GameRouletteVMObject_GetAllAwardItemCompleted()
        {
            SetComboxSelected();
        }

        void GameRouletteVMObject_GetCurrentAwardItemCompleted()
        {
            SetComboxSelected();
        }

        private void SetComboxSelected()
        {
            if (App.GameRouletteVMObject.ListCurrentRouletteAwardItems != null && App.GameRouletteVMObject.ListCurrentRouletteAwardItems.Count == 12)
            {
                this.cmb1.SelectedValue = App.GameRouletteVMObject.ListCurrentRouletteAwardItems[0].ID;
                this.cmb2.SelectedValue = App.GameRouletteVMObject.ListCurrentRouletteAwardItems[1].ID;
                this.cmb3.SelectedValue = App.GameRouletteVMObject.ListCurrentRouletteAwardItems[2].ID;
                this.cmb4.SelectedValue = App.GameRouletteVMObject.ListCurrentRouletteAwardItems[3].ID;
                this.cmb5.SelectedValue = App.GameRouletteVMObject.ListCurrentRouletteAwardItems[4].ID;
                this.cmb6.SelectedValue = App.GameRouletteVMObject.ListCurrentRouletteAwardItems[5].ID;
                this.cmb7.SelectedValue = App.GameRouletteVMObject.ListCurrentRouletteAwardItems[6].ID;
                this.cmb8.SelectedValue = App.GameRouletteVMObject.ListCurrentRouletteAwardItems[7].ID;
                this.cmb9.SelectedValue = App.GameRouletteVMObject.ListCurrentRouletteAwardItems[8].ID;
                this.cmb10.SelectedValue = App.GameRouletteVMObject.ListCurrentRouletteAwardItems[9].ID;
                this.cmb11.SelectedValue = App.GameRouletteVMObject.ListCurrentRouletteAwardItems[10].ID;
                this.cmb12.SelectedValue = App.GameRouletteVMObject.ListCurrentRouletteAwardItems[11].ID;

                this.numWinProbability1.Value = App.GameRouletteVMObject.ListCurrentRouletteAwardItems[0].WinProbability;
                this.numWinProbability2.Value = App.GameRouletteVMObject.ListCurrentRouletteAwardItems[1].WinProbability;
                this.numWinProbability3.Value = App.GameRouletteVMObject.ListCurrentRouletteAwardItems[2].WinProbability;
                this.numWinProbability4.Value = App.GameRouletteVMObject.ListCurrentRouletteAwardItems[3].WinProbability;
                this.numWinProbability5.Value = App.GameRouletteVMObject.ListCurrentRouletteAwardItems[4].WinProbability;
                this.numWinProbability6.Value = App.GameRouletteVMObject.ListCurrentRouletteAwardItems[5].WinProbability;
                this.numWinProbability7.Value = App.GameRouletteVMObject.ListCurrentRouletteAwardItems[6].WinProbability;
                this.numWinProbability8.Value = App.GameRouletteVMObject.ListCurrentRouletteAwardItems[7].WinProbability;
                this.numWinProbability9.Value = App.GameRouletteVMObject.ListCurrentRouletteAwardItems[8].WinProbability;
                this.numWinProbability10.Value = App.GameRouletteVMObject.ListCurrentRouletteAwardItems[9].WinProbability;
                this.numWinProbability11.Value = App.GameRouletteVMObject.ListCurrentRouletteAwardItems[10].WinProbability;
                this.numWinProbability12.Value = App.GameRouletteVMObject.ListCurrentRouletteAwardItems[11].WinProbability;
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void Refresh()
        {
            App.GameRouletteVMObject.AsyncGetAllAwardItems();
            App.GameRouletteVMObject.AsyncGetCurrentAwardItems();
        }

        private void btnSaveAllAwardItems_Click(object sender, RoutedEventArgs e)
        {
            RouletteAwardItem[] items = new RouletteAwardItem[12];
            RouletteAwardItemUIModel item1 = this.cmb1.SelectedItem as RouletteAwardItemUIModel;
            if (item1 == null)
            {
                MyMessageBox.ShowInfo("请设置第1项");
                return;
            }
            item1.WinProbability = (float)this.numWinProbability1.Value;
            items[0] = item1.ParentObject;

            RouletteAwardItemUIModel item2 = this.cmb2.SelectedItem as RouletteAwardItemUIModel;
            if (item2 == null)
            {
                MyMessageBox.ShowInfo("请设置第2项");
                return;
            }
            item2.WinProbability = (float)this.numWinProbability2.Value;
            items[1] = item2.ParentObject;

            RouletteAwardItemUIModel item3 = this.cmb3.SelectedItem as RouletteAwardItemUIModel;
            if (item3 == null)
            {
                MyMessageBox.ShowInfo("请设置第3项");
                return;
            }
            item3.WinProbability = (float)this.numWinProbability3.Value;
            items[2] = item3.ParentObject;

            RouletteAwardItemUIModel item4 = this.cmb4.SelectedItem as RouletteAwardItemUIModel;
            if (item4 == null)
            {
                MyMessageBox.ShowInfo("请设置第4项");
                return;
            }
            item4.WinProbability = (float)this.numWinProbability4.Value;
            items[3] = item4.ParentObject;

            RouletteAwardItemUIModel item5 = this.cmb5.SelectedItem as RouletteAwardItemUIModel;
            if (item5 == null)
            {
                MyMessageBox.ShowInfo("请设置第5项");
                return;
            }
            item5.WinProbability = (float)this.numWinProbability5.Value;
            items[4] = item5.ParentObject;

            RouletteAwardItemUIModel item6 = this.cmb6.SelectedItem as RouletteAwardItemUIModel;
            if (item6 == null)
            {
                MyMessageBox.ShowInfo("请设置第6项");
                return;
            }
            item6.WinProbability = (float)this.numWinProbability6.Value;
            items[5] = item6.ParentObject;

            RouletteAwardItemUIModel item7 = this.cmb7.SelectedItem as RouletteAwardItemUIModel;
            if (item7 == null)
            {
                MyMessageBox.ShowInfo("请设置第7项");
                return;
            }
            item7.WinProbability = (float)this.numWinProbability7.Value;
            items[6] = item7.ParentObject;

            RouletteAwardItemUIModel item8 = this.cmb8.SelectedItem as RouletteAwardItemUIModel;
            if (item8 == null)
            {
                MyMessageBox.ShowInfo("请设置第8项");
                return;
            }
            item8.WinProbability = (float)this.numWinProbability8.Value;
            items[7] = item8.ParentObject;

            RouletteAwardItemUIModel item9 = this.cmb9.SelectedItem as RouletteAwardItemUIModel;
            if (item9 == null)
            {
                MyMessageBox.ShowInfo("请设置第9项");
                return;
            }
            item9.WinProbability = (float)this.numWinProbability9.Value;
            items[8] = item9.ParentObject;

            RouletteAwardItemUIModel item10 = this.cmb10.SelectedItem as RouletteAwardItemUIModel;
            if (item10 == null)
            {
                MyMessageBox.ShowInfo("请设置第10项");
                return;
            }
            item10.WinProbability = (float)this.numWinProbability10.Value;
            items[9] = item10.ParentObject;

            RouletteAwardItemUIModel item11 = this.cmb11.SelectedItem as RouletteAwardItemUIModel;
            if (item11 == null)
            {
                MyMessageBox.ShowInfo("请设置第11项");
                return;
            }
            item11.WinProbability = (float)this.numWinProbability11.Value;
            items[10] = item11.ParentObject;

            RouletteAwardItemUIModel item12 = this.cmb12.SelectedItem as RouletteAwardItemUIModel;
            if (item12 == null)
            {
                MyMessageBox.ShowInfo("请设置第12项");
                return;
            }
            item12.WinProbability = (float)this.numWinProbability12.Value;
            items[11] = item12.ParentObject;

            App.GameRouletteVMObject.AsyncSetCurrentAwardItem(items);
        }
    }
}
