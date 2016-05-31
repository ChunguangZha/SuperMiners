using MetaData;
using SuperMinersServerApplication.Controller;
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

namespace SuperMinersServerApplication.Views
{
    /// <summary>
    /// Interaction logic for AddNoticeWindow.xaml
    /// </summary>
    public partial class AddNoticeWindow : Window
    {
        public AddNoticeWindow()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (this.txtNoticeTitle.Text == "")
            {
                MessageBox.Show("请填写标题");
                return;
            }
            if (this.txtNoticeContent.Text == "")
            {
                MessageBox.Show("请填写内容");
                return;
            }

            NoticeInfo notice = new NoticeInfo()
            {
                Title = this.txtNoticeTitle.Text,
                Content = this.txtNoticeContent.Text
            };
            bool isOK = NoticeController.Instance.CreateNotice(notice);
            if (isOK)
            {
                MessageBox.Show("新通知添加成功！");
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("新通知添加失败！");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
