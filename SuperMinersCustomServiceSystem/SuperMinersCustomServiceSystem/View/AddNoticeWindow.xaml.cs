using MetaData;
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

namespace SuperMinersCustomServiceSystem.View
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
            App.NoticeVMObject.SaveNoticeCompleted += NoticeVMObject_SaveNoticeCompleted;
        }

        public AddNoticeWindow(NoticeInfo notice)
        {
            InitializeComponent();
            _notice = notice;
            this.txtNoticeTitle.Text = notice.Title;
            this.txtNoticeContent.Text = notice.Content;
            isAdd = false;
            this.txtNoticeTitle.IsReadOnly = true;
            App.NoticeVMObject.SaveNoticeCompleted += NoticeVMObject_SaveNoticeCompleted;
        }

        void NoticeVMObject_SaveNoticeCompleted(bool obj)
        {
            this.Close();
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

            if (isAdd)
            {
                _notice = new NoticeInfo()
                {
                    Title = this.txtNoticeTitle.Text,
                    Content = this.txtNoticeContent.Text
                };
            }
            else
            {
                _notice.Content = this.txtNoticeContent.Text;
            }

            App.NoticeVMObject.AsyncSaveNotice(_notice, isAdd);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
