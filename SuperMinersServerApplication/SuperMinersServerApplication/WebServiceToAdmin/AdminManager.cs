using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.WebServiceToAdmin
{
    public class AdminLoginnedInfo
    {
        public string UserName
        {
            get;
            set;
        }

        public string ActionPassword
        {
            get;
            set;
        }

        public string IP
        {
            get;
            set;
        }

        public DateTime TimeLoggedIn
        {
            get;
            set;
        }

        public string Token
        {
            get;
            set;
        }
    }

    class AdminManager
    {
        private static Dictionary<string, DateTime> _lastUpdateTimeDic = new Dictionary<string, DateTime>();
        private static Dictionary<string, AdminLoginnedInfo> _infoDic = new Dictionary<string, AdminLoginnedInfo>();
        private static readonly object _locker = new object();

        public static void AddClient(string userName, string actionPassword, string token)
        {
            string ip = null;
            try
            {
                MessageProperties messageProperties = OperationContext.Current.IncomingMessageProperties;
                RemoteEndpointMessageProperty endpointProperty = messageProperties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                ip = endpointProperty.Address + ":" + endpointProperty.Port;
            }
            catch
            {
                ip = "N/A";
            }

            lock (_locker)
            {
                _infoDic[token] = new AdminLoginnedInfo()
                {
                    UserName = userName,
                    ActionPassword = actionPassword,
                    Token = token,
                    IP = ip,
                    TimeLoggedIn = DateTime.Now
                };

                _lastUpdateTimeDic[token] = DateTime.Now;
            }
        }

        public static void RemoveClient(string token)
        {
            lock (_locker)
            {
                _infoDic.Remove(token);
                _lastUpdateTimeDic.Remove(token);
            }
        }

        public static string[] GetInvalidClients()
        {
            lock (_locker)
            {
                return _lastUpdateTimeDic.Where(data => (DateTime.Now - data.Value).TotalSeconds > GlobalData.TimeoutSeconds).Select(data => data.Key).ToArray();
            }
        }

        public static bool IsExistUserName(string userName)
        {
            lock (_locker)
            {
                foreach (var info in _infoDic.Values)
                {
                    if (info.UserName.Equals(userName))
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public static string GetToken(string userName)
        {
            lock (_locker)
            {
                foreach (var info in _infoDic.Values)
                {
                    if (info.UserName.Equals(userName))
                    {
                        return info.Token;
                    }
                }

                return null;
            }
        }

        public static string GetClientIP(string token)
        {
            lock (_locker)
            {
                AdminLoginnedInfo info;
                if (_infoDic.TryGetValue(token, out info))
                {
                    return info.IP;
                }
            }

            return token;
        }

        public static string GetClientUserName(string token)
        {
            lock (_locker)
            {
                AdminLoginnedInfo info;
                if (_infoDic.TryGetValue(token, out info))
                {
                    return info.UserName;
                }
            }

            return null;
        }

        public static AdminLoginnedInfo GetClient(string token)
        {
            lock (_locker)
            {
                AdminLoginnedInfo info;
                if (_infoDic.TryGetValue(token, out info))
                {
                    return info;
                }
            }

            return null;
        }
    }
}
