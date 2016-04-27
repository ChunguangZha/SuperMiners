using SuperMinersServerApplication.WebServiceToAdmin.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.WebServiceToAdmin.Services
{
    public class ServiceToServer : IServiceToServer
    {
        public void DeleteUsers(string token, string[] userIDs)
        {
            throw new NotImplementedException();
        }

        public Model.LoggedInUser[] GetLoggedInUsers(string token)
        {
            throw new NotImplementedException();
        }

        public bool LogOutUsers(string token, Model.LoggedInUser[] users)
        {
            throw new NotImplementedException();
        }
    }
}
