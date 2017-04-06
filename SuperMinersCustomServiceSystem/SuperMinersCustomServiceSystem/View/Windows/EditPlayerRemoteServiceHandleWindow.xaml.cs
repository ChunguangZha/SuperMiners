using MetaData;
using MetaData.Trade;
using SuperMinersCustomServiceSystem.Model;
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
    /// Interaction logic for EditPlayerRemoteServiceHandleWindow.xaml
    /// </summary>
    public partial class EditPlayerRemoteServiceHandleWindow : Window
    {
        public EditPlayerRemoteServiceHandleWindow()
        {
            InitializeComponent();

        }

        public void AddNewRecord(string userName)
        {
            this.Title = "添加新的远程协助服务记录";
            this.txtPlayerUserName.Text = userName;
        }

        public void UpdateRecord(UserRemoteHandleServiceRecord oldRecord)
        {
            this.Title = "修改远程协助服务记录";
            this.txtPlayerUserName.Text = oldRecord.UserName;
            this.txtWorkerName.Text = oldRecord.WorkerName;
            this.myTimeServiceTime.ValueTime = oldRecord.ServiceTime;
            this.txtServiceContent.Text = oldRecord.ServiceContent;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (this.txtWorkerName.Text.Trim() == "")
            {
                MyMessageBox.ShowInfo("请填写服务工程师");
                return;
            }
            if (this.myTimeServiceTime.ValueTime == null || this.myTimeServiceTime.ValueTime.IsNull)
            {
                MyMessageBox.ShowInfo("请填写服务时间");
                return;
            }
            if (this.txtServiceContent.Text == "")
            {
                MyMessageBox.ShowInfo("请填写服务内容");
                return;
            }

            UserRemoteHandleServiceRecord record = new UserRemoteHandleServiceRecord()
            {
                UserName = this.txtPlayerUserName.Text,
                 WorkerName = this.txtWorkerName.Text.Trim(),
                  ServiceTime = this.myTimeServiceTime.ValueTime,
                   ServiceContent = this.txtServiceContent.Text
            };

            InputActionPasswordWindow winInputActionPassword = new InputActionPasswordWindow();
            if (winInputActionPassword.ShowDialog() == true)
            {
                AsyncHandlePlayerRemoteService(winInputActionPassword.ActionPassword, record.UserName, record.ServiceContent, record.ServiceTime, record.WorkerName);
            }

        }

        public void AsyncHandlePlayerRemoteService(string actionPassword, string playerUserName, string serviceContent, MyDateTime serviceTime, string engineerName)
        {
            if (GlobalData.Client.IsConnected)
            {
                GlobalData.Client.HandlePlayerRemoteServiceCompleted += Client_HandlePlayerRemoteServiceCompleted;
                App.BusyToken.ShowBusyWindow("正在提交数据...");
                GlobalData.Client.HandlePlayerRemoteService(actionPassword, playerUserName, serviceContent, serviceTime, engineerName);
            }
        }

        void Client_HandlePlayerRemoteServiceCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<int> e)
        {
            try
            {
                GlobalData.Client.HandlePlayerRemoteServiceCompleted -= Client_HandlePlayerRemoteServiceCompleted;
                App.BusyToken.CloseBusyWindow();

                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("提交玩家远程服务处理信息失败，服务器返回异常。异常信息为：" + e.Error.Message);
                    return;
                }

                if (e.Result == OperResult.RESULTCODE_TRUE)
                {
                    MyMessageBox.ShowInfo("提交玩家远程服务处理信息成功");
                    this.Close();
                }
                else
                {
                    MyMessageBox.ShowInfo("提交玩家远程服务处理信息失败。错误信息为：" + OperResult.GetMsg(e.Result));
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("提交玩家远程服务处理信息失败，服务器回调异常。信息为：" + exc.Message);
            }
        }
    }
}
