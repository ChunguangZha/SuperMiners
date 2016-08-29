using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperMinersWPF.Utility
{
    public static class MyMessageBox
    {
        public static void ShowInfo(string infoMessage)
        {
            MessageBox.Show(infoMessage);
        }

        public static DialogResult ShowQuestionOKCancel(string infoMessage)
        {
            return MessageBox.Show(infoMessage, "请确认", MessageBoxButtons.OKCancel);
        }

        public static MessageBoxAlipayPayQuestionResult ShowAlipayPayQuestion()
        {
            MessageBoxAlipayPayQuestion dig = new MessageBoxAlipayPayQuestion();
            dig.ShowDialog();
            return dig.Result;
        }
    }

    public enum MessageBoxAlipayPayQuestionResult
    {
        Succeed,
        Failed,
        Cancel
    }
}
