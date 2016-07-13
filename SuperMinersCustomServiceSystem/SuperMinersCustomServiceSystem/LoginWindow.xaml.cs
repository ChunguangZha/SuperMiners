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

namespace SuperMinersCustomServiceSystem
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        MainWindow _winMain = null;

        public LoginWindow()
        {
            InitializeComponent();
            this.txtAdminUserName.Focus();
            GlobalData.Client.LoginAdminCompleted += Client_LoginAdminCompleted;
        }

        void Client_LoginAdminCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<string> e)
        {
            throw new NotImplementedException();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (this.txtAdminUserName.Text == "")
            {
                MessageBox.Show("");
            }
        }
    }
}
