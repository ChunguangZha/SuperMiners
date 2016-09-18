using SuperMinersCustomServiceSystem.Model;
using SuperMinersCustomServiceSystem.Uility;
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

namespace SuperMinersCustomServiceSystem.View.Windows
{
    /// <summary>
    /// Interaction logic for EditPlayerStoneWindow.xaml
    /// </summary>
    public partial class EditPlayerStoneWindow : Window
    {
        private PlayerInfoUIModel _player = null;

        public EditPlayerStoneWindow(PlayerInfoUIModel player)
        {
            InitializeComponent();
            this._player = player;

            this.txtCurrentFreezingStones.Text = _player.FreezingStones.ToString();
            this.numChangedFreezingStones.Value = (double)_player.FreezingStones;
            this.txtCurrentSellableStones.Text = _player.SellableStones.ToString();
            this.numChangedSellableStones.Value = (double)_player.SellableStones;
            this.txtCurrentStockOfStones.Text = _player.StockOfStones.ToString();
            this.numChangedStockOfStones.Value = (double)_player.StockOfStones;
            this.txtCurrentStonesReserves.Text = _player.StonesReserves.ToString();
            this.numChangedStonesReserves.Value = (double)_player.StonesReserves;

            this.numChangedStockOfStones.ValueChanged += numChangedStockOfStones_ValueChanged;
            this.numChangedFreezingStones.ValueChanged += numChangedFreezingStones_ValueChanged;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (this.numChangedStockOfStones.Value < this.numChangedFreezingStones.Value)
            {
                MyMessageBox.ShowInfo("冻结矿石量不能大于库存矿石量");
                return;
            }
            if (MyMessageBox.ShowQuestionOKCancel("请确认要修改玩家矿石信息") == System.Windows.Forms.DialogResult.OK)
            {
                this._player.SetStone((decimal)this.numChangedStonesReserves.Value, (decimal)this.numChangedStockOfStones.Value, (decimal)this.numChangedFreezingStones.Value);
            }
            this.DialogResult = true;
        }

        private void numChangedStockOfStones_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double sellableStones = this.numChangedStockOfStones.Value - this.numChangedFreezingStones.Value;
            this.numChangedSellableStones.Value = sellableStones;
        }

        private void numChangedFreezingStones_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double sellableStones = this.numChangedStockOfStones.Value - this.numChangedFreezingStones.Value;
            this.numChangedSellableStones.Value = sellableStones;
        }
    }
}
