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
              new PropertyMetadata(100.0, (o, e) =>
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

                tb.Text = value.ToString(tb.StringFormat + tb.DecimalPlaces, System.Globalization.CultureInfo.InvariantCulture);
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

        protected override void OnKeyDown(System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (FocusWindow == null)
                {
                    if (FocusControl == null)
                    {
                        this.ChangeValue();
                    }
                    else
                    {
                        if (!FocusControl.Focus())
                        {
                            this.ChangeValue();
                        }
                    }
                }
                else
                {
                    if (!FocusWindow.Focus())
                    {
                        this.ChangeValue();
                    }
                }
                e.Handled = true;
            }

            base.OnKeyDown(e);
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            this.ChangeValue();

            base.OnLostFocus(e);
        }

        public void ChangeValue()
        {
            double value;

            if (this.Text.Length == 0)
            {
                value = 0;
            }
            else
            {
                if (!Double.TryParse(this.Text, NumberStyles.AllowExponent | NumberStyles.Float, CultureInfo.InvariantCulture, out value))
                {
                    this.Text = this._inputed;
                    this.SelectionStart = this.Text.Length;
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
