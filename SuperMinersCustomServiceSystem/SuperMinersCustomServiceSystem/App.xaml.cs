using SuperMinersCustomServiceSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SuperMinersCustomServiceSystem
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static BusyToken BusyToken = new BusyToken();
        public static PlayerViewModel PlayerVMObject = new PlayerViewModel();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            PlayerVMObject.RegisterEvents();
        }
    }
}
