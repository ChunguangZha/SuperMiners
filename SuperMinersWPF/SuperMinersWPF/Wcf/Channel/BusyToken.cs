using SuperMinersWPF.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SuperMinersWPF.Wcf.Channel
{
    public static class BusyToken
    {
        private static BusyWindow _busy = null;
        private static List<MyWebRequest> _reqList = new List<MyWebRequest>();
        private static object _locker = new object();

        private static SynchronizationContext _context;

        public static void SetContext(SynchronizationContext context)
        {
            _context = context;
        }

        public static void AddRequest(MyWebRequest req)
        {
            lock (_locker)
            {
                _reqList.Add(req);
            }
        }

        public static void RemoveRequest(MyWebRequest req)
        {
            lock (_locker)
            {
                _reqList.Remove(req);
            }
        }

        public static void Show()
        {
            lock (_locker)
            {
                if (null == _busy)
                {
                    if (SynchronizationContext.Current == _context)
                    {
                        _busy = new BusyWindow();
                        _busy.Canceled += new EventHandler(_busy_Canceled);
                        _busy.Show();
                    }
                    else
                    {
                        _context.Post(_ =>
                        {
                            _busy = new BusyWindow();
                            _busy.Canceled += new EventHandler(_busy_Canceled);
                            _busy.Show();
                        }, null);
                    }
                }
            }
        }

        public static void Hide()
        {
            lock (_locker)
            {
                if (null != _busy)
                {
                    if (SynchronizationContext.Current == _context)
                    {
                        _busy.Close();
                        _busy = null;
                    }
                    else
                    {
                        _context.Post(_ =>
                        {
                            _busy.Close();
                            _busy = null;
                        }, null);
                    }
                }
            }
        }

        /// <summary>
        /// 0 to 100
        /// </summary>
        /// <param name="progress"></param>
        public static void SetProgress(double progress)
        {
            lock (_locker)
            {
                if (null == _busy)
                {
                    return;
                }

                if (SynchronizationContext.Current == _context)
                {
                    _busy.SetProgress(progress);
                }
                else
                {
                    _context.Send(_ =>
                    {
                        _busy.SetProgress(progress);
                    }, null);
                }
            }
        }

        private static void _busy_Canceled(object sender, EventArgs e)
        {
            lock (_locker)
            {
                foreach (var item in _reqList)
                {
                    item.Cancel();
                }
            }
        }
    }
}
