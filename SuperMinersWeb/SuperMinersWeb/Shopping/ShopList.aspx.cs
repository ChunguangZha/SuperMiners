using SuperMinersWeb.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SuperMinersWeb.Shopping
{
    public partial class ShopList : System.Web.UI.Page
    {
        private List<ShopItem> list = new List<ShopItem>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                CreateItems();

                this.listShop.DataSource = list;
                this.listShop.DataBind();
            }
        }

        private void CreateItems()
        {
            GlobalData.GameConfig = Wcf.WcfClient.Instance.GetGameConfig();
            if (GlobalData.GameConfig == null)
            {
                Response.Write("<script>alart('获取不到配置，无法打开商城');</script>");
                return;
            }
            if (list == null || list.Count == 0)
            {
                list = new List<ShopItem>();
                list.Add(new ShopItem()
                {
                    Name = "矿工",
                    ImgPath = "~/Images/miner.jpg",
                    Price = (float)Math.Round(GlobalData.GameConfig.GoldCoin_Miner / (GlobalData.GameConfig.RMB_GoldCoin * GlobalData.GameConfig.Yuan_RMB), 2),
                    Href = "MinerTrade.aspx"
                });
                list.Add(new ShopItem()
                {
                    Name = "矿山",
                    ImgPath = "~/Images/mine.jpg",
                    Price = (float)Math.Round(GlobalData.GameConfig.RMB_Mine / GlobalData.GameConfig.Yuan_RMB, 2),
                    Href = "MineTrade.aspx"
                });

                //矿石的价格已100块矿石为单位。
                list.Add(new ShopItem()
                {
                    Name = "矿石",
                    ImgPath = "~/Images/stones.jpg",
                    Price = (float)Math.Round((1 * 100) / GlobalData.GameConfig.Stones_RMB / GlobalData.GameConfig.Yuan_RMB, 2),
                    Href = "StoneTrade.aspx"
                });
            }
        }
    }
}