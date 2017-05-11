using MetaData;
using SuperMinersCustomServiceSystem.Model;
using SuperMinersCustomServiceSystem.Uility;
using SuperMinersCustomServiceSystem.View.Windows;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SuperMinersCustomServiceSystem.View.Controls
{
    /// <summary>
    /// Interaction logic for TransferServerManagerControl.xaml
    /// </summary>
    public partial class TransferServerManagerControl : UserControl
    {
        private System.Threading.SynchronizationContext _syn;
        private OldPlayerTransferRegisterInfoUIModel selectedPlayer = null;

        public TransferServerManagerControl()
        {
            InitializeComponent();
            _syn = SynchronizationContext.Current;
            BindUI();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void BindUI()
        {
            Binding bind = new Binding()
            {
                Source = App.PlayerVMObject.ListFilterPlayerTransferRecords
            };

            this.datagridPlayerInfos.SetBinding(DataGrid.ItemsSourceProperty, bind);

        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            this.txtUserName.Text = "";
            this.cmbTransferState.SelectedIndex = 1;
            App.PlayerVMObject.AsyncGetAllTransferPlayerRecords();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            App.PlayerVMObject.SearchTransferPlayers(this.txtUserName.Text.Trim(), cmbTransferState.SelectedIndex);
        }

        private void btnEditPlayerInfo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.datagridPlayerInfos.SelectedItem is OldPlayerTransferRegisterInfoUIModel)
                {
                    OldPlayerTransferRegisterInfoUIModel player = this.datagridPlayerInfos.SelectedItem as OldPlayerTransferRegisterInfoUIModel;

                    GlobalData.Client.GetPlayerCompleted += Client_GetPlayerCompleted;
                    GlobalData.Client.GetPlayer(player.UserName, "ViewPlayer");
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo(exc.Message);
            }
        }

        public void RefreshDB()
        {
            try
            {
                if (App.PlayerVMObject == null || GlobalData.CurrentAdmin == null)
                {
                    return;
                }

                App.PlayerVMObject.AsyncGetAllTransferPlayerRecords();
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo(exc.Message);
            }
        }

        void Client_GetPlayerCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<SuperMinersServerApplication.Model.PlayerInfoLoginWrap> e)
        {
            try
            {
                if (e.UserState == null)
                {
                    return;
                }
                string strState = e.UserState as string;
                if (strState == null)
                {
                    return;
                }

                GlobalData.Client.GetPlayerCompleted -= Client_GetPlayerCompleted;

                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("获取用户信息失败。原因为：" + e.Error.Message);
                    return;
                }
                if (e.Result == null)
                {
                    MyMessageBox.ShowInfo("获取用户信息失败。");
                    return;
                }
                if (strState == "ViewPlayer")
                {
                    this._syn.Post(o =>
                    {
                        EditPlayerWindow win = new EditPlayerWindow(new PlayerInfoUIModel(e.Result));
                        win.ShowDialog();

                    }, null);
                }
                else if (strState == "Transfer")
                {
                    if (selectedPlayer == null)
                    {
                        MyMessageBox.ShowInfo("没有选中的用户信息。");
                        return;
                    }
                    //if (e.Result.FortuneInfo.StockOfStones + e.Result.FortuneInfo.FreezingStones < 100000)
                    //{
                    //    MyMessageBox.ShowInfo("玩家["+e.Result.SimpleInfo.UserLoginName+"] 矿石量不足十万，无法进行转区！");
                    //    return;
                    //}
                    string serverUri2 = System.Configuration.ConfigurationManager.AppSettings["ServerUri2"];
                    GlobalData.Client.Init(serverUri2);
                    GlobalData.Client.TransferPlayerToCompleted += Client_TransferPlayerToCompleted;
                    GlobalData.Client.TransferPlayerTo(e.Result.SimpleInfo, e.Result.FortuneInfo, selectedPlayer.ParentObject.NewServerUserLoginName, selectedPlayer.ParentObject.NewServerPassword, null);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        void Client_TransferPlayerToCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<int> e)
        {
            try
            {
                string serverUri1 = System.Configuration.ConfigurationManager.AppSettings["ServerUri1"];
                GlobalData.Client.Init(serverUri1);
                
                GlobalData.Client.TransferPlayerToCompleted -= Client_TransferPlayerToCompleted;
                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("转区失败1，原因为：" + e.Error.Message);
                    return;
                }
                if (e.Result != OperResult.RESULTCODE_TRUE)
                {
                    MyMessageBox.ShowInfo("转区失败2，原因为：" + OperResult.GetMsg(e.Result));
                    return;
                }

                if (this.datagridPlayerInfos.SelectedItem is OldPlayerTransferRegisterInfoUIModel)
                {
                    OldPlayerTransferRegisterInfoUIModel player = this.datagridPlayerInfos.SelectedItem as OldPlayerTransferRegisterInfoUIModel;

                    GlobalData.Client.TransferPlayerFromCompleted += Client_TransferPlayerFromCompleted;
                    GlobalData.Client.TransferPlayerFrom(player.ID, player.UserName, GlobalData.CurrentAdmin.UserName);

                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo(exc.Message);
            }
        }

        void Client_TransferPlayerFromCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<int> e)
        {
            try
            {
                GlobalData.Client.TransferPlayerFromCompleted -= Client_TransferPlayerFromCompleted;

                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("转区失败3，原因：" + e.Error.Message);
                    return;
                }
                if (e.Result != OperResult.RESULTCODE_TRUE)
                {
                    MyMessageBox.ShowInfo("转区失败4，原因：" + OperResult.GetMsg(e.Result));
                    return;
                }

                MyMessageBox.ShowInfo("转区成功！");
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo(exc.Message);
            }
        }

        private void btnTransfer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.datagridPlayerInfos.SelectedItem is OldPlayerTransferRegisterInfoUIModel)
                {
                    selectedPlayer = this.datagridPlayerInfos.SelectedItem as OldPlayerTransferRegisterInfoUIModel;
                    if (selectedPlayer.isTransfered)
                    {
                        MyMessageBox.ShowInfo("玩家[" + selectedPlayer.UserName + "] 已经转区成功。");
                        return;
                    }

                    if (MyMessageBox.ShowQuestionOKCancel("请确认要将玩家[" + selectedPlayer.UserName + "]转区？") == System.Windows.Forms.DialogResult.OK)
                    {
                        if (string.IsNullOrEmpty(selectedPlayer.ParentObject.NewServerUserLoginName) || string.IsNullOrEmpty(selectedPlayer.ParentObject.NewServerPassword))
                        {
                            InputUserLoginNamePasswordWindow winInput = new InputUserLoginNamePasswordWindow();
                            if (winInput.ShowDialog() != true)
                            {
                                return;
                            }

                            selectedPlayer.ParentObject.NewServerUserLoginName = winInput.NewServerUserLoginName;
                            selectedPlayer.ParentObject.NewServerPassword = winInput.NewServerPassword;
                        }

                        GlobalData.Client.GetPlayerCompleted += Client_GetPlayerCompleted;
                        GlobalData.Client.GetPlayer(selectedPlayer.UserName, "Transfer");

                    }
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo(exc.Message);
            }
        }

    }
}
