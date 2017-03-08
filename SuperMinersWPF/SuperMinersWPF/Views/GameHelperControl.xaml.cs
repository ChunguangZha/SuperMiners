using SuperMinersWPF.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Resources;
using System.Windows.Shapes;

namespace SuperMinersWPF.Views
{
    /// <summary>
    /// Interaction logic for GameHelperControl.xaml
    /// </summary>
    public partial class GameHelperControl : UserControl
    {
        string helpText = 
            "一、	购买矿工\r\n"+
"注册时系统赠送你一座初始矿山，价值<$注册赠送矿山价值人民币$>元；赠送<$注册赠送矿工数量$>矿工，每天可以轻松收益<$每天免费收益人民币$>元，您也可以通过购买更多的矿工来获取更多收益；金币可以通过充值来获取，或者您也可以在交易区中购买迅灵矿石来免费赚取金币，或者通过活动来获取金币。\r\n" +
"二、	领取收益\r\n" +
"全自动挖矿，无需人工干预，仅需每天进入矿场领取收益即可，注意事项：每<$100矿工数量$>矿工每天可以生产迅灵矿石<$100矿工每天生产矿石数量$>块，收益每<$收益时间$>分钟结算一次，最高可累计<$累计上限小时$>小时，当超过<$累计上限小时$>小时未领取收益将不在产生收益；\r\n" +
"三、	贡献值系统\r\n" +
"在矿场上充值、推广、娱乐、活动中可以获得贡献值，贡献值满<$提现贡献值$>时，即可提现；\r\n" + //贡献值达到<$折扣贡献值$>之后购买矿工可以获得相应的折扣；\r\n" +
"四、	推荐人系统\r\n" +
"矿场的发展靠大家，为了给予对网站宣传做出贡献的玩家一定奖励，特推出推广系统，每推荐一人矿场奖励<$推荐奖励金币$>金币；\r\n" +
"五、	矿石交易区\r\n" +
"在矿场上购买矿工，勘探矿山，开始挖矿，就可以源源不断的收集矿石，是不是速度总不尽人意呢？除了购买更多的矿工增加挖矿的速度，您也可以在矿石交易区中直接购买玩家挖出的矿石，我会告诉你这是最快收集矿石的方法么（没有之一）；在这完全是由玩家发起，玩家购买，玩家自主交易，网站担保，安全可靠；\r\n" +
"六、	聊天室系统\r\n" +
"在聊天室里大家可以畅所欲言，交流经验，“你今天挖了么？”；或者您在使用过程中碰到什么难题，或者您的事情太多，工作太忙，忘记了密码；或者注册的时候填错了信息，都可以在这里找客服为您解决；\r\n" +
"七、	排行榜系统\r\n" +
"我靠，他又第一了。排行榜系统让大家可以看到大神们拥有的财富，还可以领取排行榜奖励，快来计算一下，今天大神都挖了多少矿石；排行榜每天更新一次，快来看看你今天超过了多少人吧。\r\n" +
"八、	娱乐系统\r\n" +
"挖矿挖累了，看交易区看腻了，来这里娱乐一下吧，《幸运大转盘》、《快乐52》、《疯狂时时彩》、《一元夺宝》多种多样的休闲娱乐游戏等待着你，运气大碰撞，看看谁能一元钱买到“肾六”；\r\n" +
"九、	兴奋剂系统\r\n" +
"一个合格的矿场主要学会压榨矿工所有的剩余劳动力，让矿工快去干活；兴奋剂可以增加当前产量，但是矿工也是人，会累的，兴奋剂使用之后会进入一个疲劳期，想延长矿工的疲劳期的到来，那就来连续使用兴奋剂，最长可以累积7天哦。在使用兴奋剂的期间矿产是可以自动收取的哦，无需上线手动操作；\r\n";

        private Dictionary<string, decimal> _kv = new Dictionary<string, decimal>();

        public GameHelperControl()
        {
            InitializeComponent();

            if (GlobalData.ServerType == ServerType.Server1)
            {
                CreateVariableValue();
                SetText();
            }
            else
            {
                GetGameHelperFromResourceFile();
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void GetGameHelperFromResourceFile()
        {
            try
            {
                Uri uri = new Uri("Resources\\Server2GameHelper.txt", UriKind.Relative);//这个就是所以的pack uri。
                StreamResourceInfo info = Application.GetContentStream(uri);
                if (info == null)
                {
                    return;
                }
                Stream s = info.Stream;
                byte[] buffer = new byte[s.Length];
                s.Read(buffer, 0, buffer.Length);
                string x = Encoding.GetEncoding("gb2312").GetString(buffer);
                this.txtHelperInfo.Text = x;
                s.Close();
                s.Dispose();
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("Load Game Helper Exception", exc);
            }
        }

        private void CreateVariableValue()
        {
            if (_kv == null || GlobalData.RegisterUserConfig == null || GlobalData.GameConfig == null || GlobalData.AwardReferrerLevelConfig == null)
            {
                return;
            }

            _kv.Clear();

            _kv.Add("<$注册赠送矿山价值人民币$>", GlobalData.RegisterUserConfig.GiveToNewUserMines * GlobalData.GameConfig.StonesReservesPerMines /GlobalData.GameConfig.Stones_RMB / GlobalData.GameConfig.Yuan_RMB);
            _kv.Add("<$注册赠送矿工数量$>", GlobalData.RegisterUserConfig.GiveToNewUserMiners);
            _kv.Add("<$每天免费收益人民币$>", GlobalData.RegisterUserConfig.GiveToNewUserMiners * GlobalData.GameConfig.OutputStonesPerHour * 24 / GlobalData.GameConfig.Stones_RMB/GlobalData.GameConfig.Yuan_RMB);
            _kv.Add("<$100矿工数量$>", 100);
            _kv.Add("<$100矿工每天生产矿石数量$>", 100 * GlobalData.GameConfig.OutputStonesPerHour * 24);
            _kv.Add("<$收益时间$>", 1);
            _kv.Add("<$累计上限小时$>", GlobalData.GameConfig.TempStoneOutputValidHour);
            _kv.Add("<$提现贡献值$>", GlobalData.GameConfig.CanExchangeMinExp);
            _kv.Add("<$折扣贡献值$>", GlobalData.GameConfig.CanDiscountMinExp);
            _kv.Add("<$推荐奖励金币$>", GlobalData.AwardReferrerLevelConfig.GetAwardByLevel(1).AwardReferrerGoldCoin);
        }

        private void SetText()
        {
            try
            {
                foreach (var item in this._kv)
                {
                    helpText = helpText.Replace(item.Key, item.Value.ToString());
                }

                this.txtHelperInfo.Text = helpText;
            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("Read HelperTxt Error", exc);
            }
        }
    }
}
