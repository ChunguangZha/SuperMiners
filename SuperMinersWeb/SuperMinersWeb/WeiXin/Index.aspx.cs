using SuperMinersWeb.WeiXin.Controller;
using SuperMinersWeb.WeiXin.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SuperMinersWeb.WeiXin
{
    public partial class Index : System.Web.UI.Page
    {
        string jsonMsg = "{\"errcode\":40125,\"errmsg\":\"invalid appsecret, view more at http:\\/\\/t.cn\\/RAEkdVq, hints: [ req_id: JRk49a0882ns82 ]\"}";

        protected void Page_Load(object sender, EventArgs e)
        {
            CreateNavigateUrl();
            //WeiXinHandler.AsyncGetUserCode();
        }

        private void CreateNavigateUrl()
        {
            this.link.NavigateUrl = WeiXinHandler.CreateGetCodeUrl();

            //ErrorModel error = new ErrorModel()
            //{
            //    errcode = 1133,
            //    errmsg = "afwefr2524qt2q123"
            //};

            //string json = JsonSeralize(error);
            //Console.WriteLine(json);
            //ErrorModel errDesc = JsonDeseralize(json);
            //Console.WriteLine("errDesc.errorcode: " + errDesc.errcode + " ; errmsg: " + errDesc.errmsg);
        }

        private string JsonSeralize(ErrorModel error)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                DataContractJsonSerializer deseralizer = new DataContractJsonSerializer(typeof(ErrorModel));
                deseralizer.WriteObject(stream, error);

                stream.Flush();
                stream.Position = 0;
                StreamReader reader = new StreamReader(stream);
                string json = reader.ReadToEnd();
                return json;
            }
        }

        private ErrorModel JsonDeseralize(string json)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                StreamWriter writer = new StreamWriter(stream);
                writer.Write(json);
                writer.Flush();
                stream.Position = 0;
                DataContractJsonSerializer deseralizer = new DataContractJsonSerializer(typeof(ErrorModel));
                ErrorModel err = (ErrorModel)deseralizer.ReadObject(stream);
                return err;
            }
        }
    }
}