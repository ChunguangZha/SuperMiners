using SuperMinersWeiXin.Wcf.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SuperMinersWeiXin.Handler
{
    /// <summary>
    /// Summary description for RefreshXLUserInfo
    /// </summary>
    public class RefreshXLUserInfo : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Cache.SetNoStore();
            var xlUserName = context.User.Identity.Name;
            if (string.IsNullOrEmpty(xlUserName))
            {
                context.Response.Write("");
                return;
            }
            var player = WcfClient.Instance.GetPlayerByXLUserName(xlUserName);
            if (player == null)
            {
                context.Response.Write("");
            }

//{ "name":"菜鸟教程" , "url":"www.runoob.com" }, 
//{ "name":"google" , "url":"www.google.com" }, 
//{ "name":"微博" , "url":"www.weibo.com" }

            StringBuilder builder = new StringBuilder();
            builder.Append("{");
            builder.Append("\"exp\":" + player.FortuneInfo.Exp.ToString("f2") + ",");
            builder.Append("\"goldcoin\":" + player.FortuneInfo.GoldCoin.ToString("f2") + ",");
            builder.Append("\"minerscount\":" + player.FortuneInfo.MinersCount.ToString("f2") + ",");
            builder.Append("\"rmb\":" + player.FortuneInfo.RMB.ToString("f2") + ",");
            builder.Append("\"stockofstones\":" + (player.FortuneInfo.StockOfStones - player.FortuneInfo.FreezingStones).ToString("f2") + ",");
            builder.Append("\"workstonesreservers\":" + (player.FortuneInfo.StonesReserves - player.FortuneInfo.TotalProducedStonesCount).ToString("f2") + ",");
            builder.Append("\"StoneSellQuan\":" + player.FortuneInfo.StoneSellQuan + ",");
            builder.Append("\"lastgathertime\":\"" + player.FortuneInfo.TempOutputStonesStartTime.ToString() + "\",");
            builder.Append("}");
            context.Response.Write(builder.ToString());
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}