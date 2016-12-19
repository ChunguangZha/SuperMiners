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
        private NoticeInfo _notice;
        bool isAdd = false;

        public AddNoticeWindow()
        {
            InitializeComponent();
            isAdd = true;
        }

        public AddNoticeWindow(NoticeInfo notice)
        {
            InitializeComponent();
            _notice = notice;
            this.txtNoticeTitle.Text = notice.Title;
            this.txtNoticeContent.Text = notice.Content;
            isAdd = false;
            this.txtNoticeTitle.IsReadOnly = true;
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

            bool isOK = false;
            if (_notice == null)
            {
                _notice = new NoticeInfo()
                {
                    Title = this.txtNoticeTitle.Text,
                    Content = this.txtNoticeContent.Text,
                    Time = DateTime.Now
                };
            }
            else
            {
                _notice.Content = this.txtNoticeContent.Text;
            }

            isOK = NoticeController.Instance.SaveNotice(_notice, isAdd);
            if (isOK)
            {
                MessageBox.Show("保存通知成功！");
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("保存通知失败！");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
