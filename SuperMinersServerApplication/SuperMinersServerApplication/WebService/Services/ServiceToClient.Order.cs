using SuperMinersServerApplication.Controller;
using SuperMinersServerApplication.Encoder;
using SuperMinersServerApplication.WebService.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.WebService.Services
{
    public partial class ServiceToClient : IServiceToClient
    {
        #region IServiceToClient Members
        
        public bool SellStone(string token, string userName, int sellStonesCount)
        {
            if (RSAProvider.LoadRSA(token))
            {
                if (ClientManager.GetClientUserName(token) != userName)
                {
                    return false;
                }

                return OrderController.Instance.CreateSellOrder(userName, sellStonesCount);
            }
            else
            {
                throw new Exception();
            }
        }

        public MetaData.SellStonesOrder[] GetNotFinishedStonesOrder(string token)
        {
            if (RSAProvider.LoadRSA(token))
            {
                return OrderController.Instance.GetSellOrders();
            }
            else
            {
                throw new Exception();
            }
        }

        #endregion

    }
}
