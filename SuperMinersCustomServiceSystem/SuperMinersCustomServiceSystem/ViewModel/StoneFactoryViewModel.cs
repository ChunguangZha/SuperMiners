using MetaData.StoneFactory;
using MetaData.SystemConfig;
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
    public class StoneFactoryViewModel : BaseModel
    {
        private ObservableCollection<StoneFactorySystemDailyProfitUIModel> _listSystemDailyProfit = new ObservableCollection<StoneFactorySystemDailyProfitUIModel>();

        public ObservableCollection<StoneFactorySystemDailyProfitUIModel> ListSystemDailyProfit
        {
            get { return _listSystemDailyProfit; }
        }

        private ObservableCollection<PlayerStoneFactoryAccountInfoUIModel> _listPlayerFactoryInfo = new ObservableCollection<PlayerStoneFactoryAccountInfoUIModel>();

        public ObservableCollection<PlayerStoneFactoryAccountInfoUIModel> ListPlayerFactoryInfo
        {
            get { return _listPlayerFactoryInfo; }
        }

        private ObservableCollection<PlayerStoneFactoryAccountInfoUIModel> _listFiltedPlayerFactoryInfos = new ObservableCollection<PlayerStoneFactoryAccountInfoUIModel>();

        public ObservableCollection<PlayerStoneFactoryAccountInfoUIModel> ListFiltedPlayerFactoryInfos
        {
            get { return _listFiltedPlayerFactoryInfos; }
        }



        private decimal _sumAllFactory_FreezingStoneCount;

        public decimal SumAllFactory_FreezingStoneCount
        {
            get { return _sumAllFactory_FreezingStoneCount; }
            set
            {
                _sumAllFactory_FreezingStoneCount = value;
                NotifyPropertyChange("SumAllFactory_FreezingStoneCount");
            }
        }

        private decimal _sumAllFactory_WorkableStoneCount;

        public decimal SumAllFactory_WorkableStoneCount
        {
            get { return _sumAllFactory_WorkableStoneCount; }
            set
            {
                _sumAllFactory_WorkableStoneCount = value;
                NotifyPropertyChange("SumAllFactory_WorkableStoneCount");
            }
        }

        private decimal _sumAllFactory_WithdrawableStoneCount;

        public decimal SumAllFactory_WithdrawableStoneCount
        {
            get { return _sumAllFactory_WithdrawableStoneCount; }
            set
            {
                _sumAllFactory_WithdrawableStoneCount = value;
                NotifyPropertyChange("SumAllFactory_WithdrawableStoneCount");
            }
        }

        private decimal _sumAllFactory_YesterdayValidStoneCount;

        public decimal SumAllFactory_YesterdayValidStoneCount
        {
            get { return _sumAllFactory_YesterdayValidStoneCount; }
            set
            {
                _sumAllFactory_YesterdayValidStoneCount = value;
                NotifyPropertyChange("SumAllFactory_YesterdayValidStoneCount");
            }
        }

        private decimal _sumAllFactory_FreezingMinersCount;

        public decimal SumAllFactory_FreezingMinersCount
        {
            get { return _sumAllFactory_FreezingMinersCount; }
            set
            {
                _sumAllFactory_FreezingMinersCount = value;
                NotifyPropertyChange("SumAllFactory_FreezingMinersCount");
            }
        }

        private decimal _sumAllFactory_WorkingMinersCount;

        public decimal SumAllFactory_WorkingMinersCount
        {
            get { return _sumAllFactory_WorkingMinersCount; }
            set
            {
                _sumAllFactory_WorkingMinersCount = value;
                NotifyPropertyChange("SumAllFactory_WorkingMinersCount");
            }
        }

        private decimal _sumAllFactory_Foods;

        public decimal SumAllFactory_Foods
        {
            get { return _sumAllFactory_Foods; }
            set
            {
                _sumAllFactory_Foods = value;
                NotifyPropertyChange("SumAllFactory_Foods");
            }
        }

        private decimal _sumAllFactory_TotalProfit;

        public decimal SumAllFactory_TotalProfit
        {
            get { return _sumAllFactory_TotalProfit; }
            set
            {
                _sumAllFactory_TotalProfit = value;
                NotifyPropertyChange("SumAllFactory_TotalProfit");
            }
        }

        private decimal _sumAllFactory_YesterdayProfit;

        public decimal SumAllFactory_YesterdayProfit
        {
            get { return _sumAllFactory_YesterdayProfit; }
            set
            {
                _sumAllFactory_YesterdayProfit = value;
                NotifyPropertyChange("SumAllFactory_YesterdayProfit");
            }
        }

        


        private StoneFactorySystemDailyProfitUIModel _yesterdayProfit;

        public StoneFactorySystemDailyProfitUIModel YesterdayProfit
        {
            get { return _yesterdayProfit; }
            set { _yesterdayProfit = value; }
        }

        private bool _setProfitEnable = true;

        public bool SetProfitEnable
        {
            get { return _setProfitEnable; }
            set
            {
                _setProfitEnable = value;
                NotifyPropertyChange("SetProfitEnable");
            }
        }


        public void AsyncGetStoneFactorySystemDailyProfitList(int pageItemCount, int pageIndex)
        {
            App.BusyToken.ShowBusyWindow("正在加载加工厂收益信息");
            GlobalData.Client.GetStoneFactorySystemDailyProfitList(pageItemCount, pageIndex);
        }

        public void AsyncGetYesterdayFactoryProfitRate()
        {
            App.BusyToken.ShowBusyWindow("正在加载加工厂收益信息");
            GlobalData.Client.GetYesterdayFactoryProfitRate();
        }

        public void AsyncGetAllPlayerStoneFactoryAccountInfos()
        {
            App.BusyToken.ShowBusyWindow("正在加载加工厂账户信息");
            GlobalData.Client.GetAllPlayerStoneFactoryAccountInfos();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="factoryOpenState">0全部；1开启中；2已关闭</param>
        public void FiltePlayerFactoryAccount(string userName, int factoryOpenState)
        {
            this.ListFiltedPlayerFactoryInfos.Clear();
            this.SumAllFactory_Foods = 0;
            this.SumAllFactory_FreezingMinersCount = 0;
            this.SumAllFactory_FreezingStoneCount = 0;
            this.SumAllFactory_TotalProfit = 0;
            this.SumAllFactory_WithdrawableStoneCount = 0;
            this.SumAllFactory_WorkableStoneCount = 0;
            this.SumAllFactory_WorkingMinersCount = 0;
            this.SumAllFactory_YesterdayProfit = 0;
            this.SumAllFactory_YesterdayValidStoneCount = 0;

            foreach (var item in this.ListPlayerFactoryInfo)
            {
                bool isFiltedNameOK = false;
                bool isFiltedStateOK = false;

                if (string.IsNullOrEmpty(userName))
                {
                    isFiltedNameOK = true;
                }
                else
                {
                    if (item.UserName.Contains(userName))
                    {
                        isFiltedNameOK = true;
                    }
                }

                if (factoryOpenState == 0)
                {
                    isFiltedStateOK = true;
                }
                else if (factoryOpenState == 1)
                {
                    if (item.FactoryIsOpening)
                    {
                        isFiltedStateOK = true;
                    }
                }
                else if (factoryOpenState == 2)
                {
                    if (!item.FactoryIsOpening)
                    {
                        isFiltedStateOK = true;
                    }
                }

                if (isFiltedNameOK && isFiltedStateOK)
                {
                    this.ListFiltedPlayerFactoryInfos.Add(item);

                    this.SumAllFactory_Foods += item.Food;
                    this.SumAllFactory_FreezingMinersCount += item.FreezingSlavesCount;
                    this.SumAllFactory_FreezingStoneCount += item.FreezingStoneCount;
                    this.SumAllFactory_TotalProfit += item.TotalProfitRMB;
                    this.SumAllFactory_WithdrawableStoneCount += item.WithdrawableStoneCount;
                    this.SumAllFactory_WorkableStoneCount += item.TotalStoneCount;
                    this.SumAllFactory_WorkingMinersCount += item.EnableSlavesCount;
                    this.SumAllFactory_YesterdayProfit += item.YesterdayTotalProfitRMB;
                    this.SumAllFactory_YesterdayValidStoneCount += (item.LastDayValidStoneStack * StoneFactoryConfig.StoneFactoryStone_Stack);

                }
            }
        }

        public StoneFactoryViewModel()
        {
            GlobalData.Client.GetStoneFactorySystemDailyProfitListCompleted += Client_GetStoneFactorySystemDailyProfitListCompleted;
            GlobalData.Client.GetYesterdayFactoryProfitRateCompleted += Client_GetYesterdayFactoryProfitRateCompleted;
            GlobalData.Client.GetAllPlayerStoneFactoryAccountInfosCompleted += Client_GetAllPlayerStoneFactoryAccountInfosCompleted;
        }

        void Client_GetAllPlayerStoneFactoryAccountInfosCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<PlayerStoneFactoryAccountInfo[]> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("加载加工厂账户信息异常。原因为：" + e.Error.Message);
                    return;
                }
                this.ListPlayerFactoryInfo.Clear();
                this.ListFiltedPlayerFactoryInfos.Clear();
                this.SumAllFactory_Foods = 0;
                this.SumAllFactory_FreezingMinersCount = 0;
                this.SumAllFactory_FreezingStoneCount = 0;
                this.SumAllFactory_TotalProfit = 0;
                this.SumAllFactory_WithdrawableStoneCount = 0;
                this.SumAllFactory_WorkableStoneCount = 0;
                this.SumAllFactory_WorkingMinersCount = 0;
                this.SumAllFactory_YesterdayProfit = 0;
                this.SumAllFactory_YesterdayValidStoneCount = 0;

                if (e.Result != null && e.Result.Length != 0)
                {
                    foreach (var item in e.Result)
                    {
                        var factory = new PlayerStoneFactoryAccountInfoUIModel(item);
                        this.ListPlayerFactoryInfo.Add(factory);
                        this.ListFiltedPlayerFactoryInfos.Add(factory);
                        this.SumAllFactory_Foods += item.Food;
                        this.SumAllFactory_FreezingMinersCount += factory.FreezingSlavesCount;
                        this.SumAllFactory_FreezingStoneCount += factory.FreezingStoneCount;
                        this.SumAllFactory_TotalProfit += factory.TotalProfitRMB;
                        this.SumAllFactory_WithdrawableStoneCount += factory.WithdrawableStoneCount;
                        this.SumAllFactory_WorkableStoneCount += factory.TotalStoneCount;
                        this.SumAllFactory_WorkingMinersCount += factory.EnableSlavesCount;
                        this.SumAllFactory_YesterdayProfit += factory.YesterdayTotalProfitRMB;
                        this.SumAllFactory_YesterdayValidStoneCount += (factory.LastDayValidStoneStack * StoneFactoryConfig.StoneFactoryStone_Stack);
                    }
                }

            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("获取工厂每日收益信息返回处理异常。原因为：" + exc.Message);
            }
        }

        void Client_GetYesterdayFactoryProfitRateCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<MetaData.StoneFactory.StoneFactorySystemDailyProfit> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("获取工厂每日收益信息异常1。原因为：" + e.Error.Message);
                    return;
                }
                if (e.Result != null)
                {
                    this.YesterdayProfit = new StoneFactorySystemDailyProfitUIModel(e.Result);
                    if ((DateTime.Now.Date - e.Result.Day.ToDateTime().Date).Days == 0)
                    {
                        SetProfitEnable = false;
                    }
                    else
                    {
                        SetProfitEnable = true;
                    }
                    if (this.ListSystemDailyProfit != null)
                    {
                        this.ListSystemDailyProfit.Insert(0, this.YesterdayProfit);
                    }
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("获取工厂每日收益信息返回处理异常1。原因为：" + exc.Message);
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
                }

                AsyncGetYesterdayFactoryProfitRate();
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("获取工厂每日收益信息返回处理异常。原因为：" + exc.Message);
            }
        }

    }
}
