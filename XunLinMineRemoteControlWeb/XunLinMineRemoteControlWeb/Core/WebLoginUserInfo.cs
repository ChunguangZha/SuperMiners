using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace XunLinMineRemoteControlWeb.Core
{
    public class WebLoginUserInfo : IPrincipal
    {
        public string UserLoginName;

        public string UserName;

        public string Token;

        public int ShoppingCredits;

        public string UserRemoteServerValidStopTimeText = "";
        
        #region IPrincipal Members

        public IIdentity Identity
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}