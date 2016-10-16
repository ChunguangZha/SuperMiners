using MetaData;
using SuperMinersCustomServiceSystem.Uility;
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
    /// Interaction logic for EditAgentInfoWindow.xaml
    /// </summary>
    public partial class EditAgentInfoWindow : Window
    {
        private int _userID;
        private string _userName;

        public bool ISOK = false;

        public EditAgentInfoWindow(int userID, string userName)
        {
            InitializeComponent();

            this._userID = userID;
            this._userName = userName;
            this.txtUserName.Text = userName;

            GlobalData.Client.SetPlayerAsAgentCompleted += Client_SetPlayerAsAgentCompleted;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            GlobalData.Client.SetPlayerAsAgentCompleted -= Client_SetPlayerAsAgentCompleted;
        }

        void Client_SetPlayerAsAgentCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<int> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Cancelled)
                {
                    return;
                }

                if (e.Error != null)
                {
                    MessageBox.Show("设置玩家为代理服务器操作异常。信息为：" + e.Error.Message);
                    return;
                }

                if (e.Result == OperResult.RESULTCODE_TRUE)
                {
                    MyMessageBox.ShowInfo("设置玩家为代理成功。");
                    ISOK = true;
                }
                else
                {
                    MyMessageBox.ShowInfo("设置玩家为代理失败。原因为：" + OperResult.GetMsg(e.Result));
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("获取玩家信息,服务器回调异常。信息为：" + exc.Message);
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (this.txtURL.Text.Trim() == "")
            {
                MyMessageBox.ShowInfo("请输入推广链接");
                return;
            }

            AsyncSetPlayerAsAgent(this._userID, this._userName, this.txtURL.Text.Trim());
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void AsyncSetPlayerAsAgent(int userID, string userName, string agentReferURL)
        {
            if (GlobalData.Client.IsConnected)
            {
                App.BusyToken.ShowBusyWindow("正在提交数据...");
                GlobalData.Client.SetPlayerAsAgent(userID, userName, agentReferURL);
            }
        }

    }
}
