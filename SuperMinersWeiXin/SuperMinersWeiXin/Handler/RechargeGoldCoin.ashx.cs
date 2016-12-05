using MetaData;
using MetaData.User;
using SuperMinersWeiXin.Wcf.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace SuperMinersWeiXin.Handler
{
    /// <summary>
    /// Summary description for RechargeGoldCoin
    /// </summary>
    public class RechargeGoldCoin : IHttpHandler, IRequiresSessionState
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
            //PlayerInfo player = context.Session[xlUserName] as PlayerInfo;
            //if (player == null)
            //{
            //    context.Response.Write("登录失败");
            //    return;
            //}

            var result = WcfClient.Instance.RechargeGoldCoin(xlUserName, number, MetaData.Trade.PayType.RMB);
            if (result.ResultCode == OperResult.RESULTCODE_TRUE)
            {
                context.Response.Write("OK");
            }
            else if (result.ResultCode == OperResult.RESULTCODE_TRUE)
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