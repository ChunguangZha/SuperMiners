using MetaData.StoneFactory;
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

        public StoneFactoryViewModel()
        {
            GlobalData.Client.GetStoneFactorySystemDailyProfitListCompleted += Client_GetStoneFactorySystemDailyProfitListCompleted;
            GlobalData.Client.GetYesterdayFactoryProfitRateCompleted += Client_GetYesterdayFactoryProfitRateCompleted;
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
