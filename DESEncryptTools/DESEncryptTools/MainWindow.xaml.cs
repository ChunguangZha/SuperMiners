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

namespace DESEncryptTools
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnEncrypt_Click(object sender, RoutedEventArgs e)
        {
            if (this.txtSrcString_ToEncrypt.Text == "")
            {
                return;
            }

            this.txtDescString_ToEncrypt.Text = DESEncrypt.EncryptDES(this.txtSrcString_ToEncrypt.Text);
        }

        private void btnDecrypt_Click(object sender, RoutedEventArgs e)
        {
            if (this.txtSrcString_ToDecrypt.Text == "")
            {
                return;
            }

            this.txtDescString_ToDecrypt.Text = DESEncrypt.DecryptDES(this.txtSrcString_ToDecrypt.Text);
        }
    }
}
