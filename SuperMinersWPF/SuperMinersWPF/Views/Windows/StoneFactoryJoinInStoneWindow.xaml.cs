using MetaData.SystemConfig;
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
using System.Windows.Shapes;

namespace SuperMinersWPF.Views.Windows
{
    /// <summary>
    /// Interaction logic for StoneFactoryJoinInStoneWindow.xaml
    /// </summary>
    public partial class StoneFactoryJoinInStoneWindow : Window
    {
        public int JoinInStoneStackCount = 0;
        private decimal allStoneCount;
        private decimal maxJoinableStoneStackCount;

        public StoneFactoryJoinInStoneWindow(decimal allStoneCount)
        {
            InitializeComponent();
            this.allStoneCount = allStoneCount;
            this.txtTotalStone.Text = allStoneCount.ToString();
            maxJoinableStoneStackCount = Math.Floor(allStoneCount / StoneFactoryConfig.StoneFactoryStone_Stack);
            this.numAddToFactoryStoneCount.Maximum = (int)maxJoinableStoneStackCount;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (this.numAddToFactoryStoneCount.Value == 0)
            {
                MyMessageBox.ShowInfo("请输入要添加的矿石");
                return;
            }
            if ((decimal)this.numAddToFactoryStoneCount.Value * StoneFactoryConfig.StoneFactoryStone_Stack > allStoneCount)
            {
                MyMessageBox.ShowInfo("没有足够的矿石");
                return;
            }
            else
            {
                this.JoinInStoneStackCount = (int)this.numAddToFactoryStoneCount.Value;
            }
            this.DialogResult = true;
        }
    }
}
