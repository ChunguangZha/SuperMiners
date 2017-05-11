using MetaData;
using MetaData.Shopping;
using Microsoft.Win32;
using SuperMinersCustomServiceSystem.Model;
using SuperMinersCustomServiceSystem.Uility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for EditDiamondShoppingItemWindow.xaml
    /// </summary>
    public partial class EditDiamondShoppingItemWindow : Window
    {
        private System.Threading.SynchronizationContext _syn;
        byte[] _iconBuffer = null;

        bool isAdd = false;

        int oldID = 0;
        private DiamondShoppingItemUIModel _oldItem = null;

        public Dictionary<int, string> dicItemTypeItemsSource = new Dictionary<int, string>();
        public ObservableCollection<BitmapSource> ListDetailImages = new ObservableCollection<BitmapSource>();
        public ObservableCollection<byte[]> ListDetailImageBuffers = new ObservableCollection<byte[]>();
        public ObservableCollection<string> ListDetailImageNames = new ObservableCollection<string>();

        public EditDiamondShoppingItemWindow(DiamondsShoppingItemType itemType)
        {
            InitializeComponent();
            this.Title = "添加钻石商品";
            isAdd = true;
            _syn = SynchronizationContext.Current;
            Init();
            this.cmbItemType.SelectedValue = (int)itemType;

            GlobalData.Client.AddDiamondShoppingItemCompleted += Client_AddDiamondShoppingItemCompleted;
        }

        public EditDiamondShoppingItemWindow(DiamondShoppingItemUIModel oldItem)
        {
            InitializeComponent();
            this.Title = "修改钻石商品";
            isAdd = false;
            _syn = SynchronizationContext.Current;
            Init();

            this._oldItem = oldItem;
            this.oldID = oldItem.ID;
            this.txtID.Text = oldItem.ID.ToString();
            this.txtTitle.Text = oldItem.Name;
            this.txtRemark.Text = oldItem.Remark;
            this.numPrice.Value = (double)oldItem.ValueDiamonds;
            this.txtDetailText.Text = oldItem.DetailText;
            this.imgIcon.Source = oldItem.Icon;
            if (oldItem.DetailImageNames != null)
            {
                this.ListDetailImageNames = new ObservableCollection<string>(oldItem.DetailImageNames);
            }
            this.cmbItemType.SelectedValue = (int)oldItem.Type;

            GlobalData.Client.UpdateDiamondShoppingItemCompleted += Client_UpdateDiamondShoppingItemCompleted;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!isAdd)
            {
                GlobalData.Client.GetDiamondShoppingItemDetailImageBufferCompleted += Client_GetDiamondShoppingItemDetailImageBufferCompleted;
                this.IsEnabled = false;

                App.BusyToken.ShowBusyWindow("正在加载钻石商品详细信息...");
                GlobalData.Client.GetDiamondShoppingItemDetailImageBuffer(this._oldItem.ID);
            }
        }

        void Client_GetDiamondShoppingItemDetailImageBufferCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<byte[][]> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("加载钻石商品详细信息失败。原因为：" + e.Error.Message);
                    return;
                }

                ListDetailImages.Clear();
                ListDetailImageBuffers.Clear();
                if (e.Result != null)
                {
                    foreach (var item in e.Result)
                    {
                        ListDetailImageBuffers.Add(item);
                        ListDetailImages.Add(MyImageConverter.GetIconSource(item));
                    }
                }

                this.IsEnabled = true;
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("加载钻石商品详细信息异常。原因为：" + exc.Message);
            }
        }

        private void Init()
        {
            dicItemTypeItemsSource.Add((int)DiamondsShoppingItemType.LiveThing, "生活用品");
            dicItemTypeItemsSource.Add((int)DiamondsShoppingItemType.Digital, "数码产品");
            dicItemTypeItemsSource.Add((int)DiamondsShoppingItemType.Food, "食品专区");
            dicItemTypeItemsSource.Add((int)DiamondsShoppingItemType.HomeAppliances, "家用电器");
            dicItemTypeItemsSource.Add((int)DiamondsShoppingItemType.PhoneFee, "话费充值");

            this.cmbItemType.ItemsSource = dicItemTypeItemsSource;
            this.cmbItemType.SelectedIndex = 0;

            this.lvDetailImages.ItemsSource = this.ListDetailImages;
        }

        private void btnUploadFirstImage_Click(object sender, RoutedEventArgs e)
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

                    this.imgIcon.Source = MyImageConverter.GetIconSource(_iconBuffer);
                }
            }
            catch (Exception exc)
            {

            }
        }

        private void btnAddDetailImage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openDig = new OpenFileDialog();
                if (openDig.ShowDialog() == true)
                {
                    string imgName = Guid.NewGuid().ToString();
                    byte[] buffer = null;
                    using (FileStream stream = new FileStream(openDig.FileName, FileMode.Open))
                    {
                        buffer = new byte[stream.Length];
                        stream.Read(buffer, 0, (int)stream.Length);
                    }

                    this.ListDetailImageNames.Add(imgName);
                    this.ListDetailImageBuffers.Add(buffer);
                    this.ListDetailImages.Add(MyImageConverter.GetIconSource(buffer));
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo(exc.Message);
            }
        }

        private void btnMoveUpDetailImage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.lvDetailImages.SelectedItem == null)
                {
                    MyMessageBox.ShowInfo("请选择一张图片");
                    return;
                }

                int index = this.lvDetailImages.SelectedIndex;
                if (index > 0)
                {
                    this.ListDetailImageNames.Move(index, index - 1);
                    this.ListDetailImages.Move(index, index - 1);
                    this.ListDetailImageBuffers.Move(index, index - 1);
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo(exc.Message);
            }
        }

        private void btnMoveDownDetailImage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.lvDetailImages.SelectedItem == null)
                {
                    MyMessageBox.ShowInfo("请选择一张图片");
                    return;
                }

                int index = this.lvDetailImages.SelectedIndex;
                if (index < this.ListDetailImages.Count)
                {
                    this.ListDetailImageNames.Move(index, index + 1);
                    this.ListDetailImages.Move(index, index + 1);
                    this.ListDetailImageBuffers.Move(index, index + 1);
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo(exc.Message);
            }
        }

        private void btnDeleteDetailImage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.lvDetailImages.SelectedItem == null)
                {
                    MyMessageBox.ShowInfo("请选择一张图片");
                    return;
                }

                int index = this.lvDetailImages.SelectedIndex;
                this.ListDetailImageNames.RemoveAt(index);
                this.ListDetailImages.RemoveAt(index);
                this.ListDetailImageBuffers.RemoveAt(index);
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo(exc.Message);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = this.txtTitle.Text.Trim();
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
                if (this.numPrice.Value == 0)
                {
                    MyMessageBox.ShowInfo("请设置价格");
                    return;
                }

                if (MyMessageBox.ShowQuestionOKCancel("请确认奖项信息填写正确") != System.Windows.Forms.DialogResult.OK)
                {
                    return;
                }

                DiamondShoppingItem item = new DiamondShoppingItem()
                {
                    ID = this.oldID,
                    Name = this.txtTitle.Text.Trim(),
                    Remark = this.txtRemark.Text.Trim(),
                    IconBuffer = this._iconBuffer,
                    ItemType = (DiamondsShoppingItemType)(int)this.cmbItemType.SelectedValue,
                    SellState = (SellState)(int)this.cmbSellState.SelectedIndex,
                    ValueDiamonds = (decimal)this.numPrice.Value,
                    DetailText = this.txtDetailText.Text.Trim(),
                    DetailImageNames = this.ListDetailImageNames.ToArray()
                };

                InputActionPasswordWindow winActionPassword = new InputActionPasswordWindow();
                if (winActionPassword.ShowDialog() == true)
                {
                    string password = winActionPassword.ActionPassword;
                    if (isAdd)
                    {
                        this.AsyncAddDiamondShoppingItem(password, item);
                    }
                    else
                    {
                        item.ID = this.oldID;
                        this.AsyncUpdateDiamondShoppingItem(password, item);
                    }
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("保存失败。原因为：" + exc.Message);
            }
        }

        public void AsyncAddDiamondShoppingItem(string password, DiamondShoppingItem item)
        {
            App.BusyToken.ShowBusyWindow("正在保存钻石商品...");
            GlobalData.Client.AddDiamondShoppingItem(password, item, this.ListDetailImageBuffers.ToArray());
        }

        public void AsyncUpdateDiamondShoppingItem(string password, DiamondShoppingItem item)
        {
            App.BusyToken.ShowBusyWindow("正在保存钻石商品...");
            GlobalData.Client.UpdateDiamondShoppingItem(password, item, this.ListDetailImageBuffers.ToArray());
        }

        void Client_AddDiamondShoppingItemCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<int> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("保存钻石商品失败。原因为：" + e.Error.Message);
                    return;
                }

                if (e.Result == OperResult.RESULTCODE_TRUE)
                {
                    MyMessageBox.ShowInfo("保存钻石商品成功");
                    _syn.Post((o) =>
                    {
                        this.Close();
                    }, null);
                }
                else
                {
                    MyMessageBox.ShowInfo("保存钻石商品失败。原因为：" + OperResult.GetMsg(e.Result));
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("保存钻石商品异常。原因为：" + exc.Message);
            }
        }

        void Client_UpdateDiamondShoppingItemCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<int> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("保存钻石商品失败。原因为：" + e.Error.Message);
                    return;
                }

                if (e.Result == OperResult.RESULTCODE_TRUE)
                {
                    MyMessageBox.ShowInfo("保存钻石商品成功");
                    _syn.Post((o) =>
                    {
                        this.Close();
                    }, null);
                }
                else
                {
                    MyMessageBox.ShowInfo("保存钻石商品失败。原因为：" + OperResult.GetMsg(e.Result));
                }
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("保存钻石商品异常。原因为：" + exc.Message);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
