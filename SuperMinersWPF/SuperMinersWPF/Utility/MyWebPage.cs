using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersWPF.Utility
{
    public static class MyWebPage
    {
        public static void ShowMyWebPage(string subUrl)
        {
            string baseuri = "";
#if DEBUG
            baseuri = "http://localhost:8509/";
#else

            baseuri = System.Configuration.ConfigurationManager.AppSettings["WebUri"];
#endif

            Process.Start(new ProcessStartInfo(baseuri + subUrl));
        }
    }
}
