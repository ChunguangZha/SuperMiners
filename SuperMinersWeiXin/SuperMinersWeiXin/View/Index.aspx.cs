using SuperMinersWeiXin.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SuperMinersWeiXin
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string encode = WeiXinHandler.CreateGetCodeUrl();
            //this.txtUserName.Text = encode;
            if (!this.IsPostBack)
            {
                if (MainController.Player != null)
                {
                    this.txtUserName.Text = MainController.Player.SimpleInfo.UserName;
                    this.txtExp.Text = MainController.Player.FortuneInfo.Exp.ToString("0.00");
                    this.txtGoldCoin.Text = MainController.Player.FortuneInfo.GoldCoin.ToString("0.00");
                    this.txtMiners.Text = MainController.Player.FortuneInfo.MinersCount.ToString("f2");
                    this.txtRMB.Text = MainController.Player.FortuneInfo.RMB.ToString("f2");
                    this.txtStones.Text = MainController.Player.FortuneInfo.StockOfStones.ToString("f2");
                    this.txtTempOutputStones.Text = MainController.Player.FortuneInfo.TempOutputStones.ToString("f2");
                    this.txtWorkStonesReservers.Text = (MainController.Player.FortuneInfo.StonesReserves - MainController.Player.FortuneInfo.FreezingStones).ToString("f2");

                }
            }
        }
    }
}