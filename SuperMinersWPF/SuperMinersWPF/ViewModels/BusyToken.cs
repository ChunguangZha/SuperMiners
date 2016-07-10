using SuperMinersWPF.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersWPF.ViewModels
{
    class BusyToken
    {
        #region Busy Window Methods

        private BusyWindow _busy;
        private object _lockShowBusy = new object();
        private int _showBusyCount = 0;

        public void ShowBusyWindow(string message)
        {
            lock (_lockShowBusy)
            {
                _showBusyCount++;

                if (_busy == null)
                {
                    _busy = new BusyWindow();
                    _busy.SetMessage(message);
                    _busy.Show();
                }
            }
        }

        public void CloseBusyWindow()
        {
            lock (_lockShowBusy)
            {
                _showBusyCount--;

                if (_showBusyCount <= 0)
                {
                    _showBusyCount = 0;
                    if (_busy != null)
                    {
                        _busy.Close();
                        _busy = null;
                    }
                }
            }
        }

        public void CloseAllBusyWindow()
        {
            lock (_lockShowBusy)
            {
                _showBusyCount = 0;
                if (_busy != null)
                {
                    _busy.Close();
                    _busy = null;
                }
            }
        }

        #endregion
        
    }
}
