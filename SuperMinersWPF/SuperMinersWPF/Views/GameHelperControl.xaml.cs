using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace SuperMinersWPF.Views
{
    /// <summary>
    /// Interaction logic for GameHelperControl.xaml
    /// </summary>
    public partial class GameHelperControl : UserControl
    {
        public GameHelperControl()
        {
            InitializeComponent();

            SetText();
        }

        private void SetText()
        {
            this.txtHelperInfo.Text = "使用帮助\r\n" + 
                                        "仅需简单操作，即可轻松赚钱\r\n" + 
                                        "一、	购买矿工\r\n" + 
"注册时系统赠送你一座初始矿山，价值<$矿山价值$>元；赠送<$矿工数量$>矿工，每天可以轻松收益<$免费收益$>元，您也可以通过购买更多的矿工来获取更多收益；金币可以通过充值来获取，或者您也可以在交易区中购买迅灵矿石来免费赚取金币，或者通过活动来获取金币。\r\n" +
"二、	领取收益\r\n" +
"全自动挖矿，无需人工干预，仅需每天进入矿场领取收益即可，注意事项：每<$矿工数量$>矿工每天可以生产迅灵矿石<$矿石数量$>块，收益每<$收益时间$>结算一次，最高可累计<$累计上限$>小时，当超过<$累计上限$>小时未领取收益将不在产生收益；\r\n" +
"三、	贡献值系统\r\n" +
"在网站上充值、推广、娱乐、活动中可以获得贡献值，贡献值达到<$贡献值数量$>之后购买矿工可以获得相应的折扣；\r\n" +
"四、	推荐人系统\r\n" +
"网站的发展靠大家，为了给予对网站宣传做出贡献的玩家一定奖励，特推出推广系统，每推荐一人网站奖励<$推荐奖励$>金币；\r\n" +
"五、	矿石交易区\r\n" +
"在网站上购买矿工，勘探矿山，开始挖矿，就可以源源不断的收集矿石，是不是速度总不尽人意呢？除了购买更多的矿工增加挖矿的速度，您也可以在矿石交易区中直接购买玩家挖出的矿石，我会告诉你这是最快收集矿石的方法么（没有之一）；在这完全是由玩家发起，玩家购买，玩家自主交易，网站担保，安全可靠；\r\n" +
"六、	聊天室系统\r\n" +
"在聊天室里大家可以畅所欲言，交流经验，“你今天挖了么？”；或者您在使用过程中碰到什么难题，或者您的事情太多，工作太忙，忘记了密码；或者注册的时候填错了信息，都可以在这里找客服为您解决；\r\n" +
"七、	排行榜系统\r\n" +
"我靠，他又第一了。排行榜系统让大家可以看到大神们拥有的财富，还可以领取排行榜奖励，快来计算一下，今天大神都挖了多少矿石；排行榜每<$排行榜更新时间$>更新一次，快来看看你今天超过了多少人吧。\r\n" +
"八、	娱乐系统\r\n" +
"挖矿挖累了，看交易区看腻了，来这里娱乐一下吧，《幸运大转盘》、《快乐52》、《疯狂时时彩》、《一元夺宝》多种多样的休闲娱乐游戏等待着你，运气大碰撞，看看谁能一元钱买到“肾六”；\r\n" +
"九、	兴奋剂系统\r\n" +
"一个合格的矿场主要学会压榨矿工所有的剩余劳动力，让矿工快去干活；兴奋剂可以增加当前<$产量增加值$>产量，但是矿工也是人，会累的，兴奋剂使用之后会进入一个疲劳期，想延长矿工的疲劳期的到来，那就来连续使用兴奋剂，最长可以累积7天哦。在使用兴奋剂的期间矿产是可以自动收取的哦，无需上线手动操作；\r\n";
        }
    }
}
