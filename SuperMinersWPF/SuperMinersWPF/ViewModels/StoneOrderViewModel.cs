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
    class StoneOrderViewModel
    {
        private ObservableCollection<SellStonesOrderUIModel> _allNotFinishStonesOrder = new ObservableCollection<SellStonesOrderUIModel>();

        public ObservableCollection<SellStonesOrderUIModel> AllNotFinishStonesOrder
        {
            get { return _allNotFinishStonesOrder; }
        }
        
        public void AsyncGetNotFinishedStonesOrder()
        {
            GlobalData.Client.GetNotFinishedStonesOrder(null);
        }

        public void RegisterEvent()
        {
            GlobalData.Client.GetNotFinishedStonesOrderCompleted += Client_GetNotFinishedStonesOrderCompleted;
        }

        void Client_GetNotFinishedStonesOrderCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<MetaData.SellStonesOrder[]> e)
        {
            if (e.Cancelled)
            {
                return;
            }

            if (e.Error != null || e.Result == null)
            {
                MyMessageBox.ShowInfo("获取矿石卖单失败。");
                return;
            }

            this.AllNotFinishStonesOrder.Clear();
            for (int i = 0; i < e.Result.Length; i++)
            {
                var item = e.Result[i];
                this.AllNotFinishStonesOrder.Add(new SellStonesOrderUIModel(item));
            }
        }
    }
}
