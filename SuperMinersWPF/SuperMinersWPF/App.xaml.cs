using SuperMinersWPF.ViewModels;
using SuperMinersWPF.Views;
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
        internal static UserReferrerTreeViewModel UserReferrerTreeVMObject = new UserReferrerTreeViewModel();
        internal static NoticeViewModel NoticeVMObject = new NoticeViewModel();
        internal static StoneOrderViewModel StoneOrderVMObject = new StoneOrderViewModel();
        internal static BusyToken BusyToken = new BusyToken();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            UserVMObject.RegisterEvent();
            MessageVMObject.RegisterEvent();
            TopListVMObject.RegisterEvent();
            UserReferrerTreeVMObject.RegisterEvent();
            NoticeVMObject.RegisterEvent();
            StoneOrderVMObject.RegisterEvent();
        }
    }
}
