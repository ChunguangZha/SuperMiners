using MetaData;
using SuperMinersWeiXin.Core;
using SuperMinersWeiXin.Utility;
using SuperMinersWeiXin.Wcf.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;

namespace SuperMinersWeiXin.Handler
{
    /// <summary>
    /// Summary description for AsyncGetGoldCoinBuyRecordHandler
    /// </summary>
    public class AsyncGetGoldCoinBuyRecordHandler : IHttpAsyncHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData)
        {
            try
            {
                var xlUserName = context.User.Identity.Name;
                object p1 = context.Request["pageitemcount"];
                object p2 = context.Request["pageindex"];
                if (p1 == null || p2 == null)
                {
                    context.Response.Write("0param invalid");
                    return null;
                }

                int pageitemcount = Convert.ToInt32(p1);
                int pageindex = Convert.ToInt32(p2);

                MyGetNotFinishedSellStoneAsyncResult asyncResult = new MyGetNotFinishedSellStoneAsyncResult(context, cb, () =>
                {
                    if (string.IsNullOrEmpty(xlUserName))
                    {
                        throw new Exception("登录失败");
                    }
                    return WcfClient.Instance.GetFinishedGoldCoinRechargeRecordList(xlUserName, pageitemcount, pageindex);
                },
                xlUserName);
                asyncResult.StartWork();

                return asyncResult;
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("AsyncGetGoldCoinBuyRecordHandler.BeginProcessRequest Exception", exc);
                return null;
            }
        }

        public void EndProcessRequest(IAsyncResult result)
        {
            try
            {
                MyGetNotFinishedSellStoneAsyncResult myresult = result as MyGetNotFinishedSellStoneAsyncResult;
                if (myresult == null)
                {
                    return;
                }

                myresult.Context.Response.ContentType = "text/plain";
                myresult.Context.Response.Cache.SetNoStore();
                //myresult.Context.Response.Cache.VaryByParams.IgnoreParams = false;
                //myresult.Context.Response.Cache.SetCacheability(HttpCacheability.Private);
                //myresult.Context.Response.Cache.SetExpires(DateTime.Now.AddSeconds(10));

                if (myresult.Exception != null)
                {
                    LogHelper.Instance.AddErrorLog("AsyncGetGoldCoinBuyRecordHandler.GetResult Exception", myresult.Exception);
                    myresult.Context.Response.Write("0" + myresult.Exception.Message);
                    return;
                }

                string jsonString = "";
                if (myresult.Result != null)
                {
                    GoldCoinRechargeRecord[] orders = myresult.Result as GoldCoinRechargeRecord[];
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(orders.GetType());
                    MemoryStream ms = new MemoryStream();
                    serializer.WriteObject(ms, orders);
                    jsonString = Encoding.UTF8.GetString(ms.ToArray());
                    ms.Close();
                }
                myresult.Context.Response.Write("1" + jsonString);
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("AsyncGetGoldCoinBuyRecordHandler.EndProcessRequest Exception", exc);
            }
        }
    }
}