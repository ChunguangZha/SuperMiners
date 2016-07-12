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
using System.Net.NetworkInformation;

namespace SuperMinersCustomServiceSystem
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

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            GetMac();
        }

        private void btnEditPlayerInfo_Click(object sender, RoutedEventArgs e)
        {

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
