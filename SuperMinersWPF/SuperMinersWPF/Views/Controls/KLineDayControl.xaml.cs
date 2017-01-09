using MetaData.Game.StoneStack;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for KLineDayControl.xaml
    /// </summary>
    public partial class KLineDayControl : UserControl
    {
        private SolidColorBrush _baseLineBrush = new SolidColorBrush(Color.FromArgb(0xff, 0x32, 0x32, 0x32));
        private SolidColorBrush _yellowBrush = new SolidColorBrush(Colors.Yellow);

        private SolidColorBrush _redBrush = new SolidColorBrush(Colors.Red);
        private SolidColorBrush _blueBrush = new SolidColorBrush(Colors.Blue);

        /// <summary>
        /// 每天一条记录
        /// </summary>
        private StoneStackDailyRecordInfo[] _listTodayMinuteTradeRecords = null;
        private double _maxPrice = 0;
        private double _minPrice = double.MaxValue;
        private double _priceRange = 0;

        /// <summary>
        /// 
        /// </summary>
        private double _startY = 5;
        /// <summary>
        /// 一定小于_minPrice
        /// </summary>
        private double _endY;

        private double _drawablePanelHeight;
        private double _drawableItemsCount;


        double yOffsetUnit = 0;
        double xOffsetUnit = 0;

        public KLineDayControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            App.StackStoneVMObject.GetAllStoneStackDailyRecordInfoCompleted += StackStoneVMObject_GetAllStoneStackDailyRecordInfoCompleted;
        }

        void StackStoneVMObject_GetAllStoneStackDailyRecordInfoCompleted(StoneStackDailyRecordInfo[] obj)
        {
            _listTodayMinuteTradeRecords = obj;
            foreach (var item in _listTodayMinuteTradeRecords)
            {
                if ((double)item.MaxTradeSucceedPrice > this._maxPrice)
                {
                    this._maxPrice = (double)item.MaxTradeSucceedPrice;
                }
                if ((double)item.MinTradeSucceedPrice < this._minPrice)
                {
                    this._minPrice = (double)item.MinTradeSucceedPrice;
                }
            }
        }

        private void Draw()
        {
            if (this == null || GlobalData.GameConfig == null || this.canvas == null || this._minPrice >= this._maxPrice)
            {
                return;
            }
            if (this.canvas.ActualWidth > 0 && this.canvas.ActualHeight > 0)
            {
                Compute();
                DrawBaseLine();
                DrawText();
            }
        }

        private void Compute()
        {
            this._priceRange = this._maxPrice - this._minPrice;
            this._endY = this.canvas.ActualHeight - 5;
            //上下各留出5像素
            this._drawablePanelHeight = this.canvas.ActualHeight - 10;
            this.yOffsetUnit = (this._drawablePanelHeight) / (this._priceRange);

            this._drawableItemsCount = (int)(this.canvas.ActualWidth / DailyRecordUIElement.ElementPixelWidth);
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
            this.txtOpenPrice.Text = ((this._maxPrice - this._minPrice) / 2).ToString("F2");

            //this.txtLastDay.Text = 
        }
    }

    public class DailyRecordUIElement
    {
        public static int ElementPixelWidth = 6;


    }
}
