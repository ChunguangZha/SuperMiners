using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SuperMinersWPF.Utility
{
    public static class MyMessageBox
    {
        public static void ShowInfo(string infoMessage)
        {
            MessageBox.Show(infoMessage);
        }
    }
}
