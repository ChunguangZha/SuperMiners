using SuperMinersWPF.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SuperMinersWPF.Wcf.Channel
{
    public static class RestClient
    {
        /// <summary>
        /// 2分钟超时
        /// </summary>
        private static int DefaultTimeout = 2 * 60 * 1000;

        #region Public Methods

        public static void Do(SynchronizationContext context, string url, string method, byte[] data, object userState, Func<Exception, byte[], bool, object, bool> resultHandler)
        {
            Send(context, url, method, data, userState, resultHandler);
        }

        public static void Post(SynchronizationContext context, string url, byte[] data, object userState, Func<Exception, byte[], bool, object, bool> resultHandler)
        {
            Send(context, url, "POST", data, userState, resultHandler);
        }

        public static void Get(SynchronizationContext context, string url, byte[] data, object userState, Func<Exception, byte[], bool, object, bool> resultHandler)
        {
            Send(context, url, "GET", data, userState, resultHandler);
        }

        public static void Put(SynchronizationContext context, string url, byte[] data, object userState, Func<Exception, byte[], bool, object, bool> resultHandler)
        {
            Send(context, url, "PUT", data, userState, resultHandler);
        }

        public static void Delete(SynchronizationContext context, string url, byte[] data, object userState, Func<Exception, byte[], bool, object, bool> resultHandler)
        {
            Send(context, url, "DELETE", data, userState, resultHandler);
        }

        public static void Callback(SynchronizationContext context, string url, byte[] data, Action<Exception, byte[]> resultHandler)
        {
            MyWebRequest req = null;
            try
            {
                req = new MyWebRequest(HttpWebRequest.Create(url));
                //LogHelper.Instance.AddErrorLog("RestClient Callback 1. " + url, null);

            }
            catch (Exception ex)
            {
                //LogHelper.Instance.AddErrorLog("RestClient Callback 1. Exception", null);

                context.Post(_ =>
                {
                    //LogHelper.Instance.AddErrorLog("RestClient Callback 1. Exception. to callback", null);

                    resultHandler(ex, null);
                }, null);
                return;
            }

            //LogHelper.Instance.AddErrorLog("RestClient Callback 2. " + url, null);

            req.Request.Method = "POST";
            if ((null == data) || (data.Length == 0))
            {
                req.Request.ContentLength = 0;

                //LogHelper.Instance.AddErrorLog("RestClient Callback 3. " + url, null);

                ReceiveCallback(context, req, resultHandler);
            }
            else
            {
                req.Request.ContentType = "application/json";

                try
                {
                    data = CryptEncoder.Build(data);
                    req.Request.ContentLength = data.Length;

                    //LogHelper.Instance.AddErrorLog("RestClient Callback 4. " + url, null);

                    req.Request.BeginGetRequestStream(new AsyncCallback(result =>
                    {
                        //LogHelper.Instance.AddErrorLog("RestClient Callback 5. " + url, null);

                        Stream stream = null;
                        try
                        {
                            stream = req.Request.EndGetRequestStream(result);
                            stream.Write(data, 0, data.Length);
                            stream.Close();
                            //LogHelper.Instance.AddErrorLog("RestClient Callback 6. " + url, null);

                        }
                        catch (Exception ex)
                        {
                            //LogHelper.Instance.AddErrorLog("RestClient Callback 7. " + url, null);

                            context.Post(_ =>
                            {
                                //LogHelper.Instance.AddErrorLog("RestClient Callback 8. " + url, null);

                                resultHandler(ex, null);
                            }, null);
                            return;
                        }

                        ReceiveCallback(context, req, resultHandler);

                    }), null);
                }
                catch (Exception ex)
                {
                    //LogHelper.Instance.AddErrorLog("RestClient Callback 9. " + url, null);

                    context.Post(_ =>
                    {
                        //LogHelper.Instance.AddErrorLog("RestClient Callback 10. " + url, null);

                        resultHandler(ex, null);
                    }, null);
                }
            }
        }

        #endregion

        #region Private Methods

        private static void Send(SynchronizationContext context, string url, string method, byte[] data, object userState, Func<Exception, byte[], bool, object, bool> resultHandler)
        {
            MyWebRequest req = null;
            try
            {
                req = new MyWebRequest(HttpWebRequest.Create(url));
            }
            catch (Exception ex)
            {
                if (null != resultHandler)
                {
                    context.Post(_ =>
                    {
                        resultHandler(ex, null, false, userState);
                    }, null);
                }
                return;
            }

            //BusyToken.AddRequest(req);
            //BusyToken.Show();

            req.Request.Method = method;
            if ((null == data) || (data.Length == 0))
            {
                req.Request.ContentLength = 0;

                Receive(context, req, userState, resultHandler);
            }
            else
            {
                req.Request.ContentType = "application/json";

                try
                {
                    data = CryptEncoder.Build(data);
                    req.Request.ContentLength = data.Length;

                    req.Request.BeginGetRequestStream(new AsyncCallback(result =>
                    {
                        Stream stream = null;
                        try
                        {
                            stream = req.Request.EndGetRequestStream(result);
                            stream.Write(data, 0, data.Length);
                            stream.Close();
                        }
                        catch (Exception ex)
                        {
                            context.Post(_ =>
                            {
                                //BusyToken.Hide();
                                if (null != resultHandler)
                                {
                                    resultHandler(ex, null, req.IsCancel, userState);
                                }
                            }, null);
                            return;
                        }

                        Receive(context, req, userState, resultHandler);

                    }), null);
                }
                catch (Exception ex)
                {
                    context.Post(_ =>
                    {
                        //BusyToken.Hide();
                        if (null != resultHandler)
                        {
                            resultHandler(ex, null, req.IsCancel, userState);
                        }
                    }, null);
                }
            }
        }

        private static void Receive(SynchronizationContext context, MyWebRequest req, object userState, Func<Exception, byte[], bool, object, bool> resultHandler)
        {
            try
            {
                req.Request.BeginGetResponse(new AsyncCallback(result =>
                {
                    WebResponse resp = null;
                    try
                    {
                        resp = req.Request.EndGetResponse(result);
                    }
                    catch (Exception ex)
                    {
                        //BusyToken.Hide();
                        if (null != resultHandler)
                        {
                            context.Post(_ =>
                            {
                                resultHandler(ex, null, req.IsCancel, userState);
                            }, null);
                        }
                        return;
                    }

                    byte[] resultData = null;
                    try
                    {
                        resultData = CryptEncoder.Read(resp.GetResponseStream());
                    }
                    catch (Exception ex)
                    {
                        //BusyToken.Hide();
                        if (null != resultHandler)
                        {
                            context.Post(_ =>
                            {
                                resultHandler(ex, null, req.IsCancel, userState);
                            }, null);
                        }
                        return;
                    }

                    //BusyToken.Hide();
                    if (null != resultHandler)
                    {
                        context.Post(_ =>
                        {
                            resultHandler(null, resultData, req.IsCancel, userState);
                        }, null);
                    }
                }), null);
            }
            catch (Exception ex)
            {
                //BusyToken.Hide();
                if (null != resultHandler)
                {
                    context.Post(_ =>
                    {
                        resultHandler(ex, null, req.IsCancel, userState);
                    }, null);
                }
            }
        }

        private static void ReceiveCallback(SynchronizationContext context, MyWebRequest req, Action<Exception, byte[]> resultHandler)
        {
            try
            {
                LogHelper.Instance.AddErrorLog("RestClient ReceiveCallback 1. " + req.Request.RequestUri.ToString(), null);

                IAsyncResult asyncResponseResult = req.Request.BeginGetResponse(new AsyncCallback(result =>
                {
                    WebResponse resp = null;
                    try
                    {
                        resp = req.Request.EndGetResponse(result);
                        LogHelper.Instance.AddErrorLog("RestClient ReceiveCallback 2", null);

                    }
                    catch (Exception ex)
                    {
                        LogHelper.Instance.AddErrorLog("RestClient ReceiveCallback 3", ex);

                        if (null != resultHandler)
                        {
                            context.Post(_ =>
                            {
                                LogHelper.Instance.AddErrorLog("RestClient ReceiveCallback 4", null);

                                resultHandler(ex, null);
                            }, null);
                        }
                        return;
                    }

                    byte[] resultData = null;
                    try
                    {
                        resultData = CryptEncoder.Read(resp.GetResponseStream());
                        LogHelper.Instance.AddErrorLog("RestClient ReceiveCallback 5", null);

                    }
                    catch (Exception ex)
                    {
                        LogHelper.Instance.AddErrorLog("RestClient ReceiveCallback 6", null);

                        if (null != resultHandler)
                        {
                            LogHelper.Instance.AddErrorLog("RestClient ReceiveCallback 7", null);

                            context.Post(_ =>
                            {
                                LogHelper.Instance.AddErrorLog("RestClient ReceiveCallback 8", null);

                                resultHandler(ex, null);
                            }, null);
                        }
                        return;
                    }

                    LogHelper.Instance.AddErrorLog("RestClient ReceiveCallback 9", null);

                    if (null != resultHandler)
                    {
                        context.Post(_ =>
                        {
                            //LogHelper.Instance.AddErrorLog("RestClient ReceiveCallback 10", null);

                            resultHandler(null, resultData);
                        }, null);
                    }
                }), null);


                ThreadPool.RegisterWaitForSingleObject(
                    asyncResponseResult.AsyncWaitHandle,
                    new WaitOrTimerCallback((state, timedOut) =>
                    {
                        try
                        {
                            if (timedOut)
                            {
                                LogHelper.Instance.AddErrorLog("time out : " + timedOut, null);

                                //if (null != resultHandler)
                                //{
                                //    context.Post(_ =>
                                //    {
                                //        resultHandler(new TimeoutException("AA AsyncGetWebResponse Exception"), null);
                                //    }, null);
                                //}
                                MyWebRequest request = state as MyWebRequest;
                                if (request != null && request.Request != null)
                                {
                                    request.Request.Abort();
                                }
                            }
                        }
                        catch (Exception exc)
                        {
                            if (null != resultHandler)
                            {
                                context.Post(_ =>
                                {
                                    resultHandler(exc, null);
                                }, null);
                            }
                        }
                        }),
                    req,
                    DefaultTimeout,
                    true);
            }
            catch (Exception ex)
            {
                LogHelper.Instance.AddErrorLog("RestClient ReceiveCallback 11", null);

                if (null != resultHandler)
                {
                    context.Post(_ =>
                    {
                        LogHelper.Instance.AddErrorLog("RestClient ReceiveCallback 12", null);

                        resultHandler(ex, null);
                    }, null);
                }
            }
        }

        #endregion
    }
}
