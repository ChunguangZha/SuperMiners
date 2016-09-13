using SuperMinersCustomServiceSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersCustomServiceSystem.ViewModel
{
    public class StoneTradeViewModel : BaseViewModel
    {
        public override string MenuHeader
        {
            get
            {
                return "矿石交易";
            }
        }

        private ObservableCollection<SellStonesOrderUIModel> _listSellStoneOrderRecords = new ObservableCollection<SellStonesOrderUIModel>();

        public ObservableCollection<SellStonesOrderUIModel> ListSellStoneOrderRecords
        {
            get { return _listSellStoneOrderRecords; }
            set { _listSellStoneOrderRecords = value; }
        }


    }
}
