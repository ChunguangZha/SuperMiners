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

namespace DESEncryptTools
{
    /// <summary>
    /// Interaction logic for TestJsonSerializeWindow.xaml
    /// </summary>
    public partial class TestJsonSerializeWindow : Window
    {
        public TestJsonSerializeWindow()
        {
            InitializeComponent();
        }

        private void btnEncrypt_Click(object sender, RoutedEventArgs e)
        {
            //TestModel obj = new TestModel()
            //{
            //    Gateway = "22.33.44.55",
            //    GatewayEnable = true,
            //    PrimaryDNS = "primary dns",
            //    PrimaryDNSEnable = false,
            //    //SecondaryDNS = "99.123.345.567",
            //     id= 99,
            //      name="test",
            //    SecondaryDNSEnable = true
            //};

            RTU320PortConfigInfo port = new RTU320PortConfigInfo();

            string json = JsonSerializeTest<RTU320PortConfigInfo>.SaveToJson(port);
            this.txtDescString_ToEncrypt.Text = json;
        }

        private void btnDecrypt_Click(object sender, RoutedEventArgs e)
        {
            string json = this.txtDescString_ToEncrypt.Text;
            RTU320PortConfigInfo obj = JsonSerializeTest<RTU320PortConfigInfo>.ReadFromJson(json);
            Console.WriteLine(obj);

        }
    }
}
