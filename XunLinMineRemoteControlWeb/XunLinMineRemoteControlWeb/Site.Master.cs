using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XunLinMineRemoteControlWeb
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.IsAuthenticated)
                {
                    FormsIdentity identity = Context.User.Identity as FormsIdentity;
                    //identity.Ticket.UserData;
                }
            }
        }

        public string GetValidTime()
        {
            return DateTime.Now.AddDays(10).ToString();
        }
    }
}