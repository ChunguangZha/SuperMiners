using MetaData.Game.Roulette;
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

namespace SuperMinersWPF.Views.Windows
{
    /// <summary>
    /// Interaction logic for RouletteWinAwardAlertWindow.xaml
    /// </summary>
    public partial class RouletteWinAwardAlertWindow : Window
    {
        RouletteWinnerRecord _record = null;
        public RouletteWinAwardAlertWindow(RouletteWinnerRecord record)
        {
            InitializeComponent();
            this._record = record;
            this.txtAwardName.Text = this._record.AwardItem.AwardName;
            if (this._record.AwardItem.IsRealAward)
            {
                this.btnTakeAward.Visibility = System.Windows.Visibility.Visible;
                this.txtMessage.Visibility = System.Windows.Visibility.Collapsed;
                this.btnClose.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                this.btnTakeAward.Visibility = System.Windows.Visibility.Collapsed;
                this.txtMessage.Visibility = System.Windows.Visibility.Visible;
                this.btnClose.Visibility = System.Windows.Visibility.Visible;
                this.txtMessage.Text = "奖品已经添加到您的账户中，敬请查收。";
            }
        }

        private void btnTakeAward_Click(object sender, RoutedEventArgs e)
        {
            RouletteWinAwardTakeWindow win = new RouletteWinAwardTakeWindow(this._record);
            if (win.ShowDialog() == true)
            {
                this.DialogResult = true;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
