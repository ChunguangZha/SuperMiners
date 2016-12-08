using SuperMinersWeiXin.Model;
using SuperMinersWeiXin.Wcf.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SuperMinersWeiXin.Controller
{
    [DataObject]
    public class SellStoneOrderController
    {
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static SellStoneOrderUIModel[] GetNotFinishedSellorders(string userName)
        {
            if (!WcfClient.IsReady)
            {
                return null;
            }

            var ordersList = WcfClient.Instance.GetAllNotFinishedSellOrders(userName);
            if (ordersList == null)
            {
                return null;
            }

            DateTime time = DateTime.Now;
            SellStoneOrderUIModel[] uiOrdersList = new SellStoneOrderUIModel[ordersList.Length];
            for (int i = 0; i < ordersList.Length; i++)
            {
                ordersList[i].SellerUserName += time.ToString("HHmmss");
                uiOrdersList[i] = new SellStoneOrderUIModel(ordersList[i]);
            }

            return uiOrdersList;
        }

    }
}