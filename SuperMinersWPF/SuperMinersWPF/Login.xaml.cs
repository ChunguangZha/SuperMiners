using MetaData.SystemConfig;
using MetaData.User;
using SuperMinersWPF.Models;
using SuperMinersWPF.StringResources;
using SuperMinersWPF.Utility;
using SuperMinersWPF.Wcf.Channel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SuperMinersWPF
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private SynchronizationContext _syn;
        private bool isHidden = false;

        public Login()
        {
            InitializeComponent();

            this.Title = Strings.Title + System.Configuration.ConfigurationManager.AppSettings["softwareversion"];

            isHidden = false;

            LogHelper.Instance.Init();
            this._syn = SynchronizationContext.Current;
            GlobalData.Client.SetContext(this._syn);
            App.UserVMObject.GetPlayerInfoCompleted += UserVMObject_GetPlayerInfoCompleted;
            GlobalData.Client.LoginCompleted += Client_LoginCompleted;
            GlobalData.Client.GetGameConfigCompleted += Client_GetGameConfigCompleted;
            this.txtUserName.Focus();
        }

        void UserVMObject_GetPlayerInfoCompleted(object sender, EventArgs e)
        {
            GlobalData.Client.HandleCallback();
            if (!isHidden)
            {
                isHidden = true;
                Window1 winMain = new Window1();
                winMain.Closed += winMain_Closed;
                this.Visibility = System.Windows.Visibility.Hidden;
                winMain.Show();
                GlobalData.Client.GetPlayerInfo();
            }
        }

        void Client_GetGameConfigCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<GameConfig> e)
        {
            if (e.Cancelled)
            {
                return;
            }

            if (e.Error != null || e.Result == null)
            {
                MyMessageBox.ShowInfo("获取用户信息失败。");
                GlobalData.Client.Logout();
                return;
            }

            e.Continue = true;
            GlobalData.GameConfig = e.Result;

            App.UserVMObject.AsyncGetPlayerInfo();
        }

        void Client_LoginCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<string> e)
        {
            if (e.Cancelled)
            {
                return;
            }

            if (e.Error != null)
            {
                LogHelper.Instance.AddErrorLog("服务器连接失败。", e.Error);
                MyMessageBox.ShowInfo("服务器连接失败。");
                return;
            }

            if (string.IsNullOrEmpty(e.Result))
            {
                MyMessageBox.ShowInfo("该用户不存在，或密码不正确。");
                return;
            }

            if (e.Result == "ISLOGGED")
            {
                MyMessageBox.ShowInfo("该用户已经在其它客户端登录。");
                return;
            }

            this._syn.Post(o =>
            {
                this.txtUserName.Text = "";
                this.txtPassword.Password = "";
            }, null);

            GlobalData.InitToken(e.Result);

            e.Continue = true;

            GlobalData.Client.GetGameConfig();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginAync();
        }

        private void LoginAync()
        {
            if (!CryptEncoder.Ready)
            {
                MyMessageBox.ShowInfo("正在初始化，请等待...");
                return;
            }

            if (this.txtUserName.Text.Length == 0)
            {
                MyMessageBox.ShowInfo("请输入用户名");
                return;
            }

            if (this.txtPassword.Password.Length == 0)
            {
                MyMessageBox.ShowInfo("请输入密码");
                return;
            }

            GlobalData.InitToken(null);

#if DEBUG
            GlobalData.Client.Init(GlobalData.DebugServer);
#else
            string serverUri = System.Configuration.ConfigurationManager.AppSettings["ServerUri"];
            if (string.IsNullOrEmpty(serverUri))
            {
                MyMessageBox.ShowInfo("找不到服务器Uri地址，请联系系统管理员，或者安装最新版本。");
                return;
            }

            GlobalData.Client.Init(serverUri);
#endif
            string userName = this.txtUserName.Text;
            string password = this.txtPassword.Password;

            GlobalData.Client.Login(userName, password, CryptEncoder.Key);
        }

        void winMain_Closed(object sender, EventArgs e)
        {
            this.Close();
            //if (GlobalData.IsLogined)
            //{
            //    this.isHidden = false;
            //    this.Visibility = System.Windows.Visibility.Visible;
            //}
        }

        private void hlinkForgetPassword_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.ToString()));
            e.Handled = true;
        }

        private void hlinkRegister_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            string baseuri = "";
#if DEBUG
            baseuri = "http://localhost:8509/";
#else

            baseuri = System.Configuration.ConfigurationManager.AppSettings["WebUri"];
#endif

            Process.Start(new ProcessStartInfo(baseuri + "Register.aspx"));
            e.Handled = true;
        }

        private void hlinkHomePage_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            string baseuri = "";
#if DEBUG
            baseuri = "http://localhost:8509/";
#else

            baseuri = System.Configuration.ConfigurationManager.AppSettings["WebUri"];
#endif

            Process.Start(new ProcessStartInfo(baseuri));
            e.Handled = true;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void panelTopBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
    }
}
