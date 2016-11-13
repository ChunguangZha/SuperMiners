using SuperMinersWeb.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SuperMinersWeb.WeiXin
{
    public partial class WeiXinResponse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //code说明 ： code作为换取access_token的票据，每次用户授权带上的code将不一样，code只能使用一次，5分钟未被使用自动过期。
                string code = Request["code"];
                string state = Request["state"];

                LogHelper.Instance.AddInfoLog("code:" + code + "; state: " + state );

                string baseurl = "https://api.weixin.qq.com/sns/oauth2/access_token?";
                string url = baseurl + "appid=" + Config.appid + "&secret=" + Config.appSecret + "&code=" + code + "&grant_type=authorization_code";
                //Response.Redirect(url);

                HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(url);
                myReq.ContentType = "get";
                myReq.BeginGetResponse(o =>
                {
                    if (o.IsCompleted)
                    {
                        var response = (HttpWebResponse)myReq.EndGetResponse(o);
                        var stream = response.GetResponseStream();
                        //var encoding = Encoding.GetEncoding(response.ContentEncoding);
                        //if (encoding == null)
                        //{
                        //    encoding = Encoding.ASCII;
                        //}

                        StreamReader reader = new StreamReader(stream);
                        string getString = reader.ReadToEnd();

                        LogHelper.Instance.AddInfoLog("getString:" + getString);

                        reader.Close();
                        reader.Dispose();
                        stream.Close();
                        stream.Dispose();
                    }
                }, null);
            }
        }
    }
}