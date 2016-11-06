using MetaData;
using SuperMinersAgentWeb.Wcf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperMinersAgentWeb
{
    /// <summary>
    /// Summary description for CheckEmail
    /// </summary>
    public class CheckEmail : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var email = context.Request["Email"];

            int result = WcfClient.Instance.CheckEmailExist(email);
            if (result == OperResult.RESULTCODE_FALSE)
            {
                context.Response.Write("OK");
            }
            else if (result == OperResult.RESULTCODE_TRUE)
            {
                context.Response.Write("该邮箱已被其它用户使用，请选择其它邮箱");
            }
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