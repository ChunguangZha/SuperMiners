using MetaData.Trade;
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
    /// Summary description for AsyncGetSellStoneOrderHandler
    /// </summary>
    public class AsyncGetSellStoneOrderHandler : IHttpAsyncHandler
    {
        public IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData)
        {
            try
            {
                context.Response.ContentType = "text/plain";
                context.Response.Cache.SetNoStore();
                var xlUserName = context.User.Identity.Name;
                MyGetNotFinishedSellStoneAsyncResult asyncResult = new MyGetNotFinishedSellStoneAsyncResult(context, cb, () =>
                {
                    if (string.IsNullOrEmpty(xlUserName))
                    {
                        throw new Exception("登录失败");
                    }
                    return WcfClient.Instance.GetAllNotFinishedSellOrders(xlUserName);
                },
                xlUserName);
                asyncResult.StartWork();

                return asyncResult;
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("AsyncGetSellStoneOrderHandler.BeginProcessRequest Exception", exc);
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
                myresult.Context.Response.Cache.VaryByParams.IgnoreParams = false;
                myresult.Context.Response.Cache.SetCacheability(HttpCacheability.Public);
                myresult.Context.Response.Cache.SetExpires(DateTime.Now.AddSeconds(5));

                if (myresult.Exception != null)
                {
                    LogHelper.Instance.AddErrorLog("AsyncGetSellStoneOrderHandler.GetResult Exception", myresult.Exception);
                    myresult.Context.Response.Write("0" + myresult.Exception.Message);
                    return;
                }

                string jsonString = "";
                if (myresult.Result != null)
                {
                    SellStonesOrder[] orders = myresult.Result as SellStonesOrder[];

                    Dictionary<string, SellStonesOrder> dicOrders_VipPlayer = new Dictionary<string, SellStonesOrder>();
                    List<SellStonesOrder> listOrders_NormalPlayer = new List<SellStonesOrder>();
                    foreach (var item in orders)
                    {
                        if (item.OrderState == SellOrderState.Wait && item.SellerExpLevel > 0)
                        {
                            if (!dicOrders_VipPlayer.ContainsKey(item.SellerUserName))
                            {
                                dicOrders_VipPlayer.Add(item.SellerUserName, item);
                                continue;
                            }
                        }

                        listOrders_NormalPlayer.Add(item);
                    }

                    var listASC_VipOrders = dicOrders_VipPlayer.Values.OrderByDescending(s => s.SellTime).OrderByDescending(s => s.SellerCreditValue);
                    var listASC_NormalOrders = listOrders_NormalPlayer.OrderByDescending(s => s.SellTime).OrderByDescending(s => s.SellerCreditValue).OrderBy(s => s.OrderStateInt);
                    SellStonesOrder[] listAll = new SellStonesOrder[orders.Length];

                    int i = 0;
                    foreach (var item in listASC_VipOrders)
                    {
                        listAll[i] = item;
                        i++;
                    }
                    foreach (var item in listASC_NormalOrders)
                    {
                        listAll[i] = item;
                        i++;
                    }

                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(SellStonesOrder[]));
                    MemoryStream ms = new MemoryStream();
                    serializer.WriteObject(ms, listAll);
                    jsonString = Encoding.UTF8.GetString(ms.ToArray());
                    ms.Close();
                }
                myresult.Context.Response.Write("1" + jsonString);
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("AsyncGetSellStoneOrderHandler.EndProcessRequest Exception", exc);
            }
        }

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            throw new NotImplementedException();
        }
    }
}