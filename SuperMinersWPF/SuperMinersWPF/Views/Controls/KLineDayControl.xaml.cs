using MetaData.Game.StoneStack;
using System;
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
    /// Interaction logic for KLineDayControl.xaml
    /// </summary>
    public partial class KLineDayControl : UserControl
    {
        public static int ElementPixelWidth = 9;

        private SolidColorBrush _baseLineBrush = new SolidColorBrush(Color.FromArgb(0xff, 0x32, 0x32, 0x32));
        private SolidColorBrush _yellowBrush = new SolidColorBrush(Colors.Yellow);

        private SolidColorBrush _redBrush = new SolidColorBrush(Colors.Red);
        private SolidColorBrush _blueBrush = new SolidColorBrush(Colors.LightBlue);

        /// <summary>
        /// 每天一条记录
        /// </summary>
        private List<StoneStackDailyRecordInfo> _listTodayMinuteTradeRecords = new List<StoneStackDailyRecordInfo>();
        private StoneStackDailyRecordInfo _firstItem = null;
        private StoneStackDailyRecordInfo _lastItem = null;
        int _firstItemIndex;
        int _lastItemIndex;

        private decimal _maxPrice = 0;
        private decimal _minPrice = decimal.MaxValue;
        private decimal _priceRange = 0;

        /// <summary>
        /// 
        /// </summary>
        private int _startY = 5;
        /// <summary>
        /// 一定小于_minPrice
        /// </summary>
        private double _endY;

        private double _drawablePanelHeight;
        private int _drawableItemsCount;

        /// <summary>
        /// 大于等于0
        /// </summary>
        private int _viewStartIndexOffset = 0;
        double yOffsetUnit = 0;

        private System.Threading.SynchronizationContext _syn;

        public KLineDayControl()
        {
            InitializeComponent();
            this._syn = SynchronizationContext.Current;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            App.StackStoneVMObject.GetAllStoneStackDailyRecordInfoCompleted += StackStoneVMObject_GetAllStoneStackDailyRecordInfoCompleted;
            App.StackStoneVMObject.GetTodayStackRecordInfoCompleted += StackStoneVMObject_GetTodayStackRecordInfoCompleted;
        }

        void StackStoneVMObject_GetTodayStackRecordInfoCompleted(StoneStackDailyRecordInfo obj)
        {
            this._syn.Post(o =>
            {
                if (this._lastItem != null && this._listTodayMinuteTradeRecords.Count > 0 && obj.Day!=null)
                {
                    if (this._lastItem.Day.Year == obj.Day.Year && this._lastItem.Day.Month == obj.Day.Month && this._lastItem.Day.Day == obj.Day.Day)
                    {
                        this._listTodayMinuteTradeRecords[this._listTodayMinuteTradeRecords.Count - 1] = obj;
                        this._lastItem = obj;
                    }
                    else
                    {
                        this._listTodayMinuteTradeRecords.Add(obj);
                        this._lastItem = obj;
                    }

                    if (obj.ClosePrice < this._minPrice)
                    {
                        this._minPrice = obj.ClosePrice;
                    }
                    if (obj.ClosePrice > this._maxPrice)
                    {
                        this._maxPrice = obj.ClosePrice;
                    }

                    Draw();
                }
            }, null);
        }

        void StackStoneVMObject_GetAllStoneStackDailyRecordInfoCompleted(StoneStackDailyRecordInfo[] items)
        {
            //按时间正序
            _listTodayMinuteTradeRecords = new List<StoneStackDailyRecordInfo>(items);
            if (this._listTodayMinuteTradeRecords != null)
            {
                foreach (var item in _listTodayMinuteTradeRecords)
                {
                    if (item.MinTradeSucceedPrice > item.MaxTradeSucceedPrice)
                    {
                        item.MinTradeSucceedPrice = item.MaxTradeSucceedPrice;
                    }
                    if (item.MaxTradeSucceedPrice > this._maxPrice)
                    {
                        this._maxPrice = item.MaxTradeSucceedPrice;
                    }
                    if (item.MinTradeSucceedPrice < this._minPrice)
                    {
                        this._minPrice = item.MinTradeSucceedPrice;
                    }
                }

                if (this._listTodayMinuteTradeRecords.Count > 0)
                {
                    this._firstItem = this._listTodayMinuteTradeRecords[0];
                    this._lastItem = this._listTodayMinuteTradeRecords[this._listTodayMinuteTradeRecords.Count - 1];
                }
            }

            Draw();
        }

        private void Draw()
        {
            if (this == null || GlobalData.GameConfig == null || this.canvas == null || this._minPrice > this._maxPrice || this._lastItem == null || this._firstItem == null)
            {
                return;
            }
            if (this.canvas.ActualWidth > 0 && this.canvas.ActualHeight > 0)
            {
                this.canvas.Children.Clear();
                Compute();
                DrawBaseLine();
                DrawText();
                DrawItem();
            }
        }

        private void Compute()
        {
            this._priceRange = this._maxPrice - this._minPrice;
            this._endY = this.canvas.ActualHeight - 5;
            //上下各留出5像素
            this._drawablePanelHeight = this.canvas.ActualHeight - 10;
            if (this._priceRange == 0)
            {
                this.yOffsetUnit = this._drawablePanelHeight / 10;
            }
            else
            {
                this.yOffsetUnit = this._drawablePanelHeight / (double)this._priceRange;
            }
            //左右各留出2个像素边距
            this._drawableItemsCount = (int)((this.canvas.ActualWidth - 4) / ElementPixelWidth);

            if (this._drawableItemsCount > this._listTodayMinuteTradeRecords.Count)
            {
                this._drawableItemsCount = this._listTodayMinuteTradeRecords.Count;
            }

            _lastItemIndex = this._listTodayMinuteTradeRecords.Count - 1 - this._viewStartIndexOffset;
            _firstItemIndex = _lastItemIndex - this._drawableItemsCount;
            if (_firstItemIndex < 0)
            {
                _firstItemIndex = 0;
            }
            this._lastItem = this._listTodayMinuteTradeRecords[_lastItemIndex];
            this._firstItem = this._listTodayMinuteTradeRecords[_firstItemIndex];
        }

        private void DrawBaseLine()
        {
            for (int i = 1; i <= 3; i++)
            {
                double y = this._drawablePanelHeight / 4 * i + this._startY;
                Line lineCenterH = new Line()
                {
                    X1 = 0,
                    Y1 = y,
                    X2 = this.canvas.ActualWidth,
                    Y2 = y,
                    Stroke = _baseLineBrush,
                    StrokeDashArray = new DoubleCollection(new double[] { 1, 1 })
                };
                if (i == 2)
                {
                    lineCenterH.Stroke = this._yellowBrush;
                }
                this.canvas.Children.Add(lineCenterH);
            }
        }

        private void DrawText()
        {
            this.txtDownRiseValue.Text = this._minPrice.ToString("F2");
            this.txtUpRiseValue.Text = this._maxPrice.ToString("F2");
            this.txtOpenPrice.Text = ((this._maxPrice - this._minPrice) / 2 + this._minPrice).ToString("F2");

            this.txtLastDay.Text = this._lastItem.Day.ToDateTime().ToString("yy/MM/dd");
            this.txtFirstDay.Text = this._firstItem.Day.ToDateTime().ToString("yy/MM/dd");
        }

        private void DrawItem()
        {
            //从左往右画
            for (int i = _firstItemIndex; i <= _lastItemIndex; i++)
            {
                Polygon pgon = CreatePolygon(i - _firstItemIndex, this._listTodayMinuteTradeRecords[i]);
                this.canvas.Children.Add(pgon);
            }
        }

        private Polygon CreatePolygon(int index, StoneStackDailyRecordInfo data)
        {
            Polygon pgon = new Polygon();
            pgon.StrokeThickness = 1;
            if (data.ClosePrice >= data.OpenPrice)
            {
                pgon.Stroke = this._redBrush;
            }
            else
            {
                pgon.Stroke = this._blueBrush;
                pgon.Fill = this._blueBrush;
            }

            //从左往右画
            //留出两个像素边距
            int startX = (ElementPixelWidth * index) + 2;
            int middleX = startX + 3;
            int endX = startX + 6;
            decimal topYValue = data.OpenPrice >= data.ClosePrice ? data.OpenPrice : data.ClosePrice;
            decimal bottomYValue = data.OpenPrice < data.ClosePrice ? data.OpenPrice : data.ClosePrice;
            int minY = (int)((this._maxPrice - data.MaxTradeSucceedPrice) * (decimal)this.yOffsetUnit + this._startY);
            int maxY = (int)((this._maxPrice - data.MinTradeSucceedPrice) * (decimal)this.yOffsetUnit + this._startY);
            int topY = (int)((this._maxPrice - topYValue) * (decimal)this.yOffsetUnit + this._startY);
            int bottomY = (int)((this._maxPrice - bottomYValue) * (decimal)this.yOffsetUnit + this._startY);
            if (minY > topY)
            {
                minY = topY;
            }
            if (maxY < bottomY)
            {
                maxY = bottomY;
            }

            pgon.Points.Add(new Point(middleX, minY));
            pgon.Points.Add(new Point(middleX, topY));
            pgon.Points.Add(new Point(endX, topY));
            pgon.Points.Add(new Point(endX, bottomY));
            pgon.Points.Add(new Point(middleX, bottomY));
            pgon.Points.Add(new Point(middleX, maxY));
            pgon.Points.Add(new Point(middleX, bottomY));
            pgon.Points.Add(new Point(startX, bottomY));
            pgon.Points.Add(new Point(startX, topY));
            pgon.Points.Add(new Point(middleX, topY));
            pgon.Points.Add(new Point(middleX, minY));

            return pgon;
        }

        private void canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Draw();
        }
    }

    public class DailyRecordUIElement
    {
        private StoneStackDailyRecordInfo Data = null;

        public Polygon polygon = null;

        public DailyRecordUIElement(StoneStackDailyRecordInfo data)
        {
            this.Data = data;
        }

        private void CreatePolygon()
        {

        }
    }
}
