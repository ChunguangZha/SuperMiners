using SuperMinersWeiXin.Controller;
using SuperMinersWeiXin.Utility;
using SuperMinersWeiXin.Wcf.Services;
using SuperMinersWeiXin.WeiXinCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SuperMinersWeiXin
{
    public partial class createMenu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string menu = " {\"button\":[{	\"type\":\"view\",\"name\":\"迅灵矿场\",\"url\":\"" + WeiXinHandler.CreateGetCodeUrl() + "\"}]}";

                //using (FileStream stream = new FileStream(Server.MapPath(".") + "\\menu.txt", FileMode.Open))
                //{
                //    StreamReader reader = new StreamReader(stream, ASCIIEncoding.ASCII);
                //    menu = reader.ReadToEnd();
                //    reader.Close();
                //}

                string access_token = WcfClient.Instance.GetAccessToken();

                LogHelper.Instance.AddInfoLog("Create menu access_token: " + access_token);
                if (access_token == null)
                {
                    Response.Write("get access_token failed");
                }
                else
                {
                    HttpHandler.SyncPost(WeiXinHandler.CreateCreateMenuUrl(access_token), menu);
                }
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("Create menu Exception ", exc);
            }
        }
    }

    class MenuItem
    {
        public string type { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }
}