using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reflex.Domain.Response
{
    public class BaseResponse
    {
        public string ResponseCode { get; set; }
        public string Message { get; set; }
        public bool Status{ get; set; }
        public object data { get; set; }
    }

    public class BaseResponse<T> where T : class
    {
        public string ResponseCode { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }
        public T data { get; set; }
    }
}
