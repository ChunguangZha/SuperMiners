using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeiXinAccess.Model;

namespace WeiXinAccess
{
    public class TokenController
    {
        private object _objLockToken = new object();
        private AccessToken _weixin_AccessToken = null;
        private DateTime _lastGetTime = DateTime.MinValue;

        public string GetWeiXinAccessToken()
        {
            lock (_objLockToken)
            {
                if (_weixin_AccessToken == null)
                {
                    GetAccessToken();
                }
                else
                {
                    TimeSpan span = DateTime.Now - _lastGetTime;
                    if (span.TotalSeconds >= (_weixin_AccessToken.expires_in - 10))//为了安全起见，token到期前10秒就开始重新获取
                    {
                        GetAccessToken();
                    }
                }

                return _weixin_AccessToken == null ? "" : _weixin_AccessToken.access_token;
            }
        }

        private bool GetAccessToken()
        {
            try
            {
                this._weixin_AccessToken = null;

                string baseUrl = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential";
                string url = baseUrl + "&appid=" + Config.appid + "&secret=" + Config.appSecret;
                var result = HttpHandler.SyncGet<AccessToken>(url);
                if (result == null)
                {
                    Output("WeiXinAccess.GetAccessToken Server Response null.");
                    return false;
                }

                if (result.Exception != null)
                {
                    Output("WeiXinAccess.GetAccessToken Server Response Exception." + result.Exception.Message);
                    return false;
                }

                if (result.ResponseError != null)
                {
                    Output("WeiXinAccess.GetAccessToken Server Response Error. ErrorCode:" + result.ResponseError.errcode + "ErrorMsg:" + result.ResponseError.errmsg);
                    return false;
                }

                AccessToken token = result.ResponseResult as AccessToken;
                if (token == null)
                {
                    Output("WeiXinAccess.GetAccessToken Server ResultToken is null. ");
                    return false;
                }

                this._lastGetTime = DateTime.Now;
                this._weixin_AccessToken = token;
                return true;
            }
            catch (Exception exc)
            {
                Output("WeiXinAccess.GetAccessToken Exception. " + exc.Message);
                return false;
            }
        }

        private void Output(string message)
        {
            if (OutputError != null)
            {
                OutputError(message);
            }
        }

        public event Action<string> OutputError;
    }
}
