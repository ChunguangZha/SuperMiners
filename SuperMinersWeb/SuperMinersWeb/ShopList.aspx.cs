using SuperMinersWeb.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SuperMinersWeb
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
            if (GlobalData.GameConfig == null)
            {
                Response.Write("<script>alert('获取不到配置，无法打开商城');</script>");
                return;
            }
            if (list == null || list.Count == 0)
            {
                list = new List<ShopItem>();
                list.Add(new ShopItem()
                {
                    Name = "矿工",
                    ImgPath = "~/Images/miner.jpg",
                    Price = GlobalData.MinerPrice,
                    Href = "Shopping/MinerTrade.aspx"
                });
                list.Add(new ShopItem()
                {
                    Name = "矿山",
                    ImgPath = "~/Images/mine.jpg",
                    Price = GlobalData.MinePrice,
                    Href = "Shopping/MineTrade.aspx"
                });

                //矿石的价格已100块矿石为单位。
                list.Add(new ShopItem()
                {
                    Name = "矿石",
                    ImgPath = "~/Images/stones.jpg",
                    Price = GlobalData.StonePrice,
                    Href = "Shopping/StoneTrade.aspx"
                });
            }
        }
    }
}