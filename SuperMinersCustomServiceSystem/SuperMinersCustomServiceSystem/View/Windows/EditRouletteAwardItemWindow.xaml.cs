using MetaData.Game.Roulette;
using SuperMinersCustomServiceSystem.Model;
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
    /// Interaction logic for EditRouletteAwardItemWindow.xaml
    /// </summary>
    public partial class EditRouletteAwardItemWindow : Window
    {
        int maxIndex;
        bool isAdd = false;
        RouletteAwardItemUIModel uiItem = null;

        public EditRouletteAwardItemWindow(int count)
        {
            InitializeComponent();
            isAdd = true;
            maxIndex = count;
            this.Title = "添加幸运大转盘奖项";
        }

        public EditRouletteAwardItemWindow(RouletteAwardItemUIModel awarditem)
        {
            InitializeComponent();
            this.Title = "修改幸运大转盘奖项";
            isAdd = false;
            uiItem = awarditem;

            this.txtRecordID.Text = awarditem.ID.ToString();
            this.txtAwardName.Text = awarditem.AwardName;
            this.numAwardNumber.Value = awarditem.AwardNumber;
            this.chkIsLargeAward.IsChecked = awarditem.IsLargeAward;
            //this.chkIsRealAward.IsChecked = awarditem.IsRealAward;
            this.cmbAwardType.SelectedIndex = (int)awarditem.RouletteAwardType;
            this.numValueMoneyYuan.Value = awarditem.ValueMoneyYuan;
            this.numWinProbability.Value = awarditem.WinProbability;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (txtAwardName.Text.Trim() == "")
            {
                MyMessageBox.ShowInfo("请填写奖项信息");
                return;
            }

            if (MyMessageBox.ShowQuestionOKCancel("请确认奖项信息填写正确") != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }

            RouletteAwardItem item = new RouletteAwardItem();
            if (isAdd)
            {
                item.ID = maxIndex + 1;
            }
            item.AwardName = txtAwardName.Text.Trim();
            item.AwardNumber = (int)this.numAwardNumber.Value;
            item.IsLargeAward = this.chkIsLargeAward.IsChecked.Value;
            //item.IsRealAward = this.chkIsRealAward.IsChecked.Value;
            item.RouletteAwardType = (RouletteAwardType)this.cmbAwardType.SelectedIndex;
            item.ValueMoneyYuan = (float)this.numValueMoneyYuan.Value;
            item.WinProbability = (int)this.numWinProbability.Value;

            if (isAdd)
            {
                App.GameRouletteVMObject.ListRouletteAwardItems.Add(new Model.RouletteAwardItemUIModel(item));
            }
            else
            {
                uiItem.ParentObject = item;
            }
            this.DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
