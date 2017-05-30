using MetaData.SystemConfig;
using SuperMinersWPF.Utility;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for StoneFActoryJoinInSlaveWindow.xaml
    /// </summary>
    public partial class StoneFactoryJoinInSlaveWindow : Window
    {
        public int JoinInSlaveGroupCount = 0;
        private decimal allMinersCount;
        private decimal maxJoinableSlaveGroupCount;

        public StoneFactoryJoinInSlaveWindow(decimal allMinersCount)
        {
            InitializeComponent();
            this.allMinersCount = allMinersCount;
            this.txtTotalMinersCount.Text = allMinersCount.ToString();
            maxJoinableSlaveGroupCount = Math.Floor(allMinersCount / StoneFactoryConfig.OneGroupSlaveHasMiners);
            this.numAddToFactorySlaveGroupCount.Maximum = (int)maxJoinableSlaveGroupCount;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (this.numAddToFactorySlaveGroupCount.Value == 0)
            {
                MyMessageBox.ShowInfo("请输入要增加的矿工");
                return;
            }
            if ((decimal)this.numAddToFactorySlaveGroupCount.Value * StoneFactoryConfig.OneGroupSlaveHasMiners > allMinersCount)
            {
                MyMessageBox.ShowInfo("没有足够的矿工");
                return;
            }
            else
            {
                this.JoinInSlaveGroupCount = (int)this.numAddToFactorySlaveGroupCount.Value;
            }
            this.DialogResult = true;
        }
    }
}
