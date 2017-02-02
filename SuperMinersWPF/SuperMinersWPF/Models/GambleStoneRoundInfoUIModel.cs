using MetaData.Game.GambleStone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SuperMinersWPF.Models
{
    public class GambleStoneRoundInfoUIModel : BaseModel
    {
        public GambleStoneRoundInfoUIModel(GambleStoneRoundInfo parent)
        {
            this.ParentObject = parent;
        }

        private GambleStoneRoundInfo _parentObject;

        public GambleStoneRoundInfo ParentObject
        {
            get { return _parentObject; }
            set
            {
                _parentObject = value;
                RefreshUI();
            }
        }

        public void RefreshUI()
        {
            NotifyPropertyChange("ID");
            NotifyPropertyChange("StartTimeText");
            NotifyPropertyChange("FinishedInningCount");
            NotifyPropertyChange("EndTimeText");
            NotifyPropertyChange("CurrentWinRedCount");
            NotifyPropertyChange("CurrentWinGreenCount");
            NotifyPropertyChange("CurrentWinBlueCount");
            NotifyPropertyChange("CurrentWinPurpleCount");
            NotifyPropertyChange("LastWinRedCount");
            NotifyPropertyChange("LastWinGreenCount");
            NotifyPropertyChange("LastWinBlueCount");
            NotifyPropertyChange("LastWinPurpleCount");
            NotifyPropertyChange("AllBetInStone");
            NotifyPropertyChange("AllWinnedOutStone");

            NotifyPropertyChange("WinnedInning1ColorImg1");
            NotifyPropertyChange("WinnedInning1ColorImg2");
            NotifyPropertyChange("WinnedInning1ColorImg3");
            NotifyPropertyChange("WinnedInning1ColorImg4");
            NotifyPropertyChange("WinnedInning1ColorImg5");
            NotifyPropertyChange("WinnedInning1ColorImg6");
            NotifyPropertyChange("WinnedInning1ColorImg7");
            NotifyPropertyChange("WinnedInning1ColorImg8");
            NotifyPropertyChange("WinnedInning1ColorImg9");
            NotifyPropertyChange("WinnedInning1ColorImg10");
            NotifyPropertyChange("WinnedInning1ColorImg11");
            NotifyPropertyChange("WinnedInning1ColorImg12");
            NotifyPropertyChange("WinnedInning1ColorImg13");
            NotifyPropertyChange("WinnedInning1ColorImg14");
            NotifyPropertyChange("WinnedInning1ColorImg15");
            NotifyPropertyChange("WinnedInning1ColorImg16");
            NotifyPropertyChange("WinnedInning1ColorImg17");
            NotifyPropertyChange("WinnedInning1ColorImg18");
            NotifyPropertyChange("WinnedInning1ColorImg19");
            NotifyPropertyChange("WinnedInning1ColorImg20");
            NotifyPropertyChange("WinnedInning1ColorImg21");
            NotifyPropertyChange("WinnedInning1ColorImg22");
            NotifyPropertyChange("WinnedInning1ColorImg23");
            NotifyPropertyChange("WinnedInning1ColorImg24");
            NotifyPropertyChange("WinnedInning1ColorImg25");
            NotifyPropertyChange("WinnedInning1ColorImg26");
            NotifyPropertyChange("WinnedInning1ColorImg27");
            NotifyPropertyChange("WinnedInning1ColorImg28");
            NotifyPropertyChange("WinnedInning1ColorImg29");
            NotifyPropertyChange("WinnedInning1ColorImg30");
            NotifyPropertyChange("WinnedInning1ColorImg31");
            NotifyPropertyChange("WinnedInning1ColorImg32");
            NotifyPropertyChange("WinnedInning1ColorImg33");
            NotifyPropertyChange("WinnedInning1ColorImg34");
            NotifyPropertyChange("WinnedInning1ColorImg35");
            NotifyPropertyChange("WinnedInning1ColorImg36");
            NotifyPropertyChange("WinnedInning1ColorImg37");
            NotifyPropertyChange("WinnedInning1ColorImg38");
            NotifyPropertyChange("WinnedInning1ColorImg39");
            NotifyPropertyChange("WinnedInning1ColorImg40");
            NotifyPropertyChange("WinnedInning1ColorImg41");
            NotifyPropertyChange("WinnedInning1ColorImg42");
            NotifyPropertyChange("WinnedInning1ColorImg43");
            NotifyPropertyChange("WinnedInning1ColorImg44");
            NotifyPropertyChange("WinnedInning1ColorImg45");
            NotifyPropertyChange("WinnedInning1ColorImg46");
            NotifyPropertyChange("WinnedInning1ColorImg47");
            NotifyPropertyChange("WinnedInning1ColorImg48");
            NotifyPropertyChange("WinnedInning1ColorImg49");
            NotifyPropertyChange("WinnedInning1ColorImg50");
            NotifyPropertyChange("WinnedInning1ColorImg51");
            NotifyPropertyChange("WinnedInning1ColorImg52");
            NotifyPropertyChange("WinnedInning1ColorImg53");
            NotifyPropertyChange("WinnedInning1ColorImg54");
            NotifyPropertyChange("WinnedInning1ColorImg55");
            NotifyPropertyChange("WinnedInning1ColorImg56");
            NotifyPropertyChange("WinnedInning1ColorImg57");
            NotifyPropertyChange("WinnedInning1ColorImg58");
            NotifyPropertyChange("WinnedInning1ColorImg59");
            NotifyPropertyChange("WinnedInning1ColorImg60");
            NotifyPropertyChange("WinnedInning1ColorImg61");
            NotifyPropertyChange("WinnedInning1ColorImg62");
            NotifyPropertyChange("WinnedInning1ColorImg63");
            NotifyPropertyChange("WinnedInning1ColorImg64");
        }

        public int ID
        {
            get
            {
                return this._parentObject.ID;
            }
        }

        public string StartTimeText
        {
            get
            {
                if (this._parentObject.StartTime == null)
                {
                    return "";
                }
                return this._parentObject.StartTime.ToDateTime().ToString();
            }
        }

        public int FinishedInningCount
        {
            get
            {
                return this._parentObject.FinishedInningCount;
            }
        }

        public string EndTimeText
        {
            get
            {
                if (this._parentObject.EndTime == null)
                {
                    return "";
                }
                return this._parentObject.EndTime.ToDateTime().ToString();
            }
        }

        public int CurrentWinRedCount
        {
            get
            {
                return this._parentObject.CurrentWinRedCount;
            }
        }

        public int CurrentWinGreenCount
        {
            get
            {
                return this._parentObject.CurrentWinGreenCount;
            }
        }

        public int CurrentWinBlueCount
        {
            get
            {
                return this._parentObject.CurrentWinBlueCount;
            }
        }

        public int CurrentWinPurpleCount
        {
            get
            {
                return this._parentObject.CurrentWinPurpleCount;
            }
        }

        public int LastWinRedCount
        {
            get
            {
                return this._parentObject.LastWinRedCount;
            }
        }

        public int LastWinGreenCount
        {
            get
            {
                return this._parentObject.LastWinGreenCount;
            }
        }

        public int LastWinBlueCount
        {
            get
            {
                return this._parentObject.LastWinBlueCount;
            }
        }

        public int LastWinPurpleCount
        {
            get
            {
                return this._parentObject.LastWinPurpleCount;
            }
        }

        public int AllBetInStone
        {
            get
            {
                return this._parentObject.AllBetInStone;
            }
        }

        public int AllWinnedOutStone
        {
            get
            {
                return this._parentObject.AllWinnedOutStone;
            }
        }

        private static BitmapImage ImgRed = new BitmapImage(new Uri(@"Resources/gamblered.png", UriKind.Relative));
        private static BitmapImage imgGreen = new BitmapImage(new Uri(@"Resources/gamblegreen.png", UriKind.Relative));
        private static BitmapImage imgBlue = new BitmapImage(new Uri(@"Resources/gambleblue.png", UriKind.Relative));
        private static BitmapImage imgPurple = new BitmapImage(new Uri(@"Resources/gambleyellow.png", UriKind.Relative));

        private BitmapSource GetWinnedInningColorImg(int index)
        {
            if (this._parentObject.WinColorItems == null || this._parentObject.WinColorItems.Length != 64)
            {
                return null;
            }

            if (this._parentObject.WinColorItems[index] == 0)
            {
                return null;
            }
            switch ((GambleStoneItemColor)this._parentObject.WinColorItems[index])
            {
                case GambleStoneItemColor.Red:
                    return ImgRed;
                case GambleStoneItemColor.Green:
                    return imgGreen;
                case GambleStoneItemColor.Blue:
                    return imgBlue;
                case GambleStoneItemColor.Purple:
                    return imgPurple;
                default:
                    break;
            }

            return null;
        }

        public BitmapSource WinnedInning1ColorImg1
        {
            get
            {
                return GetWinnedInningColorImg(0);
            }
        }

        public BitmapSource WinnedInning1ColorImg2
        {
            get
            {
                return GetWinnedInningColorImg(1);
            }
        }

        public BitmapSource WinnedInning1ColorImg3
        {
            get
            {
                return GetWinnedInningColorImg(2);
            }
        }

        public BitmapSource WinnedInning1ColorImg4
        {
            get
            {
                return GetWinnedInningColorImg(3);
            }
        }

        public BitmapSource WinnedInning1ColorImg5
        {
            get
            {
                return GetWinnedInningColorImg(4);
            }
        }

        public BitmapSource WinnedInning1ColorImg6
        {
            get
            {
                return GetWinnedInningColorImg(5);
            }
        }

        public BitmapSource WinnedInning1ColorImg7
        {
            get
            {
                return GetWinnedInningColorImg(6);
            }
        }

        public BitmapSource WinnedInning1ColorImg8
        {
            get
            {
                return GetWinnedInningColorImg(7);
            }
        }

        public BitmapSource WinnedInning1ColorImg9
        {
            get
            {
                return GetWinnedInningColorImg(8);
            }
        }

        public BitmapSource WinnedInning1ColorImg10
        {
            get
            {
                return GetWinnedInningColorImg(9);
            }
        }

        public BitmapSource WinnedInning1ColorImg11
        {
            get
            {
                return GetWinnedInningColorImg(10);
            }
        }

        public BitmapSource WinnedInning1ColorImg12
        {
            get
            {
                return GetWinnedInningColorImg(11);
            }
        }

        public BitmapSource WinnedInning1ColorImg13
        {
            get
            {
                return GetWinnedInningColorImg(12);
            }
        }

        public BitmapSource WinnedInning1ColorImg14
        {
            get
            {
                return GetWinnedInningColorImg(13);
            }
        }

        public BitmapSource WinnedInning1ColorImg15
        {
            get
            {
                return GetWinnedInningColorImg(14);
            }
        }

        public BitmapSource WinnedInning1ColorImg16
        {
            get
            {
                return GetWinnedInningColorImg(15);
            }
        }

        public BitmapSource WinnedInning1ColorImg17
        {
            get
            {
                return GetWinnedInningColorImg(16);
            }
        }

        public BitmapSource WinnedInning1ColorImg18
        {
            get
            {
                return GetWinnedInningColorImg(17);
            }
        }

        public BitmapSource WinnedInning1ColorImg19
        {
            get
            {
                return GetWinnedInningColorImg(18);
            }
        }

        public BitmapSource WinnedInning1ColorImg20
        {
            get
            {
                return GetWinnedInningColorImg(19);
            }
        }

        public BitmapSource WinnedInning1ColorImg21
        {
            get
            {
                return GetWinnedInningColorImg(20);
            }
        }

        public BitmapSource WinnedInning1ColorImg22
        {
            get
            {
                return GetWinnedInningColorImg(21);
            }
        }

        public BitmapSource WinnedInning1ColorImg23
        {
            get
            {
                return GetWinnedInningColorImg(22);
            }
        }

        public BitmapSource WinnedInning1ColorImg24
        {
            get
            {
                return GetWinnedInningColorImg(23);
            }
        }

        public BitmapSource WinnedInning1ColorImg25
        {
            get
            {
                return GetWinnedInningColorImg(24);
            }
        }

        public BitmapSource WinnedInning1ColorImg26
        {
            get
            {
                return GetWinnedInningColorImg(25);
            }
        }

        public BitmapSource WinnedInning1ColorImg27
        {
            get
            {
                return GetWinnedInningColorImg(26);
            }
        }

        public BitmapSource WinnedInning1ColorImg28
        {
            get
            {
                return GetWinnedInningColorImg(27);
            }
        }

        public BitmapSource WinnedInning1ColorImg29
        {
            get
            {
                return GetWinnedInningColorImg(28);
            }
        }

        public BitmapSource WinnedInning1ColorImg30
        {
            get
            {
                return GetWinnedInningColorImg(29);
            }
        }

        public BitmapSource WinnedInning1ColorImg31
        {
            get
            {
                return GetWinnedInningColorImg(30);
            }
        }

        public BitmapSource WinnedInning1ColorImg32
        {
            get
            {
                return GetWinnedInningColorImg(31);
            }
        }

        public BitmapSource WinnedInning1ColorImg33
        {
            get
            {
                return GetWinnedInningColorImg(32);
            }
        }

        public BitmapSource WinnedInning1ColorImg34
        {
            get
            {
                return GetWinnedInningColorImg(33);
            }
        }

        public BitmapSource WinnedInning1ColorImg35
        {
            get
            {
                return GetWinnedInningColorImg(34);
            }
        }

        public BitmapSource WinnedInning1ColorImg36
        {
            get
            {
                return GetWinnedInningColorImg(35);
            }
        }

        public BitmapSource WinnedInning1ColorImg37
        {
            get
            {
                return GetWinnedInningColorImg(36);
            }
        }

        public BitmapSource WinnedInning1ColorImg38
        {
            get
            {
                return GetWinnedInningColorImg(37);
            }
        }

        public BitmapSource WinnedInning1ColorImg39
        {
            get
            {
                return GetWinnedInningColorImg(38);
            }
        }

        public BitmapSource WinnedInning1ColorImg40
        {
            get
            {
                return GetWinnedInningColorImg(39);
            }
        }

        public BitmapSource WinnedInning1ColorImg41
        {
            get
            {
                return GetWinnedInningColorImg(40);
            }
        }

        public BitmapSource WinnedInning1ColorImg42
        {
            get
            {
                return GetWinnedInningColorImg(41);
            }
        }

        public BitmapSource WinnedInning1ColorImg43
        {
            get
            {
                return GetWinnedInningColorImg(42);
            }
        }

        public BitmapSource WinnedInning1ColorImg44
        {
            get
            {
                return GetWinnedInningColorImg(43);
            }
        }

        public BitmapSource WinnedInning1ColorImg45
        {
            get
            {
                return GetWinnedInningColorImg(44);
            }
        }

        public BitmapSource WinnedInning1ColorImg46
        {
            get
            {
                return GetWinnedInningColorImg(45);
            }
        }

        public BitmapSource WinnedInning1ColorImg47
        {
            get
            {
                return GetWinnedInningColorImg(46);
            }
        }

        public BitmapSource WinnedInning1ColorImg48
        {
            get
            {
                return GetWinnedInningColorImg(47);
            }
        }

        public BitmapSource WinnedInning1ColorImg49
        {
            get
            {
                return GetWinnedInningColorImg(48);
            }
        }

        public BitmapSource WinnedInning1ColorImg50
        {
            get
            {
                return GetWinnedInningColorImg(49);
            }
        }

        public BitmapSource WinnedInning1ColorImg51
        {
            get
            {
                return GetWinnedInningColorImg(50);
            }
        }

        public BitmapSource WinnedInning1ColorImg52
        {
            get
            {
                return GetWinnedInningColorImg(51);
            }
        }

        public BitmapSource WinnedInning1ColorImg53
        {
            get
            {
                return GetWinnedInningColorImg(52);
            }
        }

        public BitmapSource WinnedInning1ColorImg54
        {
            get
            {
                return GetWinnedInningColorImg(53);
            }
        }

        public BitmapSource WinnedInning1ColorImg55
        {
            get
            {
                return GetWinnedInningColorImg(54);
            }
        }

        public BitmapSource WinnedInning1ColorImg56
        {
            get
            {
                return GetWinnedInningColorImg(55);
            }
        }

        public BitmapSource WinnedInning1ColorImg57
        {
            get
            {
                return GetWinnedInningColorImg(56);
            }
        }

        public BitmapSource WinnedInning1ColorImg58
        {
            get
            {
                return GetWinnedInningColorImg(57);
            }
        }

        public BitmapSource WinnedInning1ColorImg59
        {
            get
            {
                return GetWinnedInningColorImg(58);
            }
        }

        public BitmapSource WinnedInning1ColorImg60
        {
            get
            {
                return GetWinnedInningColorImg(59);
            }
        }

        public BitmapSource WinnedInning1ColorImg61
        {
            get
            {
                return GetWinnedInningColorImg(60);
            }
        }

        public BitmapSource WinnedInning1ColorImg62
        {
            get
            {
                return GetWinnedInningColorImg(61);
            }
        }

        public BitmapSource WinnedInning1ColorImg63
        {
            get
            {
                return GetWinnedInningColorImg(62);
            }
        }

        public BitmapSource WinnedInning1ColorImg64
        {
            get
            {
                return GetWinnedInningColorImg(63);
            }
        }

    }
}
