using SuperMinersCustomServiceSystem.Wcf.Channel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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

namespace SuperMinersCustomServiceSystem
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        MainWindow _winMain = null;
        private bool isHidden = false;
        private SynchronizationContext _syn;

        public LoginWindow()
        {
            InitializeComponent();

            isHidden = false;
            this.txtAdminUserName.Focus();
            GlobalData.Client.LoginAdminCompleted += Client_LoginAdminCompleted;
        }

        void Client_LoginAdminCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<string> e)
        {
            if (e.Cancelled)
            {
                App.BusyToken.CloseBusyWindow();
                return;
            }

            if (e.Error != null)
            {
                App.BusyToken.CloseBusyWindow();
                //LogHelper.Instance.AddErrorLog("服务器连接失败。", e.Error);
                MessageBox.Show("服务器连接失败。");
                return;
            }

            if (string.IsNullOrEmpty(e.Result))
            {
                App.BusyToken.CloseBusyWindow();
                MessageBox.Show("用户名不存在，或密码不正确。");
                return;
            }

            if (e.Result == "ISLOGGED")
            {
                App.BusyToken.CloseBusyWindow();
                MessageBox.Show("您的账户正在其它客户端登录，我们已将对方退出，请重新登录。");
                return;
            }

            this._syn.Post(o =>
            {
                this.txtAdminUserName.Text = "";
                this.txtPassword.Password = "";
            }, null);

            GlobalData.InitToken(e.Result);

            if (!isHidden)
            {
                isHidden = true;
                _winMain = new MainWindow();
                _winMain.Closed += WinMain_Closed;
                this.Visibility = System.Windows.Visibility.Hidden;
                _winMain.Show();
            }
        }

        void WinMain_Closed(object sender, EventArgs e)
        {
            this.isHidden = false;
            this.Visibility = System.Windows.Visibility.Visible;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (!CryptEncoder.Ready)
            {
                MessageBox.Show("正在初始化，请等待...");
                return;
            }

            if (this.txtAdminUserName.Text == "")
            {
                MessageBox.Show("请输入用户名");
                return;
            }

            if (this.txtPassword.Password == "")
            {
                MessageBox.Show("请输入密码");
                return;
            }

            string mac = GetMac();
            GlobalData.Client.LoginAdmin(this.txtAdminUserName.Text.Trim(), this.txtPassword.Password.Trim(), mac, CryptEncoder.Key);
        }

        private string GetMac()
        {
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();//获取本地计算机上网络接口的对象

            foreach (NetworkInterface adapter in adapters)
            {
                if (adapter.NetworkInterfaceType == NetworkInterfaceType.Ethernet || adapter.NetworkInterfaceType == NetworkInterfaceType.Ethernet3Megabit || adapter.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
                {
                    if (adapter.OperationalStatus == OperationalStatus.Up && adapter.Speed > 0)
                    {
                        // 格式化显示MAC地址               
                        PhysicalAddress pa = adapter.GetPhysicalAddress();//获取适配器的媒体访问（MAC）地址
                        byte[] bytes = pa.GetAddressBytes();//返回当前实例的地址
                        StringBuilder sb = new StringBuilder();
                        for (int i = 0; i < bytes.Length; i++)
                        {
                            sb.Append(bytes[i].ToString("X2"));//以十六进制格式化
                            if (i != bytes.Length - 1)
                            {
                                sb.Append("-");
                            }
                        }

                        return sb.ToString();
                    }
                }
                //Console.WriteLine("描述：" + adapter.Description);
                //Console.WriteLine("标识符：" + adapter.Id);
                //Console.WriteLine("名称：" + adapter.Name);
                //Console.WriteLine("类型：" + adapter.NetworkInterfaceType);
                //Console.WriteLine("速度：" + adapter.Speed * 0.001 * 0.001 + "M");
                //Console.WriteLine("操作状态：" + adapter.OperationalStatus);
                //Console.WriteLine("MAC 地址：" + adapter.GetPhysicalAddress());

                //Console.WriteLine("MAC 地址：" + sb);
                //Console.WriteLine();
            }

            return null;
        }

    }
}
