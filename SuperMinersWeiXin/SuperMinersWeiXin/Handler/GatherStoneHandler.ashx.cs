using MetaData;
using SuperMinersWeiXin.Wcf.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperMinersWeiXin.Handler
{
    /// <summary>
    /// Summary description for GatherStoneHandler
    /// </summary>
    public class GatherStoneHandler : IHttpHandler
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

            //按服务器计算的临时产出收取
            var result = WcfClient.Instance.GatherStones(xlUserName, 10);
            if (result == null)
            {
                context.Response.Write("服务器连接失败");
                return;
            }

            if (result.OperResult == OperResult.RESULTCODE_TRUE)
            {
                context.Response.Write("OK," + result.GatherStoneCount);
            }
            else
            {
                string messag = OperResult.GetMsg(result.OperResult);
                context.Response.Write(messag);
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