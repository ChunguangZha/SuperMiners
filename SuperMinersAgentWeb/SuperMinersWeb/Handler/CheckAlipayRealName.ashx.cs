using MetaData;
using SuperMinersAgentWeb.Wcf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperMinersAgentWeb
{
    /// <summary>
    /// Summary description for CheckAlipayRealName
    /// </summary>
    public class CheckAlipayRealName : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var alipayRealName = context.Request["AlipayRealName"];
            int result;
            result = WcfClient.Instance.CheckUserAlipayRealNameExist(alipayRealName);
            if (result == OperResult.RESULTCODE_FALSE)
            {
                context.Response.Write("OK");
            }
            else if (result == OperResult.RESULTCODE_TRUE)
            {
                context.Response.Write("该实名已经被其它人使用，无法注册");
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