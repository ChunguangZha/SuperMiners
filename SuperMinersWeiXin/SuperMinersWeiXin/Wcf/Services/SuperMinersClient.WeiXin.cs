using MetaData;
using MetaData.User;
using SuperMinersServerApplication.WebServiceToWeb.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace SuperMinersWeiXin.Wcf.Services
{
    public partial class SuperMinersClient : ClientBase<IServiceToWeb>, IServiceToWeb
    {

        public string GetAccessToken()
        {
            try
            {
                return base.Channel.GetAccessToken();
            }
            catch (Exception)
            {
                return "";
            }
        }

        public int BindWeiXinUser(string wxUserOpenID, string wxUserName, string xlUserName, string xlUserPassword, string ip)
        {
            try
            {
                return base.Channel.BindWeiXinUser(wxUserOpenID, wxUserName, xlUserName, xlUserPassword, ip);
            }
            catch (Exception)
            {
                return OperResult.RESULTCODE_EXCEPTION;
            }
        }

        public int WeiXinLogin(string wxUserOpenID, string wxUserName, string ip)
        {
            try
            {
                return base.Channel.WeiXinLogin(wxUserOpenID, wxUserName, ip);
            }
            catch (Exception)
            {
                return OperResult.RESULTCODE_EXCEPTION;
            }
        }

        public PlayerInfo GetPlayerByWeiXinOpenID(string openid)
        {
            try
            {
                return base.Channel.GetPlayerByWeiXinOpenID(openid);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}