using MetaData;
using SuperMinersWeb.Wcf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperMinersWeb.Handler
{
    /// <summary>
    /// Summary description for CheckNickName
    /// </summary>
    public class CheckNickName : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var userName = context.Request["UserName"];
            int result;
            result = WcfClient.Instance.CheckUserNameExist(userName);
            if (result == OperResult.RESULTCODE_FALSE)
            {
                context.Response.Write("OK");
            }
            else if (result == OperResult.RESULTCODE_TRUE)
            {
                context.Response.Write("昵称已经存在，请选择其它昵称");
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