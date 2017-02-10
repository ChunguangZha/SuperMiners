using MetaData.Game.GambleStone;
using SuperMinersWPF.Utility;
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
using System.Windows.Media.Animation;
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
        private System.Threading.SynchronizationContext _syn;

        private Image[] _WinnedColorItem = new Image[64];

        public GambleStoneControl()
        {
            InitializeComponent();

            _syn = SynchronizationContext.Current;
            this.DataContext = App.GambleStoneVMObject;

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            CreateImagesControl();
            CreateSrcStoneDisplayStory();
            this.storySrcStoneDisplay.Completed += storySrcStoneDisplay_Completed;
        }

        private void CreateImagesControl()
        {
            int row = 0;
            int col = 4;
            for (int i = 0; i < this._WinnedColorItem.Length; i++)
            {
                Image imgControl = new Image();
                imgControl.Margin = new Thickness(5);
                imgControl.Stretch = Stretch.Uniform;
                gridWinnedColor.Children.Add(imgControl);
                Grid.SetColumn(imgControl, col++);
                Grid.SetRow(imgControl, row);

                if (col > 9)
                {
                    row++;
                    if (row < 4)
                    {
                        col = 4;
                    }
                    else
                    {
                        col = 0;
                    }
                }

                this._WinnedColorItem[i] = imgControl;
            }
        }

        public void AddEventHandlers()
        {
            App.GambleStoneVMObject.GambleStoneInningFinished += GambleStoneVMObject_GambleStoneInningFinished;
            App.GambleStoneVMObject.GambleStoneGetRoundInfoEvent += GambleStoneVMObject_GambleStoneGetRoundInfoEvent;
        }

        void GambleStoneVMObject_GambleStoneGetRoundInfoEvent(GambleStoneRoundInfo obj)
        {
            _syn.Post(o =>
            {
                for (int i = 0; i < obj.WinColorItems.Length; i++)
                {
                    BitmapImage bmp = null;
                    switch ((GambleStoneItemColor)obj.WinColorItems[i])
                    {
                        case GambleStoneItemColor.Red:
                            bmp = new BitmapImage(new Uri(@"../../Resources/gamblered.png", UriKind.Relative));
                            break;
                        case GambleStoneItemColor.Green:
                            bmp = new BitmapImage(new Uri(@"../../Resources/gamblegreen.png", UriKind.Relative));
                            break;
                        case GambleStoneItemColor.Blue:
                            bmp = new BitmapImage(new Uri(@"../../Resources/gambleblue.png", UriKind.Relative));
                            break;
                        case GambleStoneItemColor.Purple:
                            bmp = new BitmapImage(new Uri(@"../../Resources/gambleyellow.png", UriKind.Relative));
                            break;
                        default:
                            bmp = new BitmapImage();
                            break;
                    }
                    this._WinnedColorItem[i].Source = bmp;
                }

                GC.Collect();
            }, null);
        }

        private Storyboard storySrcStoneDisplay = new Storyboard();

        private void CreateSrcStoneDisplayStory()
        {
            storySrcStoneDisplay.AutoReverse = true;

            DoubleAnimation myDoubleAnimation = new DoubleAnimation();
            myDoubleAnimation.From = 1;
            myDoubleAnimation.To = 0;
            myDoubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(2));
            storySrcStoneDisplay.Children.Add(myDoubleAnimation);
            Storyboard.SetTarget(myDoubleAnimation, this.imgSrcStone);
            Storyboard.SetTargetProperty(myDoubleAnimation, new PropertyPath(Image.OpacityProperty));
        }

        void storySrcStoneDisplay_Completed(object sender, EventArgs e)
        {
            this.imgCurrentWinnedColor.Source = null;
        }

        void GambleStoneVMObject_GambleStoneInningFinished(GambleStoneInningInfo inningInfo, GambleStonePlayerBetRecord maxWinner)
        {
            _syn.Post(o =>
            {
                if (maxWinner != null || !string.IsNullOrEmpty(maxWinner.UserName))
                {
                    this.txtWinnerNotice.Text = maxWinner.UserName + " 赢得 " + maxWinner.WinnedStone + " 矿石";
                }

                if (inningInfo != null && inningInfo.InningIndex > 0 && inningInfo.InningIndex <= 64)
                {
                    BitmapImage bmp = null;
                    switch (inningInfo.WinnedColor)
                    {
                        case GambleStoneItemColor.Red:
                            bmp = new BitmapImage(new Uri(@"../../Resources/gamblered.png", UriKind.Relative));
                            break;
                        case GambleStoneItemColor.Green:
                            bmp = new BitmapImage(new Uri(@"../../Resources/gamblegreen.png", UriKind.Relative));
                            break;
                        case GambleStoneItemColor.Blue:
                            bmp = new BitmapImage(new Uri(@"../../Resources/gambleblue.png", UriKind.Relative));
                            break;
                        case GambleStoneItemColor.Purple:
                            bmp = new BitmapImage(new Uri(@"../../Resources/gambleyellow.png", UriKind.Relative));
                            break;
                        default:
                            bmp = new BitmapImage();
                            break;
                    }

                    if (inningInfo.InningIndex > 1)
                    {
                        this._WinnedColorItem[inningInfo.InningIndex - 1].Source = null;
                    }
                    this._WinnedColorItem[inningInfo.InningIndex - 1].Source = bmp;
                    this.imgCurrentWinnedColor.Source = bmp;
                    storySrcStoneDisplay.Begin();
                }
            }, null);
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

            int allStone = 10;
            if (chk1Times.IsChecked == true)
            {
                allStone = 10;
            }
            else if (chk10Times.IsChecked == true)
            {
                allStone = 100;
            }
            else if (chk100Times.IsChecked == true)
            {
                allStone = 1000;
            }

            //优先使用碎片
            if (GlobalData.CurrentUser.Gravel >= allStone)
            {
                gravelCount = allStone;
            }
            else
            {
                gravelCount = GlobalData.CurrentUser.Gravel;
            }
            stoneCount = allStone - gravelCount;
            if (stoneCount > GlobalData.CurrentUser.SellableStones)
            {
                MyMessageBox.ShowInfo("没有足够的矿石");
                return;
            }

            App.GambleStoneVMObject.AsyncBetIn(itemColor, stoneCount, gravelCount);
        }
    }
}
