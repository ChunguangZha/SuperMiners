using SuperMinersServerApplication.Model;
using SuperMinersServerApplication.WebService.Contracts;
using SuperMinersWPF.Wcf.Clients;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.ServiceModel.Web;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SuperMinersWPF.Wcf.Channel
{
    public class RestInvoker<T> where T : class
    {
        #region MethodData
        private class MethodData
        {
            public string Url
            {
                get;
                set;
            }

            public string Method
            {
                get;
                set;
            }

            public Type ReturnType
            {
                get;
                set;
            }

            public Type[] ReturnKnownTypes
            {
                get;
                set;
            }

            public string[] ParaNames
            {
                get;
                set;
            }

            public Type[] ParaTypes
            {
                get;
                set;
            }

            public Type[][] ParaKnownTypes
            {
                get;
                set;
            }
        }
        #endregion

        public event EventHandler Error;

        #region Private Fields

        private Dictionary<string, MethodData> _methodDic = new Dictionary<string, MethodData>();
        private readonly object _methodDicLocker = new object();

        private static readonly string CallbackPrefix = "On";
        private static readonly string CallbackRaisePrefix = "RaiseOn";
        private static readonly string CallbackMethod = "Callback";
        private Dictionary<string, MethodInfo> _callbackDic = new Dictionary<string, MethodInfo>();
        private List<Type> _callbackKnownTypeList = new List<Type>();
        private readonly object _callbackDicLocker = new object();

        private SynchronizationContext _callbackContext;
        private bool _callbackRunning = false;
        private readonly object _callbackRunningLocker = new object();

        private string _baseUrl;
        private object _receiver;

        #endregion

        #region Constructor
        public RestInvoker()
        {
            Type type = typeof(T);
            if (!type.IsInterface)
            {
                throw new Exception("T must be interface");
            }

            var methods = type.GetMethods();
            foreach (var method in methods)
            {
                object[] attrs = method.GetCustomAttributes(false);
                if (null == attrs)
                {
                    continue;
                }

                foreach (var attr in attrs)
                {
                    WebInvokeAttribute invokeAttr = attr as WebInvokeAttribute;
                    if (null != invokeAttr)
                    {
                        MethodData data = new MethodData();
                        data.Url = invokeAttr.UriTemplate;
                        data.Method = invokeAttr.Method;
                        data.ReturnType = method.ReturnType;
                        data.ReturnKnownTypes = GetKnownTypes(method.ReturnType);

                        var paras = method.GetParameters();
                        if ((null == paras) || (paras.Length == 0))
                        {
                            data.ParaNames = null;
                            data.ParaTypes = null;
                            data.ParaKnownTypes = null;
                        }
                        else
                        {
                            data.ParaNames = paras.Select(para => "\"" + para.Name + "\"").ToArray();
                            data.ParaTypes = paras.Select(para => para.ParameterType).ToArray();
                            data.ParaKnownTypes = paras.Select(para => GetKnownTypes(para.ParameterType)).ToArray();
                        }

                        this._methodDic[method.Name] = data;
                        break;
                    }
                }
            }
        }
        #endregion

        #region Public Methods

        public void SetCallbackReceiver(object receiver)
        {
            lock (this._callbackDicLocker)
            {
                this._receiver = receiver;
                Type receiverType = receiver.GetType();

                this._callbackDic.Clear();
                this._callbackKnownTypeList.Clear();

                Type type = typeof(T);
                var methods = type.GetMethods();
                foreach (var method in methods)
                {
                    object[] attrs = method.GetCustomAttributes(typeof(CallbackAttribute), false);
                    if ((null == attrs) || (attrs.Length == 0))
                    {
                        continue;
                    }

                    if (method.ReturnType != typeof(void))
                    {
                        continue;
                    }

                    var pis = method.GetParameters();
                    if (null != pis)
                    {
                        foreach (var pi in pis)
                        {
                            if (!this._callbackKnownTypeList.Contains(pi.ParameterType))
                            {
                                this._callbackKnownTypeList.Add(pi.ParameterType);
                            }
                        }
                    }

                    var ev = receiverType.GetEvent(CallbackPrefix + method.Name);
                    if (null != ev)
                    {
                        var mi = receiverType.GetMethod(CallbackRaisePrefix + method.Name);
                        if (null != mi)
                        {
                            this._callbackDic[method.Name] = mi;
                        }
                    }
                }
            }
        }

        public void Init(string baseUrl)
        {
            this._baseUrl = baseUrl;
        }

        public void HandleCallback(SynchronizationContext context)
        {
            lock (this._callbackRunningLocker)
            {
                if (this._callbackRunning)
                {
                    return;
                }

                this._callbackRunning = true;
                this._callbackContext = context;

                this.InvokeCallback();
            }
        }

        public void Invoke(SynchronizationContext context, string methodName, EventHandler<WebInvokeEventArgs> resultHandler, params object[] paras)
        {
            InvokeUserState(context, methodName, resultHandler, null, paras);
        }

        /// <summary>
        /// 2015-08-21,jiang,
        /// add userState
        /// </summary>
        /// <param name="context"></param>
        /// <param name="methodName"></param>
        /// <param name="resultHandler"></param>
        /// <param name="userState"></param>
        /// <param name="paras"></param>
        public void InvokeUserState(SynchronizationContext context, string methodName, EventHandler<WebInvokeEventArgs> resultHandler, object userState, params object[] paras)
        {
            if (String.IsNullOrEmpty(this._baseUrl))
            {
                return;
            }

            MethodData method = GetMethodData(methodName, typeof(void), paras);

            byte[] paraData = GetParaData(paras, method);

            RestClient.Do(context, this._baseUrl + method.Url, method.Method, paraData, userState, new Func<Exception, byte[], bool, object, bool>((ex, result, cancel, state) =>
            {
                if (null != ex)
                {
                    if (ex is WebException)
                    {
                        if (((WebException)ex).Status == WebExceptionStatus.UnknownError)
                        {
                            if (null != this.Error)
                            {
                                this.Error(this, EventArgs.Empty);
                            }
                            return false;
                        }
                    }
                }

                if ((resultHandler != null))
                {
                    WebInvokeEventArgs arg = new WebInvokeEventArgs(ex, cancel, state);
                    resultHandler(this, arg);
                    return arg.Continue;
                }

                return false;
            }));
        }

        public void Invoke<T1>(SynchronizationContext context, string methodName, EventHandler<WebInvokeEventArgs<T1>> resultHandler, params object[] paras)
        {
            InvokeUserState<T1>(context, methodName, resultHandler, null, paras);
        }

        /// <summary>
        /// 2015-08-21,jiang,
        /// add userState
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="context"></param>
        /// <param name="methodName"></param>
        /// <param name="resultHandler"></param>
        /// <param name="userState"></param>
        /// <param name="paras"></param>
        public void InvokeUserState<T1>(SynchronizationContext context, string methodName, EventHandler<WebInvokeEventArgs<T1>> resultHandler, object userState, params object[] paras)
        {
            if (String.IsNullOrEmpty(this._baseUrl))
            {
                return;
            }

            MethodData method = GetMethodData(methodName, typeof(T1), paras);

            byte[] paraData = GetParaData(paras, method);

            RestClient.Do(context, this._baseUrl + method.Url, method.Method, paraData, userState, new Func<Exception, byte[], bool, object, bool>((ex, result, cancel, state) =>
            {
                if (null != ex)
                {
                    if (ex is WebException)
                    {
                        if (((WebException)ex).Status == WebExceptionStatus.UnknownError)
                        {
                            if (null != this.Error)
                            {
                                this.Error(this, EventArgs.Empty);
                            }
                            return false;
                        }
                    }
                }

                if ((resultHandler != null))
                {
                    T1 resultData = default(T1);
                    if (ex == null)
                    {
                        try
                        {
                            DataContractJsonSerializer s = new DataContractJsonSerializer(method.ReturnType, method.ReturnKnownTypes);
                            using (MemoryStream ms = new MemoryStream(result))
                            {
                                resultData = (T1)s.ReadObject(ms);
                            }
                        }
                        catch (Exception ex1)
                        {
                            ex = ex1;
                        }
                    }

                    WebInvokeEventArgs<T1> arg = new WebInvokeEventArgs<T1>(resultData, ex, cancel, state);
                    resultHandler(this, arg);
                    return arg.Continue;
                }

                return false;
            }));
        }

        #endregion

        #region Private Methods

        private void InvokeCallback()
        {
            if (!GlobalData.IsLogined)
            {
                lock (this._callbackRunningLocker)
                {
                    this._callbackRunning = false;
                }
                return;
            }

            object[] invokeParas = new object[] { GlobalData.Token };

            MethodData method = GetMethodData(CallbackMethod, typeof(CallbackInfo), invokeParas);

            byte[] paraData = GetParaData(invokeParas, method);

            RestClient.Callback(this._callbackContext, this._baseUrl + method.Url, paraData, new Action<Exception, byte[]>((ex, result) =>
            {
                if (null != ex)
                {
                    this.InvokeCallback();
                    return;
                }

                CallbackInfo arg;
                try
                {
                    DataContractJsonSerializer s = new DataContractJsonSerializer(method.ReturnType, method.ReturnKnownTypes);
                    using (MemoryStream ms = new MemoryStream(result))
                    {
                        arg = (CallbackInfo)s.ReadObject(ms);
                    }
                }
                catch
                {
                    this.InvokeCallback();
                    return;
                }

                if (null == arg)
                {
                    this.InvokeCallback();
                    return;
                }

                MethodInfo mi = null;
                lock (this._callbackDicLocker)
                {
                    if (!this._callbackDic.TryGetValue(arg.MethodName, out mi))
                    {
                        mi = null;
                    }
                }

                if (null != mi)
                {
                    try
                    {
                        var pInfos = mi.GetParameters();
                        object[] paras = null;
                        if (pInfos.Length > 0)
                        {
                            paras = new object[pInfos.Length];
                            for (int i = 0; i < paras.Length; i++)
                            {
                                DataContractJsonSerializer s = new DataContractJsonSerializer(pInfos[i].ParameterType, this._callbackKnownTypeList);
                                using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(arg.Parameters[i])))
                                {
                                    paras[i] = s.ReadObject(ms);
                                }
                            }
                        }

                        mi.Invoke(this._receiver, paras);
                    }
                    catch
                    {
                    }
                }

                this.InvokeCallback();
            }));
        }

        private static Type[] GetKnownTypes(Type type)
        {
            Func<MemberInfo, Type, bool> hasAttribute = (info, attrType) =>
            {
                var typeAttrs = info.GetCustomAttributes(attrType, false);
                if ((typeAttrs != null) && (typeAttrs.Length > 0))
                {
                    return true;
                }

                return false;
            };

            List<Type> typeList = new List<Type>();

            PropertyInfo[] props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in props)
            {
                if (hasAttribute(prop, typeof(DataMemberAttribute)))
                {
                    if (!typeList.Contains(prop.PropertyType))
                    {
                        typeList.Add(prop.PropertyType);
                    }
                }
            }

            return typeList.Count > 0 ? typeList.ToArray() : null;
        }

        private MethodData GetMethodData(string methodName, Type returnType, object[] paras)
        {
            MethodData method = null;
            lock (this._methodDicLocker)
            {
                if (!this._methodDic.TryGetValue(methodName, out method))
                {
                    throw new Exception("No method");
                }
            }

            if (method == null)
            {
                throw new Exception("No method");
            }

            if (null == paras)
            {
                if (null != method.ParaNames)
                {
                    throw new Exception("Invalid paras");
                }
            }
            else
            {
                if (null == method.ParaNames)
                {
                    throw new Exception("Invalid paras");
                }
                else
                {
                    if (paras.Length != method.ParaNames.Length)
                    {
                        throw new Exception("Invalid paras");
                    }
                }
            }

            if (method.ReturnType != returnType)
            {
                throw new Exception("Invalid retrun type");
            }
            return method;
        }

        private static byte[] GetParaData(object[] paras, MethodData method)
        {
            byte[] paraData = null;
            if (null != paras)
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("{");

                for (int i = 0; i < paras.Length; i++)
                {
                    sb.Append(method.ParaNames[i]);
                    sb.Append(":");

                    using (MemoryStream ms = new MemoryStream())
                    {
                        DataContractJsonSerializer s = new DataContractJsonSerializer(method.ParaTypes[i], method.ParaKnownTypes[i]);
                        s.WriteObject(ms, paras[i]);

                        byte[] tmp = ms.ToArray();
                        sb.Append(Encoding.UTF8.GetString(tmp, 0, tmp.Length).Trim());
                    }

                    if (i != paras.Length - 1)
                    {
                        sb.Append(",");
                    }
                }

                sb.Append("}");

                paraData = Encoding.UTF8.GetBytes(sb.ToString());
            }

            return paraData;
        }

        #endregion
    }
}
