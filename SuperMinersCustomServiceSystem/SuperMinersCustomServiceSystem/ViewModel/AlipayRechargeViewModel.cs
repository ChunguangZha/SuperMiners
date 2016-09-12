using MetaData;
using MetaData.Trade;
using SuperMinersCustomServiceSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SuperMinersCustomServiceSystem.ViewModel
{
    public class AlipayRechargeViewModel : BaseViewModel
    {
        public override string MenuHeader
        {
            get
            {
                return "支付宝充值";
            }
        }

        private object lockList = new object();
        private ObservableCollection<AlipayRechargeRecordUIModel> _listExceptionAlipayRecords = new ObservableCollection<AlipayRechargeRecordUIModel>();

        public ObservableCollection<AlipayRechargeRecordUIModel> ListExceptionAlipayRecords
        {
            get
            {
                return _listExceptionAlipayRecords;
            }
        }

        public AlipayRechargeViewModel()
        {
            GlobalData.Client.GetAllExceptionAlipayRechargeRecordsCompleted += Client_GetAllExceptionAlipayRechargeRecordsCompleted;
            GlobalData.Client.HandleExceptionAlipayRechargeRecordCompleted += Client_HandleExceptionAlipayRechargeRecordCompleted;
        }

        void Client_HandleExceptionAlipayRechargeRecordCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<int> e)
        {
            App.BusyToken.CloseBusyWindow();
            if (e.Error != null)
            {
                MessageBox.Show("服务器连接失败。" + e.Error);
                return;
            }
            string orderNumber = e.UserState as string;

            if (e.Result == OperResult.RESULTCODE_TRUE)
            {
                MessageBox.Show("处理异常的支付宝订单成功，商品订单号为：" + orderNumber);
                lock (lockList)
                {
                    for (int i = 0; i < this.ListExceptionAlipayRecords.Count; i++)
                    {
                        if (this.ListExceptionAlipayRecords[i].out_trade_no == orderNumber)
                        {
                            this.ListExceptionAlipayRecords.RemoveAt(i);
                            break;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("处理异常的支付宝订单失败，原因为：" + OperResult.GetMsg(e.Result));
            }
        }

        void Client_GetAllExceptionAlipayRechargeRecordsCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<MetaData.Trade.AlipayRechargeRecord[]> e)
        {
            App.BusyToken.CloseBusyWindow();
            if (e.Error != null)
            {
                MessageBox.Show("服务器连接失败。" + e.Error);
                return;
            }
            this.ListExceptionAlipayRecords.Clear();
            if (e.Result != null)
            {
                foreach (var item in e.Result)
                {
                    this.ListExceptionAlipayRecords.Add(new AlipayRechargeRecordUIModel(item));
                }
            }
        }

        public void AsyncGetAllExceptionAlipayRechargeRecords()
        {
            if (GlobalData.Client != null)
            {
                App.BusyToken.ShowBusyWindow("正在加载异常的支付宝支付记录...");
                GlobalData.Client.GetAllExceptionAlipayRechargeRecords();
            }
        }

        public void AsyncHandleExceptionAlipayRechargeRecord(AlipayRechargeRecord alipayrecord)
        {
            if (GlobalData.Client != null)
            {
                App.BusyToken.ShowBusyWindow("正在提交数据...");
                GlobalData.Client.HandleExceptionAlipayRechargeRecord(alipayrecord, alipayrecord.out_trade_no);
            }
        }
    }
}
