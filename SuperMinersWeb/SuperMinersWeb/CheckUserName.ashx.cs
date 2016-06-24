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
            context.Response.Write("Hello " + userName);
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