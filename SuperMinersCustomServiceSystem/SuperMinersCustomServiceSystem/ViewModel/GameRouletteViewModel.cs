using MetaData;
using MetaData.Game.Roulette;
using SuperMinersCustomServiceSystem.Model;
using SuperMinersCustomServiceSystem.Uility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersCustomServiceSystem.ViewModel
{
    public class GameRouletteViewModel : BaseViewModel
    {
        public override string MenuHeader
        {
            get
            {
                return "幸运大转盘";
            }
        }

        private ObservableCollection<RouletteAwardItemUIModel> _listCurrentRouletteAwardItems = new ObservableCollection<RouletteAwardItemUIModel>();

        public ObservableCollection<RouletteAwardItemUIModel> ListCurrentRouletteAwardItems
        {
            get { return _listCurrentRouletteAwardItems; }
            set { _listCurrentRouletteAwardItems = value; }
        }
        
        private ObservableCollection<RouletteAwardItemUIModel> _listAllRouletteAwardItems = new ObservableCollection<RouletteAwardItemUIModel>();

        public ObservableCollection<RouletteAwardItemUIModel> ListAllRouletteAwardItems
        {
            get { return _listAllRouletteAwardItems; }
            set { _listAllRouletteAwardItems = value; }
        }

        private ObservableCollection<RouletteAwardItemUIModel> _listToComboxRouletteAwardItems = new ObservableCollection<RouletteAwardItemUIModel>();

        public ObservableCollection<RouletteAwardItemUIModel> ListToComboxRouletteAwardItems
        {
            get { return _listToComboxRouletteAwardItems; }
            set { _listToComboxRouletteAwardItems = value; }
        }

        private ObservableCollection<RouletteWinnerRecordUIModel> _listNotPayRouletteWinnerRecords = new ObservableCollection<RouletteWinnerRecordUIModel>();

        public ObservableCollection<RouletteWinnerRecordUIModel> ListNotPayRouletteWinnerRecords
        {
            get { return _listNotPayRouletteWinnerRecords; }
            set { _listNotPayRouletteWinnerRecords = value; }
        }

        private ObservableCollection<RouletteWinnerRecordUIModel> _listAllPayRouletteWinnerRecords = new ObservableCollection<RouletteWinnerRecordUIModel>();

        public ObservableCollection<RouletteWinnerRecordUIModel> ListAllPayRouletteWinnerRecords
        {
            get { return _listAllPayRouletteWinnerRecords; }
            set { _listAllPayRouletteWinnerRecords = value; }
        }

        private ObservableCollection<RouletteRoundInfoUIModel> _listRoundRecords = new ObservableCollection<RouletteRoundInfoUIModel>();

        public ObservableCollection<RouletteRoundInfoUIModel> ListRoundRecords
        {
            get { return _listRoundRecords; }
            set { _listRoundRecords = value; }
        }


        public GameRouletteViewModel()
        {
            GlobalData.Client.GetAllAwardItemsCompleted += Client_GetAllAwardItemsCompleted;
            GlobalData.Client.GetCurrentAwardItemsCompleted += Client_GetCurrentAwardItemsCompleted;
            GlobalData.Client.SetCurrentAwardItemsCompleted += Client_SetCurrentAwardItemsCompleted;
            GlobalData.Client.GetNotPayWinAwardRecordsCompleted += Client_GetNotPayWinAwardRecordsCompleted;
            GlobalData.Client.GetAllPayWinAwardRecordsCompleted += Client_GetAllPayWinAwardRecordsCompleted;
            GlobalData.Client.OnSomebodyWinRouletteAward += Client_OnSomebodyWinRouletteAward;
            GlobalData.Client.DeleteAwardItemCompleted += Client_DeleteAwardItemCompleted;
            GlobalData.Client.GetAllRouletteRoundInfoCompleted += Client_GetAllRouletteRoundInfoCompleted;
        }

        void Client_GetAllRouletteRoundInfoCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<RouletteRoundInfo[]> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("查询失败，服务器返回异常。异常信息：" + e.Error.Message);
                    return;
                }

                this.ListRoundRecords.Clear();
                if (e.Result != null)
                {
                    foreach (var item in e.Result.OrderByDescending(r=>r.StartTime))
                    {
                        this.ListRoundRecords.Add(new RouletteRoundInfoUIModel(item));
                    }
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("查询失败，返回后处理异常。异常信息：" + exc.Message);
            }
        }

        void Client_DeleteAwardItemCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<int> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("删除幸运大转盘奖项时，服务器返回异常。异常信息：" + e.Error.Message);
                    return;
                }

                if (e.Result == OperResult.RESULTCODE_TRUE)
                {
                    MyMessageBox.ShowInfo("删除成功");
                    this.AsyncGetAllAwardItems();
                }
                else
                {
                    MyMessageBox.ShowInfo("删除奖项失败，可能是奖励被使用。" + OperResult.GetMsg(e.Result));
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("删除幸运大转盘奖项时，返回后处理异常。异常信息：" + exc.Message);
            }
        }

        void Client_OnSomebodyWinRouletteAward(RouletteWinnerRecord obj)
        {
            try
            {
                if (obj == null)
                {
                    MyMessageBox.ShowInfo("幸运大转盘开奖，但服务器推送结果为空");
                    return;
                }

                this.ListNotPayRouletteWinnerRecords.Add(new RouletteWinnerRecordUIModel(obj));
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("幸运大转盘开奖，服务器推送后处理异常。异常信息：" + exc.Message);
            }
        }

        void Client_GetAllPayWinAwardRecordsCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<MetaData.Game.Roulette.RouletteWinnerRecord[]> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("查询幸运大转盘中奖历史记录，服务器返回异常。异常信息：" + e.Error.Message);
                    return;
                }

                this.ListAllPayRouletteWinnerRecords.Clear();
                if (e.Result != null)
                {
                    foreach (var item in e.Result)
                    {
                        this.ListAllPayRouletteWinnerRecords.Add(new RouletteWinnerRecordUIModel(item));
                    }
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("查询幸运大转盘中奖历史记录，返回后处理异常。异常信息：" + exc.Message);
            }
        }

        void Client_GetNotPayWinAwardRecordsCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<MetaData.Game.Roulette.RouletteWinnerRecord[]> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("查询幸运大转盘中奖未处理记录，服务器返回异常。异常信息：" + e.Error.Message);
                    return;
                }

                this.ListNotPayRouletteWinnerRecords.Clear();
                if (e.Result != null)
                {
                    foreach (var item in e.Result)
                    {
                        this.ListNotPayRouletteWinnerRecords.Add(new RouletteWinnerRecordUIModel(item));
                    }
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("查询幸运大转盘中奖未处理记录，返回后处理异常。异常信息：" + exc.Message);
            }
        }

        void Client_SetCurrentAwardItemsCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<bool> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("保存所有奖项信息，服务器返回异常。异常信息：" + e.Error.Message);
                    return;
                }

                if (e.Result)
                {
                    MyMessageBox.ShowInfo("保存所有奖项信息成功");
                    this.AsyncGetCurrentAwardItems();
                }
                else
                {
                    MyMessageBox.ShowInfo("保存所有奖项信息失败。");
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("保存所有奖项信息，返回后处理异常。异常信息：" + exc.Message);
            }
        }

        void Client_GetCurrentAwardItemsCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<MetaData.Game.Roulette.RouletteAwardItem[]> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("查询幸运大转盘当前展示奖项时，服务器返回异常。异常信息：" + e.Error.Message);
                    return;
                }

                this.ListCurrentRouletteAwardItems.Clear();
                if (e.Result != null)
                {
                    foreach (var item in e.Result)
                    {
                        this.ListCurrentRouletteAwardItems.Add(new RouletteAwardItemUIModel(item));
                    }
                }

                if (GetCurrentAwardItemCompleted != null)
                {
                    GetCurrentAwardItemCompleted();
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("查询幸运大转盘当前展示奖项时，返回后处理异常。异常信息：" + exc.Message);
            }
        }

        void Client_GetAllAwardItemsCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<RouletteAwardItem[]> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("查询幸运大转盘所有奖项时，服务器返回异常。异常信息：" + e.Error.Message);
                    return;
                }

                this.ListAllRouletteAwardItems.Clear();
                this.ListToComboxRouletteAwardItems.Clear();
                this.ListToComboxRouletteAwardItems.Add(new RouletteAwardItemUIModel(new RouletteAwardItem()
                {
                    ID = -1,
                    AwardName = "全部"
                }));
                if (e.Result != null)
                {
                    foreach (var item in e.Result)
                    {
                        this.ListAllRouletteAwardItems.Add(new RouletteAwardItemUIModel(item));
                        ListToComboxRouletteAwardItems.Add(new RouletteAwardItemUIModel(item));
                    }
                }

                if (GetAllAwardItemCompleted != null)
                {
                    GetAllAwardItemCompleted();
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("查询幸运大转盘所有奖项时，返回后处理异常。异常信息：" + exc.Message);
            }
        }

        public void RemoveNotPayWinAwardItem(RouletteWinnerRecordUIModel record)
        {
            this.ListNotPayRouletteWinnerRecords.Remove(record);
        }

        public void AsyncGetAllAwardItems()
        {
            App.BusyToken.ShowBusyWindow("正在加载幸运大转盘奖项");
            GlobalData.Client.GetAllAwardItems();
        }

        public void AsyncGetCurrentAwardItems()
        {
            App.BusyToken.ShowBusyWindow("正在加载幸运大转盘奖项");
            GlobalData.Client.GetCurrentAwardItems();
        }

        public void AsyncDeleteAwardItem(RouletteAwardItem item)
        {
            App.BusyToken.ShowBusyWindow("正在删除幸运大转盘奖项");
            GlobalData.Client.DeleteAwardItem(item);
        }

        public void AsyncGetNotPayWinAwardRecords()
        {
            App.BusyToken.ShowBusyWindow("正在加载幸运大转盘未支付的中奖记录");
            GlobalData.Client.GetNotPayWinAwardRecords();
        }

        public void AsyncGetAllPayWinAwardRecords(string UserName, int RouletteAwardItemID, MyDateTime BeginWinTime, MyDateTime EndWinTime, int IsGot, int IsPay, int pageItemCount, int pageIndex)
        {
            App.BusyToken.ShowBusyWindow("正在查询幸运大转盘中奖记录");
            GlobalData.Client.GetAllPayWinAwardRecords(UserName, RouletteAwardItemID, BeginWinTime, EndWinTime, IsGot, IsPay, pageItemCount, pageIndex);
        }

        public void AsyncSetCurrentAwardItem(RouletteAwardItem[] items)
        {
            App.BusyToken.ShowBusyWindow("正在保存所有奖项信息");
            GlobalData.Client.SetCurrentAwardItems(items);
        }

        public void AsyncGetAllRouletteRoundInfo()
        {
            App.BusyToken.ShowBusyWindow("正在查询");
            GlobalData.Client.GetAllRouletteRoundInfo();
        }

        public event Action GetAllAwardItemCompleted;
        public event Action GetCurrentAwardItemCompleted;
    }
}
