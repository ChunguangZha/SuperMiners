using SuperMinersWeiXin.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace SuperMinersWeiXin.Core
{

    ///// <summary>
    ///// 对异步发送HTTP请求全过程的包装类，
    ///// 按IAsyncResult接口要求提供BeginSendHttpRequest/EndSendHttpRequest方法（一次回调）
    ///// </summary>
    ///// <typeparam name="TIn"></typeparam>
    ///// <typeparam name="TOut"></typeparam>
    //public class MyHttpClient<TIn, TOut>
    //{
    //    /// <summary>
    //    /// 用于保存额外的用户数据。
    //    /// </summary>
    //    public object UserData;

    //    public IAsyncResult BeginSendHttpRequest(string url, TIn input, AsyncCallback cb, object state)
    //    {
    //        // 准备返回值
    //        MyHttpAsyncResult ar = new MyHttpAsyncResult(cb, state);

    //        // 开始异步调用
    //        HttpWebRequestHelper<TIn, TOut>.SendHttpRequestAsync(url, input, SendHttpRequestCallback, ar);
    //        return ar;
    //    }

    //    private void SendHttpRequestCallback(TIn input, TOut result, Exception ex, object state)
    //    {
    //        // 进入这个方法表示异步调用已完成
    //        MyHttpAsyncResult ar = (MyHttpAsyncResult)state;

    //        // 设置完成状态，并发出完成通知。
    //        ar.SetCompleted(ex, result);
    //    }

    //    public TOut EndSendHttpRequest(IAsyncResult ar)
    //    {
    //        if (ar == null)
    //            throw new ArgumentNullException("ar");

    //        // 说明：我并没有检查ar对象是不是与之匹配的BeginSendHttpRequest实例方法返回的，
    //        // 虽然这是不规范的，但我还是希望示例代码能更简单。
    //        // 我想应该极少有人会乱传递这个参数。

    //        MyHttpAsyncResult myResult = ar as MyHttpAsyncResult;
    //        if (myResult == null)
    //            throw new ArgumentException("无效的IAsyncResult参数，类型不是MyHttpAsyncResult。");

    //        if (myResult.EndCalled)
    //            throw new InvalidOperationException("不能重复调用EndSendHttpRequest方法。");

    //        myResult.EndCalled = true;
    //        myResult.WaitForCompletion();

    //        return (TOut)myResult.Result;
    //    }
    //}

    internal class MyHttpAsyncResult : IAsyncResult
    {
        internal MyHttpAsyncResult(AsyncCallback callBack, object state)
        {
            _state = state;
            _asyncCallback = callBack;
        }

        internal object Result { get; private set; }
        internal bool EndCalled;

        private object _state;
        private volatile bool _isCompleted;
        private ManualResetEvent _event;
        private Exception _exception;
        private AsyncCallback _asyncCallback;


        public object AsyncState
        {
            get { return _state; }
        }
        public bool CompletedSynchronously
        {
            get { return false; } // 其实是不支持这个属性
        }
        public bool IsCompleted
        {
            get { return _isCompleted; }
        }
        public WaitHandle AsyncWaitHandle
        {
            get
            {
                if (_isCompleted)
                    return null;    // 注意这里并不返回WaitHandle对象。

                if (_event == null)     // 注意这里的延迟创建模式。
                    _event = new ManualResetEvent(false);
                return _event;
            }
        }

        internal void SetCompleted(Exception ex, object result)
        {
            this.Result = result;
            this._exception = ex;

            this._isCompleted = true;
            ManualResetEvent waitEvent = Interlocked.CompareExchange(ref _event, null, null);

            if (waitEvent != null)
                waitEvent.Set();        // 通知 EndSendHttpRequest() 的调用者

            if (_asyncCallback != null)
                _asyncCallback(this);    // 调用 BeginSendHttpRequest()指定的回调委托
        }

        internal void WaitForCompletion()
        {
            if (_isCompleted == false)
            {
                WaitHandle waitEvent = this.AsyncWaitHandle;
                if (waitEvent != null)
                    waitEvent.WaitOne();    // 使用者直接(非回调方式)调用了EndSendHttpRequest()方法。
            }

            if (_exception != null)
                throw _exception;    // 将异步调用阶段捕获的异常重新抛出。
        }

        // 注意有二种线程竞争情况：
        //  1. 在回调线程中调用SetCompleted时，原线程访问AsyncWaitHandle
        //  2. 在回调线程中调用SetCompleted时，原线程调用WaitForCompletion

        // 说明：在回调线程中，会先调用SetCompleted，再调用WaitForCompletion
    }

    public class MyGetNotFinishedSellStoneAsyncResult : IAsyncResult
    {

        public object AsyncState
        {
            get { return _state; }
        }

        public WaitHandle AsyncWaitHandle
        {
            get { throw new NotImplementedException(); }
        }

        public bool CompletedSynchronously
        {
            get { return false; }
        }

        public bool IsCompleted
        {
            get { return _isCompleted; }
        }

        public object Result
        {
            get { return this._result; }
        }

        public Exception Exception
        {
            get { return this._exception; }
        }

        public HttpContext Context
        {
            get { return this._context; }
        }

        private object _result;

        private object _state;
        private volatile bool _isCompleted;
        private ManualResetEvent _event;
        private Exception _exception;
        private AsyncCallback _asyncCallback;
        private GetResultDelegate _action;
        private HttpContext _context;

        public MyGetNotFinishedSellStoneAsyncResult(HttpContext context, AsyncCallback callBack, GetResultDelegate action, object state)
        {
            this._context = context;
            this._asyncCallback = callBack;
            this._state = state;
            this._action = action;
        }

        public void SetCompleted()
        {
            this._isCompleted = true;

            if (this._asyncCallback != null)
            {
                this._asyncCallback(this);
            }
        }

        public void StartWork()
        {
            try
            {
                Thread thr = new Thread(AsyncWork);
                thr.IsBackground = true;
                thr.Start();
            }
            catch (Exception exc)
            {

            }
        }

        public void AsyncWork()
        {
            try
            {
                if (_action != null)
                {
                    this._result = _action();
                }
            }
            catch (Exception exc)
            {
                this._exception = exc;
            }

            SetCompleted();
        }
    }

    public delegate object GetResultDelegate();
}