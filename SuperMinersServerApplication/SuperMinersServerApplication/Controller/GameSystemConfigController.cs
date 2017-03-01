using DataBaseProvider;
using MetaData.SystemConfig;
using SuperMinersServerApplication.UIModel;
using SuperMinersServerApplication.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SuperMinersServerApplication.Controller
{
    class GameSystemConfigController : DependencyObject
    {
        #region Single

        private static GameSystemConfigController _instance = new GameSystemConfigController();

        public static GameSystemConfigController Instance
        {
            get { return _instance; }
        }

        private GameSystemConfigController()
        {

        }

        #endregion

        public event Action GameConfigChanged;

        public GameConfigUIModel InnerGameConfig { get; set; }
        //public IncomeMoneyAccountUIModel InnerIncomeMoneyAccount { get; set; }
        public RegisterUserConfigUIModel InnerRegisterPlayerConfig { get; set; }

        private ObservableCollection<AwardReferrerConfigUIModel> _innerListAwardReferrerConfig = new ObservableCollection<AwardReferrerConfigUIModel>();
        public ObservableCollection<AwardReferrerConfigUIModel> InnerListAwardReferrerConfig
        {
            get { return this._innerListAwardReferrerConfig; }
            set
            {
                this._innerListAwardReferrerConfig = value;
            }
        }

        #region AwardLevelFrom0

        /// <summary>
        /// From 0
        /// </summary>
        public int AwardLevelFrom0
        {
            get { return (int)GetValue(AwardLevelProperty); }
            set { SetValue(AwardLevelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AwardLevel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AwardLevelProperty =
            DependencyProperty.Register("AwardLevel", typeof(int), typeof(GameSystemConfigController), new PropertyMetadata(0, (d, e) =>
            {
                int level = (int)e.NewValue + 1;
                GameSystemConfigController controller = d as GameSystemConfigController;
                if (level < controller.InnerListAwardReferrerConfig.Count)
                {
                    while (level < controller.InnerListAwardReferrerConfig.Count)
                    {
                        controller.InnerListAwardReferrerConfig.RemoveAt(controller.InnerListAwardReferrerConfig.Count - 1);
                    }
                }
                else if(level > controller.InnerListAwardReferrerConfig.Count)
                {
                    int count = controller.InnerListAwardReferrerConfig.Count;
                    for (int i = count; i < level; i++)
                    {
                        controller.InnerListAwardReferrerConfig.Add(
                            AwardReferrerConfigUIModel.CreateFromDBObject(new MetaData.SystemConfig.AwardReferrerConfig()
                            {
                                ReferLevel = i + 1
                            }));
                    }
                }
            }));

        #endregion

        public void Init()
        {
            var trans = MyDBHelper.Instance.CreateTrans();
            try
            {
                //GlobalConfig.IncomeMoneyAccount = DBProvider.SystemDBProvider.GetIncomeMoneyAccountConfig();
                //if (GlobalConfig.IncomeMoneyAccount == null)
                //{
                //    //LogHelper.Instance.AddErrorLog("没有设置收款信息。", null);
                //}

                GlobalConfig.GameConfig = DBProvider.SystemDBProvider.GetGameConfig();
                if (GlobalConfig.GameConfig == null)
                {
                    GlobalConfig.GameConfig = new GameConfig();
                    DBProvider.SystemDBProvider.SaveGameConfig(GlobalConfig.GameConfig, trans);
                }

                GlobalConfig.RegisterPlayerConfig = DBProvider.SystemDBProvider.GetRegisterUserConfig();
                if (GlobalConfig.RegisterPlayerConfig == null)
                {
                    GlobalConfig.RegisterPlayerConfig = new RegisterUserConfig();
                    DBProvider.SystemDBProvider.SaveRegisterUserConfig(GlobalConfig.RegisterPlayerConfig, trans);
                }

                GlobalConfig.AwardReferrerLevelConfig = new AwardReferrerLevelConfig();
                GlobalConfig.AwardReferrerLevelConfig.SetListAward(DBProvider.SystemDBProvider.GetAwardReferrerConfig());
                if (GlobalConfig.AwardReferrerLevelConfig.AwardLevelCount == 0)
                {
                    var config = new AwardReferrerConfig();
                    List<AwardReferrerConfig> listConfig = new List<AwardReferrerConfig>();
                    listConfig.Add(config);
                    DBProvider.SystemDBProvider.SaveAwardReferrerConfig(listConfig, trans);
                    GlobalConfig.AwardReferrerLevelConfig.SetListAward(listConfig);
                }
                GlobalConfig.RouletteConfig = DBProvider.SystemDBProvider.GetRouletteConfig();
                trans.Commit();
            }
            catch (Exception exc)
            {
                trans.Rollback();
                throw exc;
            }
            finally
            {
                trans.Dispose();
            }
        }

        public void GetSystemConfig()
        {
            try
            {
                this.InnerListAwardReferrerConfig.Clear();
                for (int i = 1; i <= GlobalConfig.AwardReferrerLevelConfig.AwardLevelCount; i++)
                {
                    var awardConfig = GlobalConfig.AwardReferrerLevelConfig.GetAwardByLevel(i);
                    if (awardConfig != null)
                    {
                        this.InnerListAwardReferrerConfig.Add(AwardReferrerConfigUIModel.CreateFromDBObject(awardConfig));
                    }
                }
                this.AwardLevelFrom0 = GlobalConfig.AwardReferrerLevelConfig.AwardLevelCount - 1;
                this.InnerGameConfig = GameConfigUIModel.CreateFromDBObject(GlobalConfig.GameConfig);
                this.InnerRegisterPlayerConfig = RegisterUserConfigUIModel.CreateFromDBObject(GlobalConfig.RegisterPlayerConfig);

            }
            catch (Exception exc)
            {
                LogHelper.Instance.AddErrorLog("GameSystemConfigController Get System Config failed", exc);
            }
        }

        public void SaveSystemConfig()
        {
            var trans = MyDBHelper.Instance.CreateTrans();
            try
            {
                List<AwardReferrerConfig> listBaseAwardConfig = new List<AwardReferrerConfig>();
                foreach (var awardUIConfig in this.InnerListAwardReferrerConfig)
                {
                    listBaseAwardConfig.Add(awardUIConfig.ToDBObject());
                }

                bool isOK = DBProvider.SystemDBProvider.SaveAwardReferrerConfig(listBaseAwardConfig, trans);
                if (this.InnerGameConfig.IsChanged)
                {
                    isOK = DBProvider.SystemDBProvider.SaveGameConfig(this.InnerGameConfig.ToDBObject(), trans);
                    this.InnerGameConfig.IsChanged = false;
                }
                //if (this.InnerIncomeMoneyAccount.IsChanged)
                //{
                //    isOK = DBProvider.SystemDBProvider.SaveIncomeMoneyAccountConfig(this.InnerIncomeMoneyAccount.ToDBObject());
                //    this.InnerIncomeMoneyAccount.IsChanged = false;
                //}
                if (this.InnerRegisterPlayerConfig.IsChanged)
                {
                    isOK = DBProvider.SystemDBProvider.SaveRegisterUserConfig(this.InnerRegisterPlayerConfig.ToDBObject(), trans);
                    this.InnerRegisterPlayerConfig.IsChanged = false;
                }

                trans.Commit();

                if (GameConfigChanged != null)
                {
                    GameConfigChanged();
                }

                GlobalConfig.AwardReferrerLevelConfig.SetListAward(listBaseAwardConfig);
                GlobalConfig.GameConfig = this.InnerGameConfig.ToDBObject();
                //GlobalConfig.IncomeMoneyAccount = this.InnerIncomeMoneyAccount.ToDBObject();
                GlobalConfig.RegisterPlayerConfig = this.InnerRegisterPlayerConfig.ToDBObject();
            }
            catch (Exception exc)
            {
                trans.Rollback();
                Init();
                GetSystemConfig();
                throw exc;
            }
            finally
            {
                trans.Dispose();
            }
        }
    }
}
