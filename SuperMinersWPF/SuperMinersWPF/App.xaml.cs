using SuperMinersWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SuperMinersWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        internal static UserViewModel UserVMObject = new UserViewModel();
        internal static MessageViewModel MessageVMObject = new MessageViewModel();
        internal static TopListViewModel TopListVMObject = new TopListViewModel();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            UserVMObject.RegisterEvent();
            MessageVMObject.RegisterEvent();
            TopListVMObject.RegisterEvent();
        }
    }
}
