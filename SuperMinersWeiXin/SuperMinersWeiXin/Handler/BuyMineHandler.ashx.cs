using MetaData;
using SuperMinersWeiXin.Wcf.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperMinersWeiXin.Handler
{
    /// <summary>
    /// Summary description for BuyMineHandler
    /// </summary>
    public class BuyMineHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var xlUserName = context.User.Identity.Name;
            if (string.IsNullOrEmpty(xlUserName) && context.Session == null)
            {
                context.Response.Write("登录失败");
                return;
            }

            var result = WcfClient.Instance.BuyMine(xlUserName, 1, MetaData.Trade.PayType.RMB);
            if (result.ResultCode == OperResult.RESULTCODE_TRUE)
            {
                context.Response.Write("OK");
            }
            else
            {
                context.Response.Write(OperResult.GetMsg(result.ResultCode));
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