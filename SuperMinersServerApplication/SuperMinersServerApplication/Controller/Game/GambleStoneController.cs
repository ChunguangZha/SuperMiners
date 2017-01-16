using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace SuperMinersServerApplication.Controller.Game
{
    public class GambleStoneController
    {
        #region Single

        private static GambleStoneController _instance = new GambleStoneController();

        public static GambleStoneController Instance
        {
            get
            {
                return _instance;
            }
        }

        private GambleStoneController()
        {
            _timer = new Timer(OpenWinTimeSeconds * 1000);
            this._timer.Elapsed += _timer_Elapsed;
        }

        #endregion

        private int OpenWinTimeSeconds = 40;
        private Timer _timer = null;

        public void Init()
        {
            this._timer.Start();
        }

        public void StopService()
        {
            this._timer.Stop();
        }

        void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            throw new NotImplementedException();
        }


    }

    public class GambleStoneRoundRunner
    {

    }
}
