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

namespace SuperMinersWPF.Views
{
    /// <summary>
    /// Interaction logic for InvitationFriendsWindow.xaml
    /// </summary>
    public partial class InvitationFriendsWindow : Window
    {
        public InvitationFriendsWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            string baseuri = "";
#if DEBUG
            baseuri = "http://localhost:8509/";
#else

            baseuri = System.Configuration.ConfigurationManager.AppSettings["WebUri"];
#endif

            string uri = baseuri + "Register.aspx?invitationcode=" + GlobalData.CurrentUser.InvitationCode;

            this.txtInvitationCode.Text = uri;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
