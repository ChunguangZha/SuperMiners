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
            StringBuilder builder = new StringBuilder();
            string baseuri = "";
#if DEBUG
            baseuri = "http://localhost:8509/";
#else

            baseuri = System.Configuration.ConfigurationManager.AppSettings["WebUri"];
#endif

            string uri = baseuri + "Register.aspx?ic=" + GlobalData.CurrentUser.InvitationCode;
            builder.Append(" " + uri);

            this.txtInvitationCode.Text = builder.ToString();
            this.txtReferrerMsg.Text = "推荐好友，注册后成功登录，即可获取如下奖励：" + GlobalData.AwardReferrerLevelConfig.GetAwardByLevel(1).ToString();
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
