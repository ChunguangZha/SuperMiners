using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SuperMinersCustomServiceSystem
{
    /// <summary>
    /// Interaction logic for BusyWindow.xaml
    /// </summary>
    public partial class BusyWindow : Window
    {
        public delegate void CancelActionCallback(object arg);

        public event EventHandler Canceled;

        private static readonly int Delay = 500;

        private CancelActionCallback _cancelAction = null;
        private object _state = null;
        private object _cancelActionLock = new object();

        private object _objShow = new object();
        private bool _show = false;
        private ManualResetEvent _wait = new ManualResetEvent(false);
        private SynchronizationContext _syn;

        public BusyWindow()
        {
            InitializeComponent();
            this._syn = SynchronizationContext.Current;
        }


        #region Public Methods

        /// <summary>
        /// 打开 System.Windows.Controls.ChildWindow 并返回，而不等待 System.Windows.Controls.ChildWindow关闭。
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        public void Show(string msg, CancelActionCallback callback, object state)
        {
            _cancelAction = callback;
            _state = state;

            this.Title = msg;
            this.Show();
        }

        public void NoDelayShow()
        {
            base.Show();
        }

        public new void Show()
        {
            if (this._show)
            {
                return;
            }
            this._show = true;
            ThreadPool.QueueUserWorkItem(o =>
            {
                this._wait.WaitOne(Delay);
                this._syn.Send(o1 =>
                {
                    if (this._show)
                    {
                        base.Show();
                    }
                }, null);
            });

        }

        public void SetMessage(string message)
        {
            this.txtMessage.Text = message;
        }

        /// <summary>
        /// Sets current progress
        /// </summary>
        /// <param name="value"></param>
        public void SetProgress(double value)
        {
            prg.Visibility = Visibility.Visible;
            prg.IsIndeterminate = false;
            prg.Value = value;
        }

        #endregion

        #region Private Methods

        protected override void OnClosing(CancelEventArgs e)
        {
            lock (this._objShow)
            {
                this._show = false;
                this._wait.Set();
            }

            base.OnClosing(e);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            EventHandler handler = this.Canceled;
            if (null != handler)
            {
                handler(this, EventArgs.Empty);
            }

            lock (_cancelActionLock)
            {
                if (null != _cancelAction)
                {
                    _cancelAction(_state);
                }
            }

            this.Close();
        }

        #endregion
    }
}
