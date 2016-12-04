using MetaData;
using SuperMinersWeiXin.Wcf.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperMinersWeiXin
{
    /// <summary>
    /// Summary description for CheckIDCardNo
    /// </summary>
    public class CheckIDCardNo : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var IDCardNo = context.Request["IDCardNo"];
            int result;
            result = WcfClient.Instance.CheckUserIDCardNoExist(IDCardNo);
            if (result == OperResult.RESULTCODE_FALSE)
            {
                context.Response.Write("OK");
            }
            else if (result == OperResult.RESULTCODE_TRUE)
            {
                context.Response.Write("身份证号已经被其它人使用，无法注册");
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