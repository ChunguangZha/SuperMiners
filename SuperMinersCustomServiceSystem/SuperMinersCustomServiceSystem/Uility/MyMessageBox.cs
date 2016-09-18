using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperMinersCustomServiceSystem.Uility
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

    }

}
