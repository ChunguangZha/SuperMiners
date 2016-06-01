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
        internal static UserReferrerTreeViewModel UserReferrerTreeVMObject = new UserReferrerTreeViewModel();
        internal static NoticeViewModel NoticeVMObject = new NoticeViewModel();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            UserVMObject.RegisterEvent();
            MessageVMObject.RegisterEvent();
            TopListVMObject.RegisterEvent();
            UserReferrerTreeVMObject.RegisterEvent();
            NoticeVMObject.RegisterEvent();

//#if !DEBUG
//            CreateDesktopShortCut();
//#endif
        }

        private static void CreateDesktopShortCut()
        {
           
            string path = System.Environment.GetFolderPath(Environment.SpecialFolder.StartMenu);
            if (!path.EndsWith("\\"))
            {
                path += "\\";
            }
            path += @"Programs\迅灵信息";
            if (System.IO.Directory.Exists(path))
            {
                string desktop = System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                if (!desktop.EndsWith("\\"))
                {
                    desktop += "\\";
                }
                foreach (String file in System.IO.Directory.GetFiles(path))
                {
                    System.IO.FileInfo fi = new System.IO.FileInfo(file);
                    if (!System.IO.File.Exists(desktop + fi.Name))
                    {
                        fi.CopyTo(desktop + fi.Name);
                    }
                }
            }
        }
    }
}
