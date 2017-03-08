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
    /// Interaction logic for InputUserLoginNamePasswordWindow.xaml
    /// </summary>
    public partial class InputUserLoginNamePasswordWindow : Window
    {
        private string _newServerUserLoginName;

        public string NewServerUserLoginName
        {
            get { return _newServerUserLoginName; }
            set { _newServerUserLoginName = value; }
        }

        private string _newServerPassword;

        public string NewServerPassword
        {
            get { return _newServerPassword; }
            set { _newServerPassword = value; }
        }


        public InputUserLoginNamePasswordWindow()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.NewServerUserLoginName = this.txtNewServerUserLoginName.Text.Trim();
            this.NewServerPassword = this.txtNewServerPassword.Password;
            this.DialogResult = true;
        }
    }
}
