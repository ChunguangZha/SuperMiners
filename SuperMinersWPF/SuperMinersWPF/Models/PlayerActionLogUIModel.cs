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
                        message = "注册为新矿主。";
                        break;
                    case ActionType.Refer:
                        message = string.Format("成功推荐 {0} 位玩家，" + this._parentObject.Remark, this._parentObject.OperNumber);
                        break;
                    case ActionType.RMBRecharge:
                        break;
                    case ActionType.GoldCoinRecharge:
                        message = this._parentObject.Remark;
                        break;
                    case ActionType.BuyMine:
                        message = string.Format("购买了 {0} 座矿山，" + this._parentObject.Remark, this._parentObject.OperNumber);
                        break;
                    case ActionType.BuyMiner:
                        message = string.Format("购买了 {0} 位矿工", this._parentObject.OperNumber);
                        break;
                    case ActionType.BuyStone:
                        message = string.Format("购买了 {0} 矿石，并获取了 {1} 金币的奖励", this._parentObject.OperNumber, this._parentObject.Remark);
                        break;
                    case ActionType.SellStone:
                        message = string.Format("挂单出售 {0} 矿石", this._parentObject.OperNumber);
                        break;
                    case ActionType.SellDiamond:
                        message = string.Format("挂单出售 {0} 钻石", this._parentObject.OperNumber);
                        break;
                    case ActionType.GatherStone:
                        message = string.Format("收取了 {0} 矿石", this._parentObject.OperNumber);
                        break;
                    case ActionType.Login:
                        message = "进入矿场";
                        break;
                    case ActionType.WithdrawRMB:
                        message = this._parentObject.Remark;
                        break;
                    case ActionType.DelegateBuyStone:
                        message = string.Format("挂单委托收购 {0} 手矿石", this._parentObject.OperNumber);
                        break;
                    case ActionType.DelegateSellStone:
                        message = string.Format("挂单委托出售 {0} 手矿石", this._parentObject.OperNumber);
                        break;
                    case ActionType.DelegateBuyStoneSucceed:
                        message = string.Format("成功购买了 {0} 矿石，并获取了 {1} 金币的奖励", this._parentObject.OperNumber, this._parentObject.Remark);
                        break;
                    case ActionType.DelegateSellStoneSucceed:
                        message = string.Format("成功出售了 {0} 矿石", this._parentObject.OperNumber);
                        break;
                    case ActionType.GameRaiderJoinBet:
                        message = string.Format("第{0}期 夺宝奇兵 下注{1}矿石", this.ParentObject.OperNumber, this.ParentObject.Remark);
                        break;
                    case ActionType.GameRaiderWin:
                        message = string.Format("赢得 第{0}期 夺宝奇兵 获取{1}矿石", this.ParentObject.OperNumber, this.ParentObject.Remark);
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
