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

namespace SuperMinersCustomServiceSystem.View
{
    /// <summary>
    /// Interaction logic for InputActionPassword.xaml
    /// </summary>
    public partial class InputActionPasswordWindow : Window
    {
        public string ActionPassword = "";

        public InputActionPasswordWindow()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (this.txtActionPassword.Password == "")
            {
                MessageBox.Show("请输入操作密码");
                return;
            }

            if (GlobalData.CurrentAdmin.ActionPassword != this.txtActionPassword.Password)
            {
                MessageBox.Show("操作密码不正确");
                return;
            }

            ActionPassword = this.txtActionPassword.Password;

            this.DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
