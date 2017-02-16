using MetaData.ActionLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersWPF.Models
{
    public class PlayerActionLogUIModel
    {
        PlayerActionLog _parentObject;

        public PlayerActionLogUIModel(PlayerActionLog parent)
        {
            this._parentObject = parent;
        }

        public PlayerActionLog ParentObject
        {
            get
            {
                return _parentObject;
            }
        }

        public DateTime Time
        {
            get
            {
                return this._parentObject.Time;
            }
        }

        public string UserName
        {
            get
            {
                return this._parentObject.UserName;
            }
        }

        public string LogMessage
        {
            get
            {
                string message = "";
                switch (this._parentObject.ActionType)
                {
                    case ActionType.Register:
                        message = string.Format("新人 {0} 进入矿场，快去申请救济吧。", this.UserName);
                        break;
                    case ActionType.Refer:
                        message = string.Format("成功推荐 {0} 位玩家，" + this._parentObject.Remark, this._parentObject.OperNumber);
                        break;
                    case ActionType.RMBRecharge:
                        break;
                    case ActionType.GoldCoinRecharge:
                        message = string.Format("矿主 {0} 兑换了一些金币", this.UserName);
                        break;
                    case ActionType.BuyMine:
                        message = string.Format("矿主 {0} 又勘探了一条新的矿脉", this.UserName);
                        break;
                    case ActionType.BuyMiner:
                        message = string.Format("矿主 {0} 又增加了{1}位矿工", this.UserName, this._parentObject.OperNumber);
                        break;
                    case ActionType.BuyStone:
                        message = string.Format("购买了 {0} 矿石，并获取了 {1} 金币的奖励", this._parentObject.OperNumber, this._parentObject.Remark);
                        break;
                    //case ActionType.SellStone:
                    //    message = string.Format("挂单出售 {0} 矿石", this._parentObject.OperNumber);
                    //    break;
                    //case ActionType.SellDiamond:
                    //    message = string.Format("挂单出售 {0} 钻石", this._parentObject.OperNumber);
                    //    break;
                    case ActionType.GatherStone:
                        message = string.Format("矿主 {0} 收取了{1}矿石", this.UserName, this._parentObject.OperNumber);
                        break;
                    case ActionType.Login:
                        if (this._parentObject.OperNumber > 0)
                        {
                            message = string.Format("VIP{0} {1} 进入矿场，各种特权随心享。", this._parentObject.OperNumber, this.UserName);
                        }
                        else
                        {
                            message = string.Format("矿主 {0} 进入矿场，矿石交易赢翻天。", this.UserName);
                        }
                        break;
                    case ActionType.WithdrawRMB:
                        message = this._parentObject.Remark;
                        break;
                    //case ActionType.DelegateBuyStone:
                    //    message = string.Format("挂单委托收购 {0} 手矿石", this._parentObject.OperNumber);
                    //    break;
                    //case ActionType.DelegateSellStone:
                    //    message = string.Format("挂单委托出售 {0} 手矿石", this._parentObject.OperNumber);
                    //    break;
                    case ActionType.DelegateBuyStoneSucceed:
                        message = string.Format("{0} 看准时机，低价收购了一批矿石。", this.UserName);
                        break;
                    case ActionType.DelegateSellStoneSucceed:
                        message = string.Format("{0} 抓住机会，高价出手了一批矿石。", this.UserName);
                        break;
                    case ActionType.GameRoulette:
                        message = string.Format("江湖速报，{0} 在幸运大转盘的游戏中获得{1}。", this.UserName, this._parentObject.Remark);
                        break;
                    case ActionType.GameRaiderJoinBet:
                        message = string.Format("江湖速报，{0} 在{1}期夺宝奇兵中下注{2}矿石。", this.UserName, this.ParentObject.OperNumber, this.ParentObject.Remark);
                        break;
                    case ActionType.GameRaiderWin:
                        message = string.Format("江湖速报，{0} 在{1}期夺宝奇兵中获得头魁，赢取{2}矿石。", this.UserName, this.ParentObject.OperNumber, this.ParentObject.Remark);
                        break;
                    case ActionType.GambleStoneMaxWinner:
                        message = string.Format("江湖速报，{0} 在疯狂猜石游戏中猜中大奖，赢取{1}矿石。", this.UserName, this.ParentObject.OperNumber);
                        break;
                    case ActionType.ShoppingCredits:
                        message = string.Format("矿主 {0} 通过活动获赠积分{1}。", this.UserName, this.ParentObject.OperNumber);
                        break;
                    default:
                        message = this._parentObject.Remark;
                        break;
                }

                return message;
            }
        }
    }
}
