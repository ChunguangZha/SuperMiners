using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SuperMinersWPF.MyControl
{
    public class NumericTextBox : TextBox
    {
        public static Control FocusControl = null;
        public static Control FocusWindow = null;

        public event RoutedPropertyChangedEventHandler<double> ValueChanged;

        private string _inputed = "0";

        public NumericTextBox()
        {
            this.Text = "0";
        }

        #region Public Properties

        public static readonly DependencyProperty DecimalPlacesProperty =
           DependencyProperty.Register(
               "DecimalPlaces",
               typeof(int),
               typeof(NumericTextBox),
               new PropertyMetadata(0, (o, e) =>
               {
                   NumericTextBox tb = (NumericTextBox)o;
                   tb.Text = tb.Value.ToString(tb.StringFormat + tb.DecimalPlaces, System.Globalization.CultureInfo.InvariantCulture);
               }));

        public static readonly DependencyProperty StringFormatProperty =
          DependencyProperty.Register(
              "StringFormat",
              typeof(string),
              typeof(NumericTextBox),
              new PropertyMetadata("F", (o, e) =>
              {
                  NumericTextBox tb = (NumericTextBox)o;
                  tb.Text = tb.Value.ToString(tb.StringFormat + tb.DecimalPlaces, System.Globalization.CultureInfo.InvariantCulture);
              }));

        public static readonly DependencyProperty MinimumProperty =
           DependencyProperty.Register(
               "Minimum",
               typeof(double),
               typeof(NumericTextBox),
               new PropertyMetadata(0.0, (o, e) =>
               {
                   NumericTextBox tb = (NumericTextBox)o;
                   if (tb.Minimum <= tb.Maximum)
                   {
                       if (tb.Value < tb.Minimum)
                       {
                           tb.Text = tb.Minimum.ToString(tb.StringFormat + tb.DecimalPlaces, System.Globalization.CultureInfo.InvariantCulture);
                           tb.Value = tb.Minimum;
                       }
                       else if (tb.Value > tb.Maximum)
                       {
                           tb.Text = tb.Maximum.ToString(tb.StringFormat + tb.DecimalPlaces, System.Globalization.CultureInfo.InvariantCulture);
                           tb.Value = tb.Maximum;
                       }
                   }
               }));

        public static readonly DependencyProperty MaximumProperty =
          DependencyProperty.Register(
              "Maximum",
              typeof(double),
              typeof(NumericTextBox),
              new PropertyMetadata(10000.0, (o, e) =>
              {
                  NumericTextBox tb = (NumericTextBox)o;
                  if (tb.Minimum <= tb.Maximum)
                  {
                      if (tb.Value < tb.Minimum)
                      {
                          tb.Text = tb.Minimum.ToString(tb.StringFormat + tb.DecimalPlaces, System.Globalization.CultureInfo.InvariantCulture);
                          tb.Value = tb.Minimum;
                      }
                      else if (tb.Value > tb.Maximum)
                      {
                          tb.Text = tb.Maximum.ToString(tb.StringFormat + tb.DecimalPlaces, System.Globalization.CultureInfo.InvariantCulture);
                          tb.Value = tb.Maximum;
                      }
                  }
              }));

        public static readonly DependencyProperty ValueProperty =
        DependencyProperty.Register(
            "Value",
            typeof(double),
            typeof(NumericTextBox),
            new PropertyMetadata(0.0, (o, e) =>
            {
                NumericTextBox tb = (NumericTextBox)o;

                bool changeAgain = false;
                double value = (double)e.NewValue;
                if (value > tb.Maximum)
                {
                    value = tb.Maximum;
                    changeAgain = true;
                }
                else if (value < tb.Minimum)
                {
                    value = tb.Minimum;
                    changeAgain = true;
                }

                //tb.Text = value.ToString(tb.StringFormat + tb.DecimalPlaces, System.Globalization.CultureInfo.InvariantCulture);
                if (changeAgain)
                {
                    try
                    {
                        tb.Dispatcher.BeginInvoke(new EventHandler((o1, e1) =>
                        {
                            try
                            {
                                tb.Value = value;
                            }
                            catch
                            {
                            }
                        }));
                    }
                    catch
                    {
                    }
                    return;
                }

                RoutedPropertyChangedEventHandler<double> eh = tb.ValueChanged;
                if (null != eh)
                {
                    eh(tb, new RoutedPropertyChangedEventArgs<double>((double)e.OldValue, value));
                }
            }));

        public string StringFormat
        {
            get
            {
                return (string)this.GetValue(StringFormatProperty);
            }
            set
            {
                this.SetValue(StringFormatProperty, value);
            }
        }

        public int DecimalPlaces
        {
            get
            {
                return (int)this.GetValue(DecimalPlacesProperty);
            }
            set
            {
                this.SetValue(DecimalPlacesProperty, value);
            }
        }

        public double Minimum
        {
            get
            {
                return (double)this.GetValue(MinimumProperty);
            }
            set
            {
                this.SetValue(MinimumProperty, value);
            }
        }

        public double Maximum
        {
            get
            {
                return (double)this.GetValue(MaximumProperty);
            }
            set
            {
                this.SetValue(MaximumProperty, value);
            }
        }

        public double Value
        {
            get
            {
                return (double)this.GetValue(ValueProperty);
            }
            set
            {
                this.SetValue(ValueProperty, value);
            }
        }

        #endregion

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            //this.ChangeValue(this.Text);
        }

        protected override void OnKeyDown(System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.D0 || e.Key == Key.D1 || e.Key == Key.D2 || e.Key == Key.D3 || e.Key == Key.D4 || 
                e.Key == Key.D5 || e.Key == Key.D6 || e.Key == Key.D7 || e.Key == Key.D8 || e.Key == Key.D9 ||
                e.Key == Key.NumPad0 || e.Key == Key.NumPad1 || e.Key == Key.NumPad2 || e.Key == Key.NumPad3 || e.Key == Key.NumPad4 || 
                e.Key == Key.NumPad5 || e.Key == Key.NumPad6 || e.Key == Key.NumPad7 || e.Key == Key.NumPad8 || e.Key == Key.NumPad9)
            {
                if (this.Text == "0")
                {
                    this.Text = "";
                }
            }
            else if (e.Key == Key.OemPeriod ||e.Key == Key.Decimal)
            {
                if (this.DecimalPlaces <= 0)
                {
                    e.Handled = true;
                }
                else
                {
                    if (this.Text.Contains('.'))
                    {
                        e.Handled = true;
                    }
                }
            }
            else
            {
                e.Handled = true;
            }


            //if (e.Key == Key.Enter)
            //{
            //    if (FocusWindow == null)
            //    {
            //        if (FocusControl == null)
            //        {
            //            this.ChangeValue(this.Text);
            //        }
            //        else
            //        {
            //            if (!FocusControl.Focus())
            //            {
            //                this.ChangeValue(this.Text);
            //            }
            //        }
            //    }
            //    else
            //    {
            //        if (!FocusWindow.Focus())
            //        {
            //            this.ChangeValue(this.Text);
            //        }
            //    }
            //    e.Handled = true;
            //}

            base.OnKeyDown(e);
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            this.SelectAll();
            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            this.ChangeValue(this.Text);

            base.OnLostFocus(e);
        }

        public void ChangeValue(string text)
        {
            double value;

            if (text.Length == 0)
            {
                value = 0;
            }
            else
            {
                if (!Double.TryParse(text, NumberStyles.AllowExponent | NumberStyles.Float, CultureInfo.InvariantCulture, out value))
                {
                    text = this._inputed;
                    this.SelectionStart = text.Length;
                    return;
                }
            }

            if (value > this.Maximum)
            {
                value = this.Maximum;
            }
            else if (value < this.Minimum)
            {
                value = this.Minimum;
            }

            this.Text = value.ToString(this.StringFormat + this.DecimalPlaces, System.Globalization.CultureInfo.InvariantCulture);
            this._inputed = this.Text;
            this.Value = value;
        }
    }
}
