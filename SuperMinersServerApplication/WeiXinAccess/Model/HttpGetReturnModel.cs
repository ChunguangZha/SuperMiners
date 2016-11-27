using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeiXinAccess.Model
{
    public class HttpGetReturnModel
    {
        public ErrorModel ResponseError { get; set; }

        public object ResponseResult { get; set; }

        public Exception Exception { get; set; }
    }
}
