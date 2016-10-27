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

        private ObservableCollection<AlipayRechargeRecordUIModel> _listAllAlipayRecords = new ObservableCollection<AlipayRechargeRecordUIModel>();

        public ObservableCollection<AlipayRechargeRecordUIModel> ListAllAlipayRecords
        {
            get { return _listAllAlipayRecords; }
        }

        private decimal _sumListAllAlipayRecords_PayYuan;

        public decimal SumListAllAlipayRecords_PayYuan
        {
            get { return _sumListAllAlipayRecords_PayYuan; }
            set
            {
                _sumListAllAlipayRecords_PayYuan = value;
                NotifyPropertyChanged("SumListAllAlipayRecords_PayYuan");
            }
        }

        private decimal _sumListAllAlipayRecords_RMB;

        public decimal SumListAllAlipayRecords_RMB
        {
            get { return _sumListAllAlipayRecords_RMB; }
            set
            {
                _sumListAllAlipayRecords_RMB = value;
                NotifyPropertyChanged("SumListAllAlipayRecords_RMB");
            }
        }



        public AlipayRechargeViewModel()
        {
            GlobalData.Client.GetAllExceptionAlipayRechargeRecordsCompleted += Client_GetAllExceptionAlipayRechargeRecordsCompleted;
            GlobalData.Client.HandleExceptionAlipayRechargeRecordCompleted += Client_HandleExceptionAlipayRechargeRecordCompleted;
            GlobalData.Client.GetAllAlipayRechargeRecordsCompleted += Client_GetAllAlipayRechargeRecordsCompleted;
        }

        void Client_GetAllAlipayRechargeRecordsCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<AlipayRechargeRecord[]> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MessageBox.Show("查询支付宝付款记录失败。" + e.Error);
                    return;
                }

                ListAllAlipayRecords.Clear();
                decimal sumYuan = 0;
                decimal sumRMB = 0;

                if (e.Result != null)
                {
                    foreach (var item in e.Result)
                    {
                        ListAllAlipayRecords.Add(new AlipayRechargeRecordUIModel(item));
                        sumYuan += item.total_fee;
                        sumRMB += item.value_rmb;
                    }
                }

                this.SumListAllAlipayRecords_PayYuan = sumYuan;
                this.SumListAllAlipayRecords_RMB = sumRMB;
            }
            catch (Exception exc)
            {
                MessageBox.Show("处理支付宝订单回调异常。" + exc.Message);
            }
        }

        void Client_HandleExceptionAlipayRechargeRecordCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<int> e)
        {
            try
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
            catch (Exception exc)
            {
                MessageBox.Show("处理支付宝订单回调异常。" + exc.Message);
            }
        }

        void Client_GetAllExceptionAlipayRechargeRecordsCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<MetaData.Trade.AlipayRechargeRecord[]> e)
        {
            try
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
            catch (Exception exc)
            {
                MessageBox.Show("查询异常的支付宝订单回调处理异常。" + exc.Message);
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

        public void AsyncGetAllAlipayRechargeRecords(string orderNumber, string alipayOrderNumber, string payEmail, string playerUserName, MyDateTime beginPayTime, MyDateTime endPayTime, int pageItemCount, int pageIndex)
        {
            if (GlobalData.Client != null)
            {
                App.BusyToken.ShowBusyWindow("正在查询支付宝付款记录...");
                this.ListAllAlipayRecords.Clear();
                GlobalData.Client.GetAllAlipayRechargeRecords(orderNumber, alipayOrderNumber, payEmail, playerUserName, beginPayTime, endPayTime, pageItemCount, pageIndex);

            }
        }
    }
}
