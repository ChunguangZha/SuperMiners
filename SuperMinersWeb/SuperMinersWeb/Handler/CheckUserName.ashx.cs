using SuperMinersWeb.Wcf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperMinersWeb
{
    /// <summary>
    /// Summary description for CheckUserName
    /// </summary>
    public class CheckUserName : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var userName = context.Request["UserName"];
            int result;
            result = WcfClient.Instance.CheckUserNameExist(userName);
            if (result < 0)
            {
                return;
            }
            if (result > 0)
            {
                context.Response.Write("用户名已经存在，请选择其它用户名");
                return;
            }

            context.Response.Write("OK");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}