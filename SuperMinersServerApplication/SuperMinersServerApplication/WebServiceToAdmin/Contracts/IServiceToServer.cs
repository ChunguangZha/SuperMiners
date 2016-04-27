using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.WebServiceToAdmin.Contracts
{
    [ServiceContract]
    interface IServiceToServer
    {
        [OperationContract]
        void DeleteUsers(string token, string[] userIDs);

        [OperationContract]
        Model.LoggedInUser[] GetLoggedInUsers(string token);

        [OperationContract]
        bool LogOutUsers(string token, Model.LoggedInUser[] users);
    }
}
