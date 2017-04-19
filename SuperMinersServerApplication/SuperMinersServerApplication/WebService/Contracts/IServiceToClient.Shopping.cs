using MetaData;
using MetaData.Shopping;
using MetaData.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.WebService.Contracts
{
    public partial interface IServiceToClient
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/GetPlayerBuyVirtualShoppingItemRecord",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PlayerBuyVirtualShoppingItemRecord[] GetPlayerBuyVirtualShoppingItemRecord(string token, int itemID, MyDateTime beginBuyTime, MyDateTime endBuyTime, int pageItemCount, int pageIndex);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/GetVirtualShoppingItems",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        VirtualShoppingItem[] GetVirtualShoppingItems(string token);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/BuyVirtualShoppingItem",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        int BuyVirtualShoppingItem(string token, VirtualShoppingItem shoppingItem);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/GetDiamondShoppingItems",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        DiamondShoppingItem[] GetDiamondShoppingItems(string token);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/BuyDiamondShoppingItem",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        int BuyDiamondShoppingItem(string token, DiamondShoppingItem shoppingItem, PostAddress address);

        [OperationContract]
        [WebInvoke(UriTemplate = "/WebService/GetPlayerBuyDiamondShoppingItemRecord",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PlayerBuyDiamondShoppingItemRecord[] GetPlayerBuyDiamondShoppingItemRecord(string token, int itemID, int shoppingStateInt, MyDateTime beginBuyTime, MyDateTime endBuyTime, int pageItemCount, int pageIndex);

    }
}
