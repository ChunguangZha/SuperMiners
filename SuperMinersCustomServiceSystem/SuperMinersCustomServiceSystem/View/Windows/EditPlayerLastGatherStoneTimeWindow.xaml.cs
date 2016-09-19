using MetaData;
using SuperMinersCustomServiceSystem.Uility;
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

namespace SuperMinersCustomServiceSystem.View.Windows
{
    /// <summary>
    /// Interaction logic for EditPlayerLastGatherStoneTimeWindow.xaml
    /// </summary>
    public partial class EditPlayerLastGatherStoneTimeWindow : Window
    {
        public MyDateTime DataTimeValue;

        public EditPlayerLastGatherStoneTimeWindow(string userName, DateTime? lastGatherStoneTime)
        {
            InitializeComponent();

            this.txtUserName.Text = userName;
            if (lastGatherStoneTime == null)
            {
                this.numChangedTime.ValueTime = new MyDateTime();
            }
            else
            {
                this.numChangedTime.ValueTime = MyDateTime.FromDateTime(lastGatherStoneTime.Value);
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (this.numChangedTime.ValueTime.IsNull)
            {
                MyMessageBox.ShowInfo("必须要输入时间");
                return;
            }

            if (MyMessageBox.ShowQuestionOKCancel("请确认要修改玩家上一次收取矿石时间") == System.Windows.Forms.DialogResult.OK)
            {
                this.DataTimeValue = this.numChangedTime.ValueTime;
                this.DialogResult = true;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
