using MetaData;
using SuperMinersWeiXin.Wcf.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperMinersWeiXin.Handler
{
    /// <summary>
    /// Summary description for BuyStoneHandler
    /// </summary>
    public class BuyStoneHandler : IHttpHandler
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
            object objParam_orderNumber = context.Request["orderNumber"];
            object objParam_sellerUserName = context.Request["sellerUserName"];
            string orderNumber = objParam_orderNumber as string;
            if (string.IsNullOrEmpty(orderNumber))
            {
                context.Response.Write("请选择订单");
                return;
            }
            string sellerUserName = objParam_sellerUserName as string;
            if (string.IsNullOrEmpty(sellerUserName))
            {
                context.Response.Write("请选择订单");
                return;
            }

            if (sellerUserName == xlUserName)
            {
                context.Response.Write("不能购买自己的订单");
                return;
            }

            var result = WcfClient.Instance.BuyStone(xlUserName, orderNumber);
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