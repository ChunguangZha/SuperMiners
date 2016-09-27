using SuperMinersCustomServiceSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersCustomServiceSystem.ViewModel
{
    public class GameRouletteViewModel : BaseViewModel
    {
        public override string MenuHeader
        {
            get
            {
                return "幸运大转盘";
            }
        }

        private ObservableCollection<RouletteAwardItemUIModel> _listRouletteAwardItems = new ObservableCollection<RouletteAwardItemUIModel>();

        public ObservableCollection<RouletteAwardItemUIModel> ListRouletteAwardItems
        {
            get { return _listRouletteAwardItems; }
            set { _listRouletteAwardItems = value; }
        }

        private ObservableCollection<RouletteWinnerRecordUIModel> _listNotPayRouletteWinnerRecords = new ObservableCollection<RouletteWinnerRecordUIModel>();

        public ObservableCollection<RouletteWinnerRecordUIModel> ListNotPayRouletteWinnerRecords
        {
            get { return _listNotPayRouletteWinnerRecords; }
            set { _listNotPayRouletteWinnerRecords = value; }
        }

    }
}
