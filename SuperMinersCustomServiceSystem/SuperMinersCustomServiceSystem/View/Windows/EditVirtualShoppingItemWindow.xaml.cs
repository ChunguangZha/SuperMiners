using MetaData;
using MetaData.Shopping;
using Microsoft.Win32;
using SuperMinersCustomServiceSystem.Model;
using SuperMinersCustomServiceSystem.Uility;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace SuperMinersCustomServiceSystem.View.Windows
{
    /// <summary>
    /// Interaction logic for EditVirtualShoppingItemWindow.xaml
    /// </summary>
    public partial class EditVirtualShoppingItemWindow : Window
    {
        private System.Threading.SynchronizationContext _syn;
        byte[] _iconBuffer = null;

        bool isAdd = false;

        int oldID = 0;

        public EditVirtualShoppingItemWindow()
        {
            InitializeComponent();
            GlobalData.Client.AddVirtualShoppingItemCompleted += Client_AddVirtualShoppingItemCompleted;
            this.Title = "添加积分商品";
            isAdd = true;

            _syn = SynchronizationContext.Current;
        }

        public EditVirtualShoppingItemWindow(VirtualShoppingItemUIModel item)
        {
            InitializeComponent();
            GlobalData.Client.UpdateVirtualShoppingItemCompleted += Client_UpdateVirtualShoppingItemCompleted;
            isAdd = false;
            this.Title = "修改积分商品";
            this.oldID = item.ID;
            this.txtID.Text = item.ID.ToString();
            this.txtName.Text = item.Name;
            this.txtRemark.Text = item.Remark;

            this.cmbItemState.SelectedIndex = (int)item.SellState;
            this.txtPlayerMaxBuyCount.Value = item.PlayerMaxBuyableCount;
            this.txtPriceRMB.Value = (double)item.ValueShoppingCredits;
            this.txtGainExp.Value = (double)item.GainExp;
            this.txtGainRMB.Value = (double)item.GainRMB;
            this.txtGainGoldCoin.Value = (double)item.GainGoldCoin;
            this.txtGainMine_StoneReserves.Value = (double)item.GainMine_StoneReserves;
            this.txtGainMiner.Value = (double)item.GainMiner;
            this.txtGainStone.Value = (double)item.GainStone;
            this.txtGainDiamond.Value = (double)item.GainDiamond;
            this.txtGainShoppingCredits.Value = (double)item.GainShoppingCredits;
            this.txtGainGravel.Value = (double)item.GainGravel;
            this.imgIcon.Source = item.Icon;
            this._iconBuffer = item.ParentObject.IconBuffer;

            _syn = SynchronizationContext.Current;
        }

        public void AsyncAddVirtualShoppingItem(string actionPassword, MetaData.Shopping.VirtualShoppingItem item)
        {
            if (GlobalData.Client != null)
            {
                App.BusyToken.ShowBusyWindow("正在添加新的积分商品...");
                GlobalData.Client.AddVirtualShoppingItem(actionPassword, item);
            }
        }

        public void AsyncUpdateVirtualShoppingItem(string actionPassword, MetaData.Shopping.VirtualShoppingItem item)
        {
            if (GlobalData.Client != null)
            {
                App.BusyToken.ShowBusyWindow("正在修改积分商品...");
                GlobalData.Client.UpdateVirtualShoppingItem(actionPassword, item);
            }
        }

        private void btnUploadIcon_Click(object sender, RoutedEventArgs e)
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

                    this.imgIcon.Source = VirtualShoppingItemUIModel.GetIconSource(_iconBuffer);
                }
            }
            catch (Exception exc)
            {

            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            string name = this.txtName.Text.Trim();
            string remark = this.txtRemark.Text.Trim();

            if (name == "")
            {
                MyMessageBox.ShowInfo("请填写名称");
                return;
            }
            if (remark == "")
            {
                MyMessageBox.ShowInfo("请填写说明");
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


            VirtualShoppingItem item = new VirtualShoppingItem()
            {
                Name = name,
                Remark = remark,
                GainDiamond = (decimal)this.txtGainDiamond.Value,
                GainExp = (decimal)this.txtGainExp.Value,
                GainGoldCoin = (decimal)this.txtGainGoldCoin.Value,
                GainGravel = (decimal)this.txtGainGravel.Value,
                GainMine_StoneReserves = (decimal)this.txtGainMine_StoneReserves.Value,
                GainMiner = (decimal)this.txtGainMiner.Value,
                GainRMB = (decimal)this.txtGainRMB.Value,
                GainShoppingCredits = (decimal)this.txtGainShoppingCredits.Value,
                GainStone = (decimal)this.txtGainStone.Value,
                IconBuffer = this._iconBuffer,
                PlayerMaxBuyableCount = (int)this.txtPlayerMaxBuyCount.Value,
                SellState = (SellState)this.cmbItemState.SelectedIndex,
                ValueShoppingCredits = (decimal)this.txtPriceRMB.Value
            };

            InputActionPasswordWindow winActionPassword = new InputActionPasswordWindow();
            if (winActionPassword.ShowDialog() == true)
            {
                string password = winActionPassword.ActionPassword;
                if (isAdd)
                {
                    this.AsyncAddVirtualShoppingItem(password, item);
                }
                else
                {
                    item.ID = this.oldID;
                    this.AsyncUpdateVirtualShoppingItem(password, item);
                }
            }

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }


        void Client_AddVirtualShoppingItemCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<int> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MessageBox.Show("添加新的虚拟商品失败。" + e.Error.Message);
                    return;
                }

                if (e.Result == OperResult.RESULTCODE_TRUE)
                {
                    MessageBox.Show("虚拟商品添加成功。");
                    this._syn.Post((o) =>
                    {
                        this.Close();
                    }, null);
                }
                else
                {
                    MessageBox.Show("添加新的虚拟商品失败。原因为：" + OperResult.GetMsg(e.Result));
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("添加新的虚拟商品回调处理异常。" + exc.Message);
            }
        }

        void Client_UpdateVirtualShoppingItemCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<int> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MessageBox.Show("修改虚拟商品失败。" + e.Error.Message);
                    return;
                }

                if (e.Result == OperResult.RESULTCODE_TRUE)
                {
                    MessageBox.Show("虚拟商品修改成功。");
                    this._syn.Post((o) =>
                    {
                        this.Close();
                    }, null);
                }
                else
                {
                    MessageBox.Show("修改虚拟商品失败。原因为：" + OperResult.GetMsg(e.Result));
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("修改虚拟商品回调处理异常。" + exc.Message);
            }
        }

    }
}
