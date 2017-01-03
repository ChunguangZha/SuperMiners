using SuperMinersWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersWPF.ViewModels
{
    public class GameRaiderofLostArkViewModel
    {
        private RaiderRoundMetaDataInfoUIModel _currentRaiderRound = new RaiderRoundMetaDataInfoUIModel(new MetaData.Game.RaideroftheLostArk.RaiderRoundMetaDataInfo());

        public RaiderRoundMetaDataInfoUIModel CurrentRaiderRound
        {
            get
            {
                return this._currentRaiderRound;
            }
        }

        private ObservableCollection<PlayerBetInfoUIModel> _listSelfBetRecords = new ObservableCollection<PlayerBetInfoUIModel>();

        public ObservableCollection<PlayerBetInfoUIModel> ListSelfBetRecords
        {
            get { return _listSelfBetRecords; }
        }


        public void RegisterEvents()
        {
            GlobalData.Client.GetCurrentRaiderRoundInfoCompleted += Client_GetCurrentRaiderRoundInfoCompleted;
        }

        void Client_GetCurrentRaiderRoundInfoCompleted(object sender, Wcf.Clients.WebInvokeEventArgs<MetaData.Game.RaideroftheLostArk.RaiderRoundMetaDataInfo> e)
        {
            try
            {

            }
            catch (Exception exc)
            {

            }
        }

    }
}
