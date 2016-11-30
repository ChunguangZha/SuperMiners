using MetaData.User;
using SuperMinersWeiXin.Controller;
using SuperMinersWeiXin.Model;
using SuperMinersWeiXin.WeiXinCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SuperMinersWeiXin.View
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string encode = WeiXinHandler.CreateGetCodeUrl();
            //this.txtUserName.Text = encode;
            if (!this.IsPostBack)
            {
                PlayerInfo player = Session[Config.SESSIONKEY_XLUSERINFO] as PlayerInfo;
                if (player != null)
                {
                    this.txtUserName.Text = player.SimpleInfo.UserName;
                    this.txtExp.Text = player.FortuneInfo.Exp.ToString("0.00");
                    this.txtGoldCoin.Text = player.FortuneInfo.GoldCoin.ToString("0.00");
                    this.txtMiners.Text = player.FortuneInfo.MinersCount.ToString("f2");
                    this.txtRMB.Text = player.FortuneInfo.RMB.ToString("f2");
                    this.txtStones.Text = player.FortuneInfo.StockOfStones.ToString("f2");
                    this.txtTempOutputStones.Text = player.FortuneInfo.TempOutputStones.ToString("f2");
                    this.txtWorkStonesReservers.Text = (player.FortuneInfo.StonesReserves - player.FortuneInfo.FreezingStones).ToString("f2");

                }
            }
        }
    }
}