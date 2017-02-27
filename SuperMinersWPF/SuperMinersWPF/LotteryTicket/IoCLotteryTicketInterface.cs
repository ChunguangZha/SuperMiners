using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersWPF.LotteryTicket
{
    public class OperResultObject
    {
        /// <summary>
        /// 标识成功、失败
        /// </summary>
        public bool isOK;

        /// <summary>
        /// 失败的提示消息
        /// </summary>
        public string Message;
    }

    /// <summary>
    /// 彩票用户数据
    /// </summary>
    public class LotteryTicketUserData
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID;

        /// <summary>
        /// 用户积分
        /// </summary>
        public int UserShoppingCredits;
    }

    /// <summary>
    /// 
    /// </summary>
    public class LotteryTicketUserBetInRecord
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID;

        /// <summary>
        /// 用户下注号码，用数组支持下多注（如5,9,1,4,0）
        /// TODO:示例用，根据你具体下注和开奖号码方式自行修改
        /// </summary>
        public List<byte[]> BetInNumberList;

        /// <summary>
        /// 下注倍数
        /// </summary>
        public int BetInTimes;
    }

    /// <summary>
    /// 开奖结果
    /// </summary>
    public class LotteryResult
    {
        /// <summary>
        /// 奖期
        /// </summary>
        public int No;

        /// <summary>
        /// 开奖时间
        /// </summary>
        public DateTime Time;
        
        /// <summary>
        /// 开奖结果号码（如5,9,1,4,0）
        /// TODO:示例用，根据你具体下注和开奖号码方式自行修改
        /// </summary>
        public byte[] Result;
    }

    /// <summary>
    /// 彩票依赖注入交互接口
    /// </summary>
    interface IoCLotteryTicketInterface
    {
        /// <summary>
        /// 获取当前登录的用户信息
        /// </summary>
        /// <returns></returns>
        LotteryTicketUserData GetUserData();

        /// <summary>
        /// 用户下注
        /// </summary>
        /// <param name="betinRecord"></param>
        /// <returns></returns>
        OperResultObject BetIn(LotteryTicketUserBetInRecord betinRecord);

        /// <summary>
        /// 开奖
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        OperResultObject OpenLottery(LotteryResult result);

        /// <summary>
        /// 写错误日志
        /// </summary>
        /// <param name="logInfo"></param>
        /// <returns></returns>
        OperResultObject Log(string logInfo);
    }
}
