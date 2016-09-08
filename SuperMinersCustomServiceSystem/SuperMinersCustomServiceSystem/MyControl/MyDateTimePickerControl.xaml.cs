using MetaData;
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

namespace SuperMinersCustomServiceSystem.MyControl
{
    /// <summary>
    /// Interaction logic for MyDateTimePickerControl.xaml
    /// </summary>
    public partial class MyDateTimePickerControl : UserControl
    {
        private MyDateTime _valueTime = MyDateTime.FromDateTime(DateTime.Now);

        public MyDateTime ValueTime
        {
            get { return _valueTime; }
            set
            {
                _valueTime = value;
                SetTimeValue(value);
            }
        }

        private bool _showTime;

        public bool ShowTime
        {
            get { return _showTime; }
            set
            {
                _showTime = value;
                if (value)
                {
                    this.panelTime.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    this.panelTime.Visibility = System.Windows.Visibility.Collapsed;
                }
            }
        }


        internal void SetTimeValue(MyDateTime time)
        {
            if (time == null)
            {
                this.chkSelectDate.IsChecked = false;
            }
            this.chkSelectDate.IsChecked = !time.IsNull;
            this.datePicker.SelectedDate = time.ToDateTime();
            this.numHour.Value = time.Hour;
            this.numMinute.Value = time.Minute;
            this.numSecond.Value = time.Second;
        }

        public MyDateTimePickerControl()
        {
            InitializeComponent();
        }

        private void chkSelectDate_Checked(object sender, RoutedEventArgs e)
        {
            if (this.datePicker == null)
            {
                return;
            }

            this.datePicker.IsEnabled = true;
            this.numHour.IsEnabled = true;
            this.numMinute.IsEnabled = true;
            this.numSecond.IsEnabled = true;

            this._valueTime.IsNull = false;
        }

        private void chkSelectDate_Unchecked(object sender, RoutedEventArgs e)
        {
            if (this.datePicker == null)
            {
                return;
            }

            this.datePicker.IsEnabled = false;
            this.numHour.IsEnabled = false;
            this.numMinute.IsEnabled = false;
            this.numSecond.IsEnabled = false;

            this._valueTime.IsNull = true;
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime selectedValue = this.datePicker.SelectedDate.Value;
            this._valueTime.Year = selectedValue.Year;
            this._valueTime.Month = selectedValue.Month;
            this._valueTime.Day = selectedValue.Day;
        }

        private void numHour_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this._valueTime.Hour = (int)this.numHour.Value;
        }

        private void numMinute_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this._valueTime.Minute = (int)this.numMinute.Value;
        }

        private void numSecond_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this._valueTime.Second = (int)this.numSecond.Value;
        }
    }
}
