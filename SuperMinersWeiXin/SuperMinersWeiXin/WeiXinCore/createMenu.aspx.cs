using SuperMinersWeiXin.Controller;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SuperMinersWeiXin.WeiXinCore
{
    public partial class createMenu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string menu = "";
                using (FileStream stream = new FileStream(Server.MapPath(".") + "\\menu.txt", FileMode.Open))
                {
                    StreamReader reader = new StreamReader(stream, ASCIIEncoding.ASCII);
                    menu = reader.ReadToEnd();
                    reader.Close();
                }

                HttpHandler.SyncPost(WeiXinHandler.CreateCreateMenuUrl(), menu);
            }
            catch (Exception exc)
            {

            }
        }
    }
}