using SuperMinersWPF.Models;
using SuperMinersWPF.Utility;
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

namespace SuperMinersWPF.Views.Windows
{
    /// <summary>
    /// Interaction logic for DiamondShoppingItemDetailWindow.xaml
    /// </summary>
    public partial class DiamondShoppingItemDetailWindow : Window
    {
        private DiamondShoppingItemUIModel shoppingItem = null;
        private BitmapSource[] detailImageSources = null;

        public DiamondShoppingItemDetailWindow(DiamondShoppingItemUIModel item)
        {
            InitializeComponent();

            this.shoppingItem = item;
            this.DataContext = this.shoppingItem;
            if (item.DetailImageNames != null)
            {
                detailImageSources = new BitmapSource[item.DetailImageNames.Length];
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.shoppingItem.DetailImageNames != null)
            {
                if (GetLocalTempDetailImages())//如果本地缓存图片加载成功，则不需要再重服务器下载图片
                {
                    this.shoppingItem.ListDetailImages.Clear();
                    foreach (var item in this.detailImageSources)
                    {
                        this.shoppingItem.ListDetailImages.Add(item);
                    }
                }
                else
                {
                    GlobalData.Client.GetDiamondShoppingItemDetailImageBufferCompleted += Client_GetDiamondShoppingItemDetailImageBufferCompleted;
                    App.BusyToken.ShowBusyWindow("加载商品详情信息");
                    GlobalData.Client.GetDiamondShoppingItemDetailImageBuffer(this.shoppingItem.ID);
                }
            }
        }

        private bool SaveDetailImagesToLocalTempDir(byte[][] listImageBuffers)
        {
            this.shoppingItem.ListDetailImages.Clear();
            if (listImageBuffers == null)
            {
                return false;
            }

            string itemRootPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "xlmines", "diamondshopping", this.shoppingItem.Name);
            if (!Directory.Exists(itemRootPath))
            {
                Directory.CreateDirectory(itemRootPath);
            }

            for (int i = 0; i < this.shoppingItem.DetailImageNames.Length; i++)
            {
                this.shoppingItem.ListDetailImages.Add(SuperMinersCustomServiceSystem.Uility.MyImageConverter.GetIconSource(listImageBuffers[i]));

                try
                {
                    string detailImgFilePath = System.IO.Path.Combine(itemRootPath, this.shoppingItem.DetailImageNames[i] + ".jpg");
                    using (FileStream stream = new FileStream(detailImgFilePath, FileMode.Create))
                    {
                        stream.Write(listImageBuffers[i], 0, listImageBuffers[i].Length);
                    }
                }
                catch (Exception exc)
                {

                }
            }

            return true;
        }

        private bool GetLocalTempDetailImages()
        {
            string itemRootPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "xlmines", "diamondshopping", this.shoppingItem.Name);
            if (!Directory.Exists(itemRootPath))
            {
                return false;
            }

            string[] fileFullPaths = Directory.GetFiles(itemRootPath);
            if (fileFullPaths == null)
            {
                return false;
            }

            foreach (var fileFullPath in fileFullPaths)
            {
                FileInfo file = new FileInfo(fileFullPath);
                if (file != null && file.Extension == ".jpg")
                {
                    int index = GetIndexFromDetailImageNames(file.Name.Substring(0, file.Name.Length - 4));
                    if (index >= 0)
                    {
                        using (FileStream stream = File.OpenRead(fileFullPath))
                        {
                            byte[] buffer = new byte[stream.Length];
                            stream.Read(buffer, 0, buffer.Length);
                            this.detailImageSources[index] = SuperMinersCustomServiceSystem.Uility.MyImageConverter.GetIconSource(buffer);
                        }
                    }
                }
            }

            bool hasFullImage = true;
            for (int i = 0; i < this.detailImageSources.Length; i++)
            {
                if (this.detailImageSources[i] == null)
                {
                    hasFullImage = false;
                    break;
                }
            }

            return hasFullImage;
        }

        private int GetIndexFromDetailImageNames(string fileName)
        {
            if (this.shoppingItem.DetailImageNames != null)
            {
                for (int i = 0; i < this.shoppingItem.DetailImageNames.Length; i++)
                {
                    if (this.shoppingItem.DetailImageNames[i] == fileName)
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        void Client_GetDiamondShoppingItemDetailImageBufferCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<byte[][]> e)
        {
            try
            {
                App.BusyToken.CloseBusyWindow();
                if (e.Error != null)
                {
                    MyMessageBox.ShowInfo("加载商品详情信息失败。原因为：" + e.Error.Message);
                    return;
                }

                SaveDetailImagesToLocalTempDir(e.Result);
            }
            catch (Exception exc)
            {
                MyMessageBox.ShowInfo("加载商品详情信息异常。原因为：" + exc.Message);
            }
        }

        private void btnBuy_Click(object sender, RoutedEventArgs e)
        {
            if (GlobalData.CurrentUser.StockOfDiamonds < this.shoppingItem.ValueDiamonds)
            {
                MyMessageBox.ShowInfo("您的钻石不足");
                return;
            }

            MyMessageBox.ShowInfo("该商品暂时不能购买");
        }
    }
}
