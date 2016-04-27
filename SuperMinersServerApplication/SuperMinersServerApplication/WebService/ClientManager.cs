using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.WebService
{
    #region ClientInfo
    public class ClientInfo
    {
        public string UserName
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
    #endregion

    public static class ClientManager
    {
        private static Dictionary<string, DateTime> _lastUpdateTimeDic = new Dictionary<string, DateTime>();
        private static Dictionary<string, ClientInfo> _infoDic = new Dictionary<string, ClientInfo>();
        private static readonly object _locker = new object();

        public static void AddClient(string userName, string token)
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
                _infoDic[token] = new ClientInfo()
                {
                    UserName = userName,
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
                //GlobalData.App.ClentClosed(GetIPEndPoint(token));
                _infoDic.Remove(token);
                _lastUpdateTimeDic.Remove(token);
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

        public static IPEndPoint GetIPEndPoint(string token)
        {
            lock (_locker)
            {
                string ip = GetClientIP(token);
                string ipaddress = ip.Remove(ip.LastIndexOf(":"));
                int port = 0;
                int.TryParse(ip.Remove(0, ip.LastIndexOf(":") + 1), out port);

                return new IPEndPoint(IPAddress.Parse(ipaddress), port);
            }
        }

        public static string[] GetInvalidClients()
        {
            lock (_locker)
            {
                return _lastUpdateTimeDic.Where(data => (DateTime.Now - data.Value).TotalSeconds > GlobalData.TimeoutSeconds).Select(data => data.Key).ToArray();
            }
        }

        public static void Active(string token)
        {
            lock (_locker)
            {
                if (_lastUpdateTimeDic.ContainsKey(token))
                {
                    _lastUpdateTimeDic[token] = DateTime.Now;
                }
            }
        }

        public static DateTime GetLastUpdateTime(string token)
        {
            lock (_locker)
            {
                DateTime info;
                if (_lastUpdateTimeDic.TryGetValue(token, out info))
                {
                    return info;
                }
            }

            return DateTime.MinValue;
        }

        public static string GetClientIP(string token)
        {
            lock (_locker)
            {
                ClientInfo info;
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
                ClientInfo info;
                if (_infoDic.TryGetValue(token, out info))
                {
                    return info.UserName;
                }
            }

            return null;
        }

        public static ClientInfo[] AllClients
        {
            get
            {
                lock (_locker)
                {
                    ClientInfo[] infos = new ClientInfo[_infoDic.Count];
                    _infoDic.Values.CopyTo(infos, 0);
                    return infos;
                }
            }
        }
    }
}
