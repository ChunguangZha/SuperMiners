using MetaData;
using SuperMinersWPF.Models;
using SuperMinersWPF.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersWPF.ViewModels
{
    public class StoneFactoryViewModel : BaseModel
    {
        private PlayerStoneFactoryAccountInfoUIModel _factoryAccount;

        public PlayerStoneFactoryAccountInfoUIModel FactoryAccount
        {
            get { return _factoryAccount; }
            set
            {
                _factoryAccount = value;
                NotifyPropertyChange("FactoryAccount");
            }
        }

        private ObservableCollection<StoneFactorySystemDailyProfitUIModel> _listSystemDailyProfit = new ObservableCollection<StoneFactorySystemDailyProfitUIModel>();

        public ObservableCollection<StoneFactorySystemDailyProfitUIModel> ListSystemDailyProfit
        {
            get { return _listSystemDailyProfit; }
        }

        private ObservableCollection<StoneFactoryProfitRMBChangedRecordUIModel> _listProfitRecords = new ObservableCollection<StoneFactoryProfitRMBChangedRecordUIModel>();

        public ObservableCollection<StoneFactoryProfitRMBChangedRecordUIModel> ListProfitRecords
        {
            get { return _listProfitRecords; }
        }

        public void AsyncGetPlayerFactoryAccountInfo()
        {
            App.BusyToken.ShowBusyWindow("正在获取玩家工厂账户信息");
            GlobalData.Client.GetPlayerStoneFactoryAccountInfo(GlobalData.CurrentUser.UserID);
        }

        public void AsyncGetStoneFactoryProfitRMBChangedRecordList(MyDateTime beginTime, MyDateTime endTime, int pageItemCount, int pageIndex)
        {
            App.BusyToken.ShowBusyWindow("正在获取玩家收益历史记录");
            GlobalData.Client.GetStoneFactoryProfitRMBChangedRecordList(GlobalData.CurrentUser.UserID,  beginTime, endTime, pageItemCount, pageIndex);
        }

        public void AsyncGetStoneFactorySystemDailyProfitList(int pageItemCount, int pageIndex)
        {
            App.BusyToken.ShowBusyWindow("正在获取玩家收益历史记录");
            GlobalData.Client.GetStoneFactorySystemDailyProfitList(pageItemCount, pageIndex);
        }

        public void AsyncAddStoneToFactory(int stoneStackCount)
        {
            App.BusyToken.ShowBusyWindow("正在向工厂中添加矿石");
            GlobalData.Client.AddStoneToFactory(stoneStackCount);
        }

        public void AsyncWithdrawOutputRMBFromFactory(decimal withdrawRMB)
        {
            App.BusyToken.ShowBusyWindow("正在从工厂中提取灵币");
            GlobalData.Client.WithdrawOutputRMBFromFactory(withdrawRMB);
        }

        public void AsyncWithdrawStoneFromFactory(int withdrawStoneStack)
        {
            App.BusyToken.ShowBusyWindow("正在从工厂中取回矿石");
            GlobalData.Client.WithdrawStoneFromFactory(withdrawStoneStack);
        }

        public void AsyncAddMinersToFactory(int minersGroupCount)
        {
            App.BusyToken.ShowBusyWindow("正在向工厂增加苦力");
            GlobalData.Client.AddMinersToFactory(minersGroupCount);
        }

        public void AsyncFeedSlave()
        {
            App.BusyToken.ShowBusyWindow("投喂苦力");
            GlobalData.Client.FeedSlave();
        }

        public StoneFactoryViewModel()
        {
            GlobalData.Client.GetStoneFactoryProfitRMBChangedRecordListCompleted += Client_GetStoneFactoryProfitRMBChangedRecordListCompleted;
            GlobalData.Client.GetStoneFactorySystemDailyProfitListCompleted += Client_GetStoneFactorySystemDailyProfitListCompleted;
            GlobalData.Client.GetPlayerStoneFactoryAccountInfoCompleted += Client_GetPlayerStoneFactoryAccountInfoCompleted;
            GlobalData.Client.AddStoneToFactoryCompleted += Client_AddStoneToFactoryCompleted;
            GlobalData.Client.WithdrawOutputRMBFromFactoryCompleted += Client_WithdrawOutputRMBFromFactoryCompleted;
            GlobalData.Client.WithdrawStoneFromFactoryCompleted += Client_WithdrawStoneFromFactoryCompleted;
            GlobalData.Client.AddMinersToFactoryCompleted += Client_AddMinersToFactoryCompleted;
            GlobalData.Client.FeedSlaveCompleted += Client_FeedSlaveCompleted;
        }

        void Client_FeedSlaveCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<int> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("投喂苦力异常。原因为：" + e.Error.Message);
                    return;
                }
                if (e.Result == OperResult.RESULTCODE_TRUE)
                {
                    MyMessageBox.ShowInfo("投喂苦力成功");
                    this.AsyncGetPlayerFactoryAccountInfo();
                }
                else
                {
                    MyMessageBox.ShowInfo("投喂苦力失败。原因为：" + OperResult.GetMsg(e.Result));
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("投喂苦力返回处理异常。原因为：" + exc.Message);
            }
        }

        void Client_AddMinersToFactoryCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<int> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("向工厂中增加苦力异常。原因为：" + e.Error.Message);
                    return;
                }
                if (e.Result == OperResult.RESULTCODE_TRUE)
                {
                    MyMessageBox.ShowInfo("成功向工厂增加苦力");
                    this.AsyncGetPlayerFactoryAccountInfo();
                    App.UserVMObject.AsyncGetPlayerInfo();
                }
                else
                {
                    MyMessageBox.ShowInfo("向工厂中增加苦力失败。原因为：" + OperResult.GetMsg(e.Result));
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("向工厂中增加苦力返回处理异常。原因为：" + exc.Message);
            }
        }

        void Client_WithdrawStoneFromFactoryCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<int> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("从工厂中取回矿石异常。原因为：" + e.Error.Message);
                    return;
                }
                if (e.Result == OperResult.RESULTCODE_TRUE)
                {
                    MyMessageBox.ShowInfo("成功从工厂中取回矿石");
                    this.AsyncGetPlayerFactoryAccountInfo();
                    App.UserVMObject.AsyncGetPlayerInfo();
                }
                else
                {
                    MyMessageBox.ShowInfo("从工厂中取回矿石失败。原因为：" + OperResult.GetMsg(e.Result));
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("从工厂中取回矿石返回处理异常。原因为：" + exc.Message);
            }
        }

        void Client_WithdrawOutputRMBFromFactoryCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<int> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("从工厂中提取灵币异常。原因为：" + e.Error.Message);
                    return;
                }
                if (e.Result == OperResult.RESULTCODE_TRUE)
                {
                    MyMessageBox.ShowInfo("成功从工厂中提取灵币");
                    this.AsyncGetPlayerFactoryAccountInfo();
                    App.UserVMObject.AsyncGetPlayerInfo();
                }
                else
                {
                    MyMessageBox.ShowInfo("从工厂中提取灵币失败。原因为：" + OperResult.GetMsg(e.Result));
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("从工厂中提取灵币返回处理异常。原因为：" + exc.Message);
            }
        }

        void Client_AddStoneToFactoryCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<int> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("向工厂中添加矿石异常。原因为：" + e.Error.Message);
                    return;
                }
                if (e.Result == OperResult.RESULTCODE_TRUE)
                {
                    MyMessageBox.ShowInfo("成功向工厂添加矿石");
                    this.AsyncGetPlayerFactoryAccountInfo();
                    App.UserVMObject.AsyncGetPlayerInfo();
                }
                else
                {
                    MyMessageBox.ShowInfo("向工厂中添加矿石失败。原因为：" + OperResult.GetMsg(e.Result));
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("向工厂中添加矿石返回处理异常。原因为：" + exc.Message);
            }
        }

        void Client_GetPlayerStoneFactoryAccountInfoCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<MetaData.StoneFactory.PlayerStoneFactoryAccountInfo> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("获取工厂账户信息异常。原因为：" + e.Error.Message);
                    return;
                }
                if (e.Result == null || !e.Result.FactoryIsOpening || e.Result.FactoryLiveDays == 0)
                {
                    MyMessageBox.ShowInfo("加工厂大门关闭，请使用钥匙开启工厂（积分商城有售）。");
                }
                else
                {
                    if (this.FactoryAccount == null)
                    {
                        this.FactoryAccount = new PlayerStoneFactoryAccountInfoUIModel(e.Result);
                    }
                    else
                    {
                        this.FactoryAccount.ParentObject = e.Result;
                    }

                    if (PlayerFactoryAccountGetCompleted != null)
                    {
                        PlayerFactoryAccountGetCompleted();
                    }
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("获取工厂账户信息返回处理异常。原因为：" + exc.Message);
            }
        }

        void Client_GetStoneFactorySystemDailyProfitListCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<MetaData.StoneFactory.StoneFactorySystemDailyProfit[]> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("获取工厂每日收益信息异常。原因为：" + e.Error.Message);
                    return;
                }
                this.ListSystemDailyProfit.Clear();
                if (e.Result != null && e.Result.Length != 0)
                {
                    foreach (var item in e.Result)
                    {
                        this.ListSystemDailyProfit.Add(new StoneFactorySystemDailyProfitUIModel(item));
                    }

                    GlobalData.CurrentUser.YesterdayFactoryProfitRate = e.Result[0].profitRate;
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("获取工厂每日收益信息返回处理异常。原因为：" + exc.Message);
            }
        }

        void Client_GetStoneFactoryProfitRMBChangedRecordListCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<MetaData.StoneFactory.StoneFactoryProfitRMBChangedRecord[]> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("获取玩家收益历史异常。原因为：" + e.Error.Message);
                    return;
                }
                this.ListProfitRecords.Clear();
                if (e.Result != null)
                {
                    foreach (var item in e.Result)
                    {
                        this.ListProfitRecords.Add(new StoneFactoryProfitRMBChangedRecordUIModel(item));
                    }
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("获取玩家收益历史返回处理异常。原因为：" + exc.Message);
            }
        }

        public event Action PlayerFactoryAccountGetCompleted;
    }
}
