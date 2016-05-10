using SuperMinersServerApplication.WebServiceToAdmin.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.WebServiceToAdmin.Services
{
    public class ServiceToAdmin : IServiceToAdmin
    {
        public void DeletePlayers(string token, string[] userIDs)
        {
            throw new NotImplementedException();
        }

        public Model.UserInfo[] GetUsers(string token)
        {
            throw new NotImplementedException();
        }

        public bool LogOutUsers(string token, Model.UserInfo[] users)
        {
            throw new NotImplementedException();
        }
    }
}
