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

        BitmapImage bmpRed = new BitmapImage(new Uri(@"../../Resources/gamblered.png", UriKind.Relative));
        BitmapImage bmpGreen = new BitmapImage(new Uri(@"../../Resources/gamblegreen.png", UriKind.Relative));
        BitmapImage bmpBlue = new BitmapImage(new Uri(@"../../Resources/gambleblue.png", UriKind.Relative));
        BitmapImage bmpPurple = new BitmapImage(new Uri(@"../../Resources/gambleyellow.png", UriKind.Relative));

        private Image[] _WinnedColorItem = new Image[64];
        private bool gridImagesCreated = false;

        public GambleStoneControl()
        {
            InitializeComponent();

            _syn = SynchronizationContext.Current;
            this.DataContext = App.GambleStoneVMObject;

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            CreateImagesControl();
        }

        private void CreateImagesControl()
        {
            if (gridImagesCreated)
            {
                return;
            }

            gridImagesCreated = true;
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
                Panel.SetZIndex(imgControl, 10);

                imgControl.RenderTransformOrigin = new Point(0.5, 0.5);

                TransformGroup transGroup = new TransformGroup();
                transGroup.Children.Add(new ScaleTransform(1,1));
                transGroup.Children.Add(new SkewTransform());
                transGroup.Children.Add(new RotateTransform());
                transGroup.Children.Add(new TranslateTransform(0,0));
                imgControl.RenderTransform = transGroup;

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

            CreateMoveWinnedColorItem();
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
                            //bmp = new BitmapImage(new Uri(@"../../Resources/gamblered.png", UriKind.Relative));
                            bmp = bmpRed;
                            break;
                        case GambleStoneItemColor.Green:
                            //bmp = new BitmapImage(new Uri(@"../../Resources/gamblegreen.png", UriKind.Relative));
                            bmp = bmpGreen;
                            break;
                        case GambleStoneItemColor.Blue:
                            //bmp = new BitmapImage(new Uri(@"../../Resources/gambleblue.png", UriKind.Relative));
                            bmp = bmpBlue;
                            break;
                        case GambleStoneItemColor.Purple:
                            //bmp = new BitmapImage(new Uri(@"../../Resources/gambleyellow.png", UriKind.Relative));
                            bmp = bmpPurple;
                            break;
                        default:
                            break;
                    }
                    this._WinnedColorItem[i].Source = bmp;
                    if (bmp == null)
                    {
                        this._WinnedColorItem[i].Visibility = System.Windows.Visibility.Collapsed;
                    }
                    else
                    {
                        this._WinnedColorItem[i].Visibility = System.Windows.Visibility.Visible;
                    }
                }

                GC.Collect();
            }, null);
        }
        
        void GambleStoneVMObject_GambleStoneInningFinished(GambleStoneInningInfo inningInfo, GambleStonePlayerBetRecord maxWinner)
        {
            _syn.Post(o =>
            {
                if (maxWinner == null || string.IsNullOrEmpty(maxWinner.UserName) || maxWinner.WinnedStone == 0)
                {
                    this.txtWinnerNotice.Text = "";
                }
                else
                {
                    this.txtWinnerNotice.Text = maxWinner.UserName + " 赢得 " + maxWinner.WinnedStone + " 矿石";
                }

                if (inningInfo != null && inningInfo.InningIndex > 0 && inningInfo.InningIndex <= 64)
                {
                    BitmapImage bmp = null;
                    switch (inningInfo.WinnedColor)
                    {
                        case GambleStoneItemColor.Red:
                            //bmp = new BitmapImage(new Uri(@"../../Resources/gamblered.png", UriKind.Relative));
                            bmp = bmpRed;
                            break;
                        case GambleStoneItemColor.Green:
                            //bmp = new BitmapImage(new Uri(@"../../Resources/gamblegreen.png", UriKind.Relative));
                            bmp = bmpGreen;
                            break;
                        case GambleStoneItemColor.Blue:
                            //bmp = new BitmapImage(new Uri(@"../../Resources/gambleblue.png", UriKind.Relative));
                            bmp = bmpBlue;
                            break;
                        case GambleStoneItemColor.Purple:
                            //bmp = new BitmapImage(new Uri(@"../../Resources/gambleyellow.png", UriKind.Relative));
                            bmp = bmpPurple;
                            break;
                        default:
                            break;
                    }
                    
                    //this.imgCurrentWinnedColor.Source = bmp;
                    this.imgSplitStone1.Visibility = System.Windows.Visibility.Visible;
                    this.imgSplitStone2.Visibility = System.Windows.Visibility.Visible;
                    this.imgSplitStone3.Visibility = System.Windows.Visibility.Visible;
                    this.imgSplitStone4.Visibility = System.Windows.Visibility.Visible;

                    SetNewValueToStoryboard(inningInfo.InningIndex - 1, bmp);
                    Storyboard StoryboardOpenGamble = this.FindResource("StoryboardOpenGamble") as Storyboard;
                    if (StoryboardOpenGamble != null)
                    {
                        StoryboardOpenGamble.Begin();
                    }
                }
            }, null);
        }

        private int _destImgLocationX = 42 * 4 / 2;
        private int _destImgLocationY = (42 * 4 - 16) / 2;
        Storyboard boardMoveWinnedItem = new Storyboard();
        DoubleAnimationUsingKeyFrames keyFrameTransX = new DoubleAnimationUsingKeyFrames();
        DoubleAnimationUsingKeyFrames keyFrameTransY = new DoubleAnimationUsingKeyFrames();
        DoubleAnimationUsingKeyFrames keyFrameScaleX = new DoubleAnimationUsingKeyFrames();
        DoubleAnimationUsingKeyFrames keyFrameScaleY = new DoubleAnimationUsingKeyFrames();

        private void SetNewValueToStoryboard(int index, BitmapImage bmp)
        {
            int currentXPixel = 0;
            int currentYPixel = 0;
            int xIndex = 0;
            int yIndex = 0;
            if (index < 24)
            {
                yIndex = index / 6;
                xIndex = index % 6 + 4;
            }
            else
            {
                yIndex = (index - 24) / 10 + 4;
                xIndex = (index - 24) % 10;
            }
            currentXPixel = xIndex * 42 + 21;
            currentYPixel = yIndex * 42 + 21;

            int offsetX = _destImgLocationX - currentXPixel;
            int offsetY = _destImgLocationY - currentYPixel;

            Console.WriteLine("index = " + index + "; xIndex = " + xIndex + "; yIndex = " + yIndex + "; currentXPixel = " + currentXPixel + "; currentYPixel = " + currentYPixel + "; offsetX = " + offsetX + "; offsetY = " + offsetY);


            Storyboard.SetTarget(keyFrameTransX, this._WinnedColorItem[index]);
            keyFrameTransX.KeyFrames[0].Value = offsetX;
            keyFrameTransX.KeyFrames[1].Value = offsetX;

            Storyboard.SetTarget(keyFrameTransY, this._WinnedColorItem[index]);
            keyFrameTransY.KeyFrames[0].Value = offsetY;
            keyFrameTransY.KeyFrames[1].Value = offsetY;

            Storyboard.SetTarget(keyFrameScaleX, this._WinnedColorItem[index]);
            Storyboard.SetTarget(keyFrameScaleY, this._WinnedColorItem[index]);

            this._WinnedColorItem[index].Source = bmp;
            if (bmp == null)
            {
                this._WinnedColorItem[index].Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                this._WinnedColorItem[index].Visibility = System.Windows.Visibility.Visible;
            }
            boardMoveWinnedItem.Begin();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index">从0开始</param>
        /// <returns></returns>
        private Storyboard CreateMoveWinnedColorItem()
        {
            //Storyboard.SetTarget(keyFrameTransX, this._WinnedColorItem[index]);
            Storyboard.SetTargetProperty(keyFrameTransX, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.X)"));
            keyFrameTransX.KeyFrames.Add(new EasingDoubleKeyFrame()
            {
                KeyTime = TimeSpan.FromSeconds(0),
                Value = 0
            });
            keyFrameTransX.KeyFrames.Add(new EasingDoubleKeyFrame()
            {
                KeyTime = TimeSpan.FromSeconds(4),
                Value = 0
            });
            keyFrameTransX.KeyFrames.Add(new EasingDoubleKeyFrame()
            {
                KeyTime = TimeSpan.FromSeconds(5),
                Value = 0
            });
            boardMoveWinnedItem.Children.Add(keyFrameTransX);

            Storyboard.SetTargetProperty(keyFrameTransY, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.Y)"));
            keyFrameTransY.KeyFrames.Add(new EasingDoubleKeyFrame()
            {
                KeyTime = TimeSpan.FromSeconds(0),
                Value = 0
            });
            keyFrameTransY.KeyFrames.Add(new EasingDoubleKeyFrame()
            {
                KeyTime = TimeSpan.FromSeconds(4),
                Value = 0
            });
            keyFrameTransY.KeyFrames.Add(new EasingDoubleKeyFrame()
            {
                KeyTime = TimeSpan.FromSeconds(5),
                Value = 0
            });
            boardMoveWinnedItem.Children.Add(keyFrameTransY);
            
            Storyboard.SetTargetProperty(keyFrameScaleX, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)"));
            keyFrameScaleX.KeyFrames.Add(new EasingDoubleKeyFrame()
            {
                KeyTime = TimeSpan.FromSeconds(0),
                Value = 3
            });
            keyFrameScaleX.KeyFrames.Add(new EasingDoubleKeyFrame()
            {
                KeyTime = TimeSpan.FromSeconds(4),
                Value = 3
            });
            keyFrameScaleX.KeyFrames.Add(new EasingDoubleKeyFrame()
            {
                KeyTime = TimeSpan.FromSeconds(5),
                Value = 1
            });
            boardMoveWinnedItem.Children.Add(keyFrameScaleX);

            Storyboard.SetTargetProperty(keyFrameScaleY, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)"));
            keyFrameScaleY.KeyFrames.Add(new EasingDoubleKeyFrame()
            {
                KeyTime = TimeSpan.FromSeconds(0),
                Value = 3
            });
            keyFrameScaleY.KeyFrames.Add(new EasingDoubleKeyFrame()
            {
                KeyTime = TimeSpan.FromSeconds(4),
                Value = 3
            });
            keyFrameScaleY.KeyFrames.Add(new EasingDoubleKeyFrame()
            {
                KeyTime = TimeSpan.FromSeconds(5),
                Value = 1
            });
            boardMoveWinnedItem.Children.Add(keyFrameScaleY);

            return boardMoveWinnedItem;
        }

        private void Storyboard_Completed(object sender, EventArgs e)
        {
            this.imgSplitStone1.Visibility = System.Windows.Visibility.Collapsed;
            this.imgSplitStone2.Visibility = System.Windows.Visibility.Collapsed;
            this.imgSplitStone3.Visibility = System.Windows.Visibility.Collapsed;
            this.imgSplitStone4.Visibility = System.Windows.Visibility.Collapsed;
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
            if (chk10Stones.IsChecked == true)
            {
                allStone = 10;
            }
            else if (chk100Stones.IsChecked == true)
            {
                allStone = 100;
            }
            else if (chk1000Stones.IsChecked == true)
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

        private void btnViewMyBetRecord_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
