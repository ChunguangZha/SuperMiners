using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using XunLinMineRemoteControlWeb.Core;

namespace XunLinMineRemoteControlWeb
{
    public partial class ShoppingList : System.Web.UI.Page
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
            if (list == null || list.Count == 0)
            {
                list = new List<ShopItem>();
                list.Add(new ShopItem()
                {
                    Name = "远程协助服务一次",
                    ImgPath = "~/images/stones.jpg",
                    Price = 50,
                    Href = "Shopping/MinerTrade.aspx"
                });
                list.Add(new ShopItem()
                {
                    Name = "远程协助服务一月",
                    ImgPath = "~/images/stones.jpg",
                    Price = 300,
                    Href = "Shopping/MineTrade.aspx"
                });

                list.Add(new ShopItem()
                {
                    Name = "远程协助服务半年",
                    ImgPath = "~/images/stones.jpg",
                    Price = 2000,
                    Href = "Shopping/StoneTrade.aspx"
                });

                list.Add(new ShopItem()
                {
                    Name = "远程协助服务一年",
                    ImgPath = "~/images/stones.jpg",
                    Price = 5000,
                    Href = "Shopping/StoneTrade.aspx"
                });
            }
        }
    }
}