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

namespace SuperMinersWPF.Utility
{
    /// <summary>
    /// Interaction logic for MessageBoxAlipayPayQuestion.xaml
    /// </summary>
    public partial class MessageBoxAlipayPayQuestion : Window
    {
        public MessageBoxAlipayPayQuestionResult Result { get; private set; }

        public MessageBoxAlipayPayQuestion()
        {
            InitializeComponent();
            Result = MessageBoxAlipayPayQuestionResult.Cancel;
        }

        private void btnSucceed_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxAlipayPayQuestionResult.Succeed;
            this.Close();
        }

        private void btnFailed_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxAlipayPayQuestionResult.Failed;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxAlipayPayQuestionResult.Cancel;
            this.Close();
        }

    }
}
