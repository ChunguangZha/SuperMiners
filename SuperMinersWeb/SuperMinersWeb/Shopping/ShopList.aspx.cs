using SuperMinersWeb.Data;
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
                
                //this.LinqDataSource1
            }
        }

        private void CreateItems()
        {
            list.Add(new ShopItem()
            {
                Name="矿工",
                ImgPath = "../Images/miner.jpg",
                Price = (float)Math.Round(GlobalData.GameConfig.GoldCoin_Miner / (GlobalData.GameConfig.RMB_GoldCoin * GlobalData.GameConfig.Yuan_RMB), 2)
            });
            list.Add(new ShopItem()
            {
                Name = "矿山",
                ImgPath = "../Images/mine.jpg",
                Price = (float)Math.Round(GlobalData.GameConfig.RMB_Mine / GlobalData.GameConfig.Yuan_RMB, 2)
            });
            list.Add(new ShopItem()
            {
                Name = "矿石",
                ImgPath = "../Images/stones.jpg",
                Price = (float)Math.Round(GlobalData.GameConfig.Stones_RMB / GlobalData.GameConfig.Yuan_RMB, 2)
            });
        }
    }
}