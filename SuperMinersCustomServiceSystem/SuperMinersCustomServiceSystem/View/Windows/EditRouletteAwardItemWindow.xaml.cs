using MetaData;
using MetaData.Game.Roulette;
using Microsoft.Win32;
using SuperMinersCustomServiceSystem.Model;
using SuperMinersCustomServiceSystem.Uility;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for EditRouletteAwardItemWindow.xaml
    /// </summary>
    public partial class EditRouletteAwardItemWindow : Window
    {
        public bool IsOK { get; private set; }
        //int maxIndex;
        bool isAdd = false;
        RouletteAwardItemUIModel uiItem = null;
        //private BitmapImage _icon = null;

        public EditRouletteAwardItemWindow(int count)
        {
            InitializeComponent();
            isAdd = true;
            IsOK = false;
            //maxIndex = count;
            this.Title = "添加幸运大转盘奖项";

            GlobalData.Client.AddAwardItemCompleted += Client_AddAwardItemCompleted;
        }

        public EditRouletteAwardItemWindow(RouletteAwardItemUIModel awarditem)
        {
            InitializeComponent();
            this.Title = "修改幸运大转盘奖项";
            isAdd = false;
            IsOK = false;
            uiItem = awarditem;

            this.txtRecordID.Text = awarditem.ID.ToString();
            this.txtAwardName.Text = awarditem.AwardName;
            this.numAwardNumber.Value = awarditem.AwardNumber;
            this.chkIsLargeAward.IsChecked = awarditem.IsLargeAward;
            //this.chkIsRealAward.IsChecked = awarditem.IsRealAward;
            this.cmbAwardType.SelectedIndex = (int)awarditem.RouletteAwardType;
            this.numValueMoneyYuan.Value = awarditem.ValueMoneyYuan;
            this.numWinProbability.Value = awarditem.WinProbability;
            this.imgIcon.Source = awarditem.Icon;
            this._iconBuffer = awarditem.ParentObject.IconBuffer;
            //this._icon = awarditem.Icon;

            GlobalData.Client.UpdateAwardItemCompleted += Client_UpdateAwardItemCompleted;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            GlobalData.Client.AddAwardItemCompleted -= Client_AddAwardItemCompleted;
            GlobalData.Client.UpdateAwardItemCompleted -= Client_UpdateAwardItemCompleted;
        }

        void Client_UpdateAwardItemCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<int> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("修改奖项服务器返回异常。异常信息为：" + e.Error);
                    return;
                }

                if (e.Result == OperResult.RESULTCODE_TRUE)
                {
                    MyMessageBox.ShowInfo("修改奖项成功");
                    this.IsOK = true;
                    this.Close();
                }
                else
                {
                    MyMessageBox.ShowInfo("修改奖项失败，原因为：" + OperResult.GetMsg(e.Result));
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("修改奖项回调处理异常。异常信息为：" + exc.Message);
            }
        }

        void Client_AddAwardItemCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<int> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("增加奖项服务器返回异常。异常信息为：" + e.Error);
                    return;
                }

                if (e.Result == OperResult.RESULTCODE_TRUE)
                {
                    MyMessageBox.ShowInfo("增加奖项成功");
                    this.IsOK = true;
                    this.Close();
                }
                else
                {
                    MyMessageBox.ShowInfo("增加奖项失败，原因为：" + OperResult.GetMsg(e.Result));
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("增加奖项回调处理异常。异常信息为：" + exc.Message);
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (txtAwardName.Text.Trim() == "")
            {
                MyMessageBox.ShowInfo("请填写奖项信息");
                return;
            }
            if (this.imgIcon.Source == null || _iconBuffer == null)
            {
                MyMessageBox.ShowInfo("请上传图标");
                return;
            }

            if (MyMessageBox.ShowQuestionOKCancel("请确认奖项信息填写正确") != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }

            RouletteAwardItem item = new RouletteAwardItem();

            item.ID = Convert.ToInt32(this.txtRecordID.Text);
            item.AwardName = txtAwardName.Text.Trim();
            item.AwardNumber = (int)this.numAwardNumber.Value;
            item.IsLargeAward = this.chkIsLargeAward.IsChecked.Value;
            //item.IsRealAward = this.chkIsRealAward.IsChecked.Value;
            item.RouletteAwardType = (RouletteAwardType)this.cmbAwardType.SelectedIndex;
            item.ValueMoneyYuan = (float)this.numValueMoneyYuan.Value;
            item.WinProbability = (int)this.numWinProbability.Value;

            item.IconBuffer = _iconBuffer;

            if (isAdd)
            {
                App.BusyToken.ShowBusyWindow("正在提交数据...");
                GlobalData.Client.AddAwardItem(item);
            }
            else
            {
                App.BusyToken.ShowBusyWindow("正在提交数据...");
                GlobalData.Client.UpdateAwardItem(item);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        byte[] _iconBuffer = null;

        private void btnUpdateIcon_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openDig = new OpenFileDialog();
                if (openDig.ShowDialog() == true)
                {
                    using (FileStream stream = new FileStream(openDig.FileName, FileMode.Open))
                    {
                        _iconBuffer = new byte[stream.Length];
                        stream.Read(_iconBuffer, 0, (int)stream.Length);
                    }

                    this.imgIcon.Source = RouletteAwardItemUIModel.GetIconSource(_iconBuffer);
                }
            }
            catch (Exception exc)
            {

            }
        }

    }
}
