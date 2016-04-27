using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;
using System.Windows;

namespace SuperMinersServerApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static SuperMinersService ServiceToRun = new SuperMinersService();

        protected override void OnStartup(StartupEventArgs e)
        {

        }
    }
}
