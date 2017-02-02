using MetaData.Game.GambleStone;
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

namespace SuperMinersWPF.Views.Controls
{
    /// <summary>
    /// Interaction logic for GambleStoneControl.xaml
    /// </summary>
    public partial class GambleStoneControl : UserControl
    {
        public GambleStoneControl()
        {
            InitializeComponent();
            this.DataContext = App.GambleStoneVMObject;

        }

        private void btnBetRed_Click(object sender, RoutedEventArgs e)
        {
            BetIn(GambleStoneItemColor.Red);
        }

        private void btnBetGreen_Click(object sender, RoutedEventArgs e)
        {
            BetIn(GambleStoneItemColor.Green);
        }

        private void btnBetBlue_Click(object sender, RoutedEventArgs e)
        {
            BetIn(GambleStoneItemColor.Blue);
        }

        private void btnBetPurple_Click(object sender, RoutedEventArgs e)
        {
            BetIn(GambleStoneItemColor.Purple);
        }

        private void BetIn(GambleStoneItemColor itemColor)
        {
            int stoneCount = 0;
            int gravelCount = 0;
            //优先使用碎石
            if (GlobalData.CurrentUser.Gravel >= GlobalData.GameConfig.GambleStone_OneBetStoneCount)
            {
                gravelCount = GlobalData.GameConfig.GambleStone_OneBetStoneCount;
            }
            else
            {
                gravelCount = GlobalData.CurrentUser.Gravel;
            }
            stoneCount = GlobalData.GameConfig.GambleStone_OneBetStoneCount - gravelCount;

            App.GambleStoneVMObject.AsyncBetIn(itemColor, stoneCount, gravelCount);
        }
    }
}
