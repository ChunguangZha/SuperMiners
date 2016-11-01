using MetaData;
using MetaData.Game.Roulette;
using SuperMinersServerApplication.Utility;
using SuperMinersServerApplication.WebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.Controller.Game
{
    public class RouletteAwardController
    {
        #region Single

        private static RouletteAwardController _instance = new RouletteAwardController();

        public static RouletteAwardController Instance
        {
            get
            {
                return _instance;
            }
        }

        private RouletteAwardController()
        {

        }

        #endregion

        /// <summary>
        /// 奖项只限12个。Key：ID
        /// </summary>
        public Dictionary<int, RouletteAwardItem> _dicCurrentRouletteAwardItems = null;
        private object _lockCurrentAwardIems = new object();

        public Dictionary<int, RouletteAwardItem> _dicAllRouletteAwardItems = null;

        /// <summary>
        /// 临时中奖记录。key: UserName, value:AwardItemIndex
        /// </summary>
        public Dictionary<string, int> _tempRouletteWinnerRecord = new Dictionary<string, int>();

        /// <summary>
        /// 最终中奖记录。key: RecordID
        /// </summary>
        public List<RouletteWinnerRecord> _finishedRouletteWinnerRecord = new List<RouletteWinnerRecord>();

        public RouletteRoundInfo _currentRound = null;

        public decimal CurrentRoundProfitYuan
        {
            get
            {
                if (_currentRound == null)
                {
                    return 0;
                }

                return _currentRound.AwardPoolSumStone / GlobalConfig.GameConfig.Stones_RMB / GlobalConfig.GameConfig.Yuan_RMB - _currentRound.WinAwardSumYuan;
            }
        }

        private int _noneAwardID = -1;

        /// <summary>
        /// 小奖集合
        /// </summary>
        private List<int> _listSmallAwardID = new List<int>();
        /// <summary>
        /// 概率为0的大奖集合
        /// </summary>
        private List<int> _listLargeAwardID = new List<int>();


        public object _lockStart = new object();

        public void Init()
        {
            try
            {
                //从数据库读取
                var awardItems = DBProvider.GameRouletteDBProvider.GetCurrentRouletteAwardItemsList();
                if (awardItems != null)
                {
                    _dicCurrentRouletteAwardItems = new Dictionary<int, RouletteAwardItem>();
                    foreach (var item in awardItems)
                    {
                        _dicCurrentRouletteAwardItems.Add(item.ID, item);
                    }

                    var notPayRecords = DBProvider.GameRouletteDBProvider.GetNotPayWinAwardRecords();
                    if (notPayRecords != null)
                    {
                        foreach (var record in notPayRecords)
                        {
                            _dicCurrentRouletteAwardItems.TryGetValue(record.RouletteAwardItemID, out record.AwardItem);
                            this._finishedRouletteWinnerRecord.Add(record);
                        }
                    }

                    FindKeyIndex(); 
                    InitLastRouletteRoundInfoFromDB();
                }
                awardItems = DBProvider.GameRouletteDBProvider.GetAllRouletteAwardItems();
                if (awardItems != null)
                {
                    _dicAllRouletteAwardItems = new Dictionary<int, RouletteAwardItem>();
                    foreach (var item in awardItems)
                    {
                        this._dicAllRouletteAwardItems.Add(item.ID, item);
                    }
                }
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("RouletteAwardController.Init Exception", exc);
            }
        }

        public void SaveRouletteRoundInfoToData()
        {
            if (this._currentRound != null && this._currentRound.MustWinAwardItemID > 0)
            {
                DBProvider.GameRouletteDBProvider.SaveRouletteRoundInfo(this._currentRound);
            }
        }

        private void InitLastRouletteRoundInfoFromDB()
        {
            this._currentRound = DBProvider.GameRouletteDBProvider.GetLastRouletteRoundInfo();
            if (this._currentRound == null || this._currentRound.Finished)
            {
                this._currentRound = new RouletteRoundInfo()
                {
                    StartTime = DateTime.Now,
                    Finished = false,
                    MustWinAwardItemID = ReStartLargeAwardExponent()
                };
            }
            else
            {
                if (!this._listLargeAwardID.Contains(this._currentRound.MustWinAwardItemID))
                {
                    this._currentRound.MustWinAwardItemID = ReStartLargeAwardExponent();
                }
            }
        }

        private void CreateNewRound()
        {

            this._currentRound = new RouletteRoundInfo()
            {
                StartTime = DateTime.Now,
                Finished = false,
                MustWinAwardItemID = ReStartLargeAwardExponent()
            };
        }
        
        public RouletteWinnerRecord[] GetNotPayWinAwardRecords()
        {
            return this._finishedRouletteWinnerRecord.ToArray();
        }

        public RouletteRoundInfo[] GetAllRouletteRoundInfo()
        {
            var roundInfos = DBProvider.GameRouletteDBProvider.GetAllRouletteRoundInfo();
            if (roundInfos == null || roundInfos.Length == 0 || this._currentRound == null)
            {
                return new RouletteRoundInfo[] { this._currentRound };
            }

            var lastRound = roundInfos[roundInfos.Length - 1];
            if (!lastRound.Finished && lastRound.MustWinAwardItemID == this._currentRound.MustWinAwardItemID)
            {
                roundInfos[roundInfos.Length - 1] = this._currentRound;
            }

            return roundInfos;
        }

        public RouletteWinnerRecord[] GetAllPayWinAwardRecords(string UserName, int RouletteAwardItemID, bool ContainsNone, MyDateTime BeginWinTime, MyDateTime EndWinTime, int IsGot, int IsPay, int pageItemCount, int pageIndex)
        {
            if (this._dicAllRouletteAwardItems == null || this._dicAllRouletteAwardItems.Count == 0 || !this._dicAllRouletteAwardItems.ContainsKey(this._noneAwardID))
            {
                return null;
            }
            var records = DBProvider.GameRouletteDBProvider.GetAllPayWinAwardRecords(UserName, RouletteAwardItemID, ContainsNone, BeginWinTime, EndWinTime, IsGot, IsPay, pageItemCount, pageIndex, this._dicAllRouletteAwardItems[this._noneAwardID]);

            if (records != null)
            {
                foreach (var record in records)
                {
                    this._dicAllRouletteAwardItems.TryGetValue(record.RouletteAwardItemID, out record.AwardItem);
                }
            }

            return records;
        }

        private int ReStartLargeAwardExponent()
        {
            if (this._listLargeAwardID == null || this._listLargeAwardID.Count == 0)
            {
                return -1;
            }

            int index = r.Next(0, this._listLargeAwardID.Count);
            return this._listLargeAwardID[index];
        }

        private void FindKeyIndex()
        {
            this._listLargeAwardID.Clear();
            this._listSmallAwardID.Clear();
            foreach (var item in this._dicCurrentRouletteAwardItems.Values)
            {
                if (item.RouletteAwardType == RouletteAwardType.None)
                {
                    this._noneAwardID = item.ID;
                }
                if (item.WinProbability == 0)
                {
                    this._listLargeAwardID.Add(item.ID);
                }
                else
                {
                    this._listSmallAwardID.Add(item.ID);
                }
            }
        }

        public int AddAwardItem(RouletteAwardItem item)
        {
            //保存到数据库
            bool isOK = DBProvider.GameRouletteDBProvider.AddRouletteAwardItem(item);
            if (isOK)
            {
                return OperResult.RESULTCODE_TRUE;
            }

            return OperResult.RESULTCODE_FALSE;
        }

        public int UpdateAwardItem(RouletteAwardItem item)
        {
            //保存到数据库
            bool isOK = DBProvider.GameRouletteDBProvider.UpdateRouletteAwardItem(item);
            if (isOK)
            {
                if (_dicCurrentRouletteAwardItems != null)
                {
                    if (_dicCurrentRouletteAwardItems.ContainsKey(item.ID))
                    {
                        SaveRouletteRoundInfoToData();
                        this.Init();
                    }
                }

                return OperResult.RESULTCODE_TRUE;
            }

            return OperResult.RESULTCODE_FALSE;
        }

        public int DeleteAwardItem(RouletteAwardItem item)
        {
            bool isOK = DBProvider.GameRouletteDBProvider.DeleteAwardItem(item);
            if (isOK)
            {
                return OperResult.RESULTCODE_TRUE;
            }

            return OperResult.RESULTCODE_FALSE;
        }

        public bool SetCurrentAwardItemsList(RouletteAwardItem[] items)
        {
            try
            {
                if (items == null || items.Length != 12)
                {
                    return false;
                }

                lock (_lockCurrentAwardIems)
                {
                    //保存到数据库
                    DBProvider.GameRouletteDBProvider.SaveCurrentRouletteAwardItemsList(items);

                    SaveRouletteRoundInfoToData();
                    Init();
                }
                return true;
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("SetAwardItems Exception.", exc);
                return false;
            }
        }

        public RouletteAwardItem[] GetCurrentAwardItems()
        {
            lock (_lockCurrentAwardIems)
            {
                if (this._dicCurrentRouletteAwardItems == null || this._dicCurrentRouletteAwardItems.Count == 0)
                {
                    return null;
                }

                return this._dicCurrentRouletteAwardItems.Values.ToArray();
            }
        }

        public RouletteAwardItem[] GetAllAwardItems()
        {
            return DBProvider.GameRouletteDBProvider.GetAllRouletteAwardItems();
        }

        public RouletteWinAwardResult Start(string userName)
        {
            RouletteWinAwardResult result = new RouletteWinAwardResult();
            if (_dicCurrentRouletteAwardItems == null)
            {
                result.OperResultCode = OperResult.RESULTCODE_FALSE;
                return result;
            }

            int value = PlayerController.Instance.RouletteSpendStone(userName, 100);
            if (value != OperResult.RESULTCODE_TRUE)
            {
                result.OperResultCode = value;
                return result;
            }

            lock (_lockStart)
            {
                this._currentRound.AwardPoolSumStone += GlobalConfig.GameConfig.RouletteSpendStone;

                if (this._currentRound.MustWinAwardItemID > 0)
                {
                    var mustWinAwardItem = this._dicCurrentRouletteAwardItems[this._currentRound.MustWinAwardItemID];
                    if (CurrentRoundProfitYuan >= (decimal)mustWinAwardItem.ValueMoneyYuan * 3)
                    {
                        //返回必中奖项，同时结束本轮，并保存到数据库
                        this._currentRound.WinAwardSumYuan += (decimal)mustWinAwardItem.ValueMoneyYuan;
                        this._currentRound.Finished = true;
                        //保存前一轮信息
                        this.SaveRouletteRoundInfoToData();
                        //开始新一轮信息
                        this.CreateNewRound();
                        //保存到数据库
                        this.SaveRouletteRoundInfoToData();

                        result.WinAwardItemID = mustWinAwardItem.ID;
                        result.OperResultCode = OperResult.RESULTCODE_TRUE;

                        if (_tempRouletteWinnerRecord.ContainsKey(userName))
                        {
                            _tempRouletteWinnerRecord.Remove(userName);
                        }
                        _tempRouletteWinnerRecord.Add(userName, mustWinAwardItem.ID);
                        return result;
                    }
                }
                
                var winAwardItem = ComputeWinAwardItem();

                this._currentRound.WinAwardSumYuan += (decimal)winAwardItem.ValueMoneyYuan;
                result.WinAwardItemID = winAwardItem.ID;
                result.OperResultCode = OperResult.RESULTCODE_TRUE;

                if (_tempRouletteWinnerRecord.ContainsKey(userName))
                {
                    _tempRouletteWinnerRecord.Remove(userName);
                }
                _tempRouletteWinnerRecord.Add(userName, winAwardItem.ID);
            }

            LogHelper.Instance.AddInfoLog("玩家["+ userName +"]，开始幸运大转盘抽奖。");

            return result;
        }

        private RouletteAwardItem ComputeWinAwardItem()
        {
            RouletteAwardItem winAwardItem = this._dicCurrentRouletteAwardItems[this._noneAwardID];
            int totalProbabilityMaxValue = ComputeWinAwardProbabilitySum();

            int ran = GetRandomValue(totalProbabilityMaxValue);
            int tempTotalProbability = 0;

            foreach (var awardItem in this._dicCurrentRouletteAwardItems.Values)
            {
                int awardWinProbability = (int)awardItem.WinProbability;
                if (awardWinProbability != 0)
                {
                    if (tempTotalProbability <= ran && ran <= tempTotalProbability + awardWinProbability)
                    {
                        winAwardItem = awardItem;
                    }
                    tempTotalProbability += (int)awardItem.WinProbability;
                }
            }

            return winAwardItem;
        }

        Random r = new Random(1);

        private int GetRandomValue(int maxValue)
        {
            int value = 0;
            for (int i = 0; i < maxValue; i++)
            {
                value = r.Next(1, maxValue);
            }

            return value;
        }

        public int ComputeWinAwardProbabilitySum()
        {
            int totalProbabilityMaxValue = 0;
            foreach(var awardItem in this._dicCurrentRouletteAwardItems.Values)
            {
                totalProbabilityMaxValue += (int)awardItem.WinProbability;
            }

            return totalProbabilityMaxValue;
        }

        public RouletteWinnerRecord Finish(int userID, string userName, string userNickName, int winAwardID)
        {
            int serverWinAwardID = -1;

            if (!this._tempRouletteWinnerRecord.TryGetValue(userName, out serverWinAwardID))
            {
                return null;
            }
            
            if (serverWinAwardID != winAwardID && winAwardID != this._noneAwardID)
            {
                return null;
            }
            this._tempRouletteWinnerRecord.Remove(userName);
            
            RouletteWinnerRecord record = new RouletteWinnerRecord()
            {
                RouletteAwardItemID = winAwardID,
                AwardItem = this._dicCurrentRouletteAwardItems[winAwardID],
                UserID = userID,
                UserName = userName,
                UserNickName = userNickName,
                WinTime = DateTime.Now,
                IsGot = false,
                IsPay = false
            };

            var awardItem = this._dicCurrentRouletteAwardItems[winAwardID];
            if (awardItem.RouletteAwardType != RouletteAwardType.None)
            {
                if (awardItem.RouletteAwardType != RouletteAwardType.RealAward)
                {
                    var isOK = PlayerController.Instance.RouletteWinVirtualAwardPayUpdatePlayer(userName, awardItem);
                    if (isOK)
                    {
                        record.IsGot = true;
                        record.GotTime = DateTime.Now;
                        record.IsPay = true;
                        record.PayTime = DateTime.Now;
                    }
                }

                //通知
                LogHelper.Instance.AddInfoLog("玩家[" + userName + "]，完成了幸运大转盘抽奖。并抽中" + record.AwardItem.AwardName);

                if (record.AwardItem.RouletteAwardType == RouletteAwardType.RealAward)
                {
                    this._finishedRouletteWinnerRecord.Add(record);
                }
            }
            else
            {
                record.IsGot = true;
                record.GotTime = DateTime.Now;
                record.IsPay = true;
                record.PayTime = DateTime.Now;
            }

            //Save Record
            DBProvider.GameRouletteDBProvider.AddRouletteWinnerRecord(record);
            var dbRecord = DBProvider.GameRouletteDBProvider.GetPayWinAwardRecord(record.UserID, record.UserName, winAwardID, record.WinTime);
            if (dbRecord != null)
            {
                record.RecordID = dbRecord.RecordID;
            }

            return record;
        }

        /// <summary>
        /// 领取奖励
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="recordID"></param>
        /// <param name="info1"></param>
        /// <param name="info2"></param>
        public int TakeAward(string userName, int recordID, string info1, string info2)
        {
            RouletteWinnerRecord record = _finishedRouletteWinnerRecord.FirstOrDefault(r=>r.RecordID == recordID);
            if (record == null)
            {
                return OperResult.RESULTCODE_GAME_WINAWARDRECORD_NOT_EXIST;
            }
            if (record.UserName != userName)
            {
                return OperResult.RESULTCODE_GAME_WINAWARDRECORD_NOT_EXIST;
            }
            record.IsGot = true;
            record.GotTime = DateTime.Now;
            record.GotInfo1 = info1;
            record.GotInfo2 = info2;
            //Save to DB
            DBProvider.GameRouletteDBProvider.SetWinnerRecordGot(record);

            //Notify Administrator
            LogHelper.Instance.AddInfoLog("玩家[" + userName + "]，领取了幸运转盘大奖，" + record.AwardItem.AwardName);


            return OperResult.RESULTCODE_TRUE;
        }

        public int PayAward(string adminUserName, string playerUserName, int recordID)
        {
            RouletteWinnerRecord record = _finishedRouletteWinnerRecord.FirstOrDefault(r=>r.RecordID == recordID);
            if (record == null)
            {
                return OperResult.RESULTCODE_GAME_WINAWARDRECORD_NOT_EXIST;
            }
            if (record.UserName != playerUserName)
            {
                return OperResult.RESULTCODE_GAME_WINAWARDRECORD_NOT_EXIST;
            }

            record.IsPay = true;
            record.PayTime = DateTime.Now;

            //save To DB
            DBProvider.GameRouletteDBProvider.SetWinnerRecordPay(record);
            //notify player
            LogHelper.Instance.AddInfoLog("管理员[" + adminUserName + "]，确认支付了玩家[" + playerUserName + "]的幸运转盘大奖，" + record.AwardItem.AwardName);

            string playerToken = ClientManager.GetToken(playerUserName);
            if (!string.IsNullOrEmpty(playerToken))
            {
                if (RouletteWinRealAwardPaySucceedNotify != null)
                {
                    RouletteWinRealAwardPaySucceedNotify(playerToken, record);
                }
            }

            _finishedRouletteWinnerRecord.Remove(record);

            return OperResult.RESULTCODE_TRUE;
        }

        /// <summary>
        /// key: token
        /// </summary>
        public event Action<string, RouletteWinnerRecord> RouletteWinRealAwardPaySucceedNotify;
    }
}
