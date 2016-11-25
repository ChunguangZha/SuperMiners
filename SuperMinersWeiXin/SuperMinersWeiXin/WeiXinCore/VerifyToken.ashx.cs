using SuperMinersWeiXin.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace SuperMinersWeiXin.WeiXinCore
{
    /// <summary>
    /// Summary description for VerifyToken
    /// </summary>
    public class VerifyToken : IHttpHandler
    {

        //signature:a4f11862ec04e38238be867bdc7ed3491ba0b0d4; 
        //timestamp: 1478957202; 
        //nonce: 1058836731; 
        //echostr: 7491075066884472830-----------------------------------------------------

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                context.Response.ContentType = "text/plain";

                string signature = context.Request["signature"];
                string timestamp = context.Request["timestamp"];
                string nonce = context.Request["nonce"];
                string echostr = context.Request["echostr"];

                LogHelper.Instance.AddInfoLog("signature:" + signature + "; timestamp: " + timestamp + "; nonce: " + nonce + "; echostr: " + echostr);

                if (string.IsNullOrEmpty(signature) ||
                    string.IsNullOrEmpty(timestamp) ||
                    string.IsNullOrEmpty(nonce) ||
                    string.IsNullOrEmpty(echostr))
                {
                    return;
                }
                List<string> list = new List<string>()
                {
                    Config.token, timestamp, nonce
                };
                list.Sort();

                string hash_sha1_encoder = SHA1_Hash(list[0] + list[1] + list[2]);
                LogHelper.Instance.AddInfoLog("hash_sha1_encoder: " + hash_sha1_encoder);
                if (signature == hash_sha1_encoder)
                {
                    LogHelper.Instance.AddInfoLog("VerifyToken: True");
                    context.Response.Write(echostr);
                }
                else
                {
                    context.Response.Write("");
                }
            }
            catch (Exception exc)
            {

            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        //SHA1
        static public string SHA1_Hash(string str_sha1_in)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            ASCIIEncoding enc = new ASCIIEncoding();
            byte[] bytes_sha1_in = enc.GetBytes(str_sha1_in);
            byte[] bytes_sha1_out = sha1.ComputeHash(bytes_sha1_in);
            string str_sha1_out = BitConverter.ToString(bytes_sha1_out).Replace("-", "");
            //str_sha1_out = str_sha1_out.Replace("-", "");
            return str_sha1_out.ToLower();
        }
    }
}