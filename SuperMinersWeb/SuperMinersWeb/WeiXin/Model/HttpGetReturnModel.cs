using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperMinersWeb.WeiXin.Model
{
    public class HttpGetReturnModel
    {
        public ErrorModel ResponseError { get; set; }

        public object ResponseResult { get; set; }

        public Exception Exception { get; set; }
    }
}