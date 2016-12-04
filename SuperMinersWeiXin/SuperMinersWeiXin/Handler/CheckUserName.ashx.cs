using MetaData;
using SuperMinersWeiXin.Wcf.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperMinersWeiXin
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
            if (result == OperResult.RESULTCODE_FALSE)
            {
                context.Response.Write("OK");
            }
            else if (result == OperResult.RESULTCODE_TRUE)
            {
                context.Response.Write("用户名已经存在，请选择其它用户名");
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