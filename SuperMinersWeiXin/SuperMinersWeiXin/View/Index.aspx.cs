using MetaData.User;
using SuperMinersWeiXin.Controller;
using SuperMinersWeiXin.Model;
using SuperMinersWeiXin.Wcf.Services;
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
                if (!Request.IsAuthenticated)
                {
                    Response.Redirect("LoginPage.aspx");
                }
                string username = Context.User.Identity.Name;
                PlayerInfo player = WcfClient.Instance.GetPlayerByXLUserName(username);
                if (player != null)
                {
                    this.txtUserName.Text = player.SimpleInfo.UserName;
                    this.txtExp.Text = player.FortuneInfo.Exp.ToString("f0");
                    this.txtGoldCoin.Text = player.FortuneInfo.GoldCoin.ToString("f2");
                    this.txtMiners.Text = player.FortuneInfo.MinersCount.ToString("f2");
                    this.txtRMB.Text = player.FortuneInfo.RMB.ToString("f2");
                    this.txtStones.Text = (player.FortuneInfo.StockOfStones - player.FortuneInfo.FreezingStones).ToString("f2");
                    this.txtLastGatherTime.Text = player.FortuneInfo.TempOutputStonesStartTime.ToString();
                    this.txtWorkStonesReservers.Text = ((player.FortuneInfo.StonesReserves - player.FortuneInfo.TotalProducedStonesCount)).ToString("f2");

                }
                else
                {
                    Response.Redirect("../LoginPage.aspx");
                }
            }
        }
    }
}