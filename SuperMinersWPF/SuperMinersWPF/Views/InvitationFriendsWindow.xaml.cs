using SuperMinersWPF.Utility;
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
            if (GlobalData.AgentUserInfo == null)
            {
                SetAdText();
            }
            else
            {
                this.txtInvitationCode.Text = GlobalData.AgentUserInfo.InvitationURL;
                this.txtReferrerMsg.Text = "";
            }
        }

        private void SetAdText()
        {
            string baseuri = "";
#if DEBUG

            if (GlobalData.ServerType == ServerType.Server1)
            {
                baseuri = "http://localhost:8509/";
            }
            else
            {
                baseuri = "http://localhost:34634/";
            }
#else

            if (GlobalData.ServerType == ServerType.Server1)
            {
                baseuri = System.Configuration.ConfigurationManager.AppSettings["WebUri1"];
            }
            else
            {
                baseuri = System.Configuration.ConfigurationManager.AppSettings["WebUri2"];
            }
#endif

            string uri = baseuri + "Register.aspx?ic=" + GlobalData.CurrentUser.InvitationCode;

            this.txtInvitationCode.Text = uri;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(this.txtInvitationCode.Text);
            MyMessageBox.ShowInfo("已复制到剪切版");
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
