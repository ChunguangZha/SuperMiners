using SuperMinersServerApplication.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;
using System.Windows;
using WeiXinAccess;

namespace SuperMinersServerApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static SuperMinersService ServiceToRun = new SuperMinersService();

        public static TokenController TokenController = new TokenController();

        protected override void OnStartup(StartupEventArgs e)
        {
            TokenController.OutputError += TokenController_OutputError;
        }

        void TokenController_OutputError(string message)
        {
            LogHelper.Instance.AddInfoLog(message);

        }
    }
}
