using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype.Model.DTOs.Response
{
    public class BaseResponse<T>
    {
        public T Response { get; set; }
        public bool Success { get; set; } = false;
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public int TotalCount { get; set; } = 0;
    }
}
