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
        public AddNoticeWindow()
        {
            InitializeComponent();
            App.NoticeVMObject.CreateNoticeCompleted += NoticeVMObject_CreateNoticeCompleted;
        }

        void NoticeVMObject_CreateNoticeCompleted(bool obj)
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

            NoticeInfo notice = new NoticeInfo()
            {
                Title = this.txtNoticeTitle.Text,
                Content = this.txtNoticeContent.Text
            };

            App.NoticeVMObject.AsyncCreateNotice(notice);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
