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
    /// Interaction logic for EditPlayerExpWindow.xaml
    /// </summary>
    public partial class EditPlayerExpWindow : Window
    {
        public decimal ExpChanged;

        public EditPlayerExpWindow(string userName, decimal exp)
        {
            InitializeComponent();
            this.txtCurrentExp.Text = exp.ToString();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.ExpChanged = (decimal)this.numChangedExp.Value;
            this.DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
