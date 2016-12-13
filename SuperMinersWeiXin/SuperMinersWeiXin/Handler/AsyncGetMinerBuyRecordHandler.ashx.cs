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
    /// Summary description for AsyncGetMinerBuyRecordHandler
    /// </summary>
    public class AsyncGetMinerBuyRecordHandler : IHttpAsyncHandler
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
                //context.Response.ContentType = "text/plain";
                //context.Response.Cache.SetNoStore();
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
                    return WcfClient.Instance.GetBuyMinerFinishedRecordList(xlUserName, pageitemcount, pageindex);
                },
                xlUserName);
                asyncResult.StartWork();

                return asyncResult;
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("AsyncGetMinerBuyRecordHandler.BeginProcessRequest Exception", exc);
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

                if (myresult.Exception != null)
                {
                    LogHelper.Instance.AddErrorLog("AsyncGetMinerBuyRecordHandler.GetResult Exception", myresult.Exception);
                    myresult.Context.Response.Write("0" + myresult.Exception.Message);
                    return;
                }

                string jsonString = "";
                if (myresult.Result != null)
                {
                    MinersBuyRecord[] orders = myresult.Result as MinersBuyRecord[];
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
                LogHelper.Instance.AddErrorLog("AsyncGetMinerBuyRecordHandler.EndProcessRequest Exception", exc);
            }
        }
    }
}