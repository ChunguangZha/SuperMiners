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
        /// 奖项只限12个
        /// </summary>
        public RouletteAwardItem[] _listRouletteAwardItems = null;
        private object _lockAwardIems = new object();

        /// <summary>
        /// 临时中奖记录。key: UserName, value:AwardItemIndex
        /// </summary>
        public Dictionary<string, int> _tempRouletteWinnerRecord = new Dictionary<string, int>();

        /// <summary>
        /// 最终中奖记录。key: RecordID
        /// </summary>
        public List<RouletteWinnerRecord> _finishedRouletteWinnerRecord = new List<RouletteWinnerRecord>();

        /// <summary>
        /// 奖池累计矿石数
        /// </summary>
        public int _tempTotalStone = 0;
        /// <summary>
        /// 概率为0的大奖本次中奖下标(每轮随机计算)
        /// </summary>
        private int _mustWinLargeAwardIndex = -1;

        private int _noneAwardIndex = -1;

        /// <summary>
        /// 小奖集合
        /// </summary>
        private int[] _smallAwardIndexes = null;
        /// <summary>
        /// 概率为0的大奖集合
        /// </summary>
        private int[] _largeAwardIndexes = null;


        public object _lockStart = new object();

        public void Init()
        {
            try
            {
                //从数据库读取
                _listRouletteAwardItems = DBProvider.GameRouletteDBProvider.GetRouletteAwardItems();
                if (_listRouletteAwardItems != null)
                {
                    var notPayRecords = DBProvider.GameRouletteDBProvider.GetNotPayWinAwardRecords();
                    if (notPayRecords != null)
                    {
                        foreach (var record in notPayRecords)
                        {
                            record.AwardItem = _listRouletteAwardItems.FirstOrDefault(o => o.ID == record.RouletteAwardItemID);
                            this._finishedRouletteWinnerRecord.Add(record);
                        }
                    }

                    FindKeyIndex(this._listRouletteAwardItems, out this._noneAwardIndex, out this._smallAwardIndexes, out this._largeAwardIndexes);
                    ReStartLargeAwardExponent();
                }
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("RouletteAwardController.Init Exception", exc);
            }
        }

        public RouletteWinnerRecord[] GetNotPayWinAwardRecords()
        {
            return this._finishedRouletteWinnerRecord.ToArray();
        }

        public RouletteWinnerRecord[] GetAllPayWinAwardRecords(string UserName, int RouletteAwardItemID, MyDateTime BeginWinTime, MyDateTime EndWinTime, int IsGot, int IsPay, int pageItemCount, int pageIndex)
        {
            if (this._listRouletteAwardItems == null)
            {
                return null;
            }
            var records = DBProvider.GameRouletteDBProvider.GetAllPayWinAwardRecords(UserName, RouletteAwardItemID, BeginWinTime, EndWinTime, IsGot, IsPay, pageItemCount, pageIndex);
            foreach (var record in records)
            {
                record.AwardItem = this._listRouletteAwardItems.FirstOrDefault(item => item.ID == record.RouletteAwardItemID);
            }

            return records;
        }

        private void ReStartLargeAwardExponent()
        {
            if (this._largeAwardIndexes == null || this._largeAwardIndexes.Length == 0)
            {
                _mustWinLargeAwardIndex = new Random(1).Next(0, this._largeAwardIndexes.Length);
            }

            _tempTotalStone = 0;
        }

        private void FindKeyIndex(RouletteAwardItem[] items, out int noneIndex, out int[] smallAwardIndexes, out int[] largeAwardIndexes)
        {
            List<int> listSmallAwardIndexes = new List<int>();
            List<int> listLargeAwardIndexes = new List<int>();
            int noneAwardIndex = -1;

            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].RouletteAwardType == RouletteAwardType.None)
                {
                    noneAwardIndex = i;
                }
                if (items[i].WinProbability == 0)
                {
                    listLargeAwardIndexes.Add(i);
                }
                else
                {
                    listSmallAwardIndexes.Add(i);
                }
            }

            noneIndex = noneAwardIndex;
            smallAwardIndexes = listSmallAwardIndexes.ToArray();
            largeAwardIndexes = listLargeAwardIndexes.ToArray();
        }

        public bool SetAwardItems(RouletteAwardItem[] items)
        {
            try
            {
                if (items == null || items.Length != 12)
                {
                    return false;
                }

                int noneIndex = -1;
                int[] level0AwardIndexes = null;
                int[] level1AwardIndexes = null;
                FindKeyIndex(items, out noneIndex, out level0AwardIndexes, out level1AwardIndexes);

                if (noneIndex < 0)
                {
                    return false;
                }

                lock (_lockAwardIems)
                {
                    this._listRouletteAwardItems = items;
                    this._noneAwardIndex = noneIndex;
                    this._smallAwardIndexes = level0AwardIndexes;
                    this._largeAwardIndexes = level1AwardIndexes;
                    ReStartLargeAwardExponent();

                    //保存到数据库
                    DBProvider.GameRouletteDBProvider.SaveRouletteAwardItems(items);
                }
                return true;
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("SetAwardItems Exception.", exc);
                return false;
            }
        }

        public RouletteAwardItem[] GetAwardItems()
        {
            lock (_lockAwardIems)
            {
                return this._listRouletteAwardItems;
            }
        }

        public RouletteWinAwardResult Start(string userName)
        {
            RouletteWinAwardResult result = new RouletteWinAwardResult();
            if (_listRouletteAwardItems == null)
            {
                result.OperResultCode = OperResult.RESULTCODE_FALSE;
                return result;
            }

            var playerRun = PlayerController.Instance.GetRunnable(userName);
            if (playerRun == null)
            {
                result.OperResultCode = OperResult.RESULTCODE_USER_OFFLINE;
                return result;
            }

            int value = playerRun.RouletteSpendStone(100);
            if (value != OperResult.RESULTCODE_TRUE)
            {
                result.OperResultCode = value;
                return result;
            }

            lock (_lockStart)
            {
                _tempTotalStone += GlobalConfig.GameConfig.RouletteSpendStone;

                if (_mustWinLargeAwardIndex >= 0)
                {
                    var mustWinAwardItem = this._listRouletteAwardItems[_mustWinLargeAwardIndex];
                    if (_tempTotalStone >= (int)((decimal)mustWinAwardItem.ValueMoneyYuan * GlobalConfig.GameConfig.Yuan_RMB * GlobalConfig.GameConfig.Stones_RMB))
                    {
                        result.WinAwardItemIndex = this._mustWinLargeAwardIndex;
                        result.OperResultCode = OperResult.RESULTCODE_TRUE;
                        return result;
                    }
                }

                int totalProbabilityMaxValue = ComputeWinAwardProbabilitySum();

                value = GetRandomValue(totalProbabilityMaxValue);
                int winAwardIndex = this._noneAwardIndex;
                int tempTotalProbability = 0;
                for (int i = 0; i < 12; i++)
                {
                    var awardItem = this._listRouletteAwardItems[i];
                    int awardWinProbability = (int)awardItem.WinProbability;
                    if (awardWinProbability != 0)
                    {
                        if (tempTotalProbability <= value && value <= tempTotalProbability + awardWinProbability)
                        {
                            winAwardIndex = i;
                        }
                        tempTotalProbability += (int)awardItem.WinProbability;
                    }
                }

                result.WinAwardItemIndex = winAwardIndex;
                result.OperResultCode = OperResult.RESULTCODE_TRUE;

                if (_tempRouletteWinnerRecord.ContainsKey(userName))
                {
                    _tempRouletteWinnerRecord.Remove(userName);
                }
                _tempRouletteWinnerRecord.Add(userName, winAwardIndex);
            }

            LogHelper.Instance.AddInfoLog("玩家["+ userName +"]，开始幸运大转盘抽奖。");

            return result;
        }

        private int GetRandomValue(int maxValue)
        {
            int value = 0;
            Random r = new Random(1);
            for (int i = 0; i < maxValue; i++)
            {
                value = r.Next(1, maxValue);
                //Console.WriteLine(value);
            }

            Console.WriteLine("Random : " + value);
            return value;
        }

        public int ComputeWinAwardProbabilitySum()
        {
            int totalProbabilityMaxValue = 0;
            for (int i = 0; i < 12; i++)
            {
                var awardItem = this._listRouletteAwardItems[i];
                totalProbabilityMaxValue += (int)awardItem.WinProbability;
            }

            return totalProbabilityMaxValue;
        }

        public RouletteWinnerRecord Finish(int userID, string userName, string userNickName, int winAwardIndex)
        {
            int serverWinAwardIndex = -1;

            if (!this._tempRouletteWinnerRecord.TryGetValue(userName, out serverWinAwardIndex))
            {
                return null;
            }
            
            if (serverWinAwardIndex != winAwardIndex && winAwardIndex != this._noneAwardIndex)
            {
                return null;
            }
            this._tempRouletteWinnerRecord.Remove(userName);
            
            RouletteWinnerRecord record = new RouletteWinnerRecord()
            {
                AwardItem = this._listRouletteAwardItems[winAwardIndex],
                UserID = userID,
                UserName = userName,
                UserNickName = userNickName,
                WinTime = DateTime.Now,
                IsGot = false,
                IsPay = false
            };

            //Save Record
            DBProvider.GameRouletteDBProvider.AddRouletteWinnerRecord(record);
            var dbRecord = DBProvider.GameRouletteDBProvider.GetPayWinAwardRecord(record.UserName, record.RouletteAwardItemID, record.WinTime);
            if (dbRecord != null)
            {
                record.RecordID = dbRecord.RecordID;
            }

            var awardItem = this._listRouletteAwardItems[winAwardIndex];
            if (awardItem.RouletteAwardType != RouletteAwardType.None)
            {
                if (!awardItem.IsRealAward)
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

                if (record.AwardItem.IsRealAward)
                {
                    this._finishedRouletteWinnerRecord.Add(record);
                }
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
