using MetaData;
using SuperMinersWeiXin.Wcf.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperMinersWeiXin.Handler
{
    /// <summary>
    /// Summary description for SellStoneHandler
    /// </summary>
    public class SellStoneHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var xlUserName = context.User.Identity.Name;
            object objParam_number = context.Request["number"];
            int number;
            if (objParam_number == null)
            {
                context.Response.Write("没有输入数值");
                return;
            }

            number = Convert.ToInt32(objParam_number);
            if (string.IsNullOrEmpty(xlUserName) && context.Session == null)
            {
                context.Response.Write("登录失败");
                return;
            }

            var result = WcfClient.Instance.SellStones(xlUserName, number);
            if (result == OperResult.RESULTCODE_TRUE)
            {
                context.Response.Write("OK");
            }
            else
            {
                context.Response.Write(OperResult.GetMsg(result));
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