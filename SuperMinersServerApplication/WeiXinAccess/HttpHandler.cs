using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WeiXinAccess
{
    public static class HttpHandler
    {
        //public static event Action<HttpGetReturnModel> AsyncGetCallback;

        public static bool AsyncGet(string url)
        {
            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(url);
            myReq.ContentType = "get";
            myReq.BeginGetResponse(o =>
            {
            }, null);

            return true;
        }
    }
}
