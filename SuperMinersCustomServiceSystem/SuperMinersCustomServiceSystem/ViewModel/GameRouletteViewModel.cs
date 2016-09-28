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

        private ObservableCollection<RouletteAwardItemUIModel> _listRouletteAwardItems = new ObservableCollection<RouletteAwardItemUIModel>();

        public ObservableCollection<RouletteAwardItemUIModel> ListRouletteAwardItems
        {
            get { return _listRouletteAwardItems; }
            set { _listRouletteAwardItems = value; }
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

        public GameRouletteViewModel()
        {
            GlobalData.Client.GetAwardItemsCompleted += Client_GetAwardItemsCompleted;
            GlobalData.Client.SetAwardItemsCompleted += Client_SetAwardItemsCompleted;
            GlobalData.Client.GetNotPayWinAwardRecordsCompleted += Client_GetNotPayWinAwardRecordsCompleted;
            GlobalData.Client.GetAllPayWinAwardRecordsCompleted += Client_GetAllPayWinAwardRecordsCompleted;
            GlobalData.Client.OnSomebodyWinRouletteAward += Client_OnSomebodyWinRouletteAward;
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

        void Client_SetAwardItemsCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<bool> e)
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

        void Client_GetAwardItemsCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<MetaData.Game.Roulette.RouletteAwardItem[]> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("查询幸运大转盘奖项服务器返回异常。异常信息：" + e.Error.Message);
                    return;
                }

                this.ListRouletteAwardItems.Clear();
                if (e.Result != null)
                {
                    foreach (var item in e.Result)
                    {
                        this.ListRouletteAwardItems.Add(new RouletteAwardItemUIModel(item));
                    }
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("查询幸运大转盘奖项返回后处理异常。异常信息：" + exc.Message);
            }
        }

        public void RemoveNotPayWinAwardItem(RouletteWinnerRecordUIModel record)
        {
            this.ListNotPayRouletteWinnerRecords.Remove(record);
        }

        public void AsyncGetAwardItems()
        {
            App.BusyToken.ShowBusyWindow("正在加载幸运大转盘奖项");
            GlobalData.Client.GetAwardItems();
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

        public void AsyncSaveAllAwardItem()
        {
            App.BusyToken.ShowBusyWindow("正在保存所有奖项信息");
            List<RouletteAwardItem> listAwardItems = new List<RouletteAwardItem>();
            foreach (var item in this._listRouletteAwardItems)
            {
                listAwardItems.Add(item.ParentObject);
            }

            GlobalData.Client.SetAwardItems(listAwardItems.ToArray());
        }
    }
}
