using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SuperMinersWPF.Wcf.Channel
{
    public class MyWebRequest
    {
        private WebRequest _req;

        public MyWebRequest(WebRequest req) 
        {
            this._req = req;
            this.IsCancel = false;
        }

        public WebRequest Request
        {
            get
            {
                return this._req;
            }
        }

        public bool IsCancel
        {
            get;
            private set;
        }

        public void Cancel()
        {
            this._req.Abort();
            this.IsCancel = true;
        }
    }
}
