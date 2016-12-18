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

namespace SuperMinersCustomServiceSystem.View.Windows
{
    /// <summary>
    /// Interaction logic for InputValueWindow.xaml
    /// </summary>
    public partial class InputValueWindow : Window
    {

        public double Value { get; private set; }

        public InputValueWindow(string title, string msg, int maxValue, int minValue)
        {
            InitializeComponent();
            this.Title = title;
            this.lblMsg.Text = msg;
            this.numValue.Maximum = maxValue;
            this.numValue.Value = minValue;
            this.numValue.Minimum = minValue;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.Value = this.numValue.Value;
            this.DialogResult = true;
        }
    }
}
