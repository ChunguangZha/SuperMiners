using SuperMinersWPF.Models;
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

        public DiamondShoppingItemDetailWindow(DiamondShoppingItemUIModel item)
        {
            InitializeComponent();

            this.shoppingItem = item;
            this.DataContext = this.shoppingItem;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GlobalData.Client.GetDiamondShoppingItemDetailImageBufferCompleted += Client_GetDiamondShoppingItemDetailImageBufferCompleted;
            GlobalData.Client.GetDiamondShoppingItemDetailImageBuffer(this.shoppingItem.Name);
        }

        private bool GetLocalTempDetailImages()
        {
            string itemRootPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "xlmines", "diamondshopping", this.shoppingItem.Name);
            if (!Directory.Exists(itemRootPath))
            {
                return false;
            }

            string[] fileNames = Directory.GetFiles(itemRootPath);
            if (fileNames == null)
            {
                return false;
            }

            foreach (var fileName in fileNames)
            {
                FileInfo file = new FileInfo(fileName);
                if(file!=null &&file.Extension == ".jpg")
                this.shoppingItem.DetailImageNames;
            }
        }

        void Client_GetDiamondShoppingItemDetailImageBufferCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<byte[][]> e)
        {
            throw new NotImplementedException();
        }
    }
}
