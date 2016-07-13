using SuperMinersCustomServiceSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersCustomServiceSystem.ViewModel
{
    class PlayerViewModel
    {
        private ObservableCollection<PlayerInfoUIModel> _listPlayers = new ObservableCollection<PlayerInfoUIModel>();

        public ObservableCollection<PlayerInfoUIModel> ListPlayers
        {
            get { return this._listPlayers; }
        }

        public void AsyncGetListPlayers()
        {
            GlobalData.Client.GetPlayers(
        }
    }
}
