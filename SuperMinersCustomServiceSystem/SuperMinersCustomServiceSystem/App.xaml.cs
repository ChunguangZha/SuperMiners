using SuperMinersCustomServiceSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SuperMinersCustomServiceSystem
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static BusyToken BusyToken = new BusyToken();
        public static PlayerViewModel PlayerVMObject = new PlayerViewModel();
        public static NoticeViewModel NoticeVMObject = new NoticeViewModel();
        public static WithdrawRMBViewModel WithdrawRMBVMObject = new WithdrawRMBViewModel();
        public static StoneTradeViewModel StoneTradeVMObject = new StoneTradeViewModel();
        public static MineTradeViewModel MineTradeVMObject = new MineTradeViewModel();
        public static MinerTradeViewModel MinerTradeVMObject = new MinerTradeViewModel();
        public static GoldCoinTradeViewModel GoldCoinTradeVMObject = new GoldCoinTradeViewModel();
        public static AlipayRechargeViewModel AlipayRechargeVMObject = new AlipayRechargeViewModel();
        public static GameRouletteViewModel GameRouletteVMObject = new GameRouletteViewModel();
        public static StoneDelegateTradeViewModel StoneDelegateTradeVMObject = new StoneDelegateTradeViewModel();
        public static RemoteServiceViewModel RemoteServiceVMObject = new RemoteServiceViewModel();
        public static ShoppingViewModel ShoppingVMObject = new ShoppingViewModel();
        public static StoneFactoryViewModel StoneFactoryVMObject = new StoneFactoryViewModel();


        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            PlayerVMObject.RegisterEvents();
            NoticeVMObject.RegisterEvents();
        }
    }
}
