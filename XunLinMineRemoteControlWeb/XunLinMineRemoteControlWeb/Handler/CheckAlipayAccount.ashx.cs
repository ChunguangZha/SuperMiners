using MetaData;
using XunLinMineRemoteControlWeb.Wcf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XunLinMineRemoteControlWeb
{
    /// <summary>
    /// Summary description for CheckAlipay
    /// </summary>
    public class CheckAlipayAccount : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var alipayAccount = context.Request["AlipayAccount"];
            int result;
            result = WcfClient.Instance.CheckUserAlipayAccountExist(alipayAccount);
            if (result == OperResult.RESULTCODE_FALSE)
            {
                context.Response.Write("OK");
            }
            else if (result == OperResult.RESULTCODE_TRUE)
            {
                context.Response.Write("该支付宝已经被其它人使用，无法注册");
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