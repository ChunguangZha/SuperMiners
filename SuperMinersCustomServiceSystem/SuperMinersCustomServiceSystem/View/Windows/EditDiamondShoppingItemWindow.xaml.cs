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
    /// Interaction logic for EditDiamondShoppingItemWindow.xaml
    /// </summary>
    public partial class EditDiamondShoppingItemWindow : Window
    {
        private System.Threading.SynchronizationContext _syn;
        byte[] _iconBuffer = null;

        bool isAdd = false;

        int oldID = 0;

        public Dictionary<int, string> dicItemTypeItemsSource = new Dictionary<int, string>();
        public List<BitmapSource> ListDetailImages = new List<BitmapSource>();

        public EditDiamondShoppingItemWindow(DiamondsShoppingItemType itemType)
        {
            InitializeComponent();
            this.Title = "添加钻石商品";
            isAdd = true;
            _syn = SynchronizationContext.Current;
            Init();
            this.cmbItemType.SelectedValue = (int)itemType;
        }

        public EditDiamondShoppingItemWindow(DiamondShoppingItemUIModel oldItem)
        {
            InitializeComponent();
            this.Title = "修改钻石商品";
            isAdd = false;
            _syn = SynchronizationContext.Current;
            Init();

            this.oldID = oldItem.ID;
            this.txtID.Text = oldItem.ID.ToString();
            this.txtTitle.Text = oldItem.Name;
            this.txtRemark.Text = oldItem.Remark;
            this.txtDetailText.Text = oldItem.DetailText;
            this.imgIcon.Source = oldItem.Icon;

            this.cmbItemType.SelectedValue = (int)oldItem.Type;
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

        }

        private void btnMoveUpDetailImage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnMoveDownDetailImage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDeleteDetailImage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
