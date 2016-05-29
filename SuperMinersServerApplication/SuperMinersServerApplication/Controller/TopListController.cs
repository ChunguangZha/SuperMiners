using MetaData.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.Controller
{
    class TopListController
    {
        #region Single Stone

        private static TopListController _instance = new TopListController();

        public static TopListController Instance
        {
            get { return _instance; }
        }

        private TopListController()
        {

        }

        #endregion


        /// <summary>
        /// 按天缓存。
        /// </summary>
        Dictionary<int, TopListInfo[]> _dicExpTopListBuffer = new Dictionary<int, TopListInfo[]>();
        Dictionary<int, TopListInfo[]> _dicBuyTopListBuffer = new Dictionary<int, TopListInfo[]>();
        Dictionary<int, TopListInfo[]> _dicReferrerCountTopListBuffer = new Dictionary<int, TopListInfo[]>();
        Dictionary<int, TopListInfo[]> _dicMinersTopListBuffer = new Dictionary<int, TopListInfo[]>();


        public TopListInfo[] GetExpTopList()
        {
            DateTime dateNow = DateTime.Now;
            int dayInYears = dateNow.DayOfYear;
            if (this._dicExpTopListBuffer.Count > 0)
            {
                if (this._dicExpTopListBuffer.ContainsKey(dayInYears))
                {
                    return this._dicExpTopListBuffer[dayInYears];
                }

                this._dicExpTopListBuffer.Clear();
            }

            var toplist = DBProvider.UserDBProvider.GetExpTopList();
            if (toplist != null)
            {
                this._dicExpTopListBuffer.Add(dayInYears, toplist);
            }

            return toplist;
        }

        public TopListInfo[] GetBuyTopList()
        {
            return null;
            //DateTime dateNow = DateTime.Now;
            //int dayInYears = dateNow.DayOfYear;
            //if (this._dicBuyTopListBuffer.Count > 0)
            //{
            //    if (this._dicBuyTopListBuffer.ContainsKey(dayInYears))
            //    {
            //        return this._dicBuyTopListBuffer[dayInYears];
            //    }

            //    this._dicBuyTopListBuffer.Clear();
            //}

            //var toplist = DBProvider.UserDBProvider.GetBuyTopList();
            //if (toplist != null)
            //{
            //    this._dicBuyTopListBuffer.Add(dayInYears, toplist);
            //}

            //return toplist;
        }

        public TopListInfo[] GetReferrerTopList()
        {
            DateTime dateNow = DateTime.Now;
            int dayInYears = dateNow.DayOfYear;
            if (this._dicReferrerCountTopListBuffer.Count > 0)
            {
                if (this._dicReferrerCountTopListBuffer.ContainsKey(dayInYears))
                {
                    return this._dicReferrerCountTopListBuffer[dayInYears];
                }

                this._dicReferrerCountTopListBuffer.Clear();
            }

            var toplist = DBProvider.UserDBProvider.GetReferrerTopList();
            if (toplist != null)
            {
                this._dicReferrerCountTopListBuffer.Add(dayInYears, toplist);
            }

            return toplist;
        }

        public TopListInfo[] GetMinerTopList()
        {
            DateTime dateNow = DateTime.Now;
            int dayInYears = dateNow.DayOfYear;
            if (this._dicMinersTopListBuffer.Count > 0)
            {
                if (this._dicMinersTopListBuffer.ContainsKey(dayInYears))
                {
                    return this._dicMinersTopListBuffer[dayInYears];
                }

                this._dicMinersTopListBuffer.Clear();
            }

            var toplist = DBProvider.UserDBProvider.GetMinerTopList();
            if (toplist != null)
            {
                this._dicMinersTopListBuffer.Add(dayInYears, toplist);
            }

            return toplist;
        }

    }
}
