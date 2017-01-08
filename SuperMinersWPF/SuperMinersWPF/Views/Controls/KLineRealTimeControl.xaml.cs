using MetaData.Game.StoneStack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace SuperMinersWPF.Views.Controls
{
    /// <summary>
    /// Interaction logic for KLineControl.xaml
    /// </summary>
    public partial class KLineRealTimeControl : UserControl
    {

        private SolidColorBrush _baseLineBrush = new SolidColorBrush(Color.FromArgb(0xff, 0x32, 0x32, 0x32));

        /// <summary>
        /// 每分钟一条记录
        /// </summary>
        private ObservableCollection<StoneStackDailyRecordInfo> _listTodayMinuteTradeRecords = new ObservableCollection<StoneStackDailyRecordInfo>();

        private Polyline polyLine = new Polyline();
        private double _maxRangeValue = 1;
        private bool _needRendAll = false;
        private bool _addItem = true;

        double startY = 0;
        double yOffsetUnit = 0;
        double xOffsetUnit = 0;
        private int marketOpeningHours = 0;
        private int marketOpeningMinutes = 0;


        private System.Threading.SynchronizationContext _syn;

        private SolidColorBrush _redBrush = new SolidColorBrush(Colors.Red);
        private SolidColorBrush _greenBrush = new SolidColorBrush(Colors.Green);

        public KLineRealTimeControl()
        {
            InitializeComponent();

            polyLine.Stroke = new SolidColorBrush(Colors.White);
            _syn = SynchronizationContext.Current;

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            App.StackStoneVMObject.ListTodayRealTimeTradeRecords.CollectionChanged += ListTodayRealTimeTradeRecords_CollectionChanged;
        }

        void ListTodayRealTimeTradeRecords_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            StoneStackDailyRecordInfo newItem = null;
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Reset)
            {
                _listTodayMinuteTradeRecords.Clear();
                _needRendAll = true;
            }
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                if (e.NewItems == null || e.NewItems.Count == 0)
                {
                    return;
                }
                newItem = e.NewItems[0] as StoneStackDailyRecordInfo;
                if (newItem == null)
                {
                    return;
                }

                if (this._listTodayMinuteTradeRecords.Count == 0)
                {
                    _maxRangeValue = Math.Abs(Math.Round((double)(newItem.ClosePrice - newItem.OpenPrice), 2));
                    _needRendAll = true;
                    this._listTodayMinuteTradeRecords.Add(newItem);
                }
                else
                {
                    _needRendAll = true;
                    var lastRecord = this._listTodayMinuteTradeRecords[this._listTodayMinuteTradeRecords.Count - 1];
                    if (newItem.ClosePrice - newItem.OpenPrice > lastRecord.ClosePrice - lastRecord.OpenPrice)
                    {
                        _maxRangeValue = Math.Abs(Math.Round((double)(newItem.ClosePrice - newItem.OpenPrice), 2));
                    }

                    if (lastRecord.Day.Hour == newItem.Day.Hour && lastRecord.Day.Minute == newItem.Day.Minute)
                    {
                        //if(lastRecord.Day.Second == newItem.Day.Second)
                        this._listTodayMinuteTradeRecords[this._listTodayMinuteTradeRecords.Count - 1] = newItem;
                        this._addItem = false;
                    }
                    else
                    {
                        this._listTodayMinuteTradeRecords.Add(newItem);
                        this._addItem = true;
                    }
                }

            }

            _syn.Post(o =>
            {
                if (_needRendAll)
                {
                    Draw();
                }
                else
                {
                    if (newItem != null)
                    {
                        Point newPoint = ConvertStoneStackDailyRecordInfoToPoint(newItem);
                        if (this._addItem)
                        {
                            this.polyLine.Points.Add(newPoint);
                        }
                        else
                        {
                            this.polyLine.Points[this.polyLine.Points.Count - 1] = newPoint;
                        }
                    }
                }
            }, null);
        }

        private void CreateTempRecords()
        {
            DateTime time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 9, 0, 0);

            for (int i = 0; i < 60; i++)
            {
                _listTodayMinuteTradeRecords.Add(new StoneStackDailyRecordInfo()
                {
                    OpenPrice = 100m,
                    ClosePrice = 100m + i * 0.1m,
                    Day = new MetaData.MyDateTime(time.AddMinutes(i))
                });
            }
        }

        public void Draw()
        {
            if (this == null || GlobalData.GameConfig == null || this.canvas == null)
            {
                return;
            }
            if (this.canvas.ActualWidth > 0 && this.canvas.ActualHeight > 0)
            {
                marketOpeningHours = GlobalData.GameConfig.StackMarketMorningCloseTime - GlobalData.GameConfig.StackMarketMorningOpenTime +
                                        GlobalData.GameConfig.StackMarketAfternoonCloseTime - GlobalData.GameConfig.StackMarketAfternoonOpenTime +
                                        GlobalData.GameConfig.StackMarketNightCloseTime - GlobalData.GameConfig.StackMarketNightOpenTime;
                marketOpeningMinutes = marketOpeningHours * 60;

                startY = this.canvas.ActualHeight / 2;
                yOffsetUnit = _maxRangeValue * 100 / startY;
                xOffsetUnit = this.canvas.ActualWidth / marketOpeningMinutes;

                this.canvas.Children.Clear();
                this.polyLine.Points.Clear();
                //DrawText();
                DrawBaseLine();
                //DrawValueLine();
            }
        }

        private void DrawText()
        {
            TextBlock txtUpRise = new TextBlock()
            {
                Text = _maxRangeValue.ToString(),
                Foreground = _redBrush
            };
            this.canvas.Children.Add(txtUpRise);

            TextBlock txtDownRise = new TextBlock()
            {
                Text = "-" + _maxRangeValue.ToString(),
                Foreground = this._greenBrush,
            };
            this.canvas.Children.Add(txtDownRise);

        }

        private void DrawValueLine()
        {
            for (int i = 0; i < _listTodayMinuteTradeRecords.Count; i++)
            {
                var item = _listTodayMinuteTradeRecords[i];
                polyLine.Points.Add(ConvertStoneStackDailyRecordInfoToPoint(item));
            }

            this.canvas.Children.Add(polyLine);
        }

        private Point ConvertStoneStackDailyRecordInfoToPoint(StoneStackDailyRecordInfo item)
        {
            //TODO: 为了测试，此处临时减去1小时，正式发布时需删除！！！
            int Hours = item.Day.Hour - GlobalData.GameConfig.StackMarketMorningOpenTime;
            if (item.Day.Hour > GlobalData.GameConfig.StackMarketNightOpenTime)
            {
                Hours -= 2;
            }
            else if (item.Day.Hour > GlobalData.GameConfig.StackMarketAfternoonOpenTime)
            {
                Hours -= 1;
            }
            int Minutes = Hours * 60 + item.Day.Minute;
            double pointX = Minutes * xOffsetUnit;
            double Value = (double)(item.ClosePrice - item.OpenPrice);

            return new Point(pointX, startY - Value * yOffsetUnit);
        }

        private void DrawBaseLine()
        {
            for (int i = 1; i <= 3; i++)
            {
                double y = this.canvas.ActualHeight / 4 * i;
                Line lineCenterH = new Line()
                {
                    X1 = 0,
                    Y1 = y,
                    X2 = this.canvas.ActualWidth,
                    Y2 = y,
                    Stroke = _baseLineBrush,
                    StrokeDashArray = new DoubleCollection(new double[] { 1, 1 })
                };
                this.canvas.Children.Add(lineCenterH);

            }

            for (int i = 1; i < this.marketOpeningHours; i++)
            {
                double x = xOffsetUnit * i * 60;
                Line lineV1 = new Line()
                {
                    X1 = x,
                    Y1 = 0,
                    X2 = x,
                    Y2 = this.canvas.ActualHeight,
                    Stroke = _baseLineBrush,
                    StrokeDashArray = new DoubleCollection(new double[] { 1, 1 })
                };
                this.canvas.Children.Add(lineV1);
            }
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Draw();
        }
    }
}
