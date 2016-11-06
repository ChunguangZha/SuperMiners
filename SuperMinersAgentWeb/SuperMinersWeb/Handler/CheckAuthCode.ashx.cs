using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperMinersAgentWeb
{
    /// <summary>
    /// Summary description for CheckAuthCode
    /// </summary>
    public class CheckAuthCode : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var authCode = context.Request["AuthCode"];
            HttpCookie cookie = context.Request.Cookies["CheckCode"];
            if (cookie.Value.ToLower() != authCode.ToLower())
            {
                context.Response.Write("验证码错误");
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