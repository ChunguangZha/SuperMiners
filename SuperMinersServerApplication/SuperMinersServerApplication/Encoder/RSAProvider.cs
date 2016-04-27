using SuperMinersServerApplication.WebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.Encoder
{
    internal static class RSAProvider
    {
        private static Dictionary<int, RSACryptoServiceProvider> _rsaDic = new Dictionary<int, RSACryptoServiceProvider>();
        private static Dictionary<string, RSACryptoServiceProvider> _tokenDic = new Dictionary<string, RSACryptoServiceProvider>();
        private static Dictionary<int, bool> _noRSADic = new Dictionary<int, bool>();
        private static readonly object _locker1 = new object();
        private static readonly object _locker2 = new object();
        private static readonly object _locker3 = new object();

        public static void SetRSA(string token, RSACryptoServiceProvider rsa)
        {
            lock (_locker1)
            {
                _tokenDic[token] = rsa;
            }
        }

        public static void RemoveRSA(string token)
        {
            lock (_locker1)
            {
                _tokenDic.Remove(token);
            }
        }

        public static bool NoRSA()
        {
            int code = OperationContext.Current.GetHashCode();
            lock (_locker3)
            {
                bool result = false;
                if (_noRSADic.TryGetValue(code, out result))
                {
                    _noRSADic.Remove(code);
                    return result;
                }
            }

            return false;
        }

        public static void LoadNoRSA()
        {
            lock (_locker3)
            {
                _noRSADic[OperationContext.Current.GetHashCode()] = true;
            }
        }

        public static RSACryptoServiceProvider UseRSA()
        {
            int code = OperationContext.Current.GetHashCode();
            lock (_locker2)
            {
                RSACryptoServiceProvider rsa = null;
                if (_rsaDic.TryGetValue(code, out rsa))
                {
                    _rsaDic.Remove(code);
                    return rsa;
                }
            }

            return null;
        }

        public static bool LoadRSA(string token)
        {
            if (String.IsNullOrEmpty(token))
            {
                return false;
            }
            lock (_locker1)
            {
                RSACryptoServiceProvider rsa = null;
                if (_tokenDic.TryGetValue(token, out rsa))
                {
                    if (null != rsa)
                    {
                        lock (_locker2)
                        {
                            _rsaDic[OperationContext.Current.GetHashCode()] = rsa;

                            ClientManager.Active(token);

                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}
